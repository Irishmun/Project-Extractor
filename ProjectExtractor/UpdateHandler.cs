using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectExtractor
{
    //Current Request making count: 2 (CheckProjectAccessible, SetRelease) GetDirectRateLimit as well but it isn't used
    internal class UpdateHandler
    {
        private const string PROJECT_OWNER = "Irishmun", THIS_PROJECT = "Project-Extractor";
        private const string LATEST_URL = "https://github.com/Irishmun/Project-Extractor/releases/latest";
        private IReadOnlyList<Release> _releases;
        private bool _gitProjectAvailable;

        private GitHubClient _client;
        public UpdateHandler()
        {
            _client = new GitHubClient(new ProductHeaderValue("Getting-updates"));
        }
        public async Task<bool> CheckProjectAccessible()
        {
            if (CanMakeRequests() == false)
            { return false; }
            Repository repo;
            try
            {
                repo = await _client.Repository.Get(PROJECT_OWNER, THIS_PROJECT);
                await SetRelease();
            }
            catch (Exception)
            {
                return false;
            }
            //var latest = repo.Result.ElementAt(0);
            //System.Diagnostics.Debug.WriteLine("Repo: "+repo.Name);
            _gitProjectAvailable = repo != null;
            return _gitProjectAvailable;
        }
        public async Task<bool> IsNewerVersionAvailable()
        {
            if (ReleaseAvailable == false)
            { return false; }
            Version ver = Assembly.GetExecutingAssembly().GetName().Version;
            string latestTag = GetLatestRelease();
            try
            {
                Match verNum = Regex.Match(latestTag, @"[\d|\.|\,]+");
                Version latest = Version.Parse(verNum.Value);
                if (ver.CompareTo(latest) < 0)
                {
                    return true;
                }
            }
            catch (Exception) { }
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Current version({ver}) is equal to, or newer than, latest version on Git({latestTag})");
#endif
            return false;
        }
        public async Task<IReadOnlyList<string>> GetAllReleases()
        {
            if (CanMakeRequests() == false)
            { return null; }
            IReadOnlyList<Release> releases = await _client.Repository.Release.GetAll(PROJECT_OWNER, THIS_PROJECT);
            List<string> versionTags = new List<string>();
            foreach (Release item in releases)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Release: " + item.TagName);
#endif
                versionTags.Add(item.TagName);
            }
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Latest: " + releases[0].TagName);
#endif
            return versionTags.AsReadOnly();
        }

        public string GetLatestRelease()
        {
            if (ReleaseAvailable == false)
            { return string.Empty; }
            Release release = LatestRelease;
#if DEBUG
            System.Diagnostics.Debug.WriteLine(release.AssetsUrl);
#endif
            return release.TagName;
        }
        public async Task DownloadAndInstallRelease(string TagName)
        {//TODO: make this actually download and install
            if (ReleaseAvailable == false)
            { return; }
            OpenReleasePage();//for now just open release page
            //if (CanMakeRequests() == false)
            //{ return; }
            //Release release = GetReleaseByTag(TagName);
            //System.Diagnostics.Debug.WriteLine("Latest: " + release?.Name);
        }
        public async Task<string> GetReleaseBodies()
        {
            if (ReleaseAvailable == false)
            { return string.Empty; }
            StringBuilder str = new StringBuilder();
            foreach (Release item in _releases)
            {
                //System.Diagnostics.Debug.WriteLine($"====={item.TagName}====={Environment.NewLine}{item.Body}");
                str.AppendLine($"====={item.TagName}====={Environment.NewLine}{item.Body}");
            }
            return str.ToString();
        }

        /// <summary>Opens the URL specified in <see cref="LATEST_URL"/> in the default browser</summary>
        public void OpenReleasePage()
        {
            System.Diagnostics.Process.Start("explorer", LATEST_URL);
        }

        /// <summary>Returns a <see cref="RateLimit"/> from the last made call to the GitHub API. returns null if no previous request has been made</summary>
        /// <returns>null or <see cref="RateLimit"/></returns>
        private RateLimit GetRateLimit()
        {
            ApiInfo apiInfo = _client.GetLastApiInfo();
#if DEBUG
            if (apiInfo == null)
            {
                System.Diagnostics.Debug.WriteLine("Rate Limit: null, no call made");
            }
            System.Diagnostics.Debug.WriteLine($"Rate Limit: {apiInfo?.RateLimit.Limit}, Remaining: {apiInfo?.RateLimit.Remaining}, Reset: {apiInfo?.RateLimit.Reset}");
#endif
            return apiInfo?.RateLimit;
        }

        private bool CanMakeRequests()
        {
            RateLimit rateLimit = GetRateLimit();
            if (rateLimit?.Remaining < 1)
            { return false; }
            return true;
        }

        private async Task SetRelease()
        {
            try
            {
                if (CanMakeRequests() == false)
                { return; }
                _releases = await _client.Repository.Release.GetAll(PROJECT_OWNER, THIS_PROJECT);
#if DEBUG
                Debug.WriteLine("releases set");
#endif
            }
            catch (Exception)
            {
            }
        }

        private Release GetReleaseByTag(string TagName)
        {
            if (ReleaseAvailable == false)
            { return null; }
            foreach (Release release in _releases)
            {
                if (release.TagName.Equals(TagName) == true)
                {
                    return release;
                }
            }
            return null;
        }

        private bool ReleaseAvailable => _releases != null && _releases.Count > 0;
        private Release LatestRelease => _releases[0];//latest release SHOULD be 0th item in Release List

        public string ReleaseUrl => LATEST_URL;
        public bool GitProjectAvailable => _gitProjectAvailable;
    }
}

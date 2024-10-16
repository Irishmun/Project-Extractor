using System.IO;

namespace ConfluenceExtractor
{
    internal static class Util
    {
        #region I/O
        /// <summary>Writes text to file at path, creating a unique filename if needed</summary>
        /// <param name="str">content to write</param>
        /// <param name="outpath">directory to write to</param>
        /// <param name="filename">name for the file</param>
        internal static void WriteToFile(string str, string outpath, string filename)
        {
            string fullPath = CreateUniqueFileName(filename, outpath);
            System.Diagnostics.Debug.WriteLine("Writing to: " + fullPath);// Path.GetFileNameWithoutExtension(fullPath));
            using (StreamWriter sw = File.CreateText(fullPath))
            {
                //write the final result to a text document
                sw.Write(str.ToString().Trim());
                sw.Close();
            }
        }

        /// <summary>Returns if the file exists at path</summary>
        internal static bool FileExists(string path)
        {
            return System.IO.File.Exists(path);
        }
        /// <summary>Creates a unique, valid, file name if needed</summary>
        /// <param name="filename">original name to use if possible</param>
        /// <param name="path">path for the name</param>
        /// <returns>full destination path</returns>
        internal static string CreateUniqueFileName(string filename, string path)
        {
            //always sanitize file name
            filename = string.Join("_", filename.Split(Path.GetInvalidFileNameChars())).Trim();
            if (File.Exists(Path.Combine(path, filename)) == false)
            { return Path.Combine(path, filename); }
            int dup = 1;
            bool exists = true;
            filename = Path.GetFileNameWithoutExtension(filename);
            string name = $"{filename} ({dup}).txt";
            while (exists)
            {
                exists = File.Exists(Path.Combine(path, name));
                if (exists == true)
                {
                    dup += 1;
                    name = $"{filename} ({dup}).txt";
                    continue;
                }
            }
            return Path.Combine(path, name);
        }
        #endregion
    }
}

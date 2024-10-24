using System.IO;
using ProjectUtility;

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
            string fullPath = FileUtil.CreateUniqueFileName(filename, outpath);
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
        
        #endregion
    }
}

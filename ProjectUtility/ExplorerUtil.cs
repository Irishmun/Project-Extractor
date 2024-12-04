using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace ProjectUtility
{
    public static class ExplorerUtil
    {
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);

        /// <summary>Selectes file in NEW explorer window</summary>
        /// <param name="path">path to file</param>
        /// <returns>true if file could be found</returns>
        public static bool SelectFileInNewExplorer(string path)
        {
            if (!File.Exists(path))
            { return false; }
            string argument = $"/select, \"{path}\"";
            Process.Start("explorer.exe", argument);
            return true;
        }

        /// <summary>Opens the explorer and selects the file at given path</summary>
        /// <param name="path"></param>
        /// <remarks>https://stackoverflow.com/a/39427395</remarks>
        public static bool OpenFolderAndSelectItem(string path)
        {
            string folderPath, file;
            folderPath = Path.GetDirectoryName(path);
            file = Path.GetFileName(path);
            IntPtr nativeFolder;
            uint psfgaoOut;
            SHParseDisplayName(folderPath, IntPtr.Zero, out nativeFolder, 0, out psfgaoOut);

            if (nativeFolder == IntPtr.Zero)//can't find folder
            { return false; }

            IntPtr nativeFile;
            SHParseDisplayName(Path.Combine(folderPath, file), IntPtr.Zero, out nativeFile, 0, out psfgaoOut);

            IntPtr[] fileArray;
            if (nativeFile == IntPtr.Zero)
            {// Open the folder without the file selected if we can't find the file
                fileArray = new IntPtr[0];
            }
            else
            {
                fileArray = new IntPtr[] { nativeFile };
            }

            SHOpenFolderAndSelectItems(nativeFolder, (uint)fileArray.Length, fileArray, 0);

            Marshal.FreeCoTaskMem(nativeFolder);
            if (nativeFile != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(nativeFile);
            }
            return true;
        }
    }
}
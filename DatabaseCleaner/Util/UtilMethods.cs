using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCleaner.Util
{
    public static class UtilMethods
    {
        /// <summary>Returns whether the application is running in 32 bit mode or not</summary>
        /// <returns>true if application is running in 32 bit mode, false if in another mode</returns>
        public static bool Is32Bit()
        {
            //IntPtr size of 4 is 32 bit, 8 is 64 bit and more is something not present at this time
            return IntPtr.Size == 4;
        }
    }
}

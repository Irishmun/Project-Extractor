using System;

namespace DatabaseCleaner.Util
{
    public class UtilMethods
    {
        /// <summary>Returns whether the application is running in 32 bit mode or not</summary>
        /// <returns>true if application is running in 32 bit mode, false if in another mode</returns>
        public static bool Is32Bit()
        {
            //IntPtr size of 4 is 32 bit, 8 is 64 bit and more is something not present at this time
            return IntPtr.Size == 4;
        }

        internal static object[] CreateBackgroundWorkerArgs(WorkerStates state, params object[] args)
        {
            object[] res = new object[args.Length + 1];
            res[0] = state;
            args.CopyTo(res, 1);
            args = null;
            return res;
        }

        internal static object[] ReadBackgroundWorkerArgs(object? arg, out WorkerStates state)
        {

            if (arg == null)
            {
                state = WorkerStates.NONE;
                return null;
            }
            object[] args = (object[])arg;
            state = (WorkerStates)args[0];
            if (args.Length == 1)
            {//should just be the state
                return null;
            }
            ArraySegment<object> res = new ArraySegment<object>(args, 1, args.Length - 1);
            arg = null;
            args = null;
            return res.ToArray();
        }
    }
}

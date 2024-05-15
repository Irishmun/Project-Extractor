using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExtractor.Util
{
    public class WorkerMethods
    {
        /// <summary>Create an array for the background worker to use</summary>
        /// <param name="state">state of the worker, will the first entry in the array</param>
        /// <param name="objects">additional objects to use</param>
        public static object[] CreateWorkerArray(WorkerStates state, params object[] objects)
        {
            object[] res = new object[objects.Length + 1];
            res[0] = state;
            for (int i = 1; i < res.Length; i++)
            {
                res[i] = objects[i - 1];
            }
            return res;
        }
    }
}

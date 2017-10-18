using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBI.Core
{
    public static class TaskHelpers
    {
        public static void RunSync(this Func<Task> func)
        {
            Task.Run(async () => { await func(); });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace IBI.Core
{
    public static class CommandLineHelpers
    {
        public static void RunCMD(this String command)
        {
            RunCMD(command, null);
        }

        public static void RunCMD(this String command, Action onComplete)
        {
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
            cmdsi.Arguments = "/c " + command;
            cmdsi.WindowStyle = ProcessWindowStyle.Hidden;
            Process cmd = Process.Start(cmdsi);
            cmd.WaitForExit();

            if (onComplete != null) onComplete();
        }

        public static async Task RunCMDAsync(this String command)
        {
            await Task.Run(() =>
            {
                RunCMD(command);
            });
        }
    }
}

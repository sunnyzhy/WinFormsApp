using System;
using System.Diagnostics;
using System.IO;

namespace ConvertApp.Service
{
    internal class ProcessService
    {
        public static bool start(string arguments)
        {
            try
            {
                killProcess("ffmpeg");
                string ffmpeg = string.Format("{0}ffmpeg.exe", AppDomain.CurrentDomain.BaseDirectory);
                ProcessStartInfo startInfo = new ProcessStartInfo(ffmpeg);
                startInfo.FileName = ffmpeg;
                startInfo.Arguments = arguments;
                startInfo.WorkingDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process process = Process.Start(startInfo);
                process.WaitForExit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private static void killProcess(string processName)
        {
            foreach (Process process in Process.GetProcessesByName(processName))
            {
                process.Kill();
            }
        }

        public static bool copy(string source, string target)
        {
            try
            {
                File.Copy(source, target);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

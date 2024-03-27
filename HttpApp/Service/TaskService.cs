using System;
using System.Threading;
using System.Windows.Forms;

namespace HttpApp.Service
{
    internal class TaskService
    {
        private static TaskService taskService;
        private static readonly object lockOject = new object();
        public delegate void HttpDelegate(string url, string filePath);
        public event HttpDelegate HttpEvent;
        public delegate void HttpDelegates(string[] urls, string filePath);
        public event HttpDelegates HttpEvents;

        private TaskService() { }

        public static TaskService CreateInstance()
        {
            if (taskService == null)
            {
                lock (lockOject)
                {
                    if (taskService == null)
                    {
                        taskService = new TaskService();
                    }
                }
            }
            return taskService;
        }

        public void Start(string url, string filePath, Button[] buttons, ProgressBar progressBar)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                progressBar.BeginInvoke(new Action(() => progressBar.Style = ProgressBarStyle.Marquee));
                foreach (var button in buttons)
                {
                    button.BeginInvoke(new Action(() => button.Enabled = false));
                }
                if (HttpEvent != null)
                {
                    HttpEvent(url, filePath);
                }
                progressBar.BeginInvoke(new Action(() =>
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Value = 0;
                }));
                foreach (var button in buttons)
                {
                    button.BeginInvoke(new Action(() => button.Enabled = true));
                }
            });
        }

        public void Start(string[] urls, string filePath, Button[] buttons, ProgressBar progressBar)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                progressBar.BeginInvoke(new Action(() => progressBar.Style = ProgressBarStyle.Marquee));
                foreach (var button in buttons)
                {
                    button.BeginInvoke(new Action(() => button.Enabled = false));
                }
                if (HttpEvents != null)
                {
                    HttpEvents(urls, filePath);
                }
                progressBar.BeginInvoke(new Action(() =>
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Value = 0;
                }));
                foreach (var button in buttons)
                {
                    button.BeginInvoke(new Action(() => button.Enabled = true));
                }
            });
        }
    }
}

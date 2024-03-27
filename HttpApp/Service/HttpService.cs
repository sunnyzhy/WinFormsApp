using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace HttpApp.Service
{
    internal class HttpService
    {
        private static HttpService httpService;
        private static readonly object lockOject = new object();
        private byte[] buffer = new byte[1024];
        private int length;
        private Random random = new Random();

        private HttpService() { }

        public static HttpService CreateInstance()
        {
            if (httpService == null)
            {
                lock (lockOject)
                {
                    if (httpService == null)
                    {
                        httpService = new HttpService();
                    }
                }
            }
            return httpService;
        }

        private int Timeout()
        {
            int timeout = random.Next(5, 15);
            return timeout * 1000;
        }

        public void GetM3u8(string url, string filePath)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webRequest.Method = HttpMethod.Get.Method;
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                using (Stream stream = webResponse.GetResponseStream())
                {
                    while ((length = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, length));
                    }
                }
                Thread.Sleep(Timeout());
                string content = stringBuilder.ToString();
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                MatchCollection matches = Regex.Matches(content, "\\d+?\\.ts");
                url = url.Substring(0, url.LastIndexOf("/") + 1);
                using (FileStream fileStream = fileInfo.Create())
                {
                    foreach (Match match in matches)
                    {
                        webRequest = (HttpWebRequest)HttpWebRequest.Create(url + match.Value);
                        webResponse = (HttpWebResponse)webRequest.GetResponse();
                        using (Stream stream = webResponse.GetResponseStream())
                        {
                            //while ((length = stream.Read(buffer, 0, buffer.Length)) > 0)
                            //{
                            //    fileStream.Write(buffer, 0, length);
                            //}
                            stream.CopyTo(fileStream);
                            //fileStream.Flush();
                        }
                        Thread.Sleep(Timeout());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void GetM3u8(string[] urls, string filePath)
        {
            for (int i = 0; i < urls.Length; i++)
            {
                string url = urls[i];
                string fileName = url.Substring(url.LastIndexOf("/") + 1);
                fileName = fileName.Substring(0, fileName.LastIndexOf("."));
                fileName += ".mp4";
                GetM3u8(url, Path.Combine(filePath, fileName));
            }
        }
    }
}

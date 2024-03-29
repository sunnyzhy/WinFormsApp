using ConvertApp.Entity;
using System;

namespace ConvertApp.Service.Video
{
    internal class Mp4Convert : BaseConvert
    {
        private static readonly Lazy<Mp4Convert> lazy = new Lazy<Mp4Convert>(() => new Mp4Convert());

        public static Mp4Convert Instance { get { return lazy.Value; } }

        private Mp4Convert() { }

        public bool convert(string source, string target)
        {
            string extention = target.Substring(target.LastIndexOf('.') + 1);
            Enum.TryParse(extention.ToUpper(), out VideoType videoType);
            string arguments = "";
            switch (videoType)
            {
                case VideoType.M3U8:
                    arguments = string.Format("-i {0} -vcodec copy -acodec copy -vbsf h264_mp4toannexb {1}", source, target);
                    break;
                case VideoType.MKV:
                    arguments = string.Format("-i {0} -vcodec copy -acodec copy {1}", source, target);
                    break;
                case VideoType.MP4:
                    bool v = ProcessService.copy(source, target);
                    return v;
            }
            if (string.IsNullOrEmpty(arguments))
            {
                return true;
            }
            bool value = ProcessService.start(arguments);
            return value;
        }
    }
}

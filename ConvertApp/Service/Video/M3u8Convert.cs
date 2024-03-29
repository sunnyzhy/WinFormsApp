using ConvertApp.Entity;
using System;

namespace ConvertApp.Service.Video
{
    internal class M3u8Convert : BaseConvert
    {
        private static readonly Lazy<M3u8Convert> lazy = new Lazy<M3u8Convert>(() => new M3u8Convert());

        public static M3u8Convert Instance { get { return lazy.Value; } }

        private M3u8Convert() { }

        public bool convert(string source, string target)
        {
            string extention = target.Substring(target.LastIndexOf('.') + 1);
            Enum.TryParse(extention.ToUpper(), out VideoType videoType);
            string arguments = "";
            switch (videoType)
            {
                case VideoType.M3U8:
                    target = target.Substring(0, target.LastIndexOf('.') + 1);
                    target += "ts";
                    arguments = string.Format("-i {0} -c copy {1}", source, target);
                    break;
                case VideoType.MKV:
                    arguments = string.Format("-i {0} -c copy -bsf:a aac_adtstoasc {1}", source, target);
                    break;
                case VideoType.MP4:
                    arguments = string.Format("-i {0} -c copy -bsf:a aac_adtstoasc {1}", source, target);
                    break;
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

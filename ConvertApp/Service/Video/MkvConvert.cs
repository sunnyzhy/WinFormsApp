using ConvertApp.Entity;
using System;

namespace ConvertApp.Service.Video
{
    internal class MkvConvert : BaseConvert
    {
        private static readonly Lazy<MkvConvert> lazy = new Lazy<MkvConvert>(() => new MkvConvert());

        public static MkvConvert Instance { get { return lazy.Value; } }

        private MkvConvert() { }

        public bool convert(string source, string target)
        {
            string extention = target.Substring(target.LastIndexOf('.') + 1);
            Enum.TryParse(extention.ToUpper(), out VideoType videoType);
            string arguments = "";
            switch (videoType)
            {
                case VideoType.M3U8:
                    arguments = string.Format("-i {0} -c:v copy -c:a aac {1}", source, target);
                    break;
                case VideoType.MKV:
                    bool v = ProcessService.copy(source, target);
                    return v;
                case VideoType.MP4:
                    arguments = string.Format("-i {0} -c:v copy -c:a libvorbis {1}", source, target);
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

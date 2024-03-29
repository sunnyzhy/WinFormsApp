using ConvertApp.Entity;
using System;

namespace ConvertApp.Service.Audio
{
    internal class M4aConvert : BaseConvert
    {
        private static readonly Lazy<M4aConvert> lazy = new Lazy<M4aConvert>(() => new M4aConvert());

        public static M4aConvert Instance { get { return lazy.Value; } }

        private M4aConvert() { }

        public bool convert(string source, string target)
        {
            string extention = target.Substring(target.LastIndexOf('.') + 1);
            Enum.TryParse(extention.ToUpper(), out AudioType imageType);
            string arguments = "";
            switch (imageType)
            {
                case AudioType.M4A:
                    bool v = ProcessService.copy(source, target);
                    return v;
                case AudioType.MP3:
                    arguments = string.Format("-i {0} -acodec libmp3lame -q:a 2 {1}", source, target);
                    break;
                case AudioType.FLAC:
                    arguments = string.Format("-i {0} -acodec flac {1}", source, target);
                    break;
                case AudioType.WAV:
                    arguments = string.Format("-i {0} -acodec pcm_s16le -ac 1 -ar 16000 {1}", source, target);
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

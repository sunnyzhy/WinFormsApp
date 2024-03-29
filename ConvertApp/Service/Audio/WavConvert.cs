using ConvertApp.Entity;
using System;

namespace ConvertApp.Service.Audio
{
    internal class WavConvert : BaseConvert
    {
        private static readonly Lazy<WavConvert> lazy = new Lazy<WavConvert>(() => new WavConvert());

        public static WavConvert Instance { get { return lazy.Value; } }

        private WavConvert() { }

        public bool convert(string source, string target)
        {
            string extention = target.Substring(target.LastIndexOf('.') + 1);
            Enum.TryParse(extention.ToUpper(), out AudioType imageType);
            string arguments = "";
            switch (imageType)
            {
                case AudioType.M4A:
                    arguments = string.Format("-i {0} -c:a aac -b:a 256k {1}", source, target);
                    break;
                case AudioType.MP3:
                    arguments = string.Format("-i {0} -acodec mp3 -ab 64k {1}", source, target);
                    break;
                case AudioType.FLAC:
                    arguments = string.Format("-i {0} -c:a flac {1}", source, target);
                    break;
                case AudioType.WAV:
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

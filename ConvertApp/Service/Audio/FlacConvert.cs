using ConvertApp.Entity;
using System;

namespace ConvertApp.Service.Audio
{
    internal class FlacConvert : BaseConvert
    {
        private static readonly Lazy<FlacConvert> lazy = new Lazy<FlacConvert>(() => new FlacConvert());

        public static FlacConvert Instance { get { return lazy.Value; } }

        private FlacConvert() { }

        public bool convert(string source, string target)
        {
            string extention = target.Substring(target.LastIndexOf('.') + 1);
            Enum.TryParse(extention.ToUpper(), out AudioType imageType);
            string arguments = "";
            switch (imageType)
            {
                case AudioType.M4A:
                    arguments = string.Format("-i {0} -ab 320k {1}", source, target);
                    break;
                case AudioType.MP3:
                    arguments = string.Format("-i {0} -ab 320k -map_metadata 0 -id3v2_version 3 {1}", source, target);
                    break;
                case AudioType.FLAC:
                    bool v = ProcessService.copy(source, target);
                    return v;
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

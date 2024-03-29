using ConvertApp.Entity;
using System;

namespace ConvertApp.Service.Audio
{
    internal class Mp3Convert : BaseConvert
    {
        private static readonly Lazy<Mp3Convert> lazy = new Lazy<Mp3Convert>(() => new Mp3Convert());

        public static Mp3Convert Instance { get { return lazy.Value; } }

        private Mp3Convert() { }

        public bool convert(string source, string target)
        {
            string extention = target.Substring(target.LastIndexOf('.') + 1);
            Enum.TryParse(extention.ToUpper(), out AudioType imageType);
            string arguments = "";
            switch (imageType)
            {
                case AudioType.M4A:
                    arguments = string.Format("-i {0} -c:a aac -b:a 192k {1}", source, target);
                    break;
                case AudioType.MP3:
                    bool v = ProcessService.copy(source, target);
                    return v;
                case AudioType.FLAC:
                    arguments = string.Format("-i {0} -f flac -acodec flac -ac 1 -ar 16000 {1}", source, target);
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

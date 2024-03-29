using ConvertApp.Entity;
using System;

namespace ConvertApp.Service.Image
{
    internal class JpegConvert : BaseConvert
    {
        private static readonly Lazy<JpegConvert> lazy = new Lazy<JpegConvert>(() => new JpegConvert());

        public static JpegConvert Instance { get { return lazy.Value; } }

        private JpegConvert() { }

        public bool convert(string source, string target)
        {
            string extention = target.Substring(target.LastIndexOf('.') + 1);
            Enum.TryParse(extention.ToUpper(), out ImageType imageType);
            string arguments = "";
            switch (imageType)
            {
                case ImageType.BMP:
                    arguments = string.Format("-i {0} {1}", source, target);
                    break;
                case ImageType.ICO:
                    arguments = string.Format("-i {0} {1}", source, target);
                    break;
                case ImageType.JPEG:
                    bool v = ProcessService.copy(source, target);
                    return v;
                case ImageType.PNG:
                    arguments = string.Format("-i {0} {1}", source, target);
                    break;
                case ImageType.SVG:
                    arguments = string.Format("-i {0} {1}", source, target);
                    break;
                case ImageType.WEBP:
                    arguments = string.Format("-i {0} {1}", source, target);
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

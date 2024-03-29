using ConvertApp.Entity;
using System;

namespace ConvertApp.Service.Image
{
    internal class WebpConvert : BaseConvert
    {
        private static readonly Lazy<WebpConvert> lazy = new Lazy<WebpConvert>(() => new WebpConvert());

        public static WebpConvert Instance { get { return lazy.Value; } }

        private WebpConvert() { }

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
                    arguments = string.Format("-i {0} {1}", source, target);
                    break;
                case ImageType.PNG:
                    arguments = string.Format("-i {0} {1}", source, target);
                    break;
                case ImageType.SVG:
                    arguments = string.Format("-i {0} {1}", source, target);
                    break;
                case ImageType.WEBP:
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

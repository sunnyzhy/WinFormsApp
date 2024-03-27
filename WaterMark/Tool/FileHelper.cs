using System.IO;
using System.Text;

/// <summary>
/// 中文编码基本可以分成两大类：
/// 1.ANSI编码的扩展集合：比如GBK, GB2312, GB18030等，这类编码都不存在BOM（一些更新的标准中文编码，比如GB18030和GBK编码，都向后兼容GB2312编码）。
/// 2.Unicode编码集合：比如UTF-8, UTF-16, UTF-32等。这类编码可以有BOM，也可以没有BOM。
/// 3.部分Unicode编码还存在具体字节次序问题（Endianess），就是所谓的Little endian和Big endian之分，不同此节次序对于不同的BOM，比如UTF16，不过UTF8不存在字节次序问题。
/// 4.如果包含BOM的话，就可以知道它的具体编码。如果没有发现BOM，那就不是Unicode，我们用系统默认的ANSI扩展中文编码集打开文本文件就OK了。
/// 5.如果Unicode编码没有BOM的话，那么就需要手动从原始字节中判断它的具体编码了
/// 
/// 
/// 带有BOM的编码:
/// UTF-8: EF BB BF
/// UTF-16 BE: FE FF
/// UTF-16 LE: FF FE
/// 
/// </summary>
namespace WaterMark.Tool
{
    internal class FileHelper
    {
        private static Encoding defaultEncoding = Encoding.Default;
        public static void Write(string fileName, string content)
        {
            File.WriteAllText(fileName, content, defaultEncoding);
        }

        public static string Read(string fileName)
        {
            string content = "";
            EncodingInfo encoding = GetEncoding(fileName);
            if (encoding.Bom)
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.Default, true))
                {
                    content = sr.ReadToEnd();
                }
            }
            else
            {
                content = File.ReadAllText(fileName, encoding.Encoding);
            }
            return content;
        }

        private static EncodingInfo GetEncoding(string fileName)
        {
            EncodingInfo encodingInfo = new EncodingInfo();
            using (var stream = File.OpenRead(fileName))
            {
                if (!stream.CanRead)
                {
                    return encodingInfo;
                }
                var bom = new byte[4];
                int readc = stream.Read(bom, 0, 4);
                if (readc >= 2)
                {
                    if (readc >= 4)
                    {
                        // UTF32，Big-Endian
                        if (CheckBytes(bom, 4, 0x00, 0x00, 0xFE, 0xFF))
                        {
                            encodingInfo.Bom = true;
                            encodingInfo.Encoding = new UTF32Encoding(true, true);
                        }
                        // UTF32，Little-Endian
                        if (CheckBytes(bom, 4, 0xFF, 0xFE, 0x00, 0x00))
                        {
                            encodingInfo.Bom = true;
                            encodingInfo.Encoding = new UTF32Encoding(false, true);
                        }
                    }
                    // UTF8
                    if (readc >= 3 && CheckBytes(bom, 3, 0xEF, 0xBB, 0xBF))
                    {
                        encodingInfo.Bom = true;
                        encodingInfo.Encoding = new UTF8Encoding(true);
                    }
                    // UTF16，Big-Endian
                    if (CheckBytes(bom, 2, 0xFE, 0xFF))
                    {
                        encodingInfo.Bom = true;
                        encodingInfo.Encoding = new UnicodeEncoding(true, true);
                    }
                    // UTF16，Little-Endian
                    if (CheckBytes(bom, 2, 0xFF, 0xFE))
                    {
                        encodingInfo.Bom = true;
                        encodingInfo.Encoding = new UnicodeEncoding(false, true);
                    }
                }
            }
            return encodingInfo;
        }

        private static bool CheckBytes(byte[] bytes, int count, params int[] values)
        {
            for (int i = 0; i < count; i++)
            {
                if (bytes[i] != values[i])
                {
                    return false;
                }
            }
            return true;
        }

        private class EncodingInfo
        {
            private bool bom;
            private Encoding encoding = defaultEncoding;

            public bool Bom { get => bom; set => bom = value; }
            public Encoding Encoding { get => encoding; set => encoding = value; }
        }
    }
}

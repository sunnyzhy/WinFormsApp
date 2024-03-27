using System.IO;
using System.Text;

namespace WaterMark.Tool
{
    internal class FileCodeHelper
    {
        public static Encoding GB2312 => Encoding.GetEncoding("GB2312");

        static FileCodeHelper()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static Encoding GetEncoding(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                if (!stream.CanRead)
                {
                    return null;
                }
                var bom = new byte[4];
                int readc = stream.Read(bom, 0, 4);
                // 带有BOM
                if (readc >= 2)
                {
                    if (readc >= 4)
                    {
                        // UTF32，Big-Endian
                        if (CheckBytes(bom, 4, 0x00, 0x00, 0xFE, 0xFF))
                        {
                            return new UTF32Encoding(true, true);
                        }
                        // UTF32，Little-Endian
                        if (CheckBytes(bom, 4, 0xFF, 0xFE, 0x00, 0x00))
                        {
                            return new UTF32Encoding(false, true);
                        }
                    }
                    // UTF8
                    if (readc >= 3 && CheckBytes(bom, 3, 0xEF, 0xBB, 0xBF))
                    {
                        return new UTF8Encoding(true);
                    }
                    // UTF16，Big-Endian
                    if (CheckBytes(bom, 2, 0xFE, 0xFF))
                    {
                        return new UnicodeEncoding(true, true);
                    }
                    // UTF16，Little-Endian
                    if (CheckBytes(bom, 2, 0xFF, 0xFE))
                    {
                        return new UnicodeEncoding(false, true);
                    }
                }
                // 不带BOM
                if (Utf8WithoutBom(stream))
                {
                    return Encoding.UTF8;
                }
                if (Ascii(stream))
                {
                    return Encoding.ASCII;
                }
            }
            Encoding encoding = GbkOrUtf8(fileName);
            return encoding;
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

        private static bool Utf8WithoutBom(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);//重置 position 位置
            bool isAllASCII = true;
            long totalLength = stream.Length;
            long nBytes = 0;
            using (var br = new BinaryReader(stream, Encoding.Default, true))
            {
                for (long i = 0; i < totalLength; i++)
                {
                    byte b = br.ReadByte();
                    // (1000 0000): 值小于0x80的为ASCII字符    
                    // 等同于 if(b < 0x80 )
                    if ((b & 0x80) != 0) //0x80 128
                    {
                        isAllASCII = false;
                    }
                    if (nBytes == 0)
                    {
                        if (b >= 0x80)
                        {
                            if (b >= 0xFC && b <= 0xFD) { nBytes = 6; }//此范围内为6字节UTF-8字符
                            else if (b >= 0xF8) { nBytes = 5; }// 此范围内为5字节UTF-8字符
                            else if (b >= 0xF0) { nBytes = 4; }// 此范围内为4字节UTF-8字符    
                            else if (b >= 0xE0) { nBytes = 3; }// 此范围内为3字节UTF-8字符    
                            else if (b >= 0xC0) { nBytes = 2; }// 此范围内为2字节UTF-8字符    
                            else { return false; }
                            nBytes--;
                        }
                    }
                    else
                    {
                        if ((b & 0xC0) != 0x80) { return false; }//0xc0 192  (11000000): 值介于0x80与0xC0之间的为无效UTF-8字符    
                        nBytes--;
                    }
                }
            }
            if (nBytes > 0)
            {
                return false;
            }
            if (isAllASCII)
            {
                return false;
            }
            return true;
        }

        private static bool Ascii(Stream stream)
        {
            bool isAllASCII = true;
            long totalLength = stream.Length;
            stream.Seek(0, SeekOrigin.Begin);//重置 position 位置
            using (var br = new BinaryReader(stream, Encoding.Default, true))
            {
                for (long i = 0; i < totalLength; i++)
                {
                    byte b = br.ReadByte();
                    /*
                     * 原理是
                     * 0x80     1000 0000
                     * &
                     * 0x75 (p) 0111 0101
                     * ASCII字符都比128小，与运算自然都是0
                     */
                    if ((b & 0x80) != 0)// (1000 0000): 值小于0x80的为ASCII字符    
                    {
                        isAllASCII = false;
                        break;
                    }
                }
            }
            return isAllASCII;
        }

        private static Encoding GbkOrUtf8(string fileName)
        {
            var utf8Str = ReadFile(fileName, Encoding.UTF8);
            var gbkStr = ReadFile(fileName, GB2312);
            return utf8Str.Length <= gbkStr.Length ? Encoding.UTF8 : GB2312;
        }

        public static string ReadFile(string fileName)
        {
            Encoding encoding = FileCodeHelper.GetEncoding(fileName);
            string content = ReadFile(fileName, encoding);
            return content;
        }

        public static string ReadFile(string fileName, Encoding encoding)
        {
            using (StreamReader sr = new StreamReader(fileName, encoding, true))
            {
                return sr.ReadToEnd();
            }
        }

        public static void WriteFile(string fileName, string content, Encoding encoding)
        {
            File.WriteAllText(fileName, content, encoding);
        }

        //public static Encoding UTF8 => Encoding.UTF8;
        //public static Encoding Unicode => Encoding.Unicode;
        //public static Encoding BigEndianUnicode => Encoding.BigEndianUnicode;
        //public static Encoding ASCII => Encoding.ASCII;
        //public static Encoding UTF16BE => Encoding.BigEndianUnicode;
        //public static Encoding UTF16LE => Encoding.Unicode;
        //public static Encoding GB2312 => Encoding.GetEncoding("GB2312");
        //public static Encoding GBK => Encoding.GetEncoding("GBK");
        //public static Encoding GB18030 => Encoding.GetEncoding("GB18030");

        //public static Encoding GetEncoding1(string filename)
        //{
        //    if (!File.Exists(filename))
        //    {
        //        return Encoding.Default;
        //    }
        //    Encoding encoding = null;
        //    using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
        //    {
        //        encoding = GetEncoding(fs);
        //        if (encoding == null)
        //        {
        //            encoding = GbkOrUtf8(fs);
        //        }
        //    }
        //    return encoding;
        //}

        //public static Encoding GetEncoding(Stream stream)
        //{
        //    if (!stream.CanRead)
        //    {
        //        return null;
        //    }
        //    using (var br = new BinaryReader(stream))
        //    {
        //        var buffer = br.ReadBytes(3);
        //        if (buffer[0] == 0xFE && buffer[1] == 0xFF)//FE FF 254 255  UTF-16 BE (big-endian)
        //        {
        //            return Encoding.BigEndianUnicode;
        //        }

        //        if (buffer[0] == 0xFF && buffer[1] == 0xFE)//FF FE 255 254  UTF-16 LE (little-endian)
        //        {
        //            return Encoding.Unicode;
        //        }
        //        if (buffer[0] == 0xEF && buffer[1] == 0xBB & buffer[2] == 0xBF)//EF BB BF 239 187 191 UTF-8 
        //        {
        //            return Encoding.UTF8;//with BOM
        //        }

        //        if (Utf8WithoutBom(stream))
        //        {
        //            return Encoding.UTF8;//without BOM
        //        }
        //        if (Ascii(stream))
        //        {
        //            return Encoding.ASCII; //默认返回ascii编码
        //        }
        //        return Encoding.Default;
        //    }
        //}

        //public static bool Ascii(string path)
        //{
        //    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        //    {
        //        return Ascii(fs);
        //    }
        //}

        //public static string ReadStream(Stream stream, Encoding encoding)
        //{
        //    if (!stream.CanRead)
        //    {
        //        return null;
        //    }
        //    using (StreamReader sr = new StreamReader(stream, encoding, true))
        //    {
        //        return sr.ReadToEnd();
        //    }
        //}

    }
}

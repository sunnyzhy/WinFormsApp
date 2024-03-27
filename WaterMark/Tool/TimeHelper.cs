using System;

namespace WaterMark.Tool
{
    internal class TimeHelper
    {
        public static string GetSystemMillis()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
    }
}

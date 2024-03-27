using StackExchange.Redis;

namespace WindowsFormsApp1.entity
{
    internal class GlobalData
    {
        private ConnectionMultiplexer redisConnection;
        private RedisServer redisServer;

        public ConnectionMultiplexer RedisConnection { get => redisConnection; set => redisConnection = value; }
        internal RedisServer RedisServer { get => redisServer; set => redisServer = value; }

        private GlobalData() { }

        private static class InstanceHolder
        {
            internal static GlobalData GLOBALDATA = new GlobalData();
        }

        public static GlobalData getInstance()
        {
            return InstanceHolder.GLOBALDATA;
        }


    }
}

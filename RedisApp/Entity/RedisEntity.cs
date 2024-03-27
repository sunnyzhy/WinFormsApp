using StackExchange.Redis;

namespace RedisApp.Entity
{
    public class RedisEntity
    {
        private RedisType redisType;
        private RedisKey redisKey;

        public RedisEntity(RedisType redisType, RedisKey redisKey)
        {
            this.redisType = redisType;
            this.redisKey = redisKey;
        }

        public RedisKey RedisKey { get => redisKey; }
        public RedisType RedisType { get => redisType; }
        
    }
}

using StackExchange.Redis;

namespace RedisApp.Entity
{
    class HashEntity
    {
        private RedisKey name;
        private RedisKey value;

        public HashEntity(RedisKey name, RedisKey value)
        {
            this.name = name;
            this.value = value;
        }

        public RedisKey Name { get => name; }
        public RedisKey Value { get => value; }
    }
}

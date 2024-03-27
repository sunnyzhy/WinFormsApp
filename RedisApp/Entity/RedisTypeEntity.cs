using StackExchange.Redis;

namespace RedisApp.Entity
{
    class RedisTypeEntity
    {
        private string name;
        private RedisType value;

        public RedisTypeEntity(string name, RedisType value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name { get => name;  }
        public RedisType Value { get => value;  }
    }
}

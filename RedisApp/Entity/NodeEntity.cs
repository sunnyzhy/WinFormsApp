using StackExchange.Redis;

namespace RedisApp.Entity
{
    public class NodeEntity
    {
        private RedisType type;
        private RedisKey key;
        private string text;

        public NodeEntity(RedisType type, RedisKey key, string name)
        {
            this.type = type;
            this.key = key;
            this.text = name;
        }

        public RedisType Type { get => type; }
        public RedisKey Key { get => key; }
        public string Text { get => text; }
    }
}

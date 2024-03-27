using RedisApp.Entity;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace RedisApp.Helper
{
    class RedisHelper
    {
        private static volatile RedisHelper redisHelper;
        private static object locked = new object();
        private ConnectionMultiplexer connectionMultiplexer;
        private string host;
        public string getHost()
        {
            return host;
        }

        public void setHost(string host)
        {
            this.host = host;
        }
        private string port;
        private string password;

        private IDatabase database;
        private IServer server;
        public void selectDb(int db)
        {
            connectionMultiplexer.ConnectionFailed += ConnectionMultiplexer_ConnectionFailed;
            database = connectionMultiplexer.GetDatabase(db);
            server = connectionMultiplexer.GetServer(host, int.Parse(port));
        }

        private RedisHelper()
        {

        }

        public static RedisHelper GetInstance()
        {
            if (redisHelper == null)
            {
                lock (locked)
                {
                    if (redisHelper == null)
                    {
                        redisHelper = new RedisHelper();
                    }
                }
            }
            return redisHelper;
        }

        public ConnectionMultiplexer Connect(string host, string port, string password)
        {
            if (connectionMultiplexer == null)
            {
                this.host = host;
                this.port = port;
                this.password = password;
                ConfigurationOptions configuration = new ConfigurationOptions();
                configuration.EndPoints.Add(host, int.Parse(port));
                if (!string.IsNullOrEmpty(password))
                {
                    configuration.Password = password;
                }
                try
                {
                    connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
                }
                catch (RedisConnectionException e)
                {
                    if ((int)e.FailureType >= (int)ConnectionFailureType.None && (int)e.FailureType <= (int)ConnectionFailureType.UnableToConnect)
                    {
                        connectionMultiplexer = null;
                    }
                    throw e;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return connectionMultiplexer;
        }

        private void ConnectionMultiplexer_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            lock (locked)
            {
                if (!connectionMultiplexer.IsConnected)
                {
                    connectionMultiplexer = null;
                    connectionMultiplexer = Connect(host, port, password);
                    if (!connectionMultiplexer.IsConnected)
                    {
                        throw new Exception("connect failed");
                    }
                }
            }
        }

        public RedisValue Get(RedisKey redisKey)
        {
            RedisValue value = database.StringGet(redisKey);
            return value;
        }

        public List<RedisEntity> Search(params string[] args)
        {
            List<RedisEntity> redisEntities = new List<RedisEntity>();
            IEnumerable<RedisKey> enumerable = null;
            if (args.Length == 0)
            {
                enumerable = server.Keys(database: database.Database, pageSize: 500, pageOffset: 0);
            }
            else
            {
                enumerable = Search(args[0]);
            }
            IEnumerator<RedisKey> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                RedisKey redisKey = enumerator.Current;
                RedisType redisType = database.KeyType(redisKey);
                // 目前暂时支持的数据类型：String 、Hash
                if (redisType != RedisType.String && redisType != RedisType.Hash)
                {
                    continue;
                }
                redisEntities.Add(new RedisEntity(redisType, redisKey));
            }
            return redisEntities;
        }

        private IEnumerable<RedisKey> Search(string key)
        {
            IEnumerable<RedisKey> enumerable = null;
            if (string.IsNullOrEmpty(key))
            {
                enumerable = server.Keys(database: database.Database, pageSize: 500, pageOffset: 0);
            }
            else
            {
                RedisValue pattern = new RedisValue(key);
                enumerable = server.Keys(database: database.Database, pattern: pattern, pageSize: 500, pageOffset: 0);
            }
            return enumerable;
        }

        public bool SetString(RedisKey key, RedisValue value)
        {
            bool result = database.StringSet(key,value);
            return result;
        }

        public RedisValue Delete(RedisKey[] keys)
        {
            RedisValue value = database.KeyDelete(keys);
            return value;
        }

        public void Delete(string key)
        {
            List<RedisKey> keys = new List<RedisKey>();
            IEnumerable<RedisKey> enumerable = Search(key);
            IEnumerator<RedisKey> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                RedisKey redisKey = enumerator.Current;
                keys.Add(redisKey);
                if (keys.Count == 500)
                {
                    Delete(keys.ToArray());
                    keys.Clear();
                }
            }
            if (keys.Count > 0)
            {
                Delete(keys.ToArray());
                keys.Clear();
            }
        }

        public HashEntry[] GetHash(RedisKey redisKey)
        {
            HashEntry[] entries = database.HashGetAll(redisKey);
            return entries;
        }

        public RedisValue GetHash(RedisKey key, RedisValue hashField)
        {
            RedisValue value = database.HashGet(key, hashField);
            return value;
        }

        public void SetHash(RedisKey key, HashEntry[] hashFields)
        {
            database.HashSet(key, hashFields);
        }

        public void DeleteHash(RedisKey[] keys)
        {
            foreach (RedisKey key in keys)
            {
                HashEntry[] entries = GetHash(key);
                RedisValue[] hashFields = new RedisValue[entries.Length];
                for (int i = 0; i < entries.Length; i++)
                {
                    hashFields[i] = entries[i].Name;
                }
                database.HashDelete(key, hashFields);
            }
        }

        public void DeleteHash(RedisKey key, RedisValue[] hashFields)
        {
            database.HashDelete(key, hashFields);
        }
    }
}

using Newtonsoft.Json;
using ServiceStack.Redis;
using WebApplication3.Common;

namespace WebApplication3.Services
{
    public sealed class RedisService
    {
        private readonly RedisEndpoint _redisEndpoint;
        private static RedisService _instance = null;
        private static readonly object _instanceLock = new object();

        private RedisService()
        {
            string host = Constants.REDIS_HOSTNAME;
            int port = Constants.REDIS_PORT;
            _redisEndpoint = new RedisEndpoint(host, port);
        }

        public static RedisService Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new RedisService();
                    }
                    return _instance;
                }
            }
        }

        public void SetValue<T>(string key, T value)
        {
            using (IRedisClient redisClient = new RedisClient(_redisEndpoint))
            {
                redisClient.Set<T>(key, value);
            }
        }

        public T GetValue<T>(string key)
        {
            using (IRedisClient redisClient = new RedisClient(_redisEndpoint))
            {
                string value = redisClient.GetValue(key);
                return string.IsNullOrEmpty(value) ? default(T) : JsonConvert.DeserializeObject<T>(value);
            }
        }
    }
}
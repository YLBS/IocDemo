using Model;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Cache
{
    public static class RedisCache
    {
        private const string Host = "127.0.0.1";
        /// <summary>
        /// 存储值为string类型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public static void SetKey(string key, string value, DateTime time)
        {
            using (var client = new RedisClient(Host, 6379))
            {
                if (client.ContainsKey(key))
                {
                    client.Del(key);
                }
                client.Add<string>(key, value, time);
            }
        }
        /// <summary>
        /// 通过key获得string类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetKey(string key)
        {
            using (var client = new RedisClient(Host, 6379))
            {
                if (client.ContainsKey(key))
                    return client.Get<string>(key);
                else
                    return string.Empty;
            }
        }
        public static void DelPosKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;
            using (var client = new RedisClient(Host, 6379))
            {
                var sss = client.GetAllKeys();
                var dellres = sss.Where(m => m.Contains(key)).ToList();
                foreach (var item in dellres)
                {
                    client.Del(item);
                }

            }
        }
        public static IList<OutRedis> GetKeyValue(string key)
        {
            var list = new List<OutRedis>();
            using (var client = new RedisClient(Host, 6379))
            {
                var s = client.GetAllKeys().Where(r => r.Contains(key));
                foreach (var item in s)
                {
                    var str = client.Get<string>(item);
                    var date = client.GetTimeToLive(item)?.TotalSeconds ?? 0;
                    list.Add(new OutRedis(item, str, date));
                }
            }
            return list;
        }
        public static List<OutRedis> GetAll()
        {
            var list = new List<OutRedis>();
            var s = new List<string>();
            using (var client = new RedisClient(Host, 6379))
            {
                s = client.GetAllKeys();
                foreach (var item in s)
                {
                    try
                    {
                        var str = client.Get<string>(item);
                        var date = client.GetTimeToLive(item)?.TotalSeconds ?? 0;
                        list.Add(new OutRedis(item, str, date));
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            return list;
        }

    }
}

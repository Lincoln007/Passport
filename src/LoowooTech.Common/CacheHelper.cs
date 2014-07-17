using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ServiceStack.Redis;
namespace LoowooTech.Common
{
    public static class Cache
    {
        private static RedisClient GetRedisClient()
        {
            return new RedisClient(AppSettings.Current["Cache_Host"]);
        }

        private static byte[] GetHashKey(string key)
        {
            return Encoding.UTF8.GetBytes(key);
        }

        public static void Set(string key, object value)
        {
            using (var client = GetRedisClient())
            {
                client.Set(key, value.ToBinary());
            }
        }

        public static T Get<T>(string key)
        {
            using (var client = GetRedisClient())
            {
                var data = client.Get(key);
                return data == null ? default(T) : data.ToObject<T>();
            }
        }

        public static void Remove(string key)
        {
            using (var client = GetRedisClient())
            {
                client.Del(key);
            }
        }

        public static void HSet<T>(string hashId, string key, T value)
        {
            using (var client = GetRedisClient())
            {
                client.HSet(hashId, GetHashKey(key), value.ToBinary());
            }
        }

        public static T HGet<T>(string hashId, string key)
        {
            using (var client = GetRedisClient())
            {
                var data = client.HGet(hashId, Encoding.UTF8.GetBytes(key));
                return data == null ? default(T) : data.ToObject<T>();
            }
        }

        public static void HRemove(string hashId, string key = null)
        {
            using (var client = GetRedisClient())
            {
                if (string.IsNullOrEmpty(key))
                {
                    client.Del(key);
                }
                else
                {
                    client.HDel(hashId, GetHashKey(key));
                }
            }
        }

        public static void LSet<T>(string listId, IEnumerable<T> list)
        {
            using (var client = GetRedisClient())
            {
                client.Del(listId);
                client.LPush(listId, list.Select(e => e.ToBinary()).ToArray());
            }
        }

        public static void LAppend(string listId, object value)
        {
            using (var client = GetRedisClient())
            {
                client.LPush(listId, value.ToBinary());
            }
        }

        public static IEnumerable<T> LGet<T>(string listId)
        {
            using (var client = GetRedisClient())
            {
                return client.LRange(listId, 0, -1).Select(e => e.ToObject<T>());
            }
        }

    }
}

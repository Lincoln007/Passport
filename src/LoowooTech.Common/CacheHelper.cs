using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HData = System.Collections.Concurrent.ConcurrentDictionary<string, object>;
using LData = System.Collections.Generic.List<object>;
namespace LoowooTech.Common
{
    public static class Cache
    {
        private static readonly ConcurrentDictionary<string, object> NDatas = new ConcurrentDictionary<string, object>();
        private static readonly ConcurrentDictionary<string, HData> HDatas = new ConcurrentDictionary<string, HData>();
        private static readonly ConcurrentDictionary<string, LData> LDatas = new ConcurrentDictionary<string, LData>();
        
        #region Session Cache
        public static void SSet(string sessionName, object value)
        {
            HttpContext.Current.Session[sessionName] = value;
        }

        public static T SGet<T>(string sessionName)
        {
            var value = HttpContext.Current.Session[sessionName];
            return value == null ? default(T) : (T)value;
        }

        public static void SRemove(string sessionName)
        {
            HttpContext.Current.Session.Remove(sessionName);
        }

        #endregion

        #region Normal Cache
        public static void Set(string key, object value)
        {
            if (NDatas.ContainsKey(key))
            {
                NDatas[key] = value;
            }
            else
            {
                NDatas.TryAdd(key, value);
            }
        }

        public static T Get<T>(string key)
        {
            object value;
            NDatas.TryGetValue(key, out value);
            return (T)value;
        }

        public static void Remove(string key)
        {
            object value;
            NDatas.TryRemove(key, out value);
        }

        #endregion

        #region HashTable Cache
        private static HData GetHdata(string hashId)
        {
            return HDatas.GetOrAdd(hashId, new HData());
        }

        public static void HSet<T>(string hashId, string key, T value)
        {
            var data = GetHdata(hashId);
            if (data.ContainsKey(key))
            {
                data[key] = value;
            }
            else
            {
                data.TryAdd(key, value);
            }
        }

        public static T HGet<T>(string hashId, string key)
        {
            var data = GetHdata(hashId);
            return data.ContainsKey(key) ? (T)data[key] : default(T);
        }

        public static void HRemove(string hashId, string key = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                HData value;
                HDatas.TryRemove(hashId, out value);
            }
            else
            {
                var data = GetHdata(hashId);
                object value;
                data.TryRemove(key, out value);
            }
        }

        #endregion

        #region List Cache

        public static void LSet<T>(string key, IEnumerable<T> list)
        {
            var data = LGet(key);
            data.Clear();
            foreach (var item in list)
            {
                data.Add(item);
            }
        }

        public static void LAppend(string key, object value)
        {
            LGet(key).Add(value);
        }

        public static LData LGet(string key)
        {
            return LDatas.GetOrAdd(key, new LData());
        }

        public static IEnumerable<T> LGet<T>(string key, Func<T, bool> findFunc = null)
        {
            if (findFunc == null)
            {
                return LGet(key).Select(item => (T)item);
            }
            return LGet(key).Where(item => findFunc((T)item)).Select(a => (T)a);
        }

        public static void LRemove<T>(string key, Func<T, bool> findFunc = null)
        {
            if (findFunc != null)
            {
                LSet(key, LGet(key).Where(item => !findFunc((T)item)).ToList());
            }
            else
            {
                LData value;
                LDatas.TryRemove(key, out value);
            }
        }

        public static void LUpdate<T>(string key, Action<T> updateFunc)
        {
            LGet(key).ForEach(item => updateFunc((T)item));
        }
        #endregion
    }
}

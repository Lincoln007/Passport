using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using LoowooTech.Common;

namespace LoowooTech.Passport.Dao
{
    public static class MappingHelper
    {
        private static readonly string TableSchema;
        private static readonly ConcurrentDictionary<Type, string> TableNames = new ConcurrentDictionary<Type, string>();

        static MappingHelper()
        {
            TableSchema = AppSettings.Current["DBSchema"];

        }

        public static string GetTableName<T>(string tableName = null)
        {
            var type = typeof(T);
            if (TableNames.ContainsKey(type))
            {
                return TableNames[type];
            }

            var attr = type.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault();
            if (attr == null)
            {
                if (string.IsNullOrEmpty(tableName))
                    tableName = type.Name;

            }
            else
            {
                tableName = ((TableAttribute)attr).Name;
            }

            tableName = string.Format("{0}.{1}", TableSchema, tableName);

            TableNames.TryAdd(type, tableName);

            return TableNames[type];
        }
    }
}

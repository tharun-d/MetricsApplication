using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace MetricsApplication.Common
{
    public class CommonThings
    {
        private static string key = "CONN_STRING";
        private static ObjectCache _cache = MemoryCache.Default;
        // keeping connectin  string in Cache 24 hours 
        private static int _defaultCacheExpireInSeconds = 86400;
        public static string GetConnectionString()
        {
            string connString = string.Empty;
            if (_cache.Contains(key))
            {
                connString = (string)_cache.Get(key);
            }
            else
            {
                connString = ConfigurationManager.ConnectionStrings["Dev"].ConnectionString;
                _cache.Add(key, connString, DateTimeOffset.Now.AddSeconds(_defaultCacheExpireInSeconds));
            }

            return connString;
        }
        #region Procedures
        public readonly string loginValidator = "LoginValidator @userName,@password";

        #endregion
    }
}
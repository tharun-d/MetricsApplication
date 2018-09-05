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
        public readonly string loginValidator = "LoginValidator @email,@secretPassword";
        public readonly string insertIntoMetricsData = "InsertIntoMetricsData @userName,@applicationName,@taskDescription,@taskClassification,@startDate,@endDate,@effortHours";
        public readonly string allMetricsGetter = "AllMetricsGetter";
        public readonly string insertIntoLeaveDetails = "InsertIntoLeaveDetails @userName,@startDate,@endDate,@noOfDays,@vacationType,@comment";
        public readonly string updateCATWHours = "updateCATWHours @userName,@totalHours,@manuallyFilled";
        public readonly string allMetricsGetterbyUserName = "AllMetricsGetterbyUserName @userName";
        public readonly string metricsGetterbyId = "MetricsGetterbyId @id";
        public readonly string metricsDeletebyId = "MetricsDeletebyId @id";
        public readonly string allLeaveDetailsbyUserName = "AllLeaveDetailsbyUserName @userName";
        public readonly string allLeaveDetailsGetter = "AllLeaveDetailsGetter";
        public readonly string deleteLeaveDetailsByid = "DeleteLeaveDetailsByid @id";
        public readonly string getCatwHoursByUserName = "GetCatwHoursByUserName @userName";
        public readonly string allstatussheet = "Allstatussheet";
        public readonly string allEmployees = "AllEmployees";
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HeiMa8.OA.Common.Cache
{
    public class HttpRuntimeCacheWriter:ICacheWriter
    {

        public void AddCache(string key, object value, DateTime extDate)
        {
            HttpRuntime.Cache.Insert(key, value, null, extDate, TimeSpan.Zero);
        }

        public void AddCache(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value);
        }

        public void SetCache(string key, object value)
        {
            HttpRuntime.Cache.Remove(key);
            AddCache(key, value);
        }

        public void SetCache(string key, object value, DateTime extDate)
        {
            HttpRuntime.Cache.Remove(key);
            AddCache(key, value, extDate);
        }

        public object GetCache(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public T GetCache<T>(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memcached.ClientLibrary;

namespace HeiMa8.OA.Common.Cache
{
    public class MemcacheWriter:ICacheWriter
    {
        private MemcachedClient memcachedClient;

        public MemcacheWriter()
        {
            //分布Memcachedf服务IP 端口
            //string[] servers = { "192.168.1.100:11211", "192.168.1.2:11211" };
            string strAppMemcachedServer = System.Configuration.ConfigurationManager.AppSettings["MemcachedServerList"];

            string[] servers = strAppMemcachedServer.Split(',');
            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            //客户端实例
            MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.EnableCompression = false;
            memcachedClient = mc;
        }
        public void AddCache(string key, object value, DateTime extDate)
        {
            memcachedClient.Add(key, value, extDate);
        }

        public void AddCache(string key, object value)
        {
            memcachedClient.Add(key, value);
        }

        public void SetCache(string key, object value)
        {
            memcachedClient.Set(key, value);
        }

        public void SetCache(string key, object value, DateTime extDate)
        {
            memcachedClient.Set(key, value, extDate);
        }

        public object GetCache(string key)
        {
            return memcachedClient.Get(key);
        }

        public T GetCache<T>(string key)
        {
            return (T)memcachedClient.Get(key);
        }
    }
}

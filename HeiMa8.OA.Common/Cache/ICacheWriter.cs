using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeiMa8.OA.Common.Cache
{
   public interface ICacheWriter
    {
       //添加缓存
       void AddCache(string key,object value,DateTime extDate);
       void AddCache(string key, object value);

       //设置缓存
       void SetCache(string key, object value);
       void SetCache(string key, object value, DateTime extDate);

       //获取缓存
       object GetCache(string key);
       T GetCache<T>(string key);
    }
}

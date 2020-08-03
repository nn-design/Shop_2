using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace COMMON
{
    public class RedisHelper
    {
        static ConnectionMultiplexer conn = ConnectionMultiplexer.Connect("192.168.178.188:6379,password=123456");
        static IDatabase db = conn.GetDatabase(0);
        public static void Set(string key,int value,TimeSpan timeSpan)
        {
            db.StringSet(key, value, timeSpan);
        }
        public static string Get(string key)
        {
            return db.StringGet(key);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //c#操作redis
            //redis一班存储kv类型的键值对数据（字典数据库）

            //1 声明一个链接
            var conn= ConnectionMultiplexer.Connect("192.168.178.188:6379,password=123456");
            //2 指定操作的数据库
            var db= conn.GetDatabase(0);
            //3 写入数据
            db.StringSet("name", "nmz",DateTime.Now.AddDays(15)-DateTime.Now);
            db.StringSet("age", "19");
            //4 读取数据
            //设置name的有效期为15天
            string name= db.StringGet("name");
            string age = db.StringGet("age");

            //redis如果不设置有效期，默认是永远有效（内存足够用）
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            ////1 声明一个链接
            //var conn= ConnectionMultiplexer.Connect("192.168.178.188:6379,password=123456");
            ////2 指定操作的数据库
            //var db= conn.GetDatabase(0);
            ////3 写入数据
            //db.StringSet("name", "nmz",DateTime.Now.AddDays(15)-DateTime.Now);
            //db.StringSet("age", "19");
            ////4 读取数据
            ////设置name的有效期为15天
            //string name= db.StringGet("name");
            //string age = db.StringGet("age");

            //redis如果不设置有效期，默认是永远有效（内存足够用）


            //c#实现异步编程
            //1、通过线程

            //window下时间片默认为20毫秒

            //声明一个线程
            //Thread thread = new Thread(F1);
            //thread.Start();//启动一个线程

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("y");
            //}

            ////线程阻塞
            //thread.Join();//主线程会等待thread线程执行完毕后才会继续往下执行




            // 2 task

            //声明一个task,没有返回值并开始异步操作

            //Task task = Task.Run(() =>
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        Console.WriteLine("x");
            //    }
            //    return "hello world";
            //});

            //声明一个task,有返回值(task类型为泛型类型)并开始异步操作
            //Task<string> task = Task.Run(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.WriteLine("aaa");
            //    }
            //    Thread.Sleep(2000);
            //    return "hello world";

            //});


            //Task task1 = Task.Run(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.WriteLine("bbb");
            //    }
            //    Thread.Sleep(3000);
            //});


            //Task task2 = Task.Run(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.WriteLine("bbb");
            //    }
            //    Thread.Sleep(3000);
            //});

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("111");
            //}

            //Console.WriteLine(task.Result);//遇到task.Result 会产生一个阻塞，等待异步任务执行结束后才会继续往下执行

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("y");
            //}
            Task<string> ts = F1();
            Console.WriteLine(ts.Result);
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("1");
            }
            Console.ReadLine();

        }


        //async：标识该方法内部会至少一个异步任务
        //async修饰的方法内必须至少包含一个await运算符，await后一般跟一个异步任务
        //async修饰方法的返回值：void、task、task<>

        //void、task不用返回值
        //task<T>:直接返回T类型的值就可以
        public async static Task<string> F1()
        {

            //主线程执行
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("a");
            }
            //遇到await之后会从线程池中取一个线程执行await之后的所有代码
            await Task.Run(() =>
            {

                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("b");
                }
            });
            //await Task.Run(() =>
            //{

            //    for (int i = 0; i < 20; i++)
            //    {
            //        Console.WriteLine("c");
            //    }
            //});
            ////await也会产生一个阻塞，等待await之后的异步任务执行结束之后才会继续往下执行

            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine("d");
            //}

            return "hello world";

        }
    }
}

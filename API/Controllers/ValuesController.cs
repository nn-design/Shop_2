using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class ValuesController : ApiController
    {
        //根据http动作匹配以动作名称开头的action，如果多个action动作名字相同，会根据参数进行更精确的匹配

        // GET api/values
        //返回全部数据
        public IEnumerable<Person> GetVale()
        {
            return new List<Person>()
            {
                //初始化集合器
                new Person()
                {
                    ID=1,
                    Name="aaa",
                    Age=20
                },
                new Person()
                {
                    ID=2,
                    Name="bbb",
                    Age=21
                },
            };
        }
       
        // GET api/values/5
        //返回单条数据
        public Person Get(int id)//简单数据类型（非主体值，通过url传递值------int string double由模型绑定器处理）
        {
            return new Person()
            {
                Name = "aaa",
                Age = 20
            };
        }

        // POST api/values
        public void Post(Person p)//复杂数据类型（主体值只能有一个，通过请求体传递-------------类，由格式化器处理）
        {
        }

        // PUT api/values/5
        //修改
        public void Put(Person p)
        {
        }

        // DELETE api/values/5
        //删除
        public void Delete(int id)
        {
        }
    }
}

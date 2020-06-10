using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace DAL
{
    public class DBContextFactory
    {
        public static ShopEntities GetEntities()
        {
            //首先判断容器中是否包含上下文对象
            var obj = CallContext.GetData("dbContext");
            if (obj==null)
            {
                //创建一个上下文类，并将其放到线程容器中
                ShopEntities entities = new ShopEntities();
                CallContext.SetData("dbContext", entities);
                return entities;
            }
            return (ShopEntities)obj;
            
        }
    }
}

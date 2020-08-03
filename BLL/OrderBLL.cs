using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using IBLL;
using DAL;
using IDAL;

namespace BLL
{
    public class OrderBLL : BaseBLL<Order, OrderDAL>, IOrderBLL
    {

        IOrderDAL orderDAL = new OrderDAL();
        IOrderDetailDAL orderDetailDAL = new OrderDetailDAL();


        public  int Add(Order order, List<OrderDetail> orderDetail)
        {
            int result = 0;
            var tran = dal.BeginTran();
            try
            {
                //Order表插入值
                orderDAL.Add(order);
                result += SaveChange();//相当于预提交，  默认情况下，调用SaveChange会开启一个事务
                
                //OrderDetail表插入值
                foreach (var item in orderDetail)
                {
                    item.OrderID = order.ID;
                    orderDetailDAL.Add(item);
                }
                result += SaveChange();//相当于预提交，  默认情况下，调用SaveChange会开启一个事务

                tran.Commit();//总提交
            }
            catch (Exception)
            {
                tran.Rollback();
            }
            return result;
        }
    }
}

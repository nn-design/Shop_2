using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace IBLL
{
    public interface IOrderBLL :IBaseBLL<Order>
    {
        int Add(Order order, List<OrderDetail> orderDetail);
    }
}

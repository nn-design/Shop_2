using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using DAL;
using IBLL;

namespace BLL
{
    public class OrderDetailBLL : BaseBLL<OrderDetail, OrderDetailDAL>, IOrderDetailBLL
    {
    }
}

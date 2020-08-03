using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using BLL;
using IBLL;
using MODEL;
using COMMON;

namespace API.Controllers
{
    public class OrderController : ApiController
    {
        public IOrderBLL Bll
        {
            get
            {
                return new OrderBLL();
            }
        }

        [Route("api/Order/GetOrder")]
        [HttpPost]
        public ResponsMessage<string> GetOrder(OrderVModel OrderVModel)
        {
            IEnumerable<string> headValues;
            string token = null;
            if (ActionContext.Request.Headers.TryGetValues("Authorization", out headValues))
            {
                token = headValues.FirstOrDefault();
            }
            //Order表插入值
            Order order = new Order();
            order = OrderVModel.order;
            order.MemberID = int.Parse(RedisHelper.Get(token));

            //OrderDetail表插入值
            OrderDetail orderDetail = new OrderDetail();
            orderDetail = OrderVModel.OrderDetail;
            Bll.Add(order, orderDetail);

            return new ResponsMessage<string>()
            {
                Code = 200
            };

        }
       
    }
}

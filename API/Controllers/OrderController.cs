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
            Order order = new Order();
            order = OrderVModel.order;
            order.MemberID = int.Parse(RedisHelper.Get(token));
            order.CreatTime = DateTime.Now;
            order.State = "未支付";
            order.OrderNum = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + new Random().Next(10000, 99999).ToString();
            
            Bll.Add(order, OrderVModel.OrderDetail);

            return new ResponsMessage<string>()
            {
                Code = 200,
                Message = "请求成功",
                Data = "OK"
            };

        }
       
    }
}

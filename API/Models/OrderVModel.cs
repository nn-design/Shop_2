using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MODEL;

namespace API.Models
{
    public class OrderVModel
    {
        public Order order { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
}
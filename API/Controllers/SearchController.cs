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

namespace API.Controllers
{
    public class SearchController : ApiController
    {
        public IProductBLL Bll
        {
            get
            {
                return new ProductBLL();
            }
        }

        public ResponsMessage<List<Product>> Get(string query)
        {
            var list = Bll.Search(x => x.ProductTitle.Contains(query));
            return new ResponsMessage<List<Product>>()
            {
                Code = 200,
                Message = "请求成功",
                Data = list
            };
        }
    }
}

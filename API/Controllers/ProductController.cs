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
using Shop.Models;

namespace API.Controllers
{
    public class ProductController : ApiController
    {
        public IProductBLL Bll
        {
            get
            {
                return new ProductBLL();
            }
        }
       


    }
}

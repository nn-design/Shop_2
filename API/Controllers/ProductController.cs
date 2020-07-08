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
        //public ResponsMessage<List<ProductCategoryVModel>> GetMsg(string kewWords,int pageNum,int pageSize)
        //{
        //    var list = Bll.Search(kewWords);

        //}
        //public ActionResult GetAll(int draw, int pageSize, int pageIndex)
        //{
        //    var list = Bll.Search(pageSize, pageIndex, false, x => true);
        //    var count = Bll.GetCount(x => true);
        //    ////构造返回json对象{"draw":  ,"data": }

        //    var result = new
        //    {
        //        draw = draw,
        //        data = list,
        //        recordsTotal = count,
        //        recordsFiltered = count,
        //    };
        //    return Json(result);
        //}


    }
}

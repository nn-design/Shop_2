using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MODEL;
using BLL;
using IBLL;
using Shop.Models;

namespace Shop.Controllers
{
    public class ProductController : BaseController<Product, IProductBLL>
    {
        public override IProductBLL Bll
        {
            get
            {
                return new ProductBLL();
            }
        }
        //IProductBLL bll = new ProductBLL();
        //public ActionResult Add()
        //{
        //    return View();
        //}
        [HttpPost]
        public ActionResult Add(ProductVModel vModel)
        {
            Product product = vModel.Product;
            List<ProductSku> skuList = vModel.Skus;
            List<ProductAttr> attrList = vModel.Attrs;
            Bll.Add(product, skuList, attrList);
            return Json(new { state = true });
        }
        [HttpPost]
        public ActionResult GetAll(int draw, int pageSize, int pageIndex)
        {
            var list = Bll.Search(pageSize, pageIndex, false, x => true);
            var count = Bll.GetCount(x => true);
            ////构造返回json对象{"draw":  ,"data": }

            var result = new
            {
                draw = draw,
                data = list,
                recordsTotal = count,
                recordsFiltered = count,
        };
            return Json(result);
    }
}
}
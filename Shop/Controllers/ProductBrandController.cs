using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using IBLL;
using MODEL;
using Shop.Models;

namespace Shop.Controllers
{
    public class ProductBrandController : BaseController<ProductBrand, IProductBrandBLL>
    {
        // GET: ProductBrand
        public override IProductBrandBLL Bll
        {
            get
            {
                return new ProductBrandBLL();
            }
        }
        [HttpPost]
        public ActionResult Add(ProductBrand Brand)
        {
            Bll.Add(Brand);
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

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(ProductBrand Brand)
        {
            
            Bll.Update(Brand);
            return Json(new { state = true });
        }


        [HttpGet]
        public virtual ActionResult GetOne(int id)
        {
            
            //默认情况下，c#方法返回值只有一个，为了弥补这种缺陷，应使用out参数变相增加返回值
            //out参数：必须在方法体内为其赋值
            var productBrand = Bll.GetOne(id);
            var result = new
            {
                product = productBrand
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
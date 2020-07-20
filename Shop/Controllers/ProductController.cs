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
            List<ProductAttr> attrList = vModel.Attrs;
            List<ProductSku> skuList = vModel.ProductSkus;
            //List<ProductSkuImg> skuImgs = vModel.ProductSkuImg;

            Bll.Add(product, skuList, attrList);
            return Json(new { state = true });
        }
        [HttpPost]
        public ActionResult GetAll(int draw, int pageSize, int pageIndex)
        {
            int count;
            var list = Bll.Search(pageSize, pageIndex, false,x=>x.ID, x => true,out count);
            
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
        public ActionResult Update(ProductVModel vModel)
        {

            Product product = vModel.Product;
            List<ProductSku> skuList = vModel.ProductSkus;
            List<ProductAttr> attrList = vModel.Attrs;

            Bll.Update(product, skuList, attrList);
            return Json(new { state = true });
        }

        [HttpGet]
        public virtual ActionResult GetOne(int id)
        {

            List<ProductAttr> attrs;
            List<ProductSku> skus;
            //默认情况下，c#方法返回值只有一个，为了弥补这种缺陷，应使用out参数变相增加返回值
            //out参数：必须在方法体内为其赋值
            var product = Bll.GetOne(id, out attrs, out skus);
            var result = new
            {
                product = product,
                attrs = attrs,
                skus = skus,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}

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
    public class ProductAttrKeySkuController : Controller
    {
        IProductAttrKeyBLL atterKeyBLL = new ProductAttrKeyBLL();
        IProductAttrValueBLL attrValueBLL = new ProductAttrValueBLL();

        // GET: ProductAttrKey

        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ProductAttrKeyVModel productAttrKeyVModel)
        {
            //插入attr表
            ProductAttrKey productAttrKey = new ProductAttrKey()
            {
                AttrName = productAttrKeyVModel.AttrName,
                OrderNum = productAttrKeyVModel.OrderNum,
                EnterType = productAttrKeyVModel.EnterType,
                IsSku = 1,
                IsImg = productAttrKeyVModel.IsImg,
                ProductCategoryID = productAttrKeyVModel.ProductCategoryID
            };
            atterKeyBLL.Add(productAttrKey);
            //2 插入value表
            if (productAttrKeyVModel.AttrValues != null)
            {
                foreach (var item in productAttrKeyVModel.AttrValues)
                {
                    ProductAttrValue productAttrValue = new ProductAttrValue()
                    {
                        AttrValue = item,
                        ProductAttrKeyID = productAttrKey.ID
                    };
                    attrValueBLL.Add(productAttrValue);
                }
            }

            return Json(new { state = true, msg = "添加成功" });

        }

        public ActionResult GetByCatecoryID(int draw, int categoryId)
        {
            var list = atterKeyBLL.GetByCatecoryID(categoryId,true);
            var result = new { draw = draw, data = list };
            return Json(result);
        }
        [HttpGet]
        public ActionResult GetOne(int id)
        {
            //获取attrKey的数据
            var attrKey = atterKeyBLL.GetOne(id);
            //获取attrValue的数据
            var attrValue = attrValueBLL.GetAllByAttrKeyID(id);
            var data = new { attrKey = attrKey, attrValue = attrValue };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(ProductAttrKeyVModel productAttrKeyVModel)
        {
            //插入attr表
            ProductAttrKey productAttrKey = new ProductAttrKey()
            {
                ID = productAttrKeyVModel.ID,
                AttrName = productAttrKeyVModel.AttrName,
                OrderNum = productAttrKeyVModel.OrderNum,
                EnterType = productAttrKeyVModel.EnterType,
                IsSku = 1,

                IsImg = productAttrKeyVModel.IsImg,
                ProductCategoryID = productAttrKeyVModel.ProductCategoryID
            };

            List<ProductAttrValue> attrValues = new List<ProductAttrValue>();
            //2 插入value表
            if (productAttrKeyVModel.AttrValues != null)
            {
                foreach (var item in productAttrKeyVModel.AttrValues)
                {
                    ProductAttrValue productAttrValue = new ProductAttrValue()
                    {
                        AttrValue = item,
                        ProductAttrKeyID = productAttrKey.ID
                    };
                    attrValues.Add(productAttrValue);
                }
            }
            atterKeyBLL.Update(productAttrKey, attrValues);
            return Json(new { state = true, msg = "修改成功" });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            int result = atterKeyBLL.DeLete(id);
            return Json(new { state = result > 0 ? true : false });
        }
        [HttpGet]
        public ActionResult GetByCategoryID(int categoryID)
        {
            //查询attrkey表数据
            var list = atterKeyBLL.Search(x => x.ProductCategoryID == categoryID && x.IsSku == 1);

            List<ProductAttrKeyVModel> vList = new List<ProductAttrKeyVModel>();
            foreach (var item in list)
            {
                var vModel = new ProductAttrKeyVModel();
                vModel.ID = item.ID;
                vModel.AttrName = item.AttrName;
                vModel.EnterType = item.EnterType;
                vModel.IsImg = item.IsImg;
                vModel.AttrValues = new List<string>();
                //查询attrvalue表数据
                var attrvalus = attrValueBLL.Search(x => x.ProductAttrKeyID == item.ID);
                foreach (var valueItem in attrvalus)
                {
                    vModel.AttrValues.Add(valueItem.AttrValue);
                }
                //将attrkey表数据，attrvalue表数据放到vList
                vList.Add(vModel);
            }
            return Json(vList, JsonRequestBehavior.AllowGet);
        }

    }
}

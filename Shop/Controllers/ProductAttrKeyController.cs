﻿using System;
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
    //当请求到达服务端后，服务器会分配一个线程去处理该请求，该线程会有一个容器，我们可以将EF对现象放到该容器中
    public class ProductAttrKeyController : Controller
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
                IsSku = 0,
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

        [HttpGet]
        public ActionResult GetWithCategoryID(int categoryID)
        {
            var vList = atterKeyBLL.GetByCatecoryID(categoryID, false);

            return Json(vList, JsonRequestBehavior.AllowGet);
            ////查询attrkey表数据
            //var list = atterKeyBLL.Search(x => x.ProductCategoryID == categoryID && x.IsSku == 0);

            //List<ProductAttrKeyVModel> vList = new List<ProductAttrKeyVModel>();
            //foreach (var item in list)
            //{
            //    var vModel = new ProductAttrKeyVModel();
            //    vModel.ID = item.ID;
            //    vModel.AttrName = item.AttrName;
            //    vModel.EnterType = item.EnterType;
            //    vModel.AttrValues = new List<string>();
            //    //查询attrvalue表数据
            //    var attrvalus = attrValueBLL.Search(x => x.ProductAttrKeyID == item.ID);
            //    foreach (var valueItem in attrvalus)
            //    {
            //        vModel.AttrValues.Add(valueItem.AttrValue);
            //    }
            //    //将attrkey表数据，attrvalue表数据放到vList
            //    vList.Add(vModel);
            //}
            ////var vList = atterKeyBLL.GetByCatecoryID(categoryID, true);
            //return Json(vList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetByCatecoryID(int draw,int categoryId)
        {
             var list = atterKeyBLL.GetByCategoryID(categoryId);
            list = list.Where(x => x.IsSku == 0).ToList();
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
                ID= productAttrKeyVModel.ID,
                AttrName = productAttrKeyVModel.AttrName,
                OrderNum = productAttrKeyVModel.OrderNum,
                EnterType = productAttrKeyVModel.EnterType,
                IsSku = 0,
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

    }
}
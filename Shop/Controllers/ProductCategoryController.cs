using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using COMMON;
using IBLL;
using MODEL;
using Shop.Models;

namespace Shop.Controllers
{
   
    public class ProductCategoryController : Controller
    {

        IProductCategoryBLL bll = new ProductCategoryBLL();
        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add()

        {
            List<ProductCategory> categories = bll.GetAll();
            ProductCategoryAddVModel vModel = new ProductCategoryAddVModel() { Categories = categories };
            return View(vModel);
            
        }
        [HttpPost]
        public ActionResult Add(ProductCategory category)
        {
            if (!ModelState.IsValid)//对模型的验证是否通过
            {
                //获取第一条错误信息
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        return Json(new { state = false, msg = error.ErrorMessage });
                    }
                }


                
            }
            
            

            //4.将数据保存到数据库

            //category.Img = newFileName;
            category.CreateTime = DateTime.Now;

            //匿名类型：var 变量名=new{属性名1=值1，属性名2=值2...}
            //var result = new { state = true,name=Name,pid=PID, ordernum= OrderNum };

            bll.Add(category);
            //return View();
            //Json方法用来将一个对象序列化json串
            //return Json(result);
            return Json(new { state=true,msg="添加成功"});
        }
       

        public ActionResult List()
        {
            return View();
            
        }
        [HttpPost]
        public ActionResult GetAll(int draw)
        {
            //获取全部分类列表
            //var list = bll.GetAll();

            //递归生成json数据
            //1.获取所有一级节点（分类）
            var rootList= bll.GetSub(0);
            List<ProductCategoryVModel> list = new List<ProductCategoryVModel>();
            foreach (var category in rootList)
            {
                ProductCategoryVModel vModel = new ProductCategoryVModel();
                vModel.ID = category.ID;
                vModel.Name = category.Name;
                vModel.PID = category.PID;
                vModel.OrderNum = category.OrderNum;
                vModel.Img = category.Img;
                vModel.children = new List<ProductCategoryVModel>();//初始化子节点集合
                GetSub(vModel);
                list.Add(vModel);
            }

            //构造返回json对象{"draw":  ,"data": }

            var result = new { draw = draw, data = list };
            return Json(result);
        }
        //获取父节点的所有子节点
        private void GetSub(ProductCategoryVModel parentNode)
        {
            var subList = bll.GetSub(parentNode.ID);

            if (subList.Count()== 0)//递归终止条件
            {
                return;
            }
            //判断子节点下是否还包含子结点
            foreach (var category in subList)
            {
                ProductCategoryVModel vModel = new ProductCategoryVModel();
                vModel.ID = category.ID;
                vModel.Name = category.Name;
                vModel.PID = category.PID;
                vModel.OrderNum = category.OrderNum;
                vModel.Img = category.Img;
                vModel.children = new List<ProductCategoryVModel>();//初始化子节点集合
                GetSub(vModel);//开始递归
                parentNode.children.Add(vModel);
                
            }
        }
        [HttpPost]
        public ActionResult Delete(int id) {
            int result = bll.DeLete(id);
            return Json(new { state = result > 0 ? true : false });
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            ProductCategory category = bll.GetOne(id);
            List<ProductCategory> categories = bll.GetAll();
            ProductCategoryUpdateVModel vModel = new ProductCategoryUpdateVModel() { Category = category, Categories=categories };
            return View(vModel);
        }
        [HttpPost]
        public ActionResult Update(ProductCategory category)
        {
            

            int result = bll.Update(category);
            return Json(new { state = result > 0 ? true : false });
        }
        [HttpGet]
        public ActionResult GetByPID(int pid)
        {
            var list = bll.GetSub(pid);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
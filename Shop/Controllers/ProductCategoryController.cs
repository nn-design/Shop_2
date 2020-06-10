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
            
            var file = Request.Files["file"];


            //1.判断文件是否存在 

            if (file==null)
            {
                return Json(new { state = false, msg = "图片不存在" });
            }
            //2.判断上传文件的类型（.jpg .jpeg .png .gif）,类型检测不通过，直接返回

            byte[] bytes= Common.StreamToBytes(file.InputStream);

            FileExtension fileExtension= FileHelper.CheckTextFile(bytes);
            if (fileExtension == FileExtension.VALIDFILE)
            {
                return Json(new { state = false, msg = "上传的文件已损坏" });
            }
            if (!(fileExtension == FileExtension.GIF || fileExtension == FileExtension.JPG || fileExtension == FileExtension.PNG))
            {
                return Json(new { state = false, msg = "上传的文件类型有误" });
            }
            
            //3.生成一个新的文件名：20200317084945444（yyyyMMddHHmmssfffff）+五位随机数+后缀名
            DateTime dt = DateTime.Now;
            //yyyy：四位数的年份，MM：两位数的月份，dd：两位数的日期，HH：24小时制的时间,mm:两位数的分钟,ss:两位数的秒，f:代表随机数
            //2020-03-18:yyyy-MM-dd
            //2020年03月18日:yyyy年MM月dd日
            dt.ToString("yyyyMMddHHmmssfffff");
            //获取随机数
            Random random = new Random();
            int randowNum= random.Next(10000,99999);
            //获取文件后缀名
            string extension= Path.GetExtension(file.FileName);
            string newFileName=dt.ToString("yyyyMMddHHmmssfffff") + randowNum.ToString()+extension;
            
            //将图片保存到服务器
            //F:\Asp.net\Shop\All Shop\shop\实现ajax的添加\Shop\Shop\CategoryImages  :绝对路径
            //file.SaveAs(@"F:\Asp.net\Shop\All Shop\shop\实现ajax的添加\Shop\Shop\CategoryImages\"+newFileName);
            //根据相对路径获取绝对路径
            file.SaveAs(Server.MapPath("/CategoryImages/") + newFileName);

            //4.将数据保存到数据库

            category.Img = newFileName;
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
            var file = Request.Files["file"];

            //1.判断文件是否存在 

            if (file != null)
            {
                //2.判断上传文件的类型（.jpg .jpeg .png .gif）,类型检测不通过，直接返回

                byte[] bytes = Common.StreamToBytes(file.InputStream);

                FileExtension fileExtension = FileHelper.CheckTextFile(bytes);
                if (fileExtension == FileExtension.VALIDFILE)
                {
                    return Json(new { state = false, msg = "上传的文件已损坏" });
                }
                if (!(fileExtension == FileExtension.GIF || fileExtension == FileExtension.JPG || fileExtension == FileExtension.PNG))
                {
                    return Json(new { state = false, msg = "上传的文件类型有误" });
                }

                //3.生成一个新的文件名：20200317084945444（yyyyMMddHHmmssfffff）+五位随机数+后缀名
                DateTime dt = DateTime.Now;
                //yyyy：四位数的年份，MM：两位数的月份，dd：两位数的日期，HH：24小时制的时间,mm:两位数的分钟,ss:两位数的秒，f:代表随机数
                //2020-03-18:yyyy-MM-dd
                //2020年03月18日:yyyy年MM月dd日
                dt.ToString("yyyyMMddHHmmssfffff");
                //获取随机数
                Random random = new Random();
                int randowNum = random.Next(10000, 99999);
                //获取文件后缀名
                string extension = Path.GetExtension(file.FileName);
                string newFileName = dt.ToString("yyyyMMddHHmmssfffff") + randowNum.ToString() + extension;

                //将图片保存到服务器
                //F:\Asp.net\Shop\All Shop\shop\实现ajax的添加\Shop\Shop\CategoryImages  :绝对路径
                //file.SaveAs(@"F:\Asp.net\Shop\All Shop\shop\实现ajax的添加\Shop\Shop\CategoryImages\"+newFileName);
                //根据相对路径获取绝对路径
                file.SaveAs(Server.MapPath("/CategoryImages/") + newFileName);

                //4.将数据保存到数据库

                category.Img = newFileName;
            }
            

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
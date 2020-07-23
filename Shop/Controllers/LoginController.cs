using System;
using System.Collections.Generic;
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
    [AllowAnonymous]
    public class LoginController : Controller
    {
        IAdminBLL bll = new AdminBLL();
        // GET: Login
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return Redirect("/Product/list");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Name, string Password)
        {
            //正常逻辑：根据用户名和密码从数据库中查询该用户是否存在，如果存在将用户信息写入session容器中，否则继续登录
           
            string jiayan = "@#$%@#$%";
            Password= Md5Helper.Md5(Md5Helper.Md5(jiayan + Password));
            var result= bll.Search(x => x.Name == Name && x.Password == Password);
            if (result.Count==1)
            {
                // 将用户信息写入session容器中
                Session["user"] = result[0];

                return Redirect("/Product/list");
            }
            return Redirect("/Login/Index");


        }
        public ActionResult Logout()
        {
            //清空session容器中的用户信息
            Session["user"] = null;
            return Redirect("/Login/Index");
        }
    }
}
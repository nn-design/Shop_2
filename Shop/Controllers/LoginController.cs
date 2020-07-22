using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            //正常逻辑：根据用户名和密码从数据库中查询该用户是否存在，如果存在将用户信息写入session容器中，否则继续登录

            //将用户信息写入session容器中
            Session["user"] = 1;
            return Redirect("/Product/list");
        }
        public ActionResult Logout()
        {
            //清空session容器中的用户信息
            Session["user"] = null;
            return Redirect("/Login/Index");
        }
    }
}
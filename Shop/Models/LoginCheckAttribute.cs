using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Models
{
    public class LoginCheckAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //检测contorller上是否包含AllowAnonymousAttribute特性
            if (filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute),false).Any())
            {
                return;
            }
            //判断session容器中是否包含用户信息，如果包含继续执行，否则跳转登陆界面
            if (filterContext.HttpContext.Session["user"] == null)
            {
                //跳转到登陆界面
                //RedirectResult:重定向
                filterContext.Result = new RedirectResult("/Login/Index");
            }
            //base.OnAuthorization(filterContext);
        }
    }
}
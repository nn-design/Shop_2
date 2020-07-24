using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL;
using IBLL;
using MODEL;

namespace Shop.Controllers
{
    public class BaseController<T,B> : Controller
        where T : BaseModel, new()
        where B : IBaseBLL<T>
    {
        IAdminBLL bll = new AdminBLL();
        public Admin CurrentUser
        {

            get
            {
                //获取session的值
                //return (Admin)Session["user"];

                //获取cookie的值
                var cookieValue= Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                var ticket = FormsAuthentication.Decrypt(cookieValue);
                var userName = ticket.UserData;
                var result = bll.Search(x => x.Name == userName);
                return result[0];
            }
        }
        public virtual B Bll { get; set; }
        //B bll = new B();
        // GET: Base
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public ActionResult List()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            int result = Bll.DeLete(id);
            return Json(new { state = result > 0 ? true : false });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using MODEL;

namespace Shop.Controllers
{
    public class BaseController<T,B> : Controller
        where T : BaseModel, new()
        where B : IBaseBLL<T>
    {
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
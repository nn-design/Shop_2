using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebDemo1.Service;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebDemo1.Controllers
{
    public class HomeController : Controller
    {
        //注入的方式：构造函数 属性（net core不支持）

        //注入的地方：Controller的构造函数，Startup类里的Configure方法
        //构造函数
        ICarService _carService;
        public HomeController(ICarService carService)
        {
            _carService = carService;
        }

        //属性
        //public ICarService carService { get; set; }


        public IActionResult Index()
        {
            _carService.PrintGuid();

            //_carService.Start();
            //_carService.Stop();

            return View();
        }
    }
}

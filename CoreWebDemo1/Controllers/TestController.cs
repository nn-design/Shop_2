using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebDemo1.Service;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebDemo1.Controllers
{
    public class TestController : Controller
    {
        ICarService _carService;
        public TestController(ICarService carService)
        {
            _carService = carService;
        }
        public IActionResult Index()
        {
            _carService.PrintGuid();
            return View();
        }
    }
}

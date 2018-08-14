using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AstuteCommander.Controllers
{
    public class AppController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
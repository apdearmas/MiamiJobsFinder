﻿using System.ComponentModel;
using System.Web.Mvc;

namespace MiamiJobsFinder.Controllers
{
    [DisplayName("Home")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
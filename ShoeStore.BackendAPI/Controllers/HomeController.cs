﻿using Microsoft.AspNetCore.Mvc;
using ShoeStore.BackendAPI.Models;
using System.Diagnostics;

namespace ShoeStore.BackendAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
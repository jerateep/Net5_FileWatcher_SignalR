using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MonitorFile.Models;
using MonitorFile.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorFile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileWatcherRepositiory _FileRepositiory;
        public HomeController(ILogger<HomeController> logger, IFileWatcherRepositiory FileRepositiory)
        {
            _logger = logger;
            _FileRepositiory = FileRepositiory;
        }

        [HttpGet]
        public IActionResult GetFiles()
        {
            List<FileDetails> data = _FileRepositiory.CheckFile();
            return PartialView(data);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

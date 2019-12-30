using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication1.CustomClasses;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LegalController : Controller
    {
        private readonly ILogger<LegalController> _logger;

        public LegalController(ILogger<LegalController> logger)
        {
            _logger = logger;
        }

        public IActionResult DSGVO()
        {
            return View();
        }

        public IActionResult Projects()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Knowledge()
        {
            return View();
        }

        public IActionResult Impressum()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<List<ImageSource>> GetImageSources()
        {
            List<ImageSource>jsonData = new List<ImageSource>();
            string path = @"..\imgSources.json";
            var jsonContent = System.IO.File.ReadAllText(path);
            try
            {
                jsonData = JsonConvert.DeserializeObject<List<ImageSource>>(jsonContent);
            }
            catch(Exception ex)
            {
                //TODO
            }
            return jsonData;
        }
    }
}

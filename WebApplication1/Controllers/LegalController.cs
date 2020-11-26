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
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication1.CustomClasses;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LegalController : Controller
    {
        private readonly ILogger<LegalController> _logger;
        private readonly IOptions<Configuration> config;

        public LegalController(ILogger<LegalController> logger, IOptions<Configuration> config)
        {
            this.config = config;
            _logger = logger;
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
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Resources\imgSources.json"));
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
        [HttpGet]
        public async Task<string> GetFullpageLicense()
        {
            return config.Value.fullpageLicenseKey;
        }
    }
}

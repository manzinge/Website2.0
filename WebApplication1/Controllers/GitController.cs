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
    public class GitController : Controller
    {
        private readonly ILogger<GitController> _logger;

        public GitController(ILogger<GitController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<List<Project>> GetProjects(string username)
        {
            List<Project> projects = new List<Project>();
            string url = "https://api.github.com/users/" + username + "/repos";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                    client.DefaultRequestHeaders.Add("User-Agent", ".NET Core Application");
                    Task<String> stringTask = client.GetStringAsync(url);
                    String result = await stringTask;
                    projects = JsonConvert.DeserializeObject<List<Project>>(result);
                    foreach (Project project in projects)
                    {
                        project.updated_at = project.updated_at.Split('T')[0];
                        project.name = project.name.ToLower()[0].ToString().ToUpper() + project.name.Substring(1, project.name.Length - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
            return projects;
        }
    }
}

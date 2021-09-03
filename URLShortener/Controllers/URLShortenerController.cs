using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    public class URLShortenerController : Controller
    {
        public URLShortenerController(IConfiguration iConfig)
        {
            Configuration = iConfig;
            DBConn = Configuration.GetSection("DbConnection").Value;
        }
        private IConfiguration Configuration { get; set; }
        private string DBConn { get; set; }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string Index(string url)
        {
            var id = Repository.SetURL(url, DBConn);

            var hex = id.ToString("X8");

            return $"localhost:44380/URLShortener/GoTo/?hex={hex}";
        }
        public void GoTo(string hex)
        {
            if (!string.IsNullOrWhiteSpace(hex))
            {
                var id = Convert.ToInt32(hex, 16);

                var url = Repository.GetURL(id, DBConn);

                Response.Redirect(url);
            }
            else
                Response.Redirect("localhost:44380/URLShortener/");

        }
    }
}

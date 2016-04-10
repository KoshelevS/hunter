﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace Hunter.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<ActionResult> PostFile(IList<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            foreach (var f in files)
            {
                //await f.SaveAsAsync(Path.Combine(HostingEnvironment.WebRoot, "test-file" + files.IndexOf(f)));
            }
            return Ok();
        }
    }
}

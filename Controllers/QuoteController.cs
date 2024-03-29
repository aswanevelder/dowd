﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using dowd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using dowd.Services;

namespace dowd.Controllers
{
    public class QuoteController : Controller
    {
        private readonly ILogger<QuoteController> _logger;
        private readonly IAWSS3 _s3;
        private readonly ISendGridService _mail;
        public QuoteController(ILogger<QuoteController> logger, IAWSS3 s3, ISendGridService mail)
        {
            _logger = logger;
            _s3 = s3;
            _mail = mail;
        }

        public IActionResult Index()
        {
            return View(new Quote());
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async Task<ActionResult> Index(Quote model, IEnumerable<IFormFile> files)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    var filenames = await _s3.Write(files);
                    var response = await _mail.SendQuote(model, filenames);
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                            return RedirectToAction("Complete");
                        else
                            return RedirectToAction("Failed");
                    }
                    else
                        return RedirectToAction("Failed");
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Complete()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Failed()
        {
            return View();
        }
    }
}

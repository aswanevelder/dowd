using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using dowd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using dowd.Services;

namespace dowd.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IAWSS3 _s3;
        private readonly ISendGridService _mail;
        public ContactController(ILogger<ContactController> logger, IAWSS3 s3, ISendGridService mail)
        {
            _logger = logger;
            _s3 = s3;
            _mail = mail;
        }

        public IActionResult Index()
        {
            return View(new Contact());
        }

        [HttpPost]
        public async Task<ActionResult> Index(Contact model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    var response = await _mail.SendContact(model);
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using dowd.Models;

namespace dowd.Controllers
{
    public class QuoteController : Controller
    {
        private readonly ILogger<QuoteController> _logger;

        public QuoteController(ILogger<QuoteController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new Quote());
        }

        [HttpPost]
        public ActionResult Index(Quote model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
            }
            return View(model);
            // var errors = await _repository.ImportFile(_user.UserToken.AccountId, file.OpenReadStream());
            // if (errors.Count > 0)
            // {
            //     foreach (var err in errors)
            //     {
            //         ModelState.AddModelError("", err);
            //     }
            //     return View();
            // }
            // else
            //     return RedirectToAction(nameof(Index));

        }
    }
}

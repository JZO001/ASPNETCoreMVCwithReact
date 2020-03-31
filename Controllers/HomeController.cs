using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreWithMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> LOGGER;

        public HomeController(ILogger<HomeController> logger)
        {
            LOGGER = logger;
        }

        public IActionResult Index()
        {
            LOGGER.LogDebug("[HomeController] new landing page request");

            ViewBag.HeadContent = "<title>Welcome</title>"; // or anything you want by default into the head
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Errors(int id)
        {
            // ID represents the HTML error code
            LOGGER.LogError(string.Format("[HomeController] request error, HTTP error code: {0}", id.ToString()));

            return View("Index"); // redirect to home, react will handle the unknown request on client side
        }

    }
}

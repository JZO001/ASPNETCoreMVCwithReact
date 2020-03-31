using System.Text;
using ASPNETCoreMVCwithReact.Codes;
using ASPNETCoreMVCwithReact.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreWithMVC.Controllers
{
    public class ArticleController : Controller
    {

        private readonly ILogger<ArticleController> LOGGER;

        public ArticleController(ILogger<ArticleController> logger)
        {
            LOGGER = logger;
        }

        public IActionResult Index(int id)
        {
            StringBuilder sbHeadContent = new StringBuilder();

            LOGGER.LogInformation(string.Format("[ArticleController] article requested, id: {0}", id.ToString()));

            if (ArticleProvider.ArticlesByKey.ContainsKey(id))
            {
                ArticleData data = ArticleProvider.ArticlesByKey[id];
                // here we can customize the head content. We can add meta, title, link, script, style, etc
                sbHeadContent.AppendFormat("<title>{0}</title>", data.Header);
                sbHeadContent.AppendFormat("<meta property=\"og:site_name\" content=\"{0}\" />", data.Header);
                sbHeadContent.AppendFormat("<meta property=\"og:description\" content=\"{0}\" />", data.Header);
            }
            else
            {
                LOGGER.LogError(string.Format("[ArticleController] article not found, id: {0}", id.ToString()));

                sbHeadContent.Append("<title>Welcome</title>");
                sbHeadContent.Append("<meta property=\"og:site_name\" content=\"My site name\" />");
                sbHeadContent.Append("<meta property=\"og:description\" content=\"My site description\" />");
            }

            ViewBag.HeadContent = sbHeadContent.ToString();
            return View();
        }

    }
}
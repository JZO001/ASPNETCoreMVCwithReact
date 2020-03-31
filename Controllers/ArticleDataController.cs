using ASPNETCoreMVCwithReact.Codes;
using ASPNETCoreMVCwithReact.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreMVCwithReact.Controllers
{
    [ApiController]
    [Route("ArticleData")]
    public class ArticleDataController : ControllerBase
    {

        private readonly ILogger<ArticleDataController> LOGGER;

        public ArticleDataController(ILogger<ArticleDataController> logger)
        {
            LOGGER = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public ArticleData Get([FromRoute]int id)
        {
            LOGGER.LogInformation(string.Format("[ArticleDataController] article requested, id: {0}", id.ToString()));
            return ArticleProvider.ArticlesByKey[id];
        }

    }
}
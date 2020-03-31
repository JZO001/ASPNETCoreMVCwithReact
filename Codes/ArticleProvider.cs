using ASPNETCoreMVCwithReact.Models;
using System.Collections.Generic;

namespace ASPNETCoreMVCwithReact.Codes
{

    public static class ArticleProvider
    {

        static ArticleProvider()
        {
            ArticlesByKey = new Dictionary<int, ArticleData>();

            ArticlesByKey[1] = new ArticleData()
            {
                Id = 1,
                Header = "Article 1",
                Content = "How to create ASP.NET Core MVC React app"
            };

            ArticlesByKey[2] = new ArticleData()
            {
                Id = 2,
                Header = "Article 2",
                Content = "Lorem ipsum or what..."
            };

        }

        public static Dictionary<int, ArticleData> ArticlesByKey { get; private set; }

    }

}

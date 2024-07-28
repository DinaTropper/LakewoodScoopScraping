using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System.Xml.Linq;

namespace LakewoodScoopScraper.Web.Services
{
    public class ScraperRepo
    {
        public List<Post> Scrape()
        {
            var html = GetHtml();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);

            IHtmlCollection<IElement> data = document.QuerySelectorAll("div.td-category-pos-image");

            List<Post> posts = new();

            foreach (IElement p in data)
            {
                Post post = new()
                {
                    Title = p.QuerySelector(".entry-title.td-module-title").TextContent,
                    Image = p.QuerySelector("span.entry-thumb").Attributes["data-img-url"].Value,
                    Blurb = p.QuerySelector(".td-excerpt").TextContent,
                    Url = p.QuerySelector("h3.td-module-title a").Attributes["href"].Value,
                    AmountOfComments = int.Parse(p.QuerySelector("span.td-module-comments").TextContent)
                };

                posts.Add(post);
            }
            return posts;
        }

        static private string GetHtml()
        {
            var url = $"https://thelakewoodscoop.com";
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
                UseCookies = true
            };
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US");
            return client.GetStringAsync(url).Result;
        }



    }
}


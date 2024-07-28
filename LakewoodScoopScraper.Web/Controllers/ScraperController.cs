using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LakewoodScoopScraper.Web.Services; 

namespace LakewoodScoopScraper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScraperController : ControllerBase
    {
        [Route("Scraper")]
        public List<Post> Scraper()
        {
            ScraperRepo repo = new();
            return repo.Scrape();
        }
    }
}

using AspNetCoreAngularSignalR.SignalRHubs;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAngularSignalR.Controllers
{
    [Produces("application/json")]
    [Route("api/News")]
    public class NewsController : Controller
    {
        private readonly NewsProvider _newsProvider;
        private readonly NewsHub _newsHub;

        public NewsController(NewsProvider newsProvider, NewsHub newsHub)
        {
            _newsProvider = newsProvider;
            _newsHub = newsHub;
        }

        [HttpPost]
        public IActionResult CreateNewsItem(NewsItem newsItem)
        {
            _newsProvider.CreateNewItem(newsItem);

            _newsHub.Send(newsItem.NewsGroup, newsItem);
            return Created("api/news", newsItem);
        }

        [HttpPost]
        [Route("addgroup")]
        public IActionResult AddGroup(string group)
        {
            if(string.IsNullOrEmpty(group))
            {
                return BadRequest("null");
            }
            _newsProvider.AddGroup(group);
            _newsHub.CreateGroup(group);
            return Created("api/news/addgroup", group);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_newsProvider.GetAllGroup());
        }
    }
}
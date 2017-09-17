using AspNetCoreAngularSignalR.SignalRHubs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreAngularSignalR.Controllers
{
    [Produces("application/json")]
    [Route("api/News")]
    public class NewsController : Controller
    {
        private readonly NewsProvider _newsProvider;
        private readonly NewssHub _newssHub;

        public NewsController(NewsProvider newsProvider, NewssHub newssHub)
        {
            _newsProvider = newsProvider;
            _newssHub = newssHub;
        }

        [HttpPost]
        public IActionResult CreateNewsItem(NewsItem newsItem)
        {
            _newsProvider.CreateNewItem(newsItem);

            _newssHub.Send(newsItem);
            return Created("api/news", newsItem);
        }

        [HttpPost]
        [Route("addgroup")]
        public async Task<IActionResult> AddGroupAsync(string group)
        {
            if(string.IsNullOrEmpty(group))
            {
                return BadRequest("null");
            }
            _newsProvider.AddGroup(group);
            await _newssHub.CreateGroup(group);
            return Created("api/news/addgroup", group);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_newsProvider.GetAllGroup());
        }
    }
}
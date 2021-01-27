using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChangFei.Route.Controllers
{
    [Route("")]
    [ApiController]
    public class FriendController:ControllerBase
    {
        [Route("")]
        [HttpGet]
        public Task GetFriends()
        {
            return Task.CompletedTask;
        }
    }
}

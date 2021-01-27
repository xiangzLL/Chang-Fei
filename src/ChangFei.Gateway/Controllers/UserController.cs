using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChangFei.Route.Controllers
{
    [ApiController]
    public class UserController:ControllerBase
    {
        public UserController()
        {

        }

        [HttpPost]
        public Task ChangeAvatar(string avatarData)
        {
            //Store image to oss
            return Task.CompletedTask;
        }
    }
}

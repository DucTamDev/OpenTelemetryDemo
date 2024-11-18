using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/[controler]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            
        }

        [Route("create")]
        [HttpPost]
        public bool CreateUser()
        {

        }
    }
}

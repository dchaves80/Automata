using AutomataServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutomataServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
       

      

       

        [HttpPost]
        [Route("Login")]
        public User EP_Login([FromBody] User usr) 
        {
            return Models.User.Login(usr);
        }
    }
}

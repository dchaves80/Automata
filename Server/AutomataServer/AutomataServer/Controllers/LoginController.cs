using AutomataServer.Models;
using AutomataServer.Models.recordsmodels;
using AutomataServer.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomataServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
       

      

       

        [HttpGet]
        [Route("Login")]
        public async Task<Users> EP_Login(string user, string password) 
        {
           Users u = await Users.User_Login(user,password);
            if (u.records != null && u.records.Count > 0 && u.records[0].banned == false)
            {
                List<User> userlist = new List<User>();
                userlist.Add(new User() { key = u.records[0].key });

                Users resultUser = new Users() { error = new Error() { has = false, exception = null }, records = userlist };

                return resultUser; 
            }
            else 
            {
                return new Users() { error = new Error() { has=true, exception="login inconrrecto" } };
            }
        }


    }
}

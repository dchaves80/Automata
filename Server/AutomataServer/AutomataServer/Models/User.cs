using System.ComponentModel.DataAnnotations;

namespace AutomataServer.Models
{
    public class User:BaseModel
    {
        public string user { get; set; }
        public string password { get; set; }

        public string guid { get; set; } = "000-000-000";
        
        public User():base()
        {
            
        }

        public static User Login(User p_user) 
        {
            if (p_user.user == "edering" && p_user.password == "cloverfield161185")
            {
                return new User()
                {
                    user = p_user.user,
                    password = "******",
                    guid = Guid.NewGuid().ToString()
                };
            }
            else 
            {
                return new User() { hasError = true, error = "Problema con Login" };
            }
        }
    }
}

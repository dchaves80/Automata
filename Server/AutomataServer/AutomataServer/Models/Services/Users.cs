using AutomataServer.Models.recordsmodels;
using Newtonsoft.Json;

namespace AutomataServer.Models.Services
{
    public class Users:BaseHttpToSqlResponse
    {
        public List<User>? records;

        public static async Task<Users> GetUserByUserName_Password(string username, string password)
        {

            using (HttpClient client = new HttpClient())
            {
                string result = await sqlQuery($"select *  from Users where username='{username}' and password='{password}'");

                Users data = JsonConvert.DeserializeObject<Users>(result);

                return data.error.has == true ? null : data;
            }


        }

    }
}

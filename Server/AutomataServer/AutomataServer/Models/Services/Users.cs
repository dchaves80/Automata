using AutomataServer.Models.recordsmodels;
using Newtonsoft.Json;

namespace AutomataServer.Models.Services
{


    public class AccesKeys : BaseHttpToSqlResponse 
    {
        public List<AccessKey>? records;
    }

    public class Users : BaseHttpToSqlResponse
    {
        public List<User>? records { get; set; }
        

        public static async Task<Users> User_Login(string username, string password)
        {

            using (HttpClient client = new HttpClient())
            {
                string result = await sqlQuery($"select *  from Users where username='{username}' and password='{password}'");

                Users data = JsonConvert.DeserializeObject<Users>(result);

                if (data != null && data.records.Count > 0)
                {

                    using (HttpClient subclient = new HttpClient())
                    {
                        string resultKey = await sqlQuery($"exec NewKey {data.records[0].id.ToString()}");
                        AccesKeys keyData = JsonConvert.DeserializeObject<AccesKeys>(resultKey);
                        if (keyData != null && keyData.records != null && keyData.records.Count > 0) 
                        {
                            data.records[0].key = keyData.records[0].accesskey;
                        }
                        

                    }
                }

                return data.error.has == true ? null : data;
            }


        }

    }
}

using Microsoft.Extensions.Configuration;

namespace AutomataServer.Models
{

      public class BaseHttpToSqlResponse
    {
        public Error? error { get; set; }

        protected static async Task<string> sqlQuery(string query) 
        {
            string result = null;
            using (HttpClient client = new HttpClient()) { 
            HttpResponseMessage rm = await client.GetAsync($"{GetConnectionStringBase()}&sql={query}");
            result = await rm.Content.ReadAsStringAsync();
            }
            return result;
        }

        private static string GetConnectionStringBase()
        {
            IConfigurationRoot _configuration;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();

            IConfigurationSection MainRoot = _configuration.GetSection("HttpToSql");
            IConfigurationSection DefaultConnection = MainRoot.GetSection("DefaultConnection");
            IConfigurationSection DatabaseConnection = DefaultConnection.GetSection("DatabaseConnection");

            string? dB_host = DatabaseConnection.GetValue<string>("host");
            string? dB_database = DatabaseConnection.GetValue<string>("database");
            string? dB_user = DatabaseConnection.GetValue<string>("user");
            string? dB_password = DatabaseConnection.GetValue<string>("password");

            string? ApiURL = DefaultConnection.GetValue<string>("ApiURL");
            string resultString = $"{ApiURL}?user={dB_user}&pass={dB_password}&host={dB_host}&db={dB_database}";
            return resultString;
        }

    }
}

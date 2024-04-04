using Microsoft.AspNetCore.Mvc;
using System.Net.Security;

namespace SQLToHttp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectorController : ControllerBase
    {
        [HttpGet]
        [Route("SQL")]
        public dynamic SQL(string sql, string user, string pass, string host, string db) 
        {

            return DataConnector.SQL.GET(sql,user,pass,host,db);
        }


    }
}
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Security.Cryptography;

namespace SQLToHttp.DataConnector
{
    public class SQL:DbContext
    {

        public virtual DbSet<dynamic> DynamicObject { get; set; }

        public SQL() { }
        public SQL(DbContextOptions<SQL> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
            optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings").GetValue<string>("SQL"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<dynamic>(entity=>entity.HasNoKey());
        }

        public static dynamic GET(string sql, string user, string pass, string host, string db) 
        {
            Console.WriteLine($"Executing:{sql}");
            List<Dictionary<string, dynamic>> list = new List<Dictionary<string,dynamic>>();
            
            using (SQL ctx = new SQL())
            {
                ctx.Database.SetConnectionString($"Server={host}; Initial Catalog={db}; Persist Security Info=False; User ID={user}; Password={pass}; MultipleActiveResultSets=False");
                try
                {
                    ctx.Database.OpenConnection();
                    DbCommand command = ctx.Database.GetDbConnection().CreateCommand();
                    command.CommandText = sql;
                    DbDataReader reader = command.ExecuteReader();
                    long Campos = reader.FieldCount;
                    
                    while (reader.Read())
                    {
                        Dictionary<string,dynamic> record = new Dictionary<string,dynamic>();
                        for (int a = 0; a < Campos; a++)
                        {
                            record.Add(reader.GetName(a), reader[a]);
                        }
                        list.Add(record);
                    }
                    ctx.Database.CloseConnection();
                    dynamic result = new { error = new { has = false }, records= list };
                    return result;
                }
                catch (SqlException ex) 
                {
                    dynamic result = new { error = new { has = true, exception=$"Message:{ex}, Stack:{ex.StackTrace}" }, };
                    return result;
                }
                
                
                
            }
        }


    }
}

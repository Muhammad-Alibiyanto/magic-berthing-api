using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing
{
	public class Extension
	{
        public static IDbConnection GetConnection(int i = 0)
        {
            string str = "";
            if (i == 0)
            {
                str = "VASA19c";
            }
            else if(i == 1)
            {
                str = "POCC19c";
            }
            else if (i == 2)
            {
                str = "Spiner";
            }
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            IConfigurationRoot configuration = builder.Build();
            IConfigurationSection configurationSection = configuration.GetSection("ConnectionStrings").GetSection(str);
            IDbConnection conn = new OracleConnection(configurationSection.Value.ToString());
            if (conn.State.Equals(ConnectionState.Closed))
                conn.Open();
            return conn;
        }
    }
}

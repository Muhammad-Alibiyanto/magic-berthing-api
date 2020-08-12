using Dapper;
using magicBerthing.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring
{
	public class MonPermohonanDL
	{
        public IEnumerable<vwSidaradina> GetDataListSidaradina(string nmKapal)
        {
            IEnumerable<vwSidaradina> result = null;
            using (IDbConnection connection = Extension.GetConnection())
            {
                try
                {
                    string sql = @"SELECT NAMA_KAPAL FROM SIDARADINA
                        WHERE NAMA_KAPAL=:NAMA_KAPAL";
                    result = connection.Query<vwSidaradina>(sql, new
                    {
                        NAMA_KAPAL = nmKapal
                    });
                }
                catch (Exception)
                {
                    result = null;
                }
            }
            return result;
        }
    }
}

using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static magicBerthing.Models.Master.MasRegionalModel;

namespace magicBerthing.DataLogics.Master
{
	public class MasRegionalDL
	{
        public RegionalModel getRegional()
        {

            RegionalModel hasil = new RegionalModel();
            IEnumerable<RegionalData> result = null;
            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    string sql = @"select * from VW_REGIONAL_POCC ";
                    result = connection.Query<RegionalData>(sql, new
                    {
                        KD_REGIONAL = ""
                    });


                    hasil.count = result.Count();
                    hasil.data = result.ToList();
                    hasil.message = "Succes";
                    hasil.status = "S";

                }
                catch (Exception e)
                {
                    // result = null;
                    hasil.count = 0;
                    hasil.message = e.Message;
                    hasil.status = "E";
                }
            }
            return hasil;
        }
    }
}

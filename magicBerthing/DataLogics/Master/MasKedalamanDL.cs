using Dapper;
using magicBerthing.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Master
{
    public class MasKedalamanDL
    {
        public IEnumerable<Kedalaman> getKedalaman(ParamKedalaman paramKedalaman)
        {
            IEnumerable<Kedalaman> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    string paramKodeRegional = "";
                    if (!string.IsNullOrEmpty(paramKedalaman.kd_regional) && paramKedalaman.kd_regional != "string")
                    {
                        paramKodeRegional = " WHERE KD_REGIONAL ='" + paramKedalaman.kd_regional + "'";
                    }

                    string paramKdCabang = "";
                    if (!string.IsNullOrEmpty(paramKedalaman.kd_cabang) && paramKedalaman.kd_cabang!= "string")
                    {
                        paramKdCabang = " AND KD_CABANG ='" + paramKedalaman.kd_cabang+ "'";

                    }

                    string paramTgl = "";
                    if (!string.IsNullOrEmpty(paramKedalaman.tgl) && paramKedalaman.tgl != "string")
                    {
                        paramTgl = " AND TO_CHAR(TGL, 'YYYY-MM-DD')='" + paramKedalaman.tgl + "'";
                    }

                    string paramJam = "";
                    if (!string.IsNullOrEmpty(paramKedalaman.jam) && paramKedalaman.jam != "string")
                    {
                        paramJam = " AND JAM='" + paramKedalaman.jam + "'";
                    }

                    string sql = "";
                    sql = @"SELECT * FROM PASANG_SURUT " + paramKodeRegional + paramKdCabang + paramTgl + paramJam;

                    result = connection.Query<Kedalaman>(sql, new
                    {
                        KD_REGIONAL = paramKedalaman.kd_regional
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

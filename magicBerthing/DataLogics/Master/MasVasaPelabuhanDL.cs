using Dapper;
using magicBerthing.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Master
{
    public class MasVasaPelabuhanDL
    {
        public IEnumerable<VasaPelabuhanData> getDataVasaPelabuhan(ParamVasaPelabuhan paramVasaPelabuhan)
        {
            IEnumerable<VasaPelabuhanData> result = null;

            using (IDbConnection connection = Extension.GetConnection(0))
            {
                try
                {

                    /*  string fnoPermohonan = "";
                      if (!string.IsNullOrEmpty(NoPermohonan) && NoPermohonan != "7")
                      {
                          fnoPermohonan = "   AND A.NO_PERMOHONAN='" + NoPermohonan + "'  ";
                      }




      */

                    string paramKodePelabuhan = "";
                    if (!string.IsNullOrEmpty(paramVasaPelabuhan.kode_pelabuhan) && paramVasaPelabuhan.kode_pelabuhan!= "string")
                    {
                        if (paramVasaPelabuhan.is_search == false)
                        {
                            paramKodePelabuhan = " WHERE MPLB_KODE='" + paramVasaPelabuhan.kode_pelabuhan + "'";
                        }
                    }

                    string paramNamaPelabuhan = "";
                    if (!string.IsNullOrEmpty(paramVasaPelabuhan.nama_pelabuhan) && paramVasaPelabuhan.nama_pelabuhan != "string")
                    {
                        if (paramVasaPelabuhan.is_search == true)
                        {
                            paramNamaPelabuhan = " WHERE MPLB_NAMA LIKE '" + paramVasaPelabuhan.nama_pelabuhan + "%' OR MPLB_KOTA LIKE '" + paramVasaPelabuhan.nama_pelabuhan + "%'";
                        }
                        else
                        {
                            paramNamaPelabuhan = " AND MPLB_NAMA LIKE '" + paramVasaPelabuhan.nama_pelabuhan + "%'  OR MPLB_KOTA LIKE '" + paramVasaPelabuhan.nama_pelabuhan + "%'";
                        }
                    }

                    string sql = @"SELECT * FROM (
                                    SELECT 
                                    MPLB_KODE KODE_PELABUHAN, 
                                    MPLB_NAMA NAMA_PELABUHAN, 
                                    MPLB_KOTA KOTA,
                                    MNEG_KODE KODE_NEGARA, KD_AKTIF
                                    FROM MASTERDATA.UPKM_PELABUHAN " +
                                    paramKodePelabuhan + paramNamaPelabuhan +
                                   ") WHERE ROWNUM <= 10 AND KD_AKTIF='A'";

                    result = connection.Query<VasaPelabuhanData>(sql);

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

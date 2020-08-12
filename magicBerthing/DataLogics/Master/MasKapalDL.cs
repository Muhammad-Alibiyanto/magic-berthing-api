using Dapper;
using magicBerthing.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Master
{
    public class MasKapalDL
    {
        public IEnumerable<KapalData> getDataKapal(ParamKapal paramKapal)
        {
            IEnumerable<KapalData> result = null;

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

                    string paramNamaKapal = "";
                    if (!string.IsNullOrEmpty(paramKapal.nama_kapal) && paramKapal.nama_kapal != "string")
                    {
                        paramNamaKapal = " WHERE MKPL_NAMA ='" + paramKapal.nama_kapal + "'";
                    }

                    string paramSearchNamaKapal = "";
                    if (!string.IsNullOrEmpty(paramKapal.search_nama_kapal) && paramKapal.search_nama_kapal!= "string")
                    {
                        paramSearchNamaKapal = " WHERE MKPL_NAMA LIKE '" + paramKapal.search_nama_kapal + "%'";
                    }

                    string sql = @"SELECT * FROM (
                                    SELECT 
                                    MKPL_KODE KODE_KAPAL, 
                                    MKPL_NAMA NAMA_KAPAL, 
                                    MKPL_JENIS JENIS_KAPAL,
                                    MKPL_GRT GRT,
                                    MKPL_LOA LOA,
                                    MKPL_BENDERA BENDERA
                                    FROM MASTERDATA.UPKM_KAPAL " +
                                    paramNamaKapal + paramSearchNamaKapal + " AND KD_AKTIF='A'" +
                                 ") WHERE ROWNUM <= 10";

                    result = connection.Query<KapalData>(sql);

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

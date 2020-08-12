using Dapper;
using magicBerthing.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Master
{
    public class MasPelabuhanDL
    {
        public IEnumerable<PelabuhanData> getDataPelabuhan()
        {
            IEnumerable<PelabuhanData> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    string sql = @"SELECT PARAM_1 KODE_CABANG, PARAM_3 NAMA_PELABUHAN, KD_REGIONAL FROM RAMP_PARAM_D WHERE RAMP_PARAM_ID = 'CABANG_VASA' ORDER BY NAMA_PELABUHAN ASC";

                    result = connection.Query<PelabuhanData>(sql);

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

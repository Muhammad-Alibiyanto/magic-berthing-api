using Dapper;
using magicBerthing.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Master
{
    public class MasKomoditiDL
    {
        public IEnumerable<KomoditiData> getKomoditiData(ParamKomoditi paramKomoditi)
        {
            IEnumerable<KomoditiData> result = null;

            using (IDbConnection connection = Extension.GetConnection(0))
            {
                try
                {
                    string paramSearch = "";
                    if (!string.IsNullOrEmpty(paramKomoditi.search) && paramKomoditi.search != "string")
                    {
                        paramSearch = " WHERE NAMA LIKE '" + paramKomoditi.search.ToUpper() + "%'";
                    }

                    string sql = @"SELECT DISTINCT NAMA,  
                                (CASE WHEN NAMA = 'GENERAL CARGO' OR NAMA = 'GENERAL CARGO (GC)' OR NAMA = 'GC' OR NAMA = 'MOBIL'  OR NAMA = 'SEPEDA MOTOR' THEN 'GC'
                                WHEN NAMA = 'CURAH CAIR' THEN 'CURAH CAIR'
                                WHEN NAMA = 'CURAH KERING' THEN 'CURAH KERING'
                                WHEN NAMA = 'BESI PRODUKSI' OR NAMA = 'HEWAN' OR NAMA = 'UNITIZED' OR NAMA = 'DRILLING MATERIAL' 
                                OR NAMA = 'SEMBAKO' OR NAMA = 'KAYU MASAK' OR NAMA = 'BUNKER' OR NAMA = 'COMBO WINDOWS' OR NAMA = 'COMBO NON WINDOWS'
                                OR NAMA = 'BAG CARGO' OR NAMA = 'CREW SERVICE' OR NAMA = 'MULTIPURPOSE' THEN 'UNITIZED'
                                WHEN NAMA = 'UMUM' THEN 'UMUM'
                                WHEN NAMA = 'PENUMPANG' THEN 'PENUMPANG'
                                WHEN NAMA = 'MUATAN PENUMPANG' THEN 'UNITIZED'
                                WHEN NAMA = 'PETI KEMAS' OR NAMA = 'PETI KEMAS NON WINDOWS' OR NAMA = 'PETIKEMAS' OR NAMA = 'KONTAINER' THEN 'PETIKEMAS'
                                END) KATEGORI
                                FROM (SELECT ID, UPPER(NAMA) NAMA 
                                FROM VASA.CLUSTERING)" + paramSearch + " ORDER BY NAMA ASC";

                    result = connection.Query<KomoditiData>(sql);

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

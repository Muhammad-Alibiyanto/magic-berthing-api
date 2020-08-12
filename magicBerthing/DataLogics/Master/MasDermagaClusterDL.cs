using Dapper;
using magicBerthing.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Master
{
    public class MasDermagaClusterDL
    {
        public IEnumerable<DermagaClusterData> getDataArea(ParamDermagaCluster paramDermagaCluster)
        {
            IEnumerable<DermagaClusterData> result = new List<DermagaClusterData>();

            using (IDbConnection connection = Extension.GetConnection(0))
            {
                try
                {
                    string paramKodeDermaga = "";
                    if (!string.IsNullOrEmpty(paramDermagaCluster.kode_dermaga) && paramDermagaCluster.kode_dermaga!= "string")
                    {
                        paramKodeDermaga = "WHERE KODE_DERMAGA = '" + paramDermagaCluster.kode_dermaga + "'";
                    }

                    string query = "SELECT * FROM (SELECT DISTINCT A.KODE_CABANG, A.KODE_DERMAGA, D.MDMG_JENIS_DMG JENIS_DERMAGA, D.MDMG_NAMA NAMA_DERMAGA, MIN (A.KADE_AWAL) KADE_AWAL, MAX (A.KADE_AKHIR) KADE_AKHIR, B.NAMA, B.NAMA_CLUSTER " +
                                    "FROM VASA.CLUSTERING_DERMAGA A, (SELECT ID, NAMA, (CASE WHEN NAMA = 'GENERAL CARGO' OR NAMA = 'GENERAL CARGO (GC)' OR NAMA = 'GC' OR NAMA = 'MOBIL'  OR NAMA = 'SEPEDA MOTOR' THEN 'GC' WHEN NAMA = 'CURAH CAIR' THEN 'CURAH CAIR' WHEN NAMA = 'CURAH KERING' THEN 'CURAH KERING' WHEN NAMA = 'BESI PRODUKSI' OR NAMA = 'HEWAN' OR NAMA = 'UNITIZED' OR NAMA = 'DRILLING MATERIAL' OR NAMA = 'SEMBAKO' OR NAMA = 'KAYU MASAK' OR NAMA = 'BUNKER' OR NAMA = 'COMBO WINDOWS' OR NAMA = 'COMBO NON WINDOWS' OR NAMA = 'BAG CARGO' OR NAMA = 'CREW SERVICE' OR NAMA = 'MULTIPURPOSE' THEN 'UNITIZED' WHEN NAMA = 'UMUM' THEN 'UMUM' WHEN NAMA = 'PENUMPANG' THEN 'PENUMPANG' WHEN NAMA = 'MUATAN PENUMPANG' THEN 'UNITIZED' WHEN NAMA = 'PETI KEMAS' OR NAMA = 'PETI KEMAS NON WINDOWS' OR NAMA = 'PETIKEMAS' OR NAMA = 'KONTAINER' THEN 'PETIKEMAS' END) NAMA_CLUSTER " +
                                    "FROM(SELECT ID, UPPER(NAMA) NAMA FROM VASA.CLUSTERING)) B, MASTERDATA.UPKM_DERMAGA D " +
                                    "WHERE A.CLUSTERING_ID = B.ID AND A.KODE_CABANG = D.KD_CABANG AND D.MDMG_JENIS_DMG = 'DMGUMUM' AND A.KODE_DERMAGA = D.MDMG_KODE AND A.KODE_CABANG = " + paramDermagaCluster.kode_cabang +
                                    " GROUP BY A.KODE_CABANG, A.KODE_DERMAGA, D.MDMG_JENIS_DMG, D.MDMG_NAMA, B.NAMA, B.NAMA_CLUSTER) " +
                                    paramKodeDermaga + " ORDER BY KODE_CABANG, KODE_DERMAGA, KADE_AWAL ASC";

                    result = connection.Query<DermagaClusterData>(query).ToList();
                }
                catch (Exception e)
                {
                    result = null;
                }
            }

            return result;
        }
    }
}

using Dapper;
using magicBerthing.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring
{
    public class MonTerminalDetailKegiatanExportDL
    {
        public IEnumerable<TerminalDetailKegiatanExport> getDataTerminalDetailKegiatanExport(ParamTerminalDetailKegiatanExport paramTerminalDetailKegiatanExport)
        {
            IEnumerable<TerminalDetailKegiatanExport> result = null;

            try
            {

                string paramNoPMHBH = "";
                if (!string.IsNullOrEmpty(paramTerminalDetailKegiatanExport.no_pmh_bm) && paramTerminalDetailKegiatanExport.no_pmh_bm != "string")
                {
                    paramNoPMHBH = " WHERE NO_PMH_BM='" + paramTerminalDetailKegiatanExport.no_pmh_bm + "'";
                }

                string paramKegiatan = "";
                if (!string.IsNullOrEmpty(paramTerminalDetailKegiatanExport.kegiatan) && paramTerminalDetailKegiatanExport.kegiatan != "string")
                {
                    paramKegiatan = " AND KEGIATAN='" + paramTerminalDetailKegiatanExport.kegiatan + "'";
                }

                string paramKodeBarang = "";
                if (!string.IsNullOrEmpty(paramTerminalDetailKegiatanExport.kd_barang) && paramTerminalDetailKegiatanExport.kd_barang != "string")
                {
                    paramKodeBarang = " AND KD_BARANG='" + paramTerminalDetailKegiatanExport.kd_barang + "'";
                }

                if (paramTerminalDetailKegiatanExport.tipe == "CARGO")
                {
                    using (IDbConnection connection = Extension.GetConnection(2))
                    {
                        string sql = "SELECT * FROM VW_MAGIC_DETAIL_BARANG_CARGO" + paramNoPMHBH + paramKegiatan + paramKodeBarang;
                        result = connection.Query<TerminalDetailKegiatanExport>(sql, new
                        {
                            NO_PMH_BH = paramTerminalDetailKegiatanExport.no_pmh_bm
                        });
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}

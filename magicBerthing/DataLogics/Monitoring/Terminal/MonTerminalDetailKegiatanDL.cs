using Dapper;
using magicBerthing.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring
{
    public class MonTerminalDetailKegiatanDL
    {
        public IEnumerable<TerminalDetailKegiatan> getDataTerminalDetailKegiatan(ParamTerminalDetailKegiatan paramTerminalDetailKegiatan)
        {
            IEnumerable<TerminalDetailKegiatan> result = null;

            try
            {
                string paramNoPMHBM = "";
                if (!string.IsNullOrEmpty(paramTerminalDetailKegiatan.no_pmh_bm) && paramTerminalDetailKegiatan.no_pmh_bm != "string")
                {
                    paramNoPMHBM = " WHERE NO_PMH_BM='" + paramTerminalDetailKegiatan.no_pmh_bm + "'";
                }

                string paramKegiatan = "";
                if (!string.IsNullOrEmpty(paramTerminalDetailKegiatan.kegiatan) && paramTerminalDetailKegiatan.kegiatan != "string")
                {
                    paramKegiatan = " AND KEGIATAN='" + paramTerminalDetailKegiatan.kegiatan + "'";
                }

                string sql = "";
                if (paramTerminalDetailKegiatan.tipe == "CARGO")
                {
                    sql = "SELECT A.*, JMLH_PLAN, JMLH_TRUCK_PLAN FROM (SELECT KD_CABANG, KD_TERMINAL, (CASE WHEN KAWASAN = 'NILAM' OR KAWASAN = 'MIRAH' THEN 'NILAMMIRAH' ELSE KAWASAN END) NAMA_TERMINAL, NO_PMH_BM, NAMA_KAPAL, KEGIATAN, KD_BARANG, NAMA_BARANG, NAMA_DERMAGA, SUM(JUMLAH) JUMLAH, KD_SATUAN, SUM(JML_TRUCK) JML_TRUK, NM_PBM" +
                          " FROM(SELECT KD_CABANG, KD_TERMINAL, KAWASAN, NO_PMH_BM, NAMA_KAPAL, NM_PBM, KD_BARANG, MBRG_NAMA NAMA_BARANG, SHIFT, HARI, KEGIATAN, NAMA_DERMAGA, ALAT1, ALAT2, ALAT3, ALAT4, JUMLAH, KD_SATUAN, NVL(JML_TRUCK, 0) JML_TRUCK, JAM_MULAI, JAM_SELESAI, TOSGC.func_startwork_info(NO_PMH_BM, KD_CABANG, KEGIATAN) START_WORK, TOSGC.func_endkegiatan(kd_cabang, kd_terminal, no_pmh_bm, kegiatan) END_WORK, KD_REGIONAL FROM(SELECT A.*, B.NO_PMH_BM, E.PARAM_6 KAWASAN, B.NM_PBM, B.KEGIATAN, C.MBRG_NAMA, D.NAMA_KAPAL, TOSGC.func_getdermaga(A.KD_CABANG, A.KD_DERMAGA) NAMA_DERMAGA, E.KD_REGIONAL FROM TOSGC.TOSGCT_TALLY A, (SELECT B.*, C.NM_PBM FROM TOSGC.tosgct_pntp b, TOSGC.V_LIST_PBM_SAPUJAGAT C WHERE STATUS <> 'BATAL' AND B.NO_PMH_BM = C.NO_PMH_BM AND B.PBM = C.PBM AND B.NO_SPK = C.NO_SPK) B, TOSGC.TOSGCM_BARANG C, (SELECT TPPKB1_NOMOR, TPPKB1_NAMA_KAPAL NAMA_KAPAL FROM TOSGC.V_PPKB1_PMH_KAPAL_ALL) D, VASA.MASTER_PARAMETER_POCC E WHERE A.NO_SPK = B.NO_SPK AND A.KD_BARANG = C.MBRG_KODE AND A.KD_CABANG = E.PARAM_1 AND A.KD_TERMINAL = E.PARAM_2 AND E.PARAMETER_ID = 'MASTER_TERMINAL' AND B.NO_PMH_BM = D.TPPKB1_NOMOR) WHERE NO_PMH_BM = '" + paramTerminalDetailKegiatan.no_pmh_bm + "' AND KEGIATAN = '" + paramTerminalDetailKegiatan.kegiatan + "'" +
                          " ORDER BY KD_CABANG, KD_TERMINAL ASC) GROUP BY KAWASAN, NO_PMH_BM, NAMA_KAPAL, KD_BARANG, NAMA_BARANG, NAMA_DERMAGA, KD_SATUAN, KEGIATAN, KD_CABANG, KD_TERMINAL, NM_PBM) A, (SELECT A.KD_CABANG, A.KD_TERMINAL, A.NO_PMH_BM, A.KEGIATAN, B.KD_BARANG, C.MBRG_NAMA, SUM(B.JML_PLAN) JMLH_PLAN, B.KD_SATUAN, SUM(B.JML_TRUK) JMLH_TRUCK_PLAN FROM TOSGC.TOSGCT_PNTP A, TOSGC.TOSGCT_OP_D B, TOSGC.TOSGCM_BARANG C" +
                          " WHERE A.STATUS <> 'BATAL' AND A.NO_OP = B.NO_OP AND B.KD_BARANG = C.MBRG_KODE(+) GROUP BY A.KD_CABANG, A.KD_TERMINAL, A.NO_PMH_BM, A.KEGIATAN, B.KD_BARANG, C.MBRG_NAMA, B.KD_SATUAN) B WHERE A.NO_PMH_BM = B.NO_PMH_BM AND A.KEGIATAN = B.KEGIATAN AND A.NAMA_BARANG = B.MBRG_NAMA";
                    using (IDbConnection connection = Extension.GetConnection(0))
                    {
                        result = connection.Query<TerminalDetailKegiatan>(sql);
                    }
                }
                if (paramTerminalDetailKegiatan.tipe == "CONTAINER")
                {
                    sql = "SELECT * FROM VW_MAGIC_TRMNL_INFO_CONTAINER" + paramNoPMHBM + paramKegiatan;
                    using (IDbConnection connection = Extension.GetConnection(1))
                    {
                        result = connection.Query<TerminalDetailKegiatan>(sql, new
                        {
                            NO_PMH_BM = paramTerminalDetailKegiatan.no_pmh_bm
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

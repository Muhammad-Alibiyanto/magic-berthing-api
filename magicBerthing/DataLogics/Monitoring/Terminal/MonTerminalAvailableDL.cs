using Dapper;
using magicBerthing.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring
{
    public class MonTerminalAvailableDL
    {
        public IEnumerable<TerminalAvailable> getDataTerminalAvailabe(ParamTerminal paramTerminal)
        {
            IEnumerable<TerminalAvailable> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    /*  string fnoPermohonan = "";
                      if (!string.IsNullOrEmpty(NoPermohonan) && NoPermohonan != "7")
                      {
                          fnoPermohonan = "   AND A.NO_PERMOHONAN='" + NoPermohonan + "'  ";
                      }



      */
                    string paramKodeRegional = "";
                    if (!string.IsNullOrEmpty(paramTerminal.kd_regional) && paramTerminal.kd_regional != "string")
                    {
                        paramKodeRegional = " WHERE KD_REGIONAL ='" + paramTerminal.kd_regional + "'";
                    }

                    string paramKdCabangInduk = "";
                    if (!string.IsNullOrEmpty(paramTerminal.kd_cabang_induk) && paramTerminal.kd_cabang_induk != "string")
                    {
                        paramKdCabangInduk = " AND KD_CABANG_INDUK='" + paramTerminal.kd_cabang_induk+ "'";
                    }

                    string paramKdCabang = "";
                    if (!string.IsNullOrEmpty(paramTerminal.kd_cabang) && paramTerminal.kd_cabang != "string")
                    {
                        paramKdCabang = " AND KD_CABANG='" + paramTerminal.kd_cabang + "'";
                    }

                    string paramKdTerminal = "";
                    if (!string.IsNullOrEmpty(paramTerminal.kd_terminal) && paramTerminal.kd_terminal != "string")
                    {
                        paramKdTerminal = " AND KD_TERMINAL='" + paramTerminal.kd_terminal+ "'";
                    }

                    string paramStatus = "";
                    if (!string.IsNullOrEmpty(paramTerminal.status) && paramTerminal.status != "string")
                    {
                        if (!string.IsNullOrEmpty(paramTerminal.kd_regional) && paramTerminal.kd_regional != "string")
                        {
                            paramStatus = " AND STATUS='" + paramTerminal.status + "'";
                        }
                        else
                        {
                            paramStatus = " WHERE STATUS='" + paramTerminal.status + "'";
                        }
                    }

                    string paramType = "";
                    if (!string.IsNullOrEmpty(paramTerminal.type_terminal) && paramTerminal.type_terminal!= "string")
                    {
                        if (paramTerminal.type_terminal == "PENUMPANG")
                        {
                            if (!string.IsNullOrEmpty(paramTerminal.kd_regional) && paramTerminal.kd_regional != "string")
                            {
                                paramType = " AND JENIS_KAPAL IN ('KPLPNMPANG', 'KPLRORO', 'KPLCRUISE')";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(paramTerminal.status) && paramTerminal.status != "string")
                                {
                                    paramType = " AND JENIS_KAPAL IN ('KPLPNMPANG', 'KPLRORO', 'KPLCRUISE')";
                                }
                                else
                                {
                                    paramType = " WHERE JENIS_KAPAL IN ('KPLPNMPANG', 'KPLRORO', 'KPLCRUISE')";
                                }
                            }
                        }
                    }

                    string paramJenisKapal = "";
                    if (!string.IsNullOrEmpty(paramTerminal.jenis_kapal) && paramTerminal.jenis_kapal!= "string")
                    {
                        paramJenisKapal = " AND JENIS_KAPAL='" + paramTerminal.jenis_kapal + "'";
                    }

                    string paramLokasi = "";
                    if (!string.IsNullOrEmpty(paramTerminal.lokasi) && paramTerminal.lokasi != "string")
                    {
                        paramLokasi = " AND NAMA_LOKASI='" + paramTerminal.lokasi + "'";
                    }

                    string paramKegiatan = "";
                    if (!string.IsNullOrEmpty(paramTerminal.kegiatan) && paramTerminal.kegiatan != "string")
                    {
                        paramKegiatan = " AND KEGIATAN='" + paramTerminal.kegiatan+ "'";
                    }

                    string paramOrderby = "";
                    if (!string.IsNullOrEmpty(paramTerminal.order_by_column) && paramTerminal.order_by_column != "string" && !string.IsNullOrEmpty(paramTerminal.order_by_sort) && paramTerminal.order_by_sort != "string")
                    {
                        paramOrderby = " ORDER BY " + paramTerminal.order_by_column + " " + paramTerminal.order_by_sort;
                    }

                    string paramSearch = "";
                    if (paramTerminal.is_searching == true && !string.IsNullOrEmpty(paramTerminal.search_key) && paramTerminal.search_key != "string")
                    {
                        paramSearch = " WHERE NAMA_KAPAL LIKE '%" + paramTerminal.search_key + "%'";
                    }

                    string paramNoPPKJasa = "";
                    if (!string.IsNullOrEmpty(paramTerminal.no_ppk_jasa) && paramTerminal.no_ppk_jasa != "string")
                    {
                        paramNoPPKJasa = " AND NO_PPK_JASA = '" + paramTerminal.no_ppk_jasa + "'";
                    }

                    string sql = "SELECT * FROM(" +
                        "SELECT * FROM (SELECT A.*, B.REGIONAL_NAMA NAMA_REGIONAL, " +
                        "(CASE WHEN A.TGL_MULAI IS NULL AND A.TGL_SELESAI IS NULL AND A.STATUS_NOTA=0 THEN 'RENCANA' " +
                        "WHEN A.TGL_MULAI IS NOT NULL AND A.TGL_SELESAI IS NULL AND A.STATUS_NOTA=0 THEN 'SANDAR' " +
                        "WHEN A.TGL_MULAI IS NOT NULL AND A.TGL_SELESAI IS NOT NULL OR A.STATUS_NOTA=1 THEN 'HISTORY' END" +
                        ") STATUS " +
                        "FROM VW_MAGIC_TERMINAL_INFO_HOME A, APP_REGIONAL B WHERE A.KD_REGIONAL=B.ID AND B.PARENT_ID IS NULL AND B.ID NOT IN (12300000,20300001) ) " + paramKodeRegional + paramKdCabangInduk + paramKdCabang + paramKdTerminal + paramStatus + paramType + paramJenisKapal + paramLokasi + paramKegiatan + paramNoPPKJasa + paramOrderby + 
                    ") " + paramSearch;

                    result = connection.Query<TerminalAvailable>(sql, new
                    {
                        KD_REGIONAL = paramTerminal.kd_regional
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

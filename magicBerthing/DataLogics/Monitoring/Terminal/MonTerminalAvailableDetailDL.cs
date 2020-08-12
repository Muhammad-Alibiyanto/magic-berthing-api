using Dapper;
using magicBerthing.Models.Monitoring;
using magicBerthing.Models.Monitoring.TerminalModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring.Terminal
{
    public class MonTerminalAvailableDetailDL
    {
        //public IEnumerable<TerminalDetail> getDataTerminalDetailAvailabe(ParamTerminalDetail paramTerminalDetail)
        public List<TerminalDetail> getDataTerminalDetailAvailabe(ParamTerminalDetail paramTerminalDetail)
        {
            List<TerminalDetail> result = new List<TerminalDetail>();
            IEnumerable<TerminalDetail> terminal = null;
            List<string> temp = new List<string>();
            List<TerminalDetail> reuse = new List<TerminalDetail>();

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
                    if (!string.IsNullOrEmpty(paramTerminalDetail.kd_regional) && paramTerminalDetail.kd_regional != "string")
                    {
                        paramKodeRegional = " WHERE KD_REGIONAL ='" + paramTerminalDetail.kd_regional + "'";
                    }

                    string paramKdCabangInduk = "";
                    if (!string.IsNullOrEmpty(paramTerminalDetail.kd_cabang_induk) && paramTerminalDetail.kd_cabang_induk != "string")
                    {
                        paramKdCabangInduk = " AND KD_CABANG_INDUK='" + paramTerminalDetail.kd_cabang_induk + "'";
                    }

                    string paramKdCabang = "";
                    if (!string.IsNullOrEmpty(paramTerminalDetail.kd_cabang) && paramTerminalDetail.kd_cabang != "string")
                    {
                        paramKdCabang = " AND KD_CABANG='" + paramTerminalDetail.kd_cabang + "'";
                    }

                    string paramKdTerminal = "";
                    if (!string.IsNullOrEmpty(paramTerminalDetail.kd_terminal) && paramTerminalDetail.kd_terminal != "string")
                    {
                        paramKdTerminal = " AND KD_TERMINAL='" + paramTerminalDetail.kd_terminal + "'";
                    }

                    string paramStatus = "";
                    if (!string.IsNullOrEmpty(paramTerminalDetail.status) && paramTerminalDetail.status != "string")
                    {
                        if (!string.IsNullOrEmpty(paramTerminalDetail.kd_regional) && paramTerminalDetail.kd_regional != "string")
                        {
                            paramStatus = " AND STATUS='" + paramTerminalDetail.status + "'";
                        }
                        else
                        {
                            paramStatus = " WHERE STATUS='" + paramTerminalDetail.status + "'";
                        }
                    }

                    string paramType = "";
                    if (!string.IsNullOrEmpty(paramTerminalDetail.type_terminal) && paramTerminalDetail.type_terminal != "string")
                    {
                        if (paramTerminalDetail.type_terminal == "PENUMPANG")
                        {
                            if (!string.IsNullOrEmpty(paramTerminalDetail.kd_regional) && paramTerminalDetail.kd_regional != "string")
                            {
                                paramType = " AND JENIS_KAPAL IN ('KPLPNMPANG', 'KPLRORO', 'KPLCRUISE')";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(paramTerminalDetail.status) && paramTerminalDetail.status != "string")
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
                    if (!string.IsNullOrEmpty(paramTerminalDetail.jenis_kapal) && paramTerminalDetail.jenis_kapal != "string")
                    {
                        paramJenisKapal = " AND JENIS_KAPAL='" + paramTerminalDetail.jenis_kapal + "'";
                    }

                    string paramNoPPKJasa = "";
                    if (!string.IsNullOrEmpty(paramTerminalDetail.no_ppk_jasa) && paramTerminalDetail.no_ppk_jasa != "string")
                    {
                        paramNoPPKJasa = " AND NO_PPK_JASA = '" + paramTerminalDetail.no_ppk_jasa + "'";
                    }

                    string sql = "SELECT * FROM(" +
                        "SELECT * FROM (SELECT A.*, B.REGIONAL_NAMA NAMA_REGIONAL, " +
                        "(CASE WHEN A.TGL_MULAI IS NULL AND A.TGL_SELESAI IS NULL THEN 'RENCANA' " +
                        "WHEN A.TGL_MULAI IS NOT NULL AND A.TGL_SELESAI IS NULL THEN 'SANDAR' " +
                        "WHEN A.TGL_MULAI IS NOT NULL AND A.TGL_SELESAI IS NOT NULL THEN 'HISTORY' END" +
                        ") STATUS " +
                        "FROM VW_MAGIC_TRMNL_INFO_ALL A, APP_REGIONAL B WHERE A.KD_REGIONAL=B.ID AND B.PARENT_ID IS NULL AND B.ID NOT IN (12300000,20300001) ) " + paramKodeRegional + paramKdCabangInduk + paramKdCabang + paramKdTerminal + paramStatus + paramType + paramJenisKapal + paramNoPPKJasa +
                    ")";


                    terminal = connection.Query<TerminalDetail>(sql).ToList();
                    
                    foreach(var Item in terminal)
                    {
                        if(temp.Contains(Item.tipe) == false)
                        {
                            temp.Add(Item.tipe);
                            result.Add(new TerminalDetail()
                            {
                                kd_cabang = Item.kd_cabang,
                                kd_cabang_induk = Item.kd_cabang_induk,
                                kd_terminal = Item.kd_terminal,
                                kawasan = Item.kawasan,
                                no_ppk1 = Item.no_ppk1,
                                no_ppk_jasa = Item.no_ppk_jasa,
                                nama_kapal = Item.nama_kapal,
                                nama_lokasi = Item.nama_lokasi,
                                nama_agen = Item.nama_agen,
                                kegiatan = Item.kegiatan,
                                jenis_kapal = Item.jenis_kapal,
                                tgl_mulai_ptp = Item.tgl_mulai_ptp,
                                tgl_selesai_ptp = Item.tgl_selesai_ptp,
                                tgl_mulai = Item.tgl_mulai,
                                tgl_selesai = Item.tgl_selesai,
                                created = Item.created,
                                start_work = Item.start_work,
                                end_work = Item.end_work,
                                tipe = Item.tipe,
                                nama_pelabuhan_asal = Item.nama_pelabuhan_asal,
                                nama_pelabuhan_tujuan = Item.nama_pelabuhan_tujuan,
                                gt_kapal = Item.gt_kapal,
                                loa = Item.loa,
                                kade_awal_ptp = Item.kade_awal_ptp,
                                kade_akhir_ptp = Item.kade_akhir_ptp,
                                kade_awal = Item.kade_awal,
                                kade_akhir = Item.kade_akhir,
                                kd_regional = Item.kd_regional,
                                kegiatan2 = Item.kegiatan,
                                nama_regional = Item.nama_regional
                            });
                        }
                        else
                        {
                            foreach(var data in result)
                            {
                                if(data.tipe == Item.tipe)
                                {
                                    if (data.kegiatan == null || data.kegiatan == "undefined" || data.kegiatan == "")
                                    {
                                        data.kegiatan = Item.kegiatan;
                                    }
                                    else
                                    {
                                        if (Item.kegiatan == "BONGKAR/MUAT") // Jika data kegiatan dari db adalah bongkar/muat maka langsung ganti data kegiatan menjadi bongkar/muat
                                        {
                                            data.kegiatan2 = Item.kegiatan;
                                        }
                                        else if (data.kegiatan == "BONGKAR/MUAT") // Jika data kegiatan di local data adalah bongkar/muat maka langsung ganti data kegiatan menjadi bongkar/muat
                                        {
                                            data.kegiatan2 = data.kegiatan;
                                        }
                                        else // Jika data kegiatan di local data dan data kegiatan dari db bukan bongkar/muat maka kombinasikan data local dan dari db
                                        {
                                            data.kegiatan2 = data.kegiatan + "/" + Item.kegiatan;
                                        }
                                    }
                                }
                            }
                        }
                    }
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

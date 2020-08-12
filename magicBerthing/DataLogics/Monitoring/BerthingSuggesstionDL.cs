using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
using magicBerthing;
using magicBerthing.Controllers.Warehouse;
using magicBerthing.Models.Monitoring;
using Oracle.ManagedDataAccess.Types;

namespace magicBerthing.DataLogics.Monitoring
{
    public class BerthingSuggestionDL
    {
        public IEnumerable<SuggestionList> getSuggestion(ParamBerthSuggestion paramBerthSuggestion)
        {
            IEnumerable<PenetapanList> TanggalMulai = new List<PenetapanList>();
            IEnumerable<PenetapanList> TanggalSelesai = new List<PenetapanList>();

            IEnumerable<DermagaList> Dermaga = new List<DermagaList>();
            IEnumerable<PasangSurutList> PasangSurut = new List<PasangSurutList>();
            IEnumerable<AturanKadeList> AturanKade = new List<AturanKadeList>();

            List<SuggestionList> TempSuggestion = new List<SuggestionList>();
            List<SuggestionList> ListSuggestion = new List<SuggestionList>();
            List<SuggestionList> SuggestionWithRecomendeed = new List<SuggestionList>();
            List<SuggestionList> ReusedPort = new List<SuggestionList>();
            List<SuggestionList> FinalSuggestion = new List<SuggestionList>();


            int status;
            int status_draft;
            int status_tgl_selesai;

            using (IDbConnection connection = Extension.GetConnection(0))
            {
                string paramKomoditi = "";
                if (paramBerthSuggestion.komoditi == "PENUMPANG" || paramBerthSuggestion.komoditi == "PETIKEMAS")
                {
                    paramKomoditi = " AND AREA_DERMAGA = '" + paramBerthSuggestion.komoditi + "'";
                }
                else
                {
                    paramKomoditi = " AND AREA_DERMAGA = '" + paramBerthSuggestion.komoditi + "' OR AREA_DERMAGA = 'UMUM'";
                }

                try
                {
                    string qtglselesai = "SELECT DISTINCT * FROM(" +
                        "SELECT * FROM(SELECT B.KODE_CABANG_INDUK, B.KODE_CABANG, A.KODE_TERMINAL, A.NO_PPK1, A.NO_PPK_JASA, B.NAMA_KAPAL, B.NAMA_AGEN, B.GT_KAPAL, B.LOA, A.KADE_AWAL, A.KADE_AKHIR, A.KODE_LOKASI, A.NAMA_LOKASI, A.TGL_MULAI, A.TGL_SELESAI, VASA.FUNC_GET_CLUSTER_POCC(A.KADE_AWAL, A.KADE_AKHIR, B.KODE_CABANG_INDUK, A.KODE_LOKASI) AREA_DERMAGA, (A.KADE_AKHIR - A.KADE_AWAL) KADE_USED " +
                        "FROM(SELECT * FROM(SELECT A.*, RANK() OVER(PARTITION BY NO_PPK1 ORDER BY CREATED DESC) AS URUTAN_PTP FROM(SELECT KODE_TERMINAL, NO_PPK1, NO_PPK_JASA, KADE_AWAL, KADE_AKHIR, KODE_LOKASI, NAMA_LOKASI, TGL_MULAI, TGL_SELESAI, RANK() OVER(PARTITION BY NO_PPK1 ORDER BY CREATED DESC) AS URUTAN, CREATED " +
                        "FROM VASA.PTP_TAMBAT WHERE TO_CHAR(TGL_SELESAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' AND PARENT_PTP_JASA_ID IS NULL AND STATUS NOT IN(6, 8, 9, 10) UNION ALL " +
                        "SELECT * FROM(SELECT A.KODE_TERMINAL, A.NO_PPK1, A.NO_PPK_JASA, B.KADE_AWAL, B.KADE_AKHIR, B.KODE_LOKASI, B.NAMA_LOKASI, B.TGL_MULAI, B.TGL_SELESAI, RANK() OVER(PARTITION BY A.NO_PPK1 ORDER BY B.CREATED DESC) AS URUTAN, B.CREATED " +
                        "FROM VASA.PTP_TAMBAT A, VASA.PTP_TAMBAT B WHERE TO_CHAR(A.TGL_SELESAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' AND B.NO_PPK1 = A.NO_PPK1 AND B.PARENT_PTP_JASA_ID = A.ID) WHERE URUTAN = 1) A) WHERE URUTAN_PTP = 1) A, VASA.PERMOHONAN B " +
                        "WHERE A.NO_PPK1 = B.NO_PPK1 AND TO_CHAR(A.TGL_SELESAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' ORDER BY A.TGL_SELESAI, A.KADE_AWAL ASC) WHERE KODE_CABANG = " + paramBerthSuggestion.kode_cabang + paramKomoditi +
                        " UNION ALL " +
                        "SELECT * FROM(SELECT B.KODE_CABANG_INDUK, B.KODE_CABANG, A.KODE_TERMINAL, A.NO_PPK1, A.NO_PPK_JASA, B.NAMA_KAPAL, B.NAMA_AGEN, B.GT_KAPAL, B.LOA, A.KADE_AWAL, A.KADE_AKHIR, A.KODE_LOKASI, A.NAMA_LOKASI, A.TGL_MULAI, A.TGL_SELESAI, VASA.FUNC_GET_CLUSTER_POCC(A.KADE_AWAL, A.KADE_AKHIR, B.KODE_CABANG_INDUK, A.KODE_LOKASI) AREA_DERMAGA, (A.KADE_AKHIR - A.KADE_AWAL) KADE_USED " +
                        "FROM(SELECT * FROM(SELECT A.*, RANK() OVER(PARTITION BY NO_PPK1 ORDER BY CREATED DESC) AS URUTAN_PTP FROM(SELECT KODE_TERMINAL, NO_PPK1, NO_PPK_JASA, KADE_AWAL, KADE_AKHIR, KODE_LOKASI, NAMA_LOKASI, TGL_MULAI, TGL_SELESAI, RANK() OVER(PARTITION BY NO_PPK1 ORDER BY CREATED DESC) AS URUTAN, CREATED " +
                        "FROM VASA.PTP_TAMBAT WHERE TO_CHAR(TGL_MULAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' AND PARENT_PTP_JASA_ID IS NULL AND STATUS NOT IN(6, 8, 9, 10) UNION ALL " +
                        "SELECT * FROM(SELECT A.KODE_TERMINAL, A.NO_PPK1, A.NO_PPK_JASA, B.KADE_AWAL, B.KADE_AKHIR, B.KODE_LOKASI, B.NAMA_LOKASI, B.TGL_MULAI, B.TGL_SELESAI, RANK() OVER(PARTITION BY A.NO_PPK1 ORDER BY B.CREATED DESC) AS URUTAN, B.CREATED FROM VASA.PTP_TAMBAT A, VASA.PTP_TAMBAT B WHERE TO_CHAR(A.TGL_MULAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' AND B.NO_PPK1 = A.NO_PPK1 AND B.PARENT_PTP_JASA_ID = A.ID) " +
                        "WHERE URUTAN = 1) A) WHERE URUTAN_PTP = 1) A, VASA.PERMOHONAN B WHERE A.NO_PPK1 = B.NO_PPK1 AND TO_CHAR(A.TGL_MULAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' ORDER BY A.TGL_MULAI, A.KADE_AWAL ASC) WHERE KODE_CABANG = " + paramBerthSuggestion.kode_cabang + paramKomoditi +
                    ") ORDER BY KADE_AWAL, NAMA_LOKASI, TGL_MULAI ASC";

                    TanggalSelesai = connection.Query<PenetapanList>(qtglselesai).ToList();

                    string qtglmulai = "SELECT DISTINCT * FROM(" +
                        "SELECT * FROM(SELECT B.KODE_CABANG_INDUK, B.KODE_CABANG, A.KODE_TERMINAL, A.NO_PPK1, A.NO_PPK_JASA, B.NAMA_KAPAL, B.NAMA_AGEN, B.GT_KAPAL, B.LOA, A.KADE_AWAL, A.KADE_AKHIR, A.KODE_LOKASI, A.NAMA_LOKASI, A.TGL_MULAI, A.TGL_SELESAI, VASA.FUNC_GET_CLUSTER_POCC(A.KADE_AWAL, A.KADE_AKHIR, B.KODE_CABANG_INDUK, A.KODE_LOKASI) AREA_DERMAGA, (A.KADE_AKHIR - A.KADE_AWAL) KADE_USED " +
                        "FROM(SELECT * FROM(SELECT A.*, RANK() OVER(PARTITION BY NO_PPK1 ORDER BY CREATED DESC) AS URUTAN_PTP FROM(SELECT KODE_TERMINAL, NO_PPK1, NO_PPK_JASA, KADE_AWAL, KADE_AKHIR, KODE_LOKASI, NAMA_LOKASI, TGL_MULAI, TGL_SELESAI, RANK() OVER(PARTITION BY NO_PPK1 ORDER BY CREATED DESC) AS URUTAN, CREATED " +
                        "FROM VASA.PTP_TAMBAT WHERE TO_CHAR(TGL_SELESAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' AND PARENT_PTP_JASA_ID IS NULL AND STATUS NOT IN(6, 8, 9, 10) UNION ALL " +
                        "SELECT * FROM(SELECT A.KODE_TERMINAL, A.NO_PPK1, A.NO_PPK_JASA, B.KADE_AWAL, B.KADE_AKHIR, B.KODE_LOKASI, B.NAMA_LOKASI, B.TGL_MULAI, B.TGL_SELESAI, RANK() OVER(PARTITION BY A.NO_PPK1 ORDER BY B.CREATED DESC) AS URUTAN, B.CREATED " +
                        "FROM VASA.PTP_TAMBAT A, VASA.PTP_TAMBAT B WHERE TO_CHAR(A.TGL_SELESAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' AND B.NO_PPK1 = A.NO_PPK1 AND B.PARENT_PTP_JASA_ID = A.ID) WHERE URUTAN = 1) A) WHERE URUTAN_PTP = 1) A, VASA.PERMOHONAN B " +
                        "WHERE A.NO_PPK1 = B.NO_PPK1 AND TO_CHAR(A.TGL_SELESAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' ORDER BY A.TGL_SELESAI, A.KADE_AWAL ASC) WHERE KODE_CABANG = " + paramBerthSuggestion.kode_cabang + paramKomoditi +
                        " UNION ALL " +
                        "SELECT * FROM(SELECT B.KODE_CABANG_INDUK, B.KODE_CABANG, A.KODE_TERMINAL, A.NO_PPK1, A.NO_PPK_JASA, B.NAMA_KAPAL, B.NAMA_AGEN, B.GT_KAPAL, B.LOA, A.KADE_AWAL, A.KADE_AKHIR, A.KODE_LOKASI, A.NAMA_LOKASI, A.TGL_MULAI, A.TGL_SELESAI, VASA.FUNC_GET_CLUSTER_POCC(A.KADE_AWAL, A.KADE_AKHIR, B.KODE_CABANG_INDUK, A.KODE_LOKASI) AREA_DERMAGA, (A.KADE_AKHIR - A.KADE_AWAL) KADE_USED " +
                        "FROM(SELECT * FROM(SELECT A.*, RANK() OVER(PARTITION BY NO_PPK1 ORDER BY CREATED DESC) AS URUTAN_PTP FROM(SELECT KODE_TERMINAL, NO_PPK1, NO_PPK_JASA, KADE_AWAL, KADE_AKHIR, KODE_LOKASI, NAMA_LOKASI, TGL_MULAI, TGL_SELESAI, RANK() OVER(PARTITION BY NO_PPK1 ORDER BY CREATED DESC) AS URUTAN, CREATED " +
                        "FROM VASA.PTP_TAMBAT WHERE TO_CHAR(TGL_MULAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' AND PARENT_PTP_JASA_ID IS NULL AND STATUS NOT IN(6, 8, 9, 10) UNION ALL " +
                        "SELECT * FROM(SELECT A.KODE_TERMINAL, A.NO_PPK1, A.NO_PPK_JASA, B.KADE_AWAL, B.KADE_AKHIR, B.KODE_LOKASI, B.NAMA_LOKASI, B.TGL_MULAI, B.TGL_SELESAI, RANK() OVER(PARTITION BY A.NO_PPK1 ORDER BY B.CREATED DESC) AS URUTAN, B.CREATED FROM VASA.PTP_TAMBAT A, VASA.PTP_TAMBAT B WHERE TO_CHAR(A.TGL_MULAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' AND B.NO_PPK1 = A.NO_PPK1 AND B.PARENT_PTP_JASA_ID = A.ID) " +
                        "WHERE URUTAN = 1) A) WHERE URUTAN_PTP = 1) A, VASA.PERMOHONAN B WHERE A.NO_PPK1 = B.NO_PPK1 AND TO_CHAR(A.TGL_MULAI, 'DD-MM-YYYY') = '" + paramBerthSuggestion.tgl + "' ORDER BY A.TGL_MULAI, A.KADE_AWAL ASC) WHERE KODE_CABANG = " + paramBerthSuggestion.kode_cabang + paramKomoditi +
                    ") ORDER BY KADE_AWAL, NAMA_LOKASI, TGL_MULAI ASC";

                    TanggalMulai = connection.Query<PenetapanList>(qtglmulai).ToList();
                }
                catch (Exception e)
                {
                    TanggalSelesai = null;
                    TanggalMulai = null;
                }
            }

            using (IDbConnection connection = Extension.GetConnection(0))
            {
                try
                {
                    string paramKomoditi = "";
                    if (paramBerthSuggestion.komoditi == "PETIKEMAS")
                    {
                        paramKomoditi = "WHERE KATEGORI = '" + paramBerthSuggestion.komoditi + "'";
                    }
                    else
                    {
                        paramKomoditi = "WHERE KATEGORI = '" + paramBerthSuggestion.komoditi + "' OR KATEGORI = 'UMUM'";
                    }

                    string query = "SELECT * FROM (SELECT DISTINCT A.KODE_CABANG, A.KODE_DERMAGA, D.MDMG_JENIS_DMG JENIS_DERMAGA, D.MDMG_NAMA NAMA_DERMAGA, MIN (A.KADE_AWAL) KADE_AWAL, MAX (A.KADE_AKHIR) KADE_AKHIR, B.NAMA, B.KATEGORI, E.KEDALAMAN, E.KODE_CABANG AS KODE_CABANG_KEDALAMAN, D.KD_CABANG AS KODE_CABANG_INDUK " +
                                    "FROM VASA.CLUSTERING_DERMAGA A, (SELECT ID, NAMA, (CASE WHEN NAMA = 'GENERAL CARGO' OR NAMA = 'GENERAL CARGO (GC)' OR NAMA = 'GC' OR NAMA = 'MOBIL'  OR NAMA = 'SEPEDA MOTOR' THEN 'GC' WHEN NAMA = 'CURAH CAIR' THEN 'CURAH CAIR' WHEN NAMA = 'CURAH KERING' THEN 'CURAH KERING' WHEN NAMA = 'BESI PRODUKSI' OR NAMA = 'HEWAN' OR NAMA = 'UNITIZED' OR NAMA = 'DRILLING MATERIAL' OR NAMA = 'SEMBAKO' OR NAMA = 'KAYU MASAK' OR NAMA = 'BUNKER' OR NAMA = 'COMBO WINDOWS' OR NAMA = 'COMBO NON WINDOWS' OR NAMA = 'BAG CARGO' OR NAMA = 'CREW SERVICE' OR NAMA = 'MULTIPURPOSE' THEN 'UNITIZED' WHEN NAMA = 'UMUM' THEN 'UMUM' WHEN NAMA = 'PENUMPANG' THEN 'PENUMPANG' WHEN NAMA = 'MUATAN PENUMPANG' THEN 'UNITIZED' WHEN NAMA = 'PETI KEMAS' OR NAMA = 'PETI KEMAS NON WINDOWS' OR NAMA = 'PETIKEMAS' OR NAMA = 'KONTAINER' THEN 'PETIKEMAS' END) KATEGORI " +
                                    "FROM(SELECT ID, UPPER(NAMA) NAMA FROM VASA.CLUSTERING)) B, MASTERDATA.UPKM_DERMAGA D, (SELECT ID, KEDALAMAN, KODE_DERMAGA, KADE_AWAL, KADE_AKHIR, KODE_CABANG FROM VASA.KEDALAMAN) E " +
                                    "WHERE A.CLUSTERING_ID = B.ID AND A.KODE_CABANG = D.KD_CABANG AND D.MDMG_JENIS_DMG = 'DMGUMUM' AND A.KODE_DERMAGA = D.MDMG_KODE AND A.KODE_CABANG = " + paramBerthSuggestion.kode_cabang + " AND E.KODE_DERMAGA = A.KODE_DERMAGA " +
                                    "AND E.KODE_CABANG=" + paramBerthSuggestion.kode_cabang + " AND A.KADE_AWAL >= E.KADE_AWAL AND A.KADE_AKHIR <= E.KADE_AKHIR GROUP BY A.KODE_CABANG, A.KODE_DERMAGA, D.MDMG_JENIS_DMG, D.MDMG_NAMA, B.NAMA, B.KATEGORI, E.KEDALAMAN, E.KODE_CABANG, D.KD_CABANG) " +
                                    paramKomoditi + " AND KODE_CABANG_KEDALAMAN=" + paramBerthSuggestion.kode_cabang + " ORDER BY KODE_CABANG, KODE_DERMAGA, KADE_AWAL ASC";

                    Dermaga = connection.Query<DermagaList>(query).ToList();
                }
                catch (Exception e)
                {
                    Dermaga = null;
                }
            }

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    string query = "SELECT * FROM ATURAN_KADE WHERE KODE_CABANG='" + paramBerthSuggestion.kd_cabang_aturan_kd + "'";

                    AturanKade = connection.Query<AturanKadeList>(query).ToList();
                }
                catch (Exception e)
                {
                    AturanKade = null;
                }
            }

            int total_loa = 0;
            foreach (var Aturan in AturanKade)
            {
                int percentage = (Convert.ToInt32(paramBerthSuggestion.loa) * Convert.ToInt32(Aturan.aturan)) / 100;
                total_loa = Convert.ToInt32(paramBerthSuggestion.loa) + (percentage * 2);
            }

            List<String> kade_reused = new List<String>();
            List<String> kade_used = new List<String>();
            List<String> dermaga_used = new List<String>();

            List<String> kade_reused_in_max_min = new List<string>();

            List<MaxMinKadeUsed> max_min_kade_used = new List<MaxMinKadeUsed>();
            List<MaxMinKadeUsed> min_tanggal_mulai = new List<MaxMinKadeUsed>();

            foreach (var ListDermaga in Dermaga)
            {
                foreach (var PenetapanTanggalSelesai in TanggalSelesai)
                {
                    if (ListDermaga.kode_dermaga == PenetapanTanggalSelesai.kode_lokasi && ListDermaga.kategori == PenetapanTanggalSelesai.area_dermaga)
                    {
                        foreach (var PenetapanTanggalMulai in TanggalMulai)
                        {
                            if (PenetapanTanggalMulai.kode_lokasi == PenetapanTanggalSelesai.kode_lokasi)
                            {
                                if (PenetapanTanggalSelesai.tgl_selesai == PenetapanTanggalMulai.tgl_mulai)
                                {
                                    if (
                                        (PenetapanTanggalSelesai.kade_akhir >= PenetapanTanggalMulai.kade_awal && PenetapanTanggalSelesai.kade_akhir <= PenetapanTanggalMulai.kade_akhir) ||
                                        (PenetapanTanggalMulai.kade_awal >= PenetapanTanggalSelesai.kade_awal && PenetapanTanggalMulai.kade_awal <= PenetapanTanggalSelesai.kade_akhir))
                                    {
                                        kade_reused.Add(PenetapanTanggalSelesai.kode_lokasi + "," + PenetapanTanggalSelesai.kade_awal.ToString() + "-" + PenetapanTanggalSelesai.kade_akhir.ToString() + ", " + PenetapanTanggalSelesai.tgl_mulai.ToString("HH:mm") + "-" + PenetapanTanggalSelesai.tgl_selesai.ToString("HH:mm"));
                                    }
                                }
                            }
                        }

                        if (kade_reused.Contains(PenetapanTanggalSelesai.kode_lokasi + "," + PenetapanTanggalSelesai.kade_awal.ToString() + "-" + PenetapanTanggalSelesai.kade_akhir.ToString() + ", " + PenetapanTanggalSelesai.tgl_mulai.ToString("HH:mm") + "-" + PenetapanTanggalSelesai.tgl_selesai.ToString("HH:mm")) == false)
                        {
                            if (kade_used.Contains(PenetapanTanggalSelesai.kode_lokasi + "," + PenetapanTanggalSelesai.kade_awal + "-" + PenetapanTanggalSelesai.kade_akhir) == false)
                            {

                                if (total_loa > PenetapanTanggalSelesai.kade_akhir - PenetapanTanggalSelesai.kade_awal)
                                {
                                    status = 0;
                                }
                                else
                                {
                                    status = 1;
                                }


                                using (IDbConnection connection = Extension.GetConnection(1))
                                {
                                    try
                                    {
                                        var qpasang = "SELECT * FROM PASANG_SURUT WHERE TO_CHAR(TGL, 'DD-MM') = '" + PenetapanTanggalSelesai.tgl_selesai.ToString("dd-MM") + "' AND JAM = " + PenetapanTanggalSelesai.tgl_selesai.ToString("HH");

                                        PasangSurut = connection.Query<PasangSurutList>(qpasang).ToList();
                                    }
                                    catch (Exception e)
                                    {
                                        PasangSurut = null;
                                    }
                                }

                                float pasang_surut = 0;
                                foreach (var ListPasangSurut in PasangSurut)
                                {
                                    pasang_surut = ListPasangSurut.pasang_surut;
                                }

                                if ((paramBerthSuggestion.draft + ((paramBerthSuggestion.draft * 10) / 100)) >= (pasang_surut + ListDermaga.kedalaman))
                                {
                                    status_draft = 0;
                                }
                                else
                                {
                                    status_draft = 1;
                                }

                                if (PenetapanTanggalSelesai.tgl_selesai > new DateTime(Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[2]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[1]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[0]), 23, 59, 0))
                                {
                                    status_tgl_selesai = 0;
                                }
                                else
                                {
                                    status_tgl_selesai = 1;
                                }

                                ListSuggestion.Add(new SuggestionList()
                                {
                                    nama_dermaga = ListDermaga.nama_dermaga,
                                    kode_dermaga = ListDermaga.kode_dermaga,
                                    kode_cabang = ListDermaga.kode_cabang,
                                    kode_cabang_induk = ListDermaga.kode_cabang_induk,
                                    kategori = ListDermaga.kategori,
                                    kade_awal = PenetapanTanggalSelesai.kade_awal,
                                    kade_akhir = PenetapanTanggalSelesai.kade_akhir,
                                    total_kade = PenetapanTanggalSelesai.kade_akhir - PenetapanTanggalSelesai.kade_awal,
                                    status = status,
                                    kedalaman = ListDermaga.kedalaman,
                                    jam = PenetapanTanggalSelesai.tgl_selesai.ToString("HH:mm"),
                                    pasang_surut = pasang_surut,
                                    total_loa = total_loa.ToString(),
                                    status_draft = status_draft,
                                    status_tgl_selesai = status_tgl_selesai
                                });

                                TempSuggestion.Add(new SuggestionList()
                                {
                                    nama_dermaga = ListDermaga.nama_dermaga,
                                    kode_dermaga = ListDermaga.kode_dermaga,
                                    kategori = ListDermaga.kategori,
                                    kade_awal = PenetapanTanggalSelesai.kade_awal,
                                    kade_akhir = PenetapanTanggalSelesai.kade_akhir,
                                    total_kade = PenetapanTanggalSelesai.kade_akhir - PenetapanTanggalSelesai.kade_awal,
                                    kedalaman = ListDermaga.kedalaman,
                                    jam = PenetapanTanggalSelesai.tgl_selesai.ToString(),
                                    total_loa = total_loa.ToString()
                                });

                                kade_used.Add(PenetapanTanggalSelesai.kode_lokasi + "," + PenetapanTanggalSelesai.kade_awal + "-" + PenetapanTanggalSelesai.kade_akhir);
                                dermaga_used.Add(PenetapanTanggalSelesai.kode_lokasi);
                            }
                        }

                    }
                }
            }

            foreach (var ListDermaga in Dermaga)
            {
                foreach (var ListTemp in TempSuggestion)
                {
                    if (ListTemp.kode_dermaga == ListDermaga.kode_dermaga && ListTemp.kategori == ListDermaga.kategori)
                    {
                        // Dermaga diantara satu kapal dengan kapal lain
                        if (ListDermaga.kade_awal < ListTemp.kade_awal)
                        {

                            // Cek tanggal pencarian jika sama dengan hari ini maka jam ditampilkan jam sekarang
                            DateTime jam = DateTime.Now;
                            string parameterJam = null;
                            if (new DateTime(Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[2]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[1]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[0]), 0, 0, 0) > DateTime.Now)
                            {
                                jam = new DateTime(Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[2]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[1]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[0]), 0, 0, 0);
                                parameterJam = "24";
                            }
                            else
                            {
                                jam = DateTime.Now;
                                parameterJam = DateTime.Now.ToString("HH");
                            }

                            using (IDbConnection connection = Extension.GetConnection(1))
                            {
                                try
                                {
                                    var qpasang = "SELECT * FROM PASANG_SURUT WHERE TO_CHAR(TGL, 'DD-MM') = '" + jam.ToString("dd-MM") + "' AND JAM = " + parameterJam;

                                    PasangSurut = connection.Query<PasangSurutList>(qpasang).ToList();
                                }
                                catch (Exception e)
                                {
                                    PasangSurut = null;
                                }
                            }

                            float pasang_surut = 0;
                            foreach (var ListPasangSurut in PasangSurut)
                            {
                                pasang_surut = ListPasangSurut.pasang_surut;
                            }

                            if (total_loa > (ListTemp.kade_awal > ListDermaga.kade_akhir ? ListDermaga.kade_akhir : ListTemp.kade_awal) - ListDermaga.kade_awal)
                            {
                                status = 0;
                            }
                            else
                            {
                                status = 1;
                            }

                            if ((paramBerthSuggestion.draft + ((paramBerthSuggestion.draft * 10) / 100)) >= (pasang_surut + ListDermaga.kedalaman))
                            {
                                status_draft = 0;
                            }
                            else
                            {
                                status_draft = 1;
                            }

                            ListSuggestion.Add(new SuggestionList()
                            {
                                nama_dermaga = ListDermaga.nama_dermaga,
                                kode_dermaga = ListDermaga.kode_dermaga,
                                kode_cabang = ListDermaga.kode_cabang,
                                kode_cabang_induk = ListDermaga.kode_cabang_induk,
                                kategori = ListDermaga.kategori,
                                kade_awal = ListDermaga.kade_awal,
                                kade_akhir = ListTemp.kade_awal > ListDermaga.kade_akhir ? ListDermaga.kade_akhir : ListTemp.kade_awal,
                                total_kade = (ListTemp.kade_awal > ListDermaga.kade_akhir ? ListDermaga.kade_akhir : ListTemp.kade_awal) - ListDermaga.kade_awal,
                                status = status,
                                kedalaman = ListDermaga.kedalaman,
                                jam = jam.ToString("HH:mm"),
                                pasang_surut = pasang_surut,
                                total_loa = total_loa.ToString(),
                                status_draft = status_draft,
                                status_tgl_selesai = 1
                            });
                        }

                        ListDermaga.kade_awal = ListTemp.kade_akhir;


                        if (kade_reused_in_max_min.Contains(ListTemp.kode_dermaga + "," + ListTemp.kade_awal + "-" + ListTemp.kade_akhir) == false)
                        {
                            max_min_kade_used.Add(new MaxMinKadeUsed()
                            {
                                kode_dermaga = ListTemp.kode_dermaga,
                                kade_awal = ListTemp.kade_awal,
                                kade_akhir = ListTemp.kade_akhir
                            });
                        }

                        kade_reused_in_max_min.Add(ListTemp.kode_dermaga + "," + ListTemp.kade_awal + "-" + ListTemp.kade_akhir);

                    }
                }


                // Dermaga yang tidak  termasuk dalam penetapan
                if (dermaga_used.Contains(ListDermaga.kode_dermaga) == false && ListDermaga.kategori == paramBerthSuggestion.komoditi)
                {
                    if (total_loa > ListDermaga.kade_akhir - ListDermaga.kade_awal)
                    {
                        status = 0;
                    }
                    else
                    {
                        status = 1;
                    }

                    // Cek tanggal pencarian jika sama dengan hari ini maka jam ditampilkan jam sekarang
                    DateTime jam = DateTime.Now;
                    string parameterJam = null;
                    if (new DateTime(Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[2]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[1]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[0]), 0, 0, 0) > DateTime.Now)
                    {
                        jam = new DateTime(Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[2]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[1]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[0]), 0, 0, 0);
                        parameterJam = "24";
                    }
                    else
                    {
                        jam = DateTime.Now;
                        parameterJam = DateTime.Now.ToString("HH");
                    }

                    using (IDbConnection connection = Extension.GetConnection(1))
                    {
                        try
                        {
                            var qpasang = "SELECT * FROM PASANG_SURUT WHERE TO_CHAR(TGL, 'DD-MM') = '" + jam.ToString("dd-MM") + "' AND JAM = " + parameterJam;

                            PasangSurut = connection.Query<PasangSurutList>(qpasang).ToList();
                        }
                        catch (Exception e)
                        {
                            PasangSurut = null;
                        }
                    }

                    float pasang_surut = 0;
                    foreach (var ListPasangSurut in PasangSurut)
                    {
                        pasang_surut = ListPasangSurut.pasang_surut;
                    }

                    if ((paramBerthSuggestion.draft + ((paramBerthSuggestion.draft * 10) / 100)) >= (pasang_surut + ListDermaga.kedalaman))
                    {
                        status_draft = 0;
                    }
                    else
                    {
                        status_draft = 1;
                    }

                    ListSuggestion.Add(new SuggestionList()
                    {
                        nama_dermaga = ListDermaga.nama_dermaga,
                        kode_dermaga = ListDermaga.kode_dermaga,
                        kode_cabang = ListDermaga.kode_cabang,
                        kode_cabang_induk = ListDermaga.kode_cabang_induk,
                        kategori = ListDermaga.kategori,
                        kade_awal = ListDermaga.kade_awal,
                        kade_akhir = ListDermaga.kade_akhir,
                        total_kade = ListDermaga.kade_akhir - ListDermaga.kade_awal,
                        status = status,
                        kedalaman = ListDermaga.kedalaman,
                        jam = jam.ToString("HH:mm"),
                        pasang_surut = pasang_surut,
                        total_loa = total_loa.ToString(),
                        status_draft = status_draft,
                        status_tgl_selesai = 1
                    });
                }
            }

            // Data dermaga yang kade terpakai lebih kecil dari panjang dermaga total
            var max_min_in_order = max_min_kade_used.OrderByDescending(m => m.kade_akhir);
            foreach (var ListDermaga in Dermaga)
            {
                foreach (var MaxMin in max_min_in_order)
                {
                    if (ListDermaga.kode_dermaga == MaxMin.kode_dermaga && ListDermaga.kategori == paramBerthSuggestion.komoditi)
                    {
                        if (MaxMin.kade_akhir >= ListDermaga.kade_awal)
                        {
                            if (total_loa > ListDermaga.kade_akhir - MaxMin.kade_akhir)
                            {
                                status = 0;
                            }
                            else
                            {
                                status = 1;
                            }

                            // Cek tanggal pencarian jika sama dengan hari ini maka jam ditampilkan jam sekarang
                            DateTime jam = DateTime.Now;
                            string parameterJam = null;
                            if (new DateTime(Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[2]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[1]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[0]), 0, 0, 0) > DateTime.Now)
                            {
                                jam = new DateTime(Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[2]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[1]), Convert.ToInt32(paramBerthSuggestion.tgl.Split('-')[0]), 0, 0, 0);
                                parameterJam = "24";
                            }
                            else
                            {
                                jam = DateTime.Now;
                                parameterJam = DateTime.Now.ToString("HH");
                            }

                            using (IDbConnection connection = Extension.GetConnection(1))
                            {
                                try
                                {
                                    var qpasang = "SELECT * FROM PASANG_SURUT WHERE TO_CHAR(TGL, 'DD-MM') = '" + jam.ToString("dd-MM") + "' AND JAM = " + parameterJam;

                                    PasangSurut = connection.Query<PasangSurutList>(qpasang).ToList();
                                }
                                catch (Exception e)
                                {
                                    PasangSurut = null;
                                }
                            }

                            float pasang_surut = 0;
                            foreach (var ListPasangSurut in PasangSurut)
                            {
                                pasang_surut = ListPasangSurut.pasang_surut;
                            }

                            if ((paramBerthSuggestion.draft + ((paramBerthSuggestion.draft * 10) / 100)) >= (pasang_surut + ListDermaga.kedalaman))
                            {
                                status_draft = 0;
                            }
                            else
                            {
                                status_draft = 1;
                            }

                            if (ListDermaga.kade_akhir > MaxMin.kade_akhir)
                            {
                                ListSuggestion.Add(new SuggestionList()
                                {
                                    nama_dermaga = ListDermaga.nama_dermaga,
                                    kode_dermaga = MaxMin.kode_dermaga,
                                    kode_cabang = ListDermaga.kode_cabang,
                                    kode_cabang_induk = ListDermaga.kode_cabang_induk,
                                    kategori = ListDermaga.kategori,
                                    kade_awal = MaxMin.kade_akhir,
                                    kade_akhir = ListDermaga.kade_akhir,
                                    total_kade = ListDermaga.kade_akhir - MaxMin.kade_akhir,
                                    status = status,
                                    kedalaman = ListDermaga.kedalaman,
                                    jam = jam.ToString("HH:mm"),
                                    pasang_surut = pasang_surut,
                                    total_loa = total_loa.ToString(),
                                    status_draft = status_draft,
                                    status_tgl_selesai = 1
                                });
                            }
                        }

                        ListDermaga.kade_awal = MaxMin.kade_akhir;
                    }
                }
            }

            SuggestionWithRecomendeed = this.getRecomendeedPort(ListSuggestion, paramBerthSuggestion);

            ReusedPort = this.getReusedPort(ListSuggestion, paramBerthSuggestion, SuggestionWithRecomendeed);

            FinalSuggestion = this.getNotBookedPort(ReusedPort, paramBerthSuggestion).Count() > 0 ? this.getNotBookedPort(ReusedPort, paramBerthSuggestion) : ReusedPort;

            //FinalSuggestion = this.getNotBookedPort(ReusedPort, paramBerthSuggestion);
            return FinalSuggestion;
        }


        public string create_two_digits(string item)
        {
            string result = null;

            if (item.Length > 1)
            {
                result = item;
            }
            else
            {
                result = "0" + item;
            }

            return result;
        }



        /**
         * Ambil data suggestion dengan label recomendeed dan unrecomendeed 
         * @return List
        */
        public List<SuggestionList> getRecomendeedPort(List<SuggestionList> ListSuggestion, ParamBerthSuggestion paramBerthSuggestion)
        {
            IEnumerable<RecomendeedDermagaList> RecomendeedDermaga = new List<RecomendeedDermagaList>();
            List<SuggestionList> SuggestionWithRecomendeed = new List<SuggestionList>();

            using (IDbConnection connection = Extension.GetConnection(0))
            {
                try
                {
                    string qrecomendeeddermaga = "SELECT B.KODE_CABANG_INDUK, B.NAMA_KAPAL, A.NAMA_LOKASI, COUNT(A.NAMA_LOKASI) JUMLAH FROM REA_TAMBAT A, PERMOHONAN B " +
                                                  " WHERE A.NO_PPK1 = B.NO_PPK1 AND NAMA_KAPAL = '" + paramBerthSuggestion.nama_kapal + "' AND B.KODE_CABANG_INDUK = '" + paramBerthSuggestion.kd_cabang_aturan_kd + "' GROUP BY B.KODE_CABANG_INDUK, B.NAMA_KAPAL, A.NAMA_LOKASI";

                    RecomendeedDermaga = connection.Query<RecomendeedDermagaList>(qrecomendeeddermaga).ToList();
                }
                catch (Exception e)
                {
                    RecomendeedDermaga = null;
                }
            }

            String[] final_suggestion_data = new string[RecomendeedDermaga.Count() * ListSuggestion.Count()];
            int index_final = 0;

            // Get Recomendeed dermaga
            foreach (var Suggestion in ListSuggestion)
            {
                foreach (var Recomendeed in RecomendeedDermaga)
                {
                    if (this.create_two_digits(Suggestion.kode_cabang_induk) == Recomendeed.kode_cabang_induk && Suggestion.nama_dermaga == Recomendeed.nama_lokasi)
                    {
                        SuggestionWithRecomendeed.Add(new SuggestionList()
                        {
                            nama_dermaga = Suggestion.nama_dermaga,
                            kode_dermaga = Suggestion.kode_dermaga,
                            kode_cabang = Suggestion.kode_cabang,
                            kode_cabang_induk = Suggestion.kode_cabang_induk,
                            kategori = Suggestion.kategori,
                            kade_awal = Suggestion.kade_awal,
                            kade_akhir = Suggestion.kade_akhir,
                            total_kade = Suggestion.total_kade,
                            status = Suggestion.status,
                            kedalaman = Suggestion.kedalaman,
                            jam = Suggestion.jam,
                            pasang_surut = Suggestion.pasang_surut,
                            total_loa = Suggestion.total_loa,
                            status_draft = Suggestion.status_draft,
                            status_tgl_selesai = Suggestion.status_tgl_selesai,
                            is_recomendeed = 1
                        });


                        final_suggestion_data[index_final] = Suggestion.nama_dermaga + "," + Suggestion.kode_cabang_induk + "," + Suggestion.kade_awal + "," + Suggestion.kade_akhir;
                        ++index_final;

                    }
                }
            }

            // Get Not Recomendeed dermaga
            foreach (var Suggestion in ListSuggestion)
            {
                if (final_suggestion_data.Contains(Suggestion.nama_dermaga + "," + Suggestion.kode_cabang_induk + "," + Suggestion.kade_awal + "," + Suggestion.kade_akhir) == false)
                {
                    SuggestionWithRecomendeed.Add(new SuggestionList()
                    {
                        nama_dermaga = Suggestion.nama_dermaga,
                        kode_dermaga = Suggestion.kode_dermaga,
                        kode_cabang = Suggestion.kode_cabang,
                        kode_cabang_induk = Suggestion.kode_cabang_induk,
                        kategori = Suggestion.kategori,
                        kade_awal = Suggestion.kade_awal,
                        kade_akhir = Suggestion.kade_akhir,
                        total_kade = Suggestion.total_kade,
                        status = Suggestion.status,
                        kedalaman = Suggestion.kedalaman,
                        jam = Suggestion.jam,
                        pasang_surut = Suggestion.pasang_surut,
                        total_loa = Suggestion.total_loa,
                        status_draft = Suggestion.status_draft,
                        status_tgl_selesai = Suggestion.status_tgl_selesai,
                        is_recomendeed = 0
                    });
                }
            }

            return SuggestionWithRecomendeed;
        }

        
        
        /**
         *  Ambil data pelabuhan yang tidak ada data kapal masuk lagi mulai rencana masuk sampai rencana keluar pelabuhan
         *  @return List
        */
        public List<SuggestionList> getReusedPort(List<SuggestionList> ListSuggestion, ParamBerthSuggestion paramBerthSuggestion, List<SuggestionList> SuggestionWithRecomendeed)
        {

            IEnumerable<PenetapanList> Checker = new List<PenetapanList>();
            List<SuggestionList> ReusedPort = new List<SuggestionList>();

            List<String> Reused = new List<String>();

            foreach (var Suggestion in SuggestionWithRecomendeed)
            {
                DateTime time = DateTime.ParseExact(paramBerthSuggestion.tgl + " " + Suggestion.jam, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

                string paramKomoditi = "";
                if (paramBerthSuggestion.komoditi == "PENUMPANG" || paramBerthSuggestion.komoditi == "PETIKEMAS")
                {
                    paramKomoditi = " AND AREA_DERMAGA = '" + paramBerthSuggestion.komoditi + "'";
                }
                else
                {
                    paramKomoditi = " AND AREA_DERMAGA = '" + paramBerthSuggestion.komoditi + "' OR AREA_DERMAGA = 'UMUM'";
                }

                string query = "SELECT DISTINCT * FROM(" +
                    "SELECT * FROM(SELECT B.KODE_CABANG_INDUK, B.KODE_CABANG, A.KODE_TERMINAL, A.NO_PPK1, A.NO_PPK_JASA, B.NAMA_KAPAL, B.NAMA_AGEN, B.GT_KAPAL, B.LOA, A.KADE_AWAL, A.KADE_AKHIR, A.KODE_LOKASI, A.NAMA_LOKASI, A.TGL_MULAI, A.TGL_SELESAI, VASA.FUNC_GET_CLUSTER_POCC(A.KADE_AWAL, A.KADE_AKHIR, B.KODE_CABANG_INDUK, A.KODE_LOKASI) AREA_DERMAGA, (A.KADE_AKHIR - A.KADE_AWAL) KADE_USED " +
                    "FROM(SELECT * FROM(SELECT A.*, RANK() OVER(PARTITION BY NO_PPK1 ORDER BY CREATED DESC) AS URUTAN_PTP FROM(SELECT KODE_TERMINAL, NO_PPK1, NO_PPK_JASA, KADE_AWAL, KADE_AKHIR, KODE_LOKASI, NAMA_LOKASI, TGL_MULAI, TGL_SELESAI, RANK() OVER(PARTITION BY NO_PPK1 ORDER BY CREATED DESC) AS URUTAN, CREATED " +
                    "FROM VASA.PTP_TAMBAT WHERE TGL_MULAI BETWEEN TO_DATE('" + time.ToString("dd-MM-yyyy HH:mm") + "', 'DD-MM-YYYY HH24:MI') AND TO_DATE('" + time.AddHours(paramBerthSuggestion.lama_sandar).ToString("dd-MM-yyyy HH:mm") + "', 'DD-MM-YYYY HH24:MI') AND PARENT_PTP_JASA_ID IS NULL AND STATUS NOT IN(6, 8, 9, 10) UNION ALL " +
                    "SELECT * FROM(SELECT A.KODE_TERMINAL, A.NO_PPK1, A.NO_PPK_JASA, B.KADE_AWAL, B.KADE_AKHIR, B.KODE_LOKASI, B.NAMA_LOKASI, B.TGL_MULAI, B.TGL_SELESAI, RANK() OVER(PARTITION BY A.NO_PPK1 ORDER BY B.CREATED DESC) AS URUTAN, B.CREATED " +
                    "FROM VASA.PTP_TAMBAT A, VASA.PTP_TAMBAT B WHERE A.TGL_MULAI BETWEEN TO_DATE('" + time.ToString("dd-MM-yyyy HH:mm") + "', 'DD-MM-YYYY HH24:MI') AND TO_DATE('" + time.AddHours(paramBerthSuggestion.lama_sandar).ToString("dd-MM-yyyy HH:mm") + "', 'DD-MM-YYYY HH24:MI') AND B.NO_PPK1 = A.NO_PPK1 AND B.PARENT_PTP_JASA_ID = A.ID) WHERE URUTAN = 1) A) WHERE URUTAN_PTP = 1) A, VASA.PERMOHONAN B " +
                    "WHERE A.NO_PPK1 = B.NO_PPK1 AND A.TGL_MULAI BETWEEN TO_DATE('" + time.ToString("dd-MM-yyyy HH:mm") + "', 'DD-MM-YYYY HH24:MI') AND TO_DATE('" + time.AddHours(paramBerthSuggestion.lama_sandar).ToString("dd-MM-yyyy HH:mm") + "', 'DD-MM-YYYY HH24:MI') ORDER BY A.TGL_MULAI, A.KADE_AWAL ASC) WHERE KODE_CABANG = " + paramBerthSuggestion.kode_cabang + paramKomoditi +
                ") ORDER BY KADE_AWAL, NAMA_LOKASI, TGL_MULAI ASC";


                using (IDbConnection connection = Extension.GetConnection(0))
                {
                    Checker = connection.Query<PenetapanList>(query).ToList();

                    bool[] is_not_empty = new bool[Checker.Count()];
                    int not_empty = 0;

                    if (Checker.Count() > 0)
                    {
                        foreach (var Val in Checker)
                        {
                            if (this.create_two_digits(Suggestion.kode_cabang) == Val.kode_cabang && Suggestion.kode_dermaga == Val.kode_lokasi)
                            {
                                if (
                                    (Suggestion.kade_awal >= Val.kade_awal && Suggestion.kade_akhir >= Val.kade_akhir) ||
                                    (Suggestion.kade_awal <= Val.kade_awal && Suggestion.kade_akhir <= Val.kade_akhir) ||
                                    (Suggestion.kade_awal <= Val.kade_awal && Suggestion.kade_akhir >= Val.kade_akhir)
                                    )
                                {
                                    is_not_empty[not_empty] = true;
                                }
                                else
                                {
                                    is_not_empty[not_empty] = false;
                                }
                            }
                        }
                    }

                    if (is_not_empty.Contains(true) == false)
                    {
                        Reused.Add("true");
                    }
                    else
                    {
                        Reused.Add("false");
                    }
                }
            }

            for (int i = 0; i < Reused.Count(); i++)
            {
                if (Reused[i] == "true")
                {
                    ReusedPort.Add(new SuggestionList()
                    {
                        nama_dermaga = SuggestionWithRecomendeed[i].nama_dermaga,
                        kode_dermaga = SuggestionWithRecomendeed[i].kode_dermaga,
                        kode_cabang = SuggestionWithRecomendeed[i].kode_cabang,
                        kode_cabang_induk = SuggestionWithRecomendeed[i].kode_cabang_induk,
                        kategori = SuggestionWithRecomendeed[i].kategori,
                        kade_awal = SuggestionWithRecomendeed[i].kade_awal,
                        kade_akhir = SuggestionWithRecomendeed[i].kade_akhir,
                        total_kade = SuggestionWithRecomendeed[i].total_kade,
                        status = SuggestionWithRecomendeed[i].status,
                        kedalaman = SuggestionWithRecomendeed[i].kedalaman,
                        jam = SuggestionWithRecomendeed[i].jam,
                        pasang_surut = SuggestionWithRecomendeed[i].pasang_surut,
                        total_loa = SuggestionWithRecomendeed[i].total_loa,
                        status_draft = SuggestionWithRecomendeed[i].status_draft,
                        status_tgl_selesai = SuggestionWithRecomendeed[i].status_tgl_selesai,
                        is_recomendeed = SuggestionWithRecomendeed[i].is_recomendeed
                    });
                }
            }

            return ReusedPort;

        }



        /**
         * Ambil data pelabuhan yang belum pernah di booking pada tanggal sesuai pencarian
         * @return List
        */
        public List<SuggestionList> getNotBookedPort(List<SuggestionList> ReusedPort, ParamBerthSuggestion paramBerthSuggestion)
        {
            List<GetBookingDermagaList> bookingData = new List<GetBookingDermagaList>();
            List<SuggestionList> result = new List<SuggestionList>();

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    string query = "SELECT * FROM MAGIC_BOOKING_HEADER";


                    bookingData = connection.Query<GetBookingDermagaList>(query).ToList();
                }
                catch (Exception e)
                {
                    bookingData = null;
                }
            }

            foreach (var Port in ReusedPort)
            {
                foreach (var booking in bookingData)
                {
                    DateTime created_at = booking.created_date.AddHours(booking.lama_rencana_tambat);
                    DateTime search_date = DateTime.ParseExact(paramBerthSuggestion.tgl, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    // JIka ada booking pada tanggal pencarian
                    if (created_at > search_date)
                    {

                        if (Port.kode_dermaga == booking.kode_pelabuhan)
                        {
                            if (
                                (booking.kade_awal >= Port.kade_awal && booking.kade_akhir >= Port.kade_akhir) ||
                                (booking.kade_awal <= Port.kade_awal && booking.kade_akhir <= Port.kade_akhir) ||
                                (booking.kade_awal <= Port.kade_awal && booking.kade_akhir >= Port.kade_akhir)
                                )
                            {
                                // No action karena kade sudah atau sedang di booking
                            }
                            else
                            {
                                result.Add(new SuggestionList()
                                {
                                    nama_dermaga = Port.nama_dermaga,
                                    kode_dermaga = Port.kode_dermaga,
                                    kode_cabang = Port.kode_cabang,
                                    kode_cabang_induk = Port.kode_cabang_induk,
                                    kategori = Port.kategori,
                                    kade_awal = Port.kade_awal,
                                    kade_akhir = Port.kade_akhir,
                                    total_kade = Port.total_kade,
                                    status = Port.status,
                                    kedalaman = Port.kedalaman,
                                    jam = Port.jam,
                                    pasang_surut = Port.pasang_surut,
                                    total_loa = Port.total_loa,
                                    status_draft = Port.status_draft,
                                    status_tgl_selesai = Port.status_tgl_selesai,
                                    is_recomendeed = Port.is_recomendeed
                                });
                            }
                        }
                        else
                        {
                            result.Add(new SuggestionList()
                            {
                                nama_dermaga = Port.nama_dermaga,
                                kode_dermaga = Port.kode_dermaga,
                                kode_cabang = Port.kode_cabang,
                                kode_cabang_induk = Port.kode_cabang_induk,
                                kategori = Port.kategori,
                                kade_awal = Port.kade_awal,
                                kade_akhir = Port.kade_akhir,
                                total_kade = Port.total_kade,
                                status = Port.status,
                                kedalaman = Port.kedalaman,
                                jam = Port.jam,
                                pasang_surut = Port.pasang_surut,
                                total_loa = Port.total_loa,
                                status_draft = Port.status_draft,
                                status_tgl_selesai = Port.status_tgl_selesai,
                                is_recomendeed = Port.is_recomendeed
                            });
                        }
                    }
                }
            }

            return result;
        }
    
    }
}
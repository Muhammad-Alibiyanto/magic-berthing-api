using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring
{
    public class BerthSuggestionModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<SuggestionList> data { get; set; }
    }
    public class PenetapanList
    {
        public string kode_cabang_induk { get; set; }
        public string kode_cabang { get; set; }
        public string kode_terminal { get; set; }
        public string no_ppk1 { get; set; }
        public string no_ppk_jasa { get; set; }
        public string nama_kapal { get; set; }
        public string kode_lokasi { get; set; }
        public string nama_lokasi { get; set; }
        public string nama_agen { get; set; }
        public int kade_awal { get; set; }
        public int kade_akhir { get; set; }
        public int gt_kapal { get; set; }
        public float loa { get; set; }
        public DateTime tgl_mulai { get; set; }
        public DateTime tgl_selesai { get; set; }
        public string area_dermaga { get; set; }
        public int kade_used { get; set; }
    }

    public class DermagaList
    {
        internal string kode_cabang;

        public string kode_cabang_induk { get; set; }
        public string kode_dermaga { get; set; }
        public string nama_dermaga { get; set; }
        public int kade_awal { get; set; }
        public int kade_akhir { get; set; }
        public string nama { get; set; }
        public string kategori { get; set; }
        public int total_kade { get; set; }
        public int kedalaman { get; set; }
    }

    public class PasangSurutList
    {
        public string kd_cabang { get; set; }
        public string tgl { get; set; }
        public float pasang_surut { get; set; }
        public string jam { get; set; }
    }
    
    public class AturanKadeList
    {
        public string kode_cabang { get; set; }
        public string aturan { get; set; }
    }
    
    public class MaxMinKadeUsed
    {
        public string kode_dermaga { get; set; }
        public int kade_awal { get; set; }
        public int kade_akhir { get; set; }
        public DateTime tgl_mulai { get; set; }
    }
    
    public class SuggestionList
    {
        public string kode_cabang { get; set; }
        public string kode_cabang_induk { get; set; }
        public string kode_dermaga { get; set; }
        public string nama_dermaga { get; set; }
        public int kade_awal { get; set; }
        public int kade_akhir { get; set; }
        public string nama { get; set; }
        public string kategori { get; set; }
        public int total_kade { get; set; }
        public int status { get; set; }
        public string jam { get; set; }
        public int kedalaman { get; set; }
        public float pasang_surut { get; set; }
        public string total_loa { get; set; }
        public int status_draft { get; set; }
        public int status_tgl_selesai { get; set; }
        public int is_recomendeed { get; set; }
    }

    
    public class RecomendeedDermagaList
    {
        public string kode_cabang_induk { get; set; }
        public string nama_kapal { get; set; }
        public string nama_lokasi { get; set; }
        public string jumlah { get; set; }
    }

    public class ParamBerthSuggestion
    {
        public int kode_cabang { get; set; }
        public string nama_kapal { get; set; }
        public string kd_cabang_aturan_kd { get; set; }
        public string komoditi { get; set; }
        public float loa { get; set; }
        public string tgl { get; set; }
        public float draft { get; set; }
        public int lama_sandar { get; set; }
    }
}
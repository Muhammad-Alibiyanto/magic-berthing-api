using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring
{
    public class MonTerminalAvailableModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public PagedList<TerminalAvailable> data { get; set; }
    }

    public class TerminalAvailable
    {
        public string kd_cabang_induk { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string kawasan { get; set; }
        public string no_ppk1 { get; set; }
        public string no_ppk_jasa { get; set; }
        public string nama_kapal { get; set; }
        public string nama_lokasi { get; set; }
        public string nama_agen { get; set; }
        public string kegiatan { get; set; }
        public string jenis_kapal { get; set; }
        public string tgl_mulai_ptp { get; set; }
        public string tgl_selesai_ptp { get; set; }
        public string tgl_mulai { get; set; }
        public string tgl_selesai { get; set; }
        public string created { get; set; }
        public string start_work { get; set; }
        public string end_work { get; set; }
        public string tipe { get; set; }
        public string kd_regional { get; set; }
        public string kode_kapal { get; set; }
        public string nama_pelabuhan_asal { get; set; }
        public string nama_pelabuhan_tujuan { get; set; }
        public string gt_kapal { get; set; }
        public string loa { get; set; }
        public string nama_regional { get; set; }
        public string kade_awal_ptp { get; set; }
        public string kade_akhir_ptp { get; set; }
        public string kade_awal { get; set; }
        public string kade_akhir { get; set; }
        public string status { get; set; }
    }

    public class ParamTerminal
    {
        public string kd_cabang_induk { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string type_terminal { get; set; }
        public string no_ppk_jasa { get; set; }
        public string kd_regional { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
        public string status { get; set; }
        public string order_by_column { get; set; }
        public string order_by_sort { get; set; }
        public string jenis_kapal { get; set; }
        public string lokasi { get; set; }
        public string kegiatan { get; set; }
        public bool is_searching { get; set; }
        public string search_key { get; set; }

    }
    
    public class MonTerminalDetailKegiatanModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<TerminalDetailKegiatan> data { get; set; }
    }

    public class TerminalDetailKegiatan
    {
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string nama_terminal { get; set; }
        public string no_pmh_bm { get; set; }
        public string nama_kapal { get; set; }
        public string kegiatan { get; set; }
        public string kd_barang { get; set; }
        public string nama_barang { get; set; }
        public string nama_dermaga { get; set; }
        public string jumlah { get; set; }
        public string kd_satuan { get; set; }
        public string jml_truk { get; set; }
        public string nm_pbm { get; set; }
        public string jmlh_plan { get; set; }
        public string jmlh_truck_plan { get; set; }
        public string voyage_id { get; set; }
        public string nama_agen { get; set; }
        public string jml_plan_bongkar { get; set; }
        public string jml_plan_muat { get; set; }
        public string jml_real_bongkar { get; set; }
        public string jml_real_muat { get; set; }
        public string kd_regional { get; set; }

    }

    public class ParamTerminalDetailKegiatan
    {
        public string no_pmh_bm { get; set; }
        public string kegiatan { get; set; }
        public string tipe { get; set; }
    }

    public class MonTerminalDetailKegiatanExportModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<TerminalDetailKegiatanExport> data { get; set; }
    }

    public class TerminalDetailKegiatanExport
    {
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string kawasan { get; set; }
        public string no_pmh_bm { get; set; }
        public string nama_kapal { get; set; }
        public string nm_pbm { get; set; }
        public string kd_barang { get; set; }
        public string nama_barang { get; set; }
        public string shift { get; set; }
        public string hari { get; set; }
        public string nama_dermaga { get; set; }
        public string alat_1 { get; set; }
        public string alat_2 { get; set; }
        public string alat_3 { get; set; }
        public string alat_4 { get; set; }
        public string jumlah { get; set; }
        public string kd_satuan { get; set; }
        public string jml_truck { get; set; }
        public string jam_mulai { get; set; }
        public string jam_selesai { get; set; }
        public DateTime start_work { get; set; }
        public string end_work { get; set; }
        public string kd_regional { get; set; }
    }

    public class ParamTerminalDetailKegiatanExport
    {
        public string no_pmh_bm { get; set; }
        public string kd_barang { get; set; }
        public string kegiatan { get; set; }
        public string tipe { get; set; }
    }
}

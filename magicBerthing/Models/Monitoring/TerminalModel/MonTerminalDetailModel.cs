using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring.TerminalModel
{
    public class MonTerminalDetailModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<TerminalDetail> data { get; set; }
    }

    public class TerminalDetail
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
        public string kegiatan2 { get; set; }
    }

    public class ParamTerminalDetail
    {
        public string kd_cabang_induk { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string type_terminal { get; set; }
        public string no_ppk_jasa { get; set; }
        public string kd_regional { get; set; }
        public string jenis_kapal { get; set; }
        public string status { get; set; }

    }

}

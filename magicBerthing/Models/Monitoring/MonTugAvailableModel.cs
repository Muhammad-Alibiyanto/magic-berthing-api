using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring
{
    public class MonTugAvailableModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public PagedList<TugAvailable> data { get; set; }
    }

    public class TugAvailable
    {
        public string kawasan { get; set; }
        public string nama_kawasan { get; set; }
        public string nama { get; set; }
        public string call_sign { get; set; }
        public string jam_kerja { get; set; }
        public string asal { get; set; }
        public string tujuan { get; set; }
        public string status { get; set; }
        public string kd_regional { get; set; }
        public string kode_cabang { get; set; }
        public string nama_kapal { get; set; }
    }

    public class ParamTug
    {
        public string call_sign { get; set; }
        public string kode_cabang { get; set; }
        public string kawasan { get; set; }
        public string status_tug { get; set; }
        public string kd_regional { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
        public string order_by_column { get; set; }
        public string order_by_sort { get; set; }
        public string created_date { get; set; }
        public string created_date_from { get; set; }
        public string created_date_to { get; set; }
        public string show_per_date { get; set; }
    }
}

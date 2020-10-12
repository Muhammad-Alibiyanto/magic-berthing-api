using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring.Warehouse
{
    public class MonWarehouseDataVAKContentModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public PagedList<WarehouseDataVAKContent> data { get; set; }
    }

    public class WarehouseDataVAKContent
    {
        public string nama_terminal { get; set; }
        public string nama_vak { get; set; }
        public string nama_barang { get; set; }
        public string pelanggan { get; set; }
        public string jumlah_real { get; set; }
        public DateTime tgl_mulai { get; set; }
        public string tipe_penumpukan { get; set; }
        public string occupied { get; set; }
        public string kd_region { get; set; }
        public DateTime created_date { get; set; }
        public string lama_tumpuk { get; set; }
        public string nama_kapal { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string kd_gudlap_d { get; set; }
        public string mglap_nama { get; set; }
        public string nama_regional { get; set; }
    }

    public class ParamWarehouseDataVAKContent
    {
        public string kd_region { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string nama_pelanggan { get; set; }
        public string nama_vak { get; set; }
        public bool is_searching { get; set; }
        public string search_key { get; set; }
        public string order_by_column { get; set; }
        public string order_by_sort { get; set; }
        public string created_date { get; set; }
        public string tgl_mulai { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
    }
}

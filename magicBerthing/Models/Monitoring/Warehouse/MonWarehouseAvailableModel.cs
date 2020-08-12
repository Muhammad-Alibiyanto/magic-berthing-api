using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring.Warehouse
{
    public class MonWarehouseAvailableModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public PagedList<WarehouseData> data { get; set; }
    }

    public class WarehouseData
    {
        public string pelanggan { get; set; }
        public int jumlah_barang { get; set; }
        public string kd_region { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string nama_vak { get; set; }
        public string nama_regional { get; set; }

    }

    public class ParamWarehouse
    {
        public string kd_region { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public bool is_searching { get; set; }
        public string search_key { get; set; }
        public string order_by_column { get; set; }
        public string order_by_sort { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
    }

}

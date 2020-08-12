using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring.Container
{
    public class MonContainerAvailableDetailModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public PagedList<ContainerDetailData> data { get; set; }
    }

    public class ContainerDetailData
    {
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string kd_regional { get; set; }
        public string area { get; set; }
        public string ves_name { get; set; }
        public string nama_pelanggan { get; set; }
        public string voyage_no { get; set; }
        public string container_no { get; set; }
        public string ctr_size { get; set; }
        public int jumlah_container { get; set; }
        public DateTime transact_date { get; set; }
        public DateTime tgl_penumpukan_disc { get; set; }
        public string nama_regional { get; set; }
    }

    public class ParamContainerDetail
    {
        public string kode_cabang { get; set; }
        public string kd_regional { get; set; }
        public string kd_terminal { get; set; }
        public string order_by_column { get; set; }
        public string order_by_sort { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
        public string area { get; set; }
        public bool is_searching { get; set; }
        public string search_key { get; set; }
    }

}

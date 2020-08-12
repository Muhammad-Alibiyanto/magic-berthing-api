using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring.Container
{
    public class MonContainerAvailableDetailContentModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public PagedList<ContainerContentData> data { get; set; }
    }

    public class ContainerContentData
    {
        public string nama_terminal { get; set; }
        public string area { get; set; }
        public string equipment { get; set; }
        public string ctr_type { get; set; }
        public string ctr_size { get; set; }
        public string container_no { get; set; }
        public string kd_regional { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string voyage_no { get; set; }
        public DateTime transact_date { get; set; }
        public string ves_name { get; set; }
        public string nama_pelanggan { get; set; }
        public string jumlah { get; set; }
        public DateTime tgl_penumpukan_recv { get; set; }
        public DateTime tgl_penumpukan_disc { get; set; }
        public string lama_penumpukan_recv { get; set; }
        public string lama_penumpukan_disc { get; set; }
        public string nama_regional { get; set; }


    }

    public class ParamContainerContent
    {
        public string kd_regional { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string area { get; set; }
        public string voyage_no { get; set; }
        public string container_no { get; set; }
        public string transact_date { get; set; }
        public string order_by_column { get; set; }
        public string order_by_sort { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
        public bool is_searching { get; set; }
        public string search_key { get; set; }
    }

}

using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring
{
    public class MonContainerAvailableModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public PagedList<ContainerData> data { get; set; }
    }

    public class ContainerData
    {
        public string nama_terminal { get; set; }
        public string blok { get; set; }
        public string total_r { get; set; }
        public string total_u { get; set; }
        public string kapasitas { get; set; }
        public string occupied { get; set; }
        public string kd_region { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string jumlah_all { get; set; }

    }

    public class ParamContainer
    {
        public string kd_region { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string order_by_column { get; set; }
        public string order_by_sort { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
    }

}

using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Master
{
    public class MasDermagaClusterModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<DermagaClusterData> data { get; set; }
    }

    public class DermagaClusterData
    {
        public string kode_cabang { get; set; }
        public string kode_dermaga { get; set; }
        public string nama_dermaga { get; set; }
        public int kade_awal { get; set; }
        public int kade_akhir { get; set; }
        public string nama_cluster { get; set; }
    }

    public class ParamDermagaCluster
    {
        public string komoditi { get; set; }
        public string kode_dermaga { get; set; }
        public int kode_cabang { get; set; }
    }
}

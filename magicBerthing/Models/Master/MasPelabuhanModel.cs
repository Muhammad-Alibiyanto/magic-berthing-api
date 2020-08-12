using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Master
{
    public class MasPelabuhanModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<PelabuhanData> data { get; set; }
    }

    public class PelabuhanData
    {
        public string kode_cabang { get; set; }
        public string kd_regional { get; set; }
        public string nama_pelabuhan { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Master
{
    public class MasVasaPelabuhanModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<VasaPelabuhanData> data { get; set; }
    }

    public class VasaPelabuhanData
    {
        public string kode_pelabuhan { get; set; }
        public string nama_pelabuhan { get; set; }
        public string kota { get; set; }
        public string kode_negara { get; set; }
    }
    public class ParamVasaPelabuhan
    {
        public string kode_pelabuhan { get; set; }
        public string nama_pelabuhan { get; set; }
        public bool is_search { get; set; }
    }
}

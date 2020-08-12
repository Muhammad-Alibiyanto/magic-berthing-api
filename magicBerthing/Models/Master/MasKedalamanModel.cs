using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Master
{
    public class MasKedalamanModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<Kedalaman> data { get; set; }
    }
    public class Kedalaman
    {
        public string kd_cabang { get; set; }
        public string tgl { get; set; }
        public string pasang_surut { get; set; }
        public string jam { get; set; }
        public string kd_regional { get; set; }
        public string id { get; set; }
    }

    public class ParamKedalaman
    {
        public string kd_regional { get; set; }
        public string kd_cabang { get; set; }
        public string tgl { get; set; }
        public string jam { get; set; }
    }
}

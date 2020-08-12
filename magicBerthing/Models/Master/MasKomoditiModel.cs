using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Master
{
    public class MasKomoditiModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<KomoditiData> data { get; set; }
    }

    public class KomoditiData
    {
        public string nama { get; set; }
        public string kategori { get; set; }
    }

    public class ParamKomoditi
    {
        public string search { get; set; }
    }
}

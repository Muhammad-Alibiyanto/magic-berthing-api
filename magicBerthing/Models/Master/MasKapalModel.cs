using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Master
{
    public class MasKapalModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public PagedList<KapalData> data { get; set; }
    }

    public class getCode
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    public class KapalData
    {
        public string kode_kapal { get; set; }
        public string nama_kapal { get; set; }
        public string jenis_kapal { get; set; }
        public string grt { get; set; }
        public string loa { get; set; }
        public string bendera { get; set; }

    }

    public class ParamKapal
    {
        public string kode_kapal { get; set; }
        public string nama_kapal { get; set; }
        public string jenis_kapal { get; set; }
        public string search_nama_kapal { get; set; }
        public string page { set; get; }
        public string limit { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring
{
	public class MasAreaModel
	{

        public class AreaModel
        {
            public string status { get; set; }
            public string message { get; set; }
            public int count { get; set; }
            public List<AreaData> data { get; set; }
        }

        public class getCode
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class AreaData
        {
            public string id { get; set; }
            public string area_name { get; set; }
            public string kd_regional { get; set; }
            public string kd_cabang { get; set; }
            public string kd_terminal { get; set; }
            public string id_pandu { get; set; }
            public string kode_cabang_induk { get; set; }
            public string kode_cabang { get; set; }
            public string kode_terminal { get; set; }
            public string nama_terminal { get; set; }



        }


        public class ParamArea
        {
            public string id { get; set; }
            public string kd_cabang { get; set; }
            public string kd_regional { get; set; }
            public string kd_terminal { get; set; }

            public string id_pandu { get; set; }

            // Parameter to know what screen call it
            public string current_screen { get; set; }

        }
    }
}

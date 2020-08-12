using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Master
{
	public class MasRegionalModel
	{
        public class RegionalModel
        {
            public string status { get; set; }
            public string message { get; set; }
            public int count { get; set; }
            public List<RegionalData> data { get; set; }
        }

        public class getCode
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        public class RegionalData
        {
            public string id { get; set; }
            public string regional_nama { get; set; }
       

        }

    }
}

using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring
{
	public class MonPanduAvailableModel
	{
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public PagedList<PanduAvailable> data { get; set; }
    }

    public class getCode
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    public class PanduAvailable
    {
        public string id_master_area { get; set; }
        public string nama { get; set; }
        public string call_sign { get; set; }
        public string kawasan { get; set; }
        public string nama_kapal { get; set; }
        public string tgl_work { get; set; }
        public string from_mdmg_nama { get; set; }
        public string to_mdmg_nama { get; set; }
        public string status { get; set; }
        public string kd_agen { get; set; }
        public string tgl_off { get; set; }
        public string kd_regional { get; set; }
        public string regional_nama { get; set; }
        public string urutan { get; set; }
        public string is_jamuang { get; set; }
        public string created_permohonan { get; set; }
        public string created_penetapan { get; set; }
        public string created_rea { get; set; }
        public string created_spkp { get; set; }
        public string tgl_mulai { get; set; }
        public string gerakan { get; set; }
        public string no_ppk1 { get; set; }
        public string nama_agen { get; set; }
        public string tgl_permohonan { get; set; }
    }

    public class ParamPandu 
    {
        public string kd_regional { get; set; }
        public string id_master_area { get; set; }
        public string is_jamuang { get; set; }
        public string status_pandu { get; set; }
        public string nama_pandu { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
        public string order_by_column { get; set; }
        public string order_by_sort { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public bool is_search { get; set; }
        public string search_key { get; set; }
        public string tgl_work { get; set; }
        public string tgl_off { get; set; }
        public string kawasan { get; set; }
    }
}

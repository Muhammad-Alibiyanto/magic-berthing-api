using magicBerthing.DataLogics.Master;
using magicBerthing.Models.Master;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasKapalController : ControllerBase
    {
        [Route("api/MasKapal/getKapal")]
        [HttpPost]
        [ActionName("getKapal")]
        public IActionResult getListKapal (ParamKapal data)
        {
            MasKapalDL dal = new MasKapalDL();

            MasKapalModel hasil = new MasKapalModel();

            /**
            * This params is for pagination function
            */
            if (!string.IsNullOrEmpty(data.limit) && data.limit != "string" && !string.IsNullOrEmpty(data.page) && data.page != "string")
            {
                data.page = data.page;
                data.limit = data.limit;
            }
            else if (!string.IsNullOrEmpty(data.page) && data.page != "string" && string.IsNullOrEmpty(data.limit) && data.limit != "string")
            {
                data.page = data.page;
                data.limit = "20";
            }
            else if (!string.IsNullOrEmpty(data.limit) && data.limit != "string" && string.IsNullOrEmpty(data.page) && data.page != "string")
            {
                data.page = "1";
                data.limit = data.limit;
            }
            else
            {
                data.page = "1";
                data.limit = "20";
            }

            IEnumerable<KapalData> result = dal.getDataKapal(data);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = new PagedList<KapalData>(result.ToList(), Convert.ToInt32(data.page), Convert.ToInt32(data.limit));

            return Ok(hasil);
        }
    }
}

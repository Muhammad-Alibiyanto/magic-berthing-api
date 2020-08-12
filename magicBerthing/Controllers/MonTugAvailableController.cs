using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using magicBerthing.DataLogics.Monitoring;
using magicBerthing.Models.Monitoring;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace magicBerthing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonTugAvailableController : ControllerBase
    {
        [Route("api/MonPermohonan/getTugAvailable")]
        [HttpPost]
        [ActionName("getTugAvailable")]
        public IActionResult getListTugAvailable(ParamTug data)
        {
            MonTugAvailableDL dal = new MonTugAvailableDL();

            MonTugAvailableModel hasil = new MonTugAvailableModel();

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
                data.limit = "10";
            }
            else if (!string.IsNullOrEmpty(data.limit) && data.limit != "string" && string.IsNullOrEmpty(data.page) && data.page != "string")
            {
                data.page = "1";
                data.limit = data.limit;
            }
            else
            {
                data.page = "1";
                data.limit = "10";
            }

            IEnumerable<TugAvailable> result = dal.getDataTugAvailabe(data);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = new PagedList<TugAvailable>(result.ToList(), Convert.ToInt32(data.page), Convert.ToInt32(data.limit));

            return Ok(hasil);
        }
    }
}

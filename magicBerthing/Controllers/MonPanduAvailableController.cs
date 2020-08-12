using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using magicBerthing.DataLogics.Monitoring;
using magicBerthing.Models.Monitoring;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace magicBerthing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonPanduAvailableController : ControllerBase
    {
        [Route("api/MonPermohonan/getPanduAvailable")]
        [HttpPost]
        [ActionName("getPanduAvailable")]
        public IActionResult getListPanduAvailable(ParamPandu data)
        {
            MonPanduAvailableDL dal = new MonPanduAvailableDL();

            MonPanduAvailableModel hasil = new MonPanduAvailableModel();

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

            IEnumerable<PanduAvailable> result = dal.getDataPanduAvailabe(data);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = new PagedList<PanduAvailable>(result.ToList(), Convert.ToInt32(data.page), Convert.ToInt32(data.limit));

            return Ok(hasil);
        }
    }
}
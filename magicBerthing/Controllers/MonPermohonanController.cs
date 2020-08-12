using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using magicBerthing.DataLogics.Monitoring;
using magicBerthing.Models.Monitoring;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace magicBerthing.Controllers
{
    [ApiController]
    public class MonPermohonanController : ControllerBase
    {
        [Route("api/MonPermohonan/getListSidaradina")]
        [HttpPost]
        [ActionName("getListSidaradina")]
        public IActionResult getListVoyStkByPeriod(reqSidaradina data)
        {
            MonPermohonanDL dal = new MonPermohonanDL();
            IEnumerable<vwSidaradina> result = dal.GetDataListSidaradina(data.NAMA_KAPAL);
            return Ok(result);
        }
    }
}
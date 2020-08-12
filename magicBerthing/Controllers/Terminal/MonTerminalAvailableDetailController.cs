using magicBerthing.DataLogics.Monitoring.Terminal;
using magicBerthing.Models.Monitoring.TerminalModel;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Controllers.Terminal
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonTerminalAvailableDetailController : ControllerBase
    {
        [Route("api/MonPermohonan/getTerminalDetailAvailable")]
        [HttpPost]
        [ActionName("getTerminalAvailable")]
        public IActionResult getListTerminalDetailAvailable(ParamTerminalDetail data)
        {
            MonTerminalAvailableDetailDL dal = new MonTerminalAvailableDetailDL();

            MonTerminalDetailModel hasil = new MonTerminalDetailModel();

            List<TerminalDetail> result = dal.getDataTerminalDetailAvailabe(data);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = result;


            return Ok(hasil);
        }
    }
}

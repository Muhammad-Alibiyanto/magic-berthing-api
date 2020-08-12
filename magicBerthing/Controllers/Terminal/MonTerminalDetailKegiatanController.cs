using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using magicBerthing.DataLogics.Monitoring;
using magicBerthing.Models.Monitoring;
using Microsoft.AspNetCore.Mvc;

namespace magicBerthing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonTerminalDetailKegiatanController : ControllerBase
    {
        [Route("api/MonPermohonan/getTerminalDetailKegiatan")]
        [HttpPost]
        [ActionName("getTerminalDetailKegiatan")]
        public IActionResult getTerminalDetailKegiatan(ParamTerminalDetailKegiatan data)
        {
            MonTerminalDetailKegiatanDL dal = new MonTerminalDetailKegiatanDL();

            MonTerminalDetailKegiatanModel hasil = new MonTerminalDetailKegiatanModel();

            IEnumerable<TerminalDetailKegiatan> result = dal.getDataTerminalDetailKegiatan(data);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = new List<TerminalDetailKegiatan>(result.ToList());

            return Ok(hasil);
        }
    }
}

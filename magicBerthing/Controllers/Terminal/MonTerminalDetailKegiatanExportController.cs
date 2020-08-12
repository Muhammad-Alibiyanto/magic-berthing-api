using magicBerthing.DataLogics.Monitoring;
using magicBerthing.Models.Monitoring;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonTerminalDetailKegiatanExportController : ControllerBase
    {
        [Route("api/MonPermohonan/getTerminalDetailKegiatanExport")]
        [HttpPost]
        [ActionName("getTerminalDetailKegiatanExport")]
        public IActionResult getTerminalDetailKegiatanExport(ParamTerminalDetailKegiatanExport data)
        {
            MonTerminalDetailKegiatanExportDL dal = new MonTerminalDetailKegiatanExportDL();

            MonTerminalDetailKegiatanExportModel hasil = new MonTerminalDetailKegiatanExportModel();

            IEnumerable<TerminalDetailKegiatanExport> result = dal.getDataTerminalDetailKegiatanExport(data);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = new List<TerminalDetailKegiatanExport>(result.ToList());

            return Ok(hasil);
        }
    }
}

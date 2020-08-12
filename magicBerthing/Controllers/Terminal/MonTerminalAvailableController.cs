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
    public class MonTerminalAvailableController : ControllerBase
    {
        [Route("api/MonPermohonan/getTerminalAvailable")]
        [HttpPost]
        [ActionName("getTerminalAvailable")]
        public IActionResult getListTerminalAvailable(ParamTerminal data)
            {
                MonTerminalAvailableDL dal = new MonTerminalAvailableDL();

                MonTerminalAvailableModel hasil = new MonTerminalAvailableModel();

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

                IEnumerable<TerminalAvailable> result = dal.getDataTerminalAvailabe(data);
                hasil.message = "Success";
                hasil.status = "S";
                hasil.count = result.Cast<Object>().Count();
                hasil.data = new PagedList<TerminalAvailable>(result.ToList(), Convert.ToInt32(data.page), Convert.ToInt32(data.limit));
                

                return Ok(hasil);
            }
    }
}

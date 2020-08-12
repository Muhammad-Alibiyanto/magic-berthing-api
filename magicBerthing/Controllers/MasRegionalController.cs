using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using magicBerthing.DataLogics.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static magicBerthing.Models.Master.MasRegionalModel;

namespace magicBerthing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasRegionalController : ControllerBase
    {
        [Route("api/masterRegional/getRegional")]
        [HttpPost]
        [ActionName("getRegional")]
        public IActionResult getRegional()
        {
            MasRegionalDL dal = new MasRegionalDL();




           // MasRegionalController hasil = new MasRegionalController();


         
                RegionalModel result = dal.getRegional();
            //    hasil.message = "Success";
              //  hasil.status = "S";
             //   hasil.count = result.Cast<Object>().Count();
              //  hasil.data = result.ToList();
        





            return Ok(result);
        }


    }
}
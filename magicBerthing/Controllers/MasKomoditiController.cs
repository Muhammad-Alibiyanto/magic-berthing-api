using magicBerthing.DataLogics.Master;
using magicBerthing.Models.Master;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasKomoditiController : ControllerBase
    {
        [Route("api/MasKomoditi/getKomoditi")]
        [HttpPost]
        [ActionName("getKomoditi")]
        public IActionResult getListKomoditi(ParamKomoditi paramKomoditi)
        {
            MasKomoditiDL dal = new MasKomoditiDL();

            MasKomoditiModel hasil = new MasKomoditiModel();

            IEnumerable<KomoditiData> result = dal.getKomoditiData(paramKomoditi);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = result.ToList();

            return Ok(hasil);
        }
    }
}

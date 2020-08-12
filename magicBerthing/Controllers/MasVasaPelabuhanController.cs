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

    public class MasVasaPelabuhanController : ControllerBase
    {

        [Route("api/MasVasaPelabuhan/getPelabuhan")]
        [HttpPost]
        [ActionName("getPelabuhan")]
        public IActionResult getVasaPelabuhanList(ParamVasaPelabuhan data)
        {
            MasVasaPelabuhanDL dal = new MasVasaPelabuhanDL();

            MasVasaPelabuhanModel hasil = new MasVasaPelabuhanModel();

            IEnumerable<VasaPelabuhanData> result = dal.getDataVasaPelabuhan(data);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = new List<VasaPelabuhanData>(result.ToList());

            return Ok(hasil);
        }

    }
}

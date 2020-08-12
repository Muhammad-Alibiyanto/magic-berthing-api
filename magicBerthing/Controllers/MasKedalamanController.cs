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
    public class MasKedalamanController : ControllerBase
    {
        [Route("api/MasKedalaman/getKedalaman")]
        [HttpPost]
        [ActionName("getKedalaman")]
        public IActionResult getKedalaman(ParamKedalaman data)
        {
            MasKedalamanDL dal = new MasKedalamanDL();

            MasKedalamanModel hasil = new MasKedalamanModel();

            IEnumerable<Kedalaman> result = dal.getKedalaman(data);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = result.ToList();

            return Ok(hasil);
        }
    }
}

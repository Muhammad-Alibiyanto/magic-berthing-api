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
    public class MasPelabuhanController : ControllerBase
    {
        [Route("api/MasPelabuhan/getPelabuhan")]
        [HttpPost]
        [ActionName("getPelabuhan")]
        public IActionResult getListPelabuhan()
        {
            MasPelabuhanDL dal = new MasPelabuhanDL();

            MasPelabuhanModel hasil = new MasPelabuhanModel();

            IEnumerable<PelabuhanData> result = dal.getDataPelabuhan();
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = result.ToList();

            return Ok(hasil);
        }
    }
}

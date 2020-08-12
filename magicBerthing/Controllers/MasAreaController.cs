using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using magicBerthing.DataLogics.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static magicBerthing.Models.Monitoring.MasAreaModel;

namespace magicBerthing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasAreaController : ControllerBase
    {
        [Route("api/MasArea/getArea")]
        [HttpPost]
        [ActionName("getArea")]
        public IActionResult getListArea(ParamArea data)
        {
            MasAreaDL dal = new MasAreaDL();




            AreaModel hasil = new AreaModel();

            if (!string.IsNullOrEmpty(data.kd_regional) && data.kd_regional != "string")
            {
                IEnumerable<AreaData> result = dal.getDataArea(data);
                hasil.message = "Success";
                hasil.status = "S";
                hasil.count = result.Cast<Object>().Count();
                hasil.data = result.ToList();
            }
            else
            {

                hasil.message = "Kode Regional Null !!!";
                hasil.status = "E";
                hasil.count = 0;


            }





            return Ok(hasil);
        }
    }
}
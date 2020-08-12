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
    public class MasDermagaClusterController : ControllerBase
    {
        [Route("api/MasDermagaCluster/getDermagaCluster")]
        [HttpPost]
        [ActionName("getDermagaCluster")]
        public IActionResult getListDermagaCluster(ParamDermagaCluster data)
        {
            MasDermagaClusterDL dal = new MasDermagaClusterDL();


            MasDermagaClusterModel hasil = new MasDermagaClusterModel();

            if (!string.IsNullOrEmpty(data.kode_dermaga) && data.kode_dermaga != "string")
            {
                IEnumerable<DermagaClusterData> result = dal.getDataArea(data);
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

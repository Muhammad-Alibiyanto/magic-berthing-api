using magicBerthing.DataLogics.Monitoring.Warehouse;
using magicBerthing.Models.Monitoring.Warehouse;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Controllers.Warehouse
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonWarehouseDataVAKContentController : ControllerBase
    {
        [Route("api/MonWarehouseDataVAKContent/getWarehouseDataVAKContent")]
        [HttpPost]
        [ActionName("getWarehouseDataVAKContent")]
        public IActionResult getWarehouseData(ParamWarehouseDataVAKContent data)
        {
            MonWarehouseDataVAKContentDL dal = new MonWarehouseDataVAKContentDL();

            MonWarehouseDataVAKContentModel hasil = new MonWarehouseDataVAKContentModel();

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

            IEnumerable<WarehouseDataVAKContent> result = dal.getWarehouseDataVAKContent(data);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = new PagedList<WarehouseDataVAKContent>(result.ToList(), Convert.ToInt32(data.page), Convert.ToInt32(data.limit));

            return Ok(hasil);
        }
    }
}

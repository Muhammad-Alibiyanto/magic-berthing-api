using magicBerthing.DataLogics.Monitoring.Container;
using magicBerthing.Models.Monitoring;
using magicBerthing.Models.Monitoring.Container;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Controllers.Container
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonContainerAvailableDetailController : ControllerBase
    {

        [Route("api/MonContainer/getContainerAvailableDetail")]
        [HttpPost]
        [ActionName("getContainerAvailableDetail")]
        public IActionResult getListContainerDetail(ParamContainerDetail data)
        {
            MonContainerAvailableDetailDL dal = new MonContainerAvailableDetailDL();

            MonContainerAvailableDetailModel hasil = new MonContainerAvailableDetailModel();

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

            if (!string.IsNullOrEmpty(data.kd_regional) && data.kd_regional != "string")
            {
                IEnumerable<ContainerDetailData> result = dal.getContainerAvailableDetailData(data);
                hasil.message = "Success";
                hasil.status = "S";
                hasil.count = result.Cast<Object>().Count();
                hasil.data = new PagedList<ContainerDetailData>(result.ToList(), Convert.ToInt32(data.page), Convert.ToInt32(data.limit));
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

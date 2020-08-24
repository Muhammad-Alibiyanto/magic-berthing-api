using magicBerthing.DataLogics.Monitoring.Notification;
using magicBerthing.Models.Monitoring.Notification;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Controllers.Notification
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        [Route("api/MonPermohonan/getNotification")]
        [HttpPost]
        [ActionName("getNotification")]
        public IActionResult getNotificationList(ParamNotification data)
        {
            NotificationDL dal = new NotificationDL();

            NotificationAvailable hasil = new NotificationAvailable();

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

            IEnumerable<NotificationData> result = dal.getNotificationData(data);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = new PagedList<NotificationData>(result.ToList(), Convert.ToInt32(data.page), Convert.ToInt32(data.limit));

            return Ok(hasil);
        }

        [Route("api/MonPermohonan/readNotification")]
        [HttpPost]
        [ActionName("readNotification")]
        public IActionResult readNotification(ParamNotification data)
        {
            NotificationDL dal = new NotificationDL();

            var hasil = dal.readNotification(data);

            return Ok(hasil);
        }

        [Route("api/MonPermohonan/unreadNotification")]
        [HttpPost]
        [ActionName("unreadNotification")]
        public IActionResult unreadNotification(ParamNotification data)
        {
            NotificationDL dal = new NotificationDL();

            var hasil = dal.unreadNotification(data);

            return Ok(hasil);
        }

        [Route("api/MonPermohonan/deleteNotification")]
        [HttpPost]
        [ActionName("deleteNotification")]
        public IActionResult deleteNotification(ParamNotification data)
        {
            NotificationDL dal = new NotificationDL();

            var hasil = dal.deleteNotification(data);

            return Ok(hasil);
        }

        [Route("api/MonPermohonan/clearNotification")]
        [HttpPost]
        [ActionName("clearNotification")]
        public IActionResult clearNotification(ParamNotification data)
        {
            NotificationDL dal = new NotificationDL();

            var hasil = dal.clearNotification(data);

            return Ok(hasil);
        }
    }
}

using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring.Notification
{
    public class NotificationAvailable
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public PagedList<NotificationData> data { get; set; }
    }

    public class NotificationData
    {
        public string message { get; set; }
        public string status { get; set; }
        public string kd_agen { get; set; }
        public string title { get; set; }
        public string is_read { get; set; }
        public string id { get; set; }
    }

    public class ParamNotification
    {
        public string is_read { get; set; }
        public string id { get; set; }
        public string kd_agen { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
        public string title { get; set; }
    }
}

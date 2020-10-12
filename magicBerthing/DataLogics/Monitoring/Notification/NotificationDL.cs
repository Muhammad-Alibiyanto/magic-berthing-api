using Dapper;
using magicBerthing.Models.Monitoring.Notification;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring.Notification
{
    public class NotificationDL
    {
        public IEnumerable<NotificationData> getNotificationData(ParamNotification paramNotification)
        {
            IEnumerable<NotificationData> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    string paramTitle = "";
                    if (!string.IsNullOrEmpty(paramNotification.title) && paramNotification.title != "string")
                    {
                        paramTitle = " AND TITLE='" + paramNotification.title + "'";
                    }

                    string paramId = "";
                    if (!string.IsNullOrEmpty(paramNotification.id) && paramNotification.id != "string")
                    {
                        paramId = " AND ID='" + paramNotification.id + "'";
                    }

                    string paramIsRead = "";
                    if (!string.IsNullOrEmpty(paramNotification.is_read) && paramNotification.is_read != "string")
                    {
                        paramIsRead = " AND IS_READ='" + paramNotification.is_read + "'";
                    }

                    string paramKdAgen = "";
                    if (!string.IsNullOrEmpty(paramNotification.kd_agen) && paramNotification.kd_agen != "string")
                    {
                        paramKdAgen = " WHERE KD_AGEN ='" + paramNotification.kd_agen + "'";
                    }

                    string sql = @"SELECT * FROM T_MAGIC_NOTIFICATION" + paramKdAgen + paramId + paramIsRead + paramTitle + " ORDER BY CREATED_DATE DESC";

                    result = connection.Query<NotificationData>(sql);
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }

        public string readNotification(ParamNotification paramNotification)
        {
            string result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    string paramTitle = "";
                    if (!string.IsNullOrEmpty(paramNotification.title) && paramNotification.title != "string")
                    {
                        paramTitle = " AND TITLE='" + paramNotification.title + "'";
                    }

                    string paramId = "";
                    if (!string.IsNullOrEmpty(paramNotification.id) && paramNotification.id != "string")
                    {
                        paramId = " AND ID='" + paramNotification.id + "'";

                    }

                    string paramKdAgen = "";
                    if (!string.IsNullOrEmpty(paramNotification.kd_agen) && paramNotification.kd_agen != "string")
                    {
                        paramKdAgen = " WHERE KD_AGEN ='" + paramNotification.kd_agen + "'";
                    }

                    string sql = @"UPDATE T_MAGIC_NOTIFICATION SET IS_READ='1'" + paramKdAgen + paramId + paramTitle;
 
                    var update = connection.Execute(sql);

                    if(update == 1)
                    {
                        result = "Success";
                    }
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }

        public string unreadNotification(ParamNotification paramNotification)
        {
            string result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    string paramTitle = "";
                    if (!string.IsNullOrEmpty(paramNotification.title) && paramNotification.title != "string")
                    {
                        paramTitle = " AND TITLE='" + paramNotification.title + "'";
                    }

                    string paramId = "";
                    if (!string.IsNullOrEmpty(paramNotification.id) && paramNotification.id != "string")
                    {
                        paramId = " AND ID='" + paramNotification.id + "'";

                    }

                    string paramKdAgen = "";
                    if (!string.IsNullOrEmpty(paramNotification.kd_agen) && paramNotification.kd_agen != "string")
                    {
                        paramKdAgen = " WHERE KD_AGEN ='" + paramNotification.kd_agen + "'";
                    }

                    string sql = @"UPDATE T_MAGIC_NOTIFICATION SET IS_READ='0'" + paramKdAgen + paramId + paramTitle;

                    var update = connection.Execute(sql);

                    if (update == 1)
                    {
                        result = "Success";
                    }
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }

        public string deleteNotification(ParamNotification paramNotification)
        {
            string result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    string paramTitle = "";
                    if (!string.IsNullOrEmpty(paramNotification.title) && paramNotification.title != "string")
                    {
                        paramTitle = " AND TITLE='" + paramNotification.title + "'";
                    }

                    string paramId = "";
                    if (!string.IsNullOrEmpty(paramNotification.id) && paramNotification.id != "string")
                    {
                        paramId = " AND ID='" + paramNotification.id + "'";

                    }

                    string paramKdAgen = "";
                    if (!string.IsNullOrEmpty(paramNotification.kd_agen) && paramNotification.kd_agen != "string")
                    {
                        paramKdAgen = " WHERE KD_AGEN ='" + paramNotification.kd_agen + "'";
                    }

                    string sql = @"DELETE FROM T_MAGIC_NOTIFICATION" + paramKdAgen + paramId + paramTitle;

                    var update = connection.Execute(sql);

                    if (update == 1)
                    {
                        result = "Success";
                    }
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }

        public string clearNotification(ParamNotification paramNotification)
        {
            string result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    string paramTitle = "";
                    if (!string.IsNullOrEmpty(paramNotification.title) && paramNotification.title != "string")
                    {
                        paramTitle = " AND TITLE='" + paramNotification.title + "'";
                    }

                    string paramKdAgen = "";
                    if (!string.IsNullOrEmpty(paramNotification.kd_agen) && paramNotification.kd_agen != "string")
                    {
                        paramKdAgen = " WHERE KD_AGEN ='" + paramNotification.kd_agen + "'";
                    }

                    string sql = @"DELETE FROM T_MAGIC_NOTIFICATION" + paramKdAgen + paramTitle;

                    var update = connection.Execute(sql);

                    result = "Success";
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }
    }
}

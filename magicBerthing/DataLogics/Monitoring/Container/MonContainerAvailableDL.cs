using Dapper;
using magicBerthing.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring.Container
{
    public class MonContainerAvailableDL
    {
        public IEnumerable<ContainerData> getContainerAvailableData(ParamContainer paramContainer)
        {
            IEnumerable<ContainerData> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    string paramKodeRegional = "";
                    if (!string.IsNullOrEmpty(paramContainer.kd_region) && paramContainer.kd_region != "string")
                    {
                        paramKodeRegional = " WHERE KD_REGION ='" + paramContainer.kd_region + "'";
                    }

                    string paramKodeCabang = "";
                    if (!string.IsNullOrEmpty(paramContainer.kd_cabang) && paramContainer.kd_cabang != "string")
                    {
                        paramKodeCabang = " AND KD_CABANG ='" + paramContainer.kd_cabang + "'";

                    }

                    string paramKodeTerminal = "";
                    if (!string.IsNullOrEmpty(paramContainer.kd_terminal) && paramContainer.kd_terminal != "string")
                    {
                        paramKodeTerminal = " AND KD_TERMINAL ='" + paramContainer.kd_terminal + "'";

                    }

                    string paramOrderby = "";
                    if (!string.IsNullOrEmpty(paramContainer.order_by_column) && paramContainer.order_by_column != "string" && !string.IsNullOrEmpty(paramContainer.order_by_sort) && paramContainer.order_by_sort != "string")
                    {
                        paramOrderby = " ORDER BY " + paramContainer.order_by_column + " " + paramContainer.order_by_sort;
                    }


                    string sql = "SELECT * FROM VW_STORAGE_CONT_DETAIL_NEW " + paramKodeRegional + paramKodeCabang + paramKodeTerminal + paramOrderby;

                    result = connection.Query<ContainerData>(sql, new
                    {
                        KD_REGIONAL = paramContainer.kd_region
                    });
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

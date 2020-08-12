using Dapper;
using magicBerthing.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring
{
    public class MonTugAvailableDL
    {
        public IEnumerable<TugAvailable> getDataTugAvailabe(ParamTug paramTug)
        {
            IEnumerable<TugAvailable> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    /*  string fnoPermohonan = "";
                      if (!string.IsNullOrEmpty(NoPermohonan) && NoPermohonan != "7")
                      {
                          fnoPermohonan = "   AND A.NO_PERMOHONAN='" + NoPermohonan + "'  ";
                      }



      */
                    string paramKodeRegional = "";
                    if (!string.IsNullOrEmpty(paramTug.kd_regional) && paramTug.kd_regional != "string")
                    {
                        paramKodeRegional = " WHERE KD_REGIONAL ='" + paramTug.kd_regional + "'";
                    }

                    string paramCallSign = "";
                    if (!string.IsNullOrEmpty(paramTug.call_sign) && paramTug.call_sign != "string")
                    {
                        paramCallSign = " AND CALL_SIGN ='" + paramTug.call_sign + "'";

                    }

                    string paramKodeCabang = "";
                    if (!string.IsNullOrEmpty(paramTug.kode_cabang) && paramTug.kode_cabang!= "string")
                    {
                        paramKodeCabang= " AND KODE_CABANG ='" + paramTug.kode_cabang + "'";

                    }

                    string paramStatusTug = "";
                    if (!string.IsNullOrEmpty(paramTug.status_tug) && paramTug.status_tug != "string")
                    {
                        if (paramTug.status_tug == "AVAILABLE")
                        {
                            paramStatusTug = " AND STATUS=0";
                        }
                        if (paramTug.status_tug == "WORKING")
                        {
                            paramStatusTug = " AND STATUS=1";
                        }

                    }

                    string paramKawasanTug = "";
                    if (!string.IsNullOrEmpty(paramTug.kawasan) && paramTug.kawasan != "string")
                    {
                        if (paramTug.status_tug == "HISTORY")
                        {
                            paramKawasanTug = " AND NAMA_KAWASAN='" + paramTug.kawasan + "'";
                        }
                        else
                        {
                            paramKawasanTug = " AND KAWASAN='" + paramTug.kawasan + "'";
                        }
                    }

                    string paramOrderby = "";
                    if (!string.IsNullOrEmpty(paramTug.order_by_column) && paramTug.order_by_column != "string" && !string.IsNullOrEmpty(paramTug.order_by_sort) && paramTug.order_by_sort != "string")
                    {
                        paramOrderby = " ORDER BY " + paramTug.order_by_column + " " + paramTug.order_by_sort;
                    }

                    string paramCreatedDate = "";
                    if (!string.IsNullOrEmpty(paramTug.created_date) && paramTug.created_date != "string")
                    {
                        paramCreatedDate = " AND TRUNC(CREATED_DATE)=TO_DATE('" + paramTug.created_date + "', 'YYYY-MM-DD')";
                        /*if (paramTug.status_tug == "HISTORY")
                        {
                            if(paramTug.show_per_date == "day")
                            {
                                paramCreatedDate = " AND CREATED_DATE=TO_DATE('" + paramTug.created_date + "', 'YYYY-MM-DD HH24:MI:SS')";
                            }
                            else
                            {
                                paramCreatedDate = " AND CREATED_DATE BETWEEN TO_DATE('" + paramTug.created_date_from + "', 'YYYY-MM-DD HH24:MI:SS') AND TO_DATE('" + paramTug.created_date_to + "', 'YYYY-MM-DD HH24:MI:SS')";
                            }
                        }
                        else
                        {
                            paramCreatedDate = " AND JAM_KERJA=TO_DATE(" + paramTug.created_date + ", 'YYYY-MM-DD HH24:MI:SS')";
                        }*/
                    }

                    string sql = "";
                    if (paramTug.status_tug == "HISTORY")
                    {
                        sql = "SELECT * FROM VW_HISTORY_TUGGING_SERVICES " + paramKodeRegional + paramKawasanTug + paramCreatedDate + paramOrderby;
                    }
                    else
                    {
                        sql = "SELECT * FROM VW_PILOT_TUGG_SERVICES_DETAIL " + paramKodeRegional + paramCallSign + paramKodeCabang + paramStatusTug + paramKawasanTug + paramCreatedDate + paramOrderby;
                    }

                    result = connection.Query<TugAvailable>(sql, new
                    {
                        KD_REGIONAL = paramTug.kd_regional
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

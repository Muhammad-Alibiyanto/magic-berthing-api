using Dapper;
using magicBerthing.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Data;

namespace magicBerthing.DataLogics.Monitoring
{
    public class MonPanduAvailableDL
	{
        public IEnumerable<PanduAvailable> getDataPanduAvailabe(ParamPandu paramPandu)
        {
            IEnumerable<PanduAvailable> result = null;

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
                    if (!string.IsNullOrEmpty(paramPandu.kd_regional) && paramPandu.kd_regional != "string")
                    {
                        paramKodeRegional = " WHERE KD_REGIONAL='" + paramPandu.kd_regional + "'";

                    }

                    string paramCallSign = "";
                    if (!string.IsNullOrEmpty(paramPandu.call_sign) && paramPandu.call_sign != "string")
                    {
                        paramCallSign = " AND CALL_SIGN ='"+ paramPandu.call_sign + "'";
                    
                    }

                    string paramIdMasterArea = "";
                    if (!string.IsNullOrEmpty(paramPandu.id_master_area) && paramPandu.id_master_area != "string")
                    {
                        paramIdMasterArea = " AND ID_MASTER_AREA='" + paramPandu.id_master_area + "'";
                    }

                    string paramNamaPandu = "";
                    if (!string.IsNullOrEmpty(paramPandu.nama_pandu) && paramPandu.nama_pandu != "string")
                    {
                        paramNamaPandu = " AND NAMA ='" + paramPandu.nama_pandu + "'";
                    }


                    string paramKawasan = "";
                    if (!string.IsNullOrEmpty(paramPandu.kawasan) && paramPandu.kawasan !="string")
                    {
                        paramKawasan = " AND KAWASAN='" + paramPandu.kawasan + "'";

                    }

                    string paramStatusPandu = "";
                    if (!string.IsNullOrEmpty(paramPandu.status_pandu) && paramPandu.status_pandu != "string")
                    {
                        if (!string.IsNullOrEmpty(paramPandu.kd_regional) && paramPandu.kd_regional != "string")
                        {
                            if (paramPandu.status_pandu == "AVAILABLE")
                            {
                                paramStatusPandu = " AND STATUS IN ('AVAILABLE', 'AVAILABLE_JAMUANG')";
                            }
                            else if (paramPandu.status_pandu == "HISTORY")
                            {
                                paramStatusPandu = " AND STATUS='HISTORY'";
                            }
                            else if (paramPandu.status_pandu == "WORK")
                            {
                                paramStatusPandu = " AND STATUS IN ('DEPARTURE', 'ARRIVAL', 'SHIFTING')";
                            }
                            else if (paramPandu.status_pandu == "RENCANA")
                            {
                                paramStatusPandu = " AND STATUS IN ('PERMOHONAN', 'PENETAPAN', 'SPK1')";
                            }
                            else
                            {
                                paramStatusPandu = " AND STATUS='" + paramPandu.status_pandu + "'";
                            }
                        }
                        else
                        {
                            if (paramPandu.status_pandu == "AVAILABLE")
                            {
                                paramStatusPandu = " WHERE STATUS IN ('AVAILABLE', 'AVAILABLE_JAMUANG')";
                            }
                            else if (paramPandu.status_pandu == "HISTORY")
                            {
                                paramStatusPandu = " WHERE STATUS='HISTORY'";
                            }
                            else if (paramPandu.status_pandu == "WORK")
                            {
                                paramStatusPandu = " WHERE STATUS IN ('DEPARTURE', 'ARRIVAL', 'SHIFTING')";
                            }
                            else if (paramPandu.status_pandu == "RENCANA")
                            {
                                paramStatusPandu = " WHERE STATUS IN ('PERMOHONAN', 'PENETAPAN', 'SPK1')";
                            }
                            else
                            {
                                paramStatusPandu = " WHERE STATUS='" + paramPandu.status_pandu + "'";
                            }
                        }

                    }

                    string paramOrderby = "";
                    if (!string.IsNullOrEmpty(paramPandu.order_by_column) && paramPandu.order_by_column != "string" && !string.IsNullOrEmpty(paramPandu.order_by_sort) && paramPandu.order_by_sort != "string")
                    {
                        if(paramPandu.order_by_column == "URUTAN")
                        {
                            paramOrderby = " ORDER BY TO_NUMBER(" + paramPandu.order_by_column + ") " + paramPandu.order_by_sort;
                        }
                        else
                        {
                            paramOrderby = " ORDER BY " + paramPandu.order_by_column + " " + paramPandu.order_by_sort;
                        }
                    }

                    string paramCreatedAt = "";
                    if (!string.IsNullOrEmpty(paramPandu.created_date) && paramPandu.created_date!= "string")
                    {
                        paramCreatedAt = " AND TRUNC(TGL_WORK)=TO_DATE('" + paramPandu.created_date + "', 'YYYY-MM-DD')";
                    }

                    string paramSearch = "";
                    if (!string.IsNullOrEmpty(paramPandu.search_key) && paramPandu.search_key != "string" && paramPandu.is_search == true)
                    {
                        if (!string.IsNullOrEmpty(paramPandu.kd_regional) && paramPandu.kd_regional != "string")
                        {
                            paramSearch = " WHERE NAMA LIKE '%" + paramPandu.search_key + "%' OR NAMA_KAPAL LIKE '%" + paramPandu.search_key + "%'";
                        }
                        else
                        {
                            var one_month_before = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss");
                            paramSearch = " WHERE NAMA LIKE '%" + paramPandu.search_key + "' OR NAMA_KAPAL LIKE '%" + paramPandu.search_key + "%' AND STATUS NOT IN ('AVAILABLE', 'AVAILABLE_JAMUANG') AND (TGL_OFF >= '" + one_month_before + "' OR TGL_OFF IS NULL)";
                        }
                    }

                    string paramNoPpk1 = "";
                    if (!string.IsNullOrEmpty(paramPandu.no_ppk1) && paramPandu.no_ppk1 != "string")
                    {
                        paramNoPpk1 = " AND NO_PPK1='" + paramPandu.no_ppk1 + "'";
                    }

                    string sql = @"SELECT * FROM (" +
                                    "SELECT * FROM VW_MAGIC_PILOT_INFORMATION " + paramKodeRegional + paramNamaPandu + paramCallSign + paramIdMasterArea + paramKawasan + paramStatusPandu + paramNoPpk1 + paramCreatedAt + paramOrderby +
                                   ")" + paramSearch;

                    result = connection.Query<PanduAvailable>(sql, new
                    {
                        KD_REGIONAL = paramPandu.kd_regional
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

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
                    string paramKodeRegional = "";
                    if (!string.IsNullOrEmpty(paramPandu.kd_regional) && paramPandu.kd_regional != "string")
                    {
                        paramKodeRegional = " WHERE KD_REGIONAL='" + paramPandu.kd_regional + "'";

                    }

                    string paramIdMasterArea = "";
                    if (!string.IsNullOrEmpty(paramPandu.id_master_area) && paramPandu.id_master_area != "string")
                    {
                        if (paramPandu.is_jamuang != "1" && paramPandu.id_master_area != "5" && paramPandu.id_master_area != "6" && paramPandu.id_master_area != "4")
                        {
                            paramIdMasterArea = " AND ID_MASTER_AREA='" + paramPandu.id_master_area + "'";
                        }
                    }

                    string paramNamaPandu = "";
                    if (!string.IsNullOrEmpty(paramPandu.nama_pandu) && paramPandu.nama_pandu != "string")
                    {
                        paramNamaPandu = " AND NAMA ='" + paramPandu.nama_pandu + "'";
                    }

                    string paramIsJamuang = "";
                    if (!string.IsNullOrEmpty(paramPandu.is_jamuang) && paramPandu.is_jamuang!= "string")
                    {
                        if (paramPandu.id_master_area == "1")
                        {
                            paramIsJamuang = " AND IS_JAMUANG='1'";
                        }
                        else
                        {
                            paramIsJamuang = " AND IS_JAMUANG IN ('1', '0')";
                        }

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
                            paramSearch = " WHERE NAMA LIKE '%" + paramPandu.search_key + "%' OR NAMA_KAPAL LIKE '%" + paramPandu.search_key + "%' OR NO_PPK1='" + paramPandu.search_key + "'";
                        }
                        else
                        {
                            var one_month_before = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss");
                            paramSearch = " WHERE NAMA LIKE '%" + paramPandu.search_key + "' OR NAMA_KAPAL LIKE '%" + paramPandu.search_key + "%' OR NO_PPK1='" + paramPandu.search_key.ToString() + "' AND STATUS NOT IN ('AVAILABLE', 'AVAILABLE_JAMUANG') AND (TGL_OFF >= '" + one_month_before + "' OR TGL_OFF IS NULL)";
                        }
                    }

                    string paramAsalTujuan = "";
                    if (!string.IsNullOrEmpty(paramPandu.id_master_area) && paramPandu.id_master_area != "string")
                    {
                        if (paramPandu.status_pandu == "RENCANA")
                        {
                            if (paramPandu.id_master_area == "6")
                            {
                                paramAsalTujuan = " AND (FROM_MDMG_NAMA IN('LAMONG CURAH KERING', 'LAMONG NUSANTARA', 'LAMONG OCEAN GOING') OR TO_MDMG_NAMA IN('LAMONG CURAH KERING', 'LAMONG NUSANTARA', 'LAMONG OCEAN GOING'))";
                            }
                            if (paramPandu.id_master_area == "5")
                            {
                                paramAsalTujuan = " AND (FROM_MDMG_NAMA IN('TPS/ICT OCEAN GOING', 'ICT NUSANTARA') OR TO_MDMG_NAMA IN('TPS/ICT OCEAN GOING', 'ICT NUSANTARA'))";
                            }
                        }
                    }

                    string paramKawasan = "";
                    if (!string.IsNullOrEmpty(paramPandu.id_master_area) && paramPandu.id_master_area != "string")
                    {
                        if (paramPandu.id_master_area == "4")
                        {
                            paramKawasan = " AND KAWASAN='GRESIK'";
                        }
                        if (paramPandu.id_master_area == "2")
                        {
                            paramKawasan = " AND KAWASAN='SURABAYA'";
                        }
                        if (paramPandu.id_master_area == "5")
                        {
                            paramKawasan = " AND KAWASAN='TPS'";
                        }
                        if (paramPandu.id_master_area == "6")
                        {
                            paramKawasan = " AND KAWASAN='TTL'";
                        }
                    }

                    string sql = @"SELECT * FROM (" +
                                    "SELECT * FROM VW_MAGIC_PILOT_INFORMATION " + paramKodeRegional + paramIdMasterArea + paramNamaPandu + paramStatusPandu + paramIsJamuang + paramKawasan + paramAsalTujuan + paramCreatedAt + paramOrderby +
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

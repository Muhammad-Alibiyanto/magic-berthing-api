using Dapper;
using magicBerthing.Models.Monitoring.Container;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring.Container
{
    public class MonContainerAvailableDetailContentDL
    {
        public IEnumerable<ContainerContentData> getContainerAvailableDetailDataContent(ParamContainerContent paramContainerContent)
        {
            IEnumerable<ContainerContentData> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    string paramKodeRegional = "";
                    if (!string.IsNullOrEmpty(paramContainerContent.kd_regional) && paramContainerContent.kd_regional != "string")
                    {
                        paramKodeRegional = " AND T_STORAGE_CONTAINER_BOX_DETAIL.KD_REGIONAL ='" + paramContainerContent.kd_regional + "'";
                    }

                    string paramKodeCabang = "";
                    if (!string.IsNullOrEmpty(paramContainerContent.kd_cabang) && paramContainerContent.kd_cabang != "string")
                    {
                        paramKodeCabang = " AND T_STORAGE_CONTAINER_BOX_DETAIL.KD_CABANG ='" + paramContainerContent.kd_cabang + "'";
                    }

                    string paramKodeTerminal = "";
                    if (!string.IsNullOrEmpty(paramContainerContent.kd_terminal) && paramContainerContent.kd_terminal != "string")
                    {
                        paramKodeTerminal = " AND T_STORAGE_CONTAINER_BOX_DETAIL.KD_TERMINAL ='" + paramContainerContent.kd_terminal + "'";
                    }

                    string paramArea = "";
                    if (!string.IsNullOrEmpty(paramContainerContent.area) && paramContainerContent.area != "string")
                    {
                        paramArea = " AND T_STORAGE_CONTAINER_BOX_DETAIL.AREA ='" + paramContainerContent.area + "'";
                    }

                    string paramVoyageNo = "";
                    if (!string.IsNullOrEmpty(paramContainerContent.voyage_no) && paramContainerContent.voyage_no != "string")
                    {
                        paramVoyageNo = " AND T_STORAGE_CONTAINER_BOX_DETAIL.VOYAGE_NO ='" + paramContainerContent.voyage_no + "'";
                    }

                    string paramContainerNo = "";
                    if (!string.IsNullOrEmpty(paramContainerContent.container_no) && paramContainerContent.container_no != "string")
                    {
                        if (!string.IsNullOrEmpty(paramContainerContent.kd_regional) && paramContainerContent.kd_regional != "string")
                        {
                            paramContainerNo = " AND T_STORAGE_CONTAINER_BOX_DETAIL.CONTAINER_NO ='" + paramContainerContent.container_no + "'";
                        }
                        else
                        {
                            paramContainerNo = " AND T_STORAGE_CONTAINER_BOX_DETAIL.CONTAINER_NO ='" + paramContainerContent.container_no + "'";
                        }
                    }

                    string paramTransactDate = "";
                    if (!string.IsNullOrEmpty(paramContainerContent.transact_date) && paramContainerContent.transact_date!= "string")
                    {
                        paramTransactDate = " AND TO_CHAR(T_STORAGE_CONTAINER_BOX_DETAIL.TRANSACT_DATE, 'YYYY-MM-DD HH24:MI:SS') = '" + paramContainerContent.transact_date + "'";
                    }

                    string paramSort = "";
                    if (!string.IsNullOrEmpty(paramContainerContent.order_by_column) && paramContainerContent.order_by_column != "string" && !string.IsNullOrEmpty(paramContainerContent.order_by_sort) && paramContainerContent.order_by_sort != "string")
                    {
                        paramSort = " ORDER BY T_STORAGE_CONTAINER_BOX_DETAIL." + paramContainerContent.order_by_column + " " + paramContainerContent.order_by_sort;
                    }

                    string paramSearch = "";
                    if (paramContainerContent.is_searching == true && !string.IsNullOrEmpty(paramContainerContent.search_key) && paramContainerContent.search_key != "string")
                    {
                        paramSearch = " WHERE NAMA_PELANGGAN LIKE '" + paramContainerContent.search_key + "%' OR VOYAGE_NO LIKE '" + paramContainerContent.search_key + "%' OR CONTAINER_NO LIKE '" + paramContainerContent.search_key + "%' OR VES_NAME LIKE '" + paramContainerContent.search_key + "%'";
                    }

                    var sql = "SELECT * FROM (" +
                                "SELECT T_STORAGE_CONTAINER_BOX_DETAIL.*, APP_REGIONAL.REGIONAL_NAMA NAMA_REGIONAL FROM T_STORAGE_CONTAINER_BOX_DETAIL JOIN APP_REGIONAL ON T_STORAGE_CONTAINER_BOX_DETAIL.KD_REGIONAL=APP_REGIONAL.ID AND APP_REGIONAL.PARENT_ID IS NULL AND APP_REGIONAL.ID NOT IN (12300000,20300001)" + paramKodeRegional + paramKodeCabang + paramKodeTerminal + paramArea + paramVoyageNo + paramContainerNo + paramTransactDate + paramSort + 
                              ")" + paramSearch;

                    result = connection.Query<ContainerContentData>(sql, new
                    {
                        KD_REGIONAL = paramContainerContent.kd_regional
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

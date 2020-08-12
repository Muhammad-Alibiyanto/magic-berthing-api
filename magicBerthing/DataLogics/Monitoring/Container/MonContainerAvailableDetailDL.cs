using Dapper;
using magicBerthing.Models.Monitoring.Container;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring.Container
{
    public class MonContainerAvailableDetailDL
    {
        public IEnumerable<ContainerDetailData> getContainerAvailableDetailData(ParamContainerDetail paramContainerDetail)
        {
            IEnumerable<ContainerDetailData> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    string paramKodeRegional = "";
                    if (!string.IsNullOrEmpty(paramContainerDetail.kd_regional) && paramContainerDetail.kd_regional!= "string")
                    {
                        paramKodeRegional = " AND T_STORAGE_CONTAINER_BOX_DETAIL.KD_REGIONAL ='" + paramContainerDetail.kd_regional+ "'";
                    }

                    string paramKodeCabang = "";
                    if (!string.IsNullOrEmpty(paramContainerDetail.kode_cabang) && paramContainerDetail.kode_cabang!= "string")
                    {
                        paramKodeCabang = " AND T_STORAGE_CONTAINER_BOX_DETAIL.KD_CABANG ='" + paramContainerDetail.kode_cabang + "'";
                    }

                    string paramKodeTerminal = "";
                    if (!string.IsNullOrEmpty(paramContainerDetail.kd_terminal) && paramContainerDetail.kd_terminal != "string")
                    {
                        paramKodeCabang = " AND T_STORAGE_CONTAINER_BOX_DETAIL.KD_TERMINAL ='" + paramContainerDetail.kd_terminal + "'";
                    }

                    string paramArea = "";
                    if (!string.IsNullOrEmpty(paramContainerDetail.area) && paramContainerDetail.area != "string")
                    {
                        paramKodeCabang = " AND T_STORAGE_CONTAINER_BOX_DETAIL.AREA ='" + paramContainerDetail.area + "'";
                    }

                    string paramSort = "";
                    if (!string.IsNullOrEmpty(paramContainerDetail.order_by_column) && paramContainerDetail.order_by_column!= "string" && !string.IsNullOrEmpty(paramContainerDetail.order_by_sort) && paramContainerDetail.order_by_sort != "string")
                    {
                        paramSort = " ORDER BY T_STORAGE_CONTAINER_BOX_DETAIL." + paramContainerDetail.order_by_column + " " + paramContainerDetail.order_by_sort;
                    }

                    string paramSearch = "";
                    if (paramContainerDetail.is_searching == true && !string.IsNullOrEmpty(paramContainerDetail.search_key) && paramContainerDetail.search_key != "string")
                    {
                        paramSearch = " WHERE NAMA_PELANGGAN LIKE '" + paramContainerDetail.search_key + "%' OR VOYAGE_NO LIKE '" + paramContainerDetail.search_key + "%' OR VES_NAME LIKE '" + paramContainerDetail.search_key + "%' OR CONTAINER_NO LIKE '" + paramContainerDetail.search_key + "%'";
                    }

                    var sql = @"SELECT * FROM (" +
                                    "SELECT " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.KD_CABANG, " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.KD_TERMINAL, " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.KD_REGIONAL, " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.AREA, " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.NAMA_PELANGGAN, " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.VOYAGE_NO, " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.CTR_SIZE, " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.VES_NAME, " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.TRANSACT_DATE, " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.TGL_PENUMPUKAN_DISC, " +
                                    "COUNT(T_STORAGE_CONTAINER_BOX_DETAIL.VOYAGE_NO) JUMLAH_CONTAINER, " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.CONTAINER_NO, " +
                                    "APP_REGIONAL.REGIONAL_NAMA NAMA_REGIONAL " +
                                    "FROM " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL JOIN APP_REGIONAL ON " +
                                    "T_STORAGE_CONTAINER_BOX_DETAIL.KD_REGIONAL=APP_REGIONAL.ID AND " +
                                    "APP_REGIONAL.PARENT_ID IS NULL AND " +
                                    "APP_REGIONAL.ID NOT IN (12300000,20300001)" + paramKodeRegional + paramKodeCabang + paramKodeTerminal + paramArea + 
                                    " GROUP BY NAMA_PELANGGAN, AREA, VES_NAME, KD_CABANG, KD_TERMINAL, KD_REGIONAL, VOYAGE_NO, CTR_SIZE, TRANSACT_DATE, TGL_PENUMPUKAN_DISC, CONTAINER_NO, REGIONAL_NAMA" +
                                ")" + paramSearch;

                    result = connection.Query<ContainerDetailData>(sql, new
                    {
                        KD_REGIONAL = paramContainerDetail.kd_regional
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

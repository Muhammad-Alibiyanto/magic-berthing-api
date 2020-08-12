using Dapper;
using magicBerthing.Models.Monitoring.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring.Warehouse
{
    public class MonWarehouseAvailableDL
    {
        public IEnumerable<WarehouseData> getWarehouseData(ParamWarehouse paramWarehouse)
        {
            IEnumerable<WarehouseData> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    string paramKdRegion = null;
                    if (!string.IsNullOrEmpty(paramWarehouse.kd_region) && paramWarehouse.kd_region != "string")
                    {
                        paramKdRegion = " AND T_STORAGE_CARGO_DETAIL.KD_REGION='" + paramWarehouse.kd_region + "'";
                    }

                    string paramKdCabang = null;
                    if (!string.IsNullOrEmpty(paramWarehouse.kd_cabang) && paramWarehouse.kd_cabang != "string")
                    {
                        paramKdCabang = " AND T_STORAGE_CARGO_DETAIL.KD_CABANG='" + paramWarehouse.kd_cabang + "'";
                    }

                    string paramKdTerminal = null;
                    if (!string.IsNullOrEmpty(paramWarehouse.kd_terminal) && paramWarehouse.kd_terminal != "string")
                    {
                        paramKdTerminal = " AND T_STORAGE_CARGO_DETAIL.KD_TERMINAL='" + paramWarehouse.kd_terminal + "'";
                    }

                    string paramSearch = null;
                    if (!string.IsNullOrEmpty(paramWarehouse.search_key) && paramWarehouse.search_key != "string" && paramWarehouse.is_searching == true)
                    {
                        paramSearch = " WHERE PELANGGAN LIKE '" + paramWarehouse.search_key + "%'";
                    }

                    string paramSort = null;
                    if (!string.IsNullOrEmpty(paramWarehouse.order_by_column) && paramWarehouse.order_by_column != "string" && !string.IsNullOrEmpty(paramWarehouse.order_by_sort) && paramWarehouse.order_by_sort != "string")
                    {
                        paramSort = " ORDER BY T_STORAGE_CARGO_DETAIL." + paramWarehouse.order_by_column + " " + paramWarehouse.order_by_sort;
                    }

                    string sql = "SELECT * FROM (" +
                                    "SELECT " +
                                    "DISTINCT(T_STORAGE_CARGO_DETAIL.PELANGGAN), " +
                                    "COUNT(T_STORAGE_CARGO_DETAIL.NAMA_BARANG) JUMLAH_BARANG, " +
                                    "T_STORAGE_CARGO_DETAIL.NAMA_VAK, " +
                                    "T_STORAGE_CARGO_DETAIL.KD_REGION, " +
                                    "T_STORAGE_CARGO_DETAIL.KD_TERMINAL, " +
                                    "T_STORAGE_CARGO_DETAIL.KD_CABANG, " +
                                    "APP_REGIONAL.REGIONAL_NAMA NAMA_REGIONAL " +
                                    "FROM T_STORAGE_CARGO_DETAIL JOIN APP_REGIONAL " +
                                    "ON T_STORAGE_CARGO_DETAIL.KD_REGION=APP_REGIONAL.ID " +
                                    "AND APP_REGIONAL.PARENT_ID IS NULL " +
                                    "AND APP_REGIONAL.ID NOT IN (12300000,20300001)" + paramKdRegion + paramKdCabang + paramKdTerminal +
                                    " GROUP BY PELANGGAN, KD_REGION, KD_CABANG, KD_TERMINAL, NAMA_VAK, REGIONAL_NAMA " + paramSort +
                                  ")" + paramSearch;
                    result = connection.Query<WarehouseData>(sql);

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

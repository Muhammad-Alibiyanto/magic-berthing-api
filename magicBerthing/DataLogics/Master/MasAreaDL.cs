using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static magicBerthing.Models.Monitoring.MasAreaModel;

namespace magicBerthing.DataLogics.Master
{
	public class MasAreaDL
	{
        public IEnumerable<AreaData> getDataArea(ParamArea paramArea)
        {
            IEnumerable<AreaData> result = null;

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
                    if (!string.IsNullOrEmpty(paramArea.kd_regional) && paramArea.kd_regional != "string")
                    {
                        if (paramArea.kd_regional == "12000001" && paramArea.current_screen == "PilotInformation")
                        {
                            paramKodeRegional = " WHERE KD_REGION ='" + paramArea.kd_regional + "'";
                        }
                        else
                        {
                            paramKodeRegional = " WHERE KD_REGIONAL ='" + paramArea.kd_regional + "'";
                        }

                    }

                    string paramId = "";
                    if (!string.IsNullOrEmpty(paramArea.id) && paramArea.id != "string")
                    {
                        paramId = " AND ID ='" + paramArea.id + "'";

                    }

                    string paramKdCabang = "";
                    if (!string.IsNullOrEmpty(paramArea.kd_cabang) && paramArea.kd_cabang != "string")
                    {
                        paramKdCabang = " AND KD_CABANG ='" + paramArea.kd_cabang + "'";

                    }


                    string paramKdTerminal = "";
                    if (!string.IsNullOrEmpty(paramArea.kd_terminal) && paramArea.kd_terminal != "string")
                    {
                        paramKdTerminal = " AND KD_TERMINAL ='" + paramArea.kd_terminal + "'";

                    }

                    string paramIdPandu = "";
                    if (!string.IsNullOrEmpty(paramArea.id_pandu) && paramArea.id_pandu != "string")
                    {
                        paramIdPandu = " AND ID_PANDU ='" + paramArea.id_pandu + "'";

                    }

                    string sql = "";
                    if (paramArea.kd_regional == "12000001" && paramArea.current_screen == "PilotInformation")
                    {
                        sql = @"SELECT * FROM MASTER_AREA " + paramKodeRegional + paramId + paramKdCabang + paramKdTerminal + paramIdPandu;
                    }
                    else if (paramArea.current_screen == "TerminalInformation" || paramArea.current_screen == "PassangerInformation" || paramArea.current_screen == "ContainerInformation" || paramArea.current_screen == "WarehouseInformation")
                    {
                        sql = @"SELECT * FROM (SELECT * FROM VW_MASTER_CABANG_VASA " + paramKodeRegional + paramId + paramKdCabang + paramKdTerminal + paramIdPandu + ")";

                        if(paramArea.current_screen == "ContainerInformation")
                        {
                                sql = @"SELECT * FROM (SELECT * FROM VW_MASTER_CABANG_VASA " + paramKodeRegional + paramId + paramKdCabang + paramKdTerminal + paramIdPandu + ") WHERE NAMA_TERMINAL NOT IN('KUMAI')";
                        }
                    }
                    else
                    {
                        sql = @"SELECT * FROM (SELECT * FROM NEW_MASTER_AREA " + paramKodeRegional + paramId + paramKdCabang + paramKdTerminal + paramIdPandu + ") WHERE AREA_NAME NOT IN('KUMAI', 'SAMPIT')";
                    }

                    result = connection.Query<AreaData>(sql, new
                    {
                        KD_REGIONAL = paramArea.kd_regional
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

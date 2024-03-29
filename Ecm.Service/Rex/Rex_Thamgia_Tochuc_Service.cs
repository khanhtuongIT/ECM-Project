using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Ecm.Service.Rex
{
    public class Rex_Thamgia_Tochuc_Service
    {
        #region private fields
        System.Data.OleDb.OleDbConnection _SqlConnection;
        #endregion

        #region Method
        public Rex_Thamgia_Tochuc_Service(System.Data.OleDb.OleDbConnection sqlMaper)
        {
            this._SqlConnection = sqlMaper;
        }
        #endregion

        #region implemetns IObService
        
        /// <summary>
        /// Trả về một dataset Thamgia_Tochuc Theo Id_Nhansu
        /// </summary>
        /// <returns></returns>
        public string Get_All_Rex_Thamgia_Tochuc_byNhanSu_Collection(object Id_Nhansu)
        {
            
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Thamgia_Tochuc_SelectByNhansu", this._SqlConnection);
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu", Id_Nhansu));
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }


        /// <summary>
        /// Delete 1 doi tuong Thamgia_Tochuc trong DB
        /// </summary>
        /// <param name="rex_Thamgia_Tochuc"></param>
        /// <returns></returns>
        public object Delete_Rex_Thamgia_Tochuc(Ecm.Domain.Rex.Rex_Thamgia_Tochuc rex_Thamgia_Tochuc)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Thamgia_Tochuc_Delete",_SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Thamgia_Tochuc", rex_Thamgia_Tochuc.Id_Thamgia_Tochuc));

                oleDbCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        /// <summary>
        /// Update 1 collection Thamgia_Tochuc vao DB
        /// </summary>
        /// <param name="dsCollection"></param>
        /// <returns></returns>
        public object Update_Rex_Thamgia_Tochuc_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Rex_Thamgia_Tochuc", _SqlConnection);
                System.Data.OleDb.OleDbCommandBuilder oleDbCommandBuilder = new System.Data.OleDb.OleDbCommandBuilder(oleDbDataAdapter);
                oleDbDataAdapter = oleDbCommandBuilder.DataAdapter;

                oleDbDataAdapter.Update(dsCollection, "GridTable");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        #endregion
    }
}

using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Ecm.Service.Rex
{
    public class Rex_Quanhe_Giadinh_Service
    {
        #region private fields
        System.Data.OleDb.OleDbConnection _SqlConnection;
        #endregion

        #region Method
        public Rex_Quanhe_Giadinh_Service(System.Data.OleDb.OleDbConnection sqlMaper)
        {
            this._SqlConnection = sqlMaper;
        }
        #endregion

        #region implemetns IObService
        
        /// <summary>
        /// Trả về một dataset Quanhe_Giadinh
        /// </summary>
        /// <returns></returns>
        public string Get_All_Rex_Quanhe_Giadinh_byNhanSu_Collection(Ecm.Domain.Rex.Rex_Nhansu rex_Nhansu)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Quanhe_Giadinh_SelectByNhansu", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu", rex_Nhansu.Id_Nhansu));
            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Delete 1 doi tuong Quanhe_Giadinh trong DB
        /// </summary>
        /// <param name="rex_Quanhe_Giadinh"></param>
        /// <returns></returns>
        public object Delete_Rex_Quanhe_Giadinh(Ecm.Domain.Rex.Rex_Quanhe_Giadinh rex_Quanhe_Giadinh)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Quanhe_Giadinh_Delete",_SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Quanhe_Giadinh", rex_Quanhe_Giadinh.Id_Quanhe_Giadinh));

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
        /// Update 1 collection Quanhe_Giadinh vao DB
        /// </summary>
        /// <param name="dsCollection"></param>
        /// <returns></returns>
        public object Update_Rex_Quanhe_Giadinh_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Rex_Quanhe_Giadinh", _SqlConnection);
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

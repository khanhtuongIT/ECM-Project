using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Ecm.Service.Rex
{
    public class Rex_Dienbien_Luong_Service
    {
        #region private fields
        System.Data.OleDb.OleDbConnection sqlConnection;
        #endregion

        #region Method
        public Rex_Dienbien_Luong_Service(System.Data.OleDb.OleDbConnection _sqlConnection)
        {
            this.sqlConnection = _sqlConnection;
        }
        #endregion

        #region implemetns IObService
        
        /// <summary>
        /// Trả về một dataset Dienbien_Luong
        /// </summary>
        /// <returns></returns>
        public string Get_All_Rex_Dienbien_Luong_byNhanSu_Collection(Ecm.Domain.Rex.Rex_Nhansu rex_Nhansu)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dienbien_Luong_SelectByNhansu", this.sqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu", rex_Nhansu.Id_Nhansu));
            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Trả về một dataset Dienbien_Luong for Hopdong_laodong
        /// </summary>
        /// <returns></returns>
        public string Get_Rex_Dienbien_Luong_For_Hopdong_Laodong_Collection(Hashtable htpara)
        {

            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Get_Rex_Dienbien_Luong_For_Hopdong_Laodong", this.sqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 

        }

        /// <summary>
        /// Update 1 doi tuong Dienbien_Luong vao DB
        /// </summary>
        /// <param name="rex_Dienbien_Luong"></param>
        /// <returns></returns>
        public object Update_Rex_Dienbien_Luong_Collection(Ecm.Domain.Rex.Rex_Dienbien_Luong rex_Dienbien_Luong)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dienbien_Luong_Update", sqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Dienbien_Luong", rex_Dienbien_Luong.Id_Dienbien_Luong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu", rex_Dienbien_Luong.Id_Nhansu));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Quyetdinh", rex_Dienbien_Luong.Id_Quyetdinh));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Bacluong", rex_Dienbien_Luong.Id_Bacluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Bhxh", rex_Dienbien_Luong.Bhxh));

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
        /// Delete 1 doi tuong Dienbien_Luong trong DB
        /// </summary>
        /// <param name="rex_Dienbien_Luong"></param>
        /// <returns></returns>
        public object Delete_Rex_Dienbien_Luong(Ecm.Domain.Rex.Rex_Dienbien_Luong rex_Dienbien_Luong)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dienbien_Luong_Delete",sqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Dienbien_Luong", rex_Dienbien_Luong.Id_Dienbien_Luong));

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
        /// Update 1 collection Dienbien_Luong vao DB
        /// </summary>
        /// <param name="dsCollection"></param>
        /// <returns></returns>
        public object Update_Rex_Dienbien_Luong_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Rex_Dienbien_Luong", sqlConnection);
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

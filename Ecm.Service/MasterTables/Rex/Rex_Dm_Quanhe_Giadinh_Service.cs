using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Ecm.Service.MasterTables.Rex
{
    public class Rex_Dm_Quanhe_Giadinh_Service
    {
        #region private fields
        System.Data.OleDb.OleDbConnection _SqlConnection;
        #endregion

        #region Method
        public Rex_Dm_Quanhe_Giadinh_Service(System.Data.OleDb.OleDbConnection sqlMaper)
        {
            this._SqlConnection = sqlMaper;
        }
        #endregion

        #region implemetns IObService

        /// <summary>
        /// Trả về một dataset Dm_Quanhe_Giadinh
        /// </summary>
        /// <returns></returns>
        public string Get_All_Rex_Dm_Quanhe_Giadinh_Collection()
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Quanhe_Giadinh_SelectAll", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Insert 1 đoi tuong Rex_Dm_Quanhe_Giadinh vao DB
        /// </summary>
        /// <param name="rex_Dm_Quanhe_Giadinh"></param>
        /// <returns></returns>
        public object Insert_Rex_Dm_Quanhe_Giadinh(Ecm.Domain.MasterTables.Rex.Rex_Dm_Quanhe_Giadinh rex_Dm_Quanhe_Giadinh)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Quanhe_Giadinh_Insert", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Quanhe_Giadinh", rex_Dm_Quanhe_Giadinh.Ma_Quanhe_Giadinh));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ten_Quanhe_Giadinh", rex_Dm_Quanhe_Giadinh.Ten_Quanhe_Giadinh));

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
        /// Update 1 doi tuong Dm_Quanhe_Giadinh vao DB
        /// </summary>
        /// <param name="rex_Dm_Quanhe_Giadinh"></param>
        /// <returns></returns>
        public object Update_Rex_Dm_Quanhe_Giadinh(Ecm.Domain.MasterTables.Rex.Rex_Dm_Quanhe_Giadinh rex_Dm_Quanhe_Giadinh)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Quanhe_Giadinh_Update", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Quanhe_Giadinh", rex_Dm_Quanhe_Giadinh.Id_Quanhe_Giadinh));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Quanhe_Giadinh", rex_Dm_Quanhe_Giadinh.Ma_Quanhe_Giadinh));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ten_Quanhe_Giadinh", rex_Dm_Quanhe_Giadinh.Ten_Quanhe_Giadinh));

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
        /// Delete 1 doi tuong Dm_Quanhe_Giadinh trong DB
        /// </summary>
        /// <param name="rex_Dm_Quanhe_Giadinh"></param>
        /// <returns></returns>
        public object Delete_Rex_Dm_Quanhe_Giadinh(Ecm.Domain.MasterTables.Rex.Rex_Dm_Quanhe_Giadinh rex_Dm_Quanhe_Giadinh)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Quanhe_Giadinh_Delete", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Quanhe_Giadinh", rex_Dm_Quanhe_Giadinh.Id_Quanhe_Giadinh));

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
        /// Update 1 collection Dm_Quanhe_Giadinh vao DB
        /// </summary>
        /// <param name="dsCollection"></param>
        /// <returns></returns>
        public object Update_Rex_Dm_Quanhe_Giadinh_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Rex_Dm_Quanhe_Giadinh", _SqlConnection);
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

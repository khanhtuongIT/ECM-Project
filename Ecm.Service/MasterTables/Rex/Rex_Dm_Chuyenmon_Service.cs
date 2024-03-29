using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Ecm.Service.MasterTables.Rex
{
    public class Rex_Dm_Chuyenmon_Service
    {
        #region private fields
        System.Data.OleDb.OleDbConnection _SqlConnection;
        #endregion

        #region Method
        public Rex_Dm_Chuyenmon_Service(System.Data.OleDb.OleDbConnection sqlMaper)
        {
            this._SqlConnection = sqlMaper;
        }
        #endregion

        #region implemetns IObService
        
        /// <summary>
        /// Trả về một dataset Dm_Chuyenmon
        /// </summary>
        /// <returns></returns>
        public string Get_All_Rex_Dm_Chuyenmon_Collection()
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Chuyenmon_SelectAll", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }
        
        /// <summary>
        /// Insert 1 đoi tuong Rex_Dm_Chuyenmon vao DB
        /// </summary>
        /// <param name="rex_Dm_Chuyenmon"></param>
        /// <returns></returns>
        public object Insert_Rex_Dm_Chuyenmon(Ecm.Domain.MasterTables.Rex.Rex_Dm_Chuyenmon rex_Dm_Chuyenmon)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Chuyenmon_Insert", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Chuyenmon", rex_Dm_Chuyenmon.Ma_Chuyenmon));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ten_Chuyenmon", rex_Dm_Chuyenmon.Ten_Chuyenmon));

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
        /// Update 1 doi tuong Dm_Chuyenmonvao DB
        /// </summary>
        /// <param name="rex_Dm_Chuyenmon"></param>
        /// <returns></returns>
        public object Update_Rex_Dm_Chuyenmon(Ecm.Domain.MasterTables.Rex.Rex_Dm_Chuyenmon rex_Dm_Chuyenmon)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Chuyenmon_Update", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Chuyenmon", rex_Dm_Chuyenmon.Id_Chuyenmon));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Chuyenmon", rex_Dm_Chuyenmon.Ma_Chuyenmon));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ten_Chuyenmon", rex_Dm_Chuyenmon.Ten_Chuyenmon));

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
        /// Delete 1 doi tuong Dm_Chuyenmon trong DB
        /// </summary>
        /// <param name="rex_Dm_Chuyenmon"></param>
        /// <returns></returns>
        public object Delete_Rex_Dm_Chuyenmon(Ecm.Domain.MasterTables.Rex.Rex_Dm_Chuyenmon rex_Dm_Chuyenmon)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Chuyenmon_Delete", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Chuyenmon", rex_Dm_Chuyenmon.Id_Chuyenmon));

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
        /// Update 1 collection Dm_Chuyenmon vao DB
        /// </summary>
        /// <param name="dsCollection"></param>
        /// <returns></returns>
        public object Update_Rex_Dm_Chuyenmon_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Rex_Dm_Chuyenmon", _SqlConnection);
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

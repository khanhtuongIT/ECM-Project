using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Ecm.Service.MasterTables.Rex
{
    public class Rex_Dm_Kynang_Chuyenmon_Service
    {
        #region private fields
        System.Data.OleDb.OleDbConnection _SqlConnection;
        #endregion

        #region Method
        public Rex_Dm_Kynang_Chuyenmon_Service(System.Data.OleDb.OleDbConnection sqlMaper)
        {
            this._SqlConnection = sqlMaper;
        }
        #endregion

        #region implemetns IObService

        public string Get_All_Rex_Dm_Kynang_Chuyenmon_Collection1()
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Kynang_Chuyenmon_SelectAll1", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Trả về một DataSet Rex_Dm_Kynang_Chuyenmon
        /// </summary>
        /// <returns></returns>
        public string Get_All_Rex_Dm_Kynang_Chuyenmon_Collection()
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Kynang_Chuyenmon_SelectAll", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Insert 1 đoi tuong 
        /// </summary>
        /// <param name="rex_Dm_Kynang_Chuyenmon"></param>
        /// <returns></returns>
        public object Insert_Rex_Dm_Kynang_Chuyenmon(Ecm.Domain.MasterTables.Rex.Rex_Dm_Kynang_Chuyenmon rex_Dm_Kynang_Chuyenmon)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Kynang_Chuyenmon_Insert", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Kynang_Chuyenmon", rex_Dm_Kynang_Chuyenmon.Ma_Kynang_Chuyenmon));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ten_Kynang_Chuyenmon", rex_Dm_Kynang_Chuyenmon.Ten_Kynang_Chuyenmon));

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
      /// Update 1 doi tuong
      /// </summary>
      /// <param name="rex_Dm_Kynang_Chuyenmon"></param>
      /// <returns></returns>
        public object Update_Rex_Dm_Kynang_Chuyenmon(Domain.MasterTables.Rex.Rex_Dm_Kynang_Chuyenmon rex_Dm_Kynang_Chuyenmon)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Kynang_Chuyenmon_Update", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Kynang_Chuyenmon", rex_Dm_Kynang_Chuyenmon.Id_Kynang_Chuyenmon));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Kynang_Chuyenmon", rex_Dm_Kynang_Chuyenmon.Ma_Kynang_Chuyenmon));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ten_Kynang_Chuyenmon", rex_Dm_Kynang_Chuyenmon.Ten_Kynang_Chuyenmon));

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
        /// Delete 1 doi tuong  
        /// </summary>
        /// <param name="rex_Dm_Kynang_Chuyenmon"></param>
        /// <returns></returns>
        public object Delete_Rex_Dm_Kynang_Chuyenmon(Ecm.Domain.MasterTables.Rex.Rex_Dm_Kynang_Chuyenmon rex_Dm_Kynang_Chuyenmon)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Kynang_Chuyenmon_Delete", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Kynang_Chuyenmon", rex_Dm_Kynang_Chuyenmon.Id_Kynang_Chuyenmon));

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
       /// Update 1 collection 
       /// </summary>
       /// <param name="dsCollection"></param>
       /// <returns></returns>
        public object Update_Rex_Dm_Kynang_Chuyenmon_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Rex_Dm_Kynang_Chuyenmon", _SqlConnection);
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

using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Ecm.Service.MasterTables.Rex
{
    public class Rex_Dm_Ndung_Tgluong_Service
    {
        #region private fields
        System.Data.OleDb.OleDbConnection _SqlConnection;
        #endregion

        public Rex_Dm_Ndung_Tgluong_Service(System.Data.OleDb.OleDbConnection sqlMaper)
        {
            this._SqlConnection = sqlMaper;
        }

        #region Method
        public Domain.MasterTables.Rex.Rex_Dm_Ndung_Tgluong Get_Rex_Dm_Ndung_Tgluong(DataRow row)
        {
            Domain.MasterTables.Rex.Rex_Dm_Ndung_Tgluong Rex_Dm_Ndung_Tgluong = new Domain.MasterTables.Rex.Rex_Dm_Ndung_Tgluong();

            if ("" + row["Id_Ndung_Tgluong"] != "")
                Rex_Dm_Ndung_Tgluong.Id_Ndung_Tgluong = row["Id_Ndung_Tgluong"];
            if ("" + row["Ma_Ndung_Tgluong"] != "")
                Rex_Dm_Ndung_Tgluong.Ma_Ndung_Tgluong = row["Ma_Ndung_Tgluong"];
            if ("" + row["Noidung"] != "")
                Rex_Dm_Ndung_Tgluong.Noidung = row["Noidung"];
            if ("" + row["Pb_Tangluong"] != "")
                Rex_Dm_Ndung_Tgluong.Pb_Tangluong = row["Pb_Tangluong"];
            if ("" + row["Pb_Thue_Tncn"] != "")
                Rex_Dm_Ndung_Tgluong.Pb_Thue_Tncn = row["Pb_Thue_Tncn"];

            return Rex_Dm_Ndung_Tgluong;
        }
        #endregion

        #region implemetns
        /// <summary>
        /// Trả về một dataset Dm_Ndung_Tgluong
        /// </summary>
        /// <returns></returns>
        public string Get_All_Rex_Dm_Ndung_Tgluong_Collection()
        {
           
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Ndung_Tgluong_SelectAll", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Insert 1 đoi tuong Rex_Dm_Ndung_Tgluong vao DB
        /// </summary>
        /// <param name="Rex_Dm_Ndung_Tgluong"></param>
        /// <returns></returns>
        public object Insert_Rex_Dm_Ndung_Tgluong(Ecm.Domain.MasterTables.Rex.Rex_Dm_Ndung_Tgluong Rex_Dm_Ndung_Tgluong)
        {
           
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Ndung_Tgluong_Insert", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Noidung", Rex_Dm_Ndung_Tgluong.Noidung));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Pb_Tangluong", Rex_Dm_Ndung_Tgluong.Pb_Tangluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Ndung_Tgluong", Rex_Dm_Ndung_Tgluong.Ma_Ndung_Tgluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Pb_Thue_Tncn", Rex_Dm_Ndung_Tgluong.Pb_Thue_Tncn));
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
        /// Update 1 doi tuong Dm_Ndung_Tgluong vao DB
        /// </summary>
        /// <param name="Rex_Dm_Ndung_Tgluong"></param>
        /// <returns></returns>
        public object Update_Rex_Dm_Ndung_Tgluong(Ecm.Domain.MasterTables.Rex.Rex_Dm_Ndung_Tgluong Rex_Dm_Ndung_Tgluong)
        {
           
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Ndung_Tgluong_Update", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Ndung_Tgluong", Rex_Dm_Ndung_Tgluong.Id_Ndung_Tgluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Noidung", Rex_Dm_Ndung_Tgluong.Noidung));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Pb_Tangluong", Rex_Dm_Ndung_Tgluong.Pb_Tangluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Ndung_Tgluong", Rex_Dm_Ndung_Tgluong.Ma_Ndung_Tgluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Pb_Thue_Tncn", Rex_Dm_Ndung_Tgluong.Pb_Thue_Tncn));
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
        /// Delete 1 doi tuong Dm_Ndung_Tgluong trong DB
        /// </summary>
        /// <param name="Rex_Dm_Ndung_Tgluong"></param>
        /// <returns></returns>
        public object Delete_Rex_Dm_Ndung_Tgluong(Ecm.Domain.MasterTables.Rex.Rex_Dm_Ndung_Tgluong Rex_Dm_Ndung_Tgluong)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Dm_Ndung_Tgluong_Delete", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Ndung_Tgluong", Rex_Dm_Ndung_Tgluong.Id_Ndung_Tgluong));

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
        /// Update 1 collection Dm_Ndung_Tgluong vao DB
        /// </summary>
        /// <param name="dsCollection"></param>
        /// <returns></returns>
        public object Update_Rex_Dm_Ndung_Tgluong_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Rex_Dm_Ndung_Tgluong", _SqlConnection);
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
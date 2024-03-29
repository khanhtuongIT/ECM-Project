﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Ecm.Service.MasterTables.Ware
{
    public class Ware_Dm_Nganhang_Service
    {
        #region private fields
        System.Data.OleDb.OleDbConnection _SqlConnection;
        #endregion

        #region Method
        public Ware_Dm_Nganhang_Service(System.Data.OleDb.OleDbConnection sqlConnection)
        {
            this._SqlConnection = sqlConnection;
        }
        #endregion

        #region implemetns IObService
        /// <summary>
        /// Trả về một dataset Dm_Nganhang
        /// </summary>
        /// <returns></returns>
        public string Get_All_Ware_Dm_Nganhang()
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Nganhang_SelectAll", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Insert đối tượng Ware_Dm_Nganhang vào DB.
        /// </summary>
        /// <param name="ware_Dm_Nganhang"></param>
        /// <returns></returns>
        public object Insert_Ware_Dm_Nganhang(Ecm.Domain.MasterTables.Ware.Ware_Dm_Nganhang ware_Dm_Nganhang)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Nganhang_Insert", this._SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Nganhang", ware_Dm_Nganhang.Ma_Nganhang));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ten_Nganhang", ware_Dm_Nganhang.Ten_Nganhang));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Diachi", ware_Dm_Nganhang.Diachi));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Dienthoai", ware_Dm_Nganhang.Dienthoai));

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
        /// Update đối tượng Ware_Dm_Nganhang vào DB.
        /// </summary>
        /// <param name="ware_Dm_Nganhang"></param>
        /// <returns></returns>
        public object Update_Ware_Dm_Nganhang(Ecm.Domain.MasterTables.Ware.Ware_Dm_Nganhang ware_Dm_Nganhang)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Nganhang_Update", this._SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nganhang",    ware_Dm_Nganhang.Id_Nganhang));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Nganhang",    ware_Dm_Nganhang.Ma_Nganhang));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ten_Nganhang",   ware_Dm_Nganhang.Ten_Nganhang));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Diachi",         ware_Dm_Nganhang.Diachi));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Dienthoai",      ware_Dm_Nganhang.Dienthoai));

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
        /// Delete đối tượng Ware_Dm_Nganhang vào DB.
        /// </summary>
        /// <param name="ware_Dm_Nganhang"></param>
        /// <returns></returns>
        public object Delete_Ware_Dm_Nganhang(Ecm.Domain.MasterTables.Ware.Ware_Dm_Nganhang ware_Dm_Nganhang)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Nganhang_Delete", this._SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nganhang", ware_Dm_Nganhang.Id_Nganhang));

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
        /// Update một Collection Ware_Dm_Nganhang vào DB.
        /// </summary>
        /// <param name="dsCollection"></param>
        /// <returns></returns>
        public object Update_Ware_Dm_Nganhang_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Ware_Dm_Nganhang", _SqlConnection);
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

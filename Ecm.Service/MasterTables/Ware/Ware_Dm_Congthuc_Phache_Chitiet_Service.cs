﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Ecm.Service.MasterTables.Ware
{
    public class Ware_Dm_Congthuc_Phache_Chitiet_Service
    {
        #region private fields
        System.Data.OleDb.OleDbConnection _SqlConnection;
        #endregion

        #region Constructor
        public Ware_Dm_Congthuc_Phache_Chitiet_Service(System.Data.OleDb.OleDbConnection sqlConnection)
        {
            this._SqlConnection = sqlConnection;
        }
        #endregion

        #region implemetns IObService
        /// <summary>
        /// Trả về một dataset Ware_Dm_Congthuc_Phache_Chitiet
        /// </summary>
        /// <returns></returns>
        public string Get_All_Ware_Dm_Congthuc_Phache_Chitiet()
        {
            DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Congthuc_Phache_Chitiet_SelectAll", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");

                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Trả về một dataset Ware_Dm_Congthuc_Phache_Chitiet
        /// </summary>
        /// <returns></returns>
        public string Get_All_Ware_Dm_Congthuc_Phache_Chitiet_By_Id_CT_Phache(object Id_Congthuc_Phache)
        {
            DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Congthuc_Phache_Chitiet_SelectAll_By_Id_CT_Phache", this._SqlConnection);
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Congthuc_Phache", Id_Congthuc_Phache));
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");

                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        public string Get_All_Ware_Dm_Congthuc_Phache_Chitiet_ByHHBan(object Id_Hanghoa_Ban)
        {
            DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Congthuc_Phache_Chitiet_SelectByHHBan", this._SqlConnection);
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Hanghoa_Ban", Id_Hanghoa_Ban));
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");

                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Insert đối tượng Ware_Dm_Congthuc_Phache_Chitiet vào DB.
        /// </summary>
        /// <param name="ware_Dm_Donvitinh"></param>
        /// <returns></returns>
        public Object Insert_Ware_Dm_Congthuc_Phache_Chitiet(Ecm.Domain.MasterTables.Ware.Ware_Dm_Congthuc_Phache_Chitiet Ware_Dm_Congthuc_Phache_Chitiet)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Congthuc_Phache_Chitiet_Insert", this._SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Congthuc_Phache", Ware_Dm_Congthuc_Phache_Chitiet.Id_Congthuc_Phache));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Hanghoa_Mua",     Ware_Dm_Congthuc_Phache_Chitiet.Id_Hanghoa_Mua));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Soluong",            Ware_Dm_Congthuc_Phache_Chitiet.Soluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Donvitinh",       Ware_Dm_Congthuc_Phache_Chitiet.Id_Donvitinh));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ghichu",             Ware_Dm_Congthuc_Phache_Chitiet.Ghichu));

                oleDbCommand.ExecuteNonQuery();

                //tinh quy doi hh ban va hhmua
                System.Data.OleDb.OleDbCommand oleDbCommand1 = new System.Data.OleDb.OleDbCommand("ware_hhban_quydoi_hhmua_init", this._SqlConnection);
                oleDbCommand1.CommandType = CommandType.StoredProcedure;
                oleDbCommand1.ExecuteNonQuery();
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        /// <summary>
        /// Update đối tượng Ware_Dm_Congthuc_Phache_Chitiet vào DB.
        /// </summary>
        /// <param name="ware_Dm_Donvitinh"></param>
        /// <returns></returns>
        public Object Update_Ware_Dm_Congthuc_Phache_Chitiet(Ecm.Domain.MasterTables.Ware.Ware_Dm_Congthuc_Phache_Chitiet Ware_Dm_Congthuc_Phache_Chitiet)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Congthuc_Phache_Chitiet_Update", this._SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Congthuc_Phache_Chitiet", Ware_Dm_Congthuc_Phache_Chitiet.Id_Congthuc_Phache_Chitiet));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Congthuc_Phache",         Ware_Dm_Congthuc_Phache_Chitiet.Id_Congthuc_Phache));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Hanghoa_Mua",             Ware_Dm_Congthuc_Phache_Chitiet.Id_Hanghoa_Mua));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Soluong",                    Ware_Dm_Congthuc_Phache_Chitiet.Soluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Donvitinh",               Ware_Dm_Congthuc_Phache_Chitiet.Id_Donvitinh));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ghichu",                     Ware_Dm_Congthuc_Phache_Chitiet.Ghichu));

                oleDbCommand.ExecuteNonQuery();

                //tinh quy doi hh ban va hhmua
                System.Data.OleDb.OleDbCommand oleDbCommand1 = new System.Data.OleDb.OleDbCommand("ware_hhban_quydoi_hhmua_init", this._SqlConnection);
                oleDbCommand1.CommandType = CommandType.StoredProcedure;
                oleDbCommand1.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        /// <summary>
        /// Delete đối tượng Ware_Dm_Congthuc_Phache_Chitiet vào DB.
        /// </summary>
        /// <param name="ware_Dm_Donvitinh"></param>
        /// <returns></returns>
        public Object Delete_Ware_Dm_Congthuc_Phache_Chitiet(Ecm.Domain.MasterTables.Ware.Ware_Dm_Congthuc_Phache_Chitiet Ware_Dm_Congthuc_Phache_Chitiet)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Congthuc_Phache_Chitiet_Delete", this._SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Congthuc_Phache_Chitiet", Ware_Dm_Congthuc_Phache_Chitiet.Id_Congthuc_Phache_Chitiet));

                oleDbCommand.ExecuteNonQuery();

                //tinh quy doi hh ban va hhmua
                System.Data.OleDb.OleDbCommand oleDbCommand1 = new System.Data.OleDb.OleDbCommand("ware_hhban_quydoi_hhmua_init", this._SqlConnection);
                oleDbCommand1.CommandType = CommandType.StoredProcedure;
                oleDbCommand1.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        /// <summary>
        /// Update Collection Ware_Dm_Congthuc_Phache_Chitiet vào DB.
        /// </summary>
        /// <param name="ware_Dm_Donvitinh"></param>
        /// <returns></returns>
        public Object Update_Ware_Dm_Congthuc_Phache_Chitiet_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Ware_Dm_Congthuc_Phache_Chitiet", this._SqlConnection);
                System.Data.OleDb.OleDbCommandBuilder oleDbCommandBuilder = new System.Data.OleDb.OleDbCommandBuilder(oleDbDataAdapter);
                oleDbDataAdapter = oleDbCommandBuilder.DataAdapter;

                oleDbDataAdapter.Update(dsCollection, "GridTable");

                //tinh quy doi hh ban va hhmua
                //System.Data.OleDb.OleDbCommand oleDbCommand1 = new System.Data.OleDb.OleDbCommand("ware_hhban_quydoi_hhmua_init", this._SqlConnection);
                //oleDbCommand1.CommandType = CommandType.StoredProcedure;
                //oleDbCommand1.ExecuteNonQuery();

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

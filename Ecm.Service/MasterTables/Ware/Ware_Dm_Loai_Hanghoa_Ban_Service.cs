﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Ecm.Service.MasterTables.Ware
{
    public class Ware_Dm_Loai_Hanghoa_Ban_Service
    {
        #region private fields
        System.Data.OleDb.OleDbConnection _SqlConnection;
        #endregion

        #region Constructor
        public Ware_Dm_Loai_Hanghoa_Ban_Service(System.Data.OleDb.OleDbConnection sqlConnection)
        {
            this._SqlConnection = sqlConnection;
        }
        #endregion

        #region implemetns IObService
      
        public string Get_All_Ware_Dm_Loai_Hanghoa_Ban()
        {
            DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Loai_Hanghoa_Ban_SelectAll", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
            return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        public string Ware_Dm_Loai_Hanghoa_Ban_SelectBy_Id_Phanloai_HHban(object Id_Phanloai_Nhom_Hanghoa_Ban)
        {
            DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Loai_Hanghoa_Ban_SelectBy_Id_Phanloai_HHban", this._SqlConnection);
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Phanloai_Nhom_Hanghoa_Ban", Id_Phanloai_Nhom_Hanghoa_Ban));
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
            return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        public string Ware_Dm_Loai_Hanghoa_Ban_SelectBy_Id_Nhansu(object id_nhansu)
        {
            DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Loai_Hanghoa_Ban_SelectBy_Id_Nhansu", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu", id_nhansu));
            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");

            return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }


        /// <summary>
        /// Trả về một dataset Ware_Dm_Loai_Hanghoa_Ban_By_Id_Cuahang_Ban
        /// </summary>
        /// <returns></returns>
        public string Get_All_Ware_Dm_Loai_Hanghoa_Ban_SelectBy_Id_Cuahang_Ban(object id_cuahang_ban)
        {
            DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Loai_Hanghoa_Ban_SelectBy_Id_Cuahang_Ban", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@id_cuahang_ban", id_cuahang_ban));
            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");

            return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Insert đối tượng Ware_Dm_Loai_Hanghoa_Ban vào DB.
        /// </summary>
        /// <param name="ware_Dm_Donvitinh"></param>
        /// <returns></returns>
        public Object Insert_Ware_Dm_Loai_Hanghoa_Ban(Ecm.Domain.MasterTables.Ware.Ware_Dm_Loai_Hanghoa_Ban Ware_Dm_Loai_Hanghoa_Ban)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Loai_Hanghoa_Ban_Insert", this._SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Loai_Hanghoa_Ban", Ware_Dm_Loai_Hanghoa_Ban.Ma_Loai_Hanghoa_Ban));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ten_Loai_Hanghoa_Ban", Ware_Dm_Loai_Hanghoa_Ban.Ten_Loai_Hanghoa_Ban));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhom_Hanghoa_Ban", Ware_Dm_Loai_Hanghoa_Ban.Id_Nhom_Hanghoa_Ban));


                oleDbCommand.ExecuteNonQuery();

                //notify last change in data
                using (Sys.Sys_Service _Sys_Service = new Ecm.Service.Sys.Sys_Service(this._SqlConnection))
                {
                    _Sys_Service.Update_Sys_Lognotify("Ware_Dm_Loai_Hanghoa_Ban", DateTime.Now);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        /// <summary>
        /// Update đối tượng Ware_Dm_Loai_Hanghoa_Ban vào DB.
        /// </summary>
        /// <param name="ware_Dm_Donvitinh"></param>
        /// <returns></returns>
        public Object Update_Ware_Dm_Loai_Hanghoa_Ban(Ecm.Domain.MasterTables.Ware.Ware_Dm_Loai_Hanghoa_Ban Ware_Dm_Loai_Hanghoa_Ban)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Loai_Hanghoa_Ban_Update", this._SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Loai_Hanghoa_Ban", Ware_Dm_Loai_Hanghoa_Ban.Id_Loai_Hanghoa_Ban));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Loai_Hanghoa_Ban", Ware_Dm_Loai_Hanghoa_Ban.Ma_Loai_Hanghoa_Ban));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ten_Loai_Hanghoa_Ban", Ware_Dm_Loai_Hanghoa_Ban.Ten_Loai_Hanghoa_Ban));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhom_Hanghoa_Ban", Ware_Dm_Loai_Hanghoa_Ban.Id_Nhom_Hanghoa_Ban));


                oleDbCommand.ExecuteNonQuery();

                //notify last change in data
                using (Sys.Sys_Service _Sys_Service = new Ecm.Service.Sys.Sys_Service(this._SqlConnection))
                {
                    _Sys_Service.Update_Sys_Lognotify("Ware_Dm_Loai_Hanghoa_Ban", DateTime.Now);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        /// <summary>
        /// Delete đối tượng Ware_Dm_Loai_Hanghoa_Ban vào DB.
        /// </summary>
        /// <param name="ware_Dm_Donvitinh"></param>
        /// <returns></returns>
        public Object Delete_Ware_Dm_Loai_Hanghoa_Ban(Ecm.Domain.MasterTables.Ware.Ware_Dm_Loai_Hanghoa_Ban Ware_Dm_Loai_Hanghoa_Ban)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Loai_Hanghoa_Ban_Delete", this._SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Loai_Hanghoa_Ban", Ware_Dm_Loai_Hanghoa_Ban.Id_Loai_Hanghoa_Ban));

                oleDbCommand.ExecuteNonQuery();

                //notify last change in data
                using (Sys.Sys_Service _Sys_Service = new Ecm.Service.Sys.Sys_Service(this._SqlConnection))
                {
                    _Sys_Service.Update_Sys_Lognotify("Ware_Dm_Loai_Hanghoa_Ban", DateTime.Now);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        /// <summary>
        /// Update Collection Ware_Dm_Loai_Hanghoa_Ban vào DB.
        /// </summary>
        /// <param name="ware_Dm_Donvitinh"></param>
        /// <returns></returns>
        public Object Update_Ware_Dm_Loai_Hanghoa_Ban_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Ware_Dm_Loai_Hanghoa_Ban", this._SqlConnection);
                System.Data.OleDb.OleDbCommandBuilder oleDbCommandBuilder = new System.Data.OleDb.OleDbCommandBuilder(oleDbDataAdapter);
                oleDbDataAdapter = oleDbCommandBuilder.DataAdapter;

                oleDbDataAdapter.Update(dsCollection, "GridTable");

                //notify last change in data
                using (Sys.Sys_Service _Sys_Service = new Ecm.Service.Sys.Sys_Service(this._SqlConnection))
                {
                    _Sys_Service.Update_Sys_Lognotify("Ware_Dm_Loai_Hanghoa_Ban", DateTime.Now);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        /// <summary>
        /// Update table ware_Dm_Loai_hanghoa_By_Cuahang sau khi nhập hàng hóa, xuất, kiểm kê,... (update lại Id_Loai_hanghoa by Id_Cuahang)
        /// nhanvuong - 1/10/2010
        /// </summary>
        /// <returns></returns>
        public object Update_Ware_Dm_Loai_Hanghoa_By_Id_Cuahang(object Id_Cuahang_Ban)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Ware_Dm_Loai_Hanghoa_By_Cuahang_Update", this._SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Cuahang_Ban", Id_Cuahang_Ban));
                oleDbCommand.ExecuteNonQuery();
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

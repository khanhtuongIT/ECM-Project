using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Ecm.Service.Rex
{
    public class Rex_Botri_Nhansu_Service
    {
        #region private fields
        System.Data.OleDb.OleDbConnection _SqlConnection;
        #endregion

        #region Method
        public Rex_Botri_Nhansu_Service(System.Data.OleDb.OleDbConnection sqlMaper)
        {
            this._SqlConnection = sqlMaper;
        }
        #endregion

        #region implemetns IObService
        
        /// <summary>
        /// Trả về một dataset Botri_Nhansu
        /// </summary>
        /// <returns></returns>
        public string Get_All_Rex_Botri_Nhansu_byNhanSu_Collection(Ecm.Domain.Rex.Rex_Nhansu rex_Nhansu)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Botri_Nhansu_SelectByNhansu", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu", rex_Nhansu.Id_Nhansu));
            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Trả về một dataset Botri_Nhansu for Hopdong_laodong
        /// </summary>
        /// <returns></returns>
        public string Get_Rex_Botri_Nhansu_For_Hopdong_Laodong_Collection(Hashtable htpara)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Get_Rex_Botri_Nhansu_For_Hopdong_Laodong", this._SqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Trả về một dataset Botri_Nhansu
        /// </summary>
        /// <returns></returns>
        public string Get_All_Rex_Botri_Nhansu_byBophan_Collection(object Id_Bophan)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Botri_Nhansu_SelectByBophan", this._SqlConnection);
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Bophan", Id_Bophan));
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Insert 1 đoi tuong Rex_Botri_Nhansu vao DB
        /// </summary>
        /// <param name="rex_Botri_Nhansu"></param>
        /// <returns></returns>
        public object Insert_Rex_Botri_Nhansu(Ecm.Domain.Rex.Rex_Botri_Nhansu rex_Botri_Nhansu)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Botri_Nhansu_Insert",_SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Bophan", rex_Botri_Nhansu.Id_Bophan));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu", rex_Botri_Nhansu.Id_Nhansu));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Chucvu", rex_Botri_Nhansu.Id_Chucvu));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Quyetdinh", rex_Botri_Nhansu.Id_Quyetdinh));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Batdau", rex_Botri_Nhansu.Ngay_Batdau));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Ketthuc", rex_Botri_Nhansu.Ngay_Ketthuc));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ghichu", rex_Botri_Nhansu.Ghichu));

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
        /// Update 1 doi tuong Botri_Nhansu vao DB
        /// </summary>
        /// <param name="rex_Botri_Nhansu"></param>
        /// <returns></returns>
        public object Update_Rex_Botri_Nhansu(Ecm.Domain.Rex.Rex_Botri_Nhansu rex_Botri_Nhansu)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Botri_Nhansu_Update", _SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Botri_Nhansu", rex_Botri_Nhansu.Id_Botri_Nhansu));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Bophan", rex_Botri_Nhansu.Id_Bophan));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu", rex_Botri_Nhansu.Id_Nhansu));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Chucvu", rex_Botri_Nhansu.Id_Chucvu));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Quyetdinh", rex_Botri_Nhansu.Id_Quyetdinh));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Batdau", rex_Botri_Nhansu.Ngay_Batdau));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Ketthuc", rex_Botri_Nhansu.Ngay_Ketthuc));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ghichu", rex_Botri_Nhansu.Ghichu));

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
        /// Delete 1 doi tuong Botri_Nhansu trong DB
        /// </summary>
        /// <param name="rex_Botri_Nhansu"></param>
        /// <returns></returns>
        public object Delete_Rex_Botri_Nhansu(Ecm.Domain.Rex.Rex_Botri_Nhansu rex_Botri_Nhansu)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Botri_Nhansu_Delete",_SqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Botri_Nhansu", rex_Botri_Nhansu.Id_Botri_Nhansu));

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
        /// Update 1 collection Botri_Nhansu vao DB
        /// </summary>
        /// <param name="dsCollection"></param>
        /// <returns></returns>
        public object Update_Rex_Botri_Nhansu_Collection(DataSet dsCollection)
        {
            try
            {
                string query = "SELECT Id_Botri_Nhansu, Id_Bophan, Id_Nhansu, Id_Chucvu, Id_Quyetdinh, Ngay_Batdau, Ngay_Ketthuc, Ghichu FROM Rex_Botri_Nhansu";
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand(query, _SqlConnection);
                oleDbCommand.CommandType = CommandType.Text;

                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
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

        public object Update_Rex_Botri_Nhansu_Stt_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Rex_Botri_Nhansu_Stt", _SqlConnection);
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


        public string GetAll_Rex_Botri_Nhansu_Stt(object Id_Bophan, object Nam, object Thang)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Botri_Nhansu_SelectStt", this._SqlConnection);
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Bophan", Id_Bophan));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Nam", Nam));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Thang", Thang));
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        #endregion
    }
}

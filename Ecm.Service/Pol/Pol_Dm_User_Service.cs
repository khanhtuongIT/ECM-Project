using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

namespace Ecm.Service.Pol
{
    public class Pol_Dm_User_Service : MarshalByRefObject
    {
        #region private fields
        private System.Data.OleDb.OleDbConnection _SqlMapper;
        #endregion

        #region Properties
        public string DataSet_Pol_Dm_User
        {
            get { return DataSet_Pol_Dm_User; }
        }
        #endregion

        #region Method
        public Pol_Dm_User_Service(System.Data.OleDb.OleDbConnection sqlmapper)
        {
            this._SqlMapper = sqlmapper;
        }

        private Domain.Pol.Pol_Dm_User Get_Pol_Dm_User(DataRow row)
        {
            Domain.Pol.Pol_Dm_User Pol_Dm_User = new Ecm.Domain.Pol.Pol_Dm_User();
            if ("" + row["Id_User"] != "")
                Pol_Dm_User.Id_User = row["Id_User"];
            if ("" + row["Id_User_Parent"] != "")
                Pol_Dm_User.Id_User_Parent = row["Id_User_Parent"];
            if ("" + row["User_Name"] != "")
                Pol_Dm_User.User_Name = row["User_Name"];
            if ("" + row["User_Password"] != "")
                Pol_Dm_User.User_Password = row["User_Password"];
            if ("" + row["User_Fullname"] != "")
                Pol_Dm_User.User_Fullname = row["User_Fullname"];
            if ("" + row["User_Description"] != "")
                Pol_Dm_User.User_Description = row["User_Description"];
            if ("" + row["Build_In"] != "")
                Pol_Dm_User.Build_In = row["Build_In"];
            return Pol_Dm_User;
        }
        #endregion

        #region implemetns IObService
        /// <summary>
        /// Chọn tất cả đối tượng dữ liệu Pol_Dm_User, trả về một mảng Pol_Dm_User_Collection
        /// </summary>
        /// <returns>Pol_Dm_User_Collection</returns>
        public string Get_Pol_Dm_User_Collection()
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Dm_User_SelectAll", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }

        /// <summary>
        /// Chọn một đối tượng dữ liệu Pol_Dm_User theo điều kiện Id_User, trả về một mảng Pol_Dm_User_Collection
        /// </summary>
        /// <returns>Pol_Dm_User_Collection</returns>
        public string Pol_Dm_User_Select_ByID(Ecm.Domain.Pol.Pol_Dm_User Pol_Dm_User)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Dm_User_Select_ByID", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Id_User", Pol_Dm_User.Id_User));

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 

        }

        /// <summary>
        /// Chọn một đối tượng dữ liệu Pol_Dm_User theo điều kiện User_Name, trả về một mảng Pol_Dm_User_Collection
        /// </summary>
        /// <returns>Pol_Dm_User_Collection</returns>
        public string Pol_Dm_User_Select_ByName(Ecm.Domain.Pol.Pol_Dm_User Pol_Dm_User)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Dm_User_Select_ByName", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("User_Name", Pol_Dm_User.User_Name));

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 

        }

        /// <summary>
        /// Chọn một đối tượng dữ liệu Pol_Dm_User theo điều kiện User_Name và User_Password, trả về một mảng Pol_Dm_User_Collection
        /// </summary>
        /// <returns>Pol_Dm_User_Collection</returns>
        public string Pol_Dm_User_Select_ByAuth(Ecm.Domain.Pol.Pol_Dm_User Pol_Dm_User)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Dm_User_Select_ByAuth", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@User_Name", Pol_Dm_User.User_Name));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@User_Password", Pol_Dm_User.User_Password));

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }
 
        /// <summary>
        /// Chọn một đối tượng dữ liệu Pol_Dm_User theo điều kiện User_Name và User_Password, trả về một mảng Pol_Dm_User_Collection
        /// </summary>
        /// <returns>Pol_Dm_User_Collection</returns>
        public string Pol_Dm_User_Select_ByMa_Nhansu(string Ma_Nhansu)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Dm_User_Select_ByMa_Nhansu", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Nhansu", Ma_Nhansu));

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None); 
        }
        /// <summary>
        /// một đối tượng dữ liệu Pol_Dm_User
        /// </summary>
        public object Pol_Dm_User_Insert(Ecm.Domain.Pol.Pol_Dm_User Pol_Dm_User)
        {
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Dm_User_Insert", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@User_Name", Pol_Dm_User.User_Name));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@User_Fullname", Pol_Dm_User.User_Fullname));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@User_Description", Pol_Dm_User.User_Description));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu", Pol_Dm_User.Id_Nhansu));
            
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_User", System.Data.OleDb.OleDbType.BigInt));
            oleDbCommand.Parameters["@Id_User"].Direction = ParameterDirection.Output;

            oleDbCommand.ExecuteNonQuery();
            return oleDbCommand.Parameters["@Id_User"].Value;// _SqlMapper.Insert("Pol_Dm_User_Insert", Pol_Dm_User);
        }

        /// <summary>
        /// Cập nhật một đối tượng dữ liệu Pol_Dm_User
        /// </summary>
        public object Pol_Dm_User_Update(Ecm.Domain.Pol.Pol_Dm_User Pol_Dm_User)
        {
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Dm_User_Update", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_User", Pol_Dm_User.Id_User));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@User_Name", Pol_Dm_User.User_Name));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@User_Fullname", Pol_Dm_User.User_Fullname));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@User_Description", Pol_Dm_User.User_Description));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu", Pol_Dm_User.Id_Nhansu));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@User_Disable", Pol_Dm_User.User_Disable));

            oleDbCommand.ExecuteNonQuery();
            return null;//_SqlMapper.Update("Pol_Dm_User_Update", Pol_Dm_User);
        }

        /// <summary>
        /// Cập nhật thuộc tính User_Password của một đối tượng dữ liệu Pol_Dm_User
        /// </summary>
        public object Pol_Dm_User_Password_Update(Ecm.Domain.Pol.Pol_Dm_User Pol_Dm_User)
        {
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Dm_User_Password_Update", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_User", Pol_Dm_User.Id_User));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@User_Password", Pol_Dm_User.User_Password));

            oleDbCommand.ExecuteNonQuery();
            return null;//_SqlMapper.Update("Pol_Dm_User_Password_Update", Pol_Dm_User);
        }

        public object Pol_Dm_User_AuthCode_Update(Ecm.Domain.Pol.Pol_Dm_User Pol_Dm_User)
        {
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Dm_User_AuthCode_Update", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_User", Pol_Dm_User.Id_User));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@AuthCode", Pol_Dm_User.AuthCode));

            oleDbCommand.ExecuteNonQuery();
            return null;//_SqlMapper.Update("Pol_Dm_User_Password_Update", Pol_Dm_User);
        }

        /// <summary>
        /// Xóa một đối tượng dữ liệu Pol_Dm_User
        /// </summary>
        public object Pol_Dm_User_Delete(Ecm.Domain.Pol.Pol_Dm_User Pol_Dm_User)
        {
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Dm_User_Delete", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_User", Pol_Dm_User.Id_User));

            oleDbCommand.ExecuteNonQuery();
            return null;//_SqlMapper.Delete("Pol_Dm_User_Delete", Pol_Dm_User);
        }

        /// <summary>
        /// Cập nhật các đối tượng dữ liệu Pol_Dm_User trong DataSet đồng thời cập nhật trong CSDL
        /// </summary>
        public bool Update_Pol_Dm_User_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from pol_dm_user", _SqlMapper);
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

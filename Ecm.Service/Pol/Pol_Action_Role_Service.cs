using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

namespace Ecm.Service.Pol
{
    public class Pol_Action_Role_Service : MarshalByRefObject
    {
        #region private fields
        private System.Data.OleDb.OleDbConnection _SqlMapper;
        #endregion

        #region Properties
        public string DataSet_Pol_Action_Role
        {
            get { return DataSet_Pol_Action_Role; }
        }
        #endregion

        #region Method
        public Pol_Action_Role_Service(System.Data.OleDb.OleDbConnection sqlmapper)
        {
            this._SqlMapper = sqlmapper;
        }

        private Domain.Pol.Pol_Action_Role Get_Pol_Action_Role(DataRow row)
        {
            Domain.Pol.Pol_Action_Role Pol_Action_Role = new Ecm.Domain.Pol.Pol_Action_Role();
            if ("" + row["Id_Role"] != "")
                Pol_Action_Role.Id_Action = row["Id_Action"];
            Pol_Action_Role.Id_Right = row["Id_Right"];
            Pol_Action_Role.Id_Role = row["Id_Role"];
            return Pol_Action_Role;
        }
        #endregion

        #region implemetns IObService
        

        /// <summary>
        /// Chọn tất cả các đối tượng dữ liệu Pol_Action_Role theo điều kiện Id_Role và Id_Right, trả về trong một mảng Pol_Dm_Action_Collection
        /// </summary>
        /// <returns>Pol_Dm_Action_Collection</returns>
        public string Pol_Action_Role_Select_ByID_RoleRight(Ecm.Domain.Pol.Pol_Action_Role Pol_Action_Role)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Action_Role_Select_ByID_RoleRight", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Id_Role", Pol_Action_Role.Id_Role));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Id_Right", Pol_Action_Role.Id_Right));

            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(dsCollection, "GridTable");
                        return FastJSON.JSON.Instance.ToJSON(dsCollection);//return Newtonsoft.Json.JsonConvert.SerializeObject(dsCollection.Tables[0], Newtonsoft.Json.Formatting.None);  
        }

        /// <summary>
        /// Thêm một đối tượng dữ liệu Pol_Action_Role
        /// </summary>
        public object Pol_Action_Role_Insert(Ecm.Domain.Pol.Pol_Action_Role Pol_Action_Role)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Action_Role_Insert", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Action", Pol_Action_Role.Id_Action));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Role", Pol_Action_Role.Id_Role));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Right", Pol_Action_Role.Id_Right));

            oleDbCommand.ExecuteNonQuery();
            return null;// _SqlMapper.Insert("Pol_Action_Role_Insert", Pol_Action_Role);
        }

       
        /// <summary>
        /// Xóa đối tượng dữ liệu Pol_Action_Role
        /// </summary>
        public object Pol_Action_Role_Delete(Ecm.Domain.Pol.Pol_Action_Role Pol_Action_Role)
        {
            System.Data.DataSet dsCollection = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Pol_Action_Role_Delete", _SqlMapper);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            //Parameters
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Action", Pol_Action_Role.Id_Action));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Role", Pol_Action_Role.Id_Role));
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Right", Pol_Action_Role.Id_Right));

            oleDbCommand.ExecuteNonQuery();
            return null;//_SqlMapper.Delete("Pol_Action_Role_Delete", Pol_Action_Role);
        }

        /// <summary>
        /// Cập nhật các đối tượng dữ liệu Pol_Action_Role trong DataSet đồng thời cập nhật trong CSDL
        /// </summary>
        /// <returns></returns>
        public bool Update_Pol_Action_Role_Collection(DataSet dsCollection)
        {
            try
            {
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("select * from Pol_Action_Role", _SqlMapper);
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

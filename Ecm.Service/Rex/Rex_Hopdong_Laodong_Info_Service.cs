using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Ecm.Service.Rex
{
    public class Rex_Hopdong_Laodong_Info_Service
    {
        #region Connection
        System.Data.OleDb.OleDbConnection sqlConnection;
        #endregion

        public Rex_Hopdong_Laodong_Info_Service(System.Data.OleDb.OleDbConnection _sqlConnection)
        {
            this.sqlConnection = _sqlConnection;
        }


        #region Get_Hopdong_Chitiet_Theo_IDHopdong
        /// <summary>
        /// Get danh sách nhân sự được bố trí theo ca làm việc
        /// </summary>
        /// <param name="id_hopdong"></param>
        /// <returns>Hop Dong Details</returns>
        public Ecm.Domain.Rex.Rex_Hopdong_Laodong Get_Rex_Hopdong_Laodong_Info_ByID_Hopdong(object id_hopdong_laodong)
        {
            try
            {
                DataSet dsCollection = new DataSet();
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Hopdong_Laodong_Info_Select_ByID_Hopdong", sqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Hopdong_Laodong", id_hopdong_laodong));
                System.Data.OleDb.OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
                Ecm.Domain.Rex.Rex_Hopdong_Laodong hdld = new Ecm.Domain.Rex.Rex_Hopdong_Laodong();
                if (oleDbDataReader.Read())
                {
                    hdld.Id_Hopdong_Laodong = id_hopdong_laodong;
                    hdld.Id_Nhansu_Nld = "" + oleDbDataReader["Id_Nhansu_Nld"];
                    hdld.Id_Loai_Hopdong = "" + oleDbDataReader["Id_Loai_Hopdong"];
                    hdld.Ma_Hopdong_Laodong = "" + oleDbDataReader["Ma_Hopdong_Laodong"];
                    hdld.Id_Nhansu_Nsd = "" + oleDbDataReader["Id_Nhansu_Nsd"];

                    hdld.So_Laodong = "" + oleDbDataReader["So_Laodong"];
                    hdld.Ngaycap_Sld = "" + oleDbDataReader["Ngaycap_Sld"];
                    hdld.Noicap_Sld = "" + oleDbDataReader["Noicap_Sld"];

                    hdld.Ngay_Batdau = "" + oleDbDataReader["Ngay_Batdau"];
                    hdld.Ngay_Ketthuc = "" + oleDbDataReader["Ngay_Ketthuc"];
                    //hdld.Ngay_Batdau_Thuviec = "" + oleDbDataReader["Ngay_Batdau_Thuviec"];
                    //hdld.Ngay_Ketthuc_Thuviec = "" + oleDbDataReader["Ngay_Ketthuc_Thuviec"];

                    hdld.Diachi_Lamviec = "" + oleDbDataReader["Diachi_Lamviec"];
                    hdld.Id_Chucvu_Nld = "" + oleDbDataReader["Id_Chucvu_Nld"];
                    hdld.Congviec_Phailam = "" + oleDbDataReader["Congviec_Phailam"];
                    hdld.Thoigio_Lamviec = "" + oleDbDataReader["Thoigio_Lamviec"];
                    hdld.Dungcu_Lamviec = "" + oleDbDataReader["Dungcu_Lamviec"];
                    hdld.Phuongtien_Dilai = "" + oleDbDataReader["Phuongtien_Dilai"];
                    hdld.Id_Dienbien_Luong = "" + oleDbDataReader["Id_Dienbien_Luong"];
                    hdld.Id_Phuongthuc_Huongluong = "" + oleDbDataReader["Id_Phuongthuc_Huongluong"];
                    hdld.Phucap_Gom = "" + oleDbDataReader["Phucap_Gom"];
                    hdld.Ngay_Tra_Luong = "" + oleDbDataReader["Ngay_Tra_Luong"];
                    hdld.Tienthuong = "" + oleDbDataReader["Tienthuong"];
                    hdld.Chedo_Nangluong = "" + oleDbDataReader["Chedo_Nangluong"];
                    hdld.Baoho_Laodong_Gom = "" + oleDbDataReader["Baoho_Laodong_Gom"];
                    hdld.Chedo_Nghingoi = "" + oleDbDataReader["Chedo_Nghingoi"];
                    hdld.Baohiem = "" + oleDbDataReader["Baohiem"];
                    hdld.Chedo_Daotao = "" + oleDbDataReader["Chedo_Daotao"];
                    hdld.Thoathuan_Khac = "" + oleDbDataReader["Thoathuan_Khac"];

                    hdld.Boithuong_Vipham = "" + oleDbDataReader["Boithuong_Vipham"];
                    hdld.Ngay_Lap_Hopdong = "" + oleDbDataReader["Ngay_Lap_Hopdong"];
                    hdld.Noiky = "" + oleDbDataReader["Noiky"];
                    hdld.Ngayky = "" + oleDbDataReader["Ngayky"];
                }

                return hdld;
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
        #endregion


        #region Update_Hopdong_Chitiet_Theo_IDHopdong
        // <summary>
        /// Update Rex_Hopdong_Laodong_Info
        /// </summary>
        /// <param name="Rex_Hopdong_Laodong_Info"></param>
        /// <returns>True/False</returns>
        public object Update_Rex_Hopdong_Laodong_Info_ByID_Hopdong(Domain.Rex.Rex_Hopdong_Laodong Rex_Hopdong_Laodong)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Hopdong_Laodong_Info_Update_ByID_Hopdong", this.sqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Hopdong_Laodong", Rex_Hopdong_Laodong.Id_Hopdong_Laodong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Loai_Hopdong", Rex_Hopdong_Laodong.Id_Loai_Hopdong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Batdau", Rex_Hopdong_Laodong.Ngay_Batdau));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Ketthuc", Rex_Hopdong_Laodong.Ngay_Ketthuc));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu_Nsd", Rex_Hopdong_Laodong.Id_Nhansu_Nsd));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@So_Laodong", Rex_Hopdong_Laodong.So_Laodong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngaycap_Sld", Rex_Hopdong_Laodong.Ngaycap_Sld));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Noicap_Sld", Rex_Hopdong_Laodong.Noicap_Sld));
                //oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Batdau_Thuviec", Rex_Hopdong_Laodong.Ngay_Batdau_Thuviec));
                //oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Ketthuc_Thuviec", Rex_Hopdong_Laodong.Ngay_Ketthuc_Thuviec));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Diachi_Lamviec", Rex_Hopdong_Laodong.Diachi_Lamviec));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Chucvu_Nld", Rex_Hopdong_Laodong.Id_Chucvu_Nld));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Congviec_Phailam", Rex_Hopdong_Laodong.Congviec_Phailam));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Thoigio_Lamviec", Rex_Hopdong_Laodong.Thoigio_Lamviec));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Dungcu_Lamviec", Rex_Hopdong_Laodong.Dungcu_Lamviec));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Phuongtien_Dilai", Rex_Hopdong_Laodong.Phuongtien_Dilai));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Dienbien_Luong", Rex_Hopdong_Laodong.Id_Dienbien_Luong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Phuongthuc_Huongluong", Rex_Hopdong_Laodong.Id_Phuongthuc_Huongluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Phucap_Gom", Rex_Hopdong_Laodong.Phucap_Gom));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Tra_Luong", Rex_Hopdong_Laodong.Ngay_Tra_Luong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Tienthuong", Rex_Hopdong_Laodong.Tienthuong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Chedo_Nangluong", Rex_Hopdong_Laodong.Chedo_Nangluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Baoho_Laodong_Gom", Rex_Hopdong_Laodong.Baoho_Laodong_Gom));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Chedo_Nghingoi", Rex_Hopdong_Laodong.Chedo_Nghingoi));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Baohiem", Rex_Hopdong_Laodong.Baohiem));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Chedo_Daotao", Rex_Hopdong_Laodong.Chedo_Daotao));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Thoathuan_Khac", Rex_Hopdong_Laodong.Thoathuan_Khac));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Boithuong_Vipham", Rex_Hopdong_Laodong.Boithuong_Vipham));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Lap_Hopdong", Rex_Hopdong_Laodong.Ngay_Lap_Hopdong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Noiky", Rex_Hopdong_Laodong.Noiky));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngayky", Rex_Hopdong_Laodong.Ngayky));

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

        #region Insert_Hopdong_Chitiet_Theo
        // <summary>
        /// Insert Rex_Hopdong_Laodong_Info
        /// </summary>
        /// <param name="Rex_Hopdong_Laodong_Info"></param>
        /// <returns>True/False</returns>
        public object Insert_Rex_Hopdong_Laodong_Info(Domain.Rex.Rex_Hopdong_Laodong Rex_Hopdong_Laodong)
        {
            try
            {
                System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Hopdong_Laodong_Info_Insert", this.sqlConnection);
                oleDbCommand.CommandType = CommandType.StoredProcedure;

                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ma_Hopdong_Laodong", Rex_Hopdong_Laodong.Ma_Hopdong_Laodong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Loai_Hopdong", Rex_Hopdong_Laodong.Id_Loai_Hopdong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu_Nld", Rex_Hopdong_Laodong.Id_Nhansu_Nld));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Batdau", Rex_Hopdong_Laodong.Ngay_Batdau));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Ketthuc", Rex_Hopdong_Laodong.Ngay_Ketthuc));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Nhansu_Nsd", Rex_Hopdong_Laodong.Id_Nhansu_Nsd));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@So_Laodong", Rex_Hopdong_Laodong.So_Laodong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngaycap_Sld", Rex_Hopdong_Laodong.Ngaycap_Sld));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Noicap_Sld", Rex_Hopdong_Laodong.Noicap_Sld));
                //oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Batdau_Thuviec", Rex_Hopdong_Laodong.Ngay_Batdau_Thuviec));
                //oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Ketthuc_Thuviec", Rex_Hopdong_Laodong.Ngay_Ketthuc_Thuviec));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Diachi_Lamviec", Rex_Hopdong_Laodong.Diachi_Lamviec));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Chucvu_Nld", Rex_Hopdong_Laodong.Id_Chucvu_Nld));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Congviec_Phailam", Rex_Hopdong_Laodong.Congviec_Phailam));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Thoigio_Lamviec", Rex_Hopdong_Laodong.Thoigio_Lamviec));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Dungcu_Lamviec", Rex_Hopdong_Laodong.Dungcu_Lamviec));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Phuongtien_Dilai", Rex_Hopdong_Laodong.Phuongtien_Dilai));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Dienbien_Luong", Rex_Hopdong_Laodong.Id_Dienbien_Luong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Phuongthuc_Huongluong", Rex_Hopdong_Laodong.Id_Phuongthuc_Huongluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Phucap_Gom", Rex_Hopdong_Laodong.Phucap_Gom));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Tra_Luong", Rex_Hopdong_Laodong.Ngay_Tra_Luong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Tienthuong", Rex_Hopdong_Laodong.Tienthuong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Chedo_Nangluong", Rex_Hopdong_Laodong.Chedo_Nangluong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Baoho_Laodong_Gom", Rex_Hopdong_Laodong.Baoho_Laodong_Gom));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Chedo_Nghingoi", Rex_Hopdong_Laodong.Chedo_Nghingoi));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Baohiem", Rex_Hopdong_Laodong.Baohiem));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Chedo_Daotao", Rex_Hopdong_Laodong.Chedo_Daotao));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Thoathuan_Khac", Rex_Hopdong_Laodong.Thoathuan_Khac));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Boithuong_Vipham", Rex_Hopdong_Laodong.Boithuong_Vipham));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngay_Lap_Hopdong", Rex_Hopdong_Laodong.Ngay_Lap_Hopdong));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Noiky", Rex_Hopdong_Laodong.Noiky));
                oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Ngayky", Rex_Hopdong_Laodong.Ngayky));

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

        public string Get_Hopdong_Nhansu_Info_In_Hoso(object id_hopdong_laodong)
        {
            System.Data.DataSet ds_HDLD = new DataSet();
            System.Data.OleDb.OleDbCommand oleDbCommand = new System.Data.OleDb.OleDbCommand("Rex_Hopdong_Laodong_Select_In_Hoso_ByID_Hopdong", this.sqlConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            oleDbCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Id_Hopdong_Laodong", id_hopdong_laodong));
            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oleDbCommand);
            oleDbDataAdapter.Fill(ds_HDLD, "rex_hopdong_laodong");
            return FastJSON.JSON.Instance.ToJSON(ds_HDLD );
        }
    }
}

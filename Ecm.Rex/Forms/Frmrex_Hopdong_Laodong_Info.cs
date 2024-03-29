#region edit
/*
 * edit     phuuongphan
 * date     06/04/2011
 * content  edit GUI
 */
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;
using DevExpress.XtraTab;
using Ecm.Rex.Forms;
using System.IO;

namespace Ecm.Rex.Forms
{
    public partial class Frmrex_Hopdong_Laodong_Info : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        #region VARIABLES
        Ecm.WebReferences.Classes.SystemService objSystemService = new Ecm.WebReferences.Classes.SystemService();
        Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        Ecm.WebReferences.Classes.RexService objRexService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.RexService>();
        Ecm.WebReferences.RexService.Rex_Hopdong_Laodong objHopdong_Laodong = new Ecm.WebReferences.RexService.Rex_Hopdong_Laodong();
        Ecm.WebReferences.RexService.Rex_Nhansu objNhansu = new Ecm.WebReferences.RexService.Rex_Nhansu();

        DataSet dsBotri_Nhansu = new DataSet();
        DataSet dsDienbien_Luong = new DataSet();
        DataSet dsPhucap = new DataSet();
        public long[] id_nhansu_chon;

        private object Id_Hopdong;
        private object Id_Nhansu;
        public object Id_Nhansu_Sd
        {
            get { return txtTen_Nhansu_A.EditValue; }
            set { txtTen_Nhansu_A.EditValue = value; }
        }// Ma Nhan Su su dung lao dong
        public delegate void PassData(string value);
        public PassData passData;
        DataSet dsDm_Heso_Chuongtrinh;
        #endregion

        public Frmrex_Hopdong_Laodong_Info()
        {
            InitializeComponent();
        }

        public Frmrex_Hopdong_Laodong_Info(object id_hopdong)
        {
            InitializeComponent();
            dtNgaycap_Nhansu_B.Properties.MinValue = new DateTime(2000, 01, 01);
            dtNgaycap_Solaodong_Nhansu_B.Properties.MinValue = new DateTime(2000, 01, 01);
            dtNgay_Batdau.Properties.MinValue = new DateTime(2000, 01, 01);
            dtNgay_Ketthuc.Properties.MinValue = new DateTime(2000, 01, 01);
            dtLap_Hopdong.Properties.MinValue = new DateTime(2000, 01, 01);
            dtNgayky.Properties.MinValue = new DateTime(2000, 01, 01);

            //dtNgaycap_Solaodong.Properties.Mask.UseMaskAsDisplayFormat = true;
            //dtNgaycap_Solaodong.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();
            dtNgaycap_Nhansu_B.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtNgaycap_Nhansu_B.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();
            dtNgaycap_Solaodong_Nhansu_B.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtNgaycap_Solaodong_Nhansu_B.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();

            dtNgay_Batdau.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtNgay_Batdau.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();
            dtNgay_Ketthuc.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtNgay_Ketthuc.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();
            //dtNgay_Batdau_Thuviec.Properties.Mask.UseMaskAsDisplayFormat = true;
            //dtNgay_Batdau_Thuviec.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();
            //dtNgay_Ketthuc_Thuviec.Properties.Mask.UseMaskAsDisplayFormat = true;
            //dtNgay_Ketthuc_Thuviec.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();
            dtLap_Hopdong.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtLap_Hopdong.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();
            dtNgayky.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtNgayky.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();

            Id_Hopdong = id_hopdong;
            //xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.item_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ChangeStatus(true);
            DisplayInfo();
            this.item_Add.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        #region DISPLAY_INFO_DETAILED

        public override void DisplayInfo()
        {
            if (this.FormState == GoobizFrame.Windows.Forms.FormState.View)
            {
                objHopdong_Laodong = objRexService.Get_Rex_Hopdong_Laodong_Info_ByID_Hopdong(Id_Hopdong);
                txtSohopdong.Text = objHopdong_Laodong.Ma_Hopdong_Laodong.ToString();
                Id_Nhansu = objHopdong_Laodong.Id_Nhansu_Nld;
                Id_Nhansu_Sd = objHopdong_Laodong.Id_Nhansu_Nsd;

                dsDm_Heso_Chuongtrinh = objMasterService.Get_Rex_Dm_Heso_Chuongtrinh_Collection3().ToDataSet();
                txtCongty.EditValue = dsDm_Heso_Chuongtrinh.Tables[0].Select("Nhom_Heso_Chuongtrinh = 'Company' and Ma_Heso_Chuongtrinh = 'CompanyName'")[0]["Heso"];
                txtCongty_Daidien.EditValue = dsDm_Heso_Chuongtrinh.Tables[0].Select("Nhom_Heso_Chuongtrinh = 'Company' and Ma_Heso_Chuongtrinh = 'CompanyName'")[0]["Heso"];
                txtDienthoai_Congty.EditValue = dsDm_Heso_Chuongtrinh.Tables[0].Select("Nhom_Heso_Chuongtrinh = 'Company' and Ma_Heso_Chuongtrinh = 'CompanyTel'")[0]["Heso"];
                txtDiachi_Congty.EditValue = dsDm_Heso_Chuongtrinh.Tables[0].Select("Nhom_Heso_Chuongtrinh = 'Company' and Ma_Heso_Chuongtrinh = 'CompanyAddress'")[0]["Heso"];

                DataSet dsNhansu = objRexService.Get_All_Rex_Nhansu_Collection().ToDataSet();
                DataSet dsQuocgia = objMasterService.Get_All_Rex_Dm_Quocgia_Collection().ToDataSet();
                DataSet dsChucvu = objMasterService.Get_All_Rex_Dm_Chucvu_Collection().ToDataSet();
                txtTen_Nhansu_A.Properties.DataSource = dsNhansu.Tables[0];
                txtTen_Nhansu_B.Properties.DataSource = dsNhansu.Tables[0];
                lookUpEdit_Quoctich_Nhansu_A.Properties.DataSource = dsQuocgia.Tables[0];
                lookUp_Chucvu_Nhansu_A.Properties.DataSource = dsChucvu.Tables[0];
                lookUpEdit_Quoctich_Nhansu_B.Properties.DataSource = dsQuocgia.Tables[0];
                lookUp_Chuyenmon_Nhansu_B.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Chuyenmon_Collection().ToDataSet().Tables[0];
                lookUp_Loai_Hopdong.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Loai_Hopdong_Collection().ToDataSet().Tables[0];
                lookUp_Chucvu_Nhansu_B.Properties.DataSource = dsChucvu.Tables[0];
                lookUp_Bacluong.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Bacluong_Collection().ToDataSet().Tables[0];
                lookUp_Phuongthuc_Huongluong.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Phuongthuc_Huongluong_Collection().ToDataSet().Tables[0];

                DisplayInfo_Nhansu_Nsd();
                DisplayInfo_Nhansu_Nld();
            }

            if (is_Numeric(objHopdong_Laodong.Id_Loai_Hopdong))
            {
                lookUp_Loai_Hopdong.EditValue = Convert.ToInt64(objHopdong_Laodong.Id_Loai_Hopdong);
            }

            txtSolaodong_Nhansu_B.EditValue = objHopdong_Laodong.So_Laodong;
            dtNgaycap_Solaodong_Nhansu_B.EditValue = objHopdong_Laodong.Ngaycap_Sld;
            txtNoicap_Solaodong_Nhansu_B.EditValue = objHopdong_Laodong.Noicap_Sld;
            //dtNgay_Batdau_Thuviec.EditValue = objHopdong_Laodong.Ngay_Batdau_Thuviec;
            //dtNgay_Ketthuc_Thuviec.EditValue = objHopdong_Laodong.Ngay_Ketthuc_Thuviec;
            txtDiadiem_Lamviec.EditValue = objHopdong_Laodong.Diachi_Lamviec;
            lookUp_Chucvu_Nhansu_B.EditValue = objHopdong_Laodong.Id_Chucvu_Nld;
            txtCongviec.EditValue = objHopdong_Laodong.Congviec_Phailam;
            txtThoigian_Lamviec.EditValue = objHopdong_Laodong.Thoigio_Lamviec;
            txtDungcu_Lamviec.EditValue = objHopdong_Laodong.Dungcu_Lamviec;
            txtPhuongtien_Dichuyen.EditValue = objHopdong_Laodong.Phuongtien_Dilai;
            lookUp_Bacluong.EditValue = objHopdong_Laodong.Id_Dienbien_Luong;
            lookUp_Phuongthuc_Huongluong.EditValue = objHopdong_Laodong.Id_Phuongthuc_Huongluong;
            //txtPhucap.EditValue = objHopdong_Laodong.Phucap_Gom;
            txtTraluong.EditValue = objHopdong_Laodong.Ngay_Tra_Luong;
            txtTienthuong.EditValue = objHopdong_Laodong.Tienthuong;
            txtChedo_Nangluong.EditValue = objHopdong_Laodong.Chedo_Nangluong;
            txtBaoho_Laodong.EditValue = objHopdong_Laodong.Baoho_Laodong_Gom;
            txtChedo_Nghingoi.EditValue = objHopdong_Laodong.Chedo_Nghingoi;
            txtBaohiem.EditValue = objHopdong_Laodong.Baohiem;
            txtChedo_Daotao.EditValue = objHopdong_Laodong.Chedo_Daotao;
            txtThoathuan_Khac.EditValue = objHopdong_Laodong.Thoathuan_Khac;
            txtBoithuong_Vatchat.EditValue = objHopdong_Laodong.Boithuong_Vipham;
            dtLap_Hopdong.EditValue = objHopdong_Laodong.Ngay_Lap_Hopdong;
            txtNoiky.EditValue = objHopdong_Laodong.Noiky;
            dtNgayky.EditValue = objHopdong_Laodong.Ngayky;

            if ("" + objHopdong_Laodong.Kyten_Nld != "")
            {
                TypeConverter tc_xSignPane1 = TypeDescriptor.GetConverter(typeof(Bitmap));
                xSignPane1.Bitmap = (Bitmap)tc_xSignPane1.ConvertFrom(objHopdong_Laodong.Kyten_Nld);
            }

            if ("" + objHopdong_Laodong.Kyten_Nsd != "")
            {
                TypeConverter tc_xSignPane2 = TypeDescriptor.GetConverter(typeof(Bitmap));
                xSignPane2.Bitmap = (Bitmap)tc_xSignPane2.ConvertFrom(objHopdong_Laodong.Kyten_Nsd);
            }

            txtLuong_Thoathuan.EditValue = objHopdong_Laodong.Luong_Thoathuan;

            //gridlookup_edit_Phucap, gridlookup_edit_Heso
            DataSet dsDm_Phucap = objMasterService.Get_All_Rex_Dm_Phucap_Collection().ToDataSet();
            GridLookUpEdit_Phucap.DataSource = dsDm_Phucap.Tables[0];
            GridLookUpEdit_Heso.DataSource = dsDm_Phucap.Tables[0];

            dg_Phucap.DataSource = null;
            dsPhucap = objRexService.Get_Rex_Phucap_For_Hopdong_Laodong_Collection(Id_Nhansu, dtNgay_Batdau.EditValue).ToDataSet();

            dg_Phucap.DataSource = dsPhucap.Tables[0];
            //dg_Phucap.DataMember = dsPhucap.Tables[0].TableName;
            gvPhucap.BestFitColumns();


        }

        private void DisplayInfo_Nhansu_Nsd()
        {
            if (Id_Nhansu_Sd == null)
                return;
            objNhansu = objRexService.Get_Rex_Nhansu_ById(Id_Nhansu_Sd);
            if (objNhansu != null)
            {
                txtTen_Nhansu_A.EditValue = objNhansu.Id_Nhansu;
                lblChuky_Nguoi_Sd_Laodong.Text = objNhansu.Ho_Nhansu + " " + objNhansu.Ten_Nhansu;
                if (is_Numeric(objNhansu.Id_Quocgia))
                    lookUpEdit_Quoctich_Nhansu_A.EditValue = Convert.ToInt64(objNhansu.Id_Quocgia);
                else
                    lookUpEdit_Quoctich_Nhansu_A.EditValue = null;

                if (is_Numeric(objNhansu.Id_Chucvu))
                    lookUp_Chucvu_Nhansu_A.EditValue = Convert.ToInt64(objNhansu.Id_Chucvu);
                else
                    lookUp_Chucvu_Nhansu_A.EditValue = null;
            }
        }

        private void DisplayInfo_Nhansu_Nld()
        {
            objNhansu = objRexService.Get_Rex_Nhansu_ById(Id_Nhansu);
            txtTen_Nhansu_B.EditValue = Id_Nhansu;
            lblChuky_Nguoi_Laodong.Text = objNhansu.Ho_Nhansu + " " + objNhansu.Ten_Nhansu;
            if (is_Numeric(objNhansu.Id_Quocgia))
            {
                lookUpEdit_Quoctich_Nhansu_B.EditValue = Convert.ToInt64(objNhansu.Id_Quocgia);
            }
            txtNgaysinh_Nhansu_B.EditValue = objNhansu.Namsinh;
            if (is_Numeric(objNhansu.Id_Chuyenmon))
            {
                lookUp_Chuyenmon_Nhansu_B.EditValue = Convert.ToInt64(objNhansu.Id_Chuyenmon);
            }
            txtDiachi_Thuongtru_Nhansu_B.EditValue = objNhansu.Diachi_Thuongtru;
            txtCMND_Nhansu_B.Text = objNhansu.Cmnd.ToString();
            dtNgaycap_Nhansu_B.EditValue = objNhansu.Ngaycap;
            txtNoicap_Nhansu_B.EditValue = objNhansu.Noicap;
            dtNgay_Batdau.EditValue = objHopdong_Laodong.Ngay_Batdau;
            dtNgay_Ketthuc.EditValue = objHopdong_Laodong.Ngay_Ketthuc;
        }

        #endregion

        #region Methods override

        public override void ChangeStatus(bool editable)
        {
            txtTen_Nhansu_A.Properties.ReadOnly = editable;
            txtTen_Nhansu_B.Properties.ReadOnly = true;

            txtSolaodong_Nhansu_B.Properties.ReadOnly = editable;
            dtNgaycap_Solaodong_Nhansu_B.Properties.ReadOnly = editable;
            txtNoicap_Solaodong_Nhansu_B.Properties.ReadOnly = editable;
            lookUp_Loai_Hopdong.Properties.ReadOnly = editable;
            dtNgay_Batdau.Properties.ReadOnly = editable;
            dtNgay_Ketthuc.Properties.ReadOnly = editable;
            //dtNgay_Batdau_Thuviec.Properties.ReadOnly = editable;
            //dtNgay_Ketthuc_Thuviec.Properties.ReadOnly = editable;
            txtDiadiem_Lamviec.Properties.ReadOnly = editable;
            lookUp_Chucvu_Nhansu_B.Properties.ReadOnly = editable;
            txtCongviec.Properties.ReadOnly = editable;
            txtThoigian_Lamviec.Properties.ReadOnly = editable;
            txtDungcu_Lamviec.Properties.ReadOnly = editable;
            txtPhuongtien_Dichuyen.Properties.ReadOnly = editable;
            lookUp_Bacluong.Properties.ReadOnly = editable;
            lookUp_Phuongthuc_Huongluong.Properties.ReadOnly = editable;
            //txtPhucap.Properties.ReadOnly = editable;
            txtTraluong.Properties.ReadOnly = editable;
            txtTienthuong.Properties.ReadOnly = editable;
            txtChedo_Nangluong.Properties.ReadOnly = editable;
            txtBaoho_Laodong.Properties.ReadOnly = editable;
            txtChedo_Nghingoi.Properties.ReadOnly = editable;
            txtBaohiem.Properties.ReadOnly = editable;
            txtChedo_Daotao.Properties.ReadOnly = editable;
            txtThoathuan_Khac.Properties.ReadOnly = editable;
            txtBoithuong_Vatchat.Properties.ReadOnly = editable;
            dtLap_Hopdong.Properties.ReadOnly = editable;
            txtNoiky.Properties.ReadOnly = editable;
            dtNgayky.Properties.ReadOnly = editable;
            simpleButton1.Enabled = !editable;
            simpleButton2.Enabled = !editable;
            simpleButton3.Enabled = !editable;
            gvPhucap.OptionsBehavior.Editable = !editable;
            xSignPane1.Enabled = !editable;
            xSignPane2.Enabled = !editable;

        }

        public override void ResetText()
        {
            txtTen_Nhansu_A.EditValue = null;
            lookUpEdit_Quoctich_Nhansu_A.EditValue = null;
            lookUp_Chucvu_Nhansu_A.EditValue = null;
            txtTen_Nhansu_B.EditValue = null;
            lookUpEdit_Quoctich_Nhansu_B.EditValue = null;
            txtNgaysinh_Nhansu_B.Text = "";
            lookUp_Chuyenmon_Nhansu_B.EditValue = null;
            txtDiachi_Thuongtru_Nhansu_B.Text = "";
            txtCMND_Nhansu_B.Text = "";
            dtNgaycap_Nhansu_B.EditValue = null;
            txtNoicap_Nhansu_B.Text = "";
            txtSolaodong_Nhansu_B.Text = "";
            dtNgaycap_Solaodong_Nhansu_B.EditValue = null;
            txtNoicap_Solaodong_Nhansu_B.Text = "";
            lookUp_Loai_Hopdong.EditValue = null;
            dtNgay_Batdau.EditValue = null;
            dtNgay_Ketthuc.EditValue = null;
            //dtNgay_Batdau_Thuviec.EditValue = null;
            //dtNgay_Ketthuc_Thuviec.EditValue = null;
            txtDiadiem_Lamviec.Text = "";
            lookUp_Chucvu_Nhansu_B.EditValue = null;
            txtCongviec.Text = "";
            txtThoigian_Lamviec.Text = "";
            txtDungcu_Lamviec.Text = "";
            txtPhuongtien_Dichuyen.Text = "";
            lookUp_Bacluong.EditValue = null;
            lookUp_Phuongthuc_Huongluong.EditValue = null;
            //txtPhucap.Text = "";
            txtTraluong.Text = "";
            txtTienthuong.Text = "";
            txtChedo_Nangluong.Text = "";
            txtBaoho_Laodong.Text = "";
            txtChedo_Nghingoi.Text = "";
            txtBaohiem.Text = "";
            txtChedo_Daotao.Text = "";
            txtThoathuan_Khac.Text = "";
            txtBoithuong_Vatchat.Text = "";
            dtLap_Hopdong.EditValue = null;
            txtNoiky.Text = "";
            dtNgayky.EditValue = null;
        }

        #endregion

        #region Event Override

        public override bool PerformSave()
        {
            bool success = false;
            try
            {

                #region Kiem Tra Rong
                GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(txtTen_Nhansu_A, lblNhansu1.Text);
                hashtableControls.Add(txtCongty_Daidien, lblCongty2.Text);
                hashtableControls.Add(txtDienthoai_Congty, lblDienthoai_Congty.Text);
                hashtableControls.Add(txtDiachi_Congty, lblDiachi_Congty.Text);
                hashtableControls.Add(txtTen_Nhansu_B, lblNhansu2.Text);
                hashtableControls.Add(lookUp_Loai_Hopdong, lblLoai_Hopdong.Text);
                hashtableControls.Add(dtNgay_Batdau, lblNgay_Batdau.Text);
                //hashtableControls.Add(dtNgay_Ketthuc, lblNgay_Ketthuc.Text);
                //hashtableControls.Add(dtNgay_Batdau_Thuviec, lblNgay_Batdau_Thuviec.Text);
                //hashtableControls.Add(dtNgay_Ketthuc_Thuviec, lblNgay_Ketthuc_Thuviec.Text);
                
                hashtableControls.Add(txtDiadiem_Lamviec, lblDiadiem_Lamviec.Text);
                hashtableControls.Add(lookUp_Chucvu_Nhansu_B, lblChucvu2.Text);
                hashtableControls.Add(txtCongviec, lblCongviec.Text);
                hashtableControls.Add(txtThoigian_Lamviec, lblThoigian_Lamviec.Text);
                hashtableControls.Add(txtDungcu_Lamviec, lblDungcu_Lamviec.Text);
                hashtableControls.Add(txtPhuongtien_Dichuyen, lblPhuongtien_Dichuyen.Text);
                hashtableControls.Add(lookUp_Bacluong, lblHeso.Text);
                hashtableControls.Add(txtLuong_Thoathuan, lblLuong_Thoathuan.Text);
                hashtableControls.Add(lookUp_Phuongthuc_Huongluong, lblHinhthuc_Traluong.Text);
                //hashtableControls.Add(txtPhucap, lblPhucap.Text);
                hashtableControls.Add(txtTraluong, lblTraluong.Text);
                hashtableControls.Add(txtTienthuong, lblTienthuong.Text);
                hashtableControls.Add(txtChedo_Nangluong, lblChedo_Nangluong.Text);
                hashtableControls.Add(txtBaoho_Laodong, lblBaoho_Laodong.Text);
                hashtableControls.Add(txtChedo_Nghingoi, lblChedo_Nghingoi.Text);
                hashtableControls.Add(txtBaohiem, lblBaohiem.Text);
                hashtableControls.Add(txtChedo_Daotao, lblChedo_Daotao.Text);
               
                hashtableControls.Add(dtLap_Hopdong, lblLap_Hopdong.Text);
                hashtableControls.Add(txtNoiky, lblKy_Hopdong.Text);
                hashtableControls.Add(dtNgayky, lblKy_Hopdong.Text);
               
                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                {
                    return false;
                }

                #endregion

                //if(Convert.ToDateTime(dtNgay_Ketthuc_Thuviec.EditValue) > Convert.ToDateTime(dtNgay_Batdau.EditValue) )
                //{
                //    GoobizFrame.Windows.Forms.MessageDialog.Show("Thời gian thử việc không đúng, Nhập lại");
                //    return dtNgay_Ketthuc_Thuviec.Focus();
                //}

                //objNhansu = objRexService.Get_Rex_Nhansu_ById(Id_Nhansu);
                //if(Convert.ToDateTime(objNhansu.Ngay_Vaolam) > Convert.ToDateTime(dtNgay_Batdau_Thuviec.EditValue))
                //{
                //    GoobizFrame.Windows.Forms.MessageDialog.Show("Thời gian bắt đầu thử việc không được nhỏ hơn thời gian bắt đầu làm việc, Nhập lại");
                //    return dtNgay_Batdau_Thuviec.Focus();
                //}

                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckDate(dtNgay_Batdau, dtNgay_Ketthuc))
                {
                    return false;
                }

                if (this.FormState == GoobizFrame.Windows.Forms.FormState.Add)
                {
                    success = Convert.ToBoolean(this.InsertObject());
                }
                if (this.FormState == GoobizFrame.Windows.Forms.FormState.Edit)
                {
                    success = Convert.ToBoolean(this.UpdateObject());
                }
                if (success)
                {
                    if (passData != null)
                    {
                        passData("Updated");
                    }
                }
                this.ChangeStatus(true);

            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");

            }
            return success;
        }

        private object InsertObject()
        {
            objHopdong_Laodong.Id_Hopdong_Laodong = -1;
            objHopdong_Laodong.Ma_Hopdong_Laodong = txtSohopdong.EditValue;
            objHopdong_Laodong.Id_Loai_Hopdong = lookUp_Loai_Hopdong.EditValue;
            objHopdong_Laodong.Id_Nhansu_Nld = txtTen_Nhansu_B.EditValue;
            objHopdong_Laodong.Ngay_Batdau = dtNgay_Batdau.EditValue;
            objHopdong_Laodong.Ngay_Ketthuc = dtNgay_Ketthuc.EditValue;
            objHopdong_Laodong.Id_Nhansu_Nsd = txtTen_Nhansu_A.EditValue;
            if (txtSolaodong_Nhansu_B.EditValue + "" != "")
                objHopdong_Laodong.So_Laodong = txtSolaodong_Nhansu_B.Text.ToString();
            if (dtNgaycap_Solaodong_Nhansu_B.EditValue + "" != "")
                objHopdong_Laodong.Ngaycap_Sld = dtNgaycap_Solaodong_Nhansu_B.DateTime.Date;
            if (txtNoicap_Solaodong_Nhansu_B.EditValue + "" != "")
                objHopdong_Laodong.Noicap_Sld = txtNoicap_Solaodong_Nhansu_B.Text.ToString();
            //objHopdong_Laodong.Ngay_Batdau_Thuviec = dtNgay_Batdau_Thuviec.DateTime.Date;
            //objHopdong_Laodong.Ngay_Ketthuc_Thuviec = dtNgay_Ketthuc_Thuviec.DateTime.Date;
            objHopdong_Laodong.Diachi_Lamviec = txtDiadiem_Lamviec.Text.ToString();
            objHopdong_Laodong.Id_Chucvu_Nld = lookUp_Chucvu_Nhansu_B.EditValue;
            objHopdong_Laodong.Congviec_Phailam = txtCongviec.Text.ToString();
            objHopdong_Laodong.Thoigio_Lamviec = txtThoigian_Lamviec.Text.ToString();
            objHopdong_Laodong.Dungcu_Lamviec = txtDungcu_Lamviec.Text.ToString();
            objHopdong_Laodong.Phuongtien_Dilai = txtPhuongtien_Dichuyen.Text.ToString();
            objHopdong_Laodong.Diachi_Lamviec = txtDiadiem_Lamviec.Text.ToString();
            objHopdong_Laodong.Id_Dienbien_Luong = lookUp_Bacluong.EditValue;
            objHopdong_Laodong.Id_Phuongthuc_Huongluong = lookUp_Phuongthuc_Huongluong.EditValue;
            //objHopdong_Laodong.Phucap_Gom = txtPhucap.Text.ToString();
            objHopdong_Laodong.Ngay_Tra_Luong = txtTraluong.Text.ToString();
            objHopdong_Laodong.Tienthuong = txtTienthuong.Text.ToString();
            objHopdong_Laodong.Chedo_Nangluong = txtChedo_Nangluong.Text.ToString();
            objHopdong_Laodong.Baoho_Laodong_Gom = txtBaoho_Laodong.Text.ToString();
            objHopdong_Laodong.Chedo_Nghingoi = txtChedo_Nghingoi.Text.ToString();
            objHopdong_Laodong.Baohiem = txtBaohiem.Text.ToString();
            objHopdong_Laodong.Chedo_Daotao = txtChedo_Daotao.Text.ToString();
            objHopdong_Laodong.Thoathuan_Khac = txtThoathuan_Khac.Text.ToString();
            objHopdong_Laodong.Boithuong_Vipham = txtBoithuong_Vatchat.Text.ToString();
            objHopdong_Laodong.Ngay_Lap_Hopdong = dtLap_Hopdong.EditValue;
            objHopdong_Laodong.Noiky = txtNoiky.Text.ToString();
            objHopdong_Laodong.Ngayky = dtNgayky.DateTime.Date;

            MemoryStream ms1 = new MemoryStream();
            xSignPane1.Bitmap.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] imgByteArray1 = ms1.GetBuffer();

            objHopdong_Laodong.Kyten_Nld = imgByteArray1;

            MemoryStream ms2 = new MemoryStream();
            xSignPane2.Bitmap.Save(ms2, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] imgByteArray2 = ms2.GetBuffer();

            objHopdong_Laodong.Kyten_Nsd = imgByteArray2;

            objHopdong_Laodong.Luong_Thoathuan = txtLuong_Thoathuan.EditValue + "";

            return objRexService.Insert_Rex_Hopdong_Laodong_Info(objHopdong_Laodong);
        }

        private object UpdateObject()
        {
            //  Gan gia tri vao doi tuong
            objHopdong_Laodong.Id_Hopdong_Laodong = Id_Hopdong;
            objHopdong_Laodong.Id_Loai_Hopdong = lookUp_Loai_Hopdong.EditValue;
            objHopdong_Laodong.Ngay_Batdau = dtNgay_Batdau.EditValue;
            objHopdong_Laodong.Ngay_Ketthuc = dtNgay_Ketthuc.EditValue;
            objHopdong_Laodong.Id_Nhansu_Nsd = txtTen_Nhansu_A.EditValue;
            if (txtSolaodong_Nhansu_B.EditValue + "" != "")
                objHopdong_Laodong.So_Laodong = txtSolaodong_Nhansu_B.Text.ToString();
            if (dtNgaycap_Solaodong_Nhansu_B.EditValue + "" != "")
                objHopdong_Laodong.Ngaycap_Sld = dtNgaycap_Solaodong_Nhansu_B.EditValue;
            if (txtNoicap_Solaodong_Nhansu_B.EditValue + "" != "")
                objHopdong_Laodong.Noicap_Sld = txtNoicap_Solaodong_Nhansu_B.Text.ToString();
            //objHopdong_Laodong.Ngay_Batdau_Thuviec = dtNgay_Batdau_Thuviec.DateTime.Date;
            //objHopdong_Laodong.Ngay_Ketthuc_Thuviec = dtNgay_Ketthuc_Thuviec.DateTime.Date;
            objHopdong_Laodong.Diachi_Lamviec = txtDiadiem_Lamviec.Text.ToString();
            objHopdong_Laodong.Id_Chucvu_Nld = lookUp_Chucvu_Nhansu_B.EditValue;
            objHopdong_Laodong.Congviec_Phailam = txtCongviec.Text.ToString();
            objHopdong_Laodong.Thoigio_Lamviec = txtThoigian_Lamviec.Text.ToString();
            objHopdong_Laodong.Dungcu_Lamviec = txtDungcu_Lamviec.Text.ToString();
            objHopdong_Laodong.Phuongtien_Dilai = txtPhuongtien_Dichuyen.Text.ToString();
            objHopdong_Laodong.Diachi_Lamviec = txtDiadiem_Lamviec.Text.ToString();
            objHopdong_Laodong.Id_Dienbien_Luong = lookUp_Bacluong.EditValue;
            objHopdong_Laodong.Id_Phuongthuc_Huongluong = lookUp_Phuongthuc_Huongluong.EditValue;
            //objHopdong_Laodong.Phucap_Gom = txtPhucap.Text.ToString();
            objHopdong_Laodong.Ngay_Tra_Luong = txtTraluong.Text.ToString();
            objHopdong_Laodong.Tienthuong = txtTienthuong.Text.ToString();
            objHopdong_Laodong.Chedo_Nangluong = txtChedo_Nangluong.Text.ToString();
            objHopdong_Laodong.Baoho_Laodong_Gom = txtBaoho_Laodong.Text.ToString();
            objHopdong_Laodong.Chedo_Nghingoi = txtChedo_Nghingoi.Text.ToString();
            objHopdong_Laodong.Baohiem = txtBaohiem.Text.ToString();
            objHopdong_Laodong.Chedo_Daotao = txtChedo_Daotao.Text.ToString();
            objHopdong_Laodong.Thoathuan_Khac = txtThoathuan_Khac.Text.ToString();
            objHopdong_Laodong.Boithuong_Vipham = txtBoithuong_Vatchat.Text.ToString();
            objHopdong_Laodong.Ngay_Lap_Hopdong = dtLap_Hopdong.EditValue;
            objHopdong_Laodong.Noiky = txtNoiky.Text.ToString();
            objHopdong_Laodong.Ngayky = dtNgayky.DateTime.Date;

            MemoryStream ms1 = new MemoryStream();
            xSignPane1.Bitmap.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] imgByteArray1 = ms1.ToArray();

            objHopdong_Laodong.Kyten_Nld = imgByteArray1;

            MemoryStream ms2 = new MemoryStream();
            xSignPane2.Bitmap.Save(ms2, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] imgByteArray2 = ms2.ToArray();

            objHopdong_Laodong.Kyten_Nsd = imgByteArray2;

            objHopdong_Laodong.Luong_Thoathuan = txtLuong_Thoathuan.EditValue + "";

            //this.FormState = GoobizFrame.Windows.Forms.FormState.Save;
            return objRexService.Update_Rex_Hopdong_Laodong_Info_ByID_Hopdong(objHopdong_Laodong);
        }

        public override bool PerformAdd()
        {
            ResetText();
            this.FormState = GoobizFrame.Windows.Forms.FormState.Add;
            this.ChangeStatus(false);
            this.txtSohopdong.EditValue = objRexService.Get_Rex_Hopdong_Laodong_SoHD();
            return true;
        }

        public override bool PerformEdit()
        {
            this.FormState = GoobizFrame.Windows.Forms.FormState.Edit;
            this.ChangeStatus(false);
            if ("" + txtTen_Nhansu_A.EditValue == "")
            {
                long CompanyRepresented = Convert.ToInt64("0"+dsDm_Heso_Chuongtrinh.Tables[0].Select("Ma_Heso_Chuongtrinh='CompanyRepresented'")[0]["Heso"].ToString().Split(new char[] { '|' })[0]);
                Id_Nhansu_Sd = CompanyRepresented;
                DisplayInfo_Nhansu_Nsd();
            }
            return true;
        }

        public override bool PerformCancel()
        {
            this.DisplayInfo();
            if (this.FormState == GoobizFrame.Windows.Forms.FormState.Add)
            {
                this.Close();
            }
            this.FormState = GoobizFrame.Windows.Forms.FormState.Cancel;
            this.ChangeStatus(true);
            return true;
        }

        public override bool PerformPrintPreview()
        {
            DataSet dsHDLD = objRexService.Get_Hopdong_Nhansu_Info_In_Hoso(Id_Hopdong).ToDataSet();
            DataSet dsHeso_Chuongtrinh = objMasterService.Get_Rex_Dm_Heso_Chuongtrinh_Collection3().ToDataSet();
            Reports.rptRex_Hopdong_Nhansu XtraReport = new Ecm.Rex.Reports.rptRex_Hopdong_Nhansu();
            GoobizFrame.Windows.Forms.FrmPrintPreview frmPrintPreview = new GoobizFrame.Windows.Forms.FrmPrintPreview();

            XtraReport.xrTen_Donvi.Text = dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "CompanyName"))[0]["Heso"].ToString();
            XtraReport.xrTen_Congty.Text = dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "CompanyName"))[0]["Heso"].ToString();
            XtraReport.xrDiachi_Congty.Text = dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "CompanyAddress"))[0]["Heso"].ToString();
            XtraReport.xrDienthoai_Congty.Text = dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "CompanyTel"))[0]["Heso"].ToString();

            XtraReport.xrPic_Kyten_Nld.DataBindings.Add(
                       new DevExpress.XtraReports.UI.XRBinding("Image", dsHDLD, dsHDLD.Tables[0].TableName + ".Kyten_Nld"));
            XtraReport.xrPic_Kyten_Nsd.DataBindings.Add(
                       new DevExpress.XtraReports.UI.XRBinding("Image", dsHDLD, dsHDLD.Tables[0].TableName + ".Kyten_Nsd"));

            frmPrintPreview.Report = XtraReport;
            XtraReport.DataSource = dsHDLD;

            Reports.rptRex_Phucap rptPhucap = new Ecm.Rex.Reports.rptRex_Phucap();
            XtraReport.xrPhucap.ReportSource = rptPhucap;
            rptPhucap.DataSource = dsPhucap;
            rptPhucap.CreateDocument();

            XtraReport.CreateDocument();

            frmPrintPreview.printControl1.PrintingSystem = XtraReport.PrintingSystem;
            frmPrintPreview.MdiParent = this.MdiParent;
            frmPrintPreview.Text = this.Text + " - In hợp đồng lao động";
            frmPrintPreview.ShowDialog();
            frmPrintPreview.Activate();
            return true;
        }

        public override bool PerformDelete()
        {
            if (GoobizFrame.Windows.Forms.UserMessage.Show("Msg00004", new string[]  {
            GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Rex_Hopdong_Laodong_Info"),
            GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Rex_Hopdong_Laodong_Info")   }) == DialogResult.Yes)
            {
                objRexService.Delete_Rex_Hopdong_Laodong(objHopdong_Laodong);
                //Pass data to mainform
                if (passData != null)
                {
                    passData("Updated");
                }
                this.Close();
            }
            return true;
        }

        #endregion

        

        #region Event Handling

        private void txtTen_Nhansu1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.FormState == GoobizFrame.Windows.Forms.FormState.Edit)
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
                {
                    Ecm.Rex.Forms.Frmrex_Nhansu_Dialog objFrmrex_Nhansu_Dialog = new Ecm.Rex.Forms.Frmrex_Nhansu_Dialog();
                    objFrmrex_Nhansu_Dialog.Text = "Chọn Nhân sự theo Bộ phận";
                    GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(objFrmrex_Nhansu_Dialog);
                    objFrmrex_Nhansu_Dialog.ShowDialog();
                    if (objFrmrex_Nhansu_Dialog.SelectedRex_Nhansu != null)
                    {
                        //Id_Nhansu_Sd = objFrmrex_Nhansu_Dialog.Id_Nhansu[0];
                        Id_Nhansu_Sd = objFrmrex_Nhansu_Dialog.SelectedRex_Nhansu.Id_Nhansu;
                        DisplayInfo_Nhansu_Nsd();
                    }
                }
        }

        private void txtTen_Nhansu2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //nhanvuong comment -- không cho thay đổi nhân sự của hợp đồng
            //if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            //{
            //    Ecm.Rex.Forms.Frmrex_Nhansu_Dialog objFrmrex_Nhansu_Dialog = new Ecm.Rex.Forms.Frmrex_Nhansu_Dialog();
            //    objFrmrex_Nhansu_Dialog.Text = "Chọn Nhân sự theo Bộ phận";
            //    GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(objFrmrex_Nhansu_Dialog);
            //    objFrmrex_Nhansu_Dialog.ShowDialog();
            //    if (objFrmrex_Nhansu_Dialog.Id_Nhansu != null)
            //    {
            //        Id_Nhansu = objFrmrex_Nhansu_Dialog.Id_Nhansu[0];
            //        DisplayInfo_Nhansu_Nld();
            //    }
            //}
        }

        private void txtTen_Nhansu1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Id_Nhansu_Sd = txtTen_Nhansu_A.EditValue;
                DisplayInfo_Nhansu_Nsd();
            }
        }

        private void txtTen_Nhansu2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Id_Nhansu = txtTen_Nhansu_B.EditValue;
                DisplayInfo_Nhansu_Nld();
            }
        }

        #endregion

        /// <summary>
        /// Phong Tran - Kiem tra gia tri co phai la so hay khong?
        /// <returns>True/False</returns>
        private bool is_Numeric(object value)
        {
            try
            {
                if (Convert.ToInt64(value) > 0)
                {
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void dg_Phucap_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dsBotri_Nhansu = objRexService.Get_Rex_Botri_Nhansu_For_Hopdong_Laodong_Collection(Id_Nhansu, dtNgay_Batdau.EditValue).ToDataSet();

            txtDiadiem_Lamviec.DataBindings.Clear();
            txtDiadiem_Lamviec.DataBindings.Add("EditValue", dsBotri_Nhansu, dsBotri_Nhansu.Tables[0].TableName + ".Ten_Bophan");
            lookUp_Phuongthuc_Huongluong.DataBindings.Clear();
            lookUp_Phuongthuc_Huongluong.DataBindings.Add("EditValue", dsBotri_Nhansu, dsBotri_Nhansu.Tables[0].TableName + ".Id_Phuongthuc_Huongluong");

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            dsBotri_Nhansu = objRexService.Get_Rex_Botri_Nhansu_For_Hopdong_Laodong_Collection(Id_Nhansu, dtNgay_Batdau.EditValue).ToDataSet();

            lookUp_Chucvu_Nhansu_B.DataBindings.Clear();
            lookUp_Chucvu_Nhansu_B.DataBindings.Add("EditValue", dsBotri_Nhansu, dsBotri_Nhansu.Tables[0].TableName + ".Id_Chucvu");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            dsDienbien_Luong = objRexService.Get_Rex_Dienbien_Luong_For_Hopdong_Laodong_Collection(Id_Nhansu, dtNgay_Batdau.EditValue).ToDataSet();

            txtLuong_Thoathuan.DataBindings.Clear();
            txtLuong_Thoathuan.DataBindings.Add("EditValue", dsDienbien_Luong, dsDienbien_Luong.Tables[0].TableName + ".Luong_Thoathuan");
            lookUp_Bacluong.DataBindings.Clear();
            lookUp_Bacluong.DataBindings.Add("EditValue", dsDienbien_Luong, dsDienbien_Luong.Tables[0].TableName + ".Id_Bacluong");
        }


    }
}


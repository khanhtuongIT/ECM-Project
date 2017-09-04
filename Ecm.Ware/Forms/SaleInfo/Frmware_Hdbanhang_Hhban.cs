﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SunLine.Ware.Forms
{

    public partial class Frmware_Hdbanhang_Hhban : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public WareService.WareService objWareService = new SunLine.Ware.WareService.WareService();
        public RexService.RexService objRexService = new SunLine.Ware.RexService.RexService();
        public MasterService.MasterService objMasterService = new SunLine.Ware.MasterService.MasterService();

        DataSet ds_Hdbanhang = new DataSet();
        DataSet ds_Hdbanhang_Chitiet = new DataSet();
        DataSet dsCtrinh_Kmai_Chitiet;
        bool AllowEdit_Per_Dongia = false;
        int Id_Nhansu_Km = -1;
        DataSet dsRptware_Nxt_Hhban;
        DataSet ds_Hanghoa_Ban;

        object so_seri_Old;

        public Frmware_Hdbanhang_Hhban()
        {
            InitializeComponent();

            //date mask
            dtNgay_Chungtu.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtNgay_Chungtu.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();

            dtNgay_Thanhtoan.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtNgay_Thanhtoan.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();

            this.DisplayInfo();
            this.gridView4.OptionsBehavior.Editable = false;
            this.item_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.item_Add.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        public override void DisplayInfo()
        {
            try
            {
                //Get data Ware_Dm_Nhacungcap
                lookUpEdit_Cuahang_Ban.Properties.DataSource = objMasterService.Get_All_Ware_Dm_Cuahang_Ban().Tables[0];
                lookUpEdit_Cuahang_Ban2.Properties.DataSource = objMasterService.Get_All_Ware_Dm_Cuahang_Ban().Tables[0];


                //Get data Ware_Dm_Khachhang
                lookUpEdit_Donvi_Muahang.Properties.DataSource = objMasterService.Get_All_Ware_Dm_Khachhang().Tables[0];

                //Get data Ware_Dm_Hanghoa_Mua
                ds_Hanghoa_Ban = objMasterService.Get_All_Ware_Dm_Hanghoa_Ban();
                gridLookUpEdit_Hanghoa_Ban.DataSource = ds_Hanghoa_Ban.Tables[0];
                gridLookUpEdit_Ma_Hanghoa_Ban.DataSource = ds_Hanghoa_Ban.Tables[0];

                //Get data Rex_Nhansu
                lookUpEdit_Nhansu_Banhang.Properties.DataSource = objRexService.Get_All_Rex_Nhansu_Collection().Tables[0];
                //Kiểm tra nếu nhân viên login không tồn tại trong kho hàng hóa mua thì access denied.
                lookUpEdit_Nhansu_Banhang.EditValue = Convert.ToInt64(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu());
                DataSet ds_Cuahang_Ban = objMasterService.Get_All_Ware_Dm_Cuahang_Ban_By_Id_Nhansu(lookUpEdit_Nhansu_Banhang.EditValue);
                if (ds_Cuahang_Ban.Tables[0].Rows.Count > 0)
                    lookUpEdit_Cuahang_Ban.EditValue = ds_Cuahang_Ban.Tables[0].Rows[0]["Id_Cuahang_Ban"];

                //Get data Ware_Dm_Donvitinh
                gridLookUpEdit_Donvitinh.DataSource = objMasterService.Get_All_Ware_Dm_Donvitinh().Tables[0];

                //Get data Ware_Hdbanhang
                ds_Hdbanhang = objWareService.Get_All_Ware_Hdbanhang_ByCuahang(lookUpEdit_Cuahang_Ban.EditValue);
                dgware_Hdbanhang.DataSource = ds_Hdbanhang;
                dgware_Hdbanhang.DataMember = ds_Hdbanhang.Tables[0].TableName;

                dsCtrinh_Kmai_Chitiet = objWareService.Get_All_Ware_Ctrinh_Kmai_Chitiet_ByDate(DateTime.Now);

                this.DataBindingControl();

                this.ChangeStatus(false);

                this.gridView1.BestFitColumns();

                DisplayInfo2();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif

                //GoobizFrame.Windows.HelperClasses.ExceptionLogger.LogException1(ex);
            }

        }

        void ClearDataBindings()
        {
            this.txtId_Hdbanhang.DataBindings.Clear();
            this.txtNguoimua.DataBindings.Clear();          
            this.txtPhuongthuc_Thanhtoan.DataBindings.Clear();
            this.txtSo_Seri.DataBindings.Clear();
            this.txtSochungtu.DataBindings.Clear();
            this.txtSotien.DataBindings.Clear();
            this.txtSotien_Vat.DataBindings.Clear();
            this.txtTongtien.DataBindings.Clear();
            this.dtNgay_Chungtu.DataBindings.Clear();
            this.dtNgay_Thanhtoan.DataBindings.Clear();

            this.lookUpEdit_Cuahang_Ban.DataBindings.Clear();
            this.txtDonvi_Muahang.DataBindings.Clear();
            this.lookUpEdit_Nhansu_Banhang.DataBindings.Clear();
        }

        public void DataBindingControl()
        {
            try
            {
                ClearDataBindings();

                this.txtId_Hdbanhang.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Id_Hdbanhang");
                this.txtNguoimua.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Hoten_Nguoimua");              
                this.txtPhuongthuc_Thanhtoan.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Phuongthuc_Thanhtoan");
                this.txtSo_Seri.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".So_Seri");
                this.txtSochungtu.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Sochungtu");
                this.txtSotien.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Sotien");
                this.txtSotien_Vat.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Sotien_Vat");
                this.txtTongtien.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Tongtien");

                this.dtNgay_Chungtu.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Ngay_Chungtu");
                this.dtNgay_Thanhtoan.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Ngay_Thanhtoan");

                this.lookUpEdit_Cuahang_Ban.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Id_Cuahang_Ban");
                this.txtDonvi_Muahang.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Donvi_muahang");
                this.lookUpEdit_Nhansu_Banhang.DataBindings.Add("EditValue", ds_Hdbanhang, ds_Hdbanhang.Tables[0].TableName + ".Id_Nhansu_Bh");
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif

                //GoobizFrame.Windows.HelperClasses.ExceptionLogger.LogException1(ex);
            }
        }

        public void ChangeStatus(bool editTable)
        {
            this.dgware_Hdbanhang.Enabled = !editTable;
            this.txtId_Hdbanhang.Properties.ReadOnly = !editTable;        
            this.txtPhuongthuc_Thanhtoan.Properties.ReadOnly = !editTable;
            this.txtSo_Seri.Properties.ReadOnly = !editTable;     
            this.dtNgay_Thanhtoan.Properties.ReadOnly = !editTable;

            this.lookUpEdit_Cuahang_Ban.Properties.ReadOnly = !editTable;
            this.lookUpEdit_Donvi_Muahang.Properties.ReadOnly = !editTable;
            this.lookUpEdit_Nhansu_Banhang.Properties.ReadOnly = !editTable;

            this.btnImport_From_Donmuahang_Kh.Enabled = editTable;

            this.txtDonvi_Muahang.Enabled = editTable;
            this.txtDienthoai.Enabled = editTable;
            this.txtDiachi.Enabled = editTable;
            this.txtMasothue.Enabled = editTable;
            this.txtNguoimua.Enabled = editTable;
        }

        public void ResetText()
        {
            this.txtId_Hdbanhang.EditValue = "";
            this.txtNguoimua.EditValue = "";
            this.txtPhuongthuc_Thanhtoan.EditValue = "";
            this.txtSo_Seri.EditValue = "";
            this.txtSochungtu.EditValue = "";
            this.txtSotien.EditValue = "";
            this.txtSotien_Vat.EditValue = "";
            this.txtTongtien.EditValue = "";
            this.dtNgay_Chungtu.EditValue = null;
            this.dtNgay_Thanhtoan.EditValue = null;

            this.lookUpEdit_Cuahang_Ban.EditValue = null;
            this.lookUpEdit_Donvi_Muahang.EditValue = "";


            ////begin******
            ////Khi add new: Get id_nhansu login -> Get cửa hàng bán(kho_hh_mua) theo id_nhansu -> Get hàng hóa sản xuất(mua) theo cửa hàng bán(kho_hh_mua)
            ////Kiểm tra nếu cửa hàng bán(kho_hh_mua) != null thì lock <-else-> unlock
            //object Id_Nhansu = Convert.ToInt64(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu());
            //this.lookUpEdit_Nhansu_Banhang.EditValue = Id_Nhansu;
            //lookUpEdit_Cuahang_Ban.EditValue = objWareService.Get_All_Ware_Dm_Cuahang_Ban_By_Id_Nhansu(Id_Nhansu).Tables[0].Rows[0]["Id_Cuahang_Ban"];

            //if ("" + lookUpEdit_Cuahang_Ban.EditValue != "")
            //    lookUpEdit_Cuahang_Ban.Properties.ReadOnly = true;
            //else
            //    lookUpEdit_Cuahang_Ban.Properties.ReadOnly = false;
            ////end******

            //this.ds_Hdbanhang_Chitiet = objWareService.Get_All_Ware_Hdbanhang_Chitiet_By_Hdbanhang(0);
            //this.dgware_Hdbanhang_Chitiet.DataSource = ds_Hdbanhang_Chitiet.Tables[0];
        }

        #region Event Override
        public object InsertObject()
        {
            try
            {
                WareService.Ware_Hdbanhang objWare_Hdbanhang = new SunLine.Ware.WareService.Ware_Hdbanhang();
                objWare_Hdbanhang.Id_Hdbanhang = -1;
                objWare_Hdbanhang.Hoten_Nguoimua = txtNguoimua.EditValue;
                objWare_Hdbanhang.Per_Vat = txtPer_Vat.EditValue;
                objWare_Hdbanhang.Phuongthuc_Thanhtoan = txtPhuongthuc_Thanhtoan.EditValue;
                objWare_Hdbanhang.So_Seri = txtSo_Seri.EditValue;
                objWare_Hdbanhang.Sochungtu = txtSochungtu.EditValue;
                objWare_Hdbanhang.Sotien = txtSotien.EditValue;
                objWare_Hdbanhang.Sotien_Vat = txtSotien_Vat.EditValue;
                objWare_Hdbanhang.Ngay_Chungtu = dtNgay_Chungtu.EditValue;
                objWare_Hdbanhang.Ngay_Thanhtoan = dtNgay_Thanhtoan.EditValue;

                if ("" + lookUpEdit_Cuahang_Ban.EditValue != "")
                    objWare_Hdbanhang.Id_Cuahang_Ban = lookUpEdit_Cuahang_Ban.EditValue;

                if ("" + lookUpEdit_Donvi_Muahang.EditValue != "")
                    objWare_Hdbanhang.Id_Khachang = lookUpEdit_Donvi_Muahang.EditValue;

                if ("" + lookUpEdit_Nhansu_Banhang.EditValue != "")
                    objWare_Hdbanhang.Id_Nhansu_Bh = lookUpEdit_Nhansu_Banhang.EditValue;

                object identity = objWareService.Insert_Ware_Hdbanhang(objWare_Hdbanhang);

                if (identity != null)
                {
                    dgware_Hdbanhang_Chitiet.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                    foreach (DataRow dr in ds_Hdbanhang_Chitiet.Tables[0].Rows)
                    {
                        dr["Id_Hdbanhang"] = identity;
                    }
                    //update hdmuahang_chitiet
                    objWareService.Update_Ware_Hdbanhang_Chitiet_Collection(ds_Hdbanhang_Chitiet);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public object UpdateObject()
        {
            try
            {
                WareService.Ware_Hdbanhang objWare_Hdbanhang = new SunLine.Ware.WareService.Ware_Hdbanhang();
                objWare_Hdbanhang.Id_Hdbanhang = gridView1.GetFocusedRowCellValue("Id_Hdbanhang");
                objWare_Hdbanhang.Hoten_Nguoimua = txtNguoimua.EditValue;             
                objWare_Hdbanhang.Phuongthuc_Thanhtoan = txtPhuongthuc_Thanhtoan.EditValue;
                objWare_Hdbanhang.So_Seri = txtSo_Seri.EditValue;
                objWare_Hdbanhang.Sochungtu = txtSochungtu.EditValue;              
                objWare_Hdbanhang.Ngay_Chungtu = dtNgay_Chungtu.EditValue;
                objWare_Hdbanhang.Ngay_Thanhtoan = dtNgay_Thanhtoan.EditValue;
                objWare_Hdbanhang.Maso_Thue = txtMasothue.EditValue;
                objWare_Hdbanhang.Dienthoai = txtDienthoai.EditValue;
                objWare_Hdbanhang.Diachi = txtDiachi.EditValue;
                objWare_Hdbanhang.Donvi_muahang = txtDonvi_Muahang.EditValue;

                if ("" + txtPer_Vat.EditValue != "")
                    objWare_Hdbanhang.Per_Vat = txtPer_Vat.EditValue;

                if ("" + txtSotien.EditValue != "")
                    objWare_Hdbanhang.Sotien = txtSotien.EditValue;

                if ("" + txtTongtien.EditValue != "")
                    objWare_Hdbanhang.Sotien_Vat = txtTongtien.EditValue;

                if ("" + lookUpEdit_Cuahang_Ban.EditValue != "")
                    objWare_Hdbanhang.Id_Cuahang_Ban = lookUpEdit_Cuahang_Ban.EditValue;
     
                  

                if ("" + lookUpEdit_Nhansu_Banhang.EditValue != "")
                    objWare_Hdbanhang.Id_Nhansu_Bh = lookUpEdit_Nhansu_Banhang.EditValue;

                objWare_Hdbanhang.Id_Nhansu_Edit = GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu();



                //update hdbanhang_chitiet
                dgware_Hdbanhang_Chitiet.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                foreach (DataRow dr in ds_Hdbanhang_Chitiet.Tables[0].Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                        dr["Id_Hdbanhang"] = txtId_Hdbanhang.EditValue;
                }
                
                //update hdbanhang
                objWareService.Update_Ware_Hdbanhang(objWare_Hdbanhang);

                //ds_Hdbanhang_Chitiet.RejectChanges();
                objWareService.Update_Ware_Hdbanhang_Chitiet_Collection(ds_Hdbanhang_Chitiet);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public object DeleteObject()
        {
            WareService.Ware_Hdbanhang objWare_Hdbanhang = new SunLine.Ware.WareService.Ware_Hdbanhang();
            objWare_Hdbanhang.Id_Hdbanhang = gridView1.GetFocusedRowCellValue("Id_Hdbanhang");

            return objWareService.Delete_Ware_Hdbanhang(objWare_Hdbanhang);
        }

        public override bool PerformAdd()
        {
            dtNgay_Chungtu.EditValue = DateTime.Now;
            //Kiểm tra nếu nhân viên login không tồn tại trong kho hàng hóa mua thì access denied.
            lookUpEdit_Nhansu_Banhang.EditValue = Convert.ToInt64(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu());
            DataSet ds_Cuahang_Ban = objMasterService.Get_All_Ware_Dm_Cuahang_Ban_By_Id_Nhansu(lookUpEdit_Nhansu_Banhang.EditValue);
            if (ds_Cuahang_Ban.Tables[0].Rows.Count > 0)
                lookUpEdit_Cuahang_Ban.EditValue = ds_Cuahang_Ban.Tables[0].Rows[0]["Id_Cuahang_Ban"];
            else
            {
                GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                lookUpEdit_Nhansu_Banhang.EditValue = null;
                return false;
            }

            ClearDataBindings();
            this.ChangeStatus(true);
            this.ResetText();
            txtSochungtu.EditValue = objWareService.GetNew_Sochungtu("ware_hdbanhang", "sochungtu", "HĐ-GTGT");

            //get ds hang hoa ton trog ngay
            DateTime ngay_chungtu = dtNgay_Chungtu.DateTime;
            dsRptware_Nxt_Hhban = objWareService.Rptware_Nxt_Hhban(new DateTime(ngay_chungtu.Year, ngay_chungtu.Month, ngay_chungtu.Day, 0, 0, 0)
                , new DateTime(ngay_chungtu.Year, ngay_chungtu.Month, ngay_chungtu.Day, 23, 0, 0)
                , lookUpEdit_Cuahang_Ban.EditValue);

            AllowEdit_Per_Dongia = false;

            return true;
        }

        public override bool PerformEdit()
        {
            //if (!GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu().Equals("" + lookUpEdit_Nhansu_Banhang.EditValue))
            //{
            //    GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
            //    return false;
            //}
            so_seri_Old = txtSo_Seri.EditValue;
            this.dockPanel1.Hide();
            this.ChangeStatus(true);
            return true;
        }

        public override bool PerformCancel()
        {
            this.dockPanel1.Show();
            this.DisplayInfo();
            this.ChangeStatus(false);
            return true;
        }

        public override bool PerformSave()
        {
            try
            {
                bool success = false;

                System.Collections.Hashtable htbControl1 = new System.Collections.Hashtable();
                htbControl1.Add(txtSochungtu, lblSochungtu.Text);
                htbControl1.Add(dtNgay_Chungtu, lblNgay_Chungtu.Text);
                htbControl1.Add(dtNgay_Thanhtoan, lblNgay_Thanhtoan.Text);
                htbControl1.Add(lookUpEdit_Cuahang_Ban, lblCuahang_Ban.Text);
                //      htbControl1.Add(lookUpEdit_Khachhang, lblKhachhang.Text);
                htbControl1.Add(lookUpEdit_Nhansu_Banhang, lblNhansu_Banhang.Text);
                htbControl1.Add(txtSo_Seri, lblSo_Seri.Text);
                htbControl1.Add(txtDiachi, lblDiachi.Text);
                htbControl1.Add(txtDienthoai, lblDienthoai.Text);
                htbControl1.Add(txtMasothue, lblMasothue.Text);
                htbControl1.Add(txtNguoimua, lblNguoimua.Text);
                htbControl1.Add(txtPhuongthuc_Thanhtoan, lblPhuongthuc_Thanhtoan.Text);

                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(htbControl1))
                    return false;

                if (!txtSo_Seri.EditValue.Equals(so_seri_Old))
                {
                    if (!GoobizFrame.Windows.MdiUtils.Validator.CheckExistValues(htbControl1, ds_Hdbanhang, "So_Seri"))
                        return false;
                }

                if (this.FormState == GoobizFrame.Windows.Forms.FormState.Add)
                {
                    success = (bool)this.InsertObject();
                }
                else if (this.FormState == GoobizFrame.Windows.Forms.FormState.Edit)
                {
                    success = (bool)this.UpdateObject();
                }

                if (success)
                {
                    this.DisplayInfo();
                }
                this.dockPanel1.Show();
                return success;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public override bool PerformDelete()
        {
            if (GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUser().ToUpper() != "ADMIN")
                if (!GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu().Equals("" + lookUpEdit_Nhansu_Banhang.EditValue))
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                    return false;
                }

            if (GoobizFrame.Windows.Forms.UserMessage.Show("Msg00004", new string[]  {
            GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Ware_Hdbanhang"),
            GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Ware_Hdbanhang")   }) == DialogResult.Yes)
            {
                try
                {
                    this.DeleteObject();
                }
                catch (Exception ex)
                {
                    GoobizFrame.Windows.MdiUtils.Validator.CheckReferencedRecord(ex.Message, "");
                }
                this.DisplayInfo();
            }
            return base.PerformDelete();
        }

        public override bool PerformPrintPreview()
        {
            //DataSet _ds_Hdbanhang_Chitiet = ds_Hdbanhang_Chitiet.Copy();
            //ds_Hdbanhang_Chitiet.Tables[0].Columns["Ten_Hanghoa_Ban"].ColumnName = "Ten_Hanghoa";

            DataSets.dsHdbanhang_Chitiet dsrHdbanhang_Chitiet = new SunLine.Ware.DataSets.dsHdbanhang_Chitiet();
            Reports.rptHdbanhang_VAT rptHdbanhang_VAT = new SunLine.Ware.Reports.rptHdbanhang_VAT();
            GoobizFrame.Windows.Forms.FormReportWithHeader objFormReport = new GoobizFrame.Windows.Forms.FormReportWithHeader();
            objFormReport.Report = rptHdbanhang_VAT;
            rptHdbanhang_VAT.DataSource = dsrHdbanhang_Chitiet;

            int i = 1;
            foreach (DataRow dr in ds_Hdbanhang_Chitiet.Tables[0].Rows)
            {
                DataRow drnew = dsrHdbanhang_Chitiet.Tables[0].NewRow();
                foreach (DataColumn dc in ds_Hdbanhang_Chitiet.Tables[0].Columns)
                {
                    try
                    {
                        drnew[dc.ColumnName] = dr[dc.ColumnName];
                    }
                    catch
                    {
                        continue;
                    }
                }
                drnew["Ten_Hanghoa"] = dr["Ten_Hanghoa_Ban"];
                drnew["Stt"] = i++;

                dsrHdbanhang_Chitiet.Tables[0].Rows.Add(drnew);
            }
            //add parameter values

            //para company
            GoobizFrame.Profile.Config conf = new GoobizFrame.Profile.Config(@"Resources\HostConfiguration.config");
            conf.GroupName = "HostConfiguration";
            rptHdbanhang_VAT.tbcCompany_Account.Text = "" + conf.GetValue("Theme", "Taikhoan");
            rptHdbanhang_VAT.tbcCompany_Address.Text = "" + conf.GetValue("Theme", "CompanyAddress");
            rptHdbanhang_VAT.tbcCompany_Masothue.Text = "" + conf.GetValue("Theme", "Masothue");
            rptHdbanhang_VAT.tbcCompany_Name.Text = "" + conf.GetValue("Theme", "CompanyName");
            rptHdbanhang_VAT.tbcCompany_Tel.Text = "" + conf.GetValue("Theme", "CompanyTel");

            //para khachhang
            rptHdbanhang_VAT.tbc_Masothue_Khachhang.Text = "" + lookUpEdit_Donvi_Muahang.GetColumnValue("Masothue");
            rptHdbanhang_VAT.tbcDiachi_Khachhang.Text = "" + lookUpEdit_Donvi_Muahang.GetColumnValue("Diachi_Khachhang");
            rptHdbanhang_VAT.tbcHinhthuc_Thanhtoan.Text = txtPhuongthuc_Thanhtoan.Text;
            rptHdbanhang_VAT.tbcKhachhang.Text = lookUpEdit_Donvi_Muahang.Text;
            rptHdbanhang_VAT.tbcNgay_Chungtu.Text = dtNgay_Chungtu.Text;

            rptHdbanhang_VAT.tbcThanhtien.Text = txtSotien.Text;
            rptHdbanhang_VAT.tbcTien_Vat.Text = txtSotien_Vat.Text;
            rptHdbanhang_VAT.tbcVat.Text = txtPer_Vat.Text;
            rptHdbanhang_VAT.tbcTongcong.Text = txtTongtien.Text;
            rptHdbanhang_VAT.lblNhansu_Banhang.Text = lookUpEdit_Nhansu_Banhang.Text;

            double tongtien = Convert.ToDouble(txtTongtien.EditValue);
            string str = GoobizFrame.Windows.HelperClasses.ReadNumber.ChangeNum2VNStr(tongtien, " đồng.");
            str = str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();

            rptHdbanhang_VAT.tbcThanhtien_Bangchu.Text = str;
            
            rptHdbanhang_VAT.CreateDocument();
            objFormReport.printControl1.PrintingSystem = rptHdbanhang_VAT.PrintingSystem;
            objFormReport.MdiParent = this.MdiParent;
            objFormReport.Text = this.Text + "(Xem trang in)";
            objFormReport.Show();
            objFormReport.Activate();
            return base.PerformPrintPreview();
        }
        #endregion

        private void gridView4_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                //cho phep nhan vien ql giam gia cho khach hang
                if (e.Column.FieldName == "Per_Dongia" && !AllowEdit_Per_Dongia)
                {
                    //neu thay doi fiel giam gia --> phai xac nhan nguoi thay doi co quyen hay khong
                    SunLine.SystemControl.DBUsers.IDCardLogonWithResult IDCardLogonWithResult = new SunLine.SystemControl.DBUsers.IDCardLogonWithResult();
                    GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(IDCardLogonWithResult);
                    IDCardLogonWithResult.Right_System_Name = this.Name;
                    IDCardLogonWithResult.ShowDialog();
                    if (IDCardLogonWithResult.Actions.Count == 0 || !IDCardLogonWithResult.Actions.Contains("EnableReduce"))
                    {
                        gridView4.CancelUpdateCurrentRow();
                        //DataRow dr_change = gridView4.GetDataRow(e.RowHandle);
                        //dr_change.CancelEdit();
                        return;
                    }
                    else
                    {
                        AllowEdit_Per_Dongia = true;
                        Id_Nhansu_Km = Convert.ToInt32(IDCardLogonWithResult.Id_Nhansu);
                    }
                }

                //Kiem tra so luong ton
                if (e.Column.FieldName == "Soluong")
                {
                    DataRow[] sr_hh_nxt_ton = dsRptware_Nxt_Hhban.Tables[0].Select("Id_Hanghoa_Ban = " + gridView4.GetFocusedRowCellValue("Id_Hanghoa_Ban")
                                + " and Soluong_Ton >= " + gridView4.GetFocusedRowCellValue("Soluong"));
                    if (sr_hh_nxt_ton == null || sr_hh_nxt_ton.Length == 0)
                    {
                        GoobizFrame.Windows.Forms.UserMessage.Show("QUANLATY_NOT_AVAILABLE", new string[] { });
                        gridView4.CancelUpdateCurrentRow();
                        //DataRow dr_change = gridView4.GetDataRow(e.RowHandle);
                        //dr_change.CancelEdit();
                        return;
                    }
                }


                if (e.Column.FieldName == "Soluong" || e.Column.FieldName == "Dongia_Ban")
                {
                    if ("" + gridView4.GetFocusedRowCellValue("Soluong") != ""
                        && "" + gridView4.GetFocusedRowCellValue("Dongia_Ban") != "")
                    {
                        gridView4.SetFocusedRowCellValue(gridView4.Columns["Thanhtien"], Convert.ToDecimal(gridView4.GetFocusedRowCellValue("Soluong"))
                                                                     * Convert.ToDecimal(gridView4.GetFocusedRowCellValue("Dongia_Ban"))
                                                        );
                        dgware_Hdbanhang_Chitiet.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                        txtSotien.EditValue = gridView4.Columns["Thanhtien"].SummaryItem.SummaryValue;
                    }
                }
                else if (e.Column.FieldName == "Id_Hanghoa_Ban")
                {
                    gridView4.SetFocusedRowCellValue(gridView4.Columns["Id_Donvitinh"]
                        , ((System.Data.DataRowView)gridLookUpEdit_Hanghoa_Ban.GetDataSourceRowByKeyValue(e.Value))["Id_Donvitinh"]);
                    gridView4.SetFocusedRowCellValue(gridView4.Columns["Dongia_Ban"]
                        , ((System.Data.DataRowView)gridLookUpEdit_Hanghoa_Ban.GetDataSourceRowByKeyValue(e.Value))["Dongia_Ban"]);

                    //set giam gia theo ct km
                    if (dsCtrinh_Kmai_Chitiet != null
                        && dsCtrinh_Kmai_Chitiet.Tables.Count > 0
                        && dsCtrinh_Kmai_Chitiet.Tables[0].Rows.Count > 0)
                    {
                        DataRow[] sdr = dsCtrinh_Kmai_Chitiet.Tables[0].Select("Id_Hanghoa_Ban=" + e.Value);
                        if (sdr != null && sdr.Length > 0)
                            gridView4.GetDataRow(gridView4.FocusedRowHandle)["Per_Dongia"] = sdr[0]["Per_Dongia"];
                    }
                    else
                        gridView4.SetFocusedRowCellValue(gridView4.Columns["Per_Dongia"], 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPer_Vat_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPer_Vat.Text != "" && txtSotien.Text != "")
            {
                txtSotien_Vat.EditValue = Convert.ToDecimal(txtSotien.EditValue) * Convert.ToDecimal(txtPer_Vat.EditValue) / 100;
                txtTongtien.EditValue = Convert.ToDecimal(txtSotien.EditValue) + Convert.ToDecimal(txtSotien_Vat.EditValue);
            }
        }

        private void txtSotien_EditValueChanged(object sender, EventArgs e)
        {
            //if (txtPer_Vat.Text != "" && txtSotien.Text != "")
            //{
            //    txtSotien_Vat.EditValue = Convert.ToDecimal(txtSotien.EditValue) * Convert.ToDecimal(txtPer_Vat.EditValue) / 100;
            //    txtTongtien.EditValue = Convert.ToDecimal(txtSotien.EditValue) + Convert.ToDecimal(txtSotien_Vat.EditValue);
            //}
        }

        void DisplayInfo2()
        {
            if ("" + gridView1.GetFocusedRowCellValue("Id_Hdbanhang") == "")
                return;

            if ("" + lookUpEdit_Donvi_Muahang.GetColumnValue("Masothue") != "")
                txtMasothue.EditValue = lookUpEdit_Donvi_Muahang.GetColumnValue("Masothue");
            else
                //txtMasothue.EditValue = ds_Hdbanhang.Tables[0].Rows[gridView1.FocusedRowHandle]["Maso_Thue"];
            txtMasothue.EditValue = gridView1.GetFocusedRowCellValue("Maso_Thue");

            if ("" + lookUpEdit_Donvi_Muahang.GetColumnValue("Diachi_Khachhang") != "")
                txtDiachi.EditValue = lookUpEdit_Donvi_Muahang.GetColumnValue("Diachi_Khachhang");
            else
                txtDiachi.EditValue = gridView1.GetFocusedRowCellValue("Diachi");

            if ("" + lookUpEdit_Donvi_Muahang.GetColumnValue("Dienthoai") != "")
                txtDienthoai.EditValue = lookUpEdit_Donvi_Muahang.GetColumnValue("Dienthoai");
            else
                txtDienthoai.EditValue = gridView1.GetFocusedRowCellValue("Dienthoai");


            this.ds_Hdbanhang_Chitiet = objWareService.Get_All_Ware_Hdbanhang_Chitiet_By_Hdbanhang(gridView1.GetFocusedRowCellValue("Id_Hdbanhang"));
            this.dgware_Hdbanhang_Chitiet.DataSource = ds_Hdbanhang_Chitiet;
            this.dgware_Hdbanhang_Chitiet.DataMember = ds_Hdbanhang_Chitiet.Tables[0].TableName;
            txtSotien.EditValue = gridView4.Columns["Thanhtien"].SummaryText;

            if (ds_Hdbanhang_Chitiet.Tables[0].Rows[0]["Dongia_Bansi"].ToString() == "")
            {
                gridView4.Columns["Dongia_Ban"].Visible = true;
                gridView4.Columns["Dongia_Bansi"].Visible = false;
            }
            else
            {
                gridView4.Columns["Dongia_Ban"].Visible = false;
                gridView4.Columns["Dongia_Bansi"].Visible = true;
            }

            gridView4.BestFitColumns();
        }

        private void lookUpEdit_Khachhang_EditValueChanged(object sender, EventArgs e)
        {
            if ("" + lookUpEdit_Donvi_Muahang.EditValue != "" && lookUpEdit_Donvi_Muahang.EditValue.ToString() != "-1")
            {
                object id_donvi_MH = lookUpEdit_Donvi_Muahang.GetColumnValue("Id_Khachhang");
                DataRow[] dtr = objMasterService.Get_All_Ware_Dm_Khachhang().Tables[0].Select("Id_Khachhang = " + id_donvi_MH); 
              
                if ("" +   dtr[0]["Masothue"] != "")
                    txtMasothue.EditValue =   txtMasothue.EditValue = dtr[0]["Masothue"];
                else
                    txtMasothue.EditValue = "";

                if ("" + dtr[0]["Diachi_Khachhang"] != "")
                    txtDiachi.EditValue = dtr[0]["Diachi_Khachhang"];
                else
                    txtDiachi.EditValue = "";

                if ("" + dtr[0]["Dienthoai"] != "")
                    txtDienthoai.EditValue = dtr[0]["Dienthoai"];
                else
                    txtDienthoai.EditValue = "";
            }
            else
            {
                txtDienthoai.EditValue = ds_Hdbanhang.Tables[0].Rows[gridView1.FocusedRowHandle]["Dienthoai"];
                txtDiachi.EditValue = ds_Hdbanhang.Tables[0].Rows[gridView1.FocusedRowHandle]["Diachi"];
                txtMasothue.EditValue = ds_Hdbanhang.Tables[0].Rows[gridView1.FocusedRowHandle]["Maso_Thue"];
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.DisplayInfo2();
        }

        private void lookUpEdit_Nhansu_Banhang_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lookUpEdit_Cuahang_Ban_EditValueChanged(object sender, EventArgs e)
        {
            object Id_Cuahang_Ban = lookUpEdit_Cuahang_Ban.EditValue;
            if (Id_Cuahang_Ban != null)
            {
                //ds_Hanghoa_Ban = objMasterService.Get_All_Ware_Dm_Hanghoa_Ban_By_Id_Cuahang_Ban(Id_Cuahang_Ban, dtNgay_Chungtu.EditValue);
                ds_Hanghoa_Ban = objMasterService.Get_All_Ware_Dm_Hanghoa_Ban();
                gridLookUpEdit_Hanghoa_Ban.DataSource = ds_Hanghoa_Ban.Tables[0];
                gridLookUpEdit_Ma_Hanghoa_Ban.DataSource = ds_Hanghoa_Ban.Tables[0];
            }
        }

        private void gridLookUpEdit_Hanghoa_Ban_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
                {
                    SunLine.MasterTables.Forms.Ware.Frmware_Dm_Hanghoa_Ban_Dialog frmware_Dm_Hanghoa_Ban_Dialog = new SunLine.MasterTables.Forms.Ware.Frmware_Dm_Hanghoa_Ban_Dialog();
                    frmware_Dm_Hanghoa_Ban_Dialog.Text = "Hàng hóa";
                    GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmware_Dm_Hanghoa_Ban_Dialog);
                    frmware_Dm_Hanghoa_Ban_Dialog.Id_Cuahang_Ban = lookUpEdit_Cuahang_Ban.EditValue;
                    frmware_Dm_Hanghoa_Ban_Dialog.ShowDialog();

                    if (frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows != null
                        && frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows.Length > 0)
                    {
                        //gridLookUpEdit_Hanghoa_Ban.DataSource = frmware_Dm_Hanghoa_Ban_Dialog.Data.Tables[0];

                        gridView4.SetFocusedRowCellValue(gridView4.Columns["Id_Hanghoa_Ban"]
                            , frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[0]["Id_Hanghoa_Ban"]);
                        gridView4.SetFocusedRowCellValue(gridView4.Columns["Id_Donvitinh"]
                            , frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[0]["Id_Donvitinh"]);
                        gridView4.SetFocusedRowCellValue(gridView4.Columns["Dongia_Ban"]
                            , frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[0]["Dongia_Ban"]);

                        //object Id_Cuahang_Ban = gridView1.GetFocusedRowCellValue("Id_Cuahang_Ban");
                        if (frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows.Length > 1)
                        {
                            for (int i = 1; i < frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows.Length; i++)
                            {
                                DataRow nrow = ds_Hdbanhang_Chitiet.Tables[0].NewRow();
                                //nrow["Id_Cuahang_Ban"] = Id_Cuahang_Ban;
                                nrow["Id_Hanghoa_Ban"] = frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[i]["Id_Hanghoa_Ban"];
                                nrow["Id_Donvitinh"] = frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[i]["Id_Donvitinh"];
                                nrow["Dongia_Ban"] = frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[i]["Dongia_Ban"];
                                ds_Hdbanhang_Chitiet.Tables[0].Rows.Add(nrow);
                            }
                        }
                    }

                    Cal_KhuyenMai();
                }
            }
        }

        private void btnImport_From_Donmuahang_Kh_Click(object sender, EventArgs e)
        {
            Frmware_Donmuahang_Kh_Hhban Frmware_Donmuahang_Kh_Hhban = new Frmware_Donmuahang_Kh_Hhban();
            Frmware_Donmuahang_Kh_Hhban.Text = "Đơn đặt hàng";
            GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(Frmware_Donmuahang_Kh_Hhban);
            Frmware_Donmuahang_Kh_Hhban.gridView4.OptionsBehavior.Editable = true;
            Frmware_Donmuahang_Kh_Hhban.ShowDialog();

            if (Frmware_Donmuahang_Kh_Hhban.dsSelected != null)
            {
                if (Frmware_Donmuahang_Kh_Hhban.dsSelected.Tables.Count > 0)
                    foreach (DataRow dr in Frmware_Donmuahang_Kh_Hhban.dsSelected.Tables[0].Rows)
                    {
                        DataRow newrow = ds_Hdbanhang_Chitiet.Tables[0].NewRow();
                        foreach (DataColumn col in ds_Hdbanhang_Chitiet.Tables[0].Columns)
                            try
                            {
                                newrow[col.ColumnName] = dr[col.ColumnName];
                            }
                            catch { continue; }
                        newrow["Dongia_Ban"] = dr["Dongia"];
                        ds_Hdbanhang_Chitiet.Tables[0].Rows.Add(newrow);
                    }
                dgware_Hdbanhang_Chitiet.DataSource = ds_Hdbanhang_Chitiet.Tables[0];
                txtSotien.EditValue = gridView4.Columns["Thanhtien"].SummaryItem.SummaryValue;
            }
        }
        /// <summary>
        /// Tinh khuyen mai
        /// </summary>
        void Cal_KhuyenMai()
        {
            foreach (DataRow dr in ds_Hdbanhang_Chitiet.Tables[0].Rows)
            {
                //set giam gia theo khuyen mai
                if (dsCtrinh_Kmai_Chitiet != null
                && dsCtrinh_Kmai_Chitiet.Tables.Count > 0
                && dsCtrinh_Kmai_Chitiet.Tables[0].Rows.Count > 0)
                {
                    DataRow[] sdr = dsCtrinh_Kmai_Chitiet.Tables[0].Select("Id_Hanghoa_Ban=" + dr["Id_Hanghoa_Ban"]);
                    if (sdr != null && sdr.Length > 0)
                        dr["Per_Dongia"] = Convert.ToDecimal("0" + sdr[0]["Per_Dongia"]); ;

                }

                dr["Thanhtien"] = Convert.ToDecimal(dr["Soluong"]) * Convert.ToDecimal(dr["Dongia_Ban"]) * (1 - Convert.ToDecimal("0" + dr["Per_Dongia"]) / 100);
            }
        }

        private void lookUpEdit_Khachhang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                SunLine.MasterTables.Forms.Ware.Frmware_Dm_Khachhang_Add frmware_Dm_Khachhang_Add = new SunLine.MasterTables.Forms.Ware.Frmware_Dm_Khachhang_Add();
                frmware_Dm_Khachhang_Add.Text = lblDonvi_Muahang.Text;
                GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmware_Dm_Khachhang_Add);
                frmware_Dm_Khachhang_Add.ShowDialog();
                if (frmware_Dm_Khachhang_Add.Id_Khachhang != null)
                    if (frmware_Dm_Khachhang_Add.Id_Khachhang.Length > 0)
                    {
                        lookUpEdit_Donvi_Muahang.EditValue = frmware_Dm_Khachhang_Add.Id_Khachhang[0];
                    }
            }
        }

        private void txtHd_Sochungtu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ("" + txtHd_Sochungtu.EditValue == "")
                    return;

                gridView1.Columns["Ngay_Chungtu"].ClearFilter();
                ds_Hdbanhang = objWareService.Get_All_Ware_Hdbanhang();
                dgware_Hdbanhang.DataSource = ds_Hdbanhang;
                dgware_Hdbanhang.DataMember = ds_Hdbanhang.Tables[0].TableName;

                gridView1.Columns["Sochungtu"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(gridView1.Columns["Sochungtu"], txtHd_Sochungtu.EditValue);
                if (gridView1.RowCount == 0) {
                    txtHd_Sochungtu.EditValue = "";
                    ResetText();
                    ds_Hdbanhang_Chitiet.Clear();
                    return;
                }
                lookUpEdit_Cuahang_Ban2.EditValue = gridView1.GetFocusedRowCellValue("Id_Cuahang_Ban");
                dtNgay_Muahang.EditValue = gridView1.GetFocusedRowCellValue("Ngay_Chungtu");
                this.DataBindingControl();
            }
        }

        private void lookUpEdit_Cuahang_Ban2_EditValueChanged(object sender, EventArgs e)
        {
            if ("" + lookUpEdit_Cuahang_Ban2.EditValue == "")
                return;
            if (dtNgay_Muahang.EditValue == null)
                gridView1.Columns["Ngay_Chungtu"].ClearFilter();

            gridView1.Columns["Sochungtu"].ClearFilter();

            ds_Hdbanhang = objWareService.Get_All_Ware_Hdbanhang_ByCuahang(lookUpEdit_Cuahang_Ban2.EditValue);
            dgware_Hdbanhang.DataSource = ds_Hdbanhang;
            dgware_Hdbanhang.DataMember = ds_Hdbanhang.Tables[0].TableName;
            DataBindingControl();
        }

        private void dtNgay_Muahang_EditValueChanged(object sender, EventArgs e)
        {
            if ("" + dtNgay_Muahang.EditValue != "")
            {
                gridView1.Columns["Sochungtu"].ClearFilter();
                ds_Hdbanhang_Chitiet.Clear();           
                gridView1.Columns["Ngay_Chungtu"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(gridView1.Columns["Ngay_Chungtu"], dtNgay_Muahang.DateTime);
            }
        }
    }
}


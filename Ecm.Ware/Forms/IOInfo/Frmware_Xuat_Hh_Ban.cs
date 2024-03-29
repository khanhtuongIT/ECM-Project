using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GoobizFrame.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace Ecm.Ware.Forms
{
    public partial class Frmware_Xuat_Hh_Ban : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.WareService objWareService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.WareService>();
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        public Ecm.WebReferences.Classes.RexService objRexService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.RexService>();
        DataSet ds_Xkbanhang = new DataSet();
        DataSet ds_Xkbanhang_Chitiet = new DataSet();
        DataSet ds_Hanghoa_Ban;
        DataSet ds_Hanghoa_Dinhgia = new DataSet();
        DataSet dsDonvitinh = new DataSet();
        DataSet dsNhansu;
        object identity;
        object id_nhansu_current;
        DataSet ds_Role_User;

        public Frmware_Xuat_Hh_Ban()
        {
            InitializeComponent();
            //date mask
            this.gridDate_Ngay_Sx.MinValue = new DateTime(2000, 01, 01);
            this.gridDate_Han_Sd.MinValue = new DateTime(2000, 01, 01);
            dtNgay_Chungtu.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtNgay_Chungtu.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();
            //reset lookup edit as delete value
            lookUpEdit_Kho_View.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);
            lookUpEdit_Nhansu_Lap.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);
            //LocationId_Cuahang_Ban = GoobizFrame.Windows.MdiUtils.ThemeSettings.GetLocation("Id_Cuahang_Ban");
            id_nhansu_current = GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu();
            this.item_Query.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.item_Verify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.item_Test.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            item_Add.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //item_Cancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //item_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //item_Edit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //item_Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ds_Role_User = objMasterService.GetRole_System_Name_ById_User(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUserId()).ToDataSet();
            this.DisplayInfo();
        }

        void LoadMasterData()
        {
            ds_Hanghoa_Ban = objMasterService.Get_All_Ware_Dm_Hanghoa_Ban().ToDataSet();
            dsNhansu = objRexService.Get_All_Rex_Nhansu_Collection().ToDataSet();
            ds_Hanghoa_Dinhgia = objWareService.Get_All_Ware_Hanghoa_Ban_Dinhgia().ToDataSet();
            dsDonvitinh = objMasterService.Get_All_Ware_Dm_Donvitinh().ToDataSet();

            gridLookUpEdit_Hanghoa_Ban.DataSource = ds_Hanghoa_Ban.Tables[0];
            gridLookUpEdit_Ma_Hanghoa_Ban.DataSource = ds_Hanghoa_Ban.Tables[0];
            //lookUpEdit_Nhansu_Kk
            lookUpEdit_Nhansu_Lap.Properties.DataSource = dsNhansu.Tables[0];
            gridLookUpEdit_Nhansu_Xuat.DataSource = dsNhansu.Tables[0];
            lookupEdit_Nhansu_Bh.Properties.DataSource = dsNhansu.Tables[0];
            gridLookUpEdit_Donvitinh.DataSource = dsDonvitinh.Tables[0];

            DataSet ds_collection = objMasterService.Get_All_Ware_Dm_Khachhang().ToDataSet();
            lookupEditKhachhang.Properties.DataSource = ds_collection.Tables[0];
            lookupEdit_MaKhachhang.Properties.DataSource = ds_collection.Tables[0];

            ds_collection = objMasterService.GetRole_System_Name_ById_User(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUserId()).ToDataSet();

            DataSet dsCuahang_Ban = objMasterService.Ware_Dm_Cuahang_Ban_Select_Kho().ToDataSet();
            gridLookUpEdit_Cuahang_Ban_Xuat.DataSource = dsCuahang_Ban.Tables[0];
            DataTable temp = dsCuahang_Ban.Tables[0].Copy();
            DataRow row = temp.NewRow();
            row["Id_Cuahang_Ban"] = -1;
            row["Ma_Cuahang_Ban"] = "All";
            row["Ten_Cuahang_Ban"] = "Tất cả";
            temp.Rows.Add(row);
            lookUpEdit_Kho_View.Properties.DataSource = temp;
            lookUpEdit_KhoEdit.Properties.DataSource = temp;
            lookUpEdit_Kho_View.EditValue = -1;
            //if (ds_collection.Tables[0].Rows.Count > 0 &&
            //    "" + ds_collection.Tables[0].Rows[0]["Role_System_Name"] == "Administrators")
            //{
            //    DataTable temp = dsCuahang_Ban.Tables[0].Copy();
            //    DataRow row = temp.NewRow();
            //    row["Id_Cuahang_Ban"] = -1;
            //    row["Ma_Cuahang_Ban"] = "All";
            //    row["Ten_Cuahang_Ban"] = "Tất cả";
            //    temp.Rows.Add(row);
            //    lookUpEdit_Kho_View.Properties.DataSource = temp;
            //    lookUpEdit_KhoEdit.Properties.DataSource = temp;
            //    lookUpEdit_Kho_View.EditValue = -1;
            //}
            //else
            //{
            //    DataSet dsCuahang = objWareService.Get_Ware_Ql_Cuahang_Ban_By_Id_Nhansu(id_nhansu_current, true).ToDataSet();
            //    lookUpEdit_Kho_View.Properties.DataSource = dsCuahang.Tables[0];
            //    lookUpEdit_KhoEdit.Properties.DataSource = dsCuahang.Tables[0];
            //}
            lookUpEditKhuvuc.Properties.DataSource = objMasterService.Ware_Dm_Cuahang_Ban_Select_Sale().ToDataSet().Tables[0];
        }

        void Reload_Chungtu()
        {
            //var id_kho = (Convert.ToInt64() == -1) ? null : lookUpEdit_Kho_View.EditValue;
            //ds_Xkbanhang = objWareService.Get_All_Ware_Xuatkho_Banhang_ByCuahang_ByDate((lookUpEdit_Kho_View.Text == "") ? -1 : lookUpEdit_Kho_View.EditValue, dtNgay_Xuatkho.DateTime).ToDataSet();
            ds_Xkbanhang = objWareService.Get_All_Ware_Xuatkho_Banhang_ByCuahang_ByDate(-1, dtNgay_Xuatkho.DateTime).ToDataSet();
            dgware_Hdbanhang.DataSource = ds_Xkbanhang;
            dgware_Hdbanhang.DataMember = ds_Xkbanhang.Tables[0].TableName;
            lookUpEdit_Sophieu.Properties.DataSource = ds_Xkbanhang.Tables[0];
            this.DataBindingControl();
            this.ChangeStatus(false);
            gridView1.SetRowExpanded(-1, true);
            if (lookUpEdit_Sophieu.EditValue == null)
                gridView1.Columns["Id_Xuatkho_Banhang"].ClearFilter();
            else
                gridView1.Columns["Id_Xuatkho_Banhang"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(gridView1.Columns["Id_Xuatkho_Banhang"], lookUpEdit_Sophieu.EditValue);
            //  DisplayInfo_Details();
        }

        #region Event Override

        public override void DisplayInfo()
        {
            try
            {
                this.dtNgay_Xuatkho.EditValue = DateTime.Now;
                checkEdit_Sendmail.Checked = false;
                lookUpEdit_Sophieu.Properties.DataSource = objWareService.Get_All_Ware_Xuatkho_Banhang_ByCuahang_ByDate(-1, dtNgay_Xuatkho.DateTime).ToDataSet().Tables[0];
                ResetText();
                LoadMasterData();
                Reload_Chungtu();
                DisplayInfo_Details();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }
        }

        void DisplayInfo_Details()
        {
            try
            {
                DataBindingControl();
                identity = gridView1.GetFocusedRowCellValue("Id_Xuatkho_Banhang");
                this.ds_Xkbanhang_Chitiet = objWareService.Get_All_Ware_Xuatkho_Banhang_Chitiet_By_Id_Xuatkho_Banhang(identity).ToDataSet();
                this.dgware_Hdbanhang_Chitiet.DataSource = ds_Xkbanhang_Chitiet;
                this.dgware_Hdbanhang_Chitiet.DataMember = ds_Xkbanhang_Chitiet.Tables[0].TableName;
                gvware_Xuat_Hanghoa_Ban_Chitiet.BestFitColumns();
                txtTongtien_Hang.EditValue = Convert.ToDecimal(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Thanhtien"].SummaryItem.SummaryValue) - Convert.ToDecimal("0" + txtHotro.EditValue);
            }
            catch { }
        }

        public override void ClearDataBindings()
        {
            this.lookUpEdit_KhoEdit.DataBindings.Clear();
            //this.txtSotien.DataBindings.Clear();
            //this.txtSotien_Vat.DataBindings.Clear();
            this.txtTongtien_Hang.DataBindings.Clear();
            //this.txtNguoinhan.DataBindings.Clear();
            this.dtNgay_Chungtu.DataBindings.Clear();
            this.lookUpEdit_Kho_View.DataBindings.Clear();
            this.lookUpEdit_Nhansu_Lap.DataBindings.Clear();
            this.lookupEditKhachhang.DataBindings.Clear();
            lookupEdit_MaKhachhang.DataBindings.Clear();
            lookupEdit_Nhansu_Bh.DataBindings.Clear();
            this.txtGhichu.DataBindings.Clear();
            dtNgaygiao.DataBindings.Clear();
            lookUpEditKhuvuc.DataBindings.Clear();
            dtNgay_Thutien.DataBindings.Clear();
            chkChiphi.DataBindings.Clear();
            txtHotro.DataBindings.Clear();
        }

        public override void DataBindingControl()
        {
            try
            {
                this.ClearDataBindings();
                //this.txtSochungtu.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Sochungtu");
                //this.txtSotien.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Sotien");
                //this.txtSotien_Vat.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Sotien_Vat");
                this.txtTongtien_Hang.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Thanhtien_NotVAT");
                //this.txtNguoinhan.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Nguoinhan");
                this.dtNgay_Chungtu.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Ngay_Chungtu");
                this.lookUpEdit_Kho_View.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Id_Cuahang_Ban");
                this.lookUpEdit_KhoEdit.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Id_Cuahang_Ban");
                this.lookUpEdit_Nhansu_Lap.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Id_Nhansu_Edit");
                this.lookupEdit_Nhansu_Bh.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Id_Nhansu_Bh");
                this.lookupEditKhachhang.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Id_Khachhang");
                lookupEdit_MaKhachhang.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Id_Khachhang");
                this.txtGhichu.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Ghichu_Edit");
                dtNgaygiao.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Ngay_Giaohang");
                dtNgay_Thutien.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Ngay_Thutien");
                lookUpEditKhuvuc.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Id_Khuvuc");
                chkChiphi.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Tinh_Chiphi");
                txtHotro.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".Chiphi_Vanchuyen");

                // lookUpEdit_Sophieu.DataBindings.Add("EditValue", ds_Xkbanhang, ds_Xkbanhang.Tables[0].TableName + ".SoPhieu");
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }
        }

        public override void ChangeStatus(bool editTable)
        {
            //this.dgware_Hdbanhang.Enabled = !editTable;
            this.txtGhichu.Properties.ReadOnly = !editTable;
            //this.lookUpEdit_Kho_View.Properties.ReadOnly = editTable;
            this.dgware_Hdbanhang_Chitiet.EmbeddedNavigator.Enabled = editTable;
            this.gvware_Xuat_Hanghoa_Ban_Chitiet.OptionsBehavior.Editable = editTable;
            lookUpEdit_KhoEdit.Properties.ReadOnly = !editTable;
            lookupEdit_MaKhachhang.Properties.ReadOnly = !editTable;
            dockPanel1_Container.Enabled = !editTable;
            gridColumn_Delete.Visible = editTable;
        }

        public override void ResetText()
        {
            this.lookupEditKhachhang.EditValue = null;
            lookupEdit_MaKhachhang.EditValue = null;
            this.dtNgay_Thutien.EditValue = null;
            this.txtDiachi.EditValue = null;
            lookUpEdit_Nhansu_Lap.EditValue = null;
            dtNgaygiao.EditValue = null;
            lookUpEditKhuvuc.EditValue = null;
            this.txtTongtien_Hang.EditValue = null;
            lookupEdit_Nhansu_Bh.EditValue = null;
            this.lookUpEdit_KhoEdit.EditValue = null;
            this.dtNgay_Chungtu.EditValue = null;
            txtGhichu.EditValue = null;
            this.ds_Xkbanhang_Chitiet = objWareService.Get_All_Ware_Xuatkho_Banhang_Chitiet_By_Id_Xuatkho_Banhang(0).ToDataSet();
            this.dgware_Hdbanhang_Chitiet.DataSource = ds_Xkbanhang_Chitiet.Tables[0];
        }

        public object InsertObject()
        {
            try
            {
                Ecm.WebReferences.WareService.Ware_Xuatkho_Banhang objWare_Xuatkho_Banhang = new Ecm.WebReferences.WareService.Ware_Xuatkho_Banhang();
                objWare_Xuatkho_Banhang.Id_Xuatkho_Banhang = -1;
                //txtSochungtu.EditValue = objWareService.Getnew_Sohoadon_XuatkhoBh(
                //    ((DataRowView)lookUpEdit_Kho_View.Properties.GetDataSourceRowByKeyValue(lookUpEdit_Kho_View.EditValue))["Ma_Cuahang_Ban"]);
                //objWare_Xuatkho_Banhang.Sochungtu = txtSochungtu.EditValue;
                objWare_Xuatkho_Banhang.Sotien = 0;
                objWare_Xuatkho_Banhang.Sotien_Vat = 0;
                objWare_Xuatkho_Banhang.Ngay_Chungtu = dtNgay_Chungtu.EditValue;
                objWare_Xuatkho_Banhang.Ngay_Thanhtoan = dtNgay_Chungtu.EditValue;
                objWare_Xuatkho_Banhang.Nguoinhan = null;
                objWare_Xuatkho_Banhang.Per_Chietkhau = 0;
                objWare_Xuatkho_Banhang.Thanhtien_Chietkhau = 0;

                if ("" + lookUpEdit_Kho_View.EditValue != "")
                    objWare_Xuatkho_Banhang.Id_Cuahang_Ban = lookUpEdit_Kho_View.EditValue;

                if ("" + lookUpEdit_Nhansu_Lap.EditValue != "")
                    objWare_Xuatkho_Banhang.Id_Nhansu_Bh = lookUpEdit_Nhansu_Lap.EditValue;

                objWare_Xuatkho_Banhang.Id_Khachhang = ("" + lookupEditKhachhang.EditValue != "") ? lookupEditKhachhang.EditValue : -1;
                identity = objWareService.Insert_ware_xuatkho_banhang(objWare_Xuatkho_Banhang);

                if (identity != null)
                {
                    dgware_Hdbanhang_Chitiet.EmbeddedNavigator.Buttons.DoClick(dgware_Hdbanhang_Chitiet.EmbeddedNavigator.Buttons.EndEdit);
                    foreach (DataRow dr in ds_Xkbanhang_Chitiet.Tables[0].Rows)
                    {
                        if (dr.RowState != DataRowState.Deleted)
                            dr["Id_Xuatkho_Banhang"] = identity;
                    }
                    objWareService.Update_Ware_Xuatkho_Banhang_Chitiet_Collection(ds_Xkbanhang_Chitiet);
                    ds_Xkbanhang_Chitiet = objWareService.Get_All_Ware_Xuatkho_Banhang_Chitiet_By_Id_Xuatkho_Banhang(identity).ToDataSet();
                    foreach (DataRow row in ds_Xkbanhang_Chitiet.Tables[0].Rows)
                    {
                        objWareService.Insert_Ware_Nhap_Xuat_Chitiet(row["Id_Xuatkho_Banhang_Chitiet"], null, row["Id_Hanghoa_Ban"]);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                return false;
            }
        }

        public object UpdateObject()
        {
            try
            {
                Ecm.WebReferences.WareService.Ware_Xuatkho_Banhang objWare_Xuatkho_Banhang = new Ecm.WebReferences.WareService.Ware_Xuatkho_Banhang();
                objWare_Xuatkho_Banhang.Id_Xuatkho_Banhang = identity;
                objWare_Xuatkho_Banhang.Nguoinhan = null;
                objWare_Xuatkho_Banhang.Ghichu_Edit = txtGhichu.Text;
                objWare_Xuatkho_Banhang.Sotien = Convert.ToDecimal("0" + txtTongtien_Hang.EditValue);
                objWare_Xuatkho_Banhang.Id_Nhansu_Edit = lookUpEdit_Nhansu_Lap.EditValue;
                objWare_Xuatkho_Banhang.Id_Khachhang = lookupEdit_MaKhachhang.EditValue;
                objWare_Xuatkho_Banhang.Id_Cuahang_Ban = lookUpEdit_KhoEdit.EditValue;
                objWareService.Ware_Xuatkho_Banhang_Update_Print(objWare_Xuatkho_Banhang);
                if (identity != null)
                {
                    dgware_Hdbanhang_Chitiet.EmbeddedNavigator.Buttons.DoClick(dgware_Hdbanhang_Chitiet.EmbeddedNavigator.Buttons.EndEdit);
                    foreach (DataRow dr in ds_Xkbanhang_Chitiet.Tables[0].Rows)
                    {
                        if (dr.RowState == DataRowState.Added)
                            dr["Id_Xuatkho_Banhang"] = identity;
                    }
                    DoClickEndEdit(dgware_Hdbanhang_Chitiet);
                    objWareService.Update_Ware_Xuatkho_Banhang_Chitiet_Collection(ds_Xkbanhang_Chitiet);
                }
                if (ds_Xkbanhang_Chitiet.HasChanges())
                {
                    DataRow[] row = ds_Xkbanhang.Tables[0].Select("Id_Xuatkho_Banhang = " + identity);
                    if (row.Length > 0)
                    {
                        DataSet ds_Hdbanhang_Chitiet = objWareService.Get_All_Ware_Hdbanhang_Chitiet_By_Hdbanhang(row[0]["Id_Hdbanhang"]).ToDataSet();
                        foreach (DataRow dtr in ds_Hdbanhang_Chitiet.Tables[0].Rows)
                        {
                            dtr.Delete();
                        }

                        foreach (DataRow dtr in ds_Xkbanhang_Chitiet.Tables[0].Rows)
                        {
                            if (dtr.RowState == DataRowState.Deleted)
                                continue;
                            DataRow row_hdban_chitiet = ds_Hdbanhang_Chitiet.Tables[0].NewRow();
                            row_hdban_chitiet["Id_Hdbanhang"] = row[0]["Id_Hdbanhang"];
                            row_hdban_chitiet["Id_Hanghoa_Ban"] = dtr["Id_Hanghoa_Ban"];
                            row_hdban_chitiet["Id_Donvitinh"] = dtr["Id_Donvitinh"];
                            row_hdban_chitiet["Soluong"] = dtr["Soluong"];
                            row_hdban_chitiet["Dongia_Ban"] = dtr["Dongia_Ban"];
                            row_hdban_chitiet["Thanhtien"] = dtr["Thanhtien"];
                            row_hdban_chitiet["Per_Dongia"] = "0" + dtr["Per_VAT"];
                            ds_Hdbanhang_Chitiet.Tables[0].Rows.Add(row_hdban_chitiet);
                        }
                        objWareService.Update_Ware_Hdbanhang_Chitiet_Collection(ds_Hdbanhang_Chitiet);
                    }
                }
                objWareService.Ware_Xuatkho_Banhang_Chitiet_UpdateSocai(identity); //update sổ cái
                return true;
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                return false;
            }
        }

        public object DeleteObject()
        {
            Ecm.WebReferences.WareService.Ware_Xuatkho_Banhang objWare_Xuatkho_Banhang = new Ecm.WebReferences.WareService.Ware_Xuatkho_Banhang();
            objWare_Xuatkho_Banhang.Id_Xuatkho_Banhang = gridView1.GetFocusedRowCellValue("Id_Xuatkho_Banhang");
            return objWareService.Delete_Ware_Xuatkho_Banhang(objWare_Xuatkho_Banhang.Id_Xuatkho_Banhang);
        }

        public override bool PerformAdd()
        {
            if (Convert.ToInt64(lookUpEdit_Kho_View.EditValue) == -1)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn kho, vui lòng chọn lại");
                lookUpEdit_Kho_View.Focus();
                return false;
            }
            try
            {
                this.ResetText();
                dtNgay_Chungtu.EditValue = objWareService.GetServerDateTime();
                //Kiểm tra nếu nhân viên login không tồn tại trong kho hàng hóa mua thì access denied.
                lookUpEdit_Nhansu_Lap.EditValue = Convert.ToInt64(id_nhansu_current);
                //DataSet ds_Cuahang_Ban = objMasterService.Get_All_Ware_Dm_Cuahang_Ban_By_Id_Nhansu(lookUpEdit_Nhansu_Banhang.EditValue).ToDataSet();
                //this.lookUpEdit_Cuahang_Ban.Properties.DataSource = ds_Cuahang_Ban.Tables[0];
                //if (ds_Cuahang_Ban.Tables[0].Rows.Count > 0)
                //    lookUpEdit_Cuahang_Ban.EditValue = ds_Cuahang_Ban.Tables[0].Rows[0]["Id_Cuahang_Ban"];
                //else
                //{
                //    GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                //    lookUpEdit_Nhansu_Banhang.EditValue = null;
                //    return false;
                //}
                ClearDataBindings();
                this.ChangeStatus(true);
                //txtSochungtu.EditValue = objWareService.GetNew_Sochungtu("ware_xuat_hh_ban", "Sochungtu", lookUpEdit_Kho_View.GetColumnValue("Ma_Cuahang_Ban") + "");
                //DateTime ngay_chungtu = dtNgay_Chungtu.DateTime;
                //ds_Xuat_Hhban = objWareService.Get_All_Ware_Nxt_HhBan(new DateTime(ngay_chungtu.Year, ngay_chungtu.Month, ngay_chungtu.Day, 0, 0, 0)
                //    , new DateTime(ngay_chungtu.Year, ngay_chungtu.Month, ngay_chungtu.Day, 23, 0, 0)
                //    , lookUpEdit_Kho_View.EditValue).ToDataSet();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public override bool PerformEdit()
        {
            try
            {
                if (gridView1.GetFocusedRowCellValue("Id_Xuatkho_Banhang") == null)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn phiếu xuất, vui lòng chọn lại");
                    return false;
                }
                //Ecm.WebReferences.WareService.DocumentProcessStatus DocumentProcessStatus = new Ecm.WebReferences.WareService.DocumentProcessStatus();
                //DocumentProcessStatus.Tablename = "ware_xuat_hh_ban";
                //DocumentProcessStatus.PK_Name = "id_xuat_hh_ban";
                //DocumentProcessStatus.Identity = gridView1.GetFocusedRowCellValue("Id_Xuatkho_Banhang");
                //DocumentProcessStatus = objWareService.Get_DocumentProcessStatus(DocumentProcessStatus);
                //if (objWareService.GetEDocumentProcessStatus((int)DocumentProcessStatus.Doc_Process_Status) != Ecm.WebReferences.WareService.EDocumentProcessStatus.NewDoc)
                //{
                //    GoobizFrame.Windows.Forms.UserMessage.Show("TASK_VERIFIED", new string[] { });
                //    return false;
                //}5
                //ds_Xuat_Hhban = objWareService.Get_All_Ware_Xuat_Hh_Ban();
                if (Convert.ToInt64(gridView1.GetFocusedRowCellValue("Doc_Process_Status")) == 0)
                {
                    lookUpEdit_Nhansu_Lap.EditValue = Convert.ToInt64(id_nhansu_current);
                    this.ChangeStatus(true);
                }
                else
                    if (ds_Role_User.Tables[0].Rows.Count > 0 &&
                    "" + ds_Role_User.Tables[0].Rows[0]["Role_System_Name"] == "Administrators")
                    {
                        //DateTime ngay_chungtu = dtNgay_Chungtu.DateTime;
                        //ds_Xuat_Hhban = objWareService.Get_All_Ware_Nxt_HhBan(new DateTime(ngay_chungtu.Year, ngay_chungtu.Month, ngay_chungtu.Day, 0, 0, 0)
                        //    , new DateTime(ngay_chungtu.Year, ngay_chungtu.Month, ngay_chungtu.Day, 23, 0, 0)
                        //    , lookUpEdit_Kho_View.EditValue).ToDataSet();
                        //DataSet ds_Cuahang_Ban = objMasterService.Get_All_Ware_Dm_Cuahang_Ban_By_Id_Nhansu(lookUpEdit_Nhansu_Nhap.EditValue);
                        //this.lookUpEdit_Cuahang_Ban_Xuat.Properties.DataSource = ds_Cuahang_Ban.Tables[0];
                        //if (ds_Cuahang_Ban.Tables[0].Rows.Count == 0)
                        //{
                        //     GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                        //    lookUpEdit_Nhansu_Nhap.EditValue = null;
                        //    return false;
                        //}
                        lookUpEdit_Nhansu_Lap.EditValue = Convert.ToInt64(id_nhansu_current);
                        this.ChangeStatus(true);
                    }
                    else
                    {
                        GoobizFrame.Windows.Forms.MessageDialog.Show("Phiếu xuất kho đã xuất, nên không thể thao tác.\nVui lòng liên hệ admin");
                        return false;
                    }


            }
            catch (Exception ex)
            {
#if (DEBUG)
                MessageBox.Show(ex.Message);
                return false;
#endif
            }
            return true;
        }

        public override bool PerformCancel()
        {
            this.FormState = GoobizFrame.Windows.Forms.FormState.View;
            this.DisplayInfo();
            return true;
        }

        public override bool PerformSave()
        {
            try
            {
                bool success = false;
                GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(lookUpEdit_KhoEdit, lblKho.Text);
                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;

                //if (gvware_Xuat_Hanghoa_Ban_Chitiet.RowCount == 0)
                //{
                //    GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa nhập hàng hóa, nhập lại");
                //    return false;
                //}
                //else
                //{
                //    hashtableControls.Clear();
                //    hashtableControls.Add(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Id_Hanghoa_Ban"], "");
                //    hashtableControls.Add(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Soluong"], "");
                //    if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gvware_Xuat_Hanghoa_Ban_Chitiet))
                //        return false;
                //}
                //try
                //{
                //    ds_Xkbanhang_Chitiet.Tables[0].Constraints.Clear();
                //    Constraint constraint = new UniqueConstraint("constraint1",
                //            new DataColumn[] {ds_Xkbanhang_Chitiet.Tables[0].Columns["Id_Hanghoa_Ban"],
                //            ds_Xkbanhang_Chitiet.Tables[0].Columns["Id_Donvitinh"] }, false);
                //    ds_Xkbanhang_Chitiet.Tables[0].Constraints.Add(constraint);
                //}
                //catch (Exception ex)
                //{
                //    if (ex.ToString().IndexOf("These columns don't currently have unique values") != -1)
                //    {
                //        GoobizFrame.Windows.Forms.MessageDialog.Show("Tên hàng hóa và đơn vị tính đã tồn tại, vui lòng nhập lại ");
                //        return false;
                //    }
                //    MessageBox.Show(ex.ToString());
                //}
                DoClickEndEdit(dgware_Hdbanhang_Chitiet);
                txtTongtien_Hang.EditValue = gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Thanhtien"].SummaryText;

                success = (bool)this.UpdateObject();
                if (success)
                {
                    this.DisplayInfo();
                }
                return success;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
                return false;
            }
        }

        public override bool PerformDelete()
        {
            try
            {
                if (gridView1.GetFocusedRowCellValue("Id_Xuatkho_Banhang") == null)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn phiếu xuất, vui lòng chọn lại");
                    return false;
                }
                //Ecm.WebReferences.WareService.DocumentProcessStatus DocumentProcessStatus = new Ecm.WebReferences.WareService.DocumentProcessStatus();
                //DocumentProcessStatus.Tablename = "ware_xuat_hh_ban";
                //DocumentProcessStatus.PK_Name = "id_xuat_hh_ban";
                //DocumentProcessStatus.Identity = gridView1.GetFocusedRowCellValue("Id_Xuatkho_Banhang");
                //DocumentProcessStatus = objWareService.Get_DocumentProcessStatus(DocumentProcessStatus);
                //if (objWareService.GetEDocumentProcessStatus((int)DocumentProcessStatus.Doc_Process_Status) != Ecm.WebReferences.WareService.EDocumentProcessStatus.NewDoc)
                //{
                //    GoobizFrame.Windows.Forms.UserMessage.Show("TASK_VERIFIED", new string[] { });
                //    return false;
                //}
                //if ( GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUser().ToUpper() != "ADMIN")
                //if (!id_nhansu_current.Equals("" + lookUpEdit_Nhansu_Lap.EditValue))
                //{
                //    GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                //    return false;
                //}
                if (ds_Role_User.Tables[0].Rows.Count > 0 &&
                   "" + ds_Role_User.Tables[0].Rows[0]["Role_System_Name"] == "Administrators")
                {
                    if (MessageBox.Show("Xóa phiếu xuất và đơn hàng của phiếu xuất này?", "Confirm Dialog", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                }
                else
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                    return true;
                }
            }
            catch (Exception ex)
            {
#if (DEBUG)
                MessageBox.Show(ex.Message);
                return false;
#endif
            }
            return base.PerformDelete();
        }

        public override object PerformSelectOneObject()
        {
            //            Ecm.WebReferences.WareService.Ware_Xuatkho_Banhang ware_xuatkho_banhang = new Ecm.WebReferences.WareService.Ware_Xuatkho_Banhang();
            //            try
            //            {
            //                int focusedRow = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
            //                DataRow dr = ds_Xkbanhang.Tables[0].Rows[focusedRow];
            //                if (dr != null)
            //                {
            //                    ware_xuatkho_banhang.Id_Xuatkho_Banhang = dr["Id_Xuatkho_Banhang"];
            //                    ware_xuatkho_banhang.Sochungtu = dr["Sochungtu"];
            //                    ware_xuatkho_banhang.Ngay_Chungtu_Xuat = dr["Ngay_Chungtu_Xuat"];
            //                    ware_xuatkho_banhang.Id_Cuahang_Ban_Xuat = dr["Id_Cuahang_Ban_Xuat"];
            //                    ware_xuatkho_banhang.Id_Nhansu_Xuat = dr["Id_Nhansu_Xuat"];
            //                    ware_xuatkho_banhang.Ghichu = dr["Ghichu"];
            //                }
            //                this.Dispose();
            //                this.Close();
            //                return ware_xuatkho_banhang;
            //            }
            //            catch (Exception ex)
            //            {
            //#if DEBUG
            //                MessageBox.Show(ex.Message);
            //#endif
            return null;
            //            }
        }

        public override bool PerformPrintPreview()
        {
            if (gridView1.GetFocusedRowCellValue("Id_Xuatkho_Banhang") == null)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn phiếu xuất, vui lòng chọn lại");
                return false;
            }
            //if (MessageBox.Show("Bạn có muốn in phiếu xuất kho này?", "Confirm Dialog", MessageBoxButtons.YesNo) == DialogResult.No)
            //    return false;
            //if (Convert.ToInt64(gridView1.GetFocusedRowCellValue("Doc_Process_Status")) == 0 || ds_Role_User.Tables[0].Rows.Count > 0 &&
            //   "" + ds_Role_User.Tables[0].Rows[0]["Role_System_Name"] == "Administrators" && Convert.ToInt64(gridView1.GetFocusedRowCellValue("Doc_Process_Status")) > 0)
            //{
            try
            {
                DataSets.DsHdbanhang_Xuatkho dsWare_Xuat_Vattu = new Ecm.Ware.DataSets.DsHdbanhang_Xuatkho();
                Reports.rptXuat_Vattu rptXuat_Vattu = new Reports.rptXuat_Vattu();
                GoobizFrame.Windows.Forms.FrmPrintPreview frmPrintPreview = new GoobizFrame.Windows.Forms.FrmPrintPreview();
                frmPrintPreview.Report = rptXuat_Vattu;
                rptXuat_Vattu.DataSource = dsWare_Xuat_Vattu;

                rptXuat_Vattu.xrTableCell_Ngay.Text = DateTime.Now.Day.ToString();
                rptXuat_Vattu.xrTableCell_Thang.Text = DateTime.Now.Month.ToString();
                rptXuat_Vattu.xrTableCell_Nam.Text = DateTime.Now.Year.ToString();

                decimal thanhtien = Convert.ToDecimal("0" + ds_Xkbanhang_Chitiet.Tables[0].Compute("Sum(Thanhtien)", ""));
                decimal thanhtien_CK = Convert.ToDecimal("0" + ds_Xkbanhang_Chitiet.Tables[0].Compute("Sum(Thanhtien_VAT)", ""));
                decimal Sotien_Congno = 0;
                if (gridView1.GetFocusedRowCellValue("Sotien_Congno_Print").ToString().Contains("-"))
                {
                    Sotien_Congno = Convert.ToDecimal(gridView1.GetFocusedRowCellValue("Sotien_Congno_Print"));
                }
                else
                    Sotien_Congno = Convert.ToDecimal("0" + gridView1.GetFocusedRowCellValue("Sotien_Congno_Print"));
                decimal tongtien = thanhtien + Sotien_Congno - thanhtien_CK;

                rptXuat_Vattu.xrTableCell_CK.Text = string.Format("{0:#,#}", thanhtien_CK);
                rptXuat_Vattu.xrcTableCell_SoPhieu.Text = gridView1.GetFocusedRowCellValue("Sochungtu").ToString();
                rptXuat_Vattu.xrTableCell_Phieucu.Text = string.Format("{0:#,#}", Sotien_Congno);
                rptXuat_Vattu.xrTableCell_Nguoinhan.Text = lookupEditKhachhang.Text;
                if (lookupEditKhachhang.GetColumnValue("Diachi").ToString() != "")
                    rptXuat_Vattu.xrTableCell_Nguoinhan.Text += " - " + lookupEditKhachhang.GetColumnValue("Diachi").ToString();
                if (lookupEditKhachhang.GetColumnValue("Dienthoai").ToString() != "")
                    rptXuat_Vattu.xrTableCell_Nguoinhan.Text += " - " + lookupEditKhachhang.GetColumnValue("Dienthoai").ToString();

                rptXuat_Vattu.xrTableCell_Sale.Text = lookupEdit_Nhansu_Bh.Text;
                if (lookupEdit_Nhansu_Bh.GetColumnValue("Dienthoai").ToString() != "")
                    rptXuat_Vattu.xrTableCell_Sale.Text += " - " + lookupEdit_Nhansu_Bh.GetColumnValue("Dienthoai").ToString();
                rptXuat_Vattu.xrTableCell_Tongtien.Text = string.Format("{0:#,#}", tongtien);
                //Ware_Xuat_Vattu_Chitiet
                for (int i = 0; i < gvware_Xuat_Hanghoa_Ban_Chitiet.RowCount; i++)
                {
                    DataRow rWare_Xuat_Vattu_Chitiet = dsWare_Xuat_Vattu.Tables[0].NewRow();
                    rWare_Xuat_Vattu_Chitiet["stt"] = i + 1;
                    //rWare_Xuat_Vattu_Chitiet["id_xuat_hh_mua"] = gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, "Id_Xuatkho_Banhang");
                    //rWare_Xuat_Vattu_Chitiet["id_xuat_hh_mua"] = identity;
                    rWare_Xuat_Vattu_Chitiet["Ma_Hanghoa_Ban"] = gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, gridColumn7);
                    rWare_Xuat_Vattu_Chitiet["Ten_Hanghoa_Ban"] = gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, gridColumn14);
                    rWare_Xuat_Vattu_Chitiet["Ten_Donvitinh"] = gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, "Id_Donvitinh");
                    rWare_Xuat_Vattu_Chitiet["Soluong"] = Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, "Soluong"));
                    rWare_Xuat_Vattu_Chitiet["Dongia_Ban"] = Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, "Dongia_Ban"));
                    rWare_Xuat_Vattu_Chitiet["Thanhtien"] = Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, "Thanhtien"));
                    rWare_Xuat_Vattu_Chitiet["Per_VAT"] = Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, "Per_VAT"));
                    rWare_Xuat_Vattu_Chitiet["DVT_Quydoi"] = gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, "DVT_Quydoi");
                    dsWare_Xuat_Vattu.Tables[0].Rows.Add(rWare_Xuat_Vattu_Chitiet);
                }
                dsWare_Xuat_Vattu.AcceptChanges();
                #region Set he so ctrinh - logo, ten cty
                DataSet dsHeso_Chuongtrinh;
                using (dsHeso_Chuongtrinh = objMasterService.Get_Rex_Dm_Heso_Chuongtrinh_Collection3().ToDataSet())
                {
                    DataSet dsCompany_Paras = new DataSet();
                    dsCompany_Paras.Tables.Add("Company_Paras");
                    dsCompany_Paras.Tables[0].Columns.Add("CompanyName", typeof(string));
                    dsCompany_Paras.Tables[0].Columns.Add("CompanyAddress", typeof(string));
                    dsCompany_Paras.Tables[0].Columns.Add("CompanyTel", typeof(string));

                    dsCompany_Paras.Tables[0].Rows.Add(new object[]  {    
                    dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'","CompanyName"))[0]["Heso"]
                    ,dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'","CompanyAddress"))[0]["Heso"]
                    ,dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'","CompanyTel"))[0]["Heso"]
                });

                    rptXuat_Vattu.xrc_CompanyName.DataBindings.Add(
                        new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyName"));
                    rptXuat_Vattu.xrc_Address.DataBindings.Add(
                    new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyAddress"));
                    rptXuat_Vattu.xrc_Phone.DataBindings.Add(
                    new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyTel"));
                }
                #endregion

                double thanhtien_text = Convert.ToDouble(tongtien);
                string str = GoobizFrame.Windows.HelperClasses.ReadNumber.ChangeNum2VNStr(thanhtien_text, " đồng.");
                str = str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
                rptXuat_Vattu.xrTableCell_Sotien_Chu.Text = str;
                rptXuat_Vattu.CreateDocument();
                GoobizFrame.Windows.Forms.ReportOptions oReportOptions = GoobizFrame.Windows.Forms.ReportOptions.GetReportOptions(rptXuat_Vattu);
                if (Convert.ToBoolean(oReportOptions.PrintPreview))
                {
                    frmPrintPreview.Text = "In Phiếu xuất Kho";//oReportOptions.Caption;
                    frmPrintPreview.printControl1.PrintingSystem = rptXuat_Vattu.PrintingSystem;
                    frmPrintPreview.MdiParent = this.MdiParent;
                    frmPrintPreview.Show();
                    frmPrintPreview.Activate();
                    if (Convert.ToInt64(gridView1.GetFocusedRowCellValue("Doc_Process_Status")) < 2)
                        objWareService.Ware_Xuatkho_Banhang_Update_Doc(gridView1.GetFocusedRowCellValue("Id_Xuatkho_Banhang"), 2, id_nhansu_current);

                    if (checkEdit_Sendmail.Checked)
                    {
                        Ecm.WebReferences.RexService.Rex_Nhansu nhansu = objRexService.Get_Rex_Nhansu_ById(lookupEdit_Nhansu_Bh.EditValue);
                        string emailTo = "" + nhansu.Email;
                        if ("" + emailTo == "")
                            return false;

                        string smtpAddress = dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "Mail_Server"))[0]["Heso"].ToString(); //"smtp.longthanhmekong.com";
                        int portNumber = Convert.ToInt32(dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "Port"))[0]["Heso"]); //25;
                        bool enableSSL = Convert.ToBoolean(dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "SSL"))[0]["Heso"]);// false;
                        string emailFrom = dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "Email_Server"))[0]["Heso"].ToString(); //"info@longthanhmekong.com";
                        string password = dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "Password_Email"))[0]["Heso"].ToString(); ///"leminhlong18072007";

                        string subject = "Đã giao Đơn hàng " + gridView1.GetFocusedRowCellValue("Sochungtu").ToString();
                        string body = "ĐÃ GIAO ĐƠN HÀNG " + gridView1.GetFocusedRowCellValue("Sochungtu").ToString().ToUpper();
                        body += "<br/><br/>Khách hàng: " + lookupEditKhachhang.Text + " - " + lookUpEditKhuvuc.Text;
                        body += "<br/><br/>Địa chỉ: " + txtDiachi.Text;
                        body += "<br/>Ngày giao: " + dtNgaygiao.Text;
                        body += "<br/>Ghi chú: " + txtGhichu.Text;
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(emailFrom);
                            mail.To.Add(emailTo);
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true;
                            // Can set to false, if you are sending pure text.

                            //mail.Attachments.Add(new Attachment("D:\\donhang\\" + txtSochungtu.Text + ".pdf"));
                            //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                            {
                                smtp.Credentials = new NetworkCredential("info", password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                            }
                        }
                    }
                }
                else
                {
                    var reportPrintTool = new DevExpress.XtraReports.UI.ReportPrintTool(rptXuat_Vattu);
                    reportPrintTool.Print();
                }
                DisplayInfo();
            }
            catch (Exception ex)
            { ex.ToString(); }
            return true;
            //}
            //else
            //{
            //    GoobizFrame.Windows.Forms.MessageDialog.Show("Phiếu xuất kho đã xuất, nên không thể thao tác.\nVui lòng liên hệ admin");
            //    return false;
            //}
        }

        #endregion

        #region Even

        private void gridLookUpEdit_Hanghoa_Ban_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
                {
                    Ecm.MasterTables.Forms.Ware.Frmware_Dm_Hanghoa_Ban_Dialog frmware_Dm_Hanghoa_Ban_Dialog = new Ecm.MasterTables.Forms.Ware.Frmware_Dm_Hanghoa_Ban_Dialog();
                    frmware_Dm_Hanghoa_Ban_Dialog.Text = "Hàng hóa";
                    GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmware_Dm_Hanghoa_Ban_Dialog);
                    frmware_Dm_Hanghoa_Ban_Dialog.ShowDialog();

                    if (frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows != null
                        && frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows.Length > 0)
                    {
                        gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Id_Hanghoa_Ban"]
                            , frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[0]["Id_Hanghoa_Ban"]);
                        gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Id_Donvitinh"]
                            , frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[0]["Id_Donvitinh"]);
                        gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Dongia_Ban"]
                            , frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[0]["Dongia_Ban"]);
                        //object Id_Cuahang_Ban = gridView1.GetFocusedRowCellValue("Id_Cuahang_Ban");
                        if (frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows.Length > 1)
                        {
                            for (int i = 1; i < frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows.Length; i++)
                            {
                                DataRow nrow = ds_Xkbanhang_Chitiet.Tables[0].NewRow();
                                //nrow["Id_Cuahang_Ban"] = Id_Cuahang_Ban;
                                nrow["Id_Hanghoa_Ban"] = frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[i]["Id_Hanghoa_Ban"];
                                nrow["Id_Donvitinh"] = frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[i]["Id_Donvitinh"];
                                nrow["Dongia"] = frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[i]["Dongia_Ban"];
                                ds_Xkbanhang_Chitiet.Tables[0].Rows.Add(nrow);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
#if (DEBUG)
                MessageBox.Show(ex.Message);
#endif
            }
        }

        public decimal Get_Soluong_Ton_Quydoi(object Id_Hanghoa_Ban, object Id_Donvitinh, object Id_Xuatkho_Banhang_Chitiet)
        {
            try
            {
                var _Id_Donvitinh = ds_Hanghoa_Ban.Tables[0].Select(string.Format("Id_Hanghoa_Ban={0}", Id_Hanghoa_Ban))[0]["Id_Donvitinh"];
                DataSet ds_hh_nxt = Get_Soluong_Ton_Quydoi(lookUpEdit_Kho_View.EditValue, Id_Hanghoa_Ban, Id_Donvitinh, ("" + Id_Xuatkho_Banhang_Chitiet == "") ? null : Id_Xuatkho_Banhang_Chitiet);
                decimal soluong_ton_quydoi = 0;
                soluong_ton_quydoi = ("" + Id_Donvitinh == "" + _Id_Donvitinh)
                ? Convert.ToDecimal("0" + ds_hh_nxt.Tables[0].Compute("sum(Soluong_Ton_Qdoi)",
                        string.Format("Id_Hanghoa_Ban={0}", Id_Hanghoa_Ban)))
                : Convert.ToDecimal("0" + ds_hh_nxt.Tables[0].Compute("sum(Soluong_Ton)",
                        string.Format("Id_Hanghoa_Ban={0} and Id_Donvitinh={1}", Id_Hanghoa_Ban, Id_Donvitinh)));
                return soluong_ton_quydoi;
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.TrayMessage.TrayMessage.Status = new GoobizFrame.Windows.TrayMessage.TrayMessageInfo(MessageBoxIcon.Asterisk, ex.Message, ex.ToString());
                return 0;
            }
        }

        public DataSet Get_Soluong_Ton_Quydoi(object Id_Cuahang_Ban, object Id_Hanghoa_Ban, object Id_Donvitinh, object Id_Xuatkho_Banhang_Chitiet)
        {
            try
            {
                DateTime current_date = objWareService.GetServerDateTime();
                int today = 1;
                if (current_date.Day == 1)
                    today = 1;
                else
                    today = current_date.Day - 1;

                return objWareService.Rptware_Nxt_Hhban_Qdoi_Id_Xuat_Chitiet(new DateTime(current_date.Year, current_date.Month, today, 0, 0, 0),
                                                                current_date, Id_Cuahang_Ban, Id_Hanghoa_Ban, Id_Donvitinh, ("" + Id_Xuatkho_Banhang_Chitiet == "") ? null : Id_Xuatkho_Banhang_Chitiet).ToDataSet();
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.TrayMessage.TrayMessage.Status = new GoobizFrame.Windows.TrayMessage.TrayMessageInfo(MessageBoxIcon.Asterisk, ex.Message, ex.ToString());
                return null;
            }
        }

        private void gridLookUpEdit_Donvitinh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
                {
                    Ecm.MasterTables.Forms.Ware.Frmware_Dm_Donvitinh_Add frm_Donvitinh = new Ecm.MasterTables.Forms.Ware.Frmware_Dm_Donvitinh_Add();
                    if (gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Id_Hanghoa_Ban"]).ToString() == "")
                        return;
                    frm_Donvitinh.setId_Hanghoa_Ban(gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Id_Hanghoa_Ban"]));
                    frm_Donvitinh.item_Select.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    frm_Donvitinh.item_Refresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    frm_Donvitinh.ShowDialog();
                    if (frm_Donvitinh.SelecteWare_Dm_Donvitinh != null)
                        gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Id_Donvitinh"], frm_Donvitinh.SelecteWare_Dm_Donvitinh.Id_Donvitinh);

                    int soluong = Convert.ToInt32(gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Soluong"]));
                    if (Get_Soluong_Ton_Quydoi(gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Id_Hanghoa_Ban"])
                                                , gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Id_Donvitinh"])
                                                , gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Id_Xuatkho_Banhang_Chitiet"])) < soluong)
                    {
                        GoobizFrame.Windows.Forms.MessageDialog.Show("Không đủ số lượng để xuất theo yêu cầu");
                        gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Soluong"], soluong);
                        return;
                    }
                    gvware_Xuat_Hanghoa_Ban_Chitiet.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
#if (DEBUG)
                MessageBox.Show(ex.Message);
#endif
            }
        }

        private void gridView5_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedDataRow() == null) return;

                object id_khuvuc = lookUpEditKhuvuc.EditValue;
                switch (e.Column.FieldName)
                {
                    case "Id_Hanghoa_Ban":
                        var _Id_Hanghoa_Ban = ds_Hanghoa_Ban.Tables[0].Select(string.Format("Id_Hanghoa_Ban={0}",
                               gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Hanghoa_Ban")))[0]["Id_Hanghoa_Ban"];
                        var _Id_Donvitinh = ds_Hanghoa_Ban.Tables[0].Select(string.Format("Id_Hanghoa_Ban={0}",
                                gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Hanghoa_Ban")))[0]["Id_Donvitinh"];
                        gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue("Id_Donvitinh", _Id_Donvitinh);

                        DataSet dsDinhgia_ById_Hanghoa = objWareService.Get_All_Ware_Hanghoa_Dinhgia_By_Id_HhBan(_Id_Hanghoa_Ban).ToDataSet();
                        DataRow[] row = dsDinhgia_ById_Hanghoa.Tables[0].Select();
                        if (row != null && row.Length > 0)
                        {
                            gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Id_Donvitinh"], row[0]["Id_Donvitinh"]);
                            DataSet dsHanghoa_Dinhgia_Khuvuc = objWareService.Get_All_Ware_Hanghoa_Dinhgia_Khuvuc(row[0]["Id_Hanghoa_Dinhgia"]).ToDataSet();
                            if (dsHanghoa_Dinhgia_Khuvuc.Tables[0].Rows.Count > 0 && id_khuvuc != null && id_khuvuc + "" != "")
                            {
                                DataRow[] dtr_gia_KV = dsHanghoa_Dinhgia_Khuvuc.Tables[0].Select("Id_Khuvuc = " + id_khuvuc);
                                if (dtr_gia_KV != null && dtr_gia_KV.Length > 0)
                                    gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Dongia_Ban"], dtr_gia_KV[0]["Dongia_Ban"]);
                            }
                            else
                            {
                                //DataRow[] dtr = ds_Hanghoa_Ban_after_Thongke.Tables[0].Select("Id_Hanghoa_Ban = " + gvHdbang_Chitiet.GetFocusedRowCellValue("Id_Hanghoa_Ban"));
                                //if (dtr == null || dtr.Length == 0)
                                //{
                                //    //lblStatus_Bar.Text = "Hàng hóa chưa được định giá";
                                //    gvHdbang_Chitiet.CancelUpdateCurrentRow();
                                //    return;
                                //}                        
                                gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Dongia_Ban"], row[0]["Dongia_Banle"]);
                                //set giam gia theo ct km
                                //gvHdbang_Chitiet.SetFocusedRowCellValue(gvHdbang_Chitiet.Columns["Per_Dongia"], 0);
                            }
                        }
                        break;
                    case "Id_Donvitinh":
                        var soluong_ton = this.Get_Soluong_Ton_Quydoi(
                                gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Hanghoa_Ban"),
                                gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Donvitinh"),
                                gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Xuatkho_Banhang_Chitiet"));
                        gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue("Soluong", soluong_ton);

                        DataRow[] dtr = ds_Hanghoa_Dinhgia.Tables[0].Select("Id_Hanghoa_Ban = " + gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Hanghoa_Ban")
                                              + "and Id_Donvitinh = " + gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Donvitinh"));
                        if (dtr.Length != 0 && dtr[0]["Dongia_Ban"].ToString() != "")
                            gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Dongia"], dtr[0]["Dongia_Ban"]);
                        else
                            gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Dongia"], null);
                        break;
                    case "Soluong":
                    case "Per_VAT":
                    case "Dongia_Ban":
                        decimal soluong = 0;
                        decimal dongia = 0;
                        decimal thanhtien = 0;
                        soluong_ton = this.Get_Soluong_Ton_Quydoi(
                               gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Hanghoa_Ban"),
                               gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Donvitinh"),
                               gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Xuatkho_Banhang_Chitiet"));
                        soluong = Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Soluong"));
                        dongia = Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Dongia_Ban"));
                        if (soluong_ton < soluong)
                        {
                            GoobizFrame.Windows.Forms.MessageDialog.Show("Không đủ số lượng để xuất theo yêu cầu");
                            gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Soluong"], soluong_ton);
                            return;
                        }
                        if ((thanhtien * dongia).ToString().Length > 16)
                        {
                            GoobizFrame.Windows.Forms.MessageDialog.Show("Giá trị thành tiền vượt quá khả năng lưu trữ.");
                            gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Dongia"], null);
                            gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Soluong"], null);
                            return;
                        }
                        decimal Per_Dongia = Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Per_VAT"));
                        gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue("Thanhtien",
                                        soluong * Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Dongia_Ban")) * (1 - Per_Dongia / 100));
                        break;
                    case "Thanhtien":
                        gvware_Xuat_Hanghoa_Ban_Chitiet.UpdateTotalSummary();
                        //txtSotien.Text = gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Thanhtien"].SummaryText;
                        DoClickEndEdit(dgware_Hdbanhang_Chitiet);
                        txtTongtien_Hang.EditValue = gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Thanhtien"].SummaryText;
                        break;
                }
                gvware_Xuat_Hanghoa_Ban_Chitiet.BestFitColumns();
                txtTongtien_Hang.EditValue = Convert.ToDecimal(gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Thanhtien"].SummaryItem.SummaryValue) - Convert.ToDecimal("0" + txtHotro.EditValue);
            }
            catch (Exception ex)
            {
#if (DEBUG)
                MessageBox.Show(ex.Message);
#endif
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ClearDataBindings();
            if (gridView1.FocusedRowHandle >= 0)
                DisplayInfo_Details();
            else
                ResetText();
        }

        private void lookupEditKhachhang_EditValueChanged(object sender, EventArgs e)
        {
            if (lookupEditKhachhang.EditValue != null)
                this.txtDiachi.EditValue = lookupEditKhachhang.GetColumnValue("Diachi_Khachhang");
            else
                this.txtDiachi.EditValue = null;
        }

        private void btnDonhang_Click(object sender, EventArgs e)
        {
            if (lookupEditKhachhang.EditValue == null)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn Khách hàng, vui lòng chọn lại");
                return;
            }
            Frmware_Hdbanhang_noVAT_Hhban frmDonhang = new Frmware_Hdbanhang_noVAT_Hhban(lookupEditKhachhang.EditValue);
            frmDonhang.item_Select.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            frmDonhang.gridColumnChon.Visible = true;
            frmDonhang.gridColumnChon.VisibleIndex = 0;
            frmDonhang.ShowDialog();
            if (frmDonhang._selectedRows != null && frmDonhang._selectedRows.Length > 0)
            {
                Fill_Dondathang(frmDonhang._selectedRows);
                txtTongtien_Hang.EditValue = gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Thanhtien"].SummaryText;
            }
        }

        private void Fill_Dondathang(DataRow[] sdrware_DonDatHang_Chitiet)
        {
            try
            {
                foreach (DataRow dtr in sdrware_DonDatHang_Chitiet)
                {
                    // Add nhap vattu chi tiet 
                    DataRow rNha_Chitiet = ds_Xkbanhang_Chitiet.Tables[0].NewRow();
                    rNha_Chitiet["Id_Hanghoa_Ban"] = dtr["Id_Hanghoa_Ban"];
                    rNha_Chitiet["Id_Donvitinh"] = dtr["Id_Donvitinh"];
                    rNha_Chitiet["Barcode_Txt"] = dtr["Barcode_Txt"];
                    rNha_Chitiet["Soluong"] = dtr["Soluong"];
                    //rNha_Chitiet["Ten_Mathang"] = dtr["Ten_Mathang"];
                    //rNha_Chitiet["Ten_Donvitinh"] = dtr["Ten_Donvitinh"];
                    rNha_Chitiet["Dongia_Ban"] = dtr["Dongia_Ban"];
                    rNha_Chitiet["Thanhtien"] = dtr["Thanhtien"];
                    ds_Xkbanhang_Chitiet.Tables[0].Rows.Add(rNha_Chitiet);
                }
            }
            catch (Exception ex)
            { ex.ToString(); }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Reload_Chungtu();
        }

        #endregion

        private void lookUpEdit_Sophieu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                lookUpEdit_Sophieu.EditValue = null;
            }
        }

        private void lookupEdit_MaKhachhang_EditValueChanged(object sender, EventArgs e)
        {
            lookupEditKhachhang.EditValue = lookupEdit_MaKhachhang.EditValue;
        }

        private void lookUpEdit_KhoEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (this.FormState != GoobizFrame.Windows.Forms.FormState.View && lookUpEdit_KhoEdit.EditValue != null)
            {
                DataSet dsTonkho;
                DateTime current_date = objWareService.GetServerDateTime();
                int today = 1;
                if (current_date.Day == 1)
                    today = 1;
                else
                    today = current_date.Day - 1;
                bool check = false;
                decimal sl_ton = 0;
                foreach (DataRow row in ds_Xkbanhang_Chitiet.Tables[0].Rows)
                {
                    if ("" + row["Id_Hanghoa_Ban"] == "" || "" + row["Id_Donvitinh"] == "") continue;
                    dsTonkho = objWareService.Rptware_Nxt_Hhban_Qdoi(new DateTime(current_date.Year, current_date.Month, today, 0, 0, 0),
                                                                            current_date, lookUpEdit_KhoEdit.EditValue, row["Id_Hanghoa_Ban"], row["Id_Donvitinh"]).ToDataSet();
                    if (dsTonkho.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToDecimal("0" + dsTonkho.Tables[0].Rows[0]["Soluong_Ton"]) >= Convert.ToDecimal("0" + row["Soluong"]))
                            check = true;
                        else
                        {
                            sl_ton = Convert.ToDecimal("0" + dsTonkho.Tables[0].Rows[0]["Soluong_Ton"]);
                            check = false;
                        }
                    }
                    else
                        check = false;
                    if (!check)
                    {
                        GoobizFrame.Windows.Forms.MessageDialog.Show(row["Ten_Hanghoa_Ban"].ToString() + " không đủ số lượng xuất\nSố lượng tồn kho là: " + sl_ton.ToString() + "\nVui lòng kiểm tra lại");
                        lookUpEdit_KhoEdit.EditValue = null;
                        return;
                    }
                }
            }
        }

        private void btnAdd_Xe_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue("Id_Xuatkho_Banhang") == null)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn phiếu xuất, vui lòng chọn lại");
                return;
            }
            Frmware_Dieuxe_Dialog _Frmware_Dieuxe_Dialog = new Frmware_Dieuxe_Dialog(gridView1.GetFocusedRowCellValue("Id_Xuatkho_Banhang"), gridView1.GetFocusedRowCellValue("Sochungtu").ToString());
            _Frmware_Dieuxe_Dialog.ShowDialog();
        }

        private void gvware_Xuat_Hanghoa_Ban_Chitiet_KeyDown(object sender, KeyEventArgs e)
        {
            if (gvware_Xuat_Hanghoa_Ban_Chitiet.FocusedColumn.VisibleIndex == gvware_Xuat_Hanghoa_Ban_Chitiet.VisibleColumns.Count - 1
             && "" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Hanghoa_Ban") != "")
                gvware_Xuat_Hanghoa_Ban_Chitiet.AddNewRow();
        }

        private void gridButtonEdit_Delete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
                gvware_Xuat_Hanghoa_Ban_Chitiet.DeleteRow(gvware_Xuat_Hanghoa_Ban_Chitiet.FocusedRowHandle);
        }

    }
}
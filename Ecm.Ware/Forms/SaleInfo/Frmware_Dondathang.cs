using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GoobizFrame.Windows.Forms;

namespace Ecm.Ware.Forms
{
    public partial class Frmware_Dondathang : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.WareService objWareService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.WareService>();
        public Ecm.WebReferences.Classes.RexService objRexService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.RexService>();
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        DataSet ds_dondathang = new DataSet();
        DataSet ds_dondathang_chitiet = new DataSet();
        DataSet dsHanghoa_Ban;
        DataSet dsDonvitinh = new DataSet();
        DataSet dsNhansu = new DataSet();
        object identity;
        object id_nhansu_current;
        DataSet ds_Role_User;
        public Ecm.WebReferences.WareService.Ware_Dondathang ware_dondathang;

        public Frmware_Dondathang()
        {
            InitializeComponent();

            this.item_Query.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ////date mask
            lookUpEdit_Nhansu_Nhanhang.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);
            id_nhansu_current = GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu();
            ds_Role_User = objMasterService.GetRole_System_Name_ById_User(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUserId()).ToDataSet();
            this.DisplayInfo();
        }

        void LoadMasterData()
        {
            dsHanghoa_Ban = objMasterService.Get_All_Ware_Dm_Hanghoa_Ban().ToDataSet();
            dsNhansu = objRexService.Get_All_Rex_Nhansu_Collection().ToDataSet();
            dsDonvitinh = objMasterService.Get_All_Ware_Dm_Donvitinh().ToDataSet();

            lookUpEdit_Nhansu_Nhanhang.Properties.DataSource = dsNhansu.Tables[0];
            gridLookUpEdit_Nhansu.DataSource = dsNhansu.Tables[0];
            DataRow row = dsNhansu.Tables[0].NewRow();
            row["Id_Nhansu"] = -1;
            row["Ma_Nhansu"] = "All";
            row["Hoten_Nhansu"] = "Tất cả";
            dsNhansu.Tables[0].Rows.Add(row);
            lookUpEdit_Nhansu_View.Properties.DataSource = dsNhansu.Tables[0];
            DataSet ds_collection = objMasterService.GetRole_System_Name_ById_User(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUserId()).ToDataSet();
            if (ds_collection.Tables[0].Rows.Count > 0 &&
                "" + ds_collection.Tables[0].Rows[0]["Role_System_Name"] != "Administrators")
            {
                lookUpEdit_Nhansu_View.EditValue = Convert.ToInt64(id_nhansu_current);
                lookUpEdit_Nhansu_View.Enabled = false;
            }

            gridLookUpEdit_Hanghoa_Ban.DataSource = dsHanghoa_Ban.Tables[0];
            gridLookupEdit_Ma_hanghoa.DataSource = dsHanghoa_Ban.Tables[0];
            gridLookUpEdit_Donvitinh.DataSource = dsDonvitinh.Tables[0];

            gridLookupEdit_Loai_hanghoa.DataSource = objMasterService.Get_All_Ware_Dm_Loai_Hanghoa().ToDataSet().Tables[0];
            lookUpEdit_Ten_NCC.Properties.DataSource = objMasterService.Get_All_Ware_Dm_Nhacungcap().ToDataSet().Tables[0];
            lookUpEdit_NCC.Properties.DataSource = objMasterService.Get_All_Ware_Dm_Nhacungcap().ToDataSet().Tables[0];
        }

        #region Event Override

        public override void DisplayInfo()
        {
            try
            {
                LoadMasterData();
                dtThang_Nam.EditValue = DateTime.Now;
                ChangeStatus(false);
                Reload_Chungtu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void DisplayInfo_Details()
        {
            try
            {
                identity = gv_Dondathang.GetFocusedRowCellValue("Id_Dondathang");
                this.ds_dondathang_chitiet = objWareService.Ware_Dondathang_Chitiet_SelectById_Dondathang(identity != null ? identity : 0).ToDataSet();
                this.dgware_Dondathang_Chitiet.DataSource = ds_dondathang_chitiet;
                this.dgware_Dondathang_Chitiet.DataMember = ds_dondathang_chitiet.Tables[0].TableName;
                gvware_Dondathang_Chitiet.BestFitColumns();
                DataBindingControl();
            }
            catch { }
        }

        public override void ClearDataBindings()
        {
            this.txtSochungtu.DataBindings.Clear();
            this.txtGhichu.DataBindings.Clear();
            this.dtNgay_Chungtu.DataBindings.Clear();
            this.lookUpEdit_Nhansu_Nhanhang.DataBindings.Clear();
            lookUpEdit_NCC.DataBindings.Clear();
            lookUpEdit_Ten_NCC.DataBindings.Clear();
        }

        public override void DataBindingControl()
        {
            try
            {
                ClearDataBindings();
                this.txtSochungtu.DataBindings.Add("EditValue", ds_dondathang, ds_dondathang.Tables[0].TableName + ".Sochungtu");
                this.txtGhichu.DataBindings.Add("EditValue", ds_dondathang, ds_dondathang.Tables[0].TableName + ".Ghichu");
                this.dtNgay_Chungtu.DataBindings.Add("EditValue", ds_dondathang, ds_dondathang.Tables[0].TableName + ".Ngay_Chungtu");
                this.lookUpEdit_Ten_NCC.DataBindings.Add("EditValue", ds_dondathang, ds_dondathang.Tables[0].TableName + ".Id_Ncc");
                this.lookUpEdit_NCC.DataBindings.Add("EditValue", ds_dondathang, ds_dondathang.Tables[0].TableName + ".Id_Ncc");
                this.lookUpEdit_Nhansu_Nhanhang.DataBindings.Add("EditValue", ds_dondathang, ds_dondathang.Tables[0].TableName + ".Id_Nhansu");
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
            this.txtGhichu.Properties.ReadOnly = !editTable;
            lookUpEdit_NCC.Properties.ReadOnly = !editTable;
            this.dgware_Dondathang_Chitiet.EmbeddedNavigator.Enabled = editTable;
            this.gvware_Dondathang_Chitiet.OptionsBehavior.Editable = editTable;
            dockPanel1.Enabled = !editTable;
        }

        public override void ResetText()
        {
            this.txtGhichu.EditValue = null;
            lookUpEdit_Ten_NCC.EditValue = null;
            lookUpEdit_NCC.EditValue = null;
            lookUpEdit_Nhansu_Nhanhang.EditValue = null;
            dtNgay_Chungtu.EditValue = null;
            txtSochungtu.EditValue = null;
            this.ds_dondathang_chitiet = objWareService.Ware_Dondathang_Chitiet_SelectById_Dondathang(0).ToDataSet();
            this.dgware_Dondathang_Chitiet.DataSource = ds_dondathang_chitiet.Tables[0];
        }

        public object InsertObject()
        {
            try
            {
                Ecm.WebReferences.WareService.Ware_Dondathang _Ware_Dondathang = new Ecm.WebReferences.WareService.Ware_Dondathang();
                _Ware_Dondathang.Id_Dondathang = -1;
                _Ware_Dondathang.Sochungtu = txtSochungtu.EditValue;
                _Ware_Dondathang.Ghichu = "" + txtGhichu.EditValue;
                _Ware_Dondathang.Ngay_Chungtu = dtNgay_Chungtu.EditValue;
                _Ware_Dondathang.Id_Ncc = ("" + lookUpEdit_Ten_NCC.EditValue == "") ? null : lookUpEdit_Ten_NCC.EditValue;
                _Ware_Dondathang.Id_Nhansu = lookUpEdit_Nhansu_Nhanhang.EditValue;
                identity = objWareService.Ware_Dondathang_Insert(_Ware_Dondathang);
                if (identity != null)
                {
                    this.DoClickEndEdit(dgware_Dondathang_Chitiet); //dgware_Nhap_Hh_Ban_Chitiet.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                    foreach (DataRow dr in ds_dondathang_chitiet.Tables[0].Rows)
                    {
                        if (dr.RowState != DataRowState.Deleted)
                            dr["Id_Dondathang"] = identity;
                    }
                    //update donmuahang_chitiet
                    objWareService.Update_Ware_Dondathang_Chitiet_Collection(ds_dondathang_chitiet);
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
                Ecm.WebReferences.WareService.Ware_Dondathang _Ware_Dondathang = new Ecm.WebReferences.WareService.Ware_Dondathang();
                _Ware_Dondathang.Id_Dondathang = gv_Dondathang.GetFocusedRowCellValue("Id_Dondathang");
                _Ware_Dondathang.Sochungtu = txtSochungtu.EditValue;
                _Ware_Dondathang.Ghichu = "" + txtGhichu.EditValue;
                _Ware_Dondathang.Ngay_Chungtu = dtNgay_Chungtu.EditValue;
                _Ware_Dondathang.Id_Ncc = ("" + lookUpEdit_Ten_NCC.EditValue == "") ? null : lookUpEdit_Ten_NCC.EditValue;
                _Ware_Dondathang.Id_Nhansu = lookUpEdit_Nhansu_Nhanhang.EditValue;
                //update donmuahang
                objWareService.Ware_Dondathang_Update(_Ware_Dondathang);
                //update donmuahang_chitiet
                this.DoClickEndEdit(dgware_Dondathang_Chitiet);
                foreach (DataRow dr in ds_dondathang_chitiet.Tables[0].Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                        dr["Id_Dondathang"] = gv_Dondathang.GetFocusedRowCellValue("Id_Dondathang");
                }
                objWareService.Update_Ware_Dondathang_Chitiet_Collection(ds_dondathang_chitiet);
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
            Ecm.WebReferences.WareService.Ware_Dondathang objWare_dondathang = new Ecm.WebReferences.WareService.Ware_Dondathang();
            objWare_dondathang.Id_Dondathang = gv_Dondathang.GetFocusedRowCellValue("Id_Dondathang");
            return objWareService.Ware_Dondathang_Delete(objWare_dondathang);
        }

        public override bool PerformAdd()
        {
            FormState = GoobizFrame.Windows.Forms.FormState.Add;
            ClearDataBindings();
            this.ChangeStatus(true);
            this.ResetText();
            dtNgay_Chungtu.EditValue = objWareService.GetServerDateTime();
            lookUpEdit_Nhansu_Nhanhang.EditValue = Convert.ToInt64(id_nhansu_current);
            txtSochungtu.EditValue = objWareService.GetNew_Sochungtu("Ware_Dondathang", "sochungtu", "DDH");
            return true;
        }

        public override bool PerformEdit()
        {
            try
            {
                if (gv_Dondathang.GetFocusedRowCellValue("Id_Dondathang") == null)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn đơn đặt hàng, vui lòng chọn lại");
                    return false;
                }
                if (Convert.ToInt64("0" + gv_Dondathang.GetFocusedRowCellValue("Doc_Process_Status")) == 2)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show("Đơn đặt hàng đã được duyệt, không thể chỉnh sửa");
                    return false;
                }
                this.ChangeStatus(true);
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
            this.DisplayInfo();
            return true;
        }

        public override bool PerformSave()
        {
            bool success = false;
            try
            {
                GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(lookUpEdit_Nhansu_Nhanhang, lblNguoi_Nhan_Hanghoa_Ban.Text);

                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;

                this.DoClickEndEdit(dgware_Dondathang_Chitiet);

                if (gvware_Dondathang_Chitiet.RowCount > 0)
                {
                    System.Collections.Hashtable htbControl2 = new System.Collections.Hashtable();
                    htbControl2.Add(gvware_Dondathang_Chitiet.Columns["Id_Hanghoa_Ban"], "");
                    htbControl2.Add(gvware_Dondathang_Chitiet.Columns["Soluong"], "");

                    if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(htbControl2, gvware_Dondathang_Chitiet))
                        return false;
                }
                else
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa có hàng hóa, nhập hàng hóa");
                    return false;
                }
                try
                {
                    if (ds_dondathang_chitiet.Tables[0].Constraints.Count == 0)
                    {
                        Constraint constraint = new UniqueConstraint("constraint1",
                                new DataColumn[] {ds_dondathang_chitiet.Tables[0].Columns["Id_Hanghoa_Ban"],
                            ds_dondathang_chitiet.Tables[0].Columns["Id_Donvitinh"] }, false);
                        ds_dondathang_chitiet.Tables[0].Constraints.Add(constraint);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.ToString().IndexOf("These columns don't currently have unique values") != -1)
                    {
                        GoobizFrame.Windows.Forms.MessageDialog.Show("Hàng hóa và đơn vị tính đã tồn tại, vui lòng nhập lại");
                        return false;
                    }
                    MessageBox.Show(ex.ToString());
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return success;
        }

        public override bool PerformDelete()
        {
            if (gv_Dondathang.GetFocusedRowCellValue("Id_Dondathang") == null)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn đơn đặt hàng, vui lòng chọn lại");
                return false;
            }
            if (Convert.ToInt64("0" + gv_Dondathang.GetFocusedRowCellValue("Doc_Process_Status")) == 2)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Đơn đặt hàng đã được duyệt, không thể xóa");
                return false;
            }
            if (GoobizFrame.Windows.Forms.UserMessage.Show("Msg00004", new string[]  {
             GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Ware_Nhap_Hh_Ban"),
             GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Ware_Nhap_Hh_Ban")   }) == DialogResult.Yes)
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

        public override object PerformSelectOneObject()
        {
            ware_dondathang = new Ecm.WebReferences.WareService.Ware_Dondathang();
            try
            {
                int focusedRow = gv_Dondathang.GetDataSourceRowIndex(gv_Dondathang.FocusedRowHandle);
                DataRow dr = ds_dondathang.Tables[0].Rows[focusedRow];
                if (dr != null)
                {
                    //ware_dondathang.Id_Dondathang = dr["Id_Dondathang"];
                    //ware_dondathang.Sochungtu = dr["Sochungtu"];
                    //ware_dondathang.Nguoi_Giaohang = dr["Nguoi_Giaohang"];
                    //ware_dondathang.Ngay_Nhanhang = dr["Ngay_Nhanhang"];
                    //ware_dondathang.Id_Cuahang_Ban = dr["Id_Cuahang_Ban"];
                    //ware_dondathang.Id_Nhansu_Nhanhang = dr["Id_Nhansu_Nhanhang"];
                    //ware_dondathang.Ghichu = dr["Ghichu"];
                }
                this.Dispose();
                this.Close();
                return ware_dondathang;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
                return null;
            }
        }

        public override bool PerformPrintPreview()
        {
            if (gv_Dondathang.GetFocusedRowCellValue("Id_Dondathang") == null)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn đơn đặt hàng, vui lòng chọn lại");
                return false;
            }
            DataSets.DsDodathang _DsDondathang = new Ecm.Ware.DataSets.DsDodathang();
            Reports.rptWare_Dondathang _rptWare_Dondathang = new Reports.rptWare_Dondathang();
            GoobizFrame.Windows.Forms.FrmPrintPreview frmPrintPreview = new GoobizFrame.Windows.Forms.FrmPrintPreview();
            frmPrintPreview.Report = _rptWare_Dondathang;
            _rptWare_Dondathang.DataSource = _DsDondathang;
            //Ware_Nhap_Vattu
            //int i = 1;
            foreach (DataRow dr in ds_dondathang_chitiet.Tables[0].Rows)
            {
                DataRow drnew = _DsDondathang.Tables[0].NewRow();
                foreach (DataColumn dc in ds_dondathang_chitiet.Tables[0].Columns)
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
                _DsDondathang.Tables[0].Rows.Add(drnew);
            }
            _DsDondathang.AcceptChanges();
            _rptWare_Dondathang.xrNgay.Text = dtNgay_Chungtu.Text;
            _rptWare_Dondathang.xrTableCell_Ghichu.Text = txtGhichu.Text;
            _rptWare_Dondathang.xrTable_ngay.Text = dtNgay_Chungtu.DateTime.Day.ToString();
            _rptWare_Dondathang.xrTable_Thang.Text = dtNgay_Chungtu.DateTime.Month.ToString();
            _rptWare_Dondathang.xrTable_Nam.Text = dtNgay_Chungtu.DateTime.Year.ToString();
            #region Set he so ctrinh - logo, ten cty

            using (DataSet dsHeso_Chuongtrinh = objMasterService.Get_Rex_Dm_Heso_Chuongtrinh_Collection3().ToDataSet())
            {
                DataSet dsCompany_Paras = new DataSet();
                dsCompany_Paras.Tables.Add("Company_Paras");
                dsCompany_Paras.Tables[0].Columns.Add("CompanyName", typeof(string));
                dsCompany_Paras.Tables[0].Columns.Add("CompanyAddress", typeof(string));
                dsCompany_Paras.Tables[0].Columns.Add("CompanyLogo", typeof(byte[]));

                byte[] imageData = Convert.FromBase64String("" + dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "CompanyLogo"))[0]["Heso"]);
                dsCompany_Paras.Tables[0].Rows.Add(new object[]  {    
                    dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'","CompanyName"))[0]["Heso"]
                    ,dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'","CompanyAddress"))[0]["Heso"]
                    ,imageData});
                _rptWare_Dondathang.xrc_CompanyName.DataBindings.Add(
                    new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyName"));
                _rptWare_Dondathang.xrc_CompanyAddress.DataBindings.Add(
                    new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyAddress"));
            }

            #endregion

            _rptWare_Dondathang.CreateDocument();
            GoobizFrame.Windows.Forms.ReportOptions oReportOptions = GoobizFrame.Windows.Forms.ReportOptions.GetReportOptions(_rptWare_Dondathang);
            if (Convert.ToBoolean(oReportOptions.PrintPreview))
            {
                frmPrintPreview.Text = "In Phiếu Nhập kho";  // "" + oReportOptions.Caption;
                frmPrintPreview.printControl1.PrintingSystem = _rptWare_Dondathang.PrintingSystem;
                frmPrintPreview.MdiParent = this.MdiParent;
                frmPrintPreview.Show();
                frmPrintPreview.Activate();
            }
            else
            {
                var reportPrintTool = new DevExpress.XtraReports.UI.ReportPrintTool(_rptWare_Dondathang);
                reportPrintTool.Print();
            }
            return true;
        }
        #endregion

        #region Even

        private void gridView5_CellValueChanged_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value + "" == "") return;
            if (e.Column.FieldName == "Id_Hanghoa_Ban")
            {
                gvware_Dondathang_Chitiet.SetFocusedRowCellValue(gvware_Dondathang_Chitiet.Columns["Id_Donvitinh"]
                    , ((System.Data.DataRowView)gridLookUpEdit_Hanghoa_Ban.GetDataSourceRowByKeyValue(e.Value))["Id_Donvitinh"]);
                gvware_Dondathang_Chitiet.SetFocusedRowCellValue("Id_Loai_Hanghoa_Ban"
                    , ((System.Data.DataRowView)gridLookUpEdit_Hanghoa_Ban.GetDataSourceRowByKeyValue(e.Value))["Id_Loai_Hanghoa_Ban"]);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ClearDataBindings();
            if (gv_Dondathang.FocusedRowHandle >= 0)
                DisplayInfo_Details();
            else
                ResetText();
        }

        #endregion

        void Reload_Chungtu()
        {
            ResetText();
            ds_dondathang = objWareService.Ware_Dondathang_SelectAll(dtThang_Nam.EditValue, lookUpEdit_Nhansu_View.EditValue).ToDataSet();
            dgware_Dondathang.DataSource = ds_dondathang;
            dgware_Dondathang.DataMember = ds_dondathang.Tables[0].TableName;
            this.DataBindingControl();
            this.ChangeStatus(false);
            DisplayInfo_Details();
        }

        private void gridLookUpEdit_Ma_Hanghoa_Ban_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                try
                {
                    var dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData(
                        "Ecm.MasterTables.dll",
                        "Ecm.MasterTables.Forms.Ware.Frmware_Dm_Hanghoa_Ban_FullEdit", this);

                    if (dialog == null)
                        return;

                    //dsHanghoa_Ban = dialog.GetType().GetProperty("DsDm_Hanghoa_Ban").GetValue(dialog, null) as DataSet;
                    //gridLookUpEdit_Hanghoa_Ban.DataSource = dsHanghoa_Ban.Tables[0];
                    //gridLookUpEdit_Ma_Hanghoa_Ban.DataSource = dsHanghoa_Ban.Tables[0];

                    var SelectedObject = dialog.GetType().GetProperty("Selected_Ware_Dm_Hanghoa_Ban").GetValue(dialog, null)
                        as Ecm.WebReferences.MasterService.Ware_Dm_Hanghoa_Ban;

                    if (SelectedObject != null)
                        gvware_Dondathang_Chitiet.SetFocusedRowCellValue("Id_Hanghoa_Ban", SelectedObject.Id_Hanghoa_Ban);
                }
                catch (Exception ex)
                {
                    GoobizFrame.Windows.TrayMessage.TrayMessage.Status = new GoobizFrame.Windows.TrayMessage.TrayMessageInfo(MessageBoxIcon.Asterisk, ex.Message, ex.ToString());
                }
            }
        }

        private void dtThang_Nam_EditValueChanged(object sender, EventArgs e)
        {
            Reload_Chungtu();
        }

        private void lookUpEdit_NCC_EditValueChanged(object sender, EventArgs e)
        {
            lookUpEdit_Ten_NCC.EditValue = lookUpEdit_NCC.EditValue;
        }

        private void lookUpEdit_NCC_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                lookUpEdit_NCC.EditValue = null;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Reload_Chungtu();
        }

    }
}


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
    public partial class Frmware_Hh_Ban_Kh_Tra : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.WareService objWareService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.WareService>();
        public Ecm.WebReferences.Classes.RexService objRexService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.RexService>();
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        DataSet ds_Ware_Hh_Kh_Tra = new DataSet();
        DataSet ds_Ware_Hh_Kh_Tra_Chitiet = new DataSet();
        DataSet ds_Hanghoa_Ban;
        DataSet ds_Hdbanhang;
        DataSet ds_Hdbanhang_Chitiet;
        DataSet dsDonvitinh;
        DataSet dsNhansu;
        DataSet dsKhachhang;
        object identity;
        object LocationId_Kho_Hanghoa_Ban;
        #region local data
        DataSet dsSys_Lognotify = null;
        string xml_WARE_DM_HANGHOA_BAN = @"Resources\localdata\Ware_Dm_Hanghoa_Ban.xml";
        string xml_REX_NHANSU = @"Resources\localdata\Rex_Nhansu.xml";
        string xml_WARE_DM_DONVITINH = @"Resources\localdata\Ware_Dm_Donvitinh.xml";
        DateTime dtlc_ware_dm_hanghoa_ban;
        DateTime dtlc_ware_dm_donvitinh;
        DateTime dtlc_rex_nhansu;
        #endregion

        #region  Initialize

        public Frmware_Hh_Ban_Kh_Tra()
        {
            InitializeComponent();
            if (!System.IO.Directory.Exists(@"Resources\localdata"))
                System.IO.Directory.CreateDirectory(@"Resources\localdata");
            //date mask
            this.dtNgay_Chungtu_View.Properties.MinValue = new DateTime(2000, 01, 01);
            dtNgay_Chungtu.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtNgay_Chungtu.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();
            repositoryItemDateEdit_Ngay_Nhap_Hh_Mua.Properties.Mask.UseMaskAsDisplayFormat = true;
            repositoryItemDateEdit_Ngay_Nhap_Hh_Mua.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();
            //reset lookup edit as delete value
            lookUpEdit_Khachhang.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);
            lookUpEdit_Cuahang_Ban.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);
            lookUpEdit_Nhansu_Lap.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);
            lookUp_soHd.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);

            this.item_Edit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.BarSystem.Visible = false;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            ShowTabPage(xtraTabControl1, xtraTabPageHh_Kh_Tra);
            dt_Ngay_LapPhieu_Tra.EditValue = DateTime.Now;
            //this.DisplayInfo();
            DisplayInfo();

            btnHh_Kh_Tra.Text = xtraTabPageHdbanhang_Chitiet.Text;

            // GoobizFrame.Windows.PlugIn.RightHelpers.CheckUserRightAction(this);   
            #region Gán quyền truy cập trên form
            btnAdd.Enabled = EnableAdd;
            btnDelete.Enabled = EnableDelete;
            btnPrint.Enabled = EnablePrintPreview;
            #endregion
        }

        /// <summary>
        /// Truy xuat DateTime thay doi du lieu cuoi cung
        /// </summary>
        /// <param name="table_name"></param>
        /// <returns></returns>
        private DateTime GetLastChange_FrmLognotify(string table_name)
        {
            try
            {
                return Convert.ToDateTime(dsSys_Lognotify.Tables[0].Select(string.Format("Table_Name='{0}'", table_name))[0]["Last_Change"]);
            }
            catch (Exception ex)
            {
                return new DateTime(2010, 01, 01);
            }
        }

        void LoadMasterTable()
        {
            //load data from local xml when last change at local differ from database
            dsSys_Lognotify = objMasterService.Get_Sys_Lognotify_SelectLastChange_OfTables("[ware_dm_hanghoa_ban], "
                  + "[ware_dm_donvitinh], [rex_nhansu]").ToDataSet();
            dtlc_rex_nhansu = GetLastChange_FrmLognotify("REX_NHANSU");
            dtlc_ware_dm_hanghoa_ban = GetLastChange_FrmLognotify("WARE_DM_HANGHOA_BAN");
            dtlc_ware_dm_donvitinh = GetLastChange_FrmLognotify("WARE_DM_DONVITINH");
            if (DateTime.Compare(dtlc_ware_dm_hanghoa_ban, System.IO.File.GetLastWriteTime(xml_WARE_DM_HANGHOA_BAN)) > 0
                || !System.IO.File.Exists(xml_WARE_DM_HANGHOA_BAN))
            {
                ds_Hanghoa_Ban = objMasterService.Get_All_Ware_Dm_Hanghoa_Ban().ToDataSet();
                ds_Hanghoa_Ban.WriteXml(xml_WARE_DM_HANGHOA_BAN, XmlWriteMode.WriteSchema);
            }
            else if (ds_Hanghoa_Ban == null || ds_Hanghoa_Ban.Tables.Count == 0)
            {
                ds_Hanghoa_Ban = new DataSet();
                ds_Hanghoa_Ban.ReadXml(xml_WARE_DM_HANGHOA_BAN);
            }
            if (DateTime.Compare(dtlc_rex_nhansu, System.IO.File.GetLastWriteTime(xml_REX_NHANSU)) > 0
                || !System.IO.File.Exists(xml_REX_NHANSU))
            {
                dsNhansu = objRexService.Get_All_Rex_Nhansu_Collection().ToDataSet();
                dsNhansu.WriteXml(xml_REX_NHANSU, XmlWriteMode.WriteSchema);
            }
            else if (dsNhansu == null || dsNhansu.Tables.Count == 0)
            {
                dsNhansu = new DataSet();
                dsNhansu.ReadXml(xml_REX_NHANSU);
            }
            if (DateTime.Compare(dtlc_ware_dm_donvitinh, System.IO.File.GetLastWriteTime(xml_WARE_DM_DONVITINH)) > 0
            || !System.IO.File.Exists(xml_WARE_DM_DONVITINH))
            {
                dsDonvitinh = objMasterService.Get_All_Ware_Dm_Donvitinh().ToDataSet();
                dsDonvitinh.WriteXml(xml_WARE_DM_DONVITINH, XmlWriteMode.WriteSchema);
            }
            else if (dsDonvitinh == null || dsDonvitinh.Tables.Count == 0)
            {
                dsDonvitinh = new DataSet();
                dsDonvitinh.ReadXml(xml_WARE_DM_DONVITINH);
            }

            dsKhachhang = objMasterService.Get_All_Ware_Dm_Khachhang().ToDataSet();
            LookUpEdit_Khachhang_Tra.DataSource = dsKhachhang.Tables[0];
            lookUpEdit_Nhansu_Lap.Properties.DataSource = dsNhansu.Tables[0];
            gridLookUpEdit_Nhansu_Lap.DataSource = dsNhansu.Tables[0];
            gridLookUpEdit_Hanghoa_Ban.DataSource = ds_Hanghoa_Ban.Tables[0];
            gridLookUpEdit_Ma_Hanghoa_Ban.DataSource = ds_Hanghoa_Ban.Tables[0];
            gridLookUpEdit_Donvitinh.DataSource = dsDonvitinh.Tables[0];
        }
        #endregion

        #region Event Override
        public override void DisplayInfo()
        {
            try
            {
                //Set lại trạng thái form là view
                FormState = GoobizFrame.Windows.Forms.FormState.View;

                LoadMasterTable();

                //Kiểm tra nếu nhân viên login không tồn tại trong kho hàng hóa mua thì access denied.
                lookUpEdit_Nhansu_Lap.EditValue = Convert.ToInt64(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu());
                LocationId_Kho_Hanghoa_Ban = Convert.ToInt64(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetLocation("Id_Cuahang_Ban"));
                lookUpEdit_Cuahang_Ban.EditValue = LocationId_Kho_Hanghoa_Ban;
                lookUpEdit_Ten_Cuahang_Ban.EditValue = LocationId_Kho_Hanghoa_Ban;
                //lookUpEdit_Khachhang
                lookUpEdit_Khachhang.Properties.DataSource = objMasterService.Get_All_Ware_Dm_Khachhang().ToDataSet().Tables[0];
                //gridLookUpEdit_Hdbanhang
                ds_Hdbanhang = objWareService.Get_All_Ware_Hdbanhang_ByCuahang(lookUpEdit_Cuahang_Ban.EditValue).ToDataSet();
                gridLookUpEdit_Hdbanhang.DataSource = ds_Hdbanhang.Tables[0];

                //Get data Ware_Dm_Cuahang_Ban
                DataSet dsCuahang_Ban = objMasterService.Get_All_Ware_Dm_Cuahang_Ban().ToDataSet();
                //DataSet dsCuahang_Ban = objWareService.Get_Ware_Ql_Cuahang_Ban_By_Id_Nhansu(lookUpEdit_Nhansu_Lap.EditValue,lookUpEdit_Cuahang_Ban.EditValue).ToDataSet();
                lookUpEdit_Cuahang_Ban.Properties.DataSource = dsCuahang_Ban.Tables[0];
                lookUpEdit_Ten_Cuahang_Ban.Properties.DataSource = dsCuahang_Ban.Tables[0];
                gridLookUpEdit_Cuahang_Ban.DataSource = dsCuahang_Ban.Tables[0];




                //Get data Ware_Nhap_Hh_Ban
                // ds_Ware_Hh_Kh_Tra = objWareService.Get_All_Ware_Hh_Kh_Tra_HdBanhang(-1).ToDataSet();

                this.DataBindingControl();
                this.ChangeStatus(false);
                gridView1.BestFitColumns();
                DisplayInfo2();
                this.FormState = GoobizFrame.Windows.Forms.FormState.View;
                changeStatusButton(true);
            }
            catch (Exception ex)
            {
#if DEBUG
                // MessageBox.Show(ex.Message);
#endif

            }
        }

        public override void ClearDataBindings()
        {
            this.txtId_Hh_Kh_Tra.DataBindings.Clear();
            this.txtSochungtu.DataBindings.Clear();
            this.txtGhichu.DataBindings.Clear();
            this.dtNgay_Chungtu.DataBindings.Clear();
            this.lookUpEdit_Nhansu_Lap.DataBindings.Clear();
            this.lookUpEdit_Khachhang.DataBindings.Clear();
        }

        public override void DataBindingControl()
        {
            try
            {
                ClearDataBindings();
                this.txtId_Hh_Kh_Tra.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Id_Hh_Kh_Tra");
                this.txtSochungtu.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Sochungtu");
                this.txtGhichu.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Ghichu");
                this.dtNgay_Chungtu.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Ngay_Chungtu");
                this.lookUpEdit_Nhansu_Lap.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Id_Nhansu_Lap");
                this.lookUpEdit_Khachhang.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Id_Khachhang");
            }
            catch (Exception ex)
            {
#if DEBUG
                // MessageBox.Show(ex.Message);
#endif

            }
        }

        public override void ChangeStatus(bool editTable)
        {
            this.dgware_Hh_Kh_Tra.Enabled = !editTable;
            this.btnHh_Kh_Tra.Enabled = !editTable;
            this.txtGhichu.Properties.ReadOnly = !editTable;
            // this.lookUp_soHd.Properties.ReadOnly = !editTable;
            // this.lookUpEdit_Ten_Cuahang_Ban.Properties.ReadOnly = !editTable;
            lookUpEdit_Khachhang.Properties.ReadOnly = !editTable;
            this.dgware_Hh_Kh_Tra_Chitiet.EmbeddedNavigator.Enabled = editTable;
            //  this.dtNgay_Chungtu_View.Properties.ReadOnly = !editTable;
            this.gridView5.OptionsBehavior.Editable = editTable;
        }

        public override void ResetText()
        {
            this.txtId_Hh_Kh_Tra.EditValue = "";
            this.txtGhichu.EditValue = "";
            this.lookUpEdit_Khachhang.EditValue = "";
            this.ds_Ware_Hh_Kh_Tra_Chitiet = objWareService.Get_All_Ware_Hh_Kh_Tra_Chitiet_ByHh_Kh_Tra(0).ToDataSet();
            this.dgware_Hh_Kh_Tra_Chitiet.DataSource = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0];
            this.lookUp_soHd.EditValue = "";
            dgware_Hdbanhang_Chitiet.DataSource = null;
            dtNgay_Chungtu_View.EditValue = null;
        }

        public object InsertObject()
        {
            try
            {
                Ecm.WebReferences.WareService.Ware_Hh_Kh_Tra objWare_Hh_Kh_Tra = new Ecm.WebReferences.WareService.Ware_Hh_Kh_Tra();
                objWare_Hh_Kh_Tra.Id_Hh_Kh_Tra = -1;
                objWare_Hh_Kh_Tra.Sochungtu = txtSochungtu.EditValue;
                objWare_Hh_Kh_Tra.Ghichu = txtGhichu.EditValue;
                objWare_Hh_Kh_Tra.Ngay_Chungtu = dtNgay_Chungtu.EditValue;

                if ("" + lookUpEdit_Cuahang_Ban.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Cuahang_Ban = lookUpEdit_Cuahang_Ban.EditValue;
                if ("" + lookUpEdit_Khachhang.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Khachhang = lookUpEdit_Khachhang.EditValue;
                if ("" + lookUpEdit_Nhansu_Lap.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Nhansu_Lap = lookUpEdit_Nhansu_Lap.EditValue;

                identity = objWareService.Insert_Ware_Hh_Kh_Tra(objWare_Hh_Kh_Tra);
                if (identity != null)
                {
                    this.DoClickEndEdit(dgware_Hh_Kh_Tra_Chitiet);
                    foreach (DataRow dr in ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows)
                    {
                        dr["Id_Hh_Kh_Tra"] = identity;
                    }
                    //update nhap_hh_mua_chitiet
                    objWareService.Update_Ware_Hh_Kh_Tra_Chitiet_Collection(ds_Ware_Hh_Kh_Tra_Chitiet);
                }
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
                return false;
            }
        }

        public object UpdateObject()
        {
            try
            {
                Ecm.WebReferences.WareService.Ware_Hh_Kh_Tra objWare_Hh_Kh_Tra = new Ecm.WebReferences.WareService.Ware_Hh_Kh_Tra();
                objWare_Hh_Kh_Tra.Id_Hh_Kh_Tra = identity;
                objWare_Hh_Kh_Tra.Sochungtu = txtSochungtu.EditValue;
                objWare_Hh_Kh_Tra.Ghichu = txtGhichu.EditValue;
                objWare_Hh_Kh_Tra.Ngay_Chungtu = dtNgay_Chungtu.EditValue;

                if ("" + lookUpEdit_Cuahang_Ban.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Cuahang_Ban = lookUpEdit_Cuahang_Ban.EditValue;
                if ("" + lookUpEdit_Khachhang.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Khachhang = lookUpEdit_Khachhang.EditValue;
                if ("" + lookUpEdit_Nhansu_Lap.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Nhansu_Lap = lookUpEdit_Nhansu_Lap.EditValue;
                //update nhap_hh_mua
                objWareService.Update_Ware_Hh_Kh_Tra(objWare_Hh_Kh_Tra);

                //update nhap_hh_mua_chitiet
                this.DoClickEndEdit(dgware_Hh_Kh_Tra_Chitiet);// dgware_Hh_Kh_Tra_Chitiet.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                foreach (DataRow dr in ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                        dr["Id_Hh_Kh_Tra"] = txtId_Hh_Kh_Tra.EditValue;
                }
                //ds_Donmuahang_Chitiet.RejectChanges();
                objWareService.Update_Ware_Hh_Kh_Tra_Chitiet_Collection(ds_Ware_Hh_Kh_Tra_Chitiet);
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
                return false;
            }
        }

        public object DeleteObject()
        {
            Ecm.WebReferences.WareService.Ware_Hh_Kh_Tra objWare_Hh_Kh_Tra = new Ecm.WebReferences.WareService.Ware_Hh_Kh_Tra();
            objWare_Hh_Kh_Tra.Id_Hh_Kh_Tra = gridView1.GetFocusedRowCellValue("Id_Hh_Kh_Tra");
            return objWareService.Delete_Ware_Hh_Kh_Tra(objWare_Hh_Kh_Tra);
        }

        public override bool PerformAdd()
        {
            dtNgay_Chungtu.EditValue = objWareService.GetServerDateTime();
            //Kiểm tra nếu nhân viên login không tồn tại trong kho hàng hóa mua thì access denied.
            lookUpEdit_Nhansu_Lap.EditValue = Convert.ToInt64(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu());
            DataSet ds_Cuahang_Ban = objMasterService.Get_All_Ware_Dm_Cuahang_Ban_By_Id_Nhansu(lookUpEdit_Nhansu_Lap.EditValue).ToDataSet();
            //if (ds_Cuahang_Ban.Tables[0].Rows.Count > 0)
            //    lookUpEdit_Cuahang_Ban.EditValue = ds_Cuahang_Ban.Tables[0].Rows[0]["Id_Cuahang_Ban"];

            //else
            //{
            //    GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
            //    lookUpEdit_Nhansu_Lap.EditValue = null;
            //    return false;
            //}
            ClearDataBindings();
            this.ChangeStatus(true);
            this.ResetText();
            txtSochungtu.EditValue = objWareService.GetNew_Sochungtu("Ware_Hh_Kh_Tra", "sochungtu", "RE-" + lookUpEdit_Cuahang_Ban.GetColumnValue("Ma_Cuahang_Ban") + "-");
            ShowTabPage(xtraTabControl1, xtraTabPageHdbanhang_Chitiet);
            lookUpEdit_Cuahang_Ban.EditValue = lookUpEdit_Ten_Cuahang_Ban.EditValue;
            this.FormState = GoobizFrame.Windows.Forms.FormState.Add;
            return true;
        }

        public override bool PerformEdit()
        {
            if (!GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu().Equals("" + lookUpEdit_Nhansu_Lap.EditValue))
            {
                GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                return false;
            }
            this.ChangeStatus(true);
            return true;
        }

        public override bool PerformCancel()
        {
            this.DisplayInfo();
            return true;
        }

        public override bool PerformSave()
        {
            if (ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Chưa có thông tin trả hàng, click vào nút 'Bỏ qua'");
                return false;
            }
            try
            {
                DataSet ds_PhieuTraHang = objWareService.Get_All_Ware_Hh_Kh_Tra_Chitiet().ToDataSet();
                ds_Hdbanhang_Chitiet = objWareService.Get_All_Ware_Hdbanhang_Chitiet_ByHdbanhang_Sochungtu(lookUp_soHd.EditValue).ToDataSet();
                for (int i = 0; i < gridView5.RowCount; i++)
                {
                    DataRow[] dtr_hdbanhang_chitiet = ds_Hdbanhang_Chitiet.Tables[0].Select("Id_Hdbanhang_Chitiet =" + gridView5.GetRowCellValue(i, "Id_Hdbanhang_Chitiet"));
                    DataRow[] dtr = ds_PhieuTraHang.Tables[0].Select("Id_Hdbanhang_Chitiet =" + gridView5.GetRowCellValue(i, "Id_Hdbanhang_Chitiet"));
                    if (dtr != null && dtr.Length > 0)
                    {
                        if (Convert.ToInt32(ds_PhieuTraHang.Tables[0].Compute("Sum(Soluong)", "Id_Hdbanhang_Chitiet =" + gridView5.GetRowCellValue(i, "Id_Hdbanhang_Chitiet")))
                            + Convert.ToInt32(gridView5.GetRowCellValue(i, "Soluong")) > Convert.ToInt32(dtr_hdbanhang_chitiet[0]["Soluong"]))
                        {
                            GoobizFrame.Windows.Forms.MessageDialog.Show("Hàng hóa đã trả hết, vui lòng kiểm tra lại");
                            return false;
                        }
                    }
                }
                bool success = false;
                GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(txtSochungtu, lblSochungtu.Text);
                hashtableControls.Add(lookUpEdit_Cuahang_Ban, lblCuahang_Ban.Text);
                hashtableControls.Add(lookUpEdit_Nhansu_Lap, lblNhansu_Lap.Text);

                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;

                success = (bool)this.InsertObject();

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
            if (GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUser().ToUpper() != "ADMIN")
                if (!GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu().Equals("" + lookUpEdit_Nhansu_Lap.EditValue))
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                    return false;
                }
            if (GoobizFrame.Windows.Forms.UserMessage.Show("Msg00004", new string[]  {
             GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Ware_Hh_Kh_Tra"),
             GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Ware_Hh_Kh_Tra")   }) == DialogResult.Yes)
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
            Ecm.WebReferences.WareService.Ware_Hh_Kh_Tra ware_Hh_Kh_Tra = new Ecm.WebReferences.WareService.Ware_Hh_Kh_Tra();
            try
            {
                int focusedRow = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
                DataRow dr = ds_Ware_Hh_Kh_Tra.Tables[0].Rows[focusedRow];
                if (dr != null)
                {
                    ware_Hh_Kh_Tra.Id_Hh_Kh_Tra = dr["Id_Hh_Kh_Tra"];
                    ware_Hh_Kh_Tra.Sochungtu = dr["Sochungtu"];
                    ware_Hh_Kh_Tra.Ngay_Chungtu = dr["Ngay_Chungtu"];
                    ware_Hh_Kh_Tra.Id_Cuahang_Ban = dr["Id_Cuahang_Ban"];
                    ware_Hh_Kh_Tra.Id_Nhansu_Lap = dr["Id_Nhansu_Lap"];
                    ware_Hh_Kh_Tra.Ghichu = dr["Ghichu"];
                    ware_Hh_Kh_Tra.Id_Khachhang = dr["Id_Khachhang"];
                }
                this.Dispose();
                this.Close();
                return ware_Hh_Kh_Tra;
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
            if (identity == null)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Bạn chưa chọn hóa đơn nên không thể in, chọn lại hóa đơn cần in");
                return false;
            }
            ds_Ware_Hh_Kh_Tra = objWareService.Get_All_Ware_Hh_Kh_Tra_Hhban_ByCuahang(lookUpEdit_Cuahang_Ban.EditValue).ToDataSet();
            DataRow[] sdr = ds_Ware_Hh_Kh_Tra.Tables[0].Select("Id_Hh_Kh_Tra = " + identity);
            if (sdr.Length == 0)
                return false;

            DataSets.dsHdbanhang_Chitiet dsrHdbanhang_Chitiet = new Ecm.Ware.DataSets.dsHdbanhang_Chitiet();
            Reports.rptWare_Hh_Ban_Kh_Tra rptHdbanhang_noVAT = new Ecm.Ware.Reports.rptWare_Hh_Ban_Kh_Tra();
            GoobizFrame.Windows.Forms.FrmPrintPreview frmPrintPreview = new GoobizFrame.Windows.Forms.FrmPrintPreview();
            frmPrintPreview.Report = rptHdbanhang_noVAT;
            rptHdbanhang_noVAT.DataSource = dsrHdbanhang_Chitiet;
            int i = 1;
            foreach (DataRow dr in ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows)
            {
                DataRow drnew = dsrHdbanhang_Chitiet.Tables[0].NewRow();
                foreach (DataColumn dc in ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Columns)
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
                drnew["Dongia_Ban"] = dr["Dongia"];
                drnew["Ma_Hanghoa"] = ds_Hdbanhang.Tables[0].Select("Id_Hdbanhang = " + dr["Id_Hdbanhang"])[0]["Sochungtu"] + " * " +
                    ds_Hanghoa_Ban.Tables[0].Select("Id_Hanghoa_Ban=" + dr["Id_Hanghoa_Ban"])[0]["Ma_Hanghoa_Ban"] + " * " +
                    ds_Hanghoa_Ban.Tables[0].Select("Id_Hanghoa_Ban=" + dr["Id_Hanghoa_Ban"])[0]["Ten_Hanghoa_Ban"];
                drnew["Stt"] = i++;

                dsrHdbanhang_Chitiet.Tables[0].Rows.Add(drnew);
            }
            //add parameter values
            rptHdbanhang_noVAT.tbc_Ngay.Text = "" + string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", sdr[0]["Ngay_Chungtu"]);
            rptHdbanhang_noVAT.lblNhansu_Order.Text = lookUpEdit_Nhansu_Lap.Text;
            rptHdbanhang_noVAT.tbcSochungtu.Text = "" + sdr[0]["Sochungtu"];
            rptHdbanhang_noVAT.xrTableRow_Tiengiam.Visible = false;
            double thanhtien = Convert.ToDouble(gridView5.Columns["Thanhtien"].SummaryItem.SummaryValue);
            //string str =  GoobizFrame.Windows.HelperClasses.ReadNumber.ChangeNum2VNStr(thanhtien, " đồng.");
            //str = str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
            rptHdbanhang_noVAT.PageSize = new Size(800, 1400 + 120 * Convert.ToInt32(dsrHdbanhang_Chitiet.Tables[0].Rows.Count));

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

                rptHdbanhang_noVAT.xrc_CompanyName.DataBindings.Add(
                    new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyName"));
                rptHdbanhang_noVAT.xrc_CompanyAddress.DataBindings.Add(
                    new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyAddress"));
                //rptHdbanhang_noVAT.xrPic_Logo.DataBindings.Add(
                //    new DevExpress.XtraReports.UI.XRBinding("Image", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyLogo"));
            }
            #endregion

            rptHdbanhang_noVAT.CreateDocument();
            GoobizFrame.Windows.Forms.ReportOptions oReportOptions = GoobizFrame.Windows.Forms.ReportOptions.GetReportOptions(rptHdbanhang_noVAT);
            if (Convert.ToBoolean(oReportOptions.PrintPreview))
            {
                frmPrintPreview.Text = "" + oReportOptions.Caption;
                frmPrintPreview.printControl1.PrintingSystem = rptHdbanhang_noVAT.PrintingSystem;
                frmPrintPreview.MdiParent = this.MdiParent;
                frmPrintPreview.Text = this.Text + "(Xem trang in)";
                frmPrintPreview.Show();
                frmPrintPreview.Activate();
            }
            else
            {
                var reportPrintTool = new DevExpress.XtraReports.UI.ReportPrintTool(rptHdbanhang_noVAT);
                reportPrintTool.Print();
            }
            return base.PerformPrintPreview();
        }
        #endregion

        #region  Even

        private void gridView5_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Soluong" || e.Column.FieldName == "Dongia")
            {
                if ("" + gridView5.GetFocusedRowCellValue("Soluong") != ""
                    && "" + gridView5.GetFocusedRowCellValue("Dongia") != "")
                    gridView5.SetFocusedRowCellValue(
                        gridView5.Columns["Thanhtien"]
                        , Convert.ToDecimal(gridView5.GetFocusedRowCellValue("Soluong"))
                            * Convert.ToDecimal(gridView5.GetFocusedRowCellValue("Dongia"))
                            * (1 - Convert.ToDecimal("0" + gridView5.GetFocusedRowCellValue("Per_Dongia")) / 100)
                                                    );
            }
            else if (e.Column.FieldName == "Id_Hanghoa_Ban")
            {
                gridView5.SetFocusedRowCellValue(gridView5.Columns["Id_Donvitinh"]
                    , ((System.Data.DataRowView)gridLookUpEdit_Hanghoa_Ban.GetDataSourceRowByKeyValue(e.Value))["Id_Donvitinh"]);
                gridView5.SetFocusedRowCellValue(gridView5.Columns["Dongia"]
                    , ((System.Data.DataRowView)gridLookUpEdit_Hanghoa_Ban.GetDataSourceRowByKeyValue(e.Value))["Dongia_Ban"]);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
                DisplayInfo2();
        }

        private void lookUpEdit_Khachhang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (FormState != GoobizFrame.Windows.Forms.FormState.View)
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
                {
                    Ecm.MasterTables.Forms.Ware.Frmware_Dm_Khachhang_Add objFrmware_Dm_Khachhang_Add = new Ecm.MasterTables.Forms.Ware.Frmware_Dm_Khachhang_Add();
                    objFrmware_Dm_Khachhang_Add.item_Add.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    objFrmware_Dm_Khachhang_Add.item_Cancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    objFrmware_Dm_Khachhang_Add.item_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    objFrmware_Dm_Khachhang_Add.item_Edit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    objFrmware_Dm_Khachhang_Add.item_Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    objFrmware_Dm_Khachhang_Add.item_Select.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    objFrmware_Dm_Khachhang_Add.ShowDialog();
                    if (objFrmware_Dm_Khachhang_Add.SelectedWare_Dm_Khachhang.Id_Khachhang != null)
                    {
                        lookUpEdit_Khachhang.EditValue = objFrmware_Dm_Khachhang_Add.SelectedWare_Dm_Khachhang.Id_Khachhang;
                    }
                }
        }

        private void gridLookUpEdit_Hanghoa_Ban_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                Show_Hdbanhang_Chitiet_Dialog();
            }
        }
        private void btnImport_Hanghoa_Click(object sender, EventArgs e)
        {
            Show_Hdbanhang_Chitiet_Dialog();
        }
        #region dgware_Dm_Hanghoa_Mua
        private void btnAccept_Click(object sender, EventArgs e)
        {
            DataRow[] sdrDm_Hanghoa_Mua = ds_Hdbanhang_Chitiet.Tables[0].Select("Soluong_Tra > 0");
            foreach (DataRow drDm_Hanghoa_Mua in sdrDm_Hanghoa_Mua)
            {
                DataRow nrow = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].NewRow();
                nrow["Id_Hdbanhang"] = drDm_Hanghoa_Mua["Id_Hdbanhang"];
                nrow["Id_Hanghoa_Ban"] = drDm_Hanghoa_Mua["Id_Hanghoa_Ban"];
                nrow["Id_Donvitinh"] = drDm_Hanghoa_Mua["Id_Donvitinh"];
                nrow["Soluong"] = drDm_Hanghoa_Mua["Soluong_Tra"];
                nrow["Dongia"] = drDm_Hanghoa_Mua["Dongia_Ban"];
                nrow["Per_Dongia"] = drDm_Hanghoa_Mua["Per_Dongia"];
                nrow["Thanhtien"] = Convert.ToDecimal(nrow["Soluong"])
                                    * Convert.ToDecimal(nrow["Dongia"])
                                    * (1 - Convert.ToDecimal("0" + nrow["Per_Dongia"]) / 100);
                ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows.Add(nrow);
            }

            ds_Hanghoa_Ban.RejectChanges();
        }
        #endregion
        void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            object value = GoobizFrame.Windows.Forms.FrmGNumboardInput.ShowInputDialog("" + gridView3.GetFocusedRowCellValue("Soluong"));
            if (value.ToString().Contains("."))
            {
                gridView3.SetFocusedRowCellValue(gridView3.FocusedColumn, gridView3.GetFocusedRowCellValue("Soluong"));
                return;
            }
            if (value.ToString().Contains("-"))
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Số lượng không được nhập số âm");
                value = value.ToString().Replace("-", "");
                gridView3.SetFocusedRowCellValue(gridView3.FocusedColumn, value);
            }
            if (Convert.ToDecimal(gridView3.GetFocusedRowCellValue("Soluong")) >= Convert.ToDecimal(value))
                gridView3.SetFocusedRowCellValue(gridView3.FocusedColumn, value);
            else
                gridView3.SetFocusedRowCellValue(gridView3.FocusedColumn, gridView3.GetFocusedRowCellValue("Soluong"));
            if (Convert.ToDecimal(value) == 0)
                gridView3.SetFocusedRowCellValue(gridView3.FocusedColumn, gridView3.GetFocusedRowCellValue("Soluong"));
        }

        void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridView3.GetFocusedRowCellValue("Soluong_Tra").ToString().Length > 5)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Số lượng trả nhập không đúng, vui lòng nhập lại");
                gridView3.SetFocusedRowCellValue("Soluong_Tra", 1);
                return;
            }
            int soluong_tra = Convert.ToInt32("0" + gridView3.GetFocusedRowCellValue("Soluong_Tra"));
            int soluong_kh_tra = Convert.ToInt32("0" + gridView3.GetFocusedRowCellValue("Soluong_Kh_Tra"));
            double soluong_temp = Convert.ToDouble("0" + gridView3.GetFocusedRowCellValue("Soluong"));
            double thanhtien_km = Convert.ToDouble("0" + gridView3.GetDataRow(gridView3.FocusedRowHandle)["Thanhtien_Km"]);
            int soluong = Convert.ToInt32(soluong_temp);
            if (soluong_tra > 0 && soluong - soluong_kh_tra >= soluong_tra)
            {
                int thanhtien_km_unit = System.Convert.ToInt32("0" + thanhtien_km) / soluong;
                DataRow[] sdr = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Select("Id_Hdbanhang_Chitiet =" + gridView3.GetFocusedRowCellValue("Id_Hdbanhang_Chitiet"));
                if (sdr != null && sdr.Length > 0)
                {
                    sdr[0]["Soluong"] = Convert.ToInt32(sdr[0]["Soluong"]) + soluong_tra;
                    sdr[0]["Thanhtien"] = Convert.ToInt32(gridView3.GetFocusedRowCellValue("Thanhtien")) / soluong * soluong_tra;
                    sdr[0]["Thanhtien_Km"] = Convert.ToInt32(sdr[0]["Soluong"]) * thanhtien_km_unit;
                }
                else
                {
                    DataRow nrow = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].NewRow();
                    nrow["Id_Hanghoa_Ban"] = gridView3.GetFocusedRowCellValue("Id_Hanghoa_Ban");
                    nrow["Id_Donvitinh"] = gridView3.GetFocusedRowCellValue("Id_Donvitinh");
                    nrow["Soluong"] = gridView3.GetFocusedRowCellValue("Soluong_Tra");
                    nrow["Dongia"] = gridView3.GetFocusedRowCellValue("Dongia_Ban");
                    nrow["Per_Dongia"] = gridView3.GetFocusedRowCellValue("Per_Dongia");
                    nrow["Thanhtien"] = (Convert.ToDecimal(nrow["Dongia"]) * (100 - Convert.ToDecimal(nrow["Per_Dongia"])) / 100) * soluong_tra;
                    nrow["Thanhtien_Km"] = Convert.ToDecimal(nrow["Soluong"]) * thanhtien_km_unit;
                    nrow["Id_Hdbanhang_Chitiet"] = gridView3.GetFocusedRowCellValue("Id_Hdbanhang_Chitiet");
                    nrow["Id_Hdbanhang"] = gridView3.GetFocusedRowCellValue("Id_Hdbanhang");
                    ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows.Add(nrow);
                }
                if (soluong_tra == soluong)
                    gridView3.GetDataRow(gridView3.FocusedRowHandle).Delete();
                else
                {
                    gridView3.SetFocusedRowCellValue(gridView3.Columns["Soluong"], soluong - soluong_tra);
                    gridView3.SetFocusedRowCellValue(gridView3.Columns["Soluong_Tra"], gridView3.GetFocusedRowCellValue("Soluong"));
                    gridView3.SetFocusedRowCellValue(gridView3.Columns["Thanhtien_Km"],
                        (soluong - soluong_tra) * (Convert.ToDecimal(gridView3.GetFocusedRowCellValue("Per_Dongia")) * Convert.ToDecimal(gridView3.GetFocusedRowCellValue("Dongia_Ban")) / 100));
                }
            }
            else
                GoobizFrame.Windows.Forms.MessageDialog.Show(
                   String.Format("SL mua: {0}, SL trả lần trước: {1}, SL còn lại có thể trả: {2}"
                   , new object[] { soluong, soluong_kh_tra, soluong - soluong_kh_tra }), "SL trả hàng không hợp lệ");
        }

        void repositoryItemButtonEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow[] sdr = ds_Hdbanhang_Chitiet.Tables[0].Select("Id_Hdbanhang_Chitiet =" + gridView5.GetFocusedRowCellValue("Id_Hdbanhang_Chitiet"));
            if (sdr != null && sdr.Length > 0)
            {
                sdr[0]["Soluong"] = Convert.ToInt32(sdr[0]["Soluong"]) + Convert.ToInt32(ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Soluong"]);
                sdr[0]["Soluong_Tra"] = sdr[0]["Soluong"];
                sdr[0]["Thanhtien"] = Convert.ToDecimal(gridView5.GetFocusedRowCellValue("Thanhtien")) * Convert.ToDecimal(sdr[0]["Soluong"]);
                sdr[0]["Thanhtien_Km"] = (Convert.ToDecimal(sdr[0]["Dongia_Ban"]) * Convert.ToDecimal(sdr[0]["Per_Dongia"]) / 100) * Convert.ToDecimal(sdr[0]["Soluong"]);
            }
            else
            {
                DataRow nrow = ds_Hdbanhang_Chitiet.Tables[0].NewRow();
                nrow["Id_Hanghoa_Ban"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Id_Hanghoa_Ban"];
                nrow["Ten_Hanghoa_Ban"] = ds_Hanghoa_Ban.Tables[0].Select("Id_Hanghoa_Ban=" + nrow["Id_Hanghoa_Ban"])[0]["Ten_Hanghoa_Ban"];
                nrow["Ma_Hanghoa_Ban"] = ds_Hanghoa_Ban.Tables[0].Select("Id_Hanghoa_Ban=" + nrow["Id_Hanghoa_Ban"])[0]["Ma_Hanghoa_Ban"];
                nrow["Id_Donvitinh"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Id_Donvitinh"];
                nrow["Soluong"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Soluong"];
                nrow["Soluong_Tra"] = nrow["Soluong"];
                nrow["Dongia_Ban"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Dongia"];
                nrow["Per_Dongia"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Per_Dongia"];
                nrow["Thanhtien"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Thanhtien"];
                nrow["Id_Hdbanhang_Chitiet"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Id_Hdbanhang_Chitiet"];
                nrow["Id_Hdbanhang"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Id_Hdbanhang"];
                nrow["Thanhtien_Km"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Thanhtien_Km"];
                ds_Hdbanhang_Chitiet.Tables[0].Rows.Add(nrow);
            }
            gridView5.GetDataRow(gridView5.FocusedRowHandle).Delete();
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            gridView3.MovePrevPage();
        }

        private void btnNext1_Click(object sender, EventArgs e)
        {
            gridView3.MoveNextPage();
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            gridView5.MovePrevPage();
        }

        private void btnNext2_Click(object sender, EventArgs e)
        {
            gridView5.MoveNextPage();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

            PerformAdd();
            changeStatusButton(false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            PerformDelete();
            DisplayInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (PerformSave())
            {
                PerformPrintPreview();
                changeStatusButton(true);
                DisplayInfo();
                this.dgware_Hdbanhang_Chitiet.DataSource = null;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PerformPrintPreview();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            PerformCancel();
            //changeStatusButton(true);
            ShowTabPage(xtraTabControl1, xtraTabPageHh_Kh_Tra);
            DisplayInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            gridView3.MovePrevPage();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            gridView3.MoveNextPage();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            gridView1.MovePrevPage();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridView1.MoveNextPage();
        }

        private void btnHh_Kh_Tra_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPageHdbanhang_Chitiet)
            {
                ShowTabPage(xtraTabControl1, xtraTabPageHh_Kh_Tra);
                btnHh_Kh_Tra.Text = xtraTabPageHdbanhang_Chitiet.Text;
                btnDelete.Enabled = EnableDelete;
            }
            else
            {
                ShowTabPage(xtraTabControl1, xtraTabPageHdbanhang_Chitiet);
                btnHh_Kh_Tra.Text = xtraTabPageHh_Kh_Tra.Text;
                btnDelete.Enabled = false;
            }
        }

        private void dtNgay_Chungtu_View_EditValueChanged(object sender, EventArgs e)
        {
            if ("" + dtNgay_Chungtu_View.EditValue != "" && "" + lookUpEdit_Ten_Cuahang_Ban.EditValue != "")
            {
                lookUp_soHd.Properties.DataSource = objWareService.Get_All_Ware_Hdbanhang_ByDate(new DateTime(dtNgay_Chungtu_View.DateTime.Year, dtNgay_Chungtu_View.DateTime.Month, dtNgay_Chungtu_View.DateTime.Day, 0, 0, 1), new DateTime(dtNgay_Chungtu_View.DateTime.Year, dtNgay_Chungtu_View.DateTime.Month, dtNgay_Chungtu_View.DateTime.Day, 23, 23, 59), lookUpEdit_Ten_Cuahang_Ban.EditValue).ToDataSet().Tables[0];
                dgware_Hdbanhang_Chitiet.DataSource = null;
            }
        }

        private void lookUp_soHd_EditValueChanged(object sender, EventArgs e)
        {
            //if ("" + lookUp_soHd.EditValue != "")
            //{
            //    ds_Hdbanhang_Chitiet = objWareService.Get_All_Ware_Hdbanhang_Chitiet_ByHdbanhang_Sochungtu(lookUp_soHd.EditValue).ToDataSet();
            //    ds_Hdbanhang_Chitiet.Tables[0].Columns.Add("Soluong_Tra", typeof(double));
            //    for (int i = 0; i < ds_Hdbanhang_Chitiet.Tables[0].Rows.Count; i++)
            //    {
            //        ds_Hdbanhang_Chitiet.Tables[0].Rows[i]["Soluong_Tra"] =
            //            Convert.ToInt32(ds_Hdbanhang_Chitiet.Tables[0].Rows[i]["Soluong"])
            //            - Convert.ToInt32("0" + ds_Hdbanhang_Chitiet.Tables[0].Rows[i]["Soluong_Kh_Tra"]);
            //    }
            //    dgware_Hdbanhang_Chitiet.DataSource = ds_Hdbanhang_Chitiet.Tables[0];
            //    ds_Ware_Hh_Kh_Tra_Chitiet.Clear();
            //    if (ds_Hdbanhang_Chitiet.Tables[0].Rows.Count != 0)
            //    {
            //        ds_Hdbanhang = objWareService.Get_All_Ware_Hdbanhang().ToDataSet();
            //        lookUpEdit_Khachhang.EditValue = ds_Hdbanhang.Tables[0].Select("id_hdbanhang=" + ds_Hdbanhang_Chitiet.Tables[0].Rows[0]["Id_hdbanhang"])[0]["id_khachhang"];

            //    }
            //}
        }

        private void repositoryButtonSLTra_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString() == "")
            {
                gridView3.SetFocusedRowCellValue("Soluong_Tra", null);
                e.Cancel = true;
                return;
            }
            int soluong_mua = Convert.ToInt32(gridView3.GetFocusedRowCellValue("Soluong"));
            if (Convert.ToInt32(e.NewValue) > soluong_mua)
                e.Cancel = true;
        }

        private void lookUpEdit_Ten_Cuahang_Ban_EditValueChanged(object sender, EventArgs e)
        {
            if (this.FormState != GoobizFrame.Windows.Forms.FormState.View && lookUpEdit_Ten_Cuahang_Ban.EditValue != null)
            {
                lookUpEdit_Cuahang_Ban.EditValue = lookUpEdit_Ten_Cuahang_Ban.EditValue;
                if ("" + dtNgay_Chungtu_View.EditValue != "" && "" + lookUpEdit_Ten_Cuahang_Ban.EditValue != "")
                {
                    lookUp_soHd.Properties.DataSource = objWareService.Get_All_Ware_Hdbanhang_ByDate(new DateTime(dtNgay_Chungtu_View.DateTime.Year, dtNgay_Chungtu_View.DateTime.Month, dtNgay_Chungtu_View.DateTime.Day, 0, 0, 1), new DateTime(dtNgay_Chungtu_View.DateTime.Year, dtNgay_Chungtu_View.DateTime.Month, dtNgay_Chungtu_View.DateTime.Day, 23, 23, 59), lookUpEdit_Ten_Cuahang_Ban.EditValue).ToDataSet().Tables[0];
                    dgware_Hdbanhang_Chitiet.DataSource = null;
                }
            }
        }

        #endregion

        #region custom Method

        /// <summary>
        /// ShowTabPage
        /// </summary>
        /// <param name="xtraTabControl2">xtraTabControl</param>
        /// <param name="xtraTabPage">xtraTabPage</param>
        void ShowTabPage(DevExpress.XtraTab.XtraTabControl xtraTabControl,
                         DevExpress.XtraTab.XtraTabPage xtraTabPage)
        {

            while (xtraTabControl.TabPages.Count > 0)
                xtraTabControl.TabPages.RemoveAt(0);
            xtraTabControl.TabPages.Add(xtraTabPage);
        }

        void changeStatusButton(bool boo)
        {
            //btnAdd.Enabled = (boo) ? EnableAdd : false;
            //btnSave.Enabled = boo;
            //btnPrint.Enabled = (boo) ? EnablePrintPreview : false;
            //btnClose.Enabled = boo;
            //btnDelete.Enabled = (boo) ? EnableDelete : false;
            //btnSave.Enabled = !boo;
            //btnCancel.Enabled = !boo;
        }

        void Show_Hdbanhang_Chitiet_Dialog()
        {
            Frmware_Hdbanhang_Chitiet_Dialog frmware_Hdbanhang_Chitiet_Dialog = new Frmware_Hdbanhang_Chitiet_Dialog();
            frmware_Hdbanhang_Chitiet_Dialog.Text = "Chọn hàng hóa";
            frmware_Hdbanhang_Chitiet_Dialog.Id_Cuahang_Ban = lookUpEdit_Cuahang_Ban.EditValue;
            GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmware_Hdbanhang_Chitiet_Dialog);
            frmware_Hdbanhang_Chitiet_Dialog.ShowDialog();

            if (frmware_Hdbanhang_Chitiet_Dialog.SelectedRows != null
                && frmware_Hdbanhang_Chitiet_Dialog.SelectedRows.Length > 0)
            {
                for (int i = 0; i < frmware_Hdbanhang_Chitiet_Dialog.SelectedRows.Length; i++)
                {
                    DataRow nrow = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].NewRow();
                    nrow["Id_Hanghoa_Mua"] = frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[i]["Id_Hanghoa_Mua"];
                    nrow["Id_Hanghoa_Ban"] = frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[i]["Id_Hanghoa_Ban"];
                    nrow["Id_Donvitinh"] = frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[i]["Id_Donvitinh"];
                    nrow["Soluong"] = frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[i]["Soluong"];
                    nrow["Dongia"] = frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[i]["Dongia_Ban"];
                    nrow["Per_Dongia"] = frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[i]["Per_Dongia"];
                    nrow["Thanhtien"] = frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[i]["Thanhtien"];
                    nrow["Id_Hdbanhang_Chitiet"] = frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[i]["Id_Hdbanhang_Chitiet"];
                    nrow["Id_Hdbanhang"] = frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[i]["Id_Hdbanhang"];
                    ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows.Add(nrow);
                }
            }
        }

        void DisplayInfo2()
        {
            try
            {
                identity = gridView1.GetFocusedRowCellValue("Id_Hh_Kh_Tra");
                if (identity == null)
                    identity = 0;
                this.ds_Ware_Hh_Kh_Tra_Chitiet = objWareService.Get_All_Ware_Hh_Kh_Tra_Chitiet_ByHh_Kh_Tra(identity).ToDataSet();
                this.dgware_Hh_Kh_Tra_Chitiet.DataSource = ds_Ware_Hh_Kh_Tra_Chitiet;
                this.dgware_Hh_Kh_Tra_Chitiet.DataMember = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].TableName;
                gridView5.BestFitColumns();
            }
            catch
            {
            }
        }

        #endregion

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {

        }

        private void dgware_Hh_Kh_Tra_Click(object sender, EventArgs e)
        {

        }

        private void btn_Fill_Donhang_Click(object sender, EventArgs e)
        {
            if ("" + lookUp_soHd.EditValue != "")
            {
                ds_Hdbanhang_Chitiet = objWareService.Get_All_Ware_Hdbanhang_Chitiet_ByHdbanhang_Sochungtu(lookUp_soHd.EditValue).ToDataSet();
                ds_Hdbanhang_Chitiet.Tables[0].Columns.Add("Soluong_Tra", typeof(double));
                for (int i = 0; i < ds_Hdbanhang_Chitiet.Tables[0].Rows.Count; i++)
                {
                    ds_Hdbanhang_Chitiet.Tables[0].Rows[i]["Soluong_Tra"] =
                        Convert.ToInt32(ds_Hdbanhang_Chitiet.Tables[0].Rows[i]["Soluong"])
                        - Convert.ToInt32("0" + ds_Hdbanhang_Chitiet.Tables[0].Rows[i]["Soluong_Kh_Tra"]);
                }
                dgware_Hdbanhang_Chitiet.DataSource = ds_Hdbanhang_Chitiet.Tables[0];
                ds_Ware_Hh_Kh_Tra_Chitiet.Clear();
                if (ds_Hdbanhang_Chitiet.Tables[0].Rows.Count != 0)
                {
                    ds_Hdbanhang = objWareService.Get_All_Ware_Hdbanhang().ToDataSet();
                    lookUpEdit_Khachhang.EditValue = ds_Hdbanhang.Tables[0].Select("id_hdbanhang=" + ds_Hdbanhang_Chitiet.Tables[0].Rows[0]["Id_hdbanhang"])[0]["id_khachhang"];

                }
            }
        }

        private void btn_Fill_Phieu_Tra_Click(object sender, EventArgs e)
        {
            Reload_Phieutra();
        }

        void Reload_Phieutra()
        {
            ds_Ware_Hh_Kh_Tra = objWareService.Get_All_Ware_Hh_Kh_Tra_SelectBy_Date(dt_Ngay_LapPhieu_Tra.EditValue).ToDataSet();
            dgware_Hh_Kh_Tra.DataSource = ds_Ware_Hh_Kh_Tra;
            dgware_Hh_Kh_Tra.DataMember = ds_Ware_Hh_Kh_Tra.Tables[0].TableName;
        }

    }
}



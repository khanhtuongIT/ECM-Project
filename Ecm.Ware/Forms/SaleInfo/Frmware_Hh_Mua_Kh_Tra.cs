using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SunLine.Ware.Forms
{
    public partial class Frmware_Hh_Mua_Kh_Tra : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public WareService.WareService objWareService = new SunLine.Ware.WareService.WareService();
        public RexService.RexService objRexService = new SunLine.Ware.RexService.RexService();
        public MasterService.MasterService objMasterService = new SunLine.Ware.MasterService.MasterService();

        DataSet ds_Ware_Hh_Kh_Tra = new DataSet();
        DataSet ds_Ware_Hh_Kh_Tra_Chitiet = new DataSet();
        DataSet ds_Hanghoa_Mua;
        DataSet ds_Hdbanhang;
        DataSet ds_Hdbanhang_Chitiet;
        DataSet ds_Khachhang;
        DataSet ds_Nhansu;
        object identity;
        object LocationId_Kho_Hanghoa_Mua;

        #region local data
        bool Exists_Sys_Lognotify_Path = false;
        DataSet dsSys_Lognotify = null;

        string log_WARE_DM_HANGHOA_MUA = "init";
        string log_WARE_HANGHOA_DINHGIA = "init";
        string log_WARE_HH_KHO_HANGHOA_MUA = "init";
        string log_REX_NHANSU = "init";
        string log_WARE_DM_KHACHHANG = "init";

        string Sys_Lognotify_Path = @"Resources\localdata\SYS_LOGNOTIFY.xml";
        string xml_WARE_DM_LOAI_HANGHOA_MUA = @"Resources\localdata\WARE_DM_LOAI_HANGHOA_MUA_BYLOCATION.xml";
        string xml_WARE_DM_HANGHOA_MUA = @"Resources\localdata\WARE_DM_HANGHOA_MUA_BYLOCATION.xml";
        string xml_REX_NHANSU = @"Resources\localdata\REX_NHANSU.xml";
        string xml_WARE_DM_KHACHHANG = @"Resources\localdata\WARE_DM_KHACHHANG.xml";
        #endregion

        public Frmware_Hh_Mua_Kh_Tra()
        {
            InitializeComponent();
            if (!System.IO.Directory.Exists(@"Resources\localdata"))
                System.IO.Directory.CreateDirectory(@"Resources\localdata");

            //date mask
            dtNgay_Chungtu.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtNgay_Chungtu.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();

            repositoryItemDateEdit_Ngay_Nhap_Hh_Mua.Properties.Mask.UseMaskAsDisplayFormat = true;
            repositoryItemDateEdit_Ngay_Nhap_Hh_Mua.Properties.Mask.EditMask = GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();
            //reset lookup edit as delete value
            lookUpEdit_Khachhang.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);
            lookUpEdit_Kho_Hanghoa_Mua.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);
            lookUpEdit_Nhansu_Lap.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);

            LocationId_Kho_Hanghoa_Mua = GoobizFrame.Windows.MdiUtils.ThemeSettings.GetLocationId_Kho_Hanghoa_Mua();
            lookUpEdit_Kho_Hanghoa_Mua.EditValue = LocationId_Kho_Hanghoa_Mua;
            lookUpEditMa_Kho_Hanghoa.EditValue = LocationId_Kho_Hanghoa_Mua;
            //if ("" + lookUpEdit_Kho_Hanghoa_Mua.EditValue != "")
            //    lookUpEdit_Kho_Hanghoa_Mua.Properties.ReadOnly = true;
            this.item_Edit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.DisplayInfo();
        }

        void GetDmHanghoaMua_ByKhoHanghoaMua()
        {
            string vlog_WARE_DM_HANGHOA_MUA = log_WARE_DM_HANGHOA_MUA;
            string vlog_WARE_HANGHOA_DINHGIA = log_WARE_HANGHOA_DINHGIA;
            string vlog_WARE_HH_KHO_HANGHOA_MUA = log_WARE_HH_KHO_HANGHOA_MUA;
            string vlog_REX_NHANSU = log_REX_NHANSU;
            string vlog_WARE_DM_KHACHHANG = log_WARE_DM_KHACHHANG;

            Exists_Sys_Lognotify_Path = System.IO.File.Exists(Sys_Lognotify_Path);
            if (Exists_Sys_Lognotify_Path)
            {
                //get last change at local
                dsSys_Lognotify = new DataSet();
                dsSys_Lognotify.ReadXml(Sys_Lognotify_Path);

                //write new log change from database --> write to local last change
                DataSet dsSys_Lognotify_db = objMasterService.GetAll_Sys_Lognotify();
                bool haschange_atlast = false;
                foreach (DataRow dr in dsSys_Lognotify_db.Tables[0].Rows)
                {
                    DataRow[] sdr = dsSys_Lognotify.Tables[0].Select("Table_Name = '" + dr["table_name"] + "'");
                    if (sdr == null || sdr.Length == 0)
                        haschange_atlast = true;
                    else if ("" + sdr[0]["Last_Change"] != "" + dr["Last_Change"])
                        haschange_atlast = true;

                    if (haschange_atlast) break;
                }

                if (haschange_atlast)
                {
                    dsSys_Lognotify_db.WriteXml(Sys_Lognotify_Path, XmlWriteMode.WriteSchema);

                    log_WARE_DM_HANGHOA_MUA = (dsSys_Lognotify.Tables[0].Select("Table_Name='WARE_DM_HANGHOA_MUA'").Length > 0)
                    ? "" + dsSys_Lognotify.Tables[0].Select("Table_Name='WARE_DM_HANGHOA_MUA'")[0]["Last_Change"] : "";
                    log_WARE_HANGHOA_DINHGIA = (dsSys_Lognotify.Tables[0].Select("Table_Name='WARE_HANGHOA_DINHGIA'").Length > 0)
                        ? "" + dsSys_Lognotify.Tables[0].Select("Table_Name='WARE_HANGHOA_DINHGIA'")[0]["Last_Change"] : "";
                    log_WARE_HH_KHO_HANGHOA_MUA = (dsSys_Lognotify.Tables[0].Select("Table_Name='WARE_HH_KHO_HANGHOA_MUA'").Length > 0)
                        ? "" + dsSys_Lognotify.Tables[0].Select("Table_Name='WARE_HH_KHO_HANGHOA_MUA'")[0]["Last_Change"] : "";
                    log_REX_NHANSU = (dsSys_Lognotify.Tables[0].Select("Table_Name='REX_NHANSU'").Length > 0)
                        ? "" + dsSys_Lognotify.Tables[0].Select("Table_Name='REX_NHANSU'")[0]["Last_Change"] : "";
                    log_WARE_DM_KHACHHANG = (dsSys_Lognotify.Tables[0].Select("Table_Name='WARE_DM_KHACHHANG'").Length > 0)
                        ? "" + dsSys_Lognotify.Tables[0].Select("Table_Name='WARE_DM_KHACHHANG'")[0]["Last_Change"] : "";

                    vlog_WARE_DM_HANGHOA_MUA = (dsSys_Lognotify_db.Tables[0].Select("Table_Name='WARE_DM_HANGHOA_MUA'").Length > 0)
                        ? "" + dsSys_Lognotify_db.Tables[0].Select("Table_Name='WARE_DM_HANGHOA_MUA'")[0]["Last_Change"] : "";
                    vlog_WARE_HANGHOA_DINHGIA = (dsSys_Lognotify_db.Tables[0].Select("Table_Name='WARE_HANGHOA_DINHGIA'").Length > 0)
                        ? "" + dsSys_Lognotify_db.Tables[0].Select("Table_Name='WARE_HANGHOA_DINHGIA'")[0]["Last_Change"] : "";
                    vlog_WARE_HH_KHO_HANGHOA_MUA = (dsSys_Lognotify.Tables[0].Select("Table_Name='WARE_HH_KHO_HANGHOA_MUA'").Length > 0)
                        ? "" + dsSys_Lognotify.Tables[0].Select("Table_Name='WARE_HH_KHO_HANGHOA_MUA'")[0]["Last_Change"] : "";
                    vlog_REX_NHANSU = (dsSys_Lognotify_db.Tables[0].Select("Table_Name='REX_NHANSU'").Length > 0)
                        ? "" + dsSys_Lognotify_db.Tables[0].Select("Table_Name='REX_NHANSU'")[0]["Last_Change"] : "";
                    vlog_WARE_DM_KHACHHANG = (dsSys_Lognotify_db.Tables[0].Select("Table_Name='WARE_DM_KHACHHANG'").Length > 0)
                        ? "" + dsSys_Lognotify_db.Tables[0].Select("Table_Name='WARE_DM_KHACHHANG'")[0]["Last_Change"] : "";

                }
            }
            else
            {
                dsSys_Lognotify = new DataSet();
                dsSys_Lognotify = objMasterService.GetAll_Sys_Lognotify();
                dsSys_Lognotify.WriteXml(Sys_Lognotify_Path, XmlWriteMode.WriteSchema);
            }

            //load data from local xml when last change at local differ from database
            if (vlog_WARE_DM_HANGHOA_MUA + vlog_WARE_HANGHOA_DINHGIA + vlog_WARE_HH_KHO_HANGHOA_MUA
                != log_WARE_DM_HANGHOA_MUA + log_WARE_HANGHOA_DINHGIA + log_WARE_HH_KHO_HANGHOA_MUA
                || !System.IO.File.Exists(xml_WARE_DM_HANGHOA_MUA))
            {
                ds_Hanghoa_Mua = objMasterService.Get_All_Ware_Dm_Hanghoa_MuaBy_Id_Kho_Hh_Mua(LocationId_Kho_Hanghoa_Mua, DateTime.Now);
                ds_Hanghoa_Mua.WriteXml(xml_WARE_DM_HANGHOA_MUA, XmlWriteMode.WriteSchema);

            }
            else if (ds_Hanghoa_Mua == null || ds_Hanghoa_Mua.Tables.Count == 0)
            {
                ds_Hanghoa_Mua = new DataSet();
                ds_Hanghoa_Mua.ReadXml(xml_WARE_DM_HANGHOA_MUA);

            }

            if (vlog_WARE_DM_KHACHHANG != log_WARE_DM_KHACHHANG || !System.IO.File.Exists(xml_WARE_DM_KHACHHANG))
            {
                ds_Khachhang = objMasterService.Get_All_Ware_Dm_Khachhang();
                ds_Khachhang.WriteXml(xml_WARE_DM_KHACHHANG, XmlWriteMode.WriteSchema);
            }
            else if (ds_Khachhang == null || ds_Khachhang.Tables.Count == 0)
            {
                ds_Khachhang = new DataSet();
                ds_Khachhang.ReadXml(xml_WARE_DM_KHACHHANG);
            }


            if (vlog_REX_NHANSU != log_REX_NHANSU || !System.IO.File.Exists(xml_REX_NHANSU))
            {
                ds_Nhansu = objRexService.Get_All_Rex_Nhansu_Collection();
                ds_Nhansu.WriteXml(xml_REX_NHANSU, XmlWriteMode.WriteSchema);
            }
            else if (ds_Nhansu == null || ds_Nhansu.Tables.Count == 0)
            {
                ds_Nhansu = new DataSet();
                ds_Nhansu.ReadXml(xml_REX_NHANSU);
            }

            //dgware_Dm_Hanghoa_Mua          
            gridLookUpEdit_Hanghoa_Mua.DataSource = ds_Hanghoa_Mua.Tables[0];
            gridLookUpEdit_Ma_Hanghoa_Mua.DataSource = ds_Hanghoa_Mua.Tables[0];

            //khach hang
            lookUpEdit_Khachhang.Properties.DataSource = ds_Khachhang.Tables[0];

            //Get data Rex_Nhansu
            lookUpEdit_Nhansu_Lap.Properties.DataSource = ds_Nhansu.Tables[0];
            lookUpEdit_Nhansu_Lap.EditValue = System.Convert.ToInt64(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu());

        }

        public override void DisplayInfo()
        {
            try
            {
                GetDmHanghoaMua_ByKhoHanghoaMua();

                //Get data Ware_Dm_Kho_Hanghoa_Mua
                DataSet dsKho_Hanghoa_Mua = objMasterService.Get_All_Ware_Dm_Kho_Hanghoa_Mua();
                lookUpEdit_Kho_Hanghoa_Mua.Properties.DataSource = dsKho_Hanghoa_Mua.Tables[0];
                lookUpEditMa_Kho_Hanghoa.Properties.DataSource = dsKho_Hanghoa_Mua.Tables[0];

                //Get data Rex_Nhansu
                lookUpEdit_Nhansu_Lap.EditValue = Convert.ToInt64(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu());

                //Get data Ware_Dm_Donvitinh
                gridLookUpEdit_Donvitinh.DataSource = objMasterService.Get_All_Ware_Dm_Donvitinh().Tables[0];

                GetWare_Hh_Kh_Tra();
                this.ChangeStatus(false);

                this.gridView1.BestFitColumns();

                //reset ds_Hdbanhang_Chitiet
                txtHd_Sochungtu.EditValue = null;
                ds_Hdbanhang_Chitiet = null;
                dgware_Hdbanhang_Chitiet.DataSource = null;

                //GetWare_Hh_Kh_Tra();
                DisplayInfo2();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif

                ////GoobizFrame.Windows.HelperClasses.ExceptionLogger.LogException1(ex);
            }

        }

        void GetWare_Hh_Kh_Tra()
        {
            //Get data Ware_Nhap_Hh_Ban
            ds_Ware_Hh_Kh_Tra = objWareService.Get_All_Ware_Hh_Kh_Tra_Hhmua_ByKhoHanghoa(lookUpEditMa_Kho_Hanghoa.EditValue);
            dgware_Hh_Kh_Tra.DataSource = ds_Ware_Hh_Kh_Tra;
            dgware_Hh_Kh_Tra.DataMember = ds_Ware_Hh_Kh_Tra.Tables[0].TableName;
            gridView1.Columns["Ngay_Chungtu"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(
                gridView1.Columns["Ngay_Chungtu"],
                dtNgay_Chungtu_View.DateTime);
            DataBindingControl();
        }

        void ClearDataBindings()
        {
            this.txtId_Hh_Kh_Tra.DataBindings.Clear();
            this.txtSochungtu.DataBindings.Clear();
            this.txtGhichu.DataBindings.Clear();
            this.dtNgay_Chungtu.DataBindings.Clear();

            //this.lookUpEdit_Kho_Hanghoa_Mua.DataBindings.Clear();
            this.lookUpEdit_Nhansu_Lap.DataBindings.Clear();
            this.lookUpEdit_Khachhang.DataBindings.Clear();
        }

        public void DataBindingControl()
        {
            try
            {
                ClearDataBindings();

                this.txtId_Hh_Kh_Tra.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Id_Hh_Kh_Tra");
                this.txtSochungtu.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Sochungtu");
                this.txtGhichu.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Ghichu");
                this.dtNgay_Chungtu.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Ngay_Chungtu");

                //this.lookUpEdit_Kho_Hanghoa_Mua.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Id_Kho_Hanghoa_Mua");
                this.lookUpEdit_Nhansu_Lap.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Id_Nhansu_Lap");
                this.lookUpEdit_Khachhang.DataBindings.Add("EditValue", ds_Ware_Hh_Kh_Tra, ds_Ware_Hh_Kh_Tra.Tables[0].TableName + ".Id_Khachhang");
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif

                ////GoobizFrame.Windows.HelperClasses.ExceptionLogger.LogException1(ex);
            }
        }

        public void ChangeStatus(bool editTable)
        {
            this.dgware_Hh_Kh_Tra.Enabled = !editTable;
            this.txtSochungtu.Properties.ReadOnly = !editTable;
            this.txtGhichu.Properties.ReadOnly = !editTable;
            this.txtHd_Sochungtu.Properties.ReadOnly = !editTable;
            this.dtNgay_Chungtu.Properties.ReadOnly = !editTable;
            //this.dtNgay_Chungtu_View.Properties.ReadOnly = !editTable;
            this.txtHd_Sochungtu.Properties.ReadOnly = !editTable;

            this.lookUpEdit_Nhansu_Lap.Properties.ReadOnly = !editTable;
            this.lookUpEdit_Kho_Hanghoa_Mua.Properties.ReadOnly = true;
            this.lookUpEdit_Khachhang.Properties.ReadOnly = !editTable;

            this.dgware_Hh_Kh_Tra_Chitiet.EmbeddedNavigator.Enabled = editTable;
            this.gridView5.OptionsBehavior.Editable = editTable;

            //xtraTabPage4.PageEnabled = editTable;
        }

        public void ResetText()
        {
            this.txtId_Hh_Kh_Tra.EditValue = "";
            this.txtSochungtu.EditValue = "";
            this.txtGhichu.EditValue = "";
            //this.lookUpEdit_Kho_Hanghoa_Mua.EditValue = "";
            this.lookUpEdit_Khachhang.EditValue = "";

            this.ds_Ware_Hh_Kh_Tra_Chitiet = objWareService.Get_All_Ware_Hh_Kh_Tra_Chitiet_ByHh_Kh_Tra(0);
            this.dgware_Hh_Kh_Tra_Chitiet.DataSource = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0];

            this.txtHd_Sochungtu.EditValue = "";
            dgware_Hdbanhang_Chitiet.DataSource = null;
        }

        void DisplayInfo2()
        {
            try
            {
                identity = gridView1.GetFocusedRowCellValue("Id_Hh_Kh_Tra");

                this.ds_Ware_Hh_Kh_Tra_Chitiet = objWareService.Get_All_Ware_Hh_Kh_Tra_Chitiet_ByHh_Kh_Tra(identity);
                this.dgware_Hh_Kh_Tra_Chitiet.DataSource = ds_Ware_Hh_Kh_Tra_Chitiet;
                this.dgware_Hh_Kh_Tra_Chitiet.DataMember = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].TableName;

                gridView5.BestFitColumns();
            }
            catch { }
        }

        private void gridView5_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "Soluong" || e.Column.FieldName == "Dongia")
                {
                    if ("" + gridView5.GetFocusedRowCellValue("Soluong") != ""
                        && "" + gridView5.GetFocusedRowCellValue("Dongia") != "")
                        gridView5.SetFocusedRowCellValue(gridView5.Columns["Thanhtien"], Convert.ToDecimal(gridView5.GetFocusedRowCellValue("Soluong"))
                                                                     * Convert.ToDecimal(gridView5.GetFocusedRowCellValue("Dongia"))
                                                                      * (1 - Convert.ToDecimal("0" + gridView5.GetFocusedRowCellValue("Per_Dongia")) / 100)
                                                        );
                }
                else if (e.Column.FieldName == "Id_Hanghoa_Mua")
                {
                    gridView5.SetFocusedRowCellValue(gridView5.Columns["Id_Donvitinh"]
                        , ((System.Data.DataRowView)gridLookUpEdit_Hanghoa_Mua.GetDataSourceRowByKeyValue(e.Value))["Id_Donvitinh"]);
                    //gridView5.SetFocusedRowCellValue(gridView5.Columns["Dongia"]
                    //, ((System.Data.DataRowView)gridLookUpEdit_Hanghoa_Mua.GetDataSourceRowByKeyValue(e.Value))["Dongia"]);
                }
            }
            catch (Exception ex)
            { }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                DisplayInfo2();
            }
        }

        #region Event Override
        public object InsertObject()
        {
            try
            {
                WareService.Ware_Hh_Kh_Tra objWare_Hh_Kh_Tra = new SunLine.Ware.WareService.Ware_Hh_Kh_Tra();
                objWare_Hh_Kh_Tra.Id_Hh_Kh_Tra = -1;
                objWare_Hh_Kh_Tra.Sochungtu = txtSochungtu.EditValue;
                objWare_Hh_Kh_Tra.Ghichu = txtGhichu.EditValue;
                objWare_Hh_Kh_Tra.Ngay_Chungtu = dtNgay_Chungtu.EditValue;

                if ("" + lookUpEdit_Kho_Hanghoa_Mua.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Kho_Hanghoa_Mua = lookUpEdit_Kho_Hanghoa_Mua.EditValue;
                if ("" + lookUpEdit_Khachhang.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Khachhang = lookUpEdit_Khachhang.EditValue;
                if ("" + lookUpEdit_Nhansu_Lap.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Nhansu_Lap = lookUpEdit_Nhansu_Lap.EditValue;

                identity = objWareService.Insert_Ware_Hh_Kh_Tra(objWare_Hh_Kh_Tra);

                if (identity != null)
                {
                    dgware_Hh_Kh_Tra_Chitiet.EmbeddedNavigator.Buttons.EndEdit.DoClick();
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
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public object UpdateObject()
        {
            try
            {
                WareService.Ware_Hh_Kh_Tra objWare_Hh_Kh_Tra = new SunLine.Ware.WareService.Ware_Hh_Kh_Tra();
                objWare_Hh_Kh_Tra.Id_Hh_Kh_Tra = identity;
                objWare_Hh_Kh_Tra.Sochungtu = txtSochungtu.EditValue;
                objWare_Hh_Kh_Tra.Ghichu = txtGhichu.EditValue;
                objWare_Hh_Kh_Tra.Ngay_Chungtu = dtNgay_Chungtu.EditValue;

                if ("" + lookUpEdit_Kho_Hanghoa_Mua.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Kho_Hanghoa_Mua = lookUpEdit_Kho_Hanghoa_Mua.EditValue;
                if ("" + lookUpEdit_Khachhang.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Khachhang = lookUpEdit_Khachhang.EditValue;
                if ("" + lookUpEdit_Nhansu_Lap.EditValue != "")
                    objWare_Hh_Kh_Tra.Id_Nhansu_Lap = lookUpEdit_Nhansu_Lap.EditValue;
                //update nhap_hh_mua
                objWareService.Update_Ware_Hh_Kh_Tra(objWare_Hh_Kh_Tra);

                //update nhap_hh_mua_chitiet
                dgware_Hh_Kh_Tra_Chitiet.EmbeddedNavigator.Buttons.EndEdit.DoClick();
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
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public object DeleteObject()
        {
            WareService.Ware_Hh_Kh_Tra objWare_Hh_Kh_Tra = new SunLine.Ware.WareService.Ware_Hh_Kh_Tra();
            objWare_Hh_Kh_Tra.Id_Hh_Kh_Tra = gridView1.GetFocusedRowCellValue("Id_Hh_Kh_Tra");

            return objWareService.Delete_Ware_Hh_Kh_Tra(objWare_Hh_Kh_Tra);
        }

        public override bool PerformAdd()
        {
            dtNgay_Chungtu.EditValue = DateTime.Now;
            //Kiểm tra nếu nhân viên login không tồn tại trong kho hàng hóa mua thì access denied.
            lookUpEdit_Nhansu_Lap.EditValue = Convert.ToInt64(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu());
            lookUpEdit_Kho_Hanghoa_Mua.EditValue = LocationId_Kho_Hanghoa_Mua;
            DataSet ds_Kho_Hanghoa = objMasterService.Get_All_Ware_Dm_Kho_Hanghoa_MuaBy_Id_Nhansu(lookUpEdit_Nhansu_Lap.EditValue);
            if (ds_Kho_Hanghoa.Tables[0].Rows.Count == 0 || ds_Kho_Hanghoa.Tables[0].Select("Id_Kho_Hanghoa_Mua = " + LocationId_Kho_Hanghoa_Mua) == null)
            {
                GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                lookUpEdit_Nhansu_Lap.EditValue = null;
                return false;
            }

            ClearDataBindings();
            this.ChangeStatus(true);
            this.ResetText();
            lookUpEdit_Nhansu_Lap.EditValue = Convert.ToInt64(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu());
            txtSochungtu.EditValue = objWareService.GetNew_Sochungtu("Ware_Hh_Kh_Tra", "Sochungtu", "RE-" + lookUpEdit_Kho_Hanghoa_Mua.GetColumnValue("Ma_Kho_Hanghoa_Mua") + "-");

            //DataRow nrow = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].NewRow();
            //ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows.Add(nrow);
            //gridView5.Focus();
            //gridView5.FocusedRowHandle = 0;

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
            this.ChangeStatus(false);
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
                bool success = false;

                System.Collections.Hashtable hashtableControls = new System.Collections.Hashtable();
                hashtableControls.Add(txtSochungtu, lblSochungtu.Text);
                hashtableControls.Add(lookUpEdit_Kho_Hanghoa_Mua, lblKho_Hanghoa_Mua.Text);
                hashtableControls.Add(lookUpEdit_Nhansu_Lap, lblNhansu_Lap.Text);

                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;

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
            WareService.Ware_Hh_Kh_Tra ware_Hh_Kh_Tra = new SunLine.Ware.WareService.Ware_Hh_Kh_Tra();
            try
            {
                int focusedRow = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
                DataRow dr = ds_Ware_Hh_Kh_Tra.Tables[0].Rows[focusedRow];
                if (dr != null)
                {
                    ware_Hh_Kh_Tra.Id_Hh_Kh_Tra = dr["Id_Hh_Kh_Tra"];
                    ware_Hh_Kh_Tra.Sochungtu = dr["Sochungtu"];
                    ware_Hh_Kh_Tra.Ngay_Chungtu = dr["Ngay_Chungtu"];
                    ware_Hh_Kh_Tra.Id_Kho_Hanghoa_Mua = dr["Id_Kho_Hanghoa_Mua"];
                    ware_Hh_Kh_Tra.Id_Nhansu_Lap = dr["Id_Nhansu_Lap"];
                    ware_Hh_Kh_Tra.Ghichu = dr["Ghichu"];
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
            DataRow[] sdr = ds_Ware_Hh_Kh_Tra.Tables[0].Select("Id_Hh_Kh_Tra = " + identity);

            DataSets.dsHdbanhang_Chitiet dsrHdbanhang_Chitiet = new SunLine.Ware.DataSets.dsHdbanhang_Chitiet();
            Reports.rptWare_Hh_Ban_Kh_Tra rptHdbanhang_noVAT = new SunLine.Ware.Reports.rptWare_Hh_Ban_Kh_Tra();
            GoobizFrame.Windows.Forms.FormReportWithHeader objFormReport = new GoobizFrame.Windows.Forms.FormReportWithHeader();
            objFormReport.Report = rptHdbanhang_noVAT;
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

                drnew["Ma_Hanghoa"] = //ds_Hdbanhang.Tables[0].Select("Id_Hdbanhang = " + dr["Id_Hdbanhang"])[0]["Sochungtu"] + " * " + 
                    ds_Hanghoa_Mua.Tables[0].Select("Id_Hanghoa_Mua=" + dr["Id_Hanghoa_Mua"])[0]["Ma_Hanghoa_Mua"] + " * " +
                    ds_Hanghoa_Mua.Tables[0].Select("Id_Hanghoa_Mua=" + dr["Id_Hanghoa_Mua"])[0]["Ten_Hanghoa_Mua"];
                drnew["Dongia_Ban"] = dr["Dongia"];
                drnew["Stt"] = i++;

                dsrHdbanhang_Chitiet.Tables[0].Rows.Add(drnew);
            }

            //add parameter values
            rptHdbanhang_noVAT.tbc_Ngay.Text = "" + string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", sdr[0]["Ngay_Chungtu"]);
            rptHdbanhang_noVAT.lblNhansu_Order.Text = lookUpEdit_Nhansu_Lap.Text;
            rptHdbanhang_noVAT.tbcSochungtu.Text = "" + sdr[0]["Sochungtu"];

            double thanhtien = Convert.ToDouble(gridView5.Columns["Thanhtien"].SummaryItem.SummaryValue);
            string str = GoobizFrame.Windows.HelperClasses.ReadNumber.ChangeNum2VNStr(thanhtien, " đồng.");
            str = str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();

            //rptHdbanhang_noVAT.tbcThanhtien_Bangchu.Text = str;

            rptHdbanhang_noVAT.PageSize = new Size(800, 1400 + 120 * Convert.ToInt32(dsrHdbanhang_Chitiet.Tables[0].Rows.Count));

            rptHdbanhang_noVAT.CreateDocument();

            if (!GoobizFrame.Windows.MdiUtils.ThemeSettings.GetPrintpreview("rptHdbanhang_noVAT"))
            {
                rptHdbanhang_noVAT.Print();
            }
            else
            {
                objFormReport.printControl1.PrintingSystem = rptHdbanhang_noVAT.PrintingSystem;
                objFormReport.MdiParent = this.MdiParent;
                objFormReport.Text = this.Text + "(Xem trang in)";
                objFormReport.Show();
                objFormReport.Activate();
            }
            return base.PerformPrintPreview();
        }
        #endregion

        private void lookUpEdit_Khachhang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                SunLine.MasterTables.Forms.Ware.Frmware_Dm_Khachhang_Add objFrmware_Dm_Khachhang_Add = new SunLine.MasterTables.Forms.Ware.Frmware_Dm_Khachhang_Add();
                objFrmware_Dm_Khachhang_Add.ShowDialog();
                if (objFrmware_Dm_Khachhang_Add.Selected_Ware_Dm_Khachhang != null)
                {
                    lookUpEdit_Khachhang.EditValue = objFrmware_Dm_Khachhang_Add.Selected_Ware_Dm_Khachhang.Id_Khachhang;
                }
            }
        }

        private void lookUpEdit_Kho_Hanghoa_Mua_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    gridLookUpEdit_Hanghoa_Mua.DataSource = objMasterService.Get_All_Ware_Dm_Hanghoa_MuaBy_Id_Kho_Hh_Mua(lookUpEdit_Kho_Hanghoa_Mua.EditValue, dtNgay_Chungtu.EditValue).Tables[0];
            //}
            //catch { }
        }

        private void gridLookUpEdit_Hanghoa_Mua_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

        void Show_Hdbanhang_Chitiet_Dialog()
        {
            Frmware_Hdbanhang_Chitiet_Dialog frmware_Hdbanhang_Chitiet_Dialog = new Frmware_Hdbanhang_Chitiet_Dialog();
            frmware_Hdbanhang_Chitiet_Dialog.Text = "Chọn hàng hóa";
            frmware_Hdbanhang_Chitiet_Dialog.Id_Kho_Hanghoa_Mua = lookUpEdit_Kho_Hanghoa_Mua.EditValue;
            GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmware_Hdbanhang_Chitiet_Dialog);
            frmware_Hdbanhang_Chitiet_Dialog.ShowDialog();

            if (frmware_Hdbanhang_Chitiet_Dialog.SelectedRows != null
                && frmware_Hdbanhang_Chitiet_Dialog.SelectedRows.Length > 0)
            {
                //gridLookUpEdit_Hanghoa_Mua.DataSource = frmware_Dm_Hanghoa_Mua_Dialog.Data.Tables[0];

                //gridView5.SetFocusedRowCellValue(gridView5.Columns["Id_Hanghoa_Mua"]
                //    , frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[0]["Id_Hanghoa_Mua"]);
                //gridView5.SetFocusedRowCellValue(gridView5.Columns["Id_Hanghoa_Ban"]
                //    , frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[0]["Id_Hanghoa_Ban"]);
                //gridView5.SetFocusedRowCellValue(gridView5.Columns["Id_Donvitinh"]
                //    , frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[0]["Id_Donvitinh"]);
                //gridView5.SetFocusedRowCellValue(gridView5.Columns["Soluong"]
                //    , frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[0]["Soluong"]);
                //gridView5.SetFocusedRowCellValue(gridView5.Columns["Dongia"]
                //    , frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[0]["Dongia_Ban"]);
                //gridView5.SetFocusedRowCellValue(gridView5.Columns["Per_Dongia"]
                //    , frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[0]["Per_Dongia"]);
                //gridView5.SetFocusedRowCellValue(gridView5.Columns["Thanhtien"]
                //    , frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[0]["Thanhtien"]);
                //gridView5.SetFocusedRowCellValue(gridView5.Columns["Id_Hdbanhang_Chitiet"]
                //    , frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[0]["Id_Hdbanhang_Chitiet"]);
                //gridView5.SetFocusedRowCellValue(gridView5.Columns["Id_Hdbanhang"]
                //    , frmware_Hdbanhang_Chitiet_Dialog.SelectedRows[0]["Id_Hdbanhang"]);

                //object Id_Cuahang_Mua = gridView1.GetFocusedRowCellValue("Id_Cuahang_Mua");
                if (frmware_Hdbanhang_Chitiet_Dialog.SelectedRows.Length > 0)
                {
                    for (int i = 0; i < frmware_Hdbanhang_Chitiet_Dialog.SelectedRows.Length; i++)
                    {
                        DataRow nrow = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].NewRow();
                        //nrow["Id_Cuahang_Mua"] = Id_Cuahang_Mua;
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
        }

        private void dgware_Dm_Hanghoa_Mua_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {

        }

        #region dgware_Dm_Hanghoa_Mua

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //cardView1.MovePrevPage();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //cardView1.MoveNextPage();
        }


        private void btnAccept_Click(object sender, EventArgs e)
        {
            DataRow[] sdrDm_Hanghoa_Mua = ds_Hdbanhang_Chitiet.Tables[0].Select("Soluong_Tra > 0");
            foreach (DataRow drDm_Hanghoa_Mua in sdrDm_Hanghoa_Mua)
            {
                DataRow nrow = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].NewRow();
                nrow["Id_Hdbanhang"] = drDm_Hanghoa_Mua["Id_Hdbanhang"];
                nrow["Id_Hanghoa_Mua"] = drDm_Hanghoa_Mua["Id_Hanghoa_Mua"];
                nrow["Id_Donvitinh"] = drDm_Hanghoa_Mua["Id_Donvitinh"];
                nrow["Soluong"] = drDm_Hanghoa_Mua["Soluong_Tra"];
                nrow["Dongia"] = drDm_Hanghoa_Mua["Dongia_Ban"];
                nrow["Per_Dongia"] = drDm_Hanghoa_Mua["Per_Dongia"];
                nrow["Thanhtien"] = Convert.ToDecimal(nrow["Soluong"])
                                    * Convert.ToDecimal(nrow["Dongia"])
                                    * (1 - Convert.ToDecimal("0" + nrow["Per_Dongia"]) / 100);
                ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows.Add(nrow);
            }

            ds_Hdbanhang_Chitiet.RejectChanges();
        }

        private void cardView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.FieldName == "Soluong" || e.Column.FieldName == "Dongia")
            //{
            //    if ("" + cardView1.GetFocusedRowCellValue("Soluong") != ""
            //        && "" + cardView1.GetFocusedRowCellValue("Dongia") != "")
            //        cardView1.SetFocusedRowCellValue(
            //            cardView1.Columns["Thanhtien"]
            //            , Convert.ToDecimal(cardView1.GetFocusedRowCellValue("Soluong"))
            //                * Convert.ToDecimal(cardView1.GetFocusedRowCellValue("Dongia"))
            //                * (1 - Convert.ToDecimal("0" + cardView1.GetFocusedRowCellValue("Per_Dongia")) / 100)
            //                                        );
            //} 
            //if (e.Column.FieldName == "Soluong_Tra")
            //{
            //    if (Convert.ToDouble(e.Value) > Convert.ToDouble(cardView1.GetFocusedRowCellValue("Soluong")))
            //    {
            //        cardView1.GetDataRow(e.RowHandle).RejectChanges();
            //    }
            //}
        }
        #endregion

        private void txtHd_Sochungtu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && txtHd_Sochungtu.Text != "")
            {
                ds_Hdbanhang_Chitiet = objWareService.Get_All_Ware_Hdbanhang_Chitiet_ByHdbanhang_Sochungtu(txtHd_Sochungtu.EditValue);
                ds_Hdbanhang_Chitiet.Tables[0].Columns.Add("Soluong_Tra", typeof(double));
                for (int i = 0; i < ds_Hdbanhang_Chitiet.Tables[0].Rows.Count; i++)
                {
                    ds_Hdbanhang_Chitiet.Tables[0].Rows[i]["Soluong_Tra"] = 
                        Convert.ToInt32( ds_Hdbanhang_Chitiet.Tables[0].Rows[i]["Soluong"] )
                        - Convert.ToInt32("0"+ds_Hdbanhang_Chitiet.Tables[0].Rows[i]["Soluong_Kh_Tra"]);
                }
                dgware_Hdbanhang_Chitiet.DataSource = ds_Hdbanhang_Chitiet.Tables[0];
                ds_Ware_Hh_Kh_Tra_Chitiet.Clear();
                if (ds_Hdbanhang_Chitiet.Tables[0].Rows.Count == 0)
                    return;
                ds_Hdbanhang = objWareService.Get_All_Ware_Hdbanhang();
                lookUpEdit_Khachhang.EditValue = ds_Hdbanhang.Tables[0].Select("id_hdbanhang=" + ds_Hdbanhang_Chitiet.Tables[0].Rows[0]["Id_hdbanhang"])[0]["id_khachhang"];
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            object value = GoobizFrame.Windows.Forms.FrmGNumboardInput.ShowInputDialog(
              "" + gridView2.GetFocusedRowCellValue("Soluong"));
            if (Convert.ToDecimal(gridView2.GetFocusedRowCellValue("Soluong")) >= Convert.ToDecimal(value))
                gridView2.SetFocusedRowCellValue(gridView2.FocusedColumn, value);
            else
                gridView2.SetFocusedRowCellValue(gridView2.FocusedColumn, gridView2.GetFocusedRowCellValue("Soluong"));

            if (Convert.ToDecimal(value) == 0)
                gridView2.SetFocusedRowCellValue(gridView2.FocusedColumn, gridView2.GetFocusedRowCellValue("Soluong"));
        }


        // click to add item into "hàng hóa trả"
        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int soluong_tra = Convert.ToInt32("0" + gridView2.GetFocusedRowCellValue("Soluong_Tra"));
                int soluong_kh_tra = Convert.ToInt32("0" + gridView2.GetFocusedRowCellValue("Soluong_Kh_Tra"));
                int soluong = Convert.ToInt32(gridView2.GetFocusedRowCellValue("Soluong"));
                if (soluong_tra > 0 && soluong - soluong_kh_tra >= soluong_tra)
                {
                    int thanhtien_km_unit = System.Convert.ToInt32("0" + gridView2.GetDataRow(gridView2.FocusedRowHandle)["Thanhtien_Km"])
                    / soluong;

                    DataRow[] sdr = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Select("Id_Hdbanhang_Chitiet =" + gridView2.GetFocusedRowCellValue("Id_Hdbanhang_Chitiet"));
                    if (sdr != null && sdr.Length > 0)
                    {
                        //sdr[0]["Soluong"] = Convert.ToInt32(sdr[0]["Soluong"])
                        //   + Convert.ToInt32( gridView2.GetFocusedRowCellValue("Soluong_Tra") );
                        //sdr[0]["Thanhtien"] = Convert.ToInt32(gridView2.GetFocusedRowCellValue("Thanhtien"))
                        //    / Convert.ToInt32(gridView2.GetFocusedRowCellValue("Soluong"))
                        //    * Convert.ToInt32(gridView2.GetFocusedRowCellValue("Soluong_Tra"));
                        sdr[0]["Soluong"] = Convert.ToInt32(sdr[0]["Soluong"]) + soluong_tra;
                        sdr[0]["Thanhtien"] = Convert.ToInt32(gridView2.GetFocusedRowCellValue("Thanhtien")) / soluong * soluong_tra;
                        sdr[0]["Thanhtien_Km"] = Convert.ToInt32(sdr[0]["Soluong"]) * thanhtien_km_unit;
                    }
                    else
                    {
                        DataRow nrow = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].NewRow();
                        //nrow["Id_Cuahang_Mua"] = Id_Cuahang_Mua;
                        nrow["Id_Hanghoa_Mua"] = gridView2.GetFocusedRowCellValue("Id_Hanghoa_Mua");
                        nrow["Id_Donvitinh"] = gridView2.GetFocusedRowCellValue("Id_Donvitinh");
                        nrow["Soluong"] = gridView2.GetFocusedRowCellValue("Soluong_Tra");
                        nrow["Dongia"] = gridView2.GetFocusedRowCellValue("Dongia_Ban");
                        nrow["Per_Dongia"] = gridView2.GetFocusedRowCellValue("Per_Dongia");
                        //nrow["Thanhtien"] = Convert.ToInt32(gridView2.GetFocusedRowCellValue("Thanhtien"))
                        //    / soluong * soluong_tra;
                        nrow["Thanhtien"] = (Convert.ToInt32(nrow["Dongia"]) * (100 - Convert.ToInt32(nrow["Per_Dongia"])) / 100) * soluong_tra;
                        nrow["Thanhtien_Km"] = Convert.ToInt32(nrow["Soluong"]) * thanhtien_km_unit;
                        nrow["Id_Hdbanhang_Chitiet"] = gridView2.GetFocusedRowCellValue("Id_Hdbanhang_Chitiet");
                        nrow["Id_Hdbanhang"] = gridView2.GetFocusedRowCellValue("Id_Hdbanhang");
                        ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows.Add(nrow);
                    }

                    if (soluong_tra == soluong)
                        gridView2.GetDataRow(gridView2.FocusedRowHandle).Delete();
                    else
                    {
                        gridView2.SetFocusedRowCellValue(gridView2.Columns["Soluong"], soluong - soluong_tra);
                        gridView2.SetFocusedRowCellValue(gridView2.Columns["Soluong_Tra"], gridView2.GetFocusedRowCellValue("Soluong"));
                    }
                }
                else
                    GoobizFrame.Windows.Forms.MessageDialog.Show(
                        String.Format("SL mua: {0}, SL trả lần trước: {1}, SL còn lại có thể trả: {2}"
                        , new object[] { soluong, soluong_kh_tra, soluong - soluong_kh_tra }), "SL trả hàng không hợp lệ");
            }
            catch (Exception ex) { }
        }

        // click to move item back
        private void repositoryItemButtonEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow[] sdr = ds_Hdbanhang_Chitiet.Tables[0].Select("Id_Hdbanhang_Chitiet =" + gridView5.GetFocusedRowCellValue("Id_Hdbanhang_Chitiet"));
            if (sdr != null && sdr.Length > 0)
            {
                //sdr[0]["Soluong"] = Convert.ToInt32(sdr[0]["Soluong"])
                //   + Convert.ToInt32( gridView2.GetFocusedRowCellValue("Soluong_Tra") );
                //sdr[0]["Thanhtien"] = Convert.ToInt32(gridView2.GetFocusedRowCellValue("Thanhtien"))
                //    / Convert.ToInt32(gridView2.GetFocusedRowCellValue("Soluong"))
                //    * Convert.ToInt32(gridView2.GetFocusedRowCellValue("Soluong_Tra"));
                sdr[0]["Soluong"] = Convert.ToInt32(sdr[0]["Soluong"]) + Convert.ToInt32(ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Soluong"]);
                sdr[0]["Soluong_Tra"] = sdr[0]["Soluong"];
                sdr[0]["Thanhtien"] = Convert.ToInt32(gridView2.GetFocusedRowCellValue("Thanhtien")) * Convert.ToInt32(sdr[0]["Soluong"]);
            }
            else
            {
                DataRow nrow = ds_Hdbanhang_Chitiet.Tables[0].NewRow();
                //nrow["Id_Cuahang_Mua"] = Id_Cuahang_Mua;    
                nrow["Id_Hanghoa_Mua"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Id_Hanghoa_Mua"];
                nrow["Ten_Hanghoa_Mua"] = ds_Hanghoa_Mua.Tables[0].Select("Id_Hanghoa_Mua=" + nrow["Id_Hanghoa_Mua"])[0]["Ten_Hanghoa_Mua"];
                nrow["Ma_Hanghoa_Mua"] = ds_Hanghoa_Mua.Tables[0].Select("Id_Hanghoa_Mua=" + nrow["Id_Hanghoa_Mua"])[0]["Ma_Hanghoa_Mua"];
                nrow["Id_Donvitinh"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Id_Donvitinh"];
                nrow["Soluong"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Soluong"];
                nrow["Soluong_Tra"] = nrow["Soluong"];
                nrow["Dongia_Ban"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Dongia"];
                nrow["Per_Dongia"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Per_Dongia"];
                nrow["Thanhtien"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Thanhtien"];
                nrow["Id_Hdbanhang_Chitiet"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Id_Hdbanhang_Chitiet"];
                nrow["Id_Hdbanhang"] = ds_Ware_Hh_Kh_Tra_Chitiet.Tables[0].Rows[gridView5.FocusedRowHandle]["Id_Hdbanhang"];
                ds_Hdbanhang_Chitiet.Tables[0].Rows.Add(nrow);
            }
            gridView5.GetDataRow(gridView5.FocusedRowHandle).Delete();
        }

        private void dtNgay_Chungtu_View_EditValueChanged(object sender, EventArgs e)
        {
            GetWare_Hh_Kh_Tra();
        }

    }
}



           



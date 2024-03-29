using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;

namespace Ecm.Ware.Forms.WareGen_Hdbanhang
{
    public partial class FrmwareGen_Donmuahang :  GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
         GoobizFrame.Windows.Forms.FrmPrintPreview objFormReport = new  GoobizFrame.Windows.Forms.FrmPrintPreview();
       
        public Ecm.WebReferences.Classes.WareService objWareService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.WareService>();
        public Ecm.WebReferences.Classes.RexService objRexService      = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.RexService>();
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();

        DataSet ds_Donmuahang           = new DataSet();
        DataSet ds_Donmuahang_Chitiet   = new DataSet();
        public DataSet dsSelected       = new DataSet();

        #region Initialize

        public FrmwareGen_Donmuahang()
        {
            InitializeComponent();

            //datetime mask
            dtNgay_Muahang.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtNgay_Muahang.Properties.Mask.EditMask =  GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat();

            //reset lookup edit as delete value
            lookUpEdit_Nguoi_Lap.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler( GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);

            this.DisplayInfo();
        }

        private void FrmwareGen_Donmuahang_Load(object sender, EventArgs e)
        {
            if (this.MdiParent != null)
                gridView5.Columns["Chon"].Visible = false;
        }
        #endregion

        #region Event Override

        public override void DisplayInfo()
        {
            try
            {
                //Get data Ware_Dm_Kho_Hanghoa_Mua
                //DataSet dsKho_Hanghoa_Mua = objMasterService.Get_All_Ware_Dm_Kho_Hanghoa_Mua().ToDataSet();
                //lookUpEditKho_Hanghoa_Mua.Properties.DataSource = dsKho_Hanghoa_Mua.Tables[0];

                //Get data Rex_Nhansu
                lookUpEdit_Nguoi_Lap.Properties.DataSource = objRexService.Get_All_Rex_Nhansu_Collection().ToDataSet().Tables[0];

                //Get data Ware_Dm_Hanghoa_Mua
                DataSet ds_Hanghoa_Mua = objMasterService.Get_All_Ware_Dm_Hanghoa_Mua().ToDataSet();
                gridLookUpEdit_Hanghoa_Mua.DataSource = ds_Hanghoa_Mua.Tables[0];
                gridLookUpEdit_Ma_Hanghoa_Mua.DataSource = ds_Hanghoa_Mua.Tables[0];

                //Get data Ware_Dm_Nhacungcap
                //gridLookUpEditNhacungcap.DataSource = objMasterService.Get_All_Ware_Dm_Nhacungcap().ToDataSet().Tables[0];

                //Get data Ware_Dm_Donvitinh
                gridLookUpEdit_Donvitinh.DataSource = objMasterService.Get_All_Ware_Dm_Donvitinh().ToDataSet().Tables[0];

                //Get data Ware_Donmuahang
                ds_Donmuahang = objWareService.Get_All_WareGen_Donmuahang().ToDataSet();
                dgware_Donmuahang.DataSource = ds_Donmuahang;
                dgware_Donmuahang.DataMember = ds_Donmuahang.Tables[0].TableName;

                this.DataBindingControl();

                this.ChangeStatus(false);

                this.gridView1.BestFitColumns();


                gridView1.Focus();
                DisplayInfo2();

            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif


            }

        }

        void DisplayInfo2()
        {
            try
            {
                object identity = gridView1.GetFocusedRowCellValue("Id_Donmuahang");
                this.ds_Donmuahang_Chitiet = objWareService.Get_All_WareGen_Donmuahang_Chitiet_By_Donmuahang(identity).ToDataSet();

                this.ds_Donmuahang_Chitiet.Tables[0].Columns.Add("Chon", typeof(Boolean));
                this.ds_Donmuahang_Chitiet.Tables[0].Columns.Add("Chitiet_Ncc", typeof(string));

                this.dgware_Donmuahang_Chitiet.DataSource = ds_Donmuahang_Chitiet;
                this.dgware_Donmuahang_Chitiet.DataMember = ds_Donmuahang_Chitiet.Tables[0].TableName;

                //neu chung tu da duoc xac nhan thi cho phep in
                if (Convert.ToInt64(gridView1.GetFocusedRowCellValue("Doc_Process_Status")) != 2)
                {
                    gridView5.Columns["Chon"].OptionsColumn.ReadOnly = true;
                    this.item_PrintPreview.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                }
                else
                {
                    gridView5.Columns["Chon"].OptionsColumn.ReadOnly = false;
                    this.item_PrintPreview.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                }

                gridView5.BestFitColumns();
            }
            catch { }
        }
     

        void ClearDataBindings()
        {
            this.txtId_Donmuahang.DataBindings.Clear();
            this.txtMa_Donmuahang.DataBindings.Clear();
            this.txtGhichu.DataBindings.Clear();
            this.dtNgay_Muahang.DataBindings.Clear();

            this.lookUpEdit_Nguoi_Lap.DataBindings.Clear();
            this.lookUpEditKho_Hanghoa_Mua.DataBindings.Clear();
        }

        public void DataBindingControl()
        {
            try
            {
                ClearDataBindings();

                this.txtId_Donmuahang.DataBindings.Add("EditValue", ds_Donmuahang, ds_Donmuahang.Tables[0].TableName + ".Id_Donmuahang");
                this.txtMa_Donmuahang.DataBindings.Add("EditValue", ds_Donmuahang, ds_Donmuahang.Tables[0].TableName + ".Ma_Donmuahang");
                this.txtGhichu.DataBindings.Add("EditValue", ds_Donmuahang, ds_Donmuahang.Tables[0].TableName + ".Ghichu");
                this.dtNgay_Muahang.DataBindings.Add("EditValue", ds_Donmuahang, ds_Donmuahang.Tables[0].TableName + ".Ngay_Muahang");

                this.lookUpEdit_Nguoi_Lap.DataBindings.Add("EditValue", ds_Donmuahang, ds_Donmuahang.Tables[0].TableName + ".Id_Nhansu_Lap");
                this.lookUpEditKho_Hanghoa_Mua.DataBindings.Add("EditValue", ds_Donmuahang, ds_Donmuahang.Tables[0].TableName + ".Id_Kho_Hanghoa_Mua");
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif


            }
        }

        public void ChangeStatus(bool editTable)
        {
            this.dgware_Donmuahang.Enabled = !editTable;
            this.gridView5.OptionsBehavior.Editable = (this.MdiParent != null) ? editTable : true;
            this.txtMa_Donmuahang.Properties.ReadOnly = !editTable;
            this.txtGhichu.Properties.ReadOnly = !editTable;
            this.dtNgay_Muahang.Properties.ReadOnly = !editTable;

            this.lookUpEdit_Nguoi_Lap.Properties.ReadOnly = true;// !editTable;
            this.lookUpEditKho_Hanghoa_Mua.Properties.ReadOnly = true;// !editTable;

            this.dgware_Donmuahang_Chitiet.EmbeddedNavigator.Enabled = editTable;
            btn_Chon_Hhmua_FrBangke_Quydoi.Enabled = editTable;
        }

        public void ResetText()
        {
            this.txtId_Donmuahang.EditValue = "";
            this.txtMa_Donmuahang.EditValue = "";
            this.txtGhichu.EditValue = "";

            this.ds_Donmuahang_Chitiet = objWareService.Get_All_WareGen_Donmuahang_Chitiet_By_Donmuahang(0).ToDataSet();
            this.dgware_Donmuahang_Chitiet.DataSource = ds_Donmuahang_Chitiet.Tables[0];
        }

        public object InsertObject()
        {
            try
            {
                Ecm.WebReferences.WareService.Ware_Donmuahang objWare_Donmuahang = new Ecm.WebReferences.WareService.Ware_Donmuahang();
                objWare_Donmuahang.Id_Donmuahang = -1;
                objWare_Donmuahang.Ma_Donmuahang = txtMa_Donmuahang.EditValue;
                objWare_Donmuahang.Ghichu = txtGhichu.EditValue;
                objWare_Donmuahang.Ngay_Muahang = dtNgay_Muahang.EditValue;

                if ("" + lookUpEditKho_Hanghoa_Mua.EditValue != "")
                    objWare_Donmuahang.Id_Kho_Hanghoa_Mua = lookUpEditKho_Hanghoa_Mua.EditValue;
                if ("" + lookUpEdit_Nguoi_Lap.EditValue != "")
                    objWare_Donmuahang.Id_Nhansu_Lap = lookUpEdit_Nguoi_Lap.EditValue;

                object identity = objWareService.Insert_WareGen_Donmuahang(objWare_Donmuahang);

                if (identity != null)
                {
                    dgware_Donmuahang_Chitiet.EmbeddedNavigator.Buttons.DoClick(dgware_Donmuahang_Chitiet.EmbeddedNavigator.Buttons.EndEdit);
                    foreach (DataRow dr in ds_Donmuahang_Chitiet.Tables[0].Rows)
                    {
                        dr["Id_Donmuahang"] = identity;
                    }
                    //update donmuahang_chitiet
                    objWareService.Update_WareGen_Donmuahang_Chitiet_Collection(ds_Donmuahang_Chitiet);
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
                Ecm.WebReferences.WareService.Ware_Donmuahang objWare_Donmuahang = new Ecm.WebReferences.WareService.Ware_Donmuahang();
                objWare_Donmuahang.Id_Donmuahang = txtId_Donmuahang.EditValue;
                objWare_Donmuahang.Ma_Donmuahang = txtMa_Donmuahang.EditValue;
                objWare_Donmuahang.Ghichu = txtGhichu.EditValue;
                objWare_Donmuahang.Ngay_Muahang = dtNgay_Muahang.EditValue;

                if ("" + lookUpEditKho_Hanghoa_Mua.EditValue != "")
                    objWare_Donmuahang.Id_Kho_Hanghoa_Mua = lookUpEditKho_Hanghoa_Mua.EditValue;
                if ("" + lookUpEdit_Nguoi_Lap.EditValue != "")
                    objWare_Donmuahang.Id_Nhansu_Lap = lookUpEdit_Nguoi_Lap.EditValue;
                //update donmuahang
                objWareService.Update_WareGen_Donmuahang(objWare_Donmuahang);

                //update donmuahang_chitiet
                DoClickEndEdit(dgware_Donmuahang_Chitiet);
                  foreach (DataRow dr in ds_Donmuahang_Chitiet.Tables[0].Rows)
                {
                    if(dr.RowState == DataRowState.Added)
                        dr["Id_Donmuahang"] = txtId_Donmuahang.EditValue;
                }
                  objWareService.Update_WareGen_Donmuahang_Chitiet_Collection(ds_Donmuahang_Chitiet);

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
            Ecm.WebReferences.WareService.Ware_Donmuahang objWare_Donmuahang = new Ecm.WebReferences.WareService.Ware_Donmuahang();
            objWare_Donmuahang.Id_Donmuahang = txtId_Donmuahang.EditValue;

            return objWareService.Delete_WareGen_Donmuahang(objWare_Donmuahang);
        }

        public override bool PerformAdd()
        {
            lookUpEdit_Nguoi_Lap.EditValue = Convert.ToInt64( GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu());
            //DataSet ds_Kho_Hanghoa = objMasterService.Get_All_Ware_Dm_Kho_Hanghoa_MuaBy_Id_Nhansu(lookUpEdit_Nguoi_Lap.EditValue).ToDataSet();
            //if (ds_Kho_Hanghoa.Tables[0].Rows.Count > 0)
            //    lookUpEditKho_Hanghoa_Mua.EditValue = ds_Kho_Hanghoa.Tables[0].Rows[0]["Id_Kho_Hanghoa_Mua"];
            //else
            //{
            //     GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
            //    lookUpEdit_Nguoi_Lap.EditValue = null;
            //    return false;
            //}

            ClearDataBindings();
            this.ChangeStatus(true);
            this.ResetText();


            dtNgay_Muahang.EditValue = objWareService.GetServerDateTime();
            txtMa_Donmuahang.EditValue = objWareService.GetNew_Sochungtu("waregen_donmuahang", "ma_donmuahang", lookUpEditKho_Hanghoa_Mua.GetColumnValue("Ma_Kho_Hanghoa_Mua"));
            return true;
        }

        public override bool PerformEdit()
        {
            try
            {
                if (! GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu().Equals("" + lookUpEdit_Nguoi_Lap.EditValue))
                {
                     GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                    return false;
                }
                else
                {
                    Ecm.WebReferences.WareService.DocumentProcessStatus DocumentProcessStatus = new Ecm.WebReferences.WareService.DocumentProcessStatus();
                    DocumentProcessStatus.Tablename = "waregen_donmuahang";
                    DocumentProcessStatus.PK_Name = "id_donmuahang";
                    DocumentProcessStatus.Identity = txtId_Donmuahang.EditValue;
                    DocumentProcessStatus = objWareService.Get_DocumentProcessStatus(DocumentProcessStatus);
                    if (objWareService.GetEDocumentProcessStatus((int)DocumentProcessStatus.Doc_Process_Status) != Ecm.WebReferences.WareService.EDocumentProcessStatus.NewDoc)
                    {
                         GoobizFrame.Windows.Forms.UserMessage.Show("TASK_VERIFIED", new string[] { });
                        return false;
                    }
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
            this.ChangeStatus(false);
            return true;
        }

        public override bool PerformSave()
        {
            try
            {
                bool success = false;

                 GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new  GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(txtMa_Donmuahang,             lbMa_Donmuahang.Text);
                hashtableControls.Add(lookUpEditKho_Hanghoa_Mua,    lblKho_Hanghoa_Mua.Text);
                hashtableControls.Add(lookUpEdit_Nguoi_Lap,         lblNguoi_Lap.Text);

                if (! GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;
 
                this.DoClickEndEdit(dgware_Donmuahang_Chitiet);

                if (this.FormState ==  GoobizFrame.Windows.Forms.FormState.Add)
                {
                    success = (bool)this.InsertObject();
                }
                else if (this.FormState ==  GoobizFrame.Windows.Forms.FormState.Edit)
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
                if ( GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUser().ToUpper() != "ADMIN")
                    if (! GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu().Equals("" + lookUpEdit_Nguoi_Lap.EditValue))
                    {
                         GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                        return false;
                    }
                    else
                    {
                        Ecm.WebReferences.WareService.DocumentProcessStatus DocumentProcessStatus = new Ecm.WebReferences.WareService.DocumentProcessStatus();
                        DocumentProcessStatus.Tablename = "waregen_donmuahang";
                        DocumentProcessStatus.PK_Name = "id_donmuahang";
                        DocumentProcessStatus.Identity = txtId_Donmuahang.EditValue;
                        DocumentProcessStatus = objWareService.Get_DocumentProcessStatus(DocumentProcessStatus);
                        if (objWareService.GetEDocumentProcessStatus((int)DocumentProcessStatus.Doc_Process_Status) != Ecm.WebReferences.WareService.EDocumentProcessStatus.NewDoc)
                        {
                             GoobizFrame.Windows.Forms.UserMessage.Show("TASK_VERIFIED", new string[] { });
                            return false;
                        }
                    }
                if ( GoobizFrame.Windows.Forms.UserMessage.Show("Msg00004", new string[]  {
                 GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("WareGen_Donmuahang"),
                 GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("WareGen_Donmuahang")   }) == DialogResult.Yes)
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
            try
            {
                DoClickEndEdit(dgware_Donmuahang_Chitiet);
               
                DataRow[] drSelected = ds_Donmuahang_Chitiet.Tables[0].Select("Chon = true");
                dsSelected = ds_Donmuahang_Chitiet.Clone();
                foreach (DataRow dr in drSelected)
                {
                    dsSelected.Tables[0].ImportRow(dr);
                }
                this.Dispose();
                this.Close();
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
                return null;
            }
        }
        
        public override bool PerformTest()
        {
            try
            {
                //show form ShowFormDocProgress
                 GoobizFrame.Windows.MdiUtils.MdiChecker.ShowFormDocProgress(
                    "WareGen_Donmuahang" //Table name
                    , "Id_Donmuahang" //PK name
                    , gridView1.GetFocusedRowCellValue("Id_Donmuahang") //value
                    ,  GoobizFrame.Windows.Forms.DocProgress.Enumerators.EDocumentProcessStatus.TestDoc //New enum DocStatus
                    , false);

                DisplayInfo();
            }
            catch (Exception ex)
            {
#if (DEBUG)
                MessageBox.Show(ex.Message);
                return false;
#endif 
            }
            return base.PerformTest();

        }

        public override bool PerformVerify()
        {
            try
            {
                //show form ShowFormDocProgress
                 GoobizFrame.Windows.MdiUtils.MdiChecker.ShowFormDocProgress(
                    "WareGen_Donmuahang" //Table name
                    , "Id_Donmuahang" //PK name
                    , gridView1.GetFocusedRowCellValue("Id_Donmuahang") //value
                    ,  GoobizFrame.Windows.Forms.DocProgress.Enumerators.EDocumentProcessStatus.VerifyDoc //New enum DocStatus
                    , false);

                DisplayInfo();
            }
            catch (Exception ex)
            {
#if (DEBUG)
                MessageBox.Show(ex.Message);
                return false;
#endif 
            }
            return base.PerformVerify();

        }

        public override bool PerformPrintPreview()
        {
            //Fill dataset
            DataSets.dsDutru_Muahang dsDutru_Muahang = new Ecm.Ware.DataSets.dsDutru_Muahang();
            Reports.rptKehoach_Banhang rptDutru_Muahang = new Ecm.Ware.Reports.rptKehoach_Banhang();
            //             GoobizFrame.Windows.Forms.FrmPrintPreview frmPrintPreview = new  GoobizFrame.Windows.Forms.FrmPrintPreview();
            objFormReport.Report = rptDutru_Muahang;
            rptDutru_Muahang.DataSource = dsDutru_Muahang;
            
            if (ds_Donmuahang_Chitiet.Tables[0].Columns.IndexOf("Stt") == -1)
                ds_Donmuahang_Chitiet.Tables[0].Columns.Add("Stt");

            int i = 1;
            foreach (DataRow dr in ds_Donmuahang_Chitiet.Tables[0].Rows)
            {
                dr["Stt"] = i++;
                dsDutru_Muahang.Tables["ware_donmuahang_chitiet"].ImportRow(dr);
            }

            //Add datasoure & show report
            rptDutru_Muahang.xrcell_Ngay_Chungtu.Text = dtNgay_Muahang.Text;
            //rptDutru_Muahang.xrcell_Sochungtu.Text      = txtMa_Donmuahang.Text;
            //rptDutru_Muahang.lblNguoi_Denghi.Text       = lookUpEdit_Nguoi_Lap.Text;


            rptDutru_Muahang.CreateDocument();

            objFormReport.printControl1.PrintingSystem = rptDutru_Muahang.PrintingSystem;
            objFormReport.MdiParent = this.MdiParent;
            objFormReport.Show();
            objFormReport.Activate();

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
                    gridView5.SetFocusedRowCellValue(gridView5.Columns["Thanhtien"], Convert.ToDecimal(gridView5.GetFocusedRowCellValue("Soluong"))
                                                                 * Convert.ToDecimal(gridView5.GetFocusedRowCellValue("Dongia"))
                                                    );
            }
            else if (e.Column.FieldName == "Id_Hanghoa_Mua")
            {
                gridView5.SetFocusedRowCellValue(gridView5.Columns["Id_Donvitinh"]
                    , ((System.Data.DataRowView)gridLookUpEdit_Hanghoa_Mua.GetDataSourceRowByKeyValue(e.Value))["Id_Donvitinh"]);
            }

            this.DoClickEndEdit(dgware_Donmuahang_Chitiet); //dgware_Donmuahang_Chitiet.EmbeddedNavigator.Buttons.EndEdit.DoClick();

        }

        private void chkShowPreview_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.OptionsView.ShowPreview = chkShowPreview.Checked;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                DisplayInfo2();
            }
        }

        private void lookUpEditKho_Hanghoa_Mua_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridLookUpEdit_Hanghoa_Mua.DataSource = objMasterService.Get_All_Ware_Dm_Hanghoa_MuaBy_Id_Kho_Hh_Mua(lookUpEditKho_Hanghoa_Mua.EditValue, null).ToDataSet().Tables[0];
            }
            catch { }
        }

        private void btn_Chon_Hhmua_FromNXT_Click(object sender, EventArgs e)
        {
            //FrmwareGen_Quydoi_Hhmua_Fr_Hhban frmwareGen_Quydoi_Hhmua_Fr_Hhban = new FrmwareGen_Quydoi_Hhmua_Fr_Hhban();
            //frmwareGen_Quydoi_Hhmua_Fr_Hhban.Text = "Hàng hóa";
            // GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmwareGen_Quydoi_Hhmua_Fr_Hhban);
            //frmwareGen_Quydoi_Hhmua_Fr_Hhban.ShowDialog();

            //if (frmwareGen_Quydoi_Hhmua_Fr_Hhban.SelectedRows != null
            //    && frmwareGen_Quydoi_Hhmua_Fr_Hhban.SelectedRows.Length > 0)
            //{
            //    if (frmwareGen_Quydoi_Hhmua_Fr_Hhban.SelectedRows.Length > 0)
            //    {
            //        for (int i = 0; i < frmwareGen_Quydoi_Hhmua_Fr_Hhban.SelectedRows.Length; i++)
            //        {
            //            DataRow nrow = ds_Donmuahang_Chitiet.Tables[0].NewRow();
            //            nrow["Id_Hanghoa_Mua"] = frmwareGen_Quydoi_Hhmua_Fr_Hhban.SelectedRows[i]["Id_Hanghoa_Mua"];
            //            nrow["Id_Donvitinh"] = frmwareGen_Quydoi_Hhmua_Fr_Hhban.SelectedRows[i]["Id_Donvitinh"];
            //            nrow["Soluong"] = frmwareGen_Quydoi_Hhmua_Fr_Hhban.SelectedRows[i]["Soluong"];
            //            nrow["Fr_Hdbanhang"] = true;

            //            ds_Donmuahang_Chitiet.Tables[0].Rows.Add(nrow);
            //        }
            //    }
            //}
        }

        private void btnInit_Hdbanhang_Chitiet_Click(object sender, EventArgs e)
        {
            //FrmwareGen_Hdbanhang_Chitiet_FrDonmuahang frmwareGen_Hdbanhang_Chitiet_FrDonmuahang = new FrmwareGen_Hdbanhang_Chitiet_FrDonmuahang();
            //frmwareGen_Hdbanhang_Chitiet_FrDonmuahang.Text = btnInit_Hdbanhang_Chitiet.Text;
            // GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmwareGen_Hdbanhang_Chitiet_FrDonmuahang);
            //frmwareGen_Hdbanhang_Chitiet_FrDonmuahang.Id_Donmuahang = gridView1.GetFocusedRowCellValue("Id_Donmuahang");
            //frmwareGen_Hdbanhang_Chitiet_FrDonmuahang.Ngay_Chungtu = dtNgay_Muahang.DateTime;
            //frmwareGen_Hdbanhang_Chitiet_FrDonmuahang.ShowDialog();

            DisplayInfo2();
        }
        #endregion

    }
}



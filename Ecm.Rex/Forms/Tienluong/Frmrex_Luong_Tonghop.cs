using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;

namespace Ecm.Rex.Forms
{
    public partial class Frmrex_Luong_Tonghop : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        private Ecm.WebReferences.Classes.MasterService objMasterTables = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        private Ecm.WebReferences.Classes.RexService objRex = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.RexService>();
        //Ecm.WebReferences.Classes.Accounting objAccounting = new Ecm.WebReferences.Classes.Accounting();
        private DataSet dsrex_Luong_Tonghop = new DataSet();
        DataSet dsNhansu;
        object id_kyluong;
        DataSet ds_Tgluong;
        DataSet dsTamung_Ky1;     

        GoobizFrame.Windows.Forms.FrmPrintPreview frmReport;

        public Frmrex_Luong_Tonghop()
        {
            InitializeComponent();

            item_Add.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            item_Edit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            item_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            dtKyluong.DateTime = DateTime.Now;            
        }

        #region override methods

        public override void ChangeStatus(bool editable)
        {
            treeListrex_Dm_Bophan.Enabled = !editable;
            gvrex_Luong_Tonghop.OptionsBehavior.Editable = false;
            dtKyluong.Properties.ReadOnly = editable;
           
        }
  
        public override void DisplayInfo()
        {
            try
            {
                #region load lookup edit

                treeListrex_Dm_Bophan.DataSource = objMasterTables.Get_All_Rex_Dm_Bophan_Collection().ToDataSet().Tables[0];
                //lookUpEdit_Dm_Kyluong.Properties.DataSource = objRex.Get_All_Rex_Kyluong_Collection().Tables[0];

                dsNhansu = objRex.Get_All_Rex_Nhansu_Collection().ToDataSet();
                gridLookUpEdit_Ma_Nhansu.DataSource = dsNhansu.Tables[0];
                gridLookUpEdit_Ten_Nhansu.DataSource = dsNhansu.Tables[0];

                DataSet dsChucvu = objMasterTables.Get_All_Rex_Dm_Chucvu_Collection().ToDataSet();
                gridLookUpEdit_Chucvu.DataSource = dsChucvu.Tables[0];

                gridLookUpEdit_Ndung_Tgluong.DataSource = objMasterTables.Get_All_Rex_Dm_Ndung_Tgluong_Collection().ToDataSet().Tables[0];
                #endregion

                ds_Tgluong = null;
                dgrex_Tgluong.DataSource = null;
                dgrex_Tgluong.RefreshDataSource();

                this.AfterCheckUserRightAction += new EventHandler(Frmrex_Luong_Tonghop_AfterCheckUserRightAction);
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
            }
        }

        public override bool PerformCancel()
        {
            Display_Tgluong();
            DisplayInfo_Luong_Tonghop();
            return base.PerformCancel();
        }

        public override bool PerformSaveChanges()
        {

            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gvrex_Tgluong.Columns["Id_Ndung_Tgluong"], "");
            hashtableControls.Add(gvrex_Tgluong.Columns["Sotien"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gvrex_Tgluong))
                return false;

            try
            {
                objRex.Update_Rex_Tgluong_Collection(ds_Tgluong);
                objRex.Rex_Luong_Tonghop_Init_ByBophan(dtKyluong.DateTime.Year, dtKyluong.DateTime.Month, treeListColumn1.TreeList.FocusedNode.GetValue("Id_Bophan"));

                Display_Tgluong();
                DisplayInfo_Luong_Tonghop();
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                return false;
            }

            return true;
        }

        void PrintBangLuongChitiet(DevExpress.XtraReports.UI.XtraReport XtraReport, DataSet dsLuong_Tonghop, string Filter)
        {
            if (frmReport == null || frmReport.IsDisposed)
                frmReport = new GoobizFrame.Windows.Forms.FrmPrintPreview();
            frmReport.Report = XtraReport;

            Forms.ReportHelper.SetCompanyInfoAtHeader(XtraReport);


            /*
             #region Set he so ctrinh - logo, ten cty

            try
            {
                using (DataSet dsHeso_Chuongtrinh = objMasterTables.Get_Rex_Dm_Heso_Chuongtrinh_Collection3())
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
                        ,imageData
                    });

                    XtraReport.FindControl("xrc_CompanyName",true).DataBindings.Add(
                        new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyName"));
                    XtraReport.FindControl("xrc_CompanyAddress", true).DataBindings.Add(
                        new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyAddress"));
                    XtraReport.FindControl("xrPic_Logo", true).DataBindings.Add(
                        new DevExpress.XtraReports.UI.XRBinding("Image", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyLogo"));
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.TrayMessage.TrayMessage.Status = new GoobizFrame.Windows.TrayMessage.TrayMessageInfo(MessageBoxIcon.Asterisk,
                    ex.Message, ex.ToString());
            }

            #endregion
             */
            var dsRex_Luong_Tonghop = new Datasets.DsRex_Luong_Tonghop();
            DataRow[] sdr_atm = dsLuong_Tonghop.Tables[0].Select(Filter);
            foreach (DataRow dr in sdr_atm)
            {
                var ndr = dsRex_Luong_Tonghop.Tables[0].NewRow();
                foreach (DataColumn col in ndr.Table.Columns)
                {
                    try
                    {
                        ndr[col.ColumnName] = dr[col.ColumnName];
                    }
                    catch { continue; }
                }
                dsRex_Luong_Tonghop.Tables[0].Rows.Add(ndr);
            }
            dsRex_Luong_Tonghop.AcceptChanges();
            XtraReport.DataSource = dsRex_Luong_Tonghop;

            XtraReport.CreateDocument();

            //show form with printcontrol
            frmReport.printControl1.PrintingSystem = XtraReport.PrintingSystem;
            frmReport.MdiParent = this.MdiParent;
            frmReport.Show();
            frmReport.Activate();
        }

        public override bool PerformPrintPreview()
        {
            Reports.RptRex_Luongtonghop_Bangctiet rpt_Rex_Luong = new Ecm.Rex.Reports.RptRex_Luongtonghop_Bangctiet();
            PrintBangLuongChitiet(rpt_Rex_Luong, dsrex_Luong_Tonghop, "");
          
            return base.PerformPrintPreview();
        }
        #endregion


      

        void Frmrex_Luong_Tonghop_AfterCheckUserRightAction(object sender, EventArgs e)
        {
            gvrex_Tgluong.OptionsBehavior.Editable = this.EnableEdit;
            navPrint_BangAll.Enabled = this.EnablePrintPreview;
            navPrint_BangATM.Enabled = this.EnablePrintPreview;
            navPrint_BangTM.Enabled = this.EnablePrintPreview;
            navPrint_Bangchitiet.Enabled = this.EnablePrintPreview;
        }

        public void DisplayInfo_Luong_Tonghop()
        {
            try
            {
                if (treeListrex_Dm_Bophan.FocusedNode != null && id_kyluong != null)
                {
                    //id bộ phận
                    object id_Bophan = treeListrex_Dm_Bophan.FocusedNode.GetValue("Id_Bophan");

                    dsrex_Luong_Tonghop = objRex.Get_Rex_Luong_Tonghop_By_Kyluong_Id_Bophan(id_kyluong, id_Bophan, false).ToDataSet();
                    dgrex_Luong_Tonghop.DataSource = dsrex_Luong_Tonghop;
                    dgrex_Luong_Tonghop.DataMember = dsrex_Luong_Tonghop.Tables[0].TableName;

                    gvrex_Luong_Tonghop.BestFitColumns();
                    ChangeStatus(false);

                    if (gvrex_Luong_Tonghop.RowCount > 0)
                    {
                        gvrex_Luong_Tonghop.Focus();
                        gvrex_Luong_Tonghop.FocusedRowHandle = 0;

                        gvrex_Tgluong.Columns["Id_Nhansu"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(
                               gvrex_Tgluong.Columns["Id_Nhansu"],
                               gvrex_Luong_Tonghop.GetFocusedRowCellValue("Id_Nhansu")
                               );
                    }
                }
                else
                {
                    dgrex_Luong_Tonghop.DataSource = null;
                    dgrex_Tgluong.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
            }
        }

        private void Frmrex_Luong_Tonghop_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void treeListrex_Dm_Bophan_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DisplayInfo_Luong_Tonghop();
            Display_Tamung_Ky1();
            Display_Tgluong();
        }

       

        /// <summary>
        /// thay doi ky luong
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtKyluong_EditValueChanged(object sender, EventArgs e)
        {
            DataSet dsKyluong = objRex.Get_All_Rex_Kyluong_ByThangNam(dtKyluong.DateTime.Month, dtKyluong.DateTime.Year).ToDataSet();
            id_kyluong = dsKyluong.Tables[0].Rows[0]["Id_Kyluong"];
           
            DisplayInfo_Luong_Tonghop();
            Display_Tamung_Ky1();
            Display_Tgluong();

            this.ChangeFormState(GoobizFrame.Windows.Forms.FormState.View);
        }

        private void dgrex_Dm_Chungchi_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {

        }

        /// <summary>
        /// display tang giam luong theo nhan su hien tai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvrex_Luong_Tonghop_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvrex_Luong_Tonghop.GetFocusedDataRow() != null)
            {
                gvrex_Tgluong.Columns["Id_Nhansu"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(
                    gvrex_Tgluong.Columns["Id_Nhansu"],
                    gvrex_Luong_Tonghop.GetFocusedRowCellValue("Id_Nhansu")
                    );
            }
        }


        /// <summary>
        /// hien thi bangtang giam luong cua tung nhan su
        /// </summary>
        void Display_Tgluong()
        {

            if ("" + id_kyluong != "" && "" + treeListColumn1.TreeList.FocusedNode !="")
            {
                ds_Tgluong = objRex.Rex_Tgluong_Select_By_Id_Kyluong( new WebReferences.RexService.Rex_Tgluong(){
                   Id_Kyluong = id_kyluong, 
                   Id_Bophan = treeListColumn1.TreeList.FocusedNode.GetValue("Id_Bophan")
                }).ToDataSet();
                ds_Tgluong.Tables[0].Columns["Nhom_Ndung_Tgluong"].AllowDBNull = true;
                ds_Tgluong.Tables[0].Columns["Id_Ndung_Tgluong"].ReadOnly = false;
                ds_Tgluong.Tables[0].Columns["Sotien"].ReadOnly = false;

                dgrex_Tgluong.DataSource = ds_Tgluong;
                dgrex_Tgluong.DataMember = ds_Tgluong.Tables[0].TableName;

                this.Data = ds_Tgluong;
                this.GridControl = dgrex_Tgluong;

                gvrex_Tgluong.BestFitColumns();
            }
        }


        /// <summary>
        /// luong tam ung ky 1
        /// </summary>
        void Display_Tamung_Ky1()
        {
            try
            {
                if (treeListrex_Dm_Bophan.FocusedNode != null && id_kyluong != null)
                {
                    //id bộ phận
                    object id_Bophan = treeListrex_Dm_Bophan.FocusedNode.GetValue("Id_Bophan");

                    dsTamung_Ky1 = objRex.Rex_Tamung_Ky1_SelectByBophan(dtKyluong.DateTime.Year, dtKyluong.DateTime.Month, id_Bophan, false).ToDataSet();

                    dgrex_Tamung_Ky1.DataSource = dsTamung_Ky1.Tables[0];

                    gvrex_Tamung_Ky1.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridLookUpEdit_Ndung_Tgluong_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                try
                {
                    System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("itvsbiz.Meta.dll", "itvsbiz.Meta.Forms.Rex.Frmrex_Dm_Ndung_Tgluong", this);
                    if (dialog == null)
                        return;
                    var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Ndung_Tgluong").GetValue(dialog, null)
                       as Ecm.WebReferences.MasterService.Rex_Dm_Ndung_Tgluong;

                    // Update database
                    gridLookUpEdit_Ndung_Tgluong.DataSource = objMasterTables.Get_All_Rex_Dm_Ndung_Tgluong_Collection().ToDataSet().Tables[0];

                    // Selected id
                    if (SelectedObject + "" != "")
                    {
                        if ("" + SelectedObject.Id_Ndung_Tgluong != "")
                        {
                            //gvrex_Dm_Ndung_Tgluong.SetFocusedRowCellValue("Id_Ndung_Tgluong", frmrex_Dm_Ndung_Tgluong.Selected_Rex_Dm_Ndung_Tgluong.Id_Ndung_Tgluong);
                            gvrex_Tgluong.SetFocusedRowCellValue(gvrex_Tgluong.FocusedColumn, SelectedObject.Id_Ndung_Tgluong);
                        }
                        gvrex_Tgluong.FocusedColumn.BestFit();
                    }
                }
                catch (Exception ex)
                {
                    GoobizFrame.Windows.TrayMessage.TrayMessage.Status = new GoobizFrame.Windows.TrayMessage.TrayMessageInfo(MessageBoxIcon.Asterisk, ex.Message, ex.ToString());
                }
            }
        }

        private void gvrex_Dm_Ndung_Tgluong_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (gvrex_Luong_Tonghop.GetFocusedDataRow() != null)
            {
                var drTgluong = gvrex_Tgluong.GetFocusedDataRow();
                drTgluong["Id_Nhansu"] = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Id_Nhansu");
                drTgluong["Id_Bophan"] = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Id_Bophan");
                drTgluong["Id_Kyluong"] = id_kyluong;
                drTgluong["Nam_Kyluong"] = dtKyluong.DateTime.Year;
                drTgluong["Thang_Kyluong"] = dtKyluong.DateTime.Month;
                drTgluong["Nhom_Ndung_Tgluong"] = "+/-";
                //gvrex_Tgluong.SetFocusedRowCellValue("Id_Nhansu", gvrex_Luong_Tonghop.GetFocusedRowCellValue("Id_Nhansu"));
                //gvrex_Tgluong.SetFocusedRowCellValue("Id_Bophan", gvrex_Luong_Tonghop.GetFocusedRowCellValue("Id_Bophan"));
                //gvrex_Tgluong.SetFocusedRowCellValue("Id_Kyluong", id_kyluong);
                //gvrex_Tgluong.SetFocusedRowCellValue("Nam_Kyluong", dtKyluong.DateTime.Year);
                //gvrex_Tgluong.SetFocusedRowCellValue("Thang_Kyluong", dtKyluong.DateTime.Month);
                //gvrex_Tgluong.SetFocusedRowCellValue("Nhom_Ndung_Tgluong", "+/-");
            }
        }

        private void gvrex_Tgluong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.DoClickEndEdit(dgrex_Tgluong);
        }

        private void navBarControl1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            switch (e.Link.ItemName)
            {
                case "navPrint_Bangchitiet":
                    Reports.RptRex_Luongtonghop_Bangctiet rpt_Rex_Luong = new Ecm.Rex.Reports.RptRex_Luongtonghop_Bangctiet();
                    PrintBangLuongChitiet(rpt_Rex_Luong, dsrex_Luong_Tonghop, "");
                    break;
                case "navPrint_BangATM":
                    Reports.RptRex_Luongtonghop_ATM rpt_Rex_Luong_atm = new Ecm.Rex.Reports.RptRex_Luongtonghop_ATM();
                    PrintBangLuongChitiet(rpt_Rex_Luong_atm, dsrex_Luong_Tonghop, "Taikhoan_Nganhang is not null");
                    break;
                case "navPrint_BangTM":
                    rpt_Rex_Luong_atm = new Ecm.Rex.Reports.RptRex_Luongtonghop_ATM();
                    PrintBangLuongChitiet(rpt_Rex_Luong_atm, dsrex_Luong_Tonghop, "Taikhoan_Nganhang is null");
                    break;
                case "navPrint_BangAll":
                    rpt_Rex_Luong_atm = new Ecm.Rex.Reports.RptRex_Luongtonghop_ATM();
                    PrintBangLuongChitiet(rpt_Rex_Luong_atm, dsrex_Luong_Tonghop, "");
                    break;
                case "navPrint_Phieuluong":
                    var rpt_Rex_Luong_phieuluong = new Ecm.Rex.Reports.RptRex_Luongtonghop_Phieuluong();
                    var dsLuong_Tonghop_Canhan = objRex.Get_Rex_Luong_Tonghop_ByInhansu(id_kyluong, gvrex_Luong_Tonghop.GetFocusedRowCellValue("Id_Nhansu")).ToDataSet();
                    PrintBangLuongChitiet(rpt_Rex_Luong_phieuluong, dsLuong_Tonghop_Canhan,"");
                    break;
                case "navPrint_TkQuyluong":
                    var rpt_TkQuyluong = new Reports.RptRex_Luong_Tonghop_TkQuyluong();
                    var dsTkQuyluong = objRex.Rex_Luong_Tonghop_Tkquyluong(dtKyluong.DateTime.Year, dtKyluong.DateTime.Month).ToDataSet();
                    rpt_TkQuyluong.DataSourceSchema = dsTkQuyluong.GetXmlSchema();

                    if (frmReport == null || frmReport.IsDisposed)
                        frmReport = new GoobizFrame.Windows.Forms.FrmPrintPreview();
                    frmReport.Report = rpt_TkQuyluong; 
                    Forms.ReportHelper.SetCompanyInfoAtHeader(rpt_TkQuyluong);
                    rpt_TkQuyluong.FindControl("xrc_Ngay_Ketthuc", true).Text = string.Format("{0:MM/yyyy}", dtKyluong.DateTime);
                    
                    rpt_TkQuyluong.DataSource = dsTkQuyluong;

                    rpt_TkQuyluong.CreateDocument();
                    frmReport.printControl1.PrintingSystem = rpt_TkQuyluong.PrintingSystem;
                    frmReport.AllowEditTemplate = true;
                    frmReport.MdiParent = this.MdiParent;
                    frmReport.Show();
                    frmReport.Activate();                    
                    break;

                case "navLuong_Tonghop_Init":
                    DevExpress.Utils.WaitDialogForm WaitDialogForm = new DevExpress.Utils.WaitDialogForm("Vui lòng chờ trong vài giây...", "Đang thực hiện");
                    try
                    {
                        GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
                        hashtableControls.Add(dtKyluong, lblKyluong.Text);
                        if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                            return;

                        if (id_kyluong != null)
                        {
                            objRex.Rex_Luong_Tonghop_Init(dtKyluong.DateTime.Year, dtKyluong.DateTime.Month);                           
                        }

                        DisplayInfo_Luong_Tonghop();
                    }
                    catch (Exception ex)
                    {
                        WaitDialogForm.Close();
                        GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");

                        return;
                    }
                    WaitDialogForm.Close();
                    break;
            }
        }

        
    }
}

/////////removed code///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
  private void lookUpEdit_Dm_Kyluong_EditValueChanged(object sender, EventArgs e)
        {
            //if (lookUpEdit_Dm_Kyluong.EditValue != null)
            //    this.item_Add.Enabled = !objRex.Check_Rex_Luong_Tonghop_Already_By_Id_Kyluong(lookUpEdit_Dm_Kyluong.EditValue);
            //DisplayInfo_Details();
        }

        private void advBandedGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal Tienluong = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tienluong") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tienluong"));
            decimal Tong_Phucap_Tinhthue = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tong_Phucap_Tinhthue") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tong_Phucap_Tinhthue"));
            decimal Tong_Phucap_Khong_Tinhthue = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tong_Phucap_Khong_Tinhthue") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tong_Phucap_Khong_Tinhthue"));
            decimal Phucap_Bhxh = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Phucap_Bhxh") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Phucap_Bhxh"));
            decimal Buluong = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Buluong") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Buluong"));
            decimal Kyluat = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Kyluat") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Kyluat"));
            decimal Bhxh = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Bhxh") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Bhxh"));
            decimal Bhyt = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Bhyt") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Bhyt"));
            decimal Bhtn = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Bhtn") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Bhtn"));
            decimal Tuthien = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tuthien") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tuthien"));
            decimal Luong_Tangca_Khong_Chiuthue = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Luong_Tangca_Khong_Chiuthue") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Luong_Tangca_Khong_Chiuthue"));
            decimal Khautru_Banthan = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Khautru_Banthan") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Khautru_Banthan"));
            decimal Sotien_Nguoi_Phuthuoc = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Sotien_Nguoi_Phuthuoc") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Sotien_Nguoi_Phuthuoc"));
            decimal Thue_Tncn = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Thue_Tncn") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Thue_Tncn"));
            decimal Tamung_Ky1 = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tamung_Ky1") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tamung_Ky1"));

            decimal Tong_Tienluong, Sotien_Conlai_Chiuthue, Sotien_Conlai_Datruthue, Thuclanh;
            switch (e.Column.FieldName)
            {
                //Tong_Tienluong = (3) + (4) + (5) + (6) + (7) - (8) 
                //Tong_Tienluong = Tienluong + Tong_Phucap_Tinhthue + Tong_Phucap_Khong_Tinhthue + Phucap_Bhxh + Buluong - Kyluat 

                //Sotien_Conlai_Chiuthue =  (9) - (5) - (6) - (10) - (11) - (12) -(13) - (15) - (16) - (18) 
                //Sotien_Conlai_Chiuthue = Tong_Tienluong - Tong_Phucap_Khong_Tinhthue - Phucap_Bhxh - Bhxh - Bhyt - Bhtn -Tuthien - Luong_Tangca_Khong_Chiuthue - Khautru_Banthan - Sotien_Nguoi_Phuthuoc 

                //Sotien_Conlai_Datruthue =  (9) - (20) 
                //Sotien_Conlai_Datruthue =  Tong_Tienluong - Thue_Tncn

                //Thuclanh = (9) - (10) - (11) - (12) - (14) - (20) - (13) 
                //Thuclanh = Tong_Tienluong - Bhxh - Bhyt - Bhtn - Tamung_Ky1 - Thue_Tncn - Tuthien 

                case "Phucap_Bhxh":
                    //Phucap_Bhxh => Tong_Tienluong => Sotien_Conlai_Chiuthue => Sotien_Conlai_Datruthue => Thuclanh
                    Tong_Tienluong = Tienluong + Tong_Phucap_Tinhthue + Tong_Phucap_Khong_Tinhthue + Phucap_Bhxh + Buluong - Kyluat;
                    Sotien_Conlai_Chiuthue = Tong_Tienluong - Tong_Phucap_Khong_Tinhthue - Phucap_Bhxh - Bhxh - Bhyt - Bhtn - Tuthien - Luong_Tangca_Khong_Chiuthue - Khautru_Banthan - Sotien_Nguoi_Phuthuoc;
                    //Tính thuế TNCN 
                    Thue_Tncn = Convert.ToDecimal(objAccounting.Acc_Khaithue_Tncn_Get_Luytien(Sotien_Conlai_Chiuthue, 1));
                    Sotien_Conlai_Datruthue = Tong_Tienluong - Thue_Tncn;
                    Thuclanh = Tong_Tienluong - Bhxh - Bhyt - Bhtn - Tamung_Ky1 - Thue_Tncn - Tuthien;

                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Tong_Tienluong", Tong_Tienluong);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Sotien_Conlai_Chiuthue", Sotien_Conlai_Chiuthue);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Thue_Tncn", Thue_Tncn);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Sotien_Conlai_Datruthue", Sotien_Conlai_Datruthue);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Thuclanh", Thuclanh);
                    break;
                case "Buluong":
                    //Buluong => Tong_Tienluong => Sotien_Conlai_Chiuthue => Sotien_Conlai_Datruthue => Thuclanh
                    Tong_Tienluong = Tienluong + Tong_Phucap_Tinhthue + Tong_Phucap_Khong_Tinhthue + Phucap_Bhxh + Buluong - Kyluat;
                    Sotien_Conlai_Chiuthue = Tong_Tienluong - Tong_Phucap_Khong_Tinhthue - Phucap_Bhxh - Bhxh - Bhyt - Bhtn - Tuthien - Luong_Tangca_Khong_Chiuthue - Khautru_Banthan - Sotien_Nguoi_Phuthuoc;
                    //Tính thuế TNCN 
                    Thue_Tncn = Convert.ToDecimal(objAccounting.Acc_Khaithue_Tncn_Get_Luytien(Sotien_Conlai_Chiuthue, 1));

                    Sotien_Conlai_Datruthue = Tong_Tienluong - Thue_Tncn;
                    Thuclanh = Tong_Tienluong - Bhxh - Bhyt - Bhtn - Tamung_Ky1 - Thue_Tncn - Tuthien;

                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Tong_Tienluong", Tong_Tienluong);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Sotien_Conlai_Chiuthue", Sotien_Conlai_Chiuthue);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Thue_Tncn", Thue_Tncn);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Sotien_Conlai_Datruthue", Sotien_Conlai_Datruthue);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Thuclanh", Thuclanh);
                    break;
                case "Tuthien":
                    //Tuthien => Sotien_Conlai_Chiuthue => Sotien_Conlai_Datruthue => Thuclanh
                    Tong_Tienluong = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tong_Tienluong") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tong_Tienluong"));
                    Sotien_Conlai_Chiuthue = Tong_Tienluong - Tong_Phucap_Khong_Tinhthue - Phucap_Bhxh - Bhxh - Bhyt - Bhtn - Tuthien - Luong_Tangca_Khong_Chiuthue - Khautru_Banthan - Sotien_Nguoi_Phuthuoc;
                    //Tính thuế TNCN 
                    Thue_Tncn = Convert.ToDecimal(objAccounting.Acc_Khaithue_Tncn_Get_Luytien(Sotien_Conlai_Chiuthue, 1));

                    Sotien_Conlai_Datruthue = Tong_Tienluong - Thue_Tncn;
                    Thuclanh = Tong_Tienluong - Bhxh - Bhyt - Bhtn - Tamung_Ky1 - Thue_Tncn - Tuthien;

                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Sotien_Conlai_Chiuthue", Sotien_Conlai_Chiuthue);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Thue_Tncn", Thue_Tncn);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Sotien_Conlai_Datruthue", Sotien_Conlai_Datruthue);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Thuclanh", Thuclanh);

                    break;
                case "Tamung_Ky1":
                    //Tamung_Ky1 => Thuclanh
                    Tong_Tienluong = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tong_Tienluong") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tong_Tienluong"));
                    Thuclanh = Tong_Tienluong - Bhxh - Bhyt - Bhtn - Tamung_Ky1 - Thue_Tncn - Tuthien;

                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Thuclanh", Thuclanh);

                    break;
                case "Luong_Tangca_Khong_Chiuthue":
                    //Luong_Tangca_Khong_Chiuthue => Sotien_Conlai_Chiuthue => Sotien_Conlai_Datruthue => Thuclanh
                    Tong_Tienluong = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tong_Tienluong") + "" == "" ? 0 : Convert.ToDecimal(gvrex_Luong_Tonghop.GetFocusedRowCellValue("Tong_Tienluong"));
                    Sotien_Conlai_Chiuthue = Tong_Tienluong - Tong_Phucap_Khong_Tinhthue - Phucap_Bhxh - Bhxh - Bhyt - Bhtn - Tuthien - Luong_Tangca_Khong_Chiuthue - Khautru_Banthan - Sotien_Nguoi_Phuthuoc;
                    //Tính thuế TNCN 
                    Thue_Tncn = Convert.ToDecimal(objAccounting.Acc_Khaithue_Tncn_Get_Luytien(Sotien_Conlai_Chiuthue, 1));
                    Sotien_Conlai_Datruthue = Tong_Tienluong - Thue_Tncn;
                    Thuclanh = Tong_Tienluong - Bhxh - Bhyt - Bhtn - Tamung_Ky1 - Thue_Tncn - Tuthien;

                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Sotien_Conlai_Chiuthue", Sotien_Conlai_Chiuthue);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Thue_Tncn", Thue_Tncn);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Sotien_Conlai_Datruthue", Sotien_Conlai_Datruthue);
                    gvrex_Luong_Tonghop.SetFocusedRowCellValue("Thuclanh", Thuclanh);
                    break;

            }
        }

       
        private void btnLuong_Tonghop_Init_Click(object sender, EventArgs e)
        {
           
        }

        private void btnTgluong_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Tienluong.Frmrex_Tgluong frmrex_Tgluong = new Ecm.Rex.Forms.Tienluong.Frmrex_Tgluong();
                GoobizFrame.Windows.PlugIn.RightHelpers.CheckUserRightAction(frmrex_Tgluong);
                GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmrex_Tgluong);

                frmrex_Tgluong._kyluong = id_kyluong;
                frmrex_Tgluong._id_nhansu = gvrex_Luong_Tonghop.GetFocusedRowCellValue("Id_Nhansu");
                frmrex_Tgluong.ma_nhansu = dsNhansu.Tables[0].Select(string.Format("Id_Nhansu={0}",
                    gvrex_Luong_Tonghop.GetFocusedRowCellValue("Id_Nhansu")))[0]["Ma_Nhansu"];
                frmrex_Tgluong.hoten_nhansu = dsNhansu.Tables[0].Select(string.Format("Id_Nhansu={0}",
                    gvrex_Luong_Tonghop.GetFocusedRowCellValue("Id_Nhansu")))[0]["Hoten_Nhansu"];

                frmrex_Tgluong.ShowDialog();

                //cập nhật thay đổi
                if (frmrex_Tgluong.HasChange)
                    btnLuong_Tonghop_Init_Click(sender, e);
            }
            catch { }
        }
 */
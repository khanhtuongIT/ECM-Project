using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;

namespace Ecm.Bar.Forms.Version2
{
    public partial class Frmbar_Table_Monitor_Pos :  GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        Ecm.WebReferences.Classes.BarService objBarService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.BarService>();
        Ecm.WebReferences.Classes.WareService objWareService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.WareService>();
        Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();

        DataSet dsTable_Order_Chitiet;
        DataSet dsWare_Dm_Nhom_Hanghoa_Ban;
        DataSet ds_Hanghoa_Ban;
        DataSet dsBar_Table_Order;
        DataRow[] sdr;
        int soluong_phucvu = 0;
        object id_cuahang_ban;
        object identity = null;
        object Id_Nhom_Hanghoa_Ban;
        //string LogTime = "init";

        #region local data
        DataSet dsSys_Lognotify = null;
        //XML local file
        string xml_WARE_DM_HANGHOA_BAN = @"Resources\localdata\WARE_DM_HANGHOA_BAN.xml";
        string xml_WARE_DM_NHOM_HANGHOA_BAN_BYBARMENU = @"Resources\localdata\WARE_DM_NHOM_HANGHOA_BAN_BYBARMENU.xml";
        string xml_bar_dm_table = @"Resources\localdata\bar_dm_table.xml";
        //datetime last change
        DateTime dtlc_ware_dm_hanghoa_ban;
        DateTime dtlc_ware_hanghoa_dinhgia;
        DateTime dtlc_bar_dm_menu_hanghoa_ban;
        DateTime dtlc_bar_dm_menu;
        DateTime dtlc_bar_dm_table;
        DateTime dtlc_bar_table_order_chitiet;
        DateTime dtlc_bar_table_order_chitiet_temp = DateTime.Now;
        #endregion

        public Frmbar_Table_Monitor_Pos()
        {
            InitializeComponent();
            DevExpress.XtraGrid.StyleFormatCondition conditionOld = new DevExpress.XtraGrid.StyleFormatCondition();
            conditionOld.Appearance.BackColor = Color.CornflowerBlue;
            conditionOld.Appearance.Options.UseBackColor = true;
            conditionOld.ApplyToRow = true;
            conditionOld.Column = this.cvware_Dm_Nhom_Hanghoa_Ban.Columns["Id_Nhom_Hanghoa_Ban"];
            conditionOld.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater;
            conditionOld.Value1 = 0;
            this.cvware_Dm_Nhom_Hanghoa_Ban.FormatConditions.Add(conditionOld);
            LoadMasterData();
            // this.DisplayInfo();
            this.BarSystem.Visible = false;
            xtraTabControl_Monitor.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            ShowTabPage(xtraTabPage_Nhom_Hanghoa_Ban);
            showButton(false);
        }

        #region display
        /// <summary>
        /// Show TabPage
        /// </summary>
        /// <param name="xtraTabPage"></param>
        void ShowTabPage(DevExpress.XtraTab.XtraTabPage xtraTabPage)
        {
            while (xtraTabControl_Monitor.TabPages.Count > 0)
                xtraTabControl_Monitor.TabPages.RemoveAt(0);
            xtraTabControl_Monitor.TabPages.Add(xtraTabPage);
        }

        void showButton(bool editTable)
        {
            btnChonNhom.Visible = editTable;
            btnBackDetail.Visible = editTable;
            btnNextDetail.Visible = editTable;
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

        /// <summary>
        /// Load data from server -> store on local PC
        /// </summary>
        void LoadMasterData()
        {
            try
            {
                if (!System.IO.Directory.Exists(@"Resources\localdata"))
                    System.IO.Directory.CreateDirectory(@"Resources\localdata");
                dsSys_Lognotify = objMasterService.Get_Sys_Lognotify_SelectLastChange_OfTables("[BAR_TABLE_ORDER_CHITIET]" ).ToDataSet();
                dtlc_bar_table_order_chitiet = GetLastChange_FrmLognotify("BAR_TABLE_ORDER_CHITIET");

                /*
                  if (!System.IO.Directory.Exists(@"Resources\localdata"))
                     System.IO.Directory.CreateDirectory(@"Resources\localdata");
                 dsSys_Lognotify = objMasterService.Get_Sys_Lognotify_SelectLastChange_OfTables(
                         "[WARE_DM_HANGHOA_BAN], [WARE_HANGHOA_DINHGIA], " +
                         "[BAR_DM_MENU_HANGHOA_BAN], [BAR_TABLE_ORDER_CHITIET], " +
                         "[BAR_DM_MENU], [bar_dm_table]").ToDataSet();

                 dtlc_ware_dm_hanghoa_ban = GetLastChange_FrmLognotify("WARE_DM_HANGHOA_BAN");
                 dtlc_ware_hanghoa_dinhgia = GetLastChange_FrmLognotify("WARE_HANGHOA_DINHGIA");
                 dtlc_bar_dm_menu = GetLastChange_FrmLognotify("BAR_DM_MENU");
                 dtlc_bar_dm_menu_hanghoa_ban = GetLastChange_FrmLognotify("BAR_DM_MENU_HANGHOA_BAN");
                 dtlc_bar_table_order_chitiet = GetLastChange_FrmLognotify("BAR_TABLE_ORDER_CHITIET");
                 dtlc_bar_dm_table = GetLastChange_FrmLognotify("bar_dm_table");
                 //load data from local xml when last change at local differ from database
                 if (!System.IO.File.Exists(xml_WARE_DM_HANGHOA_BAN)
                     || DateTime.Compare(dtlc_ware_dm_hanghoa_ban, System.IO.File.GetLastWriteTime(xml_WARE_DM_HANGHOA_BAN)) > 0
                     || DateTime.Compare(dtlc_bar_dm_menu, System.IO.File.GetLastWriteTime(xml_WARE_DM_HANGHOA_BAN)) > 0
                     || DateTime.Compare(dtlc_bar_dm_menu_hanghoa_ban, System.IO.File.GetLastWriteTime(xml_WARE_DM_HANGHOA_BAN)) > 0)
                 {
                     ds_Hanghoa_Ban = objMasterService.Get_All_Ware_Dm_Hanghoa_Ban().ToDataSet();
                     ds_Hanghoa_Ban.WriteXml(xml_WARE_DM_HANGHOA_BAN, XmlWriteMode.WriteSchema);

                     dsWare_Dm_Nhom_Hanghoa_Ban = objMasterService.Get_All_Ware_Dm_Nhom_Hanghoa_Ban_ByBarMenu().ToDataSet();
                     dsWare_Dm_Nhom_Hanghoa_Ban.WriteXml(xml_WARE_DM_NHOM_HANGHOA_BAN_BYBARMENU, XmlWriteMode.WriteSchema);
                 }
                 else
                 {
                     if (ds_Hanghoa_Ban == null || ds_Hanghoa_Ban.Tables.Count == 0)
                     {
                         ds_Hanghoa_Ban = new DataSet();
                         ds_Hanghoa_Ban.ReadXml(xml_WARE_DM_HANGHOA_BAN);
                     }
                     if (dsWare_Dm_Nhom_Hanghoa_Ban == null || dsWare_Dm_Nhom_Hanghoa_Ban.Tables.Count == 0)
                     {
                         dsWare_Dm_Nhom_Hanghoa_Ban = new DataSet();
                         dsWare_Dm_Nhom_Hanghoa_Ban.ReadXml(xml_WARE_DM_NHOM_HANGHOA_BAN_BYBARMENU);
                     }
                 }

                 if (!System.IO.File.Exists(xml_bar_dm_table)
                     || DateTime.Compare(dtlc_bar_dm_table, System.IO.File.GetLastWriteTime(xml_bar_dm_table)) > 0)
                 {
                     dsBar_Table_Order = objMasterService.Get_All_Bar_Dm_Table().ToDataSet();
                     dsBar_Table_Order.WriteXml(xml_bar_dm_table, XmlWriteMode.WriteSchema);
                 }
                 else if (dsBar_Table_Order == null || dsBar_Table_Order.Tables.Count == 0)
                 {
                     dsBar_Table_Order = new DataSet();
                     dsBar_Table_Order.ReadXml(xml_bar_dm_table);
                 }

                
                 */
                dsWare_Dm_Nhom_Hanghoa_Ban = objMasterService.Get_All_Ware_Dm_Nhom_Hanghoa_Ban_ByBarMenu().ToDataSet();
                ds_Hanghoa_Ban = objMasterService.Get_All_Ware_Dm_Hanghoa_Ban().ToDataSet();
                dsBar_Table_Order = objMasterService.Get_All_Bar_Dm_Table().ToDataSet();

                gridLookUp_Hanghoa_Ban.DataSource = ds_Hanghoa_Ban.Tables[0];
                //Get_All_Bar_Table_Order
                gridLookUp_Table.DataSource = dsBar_Table_Order.Tables[0];
                //dgware_Dm_Nhom_Hanghoa_Ban
                dgware_Dm_Nhom_Hanghoa_Ban.DataSource = dsWare_Dm_Nhom_Hanghoa_Ban;
                dgware_Dm_Nhom_Hanghoa_Ban.DataMember = dsWare_Dm_Nhom_Hanghoa_Ban.Tables[0].TableName;
                int i = Convert.ToInt32(dsWare_Dm_Nhom_Hanghoa_Ban.Tables[0].Rows[0]["Id_Nhom_Hanghoa_Ban"]);
                foreach (DataRow drNhom_Hanghoa_Ban in dsWare_Dm_Nhom_Hanghoa_Ban.Tables[0].Rows)
                {

                    DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                    styleFormatCondition.Appearance.BackColor = GoobizFrame.Windows.MdiUtils.ThemeSettings.GetColor(Convert.ToInt32(drNhom_Hanghoa_Ban["Id_Nhom_Hanghoa_Ban"]) % i);
                    styleFormatCondition.Appearance.Options.UseBackColor = true;
                    styleFormatCondition.ApplyToRow = true;
                    styleFormatCondition.Column = this.cvware_Dm_Nhom_Hanghoa_Ban.Columns["Id_Nhom_Hanghoa_Ban"];
                    styleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                    styleFormatCondition.Value1 = Convert.ToInt32(drNhom_Hanghoa_Ban["Id_Nhom_Hanghoa_Ban"]);
                    this.cvware_Dm_Nhom_Hanghoa_Ban.FormatConditions.Add(styleFormatCondition);
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.TrayMessage.TrayMessage.Status = new GoobizFrame.Windows.TrayMessage.TrayMessageInfo(MessageBoxIcon.Asterisk, ex.Message, ex.StackTrace);
            }
        }

        public override void DisplayInfo()
        {
            lblStatus_Bar.Text = "";
            id_cuahang_ban =  GoobizFrame.Windows.MdiUtils.ThemeSettings.GetLocation("Id_Cuahang_Ban");
            try
            {
                //Id_Nhom_Hanghoa_Ban = lookUpEdit_Nhom_Hanghoa_Ban.EditValue;
                if (Id_Nhom_Hanghoa_Ban != null)
                {
                    //Get_All_Bar_Table_Order_Chitiet
                    dsTable_Order_Chitiet = objBarService.Get_All_Bar_Table_Order_Chitiet_ById_Nhom_Hanghoa_Ban(Id_Nhom_Hanghoa_Ban, id_cuahang_ban).ToDataSet();
                    dgTable_Order_Chitiet.DataSource = dsTable_Order_Chitiet.Tables[0];
                }
                else
                {
                    //Get_All_Bar_Table_Order_Chitiet
                    dsTable_Order_Chitiet = objBarService.Get_All_Bar_Table_Order_Chitiet().ToDataSet();
                    dgTable_Order_Chitiet.DataSource = dsTable_Order_Chitiet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                ShowTabPage(xtraTabPage_Lock);
                lblMessage.Text = ex.Message;
                GoobizFrame.Windows.TrayMessage.TrayMessage.Status = new GoobizFrame.Windows.TrayMessage.TrayMessageInfo(MessageBoxIcon.Asterisk, ex.Message, ex.StackTrace);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.MdiParent != null && this.MdiParent.ActiveMdiChild.Name != this.Name)
                return;
            LoadMasterData();
            lblStatus_Bar.Text = "";
            if (DateTime.Compare(dtlc_bar_table_order_chitiet, dtlc_bar_table_order_chitiet_temp) != 0)
            {
                this.DisplayInfo();
                dtlc_bar_table_order_chitiet_temp = dtlc_bar_table_order_chitiet;
            }
        }
        #endregion

        #region process gridview

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            //for (int i = 1; i < 10; i++)
            //{
            //    System.Media.SystemSounds.Asterisk.Play();
            //    System.Media.SystemSounds.Question.Play();
            //    System.Media.SystemSounds.Hand.Play();
            //}
        }

        void cvware_Dm_Nhom_Hanghoa_Ban_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Card.ViewInfo.CardHitInfo cardHit = cvware_Dm_Nhom_Hanghoa_Ban.CalcHitInfo(e.X, e.Y);
                if (cardHit.InCard)
                {
                    Id_Nhom_Hanghoa_Ban = dsWare_Dm_Nhom_Hanghoa_Ban.Tables[0].Rows[cardHit.RowHandle]["Id_Nhom_hanghoa_Ban"];
                    //   xtraTabControl_Monitor.TabPages.Remove(xtraTabPage_Nhom_Hanghoa_Ban);             

                    LoadMasterData();
                    dsTable_Order_Chitiet = objBarService.Get_All_Bar_Table_Order_Chitiet_ById_Nhom_Hanghoa_Ban(Id_Nhom_Hanghoa_Ban, id_cuahang_ban).ToDataSet();
                    dgTable_Order_Chitiet.DataSource = dsTable_Order_Chitiet;
                    dgTable_Order_Chitiet.DataMember = dsTable_Order_Chitiet.Tables[0].TableName;
                    ShowTabPage(xtraTabPage_Monitor);
                    showButton(true);
                }
            }
            catch (Exception ex)
            {
                 GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
            }
        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            timer1.Stop();
            object value =  GoobizFrame.Windows.Forms.FrmGNumboardInput.ShowInputDialog(
               "" + gridView1.GetFocusedRowCellValue("" + gridView1.FocusedColumn.FieldName));

            if (value.ToString().Contains("."))
            {
                lblStatus_Bar.Text = "Số lượng phải là số nguyên";
                return;
            }
            if (value.ToString().Contains("-"))
            {
                 GoobizFrame.Windows.Forms.MessageDialog.Show("Số lượng không được nhập số âm");
                value = value.ToString().Replace("-", "");
            }
            if (Convert.ToInt32(value) == 0)
            {
                lblStatus_Bar.Text = "Số lượng phục vụ không được bằng 0";
                return;
            }
            gridView1.SetFocusedRowCellValue(gridView1.FocusedColumn, value);
            gridView1.RefreshRow(gridView1.FocusedRowHandle);
            bool hotel = false;
            sdr = dsTable_Order_Chitiet.Tables[0].Select("Id_Table_Order_Chitiet_2 = " + dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Id_Table_Order_Chitiet_2"]);
            identity = sdr[0]["Id_Table_Order_Chitiet"];
            if ("" + identity == "")
            {
                identity = sdr[0]["Id_Bar_Rent_Checkin_Table_Hanghoa"];
                hotel = true;
            }
            soluong_phucvu = Convert.ToInt32("0" + dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong_Phucvu"]) + Convert.ToInt32("0" + dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong_Phucvu_Temp"]);
            //soluong_phucvu = Convert.ToInt32("0" + dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong_Phucvu"]);          
            if (Convert.ToInt32(sdr[0]["Soluong"]) < Convert.ToInt32(soluong_phucvu))
            {
                lblStatus_Bar.Text = "Số lượng phục vụ lớn hơn số lượng yêu cầu";
                dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong_Phucvu_Temp"] = dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong_CanPV"];
                gridView1.RefreshRow(gridView1.FocusedRowHandle);
                timer1.Start();
                return;
            }
            objBarService.Update_Bar_Table_Order_Chitiet(identity, soluong_phucvu, hotel);
            DisplayInfo();
            timer1.Start();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                DataRow dr_change = gridView1.GetDataRow(e.RowHandle);
                if (e.Column.FieldName == "Soluong_Phucvu_Temp")
                {
                    if (Convert.ToInt32(dr_change["Soluong_Phucvu_Temp"]) == 0)
                    {
                        lblStatus_Bar.Text = "Số lượng phục vụ không được bằng 0";
                        dr_change.RejectChanges();
                        return;
                    }
                    sdr = dsTable_Order_Chitiet.Tables[0].Select("Id_Table_Order_Chitiet_2 = " + dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Id_Table_Order_Chitiet_2"]);
                    identity = sdr[0]["Id_Table_Order_Chitiet"];
                    soluong_phucvu =
                        Convert.ToInt32("0" + dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong_Phucvu"])
                        + Convert.ToInt32("0" + dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong_Phucvu_Temp"]);
                    if (soluong_phucvu == 0)
                    {
                        timer1.Start();
                        return;
                    }
                    if (Convert.ToInt32(sdr[0]["Soluong"]) < Convert.ToInt32(soluong_phucvu))
                    {
                        lblStatus_Bar.Text = "Số lượng phục vụ lớn hơn số lượng yêu cầu";
                        gridView1.SetFocusedRowCellValue(gridView1.Columns["Soluong_Phucvu_Temp"], dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong"]);
                        timer1.Start();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                 GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                bool hotel = false;
                sdr = dsTable_Order_Chitiet.Tables[0].Select("Id_Table_Order_Chitiet_2 = " + dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Id_Table_Order_Chitiet_2"]);
                identity = sdr[0]["Id_Table_Order_Chitiet"];
                if ("" + identity == "")
                {
                    identity = sdr[0]["Id_Bar_Rent_Checkin_Table_Hanghoa"];
                    hotel = true;
                }
                soluong_phucvu =
                    Convert.ToInt32("0" + dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong_Phucvu"])
                    + Convert.ToInt32("0" + dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong_Phucvu_Temp"]);
                if (soluong_phucvu == 0)
                {
                    timer1.Start();
                    return;
                }
                if (Convert.ToInt32(sdr[0]["Soluong"]) < Convert.ToInt32(soluong_phucvu))
                {
                    lblStatus_Bar.Text = "Số lượng phục vụ lớn hơn số lượng yêu cầu";
                    dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong_Phucvu"] = dsTable_Order_Chitiet.Tables[0].Rows[gridView1.FocusedRowHandle]["Soluong"];
                    timer1.Start();
                    return;
                }
                objBarService.Update_Bar_Table_Order_Chitiet(identity, soluong_phucvu, hotel);
                DisplayInfo();
                timer1.Start();
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
            }
        }

        #endregion

        #region process buttons

        private void btnBackDetail_Click(object sender, EventArgs e)
        {
            gridView1.MovePrevPage();
        }

        private void btnNextDetail_Click(object sender, EventArgs e)
        {
            gridView1.MoveNextPage();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnChonNhom_Click(object sender, EventArgs e)
        {
            ShowTabPage(xtraTabPage_Nhom_Hanghoa_Ban);
            showButton(false);
        }

        #endregion
    }
}


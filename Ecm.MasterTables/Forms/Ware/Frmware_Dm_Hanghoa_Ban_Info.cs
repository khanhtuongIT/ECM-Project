using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;


namespace Ecm.MasterTables.Forms.Ware
{
    public partial class Frmware_Dm_Hanghoa_Ban_Info :  GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        DataSet ds_Collection = new DataSet();
        DataSet dsWare_Dm_Loai_Hanghoa_Ban;
        DataSet ds_Congthuc_Phache_Chitiet;

        #region local data
        DataSet dsSys_Lognotify = null;
        string xml_WARE_DM_HANGHOA_BAN = @"Resources\localdata\Ware_Dm_Hanghoa_Ban.xml";
        string xml_WARE_DM_LOAI_HANGHOA_BAN = @"Resources\localdata\Ware_Dm_Loai_Hanghoa_Ban.xml";
        DateTime dtlc_ware_dm_hanghoa_ban;
        DateTime dtlc_ware_dm_loai_hanghoa;
        #endregion

        public Frmware_Dm_Hanghoa_Ban_Info()
        {
            InitializeComponent();
            if (!System.IO.Directory.Exists(@"Resources\localdata"))
                System.IO.Directory.CreateDirectory(@"Resources\localdata");
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            DisplayInfo();
            item_Add.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            item_Edit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            item_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            item_Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            item_Cancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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

        public void ShowTabPage(DevExpress.XtraTab.XtraTabPage xtraTabPage)
        {
            while (xtraTabControl1.TabPages.Count > 1)
            {
                xtraTabControl1.TabPages.RemoveAt(0);
                xtraTabControl1.TabPages.Add(xtraTabPage);
            }
        }

        void LoadMasterData()
        {
            dsSys_Lognotify = objMasterService.Get_Sys_Lognotify_SelectLastChange_OfTables("[ware_dm_hanghoa_ban], "
                   + "[ware_dm_loai_hanghoa_ban]").ToDataSet();

            dtlc_ware_dm_hanghoa_ban    = GetLastChange_FrmLognotify("WARE_DM_HANGHOA_BAN");
            dtlc_ware_dm_loai_hanghoa   = GetLastChange_FrmLognotify("ware_dm_loai_hanghoa_ban");

            //load data from local xml when last change at local differ from database
            if (DateTime.Compare(dtlc_ware_dm_hanghoa_ban, System.IO.File.GetLastWriteTime(xml_WARE_DM_HANGHOA_BAN)) > 0
                || !System.IO.File.Exists(xml_WARE_DM_HANGHOA_BAN))
            {
                ds_Collection = objMasterService.Get_All_Ware_Dm_Hanghoa_Ban().ToDataSet();
                ds_Collection.WriteXml(xml_WARE_DM_HANGHOA_BAN, XmlWriteMode.WriteSchema);
            }
            else
            {
                if (ds_Collection == null || ds_Collection.Tables.Count == 0)
                {
                    ds_Collection = new DataSet();
                    ds_Collection.ReadXml(xml_WARE_DM_HANGHOA_BAN);
                }
            }
            if (DateTime.Compare(dtlc_ware_dm_loai_hanghoa, System.IO.File.GetLastWriteTime(xml_WARE_DM_LOAI_HANGHOA_BAN)) > 0
                || !System.IO.File.Exists(xml_WARE_DM_LOAI_HANGHOA_BAN))
            {
                dsWare_Dm_Loai_Hanghoa_Ban = objMasterService.Get_All_Ware_Dm_Loai_Hanghoa_Ban().ToDataSet();
                dsWare_Dm_Loai_Hanghoa_Ban.WriteXml(xml_WARE_DM_LOAI_HANGHOA_BAN, XmlWriteMode.WriteSchema);
            }
            else
            {
                if (dsWare_Dm_Loai_Hanghoa_Ban == null || dsWare_Dm_Loai_Hanghoa_Ban.Tables.Count == 0)
                {
                    dsWare_Dm_Loai_Hanghoa_Ban = new DataSet();
                    dsWare_Dm_Loai_Hanghoa_Ban.ReadXml(xml_WARE_DM_LOAI_HANGHOA_BAN);
                }
            }
            gridLookUp_Hanghoa_Mua.DataSource   = ds_Collection.Tables[0];
            dgware_Dm_Hanghoa_Ban.DataSource        = ds_Collection.Tables[0];
            dgware_Dm_Hanghoa_Ban.DataSource        = ds_Collection;
            dgware_Dm_Hanghoa_Ban.DataMember        = ds_Collection.Tables[0].TableName;
        }

        public override void DisplayInfo()
        {
            LoadMasterData();
            dgware_Dm_Loai_Hanghoa_Ban.DataSource = dsWare_Dm_Loai_Hanghoa_Ban.Tables[0];
            if (cardView2.FormatConditions.Count == 0)
                if (dsWare_Dm_Loai_Hanghoa_Ban.Tables[0].Rows.Count > 0)
                {
                    int i = Convert.ToInt32(dsWare_Dm_Loai_Hanghoa_Ban.Tables[0].Rows[0]["Id_Nhom_Hanghoa_Ban"]);
                    foreach (DataRow dr in dsWare_Dm_Loai_Hanghoa_Ban.Tables[0].Rows)
                    {
                        DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                        styleFormatCondition.Appearance.BackColor =  GoobizFrame.Windows.MdiUtils.ThemeSettings.GetColor(Convert.ToInt32(dr["Id_Nhom_Hanghoa_Ban"]) % i);
                        styleFormatCondition.Appearance.Options.UseBackColor = true;
                        styleFormatCondition.ApplyToRow = true;
                        styleFormatCondition.Column = cardView2.Columns["Id_Nhom_Hanghoa_Ban"];
                        styleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                        styleFormatCondition.Value1 = Convert.ToInt32(dr["Id_Nhom_Hanghoa_Ban"]);
                        cardView2.FormatConditions.Add(styleFormatCondition);
                    }
                    cardView2.Columns["Id_Nhom_Hanghoa_Ban"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                }
            xtraTabControl1.TabPages.Remove(xtraTabPage2);
            // DisplayInfo2();
            base.DisplayInfo();
        }

        void DisplayInfo2()
        {
            object Id_Hanghoa_Ban = cardView1.GetFocusedRowCellValue("Id_Hanghoa_Ban");
            if (Id_Hanghoa_Ban == null)
            {
                dgware_Dm_Congthuc_Phache_Chitiet.DataSource = null;
                return;
            }
            ds_Congthuc_Phache_Chitiet = objMasterService.Get_All_Ware_Dm_Congthuc_Phache_Chitiet_ByHHBan(Id_Hanghoa_Ban).ToDataSet();
            dgware_Dm_Congthuc_Phache_Chitiet.DataSource = ds_Congthuc_Phache_Chitiet;
            dgware_Dm_Congthuc_Phache_Chitiet.DataMember = ds_Congthuc_Phache_Chitiet.Tables[0].TableName;
            try
            {
                //Get image data from gridview column.
                if ("" + cardView1.GetFocusedRowCellValue("Hinh") != "")
                {
                    byte[] imagedata = (byte[])cardView1.GetFocusedRowCellValue("Hinh");

                    //Read image data into a memory stream
                    MemoryStream ms = new MemoryStream(imagedata, 0, imagedata.Length);
                    ms.Write(imagedata, 0, imagedata.Length);

                    //Set image variable value using memory stream.
                    Image image = Image.FromStream(ms, true);
                    pic_Hinh.Image = image;
                }
                else
                {
                    pic_Hinh.Image = null;
                }
            }
            catch { }
        }

        private void cardView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayInfo2();
        }

        private void lookUpEdit_Loai_Hanghoa_Ban_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //cardView1.Columns["Id_Loai_Hanghoa_Ban"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(
                //cardView1.Columns["Id_Loai_Hanghoa_Ban"], lookUpEdit_Loai_Hanghoa_Ban.EditValue);
            }
            catch { }
        }

        private void cardView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            DisplayInfo2();
        }

        private void lookUpEdit_Loai_Hanghoa_Ban_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                Frmware_Dm_Loai_Hanghoa_Ban_Dialog frmware_Dm_Loai_Hanghoa_Ban_Dialog = new Frmware_Dm_Loai_Hanghoa_Ban_Dialog();
                //frmware_Dm_Loai_Hanghoa_Ban_Dialog.Text = lblId_Loai_Hanghoa_Ban.Text;
                 GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmware_Dm_Loai_Hanghoa_Ban_Dialog);
                frmware_Dm_Loai_Hanghoa_Ban_Dialog.ShowDialog();
                if (frmware_Dm_Loai_Hanghoa_Ban_Dialog.SelectedRows != null && frmware_Dm_Loai_Hanghoa_Ban_Dialog.SelectedRows.Length > 0)
                {
                    //lookUpEdit_Loai_Hanghoa_Ban.EditValue = frmware_Dm_Loai_Hanghoa_Ban_Dialog.SelectedRows[0]["Id_Loai_Hanghoa_Ban"];
                }
            }
        }

        private void cardView2_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Card.ViewInfo.CardHitInfo carHit = cardView2.CalcHitInfo(e.X, e.Y);
            if (carHit.InCard)
            {
                xtraTabControl1.TabPages.Add(xtraTabPage2);
                object id_loai_hanghoa_ban = cardView2.GetRowCellValue(carHit.RowHandle, "Id_Loai_Hanghoa_Ban");
                cardView1.Columns["Id_Loai_Hanghoa_Ban"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("Id_Loai_Hanghoa_Ban = " + id_loai_hanghoa_ban);
                xtraTabControl1.SelectedTabPage = xtraTabPage2;
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
                xtraTabControl1.TabPages.Remove(xtraTabPage2);
        }

        private void btnBack_Loai_Hanghoa_Ban_Click(object sender, EventArgs e)
        {
            cardView2.MovePrevPage();
        }

        private void btnNext_Loai_Hanghoa_Ban_Click(object sender, EventArgs e)
        {
            cardView2.MoveNextPage();
        }

        private void btnBack_Hanghoa_Ban_Click(object sender, EventArgs e)
        {
            cardView1.MovePrevPage();
        }

        private void btnNext_Hanghoa_Ban_Click(object sender, EventArgs e)
        {
            cardView1.MoveNextPage();
        }

        private void btnLoai_Hanghoa_Ban_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }

    }
}


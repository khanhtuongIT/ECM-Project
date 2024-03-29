using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace Ecm.MasterTables.Forms.Rex
{
    public partial class Frmrex_Dm_Chucvu_Add :  GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        DataSet ds_Chucvu = new DataSet();
        public Ecm.WebReferences.MasterService.Rex_Dm_Chucvu Selected_Rex_Dm_Chucvu;
        object heso_phucap; object luong_phucap;
        bool ShowColumn = false;
        public DataRow[] row_Select;

        public Frmrex_Dm_Chucvu_Add()
        {
            InitializeComponent();
            this.DisplayInfo();
        }

        public Frmrex_Dm_Chucvu_Add(bool show)
        {
            InitializeComponent();
            ShowColumn = true;
            this.DisplayInfo();
        }

        public override void DisplayInfo()
        {
            try
            {
                ds_Chucvu = objMasterService.Get_All_Rex_Dm_Chucvu_Collection().ToDataSet();
                dgrex_Dm_Chucvu.DataSource = ds_Chucvu;
                dgrex_Dm_Chucvu.DataMember = ds_Chucvu.Tables[0].TableName;

                if (ShowColumn)
                {
                    item_Add.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    item_Cancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    item_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    item_Edit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    item_Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    if (!ds_Chucvu.Tables[0].Columns.Contains("Chon"))
                    {
                        DataColumn col = new DataColumn("Chon", typeof(bool));
                        col.ReadOnly = false;
                        ds_Chucvu.Tables[0].Columns.Add(col);
                        gvrex_Dm_Chucvu.Columns["Chon"].Visible = true;
                        gvrex_Dm_Chucvu.Columns["Chon"].VisibleIndex = 0;
                    }
                    foreach (GridColumn c in gvrex_Dm_Chucvu.Columns)
                    {
                        if (c.FieldName != "Chon")
                            c.OptionsColumn.AllowEdit = false;
                    }
                }
                else
                {
                    this.Data = ds_Chucvu;
                    this.GridControl = dgrex_Dm_Chucvu;
                }

                this.DataBindingControl();

                this.ChangeStatus(false);

                this.gvrex_Dm_Chucvu.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void ClearDataBindings()
        {
            this.txtMa_Chucvu.DataBindings.Clear();
            this.txtTen_Chucvu.DataBindings.Clear();
            this.txtHeso.DataBindings.Clear();
            this.txtLuong_Chucvu.DataBindings.Clear();
        }

        public void DataBindingControl()
        {
            try
            {
                ClearDataBindings();
                this.txtMa_Chucvu.DataBindings.Add("EditValue", ds_Chucvu, ds_Chucvu.Tables[0].TableName + ".Ma_Chucvu");
                this.txtTen_Chucvu.DataBindings.Add("EditValue", ds_Chucvu, ds_Chucvu.Tables[0].TableName + ".Ten_Chucvu");
                this.txtHeso.DataBindings.Add("EditValue", ds_Chucvu, ds_Chucvu.Tables[0].TableName + ".Heso_Chucvu");
                this.txtLuong_Chucvu.DataBindings.Add("EditValue", ds_Chucvu, ds_Chucvu.Tables[0].TableName + ".Luong_Chucvu");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChangeStatus(bool editTable)
        {
            //this.dgrex_Dm_Chucvu.Enabled = !editTable;
            this.gvrex_Dm_Chucvu.OptionsBehavior.Editable = !editTable;
            this.txtMa_Chucvu.Properties.ReadOnly = !editTable;
            this.txtTen_Chucvu.Properties.ReadOnly = !editTable;
            this.txtHeso.Properties.ReadOnly = !editTable;
            this.txtLuong_Chucvu.Properties.ReadOnly = !editTable;
        }

        public void ResetText()
        {
            this.txtMa_Chucvu.EditValue = "";
            this.txtTen_Chucvu.EditValue = "";
            this.txtHeso.EditValue = 0;
            this.txtLuong_Chucvu.EditValue = 0;
        }

        private void Frmrex_Dm_Chucvu_Add_Load(object sender, EventArgs e)
        {
            this.DisplayInfo();
        }

        #region Event Override
        public object InsertObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Chucvu objRex_Dm_Chucvu = new Ecm.WebReferences.MasterService.Rex_Dm_Chucvu();

            objRex_Dm_Chucvu.Id_Chucvu = -1;
            objRex_Dm_Chucvu.Ma_Chucvu = txtMa_Chucvu.EditValue;
            objRex_Dm_Chucvu.Ten_Chucvu = txtTen_Chucvu.EditValue;
            objRex_Dm_Chucvu.Heso_Chucvu = txtHeso.EditValue;
            objRex_Dm_Chucvu.Luong_Chucvu = txtLuong_Chucvu.EditValue;

            return objMasterService.Insert_Rex_Dm_Chucvu(objRex_Dm_Chucvu);
        }

        public object UpdateObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Chucvu objRex_Dm_Chucvu = new Ecm.WebReferences.MasterService.Rex_Dm_Chucvu();
            objRex_Dm_Chucvu.Id_Chucvu = gvrex_Dm_Chucvu.GetFocusedRowCellValue("Id_Chucvu");
            objRex_Dm_Chucvu.Ma_Chucvu = txtMa_Chucvu.EditValue;
            objRex_Dm_Chucvu.Ten_Chucvu = txtTen_Chucvu.EditValue;
            objRex_Dm_Chucvu.Heso_Chucvu = txtHeso.EditValue;
            objRex_Dm_Chucvu.Luong_Chucvu = txtLuong_Chucvu.EditValue;

            return objMasterService.Update_Rex_Dm_Chucvu(objRex_Dm_Chucvu);
        }

        public object DeleteObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Chucvu objRex_Dm_Chucvu = new Ecm.WebReferences.MasterService.Rex_Dm_Chucvu();
            objRex_Dm_Chucvu.Id_Chucvu = gvrex_Dm_Chucvu.GetFocusedRowCellValue("Id_Chucvu");

            return objMasterService.Delete_Rex_Dm_Chucvu(objRex_Dm_Chucvu);
        }

        public override bool PerformAdd()
        {
            ClearDataBindings();
            this.ChangeStatus(true);
            this.ResetText();
            return true;
        }

        public override bool PerformEdit()
        {
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
            try
            {
                bool success = false;

                 GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new  GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(txtMa_Chucvu, lblMa_Chucvu.Text);
                hashtableControls.Add(txtTen_Chucvu, lblTen_Chucvu.Text);
                hashtableControls.Add(txtHeso, lblHeso.Text);
                hashtableControls.Add(txtLuong_Chucvu, lblLuong_Chucvu.Text);

                if (! GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;
                System.Collections.Hashtable htb = new System.Collections.Hashtable();
                htb.Add(txtMa_Chucvu, lblMa_Chucvu.Text);

                if (Double.Parse(this.txtHeso.Text) < 1)
                {
                    MessageBox.Show("Hệ số phải lớn hơn 0!");
                    this.txtHeso.Focus();
                    return false;
                }
                else if (Double.Parse(this.txtLuong_Chucvu.Text) < 1)
                {
                    MessageBox.Show("Lương tăng thêm phải lớn hơn 0!");
                    this.txtLuong_Chucvu.Focus();
                    return false;
                }

                if (this.FormState ==  GoobizFrame.Windows.Forms.FormState.Add)
                {
                    if (! GoobizFrame.Windows.MdiUtils.Validator.CheckExistValues(htb, (DataSet)dgrex_Dm_Chucvu.DataSource, "Ma_Chucvu"))
                        return false;
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
                if (ex.ToString().IndexOf("exists") != -1)
                {
                     GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblTen_Chucvu.Text, lblTen_Chucvu.Text });
                }
                return false;
            }
        }

        public override bool PerformSaveChanges()
        {
             GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new  GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gvrex_Dm_Chucvu.Columns["Ma_Chucvu"], "");
            hashtableControls.Add(gvrex_Dm_Chucvu.Columns["Ten_Chucvu"], "");
            hashtableControls.Add(gvrex_Dm_Chucvu.Columns["Heso_Chucvu"], "");
            hashtableControls.Add(gvrex_Dm_Chucvu.Columns["Luong_Chucvu"], "");

            if (! GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gvrex_Dm_Chucvu))
                return false;

            /*if (Double.Parse(heso_phucap.ToString()) < 1)
            {
                MessageBox.Show("Hệ số phải lớn hơn 0!");
                return false;
            }
            else if (Double.Parse(luong_phucap.ToString()) < 1)
            {
                MessageBox.Show("Lương tăng thêm phải lớn hơn 0!");
                return false;
            }*/

            try
            {
                this.DoClickEndEdit(dgrex_Dm_Chucvu);//dgrex_Dm_Chucvu.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                ds_Chucvu.Tables[0].Columns["Ma_Chucvu"].Unique = true;
                objMasterService.Update_Rex_Dm_Chucvu_Collection(this.ds_Chucvu);
            }
            catch (Exception ex)
            {
                if (ex.ToString().IndexOf("Unique") != -1)
                {
                     GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblMa_Chucvu.Text, lblMa_Chucvu.Text });
                    return false;
                }
                //MessageBox.Show(ex.ToString());

            }
            this.DisplayInfo();
            return true;
        }

        public override bool PerformDelete()
        {
            if ( GoobizFrame.Windows.Forms.UserMessage.Show("Msg00004", new string[]  {
             GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Rex_Dm_Chucvu"),
             GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Rex_Dm_Chucvu")   }) == DialogResult.Yes)
            {
                try
                {
                    if (Convert.ToInt32(objMasterService.GetExistReferences("Rex_Dm_Chucvu", "Id_Chucvu", this.gvrex_Dm_Chucvu.GetFocusedRowCellValue("Id_Chucvu"))) > 0)
                    {
                         GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                        return true;
                    }
                    this.DeleteObject();
                }
                catch (Exception ex)
                {
                     GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                }
                this.DisplayInfo();
            }
            return base.PerformDelete();
        }

        public override object PerformSelectOneObject()
        {
            DoClickEndEdit(dgrex_Dm_Chucvu);
            if (ShowColumn)
            {
                row_Select = ds_Chucvu.Tables[0].Select("Chon='True'");
                this.Dispose();
                this.Close();
                return true;
            }

            Ecm.WebReferences.MasterService.Rex_Dm_Chucvu rex_Dm_Chucvu = new Ecm.WebReferences.MasterService.Rex_Dm_Chucvu();
            try
            {
                int focusedRow = gvrex_Dm_Chucvu.GetDataSourceRowIndex(gvrex_Dm_Chucvu.FocusedRowHandle);
                DataRow dr = ds_Chucvu.Tables[0].Rows[focusedRow];
                if (dr != null)
                {
                    rex_Dm_Chucvu.Id_Chucvu = dr["Id_Chucvu"];
                    rex_Dm_Chucvu.Ma_Chucvu = dr["Ma_Chucvu"];
                    rex_Dm_Chucvu.Ten_Chucvu = dr["Ten_Chucvu"];
                    rex_Dm_Chucvu.Heso_Chucvu = dr["Heso_Chucvu"];
                    rex_Dm_Chucvu.Luong_Chucvu = dr["Luong_Chucvu"];
                }
                Selected_Rex_Dm_Chucvu = rex_Dm_Chucvu;
                this.Dispose();
                this.Close();
                return rex_Dm_Chucvu;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
                return null;
            }
        }

        #endregion

        #region Even

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            this.addnewrow_clicked = true;
            gvrex_Dm_Chucvu.SetFocusedRowCellValue(gvrex_Dm_Chucvu.Columns["Heso_Chucvu"], 0);
            gvrex_Dm_Chucvu.SetFocusedRowCellValue(gvrex_Dm_Chucvu.Columns["Luong_Chucvu"], 0);
        }

        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            ColumnView view_sender = sender as ColumnView;
            GridColumn col1 = view_sender.Columns["Heso_Chucvu"];
            GridColumn col2 = view_sender.Columns["Luong_Chucvu"];
            heso_phucap = view_sender.GetRowCellValue(e.RowHandle, col1);
            luong_phucap = view_sender.GetRowCellValue(e.RowHandle, col2);
        }

        private void dgrex_Dm_Chucvu_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if ("" + this.gvrex_Dm_Chucvu.GetFocusedRowCellValue("Id_Chucvu") != "")
                    if (Convert.ToInt32(objMasterService.GetExistReferences("Rex_Dm_Chucvu", "Id_Chucvu", this.gvrex_Dm_Chucvu.GetFocusedRowCellValue("Id_Chucvu"))) > 0)
                    {

                         GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                        e.Handled = true;
                    }
            }
        }

        private void txtMa_Chucvu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar).Equals(39))
                e.Handled = true;
        }

        private void txtHeso_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if ("" + e.NewValue == "")
                gvrex_Dm_Chucvu.SetFocusedRowCellValue(gvrex_Dm_Chucvu.FocusedColumn, null);
            else if ("" + e.NewValue != "" && e.NewValue.ToString().Length > 10)
                e.Cancel = true;
        }

        private void txtLuong_Chucvu_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if ("" + e.NewValue == "")
                gvrex_Dm_Chucvu.SetFocusedRowCellValue(gvrex_Dm_Chucvu.FocusedColumn, null);
            else if ("" + e.NewValue != "" && e.NewValue.ToString().Length > 15)
                e.Cancel = true;
        }

        private void gridText_Heso_Chucvu_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if ("" + e.NewValue == "")
                gvrex_Dm_Chucvu.SetFocusedRowCellValue(gvrex_Dm_Chucvu.FocusedColumn, null);
            else if ("" + e.NewValue != "" && e.NewValue.ToString().Length > 10)
                e.Cancel = true;
        }

        private void gridText_Luong_Chucvu_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if ("" + e.NewValue == "")
                gvrex_Dm_Chucvu.SetFocusedRowCellValue(gvrex_Dm_Chucvu.FocusedColumn, null);
            else if ("" + e.NewValue != "" && e.NewValue.ToString().Length > 15)
                e.Cancel = true;
        }
        #endregion

 
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;

namespace Ecm.MasterTables.Forms.Rex
{
    public partial class Frmrex_Dm_Ca_Lamviec_Add :  GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        //public Rex.Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        DataSet ds_Collection = new DataSet();
        public DataRow[] SelectedRows;

        public Frmrex_Dm_Ca_Lamviec_Add()
        {
            InitializeComponent();
            this.DisplayInfo();
            this.item_Select.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            gridView1.Columns["Chon"].Visible = false;
        }

        #region Event Override
        
        public override void DisplayInfo()
        {
            try
            {
                ds_Collection = objMasterService.Get_All_Rex_Dm_Ca_Lamviec().ToDataSet();
                ds_Collection.Tables[0].Columns.Add("Chon", typeof(bool));
                dgrex_Dm_Ca_Lamviec.DataSource = ds_Collection;
                dgrex_Dm_Ca_Lamviec.DataMember = ds_Collection.Tables[0].TableName;               

                this.Data = ds_Collection;
                this.GridControl = dgrex_Dm_Ca_Lamviec;

                this.DataBindingControl();
                this.ChangeStatus(false);
                this.gridView1.BestFitColumns();
                this.ChangeFormState( GoobizFrame.Windows.Forms.FormState.View);
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
                // GoobizFrame.Windows.HelperClasses.ExceptionLogger.LogException1(ex);
            }
        }

        public override void  ClearDataBindings()
        {
            this.txtId_Ca_Lamviec.DataBindings.Clear();
            this.txtMa_Ca_Lamviec.DataBindings.Clear();
            this.txtTen_Ca_Lamviec.DataBindings.Clear();
            this.timeEdit_Gio_Batdau.DataBindings.Clear();
            this.timeEdit_Gio_Ketthuc.DataBindings.Clear();
        }

        public override void  DataBindingControl()
        {
            try
            {
                ClearDataBindings();
                this.txtId_Ca_Lamviec.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Id_Ca_Lamviec");
                this.txtMa_Ca_Lamviec.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Ma_Ca_Lamviec");
                this.txtTen_Ca_Lamviec.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Ten_Ca_Lamviec");
                this.timeEdit_Gio_Batdau.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Gio_Batdau");
                this.timeEdit_Gio_Ketthuc.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Gio_Ketthuc");

            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
                // GoobizFrame.Windows.HelperClasses.ExceptionLogger.LogException1(ex);
            }
        }

        public override void  ChangeStatus(bool editTable)
        {
            //this.dgrex_Dm_Ca_Lamviec.Enabled = !editTable;
            this.gridView1.OptionsBehavior.Editable = !editTable;
            this.txtMa_Ca_Lamviec.Properties.ReadOnly       = !editTable;
            this.txtTen_Ca_Lamviec.Properties.ReadOnly      = !editTable;
            this.timeEdit_Gio_Batdau.Properties.ReadOnly    = !editTable;
            this.timeEdit_Gio_Ketthuc.Properties.ReadOnly   = !editTable;   
        }

        public override void  ResetText()
        {
            this.txtId_Ca_Lamviec.EditValue = "";
            this.txtMa_Ca_Lamviec.EditValue = "";
            this.txtTen_Ca_Lamviec.EditValue = "";
            this.timeEdit_Gio_Batdau.EditValue = "";
            this.timeEdit_Gio_Ketthuc.EditValue = "";
        }

        public object InsertObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Ca_Lamviec objRex_Dm_Ca_Lamviec = new Ecm.WebReferences.MasterService.Rex_Dm_Ca_Lamviec();
            objRex_Dm_Ca_Lamviec.Id_Ca_Lamviec     = -1;
            objRex_Dm_Ca_Lamviec.Ma_Ca_Lamviec     = txtMa_Ca_Lamviec.EditValue;
            objRex_Dm_Ca_Lamviec.Ten_Ca_Lamviec    = txtTen_Ca_Lamviec.EditValue;
            objRex_Dm_Ca_Lamviec.Gio_Batdau         = timeEdit_Gio_Batdau.EditValue;
            objRex_Dm_Ca_Lamviec.Gio_Ketthuc = timeEdit_Gio_Ketthuc.EditValue;

            return objMasterService.Insert_Rex_Dm_Ca_Lamviec(objRex_Dm_Ca_Lamviec);
        }

        public object UpdateObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Ca_Lamviec objRex_Dm_Ca_Lamviec = new Ecm.WebReferences.MasterService.Rex_Dm_Ca_Lamviec();
            objRex_Dm_Ca_Lamviec.Id_Ca_Lamviec = gridView1.GetFocusedRowCellValue("Id_Ca_Lamviec");
            objRex_Dm_Ca_Lamviec.Ma_Ca_Lamviec     = txtMa_Ca_Lamviec.EditValue;
            objRex_Dm_Ca_Lamviec.Ten_Ca_Lamviec    = txtTen_Ca_Lamviec.EditValue;
            objRex_Dm_Ca_Lamviec.Gio_Batdau        = timeEdit_Gio_Batdau.Text;
            objRex_Dm_Ca_Lamviec.Gio_Ketthuc       = timeEdit_Gio_Ketthuc.Text;

            return objMasterService.Update_Rex_Dm_Ca_Lamviec(objRex_Dm_Ca_Lamviec);
        }

        public object DeleteObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Ca_Lamviec objRex_Dm_Ca_Lamviec = new Ecm.WebReferences.MasterService.Rex_Dm_Ca_Lamviec();
            objRex_Dm_Ca_Lamviec.Id_Ca_Lamviec = gridView1.GetFocusedRowCellValue("Id_Ca_Lamviec");

            return objMasterService.Delete_Rex_Dm_Ca_Lamviec(objRex_Dm_Ca_Lamviec);
        }

        public override bool PerformAdd()
        {
            ClearDataBindings();
            this.ChangeStatus(true);
            this.ResetText();
            this.timeEdit_Gio_Batdau.EditValue = DateTime.Now;
            this.timeEdit_Gio_Ketthuc.EditValue = DateTime.Now;
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
            return true;
        }

        public override bool PerformSave()
        {
            try
            {
                bool success = false;

                 GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new  GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(txtMa_Ca_Lamviec, lblMa_Ca_Lamviec.Text);
                hashtableControls.Add(txtTen_Ca_Lamviec, lblTen_Ca_Lamviec.Text);

                if (! GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;

                //if (! GoobizFrame.Windows.MdiUtils.Validator.CheckDate(timeEdit_Gio_Batdau, timeEdit_Gio_Ketthuc))
                //    return false;
                if (this.timeEdit_Gio_Batdau.EditValue.Equals((DateTime)this.timeEdit_Gio_Ketthuc.EditValue))
                {
                    MessageBox.Show("Giờ bắt đầu và giờ kết thúc không thể trùng nhau!");
                    this.timeEdit_Gio_Ketthuc.Focus();
                    return false;
                }

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
                if (ex.ToString().IndexOf("exists") != -1)
                {
                     GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblMa_Ca_Lamviec.Text, lblMa_Ca_Lamviec.Text });
                }
                return false;
            }
        }

        public override bool PerformSaveChanges()
        {
             GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new  GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gridView1.Columns["Ma_Ca_Lamviec"], "");
            hashtableControls.Add(gridView1.Columns["Ten_Ca_Lamviec"], "");

            if (! GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gridView1))
                return false;

            for (int i = 0; i < gridView1.RowCount; i++) {
                if (gridView1.GetDataRow(i).RowState != DataRowState.Unchanged) {
                    if (gridView1.GetRowCellValue(i, gridView1.Columns["Gio_Batdau"]).Equals(gridView1.GetRowCellValue(i, gridView1.Columns["Gio_Ketthuc"]))) {
                         GoobizFrame.Windows.Forms.MessageDialog.Show("Thời gian bắt đầu và thời gian kết thúc không được giống nhau, nhập lại");
                        return false;
                    }
                }
            }

            try
            {
                //dgrex_Dm_Ca_Lamviec.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                this.DoClickEndEdit(dgrex_Dm_Ca_Lamviec);
                ds_Collection.Tables[0].Columns["Ma_Ca_Lamviec"].Unique = true;
                objMasterService.Update_Rex_Dm_Ca_Lamviec_Collection(this.ds_Collection);

            }
            catch (Exception ex)
            {
                if (ex.ToString().IndexOf("Unique") != -1)
                {
                     GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblMa_Ca_Lamviec.Text, lblMa_Ca_Lamviec.Text });
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
             GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Rex_Dm_Ca_Lamviec"),
             GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Rex_Dm_Ca_Lamviec")   }) == DialogResult.Yes)
            {
                try
                {
                    if (Convert.ToInt32(objMasterService.GetExistReferences("Rex_Dm_Ca_Lamviec", "Id_Ca_Lamviec", this.gridView1.GetFocusedRowCellValue("Id_Ca_Lamviec"))) > 0)
                    {
                         GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                        return true;
                    }
                    this.DeleteObject();
                }
                catch (Exception ex)
                {
                     GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                    // GoobizFrame.Windows.MdiUtils.Validator.CheckReferencedRecord(ex.Message, "");
                }
                this.DisplayInfo();
            }
            return base.PerformDelete();
        }

        public override object PerformSelectOneObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Ca_Lamviec rex_Dm_Ca_Lamviec = new Ecm.WebReferences.MasterService.Rex_Dm_Ca_Lamviec();
            try
            {
                int focusedRow = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
                DataRow dr = ds_Collection.Tables[0].Rows[focusedRow];
                if (dr != null)
                {
                    rex_Dm_Ca_Lamviec.Id_Ca_Lamviec = dr["Id_Ca_Lamviec"];
                    rex_Dm_Ca_Lamviec.Ma_Ca_Lamviec = dr["Ma_Ca_Lamviec"];
                    rex_Dm_Ca_Lamviec.Ten_Ca_Lamviec = dr["Ten_Ca_Lamviec"];
                }

                //this.dgrex_Dm_Ca_Lamviec.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                this.DoClickEndEdit(dgrex_Dm_Ca_Lamviec);
                SelectedRows = ds_Collection.Tables[0].Select("Chon = true");
                ds_Collection.AcceptChanges();
                this.FormState =  GoobizFrame.Windows.Forms.FormState.View;
                this.Dispose();
                this.Close();
                return rex_Dm_Ca_Lamviec;
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
            this.gridView1.FocusedColumn = gridView1.Columns["Ma_Ca_Lamviec"];
            this.gridView1.SetRowCellValue(e.RowHandle, "Gio_Batdau", DateTime.Now);
            this.gridView1.SetRowCellValue(e.RowHandle, "Gio_Ketthuc", DateTime.Now.AddHours(8));
            this.addnewrow_clicked = true;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Chon")
            {
                gridView1.GetDataRow(gridView1.FocusedRowHandle).AcceptChanges();

                item_Select.Enabled = true;
                item_Close.Enabled = true;
                item_Refresh.Enabled = true;
            }
            //this.dgrex_Dm_Ca_Lamviec.EmbeddedNavigator.Buttons.EndEdit.DoClick();
            this.DoClickEndEdit(dgrex_Dm_Ca_Lamviec);
        }

        private void dgrex_Dm_Ca_Lamviec_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if ("" + this.gridView1.GetFocusedRowCellValue("Id_Ca_Lamviec") != "")
                    if (Convert.ToInt32(objMasterService.GetExistReferences("Rex_Dm_Ca_Lamviec", "Id_Ca_Lamviec", this.gridView1.GetFocusedRowCellValue("Id_Ca_Lamviec"))) > 0)
                    {

                         GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                        e.Handled = true;
                    }
            }
        }

        private void txtMa_Ca_Lamviec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar).Equals(39))
                e.Handled = true;
        }
        #endregion


    }
}


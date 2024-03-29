using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GoobizFrame.Windows.Forms;

namespace Ecm.MasterTables.Forms.Ware
{
    public partial class Frmware_Dm_Tiente_Add : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        DataSet ds_Collection = new DataSet();
        public Frmware_Dm_Tiente_Add()
        {
            InitializeComponent();
        }

        private void Frmware_Dm_Tiente_Add_Load(object sender, EventArgs e)
        {
            this.DisplayInfo();
        }

        public override void DisplayInfo()
        {
            try
            {
                ds_Collection = objMasterService.Get_All_Ware_Dm_Tiente().ToDataSet();
                dgacc_Dm_Tiente.DataSource = ds_Collection;
                dgacc_Dm_Tiente.DataMember = ds_Collection.Tables[0].TableName;

                this.Data = ds_Collection;
                this.GridControl = dgacc_Dm_Tiente;
                this.ChangeStatus(false);
                this.gridView1.BestFitColumns();
                this.DataBindingControl();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
                //// GoobizFrame.Windows.HelperClasses.ExceptionLogger.LogException1(ex);
            }
        }

        public override void ClearDataBindings()
        {
            this.txtId_Tiente.DataBindings.Clear();
            this.txtMa_Tiente.DataBindings.Clear();
            this.txtTen_Tiente.DataBindings.Clear();
            this.txtTygia_Vnd.DataBindings.Clear();
        }

        public override void DataBindingControl()
        {
            try
            {
                ClearDataBindings();
                this.txtId_Tiente.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Id_Tiente");
                this.txtMa_Tiente.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Ma_Tiente");
                this.txtTen_Tiente.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Ten_Tiente");
                this.txtTygia_Vnd.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Tygia_Vnd");
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
                //// GoobizFrame.Windows.HelperClasses.ExceptionLogger.LogException1(ex);
            }
        }

        public override void ChangeStatus(bool editTable)
        {
            //this.dgacc_Dm_Tiente.Enabled = !editTable;
            this.gridView1.OptionsBehavior.Editable = !editTable;
            this.txtMa_Tiente.Properties.ReadOnly = !editTable;
            this.txtTen_Tiente.Properties.ReadOnly = !editTable;
            this.txtTygia_Vnd.Properties.ReadOnly = !editTable;
        }

        public override void ResetText()
        {
            ClearDataBindings();
            this.txtId_Tiente.EditValue = "";
            this.txtMa_Tiente.EditValue = null;
            this.txtTen_Tiente.EditValue = null;
            this.txtTygia_Vnd.EditValue = null;
        }

        #region Event Override
        public object InsertObject()
        {
            Ecm.WebReferences.MasterService.Ware_Dm_Tiente objWare_Dm_Tiente = new Ecm.WebReferences.MasterService.Ware_Dm_Tiente();
            objWare_Dm_Tiente.Id_Tiente = -1;
            objWare_Dm_Tiente.Ma_Tiente = txtMa_Tiente.EditValue;
            objWare_Dm_Tiente.Ten_Tiente = txtTen_Tiente.EditValue;
            if ("" + txtTygia_Vnd.EditValue != "")
                objWare_Dm_Tiente.Tygia_Vnd = txtTygia_Vnd.EditValue;

            return objMasterService.Insert_Ware_Dm_Tiente(objWare_Dm_Tiente);
        }

        public object UpdateObject()
        {
            Ecm.WebReferences.MasterService.Ware_Dm_Tiente objWare_Dm_Tiente = new Ecm.WebReferences.MasterService.Ware_Dm_Tiente();
            objWare_Dm_Tiente.Id_Tiente = gridView1.GetFocusedRowCellValue("Id_Tiente");
            objWare_Dm_Tiente.Ma_Tiente = txtMa_Tiente.EditValue;
            objWare_Dm_Tiente.Ten_Tiente = txtTen_Tiente.EditValue;
            if ("" + txtTygia_Vnd.EditValue != "")
                objWare_Dm_Tiente.Tygia_Vnd = txtTygia_Vnd.EditValue;

            return objMasterService.Update_Ware_Dm_Tiente(objWare_Dm_Tiente);
        }

        public object DeleteObject()
        {
            Ecm.WebReferences.MasterService.Ware_Dm_Tiente objWare_Dm_Tiente = new Ecm.WebReferences.MasterService.Ware_Dm_Tiente();
            objWare_Dm_Tiente.Id_Tiente = gridView1.GetFocusedRowCellValue("Id_Tiente");

            return objMasterService.Delete_Ware_Dm_Tiente(objWare_Dm_Tiente);
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
            return true;
        }

        public override bool PerformSave()
        {
            try
            {
                bool success = false;

                GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(txtMa_Tiente, lblMa_Tiente.Text);
                hashtableControls.Add(txtTen_Tiente, lblTen_Tiente.Text);

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
                if (ex.ToString().IndexOf("exists") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblMa_Tiente.Text, lblMa_Tiente.Text.ToLower() });
                }
                return false;
            }
        }

        public override bool PerformSaveChanges()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gridView1.Columns["Ma_Tiente"], "");
            hashtableControls.Add(gridView1.Columns["Ten_Tiente"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gridView1))
                return false;

            try
            {
                dgacc_Dm_Tiente.EmbeddedNavigator.Buttons.DoClick(dgacc_Dm_Tiente.EmbeddedNavigator.Buttons.EndEdit);
                ds_Collection.Tables[0].Columns["Ma_Tiente"].Unique = true;
                objMasterService.Update_Ware_Dm_Tiente_Collection(this.ds_Collection);
            }
            catch (Exception ex)
            {
                if (ex.ToString().IndexOf("Unique") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblMa_Tiente.Text, lblMa_Tiente.Text.ToLower() });
                    return false;
                }
                //MessageBox.Show(ex.ToString());

            }
            this.DisplayInfo();
            return true;
        }

        public override bool PerformDelete()
        {
            if (GoobizFrame.Windows.Forms.UserMessage.Show("Msg00004", new string[]  {
             GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Ware_Dm_Tiente"),
             GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Ware_Dm_Tiente")   }) == DialogResult.Yes)
            {
                try
                {
                    if (Convert.ToInt32(objMasterService.GetExistReferences("Ware_Dm_Tiente", "Id_Tiente", this.gridView1.GetFocusedRowCellValue("Id_Tiente"))) > 0)
                    {
                        GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                        return true;
                    }
                    this.DeleteObject();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                    // GoobizFrame.Windows.MdiUtils.Validator.CheckReferencedRecord(ex.Message, "");
                }
                this.DisplayInfo();
            }
            return base.PerformDelete();
        }

        public override object PerformSelectOneObject()
        {
            Ecm.WebReferences.MasterService.Ware_Dm_Tiente ware_Dm_Tiente = new Ecm.WebReferences.MasterService.Ware_Dm_Tiente();
            try
            {
                int focusedRow = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
                DataRow dr = ds_Collection.Tables[0].Rows[focusedRow];
                if (dr != null)
                {
                    ware_Dm_Tiente.Id_Tiente = dr["Id_Tiente"];
                    ware_Dm_Tiente.Ma_Tiente = dr["Ma_Tiente"];
                    ware_Dm_Tiente.Ten_Tiente = dr["Ten_Tiente"];
                    ware_Dm_Tiente.Ten_Tiente = dr["Tygia_Vnd"];
                }
                this.Dispose();
                this.Close();
                return ware_Dm_Tiente;
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
            this.gridView1.FocusedColumn = gridView1.Columns["Ma_Tiente"];
            this.addnewrow_clicked = true;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.dgacc_Dm_Tiente.EmbeddedNavigator.Buttons.DoClick(dgacc_Dm_Tiente.EmbeddedNavigator.Buttons.EndEdit);
        }

        private void dgacc_Dm_Tiente_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if (Convert.ToInt32(objMasterService.GetExistReferences("Ware_Dm_Tiente", "Id_Tiente", this.gridView1.GetFocusedRowCellValue("Id_Tiente"))) > 0)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                    e.Handled = true;
                }
            }
        }

        private void gridText_Tygia_Vnd_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString() == "" || e.NewValue.ToString() == "0")
                e.Cancel = true;
            if (e.NewValue != null)
                if (e.NewValue.ToString().Length > 12)
                    e.Cancel = true;
        }

        private void txtTygia_Vnd_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (FormState == GoobizFrame.Windows.Forms.FormState.View)
                return;
            if (e.NewValue != null)
            {
                if (e.NewValue.ToString() == "" || e.NewValue.ToString() == "0")
                    e.Cancel = true;

                if (e.NewValue.ToString().Length > 12)
                    e.Cancel = true;
            }
        }
        #endregion


    }
}


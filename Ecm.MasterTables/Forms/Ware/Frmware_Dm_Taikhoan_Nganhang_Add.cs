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
    public partial class Frmware_Dm_Taikhoan_Nganhang_Add : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        DataSet ds_Collection = new DataSet();

        public Frmware_Dm_Taikhoan_Nganhang_Add()
        {
            InitializeComponent();
        }

        private void Frmware_Dm_Taikhoan_Nganhang_Add_Load(object sender, EventArgs e)
        {
            this.DisplayInfo();
        }

        public override void DisplayInfo()
        {
            try
            {
                //Get data Get_All_Ware_Dm_Nganhang
                DataSet dsWare_Dm_Nganhang = objMasterService.Get_All_Ware_Dm_Nganhang().ToDataSet();
                lookUp_Nganhang.Properties.DataSource = dsWare_Dm_Nganhang.Tables[0];
                gridLookUp_Nganhang.DataSource = dsWare_Dm_Nganhang.Tables[0];

                //Get data Get_All_Ware_Dm_Tiente
                DataSet dsWare_Dm_Tiente = objMasterService.Get_All_Ware_Dm_Tiente().ToDataSet();
                lookUp_Tiente.Properties.DataSource = dsWare_Dm_Tiente.Tables[0];
                gridLookUp_Tiente.DataSource = dsWare_Dm_Tiente.Tables[0];


                ds_Collection = objMasterService.Get_All_Ware_Dm_Taikhoan_Nganhang().ToDataSet();
                dgware_Dm_Taikhoan_Nganhang.DataSource = ds_Collection;
                dgware_Dm_Taikhoan_Nganhang.DataMember = ds_Collection.Tables[0].TableName;

                this.Data = ds_Collection;
                this.GridControl = dgware_Dm_Taikhoan_Nganhang;

                this.DataBindingControl();
                this.ChangeStatus(false);
                this.gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }

        }

        public override void ClearDataBindings()
        {
            this.txtId_Taikhoan_Nganhang.DataBindings.Clear();
            this.txtMa_Taikhoan_Nganhang.DataBindings.Clear();
            this.txtTen_Taikhoan_Nganhang.DataBindings.Clear();
            this.txtSo_Taikhoan_Nganhang.DataBindings.Clear();
            this.lookUp_Nganhang.DataBindings.Clear();
            this.lookUp_Tiente.DataBindings.Clear();
        }

        public override void DataBindingControl()
        {
            try
            {
                ClearDataBindings();
                this.txtId_Taikhoan_Nganhang.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Id_Taikhoan_Nganhang");
                this.txtMa_Taikhoan_Nganhang.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Ma_Taikhoan_Nganhang");
                this.txtTen_Taikhoan_Nganhang.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Ten_Taikhoan_Nganhang");
                this.txtSo_Taikhoan_Nganhang.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".So_Taikhoan_Nganhang");
                this.lookUp_Nganhang.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Id_Nganhang");
                this.lookUp_Tiente.DataBindings.Add("EditValue", ds_Collection, ds_Collection.Tables[0].TableName + ".Id_Tiente");
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
            //this.dgware_Dm_Taikhoan_Nganhang.Enabled = !editTable;
            this.txtMa_Taikhoan_Nganhang.Properties.ReadOnly = !editTable;
            this.txtTen_Taikhoan_Nganhang.Properties.ReadOnly = !editTable;
            this.txtSo_Taikhoan_Nganhang.Properties.ReadOnly = !editTable;
            this.lookUp_Nganhang.Properties.ReadOnly = !editTable;
            this.lookUp_Tiente.Properties.ReadOnly = !editTable;
        }

        public override void ResetText()
        {
            this.txtId_Taikhoan_Nganhang.EditValue = "";
            this.txtMa_Taikhoan_Nganhang.EditValue = "";
            this.txtTen_Taikhoan_Nganhang.EditValue = "";
            this.txtSo_Taikhoan_Nganhang.EditValue = "";
            this.lookUp_Nganhang.EditValue = null;
            this.lookUp_Tiente.EditValue = null;
        }

        #region Event Override

        public object InsertObject()
        {
            Ecm.WebReferences.MasterService.Ware_Dm_Taikhoan_Nganhang objWare_Dm_Taikhoan_Nganhang = new Ecm.WebReferences.MasterService.Ware_Dm_Taikhoan_Nganhang();
            objWare_Dm_Taikhoan_Nganhang.Id_Taikhoan_Nganhang = -1;
            objWare_Dm_Taikhoan_Nganhang.Ma_Taikhoan_Nganhang = txtMa_Taikhoan_Nganhang.EditValue;
            objWare_Dm_Taikhoan_Nganhang.Ten_Taikhoan_Nganhang = txtTen_Taikhoan_Nganhang.EditValue;
            objWare_Dm_Taikhoan_Nganhang.So_Taikhoan_Nganhang = txtSo_Taikhoan_Nganhang.EditValue;

            if ("" + lookUp_Nganhang.EditValue != "")
                objWare_Dm_Taikhoan_Nganhang.Id_Nganhang = lookUp_Nganhang.EditValue;
            if ("" + lookUp_Tiente.EditValue != "")
                objWare_Dm_Taikhoan_Nganhang.Id_Tiente = lookUp_Tiente.EditValue;
            return objMasterService.Insert_Ware_Dm_Taikhoan_Nganhang(objWare_Dm_Taikhoan_Nganhang);
        }

        public object UpdateObject()
        {
            Ecm.WebReferences.MasterService.Ware_Dm_Taikhoan_Nganhang objWare_Dm_Taikhoan_Nganhang = new Ecm.WebReferences.MasterService.Ware_Dm_Taikhoan_Nganhang();
            objWare_Dm_Taikhoan_Nganhang.Id_Taikhoan_Nganhang = gridView1.GetFocusedRowCellValue("Id_Taikhoan_Nganhang");
            objWare_Dm_Taikhoan_Nganhang.Ma_Taikhoan_Nganhang = txtMa_Taikhoan_Nganhang.EditValue;
            objWare_Dm_Taikhoan_Nganhang.Ten_Taikhoan_Nganhang = txtTen_Taikhoan_Nganhang.EditValue;
            objWare_Dm_Taikhoan_Nganhang.So_Taikhoan_Nganhang = txtSo_Taikhoan_Nganhang.EditValue;

            if ("" + lookUp_Nganhang.EditValue != "")
                objWare_Dm_Taikhoan_Nganhang.Id_Nganhang = lookUp_Nganhang.EditValue;
            if ("" + lookUp_Tiente.EditValue != "")
                objWare_Dm_Taikhoan_Nganhang.Id_Tiente = lookUp_Tiente.EditValue;
            return objMasterService.Update_Ware_Dm_Taikhoan_Nganhang(objWare_Dm_Taikhoan_Nganhang);
        }

        public object DeleteObject()
        {
            Ecm.WebReferences.MasterService.Ware_Dm_Taikhoan_Nganhang objWare_Dm_Taikhoan_Nganhang = new Ecm.WebReferences.MasterService.Ware_Dm_Taikhoan_Nganhang();
            objWare_Dm_Taikhoan_Nganhang.Id_Taikhoan_Nganhang = gridView1.GetFocusedRowCellValue("Id_Taikhoan_Nganhang");
            return objMasterService.Delete_Ware_Dm_Taikhoan_Nganhang(objWare_Dm_Taikhoan_Nganhang);
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
                GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(txtMa_Taikhoan_Nganhang, lblMa_Taikhoan_Nganhang.Text);
                hashtableControls.Add(txtTen_Taikhoan_Nganhang, lblTen_Taikhoan_Nganhang.Text);
                hashtableControls.Add(txtSo_Taikhoan_Nganhang, lblSo_Taikhoan_Nganhang.Text);

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
                if (ex.ToString().IndexOf("exists") != -1 && ex.ToString().IndexOf("exists_Ma_Taikhoan_Nganhang") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblMa_Taikhoan_Nganhang.Text, lblMa_Taikhoan_Nganhang.Text.ToLower() });
                }
                else if (ex.ToString().IndexOf("exists") != -1 && ex.ToString().IndexOf("exists_So_Taikhoan_Nganhang") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblSo_Taikhoan_Nganhang.Text, lblSo_Taikhoan_Nganhang.Text.ToLower() });
                }
                return false;
            }
        }

        public override bool PerformSaveChanges()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gridView1.Columns["Ma_Taikhoan_Nganhang"], "");
            hashtableControls.Add(gridView1.Columns["Ten_Taikhoan_Nganhang"], "");
            hashtableControls.Add(gridView1.Columns["So_Taikhoan_Nganhang"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gridView1))
                return false;

            try
            {
                dgware_Dm_Taikhoan_Nganhang.EmbeddedNavigator.Buttons.DoClick(dgware_Dm_Taikhoan_Nganhang.EmbeddedNavigator.Buttons.EndEdit);
                ds_Collection.Tables[0].Columns["Ma_Taikhoan_Nganhang"].Unique = true;
                ds_Collection.Tables[0].Columns["So_Taikhoan_Nganhang"].Unique = true;
                objMasterService.Update_Ware_Dm_Taikhoan_Nganhang_Collection(this.ds_Collection);
            }
            catch (Exception ex)
            {
                if (ex.ToString().IndexOf("Unique") != -1 && ex.ToString().IndexOf("Ma_Taikhoan_Nganhang") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblMa_Taikhoan_Nganhang.Text, lblMa_Taikhoan_Nganhang.Text.ToLower() });
                    return false;
                }
                else if (ex.ToString().IndexOf("Unique") != -1 && ex.ToString().IndexOf("So_Taikhoan_Nganhang") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblSo_Taikhoan_Nganhang.Text, lblSo_Taikhoan_Nganhang.Text.ToLower() });
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
             GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Ware_Dm_Taikhoan_Nganhang"),
             GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Ware_Dm_Taikhoan_Nganhang")   }) == DialogResult.Yes)
            {
                try
                {
                    if (Convert.ToInt32(objMasterService.GetExistReferences("Ware_Dm_Taikhoan_Nganhang", "Id_Taikhoan_Nganhang", this.gridView1.GetFocusedRowCellValue("Id_Taikhoan_Nganhang"))) > 0)
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
            Ecm.WebReferences.MasterService.Ware_Dm_Taikhoan_Nganhang ware_Dm_Taikhoan_Nganhang = new Ecm.WebReferences.MasterService.Ware_Dm_Taikhoan_Nganhang();
            try
            {
                int focusedRow = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
                DataRow dr = ds_Collection.Tables[0].Rows[focusedRow];
                if (dr != null)
                {
                    ware_Dm_Taikhoan_Nganhang.Id_Taikhoan_Nganhang = dr["Id_Taikhoan_Nganhang"];
                    ware_Dm_Taikhoan_Nganhang.Ma_Taikhoan_Nganhang = dr["Ma_Taikhoan_Nganhang"];
                    ware_Dm_Taikhoan_Nganhang.Ten_Taikhoan_Nganhang = dr["Ten_Taikhoan_Nganhang"];
                    ware_Dm_Taikhoan_Nganhang.So_Taikhoan_Nganhang = dr["So_Taikhoan_Nganhang"];
                    ware_Dm_Taikhoan_Nganhang.Id_Nganhang = dr["Id_Nganhang"];
                    ware_Dm_Taikhoan_Nganhang.Id_Tiente = dr["Id_Tiente"];
                }
                this.Dispose();
                this.Close();
                return ware_Dm_Taikhoan_Nganhang;
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

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            this.gridView1.FocusedColumn = gridView1.Columns["Ma_Taikhoan_Nganhang"];
            this.addnewrow_clicked = true;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.dgware_Dm_Taikhoan_Nganhang.EmbeddedNavigator.Buttons.DoClick(dgware_Dm_Taikhoan_Nganhang.EmbeddedNavigator.Buttons.EndEdit);
        }

        private void dgware_Dm_Taikhoan_Nganhang_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if (Convert.ToInt32(objMasterService.GetExistReferences("Ware_Dm_Taikhoan_Nganhang", "Id_Taikhoan_Nganhang", this.gridView1.GetFocusedRowCellValue("Id_Taikhoan_Nganhang"))) > 0)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                    e.Handled = true;
                }
            }
        }



    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SunLine.SystemControl.Policy.Forms
{
    public partial class Frmpol_Dm_Role_Add : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        SunLine.WebReferences.Classes.PolicyService objPolicy = new SunLine.WebReferences.Classes.PolicyService();
        SunLine.WebReferences.PolicyService.Pol_Dm_Role Pol_Dm_Role = new SunLine.WebReferences.PolicyService.Pol_Dm_Role();
        DataSet dsRole = new DataSet();

        Frmpol_Dm_Role_Properties _Frmpol_Dm_Role_Properties;

        public Frmpol_Dm_Role_Add()
        {
            InitializeComponent();
            //update GUI with current CultureInfo
            //System.Collections.ArrayList controls = new System.Collections.ArrayList();
            //controls.Add(this.lblId_Role);
            //controls.Add(this.lblRole_System_Name);
            //controls.Add(this.lblRole_Description);
            //controls.Add(this.gridColumn1);
            //controls.Add(this.gridColumn2);
            //controls.Add(this.gridColumn3);
            //controls.Add(this.btbRole_Properties);
            //GoobizFrame.Windows.CultureInfo.CultureInfoHelper.SetupFormCultureInfo(this, controls);
            //GoobizFrame.Windows.CultureInfo.CultureInfoHelper.SetupEmbeddedNavigatorCultureInfo(this, dgpol_Dm_Role);
        }

        private void Frmpol_Dm_Role_Add_Load(object sender, EventArgs e)
        {
            this.DisplayInfo();
            this.ChangeStatus(false);
        }

        public void DisplayInfo()
        {
            try
            {
                dsRole = objPolicy.Get_Pol_Dm_Role_Collection3();

                dgpol_Dm_Role.DataSource = dsRole;
                dgpol_Dm_Role.DataMember = dsRole.Tables[0].TableName;
                
                this.Data = dsRole;
                this.GridControl = dgpol_Dm_Role;
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);

                //////SunLine.HelperClasses.ExceptionLogger.LogException1(ex);
            }

            try
            {
                txtId_Role.DataBindings.Clear();
                txtRole_System_Name.DataBindings.Clear();
                txtRole_Description.DataBindings.Clear();
                if (dsRole.Tables[0].Rows.Count > 0)
                {
                    txtId_Role.DataBindings.Add("Text", dsRole, dsRole.Tables[0].TableName + ".Id_Role");
                    txtRole_System_Name.DataBindings.Add("Text", dsRole, dsRole.Tables[0].TableName + ".Role_System_Name");
                    txtRole_Description.DataBindings.Add("Text", dsRole, dsRole.Tables[0].TableName + ".Role_Description");
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
            }
        }

        public void ChangeStatus(bool editable)
        {
            this.dgpol_Dm_Role.Enabled = !editable;
            this.txtRole_System_Name.Properties.ReadOnly = !editable;
            this.txtRole_Description.Properties.ReadOnly = !editable;
        }

        #region event override
        public object InsertObject()
        {
            SunLine.WebReferences.PolicyService.Pol_Dm_Role Pol_Dm_Role = new SunLine.WebReferences.PolicyService.Pol_Dm_Role();
            Pol_Dm_Role.Role_System_Name = this.txtRole_System_Name.Text;
            Pol_Dm_Role.Role_Description = this.txtRole_Description.Text;
            return objPolicy.Pol_Dm_Role_Insert(Pol_Dm_Role);
        }
        public object UpdateObject()
        {
            SunLine.WebReferences.PolicyService.Pol_Dm_Role Pol_Dm_Role = new SunLine.WebReferences.PolicyService.Pol_Dm_Role();
            Pol_Dm_Role.Id_Role = Convert.ToInt64(this.txtId_Role.Text);
            Pol_Dm_Role.Role_System_Name = this.txtRole_System_Name.Text;
            Pol_Dm_Role.Role_Description = this.txtRole_Description.Text;
            return objPolicy.Pol_Dm_Role_Update(Pol_Dm_Role);
        }
        public object DeleteObject()
        {
            SunLine.WebReferences.PolicyService.Pol_Dm_Role Pol_Dm_Role = new SunLine.WebReferences.PolicyService.Pol_Dm_Role();
            Pol_Dm_Role.Id_Role = Convert.ToInt64(this.txtId_Role.Text);
            return objPolicy.Pol_Dm_Role_Delete(Pol_Dm_Role);
        }
        public override bool PerformAdd()
        {
            this.ChangeStatus(true);
            this.txtId_Role.Text = "";
            this.txtRole_System_Name.Text = "";
            this.txtRole_Description.Text = "";
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
            bool saved = false;
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(txtRole_System_Name, lblRole_System_Name.Text);

            System.Collections.Hashtable htb_Role_System_Name = new System.Collections.Hashtable();
            htb_Role_System_Name.Add(txtRole_System_Name, lblRole_System_Name.Text);

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                return false;
            if (this.FormState == GoobizFrame.Windows.Forms.FormState.Add)
            {
                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckExistValues(htb_Role_System_Name, (DataSet)dgpol_Dm_Role.DataSource, "Role_System_Name"))
                    return false;
                this.InsertObject();
                saved = true;
            }
            else if (this.FormState == GoobizFrame.Windows.Forms.FormState.Edit)
            {
                DataSet _ds = GoobizFrame.Windows.MdiUtils.Validator.datasetFillter((DataSet)dgpol_Dm_Role.DataSource, "Id_Role = " + txtId_Role.Text);
                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckExistValues(htb_Role_System_Name, _ds, "Role_System_Name"))
                    return false;
                this.UpdateObject();
                saved = true;
            }
            if (saved)
            {
                this.DisplayInfo();
                this.ChangeStatus(false);
            }
            return saved;
        }
        public override bool PerformSaveChanges()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gridView1.Columns["Role_System_Name"], "");

            System.Collections.Hashtable htb_Role_System_Name = new System.Collections.Hashtable();
            htb_Role_System_Name.Add(gridView1.Columns["Role_System_Name"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gridView1))
                return false;
            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckExistGrid(htb_Role_System_Name, gridView1))
                return false;

            try
            {
                dgpol_Dm_Role.EmbeddedNavigator.Buttons.DoClick(dgpol_Dm_Role.EmbeddedNavigator.Buttons.EndEdit);
                objPolicy.Update_Pol_Dm_Role_Collection(dsRole);
            }
            catch (Exception ex)
            {
                //Error here
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
            }
            this.DisplayInfo();
            return true;
        }
        public override bool PerformDelete()
        {
            if (GoobizFrame.Windows.Forms.UserMessage.Show("Msg00004", new string[]  {
            GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Pol_Dm_Role"),
            GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Pol_Dm_Role")   }) == DialogResult.Yes)
            {
                try
                {
                    this.DeleteObject();
                }
                catch (Exception ex)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                }
                this.DisplayInfo();
            }
            return base.PerformDelete();
        }

        public override object PerformSelectOneObject()
        {
            try
            {
                int focusedRow = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
                DataRow dr = dsRole.Tables[0].Rows[focusedRow];
                if (dr != null)
                {
                    Pol_Dm_Role.Id_Role = dr["Id_Role"];
                    Pol_Dm_Role.Role_System_Name = dr["Role_System_Name"];
                    Pol_Dm_Role.Role_Description = dr["Role_Description"];
                }
                this.Dispose();
                this.Close();
                return Pol_Dm_Role;
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                return null;
            }
        }
        #endregion

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.dgpol_Dm_Role.EmbeddedNavigator.Buttons.DoClick(dgpol_Dm_Role.EmbeddedNavigator.Buttons.EndEdit);
        }

        private void dgpol_Dm_Role_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Append)
            {
                this.gridView1.FocusedColumn = gridView1.Columns["Role_System_Name"];
                this.addnewrow_clicked = true;
            }
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "btbRole_Properties":
                    if (_Frmpol_Dm_Role_Properties == null || _Frmpol_Dm_Role_Properties.IsDisposed == true)
                        _Frmpol_Dm_Role_Properties = new Frmpol_Dm_Role_Properties();
                    _Frmpol_Dm_Role_Properties.Text = e.Item.Caption;
                    _Frmpol_Dm_Role_Properties.Id_Role = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_Role"));
                    _Frmpol_Dm_Role_Properties.StartPosition = FormStartPosition.CenterScreen;
                    _Frmpol_Dm_Role_Properties.ShowDialog();
                    this.DisplayInfo();
                    break;
            }
        }

        private void dgpol_Dm_Role_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (item_Edit.Enabled == true)
                    this.popupMenu1.ShowPopup(MousePosition);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;

namespace Ecm.MasterTables.Forms.Rex
{
    public partial class Frmrex_Dm_Bacluong_Add :  GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        public Ecm.WebReferences.MasterService.Rex_Dm_Bacluong Rex_Dm_Bacluong = new Ecm.WebReferences.MasterService.Rex_Dm_Bacluong();

        public Ecm.WebReferences.MasterService.Rex_Dm_Ngachluong objRex_Dm_Ngachluong;
        DataSet dsBacluong = new DataSet();
        public Frmrex_Dm_Bacluong_Add()
        {
             InitializeComponent();           
            //reset lookup edit as delete value
            lookUp_Ngachluong.Properties.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler( GoobizFrame.Windows.MdiUtils.Validator.LookUpEdit_Properties_ProcessNewValue);
        }

        private void Frmrex_Dm_Bacluong_Add_Load(object sender, EventArgs e)
        {
            this.DisplayInfo();          
            lookUp_Ngachluong.ToolTip =  GoobizFrame.Windows.Forms.UserMessage.GetTooltips("rex_dm_ngachluong");

        }
        public override void DisplayInfo()
        {            
            try
            {
                // fill grid
                dsBacluong = objMasterService.Get_All_Rex_Dm_Bacluong_Collection().ToDataSet();
                dgrex_Dm_Bacluong.DataSource = dsBacluong;
                dgrex_Dm_Bacluong.DataMember = dsBacluong.Tables[0].TableName;                

                // fill lookupedit
                DataSet dsDm_Ngachluong = objMasterService.Get_All_Rex_Dm_Ngachluong_Collection().ToDataSet();
                lookUp_Ngachluong.Properties.DataSource = dsDm_Ngachluong.Tables[0];
                gridLookUp_Ngachluong.DataSource        = dsDm_Ngachluong.Tables[0];

                //update trên grid
                this.Data = dsBacluong;
                this.GridControl = dgrex_Dm_Bacluong;                
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.ToString());
#endif
            }
            try
            {
                txtId_Bacluong.DataBindings.Clear();
                txtMa_Bacluong.DataBindings.Clear();
                txtTen_Bacluong.DataBindings.Clear();
                txtHeso.DataBindings.Clear();
                txtLuong_Thoathuan.DataBindings.Clear();
                lookUp_Ngachluong.DataBindings.Clear();
                if (dsBacluong.Tables[0].Rows.Count > 0)
                {
                    txtId_Bacluong.DataBindings.Add("Text", dsBacluong, dsBacluong.Tables[0].TableName + ".Id_Bacluong");
                    txtMa_Bacluong.DataBindings.Add("Text", dsBacluong, dsBacluong.Tables[0].TableName + ".Ma_Bacluong");
                    txtTen_Bacluong.DataBindings.Add("Text", dsBacluong, dsBacluong.Tables[0].TableName + ".Ten_Bacluong");
                    txtHeso.DataBindings.Add("EditValue", dsBacluong, dsBacluong.Tables[0].TableName + ".Heso");
                    txtLuong_Thoathuan.DataBindings.Add("EditValue", dsBacluong, dsBacluong.Tables[0].TableName + ".Luong_Thoathuan");
                    lookUp_Ngachluong.DataBindings.Add("EditValue",dsBacluong,dsBacluong.Tables[0].TableName + ".Id_Ngachluong");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.ToString());
#endif
            }
            this.ChangeStatus(true);
        }

        #region event override
        public void ChangeStatus(bool editable)
        {
            //this.dgrex_Dm_Bacluong.Enabled = editable;
            this.gridView1.OptionsBehavior.Editable = editable;
            txtMa_Bacluong.Properties.ReadOnly = editable;
            txtTen_Bacluong.Properties.ReadOnly = editable;
            txtLuong_Thoathuan.Properties.ReadOnly = editable;
            txtHeso.Properties.ReadOnly = editable;
            lookUp_Ngachluong.Properties.ReadOnly = editable;
        }
        public object InsertObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Bacluong rex_Dm_Bacluong = new Ecm.WebReferences.MasterService.Rex_Dm_Bacluong();
            rex_Dm_Bacluong.Id_Bacluong     = -1;
            rex_Dm_Bacluong.Ma_Bacluong     = this.txtMa_Bacluong.Text;
            rex_Dm_Bacluong.Ten_Bacluong    = this.txtTen_Bacluong.Text;
            rex_Dm_Bacluong.Heso            = Convert.ToDecimal(""+ this.txtHeso.Text);
            rex_Dm_Bacluong.Luong_Thoathuan = Convert.ToDecimal("0"+ this.txtLuong_Thoathuan.Text);
            rex_Dm_Bacluong.Id_Ngachluong   = this.lookUp_Ngachluong.EditValue; 
            return objMasterService.Insert_Rex_Dm_Bacluong(rex_Dm_Bacluong);
        }
        public object UpdateObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Bacluong rex_Dm_Bacluong = new Ecm.WebReferences.MasterService.Rex_Dm_Bacluong();

            rex_Dm_Bacluong.Id_Bacluong = gridView1.GetFocusedRowCellValue("Id_Bacluong");
            rex_Dm_Bacluong.Ma_Bacluong     = this.txtMa_Bacluong.Text;
            rex_Dm_Bacluong.Ten_Bacluong    = this.txtTen_Bacluong.Text;
            rex_Dm_Bacluong.Heso            = Convert.ToDecimal(this.txtHeso.Text);
            rex_Dm_Bacluong.Luong_Thoathuan = Convert.ToDecimal(this.txtLuong_Thoathuan.Text);
            rex_Dm_Bacluong.Id_Ngachluong = this.lookUp_Ngachluong.EditValue;
            return objMasterService.Update_Rex_Dm_Bacluong(rex_Dm_Bacluong);
        }
        public object DeleteObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Bacluong rex_Dm_Bacluong = new Ecm.WebReferences.MasterService.Rex_Dm_Bacluong();
            rex_Dm_Bacluong.Id_Bacluong = gridView1.GetFocusedRowCellValue("Id_Bacluong");
            return objMasterService.Delete_Rex_Dm_Bacluong(rex_Dm_Bacluong);
        }
        public override bool PerformAdd()
        {
            txtId_Bacluong.DataBindings.Clear();
            txtMa_Bacluong.DataBindings.Clear();
            txtTen_Bacluong.DataBindings.Clear();
            txtHeso.DataBindings.Clear();
            txtLuong_Thoathuan.DataBindings.Clear();
            lookUp_Ngachluong.DataBindings.Clear();

            this.ChangeStatus(false);
            this.lookUp_Ngachluong.EditValue = null;
            this.txtId_Bacluong.Text = "";
            this.txtMa_Bacluong.Text = "";
            this.txtTen_Bacluong.Text = "";
            this.txtHeso.Text = "";
            this.txtLuong_Thoathuan.Text = "";
            return true;
        }
        public override bool PerformEdit()
        {
            lookUp_Ngachluong.DataBindings.Clear();
            this.ChangeStatus(false);
            return true;
        }
        public override bool PerformCancel()
        {
            lookUp_Ngachluong.DataBindings.Clear();
            this.DisplayInfo();
            this.ChangeStatus(true);
            return true;
        }
        public override bool PerformSave()
        {
            bool saved = false;
            try
            {
                 GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new  GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(txtMa_Bacluong,           lblMa_Bacluong.Text);
                hashtableControls.Add(txtTen_Bacluong,          lblTen_Bacluong.Text);
                hashtableControls.Add(lookUp_Ngachluong,    lblId_Ngachluong.Text);
                hashtableControls.Add(txtHeso,                  lblHeso.Text);
                hashtableControls.Add(txtLuong_Thoathuan,       lblLuong_Thoathuan.Text);

                System.Collections.Hashtable htb_Ma_Bacluong = new System.Collections.Hashtable();
                htb_Ma_Bacluong.Add(txtMa_Bacluong, lblMa_Bacluong.Text);

                if (! GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;

                if (this.FormState ==  GoobizFrame.Windows.Forms.FormState.Add)
                {
                    if (! GoobizFrame.Windows.MdiUtils.Validator.CheckExistValues(htb_Ma_Bacluong, (DataSet)dgrex_Dm_Bacluong.DataSource, "Ma_Bacluong"))
                        return false;
                    this.InsertObject();
                    saved = true;
                }
                else if (this.FormState ==  GoobizFrame.Windows.Forms.FormState.Edit)
                {
                    txtId_Bacluong.Visible = true;
                    DataSet _ds =  GoobizFrame.Windows.MdiUtils.Validator.datasetFillter((DataSet)dgrex_Dm_Bacluong.DataSource, "Id_Bacluong = " + txtId_Bacluong.Text);
                    if (! GoobizFrame.Windows.MdiUtils.Validator.CheckExistValues(htb_Ma_Bacluong, _ds, "Ma_Bacluong"))
                        return false;
                    txtId_Bacluong.Visible =false;
                    this.UpdateObject();
                    saved = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("TEN_ALREADY_EXIST") != -1)
                     GoobizFrame.Windows.Forms.UserMessage.Show("Msg00014", new string[] { lblTen_Bacluong.Text, lblTen_Bacluong.Text });
            }
            if (saved)
            {
                this.DisplayInfo();
                this.ChangeStatus(true);
            }
            return saved;
        }
        public override bool PerformSaveChanges()
        {
            //dgrex_Dm_Bacluong.EmbeddedNavigator.Buttons.EndEdit.DoClick();
            this.DoClickEndEdit(dgrex_Dm_Bacluong);
            System.Collections.Hashtable hashtableControl = new System.Collections.Hashtable();
            hashtableControl.Add(gridView1.Columns["Id_Ngachluong"], "");
            hashtableControl.Add(gridView1.Columns["Ma_Bacluong"], "");
            hashtableControl.Add(gridView1.Columns["Ten_Bacluong"], "");
            hashtableControl.Add(gridView1.Columns["Heso"], "");
            hashtableControl.Add(gridView1.Columns["Luong_Thoathuan"], "");
            
            System.Collections.Hashtable htb_Ma_Bacluong = new System.Collections.Hashtable();
            htb_Ma_Bacluong.Add(gridView1.Columns["Ma_Bacluong"], "");

            if (! GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControl, gridView1))
                return false;
            if (! GoobizFrame.Windows.MdiUtils.Validator.CheckExistGrid(htb_Ma_Bacluong, gridView1))
                return false;

            try
            {                
                objMasterService.Update_Rex_Dm_Bacluong_Collection(dsBacluong);
            }
            catch(Exception ex)
            {
                if (ex.Message.IndexOf("TEN_ALREADY_EXIST") != -1)
                     GoobizFrame.Windows.Forms.UserMessage.Show("Msg00014", new string[] { lblTen_Bacluong.Text, lblTen_Bacluong.Text });
                else
                     GoobizFrame.Windows.MdiUtils.Validator.CheckReferencedRecord(ex.Message, lblMa_Bacluong.Text);
#if DEBUG
                MessageBox.Show(ex.ToString());
#endif
            }
            this.DisplayInfo();
            return true;
        }
        public override bool PerformDelete()
        {
            if ( GoobizFrame.Windows.Forms.UserMessage.Show("Msg00004", new string[]  {
             GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Rex_Dm_Bacluong"),
             GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Rex_Dm_Bacluong")   }) == DialogResult.Yes)
            {
                try
                {
                    if (Convert.ToInt32(objMasterService.GetExistReferences("Rex_Dm_Bacluong", "Id_Bacluong", this.gridView1.GetFocusedRowCellValue("Id_Bacluong"))) > 0)
                    {
                         GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                        return true;
                    }
                    this.DeleteObject();
                }
                catch (Exception ex)
                {
                     GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                    // GoobizFrame.Windows.MdiUtils.Validator.CheckReferencedRecord(ex.Message, lblMa_Bacluong.Text);
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
                DataRow dr = dsBacluong.Tables[0].Rows[focusedRow];
                if (dr != null)
                {
                    Rex_Dm_Bacluong = new Ecm.WebReferences.MasterService.Rex_Dm_Bacluong();
                    Rex_Dm_Bacluong.Id_Bacluong     = dr["Id_Bacluong"];
                    Rex_Dm_Bacluong.Ma_Bacluong     = dr["Ma_Bacluong"];
                    Rex_Dm_Bacluong.Ten_Bacluong    = dr["Ten_Bacluong"];
                    Rex_Dm_Bacluong.Id_Ngachluong   = dr["Id_Ngachluong"];
                }
                this.Dispose();
                return Rex_Dm_Bacluong;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.ToString());
#endif
                return null;
            }
        }
        #endregion
    

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //dgrex_Dm_Bacluong.EmbeddedNavigator.Buttons.EndEdit.DoClick();
            this.DoClickEndEdit(dgrex_Dm_Bacluong);
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.FocusedColumn = gridView1.Columns[1];
             this.addnewrow_clicked = true;
        }
       
        #region Tạo popup

        private void LookUpEdit_Ngachluong_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.barButton_Ngachluong.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.popupMenu1.ShowPopup(MousePosition);
            }
        }

        private void barButton_Ngachluong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frmrex_Dm_Ngachluong_Add objFrmrex_Dm_Ngachluong_Add = new Frmrex_Dm_Ngachluong_Add();
            objFrmrex_Dm_Ngachluong_Add.AllowSelect = true;
            objFrmrex_Dm_Ngachluong_Add.MaximizeBox = false;
            objFrmrex_Dm_Ngachluong_Add.MinimizeBox = false;
            objFrmrex_Dm_Ngachluong_Add.Size = new Size(800, 600);
            objFrmrex_Dm_Ngachluong_Add.StartPosition = FormStartPosition.CenterScreen;
            objFrmrex_Dm_Ngachluong_Add.ShowDialog();
           
            if (objFrmrex_Dm_Ngachluong_Add.Rex_Dm_Ngachluong.Id_Ngachluong + "" != "")
            {
                DataSet dsNgachluong = objMasterService.Get_All_Rex_Dm_Ngachluong_Collection().ToDataSet();
                lookUp_Ngachluong.Properties.DataSource = dsNgachluong.Tables[0];
                for (int i = 0; i < dsNgachluong.Tables[0].Rows.Count; i++)
                {
                    if (dsNgachluong.Tables[0].Rows[i]["Id_Ngachluong"] + "" == objFrmrex_Dm_Ngachluong_Add.Rex_Dm_Ngachluong.Id_Ngachluong + "")
                    {
                        lookUp_Ngachluong.ItemIndex = i;
                        break;
                    }
                }
            }
        }
        #endregion 

        private void gridLookUpEdit_Ngachluong_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                Frmrex_Dm_Ngachluong_Add frmrex_Dm_Ngachluong_Add = new Frmrex_Dm_Ngachluong_Add();
                frmrex_Dm_Ngachluong_Add.AllowSelect = true;
                 GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmrex_Dm_Ngachluong_Add);
                 GoobizFrame.Windows.PlugIn.RightHelpers.CheckUserRightAction(frmrex_Dm_Ngachluong_Add);
                //Ecm.SystemControl.Policy.Auth.Authorization.SetPopupFormAction( GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUser(), frmrex_Dm_Ngachluong_Add);
                frmrex_Dm_Ngachluong_Add.ShowDialog();

                DataSet dsDm_Ngachluong = frmrex_Dm_Ngachluong_Add.Data;
                lookUp_Ngachluong.Properties.DataSource = dsDm_Ngachluong.Tables[0];
                gridLookUp_Ngachluong.DataSource = dsDm_Ngachluong.Tables[0];

                if ("" + frmrex_Dm_Ngachluong_Add.Rex_Dm_Ngachluong.Id_Ngachluong != "")
                    gridView1.SetFocusedRowCellValue(gridView1.Columns["Id_Ngachluong"], frmrex_Dm_Ngachluong_Add.Rex_Dm_Ngachluong.Id_Ngachluong);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtId_Bacluong.EditValue = gridView1.GetFocusedRowCellValue(gridView1.Columns["Id_Bacluong"]);
        }

        private void dgrex_Dm_Bacluong_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if (Convert.ToInt32(objMasterService.GetExistReferences("Rex_Dm_Bacluong", "Id_Bacluong", this.gridView1.GetFocusedRowCellValue("Id_Bacluong"))) > 0)
                {
                     GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                    e.Handled = true;
                }
            }
        }

        private void txtMa_Bacluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar).Equals(39))
                e.Handled = true;
        }

        private void txtHeso_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString().Length > 10)
                e.Cancel = true;

            if ("" + e.NewValue == "")
            {
                //txtHeso.EditValue = null;
                e.Cancel = true;
            }
        }

        private void txtLuong_Thoathuan_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString().Length > 10)
                e.Cancel = true;
        }

        private void gridText_Heso_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString().Length > 10)
                e.Cancel = true;

            if ("" + e.NewValue == "")
            {
                gridView1.SetFocusedRowCellValue(gridView1.Columns["Heso"], null);
                e.Cancel = true;
            }
        }

        private void gridText_Luong_Thoathuan_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString().Length > 10)
                e.Cancel = true;
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;

namespace Ecm.Rex.Forms
{
    public partial class Frmrex_Kyluong : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.RexService objRexService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.RexService>();
        Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        DataSet ds_Kyluong = new DataSet();
        public Ecm.WebReferences.RexService.Rex_Kyluong Selected_Rex_Kyluong;
        object Id_Kyluong = null;

        public Frmrex_Kyluong()
        {
            InitializeComponent();
            this.DisplayInfo();
        }

        public override void DisplayInfo()
        {
            try
            {
                ds_Kyluong = objRexService.Get_All_Rex_Kyluong_Collection().ToDataSet();
                dgrex_Kyluong.DataSource = ds_Kyluong;
                dgrex_Kyluong.DataMember = ds_Kyluong.Tables[0].TableName;

                this.Data = ds_Kyluong;
                this.GridControl = dgrex_Kyluong;
                this.DataBindingControl();
                this.ChangeStatus(false);
                this.gridView1.BestFitColumns();
                this.gridView1.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ClearDataBindings()
        {
            this.txtMa_Kyluong.DataBindings.Clear();
            this.txtTen_Kyluong.DataBindings.Clear();
            this.txtThang_Kyluong.DataBindings.Clear();
            this.txtNam_Kyluong.DataBindings.Clear();
        }

        public void DataBindingControl()
        {
            try
            {
                ClearDataBindings();
                this.txtMa_Kyluong.DataBindings.Add("EditValue", ds_Kyluong, ds_Kyluong.Tables[0].TableName + ".Ma_Kyluong");
                this.txtTen_Kyluong.DataBindings.Add("EditValue", ds_Kyluong, ds_Kyluong.Tables[0].TableName + ".Ten_Kyluong");
                this.txtThang_Kyluong.DataBindings.Add("EditValue", ds_Kyluong, ds_Kyluong.Tables[0].TableName + ".Thang_Kyluong");
                this.txtNam_Kyluong.DataBindings.Add("EditValue", ds_Kyluong, ds_Kyluong.Tables[0].TableName + ".Nam_Kyluong");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChangeStatus(bool editTable)
        {
            this.dgrex_Kyluong.Enabled = !editTable;    
            //this.gridView1.OptionsBehavior.Editable = !editTable;     
            this.txtMa_Kyluong.Properties.ReadOnly = !editTable;
            this.txtTen_Kyluong.Properties.ReadOnly = !editTable;
            this.txtThang_Kyluong.Properties.ReadOnly = !editTable;
            this.txtNam_Kyluong.Properties.ReadOnly = !editTable;
        }

        public override void ResetText()
        {
            this.txtMa_Kyluong.EditValue = "";
            this.txtTen_Kyluong.EditValue = "";
            this.txtThang_Kyluong.EditValue = DateTime.Today.Month;
            this.txtNam_Kyluong.EditValue = DateTime.Today.Year;
        }

        #region Event Override
        public object InsertObject()
        {
            try
            {
                Ecm.WebReferences.RexService.Rex_Kyluong objRex_Kyluong = new Ecm.WebReferences.RexService.Rex_Kyluong();
                objRex_Kyluong.Id_Kyluong = -1;
                objRex_Kyluong.Ma_Kyluong = txtMa_Kyluong.EditValue;
                objRex_Kyluong.Ten_Kyluong = txtTen_Kyluong.EditValue;
                objRex_Kyluong.Thang_Kyluong = txtThang_Kyluong.EditValue;
                objRex_Kyluong.Nam_Kyluong = txtNam_Kyluong.EditValue;
                objRexService.Insert_Rex_Kyluong(objRex_Kyluong);
                return true;
            } 
            catch(Exception ex)
            {                           
                if (ex.ToString().IndexOf("exists") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblThang_Kyluong.Text + " và " + lblNam_Kyluong.Text, lblThang_Kyluong.Text + " và " + lblNam_Kyluong.Text });
                }
                return false;
            }
        }

        public object UpdateObject()
        {
            try
            {
                Ecm.WebReferences.RexService.Rex_Kyluong objRex_Kyluong = new Ecm.WebReferences.RexService.Rex_Kyluong();
                objRex_Kyluong.Id_Kyluong = Id_Kyluong;
                objRex_Kyluong.Ma_Kyluong = txtMa_Kyluong.EditValue;
                objRex_Kyluong.Ten_Kyluong = txtTen_Kyluong.EditValue;
                objRex_Kyluong.Thang_Kyluong = txtThang_Kyluong.EditValue;
                objRex_Kyluong.Nam_Kyluong = txtNam_Kyluong.EditValue;
                objRexService.Update_Rex_Kyluong(objRex_Kyluong);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.ToString().IndexOf("exists") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblThang_Kyluong.Text + " và " + lblNam_Kyluong.Text, lblThang_Kyluong.Text + " và " + lblNam_Kyluong.Text });
                }
                return false;
            }
        }

        public object DeleteObject()
        {
            Ecm.WebReferences.RexService.Rex_Kyluong objRex_Kyluong = new Ecm.WebReferences.RexService.Rex_Kyluong();
            objRex_Kyluong.Id_Kyluong = gridView1.GetFocusedRowCellValue("Id_Kyluong");
            return objRexService.Delete_Rex_Kyluong(objRex_Kyluong);
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
            Id_Kyluong = gridView1.GetFocusedRowCellValue("Id_Kyluong");
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
                hashtableControls.Add(txtMa_Kyluong, lblMa_Kyluong.Text);
                hashtableControls.Add(txtTen_Kyluong, lblTen_Kyluong.Text);

                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;
                /*System.Collections.Hashtable htb = new System.Collections.Hashtable();
                htb.Add(txtMa_Kyluong, lblMa_Kyluong.Text);

                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckExistValues(htb, (DataSet)dgrex_Kyluong.DataSource, "Ma_Kyluong"))
                    return false;*/

                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;

                if (Convert.ToInt32(txtThang_Kyluong.EditValue) > 12)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show("Tháng Kỳ lương không được lớn hơn 12, nhập lại");
                    return false;
                }

                if (this.FormState == GoobizFrame.Windows.Forms.FormState.Add)
                {
                    if (!GoobizFrame.Windows.MdiUtils.Validator.CheckExistValues(hashtableControls, (DataSet)dgrex_Kyluong.DataSource, "Ma_Kyluong"))
                        return false;
                    success = Convert.ToBoolean(this.InsertObject());
                }
                else if (this.FormState == GoobizFrame.Windows.Forms.FormState.Edit)                
                    success = Convert.ToBoolean(this.UpdateObject());
                
                if (success)                
                    this.DisplayInfo();
                
                return success;
            }
            catch (Exception ex)
            {
                if (ex.ToString().IndexOf("exists") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblMa_Kyluong.Text, lblMa_Kyluong.Text });
                }
                return false;
            }
        }   

        public override bool PerformDelete()
        {
            if (GoobizFrame.Windows.Forms.UserMessage.Show("SYS_CONFIRM_BFDELETE", new string[]  {
            GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Rex_Kyluong"),
            GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Rex_Kyluong")   }) == DialogResult.Yes)
            {
                try
                {
                    if (Convert.ToInt32(objMasterService.GetExistReferences("Rex_Kyluong", "Id_Kyluong", this.gridView1.GetFocusedRowCellValue("Id_Kyluong"))) > 0)
                    {
                        GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                        return true;
                    }
                    this.DeleteObject();
                }
                catch (Exception ex)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                    //GoobizFrame.Windows.MdiUtils.Validator.CheckReferencedRecord(ex.Message, "");
                }
                this.DisplayInfo();
            }
            return base.PerformDelete();
        }

        public override object PerformSelectOneObject()
        {
            Ecm.WebReferences.RexService.Rex_Kyluong rex_Kyluong = new Ecm.WebReferences.RexService.Rex_Kyluong();
            try
            {
                int focusedRow = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
                DataRow dr = ds_Kyluong.Tables[0].Rows[focusedRow];
                if (dr != null)
                {
                    rex_Kyluong.Id_Kyluong = dr["Id_Kyluong"];
                    rex_Kyluong.Ma_Kyluong = dr["Ma_Kyluong"];
                    rex_Kyluong.Ten_Kyluong = dr["Ten_Kyluong"];
                }
                Selected_Rex_Kyluong = rex_Kyluong;
                this.Dispose();
                this.Close();
                return rex_Kyluong;
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

        #region code xử lý trên grid --> not use
        /*
         * 
          public override bool PerformSaveChanges()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gridView1.Columns["Ma_Kyluong"], "");
            hashtableControls.Add(gridView1.Columns["Ten_Kyluong"], "");
            hashtableControls.Add(gridView1.Columns["Thang_Kyluong"], "");
            hashtableControls.Add(gridView1.Columns["Nam_Kyluong"], "");
            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gridView1))
                return false;
            try
            {
                this.DoClickEndEdit(dgrex_Kyluong);//dgrex_Kyluong.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                ds_Kyluong.Tables[0].Columns["Ma_Kyluong"].Unique = true;
                objRexService.Update_Rex_Kyluong_Collection(this.ds_Kyluong);
            }
            catch (Exception ex)
            {
                if (ex.ToString().IndexOf("Unique") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblMa_Kyluong.Text, lblMa_Kyluong.Text });
                    return false;
                }
                if (ex.ToString().IndexOf("exists") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblTen_Kyluong.Text, lblTen_Kyluong.Text });
                    return false;
                }
                MessageBox.Show(ex.ToString());
            }
            this.DisplayInfo();
            return true;
        }
         
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.DoClickEndEdit(dgrex_Kyluong);//this.dgrex_Kyluong.EmbeddedNavigator.Buttons.EndEdit.DoClick();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            this.gridView1.FocusedColumn = gridView1.Columns["Ma_Kyluong"];
            this.addnewrow_clicked = true;
        }

        private void dgrex_Kyluong_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if (Convert.ToInt32(objMasterService.GetExistReferences("Rex_Kyluong", "Id_Kyluong", this.gridView1.GetFocusedRowCellValue("Id_Kyluong"))) > 0)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("Msg00015", new string[] { this.Text.ToLower() });
                    e.Handled = true;
                }
            }
        }
        */
        #endregion
    }
}


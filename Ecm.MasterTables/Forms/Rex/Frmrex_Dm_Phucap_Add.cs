using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using GoobizFrame.Windows.Forms;

namespace Ecm.MasterTables.Forms.Rex
{
    public partial class Frmrex_Dm_Phucap_Add : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        DataSet ds_Phucap = new DataSet();
        public Ecm.WebReferences.MasterService.Rex_Dm_Phucap Selected_Rex_Dm_Phucap;
        //object heso_phucap; 
        //object luong_phucap;

        public Frmrex_Dm_Phucap_Add()
        {
            InitializeComponent();
            this.DisplayInfo();
        }

        public override void DisplayInfo()
        {
            try
            {
                //gridLookUpEdit_Dm_Chucvu
                gridLookUpEdit_Dm_Chucvu.DataSource = objMasterService.Get_All_Rex_Dm_Chucvu_Collection().ToDataSet().Tables[0];

                ds_Phucap = objMasterService.Get_All_Rex_Dm_Phucap_Collection().ToDataSet();
                treelist_Phucap.DataSource = ds_Phucap;
                treelist_Phucap.DataMember = ds_Phucap.Tables[0].TableName;
                this.Data = ds_Phucap;

                this.DataBindingControl();
                this.ChangeStatus(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void ClearDataBindings()
        {
            this.txtMa_Phucap.DataBindings.Clear();
            this.txtTen_Phucap.DataBindings.Clear();
            this.txtHeso.DataBindings.Clear();
            this.txtLuong_Phucap.DataBindings.Clear();
            this.checkEdit_Phucap_Chung.DataBindings.Clear();
            this.checkEdit_Phucap_Chiuthue.DataBindings.Clear();
        }

        public override void DataBindingControl()
        {
            try
            {
                ClearDataBindings();
                this.txtMa_Phucap.DataBindings.Add("EditValue", ds_Phucap, ds_Phucap.Tables[0].TableName + ".Ma_Phucap");
                this.txtTen_Phucap.DataBindings.Add("EditValue", ds_Phucap, ds_Phucap.Tables[0].TableName + ".Ten_Phucap");
                this.txtHeso.DataBindings.Add("EditValue", ds_Phucap, ds_Phucap.Tables[0].TableName + ".Heso_Phucap");
                this.txtLuong_Phucap.DataBindings.Add("EditValue", ds_Phucap, ds_Phucap.Tables[0].TableName + ".Luong_Phucap");
                this.checkEdit_Phucap_Chung.DataBindings.Add("EditValue", ds_Phucap, ds_Phucap.Tables[0].TableName + ".Phucap_Chung");
                this.checkEdit_Phucap_Chiuthue.DataBindings.Add("EditValue", ds_Phucap, ds_Phucap.Tables[0].TableName + ".Chiuthue");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void ChangeStatus(bool editTable)
        {
            this.treelist_Phucap.Enabled = !editTable;
            this.txtMa_Phucap.Properties.ReadOnly = !editTable;
            this.txtTen_Phucap.Properties.ReadOnly = !editTable;
            this.txtHeso.Properties.ReadOnly = !editTable;
            this.txtLuong_Phucap.Properties.ReadOnly = !editTable;
            this.checkEdit_Phucap_Chiuthue.Properties.ReadOnly = !editTable;
            this.checkEdit_Phucap_Chung.Properties.ReadOnly = !editTable;
            btnThem.Enabled = !editTable;
            btnThem_Child.Enabled = !editTable;
            btnXoa.Enabled = !editTable;
        }

        public override void ResetText()
        {
            this.txtMa_Phucap.EditValue = "";
            this.txtTen_Phucap.EditValue = "";
            this.txtHeso.EditValue = 0;
            this.txtLuong_Phucap.EditValue = 0;
            this.checkEdit_Phucap_Chiuthue.EditValue = null;
            this.checkEdit_Phucap_Chung.EditValue = null;
        }

        private void Frmrex_Dm_Phucap_Add_Load(object sender, EventArgs e)
        {
            this.DisplayInfo();
        }

        #region Event Override
        public object InsertObject()
        {
            try
            {
                Ecm.WebReferences.MasterService.Rex_Dm_Phucap objRex_Dm_Phucap = new Ecm.WebReferences.MasterService.Rex_Dm_Phucap();

                objRex_Dm_Phucap.Ma_Phucap = txtMa_Phucap.EditValue;
                objRex_Dm_Phucap.Ten_Phucap = txtTen_Phucap.EditValue;
                objRex_Dm_Phucap.Heso_Phucap = txtHeso.EditValue;
                objRex_Dm_Phucap.Luong_Phucap = txtLuong_Phucap.EditValue;
                objRex_Dm_Phucap.Chiuthue = checkEdit_Phucap_Chiuthue.Checked;
                objRex_Dm_Phucap.Phucap_Chung = checkEdit_Phucap_Chung.Checked;

                objMasterService.Insert_Rex_Dm_Phucap(objRex_Dm_Phucap);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Arithmetic overflow error converting nvarchar to data type numeric") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("INVALID_NUMVALUE", new string[] { });
                }
                else if (ex.Message.IndexOf("Input string was not in a correct format") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("INVALID_NUMVALUE", new string[] { });
                }
                else if (ex.Message.IndexOf("Value was either too large or too small for a Decimal") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("INVALID_NUMVALUE", new string[] { });
                }
                else
                    GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");

                return false;
            }
        }

        public object UpdateObject()
        {
            try
            {
                Ecm.WebReferences.MasterService.Rex_Dm_Phucap objRex_Dm_Phucap = new Ecm.WebReferences.MasterService.Rex_Dm_Phucap();
                objRex_Dm_Phucap.Id_Dm_Phucap = treelist_Phucap.FocusedNode.GetValue("Id_Dm_Phucap");
                objRex_Dm_Phucap.Ma_Phucap = txtMa_Phucap.EditValue;
                objRex_Dm_Phucap.Ten_Phucap = txtTen_Phucap.EditValue;
                if ("" + txtHeso.EditValue != "")
                    objRex_Dm_Phucap.Heso_Phucap = txtHeso.EditValue;
                if ("" + txtLuong_Phucap.EditValue != "")
                    objRex_Dm_Phucap.Luong_Phucap = txtLuong_Phucap.EditValue;
                objRex_Dm_Phucap.Chiuthue = checkEdit_Phucap_Chiuthue.Checked;
                objRex_Dm_Phucap.Phucap_Chung = checkEdit_Phucap_Chung.Checked;
                if ("" + treelist_Phucap.FocusedNode.GetValue("Id_Parent") != "")
                    objRex_Dm_Phucap.Id_Parent = treelist_Phucap.FocusedNode.GetValue("Id_Parent");

                objRex_Dm_Phucap.Id_Chucvu = treelist_Phucap.FocusedNode.GetValue("Id_Chucvu") + "";

                objMasterService.Update_Rex_Dm_Phucap(objRex_Dm_Phucap);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Arithmetic overflow error converting nvarchar to data type numeric") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("INVALID_NUMVALUE", new string[] { });
                }
                else if (ex.Message.IndexOf("Arithmetic overflow error converting numeric to data type numeric") != -1)
                    GoobizFrame.Windows.Forms.UserMessage.Show("INVALID_NUMVALUE", new string[] { });
                else if (ex.Message.IndexOf("Error converting data type numeric to numeric") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("INVALID_NUMVALUE", new string[] { });
                }
                else
                    GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                return false;
            }
        }

        public object DeleteObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Phucap objRex_Dm_Phucap = new Ecm.WebReferences.MasterService.Rex_Dm_Phucap();
            objRex_Dm_Phucap.Id_Dm_Phucap = treelist_Phucap.FocusedNode.GetValue("Id_Dm_Phucap");

            return objMasterService.Delete_Rex_Dm_Phucap(objRex_Dm_Phucap);
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
            bool save = true;
            try
            {
                GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(txtMa_Phucap, lblMa_Phucap.Text);
                hashtableControls.Add(txtTen_Phucap, lblTen_Phucap.Text);
                //hashtableControls.Add(txtHeso, lblHeso.Text);
                //hashtableControls.Add(txtLuong_Tangthem, lblLuong_Tangthem.Text);

                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;

                System.Collections.Hashtable htb = new System.Collections.Hashtable();
                htb.Add(txtMa_Phucap, lblMa_Phucap.Text);

                System.Collections.Hashtable htbTen_Phucap = new System.Collections.Hashtable();
                htbTen_Phucap.Add(txtTen_Phucap, lblTen_Phucap.Text);

                if (this.FormState == GoobizFrame.Windows.Forms.FormState.Add)
                {
                    if (!GoobizFrame.Windows.MdiUtils.Validator.CheckExistValues(htb, ds_Phucap, "Ma_Phucap"))
                        return false;

                    if (!GoobizFrame.Windows.MdiUtils.Validator.CheckExistValues(htbTen_Phucap, ds_Phucap, "Ma_Phucap"))
                        return false;
                    save = Convert.ToBoolean(this.InsertObject());
                }
                else if (this.FormState == GoobizFrame.Windows.Forms.FormState.Edit)
                {
                    save = Convert.ToBoolean(this.UpdateObject());
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().IndexOf("exists") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("Msg00014", new string[] { lblTen_Phucap.Text, lblTen_Phucap.Text });
                }
                else
                    GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                return false;
            }
            if (save)
            {
                DisplayInfo();
                return true;
            }
            else
                return false;
        }

        public override bool PerformSaveChanges()
        {
            treelist_Phucap.MovePrev();
            treelist_Phucap.MoveNext();
            DataSet ds = ds_Phucap.GetChanges();
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                if (r.RowState != DataRowState.Deleted)
                {
                    if ("" + r["Ten_Phucap"] == "")
                    {
                        GoobizFrame.Windows.Forms.UserMessage.Show("SYS_NULL_VALUE", new string[] { "tên phụ cấp" });
                        treelist_Phucap.SetFocusedNode(treelist_Phucap.FindNodeByFieldValue("Id_Dm_Phucap", r["Id_Dm_Phucap"]));
                        return false;
                    }
                    if ("" + r["Ma_Phucap"] == "")
                    {
                        GoobizFrame.Windows.Forms.UserMessage.Show("SYS_NULL_VALUE", new string[] { "Mã phụ cấp" });
                        treelist_Phucap.SetFocusedNode(treelist_Phucap.FindNodeByFieldValue("Id_Dm_Phucap", r["Id_Dm_Phucap"]));
                        return false;
                    }
                    foreach (DataRow check in ds_Phucap.Tables[0].Rows)
                    {
                        if (check.RowState != DataRowState.Deleted)
                        {
                            if ("" + r["Ten_Phucap"] == "" + check["Ten_Phucap"] && "" + r["Id_Dm_Phucap"] != "" + check["Id_Dm_Phucap"] && "" + r["Id_Parent"] == "" + check["Id_Parent"])
                            {
                                GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { "tên phụ cấp" });
                                treelist_Phucap.SetFocusedNode(treelist_Phucap.FindNodeByFieldValue("Id_Dm_Phucap", r["Id_Dm_Phucap"]));
                                return false;
                            }
                        }
                    }
                }
            }
            try
            {
                objMasterService.Update_Rex_Dm_Phucap_Collection(ds_Phucap);
            }
            catch (Exception ex)
            {
                //if (ex.Message.IndexOf("Arithmetic overflow error converting numeric to data type numeric") != -1)
                //    GoobizFrame.Windows.Forms.UserMessage.Show("INVALID_NUMVALUE", new string[] { });
                //else if (ex.Message.IndexOf("Arithmetic overflow error converting nvarchar to data type numeric") != -1)
                //{
                //    GoobizFrame.Windows.Forms.UserMessage.Show("INVALID_NUMVALUE", new string[] { });
                //}
                //else GoobizFrame.Windows.Forms.UserMessage.Show("INVALID_NUMVALUE", new string[] { });
               
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                return false;
            }
            DisplayInfo();
            return true;
        }

        public override bool PerformDelete()
        {
            if (GoobizFrame.Windows.Forms.UserMessage.Show("SYS_CONFIRM_BFDELETE", new string[] { "phụ cấp" }) == DialogResult.Yes)
            {
                try
                {
                    this.DeleteObject();
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("ITEM_EXISTS") != -1)
                        GoobizFrame.Windows.Forms.UserMessage.Show("SYS_DATA_INUSE", new string[] { "phụ cấp" });
                    else
                        GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                    return false;

                }
                this.DisplayInfo();
            }
            return base.PerformDelete();
        }

        public override object PerformSelectOneObject()
        {
            Ecm.WebReferences.MasterService.Rex_Dm_Phucap rex_Dm_Phucap = new Ecm.WebReferences.MasterService.Rex_Dm_Phucap();
            try
            {
                rex_Dm_Phucap.Id_Dm_Phucap = treelist_Phucap.FocusedNode.GetValue("Id_Dm_Phucap");
                rex_Dm_Phucap.Ma_Phucap = treelist_Phucap.FocusedNode.GetValue("Ma_Phucap");
                rex_Dm_Phucap.Ten_Phucap = treelist_Phucap.FocusedNode.GetValue("Ten_Phucap");
                rex_Dm_Phucap.Heso_Phucap = treelist_Phucap.FocusedNode.GetValue("Heso_Phucap");
                rex_Dm_Phucap.Luong_Phucap = treelist_Phucap.FocusedNode.GetValue("Luong_Phucap");

                Selected_Rex_Dm_Phucap = rex_Dm_Phucap;
                this.Dispose();
                this.Close();
                return rex_Dm_Phucap;
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!this.EnableAdd)
            {
                GoobizFrame.Windows.Forms.UserMessage.Show("SYS_NOTALLOW_ADD", new string[] { });
                return;
            }
            //neu khong focus node -> khong thuc hien them node con
            if (treelist_Phucap.FocusedNode == null) return;

            treelist_Phucap.LockReloadNodes();
            try
            {
                object parentNode = treelist_Phucap.FocusedNode.GetValue("Id_Dm_Phucap");
                DevExpress.XtraTreeList.Nodes.TreeListNode node;
                if ("" + parentNode == "")
                {
                    node = treelist_Phucap.AppendNode(new object[] { }, null);
                }
                else
                {
                    node = treelist_Phucap.AppendNode(new object[] { }, treelist_Phucap.FindNodeByFieldValue("Id_Dm_Phucap", parentNode));
                }
                treelist_Phucap.Nodes.Add(node);
                treelist_Phucap.SetFocusedNode(node);
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, "");
            }
            finally
            {
                treelist_Phucap.UnlockReloadNodes();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            treelist_Phucap.LockReloadNodes();
            treelist_Phucap.DeleteNode(treelist_Phucap.FocusedNode);
            treelist_Phucap.UnlockReloadNodes();
        }

        private void btnThem_Child_Click(object sender, EventArgs e)
        {
            if (!this.EnableAdd)
            {
                GoobizFrame.Windows.Forms.UserMessage.Show("SYS_NOTALLOW_ADD", new string[] { });
                return;
            }
            //if (Convert.ToBoolean(treelist_Phucap.FocusedNode.GetValue("Phucap_Chung")))
            //{
            //    return;
            //}
            treelist_Phucap.LockReloadNodes();
            try
            {
                Frmrex_Dm_Chucvu_Add frmrex_Chucvu = new Frmrex_Dm_Chucvu_Add(true);
                GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmrex_Chucvu);
                frmrex_Chucvu.StartPosition = FormStartPosition.CenterScreen;
                frmrex_Chucvu.ShowDialog();
                DevExpress.XtraTreeList.Nodes.TreeListNode nodeParent = treelist_Phucap.FocusedNode;
                if (frmrex_Chucvu.row_Select != null)
                {
                    var Id_Dm_Phucap = long.MaxValue;
                    foreach (DataRow r in frmrex_Chucvu.row_Select)
                    {
                        DevExpress.XtraTreeList.Nodes.TreeListNode node = treelist_Phucap.AppendNode(new object[] { }, nodeParent);
                        node.SetValue("Id_Dm_Phucap", Id_Dm_Phucap--);
                        node.SetValue("Ma_Phucap", r["Ma_Chucvu"]);
                        node.SetValue("Ten_Phucap", r["Ten_Chucvu"]);
                        node.SetValue("Id_Chucvu", r["Id_Chucvu"]);

                        treelist_Phucap.Nodes.Add(node);
                        treelist_Phucap.SetFocusedNode(node);
                    }
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.TrayMessage.TrayMessage.Status = new GoobizFrame.Windows.TrayMessage.TrayMessageInfo(MessageBoxIcon.Asterisk, ex.Message, ex.StackTrace);
            }
            finally
            {
                treelist_Phucap.UnlockReloadNodes();
            }
        }

        private void treelist_Thue_TTDB_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null)
            {
                if (!string.IsNullOrWhiteSpace( e.Node.GetValue("Id_Dm_Phucap")+""))
                {
                    DataRow[] row = ds_Phucap.Tables[0].Select(string.Format("Id_Dm_Phucap = {0}", e.Node.GetValue("Id_Dm_Phucap")));
                    if (row[0].RowState == DataRowState.Added || "" + e.Node.GetValue("Id_Parent") != "")
                    {
                        btnThem_Child.Enabled = false;
                        //btnXoa.Enabled = true;
                    }
                    else
                    {
                        btnThem_Child.Enabled = true;
                        //btnXoa.Enabled = false;
                    }
                }
            }
        }

        private void txtMa_Phucap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar).Equals(39))
                e.Handled = true;
        }

        private void txtHeso_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //if (e.NewValue != null)
            //    if (e.NewValue.ToString().Length > 10)
            //        e.Cancel = true;
        }

        private void txtLuong_Phucap_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //if (e.NewValue != null)
            //    if (e.NewValue.ToString().Length > 10)
            //        e.Cancel = true;
        }

        private void repositoryItemTextEdit2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue != null)
            {
                if (e.NewValue.ToString() == "" || e.NewValue.ToString() == "0")
                    e.Cancel = true;

                //if (e.NewValue.ToString().Length > 10)
                //    e.Cancel = true;
            }
        }

        private void txtTen_Phucap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar).Equals(39))
                e.Handled = true;
        }

        private void treelist_Phucap_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ErrorText = GoobizFrame.Windows.Forms.UserMessage.GetMessage("INVALID_NUMVALUE");
            GoobizFrame.Windows.Forms.UserMessage.Show("INVALID_NUMVALUE", new string[] { });
        }

    }
}


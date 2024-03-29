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
    public partial class Frmpol_Dm_User_Properties : DevExpress.XtraEditors.XtraForm
    {
        private long id_user;

        SunLine.WebReferences.Classes.PolicyService objPolicy = new SunLine.WebReferences.Classes.PolicyService();
        DataSet DataSet_Pol_Dm_Role;
        DataSet DataSet_Pol_Dm_Right;

        Frmpol_Dm_Role_Find _Frmpol_Dm_Role_Find;
        Frmpol_Dm_Right_Find _Frmpol_Dm_Right_Find;
        Frmpol_Dm_Action_Find _Frmpol_Dm_Action_Find;

        public Frmpol_Dm_User_Properties()
        {
            InitializeComponent();
        }

        private void Frmpol_Dm_User_Properties_Load(object sender, EventArgs e)
        {
            this.btbInfo_Update.Enabled = false;

            this.txtUser_Password.Text = "";
            this.txtUser_Password_Confirm.Text = "";

            this.DisplayInfo();
            this.DisplayRole();
            this.DisplayRight();
        }

        public long Id_User
        {
            set { id_user = value; }
            get { return id_user; }
        }

        public void DisplayInfo()
        {
            SunLine.WebReferences.PolicyService.Pol_Dm_User Pol_Dm_User = new SunLine.WebReferences.PolicyService.Pol_Dm_User();
            Pol_Dm_User.Id_User = id_user;
            DataSet Pol_Dm_User_Array = objPolicy.Pol_Dm_User_Select_ByID(Pol_Dm_User);
            this.txtUser_Name.Text = ""+Pol_Dm_User_Array.Tables[0].Rows[0]["User_Name"];
            this.txtUser_Fullname.Text = ""+Pol_Dm_User_Array.Tables[0].Rows[0]["User_Fullname"];
            this.txtUser_Description.Text = ""+Pol_Dm_User_Array.Tables[0].Rows[0]["User_Description"];
            //this.chkUser_Disable.Checked = Pol_Dm_User_Array[0].Disable;
            //this.chkPassword_Mustchange.Checked = Pol_Dm_User_Array[0].Must_Change;
            //this.chkPassword_Changeless.Checked = Pol_Dm_User_Array[0].Cannot_Change;
            //this.chkPassword_Expireless.Checked = Pol_Dm_User_Array[0].Expire_Password;
            //this.chkPassword_Complex.Checked = Pol_Dm_User_Array[0].Complex_Password;
            //this.txtDeadtime.Text = Pol_Dm_User_Array[0].Expire_Day.ToString();
        }
        public void DisplayActionUser()
        {
            if ("" + this.gridView1.GetFocusedRowCellValue("Id_Right") != "")
            {
                SunLine.WebReferences.PolicyService.Pol_Action_User Pol_Action_User = new SunLine.WebReferences.PolicyService.Pol_Action_User();
                Pol_Action_User.Id_Right = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_Right"));
                Pol_Action_User.Id_User = id_user;
                DataSet DataSet_Pol_Dm_Action = objPolicy.Select_Pol_Action_User_ByID_UserRight3(Pol_Action_User);
                DataSet_Pol_Dm_Action.Tables[0].Columns["IsActive"].ReadOnly = false;

                this.dgpol_User_Action.DataSource = DataSet_Pol_Dm_Action.Tables[0];
            }
            else
                this.dgpol_User_Action.DataSource = null;
            
        }

        public bool Update_User_Properties()
        {
            try
            {
                if (this.CheckValidInfo() == false) { return false; }
                SunLine.WebReferences.PolicyService.Pol_Dm_User Pol_Dm_User = new SunLine.WebReferences.PolicyService.Pol_Dm_User();
                Pol_Dm_User.Id_User = id_user;
                Pol_Dm_User.User_Name = txtUser_Name.Text;
                Pol_Dm_User.User_Fullname = txtUser_Fullname.Text;
                Pol_Dm_User.User_Description = txtUser_Description.Text;
                objPolicy.Pol_Dm_User_Update(Pol_Dm_User);   
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                return false;
            }
             return true;
        }

        public bool Update_User_Password()
        {
            try
            {
                if (!this.CheckValidPassword())
                    return false;
                SunLine.WebReferences.PolicyService.Pol_Dm_User Pol_Dm_User = new SunLine.WebReferences.PolicyService.Pol_Dm_User();
                Pol_Dm_User.Id_User = id_user;
                if (txtUser_Password.Text != null & txtUser_Password.Text != "")
                    Pol_Dm_User.User_Password = SecurityManager.HashData(txtUser_Password.Text);
                else Pol_Dm_User.User_Password = txtUser_Password.Text;
                objPolicy.Pol_Dm_User_Password_Update(Pol_Dm_User);
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                return false;
            }
            return true;
        }

        public void DisplayRole()
        {
            SunLine.WebReferences.PolicyService.Pol_User_Role Pol_User_Role = new SunLine.WebReferences.PolicyService.Pol_User_Role();
            Pol_User_Role.Id_User = id_user;
            DataSet_Pol_Dm_Role = objPolicy.Select_Pol_User_Role_ByIDUser3(Pol_User_Role);
            this.dgpol_User_Role.DataSource = DataSet_Pol_Dm_Role.Tables[0];
            if (DataSet_Pol_Dm_Role.Tables[0].Rows.Count > 0)
            {
                this.btbUser_Role_Delete.Enabled = true;
                DataSet_Pol_Dm_Role.Tables[0].Columns.Add("Chon", typeof(bool));
            }
            else
            {
                this.btbUser_Role_Delete.Enabled = false;
            }
        }

        public void AddRole(long[] Id_Role_Set)
        {
            for (int i = 0; i < Id_Role_Set.Length; i++)
            {
                try
                {
                    SunLine.WebReferences.PolicyService.Pol_User_Role Pol_User_Role = new SunLine.WebReferences.PolicyService.Pol_User_Role();
                    Pol_User_Role.Id_User = id_user;
                    Pol_User_Role.Id_Role = Id_Role_Set[i];
                    objPolicy.Pol_User_Role_Insert(Pol_User_Role);
                }
                catch (Exception ex)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                }
            }
        }

        public bool DeleteRole()
        {
            try
            {
                DataRow[] sdr= this.DataSet_Pol_Dm_Role.Tables[0].Select("Chon = true");
                foreach(DataRow dr in sdr)
                {
                    SunLine.WebReferences.PolicyService.Pol_User_Role Pol_User_Role = new SunLine.WebReferences.PolicyService.Pol_User_Role();
                    Pol_User_Role.Id_User = id_user;
                    Pol_User_Role.Id_Role = Convert.ToInt64(dr[ "Id_Role" ]);
                    objPolicy.Pol_User_Role_Delete(Pol_User_Role);
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                return false;
            }
            this.DisplayRole();
            return true;
        }

        public void DisplayRight()
        {
            SunLine.WebReferences.PolicyService.Pol_User_Right Pol_User_Right = new SunLine.WebReferences.PolicyService.Pol_User_Right();
            Pol_User_Right.Id_User = id_user;
            DataSet_Pol_Dm_Right = objPolicy.Select_Pol_User_Right_ByIDUser3(Pol_User_Right);
            this.dgpol_User_Right.DataSource = DataSet_Pol_Dm_Right.Tables[0];
            if (DataSet_Pol_Dm_Right.Tables[0].Rows.Count > 0)
            {
                this.btbUser_Right_Delete.Enabled = true;
                DataSet_Pol_Dm_Right.Tables[0].Columns.Add("Chon", typeof(bool));
            }
            else
            {
                this.btbUser_Right_Delete.Enabled = false;
            }
        }

        public void AddRight(long[] Id_Right_Set)
        {
            for (int i = 0; i < Id_Right_Set.Length; i++)
            {
                try
                {
                    SunLine.WebReferences.PolicyService.Pol_User_Right Pol_User_Right = new SunLine.WebReferences.PolicyService.Pol_User_Right();
                    Pol_User_Right.Id_User = id_user;
                    Pol_User_Right.Id_Right = Id_Right_Set[i];
                    objPolicy.Pol_User_Right_Insert(Pol_User_Right);
                }
                catch (Exception ex)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                }
            }
        }

        public bool DeleteRight()
        {
            try
            {
                DataRow[] sdr = DataSet_Pol_Dm_Right.Tables[0].Select("Chon=true");
                foreach(DataRow dr in sdr)
                {
                    SunLine.WebReferences.PolicyService.Pol_User_Right Pol_User_Right = new SunLine.WebReferences.PolicyService.Pol_User_Right();
                    Pol_User_Right.Id_User = id_user;
                    Pol_User_Right.Id_Right = Convert.ToInt64(dr[ "Id_Right" ]);
                    objPolicy.Pol_User_Right_Delete(Pol_User_Right);
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                return false;
            }
            this.DisplayRight();
            return true;
        }

        public bool CheckValidInfo()
        {
            if (this.txtUser_Fullname.Text == "")
            {
                GoobizFrame.Windows.Forms.UserMessage.Show("Msg00008", new string[] { "Họ tên người dùng", "họ tên người dùng" });
                this.txtUser_Fullname.Focus();
                return false;
            }
            return true;
        }

        public bool CheckValidPassword()
        {
            if (this.txtUser_Password.Text != this.txtUser_Password_Confirm.Text)
            {
                GoobizFrame.Windows.Forms.UserMessage.Show("Msg00019", new string[] { "Mật khẩu", "mật khẩu" });
                this.txtUser_Password_Confirm.Focus();
                return false;
            }
            return true;
        }

        private void btbUser_Role_Add_Click(object sender, EventArgs e)
        {
            if (_Frmpol_Dm_Role_Find == null || _Frmpol_Dm_Role_Find.IsDisposed == true)
                _Frmpol_Dm_Role_Find = new Frmpol_Dm_Role_Find();
            _Frmpol_Dm_Role_Find.StartPosition = FormStartPosition.CenterScreen;
            _Frmpol_Dm_Role_Find.ShowDialog();
            if (_Frmpol_Dm_Role_Find.Id_Role_Selected !=null && _Frmpol_Dm_Role_Find.Id_Role_Selected.Length > 0)
                this.AddRole(_Frmpol_Dm_Role_Find.Id_Role_Selected);
            this.DisplayRole();
        }

        private void btbUser_Role_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = GoobizFrame.Windows.Forms.UserMessage.Show("Msg00021", new string[] { "nhóm quyền đã chọn" });
            if (dlgResult == DialogResult.Yes)
            {
                this.DeleteRole();
            }
        }

        private void btbUser_Right_Add_Click(object sender, EventArgs e)
        {
            if (_Frmpol_Dm_Right_Find == null || _Frmpol_Dm_Right_Find.IsDisposed == true)
                _Frmpol_Dm_Right_Find = new Frmpol_Dm_Right_Find();
            _Frmpol_Dm_Right_Find.StartPosition = FormStartPosition.CenterScreen;
            _Frmpol_Dm_Right_Find.ShowDialog();
            if (_Frmpol_Dm_Right_Find.Id_Right_Selected != null && _Frmpol_Dm_Right_Find.Id_Right_Selected.Length > 0)
                this.AddRight(_Frmpol_Dm_Right_Find.Id_Right_Selected);
            this.DisplayRight();
        }

        private void btbUser_Right_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = GoobizFrame.Windows.Forms.UserMessage.Show("Msg00021", new string[] { "quyền" });
            if (dlgResult == DialogResult.Yes)
            {
                this.DeleteRight();
            }
        }

        private void btbICancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btbPCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btbChange_Password_Click(object sender, EventArgs e)
        {
            DialogResult dlgPass = GoobizFrame.Windows.Forms.UserMessage.Show("Msg00020", new string[] { "mật khẩu" }); //Xác nhận thay đổi mật khẩu
            if (dlgPass == DialogResult.Yes)
            {
                if (this.Update_User_Password())
                    GoobizFrame.Windows.Forms.UserMessage.Show("Msg00011", new string[] { "thay đổi mật khẩu" }); //Thông báo đổi mật khẩu thành công
            }
        }

        private void btbInfo_Update_Click(object sender, EventArgs e)
        {
            if (this.Update_User_Properties())
                this.btbInfo_Update.Enabled = false; //Niếu cập nhật thành công thì khóa nút lưu
        }

        #region Phát hiện thông tin người dùng thay đổi
        private void txtUser_Fullname_Modified(object sender, EventArgs e)
        {
            this.btbInfo_Update.Enabled = true;
        }

        private void txtUser_Description_Modified(object sender, EventArgs e)
        {
            this.btbInfo_Update.Enabled = true;
        }
        #endregion

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayActionUser();
        }

        public void AddActionUser(long[] Id_Action_Set)
        {
            for (int i = 0; i < Id_Action_Set.Length; i++)
            {
                try
                {
                    SunLine.WebReferences.PolicyService.Pol_Action_User Pol_Action_User = new SunLine.WebReferences.PolicyService.Pol_Action_User();
                    Pol_Action_User.Id_Action = Id_Action_Set[i];
                    Pol_Action_User.Id_Right = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_Right"));
                    Pol_Action_User.Id_User = id_user;
                    objPolicy.Pol_Action_User_Insert(Pol_Action_User);
                }
                catch (Exception ex)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                }
            }
        }

        public bool DeleteActionUser()
        {
            try
            {
                int[] selects = this.gridView2.GetSelectedRows();
                for (int i = 0; i < selects.Length; i++)
                {
                    SunLine.WebReferences.PolicyService.Pol_Action_User Pol_Action_User = new SunLine.WebReferences.PolicyService.Pol_Action_User();
                    Pol_Action_User.Id_Action = Convert.ToInt64(this.gridView2.GetRowCellValue(selects[i], "Id_Action"));
                    Pol_Action_User.Id_Right = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_Right"));
                    Pol_Action_User.Id_User = id_user;
                    objPolicy.Pol_Action_User_Delete(Pol_Action_User);
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                return false;
            }
            this.DisplayActionUser();
            return true;
        }

        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsActive")
            {
                try
                {
                    SunLine.WebReferences.PolicyService.Pol_Action_User Pol_Action_User = new SunLine.WebReferences.PolicyService.Pol_Action_User();
                    Pol_Action_User.Id_Action = gridView3.GetFocusedRowCellValue("Id_Action");
                    Pol_Action_User.Id_Right = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_Right")); ;
                    Pol_Action_User.Id_User = Id_User;
                    if (Convert.ToBoolean(e.Value))
                        objPolicy.Pol_Action_User_Insert(Pol_Action_User);
                    else
                        objPolicy.Pol_Action_User_Delete(Pol_Action_User);

                    DisplayActionUser();
                }
                catch { }
            }
        }

        private void chkAll_Role_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in DataSet_Pol_Dm_Role.Tables[0].Rows)
                dr["Chon"] = chkAll_Role.EditValue;
        }

        private void chkAll_Right_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in DataSet_Pol_Dm_Right.Tables[0].Rows)
                dr["Chon"] = chkAll_Right.EditValue;
        }


       

      

        #region Kiểm tra các ràng buộc về mật khẩu
        //public void CheckPasswordPolicyState()
        //{
        //    if (this.chkPassword_Mustchange.Checked)
        //    {
        //        this.txtDeadtime.Enabled = false;
        //        this.txtDeadtime.Text = "1";
        //    }

        //    if (this.chkPassword_Changeless.Checked)
        //    {
        //        this.chkPassword_Mustchange.Enabled = false;
        //        this.chkPassword_Expireless.Enabled = false;
        //        this.chkPassword_Complex.Enabled = false;
        //        txtDeadtime.Enabled = false;
        //    }
        //    else
        //    {
        //        this.chkPassword_Expireless.Enabled = true;
        //        this.chkPassword_Complex.Enabled = true;
        //        txtDeadtime.Enabled = false;
        //    }

        //    if (this.chkPassword_Expireless.Checked)
        //    {
        //        this.txtDeadtime.Enabled = true;
        //    }
        //    else
        //    {
        //        this.txtDeadtime.Enabled = false;
        //    }
        //}

        //private void chkPassword_Mustchange_CheckStateChanged(object sender, EventArgs e)
        //{
        //    this.CheckPasswordPolicyState();
        //}

        //private void chkPassword_Changeless_CheckStateChanged(object sender, EventArgs e)
        //{
        //    this.CheckPasswordPolicyState();
        //}

        //private void chkPassword_Expireless_CheckStateChanged(object sender, EventArgs e)
        //{
        //    this.CheckPasswordPolicyState();
        //}
        #endregion
    }
}
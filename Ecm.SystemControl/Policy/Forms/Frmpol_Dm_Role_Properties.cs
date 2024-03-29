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
    public partial class Frmpol_Dm_Role_Properties : DevExpress.XtraEditors.XtraForm
    {
        private long id_role;
        SunLine.WebReferences.Classes.PolicyService objPolicy = new SunLine.WebReferences.Classes.PolicyService();
        GoobizFrame.Windows.Forms.Policy.Frmpol_Dm_Right_Find _Frmpol_Dm_Right_Find;
        GoobizFrame.Windows.Forms.Policy.Frmpol_Dm_Action_Find _Frmpol_Dm_Action_Find;
        GoobizFrame.Windows.Forms.Policy.Frmpol_Dm_User_Find _Frmpol_Dm_User_Find;

        public Frmpol_Dm_Role_Properties()
        {
            InitializeComponent();
            //Update GUI
            //System.Collections.ArrayList controls = new System.Collections.ArrayList();
            //controls.Add(this.tabpol_Role_Info);
            //controls.Add(this.tabpol_Role_Right);
            //controls.Add(this.tabpol_Role_User);

            //controls.Add(this.lblRole_System_Name);
            //controls.Add(this.lblRole_Description);
            //controls.Add(this.groupControl4);
            //controls.Add(this.lblNote);

            //controls.Add(this.groupControl1);
            //controls.Add(this.gridColumn1);
            //controls.Add(this.gridColumn2);
            //controls.Add(this.gridColumn3);
            //controls.Add(this.btbRole_Right_Add);
            //controls.Add(this.btbRole_Right_Delete);

            //controls.Add(this.groupControl2);
            //controls.Add(this.gridColumn4);
            //controls.Add(this.gridColumn5);
            //controls.Add(this.gridColumn6);
            //controls.Add(this.btbRole_Action_Add);
            //controls.Add(this.btbRole_Action_Delete);

            //controls.Add(this.groupControl3);
            //controls.Add(this.gridColumn7);
            //controls.Add(this.gridColumn8);
            //controls.Add(this.gridColumn9);
            //controls.Add(this.btbRole_User_Add);
            //controls.Add(this.btbRole_User_Delete);

            //controls.Add(this.btbApply);
            //controls.Add(this.btbCancel);

            //SunLine.CultureInfo.Utils.CultureInfoHelper.SetupFormCultureInfo(this, controls);
        }

        private void Frmpol_Dm_Role_Properties_Load(object sender, EventArgs e)
        {
            this.btbApply.Enabled = false;
            this.DisplayInfo();
            this.DisplayRight();
            this.DisplayAction();
            this.DisplayUser();
        }

        public void DisplayInfo()
        {
            SunLine.WebReferences.PolicyService.Pol_Dm_Role Pol_Dm_Role = new SunLine.WebReferences.PolicyService.Pol_Dm_Role();
            Pol_Dm_Role.Id_Role = id_role;
            DataSet Pol_Dm_Role_Array = objPolicy.Pol_Dm_Role_Select_ByID(Pol_Dm_Role);
            this.txtRole_System_Name.Text = ""+Pol_Dm_Role_Array.Tables[0].Rows[0]["Role_System_Name"];
            this.txtRole_Description.Text = ""+Pol_Dm_Role_Array.Tables[0].Rows[0]["Role_Description"];
        }

        public void DisplayRight()
        {
            SunLine.WebReferences.PolicyService.Pol_Role_Right Pol_Role_Right = new SunLine.WebReferences.PolicyService.Pol_Role_Right();
            Pol_Role_Right.Id_Role = id_role;
            DataSet DataSet_Pol_Dm_Right = objPolicy.Select_Pol_Role_Right_ByIDRole3(Pol_Role_Right);
            this.dgpol_Role_Right.DataSource = DataSet_Pol_Dm_Right.Tables[0];
            if (DataSet_Pol_Dm_Right.Tables[0].Rows.Count > 0)
            {
                this.btbRole_Right_Delete.Enabled = true;
            }
            else
            {
                this.btbRole_Right_Delete.Enabled = false;
            }
        }

        public void DisplayAction()
        {
            SunLine.WebReferences.PolicyService.Pol_Action_Role Pol_Action_Role = new SunLine.WebReferences.PolicyService.Pol_Action_Role();
            Pol_Action_Role.Id_Role = id_role;
            Pol_Action_Role.Id_Right = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_Right"));
            DataSet DataSet_Pol_Dm_Action = objPolicy.Select_Pol_Action_Role_ByID_RoleRight3(Pol_Action_Role);
            this.dgpol_Role_Action.DataSource = DataSet_Pol_Dm_Action.Tables[0];
            if (DataSet_Pol_Dm_Action.Tables[0].Rows.Count > 0)
            {
                this.btbRole_Action_Delete.Enabled = true;
            }
            else
            {
                this.btbRole_Action_Delete.Enabled = false;
            }
        }

        public long Id_Role
        {
            set { id_role = value; }
            get { return id_role; }
        }

        public bool Update_Role_Properties()
        {
            try
            {
                SunLine.WebReferences.PolicyService.Pol_Dm_Role Pol_Dm_Role = new SunLine.WebReferences.PolicyService.Pol_Dm_Role();
                Pol_Dm_Role.Id_Role = id_role;
                Pol_Dm_Role.Role_System_Name = txtRole_System_Name.Text;
                Pol_Dm_Role.Role_Description = txtRole_Description.Text;
                objPolicy.Pol_Dm_Role_Update(Pol_Dm_Role);
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                return false;
            }
            return true;
        }

        public void AddRight(long[] Id_Right_Set)
        {
            for (int i = 0; i < Id_Right_Set.Length; i++)
            {
                try
                {
                    SunLine.WebReferences.PolicyService.Pol_Role_Right Pol_Role_Right = new SunLine.WebReferences.PolicyService.Pol_Role_Right();
                    Pol_Role_Right.Id_Right = Id_Right_Set[i];
                    Pol_Role_Right.Id_Role = id_role;
                    objPolicy.Pol_Role_Right_Insert(Pol_Role_Right);
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
                int[] selects = this.gridView1.GetSelectedRows();
                for (int i = 0; i < selects.Length; i++)
                {
                    SunLine.WebReferences.PolicyService.Pol_Role_Right Pol_Role_Right = new SunLine.WebReferences.PolicyService.Pol_Role_Right();
                    Pol_Role_Right.Id_Right = Convert.ToInt64(this.gridView1.GetRowCellValue(selects[i], "Id_Right"));
                    Pol_Role_Right.Id_Role = id_role;
                    objPolicy.Pol_Role_Right_Delete(Pol_Role_Right);
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
        
        public void AddAction(long[] Id_Action_Set)
        {
            for (int i = 0; i < Id_Action_Set.Length; i++)
            {
                try
                {
                    SunLine.WebReferences.PolicyService.Pol_Action_Role Pol_Action_Role = new SunLine.WebReferences.PolicyService.Pol_Action_Role();
                    Pol_Action_Role.Id_Action = Id_Action_Set[i];
                    Pol_Action_Role.Id_Right = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_Right"));
                    Pol_Action_Role.Id_Role = id_role;
                    objPolicy.Pol_Action_Role_Insert(Pol_Action_Role);
                }
                catch (Exception ex)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                }
            }
        }

        public bool DeleteAction()
        {
            try
            {
                int[] selects = this.gridView2.GetSelectedRows();
                for (int i = 0; i < selects.Length; i++)
                {
                    SunLine.WebReferences.PolicyService.Pol_Action_Role Pol_Action_Role = new SunLine.WebReferences.PolicyService.Pol_Action_Role();
                    Pol_Action_Role.Id_Action = Convert.ToInt64(this.gridView2.GetRowCellValue(selects[i], "Id_Action"));
                    Pol_Action_Role.Id_Right = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_Right"));
                    Pol_Action_Role.Id_Role = id_role;
                    objPolicy.Pol_Action_Role_Delete(Pol_Action_Role);
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                return false;
            }
            this.DisplayAction();
            return true;
        }


        public void DisplayUser()
        {
            SunLine.WebReferences.PolicyService.Pol_User_Role Pol_User_Role = new SunLine.WebReferences.PolicyService.Pol_User_Role();
            Pol_User_Role.Id_Role = id_role;
            DataSet DataSet_Pol_Dm_User = objPolicy.Select_Pol_User_Role_ByIDRole3(Pol_User_Role);
            this.dgpol_Role_User.DataSource = DataSet_Pol_Dm_User.Tables[0];
            if (DataSet_Pol_Dm_User.Tables[0].Rows.Count > 0)
            {
                this.btbRole_User_Delete.Enabled = true;
            }
            else
            {
                this.btbRole_User_Delete.Enabled = false;
            }
        }

        public void AddUser(long[] Id_User_Set)
        {
            for (int i = 0; i < Id_User_Set.Length; i++)
            {
                try
                {
                    SunLine.WebReferences.PolicyService.Pol_User_Role Pol_User_Role = new SunLine.WebReferences.PolicyService.Pol_User_Role();
                    Pol_User_Role.Id_User = Id_User_Set[i];
                    Pol_User_Role.Id_Role = id_role;
                    objPolicy.Pol_User_Role_Insert(Pol_User_Role);
                }
                catch (Exception ex)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                }
            }
        }

        public bool DeleteUser()
        {
            try
            {
                int[] selects = this.gridView3.GetSelectedRows();
                for (int i = 0; i < selects.Length; i++)
                {
                    SunLine.WebReferences.PolicyService.Pol_User_Role Pol_User_Role = new SunLine.WebReferences.PolicyService.Pol_User_Role();
                    Pol_User_Role.Id_User = Convert.ToInt64(this.gridView3.GetRowCellValue(selects[i],"Id_User"));
                    Pol_User_Role.Id_Role = id_role;
                    objPolicy.Pol_User_Role_Delete(Pol_User_Role);
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                return false;
            }
            this.DisplayUser();
            return true;
        }
        
        private void btbRole_Right_Add_Click(object sender, EventArgs e)
        {
            if (_Frmpol_Dm_Right_Find == null || _Frmpol_Dm_Right_Find.IsDisposed == true)
                _Frmpol_Dm_Right_Find = new GoobizFrame.Windows.Forms.Policy.Frmpol_Dm_Right_Find();
            _Frmpol_Dm_Right_Find.StartPosition = FormStartPosition.CenterScreen;
            _Frmpol_Dm_Right_Find.ShowDialog();
            if (_Frmpol_Dm_Right_Find.Id_Right_Selected != null && _Frmpol_Dm_Right_Find.Id_Right_Selected.Length > 0)
                this.AddRight(_Frmpol_Dm_Right_Find.Id_Right_Selected);
            this.DisplayRight();
        }

        private void btbRole_Right_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = GoobizFrame.Windows.Forms.UserMessage.Show("Msg00021", new string[] { "quyền" });
            if (dlgResult == DialogResult.Yes)
            {
                this.DeleteRight();
            }
        }

        private void btbRole_Action_Add_Click(object sender, EventArgs e)
        {
            if (_Frmpol_Dm_Action_Find == null || _Frmpol_Dm_Action_Find.IsDisposed == true)
                _Frmpol_Dm_Action_Find = new GoobizFrame.Windows.Forms.Policy.Frmpol_Dm_Action_Find();
            _Frmpol_Dm_Action_Find.StartPosition = FormStartPosition.CenterScreen;
            _Frmpol_Dm_Action_Find.ShowDialog();
            if (_Frmpol_Dm_Action_Find.Id_Action_Selected != null && _Frmpol_Dm_Action_Find.Id_Action_Selected.Length > 0)
                this.AddAction(_Frmpol_Dm_Action_Find.Id_Action_Selected);
            this.DisplayAction();
        }

        private void btbRole_Action_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = GoobizFrame.Windows.Forms.UserMessage.Show("Msg00021", new string[] { "thao tác" });
            if (dlgResult == DialogResult.Yes)
            {
                this.DeleteAction();
            }
        }

        private void btbApply_Click(object sender, EventArgs e)
        {
            if (this.Update_Role_Properties())
                this.btbApply.Enabled = false;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.DisplayAction();
        }

        private void btbCancel_Click(object sender, EventArgs e)
        {
           this.Dispose();
        }

        private void btbRole_User_Add_Click(object sender, EventArgs e)
        {
            if (_Frmpol_Dm_User_Find == null || _Frmpol_Dm_User_Find.IsDisposed == true)
                _Frmpol_Dm_User_Find = new GoobizFrame.Windows.Forms.Policy.Frmpol_Dm_User_Find();
            _Frmpol_Dm_User_Find.StartPosition = FormStartPosition.CenterScreen;
            _Frmpol_Dm_User_Find.ShowDialog();
            if (_Frmpol_Dm_User_Find.Id_User_Selected != null && _Frmpol_Dm_User_Find.Id_User_Selected.Length > 0)
                this.AddUser(_Frmpol_Dm_User_Find.Id_User_Selected);
            this.DisplayUser();
        }

        private void btbRole_User_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = GoobizFrame.Windows.Forms.UserMessage.Show("Msg00021", new string[] { "người dùng" });
            if (dlgResult == DialogResult.Yes)
            {
                this.DeleteUser();
            }
        }

        #region Phát hiện thông tin nhóm quyền thay đổi
        private void txtRole_System_Name_Modified(object sender, EventArgs e)
        {
            this.btbApply.Enabled = true;
        }
        private void txtRole_Description_Modified(object sender, EventArgs e)
        {
            this.btbApply.Enabled = true;
        }
        #endregion
    }
}
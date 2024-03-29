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
    public partial class Frmpol_Dm_Right_Properties : DevExpress.XtraEditors.XtraForm
    {
        private long id_right;
        SunLine.WebReferences.Classes.PolicyService objPolicy = new SunLine.WebReferences.Classes.PolicyService();
        DataSet DataSet_Pol_Dm_User;
        DataSet DataSet_Pol_Dm_Role;

        Frmpol_Dm_User_Find _Frmpol_Dm_User_Find;
        Frmpol_Dm_Action_Find _Frmpol_Dm_Action_Find;
        Frmpol_Dm_Role_Find _Frmpol_Dm_Role_Find;

        public Frmpol_Dm_Right_Properties()
        {
            InitializeComponent();
        }

        public long Id_Right
        {
            set { id_right = value; }
            get { return id_right; }
        }

        private void Frmpol_Dm_Right_Properties_Load(object sender, EventArgs e)
        {
            this.btbApply.Enabled = false;

            this.DisplayInfo();
            this.DisplayUser();
            this.DisplayRole();
            this.DisplayActionUser();
            this.DisplayActionRole();
        }

        public void DisplayInfo()
        {
            SunLine.WebReferences.PolicyService.Pol_Dm_Right Pol_Dm_Right = new SunLine.WebReferences.PolicyService.Pol_Dm_Right();
            Pol_Dm_Right.Id_Right = id_right;
            DataSet Pol_Dm_Right_Array = objPolicy.Pol_Dm_Right_Select_ByID(Pol_Dm_Right);
            this.txtRight_System_Name.Text = ""+Pol_Dm_Right_Array.Tables[0].Rows[0]["Right_System_Name"];
            this.txtRight_Description.Text = ""+Pol_Dm_Right_Array.Tables[0].Rows[0]["Right_Description"];
        }

        public bool Update_Right_Properties()
        {
            try
            {
                SunLine.WebReferences.PolicyService.Pol_Dm_Right Pol_Dm_Right = new SunLine.WebReferences.PolicyService.Pol_Dm_Right();
                Pol_Dm_Right.Id_Right = id_right;
                Pol_Dm_Right.Right_System_Name = this.txtRight_System_Name.Text;
                Pol_Dm_Right.Right_Description = txtRight_Description.Text;
                objPolicy.Pol_Dm_Right_Update(Pol_Dm_Right);
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                return false;
            }
            return true;
        }

        #region User
        /// <summary>
        /// Display User
        /// </summary>
        public void DisplayUser()
        {
            SunLine.WebReferences.PolicyService.Pol_User_Right Pol_User_Right = new SunLine.WebReferences.PolicyService.Pol_User_Right();
            Pol_User_Right.Id_Right = id_right;
            DataSet_Pol_Dm_User = objPolicy.Select_Pol_User_Right_ByIDRight3(Pol_User_Right);
            this.dgpol_User_Right.DataSource = DataSet_Pol_Dm_User.Tables[0];
            if (DataSet_Pol_Dm_User.Tables[0].Rows.Count > 0)
            {
                this.btbUser_Right_Delete.Enabled = true;
                DataSet_Pol_Dm_User.Tables[0].Columns.Add("Chon", typeof(bool));
                DataSet_Pol_Dm_User.Tables[0].Columns["Chon"].ReadOnly = false;
            }
            else
            {
                this.btbUser_Right_Delete.Enabled = false;
            }
        }

        public void AddUser(long[] Id_User_Set)
        {
            for (int i = 0; i < Id_User_Set.Length; i++)
            {
                try
                {
                    SunLine.WebReferences.PolicyService.Pol_User_Right Pol_User_Right = new SunLine.WebReferences.PolicyService.Pol_User_Right();
                    Pol_User_Right.Id_Right = id_right;
                    Pol_User_Right.Id_User = Id_User_Set[i];
                    objPolicy.Pol_User_Right_Insert(Pol_User_Right);
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
                DataRow [] sdr = DataSet_Pol_Dm_User.Tables[0].Select("Chon = true");
                foreach (DataRow dr in sdr)
                {
                    SunLine.WebReferences.PolicyService.Pol_User_Right Pol_User_Right = new SunLine.WebReferences.PolicyService.Pol_User_Right();
                    Pol_User_Right.Id_Right = id_right;
                    Pol_User_Right.Id_User = Convert.ToInt64(dr["Id_User"]);
                    objPolicy.Pol_User_Right_Delete(Pol_User_Right);
                }

            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
            }
            this.DisplayUser();
            return true;
        }

        /// <summary>
        /// Display Action / User
        /// </summary>
        public void DisplayActionUser()
        {
            try
            {
                if ("" + this.gridView1.GetFocusedRowCellValue("Id_User") != "")
                {
                    SunLine.WebReferences.PolicyService.Pol_Action_User Pol_Action_User = new SunLine.WebReferences.PolicyService.Pol_Action_User();
                    Pol_Action_User.Id_Right = id_right;
                    Pol_Action_User.Id_User = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_User"));

                    DataSet DataSet_Pol_Dm_Action = objPolicy.Select_Pol_Action_User_ByID_UserRight3(Pol_Action_User);
                    if (DataSet_Pol_Dm_Action.Tables[0].Columns.Contains("IsActive"))
                        DataSet_Pol_Dm_Action.Tables[0].Columns["IsActive"].ReadOnly = false;
                    this.dgpol_User_Action.DataSource = DataSet_Pol_Dm_Action.Tables[0];
                }
                else
                    this.dgpol_User_Action.DataSource = null;
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
            }

        }

        /// <summary>
        /// add users
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btbUser_Right_Add_Click(object sender, EventArgs e)
        {
            if (_Frmpol_Dm_User_Find == null || _Frmpol_Dm_User_Find.IsDisposed == true)
                _Frmpol_Dm_User_Find = new Frmpol_Dm_User_Find();
            _Frmpol_Dm_User_Find.StartPosition = FormStartPosition.CenterScreen;
            _Frmpol_Dm_User_Find.ShowDialog();
            if (_Frmpol_Dm_User_Find.Id_User_Selected != null && _Frmpol_Dm_User_Find.Id_User_Selected.Length > 0)
                this.AddUser(_Frmpol_Dm_User_Find.Id_User_Selected);
            this.DisplayUser();
        }

        /// <summary>
        /// delete selected users
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btbUser_Right_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = GoobizFrame.Windows.Forms.UserMessage.Show("Msg00021", new string[] { "quyền" });
            if (dlgResult == DialogResult.Yes)
            {
                this.DeleteUser();
            }
        }

        /// <summary>
        /// focus user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.DisplayActionUser();
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsActive")
            {
                try
                {
                    SunLine.WebReferences.PolicyService.Pol_Action_User Pol_Action_User = new SunLine.WebReferences.PolicyService.Pol_Action_User();
                    Pol_Action_User.Id_Action = gridView2.GetFocusedRowCellValue("Id_Action");
                    Pol_Action_User.Id_Right = Id_Right;
                    Pol_Action_User.Id_User = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_User")); ;
                    if (Convert.ToBoolean(e.Value))
                        objPolicy.Pol_Action_User_Insert(Pol_Action_User);
                    else
                        objPolicy.Pol_Action_User_Delete(Pol_Action_User);

                    DisplayActionUser();
                }
                catch { }
            }
        }

        #endregion
        
        #region deleted code
        //        public void AddActionUser(long[] Id_Action_Set)
//        {
//            for (int i = 0; i < Id_Action_Set.Length; i++)
//            {
//                try
//                {
//                    SunLine.WebReferences.PolicyService.Pol_Action_User Pol_Action_User = new SunLine.WebReferences.PolicyService.Pol_Action_User();
//                    Pol_Action_User.Id_Action = Id_Action_Set[i];
//                    Pol_Action_User.Id_Right = id_right;
//                    Pol_Action_User.Id_User = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_User"));
//                    objPolicy.Pol_Action_User_Insert(Pol_Action_User);
//                }
//                catch (Exception ex)
//                {
//#if DEBUG
//                    MessageBox.Show(ex.Message);
//#endif
//                }
//            }
//        }

//        public bool DeleteActionUser()
//        {
//            try
//            {
//                int[] selects = this.gridView2.GetSelectedRows();
//                for (int i = 0; i < selects.Length; i++)
//                {
//                    SunLine.WebReferences.PolicyService.Pol_Action_User Pol_Action_User = new SunLine.WebReferences.PolicyService.Pol_Action_User();
//                    Pol_Action_User.Id_Action = Convert.ToInt64(this.gridView2.GetRowCellValue(selects[i], "Id_Action"));
//                    Pol_Action_User.Id_Right = id_right;
//                    Pol_Action_User.Id_User = Convert.ToInt64(this.gridView1.GetFocusedRowCellValue("Id_User"));
//                    objPolicy.Pol_Action_User_Delete(Pol_Action_User);
//                }
//            }
//            catch (Exception ex)
//            {
//                GoobizFrame.Windows.MdiUtils.Validator.CheckReferencedRecord(ex.Message, "thao tác");
//                return false;
//            }
//            this.DisplayActionUser();
//            return true;
        //        }
        
 //public void AddActionRole(long[] Id_Action_Set)
 //       {
 //           for (int i = 0; i < Id_Action_Set.Length; i++)
 //           {
 //               try
 //               {
 //                   SunLine.WebReferences.PolicyService.Pol_Action_Role Pol_Action_Role = new SunLine.WebReferences.PolicyService.Pol_Action_Role();
 //                   Pol_Action_Role.Id_Action = Id_Action_Set[i];
 //                   Pol_Action_Role.Id_Right = id_right;
 //                   Pol_Action_Role.Id_Role = Convert.ToInt64(this.gridView4.GetFocusedRowCellValue("Id_Role"));
 //                   objPolicy.Pol_Action_Role_Insert(Pol_Action_Role);
 //               }
 //               catch (Exception ex)
 //               {
 //                   #if DEBUG
 //                   MessageBox.Show(ex.Message);
 //                   #endif
 //               }
 //           }
 //       }
        //public void DeleteActionRole()
        //{
        //    try
        //    {
        //        int[] selects = this.gridView3.GetSelectedRows();
        //        for (int i = 0; i < selects.Length; i++)
        //        {
        //            SunLine.WebReferences.PolicyService.Pol_Action_Role Pol_Action_Role = new SunLine.WebReferences.PolicyService.Pol_Action_Role();
        //            Pol_Action_Role.Id_Action = Convert.ToInt64(this.gridView3.GetRowCellValue(selects[i], "Id_Action"));
        //            Pol_Action_Role.Id_Right = id_right;
        //            Pol_Action_Role.Id_Role = Convert.ToInt64(this.gridView4.GetFocusedRowCellValue("Id_Role"));
        //            objPolicy.Pol_Action_Role_Delete(Pol_Action_Role);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        GoobizFrame.Windows.MdiUtils.Validator.CheckReferencedRecord(ex.Message, "thao tác");
        //    }
        //    this.DisplayActionRole();
        //}
        #endregion

        #region Role

        public void DisplayRole()
        {
            SunLine.WebReferences.PolicyService.Pol_Role_Right Pol_Role_Right = new SunLine.WebReferences.PolicyService.Pol_Role_Right();
            Pol_Role_Right.Id_Right = id_right;
            DataSet_Pol_Dm_Role = objPolicy.Select_Pol_Role_Right_ByIDRight3(Pol_Role_Right);
            this.dgpol_Role_Right.DataSource = DataSet_Pol_Dm_Role.Tables[0];
            if (DataSet_Pol_Dm_Role.Tables[0].Rows.Count > 0)
            {
                DataSet_Pol_Dm_Role.Tables[0].Columns.Add("Chon",typeof(bool));
                this.btbRight_Role_Delete.Enabled = true;
            }
            else
            {
                this.btbRight_Role_Delete.Enabled = false;
            }
        }

        public void AddRole(long[] Id_Role_Set)
        {
            for (int i = 0; i < Id_Role_Set.Length; i++)
            {
                try
                {
                    SunLine.WebReferences.PolicyService.Pol_Role_Right Pol_Role_Right = new SunLine.WebReferences.PolicyService.Pol_Role_Right();
                    Pol_Role_Right.Id_Right = id_right;
                    Pol_Role_Right.Id_Role = Id_Role_Set[i];
                    objPolicy.Pol_Role_Right_Insert(Pol_Role_Right);
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
                DataRow[] sdr = DataSet_Pol_Dm_Role.Tables[0].Select("Chon=true");
                foreach(DataRow dr in sdr)
                {
                    SunLine.WebReferences.PolicyService.Pol_Role_Right Pol_Role_Right = new SunLine.WebReferences.PolicyService.Pol_Role_Right();
                    Pol_Role_Right.Id_Right = id_right;
                    Pol_Role_Right.Id_Role = Convert.ToInt64(dr[ "Id_Role"]);
                    objPolicy.Pol_Role_Right_Delete(Pol_Role_Right);
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), this.GetType().Name);
                return true;
            }
            this.DisplayRole();
            return true;
        }

        public void DisplayActionRole()
        {
            if ("" + this.gridView4.GetFocusedRowCellValue("Id_Role") != "")
            {
                SunLine.WebReferences.PolicyService.Pol_Action_Role Pol_Action_Role = new SunLine.WebReferences.PolicyService.Pol_Action_Role();
                Pol_Action_Role.Id_Right = id_right;
                Pol_Action_Role.Id_Role = Convert.ToInt64(this.gridView4.GetFocusedRowCellValue("Id_Role"));
                DataSet DataSet_Pol_Dm_Action = objPolicy.Select_Pol_Action_Role_ByID_RoleRight3(Pol_Action_Role);
                this.dgpol_Role_Action.DataSource = DataSet_Pol_Dm_Action.Tables[0];
                DataSet_Pol_Dm_Action.Tables[0].Columns["IsActive"].ReadOnly = false;
            }
            else
                this.dgpol_Role_Action.DataSource = null;
        }

        private void btbRight_Role_Add_Click(object sender, EventArgs e)
        {
            if (_Frmpol_Dm_Role_Find == null || _Frmpol_Dm_Role_Find.IsDisposed == true)
                _Frmpol_Dm_Role_Find = new Frmpol_Dm_Role_Find();
            _Frmpol_Dm_Role_Find.StartPosition = FormStartPosition.CenterScreen;
            _Frmpol_Dm_Role_Find.ShowDialog();
            if (_Frmpol_Dm_Role_Find.Id_Role_Selected != null && _Frmpol_Dm_Role_Find.Id_Role_Selected.Length > 0)
                this.AddRole(_Frmpol_Dm_Role_Find.Id_Role_Selected);
            this.DisplayRole();
        }

        private void btbRight_Role_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = GoobizFrame.Windows.Forms.UserMessage.Show("Msg00021", new string[] { "nhóm quyền" });
            if (dlgResult == DialogResult.Yes)
            {
                this.DeleteRole();
            }
        }

        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsActive")
            {
                try
                {
                    SunLine.WebReferences.PolicyService.Pol_Action_Role Pol_Action_Role = new SunLine.WebReferences.PolicyService.Pol_Action_Role();
                    Pol_Action_Role.Id_Action = gridView3.GetFocusedRowCellValue("Id_Action");
                    Pol_Action_Role.Id_Right = Id_Right;
                    Pol_Action_Role.Id_Role = Convert.ToInt64(this.gridView4.GetFocusedRowCellValue("Id_Role")); ;
                    if (Convert.ToBoolean(e.Value))
                        objPolicy.Pol_Action_Role_Insert(Pol_Action_Role);
                    else
                        objPolicy.Pol_Action_Role_Delete(Pol_Action_Role);

                    DisplayActionRole();
                }
                catch { }
            }
        }

        #endregion

        #region main tab

        private void btbApply_Click(object sender, EventArgs e)
        {
            if (this.Update_Right_Properties())
                this.btbApply.Enabled = false; //Khóa nút lưu khi lưu thành công
        }

          private void btbCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion


        #region Phát hiên thông tin right thay đổi
        private void txtRight_System_Name_Modified(object sender, EventArgs e)
        {
            this.btbApply.Enabled = true;
        }
        private void txtRight_Description_Modified(object sender, EventArgs e)
        {
            this.btbApply.Enabled = true;
        }
        #endregion

        private void gridView4_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayActionRole();
        }

        private void chkAll_User_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in DataSet_Pol_Dm_User.Tables[0].Rows)
                dr["Chon"] = chkAll_User.EditValue;
        }

        private void chkAll_Role_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in DataSet_Pol_Dm_Role.Tables[0].Rows)
                dr["Chon"] = chkAll_Role.EditValue;
        }

      
    }
}
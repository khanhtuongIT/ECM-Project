using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GoobizFrame.Windows.Forms;

namespace Ecm.SystemControl.Policy.Auth
{
    public class Authorization :  GoobizFrame.Windows.PlugIn.Authorization
    {
        //Khai báo các đối tượng cần dùng
        public Ecm.WebReferences.Classes.PolicyService objPolicy;
        Converter objConverter;
        
        public Authorization()
        { //Khởi tạo các đối tượng cần dùng
            objPolicy = new Ecm.WebReferences.Classes.PolicyService();
            objConverter = new Converter();
        }
       
        public override Actions Accessed2(string right_name, string user_name)
        {
            long id_right = objConverter.Get_Id_Right(right_name);
            long id_user = objConverter.Get_Id_User(user_name);

            Actions objActions = new Actions();

            DataSet Right_Pol_Dm_Action_Array = this.Get_Action_User(id_user, id_right);
            if (Right_Pol_Dm_Action_Array.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < Right_Pol_Dm_Action_Array.Tables[0].Rows.Count; j++)
                {
                    if (!objActions.Contains("" + Right_Pol_Dm_Action_Array.Tables [0].Rows[j]["Action_Name"]))
                        objActions.Add("" + Right_Pol_Dm_Action_Array.Tables [0].Rows[j]["Action_Name"]);
                }
            }
            return objActions;
        }

        public override DataSet Right_Has_Role(long id_right)
        {
            Ecm.WebReferences.PolicyService.Pol_Role_Right Pol_Role_Right = new Ecm.WebReferences.PolicyService.Pol_Role_Right();
            Pol_Role_Right.Id_Right = id_right;
            return objPolicy.Select_Pol_Role_Right_ByIDRight3(Pol_Role_Right).ToDataSet();
        }

        //Kiểm tra người dùng có tồn tại trong nhóm quyền
        public override DataSet Check_User_Role(long id_user, long id_role)
        {
            Ecm.WebReferences.PolicyService.Pol_User_Role Pol_User_Role = new Ecm.WebReferences.PolicyService.Pol_User_Role();
            Pol_User_Role.Id_User = id_user;
            Pol_User_Role.Id_Role = id_role;
            DataSet Pol_Dm_Role_Array = objPolicy.Select_Pol_User_Role_ByID_UserRole1(Pol_User_Role).ToDataSet();
            if (Pol_Dm_Role_Array.Tables[0].Rows.Count > 0)
            {
                return Pol_Dm_Role_Array;
            }
            return null;
        }
       
        //Trả về một mảng các thao tác (Pol_Dm_Action) tương ứng của nhóm quyền (id_role) trên quyền (id_right)
        public override DataSet Get_Action_Role(long id_role, long id_right)
        {
            Ecm.WebReferences.PolicyService.Pol_Action_Role Pol_Action_Role = new Ecm.WebReferences.PolicyService.Pol_Action_Role();
            Pol_Action_Role.Id_Role = id_role;
            Pol_Action_Role.Id_Right = id_right;
            return objPolicy.Select_Pol_Action_Role_ByID_RoleRight3(Pol_Action_Role).ToDataSet();
        }

        //Trả về một mảng các thao tác (Pol_Dm_Action) tương ứng của người dùng (id_user) trên quyền (id_right)
        public override DataSet Get_Action_User(long id_user, long id_right)
        {
            Ecm.WebReferences.PolicyService.Pol_Action_User Pol_Action_User = new Ecm.WebReferences.PolicyService.Pol_Action_User();
            Pol_Action_User.Id_User = id_user;
            Pol_Action_User.Id_Right = id_right;
            return objPolicy.Pol_Action_User_Select_ByID_UserRight_ForAuth(Pol_Action_User).ToDataSet();
        }

        /// <summary>
        /// Tra ve ds roles
        /// </summary>
        /// <param name="Id_User"></param>
        /// <returns></returns>
        public override DataSet Select_Pol_User_Role_ByIDUser(object Id_User)
        {
            Ecm.WebReferences.PolicyService.Pol_User_Role objPol_User_Role = new Ecm.WebReferences.PolicyService.Pol_User_Role();
            objPol_User_Role.Id_User = Id_User;
            return objPolicy.Select_Pol_User_Role_ByIDUser3(objPol_User_Role).ToDataSet();
        }

        public override DateTime GetServerDateTime()
        {
            return objPolicy.GetServerDateTime();
        }
    }
}

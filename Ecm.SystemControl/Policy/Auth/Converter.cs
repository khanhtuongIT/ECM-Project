using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GoobizFrame.Windows.Forms;

namespace Ecm.SystemControl.Policy.Auth
{
    public class Converter
    {
        //Khai báo webservice
        Ecm.WebReferences.Classes.PolicyService objPolicy;

        public Converter()
        {
            //Khởi tạo webservice
            objPolicy = new Ecm.WebReferences.Classes.PolicyService();
        }
        //Trả về ID (id_user) người dùng tương ứng với định danh (user_name)
        public long Get_Id_User(string user_name)
        {
            try
            {
                Ecm.WebReferences.PolicyService.Pol_Dm_User Pol_Dm_User = new Ecm.WebReferences.PolicyService.Pol_Dm_User();
                Pol_Dm_User.User_Name = user_name;
                DataSet Pol_Dm_User_Set = objPolicy.Pol_Dm_User_Select_ByName(Pol_Dm_User).ToDataSet();
                return Convert.ToInt64("" + Pol_Dm_User_Set.Tables[0].Rows[0]["Id_User"]);
            }
            catch (Exception ex)
            {
                 GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                return -1;
            }
        }

        //Trả về ID (id_right) quyền tương ứng với tên (right_name)
        public long Get_Id_Right(string right_name)
        {
            Ecm.WebReferences.PolicyService.Pol_Dm_Right Pol_Dm_Right = new Ecm.WebReferences.PolicyService.Pol_Dm_Right();
            Pol_Dm_Right.Right_System_Name = right_name;
            DataSet Pol_Dm_Right_Set = objPolicy.Pol_Dm_Right_Select_ByName(Pol_Dm_Right).ToDataSet();
            return
                (Pol_Dm_Right_Set.Tables[0].Rows.Count > 0)
                ? Convert.ToInt64(Pol_Dm_Right_Set.Tables[0].Rows[0]["Id_Right"])
                : -1;
        }

        //Trả về ID (id_role) nhóm quyền tương ứng với tên (role_name)
        public long Get_Id_Role(string role_name)
        {
            Ecm.WebReferences.PolicyService.Pol_Dm_Role Pol_Dm_Role = new Ecm.WebReferences.PolicyService.Pol_Dm_Role();
            Pol_Dm_Role.Role_System_Name = role_name;
            DataSet Pol_Dm_Role_Set = objPolicy.Select_Pol_Dm_Role_ByName(Pol_Dm_Role).ToDataSet();
            return Convert.ToInt64(Pol_Dm_Role_Set.Tables[0].Rows[0]["Id_Role"]);
        }
    }
}

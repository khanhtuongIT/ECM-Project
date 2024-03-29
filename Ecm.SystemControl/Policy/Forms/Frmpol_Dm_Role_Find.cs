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
    public partial class Frmpol_Dm_Role_Find : DevExpress.XtraEditors.XtraForm
    {
        private long[] id_role_selected;
        DataSet dsRole;

        public Frmpol_Dm_Role_Find()
        {
            InitializeComponent();
            //update GUI with current CultureInfo
            System.Collections.ArrayList controls = new System.Collections.ArrayList();
            controls.Add(this.gridColumn1);
            controls.Add(this.gridColumn2);
            controls.Add(this.gridColumn3);
            controls.Add(this.btbSelect);
            controls.Add(this.btbCancel);
            controls.Add(this);
            GoobizFrame.Windows.CultureInfo.CultureInfoHelper.SetupFormCultureInfo(this, controls);
        }

        private void Frmpol_Dm_Role_Find_Load(object sender, EventArgs e)
        {
            this.DisplayInfo();
        }

        public long[] Id_Role_Selected
        {
            get { return id_role_selected; }
        }

        public void DisplayInfo()
        {
            SunLine.WebReferences.Classes.PolicyService objPolicy = new SunLine.WebReferences.Classes.PolicyService();
            dsRole = objPolicy.Get_Pol_Dm_Role_Collection3();
            this.dgPol_Dm_Role.DataSource = dsRole.Tables[0];
            dsRole.Tables[0].Columns.Add("Checked",typeof(bool));
        }

        private long[] SelectedRole()
        {
            //int[] selects = gridView1.GetSelectedRows();
            DataRow[] selectedRows = dsRole.Tables[0].Select("Checked=true");
            long[] Id_Role_Array = new long[selectedRows.Length];
            for (int i = 0; i < selectedRows.Length; i++)
            {
                Id_Role_Array[i] = Convert.ToInt64(selectedRows[i]["Id_Role"]);
                //Id_Role_Array[i] = Convert.ToInt64(this.gridView1.GetRowCellValue(selects[i], "Id_Role"));
            }
            return Id_Role_Array;
        }

        private void btbCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btbSelect_Click(object sender, EventArgs e)
        {
            this.id_role_selected = this.SelectedRole();
            this.Dispose();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in dsRole.Tables[0].Rows)
                dr["Checked"] = chkAll.EditValue;
        }
    }
}
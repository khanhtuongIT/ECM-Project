using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;

namespace Ecm.Ware.Forms
{
    public partial class Frmware_Donmuahang_Dialog : Frmware_Donmuahang
    {

        public Frmware_Donmuahang_Dialog()
        {
            InitializeComponent();
            this.item_Add.Visibility    = DevExpress.XtraBars.BarItemVisibility.Never;
            this.item_Edit.Visibility   = DevExpress.XtraBars.BarItemVisibility.Never;
            this.item_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.item_Save.Visibility   = DevExpress.XtraBars.BarItemVisibility.Never;
            this.item_Cancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            this.gvware_Donmuahang_Chitiet.Columns["Chon"].Visible = true;
        }
     
    }
}



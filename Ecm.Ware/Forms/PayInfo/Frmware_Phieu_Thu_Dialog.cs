using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GoobizFrame.Windows.Forms;

namespace Ecm.Ware.Forms
{
    public partial class Frmware_Phieu_Thu_Dialog : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        public Ecm.WebReferences.Classes.WareService objWareService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.WareService>();
        Ecm.WebReferences.Classes.RexService objRexService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.RexService>();

        DataSet ds_Chungtu_Goc = new DataSet();
        object id_nhansu_current;

        public Frmware_Phieu_Thu_Dialog()
        {
            InitializeComponent();
            barSystem.Visible = false;
            id_nhansu_current = GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu();
            this.DisplayInfo();
        }

        void Reload_Hanghoa()
        {
            try
            {
                this.ds_Chungtu_Goc = objWareService.Ware_Nhap_Hh_Ban_Chitiet_SelectAll_DateHang(lookupEdit_Kho_View.EditValue).ToDataSet();
                this.dgPhanbo_Phieuthu_Chitiet.DataSource = ds_Chungtu_Goc;
                this.dgPhanbo_Phieuthu_Chitiet.DataMember = ds_Chungtu_Goc.Tables[0].TableName;
                gvPhanbo_Phieuthu_Chitiet.BestFitColumns();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }
        }

        public override void DisplayInfo()
        {
            DataSet dsNhansu = objRexService.Get_All_Rex_Nhansu_Collection().ToDataSet();
            lookUpEditNhansu_Nop.Properties.DataSource = dsNhansu.Tables[0];

            DataSet dsCuahang = objMasterService.Get_All_Ware_Dm_Cuahang_Ban().ToDataSet();
            lookupEdit_Kho_View.Properties.DataSource = dsCuahang.Tables[0];
            DataTable dtb_soquy;
            DataSet ds_collection = objMasterService.GetRole_System_Name_ById_User(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUserId()).ToDataSet();
            if (ds_collection.Tables[0].Rows.Count > 0 &&
                "" + ds_collection.Tables[0].Rows[0]["Role_System_Name"] == "Administrators")
            {
                dtb_soquy = objMasterService.GetAll_Ware_Dm_Soquy(null).ToDataSet().Tables[0];
                DataRow row = dtb_soquy.NewRow();
                row["Id_Soquy"] = -1;
                row["Ma_Soquy"] = "All";
                row["Ten_Soquy"] = "Tất cả";
                dtb_soquy.Rows.Add(row);
            }
            else
            {
                dtb_soquy = objMasterService.GetAll_Ware_Dm_Soquy_By_Id_Nhansu(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu()).ToDataSet().Tables[0];
            }
            lookUpEdit_Soquy.Properties.DataSource = dtb_soquy;
            Reload_Hanghoa();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (txtSotien_Quydoi.Text == "")
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa nhập số tiền, vui lòng nhập lại!");
                txtSotien_Quydoi.Focus();
            }
            ds_Chungtu_Goc = objWareService.Ware_Phieu_Thu_SelectXuatkho_NotPay(lookUpEdit_Ma_Khachhang.EditValue).ToDataSet();
            dgPhanbo_Phieuthu_Chitiet.DataSource = ds_Chungtu_Goc.Tables[0];

            decimal sotien_phanbo = Convert.ToDecimal("0" + txtSotien_Quydoi.EditValue);
            foreach (DataRow row in ds_Chungtu_Goc.Tables[0].Rows)
            {
                if (sotien_phanbo > 0)
                    if (Convert.ToDecimal("0" + row["Thanhtien"]) >= Convert.ToDecimal("0" + row["Sotien_Dathu"]))
                    {
                        if (Convert.ToDecimal("0" + row["Sotien_Dathu"]) >= 0)
                        {
                            if (Convert.ToDecimal("0" + row["Sotien_Conlai"]) <= sotien_phanbo)
                            {
                                row["Sotien_Phanbo"] = Convert.ToDecimal("0" + row["Sotien_Conlai"]);
                                sotien_phanbo = sotien_phanbo - Convert.ToDecimal("0" + row["Sotien_Conlai"]);
                            }
                            else
                            {
                                row["Sotien_Phanbo"] = sotien_phanbo;
                                sotien_phanbo = 0;
                            }
                        }
                        else
                        {
                            row["Sotien_Phanbo"] = row["Thanhtien"];
                            sotien_phanbo = sotien_phanbo - Convert.ToDecimal("0" + row["Sotien_Dathu"]);
                        }
                    }
                    else
                    {
                        row["Sotien_Phanbo"] = sotien_phanbo;
                        sotien_phanbo = 0;
                    }
                row["Sotien_Conlai"] = Convert.ToDecimal("0" + row["Thanhtien"]) - Convert.ToDecimal("0" + row["Sotien_Dathu"]) - Convert.ToDecimal("0" + row["Sotien_Phanbo"]);
            }
            if (sotien_phanbo > 0) // neu so tien phieu thu > so tien cong no --> tao phieu thu ky gui
            {
                DataRow row_new = ds_Chungtu_Goc.Tables[0].NewRow();
                row_new["Ngay_Chungtu"] = DateTime.Now;
                row_new["Sochungtu"] = null;
                row_new["Thanhtien"] = 0;
                row_new["Sotien_Dathu"] = 0;
                row_new["Sotien_Phanbo"] = sotien_phanbo;
                row_new["Sotien_Conlai"] = 0;
                ds_Chungtu_Goc.Tables[0].Rows.Add(row_new);
            }
            dgPhanbo_Phieuthu_Chitiet.DataSource = ds_Chungtu_Goc.Tables[0];
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                GoobizFrame.Windows.Public.OrderHashtable htbControl1 = new GoobizFrame.Windows.Public.OrderHashtable();
                htbControl1.Add(lookUpEdit_Soquy, labelControl2.Text);
                htbControl1.Add(txtNguoi_Nop, lblNguoi_Nop.Text);
                htbControl1.Add(lookUpEdit_Ma_Khachhang, labelControl3.Text);
                htbControl1.Add(txtLydo, lblLydo.Text);
                htbControl1.Add(txtSotien_Quydoi, lblSotien_Quydoi.Text);

                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(htbControl1))
                    return;

                bool check = false;
                DateTime Ngay_Chungtu = DateTime.Now;
                foreach (DataRow row in ds_Chungtu_Goc.Tables[0].Rows)
                {
                    if (Convert.ToDecimal("0" + row["Sotien_Phanbo"]) <= 0)
                        break;                    
                    Ecm.WebReferences.WareService.Ware_Phieu_Thu objWare_Phieu_Thu = new Ecm.WebReferences.WareService.Ware_Phieu_Thu();
                    objWare_Phieu_Thu.Id_Phieu_Thu = -1;
                    objWare_Phieu_Thu.Id_Cuahang_Ban = lookUpEdit_Soquy.EditValue; // id_Soquy
                    objWare_Phieu_Thu.Ma_Doituong = lookUpEdit_Ma_Khachhang.Text;
                    objWare_Phieu_Thu.Ten_Doituong = null;
                    objWare_Phieu_Thu.Nguoi_Nop = txtNguoi_Nop.Text;
                    objWare_Phieu_Thu.Sochungtu = objWareService.GetNew_Sochungtu("ware_phieu_thu", "sochungtu", "THU_" + lookUpEdit_Soquy.GetColumnValue("Ma_Soquy") + "-"); ;
                    objWare_Phieu_Thu.Ngay_Chungtu = Ngay_Chungtu;
                    objWare_Phieu_Thu.Chungtu_Goc = "" + row["Sochungtu"];
                    objWare_Phieu_Thu.Id_Nhansu_Lapphieu = Convert.ToInt64(id_nhansu_current); ;
                    objWare_Phieu_Thu.Id_Kho_Hanghoa_Mua = null;
                    objWare_Phieu_Thu.Lydo = "" + txtLydo.EditValue;
                    objWare_Phieu_Thu.Id_Tiente = null;
                    objWare_Phieu_Thu.Id_Khachhang = null; // lookUpEdit_Ma_Khachhang.EditValue;
                    objWare_Phieu_Thu.Sotien_Quydoi = row["Sotien_Phanbo"];
                    objWareService.Insert_Ware_Phieu_Thu(objWare_Phieu_Thu);
                    check = true;
                }
                if (check)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show("Đã phân bổ phiếu thu thành công!");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lookUpEdit_Ma_Khachhang_EditValueChanged(object sender, EventArgs e)
        {
            lookUpEditTen_Khachhang.EditValue = lookUpEdit_Ma_Khachhang.EditValue;
            ds_Chungtu_Goc = objWareService.Ware_Phieu_Thu_SelectXuatkho_NotPay(lookUpEdit_Ma_Khachhang.EditValue).ToDataSet();
            dgPhanbo_Phieuthu_Chitiet.DataSource = ds_Chungtu_Goc.Tables[0];
        }

        private void lookupEdit_Kho_View_EditValueChanged(object sender, EventArgs e)
        {
            if ("" + lookupEdit_Kho_View.EditValue == "")
                return;
            DataSet dsKhachhang = objMasterService.Ware_Dm_Khachhang_SelectBy_Khuvuc(lookupEdit_Kho_View.EditValue).ToDataSet();
            lookUpEdit_Ma_Khachhang.Properties.DataSource = dsKhachhang.Tables[0];
            lookUpEditTen_Khachhang.Properties.DataSource = dsKhachhang.Tables[0];
        }

        private void lookUpEditNhansu_Nop_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditNhansu_Nop.EditValue == null)
                txtNguoi_Nop.EditValue = null;
            else
                txtNguoi_Nop.EditValue = lookUpEditNhansu_Nop.GetColumnValue("Hoten_Nhansu");
        }

        private void txtSotien_Quydoi_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ("" + txtSotien_Quydoi.EditValue != "")
                {
                    decimal sotien1 = Convert.ToDecimal(txtSotien_Quydoi.EditValue);
                    if (txtSotien_Quydoi.Text != "")
                    {
                        double sotien = Convert.ToDouble(txtSotien_Quydoi.EditValue);
                        txtSotien_Chu.Text = GoobizFrame.Windows.HelperClasses.ReadNumber.ChangeNum2VNStr(sotien, " đồng.");
                        txtSotien_Chu.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GoobizFrame.Windows.HelperClasses.ReadNumber.ChangeNum2VNStr(sotien, " đồng."));
                    }
                    else
                        txtSotien_Chu.Text = "";
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Số tiền không hợp lý, vui lòng nhập lại!");
                txtSotien_Quydoi.Text = @"0";
            }
        }

    }
}


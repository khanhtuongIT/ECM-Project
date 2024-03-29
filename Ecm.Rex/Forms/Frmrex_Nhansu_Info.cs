#region edit
/*
 * edit     phuuongphan
 * date     06/04/2011
 * content  edit GUI
 */
#endregion
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using GoobizFrame.Windows.Forms;
using Ecm.Rex.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace Ecm.Rex.Forms
{
    public partial class Frmrex_Nhansu_Info : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        public Ecm.WebReferences.Classes.RexService objRexService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.RexService>();
        DataSet ds_Collection = new DataSet();
        Ecm.WebReferences.RexService.Rex_Nhansu nhan_su = new Ecm.WebReferences.RexService.Rex_Nhansu();
        GoobizFrame.Windows.Forms.FormReportWithHeader objFormReport;
        object Id_Nhansu;
        int typeUpdate;
        private bool Is_Save = false;
        private DataView filteredDataView;//dung cho tab Dien bien luong, chon ngach luong -> bac luong

        //dataset datagrid
        DataSet ds_BotriNS = new DataSet();
        DataSet ds_ThamgiaTochuc = new DataSet();
        DataSet ds_Phucap = new DataSet();
        DataSet ds_Dienbienluong = new DataSet();
        DataSet ds_Quanhe_Giadinh = new DataSet();
        DataSet ds_Ktkl = new DataSet();
        DataSet ds_Kynang = new DataSet();
        DataSet ds_QuatrinhCT = new DataSet();
        DataSet ds_QuatrinhDT = new DataSet();
        DataSet ds_Phucap_old = new DataSet();

        public Frmrex_Nhansu_Info()
        {
            InitializeComponent();





        }

        public Frmrex_Nhansu_Info(object id_nhansu)
        {
            Id_Nhansu = id_nhansu;
            InitializeComponent();

            dtNgay_Nhapngu.Properties.MinValue = new DateTime(1950, 01, 01);
            dtNgay_Thoiviec.Properties.MinValue = new DateTime(2000, 01, 01);
            dtNgay_Tuyendung.Properties.MinValue = new DateTime(2000, 01, 01);
            dtNgay_Vao_Dang.Properties.MinValue = new DateTime(1950, 01, 01);
            dtNgay_Vao_Dang_Chinhthuc.Properties.MinValue = new DateTime(1950, 01, 01);
            dtNgay_Vao_Doan.Properties.MinValue = new DateTime(1950, 01, 01);
            dtNgay_Vaolam.Properties.MinValue = new DateTime(2000, 01, 01);
            dtNgay_Xuatngu.Properties.MinValue = new DateTime(1950, 01, 01);
            dtNgaycap.Properties.MinValue = new DateTime(1950, 01, 01);
            dtNgaycap_Hochieu.Properties.MinValue = new DateTime(1950, 01, 01);
            dtNgaycap_So_Bhtn.Properties.MinValue = new DateTime(1950, 01, 01);
            dtNgaycap_So_Bhxh.Properties.MinValue = new DateTime(1950, 01, 01);
            dtNgaycap_So_Bhyt.Properties.MinValue = new DateTime(1950, 01, 01);

            gridDate_Ngay_Batdau.MinValue = new DateTime(1950, 01, 01);
            gridDate_Nam_Batdau_Tochuc.MinValue = new DateTime(1950, 01, 01);
            gridDate_Nam_Ketthuc_Tochuc.MinValue = new DateTime(1950, 01, 01);
            gridDate_Ngay_Batdau.MinValue = new DateTime(1950, 01, 01);
            gridDate_Ngay_Hieuluc_Batdau.MinValue = new DateTime(1950, 01, 01);
            gridDateEdit_Nam_Batdau.MinValue = new DateTime(1950, 01, 01);
            gridDateEdit_Nam_Ketthuc.MinValue = new DateTime(1950, 01, 01);
            gridDateEdit_Nam_Nhaphoc.MinValue = new DateTime(1950, 01, 01);
            gridDateEdit_Nam_Totnghiep.MinValue = new DateTime(1950, 01, 01);
            gridDateEdit_NgayKT.MinValue = new DateTime(1950, 01, 01);
            //this.BarSystem.Visible = false;
            this.item_Add.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.item_Refresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.DisplayInfo();
        }

        public override void ChangeStatus(bool read_only)
        {
            //txtMa_Nhansu.Properties.ReadOnly = read_only;
            //txtHo_Nhansu.Properties.ReadOnly = read_only;
            //txtTen_Nhansu.Properties.ReadOnly = read_only;
            btnThemhinh.Enabled = !read_only;
            btnXoahinh.Enabled = !read_only;

            btnTimNs.Enabled = read_only;

            //txtNgaysinh.Properties.ReadOnly = read_only;
            cmbNgaysinh.Properties.ReadOnly = read_only;
            cmbThangsinh.Properties.ReadOnly = read_only;
            txtNamsinh.Properties.ReadOnly = read_only;

            txtNoisinh.Properties.ReadOnly = read_only;
            txtQuequan.Properties.ReadOnly = read_only;
            txtCMND.Properties.ReadOnly = read_only;
            txtNoicap.Properties.ReadOnly = read_only;
            chkGioitinh.Properties.ReadOnly = read_only;
            lookUp_Chuyenmon.Properties.ReadOnly = read_only;
            lookUp_Ngoaingu.Properties.ReadOnly = read_only;
            lookUp_Vanhoa.Properties.ReadOnly = read_only;
            lookUp_Dantoc.Properties.ReadOnly = read_only;
            lookUp_Honnhan.Properties.ReadOnly = read_only;
            lookUp_Quoctich.Properties.ReadOnly = read_only;
            lookUp_Tongiao.Properties.ReadOnly = read_only;
            lookUp_Tpbanthan.Properties.ReadOnly = read_only;
            lookUp_Tpgiadinh.Properties.ReadOnly = read_only;
            lookUp_Tinhoc.Properties.ReadOnly = read_only;
            txtDiachi_Tamtru.Properties.ReadOnly = read_only;
            txtDiachi_Thuongtru.Properties.ReadOnly = read_only;
            txtDienthoai_Didong.Properties.ReadOnly = read_only;
            dtNgaycap.Properties.ReadOnly = read_only;
            txtHochieu.Properties.ReadOnly = read_only;
            txtNoicap_Hochieu.Properties.ReadOnly = read_only;
            dtNgaycap_Hochieu.Properties.ReadOnly = read_only;
            dtNgay_Vao_Doan.Properties.ReadOnly = read_only;
            dtNgay_Vao_Dang.Properties.ReadOnly = read_only;
            txtNoi_Vao_Doan.Properties.ReadOnly = read_only;
            txtNoi_Vao_Dang.Properties.ReadOnly = read_only;
            txtDienthoai_Nha.Properties.ReadOnly = read_only;
            txtFax.Properties.ReadOnly = read_only;
            txtEmail.Properties.ReadOnly = read_only;
            dtNgay_Tuyendung.Properties.ReadOnly = read_only;
            dtNgay_Vaolam.Properties.ReadOnly = read_only;
            //lookUp_Hopdong.Properties.ReadOnly = read_only;
            //lookUp_Loai_Hopdong.Properties.ReadOnly = read_only;
            //lookUp_Bophan.Properties.ReadOnly = read_only;
            //lookUp_Chucvu.Properties.ReadOnly = read_only;
            dtNgay_Thoiviec.Properties.ReadOnly = read_only;
            chkNghi_Bhxh.Properties.ReadOnly = read_only;

            //Xoa phan nay sau khi them bo nut
            //btnEdit.Enabled = read_only;
            //btnDelete.Enabled = read_only;
            //btnSave.Enabled = !read_only;
            //btnCancel.Enabled = !read_only;
            //btnClose.Enabled = read_only;
            //btnPrint.Enabled = read_only;
            //btnPrint_Card.Enabled = read_only;
            //data grid

            gvrex_Botri_Nhansu.OptionsBehavior.Editable = !read_only;
            gridView4.OptionsBehavior.Editable = !read_only;
            gridView6.OptionsBehavior.Editable = !read_only;
            gridView7.OptionsBehavior.Editable = !read_only;

            gridView3.OptionsBehavior.Editable = !read_only;
            gvQuanhe_Giadinh.OptionsBehavior.Editable = !read_only;
            //gvPhucap.OptionsBehavior.Editable = !read_only;
            gridView8.OptionsBehavior.Editable = !read_only;
            gvrex_Dienbien_Luong.OptionsBehavior.Editable = !read_only;

            //them vo sau
            txtTen_Khac.Properties.ReadOnly = read_only;
            txtTinhtrang_Suckhoe.Properties.ReadOnly = read_only;
            txtChieucao.Properties.ReadOnly = read_only;
            txtCannang.Properties.ReadOnly = read_only;
            txtNhommau.Properties.ReadOnly = read_only;
            txtThuongbinh.Properties.ReadOnly = read_only;
            txtCon_Giadinh_Chinhsach.Properties.ReadOnly = read_only;
            dtNgay_Vao_Dang_Chinhthuc.Properties.ReadOnly = read_only;
            dtNgay_Nhapngu.Properties.ReadOnly = read_only;
            dtNgay_Xuatngu.Properties.ReadOnly = read_only;
            txtQuanham.Properties.ReadOnly = read_only;
            txtDanhhieu_Caonhat.Properties.ReadOnly = read_only;
            txtLyluan_Chinhtri.Properties.ReadOnly = read_only;
            txtQuanly_Nhanuoc.Properties.ReadOnly = read_only;
            memoKhairo.Properties.ReadOnly = read_only;
            memoThamgia.Properties.ReadOnly = read_only;
            memoThannhan.Properties.ReadOnly = read_only;
            txtNghenghiep_Tuyendung.Properties.ReadOnly = read_only;
            txtCoquan_Tuyendung.Properties.ReadOnly = read_only;
            txtCongviec_Chinh.Properties.ReadOnly = read_only;
            txtSotruong_Congtac.Properties.ReadOnly = read_only;
            txtSo_Bhxh.Properties.ReadOnly = read_only;
            lookUp_Loai_Nhanvien.Properties.ReadOnly = read_only;
            dtNgaycap_So_Bhxh.Properties.ReadOnly = read_only;
            txtNoicap_So_Bhxh.Properties.ReadOnly = read_only;
            txtSo_Bhyt.Properties.ReadOnly = read_only;
            dtNgaycap_So_Bhyt.Properties.ReadOnly = read_only;
            txtNoicap_So_Bhyt.Properties.ReadOnly = read_only;
            txtSo_Bhtn.Properties.ReadOnly = read_only;
            dtNgaycap_So_Bhtn.Properties.ReadOnly = read_only;
            txtNoicap_So_Bhtn.Properties.ReadOnly = read_only;
            txtTuoihuu.Properties.ReadOnly = read_only;
        }

        #region Event Override
        public override bool PerformEdit()
        {
            this.ChangeStatus(false);
            return true;
        }
        public override bool PerformClose()
        {
            this.Close();
            return true;
        }
        public override bool PerformCancel()
        {
            this.DisplayInfo();
            return true;
        }

        public override bool PerformDelete()
        {
            if (GoobizFrame.Windows.Forms.UserMessage.Show("Msg00004", new string[]  {
            GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Rex_Nhansu"),
            GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Rex_Nhansu")   }) == DialogResult.Yes)
            {
                objRexService.Delete_Rex_Nhansu(nhan_su);

                this.Close();
            }
            return true;
        }

        public override bool PerformSave()
        {
            try
            {
                if (this.UpdateObject())
                {
                    this.DisplayInfo();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Lỗi cập nhật");
                return false;
            }

        }
        public override bool PerformPrintPreview()
        {
            try
            {

                DataSet ds_NhansuInfo_In_Hoso = objRexService.Get_NhansuInfo_In_Hoso(nhan_su).ToDataSet();
                DataSet ds_phucap = objRexService.Get_Rex_Phucap_For_Hopdong_Laodong_Collection(Id_Nhansu, DateTime.Now).ToDataSet();
                DataSet ds_qtrinh_dtao = objRexService.Get_Quatrinh_Daotao_In_Hoso_Nhansu(Id_Nhansu).ToDataSet();
                DataSet ds_qtrinh_ctac = objRexService.Get_Quatrinh_Congtac_In_Hoso_Nhansu(Id_Nhansu).ToDataSet();
                DataSet ds_qhe_gdinh = objRexService.Get_Quanhe_Giadinh_In_Hoso_Nhansu(Id_Nhansu).ToDataSet();
                DataSet ds_dienbien_luong = objRexService.Get_Dienbien_Luong_In_Hoso_Nhansu(Id_Nhansu).ToDataSet();

                NhansuInfoHelper.PrintSYLL(
                   ds_NhansuInfo_In_Hoso,
                   ds_phucap,
                   ds_qtrinh_dtao,
                   ds_qtrinh_ctac,
                   ds_qhe_gdinh,
                   ds_dienbien_luong, this);

            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
            }
            return true;
        }

        public override void DisplayInfo()
        {
            try
            {
                txtMa_Nhansu.Properties.ReadOnly = true;
                txtHo_Nhansu.Properties.ReadOnly = true;
                txtTen_Nhansu.Properties.ReadOnly = true;

                #region NhansuInfo_Display
                //Get data master table REX
                lookUp_Dantoc.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Dantoc_Collection().ToDataSet().Tables[0];
                lookUp_Honnhan.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Honnhan_Collection().ToDataSet().Tables[0];
                lookUp_Quoctich.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Quocgia_Collection().ToDataSet().Tables[0];
                lookUp_Tongiao.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Tongiao_Collection().ToDataSet().Tables[0];
                lookUp_Tpbanthan.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Tpbanthan_Collection().ToDataSet().Tables[0];
                lookUp_Tpgiadinh.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Tpgiadinh_Collection().ToDataSet().Tables[0];
                lookUp_Vanhoa.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Vanhoa_Collection().ToDataSet().Tables[0];
                lookUp_Chuyenmon.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Chuyenmon_Collection().ToDataSet().Tables[0];
                lookUp_Tinhoc.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Tinhoc_Collection().ToDataSet().Tables[0];
                lookUp_Ngoaingu.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Ngoaingu_Collection().ToDataSet().Tables[0];

                lookUp_Hopdong.Properties.DataSource = objRexService.Get_All_Rex_Hopdong_Laodong_By_Nhansu(Id_Nhansu).ToDataSet().Tables[0];
                lookUp_Loai_Hopdong.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Loai_Hopdong_Collection().ToDataSet().Tables[0];

                //lookUp_Bophan.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Bophan_Collection().Tables[0];
                //lookUp_Chucvu.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Chucvu_Collection().Tables[0];

                lookUp_Loai_Nhanvien.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Loai_Nhanvien_Collection().ToDataSet().Tables[0];

                nhan_su = objRexService.Get_Rex_Nhansu_ById(Id_Nhansu);
                txtMa_Nhansu.Text = Convert.ToString(nhan_su.Ma_Nhansu + "");
                txtHo_Nhansu.Text = Convert.ToString(nhan_su.Ho_Nhansu + "");
                txtTen_Nhansu.Text = Convert.ToString(nhan_su.Ten_Nhansu + "");
                if (nhan_su.Hinh != null)
                {
                    byte[] imagedata = (byte[])nhan_su.Hinh;
                    MemoryStream ms = new MemoryStream();
                    ms.Write(imagedata, 0, imagedata.Length);
                    Image image = Image.FromStream(ms, true);
                    pictureHinh.Image = image;
                }
                else
                {
                    pictureHinh.Image = Ecm.Rex.Properties.Resources.clipping_picture;
                }

                //txtNgaysinh.Text = nhan_su.Ngaysinh.ToString();
                cmbNgaysinh.EditValue = Convert.ToString(nhan_su.Ngay_Sinh + "");
                cmbThangsinh.EditValue = Convert.ToString(nhan_su.Thangsinh + "");
                txtNamsinh.EditValue = Convert.ToString(nhan_su.Namsinh + "");
                txtNoisinh.Text = Convert.ToString(nhan_su.Noisinh + "");
                txtQuequan.Text = Convert.ToString(nhan_su.Quequan + "");
                txtCMND.Text = Convert.ToString(nhan_su.Cmnd + "");
                txtNoicap.Text = Convert.ToString(nhan_su.Noicap + "");
                chkGioitinh.EditValue = Convert.ToBoolean(nhan_su.Gioitinh);
                lookUp_Chuyenmon.EditValue = nhan_su.Id_Chuyenmon;
                lookUp_Dantoc.EditValue = nhan_su.Id_Dantoc;
                lookUp_Honnhan.EditValue = nhan_su.Id_Honnhan;
                lookUp_Quoctich.EditValue = nhan_su.Id_Quocgia;
                lookUp_Tongiao.EditValue = nhan_su.Id_Tongiao;
                lookUp_Tpbanthan.EditValue = nhan_su.Id_Tpbanthan;
                lookUp_Tpgiadinh.EditValue = nhan_su.Id_Tpgiadinh;
                lookUp_Vanhoa.EditValue = nhan_su.Id_Vanhoa;
                lookUp_Tinhoc.EditValue = nhan_su.Id_Tinhoc;
                lookUp_Ngoaingu.EditValue = nhan_su.Id_Ngoaingu;
                txtDiachi_Tamtru.Text = Convert.ToString(nhan_su.Diachi_Tamtru + "");
                txtDiachi_Thuongtru.Text = Convert.ToString(nhan_su.Diachi_Thuongtru + "");
                txtDienthoai_Didong.Text = Convert.ToString(nhan_su.Dienthoai + "");
                txtHochieu.Text = Convert.ToString(nhan_su.Hochieu + "");
                txtNoicap_Hochieu.Text = Convert.ToString(nhan_su.Noicap_Hochieu + "");

                if ("" + nhan_su.Ngaycap != "")
                    dtNgaycap.EditValue = Convert.ToDateTime(nhan_su.Ngaycap);
                    //dtNgaycap.EditValue = String.Format("{0:dd/MM/yyyy}", nhan_su.Ngaycap);                       
                if ("" + nhan_su.Ngaycap_Hochieu != "")
                {
                    dtNgaycap_Hochieu.EditValue = Convert.ToDateTime(nhan_su.Ngaycap_Hochieu);
                }
                else
                {
                    dtNgaycap_Hochieu.EditValue = null;
                }
                if ("" + nhan_su.Ngay_Vaodoan != "")
                {
                    dtNgay_Vao_Doan.EditValue = Convert.ToDateTime(nhan_su.Ngay_Vaodoan);
                }
                else
                {
                    dtNgay_Vao_Doan.EditValue = null;
                }
                if ("" + nhan_su.Ngay_Vaodang != "")
                {
                    dtNgay_Vao_Dang.EditValue = Convert.ToDateTime(nhan_su.Ngay_Vaodang);
                }
                else
                {
                    dtNgay_Vao_Dang.EditValue = null;
                }
                if ("" + nhan_su.Ngay_Tuyendung != "")
                {
                    dtNgay_Tuyendung.EditValue = Convert.ToDateTime(nhan_su.Ngay_Tuyendung);
                }
                else
                {
                    dtNgay_Tuyendung.EditValue = null;
                }
                if ("" + nhan_su.Ngay_Vaolam != "")
                    dtNgay_Vaolam.EditValue = Convert.ToDateTime(nhan_su.Ngay_Vaolam);
                if ("" + nhan_su.Ngay_Thoiviec != "")
                    dtNgay_Thoiviec.EditValue = Convert.ToDateTime(nhan_su.Ngay_Thoiviec);
                if ("" + nhan_su.Ngayvaodang_Chinhthuc != "")
                {
                    dtNgay_Vao_Dang_Chinhthuc.EditValue = Convert.ToDateTime(nhan_su.Ngayvaodang_Chinhthuc);
                }
                else
                {
                    dtNgay_Vao_Dang_Chinhthuc.EditValue = null;
                }
                if ("" + nhan_su.Ngay_Nhapngu != "")
                {
                    dtNgay_Nhapngu.EditValue = Convert.ToDateTime(nhan_su.Ngay_Nhapngu);
                }
                else
                {
                    dtNgay_Nhapngu.EditValue = null;
                }
                if ("" + nhan_su.Ngay_Xuatngu != "")
                {
                    dtNgay_Xuatngu.EditValue = Convert.ToDateTime(nhan_su.Ngay_Xuatngu);
                }
                else
                {
                    dtNgay_Xuatngu.EditValue = null;
                }

                txtNoi_Vao_Doan.Text = Convert.ToString(nhan_su.Noi_Vaodoan + "");
                txtNoi_Vao_Dang.Text = Convert.ToString(nhan_su.Noi_Vaodang + "");
                txtDienthoai_Nha.Text = Convert.ToString(nhan_su.Dienthoai_Nharieng + "");
                txtFax.Text = Convert.ToString(nhan_su.Fax + "");
                txtEmail.Text = Convert.ToString(nhan_su.Email + "");
                lookUp_Hopdong.EditValue = nhan_su.Id_Hopdong_Laodong;
                lookUp_Loai_Hopdong.EditValue = nhan_su.Id_Loai_Hopdong;
                //lookUp_Bophan.EditValue = nhan_su.Id_Bophan;
                //lookUp_Chucvu.EditValue = nhan_su.Id_Chucvu;
                chkNghi_Bhxh.Checked = Convert.ToBoolean(nhan_su.Nghi_Bhxh);
                txtTen_Khac.Text = Convert.ToString(nhan_su.Tenkhac + "");
                txtTinhtrang_Suckhoe.Text = Convert.ToString(nhan_su.Tt_Suckhoe + "");
                txtChieucao.Text = Convert.ToString(nhan_su.Chieucao + "");
                txtCannang.Text = Convert.ToString(nhan_su.Cannang + "");
                txtNhommau.Text = Convert.ToString(nhan_su.Nhommau + "");
                txtThuongbinh.Text = Convert.ToString(nhan_su.Thuongbinh_Hang + "");
                txtCon_Giadinh_Chinhsach.Text = Convert.ToString(nhan_su.Con_Chinh_Sach + "");
                txtQuanham.Text = Convert.ToString(nhan_su.Quan_Ham + "");
                txtDanhhieu_Caonhat.Text = Convert.ToString(nhan_su.Danhhieu_Caonhat + "");
                txtLyluan_Chinhtri.Text = Convert.ToString(nhan_su.Lyluan_Chinhtri + "");
                txtQuanly_Nhanuoc.Text = Convert.ToString(nhan_su.Quanly_Nhanuoc + "");
                memoKhairo.Text = Convert.ToString(nhan_su.Bibat_Bitu + "");
                memoThamgia.Text = Convert.ToString(nhan_su.Thamgia_Chinhtri + "");
                memoThannhan.Text = Convert.ToString(nhan_su.Thannhan_Nuocngoai + "");
                txtNghenghiep_Tuyendung.Text = Convert.ToString(nhan_su.Nghenghiep_Tuyendung + "");
                txtCoquan_Tuyendung.Text = Convert.ToString(nhan_su.Coquan_Tuyendung + "");
                txtCongviec_Chinh.Text = Convert.ToString(nhan_su.Congviec_Chinh + "");
                txtSotruong_Congtac.Text = Convert.ToString(nhan_su.Sotruong_Ct + "");
                lookUp_Loai_Nhanvien.EditValue = nhan_su.Id_Loai_Nhanvien;
                // thông tin bảo hiểm
                txtSo_Bhxh.Text = Convert.ToString(nhan_su.So_Sobhxh + "");
                dtNgaycap_So_Bhxh.EditValue = nhan_su.Ngaycap_Bhxh;
                txtNoicap_So_Bhxh.Text = Convert.ToString(nhan_su.Noicap_Bhxh + "");
                txtSo_Bhyt.EditValue = nhan_su.So_Sobhyt;
                dtNgaycap_So_Bhyt.EditValue = nhan_su.Ngaycap_Bhyt;
                txtNoicap_So_Bhyt.Text = Convert.ToString(nhan_su.Noicap_Bhyt + "");
                txtSo_Bhtn.EditValue = nhan_su.So_Sobhtn;
                dtNgaycap_So_Bhtn.EditValue = nhan_su.Ngaycap_Bhtn;
                txtNoicap_So_Bhtn.Text = Convert.ToString(nhan_su.Noicap_Bhtn + "");
                if ("" + nhan_su.Tuoihuu != "")
                    txtTuoihuu.Text = Convert.ToString(nhan_su.Tuoihuu + "");

                #endregion

                #region BotriNS_Display
                gridLookUp_Bophan_Botri.DataSource = objMasterService.Get_All_Rex_Dm_Bophan_Collection().ToDataSet().Tables[0];
                gridLookUp_Chucvu_Botri.DataSource = objMasterService.Get_All_Rex_Dm_Chucvu_Collection().ToDataSet().Tables[0];
                gridLookUp_Quyetdinh_Botri.DataSource = objMasterService.Get_All_Rex_Dm_Quyetdinh_Collection().ToDataSet().Tables[0];

                ds_BotriNS = objRexService.Get_All_Rex_Botri_Nhansu_byNhanSu_Collection(nhan_su).ToDataSet();
                dgrex_Botri_Nhansu.DataSource = ds_BotriNS;
                dgrex_Botri_Nhansu.DataMember = ds_BotriNS.Tables[0].TableName;
                this.gvrex_Botri_Nhansu.BestFitColumns();
                #endregion

                #region ThamgiaTochuc_Display
                DataSet ds_Tochuc = new DataSet();
                ds_Tochuc = objMasterService.Get_All_Rex_Dm_Tochuc_Collection().ToDataSet();
                gridLookUp_Thamgia_Tochuc.DataSource = ds_Tochuc.Tables[0];

                //DataSet ds_Chucvu = new DataSet();
                gridLookUp_Chucvu_Tochuc.DataSource = objMasterService.Get_All_Rex_Dm_Chucvu_Collection().ToDataSet().Tables[0];

                ds_ThamgiaTochuc = objRexService.Get_All_Rex_Thamgia_Tochuc_ByNhanSu_Collection(nhan_su.Id_Nhansu).ToDataSet();
                dgrex_Thamgia_Tochuc.DataSource = ds_ThamgiaTochuc;
                dgrex_Thamgia_Tochuc.DataMember = ds_ThamgiaTochuc.Tables[0].TableName;
                this.gridView8.BestFitColumns();
                #endregion

                #region Phucap_Display
                //   gridLookUp_Phucap             
                DataSet ds_Dm_Phucap = new DataSet();
                ds_Dm_Phucap = objMasterService.Get_All_Rex_Dm_Phucap_Collection().ToDataSet();
                GridLookUpEdit_Phucap.DataSource = ds_Dm_Phucap.Tables[0];
                GridLookUpEdit_Heso.DataSource = ds_Dm_Phucap.Tables[0];

                DisplayInfo_Phucap();



                this.gvPhucap.BestFitColumns();
                #endregion

                #region Dienbienluong_Display
                DataSet ds_NgachLuong = new DataSet();
                ds_NgachLuong = objMasterService.Get_All_Rex_Dm_Ngachluong_Collection().ToDataSet();
                gridLookUp_Ngachluong.DataSource = ds_NgachLuong.Tables[0];

                DataSet ds_BacLuong = new DataSet();
                ds_BacLuong = objMasterService.Get_All_Rex_Dm_Bacluong_Collection().ToDataSet();
                gridLookUp_Bacluong.DataSource = ds_BacLuong.Tables[0];

                //DataSet ds_Quyetdinh = new DataSet();
                gridLookUp_Quyetdinh.DataSource = objMasterService.Get_All_Rex_Dm_Quyetdinh_Collection().ToDataSet().Tables[0];

                //ds_Dienbienluong = objRexService.Get_All_Rex_Dienbien_Luong_ByNhanSu_Collection(nhan_su).ToDataSet();
                dgrex_Dienbien_Luong.DataSource = ds_Dienbienluong;
                dgrex_Dienbien_Luong.DataMember = ds_Dienbienluong.Tables[0].TableName;
                this.bandedGridColumn1.GetBestWidth();
                #endregion

                #region Quanhe_Giadinh_Display
                DataSet ds_Dm_Quanhe_Giadinh = new DataSet();
                ds_Dm_Quanhe_Giadinh = objMasterService.Get_All_Rex_Dm_Quanhe_Giadinh_Collection().ToDataSet();
                gridLookUp_Quanhe_Giadinh.DataSource = ds_Dm_Quanhe_Giadinh.Tables[0];

                ds_Quanhe_Giadinh = objRexService.Get_All_Rex_Quanhe_Giadinh_ByNhanSu_Collection(nhan_su).ToDataSet();
                dgQuanhe_Giadinh.DataSource = ds_Quanhe_Giadinh;
                dgQuanhe_Giadinh.DataMember = ds_Quanhe_Giadinh.Tables[0].TableName;
                this.gvQuanhe_Giadinh.BestFitColumns();
                #endregion

                #region Ktkl_Display
                DataSet ds_Loai_Ktkl = new DataSet();
                ds_Loai_Ktkl = objMasterService.Get_All_Rex_Dm_Loai_Ktkl_Collection().ToDataSet();
                gridLookUp_Loai_Ktkl.DataSource = ds_Loai_Ktkl.Tables[0];

                DataSet ds_Coquan = new DataSet();
                ds_Coquan = objMasterService.Get_All_Rex_Dm_Coquan_Collection().ToDataSet();
                gridLookUp_Coquan.DataSource = ds_Coquan.Tables[0];

                ds_Ktkl = objRexService.Get_All_Rex_Khenthuong_Kyluat_ByNhanSu_Collection(nhan_su).ToDataSet();
                dgrex_Khenthuong_Kyluat.DataSource = ds_Ktkl;
                dgrex_Khenthuong_Kyluat.DataMember = ds_Ktkl.Tables[0].TableName;
                this.gridView3.BestFitColumns();
                #endregion

                #region Kynang_Display
                gridLookUp_Id_Kynang_Chuyenmon.DataSource = objMasterService.Get_All_Rex_Dm_Kynang_Chuyenmon_Collection().ToDataSet().Tables[0];

                //ds_Kynang = objRexService.Get_All_Rex_Kynang_ByNhanSu_Collection(nhan_su).ToDataSet();
                dgKynang_Lamviec.DataSource = ds_Kynang;
                dgKynang_Lamviec.DataMember = ds_Kynang.Tables[0].TableName;
                this.gridView7.BestFitColumns();
                #endregion

                #region QuatrinhCT_Display
                gridLookUp_Chucvu_Congtac.DataSource = objMasterService.Get_All_Rex_Dm_Chucvu_Collection().ToDataSet().Tables[0];

                ds_Coquan = objMasterService.Get_All_Rex_Dm_Coquan_Collection().ToDataSet();
                gridLookUp_Coquan_Congtac.DataSource = ds_Coquan.Tables[0];

                ds_QuatrinhCT = objRexService.Get_All_Rex_Quatrinh_Congtac_ByNhanSu_Collection(nhan_su).ToDataSet();
                dgQuatrinh_Congtac.DataSource = ds_QuatrinhCT;
                dgQuatrinh_Congtac.DataMember = ds_QuatrinhCT.Tables[0].TableName;
                this.gridView6.BestFitColumns();
                #endregion

                #region QuatrinhDT_Display
                DataSet ds_Chuyenmon = new DataSet();
                ds_Chuyenmon = objMasterService.Get_All_Rex_Dm_Chuyenmon_Collection().ToDataSet();
                gridLookUp_Chuyenmon.DataSource = ds_Chuyenmon.Tables[0];

                //DataSet ds_Vanhoa = new DataSet();
                //ds_Vanhoa = objMasterService.Get_All_Rex_Dm_Vanhoa_Collection();
                //gridLookUpEdit_Vanhoa.DataSource = ds_Vanhoa.Tables[0];

                DataSet ds_Chungchi = new DataSet();
                ds_Chungchi = objMasterService.Get_All_Rex_Dm_Chungchi_Collection().ToDataSet();
                gridLookUp_Chungchi.DataSource = ds_Chungchi.Tables[0];

                ds_QuatrinhDT = objRexService.Get_All_Rex_Quatrinh_Daotao_ByNhanSu_Collection(nhan_su).ToDataSet();
                dgQuatrinh_Daotao.DataSource = ds_QuatrinhDT;
                dgQuatrinh_Daotao.DataMember = ds_QuatrinhDT.Tables[0].TableName;

                this.gridView4.BestFitColumns();
                #endregion

                ChangeStatus(true);
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
            }
        }

       

        private void Display_Phucap_Macdinh()
        {


            object id_chucvu = gvrex_Botri_Nhansu.GetFocusedRowCellValue("Id_Chucvu");
            ds_Phucap = objRexService.Rex_Nhansu_Get_Phucap_By_Idchucvu(id_chucvu).ToDataSet();

            ds_Phucap.Tables[0].Columns["Id_Dm_Phucap"].ReadOnly = false;
            ds_Phucap.Tables[0].Columns["Id_Dm_Phucap"].AllowDBNull = true;
            ds_Phucap.Tables[0].Columns["Id_Nhansu"].ReadOnly = false;
            ds_Phucap.Tables[0].Columns["Ngay_Batdau"].ReadOnly = false;
            ds_Phucap.Tables[0].Columns["Ngay_Ketthuc"].ReadOnly = false;
            ds_Phucap.Tables[0].Columns["Id_Phucap"].ReadOnly = false;


            dg_Phucap.DataSource = ds_Phucap.Tables[0];

            for (int i = 0; i < ds_Phucap.Tables[0].Rows.Count; i++)
            {

                ds_Phucap.Tables[0].Rows[i]["Id_Nhansu"] = Id_Nhansu;
                ds_Phucap.Tables[0].Rows[i]["Id_Phucap"] = gvPhucap.RowCount;
            }

            typeUpdate = 0;
        }
        #endregion

        private void btnThemhinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Tập tin hình ảnh (*.gif,*.jpg,*.jpeg,*.bmp,*.wmf,*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult != DialogResult.Cancel)
            {
                pictureHinh.ImageLocation = openFileDialog.FileName;
                lbl_pathImage.Text = openFileDialog.FileName;
            }
        }

        private void btnXoahinh_Click(object sender, EventArgs e)
        {
            pictureHinh.Image = null;
        }


        /// <summary>
        /// save noi dung cac tab
        /// </summary>
        /// <returns>true: khong co loi, false: co loi</returns>
        private bool UpdateObject()
        {
            //Xác nhận thao tác trên các lưới bằng cách click EndEdit
            dgKynang_Lamviec.EmbeddedNavigator.Buttons.DoClick(dgKynang_Lamviec.EmbeddedNavigator.Buttons.EndEdit);
            dgQuanhe_Giadinh.EmbeddedNavigator.Buttons.DoClick(dgQuanhe_Giadinh.EmbeddedNavigator.Buttons.EndEdit);
            dgQuatrinh_Congtac.EmbeddedNavigator.Buttons.DoClick(dgQuatrinh_Congtac.EmbeddedNavigator.Buttons.EndEdit);
            dgQuatrinh_Daotao.EmbeddedNavigator.Buttons.DoClick(dgQuatrinh_Daotao.EmbeddedNavigator.Buttons.EndEdit);
            dgrex_Botri_Nhansu.EmbeddedNavigator.Buttons.DoClick(dgrex_Botri_Nhansu.EmbeddedNavigator.Buttons.EndEdit);
            dgrex_Dienbien_Luong.EmbeddedNavigator.Buttons.DoClick(dgrex_Dienbien_Luong.EmbeddedNavigator.Buttons.EndEdit);
            dgrex_Khenthuong_Kyluat.EmbeddedNavigator.Buttons.DoClick(dgrex_Khenthuong_Kyluat.EmbeddedNavigator.Buttons.EndEdit);
            dg_Phucap.EmbeddedNavigator.Buttons.DoClick(dg_Phucap.EmbeddedNavigator.Buttons.EndEdit);
            dgrex_Thamgia_Tochuc.EmbeddedNavigator.Buttons.DoClick(dgrex_Thamgia_Tochuc.EmbeddedNavigator.Buttons.EndEdit);

            this.NhansuInfo_Update();
            bool error = false;
            if (ds_BotriNS.HasChanges())
            {
                error = !BotriNS_Update();
                if (error) return false;
            }
            if (ds_QuatrinhDT.HasChanges())
            {
                error = !QuatrinhDT_Update();
                if (error) return false;
            }
            if (ds_QuatrinhCT.HasChanges())
            {
                error = !QuatrinhCT_Update();
                if (error) return false;
            }
            if (ds_Ktkl.HasChanges())
            {
                error = !Ktkl_Update();
                if (error) return false;
            }
            if (ds_Kynang.HasChanges())
            {
                error = !Kynang_Update();
                if (error) return false;
            }
            if (ds_Quanhe_Giadinh.HasChanges())
            {
                error = !Quanhe_Giadinh_Update();
                if (error) return false;
            }
            if (ds_Dienbienluong.HasChanges())
            {
                error = !Dienbienluong_Update();
                if (error) return false;
            }
            if (ds_Phucap.HasChanges())
            {
                error = !Phucap_Update();
                if (error) return false;
            }
            if (ds_ThamgiaTochuc.HasChanges())
            {
                error = !ThamgiaTochuc_Update();
                if (error) return false;
            }

            Is_Save = true;

            return true;
        }

        private void Frmrex_Nhansu_Info_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Is_Save)
            {
                List<string> keys = new List<string>();
                if (ds_BotriNS.HasChanges())
                {
                    keys.Add(xtraTabPage_Botri_Nhansu.Text);
                }
                if (ds_QuatrinhDT.HasChanges())
                {
                    keys.Add(xtraTabPage5.Text);
                }
                if (ds_QuatrinhCT.HasChanges())
                {
                    keys.Add(xtraTabPage3.Text);
                }
                if (ds_Kynang.HasChanges())
                {
                    keys.Add(xtraTabPage_Kynang.Text);
                }
                if (ds_Ktkl.HasChanges())
                {
                    keys.Add(xtraTabPage6.Text);
                }
                if (ds_Quanhe_Giadinh.HasChanges())
                {
                    keys.Add(xtraTabPage7.Text);
                }
                if (ds_Dienbienluong.HasChanges())
                {
                    keys.Add(xtraTabPage8.Text);
                }
                if (ds_Phucap.HasChanges())
                {
                    keys.Add(xtraTabPage_Phucap.Text);
                }
                if (ds_ThamgiaTochuc.HasChanges())
                {
                    keys.Add(tab_ThamGiaToChuc.Text);
                }
                if (keys.Count > 0)
                {
                    string error_string = "";
                    if (keys.Count > 1)
                        error_string = ":\n- " + String.Join("\n- ", keys.ToArray()) + "\n";
                    else
                        error_string = String.Join("", keys.ToArray());
                    switch (GoobizFrame.Windows.Forms.UserMessage.Show("SYS_CONFIRM_BFCLOSE", new string[] { error_string }))
                    {
                        case DialogResult.Yes:
                            //save grid
                            if (!this.UpdateObject()) e.Cancel = true;
                            break;

                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
                }
            }

        }

        #region NhansuInfo

        private void NhansuInfo_Update()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();

            hashtableControls.Add(txtNamsinh, "Năm sinh");
            hashtableControls.Add(txtCMND, lblCMND.Text);
            hashtableControls.Add(dtNgaycap, lblNgaycap.Text);
            hashtableControls.Add(txtNoicap, lblNoicap.Text);
            hashtableControls.Add(dtNgay_Vaolam, lblNgay_Vaolam.Text);

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                return;

            //if ("" + cmbNgaysinh.Text != "" && "" + cmbThangsinh.Text != "" && "" + cmbNamsinh.Text != "")
            //    if (!ValidateDate(cmbNamsinh.EditValue, cmbThangsinh.EditValue, cmbNgaysinh.EditValue))
            //        return false;

            //thong tin nhan su - 3 tab dau
            nhan_su.Ma_Nhansu = txtMa_Nhansu.Text;
            nhan_su.Ho_Nhansu = txtHo_Nhansu.Text;
            nhan_su.Ten_Nhansu = txtTen_Nhansu.Text;

            //hình ....

            //nhan_su.Ngaysinh = txtNgaysinh.Text;
            nhan_su.Ngay_Sinh = cmbNgaysinh.EditValue;
            nhan_su.Thangsinh = cmbThangsinh.EditValue;
            nhan_su.Namsinh = txtNamsinh.EditValue;
            nhan_su.Noisinh = txtNoisinh.Text;
            nhan_su.Quequan = txtQuequan.Text;
            nhan_su.Cmnd = txtCMND.Text;
            nhan_su.Noicap = txtNoicap.Text;
            nhan_su.Gioitinh = chkGioitinh.EditValue;
            nhan_su.Id_Vanhoa = lookUp_Vanhoa.EditValue;
            nhan_su.Id_Tinhoc = lookUp_Tinhoc.EditValue;
            nhan_su.Id_Ngoaingu = lookUp_Ngoaingu.EditValue;
            nhan_su.Id_Chuyenmon = lookUp_Chuyenmon.EditValue;
            nhan_su.Id_Dantoc = lookUp_Dantoc.EditValue;
            nhan_su.Id_Honnhan = lookUp_Honnhan.EditValue;
            nhan_su.Id_Quocgia = lookUp_Quoctich.EditValue;
            nhan_su.Id_Tongiao = lookUp_Tongiao.EditValue;
            nhan_su.Id_Tpbanthan = lookUp_Tpbanthan.EditValue;
            nhan_su.Id_Tpgiadinh = lookUp_Tpgiadinh.EditValue;
            nhan_su.Diachi_Tamtru = txtDiachi_Tamtru.Text;
            nhan_su.Diachi_Thuongtru = txtDiachi_Thuongtru.Text;
            nhan_su.Dienthoai = txtDienthoai_Didong.Text;
            //nhan_su.Ngaycap = dtNgaycap.EditValue;
            nhan_su.Ngaycap = !string.IsNullOrEmpty(dtNgaycap.Text)
                                      ? dtNgaycap.EditValue
                                      : null;
            nhan_su.Hochieu = txtHochieu.Text;
            nhan_su.Noicap_Hochieu = txtNoicap_Hochieu.Text;
            //nhan_su.Ngaycap_Hochieu = dtNgaycap_Hochieu.EditValue;
            nhan_su.Ngaycap_Hochieu = !string.IsNullOrEmpty(dtNgaycap_Hochieu.Text)
                                       ? dtNgaycap_Hochieu.EditValue
                                       : null;


            //nhan_su.Ngay_Vaodoan = dtNgay_Vao_Doan.EditValue;
            nhan_su.Ngay_Vaodoan = !string.IsNullOrEmpty(dtNgay_Vao_Doan.Text)
                                      ? dtNgay_Vao_Doan.EditValue
                                      : null;

            nhan_su.Ngay_Vaodang = !string.IsNullOrEmpty(dtNgay_Vao_Dang.Text)
                                        ? dtNgay_Vao_Dang.EditValue
                                        : null;

            nhan_su.Noi_Vaodoan = txtNoi_Vao_Doan.Text;
            nhan_su.Noi_Vaodang = txtNoi_Vao_Dang.Text;
            nhan_su.Dienthoai_Nharieng = txtDienthoai_Nha.Text;
            nhan_su.Fax = txtFax.Text;
            nhan_su.Email = txtEmail.Text;
            //nhan_su.Ngay_Tuyendung = dtNgay_Tuyendung.EditValue;
            nhan_su.Ngay_Tuyendung = !string.IsNullOrEmpty(dtNgay_Tuyendung.Text)
                                    ? dtNgay_Tuyendung.EditValue
                                    : null;
            //nhan_su.Ngay_Vaolam = dtNgay_Vaolam.EditValue;
            nhan_su.Ngay_Vaolam = !string.IsNullOrEmpty(dtNgay_Vaolam.Text)
                                    ? dtNgay_Vaolam.EditValue
                                    : null;
            nhan_su.Id_Hopdong_Laodong = lookUp_Hopdong.EditValue;
            nhan_su.Id_Loai_Hopdong = lookUp_Loai_Hopdong.EditValue;
            //nhan_su.Id_Bophan = lookUp_Bophan.EditValue;
            //nhan_su.Id_Chucvu = lookUp_Chucvu.EditValue;
            //nhan_su.Ngay_Thoiviec = dtNgay_Thoiviec.EditValue;
            nhan_su.Ngay_Thoiviec = !string.IsNullOrEmpty(dtNgay_Thoiviec.Text)
                                   ? dtNgay_Thoiviec.EditValue
                                   : null;
            nhan_su.Nghi_Bhxh = chkNghi_Bhxh.Checked;

            nhan_su.Tenkhac = txtTen_Khac.Text;
            nhan_su.Tt_Suckhoe = txtTinhtrang_Suckhoe.Text;
            nhan_su.Chieucao = txtChieucao.Text;
            nhan_su.Cannang = txtCannang.Text;
            nhan_su.Nhommau = txtNhommau.Text;
            nhan_su.Thuongbinh_Hang = txtThuongbinh.Text;
            nhan_su.Con_Chinh_Sach = txtCon_Giadinh_Chinhsach.Text;
            //nhan_su.Ngayvaodang_Chinhthuc = dtNgay_Vao_Dang_Chinhthuc.EditValue;
            nhan_su.Ngayvaodang_Chinhthuc = !string.IsNullOrEmpty(dtNgay_Vao_Dang_Chinhthuc.Text)
                                        ? dtNgay_Vao_Dang_Chinhthuc.EditValue
                                        : null;
            //nhan_su.Ngay_Nhapngu = dtNgay_Nhapngu.EditValue;
            nhan_su.Ngay_Nhapngu = !string.IsNullOrEmpty(dtNgay_Nhapngu.Text)
                                    ? dtNgay_Nhapngu.EditValue
                                    : null;
            //nhan_su.Ngay_Xuatngu = dtNgay_Xuatngu.EditValue;
            nhan_su.Ngay_Xuatngu = !string.IsNullOrEmpty(dtNgay_Xuatngu.Text)
                                    ? dtNgay_Xuatngu.EditValue
                                    : null;
            nhan_su.Quan_Ham = txtQuanham.Text;
            nhan_su.Danhhieu_Caonhat = txtDanhhieu_Caonhat.Text;
            nhan_su.Lyluan_Chinhtri = txtLyluan_Chinhtri.Text;
            nhan_su.Quanly_Nhanuoc = txtQuanly_Nhanuoc.Text;
            nhan_su.Bibat_Bitu = memoKhairo.Text;
            nhan_su.Thamgia_Chinhtri = memoThamgia.Text;
            nhan_su.Thannhan_Nuocngoai = memoThannhan.Text;
            nhan_su.Nghenghiep_Tuyendung = txtNghenghiep_Tuyendung.Text;
            nhan_su.Coquan_Tuyendung = txtCoquan_Tuyendung.Text;
            nhan_su.Congviec_Chinh = txtCongviec_Chinh.Text;
            nhan_su.Sotruong_Ct = txtSotruong_Congtac.Text;
            nhan_su.So_Sobhxh = txtSo_Bhxh.Text;
            nhan_su.Id_Loai_Nhanvien = lookUp_Loai_Nhanvien.EditValue;
            //Thông tin bảo hiểm

            //nhan_su.Ngaycap_Bhxh = dtNgaycap_So_Bhxh.EditValue;
            nhan_su.Ngaycap_Bhxh = !string.IsNullOrEmpty(dtNgaycap_So_Bhxh.Text)
                                  ? dtNgaycap_So_Bhxh.EditValue
                                  : null;
            nhan_su.Noicap_Bhxh = txtNoicap_So_Bhxh.EditValue;
            nhan_su.So_Sobhyt = txtSo_Bhyt.EditValue;
            nhan_su.Noicap_Bhyt = txtNoicap_So_Bhyt.EditValue;
            //nhan_su.Ngaycap_Bhyt = dtNgaycap_So_Bhyt.EditValue;
            nhan_su.Ngaycap_Bhyt = !string.IsNullOrEmpty(dtNgaycap_So_Bhyt.Text)
                                ? dtNgaycap_So_Bhyt.EditValue
                                : null;

            nhan_su.So_Sobhtn = txtSo_Bhtn.EditValue;
            //nhan_su.Ngaycap_Bhtn = dtNgaycap_So_Bhtn.EditValue;
            nhan_su.Ngaycap_Bhtn = !string.IsNullOrEmpty(dtNgaycap_So_Bhtn.Text)
                                  ? dtNgaycap_So_Bhtn.EditValue
                                  : null;
            nhan_su.Noicap_Bhtn = txtNoicap_So_Bhtn.EditValue;
            nhan_su.Tuoihuu = txtTuoihuu.EditValue;
            if (pictureHinh.Image != null)
            {
                //get image source and resize it
                Image srcImage = pictureHinh.Image;

                int percentSize = (srcImage.Width > 100) ? 100 * 100 / srcImage.Width : 100;
                Image hinh = GoobizFrame.Windows.ImageUtils.ImageResize.ScaleByPercent(srcImage, percentSize);
                //save image to memory
                MemoryStream ms = new MemoryStream();
                hinh.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageData = ms.GetBuffer();
                //asign image in buffer to property Hinh
                nhan_su.Hinh = imageData;
            }
            else
            {
                nhan_su.Hinh = null;
            }

            objRexService.Update_Rex_NhansuInfo(nhan_su);

            //end thông tin nhân sự - 3 tab đầu
        }
        #endregion

        #region BotriNS - tab4

        private bool BotriNS_Update()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gvrex_Botri_Nhansu.Columns["Id_Bophan"], "");
            hashtableControls.Add(gvrex_Botri_Nhansu.Columns["Id_Chucvu"], "");
            //hashtableControls.Add(gridView1.Columns["Id_Quyetdinh"], "");
            hashtableControls.Add(gvrex_Botri_Nhansu.Columns["Ngay_Batdau"], "");
            //hashtableControls.Add(gridView1.Columns["Ngay_Ketthuc"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gvrex_Botri_Nhansu))
                return false;
            // if (!GoobizFrame.Windows.MdiUtils.Validator.CheckDateGrid(gridView1.Columns["Ngay_Batdau"],gridView1.Columns["Ngay_Ketthuc"], gridView1))

            //return false;
            try
            {
                this.DoClickEndEdit(dgrex_Botri_Nhansu);//dgrex_Botri_Nhansu.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                objRexService.Update_Rex_Botri_Nhansu_Collection(ds_BotriNS);
            }
            catch (Exception ex)
            {

                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Lỗi cập nhật");

            }
            //BotriNS_Display();
            return true;
        }

        //deonguyen
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvrex_Botri_Nhansu.SetFocusedRowCellValue(gvrex_Botri_Nhansu.Columns["Id_Nhansu"], Id_Nhansu);
        }

        //deonguyen - 15/09/2010
        private void dgrex_Botri_Nhansu_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Append)
            {
                if (gvrex_Botri_Nhansu.RowCount > 0)
                {
                    if ("" + gvrex_Botri_Nhansu.GetRowCellValue(gvrex_Botri_Nhansu.RowCount - 1, gvrex_Botri_Nhansu.Columns["Ngay_Ketthuc"]) != "")
                    {
                        DateTime dtNgay_Ketthuc = Convert.ToDateTime(gvrex_Botri_Nhansu.GetRowCellValue(gvrex_Botri_Nhansu.RowCount - 1, gvrex_Botri_Nhansu.Columns["Ngay_Ketthuc"]));
                        //Ngày bố trí kế tiếp là ngày kết thúc kề trước cộng thêm một ngày.
                        gridDate_Ngay_Batdau.MinValue = dtNgay_Ketthuc.AddDays(1);
                        //Kiểm tra ngày kết thúc bố trí với ngày hiện tại.
                        if (dtNgay_Ketthuc.CompareTo(DateTime.Today) >= 0)
                        {
                            GoobizFrame.Windows.Forms.MessageDialog.Show("Nhân sự đang được bố trí, không thể bố trí thêm.");
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        GoobizFrame.Windows.Forms.MessageDialog.Show("Nhân sự đang được bố trí, không thể bố trí thêm.");
                        e.Handled = true;
                    }
                }
            }
            else

                if (e.Button.ButtonType == NavigatorButtonType.Remove)
                {
                    if (gvrex_Botri_Nhansu.RowCount <= 1)
                    {
                        GoobizFrame.Windows.Forms.MessageDialog.Show("Không thể xóa, nhân sự phải được bố trí vào bộ phận.");
                        e.Handled = true;
                    }
                }
        }
        #endregion

        #region Thamgia_Tochuc

        private bool ThamgiaTochuc_Update()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gridView8.Columns["Id_Tochuc"], "");
            //hashtableControls.Add(gridView8.Columns["Id_Chucvu"], "");
            hashtableControls.Add(gridView8.Columns["Tu_Nam"], "");
            //hashtableControls.Add(gridView8.Columns["Den_Nam"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gridView8))
                return false;

            if (!Check_From_Year_To_Year("Tu_Nam", "Den_Nam", gridView8))
                return false;

            try
            {
                this.DoClickEndEdit(dgrex_Thamgia_Tochuc);//dgrex_Thamgia_Tochuc.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                objRexService.Update_Rex_Thamgia_Tochuc_Collection(ds_ThamgiaTochuc);
            }
            catch (Exception ex)
            {

                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Lỗi cập nhật");

            }
            //ThamgiaTochuc_Display();
            return true;
        }
        private void gridView8_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView8.SetFocusedRowCellValue(gridView8.Columns["Id_Nhansu"], Id_Nhansu);
        }
        #endregion

        #region Phucap
        private void DisplayInfo_Phucap()
        {
            dg_Phucap.DataSource = null;
            ds_Phucap = objRexService.Rex_Nhansu_Get_Phucap_By_Idnhansu(Id_Nhansu).ToDataSet();
            ds_Phucap_old = ds_Phucap.Copy();
            if (ds_Phucap.Tables[0].Rows.Count > 0)
            {
                dg_Phucap.DataSource = ds_Phucap.Tables[0];
                gvPhucap.SetFocusedRowCellValue(gvPhucap.Columns["Id_Nhansu"], Id_Nhansu);
                typeUpdate = 1;
            }
            //else if ("" + gvrex_Botri_Nhansu.GetFocusedRowCellValue("Id_Chucvu") != "")
            //{
            //    object id_chucvu = gvrex_Botri_Nhansu.GetFocusedRowCellValue("Id_Chucvu");
            //    ds_Phucap = objRexService.Rex_Nhansu_Get_Phucap_By_Idchucvu(id_chucvu);
            //    ds_Phucap.Tables[0].Columns["Id_Dm_Phucap"].ReadOnly = false;
            //    ds_Phucap.Tables[0].Columns["Id_Dm_Phucap"].AllowDBNull = true;
            //    ds_Phucap.Tables[0].Columns["Id_Nhansu"].ReadOnly = false;
            //    ds_Phucap.Tables[0].Columns["Ngay_Batdau"].ReadOnly = false;
            //    ds_Phucap.Tables[0].Columns["Ngay_Ketthuc"].ReadOnly = false;
            //    ds_Phucap.Tables[0].Columns["Id_Phucap"].ReadOnly = false;
            //    dg_Phucap.DataSource = ds_Phucap.Tables[0];
            //    for (int i = 0; i < ds_Phucap.Tables[0].Rows.Count; i++)
            //    {
            //        ds_Phucap.Tables[0].Rows[i]["Id_Nhansu"] = Id_Nhansu;
            //        ds_Phucap.Tables[0].Rows[i]["Id_Phucap"] = gvPhucap.RowCount;
            //    }

            //    typeUpdate = 0;

            //}
        }

        private bool Phucap_Update()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gvPhucap.Columns["Id_Dm_Phucap"], "");
            hashtableControls.Add(gvPhucap.Columns["Ngay_Batdau"], "");
            // hashtableControls.Add(gvPhucap.Columns["Ngay_Ketthuc"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gvPhucap))
            {
                xtraTabPage_Phucap.Select();
                gvPhucap.Focus();
                return false;
            }

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckDateGrid(gvPhucap.Columns["Ngay_Batdau"], gvPhucap.Columns["Ngay_Ketthuc"], gvPhucap))
                return false;

            try
            {

                if (typeUpdate == 1)
                {
                    this.DoClickEndEdit(dg_Phucap);
                    objRexService.Update_Rex_Phucap_Collection(ds_Phucap);
                }
                else
                {
                    this.DoClickEndEdit(dg_Phucap);
                    if (ds_Phucap_old.Tables[0].Rows.Count > 0)
                        objRexService.Delete_Rex_Phucap_Collection(ds_Phucap_old);
                    objRexService.Insert_Rex_Phucap_Collection(ds_Phucap);
                }

            }
            catch (Exception ex)
            {

                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Lỗi cập nhật");

            }
            //Phucap_Display();
            return true;
        }


        private void dg_Phucap_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {

        }

        private void gvPhucap_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //nếu chọn ngay row phụ cấp chung, hoạc phụ cấp con thì không cho edit
            if (("" + gvPhucap.GetFocusedRowCellValue("Phucap_Chung") != "" 
                && Convert.ToBoolean(gvPhucap.GetFocusedRowCellValue("Phucap_Chung"))) 
                || "" + gvPhucap.GetFocusedRowCellValue("Id_Phucap_Con") != "")
            {
                GridLookUpEdit_Phucap.ReadOnly = true;
                gridColumn23.OptionsColumn.ReadOnly = true;

            }
            else
            {
                GridLookUpEdit_Phucap.ReadOnly = false;

                gridColumn23.OptionsColumn.ReadOnly = false;
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Id_Dm_Phucap")
            {
                DataRow[] dtr = objMasterService.Get_All_Rex_Dm_Phucap_Collection().ToDataSet().
                       Tables[0].Select("Id_Dm_Phucap = " + gvPhucap.GetFocusedRowCellValue(gvPhucap.Columns["Id_Dm_Phucap"]).ToString());
                gvPhucap.SetFocusedRowCellValue(gvPhucap.Columns["Sotien"], dtr[0]["Luong_Phucap"]);
                //  gvPhucap.SetFocusedRowCellValue(gvPhucap.Columns["Heso_Phucap"], dtr[0]["Heso_Phucap"]);
            }
        }

        private void repositoryItemTextEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString() == "" || e.NewValue.ToString() == "0")
                e.Cancel = true;
            if (e.NewValue != null)
                if (e.NewValue.ToString().Length > 10)
                    e.Cancel = true;
        }


        #endregion

        #region Dienbienluong

        private void bandedGridView1_ShownEditor(object sender, EventArgs e)
        {
            //try
            //{
            //    if (gvrex_Dienbien_Luong.GetFocusedRowCellValue(gvrex_Dienbien_Luong.Columns["Id_Ngachluong"]).ToString() != "")
            //    {
            //        GridView gridView = (GridView)sender;
            //        LookUpEdit lookup = gridView.ActiveEditor as LookUpEdit;
            //        if (gridView.FocusedColumn.FieldName == "Id_Bacluong" && lookup != null)
            //        {
            //            DataTable lookupDataTable = objMasterService.Get_All_Rex_Dm_Bacluong_Collection().Tables[0];
            //            filteredDataView = new DataView(lookupDataTable);
            //            lookup.Properties.DataSource = filteredDataView;

            //            filteredDataView.RowFilter = "Id_Ngachluong = " + gvrex_Dienbien_Luong.GetFocusedRowCellValue(gvrex_Dienbien_Luong.Columns["Id_Ngachluong"]).ToString();

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void bandedGridView1_HiddenEditor(object sender, EventArgs e)
        {
            if (filteredDataView != null)
            {
                filteredDataView.Dispose();
                filteredDataView = null;
            }
        }

        private bool Dienbienluong_Update()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gvrex_Dienbien_Luong.Columns["Id_Bacluong"], "");
            //hashtableControls.Add(bandedGridView1.Columns["Bhxh"], "");
            hashtableControls.Add(gvrex_Dienbien_Luong.Columns["Id_Quyetdinh"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gvrex_Dienbien_Luong))
                return false;

            try
            {
                this.DoClickEndEdit(dgrex_Dienbien_Luong);//dgrex_Dienbien_Luong.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                //objRexService.Update_Rex_Dienbien_Luong_Collection(ds_Dienbienluong);
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Lỗi cập nhật");
            }
            //Dienbienluong_Display();
            return true;
        }
        private void bandedGridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvrex_Dienbien_Luong.SetFocusedRowCellValue(gvrex_Dienbien_Luong.Columns["Id_Nhansu"], Id_Nhansu);
        }
        private void bandedGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "Id_Bacluong":
                    if ("" + gvrex_Dienbien_Luong.GetFocusedRowCellValue("Id_Bacluong") != "")
                    {
                        gvrex_Dienbien_Luong.SetFocusedRowCellValue("Id_Ngachluong",
                            ((DataRowView)gridLookUp_Bacluong.GetDataSourceRowByKeyValue(e.Value))["Id_Ngachluong"]);
                        gvrex_Dienbien_Luong.SetFocusedRowCellValue("Heso",
                            ((DataRowView)gridLookUp_Bacluong.GetDataSourceRowByKeyValue(e.Value))["Heso"]);
                        gvrex_Dienbien_Luong.SetFocusedRowCellValue("Luong_Thoathuan",
                           ((DataRowView)gridLookUp_Bacluong.GetDataSourceRowByKeyValue(e.Value))["Luong_Thoathuan"]);
                    }
                    break;

                case "Id_Quyetdinh":
                    var dtr = gridLookUp_Quyetdinh.GetDataSourceRowByKeyValue(e.Value) as DataRowView;
                    gvrex_Dienbien_Luong.SetFocusedRowCellValue("Ngayky", dtr["Ngayky"]);
                    gvrex_Dienbien_Luong.SetFocusedRowCellValue("Ngay_Hieuluc_Batdau", ("" + dtr["Ngay_Batdau"] != "") ? dtr["Ngay_Batdau"] : null);
                    gvrex_Dienbien_Luong.SetFocusedRowCellValue("Ngay_Hieuluc_Ketthuc", ("" + dtr["Ngay_ketthuc"] != "") ? dtr["Ngay_ketthuc"] : null);
                    break;
            }
        }
        #endregion

        #region Quanhe_Giadinh

        private bool Quanhe_Giadinh_Update()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gvQuanhe_Giadinh.Columns["Id_Loai_Quanhe_Giadinh"], "");
            hashtableControls.Add(gvQuanhe_Giadinh.Columns["Ho_Ten"], "");
            hashtableControls.Add(gvQuanhe_Giadinh.Columns["Namsinh"], "");
            hashtableControls.Add(gvQuanhe_Giadinh.Columns["Nghe_Nghiep"], "");
            hashtableControls.Add(gvQuanhe_Giadinh.Columns["Diachi_Thuongtru"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gvQuanhe_Giadinh))
                return false;

            try
            {
                this.DoClickEndEdit(dgQuanhe_Giadinh);//dgQuanhe_Giadinh.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                objRexService.Update_Rex_Quanhe_Giadinh_Collection(ds_Quanhe_Giadinh);
            }
            catch (Exception ex)
            {

                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Lỗi cập nhật");

            }
            //Quanhe_Giadinh_Display();
            return true;
        }
        private void gridView5_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvQuanhe_Giadinh.SetFocusedRowCellValue(gvQuanhe_Giadinh.Columns["Id_Nhansu"], Id_Nhansu);
        }

        #endregion

        #region Ktkl

        private bool Ktkl_Update()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gridView3.Columns["Id_Loai_Ktkl"], "");
            hashtableControls.Add(gridView3.Columns["Nam_Ktkl"], "");
            hashtableControls.Add(gridView3.Columns["Hinhthuc_Ktkl"], "");
            hashtableControls.Add(gridView3.Columns["Id_Coquan"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gridView3))
                return false;

            try
            {
                this.DoClickEndEdit(dgrex_Khenthuong_Kyluat);// dgrex_Khenthuong_Kyluat.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                objRexService.Update_Rex_Khenthuong_Kyluat_Collection(ds_Ktkl);
            }
            catch (Exception ex)
            {

                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Lỗi cập nhật");

            }
            //Ktkl_Display();
            return true;
        }
        private void gridView3_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView3.SetFocusedRowCellValue(gridView3.Columns["Id_Nhansu"], Id_Nhansu);
        }

        #endregion

        #region Ky nang

        private bool Kynang_Update()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gridView7.Columns["Id_Kynang_Chuyenmon"], "");
            hashtableControls.Add(gridView7.Columns["Trinhdo"], "");
            hashtableControls.Add(gridView7.Columns["Sonam_Sudung"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gridView7))
                return false;

            try
            {
                this.DoClickEndEdit(dgKynang_Lamviec);//dgKynang_Lamviec.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                objRexService.Update_Rex_Kynang_Collection(ds_Kynang);
            }
            catch (Exception ex)
            {

                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Lỗi cập nhật");

            }
            //Kynang_Display();
            return true;
        }
        private void gridView7_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView7.SetFocusedRowCellValue(gridView7.Columns["Id_Nhansu"], Id_Nhansu);
        }
        #endregion

        #region QuatrinhCT

        private bool QuatrinhCT_Update()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gridView6.Columns["Tu_Nam"], "");
            hashtableControls.Add(gridView6.Columns["Den_Nam"], "");
            hashtableControls.Add(gridView6.Columns["Congviec"], "");
            hashtableControls.Add(gridView6.Columns["Id_Chucvu"], "");
            hashtableControls.Add(gridView6.Columns["Id_Coquan"], "");
            hashtableControls.Add(gridView6.Columns["Bophan"], "");
            hashtableControls.Add(gridView6.Columns["Diachi"], "");
            hashtableControls.Add(gridView6.Columns["Cvchinh_Thanhtichnoibac"], "");
            hashtableControls.Add(gridView6.Columns["Luong_Khinghiviec"], "");
            hashtableControls.Add(gridView6.Columns["Lydo_Nghiviec"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gridView6))
                return false;
            if (!Check_From_Year_To_Year("Tu_Nam", "Den_Nam", gridView6))
                return false;
            try
            {
                this.DoClickEndEdit(dgQuatrinh_Congtac);// dgQuatrinh_Congtac.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                objRexService.Update_Rex_Quatrinh_Congtac_Collection(ds_QuatrinhCT);
            }
            catch (Exception ex)
            {

                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Lỗi cập nhật");

            }
            // QuatrinhCT_Display();
            return true;
        }
        private void gridView6_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView6.SetFocusedRowCellValue(gridView6.Columns["Id_Nhansu"], Id_Nhansu);
        }
        #endregion

        #region QuatrinhDT
        private bool QuatrinhDT_Update()
        {
            GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
            hashtableControls.Add(gridView4.Columns["Nam_Nhaphoc"], "");
            hashtableControls.Add(gridView4.Columns["Nam_Totnghiep"], "");
            hashtableControls.Add(gridView4.Columns["Noi_Daotao"], "");
            hashtableControls.Add(gridView4.Columns["Id_Chuyenmon"], "");
            hashtableControls.Add(gridView4.Columns["Loaihinh_Daotao"], "");
            hashtableControls.Add(gridView4.Columns["Id_Chungchi"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gridView4))
                return false;
            if (!Check_From_Year_To_Year("Nam_Nhaphoc", "Nam_Totnghiep", gridView4))
                return false;
            try
            {
                this.DoClickEndEdit(dgQuatrinh_Daotao);//dgQuatrinh_Daotao.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                objRexService.Update_Rex_Quatrinh_Daotao_Collection(ds_QuatrinhDT);
            }
            catch (Exception ex)
            {

                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Lỗi cập nhật");

            }
            //QuatrinhDT_Display();
            return true;
        }
        private void gridView4_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView4.SetFocusedRowCellValue(gridView4.Columns["Id_Nhansu"], Id_Nhansu);
        }
        #endregion

        public bool Check_From_Year_To_Year(string from_year_colum, string to_year_colum, GridView grid_view)
        {
            for (int i = 0; i < grid_view.RowCount; i++)
            {
                if (""+grid_view.GetRowCellValue(i, grid_view.Columns[to_year_colum])!="")
                if (Convert.ToDateTime(grid_view.GetRowCellValue(i, grid_view.Columns[from_year_colum])).Year 
                    > Convert.ToDateTime(grid_view.GetRowCellValue(i, grid_view.Columns[to_year_colum])).Year)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("Msg00036", new string[] { grid_view.Columns[from_year_colum].Caption, grid_view.Columns[to_year_colum].Caption });
                    return false;
                }
            }
            return true;
        }

        private void GridLookUpEdit_Heso_Popup(object sender, EventArgs e)
        {
            GridLookUpEdit_Heso.BestFit();
        }

        private void xtraHNavigator3_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            try
            {
                if (e.Button.ButtonType == NavigatorButtonType.Remove)
                {
                    object a = gvPhucap.GetFocusedRowCellValue("Phucap_Chung");
                    if (Convert.ToBoolean(gvPhucap.GetFocusedRowCellValue("Phucap_Chung")) == true)
                    {
                        GoobizFrame.Windows.Forms.UserMessage.Show("Msg00117", new string[] { "." });
                        e.Handled = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            try
            {
                //if (xtraTabControl1.SelectedTabPage == xtraTabPage_Phucap)
                //    if ("" + gridView1.GetFocusedRowCellValue("Id_Chucvu") != "" && this.FormState == GoobizFrame.Windows.Forms.FormState.Edit)
                //    {
                //        this.DisplayInfo_Phucap();
                //    }
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
            }
        }

        private void xtraHNavigator2_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Append)
            {
                if (gvrex_Botri_Nhansu.RowCount > 0)
                {
                    if ("" + gvrex_Botri_Nhansu.GetRowCellValue(gvrex_Botri_Nhansu.RowCount - 1, gvrex_Botri_Nhansu.Columns["Ngay_Ketthuc"]) != "")
                    {
                        DateTime dtNgay_Ketthuc = Convert.ToDateTime(gvrex_Botri_Nhansu.GetRowCellValue(gvrex_Botri_Nhansu.RowCount - 1, gvrex_Botri_Nhansu.Columns["Ngay_Ketthuc"]));
                        //Ngày bố trí kế tiếp là ngày kết thúc kề trước cộng thêm một ngày.
                        //gridDate_Ngay_Batdau.MinValue = dtNgay_Ketthuc.AddDays(1);
                        //Kiểm tra ngày kết thúc bố trí với ngày hiện tại.
                        //if (dtNgay_Ketthuc.CompareTo(DateTime.Today) >= 0)
                        //{
                        //    GoobizFrame.Windows.Forms.MessageDialog.Show("Nhân sự đang được bố trí, không thể bố trí thêm.");
                        //    e.Handled = true;
                        //}
                    }
                    else
                    {
                        GoobizFrame.Windows.Forms.MessageDialog.Show("Nhân sự đang được bố trí, không thể bố trí thêm.");
                        e.Handled = true;
                    }
                }
            }
            else

                if (e.Button.ButtonType == NavigatorButtonType.Remove)
                {
                    if (gvrex_Botri_Nhansu.RowCount <= 1)
                    {
                        GoobizFrame.Windows.Forms.MessageDialog.Show("Không thể xóa, nhân sự phải được bố trí vào bộ phận.");
                        e.Handled = true;
                    }
                }
        }

       

        private void gvPhucap_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvPhucap.SetFocusedRowCellValue(gvPhucap.Columns["Id_Nhansu"], Id_Nhansu);
            gvPhucap.SetFocusedRowCellValue(gvPhucap.Columns["Id_Botri_Nhansu"], gvrex_Botri_Nhansu.GetFocusedRowCellValue("Id_Botri_Nhansu"));
            gvPhucap.SetFocusedRowCellValue(gvPhucap.Columns["Ngay_Batdau"], gvrex_Botri_Nhansu.GetFocusedRowCellValue("Ngay_Batdau"));
            gvPhucap.SetFocusedRowCellValue(gvPhucap.Columns["Ngay_Ketthuc"], gvrex_Botri_Nhansu.GetFocusedRowCellValue("Ngay_Ketthuc"));
            ds_Phucap.Tables[0].Columns["Sotien"].ReadOnly = false;

        }

        private void gridLookUp_Loai_Ktkl_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {

                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                    "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Loai_Ktkl_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Loai_Ktkl").GetValue(dialog, null)
                   as Ecm.WebReferences.MasterService.Rex_Dm_Loai_Ktkl;

                if (SelectedObject != null)
                {
                    DataSet ds_Loai_Ktkl = new DataSet();
                    ds_Loai_Ktkl = objMasterService.Get_All_Rex_Dm_Loai_Ktkl_Collection().ToDataSet();
                    gridLookUp_Loai_Ktkl.DataSource = ds_Loai_Ktkl.Tables[0];
                    gridView3.SetFocusedRowCellValue(gridView3.Columns["Id_Loai_Ktkl"], SelectedObject.Id_Loai_Ktkl);

                }
            }
        }

        /// <summary>
        /// chỉ load ds phụ cấp riêng vào gridlookupedt dm phụ cấp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPhucap_ShownEditor(object sender, EventArgs e)
        {
            if (!GridLookUpEdit_Phucap.ReadOnly)
            {
                GridView gridView = (GridView)sender;
                LookUpEdit lookup = gridView.ActiveEditor as DevExpress.XtraEditors.LookUpEdit;
                //if ("" + gvPhucap.GetFocusedRowCellValue("Phucap_Chung") == "" || !Convert.ToBoolean(gvPhucap.GetFocusedRowCellValue("Phucap_Chung")))

                if (gridView.FocusedColumn.FieldName == "Id_Dm_Phucap" && lookup != null)
                {
                    DataTable lookupDataTable = objMasterService.Get_All_Rex_Dm_Phucap_Collection().ToDataSet().Tables[0];
                    DataView filteredDataView = new DataView(lookupDataTable);
                    lookup.Properties.DataSource = filteredDataView;

                }
            }
        }

        private void gridLookUp_Bacluong_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                   "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Bacluong_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("SelectedRex_Dm_Bacluong").GetValue(dialog, null)
                    as Ecm.WebReferences.MasterService.Rex_Dm_Bacluong;


                var dsBacluong = objMasterService.Get_All_Rex_Dm_Bacluong_Collection().ToDataSet();
                gridLookUp_Bacluong.DataSource = dsBacluong.Tables[0];
                if (SelectedObject != null)
                {
                    gvrex_Dienbien_Luong.SetFocusedRowCellValue("Id_Bacluong", SelectedObject.Id_Bacluong);
                }
            }
        }

        private void GridLookUpEdit_Phucap_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                       "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Phucap_Add", this);

                var dsDm_Phucap_Add = objMasterService.Get_All_Rex_Dm_Phucap_Collection().ToDataSet();
                GridLookUpEdit_Phucap.DataSource = dsDm_Phucap_Add.Tables[0];

                gvPhucap.ShowEditor();
            }
        }

    }

}



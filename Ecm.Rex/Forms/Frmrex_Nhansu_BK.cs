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
using OfficeOpenXml;

namespace Ecm.Rex.Forms
{
    public partial class Frmrex_Nhansu_BK : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        public Ecm.WebReferences.Classes.RexService objRexService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.RexService>();
        DataSet dsNhansu = new DataSet();
        DataSet ds_Bophan;
        DataSet ds_BotriNS = new DataSet();
        DataSet ds_Phucap = new DataSet();
        DataSet ds_Phucap_old = new DataSet();
        DataSet ds_Quanhe_Giadinh = new DataSet();
        DataSet ds_Dienbienluong = new DataSet();
        private DataView filteredDataView;//dung cho tab Dien bien luong, chon ngach luong -> bac luong

        int typeUpdate; 
        public int[] id_nhansu_chon;
        object Id_Bophan;
        object Id_Nhansu;
        Ecm.WebReferences.RexService.Rex_Nhansu nhan_su;
        DevExpress.XtraTreeList.Nodes.TreeListNode focusedNode;

        Ecm.WebReferences.RexService.Rex_Botri_Nhansu Rex_Botri_Nhansu = null;
        
        Frmrex_Nhansu_Import frmrex_Nhansu_Import;

        public string XlsTemplate = @"\Resources\xls\rex_chamcong_thang.xlsx";
        public string LastXlsPath = "";
        DataSet dsExcelExp = new DataSet();
        DataSet dsHesochuongtrinh_Company = new DataSet();
        int col = 1;
        int row = 9;

        public Frmrex_Nhansu_BK()
        {
            InitializeComponent();

            dtNgay_Vaolam.Properties.MinValue = new DateTime(1975, 01, 01);
            dtNgaycap.Properties.MinValue = new DateTime(1950, 01, 01);

            for (int i = 1900; i <= DateTime.Today.Year; i++)
                cmbNamsinh.Properties.Items.Add(i);

            this.DisplayInfo();
        }

        void Luongtonghop_Init()
        {
            try
            {
                //tinh luong
                objRexService.Rex_Luong_Tonghop_Init_ByBophan(DateTime.Now.Year, DateTime.Now.Month, Id_Bophan);
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
            }
        }

        #region Override Section

        public override void DisplayInfo()
        {
            try
            {
                GoobizFrame.Windows.PlugIn.RightHelpers.CheckUserRightAction(this);
                dsHesochuongtrinh_Company = objMasterService.Get_Rex_Dm_Heso_Chuongtrinh_By_Nhomheso("Company").ToDataSet();

                //Get data master table REX
                lookUp_Dantoc.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Dantoc_Collection().ToDataSet().Tables[0];
                lookUp_Honnhan.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Honnhan_Collection().ToDataSet().Tables[0];
                lookUp_Quoctich.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Quocgia_Collection().ToDataSet().Tables[0];
                lookUp_Tongiao.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Tongiao_Collection().ToDataSet().Tables[0];
                lookUp_Tpbanthan.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Tpbanthan_Collection().ToDataSet().Tables[0];
                lookUp_Tpgiadinh.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Tpgiadinh_Collection().ToDataSet().Tables[0];
                lookUp_Vanhoa.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Vanhoa_Collection().ToDataSet().Tables[0];
                lookUp_Chuyenmon.Properties.DataSource = objMasterService.Get_All_Rex_Dm_Chuyenmon_Collection().ToDataSet().Tables[0];
                lookUp_NganHang.Properties.DataSource = objMasterService.Get_Acc_Dm_Nganhang_Collection3().ToDataSet().Tables[0];

                //Get data Rex_Nhansu
                //dsNhansu.Clear();
                //dsNhansu = objRexService.Get_All_Rex_Nhansu_Collection();
                //dgrex_Nhansu.DataSource = dsNhansu;
                //dgrex_Nhansu.DataMember = dsNhansu.Tables[0].TableName;

                //TreeList
                ds_Bophan = objMasterService.Get_All_Rex_Dm_Bophan_Collection().ToDataSet();
                treeListColumn1.TreeList.DataSource = ds_Bophan;
                treeListColumn1.TreeList.DataMember = ds_Bophan.Tables[0].TableName;
                gridLookUpEdit_Bophan.DataSource = ds_Bophan.Tables[0];
                gridLookUp_Bophan_Botri.DataSource = ds_Bophan.Tables[0];

                DataSet dsQuyetdinh = objMasterService.Get_All_Rex_Dm_Quyetdinh_Collection().ToDataSet();
                gridLookUp_Chucvu_Botri.DataSource = objMasterService.Get_All_Rex_Dm_Chucvu_Collection().ToDataSet().Tables[0];
                gridLookUp_Quyetdinh_Botri.DataSource = dsQuyetdinh.Tables[0];

                DataSet ds_Dm_Phucap = new DataSet();
                ds_Dm_Phucap = objMasterService.Get_All_Rex_Dm_Phucap_Collection().ToDataSet();
                GridLookUpEdit_Phucap.DataSource = ds_Dm_Phucap.Tables[0];
                GridLookUpEdit_Heso.DataSource = ds_Dm_Phucap.Tables[0];

                DataSet ds_NgachLuong = new DataSet();
                ds_NgachLuong = objMasterService.Get_All_Rex_Dm_Ngachluong_Collection().ToDataSet();
                gridLookUp_Ngachluong.DataSource = ds_NgachLuong.Tables[0];


                var ds_BacLuong = objMasterService.Get_All_Rex_Dm_Bacluong_Collection().ToDataSet();
                gridLookUp_Bacluong.DataSource = ds_BacLuong.Tables[0];

                //DataSet ds_Quyetdinh = new DataSet();
                gridLookUp_Quyetdinh.DataSource = dsQuyetdinh.Tables[0];

                DataSet ds_Dm_Quanhe_Giadinh = new DataSet();
                ds_Dm_Quanhe_Giadinh = objMasterService.Get_All_Rex_Dm_Quanhe_Giadinh_Collection().ToDataSet();
                gridLookUp_Quanhe_Giadinh.DataSource = ds_Dm_Quanhe_Giadinh.Tables[0];

                gridLookUpEdit_Loai_Hopdong_2.DataSource = objMasterService.Get_All_Rex_Dm_Loai_Hopdong_Collection().ToDataSet().Tables[0];

                //this.DataBindingControl();
                this.ChangeStatus(false);
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }
        }

        
        public override void ClearDataBindings()
        {
           
            try
            {
                /** thong tin chung
                Id_Nhansu            
                Hinh                                                                                                                                                                                                                                                             
                Ma_Nhansu                                                                                                                                                                                                                                                        
                Ho_Nhansu                                                                                                                                                                                                                                                        
                Ten_Nhansu                                                                                                                                                                                                                                                       
                Hoten_Nhansu                                                                                                                                                                                                                                                     
                Gioitinh 
                Ngay_Sinh                                                                                                                                                                                                                                                        
                Thangsinh                                                                                                                                                                                                                                                        
                Namsinh                                                                                                                                                                                                                                                          
                Ngaysinh   
                Noisinh                                                                                                                                                                                                                                                          
                Cmnd                                                                                                                                                                                                                                                             
                Ngaycap                 
                Noicap                                                                                                                                                                                                                                                           
                Hochieu                                                                                                                                                                                                                                                          
                Ngaycap_Hochieu         
                Noicap_Hochieu                                                                                                                                                                                                                                                   
                Ngay_Vaolam             
                Ngay_Thoiviec           
                Id_Nganhang          
                Taikhoan_Nganhang */
                this.pictureHinh.DataBindings.Clear();
                this.txtMa_Nhansu.DataBindings.Clear();
                this.txtHo_Nhansu.DataBindings.Clear();
                this.txtTen_Nhansu.DataBindings.Clear();
                this.chkGioitinh.DataBindings.Clear();
                this.cmbNgaysinh.DataBindings.Clear();
                this.cmbThangsinh.DataBindings.Clear();
                this.cmbNamsinh.DataBindings.Clear();
                this.txtNoisinh.DataBindings.Clear();
                this.txtCMND.DataBindings.Clear();
                this.dtNgaycap.DataBindings.Clear();
                this.txtNoicap.DataBindings.Clear();
                this.txtHochieu.DataBindings.Clear();
                this.dtNgaycap_Hochieu.DataBindings.Clear();
                this.txtNoicap_Hochieu.DataBindings.Clear();
                this.dtNgay_Vaolam.DataBindings.Clear();
                this.lookUp_NganHang.DataBindings.Clear();
                this.txtTKNganHang.DataBindings.Clear();

                /** thong tin lien he
                Quequan                                                                                                                                                                                                                                                          
                Diachi_Tamtru                                                                                                                                                                                                                                                    
                Diachi_Thuongtru                                                                                                                                                                                                                                                 
                Dienthoai_Nharieng                                                                                                                                                                                                                                               
                Dienthoai                                                                                                                                                                                                                                                        
                Email                                                                                                                                                                                                                                                            
                Id_Quocgia           
                Id_Dantoc            
                Id_Tongiao           
                Id_Honnhan           
                Id_Tpbanthan         
                Id_Tpgiadinh         
                Id_Vanhoa            
                Id_Chuyenmon      */
                this.txtQuequan.DataBindings.Clear();
                this.txtDiachi_Tamtru.DataBindings.Clear();
                this.txtDiachi_Thuongtru.DataBindings.Clear();
                this.txtDienthoai_Nharieng.DataBindings.Clear();
                this.txtDienthoai.DataBindings.Clear();
                this.txtEmail.DataBindings.Clear();
                this.lookUp_Quoctich.DataBindings.Clear();
                this.lookUp_Dantoc.DataBindings.Clear();
                this.lookUp_Tongiao.DataBindings.Clear();
                this.lookUp_Honnhan.DataBindings.Clear();
                this.lookUp_Tpbanthan.DataBindings.Clear();
                this.lookUp_Tpgiadinh.DataBindings.Clear();
                this.lookUp_Vanhoa.DataBindings.Clear();
                this.lookUp_Chuyenmon.DataBindings.Clear();
                 dtNgay_Vaodoan.DataBindings.Clear();
                 dtNgay_Vaocongdoan.DataBindings.Clear();
                 dtNgay_Vaodang.DataBindings.Clear();
              
                //lien quan den bo phan
                this.chkChunhat.DataBindings.Clear();
                this.chkThuhai.DataBindings.Clear();
                this.chkThuba.DataBindings.Clear();
                this.chkThutu.DataBindings.Clear();
                this.chkThunam.DataBindings.Clear();
                this.chkThusau.DataBindings.Clear();
                this.chkThubay.DataBindings.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void DataBindingControl()
        {
            try
            {
                ClearDataBindings();

                /** thong tin chung
                Id_Nhansu            
                Hinh                                                                                                                                                                                                                                                             
                Ma_Nhansu                                                                                                                                                                                                                                                        
                Ho_Nhansu                                                                                                                                                                                                                                                        
                Ten_Nhansu                                                                                                                                                                                                                                                       
                Hoten_Nhansu                                                                                                                                                                                                                                                     
                Gioitinh 
                Ngay_Sinh                                                                                                                                                                                                                                                        
                Thangsinh                                                                                                                                                                                                                                                        
                Namsinh                                                                                                                                                                                                                                                          
                Ngaysinh   
                Noisinh                                                                                                                                                                                                                                                          
                Cmnd                                                                                                                                                                                                                                                             
                Ngaycap                 
                Noicap                                                                                                                                                                                                                                                           
                Hochieu                                                                                                                                                                                                                                                          
                Ngaycap_Hochieu         
                Noicap_Hochieu                                                                                                                                                                                                                                                   
                Ngay_Vaolam             
                Ngay_Thoiviec           
                Id_Nganhang          
                Taikhoan_Nganhang */
                Binding bdPhoto = new Binding("Image", dsNhansu, dsNhansu.Tables[0].TableName + ".Hinh");
                bdPhoto.Format += new ConvertEventHandler(this.PictureFormat);
                pictureHinh.DataBindings.Add(bdPhoto);

                txtMa_Nhansu.DataBindings.Add("Text", dsNhansu, dsNhansu.Tables[0].TableName + ".Ma_Nhansu");
                txtHo_Nhansu.DataBindings.Add("Text", dsNhansu, dsNhansu.Tables[0].TableName + ".Ho_Nhansu");
                txtTen_Nhansu.DataBindings.Add("Text", dsNhansu, dsNhansu.Tables[0].TableName + ".Ten_Nhansu");
                cmbNgaysinh.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Ngay_Sinh");
                cmbThangsinh.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Thangsinh");
                cmbNamsinh.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Namsinh");
                chkGioitinh.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Gioitinh");
                txtNoisinh.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Noisinh");
                txtCMND.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Cmnd");
                dtNgaycap.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Ngaycap");
                txtNoicap.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Noicap");
                this.txtHochieu.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Hochieu");
                this.dtNgaycap_Hochieu.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Ngaycap_Hochieu");
                this.txtNoicap_Hochieu.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Noicap_Hochieu");
                dtNgay_Vaolam.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Ngay_Vaolam");
                lookUp_NganHang.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Id_Nganhang");
                txtTKNganHang.DataBindings.Add("Text", dsNhansu, dsNhansu.Tables[0].TableName + ".Taikhoan_Nganhang");

                /** thong tin lien he
              Quequan                                                                                                                                                                                                                                                          
              Diachi_Tamtru                                                                                                                                                                                                                                                    
              Diachi_Thuongtru                                                                                                                                                                                                                                                 
              Dienthoai_Nharieng                                                                                                                                                                                                                                               
              Dienthoai                                                                                                                                                                                                                                                        
              Email                                                                                                                                                                                                                                                            
              Id_Quocgia           
              Id_Dantoc            
              Id_Tongiao           
              Id_Honnhan           
              Id_Tpbanthan         
              Id_Tpgiadinh         
              Id_Vanhoa            
              Id_Chuyenmon      */
                txtQuequan.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Quequan");
                txtDiachi_Thuongtru.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Diachi_Thuongtru");
                txtDiachi_Tamtru.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Diachi_Tamtru");
                txtDienthoai_Nharieng.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Dienthoai_Nharieng");
                txtDienthoai.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Dienthoai");
                txtEmail.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Email");
                lookUp_Quoctich.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Id_Quocgia");
                lookUp_Dantoc.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Id_Dantoc");
                lookUp_Tongiao.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Id_Tongiao");
                lookUp_Tpgiadinh.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Id_Tpgiadinh");
                lookUp_Tpbanthan.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Id_Tpbanthan");
                lookUp_Honnhan.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Id_Honnhan");
                lookUp_Vanhoa.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Id_Vanhoa");
                lookUp_Chuyenmon.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Id_Chuyenmon");
                dtNgay_Vaodoan.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Ngay_Vaodoan");
                dtNgay_Vaocongdoan.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Ngay_Vaocongdoan");
                dtNgay_Vaodang.DataBindings.Add("EditValue", dsNhansu, dsNhansu.Tables[0].TableName + ".Ngay_Vaodang");

                //``````````````````````````````````````````````````
                chkChunhat.DataBindings.Add("EditValue", ds_Bophan, ds_Bophan.Tables[0].TableName + ".ChuNhat");
                chkThuhai.DataBindings.Add("EditValue", ds_Bophan, ds_Bophan.Tables[0].TableName + ".ThuHai");
                chkThuba.DataBindings.Add("EditValue", ds_Bophan, ds_Bophan.Tables[0].TableName + ".ThuBa");
                chkThutu.DataBindings.Add("EditValue", ds_Bophan, ds_Bophan.Tables[0].TableName + ".ThuTu");
                chkThunam.DataBindings.Add("EditValue", ds_Bophan, ds_Bophan.Tables[0].TableName + ".ThuNam");
                chkThusau.DataBindings.Add("EditValue", ds_Bophan, ds_Bophan.Tables[0].TableName + ".ThuSau");
                chkThubay.DataBindings.Add("EditValue", ds_Bophan, ds_Bophan.Tables[0].TableName + ".ThuBay");

            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }
        }

        public override void ChangeStatus(bool editable)
        {
           //**thong tin chung
            this.txtMa_Nhansu.Properties.ReadOnly = !editable;
            this.txtHo_Nhansu.Properties.ReadOnly = !editable;
            this.txtTen_Nhansu.Properties.ReadOnly = !editable;
            this.chkGioitinh.Properties.ReadOnly = !editable;
            this.cmbNgaysinh.Properties.ReadOnly = !editable;
            this.cmbThangsinh.Properties.ReadOnly = !editable;
            this.cmbNamsinh.Properties.ReadOnly = !editable;
            this.txtNoisinh.Properties.ReadOnly = !editable;
            this.txtCMND.Properties.ReadOnly = !editable;
            this.dtNgaycap.Properties.ReadOnly = !editable;
            this.txtNoicap.Properties.ReadOnly = !editable;
            this.txtHochieu.Properties.ReadOnly = !editable;
            this.dtNgaycap_Hochieu.Properties.ReadOnly = !editable;
            this.txtNoicap_Hochieu.Properties.ReadOnly = !editable;
            this.dtNgay_Vaolam.Properties.ReadOnly = !editable;
            this.lookUp_NganHang.Properties.ReadOnly = !editable;
            this.txtTKNganHang.Properties.ReadOnly = !editable;

            //** thong tin lien he           
            this.txtQuequan.Properties.ReadOnly = !editable;
            this.txtDiachi_Tamtru.Properties.ReadOnly = !editable;
            this.txtDiachi_Thuongtru.Properties.ReadOnly = !editable;
            this.txtDienthoai_Nharieng.Properties.ReadOnly = !editable;
            this.txtDienthoai.Properties.ReadOnly = !editable;
            this.txtEmail.Properties.ReadOnly = !editable;
            this.lookUp_Quoctich.Properties.ReadOnly = !editable;
            this.lookUp_Dantoc.Properties.ReadOnly = !editable;
            this.lookUp_Tongiao.Properties.ReadOnly = !editable;
            this.lookUp_Honnhan.Properties.ReadOnly = !editable;
            this.lookUp_Tpbanthan.Properties.ReadOnly = !editable;
            this.lookUp_Tpgiadinh.Properties.ReadOnly = !editable;
            this.lookUp_Vanhoa.Properties.ReadOnly = !editable;
            this.lookUp_Chuyenmon.Properties.ReadOnly = !editable;

            this.gvrex_Botri_Nhansu.OptionsBehavior.Editable = editable;
            this.gvPhucap.OptionsBehavior.Editable = editable;
            this.gvrex_Dienbien_Luong.OptionsBehavior.Editable = editable;
            this.gvQuanhe_Giadinh.OptionsBehavior.Editable = editable;
            this.gvrex_Hopdong_Laodong_All.OptionsBehavior.Editable = false;

            this.dpBophan.Enabled = !editable;
            this.dpTimkiem.Enabled = !editable;
            this.dpCongcu.Enabled = !editable;

            btnThemhinh.Enabled = editable;
            btnXoahinh.Enabled = editable;

            treeList_Bophan.Enabled = !editable;
            dgrex_Nhansu.Enabled = !editable;

        }

        public override void ResetText()
        {
            //**thong tin chung
            this.txtMa_Nhansu.EditValue = null ;
            this.txtHo_Nhansu.EditValue = null;
            this.txtTen_Nhansu.EditValue = null ;
            this.chkGioitinh.EditValue = null ;
            this.cmbNgaysinh.EditValue = null ;
            this.cmbThangsinh.EditValue = null ;
            this.cmbNamsinh.EditValue = null ;
            this.txtNoisinh.EditValue = null ;
            this.txtCMND.EditValue = null ;
            this.dtNgaycap.EditValue = null ;
            this.txtNoicap.EditValue = null ;
            this.txtHochieu.EditValue = null ;
            this.dtNgaycap_Hochieu.EditValue = null ;
            this.txtNoicap_Hochieu.EditValue = null ;
            this.dtNgay_Vaolam.EditValue = null ;
            this.lookUp_NganHang.EditValue = null ;
            this.txtTKNganHang.EditValue = null ;
            this.pictureHinh.Image = null;

            //** thong tin lien he           
            this.txtQuequan.EditValue = null ;
            this.txtDiachi_Tamtru.EditValue = null ;
            this.txtDiachi_Thuongtru.EditValue = null ;
            this.txtDienthoai_Nharieng.EditValue = null ;
            this.txtDienthoai.EditValue = null ;
            this.txtEmail.EditValue = null ;
            this.lookUp_Quoctich.EditValue = null ;
            this.lookUp_Dantoc.EditValue = null ;
            this.lookUp_Tongiao.EditValue = null ;
            this.lookUp_Honnhan.EditValue = null ;
            this.lookUp_Tpbanthan.EditValue = null ;
            this.lookUp_Tpgiadinh.EditValue = null ;
            this.lookUp_Vanhoa.EditValue = null ;
            this.lookUp_Chuyenmon.EditValue = null ;

            Rex_Botri_Nhansu = null;
            dg_Phucap.DataSource = null;
            dgrex_Botri_Nhansu.DataSource = null;
            dgrex_Dienbien_Luong.DataSource = null;
            dgrex_Hopdong_Laodong_All.DataSource = null;
            dgQuanhe_Giadinh.DataSource = null;
        }

        public object InsertObject()
        {
            Ecm.WebReferences.RexService.Rex_Nhansu objRex_Nhansu = new Ecm.WebReferences.RexService.Rex_Nhansu();
            objRex_Nhansu.Id_Nhansu = -1;

            //**thong tin chung
            objRex_Nhansu.Ma_Nhansu = this.txtMa_Nhansu.EditValue;
            objRex_Nhansu.Ho_Nhansu = this.txtHo_Nhansu.EditValue;
            objRex_Nhansu.Ten_Nhansu = this.txtTen_Nhansu.EditValue;
            objRex_Nhansu.Gioitinh = this.chkGioitinh.EditValue;            
            objRex_Nhansu.Ngay_Sinh = ("" + this.cmbNgaysinh.EditValue != "") ? this.cmbNgaysinh.EditValue : objRex_Nhansu.Ngay_Sinh = null;
            objRex_Nhansu.Thangsinh = ("" + this.cmbThangsinh.EditValue != "")? this.cmbThangsinh.EditValue:objRex_Nhansu.Thangsinh = null;
            if ("" + this.cmbNamsinh.EditValue != "") objRex_Nhansu.Namsinh = this.cmbNamsinh.EditValue;
            objRex_Nhansu.Noisinh = (txtNoisinh.Text == "") ? null : txtNoisinh.EditValue;
            objRex_Nhansu.Cmnd = (txtCMND.Text == "") ? null : txtCMND.EditValue;
            objRex_Nhansu.Noicap = this.txtNoicap.EditValue; 
            if ("" + this.dtNgaycap.EditValue != "") objRex_Nhansu.Ngaycap = this.dtNgaycap.EditValue;
            objRex_Nhansu.Hochieu = (txtHochieu.Text == "") ? null : txtHochieu.EditValue;
            objRex_Nhansu.Noicap_Hochieu = this.txtNoicap_Hochieu.EditValue;
            if ("" + this.dtNgaycap_Hochieu.EditValue != "") objRex_Nhansu.Ngaycap_Hochieu = this.dtNgaycap_Hochieu.EditValue;
            if ("" + this.dtNgay_Vaolam.EditValue != "") objRex_Nhansu.Ngay_Vaolam = this.dtNgay_Vaolam.EditValue;
            if ("" + this.lookUp_NganHang.EditValue != "")
                objRex_Nhansu.Id_Nganhang = this.lookUp_NganHang.EditValue;
            objRex_Nhansu.Taikhoan_Nganhang = (txtTKNganHang.Text == "") ? null : txtTKNganHang.EditValue;
            if (pictureHinh.Image != null)
            {
                Image srcImage = Image.FromFile(pictureHinh.ImageLocation);
                int percentSize = (srcImage.Width > 100) ? 100 * 100 / srcImage.Width : 100;
                Image hinh = GoobizFrame.Windows.ImageUtils.ImageResize.ScaleByPercent(srcImage, percentSize);
                //save image to memory
                MemoryStream ms = new MemoryStream();
                hinh.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageData = ms.GetBuffer();
                //asign image in buffer to property Hinh
                objRex_Nhansu.Hinh = imageData;
            }
            else
            {
                Image srcImage = global::Ecm.Rex.Properties.Resources.no_image;
                int percentSize = (srcImage.Width > 100) ? 100 * 100 / srcImage.Width : 100;
                Image hinh = GoobizFrame.Windows.ImageUtils.ImageResize.ScaleByPercent(srcImage, percentSize);
                //save image to memory
                MemoryStream ms = new MemoryStream();
                hinh.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageData = ms.GetBuffer();
                //asign image in buffer to property Hinh
                objRex_Nhansu.Hinh = imageData;
            }


            //**thong tin lien lac
            objRex_Nhansu.Quequan = (txtQuequan.Text == "") ? null : txtQuequan.EditValue;
            objRex_Nhansu.Diachi_Thuongtru = (txtDiachi_Thuongtru.Text == "") ? null : txtDiachi_Thuongtru.EditValue;
            objRex_Nhansu.Diachi_Tamtru = (txtDiachi_Tamtru.Text == "") ? null : txtDiachi_Tamtru.EditValue;
            objRex_Nhansu.Dienthoai_Nharieng = (this.txtDienthoai_Nharieng.Text == "") ? null : txtDienthoai_Nharieng.EditValue;
            objRex_Nhansu.Dienthoai = (this.txtDienthoai.Text == "") ? null : txtDienthoai.EditValue;
            objRex_Nhansu.Email = (this.txtEmail.Text == "") ? null : txtEmail.EditValue;

            if ("" + this.lookUp_Dantoc.EditValue != "")
                objRex_Nhansu.Id_Dantoc = this.lookUp_Dantoc.EditValue;

            if ("" + this.lookUp_Tongiao.EditValue != "")
                objRex_Nhansu.Id_Tongiao = this.lookUp_Tongiao.EditValue;

            if ("" + this.lookUp_Quoctich.EditValue != "")
                objRex_Nhansu.Id_Quocgia = this.lookUp_Quoctich.EditValue;

            if ("" + this.lookUp_Tpgiadinh.EditValue != "")
                objRex_Nhansu.Id_Tpgiadinh = this.lookUp_Tpgiadinh.EditValue;

            if ("" + this.lookUp_Tpbanthan.EditValue != "")
                objRex_Nhansu.Id_Tpbanthan = this.lookUp_Tpbanthan.EditValue;

            if ("" + this.lookUp_Honnhan.EditValue != "")
                objRex_Nhansu.Id_Honnhan = this.lookUp_Honnhan.EditValue;

            if ("" + this.lookUp_Vanhoa.EditValue != "")
                objRex_Nhansu.Id_Vanhoa = this.lookUp_Vanhoa.EditValue;

            if ("" + this.lookUp_Chuyenmon.EditValue != "")
                objRex_Nhansu.Id_Chuyenmon = this.lookUp_Chuyenmon.EditValue;

            if ("" + this.lookUp_Chuyenmon.EditValue != "")
                objRex_Nhansu.Id_Chuyenmon = this.lookUp_Chuyenmon.EditValue;

            if ("" + dtNgay_Vaodoan.EditValue != "")
                objRex_Nhansu.Ngay_Vaodoan = dtNgay_Vaodoan.DateTime;

            if ("" + dtNgay_Vaocongdoan.EditValue != "")
                objRex_Nhansu.Ngay_Vaocongdoan = dtNgay_Vaocongdoan.DateTime;

            if ("" + dtNgay_Vaodang.EditValue != "")
                objRex_Nhansu.Ngay_Vaodang = dtNgay_Vaodang.DateTime;
           
            object identity = objRexService.Insert_Rex_Nhansu(objRex_Nhansu);
            Ecm.WebReferences.RexService.Rex_Botri_Nhansu Rex_Botri_Nhansu = new Ecm.WebReferences.RexService.Rex_Botri_Nhansu();
            Rex_Botri_Nhansu.Id_Bophan = Id_Bophan;
            Rex_Botri_Nhansu.Id_Nhansu = identity;
            Rex_Botri_Nhansu.Ngay_Batdau = objRex_Nhansu.Ngay_Vaolam;
            Rex_Botri_Nhansu.Ngay_Ketthuc = null;
            return objRexService.Insert_Rex_Botri_Nhansu(Rex_Botri_Nhansu);
        }

        public object UpdateObject()
        {
            Ecm.WebReferences.RexService.Rex_Nhansu objRex_Nhansu = new Ecm.WebReferences.RexService.Rex_Nhansu();
            objRex_Nhansu.Id_Nhansu = gvrex_Nhansu.GetFocusedRowCellValue("Id_Nhansu");

            //**thong tin chung
            objRex_Nhansu.Ma_Nhansu = this.txtMa_Nhansu.EditValue;
            objRex_Nhansu.Ho_Nhansu = this.txtHo_Nhansu.EditValue;
            objRex_Nhansu.Ten_Nhansu = this.txtTen_Nhansu.EditValue;
            objRex_Nhansu.Gioitinh = this.chkGioitinh.EditValue;
            objRex_Nhansu.Ngay_Sinh = ("" + this.cmbNgaysinh.EditValue != "") ? this.cmbNgaysinh.EditValue : objRex_Nhansu.Ngay_Sinh = null;
            objRex_Nhansu.Thangsinh = ("" + this.cmbThangsinh.EditValue != "") ? this.cmbThangsinh.EditValue : objRex_Nhansu.Thangsinh = null;
            if ("" + this.cmbNamsinh.EditValue != "") objRex_Nhansu.Namsinh = this.cmbNamsinh.EditValue;
            objRex_Nhansu.Noisinh = (txtNoisinh.Text == "") ? null : txtNoisinh.EditValue;
            objRex_Nhansu.Cmnd = (txtCMND.Text == "") ? null : txtCMND.EditValue;
            objRex_Nhansu.Noicap = this.txtNoicap.EditValue;
            if ("" + this.dtNgaycap.EditValue != "") objRex_Nhansu.Ngaycap = this.dtNgaycap.EditValue;
            objRex_Nhansu.Hochieu = (txtHochieu.Text == "") ? null : txtHochieu.EditValue;
            objRex_Nhansu.Noicap_Hochieu = (txtNoicap_Hochieu.Text == "") ? null : txtNoicap_Hochieu.EditValue;
            if ("" + this.dtNgaycap_Hochieu.EditValue != "") objRex_Nhansu.Ngaycap_Hochieu = this.dtNgaycap_Hochieu.EditValue;
            if ("" + this.dtNgay_Vaolam.EditValue != "") objRex_Nhansu.Ngay_Vaolam = this.dtNgay_Vaolam.EditValue;
            if ("" + this.lookUp_NganHang.EditValue != "")
                objRex_Nhansu.Id_Nganhang = this.lookUp_NganHang.EditValue;
            objRex_Nhansu.Taikhoan_Nganhang = (txtTKNganHang.Text == "") ? null : txtTKNganHang.EditValue;

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
                objRex_Nhansu.Hinh = imageData;
            }
            else
            {
                Image srcImage = global::Ecm.Rex.Properties.Resources.no_image;
                int percentSize = (srcImage.Width > 100) ? 100 * 100 / srcImage.Width : 100;
                Image hinh = GoobizFrame.Windows.ImageUtils.ImageResize.ScaleByPercent(srcImage, percentSize);
                //save image to memory
                MemoryStream ms = new MemoryStream();
                hinh.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageData = ms.GetBuffer();
                //asign image in buffer to property Hinh
                objRex_Nhansu.Hinh = imageData;
            }


            //**thong tin lien lac
            objRex_Nhansu.Quequan = (txtQuequan.Text == "") ? null : txtQuequan.EditValue;
            objRex_Nhansu.Diachi_Thuongtru = (txtDiachi_Thuongtru.Text == "") ? null : txtDiachi_Thuongtru.EditValue;
            objRex_Nhansu.Diachi_Tamtru = (txtDiachi_Tamtru.Text == "") ? null : txtDiachi_Tamtru.EditValue;
            objRex_Nhansu.Dienthoai_Nharieng = (this.txtDienthoai_Nharieng.Text == "") ? null : txtDienthoai_Nharieng.EditValue;
            objRex_Nhansu.Dienthoai = (this.txtDienthoai.Text == "") ? null : txtDienthoai.EditValue;
            objRex_Nhansu.Email = (this.txtEmail.Text == "") ? null : txtEmail.EditValue;

            if ("" + this.lookUp_Dantoc.EditValue != "")
                objRex_Nhansu.Id_Dantoc = this.lookUp_Dantoc.EditValue;

            if ("" + this.lookUp_Tongiao.EditValue != "")
                objRex_Nhansu.Id_Tongiao = this.lookUp_Tongiao.EditValue;

            if ("" + this.lookUp_Quoctich.EditValue != "")
                objRex_Nhansu.Id_Quocgia = this.lookUp_Quoctich.EditValue;

            if ("" + this.lookUp_Tpgiadinh.EditValue != "")
                objRex_Nhansu.Id_Tpgiadinh = this.lookUp_Tpgiadinh.EditValue;

            if ("" + this.lookUp_Tpbanthan.EditValue != "")
                objRex_Nhansu.Id_Tpbanthan = this.lookUp_Tpbanthan.EditValue;

            if ("" + this.lookUp_Honnhan.EditValue != "")
                objRex_Nhansu.Id_Honnhan = this.lookUp_Honnhan.EditValue;

            if ("" + this.lookUp_Vanhoa.EditValue != "")
                objRex_Nhansu.Id_Vanhoa = this.lookUp_Vanhoa.EditValue;

            if ("" + this.lookUp_Chuyenmon.EditValue != "")
                objRex_Nhansu.Id_Chuyenmon = this.lookUp_Chuyenmon.EditValue;

            if ("" + dtNgay_Vaodoan.EditValue != "")
                objRex_Nhansu.Ngay_Vaodoan = dtNgay_Vaodoan.DateTime;

            if ("" + dtNgay_Vaocongdoan.EditValue != "")
                objRex_Nhansu.Ngay_Vaocongdoan = dtNgay_Vaocongdoan.DateTime;

            if ("" + dtNgay_Vaodang.EditValue != "")
                objRex_Nhansu.Ngay_Vaodang = dtNgay_Vaodang.DateTime;

            bool error = false;
            this.DoClickEndEdit(dgrex_Botri_Nhansu);
            this.DoClickEndEdit(dgrex_Dienbien_Luong);
            this.DoClickEndEdit(dgQuanhe_Giadinh);
            this.DoClickEndEdit(dg_Phucap);
            if (ds_BotriNS.HasChanges())
            {
                error = !BotriNS_Update();
                if (error) return false;
            }
            if (ds_Phucap.HasChanges())
            {
                error = !Phucap_Update();
                if (error) return false;
            }
            if (ds_Dienbienluong.HasChanges())
            {
                error = !Dienbienluong_Update();
                if (error) return false;
            }
            if (ds_Quanhe_Giadinh.HasChanges())
            {
                error = !Quanhe_Giadinh_Update();
                if (error) return false;
            }

            return objRexService.Update_Rex_Nhansu(objRex_Nhansu);
        }

        public object DeleteObject()
        {
            Ecm.WebReferences.RexService.Rex_Nhansu objRex_Nhansu = new Ecm.WebReferences.RexService.Rex_Nhansu();
            objRex_Nhansu.Id_Nhansu = gvrex_Nhansu.GetFocusedRowCellValue("Id_Nhansu");
            return objRexService.Delete_Rex_Nhansu(objRex_Nhansu);
        }

        public override bool PerformAdd()
        {
            ClearDataBindings();
            this.ChangeStatus(true);
            this.ResetText();
            txtMa_Nhansu.Text = DateTime.Now.Ticks.ToString();
            txtMa_Nhansu.Focus();
            txtMa_Nhansu.SelectAll();

            gvPhucap.OptionsBehavior.Editable = false;
            gvQuanhe_Giadinh.OptionsBehavior.Editable = false;
            gvrex_Botri_Nhansu.OptionsBehavior.Editable = false;
            gvrex_Dienbien_Luong.OptionsBehavior.Editable = false;
            return true;
        }

        public override bool PerformEdit()
        {
            if (!gvrex_Nhansu.IsDataRow(gvrex_Nhansu.FocusedRowHandle))
            {
                GoobizFrame.Windows.Forms.UserMessage.Show("Msg00126", new string[] { "" });
                return false;
            }

            this.ChangeStatus(true);
            return true;
         
        }

        public override bool PerformCancel()
        {
            //this.DisplayInfo();
            treeListColumn1.TreeList.FocusedNode = focusedNode;
            DisplayInfo2();
            ChangeStatus(false);
            return true;
        }

        public override bool PerformSave()
        {
            try
            {
                bool success = false;
                GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(txtMa_Nhansu,lblMa_Nhansu.Text);
                hashtableControls.Add(txtHo_Nhansu, lblHo_Nhansu.Text);
                hashtableControls.Add(txtTen_Nhansu, lblTen_Nhansu.Text);
                hashtableControls.Add(cmbNamsinh, "Năm sinh");
                hashtableControls.Add(txtCMND, lblCMND.Text);
                hashtableControls.Add(dtNgaycap, lblNgaycap.Text);
                hashtableControls.Add(txtNoicap, lblNoicap.Text);
                hashtableControls.Add(dtNgay_Vaolam, lblNgay_Vaolam.Text);

                System.Collections.Hashtable htb_Cmnd = new System.Collections.Hashtable();
                htb_Cmnd.Add(txtCMND, lblCMND.Text);

                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;

                if ("" + cmbNgaysinh.Text != "" && "" + cmbThangsinh.Text != "" && "" + cmbNamsinh.Text != "")
                    if (!ValidateDate(cmbNamsinh.EditValue, cmbThangsinh.EditValue, cmbNgaysinh.EditValue))
                        return false;

                if (dtNgaycap.DateTime >= DateTime.Today)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show("Ngày cấp chứng minh nhân dân không hợp lý, vui lòng nhập lại");
                    dtNgaycap.EditValue = null;
                    return false;
                }

                if (this.FormState == GoobizFrame.Windows.Forms.FormState.Add)
                {
                    if (!GoobizFrame.Windows.MdiUtils.Validator.CheckExistValues(htb_Cmnd, (DataSet)dgrex_Nhansu.DataSource, "Cmnd"))
                        return false;

                    success = Convert.ToBoolean(this.InsertObject());
                }
                else if (this.FormState == GoobizFrame.Windows.Forms.FormState.Edit)
                {
                    success = Convert.ToBoolean(this.UpdateObject());
                }
                if (success)
                {
                    treeListColumn1.TreeList.FocusedNode = focusedNode;
                    this.DisplayInfo2();
                    this.ChangeStatus(false);

                    //tinh luong
                    new System.Threading.Thread(new System.Threading.ThreadStart(Luongtonghop_Init)).Start();
                }
                return success;
            }
            catch (Exception ex)
            {
                if (ex.ToString().IndexOf("exists") != -1)
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("SYS_ALREADY_EXIST", new string[] { lblMa_Nhansu.Text, lblMa_Nhansu.Text });
                }
                else
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                    
                }
                return false;
            }
        }

        public override bool PerformDelete()
        {
            if (!gvrex_Nhansu.IsDataRow(gvrex_Nhansu.FocusedRowHandle))
            {
                GoobizFrame.Windows.Forms.UserMessage.Show("Msg00126", new string[] { "" });
                return false;
            }

             if (gvrex_Nhansu.RowCount > 0)
            {
                if (""+gvrex_Nhansu.GetFocusedRowCellValue("Id_Hopdong_Laodong") != "")
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show("Nhân sự đã lập hợp đồng lao động, không thể xóa.");
                    return false;
                }
                else
                {
                    if (GoobizFrame.Windows.Forms.UserMessage.Show("SYS_CONFIRM_BFDELETE", new string[]  {txtMa_Nhansu.Text, txtHo_Nhansu.Text + " " + txtTen_Nhansu.Text }) == DialogResult.Yes)
                    {
                        try
                        {
                            this.DeleteObject();
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("INUSE"))
                                GoobizFrame.Windows.Forms.UserMessage.Show("SYS_DATA_INUSE", new string[] { 
                                txtHo_Nhansu.Text + " " + txtTen_Nhansu.Text});
                            GoobizFrame.Windows.MdiUtils.Validator.CheckReferencedRecord(ex.Message, "");
                        }
                        this.DisplayInfo();
                    }
                }
                return base.PerformDelete();
            }
            return false;
        }

        public override object PerformSelectOneObject()
        {
            try
            {
                this.id_nhansu_chon = gvrex_Nhansu.GetSelectedRows();
                this.Dispose();
                this.Close();
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
                return null;
            }
        }

        public override bool PerformPrintPreview()
        {
            Datasets.DsRex_Nhansu DsRpt = new Ecm.Rex.Datasets.DsRex_Nhansu();
            try
            {
                foreach (DataRow row in dsNhansu.Tables[0].Rows)
                    DsRpt.Tables[0].ImportRow(row);
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }
            PrintPreview(DsRpt);
            return true;
        }

        #endregion

        #region Event Handling

        private void btnXoahinh_Click(object sender, EventArgs e)
        {
            pictureHinh.Image = null;
        }

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

        private void treeList_Bophan_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            GoobizFrame.Windows.PlugIn.RightHelpers.CheckUserRightAction(this);

            focusedNode = e.Node;
            Id_Bophan = Convert.ToInt64("" + e.Node.GetValue("Id_Bophan"));
            DisplayInfo2();

            
            //btnCapnhat_Nhanvien.Enabled = EnableEdit;
            navPrint_IDCard.Enabled = EnablePrintPreview;

           
        }

        private void btnChuyen_BP_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Bophan_Add", this);
            if (dialog == null)
                return;
            var SelectedObject = dialog.GetType().GetProperty("SelectedRex_Dm_Bophan").GetValue(dialog, null)
                   as Ecm.WebReferences.MasterService.Rex_Dm_Bophan;

            if (SelectedObject != null)
            {
                Rex_Botri_Nhansu = new Ecm.WebReferences.RexService.Rex_Botri_Nhansu();
                Rex_Botri_Nhansu.Id_Bophan = SelectedObject.Id_Bophan;
                Rex_Botri_Nhansu.Id_Nhansu = gvrex_Nhansu.GetFocusedRowCellValue("Id_Nhansu");
                Rex_Botri_Nhansu.Id_Botri_Nhansu = gvrex_Nhansu.GetFocusedRowCellValue("Id_Botri_Nhansu");
            }
        }

        private void btnPrint_SelectedDs_Click(object sender, EventArgs e)
        {
//            Frmrex_Nhansu_Dialog2 Frmrex_Nhansu_Dialog2 = new Frmrex_Nhansu_Dialog2();
//            GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(Frmrex_Nhansu_Dialog2);
//            Frmrex_Nhansu_Dialog2.DisplayInfo();
//            Frmrex_Nhansu_Dialog2.ShowDialog();
//            if (Frmrex_Nhansu_Dialog2.Selected_Datarows != null && Frmrex_Nhansu_Dialog2.Selected_Datarows.Length > 0)
//            {
//                DataSets.DsRex_Nhansu DsRpt = new Ecm.Rex.DataSets.DsRex_Nhansu();
//                try
//                {
//                    foreach (DataRow row in Frmrex_Nhansu_Dialog2.Selected_Datarows)
//                        DsRpt.Tables[0].ImportRow(row);
//                }
//                catch (Exception ex)
//                {
//#if DEBUG
//                    MessageBox.Show(ex.Message);
//#endif
//                }

//                PrintPreview(DsRpt);
//            }
        }

        #region lookUpEdit_ButtonClick

        private void lookUpEdit_Quoctich_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                    "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Quocgia_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Quocgia").GetValue(dialog, null)
                    as Ecm.WebReferences.MasterService.Rex_Dm_Quocgia;

                DataSet dsDm_Quocgia = objMasterService.Get_All_Rex_Dm_Quocgia_Collection().ToDataSet();
                lookUp_Quoctich.Properties.DataSource = dsDm_Quocgia.Tables[0];
                if (SelectedObject != null)
                {
                    lookUp_Quoctich.EditValue = SelectedObject.Id_Quocgia;
                }
            }
        }

        private void lookUpEdit_Dantoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                    "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Dantoc_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Dantoc").GetValue(dialog, null)
                   as Ecm.WebReferences.MasterService.Rex_Dm_Dantoc;

                DataSet dsDm_Dantoc = objMasterService.Get_All_Rex_Dm_Dantoc_Collection().ToDataSet();
                lookUp_Dantoc.Properties.DataSource = dsDm_Dantoc.Tables[0];
                if (SelectedObject != null)
                {
                    lookUp_Dantoc.EditValue = SelectedObject.Id_Dantoc;
                }
            }
        }

        private void lookUpEdit_Tongiao_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                    "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Tongiao_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Tongiao").GetValue(dialog, null)
                   as Ecm.WebReferences.MasterService.Rex_Dm_Tongiao;

                DataSet dsDm_Tongiao = objMasterService.Get_All_Rex_Dm_Tongiao_Collection().ToDataSet();
                lookUp_Tongiao.Properties.DataSource = dsDm_Tongiao.Tables[0];
                if (SelectedObject != null)
                {
                    lookUp_Tongiao.EditValue = SelectedObject.Id_Tongiao;
                }
            }
        }

        private void lookUpEdit_Honnhan_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                    "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Honnhan_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Honnhan").GetValue(dialog, null)
                   as Ecm.WebReferences.MasterService.Rex_Dm_Honnhan;

                DataSet dsDm_Honnhan = objMasterService.Get_All_Rex_Dm_Honnhan_Collection().ToDataSet();
                lookUp_Honnhan.Properties.DataSource = dsDm_Honnhan.Tables[0];
                if (SelectedObject != null)
                {
                    lookUp_Honnhan.EditValue = SelectedObject.Id_Honnhan;
                }
            }
        }

        private void lookUpEdit_Vanhoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                    "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Vanhoa_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Ndung_Tgluong").GetValue(dialog, null)
                   as Ecm.WebReferences.MasterService.Rex_Dm_Vanhoa;

                DataSet dsDm_Vanhoa = objMasterService.Get_All_Rex_Dm_Vanhoa_Collection().ToDataSet();
                lookUp_Vanhoa.Properties.DataSource = dsDm_Vanhoa.Tables[0];
                if (SelectedObject != null)
                {
                    lookUp_Vanhoa.EditValue = SelectedObject.Id_Vanhoa;
                }
            }
        }

        private void lookUpEdit_Tpgiadinh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                    "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Tpgiadinh_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Tpgiadinh").GetValue(dialog, null)
                   as Ecm.WebReferences.MasterService.Rex_Dm_Tpgiadinh;

                DataSet dsDm_Tpgiadinh = objMasterService.Get_All_Rex_Dm_Tpgiadinh_Collection().ToDataSet();
                lookUp_Tpgiadinh.Properties.DataSource = dsDm_Tpgiadinh.Tables[0];
                if (SelectedObject != null)
                {
                    lookUp_Tpgiadinh.EditValue = SelectedObject.Id_Tpgiadinh;
                }
            }
        }

        private void lookUpEdit_Tpbanthan_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                    "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Tpbanthan_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Tpbanthan").GetValue(dialog, null)
                   as Ecm.WebReferences.MasterService.Rex_Dm_Tpbanthan;

                DataSet dsDm_Tpbanthan = objMasterService.Get_All_Rex_Dm_Tpbanthan_Collection().ToDataSet();
                lookUp_Tpbanthan.Properties.DataSource = dsDm_Tpbanthan.Tables[0];
                if (SelectedObject != null)
                {
                   
                    lookUp_Tpbanthan.EditValue = SelectedObject.Id_Tpbanthan;
                }
            }
        }

        private void lookUp_Chuyenmon_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                    "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Chuyenmon_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Chuyenmon").GetValue(dialog, null)
                   as Ecm.WebReferences.MasterService.Rex_Dm_Chuyenmon;

                DataSet dsDm_Chuyenmon = objMasterService.Get_All_Rex_Dm_Chuyenmon_Collection().ToDataSet();
                lookUp_Chuyenmon.Properties.DataSource = dsDm_Chuyenmon.Tables[0];
                if (SelectedObject != null)
                {
                    lookUp_Chuyenmon.EditValue = SelectedObject.Id_Chuyenmon;
                }
            }
        }

        #endregion

        private void btn_Hopdonglaodong_Click(object sender, EventArgs e)
        {
            
        }

       

     
        private void dgrex_Nhansu_DoubleClick(object sender, MouseEventArgs e)
        {
            if (gvrex_Nhansu.CalcHitInfo(e.Location).InRowCell)
            {
                if(EnableEdit)
                {
                    Frmrex_Nhansu_Info frmNhansu_Info = new Frmrex_Nhansu_Info(gvrex_Nhansu.GetFocusedRowCellValue("Id_Nhansu"));
                    frmNhansu_Info.EnablePrintPreview = EnablePrintPreview;
                    frmNhansu_Info.Text = "Cập nhật hồ sơ nhân viên";
                    frmNhansu_Info.ShowDialog();
                    this.DisplayInfo();
                }
            }
        }
        #endregion

        #region Custome method

        //Ham dinh dang anh truoc khi Biding vao picture edit
        private void PictureFormat(object sender, ConvertEventArgs e)
        {
            if ("" + e.Value != "")
            {
                byte[] imagedata = (byte[])e.Value;
                MemoryStream ms = new MemoryStream();
                ms.Write(imagedata, 0, imagedata.Length);
                Image image = Image.FromStream(ms, true);
                e.Value = image;
            }
            else
            {
                e.Value = Ecm.Rex.Properties.Resources.clipping_picture;
            }
        }

        void PrintPreview(Datasets.DsRex_Nhansu DsRpt)
        {
            //init object XtraReport
            Reports.rptRex_Nhansu_Inthe XtraReport = new Ecm.Rex.Reports.rptRex_Nhansu_Inthe();

            //show form print preview
            GoobizFrame.Windows.Forms.FrmPrintPreview frmPrintPreview = new GoobizFrame.Windows.Forms.FrmPrintPreview();
            //objFormReport.FileName = txtXtra_Rpt_Name.Text;
            frmPrintPreview.Report = XtraReport;
            XtraReport.DataSource = DsRpt;

            #region Set he so ctrinh - logo, ten cty

            using (DataSet dsHeso_Chuongtrinh = objMasterService.Get_Rex_Dm_Heso_Chuongtrinh_Collection3().ToDataSet())
            {
                DataSet dsCompany_Paras = new DataSet();
                dsCompany_Paras.Tables.Add("Company_Paras");
                dsCompany_Paras.Tables[0].Columns.Add("CompanyName", typeof(string));
                dsCompany_Paras.Tables[0].Columns.Add("CompanyAddress", typeof(string));
                dsCompany_Paras.Tables[0].Columns.Add("CompanyLogo", typeof(byte[]));

                byte[] imageData = Convert.FromBase64String("" + dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "CompanyLogo"))[0]["Heso"]);

                dsCompany_Paras.Tables[0].Rows.Add(new object[]  {    
                    dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'","CompanyName"))[0]["Heso"]
                    ,dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'","CompanyAddress"))[0]["Heso"]
                    ,imageData
                });              

                XtraReport.xrTableCell_Company.DataBindings.Add(
                   new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyName"));
                
            }

            #endregion

            XtraReport.CreateDocument();
            //XtraReport.Print();

            frmPrintPreview.printControl1.PrintingSystem = XtraReport.PrintingSystem;
            frmPrintPreview.MdiParent = this.MdiParent;
            frmPrintPreview.Text = this.Text + "(Xem trang in)";
            frmPrintPreview.Show();
            frmPrintPreview.Activate();
        }

        public void DisplayInfo2()
        {
            //if(isLoaded)
            lock (this)
            {
                try
                {
                    //txtNgaysinh.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
                    txtMa_Nhansu.Enabled = true;
                    if (treeList_Bophan.FocusedNode.GetDisplayText("Ten_Bophan").ToUpper() == "CHƯA BỐ TRÍ")
                    {
                        dsNhansu = objRexService.Get_All_Rex_Nhansu_Chuabotri_Collection().ToDataSet();
                    }
                    else
                    {
                        dsNhansu = objRexService.Get_Rex_Nhansu_ByBoPhan_Collection(Id_Bophan).ToDataSet();
                    }
                    //dsNhansu.Tables[0].Columns.Add("Trangthai_Hopdong");
                    //DataSet ds = objRex.GetAll_Rex_Nhansu_By_Bophan_Collection(id_bophan);
                    //foreach (DataRow row_rex_nhansu in dsNhansu.Tables[0].Rows)
                    //    row_rex_nhansu["Ngaysinh"] = GoobizFrame.Windows.MdiUtils.DateTimeMask.YMDToShortDatePattern("" + row_rex_nhansu["Ngaysinh"],
                    //        GoobizFrame.Windows.MdiUtils.DateTimeMask.GetDateTimeFormat());

                    dgrex_Nhansu.DataSource = dsNhansu;
                    dgrex_Nhansu.DataMember = dsNhansu.Tables[0].TableName;

                    DisplayNhansuInfo();

                    DataBindingControl();
                    if (gvrex_Nhansu.RowCount == 0)
                        ResetText();

                    gvrex_Nhansu.Columns["Id_Bophan"].Visible = false;


                    this.gvrex_Nhansu.BestFitColumns();
                        
                    if (xtraTabControl1.SelectedTabPage != xtraTabPage_Ttchung)
                           xtraTabControl1.SelectedTabPage = xtraTabPage_Ttchung;
                }
                catch (Exception ex)
                {
#if DEBUG
                    MessageBox.Show(ex.ToString());
#endif
                }
            }
        }

        //public void SetTrangThaiHopDong()
        //{
        //    if (gridView1.RowCount > 0)
        //    {
        //        for (int i = 0; i <= gridView1.RowCount; i++)
        //        {
        //            long id_nhansu = Convert.ToInt64(gridView1.GetRowCellValue(i, gridView1.Columns["Id_Nhansu"]));
        //            object trangthai = objRexService.Get_Nhansu_Trangthai_Hopdong(id_nhansu);
        //            gridView1.SetRowCellValue(i, gridView1.Columns["Trangthai_Hopdong"], trangthai);
        //        }
        //    }
        //}

        private bool ValidateDate(object Nam, object Thang, object Ngay)
        {
            try
            {
                DateTime objDateTime = new DateTime(Convert.ToInt32(Nam), Convert.ToInt32(Thang), Convert.ToInt32(Ngay));
                DateTime.Parse(objDateTime.ToString());
            }
            catch
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Ngay sinh không hợp lệ, vui lòng chọn lại.");
                return false;
            }
            return true;
        }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchNhansu();
        }

        private void SearchNhansu()
        {
            try
            {
                Ecm.WebReferences.RexService.Rex_Nhansu nhansu = new Ecm.WebReferences.RexService.Rex_Nhansu();
                nhansu.Ma_Nhansu = txtsMa_Nhansu.EditValue;
                nhansu.Ho_Nhansu = txtsHo_Nhansu.EditValue;
                nhansu.Ten_Nhansu = txtsTen_Nhansu.EditValue;
                nhansu.Tenkhac = txtsTen_Khac.EditValue;
                nhansu.Cmnd = txtsCMND.EditValue;
                nhansu.Hochieu = txtsHochieu.EditValue;
                nhansu.Diachi_Thuongtru = txtsDiachi_Thuongtru.EditValue;
                nhansu.Diachi_Tamtru = txtsDiachi_Tamtru.EditValue;
                nhansu.Dienthoai = txtsDienthoai_Didong.EditValue;
                nhansu.Dienthoai_Nharieng = txtsDienthoai_Nha.EditValue;
                nhansu.Fax = txtsFax.EditValue;
                nhansu.Email = txtsEmail.EditValue;

                dsNhansu = objRexService.Get_Search_Rex_Nhansu(nhansu).ToDataSet();
                dgrex_Nhansu.DataSource = dsNhansu;
                dgrex_Nhansu.DataMember = dsNhansu.Tables[0].TableName;
                gvrex_Nhansu.Columns["Id_Bophan"].Visible = true;
                gvrex_Nhansu.Columns["Id_Bophan"].VisibleIndex = 0;

                if (gvrex_Nhansu.RowCount == 0)
                    ResetText();
                DataBindingControl();

                this.gvrex_Nhansu.BestFitColumns();
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "");
            }
    }

     

        private void txtsMa_Nhansu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchNhansu();
        }

        void DisplayNhansuInfo()
        {
            if ("" + gvrex_Nhansu.GetFocusedRowCellValue("Id_Nhansu") != "")
            {
                Id_Nhansu = gvrex_Nhansu.GetFocusedRowCellValue("Id_Nhansu");
                nhan_su = new Ecm.WebReferences.RexService.Rex_Nhansu();
                nhan_su.Id_Nhansu = Id_Nhansu;

                DisplayBotriNhansu();
                DisplayInfo_Phucap();
                DisplayInfo_Dienbienluong();
                DisplayInfo_Quanhegiadinh();
                DisplayInfo_Hopdonglaodong();
            }
           
        }

        private void gvrex_Nhansu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayNhansuInfo();
           
        }

        #region thong tin nhan su chi tiet
        #region BotriNS

        private void DisplayBotriNhansu(){

            ds_BotriNS = objRexService.Get_All_Rex_Botri_Nhansu_byNhanSu_Collection(nhan_su).ToDataSet();
            dgrex_Botri_Nhansu.DataSource = ds_BotriNS;
            dgrex_Botri_Nhansu.DataMember = ds_BotriNS.Tables[0].TableName;
            this.gvrex_Botri_Nhansu.BestFitColumns();
        }

        private bool BotriNS_Update()
        {
            this.DoClickEndEdit(dgrex_Botri_Nhansu);

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

        private void dgrex_Botri_Nhansu_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Append)
            {
                //if (gvrex_Botri_Nhansu.RowCount > 0)
                //{
                //    if ("" + gvrex_Botri_Nhansu.GetRowCellValue(gvrex_Botri_Nhansu.RowCount - 1, gvrex_Botri_Nhansu.Columns["Ngay_Ketthuc"]) != "")
                //    {
                //        DateTime dtNgay_Ketthuc = Convert.ToDateTime(gvrex_Botri_Nhansu.GetRowCellValue(gvrex_Botri_Nhansu.RowCount - 1, gvrex_Botri_Nhansu.Columns["Ngay_Ketthuc"]));
                //        //Ngày bố trí kế tiếp là ngày kết thúc kề trước cộng thêm một ngày.
                //        gridDate_Ngay_Batdau.MinValue = dtNgay_Ketthuc.AddDays(1);
                //        //Kiểm tra ngày kết thúc bố trí với ngày hiện tại.
                //        if (dtNgay_Ketthuc.CompareTo(DateTime.Today) >= 0)
                //        {
                //            GoobizFrame.Windows.Forms.MessageDialog.Show("Nhân sự đang được bố trí, không thể bố trí thêm.");
                //            e.Handled = true;
                //        }
                //    }
                //    else
                //    {
                //        GoobizFrame.Windows.Forms.MessageDialog.Show("Nhân sự đang được bố trí, không thể bố trí thêm.");
                //        e.Handled = true;
                //    }
                //}
            }
            else

                if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
                {
                    if (gvrex_Botri_Nhansu.RowCount <= 1)
                    {
                        GoobizFrame.Windows.Forms.MessageDialog.Show("Không thể xóa, nhân sự phải được bố trí vào bộ phận.");
                        e.Handled = true;
                    }
                }
        }

       
        #endregion  

        #region Phucap
        private void gvPhucap_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvPhucap.SetFocusedRowCellValue(gvPhucap.Columns["Id_Nhansu"], Id_Nhansu);
            gvPhucap.SetFocusedRowCellValue(gvPhucap.Columns["Id_Botri_Nhansu"], gvrex_Botri_Nhansu.GetFocusedRowCellValue("Id_Botri_Nhansu"));
            gvPhucap.SetFocusedRowCellValue(gvPhucap.Columns["Ngay_Batdau"], gvrex_Botri_Nhansu.GetFocusedRowCellValue("Ngay_Batdau"));
            gvPhucap.SetFocusedRowCellValue(gvPhucap.Columns["Ngay_Ketthuc"], gvrex_Botri_Nhansu.GetFocusedRowCellValue("Ngay_Ketthuc"));
            ds_Phucap.Tables[0].Columns["Sotien"].ReadOnly = false;
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
            this.DoClickEndEdit(dg_Phucap);

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

        void DisplayInfo_Dienbienluong()
        {
            ds_Dienbienluong = objRexService.Get_Rex_Dienbien_Luong_By_Nhansu_Collection3(nhan_su).ToDataSet();
            dgrex_Dienbien_Luong.DataSource = ds_Dienbienluong;
            dgrex_Dienbien_Luong.DataMember = ds_Dienbienluong.Tables[0].TableName;
            this.gvrex_Dienbien_Luong.BestFitColumns();
        }
       
        /// <summary>
        /// filter bac luong theo ngach luong
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            //            DataTable lookupDataTable = objMasterService.GetHierarchical_Rex_Dm_Bacluong().Tables[0];
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
            //hashtableControls.Add(gvrex_Dienbien_Luong.Columns["Bhxh"], "");
            hashtableControls.Add(gvrex_Dienbien_Luong.Columns["Id_Quyetdinh"], "");

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullGrid(hashtableControls, gvrex_Dienbien_Luong))
                return false;

            try
            {
                this.DoClickEndEdit(dgrex_Dienbien_Luong);//dgrex_Dienbien_Luong.EmbeddedNavigator.Buttons.EndEdit.DoClick();
                objRexService.Update_Rex_Dienbien_Luong_Collection(ds_Dienbienluong);
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
                    if (""+gvrex_Dienbien_Luong.GetFocusedRowCellValue("Id_Bacluong")!="")
                    {
                        gvrex_Dienbien_Luong.SetFocusedRowCellValue("Id_Ngachluong", 
                            ((DataRowView)gridLookUp_Bacluong.GetDataSourceRowByKeyValue(e.Value)) ["Id_Ngachluong"]);
                        gvrex_Dienbien_Luong.SetFocusedRowCellValue("Heso", 
                            ((DataRowView)gridLookUp_Bacluong.GetDataSourceRowByKeyValue(e.Value)) ["Heso"]);
                         gvrex_Dienbien_Luong.SetFocusedRowCellValue("Luong_Thoathuan", 
                            ((DataRowView)gridLookUp_Bacluong.GetDataSourceRowByKeyValue(e.Value)) ["Luong_Thoathuan"]);
                    }
                    break;

                case "Id_Quyetdinh":
                    var dtr = gridLookUp_Quyetdinh.GetDataSourceRowByKeyValue(e.Value) as DataRowView;
                     gvrex_Dienbien_Luong.SetFocusedRowCellValue("Ngayky", dtr["Ngayky"]);
                    gvrex_Dienbien_Luong.SetFocusedRowCellValue("Ngay_Hieuluc_Batdau", ("" + dtr["Ngay_Batdau"] != "") ?  dtr["Ngay_Batdau"] : null);
                    gvrex_Dienbien_Luong.SetFocusedRowCellValue("Ngay_Hieuluc_Ketthuc", ("" + dtr["Ngay_ketthuc"] != "") ?  dtr["Ngay_ketthuc"] : null);
                    break;
            }
        }
        #endregion

        #region Quanhe_Giadinh

        private void DisplayInfo_Quanhegiadinh()
        {
            ds_Quanhe_Giadinh = objRexService.Get_All_Rex_Quanhe_Giadinh_ByNhanSu_Collection(nhan_su).ToDataSet();
            dgQuanhe_Giadinh.DataSource = ds_Quanhe_Giadinh;
            dgQuanhe_Giadinh.DataMember = ds_Quanhe_Giadinh.Tables[0].TableName;
            this.gvQuanhe_Giadinh.BestFitColumns();
        }

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
        private void gvQuanhe_Giadinh_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvQuanhe_Giadinh.SetFocusedRowCellValue(gvQuanhe_Giadinh.Columns["Id_Nhansu"], Id_Nhansu);
        }

        #endregion

        #region hop dong lao dong
        private void DisplayInfo_Hopdonglaodong()
        {
            if ("" + gvrex_Nhansu.GetFocusedRowCellValue("Id_Nhansu") != "")
            {
                DataSet dsHopdong_Laodong_By_Nhansu = objRexService.Get_All_Rex_Hopdong_Laodong_By_Nhansu(nhan_su.Id_Nhansu).ToDataSet();

                dgrex_Hopdong_Laodong_All.DataSource = dsHopdong_Laodong_By_Nhansu;
                dgrex_Hopdong_Laodong_All.DataMember = dsHopdong_Laodong_By_Nhansu.Tables[0].TableName;
            }
        }
        #endregion
        #endregion
        private void navBarControl1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
            switch (e.Link.ItemName)
            {
                case "navImport_Nhansu":
                    if (frmrex_Nhansu_Import == null || frmrex_Nhansu_Import.IsDisposed)
                        frmrex_Nhansu_Import = new Frmrex_Nhansu_Import();
                    frmrex_Nhansu_Import.ImpMode = Frmrex_Nhansu_Import.ImportMode.REX_NHANSU_IMP;
                    frmrex_Nhansu_Import.Id_Bophan = Id_Bophan;
                    frmrex_Nhansu_Import.ShowDialog();
                    DisplayInfo2();
                    break;

                case "navImport_Tkhoan_Nghang":
                    if (frmrex_Nhansu_Import == null || frmrex_Nhansu_Import.IsDisposed)
                        frmrex_Nhansu_Import = new Frmrex_Nhansu_Import();
                    frmrex_Nhansu_Import.ImpMode = Frmrex_Nhansu_Import.ImportMode.REX_NHANSU_ATM_IMP;
                    frmrex_Nhansu_Import.Id_Bophan = Id_Bophan;
                    frmrex_Nhansu_Import.ShowDialog();
                    DisplayInfo2();
                    break;

                case "navExport_Chamcong_ByBophan":
                     col = 1;
                     row = 8;
                    XlsTemplate = @"\Resources\xls\rex_chamcong_thang.xlsx";
                    dsExcelExp = objRexService.GetAll_Rex_Botri_Nhansu_Stt(Id_Bophan, DateTime.Now.Year, DateTime.Now.Month).ToDataSet();
                    if (dsExcelExp.Tables[0].Rows.Count > 0)
                    {
                        SaveFileDialog ofd = new SaveFileDialog();
                        ofd.Filter = "Excel Files|*.xls|Excel 2007 Files|*.xlsx";
                        ofd.FileName = "rex_chamcong_thang_"
                            + DateTime.Now.Year.ToString() + "_"
                            + DateTime.Now.Month.ToString() + "_"
                            + "_" + treeList_Bophan.FocusedNode.GetValue("Ma_Bophan");
                        if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            LastXlsPath = ofd.FileName;
                            (new System.Threading.Thread(
                                   new System.Threading.ThreadStart(ExcelExp)
                                   )).Start();
                        }
                    }
                    break;

                case "navExport_LLTN_ByBophan":
                     col = 1;
                     row = 8;
                     XlsTemplate = @"\Resources\xls\rex_danhsach_lltn.xlsx";
                     dsExcelExp = objRexService.Get_NhansuInfo_In_Hoso_ByBophan(Id_Bophan).ToDataSet();
                    if (dsExcelExp.Tables[0].Rows.Count > 0)
                    {
                        SaveFileDialog ofd = new SaveFileDialog();
                        ofd.Filter = "Excel Files|*.xls|Excel 2007 Files|*.xlsx";
                        ofd.FileName = "rex_danhsach_lltn_"
                            + DateTime.Now.Year.ToString() + "_"
                            + DateTime.Now.Month.ToString() + "_"
                            + "_" + treeList_Bophan.FocusedNode.GetValue("Ma_Bophan");
                        if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            LastXlsPath = ofd.FileName;
                            (new System.Threading.Thread(
                                   new System.Threading.ThreadStart(ExcelExp)
                                   )).Start();
                        }
                    }
                    break;

                case "navPrint_IDCard":
        //           Ecm.Rex.Forms.Frmrex_Nhansu_Dialog2 frm_Nhansu2 = new Frmrex_Nhansu_Dialog2();
        //            frm_Nhansu2.DisplayInfo();
        //            frm_Nhansu2.ShowDialog();
        //            if (frm_Nhansu2.Selected_Datarows != null)
        //            {
        //                DataSets.DsRex_Nhansu DsRpt = new Ecm.Rex.DataSets.DsRex_Nhansu();
        //                try
        //                {
        //                    foreach (DataRow dr in frm_Nhansu2.Selected_Datarows)
        //                        DsRpt.Tables[0].ImportRow(dr);
        //                }
        //                catch (Exception ex)
        //                {
        //#if DEBUG
        //                    MessageBox.Show(ex.Message);
        //#endif
        //                }
        //                PrintPreview(DsRpt);
        //            }
                    break;           
            
                case "navNhansu_Info_Edit":
                    if (!gvrex_Nhansu.IsDataRow(gvrex_Nhansu.FocusedRowHandle))
                    {
                        GoobizFrame.Windows.Forms.UserMessage.Show("Msg00126", new string[] { "" });
                        return;
                    }

                    focusedNode = treeList_Bophan.FocusedNode;

                    object id_nhansu = gvrex_Nhansu.GetFocusedRowCellValue("Id_Nhansu");
                    Frmrex_Nhansu_Info frmNhansu_Info = new Frmrex_Nhansu_Info(id_nhansu);
                    frmNhansu_Info.Text = "Cập nhật hồ sơ nhân viên";
                    frmNhansu_Info.EnablePrintPreview = EnablePrintPreview;
                    frmNhansu_Info.ShowDialog();
                    treeList_Bophan.FocusedNode = focusedNode;
                    this.DisplayInfo2();

                    break;

                    case "navNhansu_Info_Print":
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
                            GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(),"Exception");
                        }
                    break;

                    case "navNhansu_Info_PrintAll":
                    try
                    {

                        DataSet ds_NhansuInfo_In_Hoso = objRexService.Get_NhansuInfo_In_Hoso_ByBophan(Id_Bophan).ToDataSet();
                        DataSet ds_phucap = objRexService.Get_Phucap_In_Hoso_ByBophan(Id_Bophan).ToDataSet();
                        DataSet ds_qtrinh_dtao = objRexService.Get_Quatrinh_Daotao_In_Hoso_ByBophan(Id_Bophan).ToDataSet();
                        DataSet ds_qtrinh_ctac = objRexService.Get_Quatrinh_Congtac_In_Hoso_ByBophan(Id_Bophan).ToDataSet();
                        DataSet ds_qhe_gdinh = objRexService.Get_Quanhe_Giadinh_In_Hoso_ByBophan(Id_Bophan).ToDataSet();
                        DataSet ds_dienbien_luong = objRexService.Get_Dienbien_Luong_In_Hoso_ByBophan(Id_Bophan).ToDataSet();

                        NhansuInfoHelper.PrintSYLL(
                            ds_NhansuInfo_In_Hoso,
                            ds_phucap,
                            ds_qtrinh_dtao,
                            ds_qtrinh_ctac,
                            ds_qhe_gdinh,
                            ds_dienbien_luong,
                            this);

                    }
                    catch (Exception ex)
                    {
                        GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                    }
                    break;

                    case "navHopdonglaodong":
                        Frmrex_Hopdong_Laodong_ByNhansu frmrex_Hopdong_Laodong_ByNhansu = new Frmrex_Hopdong_Laodong_ByNhansu();
                        frmrex_Hopdong_Laodong_ByNhansu.Id_Nhansu = gvrex_Nhansu.GetFocusedRowCellValue("Id_Nhansu");
                        GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmrex_Hopdong_Laodong_ByNhansu);
                        frmrex_Hopdong_Laodong_ByNhansu.ShowDialog();
                    break;
            }
        }
        
        /// <summary>
        /// export excel
        /// </summary>
        void ExcelExp()
        {
            try
            {
                FileInfo newFile = new FileInfo(LastXlsPath);
                FileInfo template = new FileInfo(Application.StartupPath + XlsTemplate);

                ExcelPackage pck = new ExcelPackage(template);
                //Add the Content sheet
                //var ws = pck.Workbook.Worksheets.Add(""+lookUpEdit_Bophan.GetColumnValue("Ma_Bophan"));
                var ws = pck.Workbook.Worksheets["Sheet1"];
                ws.Name = "" + Id_Bophan;
                ws.View.ShowGridLines = true;

                #region CompanyName info
                ws.Cells[2, 3].Value = "" + dsHesochuongtrinh_Company.Tables[0].Select("ma_heso_chuongtrinh='CompanyName'")[0]["Heso"];

                try
                {
                    object logo = dsHesochuongtrinh_Company.Tables[0].Select("Nhom_Heso_Chuongtrinh = 'Company' and Ma_Heso_Chuongtrinh = 'CompanyLogo'")[0]["Heso"];
                    if ("" + logo != "")
                    {
                        byte[] imagedata = Convert.FromBase64String("" + logo);

                        //Read image data into a memory stream
                        MemoryStream ms = new MemoryStream(imagedata, 0, imagedata.Length);

                        ms.Write(imagedata, 0, imagedata.Length);

                        //Set image variable value using memory stream.
                        Image image = Image.FromStream(ms, true);

                        OfficeOpenXml.Drawing.ExcelPicture logoPic = null;
                        logoPic = ws.Drawings.AddPicture("Logo", image);
                        logoPic.From.Column = 0;
                        logoPic.From.Row = 0;
                        logoPic.SetSize(100, 100);
                    }

                }
                catch { }
                #endregion

                ws.Cells[5, 4].Value = treeList_Bophan.FocusedNode.GetDisplayText("Ten_Bophan");
                ws.Cells[6, 4].Value = string.Format("{0:MM/yyyy}", DateTime.Now);

                var range = ws.Cells[1, 1, 1, 100];
                var enumvalues = range.GetEnumerator();

               
                int i = 1;
                int end_index = 0;
                DevExpress.Utils.WaitDialogForm WaitDialogForm = new DevExpress.Utils.WaitDialogForm("Vui lòng chờ trong vài giât...", "Đang thực hiện");
                foreach (System.Data.DataRow r in dsExcelExp.Tables[0].Rows)
                {
                    col = 1;
                    enumvalues.Reset();
                    int i_header = 1;
                    while (enumvalues.MoveNext())
                    {
                        try
                        {
                            if ("" + enumvalues.Current.Value != "~!>")
                                ws.Cells[row + i, i_header].Value = "" + r["" + enumvalues.Current.Value];
                            else
                                end_index = (end_index == 0) ? i_header : end_index;
                        }
                        catch (Exception ex) {}

                        i_header++;
                    }

                    i++;
                }

                var erow = ws.Row(1);
                erow.Hidden = true;

                var border = ws.Cells[row, 1, row + i-1, end_index-1].Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                WaitDialogForm.Close();
                //ws.Cells[string.Format("A{0}:D{1}",new object[]{1, irow-1})].Style.Border.Le

                pck.SaveAs(newFile);
                //open file
                System.Diagnostics.Process.Start(LastXlsPath);
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
            }
        }

        private void gridLookUp_Chucvu_Botri_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                  "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Chucvu_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Chucvu").GetValue(dialog, null)
                    as Ecm.WebReferences.MasterService.Rex_Dm_Chucvu;

                var ds_Chucvu = objMasterService.Get_All_Rex_Dm_Chucvu_Collection().ToDataSet();
                gridLookUp_Chucvu_Botri.DataSource = ds_Chucvu.Tables[0];
                if (SelectedObject != null)
                {
                    gvrex_Botri_Nhansu.SetFocusedRowCellValue("Id_Chucvu", SelectedObject.Id_Chucvu);
                }
            }
        }

        private void gridLookUp_Bacluong_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                try
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
                catch (Exception ex)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(),"");
                }
            }
        }

        private void gridLookUp_Quyetdinh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                   "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Quyetdinh_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Quyetdinh").GetValue(dialog, null)
                    as Ecm.WebReferences.MasterService.Rex_Dm_Quyetdinh;

                var ds_Quyetdinh = objMasterService.Get_All_Rex_Dm_Quyetdinh_Collection();
                gridLookUp_Quyetdinh.DataSource = ds_Quyetdinh.ToDataSet().Tables[0];
                gridLookUp_Quyetdinh_Botri.DataSource = ds_Quyetdinh.ToDataSet().Tables[0];
                if (SelectedObject != null)
                {
                    gvrex_Dienbien_Luong.SetFocusedRowCellValue("Id_Quyetdinh", SelectedObject.Id_Quyetdinh);
                }
            }
        }

        private void gridLookUp_Quyetdinh_Botri_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                   "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Quyetdinh_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Quyetdinh").GetValue(dialog, null)
                    as Ecm.WebReferences.MasterService.Rex_Dm_Quyetdinh;

                var ds_Quyetdinh = objMasterService.Get_All_Rex_Dm_Quyetdinh_Collection().ToDataSet();
                gridLookUp_Quyetdinh.DataSource = ds_Quyetdinh.Tables[0];
                gridLookUp_Quyetdinh_Botri.DataSource = ds_Quyetdinh.Tables[0];
                if (SelectedObject != null)
                {
                    gvrex_Botri_Nhansu.SetFocusedRowCellValue("Id_Quyetdinh", SelectedObject.Id_Quyetdinh);
                }
            }
        }

        private void gridLookUp_Quanhe_Giadinh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                System.Windows.Forms.Form dialog = GoobizFrame.Windows.MdiUtils.ThemeSettings.ShowDialogOfMetaData("Ecm.MasterService.dll",
                   "Ecm.MasterService.Forms.Rex.Frmrex_Dm_Quanhe_Giadinh_Add", this);
                if (dialog == null)
                    return;
                var SelectedObject = dialog.GetType().GetProperty("Selected_Rex_Dm_Quanhe_Giadinh").GetValue(dialog, null)
                    as Ecm.WebReferences.MasterService.Rex_Dm_Quanhe_Giadinh;

                var ds_Quanhe_Giadinh = objMasterService.Get_All_Rex_Dm_Quanhe_Giadinh_Collection();
                gridLookUp_Quanhe_Giadinh.DataSource = ds_Quanhe_Giadinh.ToDataSet().Tables[0];
                if (SelectedObject != null)
                {
                    gvQuanhe_Giadinh.SetFocusedRowCellValue("Id_Quanhe_Giadinh", SelectedObject.Id_Quanhe_Giadinh);
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


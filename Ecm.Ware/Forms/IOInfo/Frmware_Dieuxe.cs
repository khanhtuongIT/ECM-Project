using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GoobizFrame.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Data.Common;

namespace Ecm.Ware.Forms
{
    public partial class Frmware_Dieuxe : GoobizFrame.Windows.Forms.FormUpdateWithToolbar
    {
        public Ecm.WebReferences.Classes.WareService objWareService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.WareService>();
        public Ecm.WebReferences.Classes.MasterService objMasterService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.MasterService>();
        public Ecm.WebReferences.Classes.RexService objRexService = Ecm.WebReferences.Instance.GetService<Ecm.WebReferences.Classes.RexService>();
        DataSet ds_Dieuxe_Xuatkho = new DataSet();
        DataSet ds_Xkbanhang_Chitiet = new DataSet();
        DataSet ds_Hanghoa_Ban;
        DataSet dsDonvitinh = new DataSet();
        DataSet ds_Dieuxe;
        DataSet ds_Dieuxe_Chitet;
        DataSet ds_Dieuxe_Chitet_tmp;
        object identity;
        object id_xuatkho;
        object id_nhansu_current;
        DataSet ds_Role_User;
        bool in_kho = false;
        object guid_dieuxe_tmp;
        bool flag = false;

        public Frmware_Dieuxe()
        {
            InitializeComponent();
            //date mask
            this.gridDate_Ngay_Sx.MinValue = new DateTime(2000, 01, 01);
            this.gridDate_Han_Sd.MinValue = new DateTime(2000, 01, 01);
            //reset lookup edit as delete value
            //LocationId_Cuahang_Ban = GoobizFrame.Windows.MdiUtils.ThemeSettings.GetLocation("Id_Cuahang_Ban");
            id_nhansu_current = GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentId_Nhansu();
            this.item_Query.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.item_Verify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.item_Test.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ds_Role_User = objMasterService.GetRole_System_Name_ById_User(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUserId()).ToDataSet();
            dtThangnam.DateTime = DateTime.Now;
            this.DisplayInfo();
        }

        void LoadMasterData()
        {
            ds_Hanghoa_Ban = objMasterService.Get_All_Ware_Dm_Hanghoa_Ban().ToDataSet();
            gridLookUpEdit_Hanghoa_Ban.DataSource = ds_Hanghoa_Ban.Tables[0];
            gridLookUpEdit_Ma_Hanghoa_Ban.DataSource = ds_Hanghoa_Ban.Tables[0];
            gridLookUpEdit_Donvitinh.DataSource = objMasterService.Get_All_Ware_Dm_Donvitinh().ToDataSet().Tables[0];
            lookUpEdit_Khuvuc.Properties.DataSource = objMasterService.Ware_Dm_Cuahang_Ban_Select_Sale().ToDataSet().Tables[0];
            //ds_collection = objMasterService.Get_All_Ware_Dm_Khachhang().ToDataSet();

            DataSet ds_collection = objMasterService.GetAll_Ware_Dm_Xe().ToDataSet();
            gridLookupEdit_Dm_Xe.DataSource = ds_collection.Tables[0];
            lookupEdit_Dm_Xe.Properties.DataSource = ds_collection.Tables[0];

            //ds_collection = objRexService.Get_Rex_Nhansu_ByBoPhan_Collection(6).ToDataSet();
            //gridLookupEdit_Taixe.DataSource = ds_collection.Tables[0];
            //lookUpEdit_Taixe.Properties.DataSource = ds_collection.Tables[0];

            ds_collection = objMasterService.GetRole_System_Name_ById_User(GoobizFrame.Windows.MdiUtils.ThemeSettings.GetCurrentUserId()).ToDataSet();
            DataSet dsCuahang_Ban = objMasterService.Ware_Dm_Cuahang_Ban_Select_Kho().ToDataSet();
            gridLookupEdit_Kho_Chitiet.DataSource = dsCuahang_Ban.Tables[0];
            //gridLookUpEdit_Cuahang_Ban_Xuat.DataSource = dsCuahang_Ban.Tables[0];
            //if (ds_collection.Tables[0].Rows.Count > 0 &&
            //    "" + ds_collection.Tables[0].Rows[0]["Role_System_Name"] == "Administrators")
            //{
            DataTable temp = dsCuahang_Ban.Tables[0].Copy();
            DataRow row = temp.NewRow();
            row["Id_Cuahang_Ban"] = -1;
            row["Ma_Cuahang_Ban"] = "All";
            row["Ten_Cuahang_Ban"] = "Tất cả";
            temp.Rows.Add(row);
            //lookUpEdit_Kho_View.Properties.DataSource = temp;
            loolupEdit_Kho_Print.Properties.DataSource = temp;
            //lookUpEdit_Kho_View.EditValue = Convert.ToInt64(-1);
            loolupEdit_Kho_Print.EditValue = Convert.ToInt64(-1);
            // }
            //  else
            // {
            //DataSet dsCuahang = objWareService.Get_Ware_Ql_Cuahang_Ban_By_Id_Nhansu(id_nhansu_current, true).ToDataSet();
            //lookUpEdit_Kho_View.Properties.DataSource = dsCuahang.Tables[0];


            //gridLookupEdit_Xuatkho.DataSource = objWareService.Ware_Doituong_SelectByCuahang_ByDate(-1, dtThangnam.DateTime, null).ToDataSet().Tables[0];
            // lookUpEdit_Khuvuc.Properties.DataSource = objMasterService.Ware_Dm_Cuahang_Ban_Select_Sale().ToDataSet().Tables[0];
            //lookUpEdit_Khuvuc.Properties.DataSource = objMasterService.Ware_Dm_Cuahang_Ban_Select_Sale().ToDataSet().Tables[0];
            // }
        }

        void Reload_Chungtu()
        {

            ds_Dieuxe = objWareService.Get_All_Ware_Dieuxe(dtThangnam.EditValue, -1).ToDataSet();
            dgDieuxe.DataSource = ds_Dieuxe;
            dgDieuxe.DataMember = ds_Dieuxe.Tables[0].TableName;
            gvDieuxe.BestFitColumns();
            this.DataBindingControl();
            this.ChangeStatus(true);
        }

        #region Event Override

        public override void DisplayInfo()
        {
            try
            {
                guid_dieuxe_tmp = Guid.Empty;
                ResetText();
                LoadMasterData();
                Reload_Chungtu();
                DisplayInfo_Details();
                ds_Dieuxe_Chitet_tmp = null;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }
        }

        void DisplayInfo_Details()
        {
            try
            {
                DataBindingControl();
                identity = gvDieuxe.GetFocusedRowCellValue("Id_Dieuxe");
                id_xuatkho = gvXuatkho.GetFocusedRowCellValue("Sochungtu");

                ds_Dieuxe_Xuatkho = objWareService.Ware_Xuatkho_Banhang_ById_Dieuxe(identity).ToDataSet();
                dgXuatkho.DataSource = ds_Dieuxe_Xuatkho.Tables[0];

                ds_Dieuxe_Chitet = objWareService.Ware_Dieuxe_Chitiet_SelectBy_Id_Dieuxe_Group(identity).ToDataSet();
                ds_Dieuxe_Chitet.Tables[0].Columns.Add("Chon", typeof(bool));
                foreach (DataRow row in ds_Dieuxe_Chitet.Tables[0].Rows)
                {
                    row["Chon"] = true;
                }
                dgDieuxe_Chitiet.DataSource = ds_Dieuxe_Chitet.Tables[0];
                //gridColumn24.GroupIndex = -1;
                //setTen_Cuahang_Ban();
            }
            catch { }
        }

        void setTen_Cuahang_Ban()
        {
            //DataSet dsDieuxe_Cuahang = objWareService.Ware_Dieuxe_Cuahang_Ban_SelectBy_Id_Dieuxe(identity).ToDataSet();
            //int lastrow = 0;
            //string ten_cuahang = "";
            //foreach (DataRowView row in dsDieuxe_Cuahang.Tables[0].DefaultView)
            //{
            //    lastrow++;
            //    ten_cuahang += row["Ten_Cuahang_Ban"] + "";
            //    if (lastrow <= (dsDieuxe_Cuahang.Tables[0].Rows.Count - 1))
            //        ten_cuahang += ", ";
            //}
            //lookUpEdit_Khuvuc.Text = ten_cuahang;
        }

        public override void ClearDataBindings()
        {
            lookupEdit_Dm_Xe.DataBindings.Clear();
            lookUpEdit_Taixe.DataBindings.Clear();
            dateEdit_Ngaydi.DataBindings.Clear();
            txtQuangduongdi.DataBindings.Clear();
            richTextBoxGhichu.DataBindings.Clear();
            txtTen_Taixe.DataBindings.Clear();
            lookUpEdit_Khuvuc.DataBindings.Clear();
        }

        public override void DataBindingControl()
        {
            try
            {
                ClearDataBindings();
                lookupEdit_Dm_Xe.DataBindings.Add("EditValue", ds_Dieuxe, ds_Dieuxe.Tables[0].TableName + ".Id_Xe");
                lookUpEdit_Taixe.DataBindings.Add("EditValue", ds_Dieuxe, ds_Dieuxe.Tables[0].TableName + ".Id_Taixe");
                dateEdit_Ngaydi.DataBindings.Add("EditValue", ds_Dieuxe, ds_Dieuxe.Tables[0].TableName + ".Ngay_Di");
                txtQuangduongdi.DataBindings.Add("EditValue", ds_Dieuxe, ds_Dieuxe.Tables[0].TableName + ".Quangduong_Di");
                richTextBoxGhichu.DataBindings.Add("Text", ds_Dieuxe, ds_Dieuxe.Tables[0].TableName + ".Ghichu");
                txtTen_Taixe.DataBindings.Add("Text", ds_Dieuxe, ds_Dieuxe.Tables[0].TableName + ".Ten_Taixe");
                lookUpEdit_Khuvuc.DataBindings.Add("EditValue", ds_Dieuxe, ds_Dieuxe.Tables[0].TableName + ".Id_Cuahang_Ban");
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }
        }

        public override void ChangeStatus(bool editTable)
        {
            lookupEdit_Dm_Xe.Properties.ReadOnly = editTable;
            lookUpEdit_Khuvuc.Properties.ReadOnly = editTable;
            lookUpEdit_Taixe.Properties.ReadOnly = editTable;
            dateEdit_Ngaydi.Properties.ReadOnly = editTable;
            txtQuangduongdi.Properties.ReadOnly = editTable;
            richTextBoxGhichu.ReadOnly = editTable;
            txtTen_Taixe.Properties.ReadOnly = editTable;

            dgXuatkho.EmbeddedNavigator.Enabled = !editTable;
            dgDieuxe_Chitiet.EmbeddedNavigator.Enabled = !editTable;

            //gvware_Dieuxe_Chitiet.OptionsBehavior.Editable = !editTable;
            //gvware_Dieuxe_Chitiet.OptionsBehavior.ReadOnly = editTable;
            gvXuatkho.OptionsBehavior.ReadOnly = editTable;
            gridColumn_Chon_Chungtu.Visible = !editTable;
            gridColumn_Delete_Chitiet.Visible = !editTable;
            if (!editTable)
                gridColumn_Delete_Chitiet.VisibleIndex = gvware_Dieuxe_Chitiet.VisibleColumns.Count;
            gridColumn_Chon_Print.Visible = editTable;
            if (editTable)
                gridColumn_Chon_Print.VisibleIndex = 0;
            //gridColumn23.OptionsColumn.AllowEdit = !editTable;
            //gridColumn23.Visible = !editTable;
            dockPanel1_Container.Enabled = editTable;
            tableLayoutPanel3.Enabled = editTable;

            gridColumn_Soluong.OptionsColumn.AllowEdit = !editTable;
            gridColumn_Kho.OptionsColumn.AllowEdit = !editTable;
            gridColumn_Mahang.OptionsColumn.AllowEdit = !editTable;
            gridColumn_Chon_Print.OptionsColumn.AllowEdit = true;
        }

        public override void ResetText()
        {
            lookupEdit_Dm_Xe.EditValue = null;
            lookUpEdit_Taixe.EditValue = null;
            dateEdit_Ngaydi.EditValue = null;
            txtQuangduongdi.EditValue = null;
            richTextBoxGhichu.Text = "";
            txtTen_Taixe.Text = "";

            lookUpEdit_Khuvuc.EditValue = null;
            this.ds_Xkbanhang_Chitiet = objWareService.Get_All_Ware_Xuatkho_Banhang_Chitiet_By_Id_Xuatkho_Banhang(0).ToDataSet();
            this.dgDieuxe_Chitiet.DataSource = ds_Xkbanhang_Chitiet.Tables[0];
            ds_Dieuxe_Xuatkho = objWareService.Ware_Xuatkho_Banhang_ById_Dieuxe(-1).ToDataSet();
            dgXuatkho.DataSource = ds_Dieuxe_Xuatkho.Tables[0];
        }

        public object InsertObject()
        {
            try
            {
                Ecm.WebReferences.WareService.Ware_Dieuxe _Ware_Dieuxe = new Ecm.WebReferences.WareService.Ware_Dieuxe();
                _Ware_Dieuxe.Id_Xuatkho_Banhang = null;
                _Ware_Dieuxe.Id_Xe = lookupEdit_Dm_Xe.EditValue;
                _Ware_Dieuxe.Id_Nhansu_Dieuxe = id_nhansu_current;
                _Ware_Dieuxe.Id_Taixe = lookUpEdit_Taixe.EditValue;
                _Ware_Dieuxe.Ngay_Di = dateEdit_Ngaydi.EditValue;
                _Ware_Dieuxe.Ngay_Den = null;
                _Ware_Dieuxe.Ngay_Ve = null;
                _Ware_Dieuxe.Quangduong_Di = txtQuangduongdi.EditValue;
                _Ware_Dieuxe.Ghichu = "" + richTextBoxGhichu.Text;
                _Ware_Dieuxe.Ten_Taixe = "" + txtTen_Taixe.Text;
                _Ware_Dieuxe.Id_Cuahang_Ban = lookUpEdit_Khuvuc.EditValue;
                identity = objWareService.Insert_Ware_Dieuxe(_Ware_Dieuxe);

                if (identity != null)
                {
                    DoClickEndEdit(dgXuatkho);
                    DataSet dsDieuxe_Xuatkho_temp = objWareService.Get_Schema_Ware_Dieuxe_Xuatkho().ToDataSet();
                    foreach (DataRow dr in ds_Dieuxe_Xuatkho.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(dr["Chon"]) == true)
                        {
                            DataRow Row = dsDieuxe_Xuatkho_temp.Tables[0].NewRow();
                            Row["Id_Dieuxe"] = identity;
                            Row["Sochungtu"] = dr["Sochungtu"];
                            dsDieuxe_Xuatkho_temp.Tables[0].Rows.Add(Row);
                        }
                    }
                    objWareService.Update_Ware_Dieuxe_Xuatkho_Collection(dsDieuxe_Xuatkho_temp);

                    //DoClickEndEdit(dgDieuxe_Chitiet);
                    ds_Dieuxe_Chitet = objWareService.Ware_Dieuxe_Chitiet_Tmp_SelectAll_By_Guid_Dieuxe(guid_dieuxe_tmp).ToDataSet();
                    DataRow[] dtr_tmp;
                    foreach (DataRow dr in ds_Dieuxe_Chitet.Tables[0].Rows)
                    {
                        //if (dr.RowState == DataRowState.Added)
                        dr.SetAdded();
                        dr["Id_Dieuxe"] = identity;
                        dtr_tmp = ds_Dieuxe_Chitet_tmp.Tables[0].Select("Id_Hanghoa_Ban = " + dr["Id_Hanghoa_Ban"]);
                        dr["Id_Cuahang_Ban"] = dtr_tmp[0]["Id_Cuahang_Ban"];
                    }
                    ds_Dieuxe_Chitet.Tables[0].Columns.Remove("Guid_Dieuxe");
                    objWareService.Update_Ware_Dieuxe_Chitiet_Collection2(ds_Dieuxe_Chitet);
                    objWareService.Ware_Dieuxe_Chitiet_Tmp_Delete();
                }
                return true;
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                return false;
            }
        }

        public object UpdateObject()
        {
            try
            {
                Ecm.WebReferences.WareService.Ware_Dieuxe _Ware_Dieuxe = new Ecm.WebReferences.WareService.Ware_Dieuxe();
                _Ware_Dieuxe.Id_Dieuxe = identity;
                _Ware_Dieuxe.Id_Xuatkho_Banhang = null;
                _Ware_Dieuxe.Id_Xe = lookupEdit_Dm_Xe.EditValue;
                _Ware_Dieuxe.Id_Nhansu_Dieuxe = id_nhansu_current;
                _Ware_Dieuxe.Id_Taixe = lookUpEdit_Taixe.EditValue;
                _Ware_Dieuxe.Ngay_Di = dateEdit_Ngaydi.EditValue;
                _Ware_Dieuxe.Ngay_Den = null;
                _Ware_Dieuxe.Ngay_Ve = null;
                _Ware_Dieuxe.Quangduong_Di = "" + txtQuangduongdi.EditValue;
                _Ware_Dieuxe.Ghichu = "" + richTextBoxGhichu.Text;
                _Ware_Dieuxe.Ten_Taixe = "" + txtTen_Taixe.Text;
                _Ware_Dieuxe.Id_Cuahang_Ban = lookUpEdit_Khuvuc.EditValue;
                objWareService.Update_Ware_Dieuxe(_Ware_Dieuxe);

                if (identity != null)
                {
                    DoClickEndEdit(dgXuatkho);
                    DataSet dsDieuxe_Xuatkho_temp = objWareService.Get_Schema_Ware_Dieuxe_Xuatkho().ToDataSet();
                    objWareService.Ware_Dieuxe_Xuatkho_Delete_ById_Dieuxe(identity);
                    foreach (DataRow dr in ds_Dieuxe_Xuatkho.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(dr["Chon"]) == true)
                        {
                            DataRow Row = dsDieuxe_Xuatkho_temp.Tables[0].NewRow();
                            Row["Id_Dieuxe"] = identity;
                            Row["Sochungtu"] = dr["Sochungtu"];
                            dsDieuxe_Xuatkho_temp.Tables[0].Rows.Add(Row);
                        }
                    }
                    objWareService.Update_Ware_Dieuxe_Xuatkho_Collection(dsDieuxe_Xuatkho_temp);
                    if (ds_Dieuxe_Chitet_tmp != null)
                    {
                        ds_Dieuxe_Chitet_tmp = objWareService.Ware_Dieuxe_Chitiet_Tmp_SelectAll_By_Guid_Dieuxe(guid_dieuxe_tmp).ToDataSet();
                        foreach (DataRow dr in ds_Dieuxe_Chitet_tmp.Tables[0].Rows)
                        {
                            //if (dr.RowState == DataRowState.Added)
                            dr["Id_Dieuxe"] = identity;
                        }
                        ds_Dieuxe_Chitet_tmp.Tables[0].Columns.Remove("Guid_Dieuxe");
                        objWareService.Update_Ware_Dieuxe_Chitiet_Collection2(ds_Dieuxe_Chitet_tmp);
                        objWareService.Ware_Dieuxe_Chitiet_Tmp_Delete();
                    }
                    else
                    {
                        DoClickEndEdit(dgDieuxe_Chitiet);
                        foreach (DataRow dr in ds_Dieuxe_Chitet.Tables[0].Rows)
                        {
                            if (dr.RowState == DataRowState.Added)
                                dr["Id_Dieuxe"] = identity;
                        }
                        objWareService.Update_Ware_Dieuxe_Chitiet_Collection2(ds_Dieuxe_Chitet);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show(ex.Message, ex.ToString(), "Exception");
                return false;
            }
        }

        public object DeleteObject()
        {
            Ecm.WebReferences.WareService.Ware_Dieuxe _Ware_Dieuxe = new Ecm.WebReferences.WareService.Ware_Dieuxe();
            _Ware_Dieuxe.Id_Dieuxe = identity;
            return objWareService.Delete_Ware_Dieuxe(_Ware_Dieuxe);
        }

        public override bool PerformAdd()
        {
            //if (Convert.ToInt64(lookUpEdit_Kho_View.EditValue) == -1)
            //{
            //    GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn kho, vui lòng chọn lại");
            //    lookUpEdit_Kho_View.Focus();
            //    return false;
            //}            
            try
            {
                this.ResetText();
                dateEdit_Ngaydi.EditValue = DateTime.Now;
                ClearDataBindings();
                this.ChangeStatus(false);
                ds_Dieuxe_Xuatkho = objWareService.Ware_Xuatkho_Banhang_ById_Dieuxe(-1).ToDataSet();
                dgXuatkho.DataSource = ds_Dieuxe_Xuatkho.Tables[0];

                ds_Dieuxe_Chitet = objWareService.Ware_Dieuxe_Chitiet_SelectBy_Id_Dieuxe(-1).ToDataSet();
                dgDieuxe_Chitiet.DataSource = ds_Dieuxe_Chitet.Tables[0];
                lookupEdit_Dm_Xe.Focus();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public override bool PerformEdit()
        {
            try
            {
                //if (Convert.ToInt64(lookUpEdit_Kho_View.EditValue) == -1)
                //{
                //    GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn kho, vui lòng chọn lại");
                //    lookUpEdit_Kho_View.Focus();
                //    return false;
                //}
                if (gvDieuxe.GetFocusedRowCellValue("Id_Dieuxe") == null)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn phiếu điều xe, vui lòng chọn lại");
                    return false;
                }
                this.ChangeStatus(false);
                ds_Dieuxe_Xuatkho.Tables[0].Columns.Add("Chon", typeof(bool));
                foreach (DataRow row in ds_Dieuxe_Xuatkho.Tables[0].Rows)
                {
                    row["Chon"] = true;
                }
                gvware_Dieuxe_Chitiet.ExpandAllGroups();
                ds_Dieuxe_Chitet = objWareService.Ware_Dieuxe_Chitiet_SelectBy_Id_Dieuxe(identity).ToDataSet();
                dgDieuxe_Chitiet.DataSource = ds_Dieuxe_Chitet.Tables[0];
                lookupEdit_Dm_Xe.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }



        public override bool PerformCancel()
        {
            this.FormState = GoobizFrame.Windows.Forms.FormState.View;
            this.DisplayInfo();
            return true;
        }

        public override bool PerformSave()
        {
            try
            {
                bool success = false;
                GoobizFrame.Windows.Public.OrderHashtable hashtableControls = new GoobizFrame.Windows.Public.OrderHashtable();
                hashtableControls.Add(lookupEdit_Dm_Xe, lblXe.Text);
                //hashtableControls.Add(lookUpEdit_Taixe, lblTaixe.Text);
                if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                    return false;

                if (this.FormState == GoobizFrame.Windows.Forms.FormState.Add)
                    success = (bool)this.InsertObject();
                else if (this.FormState == GoobizFrame.Windows.Forms.FormState.Edit)
                    success = (bool)this.UpdateObject();
                if (success)
                {
                    this.DisplayInfo();
                }
                return success;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public override bool PerformDelete()
        {
            try
            {
                if (gvDieuxe.GetFocusedRowCellValue("Id_Dieuxe") == null)
                {
                    GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn phiếu điều xe, vui lòng chọn lại");
                    return false;
                }
                if (ds_Role_User.Tables[0].Rows.Count > 0 && ds_Role_User.Tables[0].Select("Role_System_Name = 'Administrators' ", "").Length > 0)
                {
                    if (GoobizFrame.Windows.Forms.UserMessage.Show("Msg00004", new string[]  {
                      GoobizFrame.Windows.Forms.UserMessage.GetTableDescription("Ware_Dieuxe"),
                      GoobizFrame.Windows.Forms.UserMessage.GetTableRelations("Ware_Dieuxe")   }) == DialogResult.Yes)
                    {
                        try
                        {
                            this.DeleteObject();
                        }
                        catch (Exception ex)
                        {
                            GoobizFrame.Windows.MdiUtils.Validator.CheckReferencedRecord(ex.Message, "");
                        }
                        this.DisplayInfo();
                    }
                }
                else
                {
                    GoobizFrame.Windows.Forms.UserMessage.Show("ACCESS_DENIED", new string[] { });
                    return true;
                }
            }
            catch (Exception ex)
            {
#if (DEBUG)
                MessageBox.Show(ex.Message);
                return false;
#endif
            }
            return base.PerformDelete();
        }

        public override bool PerformPrintPreview()
        {
            if (gvDieuxe.GetFocusedRowCellValue("Id_Dieuxe") == null)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn phiếu điều xe, vui lòng chọn lại");
                return false;
            }
            try
            {
                ds_Dieuxe_Chitet = objWareService.Ware_Dieuxe_Chitiet_SelectBy_Id_Dieuxe_Group(identity).ToDataSet();
                DataSets.DsHdbanhang_Xuatkho dsWare_Xuat_Vattu = new Ecm.Ware.DataSets.DsHdbanhang_Xuatkho();
                Reports.rptDieuxe _rptDieuxe = new Reports.rptDieuxe();
                GoobizFrame.Windows.Forms.FrmPrintPreview frmPrintPreview = new GoobizFrame.Windows.Forms.FrmPrintPreview();
                frmPrintPreview.Report = _rptDieuxe;
                _rptDieuxe.DataSource = dsWare_Xuat_Vattu;

                _rptDieuxe.xrTableCell_Ngaydi.Text = dateEdit_Ngaydi.Text;
                _rptDieuxe.xrTableCell_Ngay.Text = DateTime.Now.Day.ToString();
                _rptDieuxe.xrTableCell_Thang.Text = DateTime.Now.Month.ToString();
                _rptDieuxe.xrTableCell_Nam.Text = DateTime.Now.Year.ToString();
                _rptDieuxe.xrTableCell_Taixe.Text = txtTen_Taixe.Text;
                _rptDieuxe.xrTableCell_SoLenh_Dieuxe.Text = gvDieuxe.GetFocusedRowCellValue("Id_Dieuxe").ToString();
                _rptDieuxe.xrTableCell_Xe.Text = lookupEdit_Dm_Xe.Text;
                _rptDieuxe.xrTableCell_Ghichu.Text = richTextBoxGhichu.Text;
                _rptDieuxe.xrTableCell_Noiden.Text = lookUpEdit_Khuvuc.Text;
                _rptDieuxe.xrTableRow_Kho.Visible = false;
                //Ware_Xuat_Vattu_Chitiet
                //for (int i = 0; i < gvware_Xuat_Hanghoa_Ban_Chitiet.RowCount; i++)
                //{
                //    if (gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowExpanded(i))
                //        continue;
                //    DataRow rWare_Xuat_Vattu_Chitiet = dsWare_Xuat_Vattu.Tables[0].NewRow();
                //    rWare_Xuat_Vattu_Chitiet["stt"] = i + 1;
                //    //rWare_Xuat_Vattu_Chitiet["id_xuat_hh_mua"] = gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, "Id_Dieuxe");
                //    //rWare_Xuat_Vattu_Chitiet["id_xuat_hh_mua"] = identity;
                //    rWare_Xuat_Vattu_Chitiet["Ma_Hanghoa_Ban"] = gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, gridColumn7);
                //    rWare_Xuat_Vattu_Chitiet["Ten_Hanghoa_Ban"] = gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, gridColumn14);
                //    rWare_Xuat_Vattu_Chitiet["Ten_Donvitinh"] = gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, "Id_Donvitinh");
                //    rWare_Xuat_Vattu_Chitiet["Soluong"] = Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, "Soluong"));
                //    dsWare_Xuat_Vattu.Tables[0].Rows.Add(rWare_Xuat_Vattu_Chitiet);
                //}
                int i = 1;
                foreach (DataRow dr in ds_Dieuxe_Chitet.Tables[0].Rows)
                {
                    DataRow drnew = dsWare_Xuat_Vattu.Tables[0].NewRow();
                    foreach (DataColumn dc in ds_Dieuxe_Chitet.Tables[0].Columns)
                    {
                        try
                        {
                            drnew[dc.ColumnName] = dr[dc.ColumnName];
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    drnew["Ten_Hanghoa_Ban"] = dr["Ten_Hanghoa_Ban"];
                    drnew["Ma_Hanghoa_Ban"] = dr["Ma_Hanghoa_Ban"];
                    drnew["Ten_Donvitinh"] = dr["Ten_Donvitinh"];
                    drnew["DVT_Quydoi"] = dr["DVT_Quydoi"];
                    drnew["DVT_Quydoi_Text"] = dr["DVT_Quydoi_Text"];
                    drnew["Stt"] = i++;
                    dsWare_Xuat_Vattu.Tables[0].Rows.Add(drnew);
                }

                dsWare_Xuat_Vattu.AcceptChanges();
                #region Set he so ctrinh - logo, ten cty
                DataSet dsHeso_Chuongtrinh;
                using (dsHeso_Chuongtrinh = objMasterService.Get_Rex_Dm_Heso_Chuongtrinh_Collection3().ToDataSet())
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

                    _rptDieuxe.xrc_CompanyName.DataBindings.Add(
                        new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyName"));
                    _rptDieuxe.xrc_CompanyAddress.DataBindings.Add(
                        new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyAddress"));
                    //rptXuat_Vattu.xrPic_Logo.DataBindings.Add(
                    //    new DevExpress.XtraReports.UI.XRBinding("Image", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyLogo"));
                }
                #endregion

                _rptDieuxe.CreateDocument();
                GoobizFrame.Windows.Forms.ReportOptions oReportOptions = GoobizFrame.Windows.Forms.ReportOptions.GetReportOptions(_rptDieuxe);
                if (Convert.ToBoolean(oReportOptions.PrintPreview))
                {
                    frmPrintPreview.Text = "In Phiếu điều xe";//oReportOptions.Caption;
                    frmPrintPreview.printControl1.PrintingSystem = _rptDieuxe.PrintingSystem;
                    frmPrintPreview.MdiParent = this.MdiParent;
                    frmPrintPreview.Show();
                    frmPrintPreview.Activate();
                }
                else
                {
                    var reportPrintTool = new DevExpress.XtraReports.UI.ReportPrintTool(_rptDieuxe);
                    reportPrintTool.Print();
                }
                DisplayInfo();
            }
            catch (Exception ex)
            { ex.ToString(); }
            return true;
        }

        #endregion

        #region Even

        private void gridLookUpEdit_Hanghoa_Ban_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
                {
                    Ecm.MasterTables.Forms.Ware.Frmware_Dm_Hanghoa_Ban_Dialog frmware_Dm_Hanghoa_Ban_Dialog = new Ecm.MasterTables.Forms.Ware.Frmware_Dm_Hanghoa_Ban_Dialog();
                    frmware_Dm_Hanghoa_Ban_Dialog.Text = "Hàng hóa";
                    GoobizFrame.Windows.MdiUtils.ThemeSettings.SetDialogShow(frmware_Dm_Hanghoa_Ban_Dialog);
                    frmware_Dm_Hanghoa_Ban_Dialog.ShowDialog();

                    if (frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows != null
                        && frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows.Length > 0)
                    {
                        gvware_Dieuxe_Chitiet.SetFocusedRowCellValue(gvware_Dieuxe_Chitiet.Columns["Id_Hanghoa_Ban"]
                            , frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[0]["Id_Hanghoa_Ban"]);
                        gvware_Dieuxe_Chitiet.SetFocusedRowCellValue(gvware_Dieuxe_Chitiet.Columns["Id_Donvitinh"]
                            , frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[0]["Id_Donvitinh"]);
                        gvware_Dieuxe_Chitiet.SetFocusedRowCellValue(gvware_Dieuxe_Chitiet.Columns["Dongia_Ban"]
                            , frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[0]["Dongia_Ban"]);
                        //object Id_Cuahang_Ban = gridView1.GetFocusedRowCellValue("Id_Cuahang_Ban");
                        if (frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows.Length > 1)
                        {
                            for (int i = 1; i < frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows.Length; i++)
                            {
                                DataRow nrow = ds_Xkbanhang_Chitiet.Tables[0].NewRow();
                                //nrow["Id_Cuahang_Ban"] = Id_Cuahang_Ban;
                                nrow["Id_Hanghoa_Ban"] = frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[i]["Id_Hanghoa_Ban"];
                                nrow["Id_Donvitinh"] = frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[i]["Id_Donvitinh"];
                                nrow["Dongia"] = frmware_Dm_Hanghoa_Ban_Dialog.SelectedRows[i]["Dongia_Ban"];
                                ds_Xkbanhang_Chitiet.Tables[0].Rows.Add(nrow);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
#if (DEBUG)
                MessageBox.Show(ex.Message);
#endif
            }
        }

        public decimal Get_Soluong_Ton_Quydoi(object Id_Hanghoa_Ban, object Id_Donvitinh, object Id_Xuatkho_Banhang_Chitiet)
        {
            try
            {
                var _Id_Donvitinh = ds_Hanghoa_Ban.Tables[0].Select(string.Format("Id_Hanghoa_Ban={0}", Id_Hanghoa_Ban))[0]["Id_Donvitinh"];
                DataSet ds_hh_nxt = Get_Soluong_Ton_Quydoi(-1, Id_Hanghoa_Ban, Id_Donvitinh, ("" + Id_Xuatkho_Banhang_Chitiet == "") ? null : Id_Xuatkho_Banhang_Chitiet);
                decimal soluong_ton_quydoi = 0;
                soluong_ton_quydoi = ("" + Id_Donvitinh == "" + _Id_Donvitinh)
                ? Convert.ToDecimal("0" + ds_hh_nxt.Tables[0].Compute("sum(Soluong_Ton_Qdoi)",
                        string.Format("Id_Hanghoa_Ban={0}", Id_Hanghoa_Ban)))
                : Convert.ToDecimal("0" + ds_hh_nxt.Tables[0].Compute("sum(Soluong_Ton)",
                        string.Format("Id_Hanghoa_Ban={0} and Id_Donvitinh={1}", Id_Hanghoa_Ban, Id_Donvitinh)));
                return soluong_ton_quydoi;
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.TrayMessage.TrayMessage.Status = new GoobizFrame.Windows.TrayMessage.TrayMessageInfo(MessageBoxIcon.Asterisk, ex.Message, ex.ToString());
                return 0;
            }
        }

        public DataSet Get_Soluong_Ton_Quydoi(object Id_Cuahang_Ban, object Id_Hanghoa_Ban, object Id_Donvitinh, object Id_Xuatkho_Banhang_Chitiet)
        {
            try
            {
                DateTime current_date = objWareService.GetServerDateTime();
                int today = 1;
                if (current_date.Day == 1)
                    today = 1;
                else
                    today = current_date.Day - 1;

                return objWareService.Rptware_Nxt_Hhban_Qdoi_Id_Xuat_Chitiet(new DateTime(current_date.Year, current_date.Month, today, 0, 0, 0),
                                                                current_date, Id_Cuahang_Ban, Id_Hanghoa_Ban, Id_Donvitinh, ("" + Id_Xuatkho_Banhang_Chitiet == "") ? null : Id_Xuatkho_Banhang_Chitiet).ToDataSet();
            }
            catch (Exception ex)
            {
                GoobizFrame.Windows.TrayMessage.TrayMessage.Status = new GoobizFrame.Windows.TrayMessage.TrayMessageInfo(MessageBoxIcon.Asterisk, ex.Message, ex.ToString());
                return null;
            }
        }

        private void gridLookUpEdit_Donvitinh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
                {
                    Ecm.MasterTables.Forms.Ware.Frmware_Dm_Donvitinh_Add frm_Donvitinh = new Ecm.MasterTables.Forms.Ware.Frmware_Dm_Donvitinh_Add();
                    if (gvware_Dieuxe_Chitiet.GetFocusedRowCellValue(gvware_Dieuxe_Chitiet.Columns["Id_Hanghoa_Ban"]).ToString() == "")
                        return;
                    frm_Donvitinh.setId_Hanghoa_Ban(gvware_Dieuxe_Chitiet.GetFocusedRowCellValue(gvware_Dieuxe_Chitiet.Columns["Id_Hanghoa_Ban"]));
                    frm_Donvitinh.item_Select.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    frm_Donvitinh.item_Refresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    frm_Donvitinh.ShowDialog();
                    if (frm_Donvitinh.SelecteWare_Dm_Donvitinh != null)
                        gvware_Dieuxe_Chitiet.SetFocusedRowCellValue(gvware_Dieuxe_Chitiet.Columns["Id_Donvitinh"], frm_Donvitinh.SelecteWare_Dm_Donvitinh.Id_Donvitinh);

                    int soluong = Convert.ToInt32(gvware_Dieuxe_Chitiet.GetFocusedRowCellValue(gvware_Dieuxe_Chitiet.Columns["Soluong"]));
                    if (Get_Soluong_Ton_Quydoi(gvware_Dieuxe_Chitiet.GetFocusedRowCellValue(gvware_Dieuxe_Chitiet.Columns["Id_Hanghoa_Ban"])
                                                , gvware_Dieuxe_Chitiet.GetFocusedRowCellValue(gvware_Dieuxe_Chitiet.Columns["Id_Donvitinh"])
                                                , gvware_Dieuxe_Chitiet.GetFocusedRowCellValue(gvware_Dieuxe_Chitiet.Columns["Id_Xuatkho_Banhang_Chitiet"])) < soluong)
                    {
                        GoobizFrame.Windows.Forms.MessageDialog.Show("Không đủ số lượng để xuất theo yêu cầu");
                        gvware_Dieuxe_Chitiet.SetFocusedRowCellValue(gvware_Dieuxe_Chitiet.Columns["Soluong"], soluong);
                        return;
                    }
                    gvware_Dieuxe_Chitiet.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
#if (DEBUG)
                MessageBox.Show(ex.Message);
#endif
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ClearDataBindings();
            if (gvDieuxe.FocusedRowHandle >= 0)
                DisplayInfo_Details();
            else
                ResetText();
        }

        private void btnDonhang_Click(object sender, EventArgs e)
        {
            //Frmware_Hdbanhang_noVAT_Hhban frmDonhang = new Frmware_Hdbanhang_noVAT_Hhban(lookupEditKhachhang.EditValue);
            //frmDonhang.item_Select.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //frmDonhang.gridColumnChon.Visible = true;
            //frmDonhang.gridColumnChon.VisibleIndex = 0;
            //frmDonhang.ShowDialog();
            //if (frmDonhang._selectedRows != null && frmDonhang._selectedRows.Length > 0)
            //{
            //    Fill_Dondathang(frmDonhang._selectedRows);
            //    txtTongtien_Hang.EditValue = gvware_Xuat_Hanghoa_Ban_Chitiet.Columns["Thanhtien"].SummaryText;
            //}
        }

        private void Fill_Dondathang(DataRow[] sdrware_DonDatHang_Chitiet)
        {
            try
            {
                foreach (DataRow dtr in sdrware_DonDatHang_Chitiet)
                {
                    // Add nhap vattu chi tiet 
                    DataRow rNha_Chitiet = ds_Xkbanhang_Chitiet.Tables[0].NewRow();
                    rNha_Chitiet["Id_Hanghoa_Ban"] = dtr["Id_Hanghoa_Ban"];
                    rNha_Chitiet["Id_Donvitinh"] = dtr["Id_Donvitinh"];
                    rNha_Chitiet["Barcode_Txt"] = dtr["Barcode_Txt"];
                    rNha_Chitiet["Soluong"] = dtr["Soluong"];
                    //rNha_Chitiet["Ten_Mathang"] = dtr["Ten_Mathang"];
                    //rNha_Chitiet["Ten_Donvitinh"] = dtr["Ten_Donvitinh"];
                    rNha_Chitiet["Dongia_Ban"] = dtr["Dongia_Ban"];
                    rNha_Chitiet["Thanhtien"] = dtr["Thanhtien"];
                    ds_Xkbanhang_Chitiet.Tables[0].Rows.Add(rNha_Chitiet);
                }
            }
            catch (Exception ex)
            { ex.ToString(); }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Reload_Chungtu();
        }

        #endregion

        private void gvDieuxe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ClearDataBindings();
            if (gvDieuxe.FocusedRowHandle >= 0)
                DisplayInfo_Details();
            else
                ResetText();
        }

        private void gridLookupEdit_Xuatkho_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit editor = (sender as DevExpress.XtraEditors.LookUpEdit);
            DataRowView row = editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue) as DataRowView;
            gvXuatkho.SetFocusedRowCellValue("Ten_Khachhang", row["Ten_Doituong"]);

            DataSet ds_Xkbanhang_Chitiet = objWareService.Ware_Xuatkho_Banhang_Chitiet_SelectBy_Sochungtu(row["Sochungtu"]).ToDataSet();
            foreach (DataRow dtr in ds_Xkbanhang_Chitiet.Tables[0].Rows)
            {
                if (Convert.ToDecimal("0" + dtr["Soluong_Conlai"]) > 0)
                {
                    DataRow[] row_xuatkho_banhang_chitiet = ds_Dieuxe_Chitet.Tables[0].Select("Id_Xuatkho_Banhang_Chitiet = " + dtr["Id_Xuatkho_Banhang_Chitiet"], "");
                    if (row_xuatkho_banhang_chitiet.Length > 0) // update
                    {
                        row_xuatkho_banhang_chitiet[0]["Soluong"] = Convert.ToDecimal("0" + row_xuatkho_banhang_chitiet[0]["Soluong"]) + Convert.ToDecimal("0" + dtr["Soluong_Conlai"]);
                    }
                    else
                    {
                        DataRow new_row = ds_Dieuxe_Chitet.Tables[0].NewRow();
                        new_row["Id_Dieuxe"] = ("" + identity == "") ? -1 : identity;
                        new_row["Id_Xuatkho_Banhang_Chitiet"] = dtr["Id_Xuatkho_Banhang_Chitiet"];
                        new_row["Id_Hanghoa_Ban"] = dtr["Id_Hanghoa_Ban"];
                        new_row["Id_Donvitinh"] = dtr["Id_Donvitinh"];
                        new_row["Soluong"] = dtr["Soluong_Conlai"];
                        new_row["Soluong_Bandau"] = dtr["Soluong_Conlai"];
                        new_row["Dongia"] = dtr["Dongia"];
                        new_row["Dongia"] = dtr["Dongia"];
                        new_row["Guid_Dieuxe_Chitiet"] = Guid.NewGuid();
                        ds_Dieuxe_Chitet.Tables[0].Rows.Add(new_row);
                    }
                }
            }
            dgDieuxe_Chitiet.DataSource = ds_Dieuxe_Chitet.Tables[0];
            //DataSet tmp = objWareService.Ware_Doituong_SelectByCuahang_ByDate(-1, dtThangnam.DateTime).ToDataSet();
            //DataRow[] dtr_tmp = tmp.Tables[0].Select("Sochungtu = '" + editor.EditValue + "' ");
            //tmp.Tables[0].Rows.Remove(dtr_tmp[0]);
            //gridLookupEdit_Xuatkho.DataSource = tmp.Tables[0];
        }

        private void gridButtonEdit_ThemHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvXuatkho.GetFocusedRowCellValue("Sochungtu").ToString() == "")
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn phiếu xuất, vui lòng chọn lại");
                return;
            }
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Frmware_Xuat_Hh_Ban_Dialog_Chonhang _Frmware_Xuat_Hh_Ban_Dialog_Chonhang = new Frmware_Xuat_Hh_Ban_Dialog_Chonhang(gvXuatkho.GetFocusedRowCellValue("Sochungtu"));
                _Frmware_Xuat_Hh_Ban_Dialog_Chonhang.ShowDialog();
                try
                {
                    foreach (DataRow dtr in _Frmware_Xuat_Hh_Ban_Dialog_Chonhang._selectedRows)
                    {
                        if (Convert.ToDecimal("0" + dtr["Soluong_Conlai"]) > 0)
                        {
                            DataRow[] row_xuatkho_banhang_chitiet = ds_Dieuxe_Chitet.Tables[0].Select("Id_Xuatkho_Banhang_Chitiet = " + dtr["Id_Xuatkho_Banhang_Chitiet"], "");
                            if (row_xuatkho_banhang_chitiet.Length > 0) // update
                            {
                                row_xuatkho_banhang_chitiet[0]["Soluong"] = Convert.ToDecimal("0" + row_xuatkho_banhang_chitiet[0]["Soluong"]) + Convert.ToDecimal("0" + dtr["Soluong_Conlai"]);
                            }
                            else
                            {
                                DataRow new_row = ds_Dieuxe_Chitet.Tables[0].NewRow();
                                new_row["Id_Dieuxe"] = ("" + identity == "") ? -1 : identity;
                                new_row["Id_Xuatkho_Banhang_Chitiet"] = dtr["Id_Xuatkho_Banhang_Chitiet"];
                                new_row["Id_Hanghoa_Ban"] = dtr["Id_Hanghoa_Ban"];
                                new_row["Id_Donvitinh"] = dtr["Id_Donvitinh"];
                                new_row["Soluong"] = dtr["Soluong_Conlai"];
                                new_row["Dongia"] = dtr["Dongia"];
                                ds_Dieuxe_Chitet.Tables[0].Rows.Add(new_row);
                            }
                        }
                    }
                }
                catch (Exception ex)
                { ex.ToString(); }
            }
        }

        private void gridButtonEdit_Delete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                if (MessageBox.Show("Xóa phiếu xuất và hàng hóa của phiếu này khỏi lệnh điều xe?", "Confirm Dialog", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    objWareService.Delete_Ware_Dieuxe_Xuatkho(gvXuatkho.GetFocusedRowCellValue("Id_Dieuxe_Xuatkho"));
                    DisplayInfo_Details();
                }
            }
        }

        private void txtCuahang_Ban_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                //Frmware_Dm_Cuahang_Ban_Dialog _Frmware_Dm_Cuahang_Ban_Dialog = new Frmware_Dm_Cuahang_Ban_Dialog(identity);
                //_Frmware_Dm_Cuahang_Ban_Dialog.ShowDialog();
                //setTen_Cuahang_Ban();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (gvDieuxe.GetFocusedRowCellValue("Id_Dieuxe") == null)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn phiếu điều xe, vui lòng chọn lại");
                return;
            }
            DataSet dsHeso_Chuongtrinh = objMasterService.Get_Rex_Dm_Heso_Chuongtrinh_Collection3().ToDataSet();
            string emailTo = "" + lookUpEdit_Taixe.GetColumnValue("Email");
            if (emailTo == "")
                return;
            //string emailTo = dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "CompanyEmail"))[0]["Heso"].ToString();
            //DataSet ds_Dieuxe_Chitet_Email = objWareService.Ware_Dieuxe_Chitiet_SelectBy_Id_Dieuxe_Mail(identity).ToDataSet();
            //string subject = "Lệnh điều xe " + identity.ToString();
            //string body = "Xe: " + lookupEdit_Dm_Xe.Text;
            //body += "<br/>Nơi đến: " + txtCuahang_Ban.Text;
            //body += "<br/>Ngay đi: " + dateEdit_Ngaydi.Text + " - Ngày về: " + dateEdit_Ngayve.Text;
            //body += "<br/>Ghi chú: " + richTextBoxGhichu.Text;
            //body += "<br/><br/> Chi tiết hàng hóa: <br/>";
            //body += ConvertDataTableToHTML(ds_Dieuxe_Chitet_Email.Tables[0]);

            //string smtpAddress = dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "Mail_Server"))[0]["Heso"].ToString(); //"smtp.longthanhmekong.com";
            //int portNumber = Convert.ToInt32(dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "Port"))[0]["Heso"]); //25;
            //bool enableSSL = Convert.ToBoolean(dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "SSL"))[0]["Heso"]);// false;
            //string emailFrom = dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "Email_Server"))[0]["Heso"].ToString(); //"info@longthanhmekong.com";
            //string password = dsHeso_Chuongtrinh.Tables[0].Select(string.Format("Ma_Heso_Chuongtrinh='{0}'", "Password_Email"))[0]["Heso"].ToString(); ///"leminhlong18072007";

            //using (MailMessage mail = new MailMessage())
            //{
            //    mail.From = new MailAddress(emailFrom);
            //    //emailTo = "hoangnhan1907@gmail.com";
            //    mail.To.Add(emailTo);
            //    mail.Subject = subject;
            //    mail.Body = body;
            //    mail.IsBodyHtml = true;
            //    // Can set to false, if you are sending pure text.
            //    //mail.Attachments.Add(new Attachment("D:\\donhang\\" + txtSochungtu.Text + ".pdf"));
            //    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));
            //    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
            //    {
            //        smtp.Credentials = new NetworkCredential("info", password);
            //        smtp.EnableSsl = enableSSL;
            //        smtp.Send(mail);
            //    }
            //}
            //GoobizFrame.Windows.Forms.MessageDialog.Show("Đã gửi mail cho " + lookUpEdit_Taixe.Text);
        }

        public static string ConvertDataTableToHTML(DataTable dt)
        {
            //string html = "</br></br>" + ten_kho + "</br>";
            string html = "<table width='100%' border=1 >";
            //add header row
            html += "<tr align='center' height='30px' bgcolor='#CCC'>";
            for (int i = 0; i < dt.Columns.Count - 1; i++)
                html += "<td >" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";

            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr height='25px' align='center'>";
                for (int j = 0; j < dt.Columns.Count - 1; j++)
                {
                    if (j == 1)
                        html += "<td align='left' >" + dt.Rows[i][j].ToString() + "</td>";
                    else
                        if (j == 3)
                            html += "<td align='right' >" + dt.Rows[i][j].ToString() + "</td>";
                        else
                            html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                }
                html += "</tr>";
            }

            html += "<tr align='center' height='30px' bgcolor='#CCC'>";
            html += "<td colspan='3'> Tổng cộng";
            html += "</td>";
            html += "<td align='right'> ";
            html += String.Format("{0:n}", dt.Compute("sum(Soluong)", ""));
            html += "</td>";
            html += "</tr>";
            html += "</table></br></br>";
            return html;
        }

        private void loolupEdit_Kho_Print_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt64(loolupEdit_Kho_Print.EditValue) == -1)
                gvware_Dieuxe_Chitiet.Columns["Id_Cuahang_Ban"].ClearFilter();
            else
                gvware_Dieuxe_Chitiet.Columns["Id_Cuahang_Ban"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(gvware_Dieuxe_Chitiet.Columns["Id_Cuahang_Ban"], loolupEdit_Kho_Print.EditValue);
            gvware_Dieuxe_Chitiet.BestFitColumns();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (gvDieuxe.GetFocusedRowCellValue("Id_Dieuxe") == null)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn phiếu điều xe, vui lòng chọn lại");
                return;
            }
            if (ds_Dieuxe_Chitet.Tables[0].Select("Chon=true").Length == 0)
            {
                GoobizFrame.Windows.Forms.MessageDialog.Show("Chưa chọn hàng hóa để in, vui lòng chọn lại");
                return;
            }
            try
            {
                DataSets.DsHdbanhang_Xuatkho dsWare_Xuat_Vattu = new Ecm.Ware.DataSets.DsHdbanhang_Xuatkho();
                Reports.rptDieuxe _rptDieuxe = new Reports.rptDieuxe();
                GoobizFrame.Windows.Forms.FrmPrintPreview frmPrintPreview = new GoobizFrame.Windows.Forms.FrmPrintPreview();
                frmPrintPreview.Report = _rptDieuxe;
                _rptDieuxe.DataSource = dsWare_Xuat_Vattu;

                _rptDieuxe.xrTableCell_Ngaydi.Text = dateEdit_Ngaydi.Text;
                _rptDieuxe.xrTableCell_Ngay.Text = DateTime.Now.Day.ToString();
                _rptDieuxe.xrTableCell_Thang.Text = DateTime.Now.Month.ToString();
                _rptDieuxe.xrTableCell_Nam.Text = DateTime.Now.Year.ToString();
                _rptDieuxe.xrTableCell_Taixe.Text = txtTen_Taixe.Text;
                _rptDieuxe.xrTableCell_SoLenh_Dieuxe.Text = gvDieuxe.GetFocusedRowCellValue("Id_Dieuxe").ToString();
                _rptDieuxe.xrTableCell_Xe.Text = lookupEdit_Dm_Xe.Text;
                _rptDieuxe.xrTableCell_Ghichu.Text = richTextBoxGhichu.Text;

                _rptDieuxe.xrTableCell_Noiden.Text = lookUpEdit_Khuvuc.Text;
                _rptDieuxe.xrTableCell_Kho.Text = loolupEdit_Kho_Print.Text;
                //Ware_Xuat_Vattu_Chitiet
                for (int i = 0; i < gvware_Dieuxe_Chitiet.RowCount; i++)
                {
                    if (gvware_Dieuxe_Chitiet.GetRowExpanded(i)
                        || gvware_Dieuxe_Chitiet.GetRowCellDisplayText(i, "Chon") == "Unchecked")
                        continue;
                    DataRow rWare_Xuat_Vattu_Chitiet = dsWare_Xuat_Vattu.Tables[0].NewRow();
                    rWare_Xuat_Vattu_Chitiet["stt"] = i + 1;
                    //rWare_Xuat_Vattu_Chitiet["id_xuat_hh_mua"] = gvware_Xuat_Hanghoa_Ban_Chitiet.GetRowCellDisplayText(i, "Id_Dieuxe");
                    //rWare_Xuat_Vattu_Chitiet["id_xuat_hh_mua"] = identity;
                    rWare_Xuat_Vattu_Chitiet["Ma_Hanghoa_Ban"] = gvware_Dieuxe_Chitiet.GetRowCellDisplayText(i, gridColumn_Mahang);
                    rWare_Xuat_Vattu_Chitiet["Ten_Hanghoa_Ban"] = gvware_Dieuxe_Chitiet.GetRowCellDisplayText(i, gridColumn14);
                    rWare_Xuat_Vattu_Chitiet["DVT_Quydoi"] = gvware_Dieuxe_Chitiet.GetRowCellDisplayText(i, gridColumn28);
                    rWare_Xuat_Vattu_Chitiet["Ten_Donvitinh"] = gvware_Dieuxe_Chitiet.GetRowCellDisplayText(i, "Id_Donvitinh");
                    rWare_Xuat_Vattu_Chitiet["DVT_Quydoi_Text"] = gvware_Dieuxe_Chitiet.GetRowCellDisplayText(i, "DVT_Quydoi");
                    rWare_Xuat_Vattu_Chitiet["Soluong"] = Convert.ToDecimal("0" + gvware_Dieuxe_Chitiet.GetRowCellDisplayText(i, "Soluong"));
                    dsWare_Xuat_Vattu.Tables[0].Rows.Add(rWare_Xuat_Vattu_Chitiet);
                }
                dsWare_Xuat_Vattu.AcceptChanges();
                #region Set he so ctrinh - logo, ten cty
                DataSet dsHeso_Chuongtrinh;
                using (dsHeso_Chuongtrinh = objMasterService.Get_Rex_Dm_Heso_Chuongtrinh_Collection3().ToDataSet())
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

                    _rptDieuxe.xrc_CompanyName.DataBindings.Add(
                        new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyName"));
                    _rptDieuxe.xrc_CompanyAddress.DataBindings.Add(
                        new DevExpress.XtraReports.UI.XRBinding("Text", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyAddress"));
                    //rptXuat_Vattu.xrPic_Logo.DataBindings.Add(
                    //    new DevExpress.XtraReports.UI.XRBinding("Image", dsCompany_Paras, dsCompany_Paras.Tables[0].TableName + ".CompanyLogo"));
                }
                #endregion

                _rptDieuxe.CreateDocument();
                GoobizFrame.Windows.Forms.ReportOptions oReportOptions = GoobizFrame.Windows.Forms.ReportOptions.GetReportOptions(_rptDieuxe);

                if (Convert.ToBoolean(oReportOptions.PrintPreview))
                {
                    frmPrintPreview.Text = "In Phiếu điều xe";//oReportOptions.Caption;
                    frmPrintPreview.printControl1.PrintingSystem = _rptDieuxe.PrintingSystem;
                    frmPrintPreview.MdiParent = this.MdiParent;
                    frmPrintPreview.Show();
                    frmPrintPreview.Activate();
                }
                else
                {
                    var reportPrintTool = new DevExpress.XtraReports.UI.ReportPrintTool(_rptDieuxe);
                    reportPrintTool.Print();
                }
            }
            catch (Exception ex)
            { ex.ToString(); }
        }

        private void dtThangnam_EditValueChanged(object sender, EventArgs e)
        {
            Reload_Chungtu();
        }

        private void gvware_Xuat_Hanghoa_Ban_Chitiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                //if (gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedDataRow() == null) return;
                switch (e.Column.FieldName)
                {
                    //case "Soluong":
                    //    decimal soluong_donhang = Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Soluong_Donhang"));
                    //    decimal soluong_dadieuxe = Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Soluong_Da_Dieuxe"));
                    //    if (Convert.ToDecimal("0" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Soluong")) > (soluong_donhang - soluong_dadieuxe))
                    //    {
                    //        GoobizFrame.Windows.Forms.MessageDialog.Show("Số lượng không được lớn hơn số lượng đơn hàng");
                    //        gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue("Soluong", gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Soluong2"));
                    //    }
                    //    break;
                    case "Id_Hanghoa_Ban":
                        var _Id_Donvitinh = ds_Hanghoa_Ban.Tables[0].Select(string.Format("Id_Hanghoa_Ban={0}",
                               gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Id_Hanghoa_Ban")))[0]["Id_Donvitinh"];
                        gvware_Dieuxe_Chitiet.SetFocusedRowCellValue("Id_Donvitinh", _Id_Donvitinh);
                        break;

                    case "Id_Cuahang_Ban":
                        check_tonkho(gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Id_Cuahang_Ban"),
                                  gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Id_Hanghoa_Ban"),
                                  gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Id_Donvitinh"),
                                  Convert.ToDecimal("0" + gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Soluong")),
                                  gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Guid_Dieuxe_Chitiet"),
                                  gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Id_Dieuxe_Chitiet"),
                                  gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Id_Xuatkho_Banhang_Chitiet"));
                        break;
                    case "Soluong":
                        if (Convert.ToDecimal("0" + gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Soluong")) > 0)
                            check_tonkho(gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Id_Cuahang_Ban"),
                                      gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Id_Hanghoa_Ban"),
                                      gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Id_Donvitinh"),
                                      Convert.ToDecimal("0" + gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Soluong")),
                                      gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Guid_Dieuxe_Chitiet"),
                                      gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Id_Dieuxe_Chitiet"),
                                      gvware_Dieuxe_Chitiet.GetFocusedRowCellValue("Id_Xuatkho_Banhang_Chitiet"));
                        break;
                }
                gvware_Dieuxe_Chitiet.BestFitColumns();
            }
            catch (Exception ex)
            {
#if (DEBUG)
                MessageBox.Show(ex.Message);
#endif
            }
        }

        void check_tonkho(object id_kho, object id_hanghoa_ban, object id_donvitinh, decimal soluong, object Guid_Dieuxe_Chitiet, object id_dieuxe_chitiet, object id_xuatkho_banhang_chititet)
        {
            DataSet dsTonkho;
            DateTime current_date = objWareService.GetServerDateTime();
            int today = 1;
            if (current_date.Day == 1)
                today = 1;
            else
                today = current_date.Day - 1;
            //bool check = false;
            decimal sl_tonhientai = 0;
            decimal sl_cannhap = 0;
            //decimal sl_dangduyet = 0;
            //decimal sl_bandau = 0;
            //     DateTime a = new DateTime(current_date.Year, current_date.Month, today, 0, 0, 0);
            dsTonkho = objWareService.Rptware_Nxt_Hhban_Qdoi(new DateTime(current_date.Year, current_date.Month, today, 0, 0, 0),
                                                                    current_date.AddDays(1), id_kho, id_hanghoa_ban, id_donvitinh).ToDataSet();
            decimal sl_danhap_Theo_Kho = 0;
            if (dsTonkho.Tables[0].Rows.Count > 0)
            {
                if ("" + id_dieuxe_chitiet != "")
                    sl_danhap_Theo_Kho = Convert.ToDecimal("0" + ds_Dieuxe_Chitet.Tables[0].Compute("Sum(Soluong)", "Guid_Dieuxe_Chitiet = '" + Guid_Dieuxe_Chitiet
                                + "' and Id_Dieuxe_Chitiet <> " + id_dieuxe_chitiet + " and Id_Cuahang_Ban = " + id_kho));
                else
                    sl_danhap_Theo_Kho = Convert.ToDecimal("0" + ds_Dieuxe_Chitet.Tables[0].Compute("Sum(Soluong)", "Guid_Dieuxe_Chitiet = '" + (("" + Guid_Dieuxe_Chitiet == "") ? null : Guid_Dieuxe_Chitiet) + "'" + " and Id_Cuahang_Ban = " + id_kho));// -soluong;
                sl_tonhientai = Convert.ToDecimal("0" + dsTonkho.Tables[0].Rows[0]["Soluong_Ton"]) - sl_danhap_Theo_Kho;
                //    if (soluong > sl_tonhientai)
                //    {
                //        check = true;
                //    }
                //}
                //else
                //    check = true;

                //if (check)
                //{
                //GoobizFrame.Windows.Forms.MessageDialog.Show("Số lượng lớn hơn số lượng trong kho");
                DataRow[] dtr_bandau = ds_Dieuxe_Chitet.Tables[0].Select("Guid_Dieuxe_Chitiet = '" + Guid_Dieuxe_Chitiet + "' ");
                decimal sl_bandau = 0;
                if (dtr_bandau.Length > 0)
                    sl_bandau = Convert.ToDecimal("0" + dtr_bandau[0]["Soluong_Bandau"]);
                sl_cannhap = sl_bandau - (Convert.ToDecimal("0" + ds_Dieuxe_Chitet.Tables[0].Compute("Sum(Soluong)", "Guid_Dieuxe_Chitiet = '" + Guid_Dieuxe_Chitiet + "' ")) - soluong);
                //= Convert.ToDecimal("0" + ds_Dieuxe_Chitet.Tables[0].Compute("Sum(Soluong)", "Guid_Dieuxe_Chitiet = '" + Guid_Dieuxe_Chitiet + "' and Id_Dieuxe_Chitiet <>" + id_dieuxe_chitiet));
                //Convert.ToDecimal("0" + ds_Dieuxe_Chitet.Tables[0].Compute("Sum(Soluong)", "Guid_Dieuxe_Chitiet = '" + Guid_Dieuxe_Chitiet + "' "));
                if (sl_tonhientai <= sl_cannhap)
                {
                    if (!flag)
                    {
                        flag = true;
                        gvware_Dieuxe_Chitiet.SetFocusedRowCellValue("Soluong", sl_tonhientai);
                    }
                    
                }
                else
                    if (sl_cannhap != soluong)
                    {
                        if (!flag)
                        {
                            flag = true;
                            gvware_Dieuxe_Chitiet.SetFocusedRowCellValue("Soluong", sl_cannhap);
                        }
                    }
                //AddRow_Dataset(ds_Dieuxe_Chitet, gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Guid_Dieuxe_Chitiet"), soluong - sl_tonhientai);
                if (ds_Dieuxe_Chitet.Tables[0].Rows.Count == 0
                    || Convert.ToDecimal("0" + ds_Dieuxe_Chitet.Tables[0].Compute("Sum(Soluong)", "Guid_Dieuxe_Chitiet = '" + Guid_Dieuxe_Chitiet + "' ")) >= sl_cannhap
                    || sl_tonhientai == 0)
                    return;
                gvware_Dieuxe_Chitiet.AddNewRow();
                gvware_Dieuxe_Chitiet.SetFocusedRowCellValue("Id_Hanghoa_Ban", id_hanghoa_ban);
                gvware_Dieuxe_Chitiet.SetFocusedRowCellValue("Id_Donvitinh", id_donvitinh);
                gvware_Dieuxe_Chitiet.SetFocusedRowCellValue("Guid_Dieuxe_Chitiet", Guid_Dieuxe_Chitiet);
                gvware_Dieuxe_Chitiet.SetFocusedRowCellValue("Soluong", sl_cannhap - sl_tonhientai);
                gvware_Dieuxe_Chitiet.SetFocusedRowCellValue("Id_Xuatkho_Banhang_Chitiet", id_xuatkho_banhang_chititet);
                //gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue("Id_Dieuxe", gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Dieuxe"));
                //gvware_Xuat_Hanghoa_Ban_Chitiet.SetFocusedRowCellValue("Dongia", gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Dongia"));
            }
            else
                gvware_Dieuxe_Chitiet.SetFocusedRowCellValue("Soluong", 0);
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow row in ds_Dieuxe_Chitet.Tables[0].Rows)
            {
                row["Chon"] = chkAll.Checked;
            }
        }

        private void gvXuatkho_KeyDown(object sender, KeyEventArgs e)
        {
            // if (gvXuatkho.FocusedColumn.VisibleIndex == gvXuatkho.VisibleColumns.Count - 1
            //&& "" + gvXuatkho.GetFocusedRowCellValue("Sochungtu") != "")
            //     gvXuatkho.AddNewRow();
        }

        private void gvware_Xuat_Hanghoa_Ban_Chitiet_KeyDown(object sender, KeyEventArgs e)
        {
            //    if (gvware_Xuat_Hanghoa_Ban_Chitiet.FocusedColumn.VisibleIndex == gvware_Xuat_Hanghoa_Ban_Chitiet.VisibleColumns.Count - 1
            //&& "" + gvware_Xuat_Hanghoa_Ban_Chitiet.GetFocusedRowCellValue("Id_Hanghoa_Ban") != "")
            //        gvware_Xuat_Hanghoa_Ban_Chitiet.AddNewRow();
        }

        private void lookUpEdit_Khuvuc_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ("" + lookUpEdit_Khuvuc.Text != "" && FormState != GoobizFrame.Windows.Forms.FormState.View)
                {
                    guid_dieuxe_tmp = Guid.NewGuid();
                    ds_Dieuxe_Chitet_tmp = objWareService.Ware_Dieuxe_Chitiet_SelectBy_Id_Dieuxe(-1).ToDataSet();
                    dgDieuxe_Chitiet.DataSource = ds_Dieuxe_Chitet_tmp.Tables[0]; // reset chi tiết

                    ds_Dieuxe_Xuatkho = objWareService.Ware_Doituong_SelectByCuahang_ByDate(lookUpEdit_Khuvuc.EditValue, dtThangnam.DateTime, identity).ToDataSet();
                    ds_Dieuxe_Xuatkho.Tables[0].Columns.Add("Chon", typeof(bool));
                    foreach (DataRow row in ds_Dieuxe_Xuatkho.Tables[0].Rows)
                    {
                        row["Chon"] = true;
                        DataSet ds_Xkbanhang_Chitiet = objWareService.Ware_Xuatkho_Banhang_Chitiet_SelectBy_Sochungtu(row["Sochungtu"]).ToDataSet();
                        foreach (DataRow dtr in ds_Xkbanhang_Chitiet.Tables[0].Rows)
                        {
                            DataRow new_row = ds_Dieuxe_Chitet_tmp.Tables[0].NewRow();
                            new_row["Id_Dieuxe"] = ("" + identity == "") ? -1 : identity;
                            new_row["Id_Xuatkho_Banhang_Chitiet"] = dtr["Id_Xuatkho_Banhang_Chitiet"];
                            new_row["Id_Hanghoa_Ban"] = dtr["Id_Hanghoa_Ban"];
                            new_row["Id_Donvitinh"] = dtr["Id_Donvitinh"];
                            new_row["Soluong"] = dtr["Soluong"];
                            new_row["Soluong_Bandau"] = dtr["Soluong"];
                            new_row["Dongia"] = dtr["Dongia"];
                            new_row["Sochungtu"] = dtr["Sochungtu"];
                            new_row["Guid_Dieuxe_Chitiet"] = Guid.NewGuid();
                            new_row["Guid_Dieuxe"] = "" + guid_dieuxe_tmp;
                            ds_Dieuxe_Chitet_tmp.Tables[0].Rows.Add(new_row);
                        }
                    }
                    objWareService.Update_Ware_Dieuxe_Chitiet_Tmp_Collection(ds_Dieuxe_Chitet_tmp); // update dieuxe_chitiet_tmp để lưu tạm (dùng để group theo hàng hóa)

                    ds_Dieuxe_Chitet_tmp = objWareService.Ware_Dieuxe_Chitiet_Tmp_SelectBy_Guid_Dieuxe(guid_dieuxe_tmp).ToDataSet();
                    dgDieuxe_Chitiet.DataSource = ds_Dieuxe_Chitet_tmp.Tables[0];
                    dgXuatkho.DataSource = ds_Dieuxe_Xuatkho.Tables[0];
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void gridCheckedit_Chon_Chungtu_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraEditors.CheckEdit chk = sender as DevExpress.XtraEditors.CheckEdit;
                if (!chk.Checked) // remove hàng hóa từ phiếu xuất bỏ chọn
                {
                    if (ds_Dieuxe_Chitet_tmp != null) // update data tmp
                    {
                        if (gvXuatkho.GetFocusedRowCellValue("Sochungtu") + "' " != "")
                            objWareService.Ware_Dieuxe_Chitiet_Tmp_DeleteBy_Sochungtu(gvXuatkho.GetFocusedRowCellValue("Sochungtu"));
                        ds_Dieuxe_Chitet_tmp = objWareService.Ware_Dieuxe_Chitiet_Tmp_SelectBy_Guid_Dieuxe(guid_dieuxe_tmp).ToDataSet();
                        dgDieuxe_Chitiet.DataSource = ds_Dieuxe_Chitet_tmp.Tables[0];
                    }
                    else //update data dieuxe_chitiet
                    {
                        if (gvXuatkho.GetFocusedRowCellValue("Sochungtu") + "' " != "")
                            objWareService.Ware_Dieuxe_Chitiet_DeleteBy_Sochungtu(gvXuatkho.GetFocusedRowCellValue("Sochungtu"));
                        ds_Dieuxe_Chitet = objWareService.Ware_Dieuxe_Chitiet_SelectBy_Id_Dieuxe_Group(identity).ToDataSet();
                        dgDieuxe_Chitiet.DataSource = ds_Dieuxe_Chitet.Tables[0];
                    }
                }
                else // add hàng hóa từ phiếu xuất chọn
                {
                    if (ds_Dieuxe_Chitet_tmp != null) // update data tmp
                    {
                        ds_Dieuxe_Chitet_tmp = objWareService.Ware_Dieuxe_Chitiet_SelectBy_Id_Dieuxe(-1).ToDataSet();
                        DataSet ds_Xkbanhang_Chitiet = objWareService.Ware_Xuatkho_Banhang_Chitiet_SelectBy_Sochungtu(gvXuatkho.GetFocusedRowCellValue("Sochungtu")).ToDataSet();
                        foreach (DataRow dtr in ds_Xkbanhang_Chitiet.Tables[0].Rows)
                        {
                            DataRow new_row = ds_Dieuxe_Chitet_tmp.Tables[0].NewRow();
                            new_row["Id_Dieuxe"] = ("" + identity == "") ? -1 : identity;
                            new_row["Id_Xuatkho_Banhang_Chitiet"] = dtr["Id_Xuatkho_Banhang_Chitiet"];
                            new_row["Id_Hanghoa_Ban"] = dtr["Id_Hanghoa_Ban"];
                            new_row["Id_Donvitinh"] = dtr["Id_Donvitinh"];
                            new_row["Soluong"] = dtr["Soluong"];
                            new_row["Soluong_Bandau"] = dtr["Soluong"];
                            new_row["Dongia"] = dtr["Dongia"];
                            new_row["Sochungtu"] = dtr["Sochungtu"];
                            new_row["Guid_Dieuxe_Chitiet"] = Guid.NewGuid();
                            new_row["Guid_Dieuxe"] = "" + guid_dieuxe_tmp;
                            ds_Dieuxe_Chitet_tmp.Tables[0].Rows.Add(new_row);
                        }
                        objWareService.Update_Ware_Dieuxe_Chitiet_Tmp_Collection(ds_Dieuxe_Chitet_tmp); // update dieuxe_chitiet_tmp để lưu tạm (dùng để group theo hàng hóa)
                        ds_Dieuxe_Chitet_tmp = objWareService.Ware_Dieuxe_Chitiet_Tmp_SelectBy_Guid_Dieuxe(guid_dieuxe_tmp).ToDataSet();
                        dgDieuxe_Chitiet.DataSource = ds_Dieuxe_Chitet_tmp.Tables[0];
                    }
                    else
                    {
                        DataSet ds_Xkbanhang_Chitiet = objWareService.Ware_Xuatkho_Banhang_Chitiet_SelectBy_Sochungtu(gvXuatkho.GetFocusedRowCellValue("Sochungtu")).ToDataSet();
                        foreach (DataRow dtr in ds_Xkbanhang_Chitiet.Tables[0].Rows)
                        {
                            ds_Dieuxe_Chitet = objWareService.Ware_Dieuxe_Chitiet_SelectBy_Id_Dieuxe_Group(identity).ToDataSet();
                            DataRow new_row = ds_Dieuxe_Chitet.Tables[0].NewRow();
                            new_row["Id_Dieuxe"] = ("" + identity == "") ? -1 : identity;
                            new_row["Id_Xuatkho_Banhang_Chitiet"] = dtr["Id_Xuatkho_Banhang_Chitiet"];
                            new_row["Id_Hanghoa_Ban"] = dtr["Id_Hanghoa_Ban"];
                            new_row["Id_Donvitinh"] = dtr["Id_Donvitinh"];
                            new_row["Soluong"] = dtr["Soluong"];
                            new_row["Soluong_Bandau"] = dtr["Soluong"];
                            new_row["Dongia"] = dtr["Dongia"];
                            new_row["Sochungtu"] = dtr["Sochungtu"];
                            new_row["Guid_Dieuxe_Chitiet"] = Guid.NewGuid();
                            ds_Dieuxe_Chitet.Tables[0].Rows.Add(new_row);
                        }
                        dgDieuxe_Chitiet.DataSource = ds_Dieuxe_Chitet.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void gridButtonDelete_Chitiet_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            {
                gvware_Dieuxe_Chitiet.DeleteRow(gvware_Dieuxe_Chitiet.FocusedRowHandle);
            }
        }

    }
}
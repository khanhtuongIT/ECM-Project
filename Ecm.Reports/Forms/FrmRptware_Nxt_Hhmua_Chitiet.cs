﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SunLine.Reports.Forms
{
    public partial class FrmRptware_Nxt_Hhmua_Chitiet : GoobizFrame.Windows.Forms.FormReportWithHeader
    {
        SunLine.WebReferences.Classes.WareService objWareService = new SunLine.WebReferences.Classes.WareService();
        SunLine.WebReferences.Classes.ReportService objReportServices = new SunLine.WebReferences.Classes.ReportService();
        SunLine.WebReferences.Classes.MasterService objMasterService = new SunLine.WebReferences.Classes.MasterService();

        public FrmRptware_Nxt_Hhmua_Chitiet()
        {
            InitializeComponent();
            DisplayInfo();
        }

        void DisplayInfo()
        {
            //Get data Ware_Dm_Hanghoa_Mua
            DataSet dsWare_Dm_Hanghoa_Mua = objMasterService.Get_All_Ware_Dm_Hanghoa_Mua();
            lookUpEdit_Hanghoa_Mua.Properties.DataSource = dsWare_Dm_Hanghoa_Mua.Tables[0];
        }

        public override bool PerformQuery()
        {
            System.Collections.Hashtable hashtableControls = new System.Collections.Hashtable();
            hashtableControls.Add(dtNgay_Batdau, lblNgay_Batdau.Text);
            hashtableControls.Add(dtNgay_Ketthuc, lblNgay_Ketthuc.Text);
            //hashtableControls.Add(lookUpEditCuahang_Ban, lblCuahang_Ban.Text);

            if (!GoobizFrame.Windows.MdiUtils.Validator.CheckNullFields(hashtableControls))
                return false;

            XtraReports.Rptware_Nxt_Hhmua_Chitiet Rptware_Nxt_Hhmua_Chitiet = new SunLine.Reports.XtraReports.Rptware_Nxt_Hhmua_Chitiet();
            this.Report = Rptware_Nxt_Hhmua_Chitiet;
            //DataSet ds_Collection = objReportServices.Rptware_Nxt_Hhmua_Chitiet(dtNgay_Batdau.EditValue, dtNgay_Ketthuc.EditValue);
            //Datasets.DsRptware_Nxt_Hhmua_Chitiet DsRpt = new SunLine.Reports.Datasets.DsRptware_Nxt_Hhmua_Chitiet();
//            try
//            {
//                foreach (DataRow row in ds_Collection.Tables[0].Rows)
//                    DsRpt.Tables[0].ImportRow(row);
//            }
//            catch (Exception ex)
//            {
//#if DEBUG
//                MessageBox.Show(ex.Message);
//#endif
//            }
            Rptware_Nxt_Hhmua_Chitiet.xrLblHanghoa.Text = lookUpEdit_Hanghoa_Mua.Text;
            Rptware_Nxt_Hhmua_Chitiet.xrLbkNgay.Text = lblNgay_Batdau.Text + " " + dtNgay_Batdau.Text + " " + lblNgay_Ketthuc.Text + " " + dtNgay_Ketthuc.Text;
            //Rptware_Nxt_Hhmua_Chitiet.DataSource = DsRpt;
            Rptware_Nxt_Hhmua_Chitiet.CreateDocument();

            this.printControl1.PrintingSystem = Rptware_Nxt_Hhmua_Chitiet.PrintingSystem;

            return base.PerformQuery();
        }
    }
}


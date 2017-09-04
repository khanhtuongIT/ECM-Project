﻿using System;
using System.Collections.Generic;
using System.Text;
using itvs.Windows.PlugIn;

namespace SunLine.Admspec.Plugins
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class Admspec : IPlugin
    {

        bool DataLoaded = false;
        #region Forms
        Forms.FrmControlPanel FrmControlPanel;
        #endregion

        public Admspec()
        {
            m_strName = "Đang kiểm tra";
            m_PluginItemConfig = @"";
        }

        public void SetMdiParent(System.Windows.Forms.Form mdiParent)
        {
            this.mdiParent = mdiParent;
        }

        public void BarManager_ItemClick(object sender, EventArgs e)
        {
            DevExpress.Utils.WaitDialogForm WaitDialogForm = new DevExpress.Utils.WaitDialogForm();

            string formName = "";
            string formText = "";

            if (e.GetType() == typeof(DevExpress.XtraNavBar.NavBarLinkEventArgs))
            {
                formName = ((DevExpress.XtraNavBar.NavBarItem)sender).Name;
                formText = ((DevExpress.XtraNavBar.NavBarItem)sender).Caption;
            }
            else if (e.GetType() == typeof(DevExpress.XtraBars.ItemClickEventArgs))
            {
                formName = ((DevExpress.XtraBars.ItemClickEventArgs)e).Item.Name;
                formText = ((DevExpress.XtraBars.ItemClickEventArgs)e).Item.Caption;
            }

            switch (formName)
            {
                case "FrmControlPanel":
                    FrmControlPanel = (Forms.FrmControlPanel) itvs.Windows.MdiUtils.MdiChecker.GetMdiChilden(mdiParent, "FrmControlPanel");

                    if (FrmControlPanel == null || FrmControlPanel.IsDisposed)
                        FrmControlPanel = new Forms.FrmControlPanel();
                    FrmControlPanel.Text = formText;
                    FrmControlPanel.MdiParent = mdiParent;
                    FrmControlPanel.Show();
                    FrmControlPanel.Activate();
                    break;
                
            }
            WaitDialogForm.Close();
        }

        #region Implements Interface
        private string m_strName;
        private string m_PluginItemConfig;
        private IPluginHost m_Host;

        private System.Windows.Forms.Form mdiParent;
        public System.Windows.Forms.Form MdiParent
        {
            get { return mdiParent; }
            set { mdiParent = value; }
        }
        public string Name
        { get { return m_strName; } set { m_strName = value; } }

        public string PluginItemConfig
        { get { return m_PluginItemConfig; } set { m_PluginItemConfig = value; } }

        //public void Show(System.Windows.Forms.Form mdiParent)
        //{ 
        //    Main1 mn = new Main1();
        //    mn.ShowDialog();
        //}

        public IPluginHost Host
        {
            get { return m_Host; }
            set
            {
                m_Host = value;
                m_Host.Register(this);
            }
        }

        public void PreLoadData()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(this.PreLoadThread)).Start();
        }

        void PreLoadThread()
        {
            lock (this)
            {
                DataLoaded = true;
            }
        }
        #endregion
    }
}

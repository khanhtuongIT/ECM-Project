﻿namespace Ecm.MasterTables.Forms.Rex
{
    partial class Frmrex_Dm_Quocgia_Add
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgrex_Dm_Quocgia = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMa_Quocgia = new DevExpress.XtraEditors.LabelControl();
            this.txtTen_Quocgia = new DevExpress.XtraEditors.TextEdit();
            this.txtMa_Quocgia = new DevExpress.XtraEditors.TextEdit();
            this.lblTen_Quocgia = new DevExpress.XtraEditors.LabelControl();
            this.txtId_Quocgia = new DevExpress.XtraEditors.TextEdit();
            this.lblId_Quocgia = new DevExpress.XtraEditors.LabelControl();
            this.xtraHNavigator1 = new GoobizFrame.Windows.Controls.XtraHNavigator();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrex_Dm_Quocgia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTen_Quocgia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMa_Quocgia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId_Quocgia.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.AppearancesBar.ItemsFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barAndDockingController1.LookAndFeel.SkinName = "Blue";
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            // 
            // dgrex_Dm_Quocgia
            // 
            this.dgrex_Dm_Quocgia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.Buttons.First.Visible = false;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.None;
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.TextStringFormat = "Dòng: {0} / {1}";
            this.dgrex_Dm_Quocgia.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.dgrex_Dm_Quocgia_EmbeddedNavigator_ButtonClick);
            this.dgrex_Dm_Quocgia.Location = new System.Drawing.Point(0, 130);
            this.dgrex_Dm_Quocgia.MainView = this.gridView1;
            this.dgrex_Dm_Quocgia.Name = "dgrex_Dm_Quocgia";
            this.dgrex_Dm_Quocgia.Size = new System.Drawing.Size(1011, 581);
            this.dgrex_Dm_Quocgia.TabIndex = 1;
            this.dgrex_Dm_Quocgia.UseEmbeddedNavigator = true;
            this.dgrex_Dm_Quocgia.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.GridControl = this.dgrex_Dm_Quocgia;
            this.gridView1.GroupPanelText = "[Kéo một cột vào đây để nhóm]";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Số thứ tự";
            this.gridColumn1.FieldName = "Id_Quocgia";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Mã quốc gia";
            this.gridColumn2.FieldName = "Ma_Quocgia";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Tên quốc gia";
            this.gridColumn3.FieldName = "Ten_Quocgia";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.tableLayoutPanel1);
            this.panelControl1.Controls.Add(this.txtId_Quocgia);
            this.panelControl1.Controls.Add(this.lblId_Quocgia);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 65);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1011, 65);
            this.panelControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblMa_Quocgia, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtTen_Quocgia, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMa_Quocgia, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTen_Quocgia, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1007, 61);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // lblMa_Quocgia
            // 
            this.lblMa_Quocgia.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMa_Quocgia.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblMa_Quocgia.Location = new System.Drawing.Point(3, 6);
            this.lblMa_Quocgia.Name = "lblMa_Quocgia";
            this.lblMa_Quocgia.Size = new System.Drawing.Size(85, 19);
            this.lblMa_Quocgia.TabIndex = 2;
            this.lblMa_Quocgia.Text = "Mã quốc gia";
            // 
            // txtTen_Quocgia
            // 
            this.txtTen_Quocgia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTen_Quocgia.Location = new System.Drawing.Point(518, 3);
            this.txtTen_Quocgia.Name = "txtTen_Quocgia";
            this.txtTen_Quocgia.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtTen_Quocgia.Properties.Appearance.Options.UseFont = true;
            this.txtTen_Quocgia.Size = new System.Drawing.Size(300, 26);
            this.txtTen_Quocgia.TabIndex = 1;
            // 
            // txtMa_Quocgia
            // 
            this.txtMa_Quocgia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMa_Quocgia.Location = new System.Drawing.Point(94, 3);
            this.txtMa_Quocgia.Name = "txtMa_Quocgia";
            this.txtMa_Quocgia.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtMa_Quocgia.Properties.Appearance.Options.UseFont = true;
            this.txtMa_Quocgia.Size = new System.Drawing.Size(300, 26);
            this.txtMa_Quocgia.TabIndex = 0;
            this.txtMa_Quocgia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMa_Quocgia_KeyPress);
            // 
            // lblTen_Quocgia
            // 
            this.lblTen_Quocgia.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTen_Quocgia.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblTen_Quocgia.Location = new System.Drawing.Point(420, 6);
            this.lblTen_Quocgia.Name = "lblTen_Quocgia";
            this.lblTen_Quocgia.Size = new System.Drawing.Size(92, 19);
            this.lblTen_Quocgia.TabIndex = 4;
            this.lblTen_Quocgia.Text = "Tên quốc gia";
            // 
            // txtId_Quocgia
            // 
            this.txtId_Quocgia.Enabled = false;
            this.txtId_Quocgia.Location = new System.Drawing.Point(988, 19);
            this.txtId_Quocgia.Name = "txtId_Quocgia";
            this.txtId_Quocgia.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtId_Quocgia.Properties.Appearance.Options.UseFont = true;
            this.txtId_Quocgia.Size = new System.Drawing.Size(100, 26);
            this.txtId_Quocgia.TabIndex = 0;
            this.txtId_Quocgia.Visible = false;
            // 
            // lblId_Quocgia
            // 
            this.lblId_Quocgia.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblId_Quocgia.Location = new System.Drawing.Point(915, 23);
            this.lblId_Quocgia.Name = "lblId_Quocgia";
            this.lblId_Quocgia.Size = new System.Drawing.Size(67, 19);
            this.lblId_Quocgia.TabIndex = 0;
            this.lblId_Quocgia.Text = "Số thứ tự";
            this.lblId_Quocgia.Visible = false;
            // 
            // xtraHNavigator1
            // 
            this.xtraHNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xtraHNavigator1.GridControl = this.dgrex_Dm_Quocgia;
            this.xtraHNavigator1.Location = new System.Drawing.Point(0, 711);
            this.xtraHNavigator1.Margin = new System.Windows.Forms.Padding(0);
            this.xtraHNavigator1.Name = "xtraHNavigator1";
            this.xtraHNavigator1.Size = new System.Drawing.Size(1011, 24);
            this.xtraHNavigator1.TabIndex = 9;
            this.xtraHNavigator1.TextStringFormat = "Dòng: {0} / {1}";
            this.xtraHNavigator1.VisibleFirst = true;
            this.xtraHNavigator1.VisibleLast = true;
            this.xtraHNavigator1.VisibleNext = true;
            this.xtraHNavigator1.VisiblePageNext = true;
            this.xtraHNavigator1.VisiblePagePrev = true;
            this.xtraHNavigator1.VisiblePrev = true;
            // 
            // Frmrex_Dm_Quocgia_Add
            // 
            this.AllowRefresh = true;
            this.ClientSize = new System.Drawing.Size(1011, 735);
            this.Controls.Add(this.dgrex_Dm_Quocgia);
            this.Controls.Add(this.xtraHNavigator1);
            this.Controls.Add(this.panelControl1);
            this.Name = "Frmrex_Dm_Quocgia_Add";
            this.Load += new System.EventHandler(this.Frmrex_Dm_Quocgia_Add_Load);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.xtraHNavigator1, 0);
            this.Controls.SetChildIndex(this.dgrex_Dm_Quocgia, 0);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrex_Dm_Quocgia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTen_Quocgia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMa_Quocgia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId_Quocgia.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dgrex_Dm_Quocgia;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtTen_Quocgia;
        private DevExpress.XtraEditors.LabelControl lblTen_Quocgia;
        private DevExpress.XtraEditors.TextEdit txtMa_Quocgia;
        private DevExpress.XtraEditors.LabelControl lblMa_Quocgia;
        private DevExpress.XtraEditors.TextEdit txtId_Quocgia;
        private DevExpress.XtraEditors.LabelControl lblId_Quocgia;
        private  GoobizFrame.Windows.Controls.XtraHNavigator xtraHNavigator1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

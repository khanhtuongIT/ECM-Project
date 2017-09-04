namespace Ecm.SystemControl.Forms
{
    partial class Frmpol_Dm_Right_Find
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmpol_Dm_Right_Find));
            this.btbSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.dgPol_Dm_Right = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.chkId_Right = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPol_Dm_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkId_Right)).BeginInit();
            this.SuspendLayout();
            // 
            // btbSelect
            // 
            this.btbSelect.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btbSelect.Location = new System.Drawing.Point(326, 5);
            this.btbSelect.Name = "btbSelect";
            this.btbSelect.Size = new System.Drawing.Size(75, 23);
            this.btbSelect.TabIndex = 0;
            this.btbSelect.Text = "Đồng ý";
            this.btbSelect.Click += new System.EventHandler(this.btbSelect_Click);
            // 
            // btbCancel
            // 
            this.btbCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btbCancel.Location = new System.Drawing.Point(407, 5);
            this.btbCancel.Name = "btbCancel";
            this.btbCancel.Size = new System.Drawing.Size(75, 23);
            this.btbCancel.TabIndex = 1;
            this.btbCancel.Text = "Bỏ qua";
            this.btbCancel.Click += new System.EventHandler(this.btbCancel_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkAll);
            this.panelControl1.Controls.Add(this.btbCancel);
            this.panelControl1.Controls.Add(this.btbSelect);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 291);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(492, 32);
            this.panelControl1.TabIndex = 9;
            // 
            // chkAll
            // 
            this.chkAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkAll.Location = new System.Drawing.Point(2, 2);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Caption = "Chọn tất cả";
            this.chkAll.Size = new System.Drawing.Size(118, 19);
            this.chkAll.TabIndex = 3;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // dgPol_Dm_Right
            // 
            this.dgPol_Dm_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPol_Dm_Right.Location = new System.Drawing.Point(0, 0);
            this.dgPol_Dm_Right.MainView = this.gridView1;
            this.dgPol_Dm_Right.Name = "dgPol_Dm_Right";
            this.dgPol_Dm_Right.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkId_Right,
            this.repositoryItemCheckEdit1});
            this.dgPol_Dm_Right.Size = new System.Drawing.Size(492, 291);
            this.dgPol_Dm_Right.TabIndex = 0;
            this.dgPol_Dm_Right.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.GridControl = this.dgPol_Dm_Right;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "STT";
            this.gridColumn1.FieldName = "Id_Right";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 65;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Quyền";
            this.gridColumn2.FieldName = "Right_System_Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 141;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mô tả";
            this.gridColumn3.FieldName = "Right_Description";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 213;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Chọn";
            this.gridColumn4.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn4.FieldName = "Checked";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 52;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // chkId_Right
            // 
            this.chkId_Right.AutoHeight = false;
            this.chkId_Right.Name = "chkId_Right";
            this.chkId_Right.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkId_Right.ValueGrayed = false;
            // 
            // Frmpol_Dm_Right_Find
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 323);
            this.Controls.Add(this.dgPol_Dm_Right);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frmpol_Dm_Right_Find";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Frmpol_Dm_Right_Find_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPol_Dm_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkId_Right)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btbSelect;
        private DevExpress.XtraEditors.SimpleButton btbCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl dgPol_Dm_Right;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkId_Right;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.CheckEdit chkAll;
    }
}
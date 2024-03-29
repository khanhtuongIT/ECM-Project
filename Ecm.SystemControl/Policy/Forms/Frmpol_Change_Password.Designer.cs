namespace SunLine.SystemControl.Policy.Forms
{
    partial class Frmpol_Change_Password
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmpol_Change_Password));
            this.lblOld_Pass = new DevExpress.XtraEditors.LabelControl();
            this.lblNew_Pass = new DevExpress.XtraEditors.LabelControl();
            this.lblConfirm_New = new DevExpress.XtraEditors.LabelControl();
            this.txtOld_Pass = new DevExpress.XtraEditors.TextEdit();
            this.txtNew_Pass = new DevExpress.XtraEditors.TextEdit();
            this.txtConfirm_New = new DevExpress.XtraEditors.TextEdit();
            this.btbOk = new DevExpress.XtraEditors.SimpleButton();
            this.btbCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtOld_Pass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNew_Pass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirm_New.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOld_Pass
            // 
            this.lblOld_Pass.Location = new System.Drawing.Point(11, 16);
            this.lblOld_Pass.Name = "lblOld_Pass";
            this.lblOld_Pass.Size = new System.Drawing.Size(82, 13);
            this.lblOld_Pass.TabIndex = 0;
            this.lblOld_Pass.Text = "Mật khẩu hiện tại";
            // 
            // lblNew_Pass
            // 
            this.lblNew_Pass.Location = new System.Drawing.Point(30, 45);
            this.lblNew_Pass.Name = "lblNew_Pass";
            this.lblNew_Pass.Size = new System.Drawing.Size(63, 13);
            this.lblNew_Pass.TabIndex = 1;
            this.lblNew_Pass.Text = "Mật khẩu mới";
            // 
            // lblConfirm_New
            // 
            this.lblConfirm_New.Location = new System.Drawing.Point(30, 73);
            this.lblConfirm_New.Name = "lblConfirm_New";
            this.lblConfirm_New.Size = new System.Drawing.Size(63, 13);
            this.lblConfirm_New.TabIndex = 2;
            this.lblConfirm_New.Text = "Xác nhận mới";
            // 
            // txtOld_Pass
            // 
            this.txtOld_Pass.Location = new System.Drawing.Point(102, 13);
            this.txtOld_Pass.Name = "txtOld_Pass";
            this.txtOld_Pass.Properties.Mask.EditMask = "([A-Za-z0-9_]+)";
            this.txtOld_Pass.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtOld_Pass.Properties.PasswordChar = '*';
            this.txtOld_Pass.Size = new System.Drawing.Size(250, 20);
            this.txtOld_Pass.TabIndex = 3;
            // 
            // txtNew_Pass
            // 
            this.txtNew_Pass.Location = new System.Drawing.Point(102, 42);
            this.txtNew_Pass.Name = "txtNew_Pass";
            this.txtNew_Pass.Properties.Mask.EditMask = "([A-Za-z0-9_]+)";
            this.txtNew_Pass.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNew_Pass.Properties.PasswordChar = '*';
            this.txtNew_Pass.Size = new System.Drawing.Size(250, 20);
            this.txtNew_Pass.TabIndex = 4;
            // 
            // txtConfirm_New
            // 
            this.txtConfirm_New.Location = new System.Drawing.Point(102, 71);
            this.txtConfirm_New.Name = "txtConfirm_New";
            this.txtConfirm_New.Properties.Mask.EditMask = "([A-Za-z0-9_]+)";
            this.txtConfirm_New.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtConfirm_New.Properties.PasswordChar = '*';
            this.txtConfirm_New.Size = new System.Drawing.Size(250, 20);
            this.txtConfirm_New.TabIndex = 5;
            // 
            // btbOk
            // 
            this.btbOk.Location = new System.Drawing.Point(196, 99);
            this.btbOk.Name = "btbOk";
            this.btbOk.Size = new System.Drawing.Size(75, 23);
            this.btbOk.TabIndex = 6;
            this.btbOk.Text = "Đồng ý";
            this.btbOk.Click += new System.EventHandler(this.btbOk_Click);
            // 
            // btbCancel
            // 
            this.btbCancel.Location = new System.Drawing.Point(277, 99);
            this.btbCancel.Name = "btbCancel";
            this.btbCancel.Size = new System.Drawing.Size(75, 23);
            this.btbCancel.TabIndex = 7;
            this.btbCancel.Text = "Bỏ qua";
            this.btbCancel.Click += new System.EventHandler(this.btbCancel_Click);
            // 
            // Frmpol_Change_Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 125);
            this.Controls.Add(this.btbCancel);
            this.Controls.Add(this.btbOk);
            this.Controls.Add(this.txtConfirm_New);
            this.Controls.Add(this.txtNew_Pass);
            this.Controls.Add(this.txtOld_Pass);
            this.Controls.Add(this.lblConfirm_New);
            this.Controls.Add(this.lblNew_Pass);
            this.Controls.Add(this.lblOld_Pass);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frmpol_Change_Password";
            this.Text = "Frmpol_Change_Password";
            ((System.ComponentModel.ISupportInitialize)(this.txtOld_Pass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNew_Pass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirm_New.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblOld_Pass;
        private DevExpress.XtraEditors.LabelControl lblNew_Pass;
        private DevExpress.XtraEditors.LabelControl lblConfirm_New;
        private DevExpress.XtraEditors.TextEdit txtOld_Pass;
        private DevExpress.XtraEditors.TextEdit txtNew_Pass;
        private DevExpress.XtraEditors.TextEdit txtConfirm_New;
        private DevExpress.XtraEditors.SimpleButton btbOk;
        private DevExpress.XtraEditors.SimpleButton btbCancel;
    }
}
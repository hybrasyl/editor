namespace HybrasylEditor.UI
{
    partial class OptionsForm
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
            this.btnSelectDarkagesInstallDirectory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDarkagesInstallPath = new System.Windows.Forms.TextBox();
            this.txtHybrasylWorldDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectHybrasylWorldDirectory = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectDarkagesInstallDirectory
            // 
            this.btnSelectDarkagesInstallDirectory.Location = new System.Drawing.Point(265, 40);
            this.btnSelectDarkagesInstallDirectory.Name = "btnSelectDarkagesInstallDirectory";
            this.btnSelectDarkagesInstallDirectory.Size = new System.Drawing.Size(40, 21);
            this.btnSelectDarkagesInstallDirectory.TabIndex = 0;
            this.btnSelectDarkagesInstallDirectory.Text = "...";
            this.btnSelectDarkagesInstallDirectory.UseVisualStyleBackColor = true;
            this.btnSelectDarkagesInstallDirectory.Click += new System.EventHandler(this.btnSelectDarkagesInstallDirectory_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Darkages Install Directory:";
            // 
            // txtDarkagesInstallPath
            // 
            this.txtDarkagesInstallPath.Location = new System.Drawing.Point(23, 40);
            this.txtDarkagesInstallPath.Name = "txtDarkagesInstallPath";
            this.txtDarkagesInstallPath.Size = new System.Drawing.Size(236, 20);
            this.txtDarkagesInstallPath.TabIndex = 2;
            // 
            // txtHybrasylWorldDirectory
            // 
            this.txtHybrasylWorldDirectory.Location = new System.Drawing.Point(23, 101);
            this.txtHybrasylWorldDirectory.Name = "txtHybrasylWorldDirectory";
            this.txtHybrasylWorldDirectory.Size = new System.Drawing.Size(236, 20);
            this.txtHybrasylWorldDirectory.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hybrasyl \"World\" Directory:";
            // 
            // btnSelectHybrasylWorldDirectory
            // 
            this.btnSelectHybrasylWorldDirectory.Location = new System.Drawing.Point(265, 101);
            this.btnSelectHybrasylWorldDirectory.Name = "btnSelectHybrasylWorldDirectory";
            this.btnSelectHybrasylWorldDirectory.Size = new System.Drawing.Size(40, 21);
            this.btnSelectHybrasylWorldDirectory.TabIndex = 3;
            this.btnSelectHybrasylWorldDirectory.Text = "...";
            this.btnSelectHybrasylWorldDirectory.UseVisualStyleBackColor = true;
            this.btnSelectHybrasylWorldDirectory.Click += new System.EventHandler(this.btnSelectHybrasylWorldDirectory_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(238, 188);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(157, 188);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 226);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtHybrasylWorldDirectory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelectHybrasylWorldDirectory);
            this.Controls.Add(this.txtDarkagesInstallPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectDarkagesInstallDirectory);
            this.Name = "OptionsForm";
            this.Text = "OptionsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectDarkagesInstallDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDarkagesInstallPath;
        private System.Windows.Forms.TextBox txtHybrasylWorldDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectHybrasylWorldDirectory;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}
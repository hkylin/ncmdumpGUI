namespace ncmdumpGUI
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectMp3Folder = new System.Windows.Forms.Button();
            this.txtMp3FolderPath = new System.Windows.Forms.TextBox();
            this.btnSelectNcmFolder = new System.Windows.Forms.Button();
            this.txtNcmFolderPath = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ncm文件目录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "mp3文件目录：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "将ncm文件转成mp3文件";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectMp3Folder);
            this.groupBox1.Controls.Add(this.txtMp3FolderPath);
            this.groupBox1.Controls.Add(this.btnSelectNcmFolder);
            this.groupBox1.Controls.Add(this.txtNcmFolderPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 108);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnSelectMp3Folder
            // 
            this.btnSelectMp3Folder.Location = new System.Drawing.Point(422, 72);
            this.btnSelectMp3Folder.Name = "btnSelectMp3Folder";
            this.btnSelectMp3Folder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectMp3Folder.TabIndex = 5;
            this.btnSelectMp3Folder.Text = "选择目录";
            this.btnSelectMp3Folder.UseVisualStyleBackColor = true;
            this.btnSelectMp3Folder.Click += new System.EventHandler(this.btnSelectMp3Folder_Click);
            // 
            // txtMp3FolderPath
            // 
            this.txtMp3FolderPath.Location = new System.Drawing.Point(95, 74);
            this.txtMp3FolderPath.Name = "txtMp3FolderPath";
            this.txtMp3FolderPath.Size = new System.Drawing.Size(313, 21);
            this.txtMp3FolderPath.TabIndex = 4;
            // 
            // btnSelectNcmFolder
            // 
            this.btnSelectNcmFolder.Location = new System.Drawing.Point(422, 26);
            this.btnSelectNcmFolder.Name = "btnSelectNcmFolder";
            this.btnSelectNcmFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectNcmFolder.TabIndex = 2;
            this.btnSelectNcmFolder.Text = "选择目录";
            this.btnSelectNcmFolder.UseVisualStyleBackColor = true;
            this.btnSelectNcmFolder.Click += new System.EventHandler(this.btnSelectNcmFolder_Click);
            // 
            // txtNcmFolderPath
            // 
            this.txtNcmFolderPath.Location = new System.Drawing.Point(95, 28);
            this.txtNcmFolderPath.Name = "txtNcmFolderPath";
            this.txtNcmFolderPath.Size = new System.Drawing.Size(313, 21);
            this.txtNcmFolderPath.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(228, 169);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "开始转换";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 204);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(549, 243);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(549, 243);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ncmdumpGUI by kpali v1.1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSelectMp3Folder;
        private System.Windows.Forms.TextBox txtMp3FolderPath;
        private System.Windows.Forms.Button btnSelectNcmFolder;
        private System.Windows.Forms.TextBox txtNcmFolderPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}


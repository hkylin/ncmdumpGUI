using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace ncmdumpGUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        FileInfo configFileInfo;

        private void Main_Load(object sender, EventArgs e)
        {
            StreamReader configFileReader = null;
            try
            {
                configFileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "config");
                if (configFileInfo.Exists)
                {
                    configFileReader = configFileInfo.OpenText();
                    while(!configFileReader.EndOfStream)
                    {
                        String line = configFileReader.ReadLine().Trim();
                        if (String.IsNullOrEmpty(line) || !line.Contains("="))
                        {
                            continue;
                        }
                        String[] config = line.Split('=');
                        String key = config[0];
                        String value = config[1];
                        if (key == "ncmFolderPath")
                        {
                            this.txtNcmFolderPath.Text = value;
                        }
                        else if (key == "mp3FolderPath")
                        {
                            this.txtMp3FolderPath.Text = value;
                        }
                    }
                    configFileReader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            finally
            {
                if (configFileReader != null)
                    configFileReader.Close();
            }
        }

        private void btnSelectNcmFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = "";
            DialogResult dlg = folderBrowserDialog.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                txtNcmFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnSelectMp3Folder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = "";
            DialogResult dlg = folderBrowserDialog.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                txtMp3FolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        Thread backgroundWork;
        delegate void DelUIThreadOperation();
        DelUIThreadOperation delUIThreadOperation;

        private void btnStart_Click(object sender, EventArgs e)
        {
            backgroundWork = new Thread(ConvertProc);
            backgroundWork.Start();
        }

        private void ConvertProc()
        {
            ProgressDialogControl progressDialogControl = new ProgressDialogControl();
            IAsyncResult asyncResult;
            try
            {
                BeginInvoke(progressDialogControl.delProgressDlg, ProgressStatusType.BackgroundWorkStart, "正在转换文件，请稍候......");
                while (!progressDialogControl.IsProgressDlgHandleCreate)
                {
                    Thread.Sleep(100);
                }

                string ncmFolderPath = "";
                string mp3FolderPath = "";

                delUIThreadOperation = new DelUIThreadOperation(delegate ()
                {
                    ncmFolderPath = this.txtNcmFolderPath.Text;
                    mp3FolderPath = this.txtMp3FolderPath.Text;
                });
                asyncResult = BeginInvoke(delUIThreadOperation);
                EndInvoke(asyncResult);

                StreamWriter configFileWriter = null;
                if (configFileInfo.Exists)
                {
                    File.Delete(configFileInfo.FullName);
                }
                try
                {
                    configFileWriter = configFileInfo.CreateText();
                    configFileWriter.WriteLine("ncmFolderPath=" + ncmFolderPath);
                    configFileWriter.WriteLine("mp3FolderPath=" + mp3FolderPath);
                    configFileWriter.Flush();
                }
                finally
                {
                    if (configFileWriter != null)
                        configFileWriter.Close();
                }

                DirectoryInfo ncmDirctoryInfo = new DirectoryInfo(ncmFolderPath);
                DirectoryInfo mp3DirctoryInfo = new DirectoryInfo(mp3FolderPath);
                foreach (FileInfo fileInfo in ncmDirctoryInfo.GetFiles("*.ncm"))
                {
                    BeginInvoke(progressDialogControl.delProgressDlg, ProgressStatusType.BackgroundWorkUpdate, "转换：" + fileInfo.Name);
                    NeteaseCrypto neteaseFile = new NeteaseCrypto(fileInfo);
                    neteaseFile.Dump(mp3FolderPath);
                }

                delUIThreadOperation = new DelUIThreadOperation(delegate ()
                {
                    MessageBox.Show("转换完成！","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
                asyncResult = BeginInvoke(delUIThreadOperation);
                EndInvoke(asyncResult);
            }
            catch (Exception ex)
            {
                delUIThreadOperation = new DelUIThreadOperation(delegate ()
                {
                    MessageBox.Show("转换失败！" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                });
                asyncResult = BeginInvoke(delUIThreadOperation);
                EndInvoke(asyncResult);
            }
            finally
            {
                if (progressDialogControl.IsProgressDlgAlive)
                {
                    asyncResult = BeginInvoke(progressDialogControl.delProgressDlg, ProgressStatusType.BackgroundWorkStop, "");
                    EndInvoke(asyncResult);
                }
            }
        }
    }
}

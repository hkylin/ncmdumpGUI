using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ncmdumpGUI
{
    public partial class ProgressDlg : Form
    {
        private Action _OnCancel;
        public ProgressDlg(String text, String caption)
        {
            InitializeComponent();
            this.btnCancel.Visible = false;
            this.labelProgress.Text = text;
            this.Text = caption;
            //this.Width = 24 + this.labelProgress.Size.Width + 24;
            //this.labelProgress.Location = new Point(((this.Width-labelProgress.Size.Width)/2) + 26, this.labelProgress.Location.Y);
            this.labelProgress.Location = new Point(33, this.labelProgress.Location.Y);
        }
        bool isCancelInProgress = false;

        public void ShowProgress(Form parent)
        {
            StartPosition = FormStartPosition.Manual;
            int sY, sX;
            sY = (parent.Height - Height) / 2;
            sX = (parent.Width - Width) / 2;
            Location = parent.PointToScreen(new Point(sX, sY));
            Show(parent);
            Update();
        }

        public void setCancelButtonVisible(bool visible)
        {
            this.btnCancel.Visible = visible;
        }

        public bool getIsCancelInProgress()
        {
            return this.isCancelInProgress;
        }

        /// <summary>
        /// 设置取消按钮事件
        /// </summary>
        /// <param name="ac"></param>
        public void SetCancelEvent(Action ac)
        {
            _OnCancel = ac;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnCancel.Enabled = false;
            this.btnCancel.Text = "取消中...";
            isCancelInProgress = true;
            if (_OnCancel != null) _OnCancel();
        }
    }
}

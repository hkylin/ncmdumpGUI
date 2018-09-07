using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ncmdumpGUI
{
    public class ProgressDialogControl
    {
        private Action _cancelEvent;
        public ProgressDialogControl()
        {
            delProgressDlg = new DelProgressDlg(UpdateProgressDlg);
        }
        public ProgressDialogControl(System.Windows.Forms.Control parent)
        {
            _parentControl = parent;
            delProgressDlg = new DelProgressDlg(UpdateProgressDlg);
        }

        private System.Windows.Forms.Control _parentControl;
        ProgressDlg progressDlg;
        public delegate void DelProgressDlg(ProgressStatusType progressStatusType, String content);
        public DelProgressDlg delProgressDlg = null;

        private bool IsValid()
        {
            return null != _parentControl;
        }
        public IAsyncResult BeginProgressDlg(String msg)
        {
            if (!IsValid()) return null;
            return _parentControl.BeginInvoke(delProgressDlg, ProgressStatusType.BackgroundWorkStart, msg);
        }
        public IAsyncResult UpdateProgressDlg(String msg)
        {
            if (!IsValid()) return null;
            return _parentControl.BeginInvoke(delProgressDlg, ProgressStatusType.BackgroundWorkUpdate, msg);
        }
        public IAsyncResult EndProgressDlg(String msg = "")
        {
            if (!IsValid()) return null;
            return _parentControl.BeginInvoke(delProgressDlg, ProgressStatusType.BackgroundWorkStop, msg);
        }

        public ProgressDialogDel CreateProgressDialogDel()
        {
            ProgressDialogDel del = new ProgressDialogDel(BeginProgressDlg, UpdateProgressDlg, EndProgressDlg, _parentControl.EndInvoke, GetProgressDlgStatus);

            return del;
        }

        void UpdateProgressDlg(ProgressStatusType progressStatusType, String content)
        {
            switch (progressStatusType)
            {
                case ProgressStatusType.BackgroundWorkStart:
                    progressDlg = new ProgressDlg(content, "正在进行");
                    progressDlg.SetCancelEvent(_cancelEvent);
                    progressDlg.ShowDialog();
                    return;
                case ProgressStatusType.BackgroundWorkUpdate:
                    progressDlg.labelProgress.Text = content;
                    return;
                case ProgressStatusType.BackgroundWorkStop:
                    progressDlg.SetCancelEvent(null);
                    progressDlg.Close();
                    return;
            }
        }

        public bool IsProgressDlgAlive
        {
            get { return GetProgressDlgStatus(); }
        }

        public bool IsProgressDlgHandleCreate
        {
            get
            {
                if (progressDlg == null) return false;
                return progressDlg.IsHandleCreated;
            }
        }

        public void SetCancelButtonVisible(bool visible)
        {
            progressDlg.setCancelButtonVisible(visible);
        }

        public bool GetIsCancelInProgress()
        {
            return progressDlg.getIsCancelInProgress();
        }

        /// <summary>
        /// 设置取消按钮事件
        /// </summary>
        /// <param name="ac"></param>
        public void SetCancelEvent(Action ac)
        {
            _cancelEvent = ac;
        }

        /// <summary>
        /// 获取ProgressDlg的状态，true：未被释放 false：已被释放
        /// </summary>
        /// <returns></returns>
        public bool GetProgressDlgStatus()
        {
            if (progressDlg == null)
            {
                return false;
            }
            return !progressDlg.IsDisposed;
        }
    }

    public enum ProgressStatusType
    {
        BackgroundWorkStart,
        BackgroundWorkUpdate,
        BackgroundWorkStop,
    };
}

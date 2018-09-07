using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ncmdumpGUI
{
    /// <summary>
    /// 进度提示委托类
    /// </summary>
    public sealed class ProgressDialogDel
    {
        public ProgressDialogDel(PROGRESS_DIALOG begin, PROGRESS_DIALOG update, PROGRESS_DIALOG end, END_INVOKE endInvoke, IS_FUN isProgressDlgAlive)
        {
            BeginProgressDlg = begin;
            UpdateProgressDlg = update;
            EndProgressDlg = end;
            EndInvoke = endInvoke;
            ProgressDlgAlive = isProgressDlgAlive;
        }
        //启动提示窗体
        public PROGRESS_DIALOG BeginProgressDlg;
        //更新提示窗体
        public PROGRESS_DIALOG UpdateProgressDlg;
        //结束提示窗体
        public PROGRESS_DIALOG EndProgressDlg;
        public END_INVOKE EndInvoke;
        //判断当前提示窗体是否有效
        private IS_FUN ProgressDlgAlive;
        public bool IsProgressDlgAlive
        {
            get
            {
                return ProgressDlgAlive();
            }
        }
    }
}

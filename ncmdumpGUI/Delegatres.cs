using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ncmdumpGUI
{
    public delegate object END_INVOKE(IAsyncResult asynRet);
    public delegate IAsyncResult PROGRESS_DIALOG(String msg);
    public delegate bool IS_FUN();
}

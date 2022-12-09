using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CustomControls
{
    static class DialogParamConversion
    {
        public static DialogResult ConvertDialogResultTask2Form(this TaskDialogResult dlg)
        {
            switch (dlg)
            {
                case TaskDialogResult.Ok:
                    return DialogResult.OK;
                case TaskDialogResult.Yes:
                    return DialogResult.Yes;
                case TaskDialogResult.No:
                    return DialogResult.No;
                case TaskDialogResult.Retry:
                    return DialogResult.Retry;

                case TaskDialogResult.Cancel:
                    return DialogResult.Cancel;
                default:
                    throw new NotImplementedException();
            }
        }
        public static TaskDialogStandardButtons ConvertButtonsForm2Task(this MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    return TaskDialogStandardButtons.Ok;
                case MessageBoxButtons.OKCancel:
                    return TaskDialogStandardButtons.Ok | TaskDialogStandardButtons.Cancel;
                case MessageBoxButtons.YesNo:
                    return TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No;
                case MessageBoxButtons.YesNoCancel:
                    return TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No | TaskDialogStandardButtons.Cancel;
                case MessageBoxButtons.RetryCancel:
                    return TaskDialogStandardButtons.Retry | TaskDialogStandardButtons.Cancel;

                default:
                    throw new NotImplementedException();
            }
        }

    }
}

using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomControls
{
    /// <summary>
    /// http://grabacr.net/archives/105
    ///  
    /// Microsoft.WindowsAPICodePack.Dialogs.TaskDialog wrapper
    /// </summary>
    public class RichMessageBox
    {
        /// <summary>
        /// dllの製品名
        /// </summary>
        private static readonly string _productName = ExeInfo.GetInstance().Name;
        /// <summary>
        /// dllのバージョン
        /// </summary>
        private static readonly Version _productVersion = ExeInfo.GetInstance().Version;
        private RichMessageBox(IWin32Window owner)
        {
            _owner = owner;
        }
        private readonly IWin32Window _owner = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="instructionText"></param>
        /// <param name="caption"></param>
        /// <param name="icon"></param>
        /// <param name="buttons"></param>
        /// <param name="detailInfo"></param>
        /// <param name="defaultButton"></param>
        /// <returns></returns>
        private DialogResult Show(
                    string text,
                    string instructionText,
                    string caption,
                    RichMessageBoxIcon icon = RichMessageBoxIcon.None,
                    RichMessageBoxButton buttons = RichMessageBoxButton.Ok,
                    string detailInfo = "",
                    RichMessageBoxDefaultButton defaultButton = RichMessageBoxDefaultButton.Button1
                    )
        {
            if (!TaskDialog.IsPlatformSupported)//TaskDialogがサポート外
                throw new Exception($"{Properties.Resources.MsgErrUnsupported} ({nameof(TaskDialog)})");

            using (var dialog = new Microsoft.WindowsAPICodePack.Dialogs.TaskDialog()
            {
                Caption = caption,
                InstructionText = instructionText,
                Text = text,
                Icon = (TaskDialogStandardIcon)icon,
                StandardButtons = (TaskDialogStandardButtons)buttons,
                DetailsExpandedLabel = Properties.Resources.TxtDetailsExpandedLabel,
                DetailsCollapsedLabel = Properties.Resources.TxtDetailsCollapsedLabel,
                DetailsExpandedText = detailInfo,
            })
            {
                if (_owner != null)
                {
                    dialog.OwnerWindowHandle = _owner.Handle;
                    dialog.StartupLocation = TaskDialogStartupLocation.CenterOwner;
                }
                else
                    dialog.StartupLocation = TaskDialogStartupLocation.CenterScreen;

                dialog.Cancelable = !(((TaskDialogStandardButtons)buttons & TaskDialogStandardButtons.Cancel) == TaskDialogStandardButtons.Cancel);//YesNo/OKのときはxボタンを無効

                switch (defaultButton)
                {
                    case RichMessageBoxDefaultButton.Button2:
                        if (((TaskDialogStandardButtons)buttons & TaskDialogStandardButtons.No) == TaskDialogStandardButtons.No)
                            dialog.DefaultButton = TaskDialogDefaultButton.No;
                        else if (((TaskDialogStandardButtons)buttons & TaskDialogStandardButtons.Cancel) == TaskDialogStandardButtons.Cancel)
                            dialog.DefaultButton = TaskDialogDefaultButton.Cancel;

                        break;
                    case RichMessageBoxDefaultButton.Button3:
                        if (buttons == RichMessageBoxButton.YesNoCancel)
                            dialog.DefaultButton = TaskDialogDefaultButton.Cancel;

                        break;
                }

                var dlg = dialog.Show();
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

                    default:
                        return DialogResult.Cancel;
                }
            }
        }
        /// <summary>
        /// Shows a rich Message box.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="text"></param>
        /// <param name="instructionText"></param>
        /// <param name="caption"></param>
        /// <param name="icon"></param>
        /// <param name="buttons"></param>
        /// <param name="detailInfo"></param>
        /// <param name="defaultButton"></param>
        /// <returns></returns>
        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string instructionText,
            string caption,
            RichMessageBoxIcon icon = RichMessageBoxIcon.None,
            RichMessageBoxButton buttons = RichMessageBoxButton.Ok,
            string detailInfo = "",
                    RichMessageBoxDefaultButton defaultButton = RichMessageBoxDefaultButton.Button1
            )
        {
            var dlg = new RichMessageBox(owner);
            return dlg.Show(text, instructionText, caption, icon, buttons, detailInfo, defaultButton);
        }
        /// <summary>
        /// Shows a rich Message box.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="text"></param>
        /// <param name="instructionText"></param>
        /// <param name="icon"></param>
        /// <param name="buttons"></param>
        /// <param name="detailInfo"></param>
        /// <param name="defaultButton"></param>
        /// <returns></returns>
        public static DialogResult Show(
             IWin32Window owner,
            string text,
            string instructionText,
            RichMessageBoxIcon icon = RichMessageBoxIcon.None,
            RichMessageBoxButton buttons = RichMessageBoxButton.Ok,
            string detailInfo = "",
             RichMessageBoxDefaultButton defaultButton = RichMessageBoxDefaultButton.Button1)
            => Show(owner, text, instructionText, $"{_productName}({_productVersion})", icon, buttons, detailInfo, defaultButton);
    }
    /// <summary>
    /// Specifies the icon displayed in a dialog.
    /// </summary>
    public enum RichMessageBoxIcon
    {
        /// <summary>
        /// Displays no icons (default).
        /// </summary>
        None = TaskDialogStandardIcon.None,
        /// <summary>
        /// Displays the User Account Control shield.
        /// </summary>
        Shield = TaskDialogStandardIcon.Shield,
        /// <summary>
        /// Displays the Information icon.
        /// </summary>
        Information = TaskDialogStandardIcon.Information,
        /// <summary>
        /// Displays the error icon.
        /// </summary>
        Error = TaskDialogStandardIcon.Error,
        /// <summary>
        /// Displays the warning icon.
        /// </summary>
        Warning = TaskDialogStandardIcon.Warning
    }

    /// <summary>
    /// Identifies one of the standard buttons that can be displayed on RichMessageBox.
    /// </summary>
    public enum RichMessageBoxButton
    {
        /// <summary>
        /// An "OK" button.
        /// </summary>
        Ok = TaskDialogStandardButtons.Ok,

        /// <summary>
        /// "Yes" and "No" buttons.
        /// </summary>
        YesNo = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No,

        /// <summary>
        /// "Yes", "No", and "Cancel" buttons.
        /// </summary>
        YesNoCancel = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No | TaskDialogStandardButtons.Cancel,

        /// <summary>
        /// "OK" and "Cancel" buttons.
        /// </summary>
        OkCancel = TaskDialogStandardButtons.Ok | TaskDialogStandardButtons.Cancel,

        /// <summary>
        /// "Retry" and "Cancel" buttons.
        /// </summary>
        RetryCancel = TaskDialogStandardButtons.Retry | TaskDialogStandardButtons.Cancel,
    }
    /// <summary>
    /// Specifies constants defining the default button on RichMessageBox.
    /// </summary>
    public enum RichMessageBoxDefaultButton
    {
        /// <summary>
        /// The first button on the message box is the default button.
        /// </summary>
        Button1 = MessageBoxDefaultButton.Button1,
        /// <summary>
        /// The second button on the message box is the default button.
        /// </summary>
        Button2 = MessageBoxDefaultButton.Button2,
        /// <summary>
        /// The third button on the message box is the default button.
        /// </summary>
        Button3 = MessageBoxDefaultButton.Button3
    }
}
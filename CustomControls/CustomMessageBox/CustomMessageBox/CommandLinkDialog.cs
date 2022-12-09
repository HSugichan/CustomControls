using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Forms;

namespace CustomControls
{
    /// <summary>
    /// Command link dialog to notify warning.
    /// http://grabacr.net/archives/288
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class CommandLinkDialog<TResult> where TResult : struct, Enum
    {
        /// <summary>
        /// メンバ変数なのはPrivateなShowとPublic staticなShowメソッドのsignatureを分けるため
        /// </summary>
        private readonly IWin32Window _owner = null;

        private CommandLinkDialog(IWin32Window owner)
        {
            _owner = owner;
        }

        private TResult Show(string text,
            string instructionText,
            string caption,
            CommandLinkIcon icon,
            CommandLinkButton[] commandLinkTables)
        {
            TResult dialogResult = default;

            using (var dlg = new TaskDialog()
            {
                Cancelable = false,//Enable a X button.
                Caption = caption,
                InstructionText = instructionText,
                Text = text,
                Icon = TaskDialogStandardIcon.Warning
            })
            {
                if (commandLinkTables.Length < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    dlg.OwnerWindowHandle = _owner.Handle;
                    dlg.StartupLocation = TaskDialogStartupLocation.CenterOwner;
                }


                for (int i = 0; i < commandLinkTables.Length; i++)
                {
                    var commandLinkTbl = commandLinkTables[i];
                    var commandLink = new TaskDialogCommandLink($"link_{i}", commandLinkTbl.TextOverview)
                    {
                        UseElevationIcon = commandLinkTbl.UseElevationIcon,
                        Instruction = commandLinkTbl.TextDetail,
                        Default = commandLinkTbl.IsDefault,
                    };
                    commandLink.Click += (s, e) =>
                    {
                        dialogResult = commandLinkTbl.Result;
                        dlg.Close();
                    };
                    dlg.Controls.Add(commandLink);
                }


                dlg.Show();
            }

            return dialogResult;
        }
        /// <summary>
        /// Show custum dialog.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="text"></param>
        /// <param name="instructionText"></param>
        /// <param name="caption"></param>
        /// <param name="icon"></param>
        /// <param name="commandLinkTables"></param>
        /// <returns></returns>
        public static TResult Show(IWin32Window owner, string text,
                    string instructionText,
                    string caption, CommandLinkIcon icon, CommandLinkButton[] commandLinkTables)
        {
            var commandLinkDialog = new CommandLinkDialog<TResult>(owner);
            return commandLinkDialog.Show(text, instructionText, caption, icon, commandLinkTables);
        }

        /// <summary>
        /// Parameter Class for Link Button.
        /// </summary>
        public class CommandLinkButton
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="textOverview">Button text</param>
            /// <param name="result">Dialog result</param>
            /// <param name="isDefault">Default or not</param>
            public CommandLinkButton(string textOverview, TResult result, bool isDefault = false)
            {
                TextOverview = textOverview;
                Result = result;
                IsDefault = isDefault;
            }
            /// <summary>
            /// Constructor for detail setting.
            /// </summary>
            /// <param name="textOverview"></param>
            /// <param name="textDetail"></param>
            /// <param name="result"></param>
            /// <param name="isDefault"></param>
            /// <param name="useElevationIcon"></param>
            public CommandLinkButton(string textOverview, string textDetail,
                TResult result, bool isDefault, bool useElevationIcon = false)
                : this(textOverview, result, isDefault)
            {
                TextDetail = textDetail;
                UseElevationIcon = useElevationIcon;
            }
            /// <summary>
            /// Overview of button.
            /// </summary>
            public string TextOverview { get; }
            /// <summary>
            /// Detail of button.
            /// </summary>
            public string TextDetail { get; }
            /// <summary>
            /// Use privilege elevation icon or don't
            /// </summary>
            public bool UseElevationIcon { get; }
            /// <summary>
            /// Dialog result of button.
            /// </summary>
            public TResult Result { get; }
            /// <summary>
            /// Default button or not.
            /// </summary>
            public bool IsDefault { get; }
        }
    }
    /// <summary>
    /// Specifies the icon displayed in a dialog.
    /// </summary>
    public enum CommandLinkIcon
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

}
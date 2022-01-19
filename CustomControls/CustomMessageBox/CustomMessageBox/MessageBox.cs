using System;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace CustomControls
{
    /// <summary>
    /// メッセージボックスクラス
    /// </summary>
    public class MessageBox
    {
        /// <summary>
        /// フックハンドル
        /// </summary>
        private IntPtr m_hHook = (IntPtr)0;

        /// <summary>
        /// メッセージボックスを表示する
        /// </summary>
        /// <param name="owner">オーナーウィンドゥ</param>
        /// <param name="text">メッセージテキスト</param>
        /// <param name="caption">タイトル</param>
        /// <param name="buttons">ボタン</param>
        /// <param name="icon">アイコン</param>
        /// <param name="defaultButton">デフォルトボタン</param>
        /// <returns></returns>
        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            var mbox = new MessageBox(owner);
            return mbox.Show(text, caption, icon, buttons, defaultButton);
        }
        /// <summary>
        /// メッセージボックスを表示する
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="text"></param>
        /// <param name="icon"></param>
        /// <param name="buttons"></param>
        /// <param name="defaultButton"></param>
        /// <returns></returns>
        public static DialogResult Show(
            IWin32Window owner,
            string text,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
            => Show(owner, text, $"{_productName}({_productVersion})", icon, buttons, defaultButton);
        /// <summary>
        /// メッセージボックスを表示する
        /// </summary>
        /// <param name="text"></param>
        /// <param name="icon"></param>
        /// <param name="buttons"></param>
        /// <param name="defaultButton"></param>
        /// <returns></returns>
        public static DialogResult Show(
            string text,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
            => Show(Owner, text, $"{_productName}({_productVersion})", icon, buttons, defaultButton);
        /// <summary>
        /// dllの製品名
        /// </summary>
        private static readonly string _productName = ExeInfo.GetInstance().Name;
        /// <summary>
        /// dllのバージョン
        /// </summary>
        private static readonly Version _productVersion = ExeInfo.GetInstance().Version;
        private static IWin32Window Owner { get; set; } = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="window">親ウィンドウ</param>
        private MessageBox(IWin32Window window)
        {
            Owner = window;
        }

        /// <summary>
        /// メッセージボックスを表示する
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultButton"></param>
        /// <returns></returns>
        private DialogResult Show(
            string text,
            string caption,
            MessageBoxIcon icon,
            MessageBoxButtons buttons,
            MessageBoxDefaultButton defaultButton )
        {
           
            if (Owner == null)
                return System.Windows.Forms.MessageBox.Show(text, caption, buttons, icon, defaultButton);
            // フックを設定する。
            if(Owner is System.Windows.Forms.Form)
            {
                if (((System.Windows.Forms.Form)Owner).InvokeRequired)
                {
                    return (DialogResult)((System.Windows.Forms.Form)Owner).Invoke
                        ((Func<DialogResult>)(() => Show(text, caption, icon, buttons, defaultButton)));                   
                }
            }
            IntPtr hInstance = WinAPI.GetWindowLong(Owner.Handle, WinAPI.GWL_HINSTANCE);
            IntPtr threadId = WinAPI.GetCurrentThreadId();
            m_hHook = WinAPI.SetWindowsHookEx(WinAPI.WH_CBT, new WinAPI.HOOKPROC(HookProc), hInstance, threadId);

            return System.Windows.Forms.MessageBox.Show(Owner, text, caption, buttons, icon, defaultButton);
        }

        /// <summary>
        /// フックプロシージャ
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        internal IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {

            if (nCode == WinAPI.HCBT_ACTIVATE)
            {
                WinAPI.RECT rcForm = new WinAPI.RECT(0, 0, 0, 0);
                WinAPI.RECT rcMsgBox = new WinAPI.RECT(0, 0, 0, 0);
                WinAPI.GetWindowRect(Owner.Handle, out rcForm);
                WinAPI.GetWindowRect(wParam, out rcMsgBox);

                // センター位置を計算する。
                int x = (rcForm.Left + (rcForm.Right - rcForm.Left) / 2) - ((rcMsgBox.Right - rcMsgBox.Left) / 2);
                int y = (rcForm.Top + (rcForm.Bottom - rcForm.Top) / 2) - ((rcMsgBox.Bottom - rcMsgBox.Top) / 2);

                WinAPI.SetWindowPos(wParam, 0, x, y, 0, 0, WinAPI.SWP_NOSIZE | WinAPI.SWP_NOZORDER | WinAPI.SWP_NOACTIVATE);

                IntPtr result = WinAPI.CallNextHookEx(m_hHook, nCode, wParam, lParam);

                // フックを解除する。
                WinAPI.UnhookWindowsHookEx(m_hHook);
                m_hHook = (IntPtr)0;

                return result;

            }
            else
            {
                return WinAPI.CallNextHookEx(m_hHook, nCode, wParam, lParam);
            }
        }


    }
}
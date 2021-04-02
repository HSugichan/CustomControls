using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using CustomControls.Properties;

namespace CustomControls
{
    /// <summary>
    /// アプリケーションのユーザー インターフェイスを構成するウィンドウまたはダイアログ ボックスを表します。
    /// エラーメッセージダイアログ表示メソッドを追加
    /// </summary>
    public class Form : System.Windows.Forms.Form
    {
        /// <summary>
        /// エラーメッセージダイアログ表示
        /// </summary>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <param name="buttons">ボタン</param>
        /// <returns></returns>
        public DialogResult ShowError(string errorMessage, MessageBoxButtons buttons = MessageBoxButtons.OK)
            => MessageBox.Show(this, errorMessage,  MessageBoxIcon.Error, buttons);
        /// <summary>
        /// メッセージダイアログ表示
        /// </summary>
        /// <param name="infoMessage"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public DialogResult ShowInfo(string infoMessage,  MessageBoxButtons buttons = MessageBoxButtons.OK)
            => MessageBox.Show(this, infoMessage,  MessageBoxIcon.Information, buttons);
        /// <summary>
        /// 警告ダイアログ表示
        /// </summary>
        /// <param name="warnMessage"></param>
        /// <param name="buttons"></param>
        /// <param name="defaultButton"></param>
        /// <returns></returns>
        public DialogResult ShowWarning(string warnMessage,  MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
            => MessageBox.Show(this, warnMessage,  MessageBoxIcon.Warning, buttons, defaultButton);
        /// <summary>
        /// クエスチョンダイアログの表示
        /// </summary>
        /// <param name="quentinoMessage"></param>
        /// <param name="buttons"></param>
        /// <param name="defaultButton"></param>
        /// <returns></returns>
        public DialogResult ShowQuetion(string quentinoMessage,MessageBoxButtons buttons = MessageBoxButtons.YesNo, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
            => MessageBox.Show(this, quentinoMessage,  MessageBoxIcon.Question, buttons, defaultButton);

        /// <summary>
        /// 指定親コントロール上にある全コントロールの取得
        /// </summary>
        /// <param name="parents"></param>
        /// <returns></returns>
        public static Control[] GetAllControls(params Control[] parents)
        {
            var listControls = new List<Control>();
            foreach (var parent in parents)
            {
                foreach (Control control in parent.Controls)
                {
                    listControls.Add(control);
                    var children = GetAllControls(control);
                    if (children.Length > 0)
                        listControls.AddRange(children);
                }
            }
            return listControls.Distinct().ToArray();
        }
    }

    /// <summary>
    /// Win API
    /// </summary>
    internal class WinAPI
    {
        /// <summary>
        /// user32.dllよりコール
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);
        /// <summary>
        /// kernel32.dllよりコール
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentThreadId();
        /// <summary>
        /// user32.dllよりコール
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="lpfn"></param>
        /// <param name="hInstance"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hInstance, IntPtr threadId);
        /// <summary>
        /// user32.dllよりコール
        /// </summary>
        /// <param name="hHook"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr hHook);
        /// <summary>
        /// user32.dllよりコール
        /// </summary>
        /// <param name="hHook"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);
        /// <summary>
        /// user32.dllよりコール
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        /// <summary>
        /// user32.dllよりコール
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);

        public const int GWL_HINSTANCE = (-6);
        public const int WH_CBT = 5;
        public const int HCBT_ACTIVATE = 5;

        public const int SWP_NOSIZE = 0x0001;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOACTIVATE = 0x0010;

        /// <summary>
        /// メッセージボックスを表す構造体
        /// </summary>
        public struct RECT
        {
            /// <summary>
            /// メッセージボックスを表す構造体
            /// </summary>
            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }

}
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
    public class FormCustom : System.Windows.Forms.Form
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
}
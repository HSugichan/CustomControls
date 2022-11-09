using System;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Text;

namespace CustomControls
{
    /// <summary>
    /// Show the informations of assemblies on UI.
    /// </summary>
    public partial class AsmInfoViewer : Form
    {
        /// <summary>
        /// EXE情報はデフォルトで追加済み
        /// 引数で渡した場合は、必ずDLLをコールしたEXEが先頭タブになる
        /// </summary>
        /// <param name="asmPaths">Assembly path.</param>
        private AsmInfoViewer(params string[] asmPaths)
        {
            InitializeComponent();

            var asmFileList = asmPaths.Distinct().ToList();//ファイル重複対策
            var exePath = ExeInfo.GetInstance().Path;
            if (asmFileList.Count < 1)
                asmFileList.Add(exePath);

            else if (asmFileList[0] != exePath)
            {
                asmFileList.Remove(exePath);
                asmFileList.Insert(0, exePath);
            }

            foreach (var asmFile in asmFileList.Where(x => System.IO.File.Exists(x)))
            {
                try
                {
                    var asmInfo = Assembly.LoadFile(asmFile);
                    var tabPage = new TabPage(System.IO.Path.GetFileName(asmFile));
                    var asmControl = new AsmInfoControl(asmInfo);
                    tabPage.Controls.Add(asmControl);
                    _tabAssembly.TabPages.Add(tabPage);

                    _stringBuilder.AppendLine(new string('-', 20));
                    _stringBuilder.AppendLine($"{asmControl.AsmName} ({asmControl.AsmVersion})");
                    _stringBuilder.AppendLine($"built {asmControl.BuildDateTimeUtc} (UTC)");
                    _stringBuilder.AppendLine(new string('-', 20));
                }
                catch { }
            }

        }
        readonly private StringBuilder _stringBuilder = new StringBuilder();
        /// <summary>
        /// Display a dialog whitch shows informations of assembly passed by filepath.
        /// </summary>
        /// <param name="owner">Window parent.</param>
        /// <param name="asmPaths">Assembly path.</param>
        public static void ShowVersionInfo(IWin32Window owner,params string[] asmPaths)
        {
            using (var fm = new AsmInfoViewer(asmPaths) { StartPosition = FormStartPosition.CenterParent })
            {
                fm.ShowDialog(owner);
                return;
            }
        }
        private void _btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AsmViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void _btnContextMenuCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_stringBuilder.ToString());
        }
    }
}
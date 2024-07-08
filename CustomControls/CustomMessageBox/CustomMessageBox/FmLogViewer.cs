using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomControls.Properties;

namespace CustomControls
{
    /// <summary>
    /// Log form
    /// </summary>
    public partial class FmLogViewer : Form
    {
        /// <summary>
        /// Getter
        /// </summary>
        /// <returns></returns>
        public static FmLogViewer GetFmLogViewer() => _logViewer;
        private static readonly FmLogViewer _logViewer = new FmLogViewer();
        static readonly object _lockObj = new object();
        /// <summary>
        /// Callback event
        /// </summary>
        public event Action<string> ChangedLogFile = null;

        readonly System.Timers.Timer _timer;
        private FmLogViewer()
        {
            InitializeComponent();

            _stringBuilder = new StringBuilder
            {
                Capacity = 10 * 1024,
            };

            StartPosition = FormStartPosition.Manual;

            ChangedLogFile += SetLogFilename;

            _timer = new System.Timers.Timer
            {
                Interval = 1000,
                AutoReset = true,
                Enabled = false
            };
            _timer.Elapsed += (s, e) => RefreshLog();

            Shown += (s, e) => _btnExit.Visible = TopLevel;

        }
        private readonly StringBuilder _stringBuilder = null;
        private string _logFileName;

        private void FmLogViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }
        /// <summary>
        /// Append log text
        /// </summary>
        /// <param name="text"></param>
        public void AppendText(string text)
        {
            if (!Visible)
                return;

            lock (_lockObj)
                _stringBuilder.Append(text);
        }
        /// <summary>
        /// Show Logger control
        /// </summary>
        /// <param name="point"></param>
        public void Show(System.Drawing.Point point)
        {
            if (Visible)
            {
                TopMost = true;
                TopMost = false;
                return;
            }
            Location = point;
            Visible = true;

        }
        /// <summary>
        /// Show Logger control
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public void Show(int x, int y) => Show(new System.Drawing.Point(x, y));
        /// <summary>
        /// Hide Logger control
        /// </summary>
        public new void Hide() => _logViewer.Visible = false;

        void RefreshLog()
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => RefreshLog()));
                return;
            }
            lock (_lockObj)
            {
                if (_stringBuilder.Length < 1)
                    return;
                string text = _stringBuilder.ToString();
                _tbxLog.AppendText(text);

                _stringBuilder.Clear();
            }
        }
        private void _btnSave_Click(object sender, EventArgs e)
        {
            string text = null;
            lock (_lockObj)
                text = _tbxLog.Text;

            if (string.IsNullOrWhiteSpace(text))
                return;

            using (var sfd = new SaveFileDialog
            {
                Filter = $"{Resources.TxtLog}|*.log",
                OverwritePrompt = true,
            })
            {
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return;

                System.IO.File.WriteAllText(sfd.FileName, text);
            }
        }

        private void FmLogViewer_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                _timer.Start();
                RefreshLog();
            }
            else
                _timer.Stop();
        }

        private void _btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void _btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshLog();
        }

        private void _btnClear_Click(object sender, EventArgs e)
        {
            lock (_lockObj)
                _tbxLog.Clear();
        }

        private void _btnOpenDirectory_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("EXPLORER.EXE", $@"/n,""{System.IO.Path.GetDirectoryName(_logFileName)}""");
        }

        private void _btnTogleAutoRefresh_Click(object sender, EventArgs e)
        {
            _btnTogleAutoRefresh.Checked = !_btnTogleAutoRefresh.Checked;
        }

        private void _btnTogleAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            _timer.Enabled = _btnTogleAutoRefresh.Checked;
        }

        private void _btnSwitchFile_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog
            {
                Filter = $"{Resources.TxtLog}|*.log",
                OverwritePrompt = false,
                Title = Resources.MsgChangeLogFile,
                InitialDirectory = System.IO.Path.GetDirectoryName(_logFileName)

            })
            {
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return;

                ChangedLogFile?.Invoke(sfd.FileName);
            }
        }
        /// <summary>
        /// Set log filename.
        /// </summary>
        /// <param name="log"></param>
        public void SetLogFilename(string log)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => SetLogFilename(log)));
                return;
            }
            _logFileName = log;
            Text = System.IO.Path.GetFileName(log);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControls
{
    internal partial class FmProgressDialog : System.Windows.Forms.Form
    {
        /// <summary>
        /// 
        /// </summary>
        public FmProgressDialog()
        {
            InitializeComponent();

            _processStateBindingSource.DataSource = _progressStates;
            Percentage = 0;
            DisplayNextProcess(Properties.Resources.MsgDefaultProcessMessage, 0);
        }
        /// <summary>
        /// Progress value
        /// </summary>
        public double Percentage
        {
            get => _percentage;

            private set
            {
                if (value >= 100)
                    _percentage = 99.9;
                else if (value < 0)
                    _percentage = 0;
                else
                    _percentage = value;

                _barProgress.Value = (int)_percentage;
            }
        }
        private double _percentage;
        /// <summary>
        /// 取り消しが要求されたかどうか
        /// </summary>
        public bool IsCancellationRequested { get; internal set; } = false;

        private readonly List<ProcessState> _progressStates = new List<ProcessState>();
        /// <summary>
        /// Update progressbar
        /// </summary>
        /// <param name="nextProgressText"></param>
        /// <param name="incrementalPercentage"></param>
        public void DisplayNextProcess(string nextProgressText, double incrementalPercentage)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => DisplayNextProcess(nextProgressText, incrementalPercentage)));
                return;
            }

            _lblProgress.Text = nextProgressText;
            Percentage += incrementalPercentage;
            _lblPercentage.Text = $"{Percentage:f1}%";
        }
        /// <summary>
        /// Update completed process
        /// </summary>
        /// <param name="processNameText"></param>
        /// <param name="processResult"></param>
        public void DisplayProcessState(string processNameText, string processResult)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => DisplayProcessState(processNameText, processResult)));
                return;
            }
            int focusIndex = _progressStates.FindIndex(x => x.Process == processNameText);
            if (focusIndex >= 0)
            {
                _progressStates[focusIndex].State = processResult;
            }
            else
            {
                _progressStates.Add(new ProcessState { Process = processNameText, State = processResult });
                focusIndex = _progressStates.Count() - 1;
            }

            _processStateBindingSource.ResetBindings(false);

            _dataGridAction.ClearSelection();
            _dataGridAction.Rows[focusIndex].Selected = true;
            _dataGridAction.FirstDisplayedScrollingRowIndex = focusIndex;
        }
        private void _btnCancel_Click(object sender, EventArgs e)
        {
            IsCancellationRequested = true;
        }

        private void ProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            IsCancellationRequested = true;
            e.Cancel = true;
            return;
        }
    }
    /// <summary>
    /// A process result
    /// </summary>
    public enum ProcessResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 未
        /// </summary>
        NotYet,
        /// <summary>
        /// 途中
        /// </summary>
        Processing,
        /// <summary>
        /// 失敗
        /// </summary>
        Failure,
        /// <summary>
        /// 停止
        /// </summary>
        Canceled
    }
    internal class ProcessState
    {
        public string Process { get; set; }
        public string State { get; set; }
    }
}
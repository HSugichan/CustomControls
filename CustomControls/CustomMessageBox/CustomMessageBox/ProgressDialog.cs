using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControls
{
    /// <summary>
    /// Wrap FmProgressDialog class not to access Form property.
    /// </summary>
    public class ProgressDialog : IDisposable 
    {
        private readonly FmProgressDialog _fmProgressDialog;
        /// <summary>
        /// 取り消しが要求されたかどうか
        /// </summary>
        public bool IsCancellationRequested
        {
            get => _fmProgressDialog.IsCancellationRequested;
            set => _fmProgressDialog.IsCancellationRequested = value;
        }        /// <summary>
                 ///  Constructor
                 /// </summary>
                 /// <param name="owner"></param>
                 /// <param name="headerText">Dialog header</param>
        public ProgressDialog(FormCustom owner, string headerText=null)
        {
            _fmProgressDialog = new FmProgressDialog()
            {
                StartPosition = FormStartPosition.Manual,
                Location = owner.Location,
                Owner = owner,
                Text = string.IsNullOrWhiteSpace(headerText) ? owner.Text : headerText
            };
        }
        /// <summary>
        /// Dispose form resources.
        /// </summary>
        public void Dispose()
        {

            if (_fmProgressDialog.IsDisposed)
                return;

            _fmProgressDialog.Owner.Enabled = true;
            _fmProgressDialog.Dispose();
        }
        /// <summary>
        /// Show
        /// </summary>
        public void Show()
        {            
            _fmProgressDialog.Show();
            _fmProgressDialog.Owner.Enabled = false;
        }
        /// <summary>
        /// close
        /// </summary>
        public void Close()
        {
            _fmProgressDialog.Owner.Enabled = true;
            _fmProgressDialog.Close();
        }

        /// <summary>
        /// Update progressbar
        /// </summary>
        /// <param name="nextProgressText"></param>
        /// <param name="incrementalPercentage"></param>
        public void DisplayNextProcess(string nextProgressText, double incrementalPercentage)
            => _fmProgressDialog.DisplayNextProcess(nextProgressText, incrementalPercentage);
        /// <summary>
        /// Update completed process
        /// </summary>
        /// <param name="processNameText"></param>
        /// <param name="processResult"></param>
        public void DisplayProcessState(string processNameText, ProcessResult processResult)
            => _fmProgressDialog.DisplayProcessState(processNameText, processResult.ToString());
        /// <summary>
        /// Update completed process
        /// </summary>
        /// <param name="processNameText"></param>
        /// <param name="processResult"></param>
        public void DisplayProcessState(string processNameText, string processResult)
            => _fmProgressDialog.DisplayProcessState(processNameText, processResult);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace CustomControls
{
    /// <summary>
    /// Show dialog for PropertyGrid folder-path input.
    /// </summary>
    public class MyFolderNameEditor : System.Drawing.Design.UITypeEditor
    {
        private CustomControls.FolderBrowserDialog _folderDialog;
        /// <summary>
        /// Initial path.
        /// </summary>
        protected virtual string DefaultFolderName => Application.StartupPath;
        /// <summary>
        /// Title.
        /// </summary>
        protected virtual string Title => "Please identify file name.";
        /// <summary>
        /// constructor
        /// </summary>
        public MyFolderNameEditor() : base() { }
        /// <summary>
        /// Select path.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (_folderDialog == null)
            {
                _folderDialog = new CustomControls.FolderBrowserDialog
                {
                    InitialDirectory = DefaultFolderName,
                    Title = Title,
                };
            }

            if (value is string)
            {
                _folderDialog.InitialDirectory = value.ToString();
            }
            if (_folderDialog.ShowDialog() != DialogResult.OK)
            {
                return value;
            }

            return _folderDialog.SelectedDirectory;
        }
        /// <summary>
        /// Define browse buttun. [...]
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
    /// <summary>
    /// Show dialog for PropertyGrid file-path input.
    /// </summary>
    public class MyFileNameEditor : System.Drawing.Design.UITypeEditor
    {
        private SaveFileDialog _fileDialog;
        /// <summary>
        /// Initial file path
        /// </summary>
        protected virtual string DefaultFileName => $"{DateTime.Now:yyMMdd_HHmmss}_default.csv";
        /// <summary>
        /// constructor
        /// </summary>
        public MyFileNameEditor() : base() { }
        /// <summary>
        /// Select path.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context
          , IServiceProvider provider, object value)
        {
            if (_fileDialog == null)
            {
                _fileDialog = new SaveFileDialog
                {
                    CheckPathExists = false,
                    CheckFileExists = false,
                    FileName = DefaultFileName,
                };
                InitializeDialog(_fileDialog);
            }

            if (value is string)
            {
                _fileDialog.FileName = value.ToString();
            }
            if (_fileDialog.ShowDialog() != DialogResult.OK)
            {
                return value;
            }

            return _fileDialog.FileName;
        }
        /// <summary>
        /// Define browse buttun. [...]
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(
          ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
        /// <summary>
        /// Dialog setup
        /// </summary>
        /// <param name="browserDialog"></param>
        protected virtual void InitializeDialog(SaveFileDialog browserDialog)
        {
            browserDialog.Title = "Please identify file name.";
        }
    }
}

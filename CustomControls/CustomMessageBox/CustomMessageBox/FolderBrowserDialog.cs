using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CustomControls
{
    /// <summary>
    /// https://qiita.com/otagaisama-1/items/b0804b9d6d37d82950f7
    /// 
    /// Folder browser dialog inherited IFileOpenDialog
    /// </summary>
    public class FolderBrowserDialog: IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        public FolderBrowserDialog(string directory)
        {
            InitialDirectory = directory;
        }
        /// <summary>
        /// 
        /// </summary>
        public FolderBrowserDialog() { }
        /// <summary>
        /// Root directory
        /// </summary>
        public string InitialDirectory { get; set; }
        /// <summary>
        /// Selected directory
        /// </summary>
        public string SelectedDirectory { get; private set; }
        /// <summary>
        /// Dialog title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Windows.Forms.DialogResult ShowDialog()
        {
            return ShowDialog(IntPtr.Zero);
        }
        /// <summary>
        /// Show dialog based owner's position.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)
        {
            return ShowDialog(owner.Handle);
        }

        private System.Windows.Forms.DialogResult ShowDialog(IntPtr owner)
        {
            var dlg = new FileOpenDialogInternal() as IFileOpenDialog;
            try
            {
                dlg.SetOptions(FOS.FOS_PICKFOLDERS | FOS.FOS_FORCEFILESYSTEM);

                IShellItem item;
                if (!string.IsNullOrWhiteSpace(this.InitialDirectory) &&
                    System.IO.Directory.Exists(InitialDirectory))//存在するディレクトリの時だけInitialDirectoryとしてセット
                {
                    uint atts = 0;
                    if (NativeMethods.SHILCreateFromPath(this.InitialDirectory, out IntPtr idl, ref atts) == 0)
                    {
                        if (NativeMethods.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, idl, out item) == 0)
                        {
                            dlg.SetFolder(item);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(this.Title))
                    dlg.SetTitle(this.Title);

                var hr = dlg.Show(owner);
                if (hr.Equals(NativeMethods.ERROR_CANCELLED))
                    return System.Windows.Forms.DialogResult.Cancel;
                if (!hr.Equals(0))
                    return System.Windows.Forms.DialogResult.Abort;

                dlg.GetResult(out item);
                item.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out string outputPath);

                this.SelectedDirectory = outputPath;

                return System.Windows.Forms.DialogResult.OK;
            }
            finally
            {
                Marshal.FinalReleaseComObject(dlg);
            }
        }

        /// <summary>
        /// ShowDialog()内でリリースしているので、実質は不要。
        /// 従来のFolderBrouserDoalogと同様にusing(){}で使用できるようにIDisposableを継承
        /// </summary>
        public void Dispose()
        {
            InitialDirectory = null;
            SelectedDirectory = null; 
            Title = null;
        }

        [ComImport]
        [Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
        private class FileOpenDialogInternal
        {
        }

        // not fully defined と記載された宣言は、支障ない範囲で端折ってあります。
        [ComImport]
        [Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IFileOpenDialog
        {
            [PreserveSig]
            UInt32 Show([In] IntPtr hwndParent);
            void SetFileTypes();     // not fully defined
            void SetFileTypeIndex();     // not fully defined
            void GetFileTypeIndex();     // not fully defined
            void Advise(); // not fully defined
            void Unadvise();
            void SetOptions([In] FOS fos);
            void GetOptions(); // not fully defined
            void SetDefaultFolder(); // not fully defined
            void SetFolder(IShellItem psi);
            void GetFolder(); // not fully defined
            void GetCurrentSelection(); // not fully defined
            void SetFileName();  // not fully defined
            void GetFileName();  // not fully defined
            void SetTitle([In, MarshalAs(UnmanagedType.LPWStr)] string pszTitle);
            void SetOkButtonLabel(); // not fully defined
            void SetFileNameLabel(); // not fully defined
            void GetResult(out IShellItem ppsi);
            void AddPlace(); // not fully defined
            void SetDefaultExtension(); // not fully defined
            void Close(); // not fully defined
            void SetClientGuid();  // not fully defined
            void ClearClientData();
            void SetFilter(); // not fully defined
            void GetResults(); // not fully defined
            void GetSelectedItems(); // not fully defined
        }

        [ComImport]
        [Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellItem
        {
            void BindToHandler(); // not fully defined
            void GetParent(); // not fully defined
            void GetDisplayName([In] SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
            void GetAttributes();  // not fully defined
            void Compare();  // not fully defined
        }

        private enum SIGDN : uint // not fully defined
        {
            SIGDN_FILESYSPATH = 0x80058000,
        }

        [Flags]
        private enum FOS // not fully defined
        {
            FOS_FORCEFILESYSTEM = 0x40,
            FOS_PICKFOLDERS = 0x20,
        }

        private class NativeMethods
        {
            [DllImport("shell32.dll")]
            public static extern int SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string pszPath, out IntPtr ppIdl, ref uint rgflnOut);

            [DllImport("shell32.dll")]
            public static extern int SHCreateShellItem(IntPtr pidlParent, IntPtr psfParent, IntPtr pidl, out IShellItem ppsi);

            public const uint ERROR_CANCELLED = 0x800704C7;
        }
    }
}

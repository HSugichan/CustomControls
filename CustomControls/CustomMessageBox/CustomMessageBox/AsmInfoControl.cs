using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using CustomControls.Properties;

namespace CustomControls
{
    internal partial class AsmInfoControl : UserControl
    {
        /// <summary>
        /// アセンブリ情報表示用UIブロック
        /// タブに各アセンブリを追加するのでカプセル化した
        /// </summary>
        /// <param name="assembly"></param>
        public AsmInfoControl(Assembly assembly)
        {
            InitializeComponent();

            _lblName.Text = assembly.GetName().Name;

            _lblVersion.Text = $"{Resources.TxtVersion}: {assembly.GetName().Version}";
            _lblBuildDate.Text = $"{Resources.TxtBuildDate}: {File.GetLastWriteTimeUtc(assembly.Location)} (UTC)";


            _pbxIcon.Image = GetIconFromPath(assembly.Location);
        }
        private Bitmap GetIconFromPath(string asmPath)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hSuccess = SHGetFileInfo(asmPath, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);

            var iconImage = ((hSuccess != IntPtr.Zero) ? Icon.FromHandle(shinfo.hIcon) : SystemIcons.WinLogo).ToBitmap();

            var resizeWidth = _pbxIcon.Width;
            var resizeHeight = _pbxIcon.Height;
            Bitmap resizeBmp = new Bitmap(resizeWidth, resizeHeight);
            Graphics g = Graphics.FromImage(resizeBmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(iconImage, 0, 0, resizeWidth, resizeHeight);
            g.Dispose();
            return resizeBmp;
        }

        /// <summary>
        /// SHGetFileInfo関数
        /// </summary>        
        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        // SHGetFileInfo関数で使用するフラグ
        private const uint SHGFI_ICON = 0x100; // アイコン・リソースの取得
        private const uint SHGFI_LARGEICON = 0x0; // 大きいアイコン
        private const uint SHGFI_SMALLICON = 0x1; // 小さいアイコン

        /// <summary>
        /// SHGetFileInfo関数で使用する構造体
        /// </summary>                                                  
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

namespace CustomControls
{
    /// <summary>
    /// An assembly inherited EXE/ DLL. 
    /// </summary>
    /// <typeparam name="TAsm">Exe/Dll</typeparam>
    internal abstract class AsmInfo<TAsm> where TAsm :  new()
    {
        public static TAsm GetInstance() => _asm;
        protected static readonly TAsm _asm = new TAsm();
        public abstract Assembly AssmInfo { get; }

        public string Path => AssmInfo.Location;
        public string Name => AssmInfo.GetName().Name;
        public Version Version => AssmInfo.GetName().Version;
        public string BuildDate => System.IO.File.GetLastWriteTime(Path).ToLongDateString();
    }
    /// <summary>
    /// Entry EXE information class.
    /// </summary>
    internal class ExeInfo : AsmInfo<ExeInfo>
    {
        public ExeInfo() { }
        public override Assembly AssmInfo => Assembly.GetEntryAssembly();
    }
    /// <summary>
    /// CustomControls.dll information class.   
    /// </summary>
    internal class DllInfo : AsmInfo<DllInfo>
    {
        public DllInfo() { }
        public override Assembly AssmInfo => Assembly.GetExecutingAssembly();
    }
}
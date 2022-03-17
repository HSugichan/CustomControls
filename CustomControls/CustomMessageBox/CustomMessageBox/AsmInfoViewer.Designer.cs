namespace CustomControls
{
    partial class AsmInfoViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsmInfoViewer));
            this._btnClose = new System.Windows.Forms.Button();
            this._tabAssembly = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._btnContextMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btnClose
            // 
            resources.ApplyResources(this._btnClose, "_btnClose");
            this._btnClose.Name = "_btnClose";
            this._btnClose.UseVisualStyleBackColor = true;
            this._btnClose.Click += new System.EventHandler(this._btnClose_Click);
            // 
            // _tabAssembly
            // 
            resources.ApplyResources(this._tabAssembly, "_tabAssembly");
            this._tabAssembly.Name = "_tabAssembly";
            this._tabAssembly.SelectedIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnContextMenuCopy});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // _btnContextMenuCopy
            // 
            this._btnContextMenuCopy.Name = "_btnContextMenuCopy";
            resources.ApplyResources(this._btnContextMenuCopy, "_btnContextMenuCopy");
            this._btnContextMenuCopy.Click += new System.EventHandler(this._btnContextMenuCopy_Click);
            // 
            // AsmInfoViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this._tabAssembly);
            this.Controls.Add(this._btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsmInfoViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AsmViewer_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnClose;
        private System.Windows.Forms.TabControl _tabAssembly;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _btnContextMenuCopy;
    }
}
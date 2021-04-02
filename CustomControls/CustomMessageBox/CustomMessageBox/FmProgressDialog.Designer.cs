namespace CustomControls
{
    partial class FmProgressDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmProgressDialog));
            this._btnCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this._barProgress = new System.Windows.Forms.ToolStripProgressBar();
            this._lblPercentage = new System.Windows.Forms.ToolStripStatusLabel();
            this._dataGridAction = new System.Windows.Forms.DataGridView();
            this.processDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._processStateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._processStateBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // _btnCancel
            // 
            resources.ApplyResources(this._btnCancel, "_btnCancel");
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lblProgress,
            this._barProgress,
            this._lblPercentage});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // _lblProgress
            // 
            resources.ApplyResources(this._lblProgress, "_lblProgress");
            this._lblProgress.Name = "_lblProgress";
            this._lblProgress.Spring = true;
            // 
            // _barProgress
            // 
            resources.ApplyResources(this._barProgress, "_barProgress");
            this._barProgress.Name = "_barProgress";
            // 
            // _lblPercentage
            // 
            resources.ApplyResources(this._lblPercentage, "_lblPercentage");
            this._lblPercentage.Name = "_lblPercentage";
            // 
            // _dataGridAction
            // 
            resources.ApplyResources(this._dataGridAction, "_dataGridAction");
            this._dataGridAction.AutoGenerateColumns = false;
            this._dataGridAction.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this._dataGridAction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridAction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.processDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn});
            this._dataGridAction.DataSource = this._processStateBindingSource;
            this._dataGridAction.Name = "_dataGridAction";
            this._dataGridAction.RowTemplate.Height = 21;
            // 
            // processDataGridViewTextBoxColumn
            // 
            this.processDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.processDataGridViewTextBoxColumn.DataPropertyName = "Process";
            resources.ApplyResources(this.processDataGridViewTextBoxColumn, "processDataGridViewTextBoxColumn");
            this.processDataGridViewTextBoxColumn.Name = "processDataGridViewTextBoxColumn";
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            resources.ApplyResources(this.stateDataGridViewTextBoxColumn, "stateDataGridViewTextBoxColumn");
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            // 
            // _processStateBindingSource
            // 
            this._processStateBindingSource.DataSource = typeof(CustomControls.ProcessState);
            // 
            // FmProgressDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dataGridAction);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this._btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FmProgressDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressDialog_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._processStateBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _lblProgress;
        private System.Windows.Forms.ToolStripProgressBar _barProgress;
        private System.Windows.Forms.DataGridView _dataGridAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource _processStateBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn processDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripStatusLabel _lblPercentage;
    }
}
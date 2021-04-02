namespace CustomControls
{
    partial class AsmInfoControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this._pbxIcon = new System.Windows.Forms.PictureBox();
            this._lblName = new System.Windows.Forms.Label();
            this._lblBuildDate = new System.Windows.Forms.Label();
            this._lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._pbxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // _pbxIcon
            // 
            this._pbxIcon.Location = new System.Drawing.Point(5, 8);
            this._pbxIcon.Name = "_pbxIcon";
            this._pbxIcon.Size = new System.Drawing.Size(70, 70);
            this._pbxIcon.TabIndex = 0;
            this._pbxIcon.TabStop = false;
            // 
            // _lblName
            // 
            this._lblName.AutoSize = true;
            this._lblName.Font = new System.Drawing.Font("Cambria", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblName.ForeColor = System.Drawing.Color.Blue;
            this._lblName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._lblName.Location = new System.Drawing.Point(81, 26);
            this._lblName.Name = "_lblName";
            this._lblName.Size = new System.Drawing.Size(89, 33);
            this._lblName.TabIndex = 2;
            this._lblName.Text = "NAME";
            // 
            // _lblBuildDate
            // 
            this._lblBuildDate.AutoSize = true;
            this._lblBuildDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._lblBuildDate.Location = new System.Drawing.Point(2, 148);
            this._lblBuildDate.Name = "_lblBuildDate";
            this._lblBuildDate.Size = new System.Drawing.Size(106, 18);
            this._lblBuildDate.TabIndex = 4;
            this._lblBuildDate.Text = "BUILD DATE";
            // 
            // _lblVersion
            // 
            this._lblVersion.AutoSize = true;
            this._lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._lblVersion.Location = new System.Drawing.Point(2, 108);
            this._lblVersion.Name = "_lblVersion";
            this._lblVersion.Size = new System.Drawing.Size(80, 18);
            this._lblVersion.TabIndex = 5;
            this._lblVersion.Text = "VERSION";
            // 
            // AsmInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this._lblBuildDate);
            this.Controls.Add(this._lblVersion);
            this.Controls.Add(this._lblName);
            this.Controls.Add(this._pbxIcon);
            this.Name = "AsmInfoControl";
            this.Size = new System.Drawing.Size(438, 195);
            ((System.ComponentModel.ISupportInitialize)(this._pbxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox _pbxIcon;
        private System.Windows.Forms.Label _lblName;
        private System.Windows.Forms.Label _lblBuildDate;
        private System.Windows.Forms.Label _lblVersion;
    }
}

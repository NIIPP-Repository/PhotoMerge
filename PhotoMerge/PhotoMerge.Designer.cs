namespace PhotoMerge
{
    partial class PhotoMerge
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPeek = new System.Windows.Forms.Button();
            this.btnCalibr = new System.Windows.Forms.Button();
            this.pictureBoxCalibr = new System.Windows.Forms.PictureBox();
            this.btnGlue = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.trkBrStep = new System.Windows.Forms.TrackBar();
            this.trkBrAlign = new System.Windows.Forms.TrackBar();
            this.lblStep = new System.Windows.Forms.Label();
            this.lblAlign = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCalibr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrAlign)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPeek
            // 
            this.btnPeek.Location = new System.Drawing.Point(6, 6);
            this.btnPeek.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPeek.Name = "btnPeek";
            this.btnPeek.Size = new System.Drawing.Size(76, 22);
            this.btnPeek.TabIndex = 0;
            this.btnPeek.Text = "Обзор";
            this.btnPeek.UseVisualStyleBackColor = true;
            this.btnPeek.Click += new System.EventHandler(this.btnPeek_Click);
            // 
            // btnCalibr
            // 
            this.btnCalibr.Location = new System.Drawing.Point(85, 6);
            this.btnCalibr.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCalibr.Name = "btnCalibr";
            this.btnCalibr.Size = new System.Drawing.Size(76, 22);
            this.btnCalibr.TabIndex = 1;
            this.btnCalibr.Text = "Калибровка";
            this.btnCalibr.UseVisualStyleBackColor = true;
            this.btnCalibr.Click += new System.EventHandler(this.btnCalibr_Click);
            // 
            // pictureBoxCalibr
            // 
            this.pictureBoxCalibr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCalibr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCalibr.Location = new System.Drawing.Point(6, 56);
            this.pictureBoxCalibr.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxCalibr.Name = "pictureBoxCalibr";
            this.pictureBoxCalibr.Size = new System.Drawing.Size(926, 664);
            this.pictureBoxCalibr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCalibr.TabIndex = 2;
            this.pictureBoxCalibr.TabStop = false;
            // 
            // btnGlue
            // 
            this.btnGlue.Location = new System.Drawing.Point(164, 6);
            this.btnGlue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGlue.Name = "btnGlue";
            this.btnGlue.Size = new System.Drawing.Size(76, 22);
            this.btnGlue.TabIndex = 3;
            this.btnGlue.Text = "Склеить";
            this.btnGlue.UseVisualStyleBackColor = true;
            this.btnGlue.Click += new System.EventHandler(this.btnGlue_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(6, 38);
            this.lblPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(111, 13);
            this.lblPath.TabIndex = 4;
            this.lblPath.Text = "Источник не выбран";
            // 
            // trkBrStep
            // 
            this.trkBrStep.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.trkBrStep.Location = new System.Drawing.Point(975, 38);
            this.trkBrStep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkBrStep.Name = "trkBrStep";
            this.trkBrStep.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trkBrStep.Size = new System.Drawing.Size(45, 461);
            this.trkBrStep.TabIndex = 5;
            this.trkBrStep.Scroll += new System.EventHandler(this.trkBrStep_Scroll);
            // 
            // trkBrAlign
            // 
            this.trkBrAlign.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.trkBrAlign.Location = new System.Drawing.Point(1062, 38);
            this.trkBrAlign.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkBrAlign.Name = "trkBrAlign";
            this.trkBrAlign.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trkBrAlign.Size = new System.Drawing.Size(45, 461);
            this.trkBrAlign.TabIndex = 7;
            this.trkBrAlign.Scroll += new System.EventHandler(this.trkBrAlign_Scroll);
            // 
            // lblStep
            // 
            this.lblStep.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(974, 516);
            this.lblStep.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(46, 13);
            this.lblStep.TabIndex = 9;
            this.lblStep.Text = "lblStepX";
            // 
            // lblAlign
            // 
            this.lblAlign.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAlign.AutoSize = true;
            this.lblAlign.Location = new System.Drawing.Point(974, 534);
            this.lblAlign.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAlign.Name = "lblAlign";
            this.lblAlign.Size = new System.Drawing.Size(47, 13);
            this.lblAlign.TabIndex = 11;
            this.lblAlign.Text = "lblAlignX";
            // 
            // PhotoMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1144, 731);
            this.Controls.Add(this.lblAlign);
            this.Controls.Add(this.lblStep);
            this.Controls.Add(this.trkBrAlign);
            this.Controls.Add(this.trkBrStep);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btnGlue);
            this.Controls.Add(this.pictureBoxCalibr);
            this.Controls.Add(this.btnCalibr);
            this.Controls.Add(this.btnPeek);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PhotoMerge";
            this.Text = "PhotoMerge";
            this.Load += new System.EventHandler(this.PhotoMerge_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCalibr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrAlign)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPeek;
        private System.Windows.Forms.Button btnCalibr;
        private System.Windows.Forms.PictureBox pictureBoxCalibr;
        private System.Windows.Forms.Button btnGlue;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TrackBar trkBrStep;
        private System.Windows.Forms.TrackBar trkBrAlign;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.Label lblAlign;
    }
}


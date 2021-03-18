namespace PrizepicsV1
{
    partial class FormReb
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
            this.dataReb = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataReb)).BeginInit();
            this.SuspendLayout();
            // 
            // dataReb
            // 
            this.dataReb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataReb.Location = new System.Drawing.Point(13, 13);
            this.dataReb.Name = "dataReb";
            this.dataReb.RowHeadersWidth = 51;
            this.dataReb.RowTemplate.Height = 24;
            this.dataReb.Size = new System.Drawing.Size(1157, 628);
            this.dataReb.TabIndex = 0;
            // 
            // FormReb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 653);
            this.Controls.Add(this.dataReb);
            this.Name = "FormReb";
            this.Text = "Josh Sucks";
            this.Load += new System.EventHandler(this.FormReb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataReb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataReb;
    }
}
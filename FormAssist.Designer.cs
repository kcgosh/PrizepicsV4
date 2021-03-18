namespace PrizepicsV1
{
    partial class FormAssist
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
            this.dataAssist = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataAssist)).BeginInit();
            this.SuspendLayout();
            // 
            // dataAssist
            // 
            this.dataAssist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataAssist.Location = new System.Drawing.Point(12, 12);
            this.dataAssist.Name = "dataAssist";
            this.dataAssist.RowHeadersWidth = 51;
            this.dataAssist.RowTemplate.Height = 24;
            this.dataAssist.Size = new System.Drawing.Size(958, 629);
            this.dataAssist.TabIndex = 0;
            // 
            // FormAssist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.dataAssist);
            this.Name = "FormAssist";
            this.Text = "Coop Sucks";
            this.Load += new System.EventHandler(this.FormAssist_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataAssist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataAssist;
    }
}

namespace GOLAssignment
{
    partial class Edit_Tick_Rate
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.AcceptMilisecond = new System.Windows.Forms.Button();
            this.CancelMiliseconds = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(68, 29);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(156, 20);
            this.numericUpDown1.TabIndex = 0;
            // 
            // AcceptMilisecond
            // 
            this.AcceptMilisecond.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AcceptMilisecond.Location = new System.Drawing.Point(68, 55);
            this.AcceptMilisecond.Name = "AcceptMilisecond";
            this.AcceptMilisecond.Size = new System.Drawing.Size(75, 23);
            this.AcceptMilisecond.TabIndex = 1;
            this.AcceptMilisecond.Text = "Accept";
            this.AcceptMilisecond.UseVisualStyleBackColor = true;
            this.AcceptMilisecond.Click += new System.EventHandler(this.AcceptMilisecond_Click);
            // 
            // CancelMiliseconds
            // 
            this.CancelMiliseconds.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelMiliseconds.Location = new System.Drawing.Point(149, 55);
            this.CancelMiliseconds.Name = "CancelMiliseconds";
            this.CancelMiliseconds.Size = new System.Drawing.Size(75, 23);
            this.CancelMiliseconds.TabIndex = 2;
            this.CancelMiliseconds.Text = "Cancel";
            this.CancelMiliseconds.UseVisualStyleBackColor = true;
            this.CancelMiliseconds.Click += new System.EventHandler(this.CancelMiliseconds_Click);
            // 
            // Edit_Tick_Rate
            // 
            this.AcceptButton = this.AcceptMilisecond;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelMiliseconds;
            this.ClientSize = new System.Drawing.Size(298, 119);
            this.Controls.Add(this.CancelMiliseconds);
            this.Controls.Add(this.AcceptMilisecond);
            this.Controls.Add(this.numericUpDown1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Edit_Tick_Rate";
            this.Text = "Edit Tick Rate";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button AcceptMilisecond;
        private System.Windows.Forms.Button CancelMiliseconds;
    }
}
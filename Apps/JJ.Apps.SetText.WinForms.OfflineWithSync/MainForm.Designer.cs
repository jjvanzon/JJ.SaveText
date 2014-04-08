namespace JJ.Apps.SetText.WinForms.OfflineWithSync
{
    partial class MainForm
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
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelValidationMessages = new System.Windows.Forms.Label();
            this.buttonSynchronize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(60, 23);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(152, 119);
            this.textBoxText.TabIndex = 0;
            this.textBoxText.TextChanged += new System.EventHandler(this.textBoxText_TextChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(105, 167);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelValidationMessages
            // 
            this.labelValidationMessages.Location = new System.Drawing.Point(73, 217);
            this.labelValidationMessages.Name = "labelValidationMessages";
            this.labelValidationMessages.Size = new System.Drawing.Size(155, 85);
            this.labelValidationMessages.TabIndex = 2;
            this.labelValidationMessages.Text = "label1";
            // 
            // buttonSynchronize
            // 
            this.buttonSynchronize.Location = new System.Drawing.Point(274, 50);
            this.buttonSynchronize.Name = "buttonSynchronize";
            this.buttonSynchronize.Size = new System.Drawing.Size(75, 23);
            this.buttonSynchronize.TabIndex = 3;
            this.buttonSynchronize.Text = "Synchronize";
            this.buttonSynchronize.UseVisualStyleBackColor = true;
            this.buttonSynchronize.Click += new System.EventHandler(this.buttonSynchronize_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 353);
            this.Controls.Add(this.buttonSynchronize);
            this.Controls.Add(this.labelValidationMessages);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxText);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelValidationMessages;
        private System.Windows.Forms.Button buttonSynchronize;
    }
}


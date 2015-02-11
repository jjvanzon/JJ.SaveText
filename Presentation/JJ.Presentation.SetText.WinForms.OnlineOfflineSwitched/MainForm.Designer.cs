namespace JJ.Presentation.SetText.WinForms.OnlineOfflineSwitched
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
            this.buttonSwitchBetweenOnlineAndOffline = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxText
            // 
            this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxText.Location = new System.Drawing.Point(8, 8);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(401, 168);
            this.textBoxText.TabIndex = 0;
            this.textBoxText.TextChanged += new System.EventHandler(this.textBoxText_TextChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(92, 195);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(85, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelValidationMessages
            // 
            this.labelValidationMessages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelValidationMessages.Location = new System.Drawing.Point(8, 258);
            this.labelValidationMessages.Name = "labelValidationMessages";
            this.labelValidationMessages.Size = new System.Drawing.Size(401, 85);
            this.labelValidationMessages.TabIndex = 2;
            this.labelValidationMessages.Text = "label1";
            // 
            // buttonSwitchBetweenOnlineAndOffline
            // 
            this.buttonSwitchBetweenOnlineAndOffline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSwitchBetweenOnlineAndOffline.Location = new System.Drawing.Point(236, 195);
            this.buttonSwitchBetweenOnlineAndOffline.Name = "buttonSwitchBetweenOnlineAndOffline";
            this.buttonSwitchBetweenOnlineAndOffline.Size = new System.Drawing.Size(87, 23);
            this.buttonSwitchBetweenOnlineAndOffline.TabIndex = 3;
            this.buttonSwitchBetweenOnlineAndOffline.Text = "Go Online";
            this.buttonSwitchBetweenOnlineAndOffline.UseVisualStyleBackColor = true;
            this.buttonSwitchBetweenOnlineAndOffline.Click += new System.EventHandler(this.buttonSwitchBetweenOnlineAndOffline_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 351);
            this.Controls.Add(this.buttonSwitchBetweenOnlineAndOffline);
            this.Controls.Add(this.labelValidationMessages);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxText);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "JJ.Presentation.SetText.WinForms.OnlineOfflineSwitched";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelValidationMessages;
        private System.Windows.Forms.Button buttonSwitchBetweenOnlineAndOffline;
    }
}


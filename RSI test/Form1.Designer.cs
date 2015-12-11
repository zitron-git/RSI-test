namespace RSI_test
{
    partial class RSIServerForm
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
            this.DebugTextBox = new System.Windows.Forms.TextBox();
            this.StartUDPButton = new System.Windows.Forms.Button();
            this.UITimer = new System.Windows.Forms.Timer(this.components);
            this.StopCorrButton = new System.Windows.Forms.CheckBox();
            this.HaltButton = new System.Windows.Forms.CheckBox();
            this.CorrectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DebugTextBox
            // 
            this.DebugTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DebugTextBox.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugTextBox.Location = new System.Drawing.Point(12, 12);
            this.DebugTextBox.Multiline = true;
            this.DebugTextBox.Name = "DebugTextBox";
            this.DebugTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DebugTextBox.Size = new System.Drawing.Size(465, 405);
            this.DebugTextBox.TabIndex = 0;
            this.DebugTextBox.WordWrap = false;
            // 
            // StartUDPButton
            // 
            this.StartUDPButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartUDPButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartUDPButton.Location = new System.Drawing.Point(483, 12);
            this.StartUDPButton.Name = "StartUDPButton";
            this.StartUDPButton.Size = new System.Drawing.Size(75, 23);
            this.StartUDPButton.TabIndex = 1;
            this.StartUDPButton.Text = "Start";
            this.StartUDPButton.UseVisualStyleBackColor = true;
            this.StartUDPButton.Click += new System.EventHandler(this.StartUDPButton_Click);
            // 
            // UITimer
            // 
            this.UITimer.Interval = 50;
            this.UITimer.Tick += new System.EventHandler(this.UITimer_Tick);
            // 
            // StopCorrButton
            // 
            this.StopCorrButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StopCorrButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.StopCorrButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.StopCorrButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.StopCorrButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopCorrButton.Location = new System.Drawing.Point(483, 327);
            this.StopCorrButton.Name = "StopCorrButton";
            this.StopCorrButton.Size = new System.Drawing.Size(75, 42);
            this.StopCorrButton.TabIndex = 2;
            this.StopCorrButton.Text = "Stop Correction";
            this.StopCorrButton.UseVisualStyleBackColor = false;
            this.StopCorrButton.Click += new System.EventHandler(this.StopCorrButton_Click);
            // 
            // HaltButton
            // 
            this.HaltButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HaltButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.HaltButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HaltButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.HaltButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HaltButton.Location = new System.Drawing.Point(483, 375);
            this.HaltButton.Name = "HaltButton";
            this.HaltButton.Size = new System.Drawing.Size(75, 42);
            this.HaltButton.TabIndex = 3;
            this.HaltButton.Text = "Halt";
            this.HaltButton.UseVisualStyleBackColor = false;
            this.HaltButton.Click += new System.EventHandler(this.HaltButton_Click);
            // 
            // CorrectButton
            // 
            this.CorrectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CorrectButton.Location = new System.Drawing.Point(483, 82);
            this.CorrectButton.Name = "CorrectButton";
            this.CorrectButton.Size = new System.Drawing.Size(75, 23);
            this.CorrectButton.TabIndex = 4;
            this.CorrectButton.Text = "Correct";
            this.CorrectButton.UseVisualStyleBackColor = true;
            this.CorrectButton.Click += new System.EventHandler(this.CorrectButton_Click);
            // 
            // RSIServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 429);
            this.Controls.Add(this.CorrectButton);
            this.Controls.Add(this.HaltButton);
            this.Controls.Add(this.StopCorrButton);
            this.Controls.Add(this.StartUDPButton);
            this.Controls.Add(this.DebugTextBox);
            this.Name = "RSIServerForm";
            this.Text = "RSI Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DebugTextBox;
        private System.Windows.Forms.Button StartUDPButton;
        private System.Windows.Forms.Timer UITimer;
        private System.Windows.Forms.CheckBox StopCorrButton;
        private System.Windows.Forms.CheckBox HaltButton;
        private System.Windows.Forms.Button CorrectButton;
    }
}


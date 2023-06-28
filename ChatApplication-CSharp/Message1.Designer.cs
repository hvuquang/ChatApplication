namespace ChatApplication_CSharp
{
    partial class Message1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbMessage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.reactMessage1 = new ChatApplication_CSharp.ReactMessage();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbMessage
            // 
            this.lbMessage.BackColor = System.Drawing.SystemColors.Control;
            this.lbMessage.ForeColor = System.Drawing.Color.Black;
            this.lbMessage.Location = new System.Drawing.Point(142, 0);
            this.lbMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.lbMessage.Size = new System.Drawing.Size(570, 76);
            this.lbMessage.TabIndex = 1;
            this.lbMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbMessage.Click += new System.EventHandler(this.lbMessage_Click);
            this.lbMessage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMessage_MouseClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ChatApplication_CSharp.Properties.Resources.avt;
            this.pictureBox1.Location = new System.Drawing.Point(40, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 51);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // reactMessage1
            // 
            this.reactMessage1.BackColor = System.Drawing.Color.White;
            this.reactMessage1.Location = new System.Drawing.Point(575, 78);
            this.reactMessage1.Name = "reactMessage1";
            this.reactMessage1.Size = new System.Drawing.Size(137, 27);
            this.reactMessage1.TabIndex = 3;
            this.reactMessage1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(165, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // Message1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reactMessage1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbMessage);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Message1";
            this.Size = new System.Drawing.Size(712, 108);
            this.Load += new System.EventHandler(this.Message1_Load);
            this.Click += new System.EventHandler(this.Message1_Click);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Message1_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ReactMessage reactMessage1;
        private System.Windows.Forms.Label label1;
    }
}

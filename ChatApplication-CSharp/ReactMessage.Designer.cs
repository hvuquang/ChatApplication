namespace ChatApplication_CSharp
{
    partial class ReactMessage
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
            this.btnLike = new System.Windows.Forms.Button();
            this.btnLaugh = new System.Windows.Forms.Button();
            this.btnHeart = new System.Windows.Forms.Button();
            this.lbLaugh = new System.Windows.Forms.Label();
            this.lbLike = new System.Windows.Forms.Label();
            this.lbHeart = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLike
            // 
            this.btnLike.BackColor = System.Drawing.Color.Transparent;
            this.btnLike.BackgroundImage = global::ChatApplication_CSharp.Properties.Resources.like;
            this.btnLike.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLike.FlatAppearance.BorderSize = 0;
            this.btnLike.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLike.ForeColor = System.Drawing.Color.Transparent;
            this.btnLike.Location = new System.Drawing.Point(48, 3);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(20, 20);
            this.btnLike.TabIndex = 3;
            this.btnLike.UseVisualStyleBackColor = false;
            this.btnLike.Click += new System.EventHandler(this.btnLike_Click);
            // 
            // btnLaugh
            // 
            this.btnLaugh.BackColor = System.Drawing.Color.Transparent;
            this.btnLaugh.BackgroundImage = global::ChatApplication_CSharp.Properties.Resources.laughing;
            this.btnLaugh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLaugh.FlatAppearance.BorderSize = 0;
            this.btnLaugh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLaugh.ForeColor = System.Drawing.Color.Transparent;
            this.btnLaugh.Location = new System.Drawing.Point(95, 3);
            this.btnLaugh.Name = "btnLaugh";
            this.btnLaugh.Size = new System.Drawing.Size(20, 20);
            this.btnLaugh.TabIndex = 2;
            this.btnLaugh.UseVisualStyleBackColor = false;
            this.btnLaugh.Click += new System.EventHandler(this.btnLaugh_Click);
            // 
            // btnHeart
            // 
            this.btnHeart.BackColor = System.Drawing.Color.Transparent;
            this.btnHeart.BackgroundImage = global::ChatApplication_CSharp.Properties.Resources.heart;
            this.btnHeart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHeart.FlatAppearance.BorderSize = 0;
            this.btnHeart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeart.ForeColor = System.Drawing.Color.Transparent;
            this.btnHeart.Location = new System.Drawing.Point(4, 3);
            this.btnHeart.Name = "btnHeart";
            this.btnHeart.Size = new System.Drawing.Size(20, 20);
            this.btnHeart.TabIndex = 1;
            this.btnHeart.UseVisualStyleBackColor = false;
            this.btnHeart.Click += new System.EventHandler(this.btnHeart_Click);
            // 
            // lbLaugh
            // 
            this.lbLaugh.AutoSize = true;
            this.lbLaugh.Location = new System.Drawing.Point(121, 7);
            this.lbLaugh.Name = "lbLaugh";
            this.lbLaugh.Size = new System.Drawing.Size(13, 13);
            this.lbLaugh.TabIndex = 4;
            this.lbLaugh.Text = "0";
            // 
            // lbLike
            // 
            this.lbLike.AutoSize = true;
            this.lbLike.Location = new System.Drawing.Point(74, 7);
            this.lbLike.Name = "lbLike";
            this.lbLike.Size = new System.Drawing.Size(13, 13);
            this.lbLike.TabIndex = 5;
            this.lbLike.Text = "0";
            // 
            // lbHeart
            // 
            this.lbHeart.AutoSize = true;
            this.lbHeart.Location = new System.Drawing.Point(29, 7);
            this.lbHeart.Name = "lbHeart";
            this.lbHeart.Size = new System.Drawing.Size(13, 13);
            this.lbHeart.TabIndex = 6;
            this.lbHeart.Text = "0";
            // 
            // ReactMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbHeart);
            this.Controls.Add(this.lbLike);
            this.Controls.Add(this.lbLaugh);
            this.Controls.Add(this.btnLike);
            this.Controls.Add(this.btnLaugh);
            this.Controls.Add(this.btnHeart);
            this.Name = "ReactMessage";
            this.Size = new System.Drawing.Size(136, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btnHeart;
        public System.Windows.Forms.Button btnLaugh;
        public System.Windows.Forms.Button btnLike;
        public System.Windows.Forms.Label lbHeart;
        public System.Windows.Forms.Label lbLaugh;
        public System.Windows.Forms.Label lbLike;
    }
}

namespace Bootstrap
{
    partial class EditorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorControl));
            this.ImageBoard = new System.Windows.Forms.PictureBox();
            this.BTNpreview = new System.Windows.Forms.Button();
            this.TBXlevel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BTNback = new System.Windows.Forms.Button();
            this.BTNsave = new System.Windows.Forms.Button();
            this.BTNsavenew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageBoard
            // 
            this.ImageBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageBoard.Location = new System.Drawing.Point(12, 374);
            this.ImageBoard.Name = "ImageBoard";
            this.ImageBoard.Size = new System.Drawing.Size(446, 175);
            this.ImageBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageBoard.TabIndex = 11;
            this.ImageBoard.TabStop = false;
            // 
            // BTNpreview
            // 
            this.BTNpreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTNpreview.Location = new System.Drawing.Point(12, 345);
            this.BTNpreview.Name = "BTNpreview";
            this.BTNpreview.Size = new System.Drawing.Size(446, 23);
            this.BTNpreview.TabIndex = 8;
            this.BTNpreview.Text = "Update Preview";
            this.BTNpreview.UseVisualStyleBackColor = true;
            this.BTNpreview.Click += new System.EventHandler(this.BTNpreview_Click);
            // 
            // TBXlevel
            // 
            this.TBXlevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBXlevel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBXlevel.Location = new System.Drawing.Point(12, 119);
            this.TBXlevel.Multiline = true;
            this.TBXlevel.Name = "TBXlevel";
            this.TBXlevel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TBXlevel.Size = new System.Drawing.Size(446, 220);
            this.TBXlevel.TabIndex = 7;
            this.TBXlevel.TabStop = false;
            this.TBXlevel.Text = "32,\r\n00b\r\n11r,\r\n1111,\r\n0111,\r\n0010\r\n2121,\r\n00rp1112\r\n10rp1121\r\n20bk0012\r\n01rp2101" +
    "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 78);
            this.label1.TabIndex = 12;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // BTNback
            // 
            this.BTNback.Location = new System.Drawing.Point(12, 12);
            this.BTNback.Name = "BTNback";
            this.BTNback.Size = new System.Drawing.Size(75, 23);
            this.BTNback.TabIndex = 13;
            this.BTNback.Text = "Back";
            this.BTNback.UseVisualStyleBackColor = true;
            this.BTNback.Click += new System.EventHandler(this.BTNback_Click);
            // 
            // BTNsave
            // 
            this.BTNsave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTNsave.Location = new System.Drawing.Point(302, 12);
            this.BTNsave.Name = "BTNsave";
            this.BTNsave.Size = new System.Drawing.Size(75, 23);
            this.BTNsave.TabIndex = 14;
            this.BTNsave.Text = "Save";
            this.BTNsave.UseVisualStyleBackColor = true;
            this.BTNsave.Click += new System.EventHandler(this.BTNsave_Click);
            // 
            // BTNsavenew
            // 
            this.BTNsavenew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTNsavenew.Location = new System.Drawing.Point(383, 12);
            this.BTNsavenew.Name = "BTNsavenew";
            this.BTNsavenew.Size = new System.Drawing.Size(75, 23);
            this.BTNsavenew.TabIndex = 15;
            this.BTNsavenew.Text = "Save New";
            this.BTNsavenew.UseVisualStyleBackColor = true;
            this.BTNsavenew.Click += new System.EventHandler(this.BTNsavenew_Click);
            // 
            // EditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BTNsavenew);
            this.Controls.Add(this.BTNsave);
            this.Controls.Add(this.BTNback);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ImageBoard);
            this.Controls.Add(this.BTNpreview);
            this.Controls.Add(this.TBXlevel);
            this.Name = "EditorControl";
            this.Size = new System.Drawing.Size(470, 552);
            ((System.ComponentModel.ISupportInitialize)(this.ImageBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageBoard;
        private System.Windows.Forms.Button BTNpreview;
        private System.Windows.Forms.TextBox TBXlevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTNback;
        private System.Windows.Forms.Button BTNsave;
        private System.Windows.Forms.Button BTNsavenew;
    }
}

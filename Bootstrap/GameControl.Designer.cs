namespace Bootstrap
{
    partial class GameControl
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
            this.BTNreset = new System.Windows.Forms.Button();
            this.BTNrun = new System.Windows.Forms.Button();
            this.ImageBoard = new System.Windows.Forms.PictureBox();
            this.ImageInstructions = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.BTNback = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.LBLstage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageInstructions)).BeginInit();
            this.SuspendLayout();
            // 
            // BTNreset
            // 
            this.BTNreset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTNreset.Location = new System.Drawing.Point(12, 517);
            this.BTNreset.Name = "BTNreset";
            this.BTNreset.Size = new System.Drawing.Size(446, 23);
            this.BTNreset.TabIndex = 1;
            this.BTNreset.Text = "Reset";
            this.BTNreset.UseVisualStyleBackColor = true;
            this.BTNreset.Click += new System.EventHandler(this.BTNreset_Click);
            // 
            // BTNrun
            // 
            this.BTNrun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTNrun.Location = new System.Drawing.Point(12, 488);
            this.BTNrun.Name = "BTNrun";
            this.BTNrun.Size = new System.Drawing.Size(446, 23);
            this.BTNrun.TabIndex = 0;
            this.BTNrun.Text = "Run";
            this.BTNrun.UseVisualStyleBackColor = true;
            this.BTNrun.Click += new System.EventHandler(this.BTNrun_Click);
            // 
            // ImageBoard
            // 
            this.ImageBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageBoard.Location = new System.Drawing.Point(12, 41);
            this.ImageBoard.Name = "ImageBoard";
            this.ImageBoard.Size = new System.Drawing.Size(446, 252);
            this.ImageBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageBoard.TabIndex = 5;
            this.ImageBoard.TabStop = false;
            // 
            // ImageInstructions
            // 
            this.ImageInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageInstructions.Location = new System.Drawing.Point(12, 299);
            this.ImageInstructions.Name = "ImageInstructions";
            this.ImageInstructions.Size = new System.Drawing.Size(446, 88);
            this.ImageInstructions.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageInstructions.TabIndex = 6;
            this.ImageInstructions.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 393);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(446, 89);
            this.flowLayoutPanel1.TabIndex = 4;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // BTNback
            // 
            this.BTNback.Location = new System.Drawing.Point(12, 12);
            this.BTNback.Name = "BTNback";
            this.BTNback.Size = new System.Drawing.Size(75, 23);
            this.BTNback.TabIndex = 7;
            this.BTNback.Text = "Back";
            this.BTNback.UseVisualStyleBackColor = true;
            this.BTNback.Click += new System.EventHandler(this.BTNback_Click);
            // 
            // LBLstage
            // 
            this.LBLstage.AutoSize = true;
            this.LBLstage.Location = new System.Drawing.Point(93, 17);
            this.LBLstage.Name = "LBLstage";
            this.LBLstage.Size = new System.Drawing.Size(79, 13);
            this.LBLstage.TabIndex = 8;
            this.LBLstage.Text = "Planning Stage";
            // 
            // GameControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LBLstage);
            this.Controls.Add(this.BTNback);
            this.Controls.Add(this.BTNreset);
            this.Controls.Add(this.ImageInstructions);
            this.Controls.Add(this.ImageBoard);
            this.Controls.Add(this.BTNrun);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "GameControl";
            this.Size = new System.Drawing.Size(470, 552);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImageBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageInstructions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BTNreset;
        private System.Windows.Forms.Button BTNrun;
        private System.Windows.Forms.PictureBox ImageBoard;
        private System.Windows.Forms.PictureBox ImageInstructions;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button BTNback;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label LBLstage;
    }
}


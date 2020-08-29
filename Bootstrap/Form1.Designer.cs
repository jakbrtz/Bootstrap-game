namespace Bootstrap
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tablessTabControl1 = new Bootstrap.TabControlWithoutHeader();
            this.Home = new System.Windows.Forms.TabPage();
            this.BTNhelp = new System.Windows.Forms.Button();
            this.BTNeditor = new System.Windows.Forms.Button();
            this.BTNplay = new System.Windows.Forms.Button();
            this.SelectLevel = new System.Windows.Forms.TabPage();
            this.LBLselectlevel = new System.Windows.Forms.Label();
            this.BTNselectBack = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Play = new System.Windows.Forms.TabPage();
            this.gameControl1 = new Bootstrap.GameControl();
            this.Editor = new System.Windows.Forms.TabPage();
            this.editorControl1 = new Bootstrap.EditorControl();
            this.Help = new System.Windows.Forms.TabPage();
            this.BTNhelpback = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tablessTabControl1.SuspendLayout();
            this.Home.SuspendLayout();
            this.SelectLevel.SuspendLayout();
            this.Play.SuspendLayout();
            this.Editor.SuspendLayout();
            this.Help.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tablessTabControl1
            // 
            this.tablessTabControl1.Controls.Add(this.Home);
            this.tablessTabControl1.Controls.Add(this.SelectLevel);
            this.tablessTabControl1.Controls.Add(this.Play);
            this.tablessTabControl1.Controls.Add(this.Editor);
            this.tablessTabControl1.Controls.Add(this.Help);
            this.tablessTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablessTabControl1.Location = new System.Drawing.Point(0, 0);
            this.tablessTabControl1.Name = "tablessTabControl1";
            this.tablessTabControl1.SelectedIndex = 0;
            this.tablessTabControl1.Size = new System.Drawing.Size(467, 576);
            this.tablessTabControl1.TabIndex = 0;
            // 
            // Home
            // 
            this.Home.BackColor = System.Drawing.SystemColors.Control;
            this.Home.Controls.Add(this.BTNhelp);
            this.Home.Controls.Add(this.BTNeditor);
            this.Home.Controls.Add(this.BTNplay);
            this.Home.Location = new System.Drawing.Point(4, 22);
            this.Home.Name = "Home";
            this.Home.Padding = new System.Windows.Forms.Padding(3);
            this.Home.Size = new System.Drawing.Size(459, 550);
            this.Home.TabIndex = 0;
            this.Home.Text = "Home";
            // 
            // BTNhelp
            // 
            this.BTNhelp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BTNhelp.Location = new System.Drawing.Point(192, 335);
            this.BTNhelp.Name = "BTNhelp";
            this.BTNhelp.Size = new System.Drawing.Size(75, 23);
            this.BTNhelp.TabIndex = 2;
            this.BTNhelp.Text = "Help";
            this.BTNhelp.UseVisualStyleBackColor = true;
            this.BTNhelp.Click += new System.EventHandler(this.BTNhelp_Click);
            // 
            // BTNeditor
            // 
            this.BTNeditor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BTNeditor.Location = new System.Drawing.Point(192, 263);
            this.BTNeditor.Name = "BTNeditor";
            this.BTNeditor.Size = new System.Drawing.Size(75, 23);
            this.BTNeditor.TabIndex = 1;
            this.BTNeditor.Text = "Editor";
            this.BTNeditor.UseVisualStyleBackColor = true;
            this.BTNeditor.Click += new System.EventHandler(this.BTNeditor_Click);
            // 
            // BTNplay
            // 
            this.BTNplay.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BTNplay.Location = new System.Drawing.Point(192, 179);
            this.BTNplay.Name = "BTNplay";
            this.BTNplay.Size = new System.Drawing.Size(75, 23);
            this.BTNplay.TabIndex = 0;
            this.BTNplay.Text = "Play";
            this.BTNplay.UseVisualStyleBackColor = true;
            this.BTNplay.Click += new System.EventHandler(this.BTNplay_Click);
            // 
            // SelectLevel
            // 
            this.SelectLevel.BackColor = System.Drawing.SystemColors.Control;
            this.SelectLevel.Controls.Add(this.LBLselectlevel);
            this.SelectLevel.Controls.Add(this.BTNselectBack);
            this.SelectLevel.Controls.Add(this.flowLayoutPanel1);
            this.SelectLevel.Location = new System.Drawing.Point(4, 22);
            this.SelectLevel.Name = "SelectLevel";
            this.SelectLevel.Padding = new System.Windows.Forms.Padding(3);
            this.SelectLevel.Size = new System.Drawing.Size(459, 550);
            this.SelectLevel.TabIndex = 1;
            this.SelectLevel.Text = "SelectLevel";
            // 
            // LBLselectlevel
            // 
            this.LBLselectlevel.AutoSize = true;
            this.LBLselectlevel.Location = new System.Drawing.Point(93, 17);
            this.LBLselectlevel.Name = "LBLselectlevel";
            this.LBLselectlevel.Size = new System.Drawing.Size(78, 13);
            this.LBLselectlevel.TabIndex = 2;
            this.LBLselectlevel.Text = "Select a Level:";
            // 
            // BTNselectBack
            // 
            this.BTNselectBack.Location = new System.Drawing.Point(12, 12);
            this.BTNselectBack.Name = "BTNselectBack";
            this.BTNselectBack.Size = new System.Drawing.Size(75, 23);
            this.BTNselectBack.TabIndex = 1;
            this.BTNselectBack.Text = "Back";
            this.BTNselectBack.UseVisualStyleBackColor = true;
            this.BTNselectBack.Click += new System.EventHandler(this.BackPressed);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(8, 41);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(429, 501);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // Play
            // 
            this.Play.BackColor = System.Drawing.SystemColors.Control;
            this.Play.Controls.Add(this.gameControl1);
            this.Play.Location = new System.Drawing.Point(4, 22);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(459, 550);
            this.Play.TabIndex = 2;
            this.Play.Text = "Play";
            // 
            // gameControl1
            // 
            this.gameControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameControl1.Location = new System.Drawing.Point(0, 0);
            this.gameControl1.Name = "gameControl1";
            this.gameControl1.Size = new System.Drawing.Size(459, 550);
            this.gameControl1.TabIndex = 0;
            this.gameControl1.BackPressed += new System.EventHandler(this.BackPressed);
            this.gameControl1.LevelWon += new System.EventHandler(this.GameControl1_LevelWon);
            // 
            // Editor
            // 
            this.Editor.BackColor = System.Drawing.SystemColors.Control;
            this.Editor.Controls.Add(this.editorControl1);
            this.Editor.Location = new System.Drawing.Point(4, 22);
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(459, 550);
            this.Editor.TabIndex = 3;
            this.Editor.Text = "Editor";
            // 
            // editorControl1
            // 
            this.editorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorControl1.Location = new System.Drawing.Point(0, 0);
            this.editorControl1.Name = "editorControl1";
            this.editorControl1.Size = new System.Drawing.Size(459, 550);
            this.editorControl1.TabIndex = 0;
            this.editorControl1.BackPressed += new System.EventHandler(this.BackPressed);
            this.editorControl1.Save += new System.EventHandler<Bootstrap.SaveEventArgs>(this.EditorControl1_Save);
            // 
            // Help
            // 
            this.Help.AutoScroll = true;
            this.Help.BackColor = System.Drawing.SystemColors.Control;
            this.Help.Controls.Add(this.BTNhelpback);
            this.Help.Controls.Add(this.label7);
            this.Help.Controls.Add(this.pictureBox7);
            this.Help.Controls.Add(this.label6);
            this.Help.Controls.Add(this.pictureBox6);
            this.Help.Controls.Add(this.label4);
            this.Help.Controls.Add(this.pictureBox4);
            this.Help.Controls.Add(this.label3);
            this.Help.Controls.Add(this.pictureBox3);
            this.Help.Controls.Add(this.label2);
            this.Help.Controls.Add(this.pictureBox2);
            this.Help.Controls.Add(this.label1);
            this.Help.Controls.Add(this.pictureBox1);
            this.Help.Location = new System.Drawing.Point(4, 22);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(459, 550);
            this.Help.TabIndex = 4;
            this.Help.Text = "Help";
            // 
            // BTNhelpback
            // 
            this.BTNhelpback.Location = new System.Drawing.Point(12, 12);
            this.BTNhelpback.Name = "BTNhelpback";
            this.BTNhelpback.Size = new System.Drawing.Size(75, 23);
            this.BTNhelpback.TabIndex = 14;
            this.BTNhelpback.Text = "Back";
            this.BTNhelpback.UseVisualStyleBackColor = true;
            this.BTNhelpback.Click += new System.EventHandler(this.BackPressed);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(82, 391);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(359, 64);
            this.label7.TabIndex = 13;
            this.label7.Text = "Goal:\r\nGo to the goal.\r\nYou cannot do any more actions after this.";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::Bootstrap.Properties.Resources.goal;
            this.pictureBox7.Location = new System.Drawing.Point(12, 391);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(64, 64);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 12;
            this.pictureBox7.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(82, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(359, 64);
            this.label6.TabIndex = 11;
            this.label6.Text = "Time travel:\r\nTravel in time to the start of the level.\r\nYou stay in the same pos" +
    "ition.";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::Bootstrap.Properties.Resources.timeMachine;
            this.pictureBox6.Location = new System.Drawing.Point(12, 321);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(64, 64);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 10;
            this.pictureBox6.TabStop = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(82, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(359, 64);
            this.label4.TabIndex = 7;
            this.label4.Text = "Unlock Door:\r\nUse a key to unlock a door.\r\nThis door will remain open.";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Bootstrap.Properties.Resources.doorClosed1;
            this.pictureBox4.Location = new System.Drawing.Point(12, 251);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(64, 64);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(82, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(359, 64);
            this.label3.TabIndex = 5;
            this.label3.Text = "Take key:\r\nPick up a key.\r\nEach key can only picked up and used once per level.";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Bootstrap.Properties.Resources.key1;
            this.pictureBox3.Location = new System.Drawing.Point(12, 181);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(64, 64);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(82, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(359, 64);
            this.label2.TabIndex = 3;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Bootstrap.Properties.Resources.plate1;
            this.pictureBox2.Location = new System.Drawing.Point(12, 111);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(64, 64);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(82, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(359, 64);
            this.label1.TabIndex = 1;
            this.label1.Text = "Move:\r\nGo to the next room.\r\nIf there is a door in your way, you will wait until " +
    "the door is open.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Bootstrap.Properties.Resources.commandUp;
            this.pictureBox1.Location = new System.Drawing.Point(12, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 576);
            this.Controls.Add(this.tablessTabControl1);
            this.Name = "Form1";
            this.Text = "Bootstrap";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tablessTabControl1.ResumeLayout(false);
            this.Home.ResumeLayout(false);
            this.SelectLevel.ResumeLayout(false);
            this.SelectLevel.PerformLayout();
            this.Play.ResumeLayout(false);
            this.Editor.ResumeLayout(false);
            this.Help.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControlWithoutHeader tablessTabControl1;
        private System.Windows.Forms.TabPage Home;
        private System.Windows.Forms.TabPage SelectLevel;
        private System.Windows.Forms.TabPage Play;
        private System.Windows.Forms.TabPage Editor;
        private System.Windows.Forms.TabPage Help;
        private GameControl gameControl1;
        private System.Windows.Forms.Button BTNhelp;
        private System.Windows.Forms.Button BTNeditor;
        private System.Windows.Forms.Button BTNplay;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private EditorControl editorControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTNhelpback;
        private System.Windows.Forms.Button BTNselectBack;
        private System.Windows.Forms.Label LBLselectlevel;
    }
}
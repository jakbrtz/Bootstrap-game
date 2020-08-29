using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bootstrap
{
    public partial class EditorControl : UserControl
    {
        public event EventHandler BackPressed;
        public event EventHandler<SaveEventArgs> Save;

        public Level currentLevel;

        public EditorControl()
        {
            InitializeComponent();
        }

        private void BTNpreview_Click(object sender, EventArgs e)
        {
            SetLevel(TBXlevel.Text);
        }

        public void SetLevel(string levelData)
        {
            TBXlevel.Text = levelData;
            currentLevel = new Level(TBXlevel.Text);
            ImageBoard.Image = Animation.DrawBoard(currentLevel);
        }

        private void BTNback_Click(object sender, EventArgs e)
        {
            BackPressed?.Invoke(this, new EventArgs());
        }

        private void BTNsave_Click(object sender, EventArgs e)
        {
            Save?.Invoke(this, new SaveEventArgs(TBXlevel.Text, false));
        }

        private void BTNsavenew_Click(object sender, EventArgs e)
        {
            Save?.Invoke(this, new SaveEventArgs(TBXlevel.Text, true));
        }
    }

    public class SaveEventArgs : EventArgs
    {
        public string levelData;
        public bool saveNew;

        public SaveEventArgs(string levelData, bool saveNew)
        {
            this.levelData = levelData;
            this.saveNew = saveNew;
        }
    }
}

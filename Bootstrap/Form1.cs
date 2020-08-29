using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bootstrap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> levels;
        int currentLevel;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("Levels.txt"))
                levels = File.ReadAllText("Levels.txt").Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            else
                levels = Properties.Resources.Levels.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private void BTNplay_Click(object sender, EventArgs e)
        {
            LoadLevels(play: true);
        }

        private void BTNeditor_Click(object sender, EventArgs e)
        {
            LoadLevels(play: false);
        }

        void LoadLevels(bool play)
        {
            flowLayoutPanel1.Controls.Clear();

            for (int levelIndex = 0; levelIndex < levels.Count; levelIndex++)
            {
                string levelData = levels[levelIndex];
                Level level = new Level(levelData);
                Button newButton = new Button
                {
                    Anchor = AnchorStyles.None,
                    BackgroundImage = Animation.DrawBoard(level),
                    BackgroundImageLayout = ImageLayout.Zoom,
                    Size = new Size(flowLayoutPanel1.Width / 2 - 20, flowLayoutPanel1.Width / 2 - 20),
                };
                int _levelIndex = levelIndex;
                if (play)
                {
                    newButton.Click += (sender, e) => SelectLevelToPlay(_levelIndex);
                    LBLselectlevel.Text = "Select a level to play:";
                }
                else
                {
                    newButton.Click += (sender, e) => SelectLevelToEdit(_levelIndex);
                    LBLselectlevel.Text = "Select a level to edit:";
                }
                flowLayoutPanel1.Controls.Add(newButton);
            }
            tablessTabControl1.SelectTab("SelectLevel");
        }

        void SelectLevelToPlay(int levelIndex)
        {
            currentLevel = levelIndex;
            gameControl1.SetLevel(levels[currentLevel]);
            tablessTabControl1.SelectTab("Play");
        }

        void SelectLevelToEdit(int levelIndex)
        {
            currentLevel = levelIndex;
            editorControl1.SetLevel(levels[currentLevel]);
            tablessTabControl1.SelectTab("Editor");
        }

        private void GameControl1_LevelWon(object sender, EventArgs e)
        {
            try
            {
                SelectLevelToPlay(currentLevel + 1);
            }
            catch
            {
                currentLevel = 0;
                tablessTabControl1.SelectTab("Home");
            }
        }

        private void EditorControl1_Save(object sender, SaveEventArgs e)
        {
            if (e.saveNew)
                levels.Add(e.levelData);
            else
                levels[currentLevel] = e.levelData;
            File.WriteAllText("Levels.txt", string.Join("\r\n\r\n", levels));
            tablessTabControl1.SelectTab("Home");
        }

        private void BackPressed(object sender, EventArgs e)
        {
            tablessTabControl1.SelectTab("Home");
        }

        private void BTNhelp_Click(object sender, EventArgs e)
        {
            tablessTabControl1.SelectTab("Help");
        }
    }
}

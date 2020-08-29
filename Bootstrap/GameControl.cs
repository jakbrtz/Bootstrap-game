using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bootstrap
{
    public partial class GameControl : UserControl
    {
        public event EventHandler BackPressed;
        public event EventHandler LevelWon;

        public Level currentLevel;

        public GameControl()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Animation.ImageBoard = ImageBoard;
            Animation.ImageInstructions = ImageInstructions;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            animationThread.Abort();
        }

        private void BTNreset_Click(object sender, EventArgs e)
        {
            ResetLevel();
        }

        public void SetLevel(string levelData)
        {
            currentLevel = new Level(levelData);
            ResetLevel();
        }

        void ResetLevel()
        {
            selectedInstructions.Clear();
            ImageInstructions.Image = null;
            player = currentLevel.Start.block;
            LoadButtons();
        }

        Coordinate player;

        void LoadButtons()
        {
            flowLayoutPanel1.Controls.Clear();
            List<Instruction> instructions = ScriptWriter.GetOptions(currentLevel, player, selectedInstructions);
            foreach (Instruction instruction in instructions)
            {
                Button newButton = new Button {
                    BackgroundImage = instruction.GetImage(),
                    BackgroundImageLayout = ImageLayout.Zoom,
                    Size = new Size(64, 64),
                    
                };
                toolTip1.SetToolTip(newButton, instruction.Name);
                newButton.Click += (sender, e) => SelectAction(instruction);
                flowLayoutPanel1.Controls.Add(newButton);
            }
            ImageBoard.Image = Animation.DrawBoard(currentLevel, extraPlayer: new Position(player));
        }

        readonly List<Instruction> selectedInstructions = new List<Instruction>();

        void SelectAction(Instruction instruction)
        {
            switch (instruction.command)
            {
                case Command.move:
                    switch (instruction.index)
                    {
                        case 'U':
                            player.Y--;
                            break;
                        case 'D':
                            player.Y++;
                            break;
                        case 'L':
                            player.X--;
                            break;
                        case 'R':
                            player.X++;
                            break;
                    }
                    break;
            }
            selectedInstructions.Add(instruction); //TODO: insert instruction

            AnswerScript script = Solver.GenerateScript(currentLevel, selectedInstructions);
            Animation.DrawScript(script, 0, 0, false);

            LoadButtons();
        }

        private bool running = false;
        Thread animationThread;

        private void BTNrun_Click(object sender, EventArgs e)
        {
            running = !running;
            if (running)
            {
                LBLstage.Text = "Executing Plan";
                BTNrun.Text = "Stop";
                AnswerScript script = Solver.GenerateScript(currentLevel, selectedInstructions);
                ImageBoard.Image = Animation.DrawBoard(currentLevel, extraPlayer: currentLevel.Start);
                Animation.DrawScript(script, 0, 0, true);

                (animationThread = new Thread(() => {
                    Animation.AnimateAnswer(currentLevel, script);
                    Invoke((MethodInvoker)delegate {
                        if (script.Success)
                            Win();
                        else
                            Lose();
                        LBLstage.Text = "Planning Stage";
                        BTNrun.Text = "Run";
                        running = false;
                    });
                })).Start();

                void Win()
                {
                    Animation.AddBanner(Properties.Resources.levelWin);
                    Thread.Sleep(1000);
                    LevelWon?.Invoke(this, new EventArgs());
                }

                void Lose()
                {
                    Animation.AddBanner(Properties.Resources.levelLose);
                    Thread.Sleep(1000);
                    ImageBoard.Image = Animation.DrawBoard(currentLevel, extraPlayer: new Position(player));
                }
            }
            else
            {
                animationThread.Abort();
                ImageBoard.Image = Animation.DrawBoard(currentLevel, extraPlayer: currentLevel.Start);
                LBLstage.Text = "Planning Stage";
                BTNrun.Text = "Run";
            }
        }

        private void BTNback_Click(object sender, EventArgs e)
        {
            BackPressed?.Invoke(this, new EventArgs());
        }
    }
}

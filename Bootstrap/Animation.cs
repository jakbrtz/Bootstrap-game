using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Bootstrap
{
    static class Animation
    {
        /// <summary>
        /// The PictureBox that contains the level
        /// </summary>
        public static PictureBox ImageBoard;
        /// <summary>
        /// The PictureBox thta contains the script that the user creates
        /// </summary>
        public static PictureBox ImageInstructions;

        public static int rewindPause = 200;
        public static float speed = 3;
        public static float rewindSpeed = 20;

        static readonly int spriteSize = 16;

        static readonly Image[] doorClosed = new Image[] {
            Properties.Resources.doorClosed1,
            Properties.Resources.doorClosed2,
            Properties.Resources.doorClosed3,
            Properties.Resources.doorClosed4,
        };
        static readonly Image[] doorOpen = new Image[] {
            Properties.Resources.doorOpen1,
            Properties.Resources.doorOpen2,
            Properties.Resources.doorOpen3,
            Properties.Resources.doorOpen4,
        };
        static readonly Image[] key = new Image[] {
            Properties.Resources.key1,
            Properties.Resources.key2,
            Properties.Resources.key3,
            Properties.Resources.key4,
        };
        static readonly Image[] dropKey = new Image[] {
            Properties.Resources.dropKey1,
            Properties.Resources.dropKey2,
            Properties.Resources.dropKey3,
            Properties.Resources.dropKey4,
        };
        static readonly Image[] plate = new Image[] {
            Properties.Resources.plate1,
            Properties.Resources.plate2,
            Properties.Resources.plate3,
            Properties.Resources.plate4,
        };

        /// <param name="script">The answer that the user submitted for this level</param>
        public static void AnimateAnswer(Level level, AnswerScript script)
        {
            Stopwatch sw = new Stopwatch();

            for (int run = 0; run < script.player.Length; run++)
            {
                bool last = run == script.player.Length - 1;
                float end = last ? script.duration + 1 : script.player[run].Keys.Last();

                float time = 0;
                while (time < end)
                {
                    sw.Restart();
                    ImageBoard.Image = DrawBoard(level, script, time, run: run);
                    DrawScript(script, time, run, true);
                    sw.Stop();
                    time += speed * sw.ElapsedMilliseconds / 1000;
                }

                if (last) break;

                System.Threading.Thread.Sleep(rewindPause);

                while (time > 0)
                {
                    sw.Restart();
                    ImageBoard.Image = DrawBoard(level, script, time, run: run, rewind: true);
                    DrawScript(script, time, run, true);
                    sw.Stop();
                    time -= rewindSpeed * sw.ElapsedMilliseconds / 1000;
                }

                System.Threading.Thread.Sleep(rewindPause);
            }
        }

        /// <summary>
        /// Animates a single frame of the level onto ImageBoard
        /// </summary>
        /// <param name="level">The level that is being displayed</param>
        /// <param name="script">The answer that the user has submitted for this level</param>
        /// <param name="time">What fram should be animated</param>
        /// <param name="run">Which version of the player should be highlighted</param>
        /// <param name="extraPlayer">Should an additional player be animated? If so, where?</param>
        /// <param name="rewind">Is the animation rewinding?</param>
        public static Image DrawBoard(Level level, AnswerScript? script = null, float time = 0, int run = -1, Position? extraPlayer = null, bool rewind = false)
        {
            Bitmap bmp = new Bitmap(level.Width * spriteSize * Level.cellSize + spriteSize, level.Height * spriteSize * Level.cellSize + spriteSize);
            List<Sprite> sprites = new List<Sprite>();

            void AddSprite(float x, float y, Image sprite, Depth z)
            {
                sprites.Add(new Sprite(sprite, x + spriteSize / 2, y + spriteSize / 2, z));
            }

            void MakeSprite(Position position, Image sprite, Depth z)
            {
                AddSprite(position.X * spriteSize, position.Y * spriteSize, sprite, z);
            }

            //Plates
            for (int door = 0; door < level.Doors.Length; door++)
                if (level.Doors[door].usesPlate)
                    MakeSprite(level.Doors[door].unlockPosition, plate[door], Depth.floor);

            //Time Machines
            foreach (Position timeMachine in level.TimeMachines)
                MakeSprite(timeMachine, Properties.Resources.timeMachine, Depth.floor);

            //Player
            if (!script.HasValue)
            {
                if (!extraPlayer.HasValue)
                    MakeSprite(level.Start, Properties.Resources.playerCurrent, Depth.playerCurrent);
            }
            else
            {
                for (int player = 0; player < script.Value.player.Length; player++)
                {
                    KeyValuePair<float, PlayerState> previousPlayer = script.Value.player[player].Last(kvp => kvp.Key <= time);

                    if (previousPlayer.Value.exists)
                    {
                        Position previous = previousPlayer.Value.position;

                        float x = previous.X * spriteSize;
                        float y = previous.Y * spriteSize;

                        if (previousPlayer.Key != time && previousPlayer.Key != script.Value.player[player].Last().Key)
                        {
                            KeyValuePair<float, PlayerState> nextPlayer = script.Value.player[player].First(kvp => kvp.Key >= time);
                            Position next = nextPlayer.Value.position;

                            float previousFraction = (time - previousPlayer.Key) / (nextPlayer.Key - previousPlayer.Key);
                            float nextFraction = (nextPlayer.Key - time) / (nextPlayer.Key - previousPlayer.Key);

                            x = (previous.X * spriteSize) * nextFraction + (next.X * spriteSize) * previousFraction;
                            y = (previous.Y * spriteSize) * nextFraction + (next.Y * spriteSize) * previousFraction;
                        }

                        AddSprite(x, y, player == run && !rewind ? Properties.Resources.playerCurrent : Properties.Resources.playerOther, player == run ? Depth.playerCurrent : Depth.playerOther);
                    }
                }
            }

            if (extraPlayer.HasValue)
            {
                MakeSprite(extraPlayer.Value, Properties.Resources.playerOther, Depth.playerCurrent);
            }

            if (run != -1 && rewind && script.HasValue)
            {
                MakeSprite(script.Value.player[run].Last().Value.position, Properties.Resources.playerCurrent, Depth.playerCurrent);
            }

            //Keys
            if (!script.HasValue)
            {
                for (int door = 0; door < level.Doors.Length; door++)
                    if (!level.Doors[door].usesPlate)
                        MakeSprite(level.Doors[door].unlockPosition, key[door], Depth.held);
            }
            else
            {
                foreach (SortedList<float, KeyState> keyScript in script.Value.keys)
                {
                    KeyValuePair<float, KeyState> previousKey = keyScript.Last(kvp => kvp.Key <= time);

                    if (previousKey.Value.exists)
                    {
                        Position previous = previousKey.Value.position;

                        float x = previous.X * spriteSize;
                        float y = previous.Y * spriteSize;

                        if (previousKey.Key != time && previousKey.Key != keyScript.Keys.Last())
                        {
                            KeyValuePair<float, KeyState> nextKey = keyScript.First(kvp => kvp.Key >= time);
                            Position next = nextKey.Value.position;

                            float previousFraction = (time - previousKey.Key) / (nextKey.Key - previousKey.Key);
                            float nextFraction = (nextKey.Key - time) / (nextKey.Key - previousKey.Key);

                            x = (previous.X * spriteSize) * nextFraction + (next.X * spriteSize) * previousFraction;
                            y = (previous.Y * spriteSize) * nextFraction + (next.Y * spriteSize) * previousFraction;
                        }

                        AddSprite(x, y, key[previousKey.Value.colour], Depth.held);
                    }
                }
            }

            if (run != -1 && rewind && script.HasValue)
            {
                foreach (SortedList<float, KeyState> keys in script.Value.keys)
                    if (keys.Last().Value.holderIndex == run)
                        MakeSprite(keys.Last().Value.position, key[keys.Last().Value.colour], Depth.held);
            }

            //Walls for walls
            foreach (Wall wall in level.Walls)
                if (wall.facingRight)
                    for (int i = 0; i < Level.cellSize; i++)
                        AddSprite((wall.coordinate.X + 1) * spriteSize * Level.cellSize - spriteSize/2, wall.coordinate.Y * spriteSize * Level.cellSize + spriteSize * i - spriteSize/2, Properties.Resources.wallVertical, Depth.wall);
                else
                    for (int i = 0; i < Level.cellSize; i++)
                        AddSprite(wall.coordinate.X * spriteSize * Level.cellSize + spriteSize * i, (wall.coordinate.Y + 1) * spriteSize * Level.cellSize - spriteSize/2, Properties.Resources.wallHorizontal, Depth.wall);

            //Walls for doors
            foreach (Door door in level.Doors)
                if (door.placement.facingRight)
                {
                    for (int i = 0; i < Level.cellSize; i++)
                        if (i != Level.CellMiddle)
                            AddSprite((door.placement.coordinate.X + 1) * spriteSize * Level.cellSize - spriteSize/2, door.placement.coordinate.Y * spriteSize * Level.cellSize + spriteSize * i - spriteSize/2, Properties.Resources.wallVertical, Depth.wall);
                }
                else
                {
                    for (int i = 0; i < Level.cellSize; i++)
                        if (i != Level.CellMiddle)
                            AddSprite(door.placement.coordinate.X * spriteSize * Level.cellSize + spriteSize * i, (door.placement.coordinate.Y + 1) * spriteSize * Level.cellSize - spriteSize/2, Properties.Resources.wallHorizontal, Depth.wall);
                }

            //Outer walls
            for (int x = 0; x < level.Width * Level.cellSize; x++)
                AddSprite(spriteSize * x, -spriteSize/2, Properties.Resources.wallHorizontal, Depth.wall);
            for (int x = 0; x < level.Width * Level.cellSize; x++)
                AddSprite(spriteSize * x, level.Height * spriteSize * Level.cellSize - spriteSize/2, Properties.Resources.wallHorizontal, Depth.wall);
            for (int y = 0; y < level.Height * Level.cellSize; y++)
                AddSprite(-spriteSize/2, spriteSize * y - spriteSize/2, Properties.Resources.wallVertical, Depth.wall);
            for (int y = 0; y < level.Height * Level.cellSize; y++)
                AddSprite(level.Width * spriteSize * Level.cellSize - spriteSize/2, spriteSize * y - spriteSize/2, Properties.Resources.wallVertical, Depth.wall);

            //Doors
            for (int door = 0; door < level.Doors.Length; door++)
            {
                Wall placement = level.Doors[door].placement;
                bool facingRight = placement.facingRight;
                bool open = script.HasValue && script.Value.doorsOpen[door].Last(kvp => kvp.Key <= time).Value;

                Position position = new Position(placement.coordinate, new Coordinate(facingRight ? Level.cellSize : Level.CellMiddle, facingRight ? Level.CellMiddle : Level.cellSize));
                int x = position.X * spriteSize;
                int y = position.Y * spriteSize;

                if (facingRight)
                    x -= spriteSize/2;
                else
                    y -= spriteSize/2;

                AddSprite(x, y, open ? doorOpen[door] : doorClosed[door], Depth.wall);
            }

            //Goal
            MakeSprite(level.Goal, Properties.Resources.goal, Depth.floor);

            sprites.Sort(new SpriteComparer());

            Graphics g = Graphics.FromImage(bmp);

            //Floor
            for (int y = 0; y < level.Height; y++)
                for (int x = 0; x < level.Width; x++)
                    g.DrawImage(Properties.Resources.cellEmpty, x * spriteSize * Level.cellSize + spriteSize/2, y * spriteSize * Level.cellSize + spriteSize/2);

            //Everything else
            foreach (Sprite sprite in sprites)
                g.DrawImage(sprite.Image, sprite.X, sprite.Y);

            //Rewind effect
            if (rewind)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(63, 0, 0, 0)), 0, 0, bmp.Width, bmp.Height);
                g.DrawImage(Properties.Resources.rewind, spriteSize * Level.cellSize * level.Width - spriteSize / 2, spriteSize / 2);
            }

            return bmp;
        }

        /// <summary>
        /// Visually display the answer that the user submitted for this level
        /// </summary>
        /// <param name="script">The answer that the user submitted for this level</param>
        /// <param name="highlightTime">Draw a vertical line that corresponds with this point in time</param>
        /// <param name="highlightRun">Highlight a row that corresponds with this point in time</param>
        /// <param name="showMistakes">Should X's be drawn where an action is not allowed</param>
        public static void DrawScript(AnswerScript script, float highlightTime, int highlightRun, bool showMistakes)
        {
            Bitmap bmp = new Bitmap((int)(1+script.duration * spriteSize), script.player.Length * spriteSize);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Gray);

                g.FillRectangle(new SolidBrush(Color.DimGray), 0, spriteSize * highlightRun, bmp.Width, spriteSize);

                for (int run = 0; run < script.player.Length; run++)
                    foreach (KeyValuePair<float, PlayerState> kvp in script.player[run])
                        if (kvp.Value.instruction.GetImage() != null)
                            g.DrawImage(kvp.Value.instruction.GetImage(), kvp.Key * spriteSize, run * spriteSize);

                if (showMistakes)
                    for (int run = 0; run < script.mistakes.Length; run++)
                        foreach (float mistake in script.mistakes[run])
                            g.DrawImage(Properties.Resources.cross, mistake * spriteSize, run * spriteSize);
                
                g.DrawLine(new Pen(Color.DarkGreen), spriteSize * highlightTime, 0, spriteSize * highlightTime, bmp.Height);

                ImageInstructions.Image = bmp;
            }
            GC.Collect();
        }

        /// <summary>
        /// Converts an instruction to an image that appears in the script
        /// </summary>
        public static Image GetImage(this Instruction instruction)
        {
            switch (instruction.command)
            {
                case Command.move:
                    switch (instruction.index)
                    {
                        case 'U':
                            return Properties.Resources.commandUp;
                        case 'D':
                            return Properties.Resources.commandDown;
                        case 'L':
                            return Properties.Resources.commandLeft;
                        case 'R':
                            return Properties.Resources.commandRight;
                    }
                    break;
                case Command.takeKey:
                    return key[instruction.index];
                case Command.waitPlate:
                    return plate[instruction.index];
                case Command.useKey:
                    return doorClosed[instruction.index];
                case Command.dropKey:
                    return dropKey[instruction.index];
                case Command.useGoal:
                    return Properties.Resources.goal;
                case Command.useTimeMachine:
                    return Properties.Resources.timeMachine;
            }

            return null;
        }

        /// <summary>
        /// Draw an image on top of the ImageBoard
        /// </summary>
        public static void AddBanner(Image image)
        {
            Graphics g = ImageBoard.CreateGraphics();
            bool scaleX = ImageBoard.Width * image.Height < ImageBoard.Height * image.Width;
            g.DrawImage(image, 
                scaleX ? 0 : (ImageBoard.Width - image.Width * ImageBoard.Height / image.Height) / 2, 
                scaleX ? (ImageBoard.Height - image.Height * ImageBoard.Width / image.Width) / 2 : 0,
                scaleX ? ImageBoard.Width : image.Width * ImageBoard.Height / image.Height,
                scaleX ? image.Height * ImageBoard.Width / image.Width : ImageBoard.Height);
        }
    }

    struct Sprite
    {
        public float X { get; }
        public float Y { get; }
        public Depth Depth { get; }
        public Image Image { get; }

        public Sprite(Image image, float x, float y, Depth depth)
        {
            X = x;
            Y = y;
            Depth = depth;
            Image = image;
        }

        public override string ToString()
        {
            return "[" + X.ToString() + ", " + Y.ToString() + "]";
        }
    }
    
    class SpriteComparer : IComparer<Sprite>
    {
        public int Compare(Sprite a, Sprite b)
        {
            if (a.Depth != b.Depth)
            {
                if (a.Depth == Depth.floor)
                    return -1;
                if (b.Depth == Depth.floor)
                    return 1;
            }
            int output = (a.Y + a.Image.Height / 2).CompareTo(b.Y + b.Image.Height / 2);
            if (output == 0)
                output = a.Depth.CompareTo(b.Depth);
            //TODO: hold many keys at once
            return output;
        }
    }

    enum Depth { floor, playerOther, playerCurrent, held, wall }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bootstrap
{
    public struct PlayerState
    {
        public Position position;
        public bool exists;
        public Instruction instruction;

        public override string ToString()
        {
            return position.ToString() + " " + instruction.ToString() + (exists ? "" : " gone");
        }
    }

    public enum Direction { Up, Down, Left, Right }

    public struct AnswerScript
    {
        public float duration;
        public SortedList<float, PlayerState>[] player;
        public SortedList<float, bool>[] doorsOpen;
        public SortedList<float, KeyState>[] keys;
        public bool goalReached;
        public List<float>[] mistakes;

        public bool Success { get { return goalReached && !mistakes.Any(list => list.Any()); } }
    }

    public class Level
    {
        public int Width { get; }
        public int Height { get; }
        public Wall[] Walls { get; }
        public Position Start { get; }
        public Position Goal { get; }
        public Position[] TimeMachines { get; }
        public Door[] Doors { get; }

        public Level(string file)
        {
            /* The input file is comma delimited. 
             * the first part the width and height of the map
             * the next part is the coordinates of the start
             * the next part is the coordinates of the goal
             * the next part is a list of coordinates of time machines
             * the next part is a list of coordinates of plate doors (first door, then plate,  then colour optionally)
             * the next part is a list of coordinates of key doors (first door, then key, then colour optionally)
             * */
            string[] parts = file.Split(',');
            string numbers;

            numbers = parts[0].TrimStart(Environment.NewLine.ToCharArray());
            Width = numbers[0] - '0';
            Height = numbers[1] - '0';

            string[] wallCoordinates = parts[1].TrimStart(Environment.NewLine.ToCharArray()).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            if (wallCoordinates.Length == 1 && wallCoordinates[0] == "")
                Walls = new Wall[0];
            else
            {
                Walls = new Wall[wallCoordinates.Length];
                for (int w = 0; w < wallCoordinates.Length; w++)
                {
                    numbers = wallCoordinates[w];
                    Walls[w] = new Wall(numbers[0] - '0', numbers[1] - '0', numbers[2] == 'r');
                }
            }

            numbers = parts[2].TrimStart(Environment.NewLine.ToCharArray());
            Start = new Position(numbers[0] - '0', numbers[1] - '0', numbers[2] - '0', numbers[3] - '0');

            numbers = parts[3].TrimStart(Environment.NewLine.ToCharArray());
            Goal = new Position(numbers[0] - '0', numbers[1] - '0', numbers[2] - '0', numbers[3] - '0');

            string[] timeMachineCoordinates = parts[4].TrimStart(Environment.NewLine.ToCharArray()).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            if (timeMachineCoordinates.Length == 1 && timeMachineCoordinates[0] == "")
                TimeMachines = new Position[0];
            else
            {
                TimeMachines = new Position[timeMachineCoordinates.Length];
                for (int d = 0; d < timeMachineCoordinates.Length; d++)
                {
                    numbers = timeMachineCoordinates[d];
                    TimeMachines[d] = new Position(numbers[0] - '0', numbers[1] - '0', numbers[2] - '0', numbers[3] - '0');
                }
            }

            string[] plateDoorCoordinates = parts[5].TrimStart(Environment.NewLine.ToCharArray()).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            if (plateDoorCoordinates.Length == 1 && plateDoorCoordinates[0] == "")
                Doors = new Door[0];
            else
            {
                Doors = new Door[plateDoorCoordinates.Length];
                for (int d = 0; d < plateDoorCoordinates.Length; d++)
                {
                    numbers = plateDoorCoordinates[d];
                    Doors[d] = new Door(numbers[0] - '0', numbers[1] - '0', numbers[2] == 'r', numbers[3] == 'p', numbers[4] - '0', numbers[5] - '0', numbers[6] - '0', numbers[7] - '0');
                }
            }
        }

        public bool HasTimeMachine(Position position)
        {
            foreach (Position timeMachine in TimeMachines)
                if (timeMachine.Equals(position))
                    return true;
            return false;
        }

        public int GetDoor(Coordinate coordinate, char direction, bool opposite = false)
        {
            if (opposite)
            {
                switch (direction)
                {
                    case 'U':
                        direction = 'D';
                        break;
                    case 'D':
                        direction = 'U';
                        break;
                    case 'L':
                        direction = 'R';
                        break;
                    case 'R':
                        direction = 'L';
                        break;
                }
            }

            int xblock = direction == 'L' ? coordinate.X - 1 : coordinate.X;
            int yblock = direction == 'U' ? coordinate.Y - 1 : coordinate.Y;
            bool facingRight = direction == 'L' || direction == 'R';

            for (int doorIndex = 0; doorIndex < Doors.Length; doorIndex++)
                if (Doors[doorIndex].placement.coordinate.X == xblock && Doors[doorIndex].placement.coordinate.Y == yblock && Doors[doorIndex].placement.facingRight == facingRight)
                    return doorIndex;
            return -1;
        }

        public char GetDirection(Coordinate coordinate, int doorIndex)
        {
            if (Doors[doorIndex].placement.facingRight)
            {
                if (Doors[doorIndex].placement.coordinate.Equals(coordinate))
                    return 'R';
                else
                    return 'L';
            }
            else
            {
                if (Doors[doorIndex].placement.coordinate.Equals(coordinate))
                    return 'D';
                else
                    return 'U';
            }
        }

        public bool HasWall(Coordinate coordinate, char direction)
        {
            bool facingRight = false;
            switch (direction)
            {
                case 'U':
                    coordinate.Y--;
                    break;
                case 'L':
                    coordinate.X--;
                    facingRight = true;
                    break;
                case 'R':
                    facingRight = true;
                    break;
            }

            return Walls.Contains(new Wall(coordinate.X, coordinate.Y, facingRight));
        }

        public static readonly int cellSize = 3;

        public static int CellMiddle
        {
            get { return cellSize / 2; }
        }
    }

    public struct Wall
    {
        public Coordinate coordinate;
        public bool facingRight;

        public Wall(int x, int y, bool facingRight)
        {
            coordinate = new Coordinate(x, y);
            this.facingRight = facingRight;
        }

        public override string ToString()
        {
            return coordinate.ToString() + (facingRight?" facing right":" facing below");
        }
    }

    public struct Door
    {
        public Wall placement;
        public bool usesPlate;
        public Position unlockPosition;
        //TODO: one-way push door

        public Door(int xBlock, int yBlock, bool facingRight, bool usesPlate, int xPlate, int yPlate, int xPlateInner, int yPlateInner)
        {
            this.placement = new Wall(xBlock, yBlock, facingRight);
            this.usesPlate = usesPlate;
            this.unlockPosition = new Position(xPlate, yPlate, xPlateInner, yPlateInner);
        }

        public override string ToString()
        {
            return placement.ToString();
        }

        public  bool Used(Position a, Position b)
        {
            return (placement.facingRight && a.block.Equals(placement.coordinate) && b.block.Equals(placement.coordinate.Right())) ||
                (placement.facingRight && b.block.Equals(placement.coordinate) && a.block.Equals(placement.coordinate.Right())) ||
                (!placement.facingRight && a.block.Equals(placement.coordinate) && b.block.Equals(placement.coordinate.Below())) ||
                (!placement.facingRight && b.block.Equals(placement.coordinate) && a.block.Equals(placement.coordinate.Below()));
        }
    }

    public struct KeyState
    {
        public int colour;
        public Position position;
        public bool exists;
        public int holderIndex;

        public override string ToString()
        {
            return colour.ToString() + ": " + position.ToString() + (exists ? " exists " : " ") + (holderIndex == -1 && exists ? "" : holderIndex.ToString());
        }
    }

    public struct Position
    {
        public Coordinate block;
        public Coordinate inner;

        public Position (Coordinate block)
        {
            this.block = block;
            this.inner = new Coordinate(Level.CellMiddle, Level.CellMiddle);
        }

        public Position(Coordinate block, Coordinate inner)
        {
            this.block = block;
            this.inner = inner;
        }

        public Position(int xBlock, int yBlock, int xInner, int yInner)
        {
            block = new Coordinate(xBlock, yBlock);
            inner = new Coordinate(xInner, yInner);
        }

        public override string ToString()
        {
            return block.ToString() + " [" + inner.ToString() + "]";
        }

        public float DistanceFrom(Position other)
        {
            float Square(float a) { return a * a; }
            return (float)Math.Sqrt(Square(other.X - X) + Square(other.Y - Y));
        }

        public int X
        {
            get { return block.X * Level.cellSize + inner.X; }
            set
            {
                block.X = value / Level.cellSize;
                inner.X = value % Level.cellSize;
            }
        }

        public int Y
        {
            get { return block.Y * Level.cellSize + inner.Y; }
            set
            {
                block.Y = value / Level.cellSize;
                inner.Y = value % Level.cellSize;
            }
        }
    }

    public struct Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return X + ", " + Y;
        }

        public Coordinate Right()
        {
            return new Coordinate(X + 1, Y);
        }

        public Coordinate Below()
        {
            return new Coordinate(X, Y + 1);
        }
    }

    public enum Command
    {
        none, spawn, despawn,
        gotoWall, waitOpen, move,
        gotoPlate, waitPlate,
        findKey, gotoKey, waitKey, takeKey, dropKey, useKey,
        unlockDoor, lockDoor,
        gotoTimeMachine, useTimeMachine, 
        gotoGoal, useGoal,
        //TODO: multiaction lever
    }

    static class CommandExtension
    {
        public static bool IsWaitCommand(this Command command)
        {
            switch (command)
            {
                case Command.waitKey:
                case Command.waitOpen:
                case Command.waitPlate:
                case Command.findKey:
                case Command.spawn:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsGotoCommand(this Command command)
        {
            switch (command)
            {
                case Command.gotoGoal:
                case Command.gotoKey:
                case Command.gotoPlate:
                case Command.gotoTimeMachine:
                case Command.gotoWall:
                case Command.move:
                    return true;
                default:
                    return false;
            }
        }
    }

    public struct Instruction
    {
        public Command command;
        public int index;

        public bool IsWaitCommand()
        {
            return command.IsWaitCommand();
        }

        public bool IsGotoCommand()
        {
            return command.IsGotoCommand();
        }

        public override string ToString()
        {
            return command.ToString() + " " + index.ToString();
        }

        public string Name
        {
            get
            {
                switch (command)
                {
                    case Command.move:
                        return "Move";
                    case Command.useTimeMachine:
                        return "Time Travel";
                    case Command.useGoal:
                        return "Finish";
                    case Command.waitPlate:
                        return "Pressure Plate";
                    case Command.takeKey:
                        return "Pick up Key";
                    case Command.useKey:
                        return "Unlock Door";
                    case Command.dropKey:
                        return "Drop Key";
                    default:
                        throw new Exception("Instruction without name");
                }
            }
        }
    }
}

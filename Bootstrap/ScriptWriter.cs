using System.Collections.Generic;

namespace Bootstrap
{
    static class ScriptWriter
    {
        /// <summary>
        /// Gets a list of actions that the user can take from their current position
        /// </summary>
        /// <param name="level">The level</param>
        /// <param name="coordinate">Where the user is currently standing</param>
        /// <param name="previousInstructions">A list of instructions that lead up to this point</param>
        /// <returns></returns>
        public static List<Instruction> GetOptions(Level level, Coordinate coordinate, List<Instruction> previousInstructions)
        {
            if (previousInstructions.Count > 0 && previousInstructions[previousInstructions.Count - 1].command == Command.useGoal) return new List<Instruction>();

            int[] heldKeys = new int[level.Doors.Length];
            bool[] keysUsed = new bool[level.Doors.Length];

            foreach (Instruction instruction in previousInstructions)
            {
                switch (instruction.command)
                {
                    case Command.takeKey:
                        heldKeys[instruction.index]++;
                        break;
                    case Command.dropKey:
                    case Command.useKey:
                        heldKeys[instruction.index]--;
                        keysUsed[instruction.index] = true;
                        break;
                }
            }

            List<Instruction> options = new List<Instruction>();
            if (coordinate.X > 0 && !level.HasWall(coordinate, 'L'))
                options.Add(new Instruction { command = Command.move, index = 'L' });
            if (coordinate.Y > 0 && !level.HasWall(coordinate, 'U'))
                options.Add(new Instruction { command = Command.move, index = 'U' });
            if (coordinate.Y + 1 < level.Height && !level.HasWall(coordinate, 'D'))
                options.Add(new Instruction { command = Command.move, index = 'D' });
            if (coordinate.X + 1 < level.Width && !level.HasWall(coordinate, 'R'))
                options.Add(new Instruction { command = Command.move, index = 'R' });
            for (int timeMachine = 0; timeMachine < level.TimeMachines.Length; timeMachine++)
                if (level.TimeMachines[timeMachine].block.Equals(coordinate))
                    options.Add(new Instruction { command = Command.useTimeMachine, index = timeMachine });
            if (level.Goal.block.Equals(coordinate))
                options.Add(new Instruction { command = Command.useGoal, index = 0 });
            for (int door = 0; door < level.Doors.Length; door++)
            {
                if (level.Doors[door].usesPlate && level.Doors[door].unlockPosition.block.Equals(coordinate))
                    options.Add(new Instruction { command = Command.waitPlate, index = door });
                if (!level.Doors[door].usesPlate)
                    if (level.Doors[door].unlockPosition.block.Equals(coordinate) && heldKeys[door] == 0 && !keysUsed[door]) // since dropKey is removed, this extra condition should be added to avoid confusion
                        options.Add(new Instruction { command = Command.takeKey, index = door });
                if (!level.Doors[door].usesPlate && heldKeys[door] > 0)
                {
                    if (level.Doors[door].placement.coordinate.Equals(coordinate) ||
                        (level.Doors[door].placement.facingRight && level.Doors[door].placement.coordinate.Right().Equals(coordinate)) ||
                        (!level.Doors[door].placement.facingRight && level.Doors[door].placement.coordinate.Below().Equals(coordinate)))
                        options.Add(new Instruction { command = Command.useKey, index = door });
                    // options.Add(new Instruction { command = Command.dropKey, index = door }); // this mechanic just makes the game confusing
                }   
            }
            return options;
        }
    }
}

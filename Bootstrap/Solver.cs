using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bootstrap
{
    static class Solver
    {
        /// <summary>
        /// Converts a list of instructions into a script that accounts for timing
        /// </summary>
        public static AnswerScript GenerateScript(Level level, List<Instruction> selectedInstructions)
        {
            // 0.0 Figure out how many runs there are (count useTimeMachine)
            int runs = 1;
            foreach (Instruction instruction in selectedInstructions)
                if (instruction.command == Command.useTimeMachine)
                    runs++;

            // 0.1.0 Record the command index for each time the player spawns
            int[] currentInstruction = new int[runs];

            // 0.1.1 Create structure to hold each state of the player
            SortedList<float, PlayerState>[] playerStates = new SortedList<float, PlayerState>[runs];
            for (int i = 0; i < playerStates.Length; i++)
                playerStates[i] = new SortedList<float, PlayerState>();

            // 0.1.2 Create structure to list every time a mistake is made
            List<float>[] mistakes = new List<float>[runs];
            for (int i = 0; i < mistakes.Length; i++)
                mistakes[i] = new List<float>();

            // 0.1.3 Count the number of keys that may appear simultaneously
            List<KeyState> startingKeyStates = new List<KeyState>();
            for (int doorIndex = 0; doorIndex < level.Doors.Length; doorIndex++)
                if (!level.Doors[doorIndex].usesPlate)
                    startingKeyStates.Add(new KeyState { colour = doorIndex, exists = true, holderIndex = -1, position = level.Doors[doorIndex].unlockPosition });

            // 1.0 Make a list of all the instructions
            List<Instruction> instructionsAsList = new List<Instruction>();

            {
                // 1.0.0 Keep track of where the player is, and which keys are held
                int run = 0;
                Position current = level.Start;
                List<KeyState> keysHeld = new List<KeyState>();

                // 1.0.1 Create the player and record that state
                AddSpawn();
                void AddSpawn()
                {
                    currentInstruction[run] = instructionsAsList.Count;
                    instructionsAsList.Add(new Instruction { command = Command.spawn });
                    playerStates[run][0] = new PlayerState
                    {
                        exists = true,
                        position = current,
                        instruction = new Instruction { command = Command.spawn }
                    }; //TODO: spawn at random time in first second
                }
                // 1.0.2 Create the keys and record their states
                foreach (KeyState state in keysHeld)
                    startingKeyStates.Add(state);
                // 1.1 Iterate over the inputted instructions
                foreach (Instruction instruction in selectedInstructions)
                {
                    switch (instruction.command)
                    {
                        // 1.1.A Moving
                        case Command.move:
                            // If there is a door, walk to the door
                            int door = level.GetDoor(current.block, (char)instruction.index);
                            if (door != -1)
                            {
                                instructionsAsList.Add(new Instruction { command = Command.gotoWall, index = (char)instruction.index });
                                instructionsAsList.Add(new Instruction { command = Command.waitOpen, index = door });
                            }
                            // Go to the next room
                            instructionsAsList.Add(new Instruction { command = Command.move, index = (char)instruction.index });
                            // Move the character to the next room
                            switch ((char)instruction.index)
                            {
                                case 'U':
                                    current.block.Y--;
                                    break;
                                case 'D':
                                    current.block.Y++;
                                    break;
                                case 'L':
                                    current.block.X--;
                                    break;
                                case 'R':
                                    current.block.X++;
                                    break;
                            }
                            break;
                        // 1.1.B Picking up a Key
                        case Command.takeKey:
                            // Make sure the key is in the room, then go to it and take it
                            instructionsAsList.Add(new Instruction { command = Command.findKey, index = instruction.index });
                            instructionsAsList.Add(new Instruction { command = Command.gotoKey, index = instruction.index });
                            instructionsAsList.Add(new Instruction { command = Command.waitKey, index = instruction.index });
                            instructionsAsList.Add(new Instruction { command = Command.takeKey, index = instruction.index });
                            // Keep track of how many keys are held
                            keysHeld.Add(new KeyState { colour = instruction.index, exists = true, holderIndex = run });
                            break;
                        // 1.1.C Standing on a plate
                        case Command.waitPlate:
                            // Go to the plate, unlock the door, wait for the plate to be used, lock the door
                            instructionsAsList.Add(new Instruction { command = Command.gotoPlate, index = instruction.index });
                            instructionsAsList.Add(new Instruction { command = Command.unlockDoor, index = instruction.index });
                            instructionsAsList.Add(new Instruction { command = Command.waitPlate, index = instruction.index });
                            instructionsAsList.Add(new Instruction { command = Command.lockDoor, index = instruction.index });
                            break;
                        // 1.1.D Using a key to unlock a door
                        case Command.useKey:
                            // Unlock the door and destroy the key
                            instructionsAsList.Add(new Instruction { command = Command.gotoWall, index = level.GetDirection(current.block, instruction.index) });
                            instructionsAsList.Add(new Instruction { command = Command.useKey, index = instruction.index });
                            instructionsAsList.Add(new Instruction { command = Command.unlockDoor, index = instruction.index });
                            // Keep track of how many keys are held
                            keysHeld.Remove(keysHeld.First(state => state.colour == instruction.index));
                            break;
                        // 1.1.E Dropping a key
                        case Command.dropKey:
                            // Drop the key
                            instructionsAsList.Add(new Instruction { command = Command.dropKey, index = instruction.index });
                            // Keep track of how many keys are held
                            keysHeld.Remove(keysHeld.First(state => state.colour == instruction.index));
                            break;
                        // 1.1.F Going to the goal
                        case Command.useGoal:
                            // Go to the goal and record that the player has reached it
                            instructionsAsList.Add(new Instruction { command = Command.gotoGoal, index = instruction.index });
                            instructionsAsList.Add(new Instruction { command = Command.useGoal, index = instruction.index });
                            break;
                        // 1.1.G Using a time machine
                        case Command.useTimeMachine:
                            // Go to the time machine and record that the player has reached it. Despawn the player
                            instructionsAsList.Add(new Instruction { command = Command.gotoTimeMachine, index = instruction.index });
                            instructionsAsList.Add(new Instruction { command = Command.useTimeMachine, index = instruction.index });
                            instructionsAsList.Add(new Instruction { command = Command.despawn });
                            // Record the start of the next run
                            run++;
                            // The next run starts at the time machine
                            current = level.TimeMachines[instruction.index];
                            AddSpawn();
                            // Include keys that are carried
                            for (int keyIndex = 0; keyIndex < keysHeld.Count; keyIndex++)
                            {
                                KeyState newKeyState = keysHeld[keyIndex];
                                newKeyState.position = current;
                                newKeyState.holderIndex++;
                                keysHeld[keyIndex] = newKeyState;
                            }
                            // All carried keys must be included in startingKeyStates
                            foreach (KeyState state in keysHeld)
                                startingKeyStates.Add(state);
                            break;
                        default:
                            throw new Exception("Wrong Instruction: " + instruction.ToString());
                    }
                }
                // 1.2 Despawn the player
                instructionsAsList.Add(new Instruction { command = Command.despawn });
            }

            // 2 Setting up remaining variables
            Instruction[] instructions = instructionsAsList.ToArray();
            // 2.0 whether the script works
            bool goalReached = selectedInstructions.Any() && selectedInstructions.Last().command.Equals(Command.useGoal);
            // 2.1 the time which doors open and close
            SortedList<float, bool>[] doorsOpen = new SortedList<float, bool>[level.Doors.Length];
            for (int i = 0; i < doorsOpen.Length; i++)
                doorsOpen[i] = new SortedList<float, bool>();
            foreach (SortedList<float, bool> doorScript in doorsOpen)
                doorScript[0] = false;
            // 2.2 the states of the keys
            SortedList<float, KeyState>[] keyStates = new SortedList<float, KeyState>[startingKeyStates.Count];
            for (int i = 0; i < startingKeyStates.Count; i++)
                keyStates[i] = new SortedList<float, KeyState>();
            for (int starterIndex = 0; starterIndex < startingKeyStates.Count; starterIndex++)
                keyStates[starterIndex][0] = startingKeyStates[starterIndex];
            // 2.3 Keep track of how many times a door has been used
            List<float>[] doorsUsed = new List<float>[level.Doors.Length];
            for (int i = 0; i < level.Doors.Length; i++)
                doorsUsed[i] = new List<float>();
            // 2.4 Keep track of how many times a player is waiting for someone to go through a door
            int[] waitPlates = new int[level.Doors.Length];

            // 3 Populating the state lists
            float duration = 0;

            // 3.1 Decide which run should next be analyzed
            int GetNextRunToAnalyze(out float earliestTime)
            {
                // compare the duration of each of the runs
                int chosenRun = -1;
                earliestTime = duration;

                for (int run = 0; run < runs; run++)
                {
                    // 3.1.0 Get the most recent instruction for this run
                    KeyValuePair<float, PlayerState> timePlayerPair = playerStates[run].Last();
                    float time = timePlayerPair.Key;

                    // 3.1.1 If the most recent instruction is to wait, figure out when the waiting ends
                    if (timePlayerPair.Value.instruction.IsWaitCommand())
                    {
                        switch (timePlayerPair.Value.instruction.command)
                        {
                            // 3.1.1.A Waiting for a key to arrive at the player's position
                            case Command.findKey:
                            case Command.waitKey:
                                int selectedKey = -1;
                                for (int keyIndex = 0; keyIndex < keyStates.Length; keyIndex++)
                                {
                                    KeyState key = keyStates[keyIndex].MostRecent(duration).Value;
                                    if (key.exists && key.colour == timePlayerPair.Value.instruction.index && key.position.block.Equals(timePlayerPair.Value.position.block) && key.holderIndex == -1)
                                        selectedKey = keyIndex;
                                }
                                if (selectedKey == -1)
                                    continue;
                                if (timePlayerPair.Value.instruction.command == Command.waitKey)
                                    time = Math.Max(keyStates[selectedKey].MostRecent(duration).Key, timePlayerPair.Key);
                                break;
                            // 3.1.1.B Waiting for the door to open
                            case Command.waitOpen:
                                KeyValuePair<float, bool> timeDoorPair = doorsOpen[timePlayerPair.Value.instruction.index].Last();
                                if (timeDoorPair.Value)
                                    time = Math.Max(timeDoorPair.Key, timePlayerPair.Key);
                                else
                                    continue;
                                break;
                            // 3.1.1.C Waiting for a door to be used
                            case Command.waitPlate:
                                if (doorsUsed[timePlayerPair.Value.instruction.index].Count >= waitPlates[timePlayerPair.Value.instruction.index])
                                    time = Math.Max(doorsUsed[timePlayerPair.Value.instruction.index][waitPlates[timePlayerPair.Value.instruction.index] - 1], timePlayerPair.Key);
                                else
                                    continue;
                                break;
                        }
                    }

                    // 3.1.2 Compare this run to the best run
                    if (timePlayerPair.Value.instruction.command != Command.despawn && time <= earliestTime)
                    {
                        chosenRun = run;
                        earliestTime = time;
                    }
                }

                if (chosenRun != -1)
                    return chosenRun;

                // 3.1.3 If none of the runs can be evaluated, check if any run is incomplete
                //TODO: look at the rest of each instruction in that run, pick the run with the most instructions
                for (int run = 0; run < runs; run++)
                {
                    Command currentCommand = playerStates[run].Last().Value.instruction.command;
                    if (currentCommand != Command.despawn)
                    {
                        if (currentCommand == Command.waitOpen || currentCommand == Command.waitKey)
                            mistakes[run].Add(earliestTime);
                        earliestTime += 1;
                        return run;
                    }
                }

                return -1;
            }
            
            // 3.2 Analyze chosen run
            while (GetNextRunToAnalyze(out float time) is int run && run != -1)
            {
                // 3.2.0 get the most recent instruction of that run
                PlayerState player = playerStates[run].Last().Value;

                // 3.2.1 Record the player's state
                void RecordPlayerAndKeys(PlayerState? playerState = null)
                {
                    //Advance the instruction
                    if (!playerState.HasValue)
                    {
                        currentInstruction[run]++;
                        if (currentInstruction[run] < instructions.Length)
                            player.instruction = instructions[currentInstruction[run]];
                        playerState = player;
                    }

                    //Record the player's next position
                    playerStates[run][time] = playerState.Value;

                    //Move any keys that the player is holding
                    foreach (SortedList<float, KeyState> keyScript in keyStates)
                    {
                        KeyState key = keyScript.MostRecent(time).Value;
                        if (key.holderIndex == run)
                        {
                            key.exists = playerState.Value.exists;
                            key.position = playerState.Value.position;
                            keyScript[time] = key;
                        }
                    }
                }
                RecordPlayerAndKeys();

                // 3.2.2 Continue evaluating the run until a wait or despawn commmand is reached
                while (!player.instruction.IsWaitCommand() && player.instruction.command != Command.despawn)
                {
                    PlayerState initialState = player;

                    // 3.2.2.0 Figure out where to go next
                    switch (player.instruction.command)
                    {
                        case Command.gotoWall:
                            switch (player.instruction.index)
                            {
                                case 'U':
                                    player.position.inner = new Coordinate(Level.CellMiddle, 0);
                                    break;
                                case 'D':
                                    player.position.inner = new Coordinate(Level.CellMiddle, Level.cellSize - 1);
                                    break;
                                case 'L':
                                    player.position.inner = new Coordinate(0, Level.CellMiddle);
                                    break;
                                case 'R':
                                    player.position.inner = new Coordinate(Level.cellSize - 1, Level.CellMiddle);
                                    break;
                            }
                            break;
                        case Command.move:
                            switch (player.instruction.index)
                            {
                                case 'U':
                                    player.position.inner.Y = 0;
                                    player.position.Y--;
                                    break;
                                case 'D':
                                    player.position.inner.Y = Level.cellSize - 1;
                                    player.position.Y++;
                                    break;
                                case 'L':
                                    player.position.inner.X = 0;
                                    player.position.X--;
                                    break;
                                case 'R':
                                    player.position.inner.X = Level.cellSize - 1;
                                    player.position.X++;
                                    break;
                            }
                            //Check if the next command is another MOVE and the current move is not through a door
                            if (level.GetDoor(initialState.position.block, (char)initialState.instruction.index) == -1 && currentInstruction[run] + 1 < instructions.Length)
                            {
                                Instruction nextInstruction = instructions[currentInstruction[run]+1];
                                if (nextInstruction.command == Command.move)
                                {
                                    switch (nextInstruction.index)
                                    {
                                        case 'U':
                                            player.position.inner.Y = 0;
                                            break;
                                        case 'D':
                                            player.position.inner.Y = Level.cellSize - 1;
                                            break;
                                        case 'L':
                                            player.position.inner.X = 0;
                                            break;
                                        case 'R':
                                            player.position.inner.X = Level.cellSize - 1;
                                            break;
                                    }
                                }
                            }
                            break;
                        case Command.gotoPlate:
                            player.position = level.Doors[player.instruction.index].unlockPosition;
                            break;
                        case Command.gotoKey:
                            for (int keyIndex = 0; keyIndex < keyStates.Length; keyIndex++)
                            {
                                KeyState key = keyStates[keyIndex].Last().Value;
                                if (key.exists && key.colour == player.instruction.index && key.position.block.Equals(player.position.block) && key.holderIndex == -1)
                                {
                                    player.position = key.position;
                                    break;
                                }
                            }
                            break;
                        case Command.gotoTimeMachine:
                            player.position = level.TimeMachines[player.instruction.index];
                            break;
                        case Command.gotoGoal:
                            player.position = level.Goal;
                            break;
                    }

                    // 3.2.2.1 If the previous action was MOVE, merge it with the current action
                    while (playerStates[run].Count > 1)
                    {
                        if (initialState.instruction.command == Command.move && level.GetDoor(initialState.position.block, (char)initialState.instruction.index) != -1)
                            break;

                        if (!initialState.instruction.command.IsGotoCommand())
                            break;

                        //Work out the previous state of the player
                        KeyValuePair<float, PlayerState> previousPlayerState = playerStates[run].MostRecent(time, inclusive: false);

                        if (previousPlayerState.Value.instruction.command != Command.move)
                            break;

                        if (level.GetDoor(previousPlayerState.Value.position.block, (char)previousPlayerState.Value.instruction.index) != -1)
                            break;

                        //lineSide is a variable made by drawing a straight line from previousPlayerState to player, then comparing the line to initialState
                        //it is written strangely because the program is dealing with integers, and to avoid mixing > with >=
                        int lineSide = ((initialState.position.Y - player.position.Y) * (previousPlayerState.Value.position.X - player.position.X))
                            - ((initialState.position.X - player.position.X) * (previousPlayerState.Value.position.Y - player.position.Y));
                        //the inequality involves multiplying by a number that might be negative
                        if (previousPlayerState.Value.position.X - player.position.X < 0)
                            lineSide *= -1;
                        //the side we're looking for varies, depending on initialState
                        if (initialState.position.inner.Y == 0)
                            lineSide *= -1;

                        if (initialState.instruction.command == Command.move && lineSide < 0)
                            break;


                        //Remove the intialState
                        playerStates[run].Remove(time);
                        foreach (SortedList<float, KeyState> keyScript in keyStates)
                        {
                            KeyState key = keyScript.MostRecent(time).Value;
                            if (key.holderIndex == run)
                                keyScript.Remove(time);
                        }
                        time = previousPlayerState.Key;
                        initialState = previousPlayerState.Value;
                    }

                    // 3.2.2.2 Figure out how much time has been spent
                    time += initialState.position.DistanceFrom(player.position);

                    // 3.2.2.3 Interact with stuff
                    switch (player.instruction.command)
                    {
                        case Command.move:
                            int doorIndex = level.GetDoor(player.position.block, (char)player.instruction.index, opposite: true);
                            if (doorIndex != -1)
                                doorsUsed[doorIndex].Add(time);
                            break;
                        case Command.gotoPlate:
                            //this is in gotoPlate instead of waitPlate because if the instruction is waitplate then the code doesn't reach this point
                            waitPlates[player.instruction.index]++;
                            break;
                        case Command.takeKey:
                            time += 0.5f;
                            foreach (SortedList<float, KeyState> keyScript in keyStates)
                            {
                                KeyState key = keyScript.MostRecent(time).Value;
                                if (key.holderIndex == -1 && player.instruction.index == key.colour && player.position.Equals(key.position))
                                {
                                    key.holderIndex = run;
                                    keyScript[time] = key;
                                    break;
                                }
                            }
                            break;
                        case Command.unlockDoor:
                            doorsOpen[player.instruction.index][time] = true;
                            break;
                        case Command.lockDoor:
                            doorsOpen[player.instruction.index][time] = false;
                            break;
                        case Command.useKey:
                            time += 1;
                            for (int keyIndex = keyStates.Length - 1; keyIndex >= 0; keyIndex--)
                            {
                                KeyState key = keyStates[keyIndex].MostRecent(time).Value;
                                if (key.holderIndex == run && player.instruction.index == key.colour)
                                {
                                    key.exists = false;
                                    key.holderIndex = -1;
                                    keyStates[keyIndex][time] = key;
                                    break;
                                }
                            }
                            break;
                        case Command.dropKey:
                            time += 0.1f;
                            foreach (SortedList<float, KeyState> keyScript in keyStates)
                            {
                                KeyState key = keyScript.MostRecent(time).Value;
                                if (key.holderIndex == run && player.instruction.index == key.colour)
                                {
                                    key.holderIndex = -1;
                                    keyScript[time] = key;
                                    break;
                                }
                            }
                            break;
                        case Command.useTimeMachine:
                            time += 1;
                            player.exists = false;
                            break;
                        case Command.useGoal:
                            time += 1;
                            break;
                    }

                    // 3.2.2.4 Update the state and adjust the duration
                    RecordPlayerAndKeys();
                    if (duration < time)
                        duration = time;
                }
            }

            // 4 Add mannerisms (eg high fives)

            // todo

            // 5 Create a visual script

            // todo

            // 6 Output the resulting lists
            return new AnswerScript
            {
                duration = duration,
                player = playerStates,
                keys = keyStates,
                doorsOpen = doorsOpen,
                goalReached = goalReached,
                mistakes = mistakes,
            };
        }

        static KeyValuePair<float, T> MostRecent<T>(this SortedList<float, T> list, float time, bool inclusive = true)
        {
            if (inclusive)
                return list.Last(kvp => kvp.Key <= time);
            else
                return list.Last(kvp => kvp.Key < time);
        }
    }
}

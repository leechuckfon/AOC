using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
namespace AOCDay5 {
    class Program {
        #region declarations
        private static List<long> allInputs = new List<long>();
        private static Point robotPosition = new Point() { x = 0, y = 0 };
        private static List<Point> passedCoordinates = new List<Point>();
        private static long index = 0;
        private static bool running = true;
        private static long tracker = 0;
        private static bool walking;
        private static bool firstInput = true;
        private static string direction = "U";
        #endregion
        #region Main
        static void Main(string[] args) {
            calc();
            Console.ReadLine();
        }

        public class Point {
            public int x { get; set; }
            public int y { get; set; }
            public char color { get; set; }
        }
        #endregion
        #region Calculate
        private static void calc() {
            var lines = File.ReadAllLines("input.txt");
            allInputs = lines[0].Split(',').Select(numberAsString => Int64.Parse(numberAsString.ToString())).ToList();
            for (long j = 0; j < 10000; j++) {
                allInputs.Add(0);
            }
            walking = false;
            while (running) {
                var instruction = allInputs[(int)index];
                var instructionAsString = instruction.ToString();
                string opcode;
                if (instructionAsString.Length != 1) {
                    opcode = instructionAsString.Substring(instructionAsString.Length - 2, 2);
                }
                else {
                    opcode = instructionAsString;
                }
                List<long> modes;
                if (instructionAsString.Length != 1) {
                    modes = instructionAsString.Substring(0, instructionAsString.Length - 2).Reverse().Select(mode => Int64.Parse(mode.ToString())).ToList();
                    while (modes.Count() != 3) {
                        modes.Add(0);
                    }
                }
                else {
                    modes = new List<long>();
                    while (modes.Count() != 3) {
                        modes.Add(0);
                    }
                }
                switch (opcode.Replace("0", "")) {
                    case "1": opcode1(modes.ToArray()); break;
                    case "2": opcode2(modes.ToArray()); break;
                    case "3": opcode3(modes.ToArray()); break;
                    case "4": opcode4(modes.ToArray()); break;
                    case "5": opcode5(modes.ToArray()); break;
                    case "6": opcode6(modes.ToArray()); break;
                    case "7": opcode7(modes.ToArray()); break;
                    case "8": opcode8(modes.ToArray()); break;
                    case "9": opcode9(modes.ToArray()); break;
                    case "99": opcode99(); break;
                }
            }
        }
        #endregion
        #region helpers
        private static long[] GetParameters(long amountOfParameters) {
            List<long> numbers = new List<long>();
            for (long number = 1; number <= amountOfParameters; number++) {
                numbers.Add(allInputs[(int)(index + number)]);
            }
            return numbers.ToArray();
        }
        private static long[] ReplaceInputs(long[] inputs, long[] modes) {
            for (long i = 0; i < modes.Length - 1; i++) {
                if (i < inputs.Length) {
                    if (modes[i] == 2) {
                        var test = inputs[i];
                        inputs[i] = allInputs[(int)(tracker + test)];
                    }
                    else if (modes[i] == 0) {
                        long b = inputs[i];
                        inputs[i] = allInputs[(int)b];
                    }
                }
            }
            return inputs;
        }

        private static long[] ReplaceIndex(long[] inputs, long[] modes) {
            for (long i = 0; i < modes.Length; i++) {
                if (i < inputs.Length) {
                    if (modes[i] == 2) {
                        var test = inputs[i];
                        inputs[i] = tracker + test;
                    }
                    else if (modes[i] == 0) {
                        long b = inputs[i];
                        inputs[i] = b;
                    }
                }
            }
            return inputs;
        }

        #endregion
        #region opcodes
        private static void opcode1(long[] modes) {
            long[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            var toPutIn = inputs[0] + inputs[1];
            ReplaceIndex(inputs, modes);
            long a = inputs[2];
            allInputs[(int)a] = toPutIn;
        }
        private static void opcode2(long[] modes) {
            long[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            var toPutIn = inputs[0] * inputs[1];
            ReplaceIndex(inputs, modes);
            long a = inputs[2];
            allInputs[(int)a] = toPutIn;
        }
        private static void opcode3(long[] modes) {
            long numberInput = 0;
            if (firstInput) {
                numberInput = 0;
                firstInput = false;
            }
            else {
                var found = passedCoordinates.LastOrDefault(x => x.x == robotPosition.x && robotPosition.y == x.y);
                if (found != null) {
                    if (found.color == 'W') {
                        numberInput = 1;
                    }
                    else if (found.color == 'B') {
                        numberInput = 0;
                    }
                }
            }
            long[] parameters = GetParameters(1);
            ReplaceIndex(parameters, modes);
            index += 2;
            long a = parameters[0];
            allInputs[(int)a] = numberInput;
        }
        private static void opcode4(long[] modes) {
            long[] parameters = GetParameters(1);
            index += 2;
            ReplaceIndex(parameters, modes);
            long a = parameters[0];
            if (walking) {
                if (modes[0] == 1) {
                    //turn and move
                    if (a == 0) {
                        switch (direction) {
                            case "U": MoveLeftAndTurn(); break;
                            case "R": MoveUpAndTurn(); break;
                            case "D": MoveRightAndTurn(); break;
                            case "L": MoveDownAndTurn(); break;

                        }
                    }
                    else if (a == 1) {
                        switch (direction) {
                            case "U": MoveRightAndTurn(); break;
                            case "R": MoveDownAndTurn(); break;
                            case "D": MoveLeftAndTurn(); break;
                            case "L": MoveUpAndTurn(); break;

                        }
                    }
                }
                else {
                    //turn and move
                    if (allInputs[(int)a] == 0) {
                        switch (direction) {
                            case "U": MoveLeftAndTurn(); break;
                            case "R": MoveUpAndTurn(); break;
                            case "D": MoveRightAndTurn(); break;
                            case "L": MoveDownAndTurn(); break;

                        }
                    }
                    else if (allInputs[(int)a] == 1) {
                        switch (direction) {
                            case "U": MoveRightAndTurn(); break;
                            case "R": MoveDownAndTurn(); break;
                            case "D": MoveLeftAndTurn(); break;
                            case "L": MoveUpAndTurn(); break;

                        }
                    }
                }
                walking = false;
            }
            else {
                if (modes[0] == 1) {
                    if (a == 0) {
                        robotPosition.color = 'B';
                        passedCoordinates.Add(robotPosition);
                    }
                    else if (a == 1) {
                        robotPosition.color = 'W';
                        passedCoordinates.Add(robotPosition);
                    }
                }
                else {

                    // paint the coordinate
                    if (allInputs[(int)a] == 0) {
                        robotPosition.color = 'B';
                        passedCoordinates.Add(robotPosition);
                    }
                    else if (allInputs[(int)a] == 1) {
                        robotPosition.color = 'W';
                        passedCoordinates.Add(robotPosition);
                    }
                }
                walking = true;
            }
        }

        private static void MoveDownAndTurn() {
            robotPosition = new Point() { x = robotPosition.x, y = robotPosition.y - 1 };
            direction = "D";
        }

        private static void MoveRightAndTurn() {
            robotPosition = new Point() { x = robotPosition.x + 1, y = robotPosition.y };
            direction = "R";

        }

        private static void MoveUpAndTurn() {
            robotPosition = new Point() { x = robotPosition.x, y = robotPosition.y + 1 };
            direction = "U";

        }

        private static void MoveLeftAndTurn() {
            robotPosition = new Point() { x = robotPosition.x - 1, y = robotPosition.y };
            direction = "L";

        }

        private static void opcode9(long[] modes) {
            long[] parameters = GetParameters(1);
            index += 2;
            ReplaceInputs(parameters, modes);
            tracker += parameters[0];
        }

        private static void opcode5(long[] modes) {
            long[] inputs = GetParameters(2);
            ReplaceInputs(inputs, modes);
            if (inputs[0] != 0) {
                index = inputs[1];
            }
            else {
                index += 3;
            }
        }
        private static void opcode6(long[] modes) {
            long[] inputs = GetParameters(2);
            ReplaceInputs(inputs, modes);
            if (inputs[0] == 0) {
                index = inputs[1];
            }
            else {
                index += 3;
            }
        }
        private static void opcode7(long[] modes) {
            long[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            if (inputs[0] < inputs[1]) {
                ReplaceIndex(inputs, modes);
                long a = inputs[2];
                allInputs[(int)a] = 1;
            }
            else {
                ReplaceIndex(inputs, modes);
                long a = inputs[2];
                allInputs[(int)a] = 0;
            }
        }
        private static void opcode8(long[] modes) {
            //Als je wegschrijft doe dat op de juiste plaats, loemperik
            long[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            if (inputs[0] == inputs[1]) {
                inputs = ReplaceIndex(inputs, modes);
                long a = inputs[2];
                allInputs[(int)a] = 1;
            }
            else {
                ReplaceIndex(inputs, modes);
                long a = inputs[2];
                allInputs[(int)a] = 0;
            }
        }
        private static void opcode99() {
            running = false;
            Console.WriteLine(passedCoordinates.Distinct(new PComparer()).Count());
            for (int i = passedCoordinates.Select(x => x.y).Max(); i >= passedCoordinates.Select(x => x.y).Min(); i--) {
                var toWrite = passedCoordinates.Where(x => x.y == i);
                for (int j = passedCoordinates.Select(x => x.x).Min(); j <= passedCoordinates.Select(x => x.x).Max(); j++) {
                    if (toWrite.LastOrDefault(x => x.x == j) is null) {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write('B');
                    }
                    else {
                        if (toWrite.LastOrDefault(x => x.x == j).color == 'B') {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write(toWrite.LastOrDefault(x => x.x == j).color);
                    }
                }
                //toWrite.LastOrDefault(x => x.x == i);
                //foreach (var thing in toWrite) {
                //    Console.Write(thing.color);
                //}
                Console.WriteLine();
            }
        }
        #endregion
    }

    internal class PointComparer : IEqualityComparer<KeyValuePair<Program.Point, char>> {
        public bool Equals([AllowNull] KeyValuePair<Program.Point, char> x, [AllowNull] KeyValuePair<Program.Point, char> y) {
            return x.Key.x == y.Key.x && x.Key.y == y.Key.y;
        }

        public int GetHashCode([DisallowNull] KeyValuePair<Program.Point, char> obj) {
            return obj.Key.x + obj.Key.y;
        }
    }
}

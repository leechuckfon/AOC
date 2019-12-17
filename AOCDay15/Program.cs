using AOCDay15;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using static AOCDay15.DSF;

namespace AOCDay5 {
    class Program {
        #region declarations
        private static List<long> allInputs = new List<long>();
        private static Point robotPosition = new Point() { x = 21, y = 21 };
        private static List<Point> passedCoordinates = new List<Point>() { new Point() { x = 21, y = 21, parent = null, type = 9 } };
        private static long index = 0;
        private static bool running = true;
        private static long tracker = 0;
        private static bool xPara;
        private static bool yPara;
        private static bool tId;
        private static long xVal;
        private static Stack<Point> q = new System.Collections.Generic.Stack<Point>() {
        };
        private static long yVal;
        private static long tIdVal;
        private static string direction;
        private static bool firstInput;
        private static bool backtrack = false;
        private static int numberInput = 0;

        #endregion
        #region Main
        static void Main(string[] args) {
            //calc();
            DSF sF = new DSF();
            var s = sF.getPathBFS(37,9);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            var i = 0;
            while (s.getParent() != null) {
                i++;
                s = s.getParent();
            }
            Console.WriteLine(i +1 /* because it doesn't count where it ends */);
            Console.ReadLine();
        }

        public class Point : ICloneable {
            public long x { get; set; }
            public long y { get; set; }
            public long type { get; set; }
            public Point parent { get; set; }
            public string direction { get; set; }
            public bool backtracking { get; set; }

            public Point Up(bool tracking) {
                return new Point() {
                    x = x,
                    y = y + 1,
                    type = 999,
                    parent = this,
                    direction = "U",
                    backtracking = tracking

                };
            }
            public Point Down(bool tracking) {
                return new Point() {
                    x = x,
                    y = y - 1,
                    type = 999,
                    parent = this,
                    direction = "D",
                    backtracking = tracking


                };
            }
            public Point Left(bool tracking) {
                return new Point() {
                    x = x - 1,
                    y = y,
                    type = 999,
                    parent = this,
                    direction = "L",
                    backtracking = tracking


                };
            }
            public Point Right(bool tracking) {
                return new Point() {
                    x = x + 1,
                    y = y,
                    type = 999,
                    parent = this,
                    direction = "R",
                    backtracking = tracking


                };
            }

            public object Clone() {
                return new Point() {
                    direction = direction,
                    parent = parent,
                    x = x,
                    y = y
                };
            }
        }
        #endregion
        #region Calculate
        private static void calc() {
            var lines = File.ReadAllLines("input.txt");
            allInputs = lines[0].Split(',').Select(numberAsString => Int64.Parse(numberAsString.ToString())).ToList();
            for (long j = 0; j < 10000; j++) {
                allInputs.Add(0);
            }
            xPara = true;
            yPara = false;
            firstInput = true;
            tId = false;
            q.Push(new Point() { x = 1, y = 0, direction = "R" });
            q.Push(new Point() { x = -1, y = 0, direction = "L" });
            q.Push(new Point() { x = 0, y = 1, direction = "U" });
            q.Push(new Point() { x = 0, y = -1, direction = "D" });
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
            Point p = new Point();
            try {
                p = q.Pop();
            } catch (InvalidOperationException e) {
                Print();
            }
            switch (p.direction) {
                case "U": numberInput = 1; break;
                case "D": numberInput = 2; break;
                case "L": numberInput = 3; break;
                case "R": numberInput = 4; break;
            }
            robotPosition.backtracking = p.backtracking;
            ChangeDirection(numberInput);
            long[] parameters = GetParameters(1);
            ReplaceIndex(parameters, modes);
            index += 2;
            long a = parameters[0];
            allInputs[(int)a] = numberInput;
        }

        private static void ChangeDirection(int numberInput) {
            switch (numberInput) {
                case 1: direction = "U"; break;
                case 2: direction = "D"; break;
                case 3: direction = "L"; break;
                case 4: direction = "R"; break;
            }
        }

        private static void opcode4(long[] modes) {
            long[] parameters = GetParameters(1);
            index += 2;
            ReplaceIndex(parameters, modes);
            long a = parameters[0];
            if (modes[0] == 1) {
                xVal = a;
            }
            else {
                xVal = allInputs[(int)a];
            }
            switch (xVal) {
                case 0: addWall(); break;
                case 1: Free(); break;
                case 2: Oxygen(); break;
            }
        }

        private static void Free() {
            var arrivedPosition = new Point() {
                y = robotPosition.y,
                type = 1,
                x = robotPosition.x,
                parent = (Point)robotPosition.Clone(),
                backtracking = robotPosition.backtracking
            };

            switch (direction) {
                case "U": arrivedPosition.y++; if (!arrivedPosition.backtracking) q.Push(arrivedPosition.Down(true)); break;
                case "D": arrivedPosition.y--; if (!arrivedPosition.backtracking) q.Push(arrivedPosition.Up(true)); break;
                case "R": arrivedPosition.x++; if (!arrivedPosition.backtracking) q.Push(arrivedPosition.Left(true)); break;
                case "L": arrivedPosition.x--; if (!arrivedPosition.backtracking) q.Push(arrivedPosition.Right(true)); break;
            }
            passedCoordinates.Add(arrivedPosition);
            if (passedCoordinates.Find(x => x.x == arrivedPosition.x && x.y == arrivedPosition.y + 1) is null) {
                var test = arrivedPosition.Up(false);
                q.Push(test);
            }
            if (passedCoordinates.Find(x => x.x == arrivedPosition.x && x.y == arrivedPosition.y - 1) is null) {
                q.Push(arrivedPosition.Down(false));
            }
            if (passedCoordinates.Find(x => x.x == arrivedPosition.x - 1 && x.y == arrivedPosition.y) is null) {
                q.Push(arrivedPosition.Left(false));
            }
            if (passedCoordinates.Find(x => x.x == arrivedPosition.x + 1 && x.y == arrivedPosition.y) is null) {
                q.Push(arrivedPosition.Right(false));
            }

            robotPosition.x = arrivedPosition.x;
            robotPosition.y = arrivedPosition.y;
            robotPosition.backtracking = arrivedPosition.backtracking;
            robotPosition.parent = (Point)robotPosition.Clone();
        }

        private static void addWall() {
            var arrivedPosition = new Point() {
                y = robotPosition.y,
                type = 0,
                x = robotPosition.x,
                parent = (Point)robotPosition.Clone(),
                backtracking = robotPosition.backtracking
            };

            switch (direction) {
                case "U": arrivedPosition.y++; break;
                case "D": arrivedPosition.y--; break;
                case "R": arrivedPosition.x++; break;
                case "L": arrivedPosition.x--; break;
            }
            passedCoordinates.Add(arrivedPosition);
        }

        private static void Oxygen() {
            var arrivedPosition = new Point() {
                y = robotPosition.y,
                type = 2,
                x = robotPosition.x,
                parent = (Point)robotPosition.Clone(),
                backtracking = robotPosition.backtracking
        };
                            Print();

            switch (direction) {
                case "U": arrivedPosition.y++; if (!arrivedPosition.backtracking) q.Push(arrivedPosition.Down(true)); break;
                case "D": arrivedPosition.y--; if (!arrivedPosition.backtracking) q.Push(arrivedPosition.Up(true)); break;
                case "R": arrivedPosition.x++; if (!arrivedPosition.backtracking) q.Push(arrivedPosition.Left(true)); break;
                case "L": arrivedPosition.x--; if (!arrivedPosition.backtracking) q.Push(arrivedPosition.Right(true)); break;
            }
            passedCoordinates.Add(arrivedPosition);
            //var p = arrivedPosition;
            //var i = 0;
            //while (p.parent != null) {
            //    i++;
            //    p = p.parent;
            //}
            //Console.WriteLine(i);
            Console.Write(arrivedPosition.x + " " + arrivedPosition.y);
        }

        private static void PrintMaze() {
            Print();
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


        }

        private static void Print() {
            Console.Clear();
            for (long i = passedCoordinates.Select(x => x.y).Max(); i > passedCoordinates.Select(x => x.y).Min(); i--) {
                var toWrite = passedCoordinates.Where(x => x.y == i);
                for (long j = passedCoordinates.Select(x => x.x).Min(); j < passedCoordinates.Select(x => x.x).Max(); j++) {
                    var tosomething = toWrite.Where(x => x.x == j).FirstOrDefault();
                    if (tosomething != null) {
                        if (toWrite.Where(x => x.x == j).First().x == robotPosition.x && robotPosition.y == toWrite.Where(x => x.y == i).First().y) {
                            Console.Write("D");
                        }
                        else {
                            
                            if (toWrite.Where(x => x.x == j).First().type == 1) {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.Write(1);
                            } else if  (toWrite.Where(x => x.x == j).First().type == 0) {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.BackgroundColor = ConsoleColor.Blue;

                                Console.Write(0);
                            } else if (toWrite.Where(x => x.x == j).First().type == 9) {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.Write(9);
                            }
                        }

                    }
                    else {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
            }
        }
    }
    #endregion
}


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
        private static long x=0;
        private static long y=0;
        private static long xVal;
        #endregion
        #region Main
        static void Main(string[] args) {
            calc();
            Console.ReadLine();
        }

        public class Point {
            public long x { get; set; }
            public long y { get; set; }
            public string type { get; set; }
        }
        #endregion
        #region Calculate
        private static void calc() {
            var lines = File.ReadAllLines("input.txt");
            allInputs = lines[0].Split(',').Select(numberAsString => Int64.Parse(numberAsString.ToString())).ToList();
            for (long j = 0; j < 10000; j++) {
                allInputs.Add(0);
            }
            allInputs[0] = 2;
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
            var numberInput = 0;
            //if (passedCoordinates.Where(x => x.type == 4).Last().x < passedCoordinates.Where(x => x.type == 3).Last().x) {
            //    numberInput -= 1;
            //}
            //else if (passedCoordinates.Where(x => x.type == 4).Last().x > passedCoordinates.Where(x => x.type == 3).Last().x) {
            //    numberInput += 1;
            //}
            Print();
            numberInput = Int32.Parse(Console.ReadLine());
            long[] parameters = GetParameters(1);
            ReplaceIndex(parameters, modes);
            index += 2;
            long a = parameters[0];
            allInputs[(int)a] = numberInput;
            //Print();
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

            switch (a) {
                case 10: AddNL(); break;
                case 46: AddDot(); break;
                case 35: AddScaff(); break;
            }

        }

        private static void AddNL() {
            y++;
        }

        private static void AddDot() {
            passedCoordinates.Add(new Point() {
                x = x,
                y = y,
                type = "."
            });
            x++;
        }

        private static void AddScaff() {
            passedCoordinates.Add(new Point() {
                x = x,
                y = y,
                type = "#"
            });
            x++;
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
            for (long i = passedCoordinates.Select(x => x.y).Min(); i < passedCoordinates.Select(x => x.y).Max(); i++) {
                var toWrite = passedCoordinates.Where(x => x.y == i);
                for (long j = 0; j < passedCoordinates.Select(x => x.x).Max(); j++) {
                    Console.Write(toWrite.Where(x => x.x == j).Last().type);
                }
                Console.Write("\n");
            }
            //Console.WriteLine(passedCoordinates.Where(x => x.type == 4).Last().x + " " + passedCoordinates.Where(x => x.type == 4).Last().y);
        }
    }
    #endregion
}


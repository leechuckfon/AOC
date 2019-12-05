using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOCDay5 {
    class Program {
        private static int[] allInputs = new int[999999];
        private static int index = 0;
        private static bool running = true;
        static void Main(string[] args) {
            calc();
            Console.ReadLine();
        }

        private static void calc() {
            var lines = File.ReadAllLines("input.txt");
            allInputs = lines[0].Split(',').Select(numberAsString => Int32.Parse(numberAsString.ToString())).ToArray();
            while (running) {
                //allInputs[1] = 12;
                //allInputs[2] = 2;
                var instruction = allInputs[index];
                var instructionAsString = instruction.ToString();
                string opcode;
                if (instructionAsString.Length != 1) { 
                    opcode = instructionAsString.Substring(instructionAsString.Length - 2, 2);
                } else {
                    opcode = instructionAsString;
                }
                List<int> modes;
                if (instructionAsString.Length != 1) {
                    modes = instructionAsString.Substring(0, instructionAsString.Length - 2).Reverse().Select(mode => Int32.Parse(mode.ToString())).ToList();
                while (modes.Count() != 3) {
                    modes.Add(0);
                }
                } else {
                    modes = new List<int>();
                    while (modes.Count() != 3) {
                        modes.Add(0);
                    }
                }
                switch (opcode.Replace("0","")) {
                    case "1": opcode1(modes.ToArray()); break;
                    case "2": opcode2(modes.ToArray()); break;
                    case "3": opcode3(); break;
                    case "4": opcode4(); break;
                    case "5": opcode5(modes.ToArray()); break;
                    case "6": opcode6(modes.ToArray()); break;
                    case "7": opcode7(modes.ToArray()); break;
                    case "8": opcode8(modes.ToArray()); break;
                    case "99": opcode99(); break;
                }
            }
            
        }

        private static int[] GetParameters(int amountOfParameters) {
            List<int> numbers = new List<int>();
            for (int number= 1;number <= amountOfParameters; number++) {
                numbers.Add(allInputs[index + number]);
            }
            return numbers.ToArray();
        }

        private static void opcode1(int[] modes) {
            int[] inputs = GetParameters(3);
            index += 4;

            for (int i=0;i<modes.Length-1; i++) {
                if (modes[i] == 1) {
                    
                } else if (modes[i] == 0) {
                    int b = inputs[i];
                    inputs[i] = allInputs[b];
                }
            }
            var toPutIn = inputs[0] + inputs[1];
            int a = inputs[2];
            allInputs[a] = toPutIn;
        }
        private static void opcode2(int[] modes) {
            int[] inputs = GetParameters(3);
            index += 4;

            for (int i = 0; i < modes.Length-1; i++) {
                if (modes[i] == 1) {
                    
                } else 
                if (modes[i] == 0) {
                    int b = inputs[i];
                    inputs[i] = allInputs[b];
                }
            }
            var toPutIn = inputs[0] * inputs[1];
            int a = inputs[2];
            allInputs[a] = toPutIn;
        }
        private static void opcode99() {
            running = false;
        }
        private static void opcode3() {
            Console.WriteLine("Write a god damn number!");
            int numberInput = Int32.Parse(Console.ReadLine().ToString());
            int[] parameters = GetParameters(1);
            index += 2;
            int a = parameters[0];
            allInputs[a] = numberInput;
        }
        private static void opcode4() {
            int[] parameters = GetParameters(1);
            index += 2;
            int a = parameters[0];
            Console.WriteLine(allInputs[a]);
        }

        private static void opcode5(int[] modes) {
            int[] inputs = GetParameters(2);

            for (int i = 0; i < modes.Length - 1; i++) {
                if (modes[i] == 1) {

                }
                else
                if (modes[i] == 0) {
                    int b = inputs[i];
                    inputs[i] = allInputs[b];
                }
            }

            if (inputs[0] != 0) {
                index = inputs[1];
            } else {
                index += 3;
            }
        }
        private static void opcode6(int[] modes) {
            int[] inputs = GetParameters(2);

            for (int i = 0; i < modes.Length - 1; i++) {
                if (modes[i] == 1) {

                }
                else
                if (modes[i] == 0) {
                    int b = inputs[i];
                    inputs[i] = allInputs[b];
                }
            }

            if (inputs[0] == 0) {
                index = inputs[1];
            }
            else {
                index += 3;
            }
        }
        private static void opcode7(int[] modes) {
            int[] inputs = GetParameters(3);
            index += 4;

            for (int i = 0; i < modes.Length - 1; i++) {
                if (modes[i] == 1) {

                }
                else
                if (modes[i] == 0) {
                    int b = inputs[i];
                    inputs[i] = allInputs[b];
                }
            }

            if (inputs[0] < inputs[1]) {
                int a = inputs[2];
                allInputs[a] = 1;
            }
            else {
                int a = inputs[2];
                allInputs[a] = 0;
            }
        }
        private static void opcode8(int[] modes) {
            int[] inputs = GetParameters(3);
            index += 4;

            for (int i = 0; i < modes.Length - 1; i++) {
                if (modes[i] == 1) {

                }
                else
                if (modes[i] == 0) {
                    int b = inputs[i];
                    inputs[i] = allInputs[b];
                }
            }

            if (inputs[0] == inputs[1]) {
                int a = inputs[2];
                allInputs[a] = 1;
            }
            else {
                int a = inputs[2];
                allInputs[a] = 0;
            }
        }
    }
}

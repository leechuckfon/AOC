using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOCDay7 {
    class Amp {
        #region declarations
        private  int[] allInputs = new int[999999];
        private  int index = 0;
        private  List<int> firstInputs = new List<int>();
        private  bool running = true;
        private  bool isFirstInput = true;
        private  int amplifierInput = 0;
        private  List<int> allOutputs = new List<int>();
        #endregion

        public Amp() {
            var lines = File.ReadAllLines("input.txt");
            allInputs = lines[0].Split(',').Select(numberAsString => Int32.Parse(numberAsString.ToString())).ToArray();
        }

        #region calc
        public  AmpState calc(int firstInput, int ampInput, int indexState, int[] vs) {
            running = true;
            if (vs != null) {
                allInputs = vs;
            }
            index = indexState;
            amplifierInput = ampInput;
            isFirstInput = true;
            while (running) {
                var instruction = allInputs[index];
                var instructionAsString = instruction.ToString();
                string opcode;
                if (instructionAsString.Length != 1) {
                    opcode = instructionAsString.Substring(instructionAsString.Length - 2, 2);
                }
                else {
                    opcode = instructionAsString;
                }
                List<int> modes;
                if (instructionAsString.Length != 1) {
                    modes = instructionAsString.Substring(0, instructionAsString.Length - 2).Reverse().Select(mode => Int32.Parse(mode.ToString())).ToList();
                    while (modes.Count() != 3) {
                        modes.Add(0);
                    }
                }
                else {
                    modes = new List<int>();
                    while (modes.Count() != 3) {
                        modes.Add(0);
                    }
                }
                switch (opcode.Replace("0", "")) {
                    case "1": opcode1(modes.ToArray()); break;
                    case "2": opcode2(modes.ToArray()); break;
                    case "3": opcode3(firstInput); break;
                    case "4": return opcode4(); break;
                    case "5": opcode5(modes.ToArray()); break;
                    case "6": opcode6(modes.ToArray()); break;
                    case "7": opcode7(modes.ToArray()); break;
                    case "8": opcode8(modes.ToArray()); break;
                    case "99": return opcode99(); break;
                }
            }
            return null;
        }
#endregion
        #region helpers
        private  int[] GetParameters(int amountOfParameters) {
            List<int> numbers = new List<int>();
            for (int number = 1; number <= amountOfParameters; number++) {
                numbers.Add(allInputs[index + number]);
            }
            return numbers.ToArray();
        }
        private  int[] ReplaceInputs(int[] inputs, int[] modes) {
            for (int i = 0; i < modes.Length - 1; i++) {
                if (modes[i] == 1) {
                }
                else if (modes[i] == 0) {
                    int b = inputs[i];
                    inputs[i] = allInputs[b];
                }
            }
            return inputs;
        }
        #endregion
        #region opcodes
        private  void opcode1(int[] modes) {
            int[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            var toPutIn = inputs[0] + inputs[1];
            int a = inputs[2];
            allInputs[a] = toPutIn;
        }
        private  void opcode2(int[] modes) {
            int[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            var toPutIn = inputs[0] * inputs[1];
            int a = inputs[2];
            allInputs[a] = toPutIn;
        }
        private  void opcode3(int firstInput) {
            int numberInput;
            if (isFirstInput) {
                numberInput = firstInput;
                isFirstInput = false;
            }
            else {
                numberInput = amplifierInput;
            }
            int[] parameters = GetParameters(1);
            index += 2;
            int a = parameters[0];
            allInputs[a] = numberInput;
        }
        private  AmpState opcode4() {
            int[] parameters = GetParameters(1);
            index += 2;
            int a = parameters[0];
            amplifierInput = allInputs[a];
            Console.WriteLine(allInputs[a]);
            return new AmpState() {
                Output = amplifierInput,
                Paused = true,
                Stopped = false,
                StateIndex = index,
                yas = allInputs

            };
        }
        private  void opcode5(int[] modes) {
            int[] inputs = GetParameters(2);
            ReplaceInputs(inputs, modes);
            if (inputs[0] != 0) {
                index = inputs[1];
            }
            else {
                index += 3;
            }
        }
        private  void opcode6(int[] modes) {
            int[] inputs = GetParameters(2);
            ReplaceInputs(inputs, modes);
            if (inputs[0] == 0) {
                index = inputs[1];
            }
            else {
                index += 3;
            }
        }
        private  void opcode7(int[] modes) {
            int[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            if (inputs[0] < inputs[1]) {
                int a = inputs[2];
                allInputs[a] = 1;
            }
            else {
                int a = inputs[2];
                allInputs[a] = 0;
            }
        }
        private  void opcode8(int[] modes) {
            int[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            if (inputs[0] == inputs[1]) {
                int a = inputs[2];
                allInputs[a] = 1;
            }
            else {
                int a = inputs[2];
                allInputs[a] = 0;
            }
        }
        private  AmpState opcode99() {
            running = false;
            return new AmpState() {
                Output = amplifierInput,
                Paused = false,
                Stopped = true,
                StateIndex = index,
                yas = allInputs
            };
        }
        #endregion
    }

    public class AmpState {
        public bool Paused { get; set; }
        public bool Stopped { get; set; }
        public int[] yas { get; set; }
        public int Output { get; set; }
        public int StateIndex { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day6 {
    public class AMP {

        #region declarations
        private int[] allInputs = new int[999999];
        private int index;
        private bool running = true;
        private bool _isFirstRun = true;
        private int input = 0;
        private int _permNumber = 0;
        private int lastOutput = 0;
        #endregion
        #region Main
        public AMP() {
            var lines = File.ReadAllLines("input.txt");
            allInputs = lines[0].Split(',').Select(numberAsString => Int32.Parse(numberAsString.ToString())).ToArray();
            index = 0;
        }
        #endregion
        #region Calculate
        public AmpState calc(bool IsFirstRun, int? pointer, int[] inputState, int outputFromLast, int permNumber) {
            _isFirstRun = IsFirstRun;
            if (inputState != null) {
                allInputs = inputState;
            }

            if (pointer.HasValue) {
                index = pointer.Value;
            } else {
                index = 0;
            }
            _permNumber = permNumber;
            input = outputFromLast;

            running = true;
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
                    case "3": opcode3(); break;
                    case "4": return opcode4();
                    case "5": opcode5(modes.ToArray()); break;
                    case "6": opcode6(modes.ToArray()); break;
                    case "7": opcode7(modes.ToArray()); break;
                    case "8": opcode8(modes.ToArray()); break;
                    case "99": return opcode99();
                }
            }
            return null;
        }
        #endregion
        #region helpers
        private int[] GetParameters(int amountOfParameters) {
            List<int> numbers = new List<int>();
            for (int number = 1; number <= amountOfParameters; number++) {
                numbers.Add(allInputs[index + number]);
            }
            return numbers.ToArray();
        }
        private int[] ReplaceInputs(int[] inputs, int[] modes) {
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
        private void opcode1(int[] modes) {
            int[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            var toPutIn = inputs[0] + inputs[1];
            int a = inputs[2];
            allInputs[a] = toPutIn;
        }
        private void opcode2(int[] modes) {
            int[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            var toPutIn = inputs[0] * inputs[1];
            int a = inputs[2];
            allInputs[a] = toPutIn;
        }
        private void opcode3() {
            //Console.WriteLine("Write a god damn number!");
            //int numberInput = Int32.Parse(Console.ReadLine().ToString());
            var numberInput = 0;
            if (_isFirstRun) {
                numberInput = _permNumber;
                _isFirstRun = false;
            }
            else {
                numberInput = input;
            }
            int[] parameters = GetParameters(1);
            index += 2;
            int a = parameters[0];
            allInputs[a] = numberInput;
        }
        private AmpState opcode4() {
            int[] parameters = GetParameters(1);
            index += 2;
            int a = parameters[0];
            lastOutput = allInputs[a];
            Console.WriteLine(allInputs[a]);
            return new AmpState {
                IndexState = index,
                MemoryState = allInputs,
                Output = lastOutput,
                Paused = true,
                Stopped = false
            };
        }
        private void opcode5(int[] modes) {
            int[] inputs = GetParameters(2);
            ReplaceInputs(inputs, modes);
            if (inputs[0] != 0) {
                index = inputs[1];
            }
            else {
                index += 3;
            }
        }
        private void opcode6(int[] modes) {
            int[] inputs = GetParameters(2);
            ReplaceInputs(inputs, modes);
            if (inputs[0] == 0) {
                index = inputs[1];
            }
            else {
                index += 3;
            }
        }
        private void opcode7(int[] modes) {
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
        private void opcode8(int[] modes) {
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
        private AmpState opcode99() {
            running = false;
            return new AmpState {
                IndexState = index,
                MemoryState = allInputs,
                Output = lastOutput,
                Paused = false,
                Stopped = true
            };
        }
        #endregion
    }
}

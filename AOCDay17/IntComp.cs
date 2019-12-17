using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static AOCDay5.Program;

namespace AOCDay17 {
    public class IntComp {
        private List<long> allInputs;
        private List<int> QueuedInput;
        private Point robotPosition;
        private List<Point> passedCoordinates;
        private long index;
        private bool running;
        private long tracker;
        private long xVal;
        private long sum;
        private bool moving;
        public Queue<int> input;
        public Queue<long> output;
        public event EventHandler queuedEvent;
        public event EventHandler needInput;

        public IntComp() {
            input = new Queue<int>();
            output = new Queue<long>();
            tracker = 0;
            running = true;
            sum = 0;
            moving = false;
            allInputs = new List<long>();
            robotPosition = new Point() { x = 0, y = 0 };
            passedCoordinates = new List<Point>();
        }



        #region Calculate
        public void calc(bool enableInput) {
            InitMemory(enableInput);
            QueuedInput = new List<int>();
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

        private void InitMemory(bool enableInput) {
            var lines = File.ReadAllLines("input.txt");
            allInputs = lines[0].Split(',').Select(numberAsString => Int64.Parse(numberAsString.ToString())).ToList();
            moving = false;
            for (long j = 0; j < 10000; j++) {
                allInputs.Add(0);
            }
            if (enableInput) {
                allInputs[0] = 2;
            }

        }
        #endregion
        #region helpers
        private long[] GetParameters(long amountOfParameters) {
            List<long> numbers = new List<long>();
            for (long number = 1; number <= amountOfParameters; number++) {
                numbers.Add(allInputs[(int)(index + number)]);
            }
            return numbers.ToArray();
        }
        private long[] ReplaceInputs(long[] inputs, long[] modes) {
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

        private long[] ReplaceIndex(long[] inputs, long[] modes) {
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
        private void opcode1(long[] modes) {
            long[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            var toPutIn = inputs[0] + inputs[1];
            ReplaceIndex(inputs, modes);
            long a = inputs[2];
            allInputs[(int)a] = toPutIn;
        }
        private void opcode2(long[] modes) {
            long[] inputs = GetParameters(3);
            index += 4;
            ReplaceInputs(inputs, modes);
            var toPutIn = inputs[0] * inputs[1];
            ReplaceIndex(inputs, modes);
            long a = inputs[2];
            allInputs[(int)a] = toPutIn;
        }
        private void opcode3(long[] modes) {
            needInput?.Invoke(this, null);
            while (input.Count == 0) {

            }
            int numberInput = input.Dequeue();
            long[] parameters = GetParameters(1);
            ReplaceIndex(parameters, modes);
            index += 2;
            moving = true;
            long a = parameters[0];
            allInputs[(int)a] = numberInput;
        }
        private void opcode4(long[] modes) {
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
            queuedEvent?.Invoke(this, new QueuedEventArgs() { value = xVal });

        }

        private void opcode9(long[] modes) {
            long[] parameters = GetParameters(1);
            index += 2;
            ReplaceInputs(parameters, modes);
            tracker += parameters[0];
        }

        private void opcode5(long[] modes) {
            long[] inputs = GetParameters(2);
            ReplaceInputs(inputs, modes);
            if (inputs[0] != 0) {
                index = inputs[1];
            }
            else {
                index += 3;
            }
        }
        private void opcode6(long[] modes) {
            long[] inputs = GetParameters(2);
            ReplaceInputs(inputs, modes);
            if (inputs[0] == 0) {
                index = inputs[1];
            }
            else {
                index += 3;
            }
        }
        private void opcode7(long[] modes) {
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
        private void opcode8(long[] modes) {
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
        private void opcode99() {
            running = false;


        }
    }
    #endregion
}


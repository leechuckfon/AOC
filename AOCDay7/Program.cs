using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOCDay7 {
    class Program {
        #region declarations
        private static int[] allInputs = new int[999999];
        private static Dictionary<string, int> inputStates;
        private static Dictionary<string, int[]> inputStates1;
        private static int index = 0;
        private static List<int> firstInputs = new List<int>();
        private static bool running = true;
        private static bool isFirstInput = true;
        private static int amplifierInput = 0;
        private static List<int> allOutputs = new List<int>();
        private static Amp a;
        private static Amp b;
        private static Amp c;
        private static Amp d;
        private static Amp e;
        #endregion

        static void Main(string[] args) {
            int[] numbers = new[]
            {
                5,6,7,8,9
            };

            Permute(numbers, Output);

            //    amplifierInput = 0;
            for (int combination = 0; combination < firstInputs.Count; combination+=5) {
                inputStates = new Dictionary<string, int>();
                inputStates1 = new Dictionary<string, int[]>();
                inputStates1.Add("a", null);
                inputStates1.Add("b", null);
                inputStates1.Add("e", null);
                inputStates1.Add("c", null);
                inputStates1.Add("d", null);
                inputStates.Add("a", 0);
                inputStates.Add("b", 0);
                inputStates.Add("e", 0);
                inputStates.Add("c", 0);
                inputStates.Add("d", 0);
                a = new Amp();
                b = new Amp();
                c = new Amp();
                d = new Amp();
                e = new Amp();
                //    // LOOP
                //    for (int i = 0; i < 5; i++) {
                //        calc(firstInputs[i+combination]);
                //     }
                //    allOutputs.Add(amplifierInput);
                //b.calc(combination+1);
                doEverythingAgain(firstInputs[combination], firstInputs[1 + combination], firstInputs[2 + combination], firstInputs[3 + combination], firstInputs[4 + combination]);
                //c.calc(combination+2);
                //d.calc(combination+3);
                //e.calc(combination+4);
            }
            Console.WriteLine(allOutputs.Max());
            
        }
        private static int globalOutput = 0;
        private static bool doEverythingAgain(int c1, int c2, int c3, int c4, int c5) {

            var state = a.calc(c1, globalOutput, inputStates["a"], inputStates1["a"]);
            if (state.Paused || state.Stopped) {
                inputStates["a"] = state.StateIndex;
                inputStates1["a"] = state.yas;
                globalOutput = state.Output;
                state = b.calc(c2, globalOutput, inputStates["b"], inputStates1["b"]);
            }
            if (state.Paused || state.Stopped) {
                inputStates["b"] = state.StateIndex;
                inputStates1["b"] = state.yas;
                globalOutput = state.Output;
                state = c.calc(c3, globalOutput, inputStates["c"], inputStates1["c"]);
            }
            if (state.Paused || state.Stopped) {
                inputStates["c"] = state.StateIndex;
                inputStates1["c"] = state.yas;
                globalOutput = state.Output;
                state = d.calc(c4, globalOutput, inputStates["d"], inputStates1["d"]);
            }
            if (state.Paused || state.Stopped) {
                inputStates["d"] = state.StateIndex;
                inputStates1["d"] = state.yas;
                globalOutput = state.Output;
                state = e.calc(c5, globalOutput, inputStates["e"], inputStates1["e"]);
            }
            if (state.Stopped) {
                allOutputs.Add(state.Output);
                globalOutput = 0;
                return true;
            }
            if (state.Paused) {
                inputStates["e"] = state.StateIndex;
                inputStates1["e"] = state.yas;
                globalOutput = state.Output;
                return doEverythingAgain(c1, c2, c3, c4, c5);
            }
            return false;
        }

        #region perm

        private static void Output(int[] permutation) {
            //StringBuilder sb = new StringBuilder();
            foreach (int item in permutation) {
                firstInputs.Add(item);
            }
            //firstInputs.Add(Int32.Parse(sb.ToString()));
        }

        public static void Permute(int[] items, Action<int[]> output) {
            Permute(items, 0, new int[items.Length], new bool[items.Length], output);
        }

        private static void Permute(int[] items, int item, int[] permutation, bool[] used, Action<int[]> output) {
            for (int i = 0; i < items.Length; ++i) {
                if (!used[i]) {
                    used[i] = true;
                    permutation[item] = items[i];

                    if (item < (items.Length - 1)) {
                        Permute(items, item + 1, permutation, used, output);
                    }
                    else {
                        output(permutation);
                    }

                    used[i] = false;
                }
            }
        }
        #endregion
    }
}

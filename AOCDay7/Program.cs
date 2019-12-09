using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6 {
    class Program {
        private static List<int> combinations = new List<int>();
        private static List<int[]> memoryStates = new List<int[]>();
        private static List<int?> indexStates = new List<int?>();
        private static List<int> allOutputs = new List<int>();
        private static AMP a;
        private static AMP b;
        private static AMP c;
        private static AMP d;
        private static AMP e;
        static void Main(string[] args) {
            int[] numbers = new[]
            {
                0,1,2,3,4
            };

            Permute(numbers, Output);
            for (int comb = 0; comb < combinations.Count; comb+=5) {
                indexStates.Add(null);
                indexStates.Add(null);
                indexStates.Add(null);
                indexStates.Add(null);
                indexStates.Add(null);
                memoryStates = new List<int[]>();
                memoryStates.Add(null);
                memoryStates.Add(null);
                memoryStates.Add(null);
                memoryStates.Add(null);
                memoryStates.Add(null);
                a = new AMP();
                b = new AMP();
                c = new AMP();
                d = new AMP();
                e = new AMP();
                var result = repeatDis(true, comb, 0);
                allOutputs.Add(result.Output);
            }

            Console.Write(allOutputs.Max());
        }

        private static AmpState repeatDis(bool firstRun,int comb, int lastOutput) {
            var result = a.calc(firstRun, indexStates[0], memoryStates[0], lastOutput, combinations[comb]);
            SaveOrClear(0, result);
            result = b.calc(firstRun, indexStates[1], memoryStates[1], result.Output, combinations[comb + 1]);
            SaveOrClear(1, result);
            result = c.calc(firstRun, indexStates[2], memoryStates[2], result.Output, combinations[comb + 2]);
            SaveOrClear(2, result);
            result = d.calc(firstRun, indexStates[3], memoryStates[3], result.Output, combinations[comb + 3]);
            SaveOrClear(3, result);
            result = e.calc(firstRun, indexStates[4], memoryStates[4], result.Output, combinations[comb + 4]);
            if (result.Paused) {
                indexStates[4] = result.IndexState;
                memoryStates[4] = result.MemoryState;
                return repeatDis(false, comb, result.Output);
            }
            else if (result.Stopped) {
                indexStates[4] = 0;
                memoryStates[4] = null;
                return new AmpState() {
                    Output = result.Output,
                };
            }
            return null;
        }

        private static void SaveOrClear(int index, AmpState result) {
            if (result.Paused) {
                indexStates[index] = result.IndexState;
                memoryStates[index] = result.MemoryState;
            }
            else if (result.Stopped) {
                indexStates[index] = 0;
                memoryStates[index] = null;
            }
        }

        private static void Output(int[] permutation) {
            foreach (int item in permutation) {
                combinations.Add(item);
            }

            Console.WriteLine();
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
    }
}

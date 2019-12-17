using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOCDay16 {
    internal class Phaseshifter {
        List<int> Repeatingpattern;
        int Phase;
        List<int> Input;
        bool first;
        List<int> RealOutput = new List<int>();
        public Phaseshifter() {
            Repeatingpattern = new List<int>() { 0, 1, 0, -1 };
            Input = new List<int>() { };
            var lines = File.ReadAllLines("input.txt");
            for (int i = 0; i < 10000; i++) {
                foreach (var line in lines) {
                    Input.AddRange(line.ToCharArray().Select(x => Int32.Parse(x.ToString())).ToList());
                }
            }

        }

        internal List<int> run(int count, List<int> input) {
            if (input is null) {
                input = Input.TakeLast(Input.Count() - 5977709).ToList();
            }
           for (int i = input.Count();i>0; i--) {
                if (i != input.Count()) {
                    input[i-1] = Int32.Parse((input[i-1] + input[i]).ToString().Last().ToString());
                } else {
                    input[i-1] = input[i-1];
                }
            }
            return input;
        }
    }
}
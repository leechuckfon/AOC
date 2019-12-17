using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOCDay16 {
    public class ShifterV2 {
        List<int> Repeatingpattern;
        int Phase;
        List<int> Input;
        bool first;
        List<int> RealOutput = new List<int>();
        public ShifterV2() {
            Repeatingpattern = new List<int>() { 0, 1, 0, -1 };
            Input = new List<int>() { };
            var lines = File.ReadAllLines("input.txt");
            for (int i = 0; i < 10000; i++) {
                foreach (var line in lines) {
                    Input.AddRange(line.ToCharArray().Select(x => Int32.Parse(x.ToString())).ToList());
                }
            }
        }
        public List<int> ShiftBetter(List<int> lol) {
            if (lol != null) {
                Input = lol;
            }
            var testOutput = new List<int>();
            for (int length = 1; length<=Input.Count;length++) {
                
                var start = Input.Skip(length-1);
                var number = 0;
                do {
                    number += start.Take(length).Sum();
                    start = start.Skip(length*2);
                    if (length < (Input.Count / 2) + 1) {
                        number -= start.Take(length).Sum();
                        start = start.Skip(length*2);
                    }
                } while (start.Count() > 0);
                var actualNumber = number.ToString();
                testOutput.Add(Int32.Parse(actualNumber.Last().ToString()));
            }
            return testOutput;
        }
    }
}


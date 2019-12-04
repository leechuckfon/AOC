using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace AOC {
    class Program {
        private static string[] allInputs;
        static void Main(string[] args) {
            for (int i = 0; i < 100; i++) {
                for (int j = 0; j < 100; j++) {
                    calc(i.ToString(), j.ToString());
                }
            }

        }

        private static void calc(string i1, string i2) {
            string text = File.ReadAllText("input.txt");
            allInputs = text.Split(',');
            allInputs[1] = i1;
            allInputs[2] = i2;
            int index = 0;
            int opcode = 0;
            int input1;
            int input2;
            int position = 0;
            while (opcode != 99) {
                var read = index * 4;
                opcode = Int32.Parse(allInputs[read]);
                if (opcode == 99) {

                    if (allInputs[0] == "19690720") {
                        Console.WriteLine(i1);
                        Console.WriteLine(i2);
                        Console.ReadLine();
                    }
                    return;
                }
                input1 = Int32.Parse(allInputs[read + 1]);
                input2 = Int32.Parse(allInputs[read + 2]);
                position = Int32.Parse(allInputs[read + 3]);
                switch (opcode) {
                    case 1: opcode1(input1, input2, position); break;
                    case 2: opcode2(input1, input2, position); break;
                }

                index++;
            }
            if (allInputs[0] == "19690720") {
                Console.WriteLine(i1);
                Console.WriteLine(i2);
                Console.ReadLine();
            }
        }
        private static void opcode1(int input, int put, int index) {
            var toPutIn = Int32.Parse(allInputs[input]) + Int32.Parse(allInputs[put]);
            allInputs[index] = toPutIn.ToString();
        }
        private static void opcode2(int input1, int input2, int position) {
            var toPutIn = Int32.Parse(allInputs[input1]) * Int32.Parse(allInputs[input2]);
            allInputs[position] = toPutIn.ToString();
        }
        private static void testEverything() {
            var testInput = (string[])allInputs.Clone();
            
        }
    }
}

using System;
using System.IO;

namespace AOC {
    class Program {
        static void Main(string[] args) {
            double sum = 0;
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines) {
                double toSubTwo = 0;
                double fuel = Int32.Parse(line);
                while (fuel / 3 - 2 > 0) {
                    double toRoundDown = fuel / 3;
                    toSubTwo = Math.Round(toRoundDown, MidpointRounding.ToNegativeInfinity);
                    fuel = toSubTwo - 2;
                    sum += toSubTwo - 2;
                }

            }
            Console.WriteLine(sum);
        }
    }
}

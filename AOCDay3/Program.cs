using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOCDay3 {
    class Program {
        private static List<Position> line1 = new List<Position>() { new Position() { x=0, y=0} };
        private static List<Position> line2 = new List<Position>() { new Position() { x = 0, y = 0 } };
        private static int steps1 = 0;
        private static int steps2 = 0;
        static void Main(string[] args) {
            var positions = "993,847,868,286,665,860,823,934,341,49,762,480,899,23,273,892,43,740,940,502,361,283,852,630,384,758,655,358,751,970,72,245,188,34,355,373,786,188,304,621,956,839,607,279,459,340,412,901,929,256,495,462,369,138,926,551,343,237,434,952,421,263,663,694,687,522,47,8,399,930,928,73,581,452,80,610,998,797,584,772,521,292,959,356,940,894,774,957,813,650,891,309,254,271,791,484,399,106,463,39,210,154,380,86,136,228,284,267,195,727,739,393,395,703,385,483,433,222,945,104,605,814,656,860,474,672,812,789,29,256,857,436,927,99,171,727,244,910,347,789,49,598,218,834,574,647,185,986,273,363,848,531,837,433,795,923,182,915,367,347,867,789,776,568,969,923,765,589,772,715,38,968,845,327,721,928,267,94,763,799,946,130,649,521,569,139,584,27,823,918,450,390,149,237,696,258,757,810,216,202,966,157,702,623,740,560,932,587,197,56,695,439,655,576,695,176,800,374,806,969,664,216,170,415,485,188,444,613,728,508,644,289,831,978,711,973,3,551,377,114,15,812,210,829,536,883,843,427,311,680,482,69,125,953,896,85,376,683,374,415,3,843,802,124,299,345,696,276,87,98,619,321,348,806,789,657,590,747,477,251,854,351,82,982,906,94,285,756,737,377,951,126,852,751,946,696,44,709,851,364,222";
            var numbers = positions.Split(',');
            var sum = 0;
            foreach (var number in numbers) {
                sum += Int32.Parse(number);
            }
            //Console.WriteLine(sum);
            var lines = File.ReadAllLines("input.txt");
            FillLine(lines[0],line1, steps1);
            FillLine(lines[1], line2, steps2);
            line1.RemoveAt(0);
            line2.RemoveAt(0);
            var closestCrossing = 99999;
            var line1Steps = 99999;
            var line2Steps = 99999;
            var totalSteps = 0;
            foreach (var pos in line1) {
                foreach (var pos2 in line2) {
                    if (pos.x == pos2.x && pos.y == pos2.y) {
                        //Console.WriteLine(Math.Abs(pos.x) + Math.Abs(pos.y));
                        //if (closestCrossing > Math.Abs(pos.x) + Math.Abs(pos.y)) {
                        //    closestCrossing = Math.Abs(pos.x) + Math.Abs(pos.y);
                        //}
                        var steps = line1.Distinct().ToList();
                        var steps2 = line2.Distinct().ToList();

                        var actualSteps = steps.IndexOf(pos);
                        var actualSteps2 = steps2.IndexOf(pos2);
                        if (actualSteps < line1Steps && actualSteps2 < line2Steps) {
                            totalSteps = actualSteps + actualSteps2;
                            line1Steps = actualSteps;
                            line2Steps = actualSteps2;
                        }
                    }
                }
            }
            //var closestCrossing =line1.Find(position => line2.Exists(pos => pos.x == position.x && pos.y == position.y));
            Console.WriteLine(totalSteps);
            //Console.ReadLine();
            //Console.WriteLine(line1.Count);
            //Console.WriteLine(line2.Count);
        }

        private static void FillLine(string line, List<Position> lineToFill, int steps) {
            var instruction = line.Split(',');
            foreach (var instr in instruction) {
                var direction = instr.Substring(0, 1);
                int movement = Int32.Parse(instr.Substring(1, instr.Length - 1));

                switch (direction.ToLower()) {
                    case "d": DOWN(movement, lineToFill[lineToFill.Count - 1], lineToFill); break;
                    case "u": UP(movement, lineToFill[lineToFill.Count - 1], lineToFill); break;
                    case "l": LEFT(movement, lineToFill[lineToFill.Count - 1], lineToFill); break;
                    case "r": RIGHT(movement, lineToFill[lineToFill.Count - 1], lineToFill); break;
                }

            }
        }

        private static void UP(int number, Position p, List<Position> line) {
            for (int i = 1; i <= number; i++) {
                var toInsertPosition = new Position() { x = p.x, y = p.y };
                toInsertPosition.y += i;
                line.Add(toInsertPosition);
            }
        }

        private static void DOWN(int number, Position p, List<Position> line) {
            for (int i = 1; i <= number; i++) {
                var toInsertPosition = new Position() { x = p.x, y = p.y };
                toInsertPosition.y -= i;
                line.Add(toInsertPosition);
            }
        }

        private static void LEFT(int number, Position p, List<Position> line) {
            for (int i=1;i<=number;i++) {
                var toInsertPosition = new Position() { x = p.x, y = p.y };
                toInsertPosition.x -= i;
                line.Add(toInsertPosition);
            }
        }

        private static void RIGHT(int number, Position p, List<Position> line) {
            for (int i = 1; i <= number; i++) {
                var toInsertPosition = new Position() { x = p.x, y = p.y };
                toInsertPosition.x += i;
                line.Add(toInsertPosition);
            }
        }
    }

    internal class Position: IComparable<Position>{
        public int x { get; set; }
        public int y { get; set; }

        public int CompareTo(Position otherPos) {
            return x - otherPos.x + y - otherPos.y;
        }
    }
}

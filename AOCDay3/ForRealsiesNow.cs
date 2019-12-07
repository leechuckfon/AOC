using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
namespace AOCDay3 {
    public class ForRealsiesNow {
        #region Declarations
        private readonly List<Position> FirstLine = new List<Position>() { new Position(0, 0) };
        private readonly List<Position> SecondLine = new List<Position>() { new Position(0, 0) };
        private readonly List<int> Steps = new List<int>();
        #endregion
        #region RunnableFunction
        public void LetsGo() {
            var lines = File.ReadAllLines("input.txt");
            CalculateFirstLine(lines[0], FirstLine);
            //CheckForSecond(lines[1]);
            CalculateFirstLine(lines[1], SecondLine);
            FirstLine.RemoveAt(0);
            SecondLine.RemoveAt(0);
            //Steps.RemoveAt(0);
            IEnumerable<Position> intersections = FirstLine.Intersect(SecondLine, new PositionEqualizer()).AsParallel();
            var closest = intersections.Select(position => Math.Abs(position.x) + Math.Abs(position.y)).Min();
            Console.WriteLine("Closest distance from center = " + closest);
            var rip = intersections.Select(inter => (FirstLine.Distinct().ToList().IndexOf(inter) + SecondLine.Distinct().ToList().IndexOf(inter))).First() + 2;
            Console.WriteLine(rip);
        }
        #endregion
        #region Failure
        private void CheckForSecond(string line) {
            var allInstructions = line.Split(',');
            foreach (var instruction in allInstructions) {
                var direction = instruction[0].ToString();
                var movement = Int32.Parse(instruction.Substring(1, instruction.Length - 1));
                switch (direction.ToLower()) {
                    case "d": CROSSDOWN(movement); break;
                    case "u": CROSSUP(movement); break;
                    case "l": CROSSLEFT(movement); break;
                    case "r": CROSSRIGHT(movement); break;
                }
            }
        }
        private void CROSSDOWN(int movement) {
            var lastPos = SecondLine.Last();
            var interection = FirstLine.Find(pos => pos.x == lastPos.x && pos.y <= lastPos.y && pos.y >= lastPos.y - movement);
            if (interection != null) {
                Steps.Add(FirstLine.Distinct().ToList().IndexOf(interection) + SecondLine.Distinct().Count() - 1 + (lastPos.y - interection.y));
            }
            DOWN(movement, lastPos, SecondLine);
        }
        private void CROSSUP(int movement) {
            var lastPos = SecondLine.Last();
            var interection = FirstLine.Find(pos => pos.x == lastPos.x && pos.y >= lastPos.y && pos.y <= lastPos.y + movement);
            if (interection != null) {
                Steps.Add(FirstLine.Distinct().ToList().IndexOf(interection) + SecondLine.Distinct().Count() - 1 + (interection.y - lastPos.y));
            }
            UP(movement, lastPos, SecondLine);
        }
        private void CROSSRIGHT(int movement) {
            var lastPos = SecondLine.Last();
            var interection = FirstLine.Find(pos => pos.y == lastPos.y && pos.x >= lastPos.x && pos.x <= lastPos.x + movement);
            if (interection != null) {
                Steps.Add(FirstLine.Distinct().ToList().IndexOf(interection) + SecondLine.Distinct().Count() - 1 + (interection.x - lastPos.x));
            }
            RIGHT(movement, lastPos, SecondLine);
        }
        private void CROSSLEFT(int movement) {
            var lastPos = SecondLine.Last();
            var interection = FirstLine.Find(pos => pos.y == lastPos.y && pos.x <= lastPos.x && pos.x >= lastPos.x - movement);
            if (interection != null) {
                Steps.Add(FirstLine.Distinct().ToList().IndexOf(interection) + SecondLine.Distinct().Count() - 1 + (lastPos.x - interection.x));
            }
            LEFT(movement, lastPos, SecondLine);
        }
        #endregion
        #region FillLine
        private void CalculateFirstLine(string line, List<Position> toFill) {
            var allInstructions = line.Split(',');
            foreach (var instruction in allInstructions) {
                var direction = instruction[0].ToString();
                var movement = Int32.Parse(instruction.Substring(1, instruction.Length - 1));
                switch (direction.ToLower()) {
                    case "d": DOWN(movement, toFill.Last(), toFill); break;
                    case "u": UP(movement, toFill.Last(), toFill); break;
                    case "l": LEFT(movement, toFill.Last(), toFill); break;
                    case "r": RIGHT(movement, toFill.Last(), toFill); break;
                }
            }
        }
        private void UP(int number, Position p, List<Position> toFill) {
            for (int i = 1; i <= number; i++) {
                var toInsertPosition = new Position() { x = p.x, y = p.y };
                toInsertPosition.y += i;
                toFill.Add(toInsertPosition);
            }
        }
        private void DOWN(int number, Position p, List<Position> toFill) {
            for (int i = 1; i <= number; i++) {
                var toInsertPosition = new Position() { x = p.x, y = p.y };
                toInsertPosition.y -= i;
                toFill.Add(toInsertPosition);
            }
        }
        private void LEFT(int number, Position p, List<Position> toFill) {
            for (int i = 1; i <= number; i++) {
                var toInsertPosition = new Position() { x = p.x, y = p.y };
                toInsertPosition.x -= i;
                toFill.Add(toInsertPosition);
            }
        }
        private void RIGHT(int number, Position p, List<Position> toFill) {
            for (int i = 1; i <= number; i++) {
                var toInsertPosition = new Position() { x = p.x, y = p.y };
                toInsertPosition.x += i;
                toFill.Add(toInsertPosition);
            }
        }
        #endregion
        #region Position
        internal class PositionEqualizer : IEqualityComparer<Position> {
            public bool Equals([DisallowNull] Position x, [DisallowNull] Position y) {
                return x.x == y.x && x.y == y.y;
            }
            public int GetHashCode([DisallowNull] Position obj) {
                return obj.x + obj.y;
            }
        }
        internal class Position : IEquatable<Position> {
            public Position() {
            }
            public Position(int x, int y) {
                this.x = x;
                this.y = y;
            }
            public int x { get; set; }
            public int y { get; set; }
            public bool Equals([AllowNull] Position other) {
                return other.x == x && other.y == y;
            }
        }
        #endregion
    }
}

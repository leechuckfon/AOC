using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace AOCDay12 {
    public class PuzzleSolver {
        private List<Planet> allPlanets = new List<Planet>();
        private string[] names = new[] { "Io", "Europa", "Ganymede", "Callisto" };


        private List<UniverseState> states = new List<UniverseState>();

        public int Solve(string filePath, int interationCount) {
            var planets = File.ReadAllLines(filePath);
            for (int i = 0; i < planets.Length; i++) {
                // Make Planet
                var coordinates = planets[i].Split(',');
                string xasString = coordinates[0].Replace("<x=", "");
                string yasString = coordinates[1].Replace("y=", "");
                string zasString = coordinates[2].Replace("z=", "").Replace(">", "");
                int x = Int32.Parse(xasString);
                int y = Int32.Parse(yasString);
                int z = Int32.Parse(zasString);

                allPlanets.Add(new Planet() {
                    Name = names[i],
                    Gravity = new Vector() {
                        x = x,
                        y = y,
                        z = z
                    }
                });

            }
            for (int i = 0; i < interationCount; i++) {
                foreach (var p in allPlanets) {
                    foreach (var testp in allPlanets) {
                        if (p != testp) {
                            p.ChangeVelocity(testp);
                        }
                    }
                }

                foreach (var p in allPlanets) {
                    p.Move();
                }
            }
            var sum = 0;
            foreach (var p in allPlanets) {
                sum += p.CalculateEnergy();
            }
            return sum;
        }

          private static List<Flatnet> allXFlatnets = new List<Flatnet>();
        public long SolveX(string filePath) {
            List<FlatnetState> states = new List<FlatnetState>();
            var planets = File.ReadAllLines(filePath);
            for (long i = 0; i < planets.Length; i++) {
                // Make Planet
                var coordinates = planets[i].Split(',');
                string xasString = coordinates[0].Replace("<x=", "");
                int x = Int32.Parse(xasString);

                allXFlatnets.Add(new Flatnet() {
                    Name = names[i],
                    Position = x
                });
            }
            var start = allXFlatnets.Select(item => (Flatnet)item.Clone()).ToList();
            long index = 0;
            while (true) {
                index++;
                foreach (var p in allXFlatnets) {
                    foreach (var testp in allXFlatnets) {
                        if (p != testp) {
                            p.ChangeX(testp);
                        }
                    }
                }

                foreach (var p in allXFlatnets) {
                    p.Move();
                }

                if (start.All(x => x.Position == allXFlatnets[start.IndexOf(x)].Position && x.Velocity == allXFlatnets[start.IndexOf(x)].Velocity)) {
                    return index;
                }
                
            }

        }

        public class FlatnetState {
            public List<Flatnet> Flatnets { get; set; }
        }

        public class Flatnet : ICloneable {
            public int Position { get; set; }
            public int Velocity { get; set; }
            public string Name { get; internal set; }

            public object Clone() {
                return new Flatnet() {
                    Name = Name,
                    Position = Position,
                    Velocity = Velocity
                };
            }

            internal void ChangeX(Flatnet testp) {
                if (testp.Position > Position) {
                    Velocity++;
                }
                else if (testp.Position < Position) {
                    Velocity--;
                }
            }

            internal void Move() {
                Position += Velocity;
            }
        }

        public long SolveY(string filePath) {
            allXFlatnets = new List<Flatnet>();
            List<FlatnetState> states = new List<FlatnetState>();
            var planets = File.ReadAllLines(filePath);
            for (long i = 0; i < planets.Length; i++) {
                // Make Planet
                var coordinates = planets[i].Split(',');
                string xasString = coordinates[1].Replace("y=", "");
                int x = Int32.Parse(xasString);

                allXFlatnets.Add(new Flatnet() {
                    Name = names[i],
                    Position = x
                });
            }
            var start = allXFlatnets.Select(item => (Flatnet)item.Clone()).ToList();
            long index = 0;
            while (true) {
                index++;
                foreach (var p in allXFlatnets) {
                    foreach (var testp in allXFlatnets) {
                        if (p != testp) {
                            p.ChangeX(testp);
                        }
                    }
                }

                foreach (var p in allXFlatnets) {
                    p.Move();
                }

                if (start.All(x => x.Position == allXFlatnets[start.IndexOf(x)].Position && x.Velocity == allXFlatnets[start.IndexOf(x)].Velocity)) {
                    return index;
                }

            }
        }
        public long SolveZ(string filePath) {
            allXFlatnets = new List<Flatnet>();

            List<FlatnetState> states = new List<FlatnetState>();
            var planets = File.ReadAllLines(filePath);
            for (long i = 0; i < planets.Length; i++) {
                // Make Planet
                var coordinates = planets[i].Split(',');
                string xasString = coordinates[2].Replace("z=", "").Replace(">","");
                int x = Int32.Parse(xasString);

                allXFlatnets.Add(new Flatnet() {
                    Name = names[i],
                    Position = x
                });
            }
            var start = allXFlatnets.Select(item => (Flatnet)item.Clone()).ToList();
            long index = 0;
            while (true) {
                index++;
                foreach (var p in allXFlatnets) {
                    foreach (var testp in allXFlatnets) {
                        if (p != testp) {
                            p.ChangeX(testp);
                        }
                    }
                }

                foreach (var p in allXFlatnets) {
                    p.Move();
                }

                if (start.All(x => x.Position == allXFlatnets[start.IndexOf(x)].Position && x.Velocity == allXFlatnets[start.IndexOf(x)].Velocity)) {
                    return index;
                }

            }
        
        }
    }

    internal class FlatnetStateComparator : IEqualityComparer<PuzzleSolver.FlatnetState> {
        public bool Equals([AllowNull] PuzzleSolver.FlatnetState x, [AllowNull] PuzzleSolver.FlatnetState y) {
            return x.Flatnets.TrueForAll(b => b.Position == y.Flatnets[x.Flatnets.IndexOf(b)].Position && b.Velocity == y.Flatnets[x.Flatnets.IndexOf(b)].Velocity);
        }

        public int GetHashCode([DisallowNull] PuzzleSolver.FlatnetState obj) {
            return obj.Flatnets.Select(x => x.Velocity + x.Position + x.Name.ToCharArray().Aggregate((a,b) => a +=b)).Aggregate((a,b) => a+=b);
        }
    }

    internal class FlatnetComparator : IEqualityComparer<PuzzleSolver.Flatnet> {
        public bool Equals([AllowNull] PuzzleSolver.Flatnet x, [AllowNull] PuzzleSolver.Flatnet y) {
            return x.Position == y.Position && x.Velocity == y.Velocity && x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] PuzzleSolver.Flatnet obj) {
            return obj.Position + obj.Velocity + obj.Name.ToCharArray().Aggregate((a,b) => a += b);
        }
    }

    internal class UniverseState {
        public List<Planet> StatesOfPlanets { get; set; }
    }
}

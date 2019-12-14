using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AOCDay12 {
    class Program {
        static void Main(string[] args) {
            PuzzleSolver ps = new PuzzleSolver();
            Console.WriteLine(ps.Solve("input.txt",1000));
            Stopwatch s = new Stopwatch();
            s.Start();
            long x = ps.SolveX("input.txt");
            long y = ps.SolveY("input.txt");
            long z = ps.SolveZ("input.txt");
            s.Stop();
            Console.WriteLine(lcm(x,lcm(y, z)));
            Console.WriteLine(s.ElapsedMilliseconds);


        }

        private static long lcm(long num1, long num2) {
            long x, y, lcm = 0;
            x = num1;
            y = num2;
            while (num1 != num2) {
                if (num1 > num2) {
                    num1 = num1 - num2;
                }
                else {
                    num2 = num2 - num1;
                }
            }
            return (x * y) / num1;
        }
    }

    public class Planet {
        public Planet() {
            Velocity = new Vector();
        }
        public string Name { get; set; }
        public Vector Gravity { get; set; }
        public Vector Velocity { get; set; }

        public void ChangeVelocity(Planet secondPlanet) {
            if (secondPlanet.Gravity.x > Gravity.x) {
                Velocity.x++;
            }
            else if (secondPlanet.Gravity.x < Gravity.x) {
                Velocity.x--;
            }

            if (secondPlanet.Gravity.y > Gravity.y) {
                Velocity.y++;
            }
            else if (secondPlanet.Gravity.y < Gravity.y) {
                Velocity.y--;
            }

            if (secondPlanet.Gravity.z > Gravity.z) {
                Velocity.z++;
            }
            else if (secondPlanet.Gravity.z < Gravity.z) {
                Velocity.z--;
            }
        }
        public void Move() {
            Gravity.x += Velocity.x;
            Gravity.y += Velocity.y;
            Gravity.z += Velocity.z;
        }

        public int CalculateEnergy() {
            return (Math.Abs(Gravity.x) + Math.Abs(Gravity.y) + Math.Abs(Gravity.z)) * (Math.Abs(Velocity.x) + Math.Abs(Velocity.y) + Math.Abs(Velocity.z));
        }
    }

    public class Vector {
        public int x { get; set; } = 0;
        public int y { get; set; } = 0;
        public int z { get; set; } = 0;

    }
}

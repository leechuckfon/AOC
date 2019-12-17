using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AOCDay10 {
    public class BaseBuilder {
        private List<Point> map = new List<Point>();
        private int width = 0;
        private int height = 0;
        public int run(string inputFile) {
            var lines = File.ReadAllLines(inputFile);
            int xindex = 0;
            int yindex = 0;
            foreach (var line in lines) {
                foreach (var pos in line) {
                    if (pos == '#') {
                        map.Add(new Point() {
                            x = xindex,
                            y = yindex
                        });
                    }
                    xindex++;
                }
                width = xindex;
                xindex = 0;
                yindex++;
            }
            height = yindex;

            return CalculateVisible(map);
        }

        public int CalculateVisible(List<Point> map) {
            var results = new Dictionary<Point, int>();
            foreach (var from in map) {
                var totalVisible = new List<Point>();
                foreach (var to in map) {
                    var blocked = false;
                    if (from != to) {
                        foreach (var test in map) {
                            if (!blocked) {
                                if (from != test && to != test) {
                                    if (OnLine(from, to, test)) {
                                        if (Between(test, from, to)) {
                                            blocked = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (!blocked) {
                            totalVisible.Add(to);
                        }
                    }
                }
                results.Add(from, totalVisible.Count);
            }
            Point bestAsteroid = null;
            int t = 0;
            foreach (var key in results.Keys) {
                if (results[key] > t) {
                    t = results[key];
                    bestAsteroid = key;
                }
            }
            var shot = 0;
            while (map.Count != 0) {
                shot = Vaporize(bestAsteroid, shot);
            }
            return t;
        }

        public List<Point> CalcVisible(Point from) {
            var totalVisible = new List<Point>();
            foreach (var to in map) {
                var blocked = false;
                if (from != to) {
                    foreach (var test in map) {
                        if (!blocked) {
                            if (from != test && to != test) {
                                if (OnLine(from, to, test)) {
                                    if (Between(test, from, to)) {
                                        blocked = true;
                                    }
                                }
                            }
                        }
                    }
                    if (!blocked) {
                        totalVisible.Add(to);
                    }
                }
            }
            return totalVisible;
        }

        private int Vaporize(Point bestAsteroid, int amountShot) {
            var start = 90;
            var coorWithAngle = new List<Point>();
            coorWithAngle = CalcVisible(bestAsteroid);
            foreach (var coor in coorWithAngle) {
                coor.angle = calcAngle(bestAsteroid, coor);
            }

            var soemthing = coorWithAngle.OrderBy(x => x.angle).ToList();
            while (soemthing.Count != 0) {
                var toRemove = new List<Point>();
                foreach (var p in soemthing) {
                    amountShot++;
                    Console.WriteLine("shot " + amountShot + ": " + p.x + " " + p.y + " at " + p.angle);
                    if (amountShot == 200) {
                        map.RemoveAll(x => x != null);
                        return 0;
                    }
                    toRemove.Add(p);
                }
                soemthing.RemoveAll(x => toRemove.Contains(x));
                map.RemoveAll(x => toRemove.Any(p => p.x == x.x && p.y == x.y));
            }
            return amountShot;

        }

        private double calcAngle(Point a, Point b) {
            var deltax = b.x - a.x;
            var deltay = b.y - a.y;
            if (Math.Atan2(deltay, deltax) * 180 / Math.PI + 90 < 0) {
                return Math.Atan2(deltay, deltax) * 180 / Math.PI + 90 + 360;
            } else {

                return Math.Atan2(deltay, deltax) * 180 /Math.PI + 90;
            }
;
        }

        private bool Between(Point test, Point from, Point to) {
            var distanceAB = Math.Abs(to.x - from.x);
            var distanceyAB = Math.Abs(to.y - from.y);
            var dya = Math.Abs(test.y - from.y);
            var dxa = Math.Abs(test.x - from.x);
            if (dxa <= distanceAB && dya <= distanceyAB) return true;
            return false;
        }

        public bool OnLine(Point from, Point to, Point test) {
            if (Math.Sign(test.x - from.x) != Math.Sign(to.x - from.x)) return false;
            if (Math.Sign(test.y - from.y) != Math.Sign(to.y - from.y)) return false;
            if (test.x == from.x && test.x == to.x) return true;
            if (test.y == from.y && test.y == to.y) return true;
            return (to.x - from.x) * (test.y - from.y) == (test.x - from.x) * (to.y - from.y);
        }
    }

    public class Point {
        public int x { get; set; }
        public int y { get; set; }
        public double angle { get; set; }
    }
}

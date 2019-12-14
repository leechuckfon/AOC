using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace AOCDay10 {
    public class BaseLocator {
        private Dictionary<Point, bool> map;
        private int width;
        public BaseLocator() {
            map = new Dictionary<Point, bool>();
        }

        public int run(string textfile) {
            var lines = File.ReadAllLines(textfile);
            var yindex = 0;
            foreach (var line in lines) {
                var xindex = 0;
                foreach (var entry in line) {
                    if (entry == '#') {
                        map.Add(new Point() { x = xindex, y = yindex }, true);
                    }
                    else {
                        map.Add(new Point() { x = xindex, y = yindex }, false);
                    }
                    xindex++;
                }
                width = xindex;
                yindex++;
            }
            return RemoveNonLOSPoints();
        }


        private int RemoveNonLOSPoints() {
            var astroidCount = new List<int>();
            var potential = new List<Point>();
            foreach (var spot in map.Keys) {
                if (map[spot] == true) {
                    potential.Add(spot);
                }
            }
            foreach (var potentialSpot in potential) {
                var howManyAstroidsSeen = calculateVector(potentialSpot, potential);
                astroidCount.Add(howManyAstroidsSeen);
            }
            return astroidCount.Max();
        }

        private int calculateVector(Point start, List<Point> endPoints) {
            var visible = new List<Point>();
            foreach (var endPoint in endPoints) {
                var isblocked = false;
                //(x - x1) / (x2 - x1) = (y - y1) / (y2 - y1) = (z - z1) / (z2 - z1)
                foreach (var testPoint in endPoints) {
                    if (testPoint != endPoint && testPoint != start) {
                        if (OnLine(start,endPoint,testPoint)){
                            if (Distance(start,endPoint) == Distance(start,testPoint) + Distance(endPoint,testPoint)) {
                                isblocked = true;
                            }
                        }
                    }
                }
                if (!isblocked) {
                    visible.Add(endPoint);

                }
            }
            return visible.Count;
        }

        private double Distance(Point endPoint, Point testPoint) {
            return Math.Sqrt(Math.Pow(endPoint.x - testPoint.x, 2) + Math.Pow(endPoint.y - testPoint.y, 2));
        }

        private bool isBlocked(Point testPoint, Point start, Point endPoint) {


            //if (dxa <= dx && dya <= dy) return true;
            return false;
        }

        private bool OnLine(Point from, Point b, Point a) {
            if (Math.Sign(a.x - from.x) != Math.Sign(b.x - from.x)) return false;
            if (Math.Sign(a.y - from.y) != Math.Sign(b.y - from.y)) return false;
            if (a.x == from.x && b.x == from.x) return true;
            if (a.y == from.y && b.y == from.y) return true;
            return ((a.x - from.x) * (b.y - from.y) == (b.x - from.x) * (a.y - from.y));

        }
    }

      

        internal class PointCheck : IEqualityComparer<Point> {
    public bool Equals([AllowNull] Point x, [AllowNull] Point y) {
        return x.x == y.x && x.y == y.y;
    }

    public int GetHashCode([DisallowNull] Point obj) {
        return obj.x + obj.y;
    }
}

public class Point {
    public int x { get; set; }
    public int y { get; set; }
}
}
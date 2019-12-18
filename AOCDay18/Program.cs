using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOCDay18 {
    class Program {
        //private static List<string> allKeys = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        //private static List<string> allKeys = new List<string>() { "a", "b", "c", "d", "e", "f", "g"};
        //private static List<string> allKeys = new List<string>() { "a", "b", "c", "d", "e", "f"};
        private static List<string> allKeys = new List<string>() { "@", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p" };
        private static List<string> Doors = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
        private static List<List<string>> map = new List<List<string>>();
        private static List<string> Inventory = new List<string>();
        public static Dictionary<string, List<BFSPoint>> destinationReached = new Dictionary<string, List<BFSPoint>>();
        private static int stepCount = 0;
        private static Queue<BFSPoint> BFSQueue = new Queue<BFSPoint>();
        static void Main(string[] args) {
            LoadMap();

            var countIndex = 1;
            //var shortestToFirstKey = BFS(startingPoint, begin, countIndex);
            //var p = shortestToFirstKey;
            //while (p != null) {
            //    stepCount++;
            //    p = p.getParent();
            //}
            foreach (var key in allKeys) {
                map = new List<List<string>>();
                LoadMap();
                var begin = map.IndexOf(map.Where(x => x.Contains(key)).FirstOrDefault());
                var startingPoint = map[begin].IndexOf(key);
                BFSPoint shortestToFirstKey;
                BFSPoint p;
                destinationReached.Add(key, new List<BFSPoint>());
                foreach (var destination in allKeys) {
                    if (destination != key) {
                        map = new List<List<string>>();
                        LoadMap();
                        countIndex++;
                        shortestToFirstKey = BFS(startingPoint, begin, destination, key);
                        p = (BFSPoint)shortestToFirstKey.Clone();
                        destinationReached[key].Add(p);
                    }
                }
            }

        }

        private static List<BFSPoint> tempList = new List<BFSPoint>();
        
        private static void AnalyzeClosestPoints(BFSPoint start) {
            // Find reachable keys
            var reachable = destinationReached[start.origin].FindAll(x => !start.Seen.Contains(x.destination) && x.Dependencies.All(a => start.Inventory.Contains(a.ToLower()))).ToList();

            // Add parent to key
            foreach (var k in reachable) {
                // add key to key's inventory
                k.Inventory.Add(k.destination);
                // add parent to seen
                k.Seen.AddRange(start.destination);
                // add step count
                k.stepsNeeded += start.getSteps();

                tempList.Add(k);
            }



            // Repeat for lower level
        }

        private static BFSPoint BFS(int x, int y, string destination, string origin) {
            BFSQueue = new Queue<BFSPoint>();
            var index = 0;
            BFSQueue.Enqueue(new BFSPoint(x, y, null, new List<string>()));
            while (!(BFSQueue.Count == 0)) {
                BFSPoint p = BFSQueue.Dequeue();
                index++;
                if (map[p.y][p.x] == destination) {
                    Console.WriteLine("reached Destination");
                    p.stepsNeeded = p.getSteps();
                    p.destination = destination;
                    p.origin = origin;
                    return p;
                }

                if (isFree(p.x + 1, p.y)) {
                    if (Doors.Contains(map[p.y][p.x + 1])) {
                        p.Dependencies.Add(map[p.y][p.x + 1]);
                    }
                    map[p.y][p.x] = "#";
                    BFSPoint nextP = new BFSPoint(p.x + 1, p.y, p, p.Dependencies);
                    BFSQueue.Enqueue(nextP);
                }

                if (isFree(p.x - 1, p.y)) {
                    if (Doors.Contains(map[p.y][p.x - 1])) {
                        p.Dependencies.Add(map[p.y][p.x - 1]);
                    }
                    map[p.y][p.x] = "#";
                    BFSPoint nextP = new BFSPoint(p.x - 1, p.y, p, p.Dependencies);
                    BFSQueue.Enqueue(nextP);
                }

                if (isFree(p.x, p.y + 1)) {
                    if (Doors.Contains(map[p.y + 1][p.x])) {
                        p.Dependencies.Add(map[p.y + 1][p.x]);
                    }
                    map[p.y][p.x] = "#";
                    BFSPoint nextP = new BFSPoint(p.x, p.y + 1, p, p.Dependencies);
                    BFSQueue.Enqueue(nextP);
                }

                if (isFree(p.x, p.y - 1)) {
                    if (Doors.Contains(map[p.y - 1][p.x])) {
                        p.Dependencies.Add(map[p.y - 1][p.x]);
                    }
                    map[p.y][p.x] = "#";
                    BFSPoint nextP = new BFSPoint(p.x, p.y - 1, p, p.Dependencies);
                    BFSQueue.Enqueue(nextP);
                }
            }
            return null;
        }

        private static bool isFree(int x, int y) {
            if ((x < map[0].Count && x >= 0) && (y >= 0 && y < map.Count) && (map[y][x] == "." || map[y][x] == "@" || allKeys.Contains(map[y][x].ToLower()))) {
                return true;
            }
            return false;
        }

        private static void LoadMap() {
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines) {
                map.Add(line.ToCharArray().Select(x => x.ToString()).ToList());
            }
        }

        public class BFSPoint : ICloneable {
            public int x;
            public List<string> Inventory { get; set; }
            public List<BFSPoint> children { get; set; }
            public int y;
            BFSPoint parent;
            internal string destination;

            public int stepsNeeded { get; set; }
            public List<string> Dependencies { get; set; }
            public List<string> Seen { get; internal set; }
            public string origin { get; internal set; }

            public BFSPoint() {
            }

            public BFSPoint(int x, int y, BFSPoint parent, List<string> deps) {
                this.x = x;
                this.y = y;
                this.parent = parent;
                Dependencies = deps;
            }

            public BFSPoint getParent() {
                return this.parent;
            }

            public String toString() {
                return "x = " + x + " y = " + y;
            }

            public int getSteps() {
                var p = this;
                var i = 0;
                while (p.parent != null) {
                    i++;
                    p = p.parent;
                }
                return i;
            }

            public object Clone() {
                return new BFSPoint() {
                    Inventory = Inventory,
                    parent = parent,
                    x = x,
                    y = y,
                    stepsNeeded = stepsNeeded,
                    Dependencies = Dependencies,
                    destination = destination,
                    origin = origin
                };
            }
        }
    }
}

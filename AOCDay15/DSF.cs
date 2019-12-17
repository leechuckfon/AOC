using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AOCDay15 {
    public class DSF {
        public int[,] arr = new int[40,40];

        public DSF() {
            var lines = File.ReadAllLines("Map.txt");
            for(int j=0;j<lines.Length; j++) {
                for (int i = 0; i<lines[0].Length;i++) {
                    arr[i,j] = Int32.Parse(lines[j][i].ToString());
                }
            }
        }

        public class BFSPoint {
            public int x;
            public int y;
            BFSPoint parent;

            public BFSPoint(int x, int y, BFSPoint parent) {
                this.x = x;
                this.y = y;
                this.parent = parent;
            }

            public BFSPoint getParent() {
                return this.parent;
            }

            public String toString() {
                return "x = " + x + " y = " + y;
            }
        }

        public Queue<BFSPoint> q = new Queue<BFSPoint>();
        private BFSPoint lastPoint;

        public BFSPoint getPathBFS(int x, int y) {

            q.Enqueue(new BFSPoint(x, y, null));

            while (!(q.Count == 0)) {
                BFSPoint p = q.Dequeue();

                if (arr[p.x,p.y] == 2) {
                    Console.WriteLine("Exit is reached!");
                    return p;
                }

                if (isFree(p.x + 1, p.y)) {
                    arr[p.x,p.y] = -1;
                    BFSPoint nextP = new BFSPoint(p.x + 1, p.y, p);
                    q.Enqueue(nextP);
                }

                if (isFree(p.x - 1, p.y)) {
                    arr[p.x,p.y] = -1;
                    BFSPoint nextP = new BFSPoint(p.x - 1, p.y, p);
                    q.Enqueue(nextP);
                }

                if (isFree(p.x, p.y + 1)) {
                    arr[p.x,p.y] = -1;
                    BFSPoint nextP = new BFSPoint(p.x, p.y + 1, p);
                    q.Enqueue(nextP);
                }

                if (isFree(p.x, p.y - 1)) {
                    arr[p.x,p.y] = -1;
                    BFSPoint nextP = new BFSPoint(p.x, p.y - 1, p);
                    q.Enqueue(nextP);
                }
                lastPoint = p;
            }
            return lastPoint;
        }

        public bool isFree(int x, int y) {
            if ((x >= 0 && x < 40) && (y >= 0 && y < 40) && (arr[x,y] == 1 || arr[x,y] == 2)) {
                return true;
            }
            return false;
        }
    }
}

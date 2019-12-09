using System;
using System.Diagnostics;

namespace Day7 {
    class Program {
        static void Main(string[] args) {
            Stopwatch s = new Stopwatch();
            s.Start();
            SIFReader sr = new SIFReader();
            //sr.DivideIntoLayers(25,6);
            sr.DecodeMessage(25, 6);
            s.Stop();
            Console.WriteLine(s.ElapsedMilliseconds);
        }
    }
}

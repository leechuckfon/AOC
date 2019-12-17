using AOCDay17;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AOCDay5 {
    partial class Program {
        private static bool quit = false;
        private static int i = 0;
        private static bool moving = false;
        private static Stopwatch s = new Stopwatch();
        private static List<int> inputs = new List<int>() { 65, 44, 66, 44, 65, 44, 67, 44, 66, 44, 67, 44, 66, 44, 67, 44, 65, 44, 66, 10, 76, 44, 54, 44, 76, 44, 52, 44, 82, 44, 56, 10, 82, 44, 56, 44, 76, 44, 54, 44, 76, 44, 52, 44, 76, 44, 49, 48, 44, 82, 44, 56, 10, 76, 44, 52, 44, 82, 44, 52, 44, 76, 44, 52, 44, 82, 44, 56, 10, 110,10 };
        #region declarations
        private static IntComp c;
        private static int a;
        #endregion
        #region Main
        static void Main(string[] args) {
            s.Start();
            c = new IntComp();
            c.queuedEvent += PrintEvent;
            c.needInput += AskInput;
            c.calc(true);
            i = 0;
        }

        private static void AskInput(object sender, EventArgs e) {
            var input = "";
            c.input.Enqueue(inputs[i]);
            i++;
        }
        public static void PrintEvent(object sender, EventArgs e) {

            var arg = (QueuedEventArgs)e;
            if (arg.value < 700000) {
                Console.Write((char)arg.value);
            } else {
              Console.WriteLine(arg.value);
                s.Stop();
                Console.WriteLine(s.ElapsedMilliseconds + "ms");
            }
        }
        #endregion
    }
}


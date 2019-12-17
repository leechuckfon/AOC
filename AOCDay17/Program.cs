using AOCDay17;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AOCDay5 {
    partial class Program {
        private static bool quit = false;
        #region declarations
        private static IntComp c;
        #endregion
        #region Main
        static void Main(string[] args) {
            c = new IntComp();
            c.queuedEvent += PrintEvent;
            c.needInput += AskInput;
            //Parallel.Invoke(() => c.calc());
            c.calc();
            Thread t = new Thread(x => {
            });
            //while (!quit) {
            //    var input = "";
            //    int test = 0;
            //    while (!Int32.TryParse(input, out test)) {
            //        input = Console.ReadLine();
            //    }
            //    c.input.Enqueue(test);
            //    if (c.output.TryDequeue(out var result)) {
            //        Console.WriteLine(result);
            //    }
            //}
        }

        private static void AskInput(object sender, EventArgs e) {
            var input = "";
            var test = 0;
            while (!Int32.TryParse(input, out test)) {
                input = Console.ReadLine();
            }
            c.input.Enqueue(test);
            
        }
        public static void PrintEvent(object sender, EventArgs e) {
            var arg = (QueuedEventArgs)e;
            Console.Write(arg.value);
        }
        #endregion
    }
}


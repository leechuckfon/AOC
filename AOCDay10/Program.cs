using System;

namespace AOCDay10 {
    class Program {
        static void Main(string[] args) {
            BaseBuilder bb = new BaseBuilder();
            Console.WriteLine(bb.run("input.txt"));
        }
    }
}

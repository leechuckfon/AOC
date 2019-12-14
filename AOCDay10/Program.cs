using System;

namespace AOCDay10 {
    class Program {
        static void Main(string[] args) {
            BaseLocator bl = new BaseLocator();
            var output = bl.run("input.txt");
            Console.WriteLine(output);
        }
    }
}

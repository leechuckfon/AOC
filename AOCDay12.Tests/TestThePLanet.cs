using Microsoft.VisualStudio.TestTools.UnitTesting;
using AOCDay12;

namespace AOCDay12.Tests {
    [TestClass]
    public class TestThePlanet {
        [TestMethod]
        public void ExampleInput() {
            PuzzleSolver ps = new PuzzleSolver();
            int result = ps.Solve("input0.txt",100);
            Assert.AreEqual(1940, result);
        }

        [TestMethod]
        public void ExampleInput2() {
            PuzzleSolver ps = new PuzzleSolver();
            int result = ps.SolveX("input0.txt");
            Assert.AreEqual(2772, result);
        }
    }
}

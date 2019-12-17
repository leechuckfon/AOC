using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCDay14.Tests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
            ChemicalFactory cf = new ChemicalFactory();
            var oreCount = cf.run("Input0.txt");
            Assert.AreEqual(165, oreCount);
        }
        [TestMethod]
        public void TestMethod2() {
            ChemicalFactory cf = new ChemicalFactory();
            var oreCount = cf.run("Input1.txt");
            Assert.AreEqual(13312, oreCount);
        }
        [TestMethod]
        public void TestMethod3() {
            ChemicalFactory cf = new ChemicalFactory();
            var oreCount = cf.run("Input2.txt");
            Assert.AreEqual(180697, oreCount);
        }
        [TestMethod]
        public void TestMethod4() {
            ChemicalFactory cf = new ChemicalFactory();
            var oreCount = cf.run("Input3.txt");
            Assert.AreEqual(2210736, oreCount);
        }
    }
}

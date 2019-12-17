using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCDay10.Tests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
            BaseBuilder bb = new BaseBuilder();
            var result = bb.run("input1.txt");
            Assert.AreEqual(33, result);

        }
        [TestMethod]
        public void TestMethod2() {
            BaseBuilder bb = new BaseBuilder();
            var result = bb.run("input2.txt");
            Assert.AreEqual(35, result);

        }
        [TestMethod]
        public void TestMethod3() {
            BaseBuilder bb = new BaseBuilder();
            var result = bb.run("input3.txt");
            Assert.AreEqual(41, result);
        }
        [TestMethod]
        public void TestMethod4() {
            BaseBuilder bb = new BaseBuilder();
            var result = bb.run("input4.txt");
            Assert.AreEqual(210, result);

        }

        [TestMethod]
        public void Test63() {
            BaseBuilder bb = new BaseBuilder();
            var p1 = new Point() { x = 2, y = 5};
            var p2 = new Point() { x = 5, y = 8 };
            var p3 = new Point() { x = 6, y = 3 };
            Assert.IsTrue(bb.OnLine(p1, p2, p3));
        }
    }
}

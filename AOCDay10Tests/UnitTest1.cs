using AOCDay10;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCDay10Tests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
            BaseLocator test = new BaseLocator();
            var result = test.run("input0.txt");
            Assert.AreEqual(33, result);
        }
        [TestMethod]
        public void TestMethod2() {
            BaseLocator test = new BaseLocator();
            var result = test.run("input1.txt");
            Assert.AreEqual(result, 35);

        }
        [TestMethod]
        public void TestMethod4() {
            BaseLocator test = new BaseLocator();
            var result = test.run("input2.txt");
            Assert.AreEqual(result, 210);

        }
        [TestMethod]
        public void TestMethod3() {
            BaseLocator test = new BaseLocator();
            var result = test.run("input3.txt");
            Assert.AreEqual(result, 41);

        }
    }
}

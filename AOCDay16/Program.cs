using System;
using System.Collections.Generic;

namespace AOCDay16 {
    class Program {
        static void Main(string[] args) {
            Phaseshifter ps = new Phaseshifter();
            List<int> inputList = null;
            for (int i = 0; i < 100; i++) {
                inputList = ps.run(1,inputList);
            }
            var temp = "";
            //for (int j=0;j<7;j++) {
            //    temp += inputList[j].ToString();
            //}
            //var offset = Int32.Parse(temp);
        }
    }
}

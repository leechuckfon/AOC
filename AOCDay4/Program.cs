using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOCDay4 {
    class Program {
        static void Main(string[] args) {
            //Random r = new Random();
            //var valid = false;
            //var hasAdjacent = false;
            //var doesNotLower = false;
            //var increases = false;
            //int rInt;
            //do {
            //    rInt = r.Next(134564, 585159);
            //    if (rInt.ToString().Length == 6) {
            //        if (rInt > 134564 && rInt < 585159) {
            //            for (int i = 0; i < 5; i++) {
            //                if (rInt.ToString()[i] == rInt.ToString()[i + 1]) {
            //                    hasAdjacent = true;
            //                }
            //                var a = Int32.Parse(rInt.ToString()[i].ToString());
            //                var b = Int32.Parse(rInt.ToString()[i + 1].ToString());
            //                if (a < b) {
            //                    doesNotLower = true;
            //                } else {
            //                    increases = true;
            //                }
            //            }
            //            if (hasAdjacent && doesNotLower && !increases) {
            //                valid = true;
            //            }
            //        }
            //    }
            //} while (!valid);
            //Console.WriteLine(rInt);
            //for (int i=134564;i<=585159;i++) {
            List<int> something = new List<int>();
            for (int i= 134564;i<= 585159;i++) {

                var r = new Random();
                //int i = r.Next(134564, 585159);
                //int i = 112233;
                var isSixLong = i.ToString().Length == 6;
                var range = i > 134564 && i < 585159;
                var adjacent = false;
                var checkedForThree = false;
                var isNotDecreasing = true;
                var numberAsString = i.ToString();
                for (int index = 0; index <= 4; index++) {
                    var isSameAsFront = numberAsString[index] == numberAsString[index + 1];
                    var isSameAsBack = false;
                    if (index == 0) {
                        isSameAsBack = false;
                    } else {
                        isSameAsBack = numberAsString[index] == numberAsString[index - 1];
                    }
                    //var isSameAsFront = numberAsString[index] == numberAsString[index + 1];
                    if (isSameAsFront) {
                        //if (!checkedForThree) {
                        //    adjacent = true;
                        //}
                        //StringBuilder b = new StringBuilder(numberAsString[index]);
                        //b.Append(numberAsString[index]);
                        //b.Append(numberAsString[index]);
                        //if (index != 4) {
                        //    if (numberAsString.Substring(index,3) != b.ToString()) {
                        //        adjacent = true;
                        //    }
                        //} else {
                        //    if (numberAsString[index] != numberAsString[index - 1]) {
                        //        adjacent = true;
                        //    }
                        //}
                        adjacent = true;
                        
                    }
                    if (Int32.Parse(numberAsString[index].ToString()) > Int32.Parse(numberAsString[index + 1].ToString())) {
                        isNotDecreasing = false;
                    }
                }
                //var isNotDecreasing = numberAsString.ToCharArray().All(c => Int32.Parse(c.ToString()) <= Int32.Parse(numberAsString[numberAsString.Length - 1].ToString()));

                if (isSixLong && range && adjacent && isNotDecreasing) {
                    something.Add(i);
                }
            }
            // 5 4 or 3
            //something.Add(112233);
            List<int> result = new List<int>();
            foreach (var entry in something) {
                var everyNumber = entry.ToString().ToCharArray();
                var distinctNumbers = everyNumber.Distinct();
                foreach (var number in distinctNumbers) {
                    if (everyNumber.Count(letter => letter == number) == 2) {
                        result.Add(entry);
                    }
                }
            }
            Console.Write(something.Count);

            Console.Write(result.Distinct().Count());

            //}
        }
    }
}

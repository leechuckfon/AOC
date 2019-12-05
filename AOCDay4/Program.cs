using System;
using System.Collections.Generic;
using System.Linq;

namespace AOCDay4 {
    class Program {
        static void Main(string[] args) {






























            List<int> something = new List<int>();
            for (int i = 134564; i <= 585159; i++) {

                var r = new Random();


                var isSixLong = i.ToString().Length == 6;
                var range = i > 134564 && i < 585159;
                var adjacent = false;
                var isNotDecreasing = true;
                var numberAsString = i.ToString();
                for (int index = 0; index <= 4; index++) {
                    var isSameAsFront = numberAsString[index] == numberAsString[index + 1];
                    var isSameAsBack = false;
                    if (index == 0) {
                        isSameAsBack = false;
                    }
                    else {
                        isSameAsBack = numberAsString[index] == numberAsString[index - 1];
                    }

                    if (isSameAsFront) {















                        adjacent = true;

                    }
                    if (Int32.Parse(numberAsString[index].ToString()) > Int32.Parse(numberAsString[index + 1].ToString())) {
                        isNotDecreasing = false;
                    }
                }


                if (isSixLong && range && adjacent && isNotDecreasing) {
                    something.Add(i);
                }
            }


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


        }
    }
}

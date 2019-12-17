using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOCDay14 {
    public class ChemicalFactory {
        private Dictionary<string, long> baseComponents = new Dictionary<string, long>();
        private long totalOre = 0;
        public long run(string inputFile) {
            FillFormulas(inputFile);

            var test = ree(1);

            Int64 ore = 1000000000000;
            Int64 lowerBound = ore / test;
            Int64 upperBound = 1000 * 2;

            while (calculateOres("FUEL",upperBound) <= ore)
                upperBound *= 2;
            while (upperBound > lowerBound + 1) {
                Int64 guess = (upperBound + lowerBound) / 2;
                if (calculateOres("FUEL", guess) <= ore)
                    lowerBound = guess;
                else
                    upperBound = guess;
            }

            return lowerBound;
        }
        private static Dictionary<string, long> Excess = new Dictionary<string, long>();

        public List<Formula>[] Stages { get; private set; }
        private long ree(int lol) {
            Excess = new Dictionary<string, long>();
            foreach (var key in Formula.allFormulas) {
                Excess.Add(key.output.Key, 0);
            }
            sortReactions();
            Excess["FUEL"] = lol;
            Excess.Add("ORE", 0);

            DoFormula("FUEL");

            return Excess["ORE"];
        }
        private void DoFormula(string outputName) {

            var formula = Formula.allFormulas.Where(x => x.output.Key == outputName).First();
                foreach (var input in formula.input) {
                    if (input.Key != "ORE") {
                        var ifor = Formula.allFormulas.Where(x => x.output.Key == input.Key).First();
                        long total = Excess[formula.output.Key];
                        long unit = formula.output.Value;
                        long quantity = total / unit;
                        if (total % unit > 0) quantity++;
                        foreach (var a in formula.input) {
                            Excess[a.Key] += a.Value * quantity;
                        }
                         DoFormula(input.Key);
                    }
                }

        }

        private long calculateOres(string output, long needed) {
            Excess = new Dictionary<string, long>();
            foreach (var key in Formula.allFormulas) {
                Excess.Add(key.output.Key, 0);
            }

            Excess["FUEL"] = needed;
            Excess.Add("ORE", 0);

            sortReactions();
            for (int i = Formula.allFormulas.Where(x => x.output.Key == "FUEL").FirstOrDefault().stage; i>0; i--) {
                foreach (var st in Stages[i-1]) {
                    long total = Excess[st.output.Key];
                    long unit = st.output.Value;
                    long quantity = total / unit;
                    if (total % unit > 0) quantity++;
                    foreach (var input in st.input) {
                        Excess[input.Key] += input.Value * quantity;
                    }
                }
            }

            return Excess["ORE"];
        }

        private void sortReactions() {
            Formula finalReaction = Formula.allFormulas.Where(x => x.output.Key == "FUEL").FirstOrDefault();
            finalReaction.CalculateStagesHelp();
            int lastStage = finalReaction.stage;

            Stages = new List<Formula>[lastStage];
            for (int i = 0; i < lastStage; i++)
                Stages[i] = new List<Formula>();
            foreach (Formula r in Formula.allFormulas) {
                Stages[r.stage - 1].Add(r);
            }
        }

        private void FillFormulas(string inputFile) {
            var formulas = File.ReadAllLines(inputFile);
            foreach (var formula in formulas) {
                var tempDic = new Dictionary<string, long>();
                var formulaSplit = formula.Split("=>");
                var outputSplit = formulaSplit[1].Split(" ");
                var output = new KeyValuePair<string, long>(outputSplit[2].Trim(), Int64.Parse(outputSplit[1].Trim()));
                var inputSplit = formulaSplit[0].Trim().Split(",");
                foreach (var inputS in inputSplit) {
                    var singleInput = inputS.Trim().Split(" ");
                    tempDic.Add(singleInput[1].Trim(), Int64.Parse(singleInput[0].Trim()));
                }
                Formula.allFormulas.Add(new Formula() {
                    input = tempDic,
                    output = output
                });
            }
        }

        public class Formula {
            public static List<Formula> allFormulas = new List<Formula>();

            public Dictionary<string, long> input { get; set; }
            public KeyValuePair<string, long> output { get; set; }
            public int stage { get; set; } = 0;

            public void CalculateStagesHelp() {
                stage = 1;
                foreach (var c in input)
                    if (c.Key != "ORE") {
                        Formula r = allFormulas.Where(x => x.output.Key == c.Key).First();
                        if (r.stage == 0)
                            r.CalculateStagesHelp();
                        if (stage <= r.stage) {
                            stage = r.stage + 1;
                        }
                    }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace AOCDay141 {
    class Component {
        public int quantity;
        public string chemical;

        public Component(int q, string c) {
            quantity = q;
            chemical = c;
        }
        public Component(string q, string c) {
            quantity = Int32.Parse(q);
            chemical = c;
        }
    }

    class Reaction {
        public List<Component> components;
        public Component result;
        public int stage = 0;

        public static Dictionary<string, Reaction> AllReactions;
        public static List<Reaction>[] Stages;

        public Reaction() {
            components = new List<Component>();
        }

        private void CalculateStagesHelp() {
            stage = 1;
            foreach (Component c in components)
                if (c.chemical != "ORE") {
                    Reaction r = AllReactions[c.chemical];
                    if (r.stage == 0)
                        r.CalculateStagesHelp();
                    if (stage <= r.stage) {
                        stage = r.stage + 1;
                    }
                }
        }

        public static void CalculateStages() {
            Reaction finalReaction = AllReactions["FUEL"];
            finalReaction.CalculateStagesHelp();
            int lastStage = finalReaction.stage;

            Stages = new List<Reaction>[lastStage];
            for (int i = 0; i < lastStage; i++)
                Stages[i] = new List<Reaction>();
            foreach (Reaction r in Reaction.AllReactions.Values) {
                Stages[r.stage - 1].Add(r);
            }
        }

    }

    class MainClass {

        public static Int64 GetOreNeeded(Int64 fuel) {
            Dictionary<string, Int64> ingredients = new Dictionary<string, Int64>();
            foreach (Reaction r in Reaction.AllReactions.Values) {
                ingredients[r.result.chemical] = 0;
            }
            ingredients["FUEL"] = fuel;
            ingredients["ORE"] = 0;

            int lastStage = Reaction.AllReactions["FUEL"].stage;

            for (int i = lastStage; i > 0; i--) {
                foreach (Reaction r in Reaction.Stages[i - 1]) {
                    Int64 total = ingredients[r.result.chemical];
                    Int64 unit = r.result.quantity;
                    Int64 quantity = total / unit;
                    if (total % unit > 0) quantity++;
                    foreach (Component c in r.components) {
                        ingredients[c.chemical] += c.quantity * quantity;
                    }
                }
            }

            return ingredients["ORE"];
        }

        public static void Main(string[] args) {
            Reaction.AllReactions = new Dictionary<string, Reaction>();
            //string line;
            Regex re = new Regex(@"(\d+) ([A-Z]+)");
            //while ((line = Console.ReadLine()) != null) {
            foreach (var line in File.ReadAllLines("Input.txt")) {
                Reaction r = new Reaction();
                foreach (Match match in re.Matches(line)) {
                    int count = match.Groups.Count;
                    for (int i = 0; i < count; i += 3) {
                        Component c = new Component(match.Groups[i + 1].Value, match.Groups[i + 2].Value);
                        r.components.Add(c);
                    }
                }
                r.result = r.components[r.components.Count - 1];
                r.components.RemoveAt(r.components.Count - 1);
                Reaction.AllReactions[r.result.chemical] = r;
            }
            //}

            Reaction.CalculateStages();

            Int64 orePerFuel = GetOreNeeded(1);
            Console.WriteLine(orePerFuel);

            Int64 ore = 1000000000000;
            Int64 lowerBound = ore / orePerFuel;
            Int64 upperBound = 1000 * 2;

            while (GetOreNeeded(upperBound) <= ore)
                upperBound *= 2;
            while (upperBound > lowerBound + 1) {
                Int64 guess = (upperBound + lowerBound) / 2;
                if (GetOreNeeded(guess) <= ore)
                    lowerBound = guess;
                else
                    upperBound = guess;
            }

            Console.WriteLine(lowerBound);


        }
    }
}


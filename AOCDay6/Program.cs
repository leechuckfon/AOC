using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOCDay6 {
    class Program {
    private static Dictionary<string, List<Orbital>> PlanetWithOrbits;
        private static int FindPlanets(string PlanetName, Dictionary<string, List<Orbital>> PlanetWithOrbits, int index) {
            index++;
            var sum = 0;
            if (PlanetWithOrbits.ContainsKey(PlanetName)) {
                foreach (var orbital in PlanetWithOrbits[PlanetName]) {
                    sum += FindPlanets(orbital.Name, PlanetWithOrbits, index);
                }
                return sum + index;
            } else {
                return index;
            }

        }
        private class FoundSpaceDickOrMe {
            public int Count { get; set; }
            public bool Found { get; set; }
        }

        private static FoundSpaceDickOrMe FindDistance(string PlanetName, Dictionary<string, List<Orbital>> PlanetWithOrbits) {
            var test = new List<FoundSpaceDickOrMe>();
            if (PlanetWithOrbits.ContainsKey(PlanetName)) {
                if (PlanetWithOrbits[PlanetName].Find(item => item.Name == "SAN" || item.Name == "YOU") != null) {
                    return new FoundSpaceDickOrMe() { Count = 1, Found = true };
                } else {
                    foreach (var orbital in PlanetWithOrbits[PlanetName]) {
                        test.Add(FindDistance(orbital.Name, PlanetWithOrbits));
                    }
                    var lol = test.FindAll(item => item.Found == true);
                    if (lol.Count == 2) {
                        Console.WriteLine(lol.Sum(item => item.Count));
                    } else if (lol.Count == 1) {
                        var returnWaarde = lol.First();
                        returnWaarde.Count += 1;
                        return returnWaarde;
                    } else {
                        return new FoundSpaceDickOrMe() { Count = 0, Found = false };
                    }
                    return new FoundSpaceDickOrMe() { Count = 0, Found = false };
                }
            }
            return new FoundSpaceDickOrMe() { Count = 0, Found = false };
        }
        static void Main(string[] args) {
            PlanetWithOrbits =  new Dictionary<string, List<Orbital>>();
            var allOrbits = File.ReadAllLines("input.txt");
            foreach (var orbit in allOrbits) {
                var planetAndOrbitSplit = orbit.Split(')');
                var planet = planetAndOrbitSplit[0];
                var orbital = planetAndOrbitSplit[1];
                if (!PlanetWithOrbits.ContainsKey(planet)) {
                    PlanetWithOrbits.Add(planet, new List<Orbital>());
                }
                PlanetWithOrbits[planet].Add(new Orbital(orbital, OrbitType.DIRECT));

              
            }

            var sum = 0;
            sum += FindPlanets("COM", PlanetWithOrbits,-1);
            FindDistance("COM", PlanetWithOrbits);
            Console.WriteLine(sum);
        }
    }

    

    public class Planet {
        public string Name { get; set; }
        public List<Planet> Orbital { get; set; }
    }

    public class Orbital {
        public string Name { get; set; }
        public OrbitType OrbitType { get; set; }

        public Orbital(string name, OrbitType orbitType) {
            Name = name;
            OrbitType = orbitType;
        }
    }

    public enum OrbitType {
        INDIRECT,
        DIRECT
    }
}

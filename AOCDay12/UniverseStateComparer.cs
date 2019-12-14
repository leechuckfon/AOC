using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AOCDay12 {
    internal class UniverseStateComparer : IEqualityComparer<UniverseState> {
        public bool Equals([AllowNull] UniverseState x, [AllowNull] UniverseState y) {
            var same = true;
            for (int i=0;i<x.StatesOfPlanets.Count;i++) {
                if (x.StatesOfPlanets[i].Gravity.x != y.StatesOfPlanets[i].Gravity.x) {
                    same = false;
                }
                if (x.StatesOfPlanets[i].Gravity.y != y.StatesOfPlanets[i].Gravity.y) {
                    same = false;
                }
                if (x.StatesOfPlanets[i].Gravity.z != y.StatesOfPlanets[i].Gravity.z) {
                    same = false;
                }
            }
            return same;
        }

        public int GetHashCode([DisallowNull] UniverseState obj) {
            var sum = 0;
            sum += obj.StatesOfPlanets.Select(x => x.Gravity.x).Aggregate((a, b) => a += Math.Abs(b));
            sum += obj.StatesOfPlanets.Select(x => x.Gravity.y).Aggregate((a, b) => a += Math.Abs(b));
            sum += obj.StatesOfPlanets.Select(x => x.Gravity.z).Aggregate((a, b) => a += Math.Abs(b));
            return sum;
        }
    }
}
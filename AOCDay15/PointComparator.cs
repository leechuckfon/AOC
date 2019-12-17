using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AOCDay5 {
    internal class PointComparator : IEqualityComparer<Program.Point> {
        public bool Equals([AllowNull] Program.Point x, [AllowNull] Program.Point y) {
            return x.x == y.x && x.y == y.y;
        }

        public int GetHashCode([DisallowNull] Program.Point obj) {
            return (int)obj.x + (int)obj.y;
        }
    }
}
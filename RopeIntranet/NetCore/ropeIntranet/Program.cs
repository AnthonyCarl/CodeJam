using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Console;

namespace RopeIntranet
{
    public static class Program
    {
        static void Main()
        {
            if (!IsInputRedirected)
            {
                WriteLine("Please pipe in data file.");
                return;
            }

            using (var inStream = new StreamReader(OpenStandardInput(), Encoding.ASCII))
            {
                using (var outStream = new StreamWriter(OpenStandardOutput(), Encoding.ASCII))
                {
                    var numberOfCases = int.Parse(inStream.ReadLine());
                    foreach (var caseNumber in Enumerable.Range(1, numberOfCases))
                    {
                        var numberOfWires = int.Parse(inStream.ReadLine());
                        var ropes = Enumerable.Range(0, numberOfWires)
                            .Select(_ => inStream.ReadLine().Split())
                            .Select(pair => Tuple.Create(int.Parse(pair[0]), int.Parse(pair[1])))
                            .ToList();
                        var intersectionsCount = GetIntersectionsCount(ropes);
                        outStream.WriteLine($"Case #{caseNumber}: {intersectionsCount}");
                    }
                }
            }
        }

        public static int GetIntersectionsCount(List<Tuple<int, int>> ropes)
        {
            return ropes
                .Select((rope, i) => ropes.Skip(i + 1).Count(rope.Intersects))
                .Sum();
        }

        static bool Intersects(this Tuple<int, int> thisWire, Tuple<int, int> thatWire)
        {
            return (thisWire.Item1 - thatWire.Item1) * (thisWire.Item2 - thatWire.Item2) < 0;
        }
    }
}

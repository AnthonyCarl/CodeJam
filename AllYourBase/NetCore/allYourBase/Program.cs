using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using static System.Console;
using static System.Math;
using static System.Numerics.BigInteger;

namespace AllYourBase
{
    public class Program
    {
        static void Main()
        {
            var inputLines = new List<string>();
            if (!IsInputRedirected)
            {
                WriteLine("Please pipe in data file.");
                return;
            }

            using (var inStream = new StreamReader(OpenStandardInput(), Encoding.ASCII))
            {
                string line;
                while ((line = inStream.ReadLine()) != null)
                {
                    inputLines.Add(line);
                }
            }

            var numberOfCases = int.Parse(inputLines.First());

            using (var outStream = new StreamWriter(OpenStandardOutput(), Encoding.ASCII))
            {
                for (int caseNumber = 1; caseNumber <= numberOfCases; caseNumber++)
                {
                    var result = GetMinimumDenaryValue(inputLines[caseNumber]);
                    outStream.WriteLine($"Case #{caseNumber}: {result}");
                }
            }
        }

        const int MinNumberBase = 2;
        public static BigInteger GetMinimumDenaryValue(string valueWithUnknownBase)
        {
            var digitMap = new Dictionary<char, int?>();
            foreach (var digit in valueWithUnknownBase)
            {
                digitMap[digit] = null;
            }

            var numberBase = new BigInteger(Max(MinNumberBase, digitMap.Keys.Count));
            var nextAvailableDigitValue = 0;

            var result = Zero;
            var exp = valueWithUnknownBase.Length - 1;

            foreach (var digit in valueWithUnknownBase)
            {
                var digitValue = digitMap[digit];
                if (digitValue == null)
                {
                    switch (nextAvailableDigitValue)
                    {
                        case 0:
                            digitValue = 1;
                            break;
                        case 1:
                            digitValue = 0;
                            break;
                        default:
                            digitValue = nextAvailableDigitValue;
                            break;
                    }
                    digitMap[digit] = digitValue;
                    nextAvailableDigitValue++;
                }
                result += Pow(numberBase, exp)*digitValue.Value;
                exp--;
            }
            return result;
        }
    }
}


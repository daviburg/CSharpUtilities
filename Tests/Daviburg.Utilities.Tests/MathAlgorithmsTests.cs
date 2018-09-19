// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathAlgorithmsTests.cs" company="Microsoft">
//   This file is part of Daviburg Utilities.
//
//   Daviburg Utilities is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   Daviburg Utilities is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with Daviburg Utilities.  If not, see <see href="https://www.gnu.org/licenses"/>.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Daviburg.Utilities.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Numerics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MathAlgorithmsTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void GreatestCommonDivisorTests()
        {
            Assert.AreEqual(0, MathAlgorithms.GreatestCommonDivisor(0, 0));
            Assert.AreEqual(123, MathAlgorithms.GreatestCommonDivisor(0, 123));
            Assert.AreEqual(123, MathAlgorithms.GreatestCommonDivisor(123, 0));
            Assert.AreEqual(1, MathAlgorithms.GreatestCommonDivisor(3, 7));
            Assert.AreEqual(1, MathAlgorithms.GreatestCommonDivisor(3, 7));
            Assert.AreEqual(1, MathAlgorithms.GreatestCommonDivisor(7, 3));
            Assert.AreEqual(1, MathAlgorithms.GreatestCommonDivisor(6, 35));
            Assert.AreEqual(1, MathAlgorithms.GreatestCommonDivisor(35, 6));
            Assert.AreEqual(12, MathAlgorithms.GreatestCommonDivisor(24, 36));
            Assert.AreEqual(12, MathAlgorithms.GreatestCommonDivisor(36, 24));
            Assert.AreEqual(21, MathAlgorithms.GreatestCommonDivisor(1071, 462));
            Assert.AreEqual(21, MathAlgorithms.GreatestCommonDivisor(462, 1071));
            Assert.AreEqual(151, MathAlgorithms.GreatestCommonDivisor(163231, 152057));
            Assert.AreEqual(151, MathAlgorithms.GreatestCommonDivisor(163231, 135749));
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void PowerModuloTests()
        {
            Assert.AreEqual(1, MathAlgorithms.PowerModulo(100, 0, 10));
            Assert.AreEqual(0, MathAlgorithms.PowerModulo(4, 6, 4));
            Assert.AreEqual(4, MathAlgorithms.PowerModulo(4, 6, 6));
            Assert.AreEqual(8, MathAlgorithms.PowerModulo(5, 7, 13));
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void BigIntegerTests()
        {
            var bigInteger = @"37107287533902102798797998220837590246510135740250
46376937677490009712648124896970078050417018260538
74324986199524741059474233309513058123726617309629
91942213363574161572522430563301811072406154908250
23067588207539346171171980310421047513778063246676
89261670696623633820136378418383684178734361726757
28112879812849979408065481931592621691275889832738
44274228917432520321923589422876796487670272189318
47451445736001306439091167216856844588711603153276
70386486105843025439939619828917593665686757934951
62176457141856560629502157223196586755079324193331
64906352462741904929101432445813822663347944758178
92575867718337217661963751590579239728245598838407
58203565325359399008402633568948830189458628227828
80181199384826282014278194139940567587151170094390
35398664372827112653829987240784473053190104293586
86515506006295864861532075273371959191420517255829
71693888707715466499115593487603532921714970056938
54370070576826684624621495650076471787294438377604
53282654108756828443191190634694037855217779295145
36123272525000296071075082563815656710885258350721
45876576172410976447339110607218265236877223636045
17423706905851860660448207621209813287860733969412
81142660418086830619328460811191061556940512689692
51934325451728388641918047049293215058642563049483
62467221648435076201727918039944693004732956340691
15732444386908125794514089057706229429197107928209
55037687525678773091862540744969844508330393682126
18336384825330154686196124348767681297534375946515
80386287592878490201521685554828717201219257766954
78182833757993103614740356856449095527097864797581
16726320100436897842553539920931837441497806860984
48403098129077791799088218795327364475675590848030
87086987551392711854517078544161852424320693150332
59959406895756536782107074926966537676326235447210
69793950679652694742597709739166693763042633987085
41052684708299085211399427365734116182760315001271
65378607361501080857009149939512557028198746004375
35829035317434717326932123578154982629742552737307
94953759765105305946966067683156574377167401875275
88902802571733229619176668713819931811048770190271
25267680276078003013678680992525463401061632866526
36270218540497705585629946580636237993140746255962
24074486908231174977792365466257246923322810917141
91430288197103288597806669760892938638285025333403
34413065578016127815921815005561868836468420090470
23053081172816430487623791969842487255036638784583
11487696932154902810424020138335124462181441773470
63783299490636259666498587618221225225512486764533
67720186971698544312419572409913959008952310058822
95548255300263520781532296796249481641953868218774
76085327132285723110424803456124867697064507995236
37774242535411291684276865538926205024910326572967
23701913275725675285653248258265463092207058596522
29798860272258331913126375147341994889534765745501
18495701454879288984856827726077713721403798879715
38298203783031473527721580348144513491373226651381
34829543829199918180278916522431027392251122869539
40957953066405232632538044100059654939159879593635
29746152185502371307642255121183693803580388584903
41698116222072977186158236678424689157993532961922
62467957194401269043877107275048102390895523597457
23189706772547915061505504953922979530901129967519
86188088225875314529584099251203829009407770775672
11306739708304724483816533873502340845647058077308
82959174767140363198008187129011875491310547126581
97623331044818386269515456334926366572897563400500
42846280183517070527831839425882145521227251250327
55121603546981200581762165212827652751691296897789
32238195734329339946437501907836945765883352399886
75506164965184775180738168837861091527357929701337
62177842752192623401942399639168044983993173312731
32924185707147349566916674687634660915035914677504
99518671430235219628894890102423325116913619626622
73267460800591547471830798392868535206946944540724
76841822524674417161514036427982273348055556214818
97142617910342598647204516893989422179826088076852
87783646182799346313767754307809363333018982642090
10848802521674670883215120185883543223812876952786
71329612474782464538636993009049310363619763878039
62184073572399794223406235393808339651327408011116
66627891981488087797941876876144230030984490851411
60661826293682836764744779239180335110989069790714
85786944089552990653640447425576083659976645795096
66024396409905389607120198219976047599490197230297
64913982680032973156037120041377903785566085089252
16730939319872750275468906903707539413042652315011
94809377245048795150954100921645863754710598436791
78639167021187492431995700641917969777599028300699
15368713711936614952811305876380278410754449733078
40789923115535562561142322423255033685442488917353
44889911501440648020369068063960672322193204149535
41503128880339536053299340368006977710650566631954
81234880673210146739058568557934581403627822703280
82616570773948327592232845941706525094512325230608
22918802058777319719839450180888072429661980811197
77158542502016545090413245809786882778948721859617
72107838435069186155435662884062257473692284509516
20849603980134001723930671666823555245252804609722
53503534226472524250874054075591789781264330331690"
                .Split('\n')
                .Select(numberInString => BigInteger.Parse(numberInString))
                .Aggregate((partialSum, bigNumber) => BigInteger.Add(bigNumber, partialSum))
                .ToString()
                .Substring(startIndex: 0, length: 10);

            Console.WriteLine($"The secret value is simply {bigInteger}");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void BinomialCoefficientTests()
        {
            // At collection size 30 the partial facorial results in an overflow in the combinations method.
            for (var collectionSize = 1; collectionSize < 30; collectionSize++)
            {
                for (var itemsTaken = 1; itemsTaken <= collectionSize; itemsTaken++)
                {
                    Assert.AreEqual(
                        MathAlgorithms.Combinations(collectionSize, itemsTaken),
                        MathAlgorithms.BinomialCoefficient(collectionSize, itemsTaken),
                        $"Combinations output {MathAlgorithms.Combinations(collectionSize, itemsTaken)} differs from BinomialCoefficient output {MathAlgorithms.BinomialCoefficient(collectionSize, itemsTaken)} for collectionSize {collectionSize} and itemsTaken {itemsTaken}.");
                }
            }
            
            Console.WriteLine($"The number of unique North-East paths in a 20x20 lattice is {MathAlgorithms.BinomialCoefficient(40, 20)}.");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void PowerOfTwoDigitsSummationTests()
        {
            Console.WriteLine($"The sum of digits of the number 2 power 1000 is {(new BigInteger(1) << 1000).ToString().Aggregate(seed: 0, func: (partialSummation, digit) => partialSummation + digit.ToInt32())}.");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void IntegerQuotientOfPowerOfTwoDigitsSummationTests()
        {
            var sequence = new List<int>();
            for (var integer = 0; integer <= 1000; integer++)
            {
                sequence.Add((new BigInteger(1) << integer).ToString().Aggregate(seed: 0, func: (partialSummation, digit) => partialSummation + digit.ToInt32()) / 9);
            }

            Console.WriteLine($"The terms of the sequence of integer quotient of sum of digits of 2^n are {string.Join(", ", sequence)}.");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void SundayTests()
        {
            // Yes, you could do the math and yes it *may* run faster... At 20ms in chk build, that is a waste of coding time.
            Console.WriteLine($"There were {Enumerable.Range(start: 1901, count: 100).SelectMany(year => Enumerable.Range(start: 1, count: 12).Where(month => new DateTime(year: year, month: month, day: 1).DayOfWeek == DayOfWeek.Sunday)).Count()} Sundays on the first of the month in the twentieth century.");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void LargeFactorialSummationTests()
        {
            Console.WriteLine($"The sum of digits of the number factorial 100 is {Enumerable.Range(start: 3, count: 97).Aggregate(seed: new BigInteger(2), func: (current, multiplicator) => current * multiplicator).ToString().Aggregate(seed: 0, func: (partialSummation, digit) => partialSummation + digit.ToInt32())}.");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void WeighingOrderedNamesTest()
        {
            // @ is the character preceding A in the ascii table, which is valid also for Unicode Latin character range.
            var sumOfNameScores = new SortedDictionary<string, int>(
                dictionary: File
                    .ReadAllText(@".\TestData\p022_names.txt")
                    .Split(new[] { ',' })
                    .Select(quotedName => quotedName.Trim(new[] { '"' }))
                    .ToDictionary(name => name, name => name.Aggregate(seed: 0, func: (sum, character) => sum + (character - '@'))))
                .Aggregate(seed: new Tuple<int, int>(1, 0), func: (indexAndSum, nameAndWorth) => new Tuple<int, int>(indexAndSum.Item1 + 1, indexAndSum.Item2 + nameAndWorth.Value * indexAndSum.Item1))
                .Item2;

            Console.WriteLine($"The total of all the name scores is {sumOfNameScores}.");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void PermutationsTests()
        {
            var goal = (long)1000000;
            var countOfDigits = 10;
            var remainingPermutations = goal;
            var permutationsByOffset = new int[countOfDigits];

            while (remainingPermutations > 1)
            {
                var offset = 1;
                while (Factorials.Singleton[offset + 1] < remainingPermutations)
                {
                    offset++;
                }

                var permutationCountAtOffset = (int)((remainingPermutations - 1) / Factorials.Singleton[offset]);
                permutationsByOffset[offset] = permutationCountAtOffset;
                remainingPermutations -= permutationCountAtOffset * Factorials.Singleton[offset];
            }

            // NOTE: this could be simplified by turning it to a string right away, and pick the digits immediately in the previous loop instead.
            var availableDigits = Enumerable.Range(start: 0, count: countOfDigits).ToList();
            var nthPermutation = string.Empty;
            for (var index = 1; index <= countOfDigits; index++)
            {
                var digitAtIndex = availableDigits[permutationsByOffset[countOfDigits - index]];
                availableDigits.Remove(digitAtIndex);
                nthPermutation += digitAtIndex.ToString();
            }

            Console.WriteLine($"The {goal}th permutation of 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9 is {nthPermutation}.");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void LongestRecurringCycleTests()
        {
            // This is the same as looking for the largest long period prime below 1k, see https://oeis.org/A006883
            Console.WriteLine($"The longest recurring cycle is for the unit fraction 1/{Enumerable.Range(1, 999).Reverse().First(divident => ((long)divident).IsPrime() && (divident.FractionDecimalPeriodLength() == divident - 1))}.");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void PrimeValueFamilyTest()
        {
            var primeIndex = 0;
            var familySize = 0;
            for (; familySize != 8; primeIndex++)
            {
                var prime = Primes.Singleton[primeIndex];
                var size = Convert.ToInt32(Math.Ceiling(Math.Log10(prime + 1)));

                // The possible replacement spots for the prime value family can be expressed as a bitmask,
                // i.e. for a prime of 3 digits the possible replacement spots are 001, 010, 011, 100, etc...
                // The count of them been 2^n - 1.
                var combinationsCount = (1 << size) - 1;
                for (var bitmask = 1; bitmask <= combinationsCount && familySize != 8; bitmask++)
                {
                    // The source prime must have the same digit at all positions replaced
                    var validBitmask = true;
                    var multiplier = 1;
                    var commonDigit = -1;
                    for (var maskPosition = bitmask; maskPosition != 0; maskPosition >>= 1, multiplier *= 10)
                    {
                        if ((maskPosition & 1) == 1)
                        {
                            var digitAtPosition = Convert.ToInt32((prime / multiplier) % 10);
                            commonDigit = (commonDigit == -1) ? digitAtPosition : commonDigit;
                            if (commonDigit != digitAtPosition)
                            {
                                validBitmask = false;
                                break;
                            }
                        }
                    }

                    if (!validBitmask)
                    {
                        continue;
                    }

                    ////Console.WriteLine($"Begin bitmask {bitmask}.");
                    familySize = 0;

                    for (var replacementDigit = 0; replacementDigit < 10; replacementDigit++)
                    {
                        // Special case: the first digit is not allowed to be replaced by 0, as the problem description
                        // first example excludes 03 prime number from 2-digit number *3, i.e. the count of digits must remain the same
                        if (replacementDigit == 0 && (bitmask & (1 << (size - 1))) != 0)
                        {
                            continue;
                        }

                        var testValue = prime;
                        multiplier = 1;
                        for (var maskPosition = bitmask; maskPosition != 0; maskPosition >>= 1, multiplier *= 10)
                        {
                            if ((maskPosition & 1) == 1)
                            {
                                // Replace the digit at the position
                                testValue += (replacementDigit - ((testValue / multiplier) % 10)) * multiplier;
                            }
                        }

                        if (testValue.IsPrime())
                        {
                            familySize++;
                            ////Console.WriteLine($"{testValue}");
                        }
                    }

                    ////Console.WriteLine();
                }
            }
            
            Console.WriteLine($"The first prime with a family of 8 is {Primes.Singleton[primeIndex - 1]} for index {primeIndex - 1}.");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void QuadraticPrimesTest()
        {
            var maximumQuadraticPrimes = 40;
            var abCoefficentProduct = 41;
            for (var coefficientA = -999; coefficientA < 1000; coefficientA++)
            {
                for (var coefficientB = -1000; coefficientB <= 1000; coefficientB++)
                {
                    var n = 0;
                    for (; (((long)n * n) + coefficientA * n + coefficientB).IsPrime(); n++)
                    {
                    }

                    if (n > maximumQuadraticPrimes)
                    {
                        maximumQuadraticPrimes = n;
                        abCoefficentProduct = coefficientA * coefficientB;
                    }
                }
            }

            Console.WriteLine($"The product of coefficients that produces the maximum number of quadratic primes is {abCoefficentProduct} for {maximumQuadraticPrimes} primes.");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void SumOfDiagonalsFromIntegerSpiral()
        {
            // This one really didn't need any program, just recording for the sake of it.
            var n = 1001 / 2;
            Console.WriteLine($"The sum of the diagonals in a 1001 by 1001 spiral of integers is {1 + 10 * n * n + (16 * n * n * n + 26 * n) / 3 }.");
        }
    }
}

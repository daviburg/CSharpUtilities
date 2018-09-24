// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathAlgorithms2.cs" company="Microsoft">
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

namespace Daviburg.Utilities2
{
    using Daviburg.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public static class MathAlgorithms2
    {
        /// <summary>
        /// Searches the starting number less that the provided limit which produces the longest Collatz chain.
        /// </summary>
        /// <param name="upperLimit">The exclusive upper search limit value.</param>
        /// <returns>The tuple starting number and length of the series.</returns>
        public static Tuple<int, int> LongestCollatzSequenceStartingNumber(int upperLimit)
        {
            var collatzSequenceLength = new AutoResizeArray<int>(3 * upperLimit);
            var maximumLength = new Tuple<int, int>(1, collatzSequenceLength[1] = 1);
            for (var startingNumber = 2; startingNumber < upperLimit; startingNumber++)
            {
                // If we've seen this number before in a sequence it won't be the start of a longer sequence and can be skipped over.
                if (collatzSequenceLength[startingNumber] != default(int))
                {
                    continue;
                }

                var collatzSequenceFragment = new Queue<int>();
                var adjustment = default(int);

                // Walk the sequence until we reach an element we've seen before, such as 1
                for (var currentValue = startingNumber;
                    adjustment == default(int);
                    adjustment = collatzSequenceLength[currentValue = currentValue.CollatzSequenceNext()])
                {
                    // Stack this element of the Collatz fragment, element we haven't seen before
                    collatzSequenceFragment.Enqueue(currentValue);
                }

                // Capture the new maximum length if found
                maximumLength = maximumLength.Item2 >= collatzSequenceFragment.Count + adjustment ? maximumLength : new Tuple<int, int>(startingNumber, collatzSequenceFragment.Count + adjustment);

                for(; collatzSequenceFragment.Count != 0; collatzSequenceFragment.Dequeue())
                {
                    collatzSequenceLength[collatzSequenceFragment.Peek()] = collatzSequenceFragment.Count + adjustment;
                }
            }

            return maximumLength;
        }

        /// <summary>
        /// Searches the starting number less that the provided limit which produces the longest Collatz chain.
        /// </summary>
        /// <param name="upperLimit">The exclusive upper search limit value.</param>
        /// <returns>The tuple starting number and length of the series.</returns>
        public static Tuple<int, int> LongestCollatzSequenceStartingNumber2(int upperLimit)
        {
            // Although the start number is a 32 bits integer, because odd numbers in the sequence can increase intermediary value wildly,
            // sequences can have values exceeding 32 bits capacity, hence keep track of known length with a 64 bits integer key.
            var collatzSequenceLength = new Dictionary<long, int>(3 * upperLimit);
            var maximumLength = new Tuple<int, int>(1, collatzSequenceLength[1] = 1);

            // Longer sequences are within the higher half starting numbers as if we can double this number while staying within the limit,
            // that starting number will result in a longer chain.
            // Also avoid starting with less than 2.
            for (var startingNumber = upperLimit > 6 ? (upperLimit - 1) >> 1 : 2; startingNumber < upperLimit; startingNumber++)
            {
                // If we've seen this number before in a sequence it won't be the start of a longer sequence and can be skipped over.
                if (collatzSequenceLength.TryGetValue(startingNumber, out int sequenceFragmentLength))
                {
                    continue;
                }

                var collatzSequenceFragment = new Queue<long>();
                var adjustment = default(int);

                // Walk the sequence until we reach an element we've seen before, such as 1
                for (long currentValue = startingNumber;
                    adjustment == default(int);
                    collatzSequenceLength.TryGetValue(currentValue = currentValue.CollatzSequenceNext(), out adjustment))
                {
                    // Stack this element of the Collatz fragment, element we haven't seen before
                    collatzSequenceFragment.Enqueue(currentValue);
                }

                // Capture the new maximum length if found
                maximumLength = maximumLength.Item2 >= collatzSequenceFragment.Count + adjustment ? maximumLength : new Tuple<int, int>(startingNumber, collatzSequenceFragment.Count + adjustment);

                for (; collatzSequenceFragment.Count != 0; collatzSequenceFragment.Dequeue())
                {
                    collatzSequenceLength[collatzSequenceFragment.Peek()] = collatzSequenceFragment.Count + adjustment;
                }
            }

            return maximumLength;
        }

        public static long SumOfPowerDigitsSumEquals(int exponent)
        {
            long sumOfEqualPowerDigitsSum = 0;

            Parallel.For<long>(
                fromInclusive: 2,
                toExclusive: (PowerDigitsSingletons.Singleton[exponent][9].Log10() + 2) * PowerDigitsSingletons.Singleton[exponent][9],
                localInit: () => 0,
                body: (index, loop, subtotal) =>
                    subtotal += index.PowerDigitsSum(exponent) == index ? index : (long)0,
                localFinally: x =>
                {
                    if (x != 0)
                    {
                        Interlocked.Add(ref sumOfEqualPowerDigitsSum, x);
                    }
                });

            return sumOfEqualPowerDigitsSum;
        }
    }
}

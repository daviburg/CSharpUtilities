// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HighComposites.cs" company="Microsoft">
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

namespace Daviburg.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Helper class to incrementally discover small high composite numbers (HCNs) aka anti-primes.
    /// </summary>
    public partial class HighComposites
    {

        private static readonly Lazy<HighComposites> HighCompositesInstance = new Lazy<HighComposites>(() => new HighComposites());
        ////private long[] highComposites = { 1, 2, 4, 6, 12, 24, 36, 48 };
        ////private long[] divisorsCounts = { 1, 2, 3, 4, 6, 8, 9, 10 };
        private readonly List<HighComposite> highComposites = new List<HighComposite> { new HighComposite(new List<int> { 0 } ) };

        // Seed the binary tree with 2^1. Because we only look at the 'tree' from each generation of nodes we don't keep a tree structure in memory, just lists.
        private List<HighComposite> leafNodeCandidates = new List<HighComposite> { new HighComposite(new List<int> { 1 }) };

        // Staged candidates are candidate HCNs which we haven't decided yet if they are actual HCN.
        private List<HighComposite> stagedCandidates = new List<HighComposite> { new HighComposite(new List<int> { 1 }) };

        public static HighComposites Singleton => HighCompositesInstance.Value;

        /// <summary>
        /// Prevents construction of instances by others, enforcing singleton pattern so HCNs are only computed once.
        /// </summary>
        private HighComposites()
        {
        }

        public HighComposite this[int index]
        {
            get
            {
                if (index < this.highComposites.Count)
                {
                    return this.highComposites[index];
                }

                this.FindHcns(index + 1);
                return this.highComposites[index];
            }
        }

        public void FindHcns(int orderOfHcnToFind)
        {
            do
            {
                // Generate the next round of leaves
                leafNodeCandidates = leafNodeCandidates
                    .SelectMany(
                        selector: existingCandidate =>
                        {
                            var newNodes = new List<HighComposite>();

                            // Always add a right node with a new exponent for one more prime
                            var rightNode = new HighComposite(
                                new List<int>(existingCandidate.Exponents)
                                {
                                    1
                                });

                            // ... if it doesn't overflow.
                            if (!rightNode.Overflows)
                            {
                                newNodes.Add(rightNode);
                            }

                            // Prepare a left node incrementing the last exponent but add it only if it doesn't exceed the previous exponent.
                            // (Or there is only one exponent.)
                            // This because HCN must satisfy the condition that the sequence of exponents is non-increasing.
                            var newLeftList = new List<int>(existingCandidate.Exponents);
                            newLeftList[newLeftList.Count - 1]++;
                            var leftNode = new HighComposite(newLeftList);

                            if ((newLeftList.Count == 1 || newLeftList[newLeftList.Count - 2] >= newLeftList[newLeftList.Count - 1]) &&
                                !leftNode.Overflows)
                            {
                                newNodes.Add(leftNode);
                            }

                            return newNodes;
                        })
                    .ToList();

                stagedCandidates.AddRange(leafNodeCandidates);

                // 1. Of the new leaves, order them by smallest to largest value
                leafNodeCandidates.Sort();

                // 2. Any staged candidate of even lesser value (and greater count of divisors than other less values) is a confirmed HCN
                // as we won't be able to generate any further candidate of lesser value.
                // Remove confirmed HCNs from the staged list.
                // Also remove staged candidates which count of divisors is less than the best confirmed HCN or better candidates.
                // Using a for iteration with index so we can modify the list as we walk it, patching the index along the way.
                stagedCandidates.Sort();
                int currentHighestCountOfDivisors = this.highComposites.Last().CountOfDivisors;
                for (int index = 0; index < stagedCandidates.Count; index++)
                {
                    HighComposite candidate = stagedCandidates[index];

                    // Filter out higher value candidates which count of divisors is less or equal than best confirmed HCN or lower candidates
                    if (candidate.CountOfDivisors <= currentHighestCountOfDivisors)
                    {
                        stagedCandidates.RemoveAt(index);
                        index--;
                        continue;
                    }

                    currentHighestCountOfDivisors = candidate.CountOfDivisors;

                    // We already know this candidate has a higher count of divisors. Is it also of lower value than the new generation of leaves?
                    if (candidate.Value <= leafNodeCandidates.First().Value)
                    {
                        this.highComposites.Add(candidate);
                        stagedCandidates.RemoveAt(index);
                        index--;
                    }
                }
            } while (this.highComposites.Count < orderOfHcnToFind);
        }
    }
}

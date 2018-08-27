// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryDirectedAcyclicGraphTests.cs" company="Microsoft">
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

namespace Daviburg.Utilities2.Tests
{
    using Daviburg.Utilities2.Graphs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BinaryDirectedAcyclicGraphTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void BinaryDirectedAcyclicGraphLoadFromFileTest()
        {
            var graphRoot = BinaryDirectedAcyclicGraphNode<int>.LoadFromTextFile(@".\TestData\p067_triangle.txt");
            for (
                var currentNodeList = new List<BinaryDirectedAcyclicGraphNode<int>>() { graphRoot.LeftChild, graphRoot.RightChild };
                currentNodeList.Any(node => node != null);
                currentNodeList = currentNodeList
                    .Aggregate(
                        seed: new List<BinaryDirectedAcyclicGraphNode<int>>(), 
                        func: (runningList, oldNode) => 
                        {
                            if (runningList.Count == 0)
                            {
                                runningList.Add(((BinaryDirectedAcyclicGraphNode<int>)oldNode).LeftChild);
                            }

                            runningList.Add(((BinaryDirectedAcyclicGraphNode<int>)oldNode).RightChild);
                            return runningList;
                        }))
            {
                foreach (var node in currentNodeList)
                {
                    node.Value += node.Parents.Aggregate(0, (currentMax, parentNode) => parentNode.Value > currentMax ? parentNode.Value : currentMax);
                }
            }

            var leftMostLeaf = graphRoot;
            while (leftMostLeaf.LeftChild != null)
            {
                leftMostLeaf = leftMostLeaf.LeftChild;
            }

            var max = 0;
            var inspectedLeaf = leftMostLeaf;
            while (inspectedLeaf != null)
            {
                max = inspectedLeaf.Value > max ? inspectedLeaf.Value : max;
                inspectedLeaf = inspectedLeaf.Successor;
            }

            Console.WriteLine($"Maximum total is {max}.");
        }
    }
}

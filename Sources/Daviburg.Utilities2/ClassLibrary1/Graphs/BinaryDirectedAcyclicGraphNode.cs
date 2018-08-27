// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryDirectedAcyclicGraphNode.cs" company="Microsoft">
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

namespace Daviburg.Utilities2.Graphs
{
    using Daviburg.Utilities.Graphs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;

    public class BinaryDirectedAcyclicGraphNode<T> : DirectedGraphNode<T>
    {
        private List<DirectedGraphNode<T>> children;
        private BinaryDirectedAcyclicGraphNode<T> leftChild;
        private BinaryDirectedAcyclicGraphNode<T> rightChild;

        public BinaryDirectedAcyclicGraphNode<T> Predecessor { get; set; }
        public BinaryDirectedAcyclicGraphNode<T> Successor { get; set; }

        /// <summary>
        /// Gets or sets the left child node.
        /// </summary>
        /// <remarks>Setting the child node resets the children list.</remarks>
        public BinaryDirectedAcyclicGraphNode<T> LeftChild
        {
            get => this.leftChild;
            set
            {
                if (value != null)
                {
                    value.Predecessor = this.leftChild?.Predecessor;
                }

                this.leftChild = value;
                value.AddParent(this);

                if (this.rightChild != null)
                {
                    value.Successor = this.rightChild;
                    this.rightChild.Predecessor = value;
                }

                this.children = null;
            }
        }

        /// <summary>
        /// Gets or sets the right child node.
        /// </summary>
        /// <remarks>Setting the child node resets the children list.</remarks>
        public BinaryDirectedAcyclicGraphNode<T> RightChild
        {
            get => this.rightChild;
            set
            {
                if (value != null)
                {
                    value.Successor = this.rightChild?.Successor;
                }

                this.rightChild = value;
                value.AddParent(this);

                if (this.leftChild != null)
                {
                    value.Predecessor = this.leftChild;
                    this.leftChild.Successor = value;
                }

                this.children = null;
            }
        }

        public override List<DirectedGraphNode<T>> Children
        {
            get => this.children ?? (this.children = new List<DirectedGraphNode<T>> { this.LeftChild, this.RightChild });
            set
            {
                this.children = value;

                if (value != null && value.Count > 2)
                {
                    throw new ArgumentException($"Invalid argument to set child nodes for a binary tree node. At most two children must be povided but {value.Count} were passed.");
                }

                this.LeftChild = (BinaryDirectedAcyclicGraphNode<T>)this.children?.ElementAtOrDefault(0);
                this.RightChild = (BinaryDirectedAcyclicGraphNode<T>)this.children?.ElementAtOrDefault(1);
            }
        }

        public static BinaryDirectedAcyclicGraphNode<T> LoadFromTextFile(string path)
        {
            var lines = File.ReadAllLines(path);
            BinaryDirectedAcyclicGraphNode<T> root = null;
            BinaryDirectedAcyclicGraphNode<T> leftMostNode = null;
            foreach (var line in lines)
            {
                if (leftMostNode == null)
                {
                    root = leftMostNode = new BinaryDirectedAcyclicGraphNode<T>() { Value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(line) };
                    continue;
                }

                var childNodes = line.Split(' ').Select(valueAsString => new BinaryDirectedAcyclicGraphNode<T>() { Value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(valueAsString) }).ToList();

                var enumerator = childNodes.GetEnumerator();
                enumerator.MoveNext();
                do
                {
                    leftMostNode.LeftChild = enumerator.Current;

                    if (!enumerator.MoveNext())
                    {
                        throw new ArgumentException($"Invalid text file at '{path}' provided to load as binary directed acyclic graph. Only full binary directed acyclic graph can be loaded.");
                    }

                    leftMostNode.RightChild = enumerator.Current;

                    leftMostNode = (BinaryDirectedAcyclicGraphNode<T>)leftMostNode.Successor;
                } while (leftMostNode != null);

                leftMostNode = childNodes.First();
            }

            return root;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeNode.cs" company="Microsoft">
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

namespace Daviburg.Utilities2.Trees
{
    using Daviburg.Utilities.Trees;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;

    public class BinaryTreeNode<T> : TreeNode<T>
    {
        private List<TreeNode<T>> children;
        private BinaryTreeNode<T> leftChild;
        private BinaryTreeNode<T> rightChild;

        /// <summary>
        /// Gets or sets the left child node.
        /// </summary>
        /// <remarks>Setting the child node resets the children list.</remarks>
        public BinaryTreeNode<T> LeftChild
        {
            get => this.leftChild;
            set
            {
                if (value != null)
                {
                    value.Predecessor = this.leftChild?.Predecessor;
                }

                this.leftChild = value;
                value.Parent = this;

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
        public BinaryTreeNode<T> RightChild
        {
            get => this.rightChild;
            set
            {
                if (value != null)
                {
                    value.Successor = this.rightChild?.Successor;
                }

                this.rightChild = value;
                value.Parent = this;

                if (this.leftChild != null)
                {
                    value.Predecessor = this.leftChild;
                    this.leftChild.Successor = value;
                }

                this.children = null;
            }
        }

        public override List<TreeNode<T>> Children
        {
            get => this.children ?? (this.children = new List<TreeNode<T>> { this.LeftChild, this.RightChild });
            set
            {
                this.children = value;

                if (value != null && value.Count > 2)
                {
                    throw new ArgumentException($"Invalid argument to set child nodes for a binary tree node. At most two children must be povided but {value.Count} were passed.");
                }

                this.LeftChild = (BinaryTreeNode<T>)this.children?.ElementAtOrDefault(0);
                this.RightChild = (BinaryTreeNode<T>)this.children?.ElementAtOrDefault(1);
            }
        }

        public static BinaryTreeNode<T> LoadFromTextFile(string path)
        {
            var lines = File.ReadAllLines(path);
            BinaryTreeNode<T> root = null;
            BinaryTreeNode<T> leftMostNode = null;
            foreach (var line in lines)
            {
                if (leftMostNode == null)
                {
                    root = leftMostNode = new BinaryTreeNode<T>() { Value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(line) };
                    continue;
                }

                var childNodes = line.Split(' ').Select(valueAsString => new BinaryTreeNode<T>() { Value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(valueAsString) }).ToList();

                if ((childNodes.Count & 1) != 0)
                {
                    throw new ArgumentException($"Invalid text file at '{path}' provided to load as binary tree. Only full binary tree can be loaded.");
                }

                BinaryTreeNode<T> previousChild = null;
                var enumerator = childNodes.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    leftMostNode.LeftChild = enumerator.Current;
                    enumerator.MoveNext();
                    leftMostNode.RightChild = enumerator.Current;

                    if (previousChild != null)
                    {
                        previousChild.Successor = leftMostNode.LeftChild;
                        leftMostNode.LeftChild.Predecessor = previousChild;
                    }

                    previousChild = leftMostNode.RightChild;

                    leftMostNode = (BinaryTreeNode<T>)leftMostNode.Successor;
                }

                leftMostNode = childNodes.First();
            }

            return root;
        }
    }
}

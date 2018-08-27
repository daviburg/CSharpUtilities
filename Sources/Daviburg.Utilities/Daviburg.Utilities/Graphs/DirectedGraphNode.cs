// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectedGraphNode.cs" company="Microsoft">
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

namespace Daviburg.Utilities.Graphs
{
    using System.Collections.Generic;

    public class DirectedGraphNode<T>
    {
        public List<DirectedGraphNode<T>> parents;

        public DirectedGraphNode()
        {
        }

        public DirectedGraphNode(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public List<DirectedGraphNode<T>> Parents
        {
            get => this.parents;
        }

        public void AddParent(DirectedGraphNode<T> parent) => (this.parents ?? (this.parents = new List<DirectedGraphNode<T>>())).Add(parent);

        /// <summary>
        /// Gets the child nodes.
        /// </summary>
        /// <remarks>This property is virtual so derived classes may override with additional constraints specific to a type of tree.</remarks>
        public virtual List<DirectedGraphNode<T>> Children { get; set; }

        public bool IsLeaf => this.Children == null || this.Children.Count == 0;
        public bool IsRoot => this.Parents == null;
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoResizeArray.cs" company="Microsoft">
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
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// An auto-resizing generic array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <remarks>
    /// Get accessor out of bound will return the default for the type.
    /// Set accessor out of bound will automatically resize the backing array to fit the index.
    /// When interfaces collide, the generic method is provided as public while the other is explicitly implemented.
    /// </remarks>
    public class AutoResizeArray<T> : ICloneable, IList, IStructuralComparable, IStructuralEquatable, IList<T>, ICollection<T>, IEnumerable<T>, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        /// <summary>
        /// The native array backing the auto-resize array.
        /// </summary>
        private T[] backingArray;

        /// <summary>
        /// Creates a new instance of <see cref="AutoResizeArray"/> with the provided initial array.
        /// </summary>
        /// <param name="initialArray">The initial array.</param>
        public AutoResizeArray(T[] initialArray)
        {
            this.backingArray = initialArray;
        }

        /// <summary>
        /// Creates a new instance of <see cref="AutoResizeArray"/> of the specified length.
        /// </summary>
        /// <param name="length">The initial size of the array to create.</param>
        public AutoResizeArray(int length)
        {
            this.backingArray = new T[length];
        }

        /// <inheritdoc/>
        object IList.this[int index]
        {
            get => index < this.backingArray.Length ? this.backingArray[index] : default(T);
            set
            {
                if (index >= this.backingArray.Length)
                {
                    Array.Resize<T>(ref this.backingArray, index + 1);
                }

                this.backingArray[index] = (T)value;
            }
        }

        /// <inheritdoc/>
        public T this[int index]
        {
            get => index < this.backingArray.Length ? this.backingArray[index] : default(T);
            set
            {
                if (index >= this.backingArray.Length)
                {
                    Array.Resize<T>(ref this.backingArray, index + 1);
                }

                this.backingArray[index] = value;
            }
        }

        /// <inheritdoc/>
        T IReadOnlyList<T>.this[int index] { get => index < this.backingArray.Length ? ((IReadOnlyList<T>)this.backingArray)[index] : default(T); }

        /// <inheritdoc/>
        public bool IsFixedSize => false;

        /// <inheritdoc/>
        public bool IsReadOnly => this.backingArray.IsReadOnly;

        /// <inheritdoc/>
        public int Count => this.backingArray.Length;

        /// <inheritdoc/>
        public bool IsSynchronized => this.backingArray.IsSynchronized;

        /// <inheritdoc/>
        public object SyncRoot { get; } = new object();

        /// <inheritdoc/>
        public int Add(object value)
        {
            return ((IList)this.backingArray).Add(value);
        }

        /// <inheritdoc/>
        public void Add(T item)
        {
            ((IList<T>)this.backingArray).Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            ((IList)this.backingArray).Clear();
        }

        /// <inheritdoc/>
        public object Clone()
        {
            return new AutoResizeArray<T>((T[])this.backingArray.Clone());
        }

        /// <inheritdoc/>
        public int CompareTo(object other, IComparer comparer)
        {
            var otherAutoResizeArray = other as AutoResizeArray<T>;
            return ((IStructuralComparable)this.backingArray).CompareTo(otherAutoResizeArray?.backingArray, comparer);
        }

        /// <inheritdoc/>
        public bool Contains(object value)
        {
            return ((IList)this.backingArray).Contains(value);
        }

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            return ((IList<T>)this.backingArray).Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(Array array, int index)
        {
            this.backingArray.CopyTo(array, index);
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.backingArray.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public bool Equals(object other, IEqualityComparer comparer)
        {
            var otherAutoResizeArray = other as AutoResizeArray<T>;
            return ((IStructuralEquatable)this.backingArray).Equals(otherAutoResizeArray?.backingArray, comparer);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.backingArray.GetEnumerator();
        }

        /// <inheritdoc/>
        public int GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)this.backingArray).GetHashCode(comparer);
        }

        /// <inheritdoc/>
        public int IndexOf(object value)
        {
            return ((IList)this.backingArray).IndexOf(value);
        }

        /// <inheritdoc/>
        public int IndexOf(T item)
        {
            return ((IList<T>)this.backingArray).IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, object value)
        {
            ((IList)this.backingArray).Insert(index, value);
        }

        /// <inheritdoc/>
        public void Insert(int index, T item)
        {
            ((IList<T>)this.backingArray).Insert(index, item);
        }

        /// <inheritdoc/>
        public void Remove(object value)
        {
            ((IList)this.backingArray).Remove(value);
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            return ((IList<T>)this.backingArray).Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            ((IList)this.backingArray).RemoveAt(index);
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.backingArray).GetEnumerator();
        }
    }
}

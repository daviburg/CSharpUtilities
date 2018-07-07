// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReferencedLock.cs" company="Microsoft">
//   This file is part of Daviburg Utilities.
//
//   Daviburg Utilities is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   Daviburg Utilities is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with Foobar.  If not, see<https://www.gnu.org/licenses/>.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Daviburg.Utilities
{
    using System.Collections.Generic;

    /// <summary>
    /// A lock object with reference count for use in <see cref="ConcurrentLockDictionary{T}"/>.
    /// </summary>
    public sealed class ReferencedLock : IEqualityComparer<ReferencedLock>
    {
        /// <summary>
        /// The lock object.
        /// </summary>
        /// <remarks>The value factory of <see cref="System.Collections.Concurrent.ConcurrentDictionary{TKey, TValue}"/> may be called multiple times before an update is successful. Hence the value factory must be idem potent. The cloning and increment clone reference count ensures idem potency.</remarks>
        public object LockObject { get; } = new object();

        /// <summary>
        /// The current reference count.
        /// </summary>
        /// <remarks>This count is only representing the value at the time this instance was read in the <see cref="ConcurrentLockDictionary{T}"/>.</remarks>
        public int ReferenceCount { get; private set; }

        /// <summary>
        /// (Idempotent) Acquires a reference to the lock through cloning.
        /// </summary>
        /// <returns>A cloned instance with increased reference count.</returns>
        public ReferencedLock Acquire()
        {
            var newLock = (ReferencedLock)this.MemberwiseClone();
            newLock.ReferenceCount++;
            return newLock;
        }

        /// <summary>
        /// (Idempotent) Releases one reference from the lock through cloning.
        /// </summary>
        /// <returns>A cloned instance with decreased reference count.</returns>
        public ReferencedLock Release()
        {
            var newLock = (ReferencedLock)this.MemberwiseClone();
            newLock.ReferenceCount--;
            return newLock;
        }

        /// <inheritdoc/>
        public bool Equals(ReferencedLock leftLock, ReferencedLock rightLock)
        {
            return leftLock?.ReferenceCount == rightLock?.ReferenceCount && object.ReferenceEquals(leftLock?.LockObject, rightLock?.LockObject);
        }

        /// <inheritdoc/>
        public int GetHashCode(ReferencedLock referencedLock)
        {
            return referencedLock.ReferenceCount.GetHashCode() ^ referencedLock.LockObject.GetHashCode();
        }
    }
}
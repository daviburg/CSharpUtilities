// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcurrentLockDictionary.cs" company="Microsoft">
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// A generic class for thread-safe dictionary of lock objects.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    public class ConcurrentLockDictionary<TKey> : ConcurrentDictionary<TKey, ReferencedLock>
    {
        /// <summary>
        /// Acquires a reference to the lock.
        /// </summary>
        /// <param name="key">The key of the lock to acquire a reference to.</param>
        /// <returns>The current lock and reference count.</returns>
        /// <remarks>This does not enter the lock. To enter a lock use either <c>lock</c> code block or <see cref="System.Threading.Monitor"/><c>.Enter</c>.</remarks>
        public ReferencedLock Acquire(TKey key)
        {
            var currentReferencedLock = this.AddOrUpdate(key: key, addValue: new ReferencedLock().Acquire(), updateValueFactory: (_, value) => value.Acquire());

            UtilitiesEventSource.Log.ConcurrentLockDictionaryAcquired(key.ToString(), currentReferencedLock.ReferenceCount);

            return currentReferencedLock;
        }

        /// <summary>
        /// Releases one reference from the lock.
        /// </summary>
        /// <param name="key">The key of the lock to release one reference from.</param>
        /// <returns>True if the reference count reach zero *and* the lock object was removed before some other thread took a new reference. False otherwise.</returns>
        public bool Release(TKey key)
        {
            var currentReferencedLock = this.AddOrUpdate(key: key, addValue: new ReferencedLock(), updateValueFactory: (_, value) => value.Release());

            UtilitiesEventSource.Log.ConcurrentLockDictionaryReleased(key.ToString(), currentReferencedLock.ReferenceCount);

            if (currentReferencedLock.ReferenceCount != 0)
            {
                return false;
            }

            // NOTE(daviburg): This is a thread safe way to remove a value *only* if it hasn't changed. A pity .NET authors did not expose it publicly.
            // If the value has changed, someone is newly taking a reference and we do not want to remove the value.
            var removed = ((ICollection<KeyValuePair<TKey, ReferencedLock>>)this).Remove(new KeyValuePair<TKey, ReferencedLock>(key: key, value: currentReferencedLock));

            if (removed)
            {
                UtilitiesEventSource.Log.ConcurrentLockDictionaryRemoved(key.ToString());
            }

            return removed;
        }
    }
}

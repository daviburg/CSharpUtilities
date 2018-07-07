// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcurrentLockDictionaryTests.cs" company="Microsoft">
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
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Daviburg.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ConcurrentLockDictionaryTests
    {
        private const string TestLockKey = "TestLockKey";

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void SimpleLockDictionaryTest()
        {
            var concurrentDictionary = new ConcurrentLockDictionary<string>();
            var testLock = concurrentDictionary.Acquire(ConcurrentLockDictionaryTests.TestLockKey);
            Assert.AreEqual(expected: 1, actual: testLock.ReferenceCount);
            Assert.IsFalse(Monitor.IsEntered(testLock.LockObject));

            Assert.AreSame(expected: testLock.LockObject, actual: concurrentDictionary[ConcurrentLockDictionaryTests.TestLockKey].LockObject);
            Assert.IsTrue(testLock.Equals(testLock, concurrentDictionary[ConcurrentLockDictionaryTests.TestLockKey]));
            Assert.AreEqual(expected: testLock.GetHashCode(testLock), actual: concurrentDictionary[ConcurrentLockDictionaryTests.TestLockKey].GetHashCode(concurrentDictionary[ConcurrentLockDictionaryTests.TestLockKey]));

            var testLockRemoved = concurrentDictionary.Release(ConcurrentLockDictionaryTests.TestLockKey);

            // NOTE(daviburg): ReferenceLock is a point-in-time copy-on-write class. So the lock we got upon acquire is not changed by the release from the dictionary.
            Assert.AreEqual(expected: 1, actual: testLock.ReferenceCount);
            Assert.IsTrue(testLockRemoved);

            Assert.IsFalse(concurrentDictionary.TryGetValue(ConcurrentLockDictionaryTests.TestLockKey, out ReferencedLock outReferencedLock));
        }

        [TestMethod]
        public void RemoveNotFoundLockDictionaryTest()
        {
            var concurrentDictionary = new ConcurrentLockDictionary<string>();
            var testLockRemoved = concurrentDictionary.Release(ConcurrentLockDictionaryTests.TestLockKey);
            Assert.IsTrue(testLockRemoved);

            // NOTE(daviburg): Out-of-order release-acquire is harmless, as release adds a dummy zero reference entry if not pre-existing
            var testLock = concurrentDictionary.Acquire(ConcurrentLockDictionaryTests.TestLockKey);
            Assert.AreEqual(expected: 1, actual: testLock.ReferenceCount);
        }

        [TestMethod]
        public void ImmutableAcquireDictionaryTest()
        {
            var concurrentDictionary = new ConcurrentLockDictionary<string>();
            var testLockReference1 = concurrentDictionary.Acquire(ConcurrentLockDictionaryTests.TestLockKey);
            var testLockReference2 = concurrentDictionary.Acquire(ConcurrentLockDictionaryTests.TestLockKey);
            Assert.IsFalse(testLockReference1.Equals(testLockReference2));
            Assert.IsFalse(testLockReference1.Equals(testLockReference1, testLockReference2));
        }

        [TestMethod]
        public void ReentryDictionaryTest()
        {
            var concurrentDictionary = new ConcurrentLockDictionary<string>();
            for (var iteration = 1; iteration < 50; iteration++)
            {
                var testLock = concurrentDictionary.Acquire(ConcurrentLockDictionaryTests.TestLockKey);
                Assert.AreEqual(expected: iteration, actual: testLock.ReferenceCount);
                if (iteration == 1)
                {
                    Monitor.Enter(testLock.LockObject);
                }
                else
                {
                    Assert.IsTrue(Monitor.IsEntered(testLock.LockObject));
                }
            }

            for (var iteration = 49; iteration > 1; iteration--)
            {
                var testLockRemoved = concurrentDictionary.Release(ConcurrentLockDictionaryTests.TestLockKey);
                Assert.IsFalse(testLockRemoved);
            }

            Monitor.Exit(concurrentDictionary[ConcurrentLockDictionaryTests.TestLockKey].LockObject);
            var finallyRemoved = concurrentDictionary.Release(ConcurrentLockDictionaryTests.TestLockKey);
            Assert.IsTrue(finallyRemoved);
        }

        [TestMethod]
        public void ReferencedLockEqualityTest()
        {
            var concurrentDictionary = new ConcurrentLockDictionary<string>();
            var testLockReference = concurrentDictionary.Acquire(ConcurrentLockDictionaryTests.TestLockKey);
            Assert.IsFalse(testLockReference.Equals(null, testLockReference));
            Assert.IsFalse(testLockReference.Equals(testLockReference, null));
            Assert.IsTrue(testLockReference.Equals(testLockReference, testLockReference));
            Assert.IsTrue(testLockReference.Equals(null, null));
        }

        [TestMethod]
        public void MultithreadedConcurrencyDictionaryTest()
        {
            var concurrentDictionary = new ConcurrentLockDictionary<string>();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            const int DegreeOfParallelism = 50;
            const int LockingDurationInMilliseconds = 40;

            var result = Parallel.For(
                fromInclusive: 1,
                toExclusive: 51,
                body: instanceNumber =>
                {
                    var testLock = concurrentDictionary.Acquire(ConcurrentLockDictionaryTests.TestLockKey);
                    lock (testLock.LockObject)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(LockingDurationInMilliseconds));
                    }

                    concurrentDictionary.Release(ConcurrentLockDictionaryTests.TestLockKey);
                });

            stopWatch.Stop();

            Assert.IsTrue(result.IsCompleted);

            // NOTE(daviburg): if thread synchronization happened as expected the parallel task will have taken at minimum the duration required for a sequential execution of all the individual locks duration.
            Assert.IsTrue(stopWatch.Elapsed >= TimeSpan.FromMilliseconds(DegreeOfParallelism * LockingDurationInMilliseconds));
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventRecordingForUtilities.cs" company="Microsoft">
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

namespace Daviburg.Utilities.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EventRecordingForUtilities
    {
        [TestMethod]
        public void EventRecordingTest()
        {
            TestEventListener.Log.AddEventSource(UtilitiesEventSource.Log.Guid);
            try
            {
                var concurrentLockDictionaryTests = new ConcurrentLockDictionaryTests();
                concurrentLockDictionaryTests.ImmutableAcquireDictionaryTest();
                concurrentLockDictionaryTests.ReentryDictionaryTest();
                concurrentLockDictionaryTests.ReferencedLockEqualityTest();
                concurrentLockDictionaryTests.RemoveNotFoundLockDictionaryTest();
                concurrentLockDictionaryTests.SimpleLockDictionaryTest();

                // If count is 1, look at the content of Payload for an error description such as 'ERROR: Exception in Command Processing for EventSource'
                Assert.IsTrue(TestEventListener.Log.ReceivedEvents.Count > 100);

                // Because logging is not disabled yet and events may trickle in, we need to make a copy of the enumeration with .ToList before .Where
                var eventSourceFailures = TestEventListener.Log.ReceivedEvents
                    .ToList()
                    .Where(
                        eventArgs =>
                        {
                            var formattedEvent = TestEventListener.FormatPayload(eventArgs.Payload);
                            return formattedEvent.Contains("ERROR: Exception in Command Processing for EventSource")
                                   || formattedEvent.Contains("Tracing failed");
                        })
                    .ToList();
                Assert.IsFalse(condition: eventSourceFailures.Any(), message: TestEventListener.FormatPayload(eventSourceFailures.FirstOrDefault()?.Payload));
            }
            finally
            {
                TestEventListener.Log.Disable();
            }
        }
    }
}

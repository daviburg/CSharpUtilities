# CSharpUtilities

Utilities for C#.

The utilities assembly targets .NET Standard 1.1 for maximum re-usability accross platforms.

The test project for the assembly is based on Visual Studio Unit Tests, targeting .NET Framework 4.6 because it leverages functionalities only added then. A lower .NET Framework version (4.5) could be used for projects taking a dependency on the utilities provided per the chart published at https://docs.microsoft.com/en-us/dotnet/standard/net-standard

Initially the utilities contain only one main class, a concurrent (thread-safe) dictionary for lock objects. The referenced lock class is used by the dictionary to keep track of reference count on the lock entries.
A secondary class and enum provide ETW tracing based on .NET EventSource. While used by the concurrent lock dictionary, they are generic and could be used idenpendently and for other utilities. They illustrate several tricks for the implementation of an event source, including lazy-initialized singleton pattern, enum-declared event ids, region-grouping of event-class.

I came up with the concurrent lock dictionary while working on clean-up of connections to line of business application servers, and I didn't find any pre-existing (open-source) art in the matter. Do share any issue, bug or improvement you see with the code. I am using these as part of some Azure services at Microsoft and would be happy to correct any problem.

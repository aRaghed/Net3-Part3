# NET 5

## What's new

There's a lot of news in NET 5, for a complete list see [What's new in .NET 5](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five) and also there's a good presentation from my friends Scott Hunter & Scott Hanselman on Channel 9: [The Journey to One .NET](https://channel9.msdn.com/Events/Build/2020/BOD106)

**Source Generators.** Source generators allow code that runs during compilation to inspect your program and produce additional files that are compiled together with the rest of your code.

**Records.** Reference types with value-based equality semantics and non-destructive mutation supported by a new `with` expression.

**System.Text.Json** namespace contains a lot of new features and improvements. Circular references, Http-extensions, Records, ready-made/predefined web defaults etc.  

No new language features for **Visual Basic** in .NET 5, but a lot of improvements in **F#.**

**Regular expressions** Relational pattern matching, extends pattern matching capabilities to relational operators for comparative evaluations and expressions, including logical patterns - new keywords `and`, `or`, and `not`.

**Performance increment.** In general NET 5 is twice as fast as NET Core 3.x and four times as fast as NET Framework 4.x.

## What's removed

* Web Forms

  Should not be used, use Blazor/Razor instead.

* Windows Workflow Foundation

  Not supported any more. It still exists in some community versions, CoreWF and Elsa-Workflow but they are not supported by Microsoft. 

## What's still supported (in some way)

* Windows Communication Foundation (WCF)

  Windows Communication Framework which is built upon Remote Procedure Call (RPC), Distributed Component Object Model (DCOM), IIS Web Services (ASMX) and SOAP is an old technology that more or less has been abandoned because of Rest based API's. Google did their own implementation based on the core principles which they released as gRPC. Microsoft recommends their developers to use gRPC as a replacement to the old technology. 

  There is however a platform independent community replacement from .NET Foundation that ports the WCF Client to NET Core/NET 5. It goes by the name WCFCore and it is fully supported by Microsoft. They are working on the Server part as well but that work is NOT supported by Microsoft.

## Top-level programs

Se sample code, SimpleConsole

## Source generators

See sample code  and [Introducing C# Source Generators](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) and [New C# Source Generator Samples](https://devblogs.microsoft.com/dotnet/new-c-source-generator-samples/)

## Single file apps

[Single file application](https://docs.microsoft.com/en-us/dotnet/core/deploying/single-file)

## Application trimming

[App Trimming in .NET 5](https://devblogs.microsoft.com/dotnet/app-trimming-in-net-5/)

## Records

See sample code ClassInitializers and [C# 9.0 on the record](https://devblogs.microsoft.com/dotnet/c-9-0-on-the-record/) which covers a bit about Pattern matching as well

## Pattern matching

See sample code PatternMatching and [Tutorial: Build algorithms with pattern matching](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/pattern-matching)

## System.Text.Json

See comprehensive description here [What's new in .NET 5](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five#systemtextjson-new-features)

## Benchmarking

[BenchmarkDotNet](https://benchmarkdotnet.org/)

[Performance Improvements in .NET 5](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-5/)

[gRPC performance improvements in .NET 5](https://devblogs.microsoft.com/aspnet/grpc-performance-improvements-in-net-5/)
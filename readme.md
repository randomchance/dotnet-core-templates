Templates for .Net Core 2.2 and .Net Standard 2.0.

Includes dependency injection, logging, metrics, and configuration using the option pattern.


**ServiceHost**

A console application that can be run as a Windows service, functions as a Generic Service Host.

**BackgroundService**

A project for creating a generic hosted service library that targets .NET Standard or .NET Core. Includes dependency injection, logging, metrics, and configuration using the option pattern.

**InjectedLibrary**

A project for creating a dependency injected class library that targets .NET Standard or .NET Core. Includes dependency injection, logging, metrics, and configuration using the option pattern.


**Build instructions** `dotnet pack` - creates a nuget package, then install with `dotnet new --install <path to nuget package>`

# Overview

Application templates for .Net Core 2.2 and .Net Standard 2.0
that make use of dependency injection, logging, metrics, and configuration using the option pattern.

I wanted to make use of the sweet GenericHost goodness in .NET Core, as well as having logging and dependency injection in quick test projects, but I always end up forgetting stuff up... enter **templates!**


## Templates

[ServiceHost](templates/ServiceHost)

A console application that can be run as a Windows service, functions as a [.NET Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2) to run background services. Exposes Prometheus metrics from all loaded components.


[BackgroundService](templates/BackgroundService)

A project for creating a generic background service library that targets .NET Standard or .NET Core and can run via console or ASP Core 2.2 application. Includes dependency injection, logging, metrics, and configuration using the option pattern.

[InjectedLibrary](templates/InjectedLibrary)

A project for creating a dependency injected class library that targets .NET Standard or .NET Core. Includes dependency injection, logging, metrics, and configuration using the option pattern.


## Usage

See [Installing a template](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates#installing-a-template)

You can use the templates in three ways:

1. Grab the nuget package `dotnet new -i RandomChance.Common.Templates` or if it's local `dotnet new -i <PATH_TO_NUPKG_FILE>`
2. Clone this repo then then add the projects directly `dotnet new -i <templates/BackgroundService>`
3. Package it yourself... because you can..? run `dotnet pack` in the root of this repo, then add the `dotnet new -i bin\Debug\RandomChance.Common.Templates.0.1.0.nupkg`

> **Note** that to uninstall a template you added from the filesystem you have to specify the **Absolute Path**

```bash
  dotnet new -u <ABSOLUTE_PATH_TO_TEMPLATE>
```

## TODO

* Service Host should handle command line args better (at all)
* `InjectedLibrary` is a kinda silly sounding, I should name it better
* Examples and description in each template.

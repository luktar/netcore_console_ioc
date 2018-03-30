# Intro

This project is an empty .Net Core 2 console application with integrated IoC, logger, configuration file, test and mock framework, command line arguments and optional database connector. You can use it as template for your console application.

Everything is up and running and will save you a lot of time for searching the internet.

Setting up the environment will take you 5 - 10 minutes depends on experience instead of few days.

# Technologies

In this project following technologies have been used:

- NLog https://github.com/NLog
- Moq https://github.com/moq
- XUnit https://xunit.github.io/
- CommandLineUtils https://github.com/natemcmaster/CommandLineUtils

Optional:
- Entity Framework
- Pomelo https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql

# Set up the environment

## Download and compile

- clone repository
- go to root directory contains NewTemplateSolution.sln
- run `dotnet build` command - the solution has been compiled successfully (at least I hope so)

## Hello world!

You can execute example command if following way:

- open `NewTemplate` directory in terminal
- execute command `dotnet run helloworld -t WORLD`
- the following output should appear:

```
### NewTemplate ###
Application started
Building IoC container...
IoC container set up successfully.
[DEBUG] Logger set up successfully.
[DEBUG] Reading command line arguments...
[INFO] Hello WORLD!
```

## Rename project

Files and folders:

- choose name for your project ex. `MyProject`
- rename `NewTemplateSolution.sln` file
- rename `NewTemplate/NewTemplate.csproj` file
- rename `NewTemplateTest/NewTemplateTest.csproj` file
- rename `NewTemplate` directory
- rename `NewTemplateTest` directory

Strings:

- open VSCode in a root folder
- rename all occurrences of '`NewTemplate`' string

Checkout:

- run `dotnet build` command in a root folder - everything should be built correctly.

# Add new command

- add new configuration properties to `IConfig` interface and `LocalConfig` class. You can use this class to pass command line arguments to your program
- open `Commands` directory and add new base class for your command ( ex. `HelloWorldCommand.cs`) and implement `ICommand` interface
- pass IoC object by the constructor. I recommend to pass `ILogger<HelloWorldCommand>` and `IConfig`.
- you can use configuration from `IConfig` interface in `Run()` method
- add command class to IoC container in `Startup.cs` file in method `ConfigureServices(IServiceCollection services)`. For instance `services.AddSingleton<ICommand, HelloWorldCommand>();`
- open CommandsProvider.cs file and add new command method in a similar way to `Version` or `HelloWorld`. You can read more about command line arguments in this articles: https://gist.github.com/iamarcel/8047384bfbe9941e52817cf14a79dc34 and https://blog.terribledev.io/Parsing-cli-arguments-in-dotnet-core-Console-App/
- use following code to run your command class
```
ICommand helloWorldCommand = 
  ServiceProvider.GetServices<ICommand>().First(
    x => x.GetType() == typeof(HelloWorldCommand));
helloWorldCommand.Run();
```
- add new command method to `Execute` method in `CommandsProvider.cs` class
- set up breakpoint in `Run()` method in your command class
- debug application with specified arguments - you should hit breakpoint

# Built-in commands

## Help

If you want to display all possible arguments add `-h` or `--help` argument to your command.

Example output:

```
Usage:  [options] [command]

Options:
  -h | --help  Show help information

Commands:
  helloworld  Hello world command.
  version     Print NewTemplate version.

Use " [command] --help" for more information about a command.
```

It doesn't work with `dotnet run`. You can perform this operation on direct `dll` file ex. `dotnet ./bin/debug/netcoreapp2.0/NewTemplate.dll -h`

## Version

To check project version execute command `dotnet run version`.

Example output:

```
[INFO] NewTemplate version: 1.0.0.0)
```

The version is taken from project `*.csproj` file.

```
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.0</TargetFramework>
    <Version>1.0.0.0</Version>
  </PropertyGroup>
  ...
</Project>
```

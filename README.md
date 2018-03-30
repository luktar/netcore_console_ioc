# Intro

Empty .Net Core 2 console application with integrated IoC, logger, configuration file, test and mock framework, command line arguments and optional database connector.

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

## Rename project

Files and folders:

- choose name for your project ex. MyProject
- rename NewTemplateSolution.sln file
- rename NewTemplate/NewTemplate.csproj file
- rename NewTemplateTest/NewTemplateTest.csproj file
- rename NewTemplate directory
- rename NewTemplateTest directory

Strings:

- open VSCode in a root folder
- rename all occurrences of 'NewTemplate' string

Checkout:

- run `dotnet build` command in a root folder - everything should be built correctly.

# SchoolAdministration
School Administration is a sample backend application built using ASP.NET Core3.1, CQRS and IdentityServer4

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.3 or later)
* [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
      ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ```

  4. change the connectionstring with your server name, user Id and password in the following
  * SchoolAdministration.Services.Identity.WebUI
  	- appsettings.Development.json
  * SchoolAdministration.Services.Identity.Infrastructure.Factories
	- ApplicationDbContextFactory.cs
	- ConfigurationDbContextFactory.cs 
	- PersistedGrantDbContextFactory
  
  5. Next, launch the SchoolAdministration.Services.Identity.WebUI 
      ```
	 dotnet run
	 ```
  
  6. Launch [https://localhost:44325/swagger/index.html) in your browser to view Identity APIs

## Technologies
* .NET Core 3.1
* Entity Framework Core 3.1
* IdentityServer4
* CQRS

## Versions
The [master](https://github.com/SomayaNaeem/SchoolAdministration) 
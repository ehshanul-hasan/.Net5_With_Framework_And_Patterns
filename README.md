# Dot.Net.Core.CRUD

The solution consistes of 3 projects.

Dot.Net.Core.CRUD.Data - .Net 5.0 Class Library (Data Access Layer)
Dot.Net.Core.CRUD.Domain – .Net 5.0 Class Library (Service Layer to implement business)
Dot.Net.Core.CRUD.Web- .Net 5.0 Kestrel Host (web API)

Here I didnt use any mapper as in this application the domain enity and database entity are same

To set the language and log file location please check the settings in appsettings.json. By default de-DE is set as default language.

To include the nuget package please run the corresponding commands and 'dotnet restore'.

Technology covered here:

#Repository pattern
#Unit Of Work
#Fluent Validation.
#i18n
#serilog
#Entity Framework
#Filters
#Result Extenders.
#web api etc

This application serves 2 client app.
  1. Angular -> https://github.com/ehshanul-hasan/angular.basic
  2. Aurelia -> https://github.com/ehshanul-hasan/aurelia.basic

# QuizApplication
## Overview
This is an ASP.NET Core REST API project for a simple quiz application. Users can create, view and solve tests consisting of single choice and multiple choice questions.

The project contains only a REST API, there is no client application. You can use the generated Swagger API to view and try out functions.

## Database
The project uses a local MSSQL database to store questions and quizes.

Alternatively: you can configure a MySQL database with the connection string in the appsettings.json file. Don't forget to change the username/password!

## Data access with EntityFramework
Before you run the application don't forget to migrate and update the database. The project uses EntityFramework Core with Code-first approach, the proper EF Tools NuGet package is also added to the project, so the only thing to left is use the ```Add-Migration DbInit``` and ```Update-Database``` commands in the Package Manager Console in Visual Studio.

The necessary data classes are in the project's Data folder with the EF context class (QuizDbContext.cs).

## Logic layer
The project is an ASP.NET Core project with its base architecture.

The Controllers folder contains these controllers which handle requests:
- QuizController.cs handles viewing and solving quizes
- ManageQuizController.cs handles new quiz creation and is responsible for displaying already existing quizes (even with the correct answers for the questions).

With the repository pattern it's easy  to implement new, custom quiz checking algorithms. The required interface is in the Interfaces folder, the initial checking algorithm is in the Services folder.
## DTOs
The application expects data input in JSON format. Output format is also JSON. For serialization the project uses the NewtonsoftJson library, for DTO-data object mapping it uses the AutoMapper library.

The concrete DTO classes are in the DTO folder along with the mapping configuration MappingProfile.cs file.

## Other remarks
1. When creating a new quiz a question's type must match any of the possibilities in the QuestionType.cs enum. Otherwise it will result in a bad request response with "Invalid input format" message.
2. When creating a new quiz the quiz's name, description, and at least 1 question is required. Single choice questions must have exactly 1 correct answer, multiple choice questions must have at least 1 correct answer. Otherwise it will result in a bad request response with a proper message.
3. When a quiz ID is required, giving a non-existing ID will result in a bad request response with the "Quiz doesnt exist" message.
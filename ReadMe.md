# dotnet-6-jwt-authentication-api

.NET 6.0 - JWT Authentication APIs dotnet list package
project 'TexCode' has the following package references
   [net6.0]: 
   Top-level Package                                          Requested   Resolved
   > AutoMapper                                               12.0.1      12.0.1  
   > AutoMapper.Extensions.Microsoft.DependencyInjection      12.0.1      12.0.1  
   > MailKit                                                  4.2.0       4.2.0   
   > Microsoft.EntityFrameworkCore                            7.0.13      7.0.13  
   > Microsoft.EntityFrameworkCore.InMemory                   7.0.13      7.0.13  
   > Microsoft.EntityFrameworkCore.Sqlite                     6.0.23      6.0.23  
   > Microsoft.EntityFrameworkCore.SqlServer                  6.0.23      6.0.23  
   > Microsoft.EntityFrameworkCore.Tools                      6.0.23      6.0.23  
   > Microsoft.VisualStudio.Web.CodeGeneration.Design         6.0.16      6.0.16  
   > MimeKit                                                  4.2.0       4.2.0   
   > Newtonsoft.Json                                          13.0.3      13.0.3  
   > Swashbuckle.AspNetCore                                   6.5.0       6.5.0   
   > System.IdentityModel.Tokens.Jwt                          7.0.3       7.0.3   

Project 'TexCodeTests' has the following package references
   [net6.0]: 
   Top-level Package                Requested   Resolved
   > coverlet.collector             3.2.0       3.2.0   
   > Microsoft.NET.Test.Sdk         17.5.0      17.5.0  
   > Moq                            4.20.69     4.20.69 
   > xunit                          2.5.3       2.5.3   
   > xunit.runner.visualstudio      2.5.3       2.5.3   

The Project has multiple endpoints / routes to demonstrate authenticating with JWT and accessing a restricted route with JWT.

/users/authenticate - public route that accepts HTTP POST requests containing the username and password in the body. If the username and password are correct then a JWT authentication token and the user details are returned.
/users - secure route that accepts HTTP GET requests and returns a list of all the users in the application if the HTTP Authorization header contains a valid JWT token. If there is no auth token or the token is invalid then a 401 Unauthorized response is returned.

Tools required to run the .NET 6.0 JWT Example Locally
To develop and run .NET 6.0 applications locally, download and install the following:

.NET SDK https://dotnet.microsoft.com/en-us/download - includes the .NET runtime and command line tools
Visual Studio Code https://code.visualstudio.com/ - code editor that runs on Windows, Mac and Linux
C# extension for Visual Studio Code https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp - adds support to VS Code for developing .NET applications

Test the .NET 6.0 JWT Authentication API with Postman one can download it at https://www.postman.com/downloads.

Here are the instructions for Postman to authenticate a user to get a JWT token from the API, and then make an authenticated request with the JWT token to retrieve a list of users from the API.

To authenticate a user with the API and get a JWT token follow these steps:

Open a new request tab by clicking the plus (+) button at the end of the tabs in Postman.
Change the HTTP method to POST with the dropdown selector on the left of the URL input field.
In the URL field enter the address to the authenticate route of your local API - http://localhost:port/users/authenticate
Select the Body tab below the URL field, change the body type radio button to raw, and change the format dropdown selector to JSON.
Enter a JSON object containing the test username and password in the Body textarea:
{
    "username": "qasim",
    "password": "sarwar"
}

Click the Send button, you should receive a "200 OK" response with the user details and a JWT token in the response body, make a copy of the token value because we'll be using it in the next step to make an authenticated request.

How to make an authenticated request to retrieve all users
To make an authenticated request using the JWT token from the previous step, follow these steps:

Open a new request tab by clicking the plus (+) button at the end of the tabs.
Change the HTTP method to GET with the dropdown selector on the left of the URL input field.
In the URL field enter the address to the user's route of your local API - http://localhost:port/users.
Select the Authorization tab below the URL field, set the Type selector to Bearer Token, and paste the JWT token from the previous authenticate step into the Token field.
Click the Send button, you should receive a "200 OK" response containing a JSON array with all the user records in the system (just the one test user in the example).


=================================================================================================================================
Question: What were your challenges in implementing this project?

Answer: The project implementation had some challenges, particularly related to Entity Framework Core and database interactions. However, these were addressed successfully through careful design and testing.
Question: How did you handle exceptions and edge cases in your code?

Answer: I handled exceptions and edge cases by using proper error handling techniques, such as try-catch blocks, and also by validating user input. This ensures that the application gracefully handles unexpected scenarios.
Question: What is the significance of using AutoMapper in this project?

Answer: AutoMapper is used to efficiently map between data transfer objects (DTOs) and domain entities. This helps reduce boilerplate code and ensures clean separation between the presentation layer and the data access layer.
Question: Could you explain your decision to use an in-memory database for testing?

Answer: Using an in-memory database is a lightweight and efficient way to perform unit tests on database-related functionality without affecting the actual database. It allows for isolation and reproducibility of tests.
Question: How do you ensure data integrity and validation in the project?

Answer: Data integrity and validation are ensured through a combination of data annotations in the model classes and explicit validation checks in the services. This helps prevent invalid data from entering the database.
Question: How do you manage dependency injection in the project?

Answer: Dependency injection is managed using the built-in dependency injection container provided by ASP.NET Core. This ensures that services and dependencies are injected into the application components, promoting modularity and testability.
Question: Could you elaborate on your approach to handling JWT tokens for authentication?

Answer: JWT tokens are used for user authentication. I generate tokens with a defined expiration time and verify them when required. This provides a secure way to authenticate and authorize users.
Question: How do you handle password hashing in user authentication?

Answer: Passwords are securely hashed before storing them in the database. This enhances security and ensures that plaintext passwords are not exposed.
Question: Explain how you manage email notifications and user communication.

Answer: I use an email service to send notifications to users, such as password reset emails. The service composes and sends email messages with appropriate content and links.
Question: What measures have you taken to prevent duplicate data and key violations in the database?

Answer: I've used checks within the code to prevent duplicate data. For example, when registering a new account, I check if an account with the same email already exists in the database. This helps avoid key violations and ensures data integrity.
Technical Decisions or Requirements:

The project relies on Entity Framework Core and an in-memory database for testing, which simplifies database operations during development and testing phases.

AutoMapper is used to streamline the mapping between domain entities and DTOs, making the codebase more maintainable and reducing redundancy.

The use of JWT tokens for authentication provides a secure means of authenticating users.

The code includes robust error handling and validation to handle exceptions and ensure data integrity.

Dependency injection is utilized throughout the project to promote modularity and testability.

Breakdown of Time Spent on Tasks:

Reading Documents: 30 minutes
Implementing and Running Tests: 4 hours
Implementation and debugging: 10 hours (2 hours daily after work)
Refactoring: 2 hours
This breakdown of time spent on tasks reflects a structured approach to development, including initial research, testing, implementation, and optimization. The allocation of time to each task ensures a methodical and efficient workflow.
# dotnet-6-jwt-authentication-api

.NET 6.0 - JWT Authentication APIs dotnet list package
Project 'TexCode' has the following package references
   [net6.0]: 
   Top-level Package                             Requested   Resolved
   > AutoMapper                                  12.0.1      12.0.1  
   > MailKit                                     4.2.0       4.2.0   
   > Microsoft.EntityFrameworkCore               7.0.13      7.0.13  
   > Microsoft.EntityFrameworkCore.InMemory      7.0.13      7.0.13  
   > MimeKit                                     4.2.0       4.2.0   
   > Newtonsoft.Json                             13.0.3      13.0.3  
   > Swashbuckle.AspNetCore                      6.5.0       6.5.0   
   > System.IdentityModel.Tokens.Jwt             7.0.3       7.0.3

The Project has multiple endpoints / routes to demonstrate authenticating with JWT and accessing a restricted route with JWT.

/users/authenticate - public route that accepts HTTP POST requests containing the username and password in the body. If the username and password are correct then a JWT authentication token and the user details are returned.
/users - secure route that accepts HTTP GET requests and returns a list of all the users in the application if the HTTP Authorization header contains a valid JWT token. If there is no auth token or the token is invalid then a 401 Unauthorized response is returned.

Tools required to run the .NET 6.0 JWT Example Locally
To develop and run .NET 6.0 applications locally, download and install the following:

.NET SDK https://dotnet.microsoft.com/en-us/download - includes the .NET runtime and command line tools
Visual Studio Code https://code.visualstudio.com/ - code editor that runs on Windows, Mac and Linux
C# extension for Visual Studio Code https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp - adds support to VS Code for developing .NET applications

Test the .NET 6.0 JWT Authentiocation API with Postman one can download it at https://www.postman.com/downloads.

Here are the instructions for Postman to authenticate a user to get a JWT token from API, and then make an authenticated request with the JWT token to retrieve a list of users from the api.

To authenticate a user with the api and get a JWT token follow these steps:

Open a new request tab by clicking the plus (+) button at the end of the tabs in postman.
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
In the URL field enter the address to the users route of your local API - http://localhost:port/users.
Select the Authorization tab below the URL field, set the Type selector to Bearer Token, and paste the JWT token from the previous authenticate step into the Token field.
Click the Send button, you should receive a "200 OK" response containing a JSON array with all the user records in the system (just the one test user in the example).
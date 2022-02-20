# Introduction 
This project resolve the **RapidPay** requirements related to the new system for Cards and Fees management including Authentication.

# Getting Started
This project is based on the framework .NET 5.0 built into Visual Studio 2019 Community, using technologies like as:
- Entity Framework Core
- LinQ
- Swagger
- JWT Authentication
- Automapper
- Memory Cache

DB use for is based on SQL Server.

This project is focused mainly in OOP design, also has consideration of some techniques:
- DDD architecture
- Repository and Singleton patterns
- Dependency injection (DI for IoC)
- SOLID principles
- Exceptions handling
- Lazy approach for DbSet's

# Build
Prior to build/run the project in Visual Studio, please execute the prepared scripts (*RapidPay.API.Sql*) in a new DB called **RapidPayDB**.

Update the connection string that is allocated in the *appsettings.json*.

When you execute the project you can test the REST API endpoints directly in Swagger or maybe using Postman or any Rest client. You could:
1. Create a new user (*/api/UserManagement/RegisterUser*)
2. Login with the user registered (*/api/UserManagement/LoginUser*)
3. Create a new Card to use (*/api/CardManagement/CreateCard*)
4. Make payments with the previous Card (*/api/CardManagement/CreatePayment*)
5. Consult the current Card balance (*/api/CardManagement/GetBalance/{cardId}*)

# Test
This project is published in [Azure](https://rapidpaytest.azurewebsites.net/swagger/index.html) currently, to allow to test end-to-end the endpoints quickly. Azure has the same data configured in the Scripts included in the project.
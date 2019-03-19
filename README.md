# ProgrammingAssessment
SDI Assessment

CREATE DATABASE ProgrammingAssessment;

USE ProgrammingAssessment;

CREATE LOGIN AssessmentLogin
    WITH PASSWORD = 'ChangeMePlease';  
GO  
CREATE USER AssessmentUser FOR LOGIN AssessmentLogin;  
GO  

exec sp_addrolemember 'db_owner', 'AssessmentUser'; 
GO



.Net Core 2.0 / Visual Studio 2017 / EF Core/ Auto Mapper/ Fluent Validator

//ON PACKAGE MANAGER CONSOLE


$env:ASPNETCORE_ENVIRONMENT='Development'
//dotnet ef migrations add AddInitialAssessmentTable
dotnet ef database update

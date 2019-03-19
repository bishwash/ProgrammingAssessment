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
from package manager console, go to Assessment.Api folder and run these command to update the tables. 
I would send the sql script if I had time.

$env:ASPNETCORE_ENVIRONMENT='Development'
//dotnet ef migrations add AddInitialAssessmentTable
dotnet ef database update


Assessment.Web is the Vue.js Bootstrap site
Wish I had more experience with Vue.js. Since this is my first attempt with Vue please execuse. :)
Please change the url on those two components (Students.vue and StudentList.vue to match the locally running api url.)

To run the web application, go to Assessment.Web folder run
npm run dev


TODO
Add ApiLocation as a global settings somewhere in the application.
Add more test cases for api testing
Add validation to client application form


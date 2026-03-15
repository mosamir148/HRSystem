Overview

HR System is a fullstack enterprise-grade Employee Management System built with ASP.NET Core Web API for the backend and Angular 17 for the frontend.
It allows efficient employee and leave management with validation rules, pagination, reporting, and printable employee summaries.

This project demonstrates:

Layered architecture (Clean Architecture)

RESTful APIs with validation and business rules

Modern Angular frontend with responsive UI

Reporting integration using SQL Server Reporting Services

Technologies & Tools

Backend:

ASP.NET Core Web API

Entity Framework Core

SQL Server

Reporting Services (SSRS)

Frontend:

Angular 17

Angular Material / Bootstrap (for UI components)

RxJS for reactive data handling

Other Tools:

Git + GitHub

Swagger for API documentation

Postman for testing APIs

Features
Employee Management

Add, edit, delete employees

Unique Employee ID and Name validation

Display employee list with pagination (5 per page)

View detailed employee information including leave summary

Leave Management

Add, edit, delete employee leaves

Select leave type from a dropdown

Start date picker & duration validation (1–30 days)

Prevent overlapping leaves for the same employee

Enforce annual leave limits (max 30 days per leave type per year)

Auto-update total leave days in employee table

Reporting

Print employee details and leave history

Export to PDF or Excel (optional enhancement)

Business Rules & Validation

Employee ID and Name must be unique.

No overlapping leaves for the same employee.

Maximum 30 days leave per leave type per year.

Leave duration: 1–30 days.

Total leave days auto-updated on leave add/edit/delete.

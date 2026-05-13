# Technology Stack — TutorHub
Last Updated: May 2026 · Version: 1.0

## Overview
The stack is selected to align with the developer's existing Azure certification portfolio (AZ-204, AZ-400, AI-102, DP-900) and to maximise portfolio impact by demonstrating real-world enterprise cloud development patterns.

Stack Breakdown
|Layer	|Technology	|Rationale|
|-------|-----------|---------|
|Frontend	|Blazor WebAssembly (.NET 8)	|Single-page application using C# throughout. Component-based UI with Razor syntax deployed to Azure Static Web Apps (CDN).|
|UI Components	|MudBlazor	|Material Design component library for Blazor providing data grids, forms, dialogs, and responsive layouts.|
|Backend API	|.NET 8 Web API (C#)|	RESTful API layer using controllers and minimal APIs. Service/repository pattern for clean separation of concerns.|
|ORM	|Entity Framework Core 8|	Code-first migrations with Azure SQL. LINQ-based queries and strongly-typed domain models.|
|Authentication|	ASP.NET Core Identity + JWT	Token-based| auth with refresh token rotation. Extensible to Azure AD B2C in V2 for institutional SSO.|
|Database|	Azure SQL Database	|Managed relational database with automated backups, scaling, and geo-redundancy. Aligns with DP-900 certification.|
|File Storage|	Azure Blob Storage	|Stores exported PDFs, session attachments, and AI reports. Accessed via SAS tokens for security.|
|AI / Reports|	Azure OpenAI (GPT-4o)	|Powers natural-language progress report generation from structured session data. Aligns with AI-102 certification.|
|PDF Export|	QuestPDF (.NET)|	Code-first PDF generation in C#. Used for progress reports and payment summaries without external services.|
|DevOps / CI-CD|	Azure DevOps Pipelines|	Automated build, test, and release pipelines to Azure App Service. Directly demonstrates AZ-400 certification skills.|
|Hosting	|Azure App Service|	PaaS hosting for both Blazor frontend and Web API. Staging slots enable zero-downtime deployments.|
|Containerisation|	Docker	|Containerised services for local development parity and simplified deployment configuration.|
|Monitoring	|Azure Application Insights	|Telemetry, distributed tracing, error tracking, and performance monitoring integrated via AZ-204 patterns.|

## Certification Alignment
|Certification | Stack Components Demonstrated |
---------------|--------------------------------|
|AZ-204 (Developing Solutions for Azure) | App Service, Blob Storage, Azure SQL, Application Insights, Managed Identities|
|AZ-400 (DevOps Engineer)|	Azure DevOps Pipelines, CI/CD, staging slots, automated testing, Docker|
|AI-102 (Designing AI Solutions)|	Azure OpenAI integration, prompt engineering, responsible AI (content filters)|
|DP-900 (Azure Data Fundamentals)|	Azure SQL Database, relational modelling, EF Core migrations|


## Local Development Requirements
|Tool	|Version	Purpose|
|-------|------------------|
|.NET SDK	8.0+|	Build and run all projects|
|Visual Studio 2022 / Rider	Latest|	IDE with Blazor & Docker support|
|Docker Desktop	Latest|	Container parity with Azure deployment|
|Azure CLI	Latest|	Infrastructure provisioning and management|
|Git	Latest|	Version control|
© 2026 TutorHub. All rights reserved. · MVP v1.0
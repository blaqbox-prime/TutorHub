Architecture Overview — TutorHub
Last Updated: May 2026 · Version: 1.0

1. Architectural Style
TutorHub follows a clean N-tier architecture. Each layer has a single responsibility and communicates through well-defined contracts.

2. System Layers
┌─────────────────────────────────────────────────┐
│ PRESENTATION LAYER │
│ Blazor WebAssembly (.NET 8) | MudBlazor | Azure Static Web App │
│ Pages: Dashboard · Students · Sessions · Payments · Reports │
│ │
└─────────────────────────────────────────────────┘
│ HTTPS / REST
▼
┌─────────────────────────────────────────────────┐
│ API LAYER │
│ .NET 8 Web API | RESTful Controllers | JWT Middleware │
│ Azure App Service │
│ Controllers: Auth · Students · Sessions · Payments · Reports │
│ · Dashboard │
└─────────────────────────────────────────────────┘
│ EF Core / Service Interfaces
▼
┌─────────────────────────────────────────────────┐
│ SERVICE LAYER │
│ StudentService → CRUD & profile │
│ SessionService → Schedule & log │
│ PaymentService → Track & balance │
│ ReportService → AI + PDF export │
└─────────────────────────────────────────────────┘
│ Azure Cloud Services
▼
┌─────────────────────────────────────────────────┐
│ AZURE INFRASTRUCTURE │
│ Azure SQL → Relational DB │
│ Blob Storage → PDFs & files │
│ OpenAI GPT-4o → AI Reports │
│ App Service → API hosting │
│ App Insights → Monitoring │
└─────────────────────────────────────────────────┘


## 3. Core Data Model

### 3.1 Entity Overview

| Entity | Key Fields | Relationships |
|---|---|---|
| **Tutor** | `Id`, `Name`, `Email`, `PasswordHash`, `Plan`, `CreatedAt` | Has many Students; has many Subjects |
| **Student** | `Id`, `TutorId`, `FirstName`, `LastName`, `Email`, `Phone`, `GradeLevel`, `IsActive` | Belongs to Tutor; has many Sessions, Payments, ProgressNotes |
| **Subject** | `Id`, `TutorId`, `Name`, `DefaultRate`, `ColorHex` | Belongs to Tutor; linked to many Sessions |
| **Session** | `Id`, `StudentId`, `SubjectId`, `Date`, `DurationMins`, `TopicCovered`, `Attended`, `Notes` | Belongs to Student and Subject |
| **Payment** | `Id`, `StudentId`, `Amount`, `Date`, `Method`, `Status`, `Reference` | Belongs to Student |
| **ProgressNote** | `Id`, `StudentId`, `GeneratedAt`, `Content`, `IsAiGenerated`, `Period` | Belongs to Student |



## 4. Authentication Flow
- Tutor submits credentials (email + password)
- API validates against ASP.NET Core Identity (hashed store)
- On success: short-lived JWT (access) + HTTP-only refresh token cookie
- Client includes JWT in Authorization header for all API requests
- Refresh token rotation: new refresh token issued on each use
- Session expires after 60 min inactivity; "Remember me" extends to 7 days
- Logout revokes refresh token and clears client state

---

## 5. AI Report Generation Flow


GLM-5.1

Share

Word
TutorHub_ProjectBrief.docx
DOCX
24.0 KB
Based on this project sheet create markdown files with this content plus templates that will be used in the docs folder of my github repo to maintain the projects documentation 

Thought Process




Here's a complete set of markdown files and templates for your docs/ folder. I'll present each file with its full path and content.

File Structure
text

docs/
├── README.md
├── project-brief.md
├── architecture.md
├── technology-stack.md
├── api-reference.md
├── user-stories.md
├── sprint-plan.md
├── scope.md
├── risks.md
├── changelog.md
├── templates/
│   ├── bug-report.md
│   ├── feature-request.md
│   ├── user-story.md
│   ├── api-endpoint.md
│   ├── sprint-review.md
│   ├── architecture-decision-record.md
│   └── changelog-entry.md
docs/README.md
📚 TutorHub — Project Documentation
Tutor & Lecturer Administration SaaS · MVP v1.0
Status: Draft — Pending Review
Target Platform: Web (Desktop & Mobile Responsive)
Date: May 2026

Welcome to the central documentation hub for TutorHub. This repository contains all planning, architecture, API, and process documentation for the project.

📖 Documentation Index
Document	Description
Project Brief	Executive summary, problem statement, goals, and success metrics
Architecture	System layers, data model, and infrastructure overview
Technology Stack	Full stack breakdown with rationale for each technology choice
API Reference	RESTful endpoint documentation for all MVP controllers
User Stories	MVP user stories with acceptance criteria
Sprint Plan	Six-sprint delivery plan for the MVP
Scope	In-scope features and V2 deferrals
Risks & Mitigations	Identified risks with severity and mitigation strategies
Changelog	Version history and release notes
🧩 Templates
Reusable templates for day-to-day project work:

Template	Purpose
Bug Report	Standardised bug reporting
Feature Request	Propose new features for V2+
User Story	Create new user stories
API Endpoint	Document a new API endpoint
Sprint Review	Record sprint outcomes and decisions
Architecture Decision Record	Capture significant technical decisions
Changelog Entry	Add a structured entry to the changelog
🚀 Quick Links
Tech Stack: .NET 8 / Blazor WebAssembly / Azure
CI/CD: Azure DevOps Pipelines → Azure App Service
AI: Azure OpenAI (GPT-4o) for progress report generation
Database: Azure SQL Database (EF Core 8, code-first)
🔒 Confidentiality
This project documentation is CONFIDENTIAL. Do not distribute outside the authorised project team.

© 2026 TutorHub. All rights reserved.

docs/project-brief.md
Project Brief — TutorHub
Version: 1.0 — Initial Release
Status: Draft — Pending Review
Date: May 2026

1. Executive Summary
TutorHub is a cloud-based SaaS platform designed to streamline the administrative workload of independent tutors, lecturers, and small private training institutions. Built on Microsoft Azure using .NET 8 and Blazor WebAssembly, it provides a unified workspace for managing students, scheduling sessions, tracking attendance, recording payments, and generating AI-powered progress reports.

The platform targets the growing private education sector in South Africa and beyond — a market currently underserved by generic business tools that do not cater to the nuances of tutoring workflows.

Problem Statement
Independent tutors rely on a fragmented mix of spreadsheets, WhatsApp messages, and manual invoicing. This leads to missed payments, poor session records, and no structured visibility into student progress. TutorHub replaces this with a single, intuitive platform.

2. Project Goals & Success Metrics
2.1 Primary Goals
Deliver a fully functional MVP within 8–10 weeks of development start.
Provide tutors with a single dashboard to manage all operational tasks.
Enable AI-powered progress report generation via Azure OpenAI.
Achieve a clean, deployed cloud architecture leveraging the Azure ecosystem.
Build a codebase extensible for V2 features such as Stripe payments and a student portal.
2.2 Success Metrics
Metric	Target
MVP delivery	Within 10 weeks
Onboarding time	Under 5 minutes to first student added
Report generation	Under 30 seconds per AI progress report
Page load	Under 2 seconds on standard broadband
Test coverage	Minimum 70% coverage on service layer
3. Related Documentation
Architecture Overview →
Technology Stack →
API Reference →
User Stories →
Sprint Plan →
Scope & Exclusions →
Risks & Mitigations →
© 2026 TutorHub. All rights reserved. · MVP v1.0

docs/architecture.md
Architecture Overview — TutorHub
Last Updated: May 2026 · Version: 1.0

1. Architectural Style
TutorHub follows a clean N-tier architecture. Each layer has a single responsibility and communicates through well-defined contracts.

2. System Layers
┌─────────────────────────────────────────────────────────────────────┐
│ PRESENTATION LAYER │
│ Blazor WebAssembly (.NET 8) | MudBlazor | Azure Static Web App │
│ Pages: Dashboard · Students · Sessions · Payments · Reports │
│ │
└──────────────────────────────┬──────────────────────────────────────┘
│ HTTPS / REST
▼
┌─────────────────────────────────────────────────────────────────────┐
│ API LAYER │
│ .NET 8 Web API | RESTful Controllers | JWT Middleware │
│ Azure App Service │
│ Controllers: Auth · Students · Sessions · Payments · Reports │
│ · Dashboard │
└──────────────────────────────┬──────────────────────────────────────┘
│ EF Core / Service Interfaces
▼
┌─────────────────────────────────────────────────────────────────────┐
│ SERVICE LAYER │
│ StudentService → CRUD & profile │
│ SessionService → Schedule & log │
│ PaymentService → Track & balance │
│ ReportService → AI + PDF export │
└──────────────────────────────┬──────────────────────────────────────┘
│ Azure Cloud Services
▼
┌─────────────────────────────────────────────────────────────────────┐
│ AZURE INFRASTRUCTURE │
│ Azure SQL → Relational DB │
│ Blob Storage → PDFs & files │
│ OpenAI GPT-4o → AI Reports │
│ App Service → API hosting │
│ App Insights → Monitoring │
└─────────────────────────────────────────────────────────────────────┘

DEVOPS: GitHub → Azure DevOps Pipelines → Build & Test → Deploy (AZ-400)

text


---

## 3. Core Data Model

### 3.1 Entity Overview

| Entity | Key Fields | Relationships |
|---|---|---|
| **Tutor** | `Id`, `Name`, `Email`, `PasswordHash`, `Plan`, `CreatedAt` | Has many Students; has many Subjects |
| **Student** | `Id`, `TutorId`, `FirstName`, `LastName`, `Email`, `Phone`, `GradeLevel`, `IsActive` | Belongs to Tutor; has many Sessions, Payments, ProgressNotes |
| **Subject** | `Id`, `TutorId`, `Name`, `DefaultRate`, `ColorHex` | Belongs to Tutor; linked to many Sessions |
| **Session** | `Id`, `StudentId`, `SubjectId`, `Date`, `DurationMins`, `TopicCovered`, `Attended`, `Notes` | Belongs to Student and Subject |
| **Payment** | `Id`, `StudentId`, `Amount`, `Date`, `Method`, `Status`, `Reference` | Belongs to Student |
| **ProgressNote** | `Id`, `StudentId`, `GeneratedAt`, `Content`, `IsAiGenerated`, `Period` | Belongs to Student |



## 4. Authentication Flow

- Tutor submits credentials (email + password)
- API validates against ASP.NET Core Identity (hashed store)
- On success: short-lived JWT (access) + HTTP-only refresh token cookie
- Client includes JWT in Authorization header for all API requests
- Refresh token rotation: new refresh token issued on each use
- Session expires after 60 min inactivity; "Remember me" extends to 7 days
- Logout revokes refresh token and clears client state


## 5. AI Report Generation Flow

- Tutor clicks "Generate Report" on student profile
- API compiles structured data (subject, topics, attendance, notes)
- Structured payload sent to Azure OpenAI GPT-4o
- Prompt instructs: formal, encouraging, third-person academic progress report
- AI response returned within 30 seconds
- Tutor reviews and edits report text
- On save: report stored in Azure SQL; PDF generated via QuestPDF
- PDF uploaded to Azure Blob Storage with SAS token access


---

## 7. Design Principles

| Principle | Application |
|---|---|
| **Single Responsibility** | Each service handles one domain aggregate |
| **Separation of Concerns** | Presentation → API → Service → Data layers are independent |
| **Dependency Injection** | All services registered via .NET DI container |
| **Repository Pattern** | Data access abstracted behind `IRepository<T>` interfaces |
| **Fail-Safe Defaults** | JWT validation on every endpoint; no anonymous data access |
| **Extensibility** | Architecture supports V2 additions (Stripe, student portal, multi-tenant) |

---

© 2026 TutorHub. All rights reserved. · MVP v1.0
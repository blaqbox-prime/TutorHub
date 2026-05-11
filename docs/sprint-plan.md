# MVP Sprint Plan — TutorHub

## Metadata

- **Last Updated:** May 2026
- **Version:** 1.0
- **Total Duration:** 10 weeks (6 sprints)

## Sprint Overview

| Sprint | Focus Area | Stories & Deliverables | Weeks |
|--------|------------|-------------------------|-------|
| Sprint 1 | Project Setup & Authentication | US-01, US-02, US-03 — Azure infra provisioning, EF Core setup, JWT auth, CI/CD pipeline baseline | 1–2 |
| Sprint 2 | Student & Subject Management | US-04, US-05, US-06, US-07 — Student CRUD, search/filter, subject management | 3–4 |
| Sprint 3 | Session Scheduling & Logging | US-08, US-09, US-10 — Calendar UI, session CRUD, attendance tracking, history view | 5–6 |
| Sprint 4 | Payments | US-11, US-12, US-13 — Payment recording, balance calculation, PDF payment summary | 6–7 |
| Sprint 5 | AI Progress Reports | US-14, US-15, US-16 — Azure OpenAI integration, report editing, PDF export, Blob Storage | 7–8 |
| Sprint 6 | Dashboard & Release Polish | US-17 — Dashboard KPIs, responsive layout, error handling, performance tuning, UAT | 9–10 |

## Sprint 1: Project Setup & Authentication (Weeks 1–2)

### Objectives

- Provision Azure infrastructure (App Service, SQL, Blob Storage)
- Set up EF Core with initial migrations
- Implement JWT authentication with refresh token rotation
- Establish CI/CD pipeline baseline in Azure DevOps

### Deliverables

- Azure resource group and services provisioned
- Solution structure with Clean Architecture layers
- EF Core DbContext with Tutor entity and initial migration
- US-01: Tutor Registration
- US-02: Tutor Login
- US-03: Account Profile Management
- CI/CD pipeline: build + test on PR

### Definition of Done

- All auth endpoints return correct HTTP status codes
- JWT validation enforces on protected endpoints
- Pipeline runs green on main branch

## Sprint 2: Student & Subject Management (Weeks 3–4)

### Objectives

- Build full student CRUD with search, sort, and filter
- Implement subject management with colour tags and rate
- Create student list and detail pages in Blazor

### Deliverables

- US-04: Add a Student
- US-05: View & Search Students
- US-06: Edit & Deactivate a Student
- US-07: Manage Teaching Subjects
- Student list with real-time search and sorting
- Student detail page with tabs (profile, sessions, payments, reports)

### Definition of Done

- All student API endpoints tested with 70%+ service coverage
- Client-side validation mirrors server-side rules
- Inactive students correctly filtered from scheduling dropdowns

## Sprint 3: Session Scheduling & Logging (Weeks 5–6)

### Objectives

- Build session scheduling with calendar UI
- Implement attendance logging and history
- Add overlap detection for scheduled sessions

### Deliverables

- US-08: Schedule a Session
- US-09: Log a Completed Session
- US-10: View Session History
- Weekly calendar view (MudBlazor)
- Session status badges and attendance summary panel

### Definition of Done

- Overlap warning triggers correctly
- Attendance increment updates student session count
- History view supports all filter combinations

## Sprint 4: Payments (Weeks 6–7)

### Objectives

- Implement payment recording and voiding
- Calculate running balances per student
- Generate payment summary PDFs via QuestPDF

### Deliverables

- US-11: Record a Payment
- US-12: View Outstanding Balances
- US-13: Export a Payment Summary PDF
- Balance calculation logic unit tested
- QuestPDF branded template for payment summaries
- Blob Storage integration for PDF persistence

### Definition of Done

- Balance matches formula: (sessions attended × rate) − total payments
- Voiding restricted to 24-hour window
- PDF generates and uploads to Blob within 5 seconds

## Sprint 5: AI Progress Reports (Weeks 7–8)

### Objectives

- Integrate Azure OpenAI GPT-4o for report generation
- Build report review/edit/save workflow
- Export reports as branded PDFs

### Deliverables

- US-14: Generate an AI Progress Report
- US-15: Export a Progress Report as PDF
- US-16: View Report History
- Azure OpenAI provisioning and prompt engineering
- Report editor with save/edit flow
- QuestPDF branded template for progress reports

### Definition of Done

- Report generates within 30 seconds
- Tutor can edit AI output before saving
- PDF accessible via SAS token download link
- max_tokens limits enforced

## Sprint 6: Dashboard & Release Polish (Weeks 9–10)

### Objectives

- Build tutor dashboard with KPIs and charts
- Responsive layout pass for mobile
- Performance tuning and error handling
- UAT and release preparation

### Deliverables

- US-17: Tutor Dashboard Overview
- KPI tiles, bar chart, upcoming sessions panel, outstanding balances panel
- Quick-action buttons (Add Student, Schedule Session, Record Payment)
- Mobile responsive layout verification
- Global error handling and loading states
- Performance audit (page load < 2s, lazy loading)
- UAT sign-off

### Definition of Done

- Dashboard loads within 2 seconds
- All pages responsive on mobile viewports
- Zero critical bugs from UAT
- Staging slot deployed and verified before production swap
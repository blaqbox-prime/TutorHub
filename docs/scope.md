# MVP Scope & Exclusions — TutorHub

## Metadata

- **Last Updated:** May 2026
- **Version:** 1.0

## 1. In Scope for MVP

These features constitute the complete MVP deliverable:

- Tutor account registration, login, and profile management (JWT auth)
- Student profile management (full CRUD)
- Subject management with rate and colour tagging
- Session scheduling and logging with attendance tracking
- Manual payment recording and running balance calculation
- Outstanding balance dashboard panel
- AI-powered progress report generation (Azure OpenAI GPT-4o)
- PDF export for progress reports and payment summaries (QuestPDF)
- Azure Blob Storage for file persistence
- Tutor dashboard with key operational metrics
- Azure DevOps CI/CD pipeline with automated testing
- Deployment to Azure App Service with Application Insights monitoring

## 2. Explicitly Out of Scope — Deferred to V2

The following features are not part of the MVP and are deferred to V2:

| Feature | Reason for Deferral |
|---------|---------------------|
| Stripe or payment gateway integration | Requires PCI compliance work; manual recording sufficient for MVP |
| Student-facing self-service portal | Requires separate auth flow and UI; adds significant scope |
| Mobile application (iOS / Android) | Blazor WASM is mobile-responsive; native app is a future investment |
| Multi-tutor and institution-level management | Requires multi-tenancy architecture; single-tutor model validates core concept first |
| Google Calendar / Outlook calendar synchronisation | External API integration adds complexity; manual scheduling sufficient for MVP |
| Email notifications via SendGrid | Placeholder architecture only in MVP; no active email sending |
| Parent communication module | Requires separate user roles and notification infrastructure |
| Gamification and student engagement features | Value-add only after core workflows are proven |

## 3. V2 Planning Notes

When planning V2, the following architectural decisions in the MVP codebase should support extension:

- **Auth:** ASP.NET Core Identity is extensible to Azure AD B2C for institutional SSO
- **Database:** Azure SQL supports row-level security for multi-tenant V2
- **Payments:** Service layer is abstracted behind IPaymentService for gateway injection
- **API:** Versioned endpoints (/api/v1/, /api/v2/) should be introduced in V2

© 2026 TutorHub. All rights reserved. · MVP v1.0

# MVP User Stories & Acceptance Criteria — TutorHub

> **Last Updated:** May 2026 · **Version:** 1.0

---

All features below constitute the TutorHub MVP scope. Stories are written from the perspective of the **Tutor** (primary user) and include testable acceptance criteria.

---

## 5.1 Authentication & Account Management

### US-01: Tutor Registration

**User Story:** As a new tutor, I want to create an account so that I can access the TutorHub platform.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Registration form captures: full name, email, password, and confirm password. |
| AC-02 | Password must be at least 8 characters with one uppercase letter and one number. |
| AC-03 | Successful registration issues a JWT access token and HTTP-only refresh token cookie. |
| AC-04 | Duplicate email addresses are rejected with a descriptive error message. |
| AC-05 | On success the user is redirected to the onboarding dashboard. |

---

### US-02: Tutor Login

**User Story:** As a registered tutor, I want to log in with my email and password so that I can securely access my data.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Login accepts email and password and validates against hashed credentials. |
| AC-02 | Invalid credentials return a generic error message without revealing which field is wrong. |
| AC-03 | Successful login issues a short-lived JWT and a rotating refresh token. |
| AC-04 | Session expires after 60 minutes of inactivity; refresh token silently renews the session. |
| AC-05 | A 'Remember me' option extends session validity to 7 days. |

---

### US-03: Account Profile Management

**User Story:** As a tutor, I want to update my profile details so that my information stays current.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Profile page allows editing: full name, email, phone number, and bio. |
| AC-02 | Password change requires current password confirmation before updating. |
| AC-03 | All changes are saved with a visible success notification toast. |
| AC-04 | Profile updates are reflected immediately across all pages. |

---

## 5.2 Student Management

### US-04: Add a Student

**User Story:** As a tutor, I want to add a new student profile so that I can track their sessions and progress.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Add Student form captures: first name, last name, email, phone, grade/level, and subject(s). |
| AC-02 | All mandatory fields are validated client-side and server-side before submission. |
| AC-03 | Newly added student appears in the student list immediately after saving. |
| AC-04 | A unique student profile page is created and accessible from the list. |
| AC-05 | Tutor receives a success notification on completion. |

---

### US-05: View & Search Students

**User Story:** As a tutor, I want to view and search my student list so that I can quickly navigate to any student.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Student list displays: full name, subjects, total sessions, and outstanding balance. |
| AC-02 | Search field filters results in real time as the tutor types (minimum 2 characters). |
| AC-03 | List can be sorted by name, outstanding balance, or last session date. |
| AC-04 | Active and inactive students can be toggled with a filter. |
| AC-05 | Empty state displays a call-to-action to add the first student. |

---

### US-06: Edit & Deactivate a Student

**User Story:** As a tutor, I want to edit a student's details and deactivate them when they are no longer active.

| # | Acceptance Criterion |
|---|---|
| AC-01 | All student profile fields are editable from the student detail page. |
| AC-02 | Deactivating a student moves them to an 'Inactive' tab without deleting historical records. |
| AC-03 | Inactive students do not appear in session scheduling dropdowns. |
| AC-04 | A confirmation dialog is displayed before deactivation is applied. |

---

## 5.3 Subject Management

### US-07: Manage Teaching Subjects

**User Story:** As a tutor, I want to create and manage the subjects I teach so that I can categorise sessions accurately.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Subject form captures: name, default hourly rate (ZAR), and a colour tag. |
| AC-02 | Subjects appear in a settings management panel. |
| AC-03 | Subjects linked to existing sessions can be archived but not deleted. |
| AC-04 | Subject colour tags are displayed on session calendar and list entries. |

---

## 5.4 Session Scheduling & Logging

### US-08: Schedule a Session

**User Story:** As a tutor, I want to schedule an upcoming tutoring session so that I can plan my week ahead.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Schedule Session form captures: student, subject, date, start time, duration, and optional notes. |
| AC-02 | Scheduled sessions appear on a weekly calendar view. |
| AC-03 | A warning is shown if the new session overlaps with an existing scheduled slot. |
| AC-04 | Sessions can be scheduled up to 6 months in advance. |
| AC-05 | Upcoming sessions within 24 hours are highlighted on the dashboard. |

---

### US-09: Log a Completed Session

**User Story:** As a tutor, I want to log the outcome of a completed session so that I have an accurate attendance and content record.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Log Session form captures: topic covered, attendance status (Attended / Absent / Cancelled), and session notes. |
| AC-02 | Logging attendance automatically increments the student's session count. |
| AC-03 | Cancelled sessions can be individually marked as billable or non-billable. |
| AC-04 | Session notes accept plain text up to 2000 characters. |
| AC-05 | Completed sessions display a colour-coded status badge in the session list. |

---

### US-10: View Session History

**User Story:** As a tutor, I want to view a student's full session history so that I can review their attendance record.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Session history lists all sessions for a student in reverse-chronological order. |
| AC-02 | Each row shows: date, subject, duration, attendance status, and topic covered. |
| AC-03 | Sessions can be filtered by date range, subject, or attendance status. |
| AC-04 | A summary panel shows: total sessions, sessions attended, and attendance percentage. |

---

## 5.5 Payment & Balance Tracking

### US-11: Record a Payment

**User Story:** As a tutor, I want to manually record a payment from a student so that I can track who has paid.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Record Payment form captures: student, amount (ZAR), date, payment method, and reference note. |
| AC-02 | Payment is saved and immediately reflected in the student's running balance. |
| AC-03 | A chronological payment history is visible on the student profile. |
| AC-04 | Payments can be voided within 24 hours of recording. |

---

### US-12: View Outstanding Balances

**User Story:** As a tutor, I want to see which students have outstanding balances so that I can follow up on overdue accounts.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Dashboard includes an Outstanding Balances panel sorted by amount owed (highest first). |
| AC-02 | Each entry shows: student name, amount owed, and date of last payment. |
| AC-03 | Clicking an entry navigates directly to that student's payment history. |
| AC-04 | Balance is calculated as: (sessions attended × subject rate) minus total payments received. |
| AC-05 | Students with a zero or credit balance are excluded from the panel. |

---

### US-13: Export a Payment Summary PDF

**User Story:** As a tutor, I want to export a payment statement for a student so that I can share it with them or their parents.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Payment summary PDF includes: student name, reporting period, session list, amounts, payments, and balance due. |
| AC-02 | PDF is generated using QuestPDF and branded with the TutorHub identity. |
| AC-03 | The PDF is downloadable from the student profile with a single click. |
| AC-04 | Generated PDF is saved to Azure Blob Storage and retrievable at any future time. |

---

## 5.6 AI-Powered Progress Reports

### US-14: Generate an AI Progress Report

**User Story:** As a tutor, I want to generate an AI-written progress report for a student so that I can save time while delivering professional feedback to parents.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Generate Report button is available on every student profile page. |
| AC-02 | The API sends structured data (subject, topics, attendance, session notes) to Azure OpenAI GPT-4o. |
| AC-03 | The AI prompt instructs the model to write a formal, encouraging, third-person academic progress report. |
| AC-04 | The report is generated and displayed within 30 seconds. |
| AC-05 | The tutor can review and edit the report text before saving. |
| AC-06 | Saved reports are stored with a timestamp and report period label. |

---

### US-15: Export a Progress Report as PDF

**User Story:** As a tutor, I want to download a progress report as a formatted PDF to share with parents.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Export PDF button is available on any saved progress report. |
| AC-02 | PDF includes: student name, subject, report period, tutor name, date, and report body. |
| AC-03 | PDF is generated via QuestPDF, saved to Azure Blob Storage, and a download link is returned immediately. |
| AC-04 | Previously exported PDFs remain downloadable from the student's report history. |

---

### US-16: View Report History

**User Story:** As a tutor, I want to view all previously generated reports for a student to track their progress over time.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Report history panel on the student profile lists all reports with date and subject. |
| AC-02 | Each report can be opened for full viewing or downloaded as a PDF. |
| AC-03 | Reports are ordered chronologically, newest first. |

---

## 5.7 Dashboard

### US-17: Tutor Dashboard Overview

**User Story:** As a tutor, I want a summary dashboard on login so that I can understand the state of my teaching business at a glance.

| # | Acceptance Criterion |
|---|---|
| AC-01 | Dashboard loads within 2 seconds of authentication. |
| AC-02 | KPI tiles display: total active students, sessions this week, total outstanding revenue, and next scheduled session. |
| AC-03 | A sessions-per-week bar chart displays the past 4 weeks of activity. |
| AC-04 | Upcoming Sessions panel lists the next 5 sessions with student name, subject, and time. |
| AC-05 | Outstanding Balances panel shows the top 5 students with the highest balances. |
| AC-06 | Quick-action buttons for Add Student, Schedule Session, and Record Payment are prominently displayed. |

---

## Story Summary

| Story ID | Title | Sprint |
|---|---|---|
| US-01 | Tutor Registration | Sprint 1 |
| US-02 | Tutor Login | Sprint 1 |
| US-03 | Account Profile Management | Sprint 1 |
| US-04 | Add a Student | Sprint 2 |
| US-05 | View & Search Students | Sprint 2 |
| US-06 | Edit & Deactivate a Student | Sprint 2 |
| US-07 | Manage Teaching Subjects | Sprint 2 |
| US-08 | Schedule a Session | Sprint 3 |
| US-09 | Log a Completed Session | Sprint 3 |
| US-10 | View Session History | Sprint 3 |
| US-11 | Record a Payment | Sprint 4 |
| US-12 | View Outstanding Balances | Sprint 4 |
| US-13 | Export a Payment Summary PDF | Sprint 4 |
| US-14 | Generate an AI Progress Report | Sprint 5 |
| US-15 | Export a Progress Report as PDF | Sprint 5 |
| US-16 | View Report History | Sprint 5 |
| US-17 | Tutor Dashboard Overview | Sprint 6 |

---

© 2026 TutorHub. All rights reserved. · MVP v1.0
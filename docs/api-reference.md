# API Reference — TutorHub

## Metadata

- **Last Updated:** May 2026
- **Version:** 1.0
- **Base URL (Production):** https://api.tutorhub.co.za
- **Base URL (Local Development):** https://localhost:5001/api

## Authentication

All endpoints (except Auth) require a valid JWT token in the Authorization header:

```
Authorization: Bearer <jwt-token>
```

## Auth

### `POST /api/auth/register`

Register a new tutor account.

#### Request Body

```json
{
  "fullName": "string",
  "email": "string",
  "password": "string",
  "confirmPassword": "string"
}
```

#### Responses

| Status          | Description                                      |
|-----------------|--------------------------------------------------|
| 200 OK         | Registration successful; returns JWT + refresh token cookie |
| 400 Bad Request | Validation error (duplicate email, weak password, mismatch) |

#### Validation Rules

- Email must be unique
- Password: minimum 8 characters, at least 1 uppercase letter, at least 1 number
- confirmPassword must match password

### `POST /api/auth/login`

Authenticate an existing tutor.

#### Request Body

```json
{
  "email": "string",
  "password": "string",
  "rememberMe": false
}
```

#### Responses

| Status            | Description                                      |
|-------------------|--------------------------------------------------|
| 200 OK           | Login successful; returns JWT + refresh token cookie |
| 401 Unauthorized | Invalid credentials (generic error message)     |

### `POST /api/auth/refresh-token`

Silently renew the access token using the refresh token cookie.

#### Responses

| Status            | Description                                      |
|-------------------|--------------------------------------------------|
| 200 OK           | New JWT issued; refresh token rotated           |
| 401 Unauthorized | Refresh token invalid or revoked                |

### `POST /api/auth/logout`

Revoke the refresh token and end the session.

#### Responses

| Status | Description                                      |
|--------|--------------------------------------------------|
| 200 OK | Logout successful; refresh token cookie cleared |

## Students

### `GET /api/students`

List all students belonging to the authenticated tutor.

#### Query Parameters

| Parameter | Type   | Description                          |
|-----------|--------|--------------------------------------|
| search    | string | Filter by name (min 2 characters)    |
| sortBy    | string | name, balance, lastSession           |
| isActive  | bool   | Filter active/inactive students      |

#### Response

Array of student summary objects.

### `POST /api/students`

Create a new student.

#### Request Body

```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phone": "string",
  "gradeLevel": "string",
  "subjectIds": ["guid"]
}
```

#### Responses

| Status       | Description                                      |
|--------------|--------------------------------------------------|
| 201 Created | Student created; returns full student object    |
| 400 Bad Request | Validation error                               |

### `GET /api/students/{id}`

Get a single student's full profile.

#### Responses

| Status     | Description                                      |
|------------|--------------------------------------------------|
| 200 OK    | Student found                                    |
| 404 Not Found | Student does not exist or does not belong to tutor |

### `PUT /api/students/{id}`

Update a student's profile.

#### Request Body

Same fields as create (all optional for partial update).

#### Responses

| Status     | Description                                      |
|------------|--------------------------------------------------|
| 200 OK    | Student updated                                  |
| 404 Not Found | Student not found                               |

### `DELETE /api/students/{id}`

Deactivate a student (soft delete — historical records preserved).

#### Responses

| Status     | Description                                      |
|------------|--------------------------------------------------|
| 200 OK    | Student deactivated                              |
| 404 Not Found | Student not found                               |

## Sessions

### `GET /api/sessions`

List sessions for the authenticated tutor.

#### Query Parameters

| Parameter  | Type   | Description                          |
|------------|--------|--------------------------------------|
| studentId  | guid   | Filter by student                    |
| subjectId  | guid   | Filter by subject                    |
| fromDate   | date   | Start of date range                  |
| toDate     | date   | End of date range                    |
| attendance | string | Attended, Absent, Cancelled          |

### `POST /api/sessions`

Schedule a new session.

#### Request Body

```json
{
  "studentId": "guid",
  "subjectId": "guid",
  "date": "2026-05-15",
  "startTime": "14:00",
  "durationMins": 60,
  "notes": "string (optional)"
}
```

**Validation:** Overlap warning if conflicting with an existing session.

### `GET /api/sessions/{id}`

Get session detail.

### `PUT /api/sessions/{id}`

Update a session (e.g., log attendance after completion).

#### Request Body (for logging)

```json
{
  "topicCovered": "string",
  "attended": "Attended | Absent | Cancelled",
  "notes": "string",
  "isBillable": true
}
```

### `DELETE /api/sessions/{id}`

Delete a session.

### `GET /api/sessions/student/{studentId}`

Get all sessions for a specific student (reverse-chronological).

## Payments

### `GET /api/payments/student/{studentId}`

List all payments for a student.

### `POST /api/payments`

Record a new payment.

#### Request Body

```json
{
  "studentId": "guid",
  "amount": 500.00,
  "date": "2026-05-15",
  "method": "EFT | Cash | Card",
  "reference": "string (optional)"
}
```

### `PUT /api/payments/{id}`

Void a payment (within 24 hours only).

### `GET /api/payments/balance/{studentId}`

Get the running balance for a student.

**Calculation:** (sessions attended × subject rate) − total payments received

## Reports

### `POST /api/reports/generate/{studentId}`

Generate an AI progress report using Azure OpenAI GPT-4o.

#### Request Body (optional overrides)

```json
{
  "periodStart": "2026-01-01",
  "periodEnd": "2026-05-01"
}
```

#### Response

Generated report content (editable before saving).

### `GET /api/reports/student/{studentId}`

List all reports for a student (newest first).

### `GET /api/reports/{id}/download`

Download the PDF of a specific report from Azure Blob Storage.

## Dashboard

### `GET /api/dashboard/summary`

Get KPI summary for the authenticated tutor.

#### Response

```json
{
  "totalActiveStudents": 0,
  "sessionsThisWeek": 0,
  "totalOutstandingRevenue": 0.00,
  "nextScheduledSession": {}
}
```

### `GET /api/dashboard/upcoming-sessions`

Get the next 5 upcoming sessions.

### `GET /api/dashboard/outstanding-balances`

Get top 5 students with the highest outstanding balances.

## Error Response Format

All error responses follow a consistent structure:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Validation Error",
  "status": 400,
  "detail": "One or more validation errors occurred.",
  "errors": {
    "Email": ["Email address is already registered."],
    "Password": ["Password must contain at least one uppercase letter."]
  }
}
```

## Rate Limiting

| Endpoint Group     | Limit              |
|--------------------|--------------------|
| Auth endpoints     | 20 requests / minute |
| Report generation  | 10 requests / minute |
| General API        | 100 requests / minute |

© 2026 TutorHub. All rights reserved. · MVP v1.0
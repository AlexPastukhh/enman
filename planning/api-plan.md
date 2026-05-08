# API Plan

## Назначение

Файл фиксирует целевые HTTP endpoints по слоям.

## Naming rules

- API должен отражать use cases, а не старые имена классов.
- L1 endpoints должны быть минимальными и стабильными.
- L2/L3 endpoints не добавлять в L1-задачах.
- В перспективе frontend API client генерируется из OpenAPI/orval.

## L1 API

### Auth

```http
POST /api/auth/register
POST /api/auth/login
POST /api/auth/logout
GET  /api/auth/me
```

### Profile

```http
GET /api/profile
PUT /api/profile
```

### Applicant Parties

```http
POST /api/applicant-parties/individual
GET  /api/applicant-parties
```

### Client Requests

```http
POST /api/requests
GET  /api/requests/my
GET  /api/requests/{id}
```

### Employee Requests

```http
GET  /api/employee/requests
GET  /api/employee/requests?view=all
GET  /api/employee/requests?view=unprocessed
GET  /api/employee/requests?view=processed
GET  /api/employee/requests/{id}
POST /api/employee/requests/{id}/take
POST /api/employee/requests/{id}/approve
POST /api/employee/requests/{id}/reject
```

### Contracts

```http
GET /api/requests/{id}/contract-draft
```

In L1 contract draft is created automatically during approve flow.

### Notifications

No public L1 notification endpoint is required.

Email is sent internally by application service during approve/reject.

## L2 API

### Applicant Parties

```http
POST /api/applicant-parties/entrepreneur
POST /api/applicant-parties/legal-entity
PUT  /api/applicant-parties/{id}
```

### Documents

```http
POST   /api/requests/{id}/documents
GET    /api/requests/{id}/documents
GET    /api/documents/{id}/download
DELETE /api/documents/{id}
```

### Verification

```http
POST /api/employee/requests/{id}/verification/run
GET  /api/employee/requests/{id}/verification
```

### Clarification

```http
POST /api/employee/requests/{id}/clarification
POST /api/requests/{id}/clarification-response
```

### Contract drafts

```http
GET  /api/employee/contract-templates
POST /api/employee/requests/{id}/contract-drafts
POST /api/employee/contract-drafts/{id}/generate-pdf
POST /api/employee/contract-drafts/{id}/send
GET  /api/requests/{id}/contract-draft
GET  /api/contract-drafts/{id}/download
POST /api/contract-drafts/{id}/acknowledge
POST /api/contract-drafts/{id}/request-correction
```

### Templates

```http
GET    /api/employee/feedback-templates
POST   /api/employee/feedback-templates
PUT    /api/employee/feedback-templates/{id}
DELETE /api/employee/feedback-templates/{id}
```

## L3 API

### Anonymous requests

```http
POST /api/anonymous/requests
GET  /api/anonymous/requests/{trackingNumber}
```

### Windows auth

```http
GET /api/auth/windows/me
```

### Admin / audit

```http
GET /api/admin/audit
GET /api/admin/security-events
GET /api/admin/health
```

## Current code mismatch notes

Current code still contains older endpoint and DTO naming, for example:

- register individual client;
- provide individual client data;
- create individual request.

Target L1 naming should gradually move to:

- account/client registration;
- applicant party creation;
- generic request creation by request type.

Do not rename all endpoints in one large unsafe change. Prefer small migration steps with tests.

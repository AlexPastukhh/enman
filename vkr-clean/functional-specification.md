# Functional Specification

Status: draft  
Scope: textual behavior specification for VKR chapter 2

This file turns planning scenarios into a diploma-friendly behavior specification. It is not only a flat requirement list.

Each scenario is described through:

```text
actor
preconditions
main flow
alternative flows
postconditions
```

## 1. Actors

| Actor | Role |
|---|---|
| Клиент | Registers, signs in, creates applicant data and submits requests |
| Сотрудник | Reviews requests, approves or rejects them, prepares follow-up documents |
| Email / notification service | Sends or supports notifications about request/document events |
| Система | Validates input, persists data, protects sessions and returns API responses |

## 2. Cross-Scenario Invariants

```text
- A request is created in the context of an authenticated client.
- A request is connected with an applicant party.
- Applicant party data belongs to a client account.
- Server-side validation is the final source of truth for accepted data.
- Client-side validation improves UX but does not replace server validation.
- Processed requests should not be approved or rejected again.
- Agreement/document work starts from an approved request context.
```

## SC-01. Register Client Account

Actor: client.

Preconditions:

```text
- the client is not authenticated;
- the entered email is not already registered;
- the entered password satisfies current validation rules.
```

Main flow:

```text
1. Client opens the registration page.
2. Client enters email and password.
3. Client confirms the password in the UI.
4. Frontend sends registration request to backend API.
5. Backend validates input.
6. Backend creates the client account.
7. Frontend shows success and directs the client to login or next step.
```

Alternative flows: email already exists; password is invalid; password confirmation does not match; server returns validation ProblemDetails.

Postconditions: client account exists and client can proceed to authentication.

## SC-02. Login Client Account

Main flow:

```text
1. Client opens login page.
2. Client enters credentials.
3. Frontend sends login request.
4. Backend validates credentials.
5. Backend creates session/cookie context.
6. Frontend invalidates/refetches current-user state.
7. Client is redirected to the target page.
```

Postconditions: authenticated client session is available.

## SC-03. Current User Session Bootstrap

Main flow:

```text
1. Frontend initializes providers and session bootstrap.
2. Frontend calls current-user API.
3. Backend returns current user or unauthorized result.
4. Frontend stores session state for routing and UI decisions.
```

## SC-04. Logout

Main flow:

```text
1. Client triggers logout action.
2. Frontend sends logout request.
3. Backend clears session context.
4. Frontend clears or invalidates cached session data.
5. Client is redirected to public or login page.
```

## SC-05. Create Individual Applicant Party

Main flow:

```text
1. Client enters individual applicant data.
2. Frontend validates visible form fields.
3. Frontend sends applicant creation request.
4. Backend derives account context from authenticated session.
5. Backend validates and persists applicant data.
6. Frontend shows saved applicant data or success state.
```

Postconditions: applicant party is saved and connected with client account.

## SC-06. Create Connection Request

Main flow:

```text
1. Client opens request creation form.
2. System loads or uses current applicant context.
3. Client enters request description and object/address data.
4. Frontend validates form state.
5. Frontend sends request creation command to backend.
6. Backend validates session, applicant context and request data.
7. Backend creates connection request.
8. Frontend shows success and can navigate to request list/detail when that UI is available.
```

Alternative flows: applicant context missing; description empty; invalid object/address data; expired session.

## SC-07. View Own Requests

Main flow:

```text
1. Client opens "My Requests".
2. Frontend requests the list through API.
3. Backend returns only requests available to the current client.
4. Frontend displays statuses and short request summaries.
```

## SC-08. View Request Details

Main flow:

```text
1. Client opens request detail page.
2. Frontend requests request details.
3. Backend verifies access.
4. Frontend displays request data, status and related document/notification information when available.
```

## SC-09. View Employee Request Queue

Main flow:

```text
1. Employee opens request queue.
2. System loads requests available for processing.
3. Employee filters or sorts requests if such UI is included.
4. Employee opens a request for review.
```

## SC-10. Approve Request

Main flow:

```text
1. Employee reviews request data.
2. Employee chooses approval action.
3. System validates that the request can still be processed.
4. System records review decision.
5. Request status becomes approved.
6. Further document/agreement preparation can start from approved request context.
```

## SC-11. Reject Request

Main flow:

```text
1. Employee reviews request data.
2. Employee chooses rejection action.
3. Employee enters feedback if required by UI/policy.
4. System validates that the request can still be processed.
5. System records rejection decision.
6. Request status becomes rejected.
```

## SC-12. Prepare Agreement / Document Draft

Main flow:

```text
1. Employee opens approved request.
2. Employee starts document/agreement preparation.
3. System creates or opens draft based on request and applicant data.
4. Employee checks draft content.
5. System saves draft and makes it available for further document workflow.
```

## SC-13. Notify Client

Main flow:

```text
1. System creates notification event after successful operation.
2. Notification service sends or records notification.
3. Client can receive email or see notification/result in UI.
```

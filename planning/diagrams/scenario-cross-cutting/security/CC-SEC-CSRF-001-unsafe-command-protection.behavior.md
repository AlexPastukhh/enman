# CC-SEC-CSRF-001 — Unsafe Command Protection Behavior

Status: current first-pass security behavior source  
Doc version: v0.1.0  
Type: cross-cutting security / server-client behavior  
Applies to: unsafe state-changing commands

## 1. Goal

Unsafe state-changing commands must not be accepted from cross-site or missing-token requests.

The system should protect command endpoints while allowing normal same-site client command flows.

## 2. Dangerous Scenario

```text
Attacker causes browser to send unsafe command request
        ↓
Request has user cookies/session context
        ↓
Request does not have valid antiforgery/CSRF token
        ↓
Server rejects the request
        ↓
State is not changed
```

## 3. Normal Scenario

```text
Authenticated user performs command in the app
        ↓
Client sends unsafe request with required CSRF protection
        ↓
Server validates token/session
        ↓
Command proceeds to normal authorization/lifecycle checks
```

## 4. Behavior Items

| ID | Behavior |
|---|---|
| `CC-SEC-CSRF-001-B01` | Unsafe state-changing endpoints require CSRF/antiforgery protection. |
| `CC-SEC-CSRF-001-B02` | Missing/invalid token rejects the command before domain state changes. |
| `CC-SEC-CSRF-001-B03` | Rejected CSRF request must not create/update/delete business state. |
| `CC-SEC-CSRF-001-B04` | Normal same-site client command flow can attach/use the required protection token. |
| `CC-SEC-CSRF-001-B05` | Client should surface a recoverable/auth/security error instead of silently failing when CSRF protection rejects a command. |

## 5. Out of Scope

```text
exact antiforgery library choice
exact token storage mechanism
business authorization checks unrelated to CSRF
domain lifecycle validation
```

## 6. Related Slice Drafts

Umbrella:

```text
planning/slices/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.md
```

Future server/client slices:

```text
planning/slices/server/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.server.md
planning/slices/client/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.client.md
```

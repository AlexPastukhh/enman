# Slice Implementation Notes Register

Status: current / L2 Employee request details client notes synchronized  
Scope: cross-slice implementation notes, client/server handoff notes, generated contract reminders and future implementation risks

## 1. Rule

Implementation notes are not behavior items.

Use this register for practical implementation considerations that affect future work but do not belong as scenario behavior.

## 2. Current L2 Employee Request Notes

### L2-EMP-DETAILS-001.client — Details read contract dependency

```text
L2-EMP-DETAILS-001.client is blocked until SL-EMP-REQ-002 server details endpoint and generated OpenAPI DTO exist.
```

Important contract boundary:

```text
StartReviewResponseDto from SL-EMP-REQ-003 is not the Employee details DTO.

Details read DTO must come from SL-EMP-REQ-002.
```

### Details read vs command sidecars

```text
Employee Request Details client sidecar:
  pages + entities
  read-only details UI
  review state display
  action availability display
  optional action slots

Future StartReview/Approve/Reject client sidecars:
  pages + features + entities
  mutation hooks
  CSRF-aware unsafe request helpers
  command feedback
  query invalidation/refetch after success
```

### Safe GET vs unsafe command

```text
Employee details read:
  safe GET;
  no local CSRF mechanics.

StartReview / ApproveReview / RejectReview:
  unsafe POST;
  must consume CC-CSRF-001 shared antiforgery behavior;
  no blind auto-replay after token refresh.
```

### DTO generation

```text
Do not handwrite final DTOs once generated OpenAPI types exist.

Until server contract exists, DTO sketches in client sidecars are target shape notes,
not final generated contract names.
```

## 3. Future Implementation Notes

```text
- Employee session/auth model may block client implementation.
- Employee request details route candidate: /employee/requests/:requestId.
- Prefer server-provided reviewState and reviewActionAvailability; client should not guess employee-relative action availability unless the contract explicitly gives all required fields.
- Details read should map 401/403/404/ProblemDetails to safe page states.
- Future command features should plug into details page action slot without moving command behavior into the details read sidecar.
```

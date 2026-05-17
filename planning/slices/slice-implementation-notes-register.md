# Slice Implementation Notes Register

Status: current / L2 Employee Review client command notes synchronized  
Scope: shared implementation notes that should be visible to future slice/implementation chats

## L2 Employee Review Client Command Notes

### L2-REVIEW-START-001.client

```text
- StartReview client action is a command sidecar, not part of details read sidecar.
- Render StartReview through Employee Request Details action slot.
- Details read DTO comes from SL-EMP-REQ-002.
- StartReviewResponseDto is compact command result and must not be used as full details DTO.
- StartReview client must not send Employee id; server resolves Employee from auth/session.
- StartReview is unsafe POST and must use shared CSRF-aware API helper.
- Do not blindly replay unsafe command after token refresh.
- On success, refresh details read state and dashboard/list state if present.
- Approve/Reject stay separate future sidecars.
```

Generated contract note:

```text
Do not invent final generated operation/type names.
Use exact names from generated OpenAPI after SL-EMP-REQ-003 backend implementation/generation.
```


## SL-EMP-REQ-001 Implementation Verification / Packaging Note

Source note:

```text
planning/slices/l2/SL-EMP-REQ-001-implementation-verification-and-packaging-note.md
```

Extracted conclusions:

```text
- client tests passed: 25 files, 121 tests;
- server project build passed with dotnet build;
- OpenAPI generation command:
  dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json;
- TypeScript API type generation command:
  npm.cmd --prefix energymanagement.client run generate:api-types;
- client build/test commands:
  npm --prefix .\energymanagement.client run build;
  npm --prefix .\energymanagement.client run test -- --run;
- `check:api` can fail when generated artifacts are only unstaged working-tree changes;
- if API shape changed, final implementation archive/commit must include:
  Shared/openapi.json
  energymanagement.client/src/shared/api/generated/openapi-types.ts;
- LF/CRLF warnings are not errors;
- final merge-ready packaging should also run dotnet test if integration tests are part of the gate.
```

Do not omit generated API artifacts from an implementation archive after API contract changes.

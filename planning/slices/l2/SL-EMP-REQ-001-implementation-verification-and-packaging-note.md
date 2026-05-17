# SL-EMP-REQ-001 — Implementation Verification And Packaging Note

Status: implementation verification note / generated-artifact workflow corrected  
Applies to: `SL-EMP-REQ-001 — Employee Request List Read`  
Scope: verification evidence summary, generated-artifact packaging requirement, final archive readiness checks

## 1. Verified So Far

The latest implementation-run notes indicate that the main verification path for `SL-EMP-REQ-001` passed.

Confirmed:

```text
client tests passed:
  25 files, 121 tests

server project builds:
  dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
  succeeded

OpenAPI generation works:
  dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
  generated Shared/openapi.json

TypeScript API types generation works:
  npm.cmd --prefix energymanagement.client run generate:api-types
  generated energymanagement.client/src/shared/api/generated/openapi-types.ts

client build/tests can be checked with:
  npm --prefix .\energymanagement.client run build
  npm --prefix .\energymanagement.client run test -- --run
```

The earlier `otnet build` command was a typo. The corrected `dotnet build` command succeeded.

`LF will be replaced by CRLF` warnings are not failures.

## 2. Correct API Generation Workflow

If backend/API shape changed, run from repository root:

```powershell
cd C:\enman\enman

# 1. Regenerate OpenAPI, no --check
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json

# 2. Regenerate TypeScript API types
npm.cmd --prefix energymanagement.client run generate:api-types

# 3. Check client build/tests
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

After this, generated files may be modified:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

This is normal when API shape changed.

## 3. Why `check:api` May Still Fail

`check:api` ends with:

```text
git diff --exit-code Shared/openapi.json energymanagement.client/src/shared/api/generated/openapi-types.ts
```

That final step means:

```text
After generation/check, generated artifacts must already be part of the expected state.
```

So:

```text
- in commit/PR workflow: include generated files in the commit;
- in archive workflow: include generated files in the implementation archive/handoff;
- if they remain only modified in working tree, git diff --exit-code intentionally fails.
```

## 4. Generated Artifact Packaging Rule

Because `SL-EMP-REQ-001` changes API shape, the final implementation patch/archive must include generated API artifacts produced by the repo workflow.

Include these files together with implementation source files:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Do not hand-edit these files.

Do not omit them from a final merge-ready archive if the server/API shape changed.

## 5. Client-Only Slice Caveat

A client-only slice should not change API shape and normally should not touch generated artifacts.

However, if backend endpoint/DTO changes were already added earlier and generated artifacts were not refreshed, `check:api` can still fail because the repo is already out of sync.

The fix is to regenerate and include generated artifacts in the appropriate API-contract sync or implementation handoff.

## 6. Remaining Verification Before Merge-Ready Archive

If server/integration tests are part of the final quality gate, run:

```powershell
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

If that also passes, `SL-EMP-REQ-001` can be packaged as a final merge-ready implementation archive containing:

```text
- implementation source files for the slice;
- related tests;
- Shared/openapi.json;
- energymanagement.client/src/shared/api/generated/openapi-types.ts;
- any planning docs intentionally updated for this implementation.
```

## 7. What This Note Does Not Do

This note does not prove the exact source diff.

It records packaging/verification conclusions from the run-log review so future archive chats do not omit generated artifacts after an API contract change.

This note is not a replacement for rerunning the final test/check commands in the working tree that will be archived.

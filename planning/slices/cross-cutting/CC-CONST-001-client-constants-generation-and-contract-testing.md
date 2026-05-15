# CC-CONST-001 — Client Constants Generation And Contract Testing

Status: implementation-ready  
Slice type: cross-cutting slice  
Layers: Tools + Shared artifacts + API integration tests + client contract support  
Depends on: API error contract  
Used by: any slice with client-facing error codes / ProblemDetails / DTO field errors

## 1. Purpose

Provide a generated client-facing constants artifact and testing strategy so server/client API contracts are not verified only by C# constant self-equality.

This slice gives the project an explicit implementation and test path for:

```text
server-owned constants
-> generated Shared/constants.json and Shared/errorcodes.json
-> client imports
-> API integration tests
-> critical behavioral literal contract tests
```

## 2. Why This Is A Cross-Cutting Slice

This is not an individual business scenario slice.

It is cross-cutting because many business slices may introduce or consume client-facing error codes, DTO field names, ProblemDetails extension names, or API constants.

### Observable/support behavior

```text
- generate-client-constants writes Shared/constants.json and Shared/errorcodes.json;
- generate-client-constants --check fails when committed artifacts are stale;
- --check does not modify files;
- API integration tests can read generated artifacts;
- critical behavioral error codes can be protected by literal contract tests.
```

### Implementation path

```text
- EnergyManagement.Tools command;
- ClientConstantsSnapshotFactory;
- ClientConstantsArtifacts;
- ClientConstantsJsonSerializer;
- ClientConstantsWriter;
- ClientConstantsChecker;
- Tools tests;
- API integration test helper.
```

### Independent testability

```text
- Tools.Tests can test generator/writer/checker without business slices;
- API integration tests can consume generated artifacts;
- client parser remains tested on the client side.
```

## 3. Cross-Cutting Inputs

Source inputs:

```text
- planning/api/api-error-contract.md;
- existing Shared/constants.json and Shared/errorcodes.json;
- existing client import path / generated JSON imports;
- server constants / error codes;
- ServerValidationError / ProblemDetails contract;
- future parent slice API error tables.
```

## 4. Pseudo Behavior Items

| ID | Behavior |
|---|---|
| CC-CONST-CMD-001 | Generate committed client constants artifacts. |
| CC-CONST-CMD-002 | Check committed artifacts without modifying files. |
| CC-CONST-ART-001 | `constants.json` and `errorcodes.json` have stable expected shape. |
| CC-CONST-ERR-001 | `ErrorCode` and `FieldName` contract is exposed to the client. |
| CC-CONST-TEST-001 | API integration tests can read generated artifact. |
| CC-CONST-TEST-002 | Critical behavioral codes can be literal-tested. |
| CC-CONST-NW-001 | Do not use hosted service generation as the primary path. |
| CC-CONST-NW-002 | Do not migrate FluentValidation `ErrorMessage`/`ErrorCode` blindly. |
| CC-CONST-NW-003 | Do not add regex convention tests or full golden-file tests initially. |

## 5. Coverage Overview

| Pseudo behavior item | Implemented by | Test coverage | Used by slices? | Status |
|---|---|---|---|---|
| CC-CONST-CMD-001 | Tools command + SnapshotFactory + Serializer + Writer | writer + serializer tests | all API/client slices that need constants | planned |
| CC-CONST-CMD-002 | Tools command + Checker | checker tests including no-modify | local/CI verification | planned |
| CC-CONST-ART-001 | Snapshot records + serializer | shape/casing/stability tests | client imports | planned |
| CC-CONST-ERR-001 | ServerValidationError contract snapshot | shape tests + API integration tests | API error handling slices | planned |
| CC-CONST-TEST-001 | ClientContractArtifacts integration-test helper | API integration tests | API slices | planned |
| CC-CONST-TEST-002 | literal integration assertions | selected API integration tests | critical behavioral errors | planned |
| CC-CONST-NW-001 | explicit command design | command/check workflow | all constants changes | accepted |
| CC-CONST-NW-002 | deferred FluentValidation note | inspection before migration | validation/API hardening | accepted |
| CC-CONST-NW-003 | test strategy rule | no tests now | Tools.Tests | accepted |

## 6. Implementation Flow

### C01 — CLI entry

Behavior item:

```text
CC-CONST-CMD-001
CC-CONST-CMD-002
```

High-level behavior:

```text
Developer runs EnergyManagement.Tools command.
```

Command examples:

```bash
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared --check
```

Implementation:

```text
Program.cs dispatches `generate-client-constants` to GenerateClientConstantsCommand.
GenerateClientConstantsOptions captures --out and --check.
```

Key point:

```text
--check is our custom command mode, not built-in dotnet behavior.
```

Routine details such as basic argument parsing can stay high-level unless they become error-prone.

### C02 — Output path and options

Behavior item:

```text
CC-CONST-CMD-001
CC-CONST-CMD-002
```

Implementation:

```text
GenerateClientConstantsOptions stores --out and --check.
ClientConstantsPathResolver resolves the output directory.
```

Current direction:

```text
Use explicit --out Shared first.
```

Open question:

```text
Default output path can be added later if needed.
```

### C03 — Snapshot creation

Behavior item:

```text
CC-CONST-ART-001
CC-CONST-ERR-001
```

Implementation:

```text
ClientConstantsSnapshotFactory creates in-memory snapshots from C# source constants.
```

Key responsibilities:

```text
- choose which constants are exposed to the client;
- create constants.json snapshot;
- create errorcodes.json snapshot;
- expose ServerValidationError field-name contract;
- keep JSON artifact shape explicit.
```

Important example:

```csharp
private static ServerValidationErrorContractSnapshot CreateServerValidationErrorContract()
{
    var instance = ServerValidationError.Create("", "");

    return new ServerValidationErrorContractSnapshot(
        FieldNameField: nameof(instance.FieldName),
        ErrorCodeField: nameof(instance.ErrorCode)
    );
}
```

Why this matters:

```text
Client parser should not hardcode FieldName/ErrorCode field-name strings.
```

### C04 — Artifact creation and serialization

Behavior item:

```text
CC-CONST-ART-001
```

Implementation:

```text
ClientConstantsArtifacts contains generated artifact strings.
ClientConstantsJsonSerializer serializes snapshots into deterministic JSON.
```

Key points:

```text
- indented JSON;
- expected casing preserved;
- stable output;
- no timestamps;
- no environment-specific values.
```

Routine details such as simple record construction stay high-level.

### C05 — Write mode

Behavior item:

```text
CC-CONST-CMD-001
```

Implementation:

```text
ClientConstantsWriter writes:
- Shared/constants.json;
- Shared/errorcodes.json.
```

Key responsibilities:

```text
- create output directory if missing;
- write both files;
- fail clearly if output path is invalid.
```

Tests:

```text
Writer_WritesConstantsAndErrorCodesFiles
Writer_CreatesOutputDirectory_WhenItDoesNotExist
Writer_Throws_WhenOutputPathIsAFile
```

### C06 — Check mode

Behavior item:

```text
CC-CONST-CMD-002
```

Implementation:

```text
ClientConstantsChecker compares generated artifact strings with committed Shared/*.json files.
```

Key point:

```text
--check must not write or repair files.
```

It should return a failure result when:

```text
- constants.json is outdated;
- errorcodes.json is outdated;
- either file is missing.
```

Tests:

```text
Checker_ReturnsSuccess_WhenFilesMatchGeneratedArtifacts
Checker_ReturnsFailure_WhenErrorCodesFileIsOutdated
Checker_ReturnsFailure_WhenConstantsFileIsOutdated
Checker_ReturnsFailure_WhenFileIsMissing
Checker_DoesNotModifyFiles_WhenMismatchExists
```

### C07 — API integration-test consumption

Behavior item:

```text
CC-CONST-TEST-001
```

Implementation:

```text
API integration tests read Shared/errorcodes.json through a helper such as ClientContractArtifacts.
```

Rule:

```text
For ordinary client-facing codes, expected value should come from generated artifact,
not directly from C# constants.
```

Sketch:

```csharp
var expectedCode = _clientContracts.GetErrorCode("Email", "IsRequired");

Assert.Equal(expectedCode, actualError.ErrorCode);
```

Why this matters:

```text
This verifies the real client-facing artifact used by the frontend.
```

### C08 — Literal integration contract tests for critical behavioral codes

Behavior item:

```text
CC-CONST-TEST-002
```

Rule:

```text
Literal tests are only for critical behavioral codes,
not for every validation code.
```

Criteria:

```text
- client takes a separate branch by this code;
- code controls stale/refetch behavior;
- code controls access denied / not found / forbidden state;
- code disables/removes actions or updates action availability;
- code starts a recovery path;
- changing it requires updating `.client.md`, scenario UI spec or client behavior coverage.
```

Example:

```csharp
Assert.Equal("request.review.request.not.in.review", actualError.ErrorCode);
```

Purpose:

```text
A literal test fails when a public behavioral signal is renamed accidentally.
```

### C09 — Client parser tests stay client-side

Behavior item:

```text
CC-CONST-TEST-001
```

Rule:

```text
Server/tool tests do not duplicate client parser behavior.
```

Client tests verify:

```text
- ProblemDetails parser;
- errors extension lookup;
- FieldName/ErrorCode reading;
- DTO field -> form field mapping;
- ErrorCode -> UI message/state mapping.
```

## 7. Target Tools Types

| Type | Responsibility |
|---|---|
| `Program.cs` | Command dispatch and exit code. |
| `GenerateClientConstantsCommand` | Orchestrates generation/check mode. |
| `GenerateClientConstantsOptions` | Stores `--out` and `--check`. |
| `ClientConstantsPathResolver` | Resolves output directory. |
| `ClientConstantsSnapshotFactory` | Builds in-memory snapshots from source constants. |
| `ClientConstantsArtifacts` | Holds serialized `constants.json` and `errorcodes.json` strings. |
| `ClientConstantsJsonSerializer` | Produces stable JSON with expected casing/format. |
| `ClientConstantsWriter` | Writes generated artifacts. |
| `ClientConstantsChecker` | Compares generated artifacts with committed files. |
| `ClientConstantsCheckResult` | Reports check success/failure and mismatched files. |

This table is not a full class reference.

Keep class details in flow only where they clarify behavior.

If class/method details start dominating the slice flow, extract them into:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.impl.md
```

Do not create `.impl.md` in advance.

## 8. Test Plan

### Tools unit tests

Minimum set:

```text
ClientConstantsSnapshotFactoryTests
- CreateErrorCodesSnapshot_ContainsRequiredTopLevelSections
- CreateErrorCodesSnapshot_ContainsServerValidationErrorContract
- ErrorCodes_AreUnique

ClientConstantsJsonSerializerTests
- Serializer_ProducesStableOutput
- Serializer_KeepsExpectedPropertyCasing

ClientConstantsWriterTests
- Writer_WritesConstantsAndErrorCodesFiles
- Writer_CreatesOutputDirectory_WhenItDoesNotExist
- Writer_Throws_WhenOutputPathIsAFile

ClientConstantsCheckerTests
- Checker_ReturnsSuccess_WhenFilesMatchGeneratedArtifacts
- Checker_ReturnsFailure_WhenErrorCodesFileIsOutdated
- Checker_ReturnsFailure_WhenConstantsFileIsOutdated
- Checker_ReturnsFailure_WhenFileIsMissing
- Checker_DoesNotModifyFiles_WhenMismatchExists
```

### API integration tests

Use generated artifact for ordinary client-facing codes.

Verify:

```text
- ProblemDetails response status;
- errors extension exists;
- FieldName uses API DTO field names;
- ErrorCode matches generated Shared/errorcodes.json artifact.
```

### Literal integration contract tests

Use only for critical behavioral codes.

### Client tests

Client side owns parser/mapping/UI behavior tests.

## 9. Not Now

Do not do in the first implementation unless explicitly requested:

```text
- full golden-file tests;
- error-code format regex/convention tests;
- full generated API client;
- .NET upgrade;
- FluentValidation ErrorCode migration without inspection;
- client parser redesign without client tests.
```

## 10. Consumer Rule For Business Slices

When a business slice introduces a client-facing error code:

```text
1. Add C# source constant.
2. Classify code stability:
   - ordinary validation;
   - important domain;
   - critical behavioral.
3. Add it to parent slice API error table.
4. Regenerate Shared/*.json.
5. Add/adjust API integration test:
   - generated JSON expectation for ordinary codes;
   - literal expectation for critical behavioral codes.
6. If client work exists, `.client.md` maps ErrorCode -> UI handling.
7. Run generate-client-constants --check.
```

Parent slice API table:

| Error code | FieldName | HTTP status | Stability | Client handling | Literal test? |
|---|---|---:|---|---|---|

Client sidecar table:

| Error code | Client branch? | Handling | Literal test? |
|---|---:|---|---:|

## 11. Local Questions

| ID | Question | Assumption / current direction | Status |
|---|---|---|---|
| Q-CC-CONST-001 | Exact source constants project now or later? | Start with existing server/shared constants, move to API contracts project later. | open |
| Q-CC-CONST-002 | Exact output path default? | Keep `--out Shared` explicit first. | accepted for first step |
| Q-CC-CONST-003 | Exact OpenAPI TypeScript tool? | Out of scope for this cross-cutting slice. | open |
| Q-CC-CONST-004 | FluentValidation ErrorMessage vs ErrorCode? | Deferred inspection note. | open |
| Q-CC-CONST-005 | Add golden tests now? | No. | accepted |
| Q-CC-CONST-006 | Add regex convention tests now? | No. | accepted |
| Q-CC-CONST-007 | Should client constants generator run in server startup? | No; explicit command only. | accepted |

## 12. ADR Impact

Decision notes / ADR candidates:

```text
- generated client constants as cross-cutting slice;
- constants testing strategy;
- no convention/golden tests initially;
- explicit command over hosted service;
- critical behavioral literal contract tests.
```

No full numbered ADR is created by this slice draft.

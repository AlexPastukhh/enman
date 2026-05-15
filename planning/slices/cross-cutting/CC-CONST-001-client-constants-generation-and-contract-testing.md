# CC-CONST-001 — Client Constants Generation And Contract Testing

Status: implementation-ready  
Slice type: cross-cutting slice  
Layers: Tools + Shared artifacts + API integration tests + client contract support  
Depends on: API error contract, client/server contract principles  
Used by: any slice with client-facing error codes / ProblemDetails / DTO field errors / semantic constants

## 1. Purpose

Provide a generated client-facing semantic constants artifact and testing strategy so server/client API contracts are not verified only by C# constant self-equality.

This slice covers:

```text
server-owned semantic constants
-> generated Shared/constants.json and Shared/errorcodes.json
-> client imports
-> API integration tests
-> critical behavioral literal contract tests
```

## 2. Why This Is A Cross-Cutting Slice

This is not an individual business scenario slice.

It is cross-cutting because many business slices may introduce or consume:

```text
client-facing error codes
ProblemDetails extension names
ServerError / ServerValidationError field names
temporary route constants while OpenAPI migration is incomplete
```

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

## 3. Inputs / Sources

| Source | Purpose |
|---|---|
| `planning/api/client-server-contract-principles.md` | OpenAPI vs generated constants split |
| `planning/api/api-error-contract.md` | ProblemDetails / ServerError / ErrorCode contract |
| `planning/api/client-constants-generation.md` | API relationship note |
| `Shared/constants.json` | Generated semantic constants artifact |
| `Shared/errorcodes.json` | Generated error-code artifact |
| server constants / error codes | Source of truth |
| parent slice API error tables | Consumer source |
| `.client.md` sidecars | Client handling consumer |

## 4. Concern-Derived Behavior Items

| ID | Behavior | Concern flow step |
|---|---|---|
| CC-CONST-SRC-001 | Server C# constants are source of truth for semantic constants. | F01 |
| CC-CONST-ART-001 | Tool generates committed `Shared/constants.json` and `Shared/errorcodes.json`. | F02 |
| CC-CONST-CMD-001 | Normal command writes generated constants artifacts. | F02 |
| CC-CONST-CMD-002 | Check mode compares committed artifacts without modifying files. | F03 |
| CC-CONST-ERR-001 | ErrorCode and FieldName contract is exposed to client artifact. | F04 |
| CC-CONST-CL-001 | Client imports generated semantic constants and does not hardcode strings. | F05 |
| CC-CONST-TEST-001 | API integration tests can read generated artifact. | F06 |
| CC-CONST-TEST-002 | Critical behavioral codes can be literal-tested. | F07 |
| CC-CONST-NW-001 | Do not use hosted service generation as the primary path. | F08 |
| CC-CONST-NW-002 | Do not migrate FluentValidation `ErrorMessage`/`ErrorCode` blindly. | F08 |
| CC-CONST-NW-003 | Do not add regex convention tests or full golden-file tests initially. | F08 |
| CC-CONST-NW-004 | Do not treat route constants as long-term replacement for OpenAPI route contract. | F08 |

## 5. Coverage Overview

| Behavior item | Concern flow | Implementation flow | Test coverage | Status |
|---|---|---|---|---|
| CC-CONST-SRC-001 | F01 | I01 | snapshot tests | planned |
| CC-CONST-ART-001 | F02 | I03-I05 | writer + serializer tests | planned |
| CC-CONST-CMD-001 | F02 | I02/I05 | command/writer tests | planned |
| CC-CONST-CMD-002 | F03 | I06 | checker no-modify tests | planned |
| CC-CONST-ERR-001 | F04 | I01/I03 | shape + API tests | planned |
| CC-CONST-CL-001 | F05 | I09 | client parser/import tests | planned |
| CC-CONST-TEST-001 | F06 | I07 | API integration tests | planned |
| CC-CONST-TEST-002 | F07 | I08 | selected literal tests | planned |
| CC-CONST-NW-001 | F08 | I02/I06 | command-only workflow | accepted |
| CC-CONST-NW-002 | F08 | I10 | inspection before migration | accepted |
| CC-CONST-NW-003 | F08 | I10 | not in first implementation | accepted |
| CC-CONST-NW-004 | F08 | I10 | OpenAPI migration docs | accepted |

## 6. Semantic Constants Concern Flow

### F01 — Server constants remain semantic source of truth

Server C# constants define semantic values that client must not invent.

Covers:

```text
CC-CONST-SRC-001
```

Examples:

```text
stable client-facing error codes
ProblemDetails extension names
ServerError / ServerValidationError field names
```

### F02 — Tool generates committed constants artifacts

Explicit tool command generates artifacts.

Covers:

```text
CC-CONST-ART-001
CC-CONST-CMD-001
```

Artifacts:

```text
Shared/constants.json
Shared/errorcodes.json
```

Required behavior:

```text
normal generation writes files only when command is explicitly run.
```

### F03 — Check mode detects stale artifacts without writing

Check mode compares generated output with committed files.

Covers:

```text
CC-CONST-CMD-002
```

Required behavior:

```text
--check fails if artifacts are missing/outdated;
--check does not repair or modify files.
```

### F04 — Client can parse semantic error contract

Generated constants expose field/extension names required by client parser.

Covers:

```text
CC-CONST-ERR-001
```

Required behavior:

```text
client parser should not hardcode errors extension name,
FieldName property name or ErrorCode property name.
```

### F05 — Client imports generated constants

Client uses generated artifacts, not handwritten string copies.

Covers:

```text
CC-CONST-CL-001
```

Required behavior:

```text
ErrorCode -> UI message/behavior mapping uses imported constants.
```

### F06 — API tests check against client-facing artifact

Most client-facing API error tests read expected values from generated artifact.

Covers:

```text
CC-CONST-TEST-001
```

Required behavior:

```text
test real HTTP response against the same artifact the client imports.
```

### F07 — Critical behavioral codes can be literal-protected

Critical behavioral error codes may have literal integration tests.

Covers:

```text
CC-CONST-TEST-002
```

Criteria:

```text
client takes a separate branch;
code controls stale/refetch/access/not-found behavior;
changing it requires client/docs/test update.
```

### F08 — Boundaries and non-goals are explicit

Constants generation stays separate from OpenAPI and server startup.

Covers:

```text
CC-CONST-NW-001
CC-CONST-NW-002
CC-CONST-NW-003
CC-CONST-NW-004
```

Required behavior:

```text
no hosted service generation;
no blind FluentValidation migration;
no regex/golden tests initially;
route constants are temporary while OpenAPI transition is incomplete.
```

## 7. Implementation Flow

### I01 — Snapshot creation

Concern flow:

```text
F01
F04
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

### I02 — CLI command

Concern flow:

```text
F02
F03
F08
```

Command examples:

```bash
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared --check
```

Key point:

```text
--check is our custom command mode, not built-in dotnet behavior.
```

### I03 — Artifact creation and serialization

Concern flow:

```text
F02
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

### I04 — Output path and options

Concern flow:

```text
F02
F03
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

### I05 — Write mode

Concern flow:

```text
F02
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

### I06 — Check mode

Concern flow:

```text
F03
```

Implementation:

```text
ClientConstantsChecker compares generated artifact strings with committed Shared/*.json files.
```

Key point:

```text
--check must not write or repair files.
```

Failure cases:

```text
- constants.json is outdated;
- errorcodes.json is outdated;
- either file is missing.
```

### I07 — API integration-test consumption

Concern flow:

```text
F06
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

### I08 — Literal integration contract tests for critical behavioral codes

Concern flow:

```text
F07
```

Rule:

```text
Literal tests are only for critical behavioral codes,
not for every validation code.
```

### I09 — Client parser tests stay client-side

Concern flow:

```text
F05
F06
```

Client tests verify:

```text
- ProblemDetails parser;
- errors extension lookup;
- FieldName/ErrorCode reading;
- DTO field -> form field mapping;
- ErrorCode -> UI message/state mapping.
```

### I10 — Deferred/non-goal handling

Concern flow:

```text
F08
```

Do not do in first implementation unless explicitly requested:

```text
- full golden-file tests;
- error-code format regex/convention tests;
- full generated API client;
- .NET upgrade;
- FluentValidation ErrorCode migration without inspection;
- client parser redesign without client tests;
- long-term new route constants when OpenAPI can cover routes.
```

## 8. Target Tools Types

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

## 9. Test Plan

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

## 11. Local Questions

| ID | Question | Assumption / current direction | Status |
|---|---|---|---|
| Q-CC-CONST-001 | Exact source constants project now or later? | Start with existing server/shared constants, move to API contracts project later. | open |
| Q-CC-CONST-002 | Exact output path default? | Keep `--out Shared` explicit first. | accepted |
| Q-CC-CONST-003 | Exact route constants removal plan? | keep legacy routes until client OpenAPI migration. | open |
| Q-CC-CONST-004 | FluentValidation ErrorMessage vs ErrorCode? | Deferred inspection note. | open |
| Q-CC-CONST-005 | Add golden tests now? | No. | accepted |
| Q-CC-CONST-006 | Add regex convention tests now? | No. | accepted |

## 12. ADR Impact

Decision notes / ADR candidates:

```text
- generated semantic constants as cross-cutting slice;
- constants testing strategy;
- no convention/golden tests initially;
- explicit command over hosted service;
- critical behavioral literal contract tests;
- route constants are temporary during OpenAPI migration.
```

No full numbered ADR is created by this slice draft.

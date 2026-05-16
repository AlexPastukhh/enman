# L1 Slice Drafting Guide

Status: current / strict example-driven drafting, client short-draft rules and flow separation synchronized  
Scope: business slices, cross-cutting/helper slices, client sidecars, scenario source intake, implementation flow, extension/change points, questions, registers and tests

## 1. Draft-Driven Discovery Gate

Before drafting any slice, read:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
planning/slices/draft-driven-discovery-principles.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/change-extension-points-principles.md
```

Client sidecar drafters must also read:

```text
planning/client/README.md
planning/client/client-layering-for-read-and-command-slices.md
planning/slices/client-slice-short-draft-rules-and-example.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/api/client-server-contract-principles.md
```

## 2. Example-Driven Drafting Rule

New chats must draft by existing examples.

Do not invent a new form, new section order or new terminology just because it seems nicer.

For client short drafts, copy the canonical shape from:

```text
planning/slices/client-slice-short-draft-rules-and-example.md
planning/slices/SL-APPL-002-account-applicant-parties-read.client.md
```

This is intentional. Consistency is more important than creativity.

## 3. Scenario Flow Source Rule

Before writing Scenario Flow or Behavior Coverage, read:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Then read the linked scenario text, DATA, UI and behavior files.

Scenario Flow is the part of the scenario that belongs to the current slice.

It is not necessarily the whole scenario.

A scenario can be implemented by several slices and extension slices.

## 4. Scenario Flow vs Implementation Flow

Scenario Flow shows user/system behavior from scenario specs.

Implementation Flow shows code/layer responsibilities.

Bad Scenario Flow:

```text
[Controller]
Extracts account id
        ↓
[Repository]
Loads ApplicantParties
        ↓
[React Query]
Caches response
```

Good Scenario Flow:

```text
[Signed-in Client]
opens Applicant Parties page / section
        ↓
Client sees current/default ApplicantParty templates highlighted
        ↓
Client sees other saved ApplicantParties below
```

Good Client Implementation Flow must show:

```text
[Layer]
path/to/file

Lives here:
Uses:
Owns:
Does not own:
```

## 5. Behavior Items Rule

Behavior items are not implementation details.

Do not treat these as behavior items:

```text
- query key includes filters;
- fetchJson is called;
- generated type exists;
- route param is parsed;
- cache invalidation happens;
- ApplicantPartyId is returned;
- repository method exists.
```

They may be implementation notes, API contract notes or test plan items.

Behavior items must come from scenario/UI/behavior sources or cross-cutting concern sources.

If source IDs are missing, write `Source BI TBD` and mark it as a source gap.

## 6. Scope / Out-of-Scope / Related-Slices Rule

Every non-trivial slice draft must explicitly define:

```text
Scope
Out of scope
Related slices / owners
Future extension points
```

Out-of-scope items must point to an owner:

```text
- existing slice;
- future slice;
- cleanup task;
- client sidecar;
- cross-cutting/helper slice;
- explicit not planned.
```

## 7. Client Sidecar Short Draft Template

Use exactly:

```text
# SLICE-ID.client — Title

Status:
Parent slice:
Slice type:
Architecture direction:
Backend/API contract evidence, when relevant:

## 1. Scope
## 2. Out of Scope
## 3. Related Slices / Owners
## 4. Visual UI / Scenario Flow
## 5. Visual Client Implementation Flow
## 6. Client API / Server Contract
## 7. Questions / Decisions
## 8. Extension / Change Points
## 9. Behavior Coverage
## 10. Client / Component / E2E Verification Plan
## 11. Implementation Checklist
## 12. Next Step
```

## 8. Client Read vs Command Placement

Read slices:

```text
pages + entities + shared/api + generated contracts
```

Command/user-action slices:

```text
pages + features + entities + shared/api + generated contracts
```

Read-only UI belongs in `entities/<entity>/ui`.

Command/action UI belongs in `features/<action>/ui`.

## 9. Behavior Coverage Is Not Test Coverage

Behavior Coverage answers:

```text
Does the draft cover source behavior?
```

Test / Verification Plan answers:

```text
How will implementation be verified?
```

Do not mix these tables.

## 10. Questions / Decisions Rule

Every non-trivial question should include:

```text
ID
Status
Question
Assumption / current direction
Impact
Shared register / local-only reason
```

Order:

```text
open -> blocked -> assumptions -> future review -> accepted -> resolved/superseded
```

## 11. Shared Register Sync Rule

Use:

```text
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

Update them when local slice changes affect source mapping, future work, extension pressure, implementation notes or shared questions.

## 12. OpenAPI / Generated Artifact Rule

If a slice changes API contract, generated artifacts must come from repo commands, not manual edits.

Read:

```text
planning/api/generated-artifact-check-workflow.md
planning/api/openapi-contract-generation.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

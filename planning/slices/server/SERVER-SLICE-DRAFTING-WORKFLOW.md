# Server Slice Drafting Workflow

Status: current local server workflow / applies root slice draft authoring workflow
Doc version: v0.1.0

## 1. Purpose

Server slice drafts define backend/API/domain behavior, implementation responsibility and generated contract impact.

This workflow applies the root slice draft authoring workflow to server/backend/API slice drafts.

Use root workflow for the general authoring process:

```text
planning/slices/slice-draft-authoring-workflow.md
```

Templates define output shape. Principles define rules. This workflow defines server-specific drafting steps.

## 2. Required Reads

Before drafting, read root slice workflow and principles:

```text
planning/slices/slice-responsibility-map.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/slice-draft-authoring-workflow.md
planning/slices/slice-test-plan-workflow.md
```

Then read server-specific docs:

```text
planning/slices/server-implementation-principles.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
```

Then identify source inputs:

```text
scenario/source files;
slice source mapping;
domain files;
behavior items;
cross-cutting behavior / concern sources;
API/testing/generated artifact owners when relevant.
```

Current slice source mapping owner:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

## 3. Drafting Order

Follow the root workflow first, then apply this server-specific order:

```text
1. Confirm that the work belongs to the server slice layer.
2. Identify scenario/source mapping for this slice.
3. Identify what scenario/source behavior this slice implements.
4. Identify related behavior this slice explicitly does not implement.
5. Identify cross-cutting concerns around the scenario and whether they apply to this slice.
6. List concrete prerequisites and planned follow-ups only when known.
7. Run future-change check without expanding scope.
8. Read applicable server implementation principles.
9. Define domain methods / domain behavior contract when domain behavior is central.
10. Define implementation components overview before implementation flow.
11. Define implementation flow by responsibility boundary.
12. Define API contract and generated artifact impact.
13. Define DTO validation boundary.
14. Define repository/persistence/no-partial-write notes.
15. Define questions/decisions.
16. Define Behavior Coverage.
17. Define Test / Verification Plan and Behavior-to-Test Trace.
18. Define implementation checklist only if preparing implementation or syncing existing code.
19. Run local/global sync check from slice-draft-authoring-workflow.md.
20. Finish with Guardrail Summary.
```

## 4. Source And Scope Rules

Start from scenario/source behavior, not from a random set of files.

A server slice draft must state:

```text
which scenario/source behavior it implements;
which related behavior it does not implement;
which owners or future slices cover excluded behavior;
which source/domain/cross-cutting files were used;
which sources are missing/provisional/blocked.
```

Do not turn the server slice draft into a full scenario registry or domain model draft.

## 5. Implementation Principles Step

Before writing Implementation Components Overview or Implementation Flow, read:

```text
planning/slices/server-implementation-principles.md
```

Use it to classify:

```text
controller boundary;
DTO validation boundary;
application/handler boundary;
domain boundary;
persistence / transaction / no-partial-write boundary;
command/read split;
API result/error mapping;
testing implications.
```

If a principle is only a current direction or implementation evidence, do not present it as an accepted rule.

## 6. Component Names And Drift

Component/class/method names in a draft are semantic first-pass names.

Not drift by itself:

```text
service/controller/DTO/repository names differ while responsibility and behavior are equivalent.
```

Real drift:

```text
controller owns lifecycle/turn rules;
validator checks domain ownership/status;
service bypasses domain method for domain-owned invariant;
failed command partially writes state;
command returns wrong response shape;
UI visibility is used as security boundary.
```

Use:

```text
planning/slices/slice-draft-authoring-principles.md
```

## 7. Validation Rule

FluentValidation / DTO validation owns shape.

Application/domain own:

```text
actor ownership;
visibility/security decisions;
current turn;
aggregate status;
lifecycle rules;
state transition validity.
```

For server request validation, use:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

## 8. Test / Verification Rule

Primary proof should use public boundary and persisted/observable outcome.

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

Behavior-to-Test Trace must include required assertions.

Direct DB setup is allowed only to arrange scenario preconditions.

Direct DB assertions are allowed to observe persisted behavior outcome.

Repository mocks, handler call order and SaveChanges count are not primary proof.

## 9. Generated Artifact Rule

Generated artifacts come from repo commands, not manual edits.

If API shape, generated TypeScript types, generated constants or error code artifacts are affected, record required generation/check steps in the draft.

Use current API/cross-cutting docs when relevant:

```text
planning/api/
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

## 10. Implemented Slice Rule

If the slice draft already has implementation, do not rewrite it only to match the newest template.

Use:

```text
planning/documentation/status-reconciliation-workflow.md
```

Check current sources, domain docs, existing draft, current implementation files and current tests before updating the draft.

## 11. Guardrails

```text
Do not broaden scope silently.
Do not invent behavior items inside a slice draft.
Do not manually set domain statuses in handlers when domain methods exist.
Do not move lifecycle/domain rules into validators/controllers.
Do not add per-command status enums unless explicitly accepted.
Do not treat harmless naming differences as implementation drift.
Do not leave required assertions outside Behavior-to-Test Trace.
Do not manually edit generated artifacts.
```

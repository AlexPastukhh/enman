# Documentation Examples Index

Status: current documentation-layer examples index / coverage tracker  
Scope: navigation for documentation-layer working examples and explicit missing/deferred example decisions

## 1. Purpose

This file indexes working examples for documentation-layer workflows, templates, response commands, output modes and draft formats.

It answers:

```text
Which examples exist?
Which owner files do they demonstrate?
Which expected examples are intentionally missing or deferred?
Where should a future chat look before creating a new example?
```

This file is navigation and coverage tracking only. It does not own command routing, source modes, output modes, permission boundaries or workflow activation.

## 2. Owner Rules

Examples are supporting artifacts.

```text
Rules define correctness.
Templates define shape.
Workflows define process.
Use-case maps define routing and output mode.
Examples demonstrate correct application.
```

If an example needs command/source/output/permission logic, link to the owner file instead of copying that logic here.

## 3. Example Coverage Table

No filled documentation-layer examples are added yet.

| Example ID | Type | Owner | Related use-case/workflow | Status | File | Missing/deferred reason |
|---|---|---|---|---|---|---|
| `REPLACEMENT-ARCHIVE-PACKAGE-v1` | output example | `planning/replacement-file-generation-guide.md` | replacement archive/package generation | deferred | _not created yet_ | Candidate from chat history; add after example infrastructure is committed. |
| `POST-APPLY-PRESERVATION-CHECK-v1` | response/check example | `planning/replacement-file-generation-guide.md` and `planning/planning-use-case-map.md` | `проверь` after replacement archive/package application | deferred | _not created yet_ | Candidate from chat history; should show applied + preserved review. |
| `DIFF-CAPTURE-UTF8-CLIPBOARD-v1` | command-block example | `planning/replacement-file-generation-guide.md` | diff capture and clipboard commands | deferred | _not created yet_ | Candidate from chat history; should show `git --output` + UTF-8 clipboard + mojibake fallback. |
| `REPLACEMENT-ARCHIVE-NEW-FILES-DIFF-v1` | command-block example | `planning/replacement-file-generation-guide.md` | replacement archive diff checks for packages that add files | deferred | _not created yet_ | Candidate from chat history; should show `$newFiles`, `git add -N`, and full diff capture for added files. |
| `REPLACEMENT-ARCHIVE-CONVERSATION-LOOP-v1` | conversation workflow example | `planning/replacement-file-generation-guide.md` | replacement archive conversation review loop | deferred | _not created yet_ | Candidate from chat history; should show assistant archive output, user diff, assistant review, commit command and remote verification. |
| `SOURCE-USAGE-CASCADE-PILOT-FRAMEWORK-v1` | register/framework example | `planning/documentation/source-usage-cascade-governance-plan.md` | source usage cascade pilot framework | deferred | _not created yet_ | Framework and pilot skeleton are added first; add a filled example after the real SC-13D pilot row set is audited. |
| `DOCUMENTATION-ACTION-LOG-ENTRY-v1` | log-entry example | `planning/documentation/documentation-action-log.md` | documentation action log entries | deferred | _not created yet_ | Add after the action log format is used on at least one follow-up update; should show concise logical action entry with PMR relation. |
| `NEW-CHAT-ONBOARDING-v1` | response example | `planning/planning-use-case-map.md` | New Chat Onboarding / first planning pass | deferred | _not created yet_ | Candidate from chat history; should show compact onboarding answer. |
| `ACCEPTED-COMMAND-NO-REINVENTION-v1` | guardrail example | `planning/planning-use-case-map.md` | accepted command / no reinvention | deferred | _not created yet_ | Candidate from chat history; should show stop-and-explain instead of silent mode switch. |

## 4. Adding A New Example

Before adding a new example, use:

```text
planning/documentation/example-coverage-workflow.md
```

A new example should include:

```text
- what it demonstrates;
- owner workflow/template/use-case link;
- the example content;
- a short note about why it is valid;
- current/deferred/historical status if relevant.
```

Do not add examples that copy routing/source/output/permission logic from owner files.

## 5. Missing Or Deferred Examples

If a template, workflow, command, output mode or draft format does not have an example, record one of:

```text
not needed: <reason>
deferred: <reason and target condition>
covered: <existing example id>
```

Do not leave important template/output/command behavior without either an example or a recorded reason.

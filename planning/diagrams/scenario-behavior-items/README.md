# Scenario Behavior Items

Status: current behavior-item source area / ApplicantParty template-per-type model synchronized  
Scope: source behavior items for scenarios, UI specs and cross-cutting concerns

## Purpose

Behavior item files provide stable source behavior IDs for slice drafts, client sidecars and tests.

Slice/client chats must not invent behavior items from local questions or implementation notes.

Use with:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

## Current Behavior Item Files

```text
SC-04-request-creation-behavior-items.md
SC-05-my-requests-behavior-items.md
SC-10-applicant-data-behavior-items.md
SC-10B-my-applicant-parties-behavior-items.md
CC-CSRF-001-antiforgery-behavior-items.md
```

## ApplicantParty Direction

Current behavior sources use:

```text
many saved ApplicantParties
+ one current/default template per applicant type
+ no hidden replacement on create
+ explicit future default selection
```

Superseded wording:

```text
one current active ApplicantParty per account
replacement makes previous current inactive
```

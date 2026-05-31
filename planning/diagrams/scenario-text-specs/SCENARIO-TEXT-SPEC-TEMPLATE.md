# Scenario Text Spec Template

Status: canonical template for new or meaningfully updated core/business scenario text specs  
Doc version: v0.1.0  
Scope: core scenario capability, inline DATA, behavior links and UI/UX alignment

Use this template for new core/business scenario text specs and for intentional rewrites of existing current specs.

Existing scenario specs may keep older structures during migration. Do not rewrite old scenario specs only to match this template.

# `<SC-ID>` — `<Scenario Name>`

Status: draft / current / needs review / historical  
Source type: core/business scenario text spec  
Actors:

Related artifacts:
- Reusable DATA concepts:
- UI scenario:
- Behavior items:
- Clarifications:
- Domain map:
- Slice map:

## 1. Purpose

Describe the business capability this scenario defines.

Template:

```text
This scenario describes how <actor> can <business capability>
so that <business outcome>.
```

Do not describe UI layout, DTOs, API contracts, domain classes or implementation steps here.

## 2. Statement Status / Source

Use this section to separate requirement strength.

### Direct requirements

- ...

### Accepted directions / accepted decisions

Use when behavior was not a direct original requirement, but is accepted as current scenario scope.

- ...

### Assumptions

- ...

### Future / deferred

- ...

### Open questions

- ...

### Deprecated / stale context

- ...

## 3. Actor / Context

Actor:
- ...

Business context:
- ...

User goal:
- ...

Related role/account state:
- ...

## 4. Entry Points

How the actor reaches this scenario in business/user terms.

- ...

UI routes may be referenced, but detailed UI belongs to UI scenario specs.

## 5. Preconditions

Business preconditions:
- ...

State preconditions:
- ...

Permission/access preconditions:
- ...

Out of scope:
- ...

## 6. DATA / Scenario Information

This section owns scenario-local DATA by default.

DATA means scenario information: what the actor enters, sees, selects, filters/searches by, attaches/uploads, references or receives as result/feedback.

DATA is not DTO, API contract, UI layout, DB schema, domain model or test assertion.

### 6.1 Entered by actor

| DATA item | Meaning | Required? | Source/status | UI/UX note |
|---|---|---:|---|---|
| ... | ... | yes/no | direct / accepted / assumption | none / general pattern / specific / see UI scenario |

### 6.2 Seen by actor

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| ... | ... | ... | ... |

### 6.3 Selected / referenced

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| ... | ... | ... | ... |

### 6.4 Filtered / searched

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| ... | ... | ... | ... |

### 6.5 Attached / uploaded

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| ... | ... | ... | ... |

### 6.6 Result / feedback information

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| ... | ... | ... | ... |

### 6.7 Not DATA

Use this section to prevent confusion.

DTO/API contract:
- ...

UI layout:
- ...

Domain invariant:
- ...

Test assertion:
- ...

## 7. Extracted / Reusable DATA Concepts

Use only when DATA is shared, large or needs a stable cross-scenario reference.

| Concept | File | Used by | Reason for extraction |
|---|---|---|---|
| ... | `planning/diagrams/scenario-data/...` | ... | reused / large / audit / transitional |

Rule:

```text
Scenario DATA starts inline.

Extract to `scenario-data/` only when DATA becomes a reusable business-data concept,
is too large for one scenario, needs separate review/audit, or needs a stable cross-scenario reference.
```

## 8. Main Flow

1. ...
2. ...
3. ...

Each step should describe business/user behavior, not implementation.

## 9. Branches / Alternatives

### Branch A — `<name>`

Condition:
- ...

Flow:
1. ...
2. ...

Outcome:
- ...

### Branch B — `<name>`

Condition:
- ...

Flow:
1. ...
2. ...

Outcome:
- ...

## 10. Business Rules / Invariants

Rules that must be true in this scenario.

| Rule | Source/status | Notes |
|---|---|---|
| ... | direct / accepted / assumption | ... |

Do not turn every rule into domain design here. Domain interpretation belongs downstream.

## 11. Observable Outcomes

Successful outcome:
- ...

Failure/blocked outcomes:
- ...

No-write / preservation outcomes:
- ...

User-visible outcome/feedback:
- ...

## 12. UI / UX Alignment

This scenario does not define UI layout or implementation.

UI scenario:
- `planning/diagrams/scenario-ui-specs/...`

Presentation/UX summary:
- ...

No scenario-specific UI requirement:
- ...

Consistency questions:
- ...

Rule:

```text
UI scenario presents DATA and business behavior.
It must not silently create or change business requirements.
If UI wording introduces business meaning, record a question, accepted decision, future/deferred item or correction.
```

## 13. Behavior Item Links

Behavior items file:
- `planning/diagrams/scenario-behavior-items/...`

Important behavior groups:

CMD:
- ...

LC:
- ...

IBS:
- ...

VI:
- ...

UCQ:
- ...

READ:
- ...

INT / FUT / NW:
- ...

UI/UX projection notes:
- ...

UI-only behavior entries:
- only if independently presentation/interaction-specific and source-linked.

## 14. Downstream Notes

Domain:
- ...

Slice:
- ...

Client/UI:
- ...

Testing:
- ...

Diagrams:
- ...

## 15. Questions / Decisions

Open:
- ...

Accepted:
- ...

Deferred:
- ...

Future:
- ...

## 16. Change Log / Source Delta

- ...

## Appendix A. DATA Item Mini-Template

```text
DATA item:
- <name>

Meaning:
- <business meaning>

Source/status:
- direct requirement / accepted direction / assumption / future / deferred / open question

Used in scenario:
- entered / seen / selected / filtered / attached / referenced / feedback

UI/UX note:
- none
- general pattern: form / table / card / detail section / status badge / feedback block / dialog / action button
- specific: ...
- see UI scenario: ...
```

## Appendix B. Reusable DATA Concept Extraction Mini-Template

```text
Reusable DATA concept:
- <name>

Extract to:
- planning/diagrams/scenario-data/<concept>.md

Extract because:
- reused by scenarios: ...
- too large for one scenario: yes/no
- needs separate audit: yes/no
- downstream stable reference needed: yes/no

Local scenario variation:
- ...
```

## Appendix C. Behavior Link Mini-Template

```text
Behavior item:
- <ID>

Business behavior:
- ...

Source:
- core scenario section:
- DATA item:
- reusable DATA concept, if any:

UI/UX projection:
- none / general pattern / specific / see UI scenario

Downstream:
- domain:
- slice:
- client:
- testing:
```

## Appendix D. UI-Only Behavior Mini-Template

```text
UI-only behavior:
- <ID or short name>

Presentation / interaction requirement:
- ...

Source:
- UI scenario section:
- related DATA item:
- related business behavior item, if any:

Why not attached to one business behavior item:
- ...

Downstream:
- client sidecar / UI test / E2E / a11y:
```

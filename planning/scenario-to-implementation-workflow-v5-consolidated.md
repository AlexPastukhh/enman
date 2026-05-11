# Scenario-To-Implementation Planning Workflow

Version: v5  
Status: consolidated baseline for future planning  
Scope: reusable project-planning method, not a final design for one specific system

---

## 0. Purpose

This document captures the full planning workflow for moving from clean user-facing scenarios to domain modeling, aggregate design, scenario-to-slice implementation planning, changeability planning, ADRs, tests and delivery.

The core problem this workflow solves:

```text
How to keep use cases clean and user-facing,
while still planning implementation deeply enough
to build the system in extensible, testable slices.
```

The workflow prevents these mistakes:

```text
- putting controller/handler/repository/table details into use-case diagrams;
- designing database schema before understanding domain behavior;
- designing aggregates from isolated screens instead of full domain requirements;
- treating every precondition/include/extend as domain logic;
- confusing responsibility decomposition with implementation slices;
- forgetting frontend planning when moving to implementation;
- designing backend layer-by-layer instead of scenario/feature slices;
- thinking every slice must pass through all layers;
- using machine-only source IDs that are unreadable for humans;
- mixing scope, change, risk and decision markers into one vague column;
- postponing changeability thinking until it is too late;
- hiding important architecture decisions instead of documenting ADRs.
```

---

## 1. High-Level Workflow

The full planning path:

```text
1. Scenario catalog
2. Pure user-facing scenario specs
3. Scenario-level acceptance and observable outcomes
4. Scenario responsibility decomposition
5. Consolidated layer maps
6. Domain discovery
7. Domain model draft
8. Aggregate boundary design
9. Scenario-to-slice implementation planning
10. Slice cards
11. Consolidated implementation maps
12. Changeability / extensibility map
13. ADRs for significant decisions
14. Walking skeleton and slice delivery
```

This process is iterative. Later stages may reveal that an earlier scenario, invariant, boundary, marker, slice or decision needs to change.

The intended dependency direction:

```text
User-facing behavior
-> scenario acceptance / observable outcomes
-> responsibility classification
-> consolidated layer maps
-> domain discovery
-> aggregate boundaries
-> implementation slices
-> tests / changeability / ADRs
```

Avoid reverse pressure:

```text
Database convenience
-> fake domain model
-> polluted user scenario
```

---

## 2. Global Reference Rule

All human-facing tables should be readable by humans and still traceable for AI/tools.

Use:

```text
Readable first, strict ref second.
```

Do not use a single machine-only `Source` column in human-facing tables.

Bad:

| Source | Domain requirement |
|---|---|
| SC-04-POST-01 | New request starts as Submitted |

Good:

| Scenario | Scenario item | Item ref | Domain requirement |
|---|---|---|---|
| Client Request Creation | Request status becomes Submitted | SC-04-POST-01 | New request starts as Submitted |

Preferred pairs:

| Context | Human-readable column | Strict ref column |
|---|---|---|
| Scenario | Scenario | Scenario ref |
| Scenario item | Scenario item | Item ref |
| Slice | Slice name | Slice ref |
| ADR | ADR title | ADR ref |
| Change candidate | Change candidate | Change ref, optional |
| Aggregate | Aggregate | Aggregate ref |

Common examples:

| Thing | Human-readable | Strict ref |
|---|---|---|
| Scenario | Client Request Creation | SC-04 |
| Scenario item | Request status becomes Submitted | SC-04-POST-01 |
| Slice | Submit connection request | SL-04.3 |
| Aggregate | ClientRequest | AGG-REQUEST |
| ADR | Request aggregate boundary | ADR-005 |
| Change candidate | Applicant types expand | CHG-APP-01 |

Rule:

```text
Never force humans to read only SC-04-POST-01.
```

---

## 3. Marker Model

Scenario specs and planning tables can contain lightweight markers.

Markers are planning metadata. They are not implementation design.

The marker model has three separate axes:

```text
1. Scope / roadmap
2. Change / volatility
3. Risk / decision
```

Do not collapse these into one vague `Change marker` column when precision matters.

### 3.1 Scope / Roadmap Markers

Scope markers answer:

```text
Is this current target behavior,
an alternative path,
additional behavior,
or intentionally deferred behavior?
```

Markers:

```text
[CORE]    required target behavior
[ALT]     alternative path inside the current target scenario
[EXT]     additional scenario/slice/capability, not required for core
[DEFER]   known but intentionally postponed
```

Examples:

```text
Client submits valid request -> sees Submitted status. [CORE]
Applicant data missing -> fill applicant data inline. [ALT]
Upload documents. [EXT:L2]
Reliable outbox delivery. [DEFER:L3]
```

Important:

```text
[EXT] means additional behavior can be added.
It does not mean that current behavior itself changes.
```

### 3.2 Change / Volatility Markers

Change markers answer:

```text
What existing behavior, model, rule, workflow, provider or strategy may change?
```

Markers:

```text
[VAR:EXPAND]    current model may gain more variants
[VAR:MODIFY]    current rule/workflow/acceptance may change
[VAR:REPLACE]   implementation/provider/strategy may be replaced
[VAR:REMOVE]    behavior may be removed/deprecated; use rarely
```

Examples:

```text
Applicant type may expand: individual -> entrepreneur -> legal entity. [VAR:EXPAND]
Request workflow may add clarification before final decision. [VAR:MODIFY]
Email provider may change from SMTP to external service. [VAR:REPLACE]
Temporary mock verification may disappear after real integration. [VAR:REPLACE] or [VAR:REMOVE]
```

Important:

```text
[VAR] means existing behavior/model/rule/workflow/provider/strategy may change.
It is not the same as adding a separate extension.
```

### 3.3 Risk / Decision Markers

Risk and decision markers answer:

```text
Is this uncertain, risky, or likely to require an architecture decision?
```

Markers:

```text
[RISK]    uncertainty or risk
[ADR?]    likely architecture decision needed later
```

Examples:

```text
Should notification failure block approval? [RISK][ADR?]
Applicant data inline vs profile-first ownership. [ADR?]
Clarification workflow changes request state model. [RISK][ADR?]
```

Useful distinction:

```text
[RISK] = we are not sure what is correct.
[ADR?] = we see architectural alternatives and may need to record a decision.
```

### 3.4 EXT vs VAR

This distinction is critical.

```text
[EXT] = what can be added?
[VAR] = what can change in existing behavior, model, rule, workflow, provider or strategy?
```

If it is `[EXT]`, ask:

```text
- Can this become a separate scenario?
- Can this become a separate slice?
- What is its attachment point?
- Can it be added without rewriting the core behavior?
```

If it is `[VAR]`, ask:

```text
- What existing rule/model/strategy may change?
- What seam prevents this change from spreading everywhere?
- Which tests protect current behavior?
- Is an ADR needed?
```

Some items can have both markers.

```text
Clarification flow. [EXT:L2][VAR:MODIFY][ADR?]
```

Reason:

```text
[EXT] because clarification is an additional scenario/slice.
[VAR:MODIFY] because it changes request workflow/status rules.
[ADR?] because status/workflow strategy may require a decision.
```

```text
Documents upload. [EXT:L2]
```

If documents later become required before submit:

```text
Documents upload. [EXT:L2][VAR:MODIFY]
```

```text
External verification. [EXT:L2/L3][VAR:REPLACE]
```

### 3.5 Where Markers Are Used

Marker depth depends on artifact type.

| Artifact | Scope | Change | Risk/Decision | Handling |
|---|---:|---:|---:|---:|
| Scenario Spec / Diagram | light inline | light inline | sometimes | almost none |
| Scenario Responsibility Table | yes | yes | yes | planning hint |
| Consolidated Layer Maps | yes | yes | yes | layer-specific hint |
| Aggregate Design Table | yes | yes | yes | domain-specific hint |
| Scenario-To-Slice Map | yes | yes | yes | slice-level seam / attachment hint |
| Slice Card | yes | yes | yes | concrete design response |
| Changeability Map | optional context | primary | yes | primary |
| ADR | optional context | if relevant | primary | accepted decision |

Handling grows gradually:

```text
scenario marker
-> planning hint
-> layer-specific hint
-> slice seam
-> changeability strategy
-> ADR decision
```

---

## 4. Scenario Catalog

### 4.1 Purpose

Scenario Catalog is the index of scenarios. It keeps stable scenario IDs and gives a high-level view of the scenario package.

Create it before detailed scenario specs.

### 4.2 Table

| Scenario | Scenario ref | Actor / Screen | Goal | Priority | Level | Status | Notes |
|---|---|---|---|---|---|---|---|

Column purpose:

| Column | Purpose |
|---|---|
| Scenario | readable scenario title |
| Scenario ref | stable scenario ID |
| Actor / Screen | who acts and where |
| Goal | short user-facing goal |
| Priority | planning order |
| Level | L1/L2/L3 if useful |
| Status | idea / draft / reviewed / ready |
| Notes | short context |

### 4.3 Recommended Scenario Package

| Scenario | Scenario ref | Actor / Screen | Goal | Priority | Level | Status | Notes |
|---|---|---|---|---|---|---|---|
| Scenario Overview / Navigation Map | SC-00 | System / planning overview | Navigate scenario package | High | all | Draft | links scenario pages |
| Guest Registration | SC-01 | Guest — Registration screen | Register account | High | L1 | Draft | account creation |
| Login | SC-02 | Guest — Login screen | Sign in | High | L1 | Draft | session issued |
| Password Recovery | SC-03 | Guest — Password recovery screen | Restore access | Medium | L1/L2 | Idea | subscenario of login |
| Client Request Creation | SC-04 | Client — Request creation screen | Submit connection request | High | L1 | Draft | applicant data may be inline |
| Client Request Status / Result | SC-05 | Client — Status page | View request status/result | High | L1 | Draft | read model |
| Employee Request Dashboard | SC-06 | Employee — Dashboard | Find submitted requests | High | L1 | Draft | queue/list |
| Employee Request Review | SC-07 | Employee — Review screen | Approve or reject request | High | L1 | Draft | review workflow |
| Approval Result: Contract Draft + Email Notification | SC-08 | System / Employee result context | Prepare approval result | High | L1/L2 | Draft | contract + notification |
| Rejection Result | SC-09 | System / Client status context | Communicate rejection | High | L1 | Draft | status + notification |
| Extended Applicant Data | SC-10 | Client — Applicant data screen | Provide richer applicant data | Medium | L2 | Idea | ФЛ/ИП/ЮЛ |
| Request Documents | SC-11 | Client — Request documents screen | Attach documents | Medium | L2 | Idea | may become required |
| Clarification | SC-12 | Client + Employee — Clarification flow | Request/answer clarification | Medium | L2 | Idea | workflow modification |
| Contract Acknowledgement | SC-13 | Client — Contract result screen | Acknowledge contract | Medium | L2 | Idea | after approval |
| Mock Verification | SC-14 | System — Verification process | Verify applicant/request data | Medium | L2/L3 | Idea | mock -> external |
| Security / Account Protection | SC-15 | System — Security flow | Protect account | Low | L3 | Idea | lockout/rate limit |
| Reliable Notification Delivery | SC-16 | System — Notification processing | Deliver notifications reliably | Low | L3 | Idea | outbox/retry |
| Anonymous Request | SC-17 | Guest — Request creation screen | Submit request without account | Low | L3 | Idea | future branch |
| Archive / Audit | SC-18 | System / Employee context | Archive and audit requests | Low | L3 | Idea | audit/security |

---

## 5. Scenario Spec

### 5.1 Purpose

Scenario Spec is the clean behavioral artifact.

It answers:

```text
What does the user/system do?
What should happen?
When is the behavior complete?
What is observable from outside?
```

Scenario specs must be user-facing and must not become implementation plans.

### 5.2 Scenario Unit

The main scenario unit is:

```text
Actor + screen/application context + user goal
```

Examples:

```text
Guest — Registration screen — register account
Guest — Login screen — sign in
Client — Request creation screen — submit connection request
Client — Request status page — see request status/result
Employee — Request dashboard — find submitted requests
Employee — Request review screen — approve or reject request
System — Notification processing — notify client about decision
```

A scenario may include multiple screens if they belong to one coherent user journey.

If a branch has its own user goal or becomes complex, split it into a subscenario.

### 5.3 Scenario Format

```text
Scenario ID:
Title:
Actor / Screen:
Goal:
Priority:
Level marker:
Preconditions:
Main flow:
Include:
Extend:
Invariants:
Postconditions:
Scenario acceptance criteria:
Observable outcomes:
Extension/change markers:
Open questions:
```

### 5.4 What Belongs Here

Allowed:

```text
actor
screen/application context
user goal
preconditions
main flow
include
extend
invariants
postconditions
scenario acceptance criteria
observable outcomes
scope/change/risk markers
open questions
```

Avoid:

```text
controller
endpoint
handler
repository
DbContext
ORM mapping
SQL table
aggregate method name
command/query class name
frontend component structure
```

### 5.5 Scenario Acceptance Criteria

Scenario-level acceptance criteria are part of the scenario.

They answer:

```text
When does the user/domain expert consider this behavior complete?
```

Good:

```text
Given client is signed in
When client submits a valid connection request
Then client sees Submitted status
```

Bad:

```text
CreateConnectionRequestCommand saves ClientRequest to L1ClientRequests.
```

### 5.6 Observable Outcomes

Observable outcomes are externally visible or verifiable results.

Allowed:

```text
account is saved
session is issued
applicant data is saved
request is stored
request status becomes Submitted
employee can see request in submitted queue
client sees approval/rejection result
email notification is sent
invalid input does not create saved data
```

Forbidden at scenario level:

```text
handler calls repository
DbContext.SaveChangesAsync is called
INSERT into L1ClientRequests
EF maps owned value object columns
Dapper query joins tables
React component calls useMutation
```

### 5.7 Applicant Data Naming

Do not use this as the primary user-facing label:

```text
Create individual applicant profile
```

Use:

```text
Provide individual applicant data
```

Reason:

```text
The user provides applicant data.
The system may internally store it as ApplicantParty,
but that is not the user-facing scenario language.
```

Represent both UX entry points:

```text
Profile-first:
Client opens profile/applicant data page
-> provides individual applicant data
-> later starts request
-> system reuses saved applicant data
```

```text
Request-first / inline:
Client starts request
-> system sees applicant data is missing
-> request form asks for applicant data inline
-> system saves applicant data
-> request submission continues
```

Do not duplicate the domain concept in user-facing diagrams/specs.

### 5.8 Example Scenario Spec

```text
Scenario ID:
SC-04

Title:
Client Request Creation

Actor / Screen:
Client — Request creation screen

Goal:
Submit a connection request.

Priority:
High

Level marker:
L1 main path, L2/L3 extension markers when relevant

Preconditions:
- Client is signed in. [CORE]

Main flow:
1. Client opens request creation page. [CORE]
2. Client fills request details. [CORE]
3. Client provides applicant data if missing. [ALT][VAR:EXPAND]
4. Client submits request. [CORE]
5. Client sees Submitted status. [CORE]

Include:
- Validate request form. [CORE]

Extend:
- Applicant data missing -> fill applicant data inline. [ALT][VAR:EXPAND]
- Upload documents. [EXT:L2]
- Anonymous request. [EXT:L3]

Invariants:
- Invalid request is not accepted. [CORE]
- Client cannot submit request for another client's applicant data. [CORE]

Postconditions:
- Request exists. [CORE]
- Request status is Submitted. [CORE]

Scenario acceptance criteria:
- Given client is signed in,
  when valid request is submitted,
  then client sees Submitted status.
- Given request form is invalid,
  when client submits the form,
  then request is not accepted and validation errors are shown.
- Given applicant data is missing,
  when client starts request creation,
  then client can provide applicant data inline.

Observable outcomes:
- Request is stored.
- Request status becomes Submitted.
- Employee can see request in submitted queue.
- Invalid input does not create saved request.

Extension/change markers:
- Applicant type may expand from individual to entrepreneur/legal entity. [VAR:EXPAND]
- Documents may become required later. [EXT:L2][VAR:MODIFY]
- Anonymous request may bypass account path later. [EXT:L3][VAR:MODIFY]

Open questions:
- Should request submission require applicant data to exist before page load, or can inline data be saved during submit?
- Should document absence remain valid in L1?
```

---

## 6. Include, Extend, Preconditions, Invariants, Postconditions

These are scenario/specification concepts. They are not automatically domain concepts.

### 6.1 Preconditions

Preconditions are conditions that must be true before a scenario starts.

Examples:

```text
Client is signed in.
```

Usually auth/application/security.

```text
Request is still in Submitted status.
```

Likely domain.

```text
Applicant data is available or can be provided inline.
```

Scenario/application flow with possible domain consequences.

Rule:

```text
Do not assume every precondition is a domain invariant.
Classify it during responsibility decomposition.
```

### 6.2 Include

`include` means mandatory reusable step or subscenario.

Examples:

```text
<<include>> Validate registration form
<<include>> Save account
<<include>> Validate request form
<<include>> Save submitted request
```

An included step may be:

```text
UI validation
application orchestration
domain invariant enforcement
persistence outcome
external observable behavior
```

Classify it later.

### 6.3 Extend

`extend` means optional branch, alternative path, error path or extension point.

| Extend Type | Example | Likely Planning Target |
|---|---|---|
| User choice | Forgot password | Separate scenario |
| Missing data branch | Applicant data missing | Scenario/application flow |
| Business branch | Approve vs reject | Domain workflow |
| Validation failure | Empty details | UI/application/domain validation |
| Domain rule failure | Already final request cannot be reviewed | Domain invariant |
| Future feature | Upload documents, L2 | Roadmap / scenario extension |
| Technical failure | Email provider unavailable | Application/infrastructure behavior |

Rule:

```text
Do not treat every extend as domain logic.
Do not treat every extend as implementation detail.
Classify the branch by meaning.
```

### 6.4 Invariants

Invariants are rules that must remain true, but they can live at different levels.

Examples:

```text
Invalid request is not accepted.
```

May involve UI/application/domain validation.

```text
A client cannot submit a request for another client's applicant data.
```

Likely application authorization plus domain ownership/reference rule.

```text
An approved request cannot be approved again.
```

Likely domain invariant.

```text
The application should not show unhandled exception pages.
```

System invariant, not domain invariant.

Rule:

```text
Domain invariants are especially important because they drive aggregate boundaries.
But not every scenario invariant is a domain invariant.
```

### 6.5 Postconditions / Observable Outcomes

Postconditions and observable outcomes state what must be true after the scenario.

Examples:

```text
request is stored
status becomes Submitted
employee can see request in queue
email notification is sent
invalid input does not create saved data
```

These outcomes are key inputs for responsibility decomposition and later testing.

---

## 7. Scenario Responsibility Decomposition

### 7.1 Purpose

Scenario Responsibility Table classifies each meaningful scenario element by responsibility layer.

It is the bridge between clean scenarios and domain discovery.

It is analysis, not implementation planning.

It answers:

```text
Which layer is responsible for this scenario item?
```

It does not yet define:

```text
endpoint
frontend component
command/query class
handler
repository
event mechanism
DB mapping
test suite
```

### 7.2 Input

Use all meaningful scenario elements:

```text
preconditions
main flow steps
include
extend
invariants
postconditions
scenario acceptance criteria
observable outcomes
extension/change markers
open questions if they imply responsibility
```

### 7.3 Responsibility Layers

Use these categories:

```text
UI / Screen behavior
Application boundary / orchestration
Auth / Security / Framework
Domain
Read model / Query
Persistence
Infrastructure / External integration
Cross-cutting / Observability
Open question
```

A simpler grouping is acceptable early:

```text
Domain
Application
Auth/Security/Framework
UI
Read model
Infrastructure
Open question
```

### 7.4 Process Questions

For each scenario item ask:

```text
1. Is this UI behavior?
2. Is this auth/security/framework behavior?
3. Is this application orchestration?
4. Is this domain rule/state/invariant?
5. Is this read model/query behavior?
6. Is this persistence outcome?
7. Is this integration/infrastructure behavior?
8. Is this cross-cutting concern?
9. Is there a domain signal?
10. Is there an extension/change/risk marker?
```

### 7.5 Responsibility Table

Recommended columns:

| Item label | Item ref | Scenario item | Type | Meaning | Responsibility layer | Domain signal | Scope | Change | Risk/Decision | Planning hint |
|---|---|---|---|---|---|---|---|---|---|---|

Column purpose:

| Column | Purpose |
|---|---|
| Item label | readable short name for humans |
| Item ref | strict item ID for AI/traceability |
| Scenario item | original or near-original scenario text |
| Type | precondition / step / include / extend / invariant / acceptance / outcome |
| Meaning | business/human meaning |
| Responsibility layer | owner layer |
| Domain signal | whether this informs domain discovery |
| Scope | CORE / ALT / EXT / DEFER |
| Change | VAR:EXPAND / VAR:MODIFY / VAR:REPLACE / VAR:REMOVE |
| Risk/Decision | RISK / ADR? |
| Planning hint | early hint, not final design |

### 7.6 Example

| Item label | Item ref | Scenario item | Type | Meaning | Responsibility layer | Domain signal | Scope | Change | Risk/Decision | Planning hint |
|---|---|---|---|---|---|---|---|---|---|---|
| Signed-in client | SC-04-PRE-01 | Client is signed in | Precondition | user authenticated | Auth / Security | no | CORE | — | — | framework/app auth |
| Request details input | SC-04-STEP-01 | Client fills request details | Main flow | user input | UI / App boundary | weak | CORE | — | — | input collection |
| Valid request accepted | SC-04-AC-01 | Valid request shows Submitted status | Acceptance | behavior complete | Domain + Read model | yes | CORE | — | — | status + visible result |
| Inline applicant data | SC-04-ALT-01 | Applicant data missing -> fill inline | Extend/Alt | collect applicant data | App + Domain | yes | ALT | VAR:EXPAND | ADR? | same domain result as profile-first |
| Invalid request rejected | SC-04-INV-01 | Invalid request is not accepted | Invariant | no invalid request saved | Domain + App boundary | yes | CORE | — | — | final guard in domain/value objects |
| Submitted status | SC-04-POST-01 | Request status becomes Submitted | Postcondition | initial lifecycle state | Domain | yes | CORE | — | — | request lifecycle state |
| Employee queue visibility | SC-04-POST-02 | Employee can see request in queue | Observable outcome | request visible in queue | Read model / Query | weak | CORE | — | — | projection/read model |
| Request documents | SC-04-EXT-01 | Upload documents | Extend | optional future branch | App + Domain + Infra later | yes | EXT:L2 | maybe VAR:MODIFY | — | separate scenario/slice |

### 7.7 Rules

```text
- Do one responsibility table per scenario.
- Use readable labels and strict refs together.
- Classify each item by responsibility.
- Keep Scope, Change and Risk/Decision separated.
- Mark uncertainty explicitly.
- Do not force everything into Domain.
- Do not ignore domain signals just because UI/app validates first.
- Keep planning hints lightweight at this stage.
- Do not decide endpoints, components, handlers, repository classes, events or DB mappings here.
```

Good planning hints:

```text
separate scenario later
domain abstraction candidate
read model candidate
external dependency candidate
ownership rule candidate
```

Too early:

```text
use MediatR notification handler + outbox + SQL table OutboxMessages
```

---

## 8. Consolidated Layer Maps

### 8.1 Purpose

After responsibility tables exist for multiple scenarios, merge rows by layer.

The goal is to see each layer across all scenarios.

This prevents designing the domain from one isolated scenario.

Several scenarios may touch one concept or aggregate.

Use readable context plus strict refs:

```text
Scenario | Scenario ref | Scenario item | Item ref
```

### 8.2 Consolidated Domain Responsibility Map

Purpose:

```text
Provide the main input for domain discovery and aggregate design.
```

Template:

| Scenario | Scenario ref | Scenario item | Item ref | Domain requirement | Candidate concept | State / invariant / event | Scope | Change | Risk/Decision | Domain handling hint |
|---|---|---|---|---|---|---|---|---|---|---|

Example:

| Scenario | Scenario ref | Scenario item | Item ref | Domain requirement | Candidate concept | State / invariant / event | Scope | Change | Risk/Decision | Domain handling hint |
|---|---|---|---|---|---|---|---|---|---|---|
| Client Request Creation | SC-04 | Request status becomes Submitted | SC-04-POST-01 | New request starts as Submitted | Request | initial status | CORE | — | — | lifecycle invariant |
| Client Request Creation | SC-04 | Invalid request is not accepted | SC-04-INV-01 | Invalid request must not be accepted | Request / value objects | invariant | CORE | — | — | final domain guard |
| Client Request Creation | SC-04 | Applicant data missing -> fill inline | SC-04-ALT-01 | Applicant data can be provided inline or profile-first | ApplicantParty | same domain result | ALT | VAR:EXPAND | ADR? | avoid duplicating concepts |
| Employee Request Review | SC-07 | Employee approves request | SC-07-STEP-03 | Request receives approval decision | Request review | transition | CORE | — | ADR? | review ownership decision |
| Clarification | SC-12 | Clarification may be requested | SC-12-EXT-01 | Clarification may appear in request workflow | Request workflow | new status/transition | EXT:L2 | VAR:MODIFY | ADR? | explicit state transitions |

### 8.3 Consolidated Application Responsibility Map

Purpose:

```text
Show orchestration responsibilities across scenarios.
```

Template:

| Scenario | Scenario ref | Scenario item | Item ref | Application responsibility | Needs domain? | Needs auth/current user? | Transaction? | Scope | Change | Risk/Decision | App handling hint |
|---|---|---|---|---|---|---|---|---|---|---|---|

Example:

| Scenario | Scenario ref | Scenario item | Item ref | Application responsibility | Needs domain? | Needs auth/current user? | Transaction? | Scope | Change | Risk/Decision | App handling hint |
|---|---|---|---|---|---|---|---|---|---|---|---|
| Client Request Creation | SC-04 | Applicant data missing -> fill inline | SC-04-ALT-01 | coordinate applicant data before request submit | yes | yes | yes | ALT | VAR:EXPAND | ADR? | profile-first and inline paths should converge |
| Client Request Creation | SC-04 | Request status becomes Submitted | SC-04-POST-01 | create submitted request | yes | yes | yes | CORE | — | — | command slice candidate |
| Approval Result / Notification | SC-08 | Email notification is sent | SC-08-OUT-01 | trigger client notification after review | maybe | no/weak | maybe async | CORE | VAR:REPLACE | ADR? | notification seam needed |

### 8.4 Consolidated Auth/Security Map

Purpose:

```text
Keep auth/security/framework concerns separate from domain rules.
```

Template:

| Scenario | Scenario ref | Scenario item | Item ref | Security responsibility | Actor | Rule | Domain dependency | Scope | Change | Risk/Decision | Notes |
|---|---|---|---|---|---|---|---|---|---|---|---|

Example:

| Scenario | Scenario ref | Scenario item | Item ref | Security responsibility | Actor | Rule | Domain dependency | Scope | Change | Risk/Decision | Notes |
|---|---|---|---|---|---|---|---|---|---|---|---|
| Client Request Creation | SC-04 | Client is signed in | SC-04-PRE-01 | require signed-in client | Client | authenticated | no | CORE | — | — | framework/app auth |
| Client Request Creation | SC-04 | Client cannot use another client's applicant data | SC-04-INV-02 | enforce ownership/access | Client | ownership | yes | CORE | — | — | app + domain reference rule |
| Employee Request Review | SC-07 | Employee opens review screen | SC-07-PRE-01 | require employee role | Employee | authorized role | no/weak | CORE | — | — | framework/app auth |

### 8.5 Consolidated Read Model Map

Purpose:

```text
Plan user-visible read needs without forcing read-only scenarios through aggregates.
```

Template:

| Scenario | Scenario ref | Scenario item | Item ref | User-visible data | Query/read model | Source domain state | Filters/sorting | Scope | Change | Risk/Decision |
|---|---|---|---|---|---|---|---|---|---|---|

Example:

| Scenario | Scenario ref | Scenario item | Item ref | User-visible data | Query/read model | Source domain state | Filters/sorting | Scope | Change | Risk/Decision |
|---|---|---|---|---|---|---|---|---|---|---|
| Client Request Status | SC-05 | Client sees own requests | SC-05-STEP-01 | own request list | MyRequestsDto | Request status | by client/account | CORE | filters later | — |
| Employee Dashboard | SC-06 | Employee sees submitted queue | SC-06-STEP-01 | submitted requests | EmployeeRequestQueueDto | Submitted requests | status/date | CORE | assignment later | — |
| Employee Review | SC-07 | Employee opens request details | SC-07-STEP-02 | request + applicant details | RequestDetailsDto | Request + applicant data | by request id | CORE | documents later | — |

### 8.6 Consolidated Infrastructure/Integration Map

Purpose:

```text
Identify external, replaceable or technical capabilities.
```

Template:

| Scenario | Scenario ref | Scenario item | Item ref | External/system behavior | Port candidate | Adapter candidate | Reliability need | Scope | Change | Risk/Decision | Notes |
|---|---|---|---|---|---|---|---|---|---|---|---|

Example:

| Scenario | Scenario ref | Scenario item | Item ref | External/system behavior | Port candidate | Adapter candidate | Reliability need | Scope | Change | Risk/Decision | Notes |
|---|---|---|---|---|---|---|---|---|---|---|---|
| Approval Result / Notification | SC-08 | Email notification is sent | SC-08-OUT-01 | notify client | IEmailSender / INotificationPort | SMTP/mock | low L1, higher L3 | CORE | VAR:REPLACE | ADR? | outbox later |
| Request Documents | SC-11 | Client uploads document | SC-11-STEP-02 | store file | IFileStorage | local/cloud | medium | EXT:L2 | VAR:REPLACE | ADR? | storage decision later |
| Mock Verification | SC-14 | Verification is performed | SC-14-STEP-01 | verify externally later | IVerificationGateway | mock/external | medium | EXT:L2/L3 | VAR:REPLACE | ADR? | external boundary |

---

## 9. Domain Discovery And Domain Model Draft

### 9.1 Purpose

Domain discovery begins after scenario behavior is understood and responsibility rows are classified.

It identifies:

```text
business terms
domain concepts
state transitions
lifecycle boundaries
business invariants
domain events
likely entities
likely value objects
candidate aggregate boundaries
```

DDD concepts become primary here.

### 9.2 Classification Question

For every domain-related row, ask:

```text
Is this real business/domain logic,
or is this UI, application orchestration, auth/security, persistence, read model, integration, or infrastructure?
```

### 9.3 Domain Discovery Table

Template:

| Scenario | Scenario ref | Scenario item | Item ref | Business meaning | Candidate concept | Rule / state / event | Layer confidence | Notes |
|---|---|---|---|---|---|---|---|---|

Example:

| Scenario | Scenario ref | Scenario item | Item ref | Business meaning | Candidate concept | Rule / state / event | Layer confidence | Notes |
|---|---|---|---|---|---|---|---|---|
| Client Request Creation | SC-04 | Client submits request | SC-04-STEP-04 | client asks for connection | Request | initial status Submitted | Domain | candidate aggregate |
| Client Request Creation | SC-04 | Applicant data missing | SC-04-ALT-01 | need applicant identity/data | Applicant data | required before request | Scenario/App/Domain | two UX entry points |
| Employee Request Review | SC-07 | Employee approves request | SC-07-STEP-03 | request receives decision | Review / Decision | Approved final state | Domain | transition rule |
| Approval Result / Notification | SC-08 | Email notification is sent | SC-08-OUT-01 | client is informed | Notification | after decision | App/Infra | port/outbox candidate |
| Client Request Status | SC-05 | Client sees status | SC-05-STEP-01 | display current state | Request status read model | visible to client | Query/read side | projection candidate |

### 9.4 Domain Signals

Useful signals:

```text
a thing has a lifecycle
a thing changes status
a rule must always be true
a decision changes what is allowed next
the business uses a specific noun repeatedly
multiple scenario branches revolve around the same concept
the system must remember something as a business fact
two concepts should not be changed independently
a historical event matters to the business
```

### 9.5 Discovery Events

Possible discovery events:

```text
AccountRegistered
ApplicantDataProvided
ConnectionRequestSubmitted
RequestTakenForReview
RequestApproved
RequestRejected
ContractDraftCreated
ClientNotificationRequested
RequestArchived
```

Events are discovery tools first.

They do not automatically require event-driven implementation.

### 9.6 Domain Model Draft

Build the initial domain model from discovery.

Model possible:

```text
entities
value objects
domain events
domain services/policies
lifecycle states
allowed transitions
forbidden transitions
important identities
```

Do not start with database tables, controllers, DTOs, or frontend components.

Likely entity:

```text
has identity
has lifecycle
changes over time
must be referenced later
history matters
```

Likely value object:

```text
defined by values
no independent lifecycle
can be replaced as a whole
validates a small concept
equality by value makes sense
```

Use domain service or policy when:

```text
the rule is real business logic
it does not naturally belong to a single entity/aggregate
it coordinates domain concepts but should not know infrastructure
it is not merely application orchestration
```

Application orchestration is not a domain service.

---

## 10. Aggregate Boundary Design

### 10.1 Working Definition

```text
Aggregate = data + lifecycle + transaction boundary + invariants.
```

More precise:

```text
Aggregate = a cluster of one or more domain objects
that must be kept consistent together
and changed through one aggregate root.
```

### 10.2 Questions To Find Aggregates

```text
1. What object changes state?
2. Which invariants must be true immediately after a command?
3. Which data must be changed atomically?
4. Which objects share one lifecycle?
5. Which objects cannot exist independently?
6. Which relationships can be eventually consistent?
7. Which references should be stored by ID instead of object ownership?
8. Which scenario commands target this concept?
9. What is the smallest boundary that can protect the required invariants?
```

### 10.3 Aggregate Candidate Table

Template:

| Aggregate | Aggregate ref | Source scenarios | Scenario refs | Owns | Stored refs | Protected invariants | Commands/scenarios | Read-only scenarios | Scope | Change | Risk/Decision | Notes |
|---|---|---|---|---|---|---|---|---|---|---|---|---|

Example:

| Aggregate | Aggregate ref | Source scenarios | Scenario refs | Owns | Stored refs | Protected invariants | Commands/scenarios | Read-only scenarios | Scope | Change | Risk/Decision | Notes |
|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Account | AGG-ACCOUNT | Guest Registration, Login | SC-01, SC-02 | password hash, role, active state | none | active login state; uniqueness may be external | register/login | maybe security dashboard later | CORE | security may expand | — | uniqueness may be repository/application concern |
| ApplicantParty | AGG-APPLICANT | Request Creation, Extended Applicant Data | SC-04, SC-10 | applicant contact/data | ClientAccountId | applicant data completeness | provide applicant data, submit request | request details | CORE/EXT | VAR:EXPAND | ADR? | does not own requests |
| ClientRequest | AGG-REQUEST | Request Creation, Status, Review, Clarification, Archive | SC-04, SC-05, SC-07, SC-12, SC-18 | status, details, object address, review child later | ApplicantPartyId, reviewer id on review | valid transitions, final state protection | submit/review/clarify/archive | my requests, employee queue | CORE/EXT | VAR:MODIFY | ADR? | owns request lifecycle |
| ContractDraft | AGG-CONTRACT | Approval Result, Contract Acknowledgement | SC-08, SC-13 | draft text/status/version later | RequestId, employee id | draft lifecycle | create draft, acknowledge | request details | EXT:L2 | VAR:EXPAND | ADR? | may become richer in L2 |
| Notification | AGG-NOTIFICATION | Approval Result, Reliable Notification Delivery | SC-08, SC-16 | notification status/delivery state | recipient id, request id | delivery lifecycle if reliable delivery | notify client | notification status | CORE/DEFER | VAR:REPLACE / VAR:MODIFY | ADR? | may move to outbox in L3 |

### 10.4 Aggregate Rules

Use small aggregates by default.

Do not make an aggregate larger only because the database has a foreign key.

```text
A database FK does not imply aggregate ownership.
```

Cross-aggregate references normally use identity.

Example:

```text
ApplicantParty stores ClientAccountId.
ClientRequest stores ApplicantPartyId.
```

Do not model this as domain ownership unless a real consistency rule requires it:

```text
ClientAccount owns ApplicantParties owns Requests
```

### 10.5 Scenario vs Aggregate

Several scenarios can target one aggregate.

Aggregate is not equal to scenario.

Better rule:

```text
One command-like interaction usually has one primary aggregate transaction boundary.
```

Examples:

```text
Submit request -> primary aggregate: ClientRequest
Approve request -> primary aggregate: ClientRequest
Provide applicant data -> primary aggregate: ApplicantParty
Register account -> primary aggregate: Account
View request status -> no aggregate method; read model/query
```

Do not force read-only scenarios to hydrate aggregates.

### 10.6 Validate Aggregates Against Scenarios

For each command-like scenario action:

```text
Which aggregate root is loaded or created?
Can it enforce the needed invariant alone?
Is another aggregate required only as a reference/permission check?
Is a cross-aggregate rule application orchestration or domain policy?
Is the transaction boundary clear?
Can this be tested without the full UI?
```

If a scenario needs multiple aggregates in one transaction, check whether:

```text
the aggregate boundary is wrong
the operation is application orchestration
eventual consistency/domain event is acceptable
the workflow should be split
an ADR is needed
```

---

## 11. Scenario-To-Slice Implementation Planning

### 11.1 Key Shift

After aggregates are drafted, do not plan backend layer-by-layer.

Do not start with:

```text
all controllers
then all handlers
then all repositories
then all frontend pages
```

Plan slices:

```text
Scenario
-> user/system interaction
-> frontend behavior, if needed
-> API contract, if needed
-> application command/query/process
-> domain operation, if needed
-> persistence/read model, if needed
-> ports/adapters, if needed
-> tests
-> changeability notes
-> ADR links
```

### 11.2 Slice Definition

```text
Slice = an independently plannable, implementable and testable unit of observable/verifiable behavior.
```

A slice includes only the layers needed for that behavior.

A slice may be:

```text
full UI-to-DB vertical slice
UI-only slice
query/read slice
backend/background slice
integration slice
extension slice attached to another slice
```

A slice does not have to pass through every layer.

### 11.3 What A Slice Is Not

Slice is not necessarily:

```text
one endpoint
one controller
one handler
one table
one aggregate
one React component
one screen
one entire scenario
```

Slice is behavior, not a file/class/table boundary.

### 11.4 What A Good Slice Has

A good slice has:

```text
1. Trigger
   user click, page load, API call, event, background schedule, external callback

2. Observable/verifiable result
   status changed, request visible, email sent, error shown, row stored, audit entry recorded

3. Implementation path
   only the layers needed for that behavior

4. Test boundary
   how to test this behavior independently

5. Extension/change notes
   likely changes, future slices, replacement points

6. Contract/seam
   API contract, component props, application service interface, event, port, pipeline hook
```

Bad slice examples:

```text
Implement request management
Create repositories
Add Status column
Implement all controllers
Implement all frontend pages
```

Good slice examples:

```text
Submit connection request
View my requests
Approve request
Reject request
Send notification after approval
Show applicant data block when missing
```

### 11.5 Slice Types

```text
Command slice:
changes system state.
Examples: Submit request, Approve request, Provide applicant data, Register account.

Query slice:
shows data.
Examples: View my requests, View submitted queue, View request details.
May not use aggregate methods.

UI-only slice:
frontend-only behavior.
Examples: form stepper, local validation display, expand/collapse details.

Integration slice:
communicates with an external system.
Examples: send email, upload file, call verification service, generate document.

Backend/background slice:
runs without direct user click.
Examples: outbox delivery, archive old requests, retry failed notifications.

Extension slice:
attaches to another slice.
Examples: send notification after approval, create contract draft after approval.
```

### 11.6 Scenario-To-Slice Conversion

A scenario can contain several slices.

Example:

```text
SC-04 Client Request Creation

SL-04.1 Load request creation screen
SL-04.2 Provide applicant data inline
SL-04.3 Submit connection request
SL-04.4 Show submitted result
```

Conversion workflow:

```text
1. Take one scenario.
2. Extract user/system interactions.
3. Classify each interaction:
   command / query / UI-only / integration / background.
4. Form candidate slices.
5. Check each candidate:
   trigger, observable result, testability, aggregate/read model, transaction.
6. For command slices, identify primary aggregate.
7. For query slices, identify read model.
8. For integration/background slices, identify port/adapter/process.
9. Create Scenario-To-Slice Map row.
10. Create Slice Card.
11. Add frontend/API/application/domain/persistence/tests as needed.
12. Add changeability notes.
13. Add ADR links/candidates.
```

Example interaction table:

| Interaction | Type | Candidate slice | Notes |
|---|---|---|---|
| Open request form | Query/UI | Load request creation screen | may load applicant data |
| Fill applicant data if missing | Command/UI | Provide applicant data inline | maybe same or separate slice |
| Submit request | Command | Submit connection request | primary aggregate: ClientRequest |
| See submitted status | Query/result | Show submitted result | can be part of submit slice |
| Employee sees request | Query | View submitted request queue | separate employee slice |

---

## 12. Scenario-To-Slice Map

### 12.1 Purpose

Scenario-To-Slice Map is the main bridge from scenarios to implementation.

It shows:

```text
which behavior units will be delivered
where they came from
what they depend on
how they are triggered
what they produce
where extension/change/decision markers matter
```

Use readable labels plus strict refs.

### 12.2 Table

| Slice name | Slice ref | Parent scenario | Scenario ref | Type | Role | Trigger | Observable result | Primary aggregate/read model | Depends on | Priority | Scope | Change | Risk/Decision | Handling / seam |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|

Column purpose:

| Column | Purpose |
|---|---|
| Slice name | readable behavior unit |
| Slice ref | strict slice ID |
| Parent scenario | readable scenario name |
| Scenario ref | strict scenario ID |
| Type | command / query / UI-only / integration / background / extension |
| Role | primary / supporting / extension / background / UI-only |
| Trigger | what starts the behavior |
| Observable result | what can be verified |
| Primary aggregate/read model | main domain or read target |
| Depends on | required previous slice/capability |
| Priority | implementation planning order |
| Scope | CORE / ALT / EXT / DEFER |
| Change | VAR:* marker if relevant |
| Risk/Decision | RISK / ADR? |
| Handling / seam | early concrete hint for attachment or seam |

### 12.3 Example

| Slice name | Slice ref | Parent scenario | Scenario ref | Type | Role | Trigger | Observable result | Primary aggregate/read model | Depends on | Priority | Scope | Change | Risk/Decision | Handling / seam |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Load request creation screen | SL-04.1 | Client Request Creation | SC-04 | Query/UI | Supporting | page open | form is ready | Applicant data read model | login | High | CORE | VAR:EXPAND | — | must support future applicant variants |
| Provide applicant data inline | SL-04.2 | Client Request Creation | SC-04 | Command/UI | Supporting | missing data in form | applicant data saved | ApplicantParty | login | High | ALT | VAR:EXPAND | ADR? | converge with profile-first path |
| Submit connection request | SL-04.3 | Client Request Creation | SC-04 | Command | Primary | submit click | request Submitted | ClientRequest | applicant data | High | CORE | — | ADR? | primary aggregate ClientRequest |
| View my requests | SL-05.1 | Client Request Status / Result | SC-05 | Query/UI | Primary | status page open | request list visible | MyRequestsDto | submitted request | High | CORE | filters later | — | read model/projection |
| Approve/reject request | SL-07.2 | Employee Request Review | SC-07 | Command/UI | Primary | employee decision | request final | ClientRequest | queue/details | High | CORE | VAR:MODIFY | ADR? | review ownership/status model |
| Upload request documents | SL-11.1 | Request Documents | SC-11 | Command/Integration | Extension | upload files | documents attached | RequestDocument / storage | request exists | Medium | EXT:L2 | maybe VAR:MODIFY | ADR? | file storage port |
| Notify client | SL-08.2 | Approval Result / Contract Notification | SC-08 | Integration/Extension | Extension | request reviewed | notification sent/recorded | Notification / port | approve/reject | Medium | CORE/EXT depends | VAR:REPLACE | ADR? | notification port/event |
| Reliable outbox delivery | SL-16.1 | Reliable Notification Delivery | SC-16 | Background | Extension | scheduled worker | pending notifications delivered | Outbox/Notification | notification record | Low | DEFER:L3 | VAR:MODIFY | ADR? | background worker/outbox |

---

## 13. Extension Slices And Seams

### 13.1 Primary vs Extension Slices

Primary slices usually have explicit user triggers.

Examples:

```text
Submit request
Approve request
Reject request
Register account
Provide applicant data
```

Extension slices grow from another slice or state change.

Examples:

```text
Send email after approval
Create contract draft after approval
Upload documents during request creation
Run verification after submit
Write audit entry
Archive completed request
```

### 13.2 Attachment Point

Each extension slice should declare its attachment point.

Template:

```text
Extension slice:
Attaches to:
Trigger:
Attachment mechanism:
Required or optional:
Failure behavior:
```

Example:

```text
Extension slice:
Send notification after request review

Attaches to:
SL-07.2 Approve Request
SL-07.3 Reject Request

Trigger:
Request reviewed / decision made

Attachment mechanism:
direct application service, event handler, or side-effect pipeline

Required or optional:
optional in early version, more reliable later

Failure behavior:
base request decision must remain consistent
notification failure is recorded or retried later
```

### 13.3 Seam Options

#### Direct Application Service / Policy Call

Use when:

```text
behavior is required for scenario completion
failure should stop or affect the scenario
immediate result is needed
same transaction or same application flow is required
```

Reduce coupling through an interface:

```text
IContractDraftCreator
IClientNotificationService
IApprovalSideEffect
```

#### Event

Use when:

```text
behavior is an additional reaction
new reactions may be added later
eventual consistency is acceptable
base behavior should not depend on concrete extension implementation
```

Events are one possible seam. They are not required for every extension.

Reliable event processing may later require outbox/background delivery.

#### Pipeline / Hook / Policy List

Use when:

```text
there is a known extension point
extensions must run in controlled order
a compromise between direct calls and event-based flow is useful
```

#### Port / Adapter

Use for external or replaceable capabilities.

Examples:

```text
IEmailSender
IFileStorage
IVerificationGateway
IDocumentGenerator
```

### 13.4 Seam Selection Rule

```text
If side effect is required for scenario completion:
use direct application service/policy call.

If side effect is optional or eventually consistent:
consider event.

If many controlled extensions attach at the same point:
consider pipeline/hook/policy list.

If external technology/provider is involved:
use port/adapter.

If the choice is significant or hard to reverse:
create ADR.
```

### 13.5 Extension Rule

```text
Core behavior should not depend on optional extension implementation.
```

Example:

```text
Approve request should not depend directly on SmtpClient.
It may depend on INotificationPort, approval side-effect interface, event, or pipeline.
```

---

## 14. Ports And Adapters

### 14.1 Definition

Port:

```text
a contract used by core/application to communicate with something external, replaceable, or technically specific.
```

Adapter:

```text
a concrete implementation of that contract.
```

Example:

```csharp
public interface IEmailSender
{
    Task SendAsync(EmailMessage message, CancellationToken ct);
}
```

`IEmailSender` is a port.

```csharp
public sealed class SmtpEmailSender : IEmailSender
{
    public Task SendAsync(EmailMessage message, CancellationToken ct)
    {
        // SMTP implementation
    }
}
```

`SmtpEmailSender` is an adapter.

### 14.2 Examples

Ports:

```text
IEmailSender
IFileStorage
IClock
ICurrentUser
IVerificationGateway
INotificationPublisher
IDocumentGenerator
IRequestRepository
IUnitOfWork
IReadDbConnectionFactory
```

Adapters:

```text
SmtpEmailSender
LocalFileStorage
SystemClock
HttpContextCurrentUser
MockVerificationGateway
SqlRequestRepository
EfUnitOfWork
DapperReadDbConnectionFactory
```

Repository can also be seen as a port:

```text
IClientRequestRepository = port
EfClientRequestRepository = adapter
```

### 14.3 Inbound And Outbound Adapters

Inbound / driving adapters:

```text
HTTP controller
frontend page/action
background worker trigger
message consumer
```

Outbound / driven adapters:

```text
database repository
email provider
file storage
external verification API
clock/current user provider
```

Example slice:

```text
Approve request

Inbound adapter:
POST /employee/requests/{id}/approve

Application:
ApproveRequestCommandHandler

Domain:
ClientRequest.Approve(...)

Outbound ports:
IClientRequestRepository
INotificationPublisher
IClock

Outbound adapters:
EfClientRequestRepository
EmailNotificationPublisher
SystemClock
```

Rule:

```text
A slice may use external capabilities,
but should depend on stable contracts,
not concrete technologies.
```

---

## 15. Backend Slice Principles

Endpoint should be designed from user intent, not database CRUD.

Good:

```text
POST /requests
POST /requests/{id}/approve
POST /requests/{id}/reject
GET /requests/my
GET /employee/requests/submitted
```

Bad:

```text
POST /clientRequests/updateStatus
PUT /tables/request
generic SaveEntity endpoint
```

Command/query DTO belongs to the slice boundary.

Do not expose domain entities as external API models.

Handler orchestrates. Domain decides.

Handler:

```text
load
authorize/check access
call domain
persist
trigger side effects or events
return result
```

Domain:

```text
protect invariants
perform state transition
create valid object
```

Query slices should not hydrate aggregates only for display.

Use read models/projections when appropriate.

Make extension points explicit when needed:

```text
IApprovalSideEffect
INotificationPort
IDocumentStorage
IVerificationService
```

Do not abstract everything in advance.

---

## 16. Frontend Slice Principles

Frontend is part of slice planning.

A frontend slice should have:

```text
UI trigger
visible behavior
loading/error/success states
validation/display logic
API call/hook, if needed
minimal public interface
tests for visible behavior
```

Suggested organization:

```text
Page = assembles scenario
Feature component = implements specific action/slice
Entity component/model = displays or edits stable domain-related concept
Shared = stable UI/API utilities
```

Example:

```text
RequestCreatePage
  uses ProvideApplicantDataBlock
  uses RequestDetailsForm
  uses SubmitRequestAction
```

Rules:

```text
1. Do not make one huge page component.
2. Do not move everything into shared early.
3. Shared appears after behavior is repeated and stable.
4. Feature should not know internals of another feature.
5. Prefer props/callbacks/API hooks/route state over chaotic global store.
6. UI-only slice is valid when it has independently testable behavior.
```

Important:

```text
Slice should have a small public surface,
not necessarily the smallest possible number of files.
```

Goal:

```text
clear boundary
localized change
testable behavior
minimal unnecessary external dependencies
```

---

## 17. Slice Card

### 17.1 Purpose

A Slice Card is the detailed planning artifact for one slice.

It answers:

```text
How do we implement and test this one observable/verifiable behavior unit?
```

A Slice Card is usually easier to read as a markdown section than as a huge table.

### 17.2 Minimal Slice Card

```text
Slice name:
Slice ref:
Parent scenario:
Scenario ref:
Type:
Role:
Priority:
Trigger:
Observable result:
Depends on:
Scenario acceptance links:
Slice acceptance criteria:
Implementation path:
Tests:
Changeability:
ADR:
```

### 17.3 Full Slice Card

```text
Slice name:
Slice ref:
Parent scenario:
Scenario ref:
Slice type:
Role:
Priority:
Depends on:
User goal:
Trigger:
Screen/context:
User action:
Observable/verifiable result:
Scenario acceptance links:
Slice acceptance criteria:

Frontend:
- route/page
- form/widget/component
- client-side validation
- state/loading/error behavior
- API call/hook
- UI tests

API contract:
- method/path
- request DTO
- response DTO
- error/problem details
- auth requirement

Application:
- command/query/process
- handler responsibility
- transaction boundary
- current user/role needs
- orchestration steps

Domain:
- primary aggregate, if any
- domain method/factory, if any
- invariants enforced
- domain events, if any

Persistence/read model:
- tables/columns touched
- repository
- read projection/query
- migration impact

Ports/adapters:
- email/file/external service/current user/clock/etc.

Tests:
- domain unit
- HTTP integration
- frontend component
- E2E/walking skeleton if needed
- adapter/contract if needed

Changeability:
- likely changes
- extension points
- seam chosen
- avoid overengineering
- tests protecting behavior
- ADR candidate/link

Migration/legacy, if relevant:
- old flow remains active?
- new route/table/context?
- compatibility needed?
- cleanup later?
```

---

## 18. Acceptance, Testing And TDD

### 18.1 Acceptance Levels

Scenario-level acceptance:

```text
When does the business/user consider this behavior complete?
```

Slice-level acceptance:

```text
How will this behavior be verified through the selected implementation boundaries?
```

Technical acceptance / Definition of Done:

```text
domain invariant covered by unit test
HTTP integration test added
frontend component test added
migration added if needed
ADR linked if decision is significant
no direct dependency on concrete provider
existing scenarios still pass
```

### 18.2 Test Types

```text
domain unit tests:
pure rules, value objects, aggregate invariants

HTTP/API integration tests:
backend behavior through real request boundary

query/read model tests:
read projections and filters

frontend component/form tests:
visible UI behavior, validation, loading/error/success states

E2E tests:
walking skeleton and critical user journeys

adapter/contract tests:
external integrations or replaceable adapters
```

### 18.3 TDD Guidance

Domain TDD:

```text
If the business rule is clear, write domain tests before or with the domain object.

If the model is exploratory and likely to be rewritten,
it is acceptable to stabilize the model first and add tests after the shape is clearer.
```

Slice-level TDD / outside-in:

```text
Start from scenario acceptance.
Define slice acceptance.
Write the most useful failing test at the right boundary:
- domain test for pure invariant;
- HTTP integration test for backend behavior;
- component test for frontend behavior;
- E2E for critical walking skeleton.
```

Avoid over-mocking handlers.

Handler is orchestration and is often better tested through HTTP/API integration unless there is complex application logic worth isolating.

---

## 19. Consolidated Implementation Maps

### 19.1 Purpose

Slice Cards show one slice across concerns.

Implementation Maps show one architectural concern across all slices.

Create them when:

```text
slices become numerous
you need to see all endpoints
you need to check frontend scope
you need to check test coverage
you need to see all ports/adapters
you need to plan PRs/tasks
you need to find duplication
you need to check query slices are not using aggregates
you need to check extension slices have seams
```

### 19.2 Frontend Implementation Map

| Slice name | Slice ref | Route/Page | Feature components | State | API call | UI tests |
|---|---|---|---|---|---|---|

### 19.3 API Contract Map

| Slice name | Slice ref | Endpoint | Request | Response | Errors | Auth |
|---|---|---|---|---|---|---|

### 19.4 Application Map

| Slice name | Slice ref | Command/Query/Process | Handler/Service | Domain operation | Transaction | Errors |
|---|---|---|---|---|---|---|

### 19.5 Persistence / Read Model Map

| Slice name | Slice ref | Write model | Read model | Tables | Migration | Notes |
|---|---|---|---|---|---|---|

### 19.6 Ports / Adapters Map

| Slice name | Slice ref | Port | Adapter | Why needed | Replaceability | Tests | ADR |
|---|---|---|---|---|---|---|---|

### 19.7 Test Map

| Slice name | Slice ref | Scenario acceptance | Domain tests | HTTP/API tests | Frontend tests | E2E | Adapter/contract | Notes |
|---|---|---|---|---|---|---|---|---|

---

## 20. Changeability And Extensibility Map

### 20.1 Purpose

Changeability planning is separate from DDD and scenarios.

DDD clarifies business boundaries, but it does not automatically make every future change easy.

Changeability Map records:

```text
likely business rule changes
likely workflow changes
likely data shape changes
likely integration changes
likely infrastructure changes
planned seams
what not to over-engineer yet
ADR candidates
tests/protection
```

### 20.2 Local And Global Tracking

Local:

```text
Slice Card -> Changeability section
```

Global:

```text
Changeability Map -> grouped change candidates across slices
```

### 20.3 Changeability Map

| Change candidate | Change ref | Change type | Affected scenarios | Scenario refs | Affected slices | Slice refs | Current handling | Future handling | Avoid now | Risk/Decision | ADR |
|---|---|---|---|---|---|---|---|---|---|---|---|

Example:

| Change candidate | Change ref | Change type | Affected scenarios | Scenario refs | Affected slices | Slice refs | Current handling | Future handling | Avoid now | Risk/Decision | ADR |
|---|---|---|---|---|---|---|---|---|---|---|---|
| Applicant types expand | CHG-APP-01 | VAR:EXPAND | Request Creation, Extended Applicant Data | SC-04, SC-10 | Provide applicant data, Submit request | SL-04.2, SL-04.3 | ApplicantParty concept, individual only in L1 | add entrepreneur/legal variants | dynamic form engine in L1 | ADR? | ADR? |
| Documents become required | CHG-DOC-01 | EXT + VAR:MODIFY | Request Creation, Request Documents | SC-04, SC-11 | Submit request, Upload documents | SL-04.3, SL-11.1 | no docs required in L1 | document slice + request rule update | hardcoding docs absence | ADR? | ADR? |
| Notification provider changes | CHG-NOTIF-01 | VAR:REPLACE | Approval Result, Reliable Notification | SC-08, SC-16 | Notify client, Outbox delivery | SL-08.2, SL-16.1 | notification port | new adapter/outbox | direct SMTP in handler | ADR? | ADR? |
| Clarification workflow | CHG-WORKFLOW-01 | EXT + VAR:MODIFY | Employee Review, Clarification | SC-07, SC-12 | Approve/reject, Clarification flow | SL-07.2, SL-12.* | approve/reject only | add clarification status/commands | generic workflow engine too early | ADR? | ADR? |

### 20.4 Design Seams

Possible seams:

```text
aggregate boundary
value object
domain policy/service
application command/query
application service interface
event
pipeline/hook/policy list
port/adapter
read model/projection
database migration boundary
scenario subpage
feature/release slice
ADR-documented decision
```

Rule:

```text
Do not abstract everything in advance.
Do identify likely change points and avoid decisions that unnecessarily block them.
```

---

## 21. ADR Protocol

### 21.1 Purpose

ADRs document significant architecture decisions and rationale.

ADRs are not diagrams and not implementation plans.

They are decision records.

### 21.2 When To Create ADR

Create ADR when a decision:

```text
changes aggregate boundaries
changes consistency/transaction strategy
introduces or postpones outbox/event-driven behavior
chooses persistence strategy
chooses migration strategy
introduces a new context/bounded area
creates a significant port/adapter boundary
defines important extension seam
affects testing strategy
is hard to reverse
resolves a non-obvious trade-off
```

Do not create ADRs for every DTO, handler, endpoint or component.

### 21.3 ADR Template

```text
# ADR-000X: Decision title

## Status
Proposed / Accepted / Superseded

## Context
What problem or uncertainty forced the decision?

## Decision
What was decided?

## Alternatives Considered
- Option A
- Option B
- Option C

## Consequences
Positive:
- ...

Negative / trade-offs:
- ...

## Links
Scenarios:
- Client Request Creation — SC-04

Scenario items:
- Request status becomes Submitted — SC-04-POST-01

Slices:
- Submit connection request — SL-04.3

Planning:
- domain-model.md
- aggregate-design.md
- scenario-to-slice-map.md
- changeability-map.md

Diagrams:
- ...
```

### 21.4 ADR Links

Do not put full ADR content into diagrams or matrices.

Use readable title plus strict ADR ref:

```text
Separate L1 DbContext — ADR-004
Request review ownership — ADR-005
Notification delivery strategy — ADR-006
```

If no decision exists yet:

```text
ADR?
ADR candidate: applicant hierarchy persistence
```

---

## 22. Walking Skeleton And Slice Delivery

### 22.1 Walking Skeleton

A walking skeleton is the minimal end-to-end technical path:

```text
Frontend/UI
-> HTTP/API
-> application command/query
-> domain operation or simple behavior
-> persistence/read model
-> visible result
-> integration/e2e test
```

It proves that the architecture works before full business complexity is implemented.

### 22.2 Slice Delivery

A slice implements one useful scenario interaction across the layers it needs.

A slice can include:

```text
frontend page/component
API contract
application command/query/process
domain operation
persistence/read model
ports/adapters
tests
```

Slice delivery does not mean there are no layers.

It means implementation is planned and delivered by scenario/feature, not by technical layer.

### 22.3 Delivery Rules

```text
- Choose a slice that gives visible or verifiable behavior.
- Prefer highest value or highest risk first.
- Keep the slice small enough to finish.
- Include tests with the slice.
- Link ADRs when decisions appear.
- Add changeability notes locally and globally.
- Do not build the whole framework before the first behavior works.
```

### 22.4 Slice Levels

```text
L1 = final MVP user-facing path
L2 = richer workflow and diploma extensions
L3 = advanced/cross-cutting capabilities
```

L1/L2/L3 are delivery/roadmap metadata, not the main meaning of scenarios.

Scenario specs/diagrams should normally show final target behavior for the selected scenario, with subtle level markers if useful.

Do not generate three separate versions of the same scenario by level unless explicitly requested.

---

## 23. Recommended Planning Files

Recommended artifact set:

```text
planning/scenario-catalog.md
planning/scenario-spec.md
planning/scenario-responsibility-tables.md
planning/consolidated-layer-maps.md
planning/domain-discovery.md
planning/domain-model.md
planning/aggregate-design.md
planning/scenario-to-slice-map.md
planning/slice-cards.md
planning/implementation-maps.md
planning/changeability-map.md
planning/adr/
```

Responsibilities:

```text
scenario-catalog.md:
readable scenario titles, scenario IDs, actors/screens, goals, priority, level, status.

scenario-spec.md:
actor/screen/goal, preconditions, flow, include/extend, invariants, acceptance, observable outcomes, markers.

scenario-responsibility-tables.md:
one table per scenario, readable item labels + strict item refs, layer classification, domain signal, markers.

consolidated-layer-maps.md:
domain/application/auth/read/infrastructure maps, all with readable context + strict refs.

domain-discovery.md:
business terms, candidate concepts, rules, state transitions, events, open questions.

domain-model.md:
entities, value objects, domain events, policies, states, transitions, important identities.

aggregate-design.md:
aggregate candidates, readable source scenarios + strict refs, ownership, refs, invariants, commands, read-only scenarios, markers, ADR candidates.

scenario-to-slice-map.md:
readable scenario + scenario ref, readable slice name + slice ref, type/role, trigger/result, dependencies, markers, seam hints.

slice-cards.md:
one card per slice with frontend, API, application, domain, persistence/read, ports/adapters, tests, changeability, ADR links.

implementation-maps.md:
frontend map, API contract map, application map, persistence/read model map, ports/adapters map, test map.

changeability-map.md:
change candidates, affected scenarios/slices, current/future handling, avoid now, ADR links.

adr/:
one file per significant architecture decision.
```

---

## 24. Minimal Practical Workflow

If the full artifact set feels too heavy, use this minimum:

```text
1. scenario-catalog.md
2. scenario-spec.md
3. scenario-responsibility-tables.md
4. consolidated-layer-maps.md
5. aggregate-design.md
6. scenario-to-slice-map.md
7. slice-cards.md
8. changeability-map.md
9. adr/
```

Add `implementation-maps.md` when slices become numerous or when layer-wide review becomes useful.

---

## 25. End-To-End Example

### 25.1 Scenario

```text
Client Request Creation — SC-04
```

### 25.2 Pure Scenario

```text
Actor / Screen:
Client — Request creation screen

Goal:
Submit a connection request.

Preconditions:
Client is signed in.

Main flow:
1. Client opens request creation page.
2. Client fills request details.
3. Client provides applicant data if missing.
4. Client submits request.
5. Client sees Submitted status.

Include:
Validate request form.

Extend:
Applicant data missing -> Fill applicant data block. [ALT][VAR:EXPAND]
Upload documents. [EXT:L2]
Anonymous request. [EXT:L3]

Invariants:
Invalid request is not accepted.
Client cannot submit request for another client's applicant data.

Scenario acceptance:
- Given client is signed in
  when valid request is submitted
  then client sees Submitted status.
- Given request form is invalid
  when client submits the form
  then request is not accepted
  and validation errors are shown.

Observable outcomes:
Request is stored.
Request status becomes Submitted.
Employee can see request in submitted queue.
```

### 25.3 Responsibility Decomposition

| Item label | Item ref | Scenario item | Type | Responsibility layer | Domain signal | Scope | Change | Risk/Decision | Planning hint |
|---|---|---|---|---|---|---|---|---|---|
| Signed-in client | SC-04-PRE-01 | Client is signed in | Precondition | Auth/Security | no | CORE | — | — | framework/app auth |
| Fill request details | SC-04-STEP-02 | Client fills request details | Main step | UI/Application boundary | weak | CORE | — | — | input collection |
| Valid request accepted | SC-04-AC-01 | Valid request shows Submitted status | Acceptance | Domain + Read model | yes | CORE | — | — | status + visible result |
| Inline applicant data | SC-04-ALT-01 | Applicant data missing -> fill inline | Extend/Alt | Scenario/Application + Domain | yes | ALT | VAR:EXPAND | ADR? | same domain result as profile-first |
| Validate request form | SC-04-INC-01 | Validate request form | Include | UI + App + Domain/VO | yes | CORE | — | — | final guard in domain/value objects |
| Invalid request rejected | SC-04-INV-01 | Invalid request is not accepted | Invariant | Domain + App boundary | yes | CORE | — | — | final guard |
| Submitted status | SC-04-POST-01 | Request status becomes Submitted | Postcondition | Domain | yes | CORE | — | — | lifecycle state |
| Employee queue visibility | SC-04-POST-02 | Employee can see request | Outcome | Read model/Query | weak | CORE | — | — | projection |
| Request documents | SC-04-EXT-01 | Upload documents | Extend | App + Domain + Infra later | yes | EXT:L2 | maybe VAR:MODIFY | — | separate scenario/slice |

### 25.4 Domain Discovery

```text
Candidate concepts:
- Applicant data
- Request
- Request status
- Object address

Candidate domain rules:
- request must have valid details/address;
- request starts as Submitted;
- request references applicant data;
- request must belong to current client/applicant context.

Layer classification:
- signed-in client: application/security;
- form validation: UI/application and value objects;
- Submitted status: domain;
- employee queue visibility: query/read side;
- ownership check: application + domain reference rule.
```

### 25.5 Aggregate Draft

```text
ApplicantParty aggregate:
- stores applicant data;
- references ClientAccountId;
- does not own requests.

ClientRequest aggregate:
- owns request details, status, address;
- references ApplicantPartyId;
- protects request lifecycle invariants.
```

### 25.6 Scenario-To-Slice Row

| Slice name | Slice ref | Parent scenario | Scenario ref | Type | Role | Trigger | Observable result | Primary aggregate/read model | Depends on | Priority | Scope | Change | Risk/Decision | Handling / seam |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Submit connection request | SL-04.3 | Client Request Creation | SC-04 | Command | Primary | submit click | request Submitted | ClientRequest | applicant data | High | CORE | — | ADR? | primary aggregate ClientRequest |

### 25.7 Slice Draft

```text
Slice name:
Submit Connection Request

Slice ref:
SL-04.3

Trigger:
Client clicks Submit.

Observable result:
Client sees Submitted status.
Request is visible in MyRequests and EmployeeQueue.

Frontend:
RequestCreatePage submit action, validation errors, loading state.

API:
POST /api/requests

Application:
CreateConnectionRequestCommand

Domain:
ClientRequest.Create(...)
ApplicantParty reference/ownership check

Persistence/read:
write ClientRequests
read visible in MyRequests and EmployeeQueue

Tests:
domain creation test
HTTP happy path
invalid request
ownership failure
frontend form test

Changeability:
applicant types may expand
documents may become required
anonymous request may bypass account path

ADR:
ADR? applicant/request boundary
ADR? inline applicant data handling
```

---

## 26. Final Working Formula

```text
Scenario = what must happen.

Scenario acceptance = when behavior is complete for user/business.

Observable outcome = what can be verified from outside.

Responsibility table = which layer owns each scenario item.

Consolidated layer maps = what each layer owns across scenarios.

Domain discovery = business meanings, rules, states and lifecycle signals.

Aggregate = where business invariants and transactional consistency live.

Scenario-to-slice map = which behavior units will be delivered.

Slice = how one observable/verifiable behavior unit is delivered and tested through the layers it needs.

Extension slice = slice attached to another slice through an explicit seam.

Seam = contract that lets behavior be added, replaced or deferred.

Port = contract for external/replaceable capability.

Adapter = concrete implementation of a port.

Changeability map = where changes are expected and which seams protect them.

ADR = record explaining significant decisions and trade-offs.
```

Keep artifacts linked, but do not merge them into one overloaded diagram or document.

Use readable labels together with strict refs in all human-facing tables.

---

## 27. Current Baseline / Next Step

This file is the consolidated baseline.

Expected next step:

```text
1. Create/refine Scenario Catalog.
2. Create clean Scenario Specs.
3. Include scenario acceptance and observable outcomes in each scenario.
4. Add lightweight Scope / Change / Risk markers.
5. Build Scenario Responsibility Tables.
6. Consolidate Layer Maps.
7. Use domain rows to design domain model and aggregates.
8. Use aggregates + scenario interactions to create Scenario-To-Slice Map.
9. Create Slice Cards.
10. Extract Changeability Map and ADR candidates.
11. Create Implementation Maps if slices become numerous.
12. Implement walking skeleton / slices.
```

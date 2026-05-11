# Scenario-To-Implementation Planning Workflow

Version: v4  
Status: intermediate baseline for future planning  
Scope: reusable project-planning method, not a final design for one specific system
Latest update: V4 addendum clarifies human-readable references, strict IDs, Scope/Change/Risk marker columns, and table usage.

---

## 0. Purpose Of This Document

This document captures the current planning workflow for moving from clean user-facing scenarios to domain modeling, aggregate design, vertical-slice implementation planning, changeability planning, and ADRs.

The core problem this workflow solves:

```text
How to keep use cases clean and user-facing,
while still planning implementation deeply enough
to build the system in extensible, testable vertical slices.
```

The workflow is intentionally staged.

It prevents these common mistakes:

```text
- putting controller/handler/repository/table details into use-case diagrams;
- designing database schema before understanding domain behavior;
- designing aggregates from isolated screens instead of full domain requirements;
- treating every precondition/include/extend as domain logic;
- forgetting frontend planning when moving to implementation;
- designing backend layer-by-layer instead of scenario/feature slices;
- postponing changeability thinking until it is too late;
- hiding important architecture decisions instead of documenting ADRs.
```

---

## 1. High-Level Workflow

The full planning path:

```text
1. Pure user-facing scenarios
2. Scenario-level acceptance / observable behavior notes
3. Scenario responsibility decomposition
4. Consolidated layer maps
5. Domain discovery
6. Domain model draft
7. Aggregate boundary design
8. Scenario-to-slice implementation planning
9. Slice-level acceptance and Slice Cards
10. Consolidated implementation maps
11. Changeability / extensibility planning
12. ADRs for significant decisions
13. Walking skeleton and slice delivery
```

This is iterative. Later stages may reveal that an earlier scenario, invariant, boundary, or design seam needs to change.

The intended dependency direction:

```text
User-facing behavior
-> observable behavior
-> responsibility classification
-> domain discovery
-> aggregate boundaries
-> implementation slices
-> tests/changeability/ADRs
```

Avoid reverse pressure:

```text
Database convenience
-> fake domain model
-> polluted use-case scenario
```

---

## 2. Stage 1 — Pure User-Facing Scenarios

### 2.1 Goal

Describe what the system must do from the user/domain-expert perspective.

At this stage, focus on:

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
observable user-visible result
priority
scenario-level extension points
```

Do not focus on:

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

### 2.2 Scenario Unit

The main unit is:

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

Example:

```text
Request creation screen
-> applicant data block if missing
-> submit confirmation
-> request status page
```

If a branch has its own user goal or becomes complex, split it into a subscenario.

Examples:

```text
Login scenario
-> extend: forgot password
-> separate Password Recovery Scenario

Request creation scenario
-> extend: upload documents
-> separate Request Documents Scenario
```

### 2.3 Scenario Structure

Recommended format:

```text
Scenario ID:
Title:
Actor / Screen:
Goal:
Priority:
Level marker, if useful:
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

### 2.4 Scenario IDs

Use stable IDs so all later artifacts can reference scenarios without copying scenario text.

Example ID set:

```text
SC-01 Guest Registration
SC-02 Login
SC-03 Password Recovery
SC-04 Client Request Creation
SC-05 Client Request Status / Result
SC-06 Employee Request Dashboard
SC-07 Employee Request Review
SC-08 Approval Result / Contract Notification
SC-09 Rejection Result
SC-10 Extended Applicant Data
SC-11 Request Documents
SC-12 Clarification
SC-13 Contract Acknowledgement
SC-14 Mock Verification
SC-15 Security / Account Protection
SC-16 Reliable Notification Delivery
SC-17 Anonymous Request
SC-18 Archive / Audit
```

The numbering can evolve early, but once IDs are referenced by implementation plans or ADRs, keep them stable.

---

## 3. Scenario-Level Extension And Change Markers

Use-case/scenario diagrams should remain clean, but they may contain small planning markers.

Suggested markers:

```text
[CORE]   required target behavior
[ALT]    alternative path in the target scenario
[EXT]    future/additional scenario or branch
[VAR]    likely point of requirement change
[RISK]   uncertain or risky behavior
[DEFER]  intentionally postponed
[ADR?]   likely architecture decision needed later
```

Examples:

```text
Client submits request [CORE]
Applicant data missing -> fill applicant data inline [ALT]
Upload documents [EXT: L2]
Applicant type may expand from individual to entrepreneur/legal entity [VAR]
Anonymous request [EXT: L3]
Notification channel may change later [VAR]
```

Rules:

```text
- Markers must not dominate the scenario.
- Markers are scenario/planning metadata, not implementation details.
- If a branch becomes complex, create a separate scenario page.
- Future scenarios can be fully specified or noted as an extension idea.
```

---

## 4. Include, Extend, Preconditions, Invariants, Postconditions

These are scenario/specification concepts. They are not automatically domain concepts.

### 4.1 Preconditions

Preconditions are conditions that must be true before a scenario starts.

They may belong to different layers.

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

### 4.2 Include

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

### 4.3 Extend

`extend` means optional branch, alternative path, error path, or extension point.

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

### 4.4 Invariants

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

### 4.5 Postconditions / Observable Outcomes

Postconditions and observable outcomes state what must be true after the scenario.

Examples:

```text
request is stored
status becomes Submitted
employee can see request in queue
email notification is sent
invalid input does not create saved data
```

These are not yet low-level implementation details. They are externally visible or verifiable behavior.

---

## 5. Stage 1.1 — Observable Behavior / Acceptance Notes

After pure scenarios are drafted, add observable behavior notes.

Allowed examples:

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

Forbidden at this stage:

```text
handler calls repository
DbContext.SaveChangesAsync is called
INSERT into L1ClientRequests
EF maps owned value object columns
Dapper query joins tables
React component calls useMutation
```

Observable behavior notes bridge pure scenarios and implementation planning.

Example:

```text
User-facing:
Client sees Submitted status.

Observable behavior:
Request is stored and later appears in employee submitted queue.

Implementation later:
CreateConnectionRequestCommand persists request.
GetSubmittedRequestsQuery reads projection.
```

Only the last two lines belong to implementation planning.

---

## 6. Stage 2 — Scenario Responsibility Decomposition

### 6.1 Goal

Take each scenario and classify all meaningful elements by responsibility layer.

This is the bridge between clean scenarios and domain discovery.

Input:

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
```

Output:

```text
a responsibility table per scenario
```

### 6.2 Responsibility Layers

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

### 6.3 Scenario Responsibility Table

Template:

| Item ID | Scenario item | Type | Meaning | Responsibility layer | Domain signal | Change marker | Notes |
|---|---|---|---|---|---|---|---|

Example:

| Item ID | Scenario item | Type | Meaning | Responsibility layer | Domain signal | Change marker | Notes |
|---|---|---|---|---|---|---|---|
| SC-04-PRE-01 | Client is signed in | Precondition | user authenticated | Auth / Security | no | CORE | not domain |
| SC-04-STEP-01 | Client fills request details | Main flow | user input | UI / App boundary | weak | CORE | input collection |
| SC-04-INC-01 | Validate request form | Include | reject invalid input | UI + App + Domain/VO | yes, partly | CORE | domain must still protect final invariant |
| SC-04-ALT-01 | Applicant data missing -> fill inline | Extend/Alt | collect applicant data | Scenario/App + Domain | yes | VAR | profile-first and inline paths |
| SC-04-INV-01 | Invalid request is not accepted | Invariant | no invalid request saved | Domain + App boundary | yes | CORE | final guard in domain/value objects |
| SC-04-POST-01 | Request status becomes Submitted | Postcondition | initial lifecycle state | Domain | yes | CORE | candidate aggregate state |
| SC-04-POST-02 | Employee can see request in queue | Observable outcome | request visible in queue | Read model / Query | no/weak | CORE | projection/query concern |
| SC-04-EXT-01 | Upload documents | Extend | optional future branch | App + Domain + Infra later | yes | EXT | separate scenario |

### 6.4 Rules

```text
- Do one table per scenario.
- Classify each item by responsibility.
- Mark uncertainty explicitly.
- Do not force everything into Domain.
- Do not ignore domain signals just because UI/app validates first.
- Keep change markers attached to rows.
```

---

## 7. Stage 3 — Consolidated Layer Maps

After responsibility tables exist for multiple scenarios, merge rows by layer.

The goal is to see each layer across all scenarios.

### 7.1 Consolidated Domain Responsibility Map

| Source | Domain requirement | Candidate concept | State / invariant / event | Change marker | Notes |
|---|---|---|---|---|---|

Example:

| Source | Domain requirement | Candidate concept | State / invariant / event | Change marker | Notes |
|---|---|---|---|---|---|
| SC-04-POST-01 | New request starts as Submitted | Request | initial status | CORE | lifecycle signal |
| SC-04-INV-01 | Invalid request is not accepted | Request / value objects | invariant | CORE | details/address validation |
| SC-04-ALT-01 | Applicant data required before request | Applicant data | completeness | VAR | profile-first or inline |
| SC-07-STEP-03 | Employee approves request | Request review | transition | CORE | review workflow |
| SC-07-INV-02 | Final request cannot be approved again | Request | invariant | CORE | aggregate rule |
| SC-12-EXT-01 | Clarification may be requested | Request workflow | new status branch | EXT/VAR | L2 change point |

### 7.2 Consolidated Application Responsibility Map

| Source | Application responsibility | Needs domain? | Transaction? | Auth/current user? | Notes |
|---|---|---|---|---|---|

### 7.3 Consolidated Auth/Security Map

| Source | Security responsibility | Actor | Rule | Framework/auth concern | Domain dependency |
|---|---|---|---|---|---|

### 7.4 Consolidated Read Model Map

| Source | User-visible data | Query/read model | Source domain state | Filters | Change marker |
|---|---|---|---|---|---|

### 7.5 Consolidated Infrastructure/Integration Map

| Source | External/system behavior | Port candidate | Adapter candidate | Reliability need | Change marker |
|---|---|---|---|---|---|

### 7.6 Why This Stage Matters

The domain should not be designed from one isolated scenario.

Several scenarios may touch one domain concept or aggregate.

The consolidated maps give a whole-system view before aggregate design.

---

## 8. Stage 4 — Domain Discovery

### 8.1 Purpose

Domain discovery begins after scenario behavior is understood and responsibility rows are classified.

Identify:

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

This is where DDD concepts become primary.

### 8.2 Classification Question

For every domain-related row, ask:

```text
Is this real business/domain logic,
or is this UI, application orchestration, auth/security, persistence, read model, integration, or infrastructure?
```

### 8.3 Domain Discovery Table

| Scenario line | Business meaning | Candidate concept | Rule / state | Layer guess | Notes |
|---|---|---|---|---|---|
| Client submits request | Client asks for connection | Request | initial status Submitted | Domain | Candidate aggregate |
| Applicant data missing | Need applicant identity/data | Applicant data | required before request | Scenario/Application/Domain | Two UX entry points |
| Employee approves request | Request receives decision | Review / Decision | Approved final state | Domain | Transition rule |
| Email notification is sent | Client is informed | Notification | after decision | Application/Infrastructure | Port/outbox candidate |
| Client sees request status | Display state | Request status read model | visible to client | Query/read side | Projection candidate |

### 8.4 Domain Signals

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

### 8.5 Discovery Events

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

Events are discovery tools first. They do not automatically require event-driven implementation.

---

## 9. Stage 5 — Domain Model Draft

### 9.1 Goal

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

### 9.2 Entity vs Value Object

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

Examples:

```text
ClientAccount          -> entity / aggregate root candidate
ConnectionRequest      -> entity / aggregate root candidate
Email                  -> value object
PhoneNumber            -> value object
FullName               -> value object
Address                -> value object
RequestStatus          -> enum/state concept, maybe richer state model later
```

### 9.3 Domain Service / Policy

Use domain service or policy when:

```text
the rule is real business logic
it does not naturally belong to a single entity/aggregate
it coordinates domain concepts but should not know infrastructure
it is not merely application orchestration
```

Application orchestration is not a domain service.

Application orchestration example:

```text
load applicant
check current user
call domain factory
persist aggregate
send notification request
commit transaction
```

---

## 10. Stage 6 — Aggregate Boundary Design

### 10.1 Working Definition

```text
Aggregate = data + lifecycle + transaction boundary + invariants.
```

More precise:

```text
Aggregate = a cluster of one or more domain objects that must be kept consistent together and changed through one aggregate root.
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

| Candidate Aggregate | Source scenarios | Owns | Stored refs | Protected invariants | Commands/scenarios | Change markers | Notes |
|---|---|---|---|---|---|---|---|
| Account | SC-01, SC-02 | password hash, role, active state | none | active login state; uniqueness may be external | Registration/Login | security may expand | uniqueness may be repository/application concern |
| ApplicantParty | SC-04, SC-10 | applicant contact/data | ClientAccountId | applicant data completeness | Provide applicant data, submit request | applicant types may expand | does not own requests |
| ClientRequest | SC-04, SC-05, SC-07, SC-12, SC-18 | status, details, object address, review child later | ApplicantPartyId, reviewer id on review | valid transitions, final state protection | Submit/review/clarify/archive | workflow may expand | owns request lifecycle |
| ContractDraft | SC-08, SC-13 | draft text/status/version later | RequestId, employee id | draft lifecycle | approval result, acknowledgement | versions/templates may expand | may become richer in L2 |
| Notification | SC-08, SC-16 | notification status/delivery state | recipient id, request id | delivery lifecycle if reliable delivery | notify client | outbox/channel expansion | may move to outbox in L3 |

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
Submit request
-> primary aggregate: ClientRequest

Approve request
-> primary aggregate: ClientRequest

Provide applicant data
-> primary aggregate: ApplicantParty

Register account
-> primary aggregate: Account

View request status
-> no aggregate method; read model/query
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

## 11. Stage 7 — Scenario-To-Slice Implementation Planning

### 11.1 Key Shift

After aggregates are drafted, do not plan backend layer-by-layer.

Do not start with:

```text
all controllers
then all handlers
then all repositories
then all frontend pages
```

Plan vertical slices:

```text
Scenario
-> user interaction
-> frontend behavior
-> API contract
-> application command/query
-> domain operation
-> persistence/read model
-> tests
-> changeability notes
-> ADR links
```

### 11.2 Slice Definition

```text
Slice = the smallest useful cross-layer piece of behavior that can be implemented, tested, and shown.
```

One scenario can contain several slices.

Example:

```text
SC-04 Client Request Creation

Slice 04.1 Load request creation screen
Slice 04.2 Provide applicant data inline
Slice 04.3 Submit connection request
Slice 04.4 Show submitted result
```

### 11.3 Interaction Types

Split scenarios into interactions:

```text
Command interaction
Query interaction
UI-only interaction
Integration/event interaction
Background/worker interaction
```

Example:

| Interaction | Type | Slice? |
|---|---|---|
| Open request form | Query/UI | yes |
| Fill applicant data inline | Command/UI | yes |
| Submit request | Command | yes |
| See submitted status | Query/result | maybe part of submit slice |
| Employee sees queue | Query | separate slice |

---

## 12. Slice Card

For each slice, create a Slice Card.

Template:

```text
Slice ID:
Parent scenario:
Slice type:
Priority:
Depends on:
User goal:
Screen/context:
User action:
Visible result:

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
- command/query
- handler responsibility
- transaction boundary
- current user/role needs
- orchestration steps

Domain:
- primary aggregate
- domain method/factory
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

Changeability:
- likely changes
- extension points
- seam chosen
- avoid overengineering
- ADR candidate/link

Migration/legacy, if relevant:
- old flow remains active?
- new route/table/context?
- compatibility needed?
- cleanup later?
```

### 12.1 Slice Card Example

```text
Slice ID:
SL-04.3 Submit Connection Request

Parent scenario:
SC-04 Client Request Creation

Slice type:
Command

User goal:
Client submits a connection request.

Screen/context:
Client — Request creation screen

User action:
Click Submit.

Visible result:
Client sees Submitted status.

Frontend:
- RequestCreatePage
- request details form
- applicant data block if missing
- validation messages
- submit loading state
- error summary
- API call: create request

API contract:
- POST /api/requests
- request: applicantData? + requestDetails + address
- response: requestId, status
- errors: validation, unauthorized, applicant ownership failure

Application:
- CreateConnectionRequestCommand
- reads current client account id
- loads applicant/applicant data if existing
- creates applicant data if inline path is accepted by chosen design
- creates request
- commits transaction

Domain:
- primary aggregate: ClientRequest
- supporting aggregate/reference: ApplicantParty
- operation: create submitted request
- invariant: request starts as Submitted
- invariant: invalid request is not accepted

Persistence/read model:
- write request row
- possibly write applicant data if inline
- read model visible in MyRequests / EmployeeQueue

Ports/adapters:
- CurrentUser
- Clock

Tests:
- domain creation test
- HTTP happy path
- invalid request
- applicant belongs to another client
- frontend form validation
- optional E2E skeleton

Changeability:
- applicant types may expand
- documents may become required
- anonymous request may bypass account path
- keep request tied to applicant identity/reference
- do not create dynamic form engine in L1

ADR:
- ADR? applicant inline vs profile-first handling
- ADR? applicant/request aggregate boundary
```

---

## 13. Scenario-To-Slice Map

This is the main bridge from scenarios to implementation.

| Scenario | Slice | Type | Priority | Depends on | Extension/change marker |
|---|---|---|---|---|---|
| SC-04 Request Creation | SL-04.1 Load request form | Query/UI | High | login | applicant type expansion |
| SC-04 Request Creation | SL-04.2 Provide applicant data inline | Command/UI | High | login | profile-first vs inline |
| SC-04 Request Creation | SL-04.3 Submit request | Command | High | applicant data | documents/anonymous later |
| SC-05 Status | SL-05.1 View my requests | Query/UI | High | submitted request | filters later |
| SC-07 Review | SL-07.1 View queue | Query/UI | High | submitted request | assignment later |
| SC-07 Review | SL-07.2 Approve/reject | Command/UI | High | queue | clarification later |

---

## 14. Consolidated Implementation Maps

After Slice Cards are drafted, build maps by implementation concern.

### 14.1 Frontend Slice Map

| Slice | Route/Page | Feature components | State | API call | UI tests |
|---|---|---|---|---|---|

Frontend should be planned as part of the slice, not as an afterthought.

Useful frontend concerns:

```text
route/page
forms
feature components
entity widgets
client-side validation
loading state
empty state
error state
API client/hook
state management boundary
component tests
E2E tests
```

Frontend can use a feature/slice-based organization if useful.

Example:

```text
pages/request-create
features/provide-applicant-data
features/submit-request
entities/request
entities/applicant
shared/ui
shared/api
```

Do not let frontend structure dictate domain boundaries.

### 14.2 API Contract Map

| Slice | Endpoint | Request | Response | Errors | Auth |
|---|---|---|---|---|---|

Start from UI/backend contract, not controller class names.

Questions:

```text
What does the UI need?
What data is sent?
What is returned?
What errors are visible?
What auth/role is required?
What idempotency/concurrency concerns exist?
```

### 14.3 Application Map

| Slice | Command/Query | Handler | Domain operation | Transaction | Errors |
|---|---|---|---|---|---|

Command handlers:

```text
orchestrate use case
load aggregates
call domain
persist
commit transaction
do not contain true business rules
```

Queries:

```text
return read models/projections
do not hydrate aggregates only for display
do not call SaveChanges
may use Dapper/read-side SQL
```

### 14.4 Persistence / Read Model Map

| Slice | Write model | Read model | Tables | Migration | Notes |
|---|---|---|---|---|---|

Persistence follows domain boundaries.

Read models follow UI/query needs.

Do not force read-only slices through aggregates.

### 14.5 Ports / Adapters Map

| Slice | Port | Adapter | Why needed | Replaceability | Tests |
|---|---|---|---|---|---|

Examples:

```text
CurrentUser
Clock
EmailProvider
FileStorage
ExternalVerificationService
NotificationChannel
DocumentGenerator
```

### 14.6 Test Map

| Slice | Domain tests | HTTP/API tests | Frontend tests | E2E | Notes |
|---|---|---|---|---|---|

Test planning should follow slices.

Recommended test levels:

```text
domain unit tests for pure rules/invariants
application tests for orchestration if useful
HTTP integration tests for backend behavior
query/read model tests
frontend component/form tests
E2E tests for walking skeleton / critical flows
adapter contract tests for integrations
```

---

## 15. Changeability And Extensibility Planning

### 15.1 Two Levels

Changeability must be tracked both locally and globally.

Local:

```text
Slice Card -> Changeability section
```

Global:

```text
Changeability Map -> grouped change candidates across slices
```

### 15.2 Local Slice Changeability

Each Slice Card should record:

```text
likely changes
extension points
chosen seam
what not to over-engineer yet
tests that protect behavior
ADR candidate/link
```

### 15.3 Global Changeability Map

| Change candidate | Affected slices | Type | Seam | Avoid now | ADR |
|---|---|---|---|---|---|
| Applicant types expand | SC-04, SC-10, SC-11 | Business/data/workflow | ApplicantParty abstraction + scenario branch | dynamic form engine in L1 | ADR? persistence strategy |
| Request workflow adds clarification | SC-07, SC-12 | Business workflow | request state transitions | generic workflow engine in L1 | ADR? status model |
| Email becomes outbox/SMS/internal | SC-08, SC-16 | Integration/reliability | notification port/outbox | reliable delivery in L1 if not needed | ADR notification strategy |
| Mock verification becomes external | SC-14 | Integration/business | verification port/result model | provider-specific domain dependency | ADR external boundary |

### 15.4 Change Types

```text
Business rule change
Workflow change
Data shape change
Integration change
Infrastructure change
UX change
Consistency/reliability change
Reporting/read model change
Security/compliance change
```

### 15.5 Design Seams

```text
aggregate boundary
value object
domain policy/service
application command/query
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

## 16. ADR Protocol

### 16.1 Purpose

ADRs document significant architecture decisions and rationale.

ADRs are not diagrams and not implementation plans. They are decision records.

### 16.2 When To Create ADR

Create ADR when a decision:

```text
changes aggregate boundaries
changes consistency/transaction strategy
introduces or postpones outbox/event-driven behavior
chooses persistence strategy
chooses migration strategy
introduces a new context/bounded area
creates a significant port/adapter boundary
affects testing strategy
is hard to reverse
resolves a non-obvious trade-off
```

Do not create ADRs for every DTO, handler, or component.

### 16.3 ADR Template

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
- SC-...

Slices:
- SL-...

Planning:
- domain-model.md
- aggregate-design.md
- implementation-plan.md
- changeability-map.md

Diagrams:
- ...
```

### 16.4 ADR Links

Do not put full ADR content into diagrams or matrices.

Use small links:

```text
ADR-004 separate L1DbContext
ADR-005 request review ownership
ADR-006 notification delivery strategy
```

If no decision exists yet:

```text
ADR?
ADR candidate: applicant hierarchy persistence
```

---

## 17. Walking Skeleton And Vertical Slice Delivery

### 17.1 Walking Skeleton

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

### 17.2 Vertical Slice

A vertical slice implements one useful scenario interaction across layers.

A slice can include:

```text
frontend page/component
API contract
application command/query
domain operation
persistence/read model
ports/adapters
tests
```

Vertical slice does not mean there are no layers.

It means implementation is planned and delivered by scenario/feature, not by technical layer.

### 17.3 Delivery Rules

```text
- Choose a slice that gives visible behavior.
- Prefer highest value or highest risk first.
- Keep the slice small enough to finish.
- Include tests with the slice.
- Link ADRs when decisions appear.
- Add changeability notes locally and globally.
- Do not build the whole framework before the first behavior works.
```

### 17.4 Slice Levels

```text
L1 = final MVP user-facing path
L2 = richer workflow and diploma extensions
L3 = advanced/cross-cutting capabilities
```

L1/L2/L3 are delivery/roadmap metadata, not the main meaning of scenarios.

Scenario diagrams should normally show final target behavior for the selected scenario, with subtle level markers if useful.

Do not generate three separate versions of the same scenario by level unless explicitly requested.

---

## 18. Recommended Planning Files

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

### scenario-catalog.md

```text
scenario IDs
titles
actors/screens
priority
level marker
status
```

### scenario-spec.md

```text
actor/screen/goal
preconditions
main flow
include
extend
invariants
postconditions
scenario acceptance criteria
observable outcomes
extension/change markers
```

### scenario-responsibility-tables.md

```text
one table per scenario
classify scenario items by responsibility layer
domain signal
change marker
notes
```

### consolidated-layer-maps.md

```text
domain map
application map
auth/security map
read model map
infrastructure map
```

### domain-discovery.md

```text
business terms
candidate concepts
rules
state transitions
events
layer classification
open questions
```

### aggregate-design.md

```text
aggregate candidates
source scenarios
owned entities
stored refs
protected invariants
commands/scenarios
rejected boundaries
change markers
ADR candidates
```

### scenario-to-slice-map.md

```text
scenario
slice
type
priority
dependencies
extension/change marker
```

### slice-cards.md

```text
one card per slice
frontend
API contract
application
domain
persistence/read model
ports/adapters
tests
changeability
ADR links
```

### implementation-maps.md

```text
frontend map
API contract map
application map
persistence/read model map
ports/adapters map
test map
```

### changeability-map.md

```text
change candidates
affected slices
change type
design seam
avoid now
tests/protection
ADR links
```

### adr/

```text
one file per significant architecture decision
```

---

## 19. Example End-To-End Planning Record

### Scenario

```text
SC-04 Client Request Creation
```

### Pure Scenario

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
Applicant data missing -> Fill applicant data block.
Upload documents, L2.
Anonymous request, L3.

Invariants:
Invalid request is not accepted.
Client cannot submit request for another client's applicant data.

Observable outcomes:
Request is stored.
Request status becomes Submitted.
Employee can see request in submitted queue.
```

### Responsibility Decomposition

| Item | Type | Responsibility | Domain signal | Change marker |
|---|---|---|---|---|
| Client is signed in | Precondition | Auth/Security | no | CORE |
| Fill request details | Main step | UI/Application boundary | weak | CORE |
| Applicant data missing | Extend/Alt | Scenario/Application + Domain | yes | VAR |
| Validate request form | Include | UI + App + Domain/VO | yes | CORE |
| Invalid request is not accepted | Invariant | Domain + App boundary | yes | CORE |
| Status becomes Submitted | Postcondition | Domain | yes | CORE |
| Employee can see request | Outcome | Read model/Query | no/weak | CORE |
| Upload documents | Extend | App + Domain + Infra later | yes | EXT |

### Domain Discovery

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

### Aggregate Draft

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

### Slice Draft

```text
SL-04.3 Submit Connection Request

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

## 20. Final Working Formula

```text
Scenarios say what must happen.

Scenario acceptance and observable outcomes say what can be verified from outside.

Responsibility tables classify scenario elements by layer.

Consolidated layer maps show the whole system by responsibility.

Domain discovery finds business meanings, rules and lifecycles.

Aggregate design defines transactional consistency boundaries.

Scenario-to-slice planning turns scenarios into frontend + API + application + domain + persistence + tests.

Changeability planning records likely changes and chosen seams.

ADRs explain important decisions and trade-offs.

Vertical slices deliver behavior across layers.
```

Keep artifacts linked, but do not merge them into one overloaded diagram or document.

---

## 21. Current Status

This file captures the current planning baseline.

Expected next step:

```text
1. Create/refine clean scenario specifications.
2. Add observable behavior and extension/change markers.
3. Build scenario responsibility tables.
4. Consolidate layer maps.
5. Use domain rows to design domain model and aggregates.
6. Use aggregates + scenario interactions to create Scenario-to-Slice Map.
7. Create Slice Cards and implementation maps.
8. Extract Changeability Map and ADR candidates.
9. Implement walking skeleton / vertical slices.
```

---

## 22. V3 Addendum — Acceptance, Slices, Ports/Adapters, Extension Seams

This addendum updates and clarifies the v2 workflow. It should be treated as part of the current baseline.

The key new conclusions are:

```text
- Scenario-level acceptance belongs inside the scenario.
- Responsibility tables are not slices.
- Slices are units of observable/verifiable behavior and delivery.
- Slices include only the layers needed for that behavior.
- Slices may be full vertical, UI-only, query-only, backend/background, integration, or extension slices.
- Extension slices should attach to primary slices through explicit seams.
- Ports/adapters are contracts and implementations for external or replaceable concerns.
- Slice-level acceptance is different from scenario-level acceptance.
```

---

### 22.1 Acceptance Criteria Levels

Acceptance criteria should exist at more than one level.

#### Scenario-Level Acceptance

Scenario-level acceptance is part of the scenario.

It answers:

```text
When does the user/domain expert consider this behavior complete?
```

It should remain behavior-focused and avoid implementation details.

Example:

```text
Given client is signed in
When client submits a valid connection request
Then client sees Submitted status

Given request form is invalid
When client submits the form
Then request is not accepted
And client sees validation errors

Given applicant data is missing
When client starts request creation
Then client can provide applicant data inline
```

Scenario-level acceptance should not mention:

```text
handler
repository
DbContext
SQL INSERT
React hook
specific ORM mapping
```

#### Observable Outcomes

Observable outcomes can be included directly in the scenario.

Examples:

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

These are externally visible or verifiable outcomes. They are not low-level implementation details.

#### Slice-Level Acceptance

Slice-level acceptance appears during slice planning.

It answers:

```text
How will this specific behavior unit be verified through the selected implementation boundaries?
```

Example:

```text
POST /api/requests with valid data returns request id and Submitted status.
Created request appears in MyRequests.
Created request appears in employee submitted queue.
Invalid input returns validation errors.
Frontend shows loading state while submitting.
Frontend shows validation summary on validation error.
```

This can mention API, frontend states, read models and tests because it belongs to implementation planning.

#### Technical Acceptance / Definition Of Done

Technical acceptance is an engineering checklist.

Examples:

```text
domain invariant covered by unit test
HTTP integration test added
frontend component test added
migration added if needed
ADR linked if decision is significant
no direct dependency on concrete provider
existing scenarios still pass
```

Summary:

```text
Scenario acceptance = business/user completion criteria.
Observable outcomes = externally verifiable state/result.
Slice acceptance = implementation-boundary verification criteria.
Technical acceptance = engineering quality checklist.
```

---

### 22.2 Responsibility Table vs Slice

Responsibility table and slice are related but different.

#### Responsibility Table

Responsibility table is analysis.

It answers:

```text
Which layer is responsible for this scenario item?
```

It is created before aggregate design.

It contains:

```text
preconditions
main flow items
include
extend
invariants
postconditions
scenario acceptance
observable outcomes
change markers
layer classification
domain signal
notes
```

It does not define:

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

#### Slice

Slice is implementation/delivery planning.

It answers:

```text
How do we implement and test one unit of observable/verifiable behavior through the layers it needs?
```

A responsibility row is not automatically a slice.

Conversion is:

```text
scenario responsibility table
-> extract interactions
-> group related rows into candidate slices
-> validate trigger/result/testability
-> finalize after aggregate design
```

---

### 22.3 Slice Definition

Current working definition:

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

A slice is not necessarily:

```text
one endpoint
one controller
one handler
one table
one aggregate
one React component
one screen
one whole scenario
```

Slice is behavior, not a file/class/table boundary.

---

### 22.4 What A Good Slice Has

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

A good slice should be small enough to implement and test, but not so small that it no longer delivers behavior.

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

---

### 22.5 Slice Types

#### Command Slice

Changes system state.

Examples:

```text
Submit request
Approve request
Provide applicant data
Register account
```

#### Query Slice

Shows data.

Examples:

```text
View my requests
View submitted queue
View request details
```

Query slices may not use aggregate methods. They often use read models/projections.

#### UI-Only Slice

Implements frontend-only behavior.

Examples:

```text
request form stepper
show applicant data block
local validation display
expand/collapse request details
```

A UI-only slice is valid if it has independently testable visible behavior.

#### Integration Slice

Communicates with an external system.

Examples:

```text
send email
upload file
call verification service
generate document
```

#### Backend / Background Slice

Runs without a direct user click.

Examples:

```text
outbox delivery
archive old requests
retry failed notifications
write audit entry after action
```

#### Extension Slice

Attaches to another slice.

Examples:

```text
send notification after approval
create contract draft after approval
upload documents during request creation
run verification after submit
```

---

### 22.6 Scenario-To-Slice Conversion

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
9. Create Slice Card.
10. Add frontend/API/application/domain/persistence/tests as needed.
11. Add changeability notes.
12. Add ADR links/candidates.
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

### 22.7 Slice vs Aggregate

Aggregate and slice are different things.

```text
Aggregate = domain consistency boundary.
Slice = delivery/testability boundary for behavior.
```

Several slices can target one aggregate.

Examples:

```text
ClientRequest aggregate

SL-04.3 Submit request
-> ClientRequest.Create(...)

SL-07.2 Approve request
-> request.Approve(...)

SL-07.3 Reject request
-> request.Reject(...)

SL-12.1 Request clarification
-> request.RequestClarification(...)

SL-18.1 Archive request
-> request.Archive(...)
```

A command slice usually has one primary aggregate transaction boundary.

A query slice may have no aggregate method.

Rule:

```text
Do not force read-only slices to hydrate aggregates.
```

Slices can be sketched before aggregates, but they should be finalized after aggregate boundaries are understood.

---

### 22.8 Primary Slices And Extension Slices

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

---

### 22.9 Seams For Extension Slices

Extension behavior can attach through different seams.

#### Direct Application Service / Policy Call

Example:

```text
ApproveRequestHandler
-> contractDraftService.CreateForApprovedRequest(...)
-> notificationService.NotifyClient(...)
```

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

Example:

```text
RequestApproved event
-> ContractDraft handler
-> Notification handler
-> Audit handler
```

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

Example:

```text
approval workflow
-> run registered approval side effects
```

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

Seam selection rule:

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

Core behavior should not depend on optional extension implementation.

Example:

```text
Approve request should not depend directly on SmtpClient.
It may depend on INotificationPort, approval side-effect interface, event, or pipeline.
```

---

### 22.10 Ports And Adapters

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

Examples of ports:

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

Examples of adapters:

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

Repository can be treated as a port:

```text
IClientRequestRepository = port
EfClientRequestRepository = adapter
```

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

Rule:

```text
A slice may use external capabilities,
but should depend on stable contracts,
not concrete technologies.
```

---

### 22.11 Backend Slice Principles

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

Handler orchestrates; domain decides.

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

### 22.12 Frontend Slice Principles

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

### 22.13 Slice Testing And TDD

Use the right test type for the behavior.

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

TDD guidance:

```text
If the domain rule is clear, write domain tests before or with the domain object.

If the model is exploratory and likely to be rewritten, it is acceptable to stabilize the model first and add tests after the shape is clearer.

For slices, start from scenario acceptance, define slice acceptance, then write the most useful failing test at the right boundary:
- domain test for pure invariant;
- HTTP integration test for backend behavior;
- component test for frontend behavior;
- E2E for critical walking skeleton.
```

Avoid over-mocking handlers.

Handler is orchestration and is often better tested through HTTP/API integration unless there is complex application logic worth isolating.

---

### 22.14 Updated Slice Card Template

```text
Slice ID:
Parent scenario:
Slice type:
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

### 22.15 Updated Final Formula

```text
Scenario = what must happen.

Scenario acceptance = when behavior is complete for user/business.

Observable outcome = what can be verified from outside.

Responsibility table = which layer owns each scenario item.

Aggregate = where business invariants and transactional consistency live.

Slice = how one observable/verifiable behavior unit is delivered and tested through the layers it needs.

Extension slice = slice attached to another slice through an explicit seam.

Seam = contract that lets behavior be added, replaced or deferred.

Port = contract for external/replaceable capability.

Adapter = concrete implementation of a port.

ADR = record explaining significant decisions and trade-offs.
```

---

## 23. V4 Addendum — Readable References, Marker Columns And Table Workflow

This addendum supersedes earlier single-column `Source` and generic `Change marker` usage where more precision is needed.

The current recommendation is:

```text
Readable first, strict ref second.

Use lightweight inline markers in scenarios.

Use explicit Scope / Change / Risk columns in responsibility tables and maps.

Use concrete Handling / Seam decisions only in slice cards, changeability map and ADRs.
```

---

### 23.1 Human-Readable References

Machine-only references are useful for AI, traceability and ADR links, but they are not readable enough for planning.

Bad:

| Source | Domain requirement |
|---|---|
| SC-04-POST-01 | New request starts as Submitted |

Good:

| Scenario | Scenario item | Item ref | Domain requirement |
|---|---|---|---|
| Client Request Creation | Request status becomes Submitted | SC-04-POST-01 | New request starts as Submitted |

General rule:

```text
Do not use a single `Source` column in human-facing tables.

Use readable labels/context plus strict refs.
```

Preferred pairs:

| Context | Human-readable column | Strict ref column |
|---|---|---|
| Scenario | Scenario | Scenario ref |
| Scenario item | Scenario item | Item ref |
| Slice | Slice name | Slice ref |
| ADR | ADR title | ADR ref |
| Change candidate | Change candidate | Change ref, optional |

Common examples:

| Thing | Human-readable | Strict ref |
|---|---|---|
| Scenario | Client Request Creation | SC-04 |
| Scenario item | Request status becomes Submitted | SC-04-POST-01 |
| Slice | Submit connection request | SL-04.3 |
| ADR | Request aggregate boundary | ADR-005 |
| Change candidate | Applicant types expand | CHG-APP-01 |

Human readability rule:

```text
Readable label first.
Strict ref second.
Never force humans to read only SC-04-POST-01.
```

---

### 23.2 Updated Marker Model

The old generic `[VAR]` marker is too broad.

The current marker model has three separate axes:

```text
1. Scope / roadmap
2. Change / volatility
3. Risk / decision
```

#### Scope / Roadmap

Scope markers answer:

```text
Is this part of the current target behavior,
or is it additional/deferred behavior?
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

#### Change / Volatility

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
Applicant type may expand:
individual -> entrepreneur -> legal entity. [VAR:EXPAND]

Request workflow may add clarification before final decision. [VAR:MODIFY]

Email provider may change from SMTP to external service. [VAR:REPLACE]
```

#### Risk / Decision

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

---

### 23.3 EXT vs VAR

This distinction is critical.

```text
[EXT] = what can be added?

[VAR] = what can change in existing behavior, model, rule, workflow, provider or strategy?
```

They are handled differently.

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

Some items legitimately have both markers.

Examples:

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

If documents become required before submit:

```text
Documents upload. [EXT:L2][VAR:MODIFY]
```

Reason:

```text
[EXT] because document upload is a new behavior.
[VAR:MODIFY] because requiring documents changes Submit Request acceptance/invariants.
```

```text
External verification. [EXT:L2/L3][VAR:REPLACE]
```

Reason:

```text
[EXT] because verification is an added capability.
[VAR:REPLACE] because mock verification may be replaced by external provider.
```

---

### 23.4 Marker Columns By Artifact

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

### 23.5 Responsibility Table Columns

Recommended responsibility table columns:

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

Example:

| Item label | Item ref | Scenario item | Type | Responsibility layer | Domain signal | Scope | Change | Risk/Decision | Planning hint |
|---|---|---|---|---|---|---|---|---|---|
| Signed-in client | SC-04-PRE-01 | Client is signed in | Precondition | Auth/Security | no | CORE | — | — | framework/app auth |
| Inline applicant data | SC-04-ALT-01 | Applicant data missing -> fill inline | Extend/Alt | App + Domain | yes | ALT | VAR:EXPAND | ADR? | same domain result as profile-first |
| Submitted status | SC-04-POST-01 | Request status becomes Submitted | Postcondition | Domain | yes | CORE | — | — | request lifecycle state |
| Request documents | SC-04-EXT-01 | Upload documents | Extend | App + Domain + Infra later | yes | EXT:L2 | maybe VAR:MODIFY | — | separate scenario/slice |

At this stage, planning hints should stay lightweight.

Good hints:

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

### 23.6 Consolidated Layer Map Columns

Consolidated maps should preserve human context and strict refs.

Recommended base prefix:

```text
Scenario | Scenario ref | Scenario item | Item ref
```

Then add layer-specific columns.

#### Domain Map

| Scenario | Scenario ref | Scenario item | Item ref | Domain requirement | Candidate concept | State / invariant / event | Scope | Change | Risk/Decision | Domain handling hint |
|---|---|---|---|---|---|---|---|---|---|---|

#### Application Map

| Scenario | Scenario ref | Scenario item | Item ref | Application responsibility | Needs domain? | Needs auth/current user? | Transaction? | Scope | Change | Risk/Decision | App handling hint |
|---|---|---|---|---|---|---|---|---|---|---|---|

#### Auth/Security Map

| Scenario | Scenario ref | Scenario item | Item ref | Security responsibility | Actor | Rule | Domain dependency | Scope | Change | Risk/Decision | Notes |
|---|---|---|---|---|---|---|---|---|---|---|---|

#### Read Model Map

| Scenario | Scenario ref | Scenario item | Item ref | User-visible data | Query/read model | Source domain state | Filters/sorting | Scope | Change | Risk/Decision |
|---|---|---|---|---|---|---|---|---|---|---|

#### Infrastructure/Integration Map

| Scenario | Scenario ref | Scenario item | Item ref | External/system behavior | Port candidate | Adapter candidate | Reliability need | Scope | Change | Risk/Decision | Notes |
|---|---|---|---|---|---|---|---|---|---|---|---|

---

### 23.7 Scenario-To-Slice Map Columns

Recommended columns:

| Slice name | Slice ref | Parent scenario | Scenario ref | Type | Role | Trigger | Observable result | Primary aggregate/read model | Depends on | Priority | Scope | Change | Risk/Decision | Handling / seam |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|

Purpose:

```text
Show which behavior units will be delivered,
where they came from,
what they depend on,
and where extension/change/decision markers matter.
```

Example:

| Slice name | Slice ref | Parent scenario | Scenario ref | Type | Role | Trigger | Observable result | Primary aggregate/read model | Depends on | Priority | Scope | Change | Risk/Decision | Handling / seam |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Submit connection request | SL-04.3 | Client Request Creation | SC-04 | Command | Primary | submit click | request Submitted | ClientRequest | applicant data | High | CORE | — | ADR? | primary aggregate ClientRequest |
| Notify client | SL-08.2 | Approval Result / Contract Notification | SC-08 | Integration/Extension | Extension | request reviewed | notification sent/recorded | Notification / port | approve/reject | Medium | CORE/EXT depends | VAR:REPLACE | ADR? | notification port/event |
| Reliable outbox delivery | SL-16.1 | Reliable Notification Delivery | SC-16 | Background | Extension | scheduled worker | pending notifications delivered | Outbox/Notification | notification record | Low | DEFER:L3 | VAR:MODIFY | ADR? | background worker/outbox |

---

### 23.8 Changeability Map Columns

Changeability map focuses on expected variation/change, not on every scope marker.

Recommended columns:

| Change candidate | Change ref | Change type | Affected scenarios | Scenario refs | Affected slices | Slice refs | Current handling | Future handling | Avoid now | Risk/Decision | ADR |
|---|---|---|---|---|---|---|---|---|---|---|---|

Example:

| Change candidate | Change ref | Change type | Affected scenarios | Scenario refs | Affected slices | Slice refs | Current handling | Future handling | Avoid now | Risk/Decision | ADR |
|---|---|---|---|---|---|---|---|---|---|---|---|
| Applicant types expand | CHG-APP-01 | VAR:EXPAND | Request Creation, Extended Applicant Data | SC-04, SC-10 | Provide applicant data, Submit request | SL-04.2, SL-04.3 | ApplicantParty concept, individual only in L1 | add entrepreneur/legal variants | dynamic form engine in L1 | ADR? | ADR? |
| Documents become required | CHG-DOC-01 | EXT + VAR:MODIFY | Request Creation, Request Documents | SC-04, SC-11 | Submit request, Upload documents | SL-04.3, SL-11.1 | no docs required in L1 | document slice + request rule update | hardcoding docs absence | ADR? | ADR? |
| Notification provider changes | CHG-NOTIF-01 | VAR:REPLACE | Approval Result, Reliable Notification | SC-08, SC-16 | Notify client, Outbox delivery | SL-08.2, SL-16.1 | notification port | new adapter/outbox | direct SMTP in handler | ADR? | ADR? |

---

### 23.9 Updated Artifact Meanings

```text
Scenario Spec:
what must happen.

Responsibility Table:
whose responsibility each scenario item is.

Consolidated Layer Maps:
what each layer owns across scenarios.

Aggregate Design:
where domain consistency lives.

Scenario-To-Slice Map:
which behavior units will be delivered.

Slice Card:
how one behavior unit will be implemented and tested.

Implementation Maps:
how all slices look by one architectural concern.

Changeability Map:
where changes are expected and which seams protect them.

ADR:
why significant decisions were made.
```

Final rule:

```text
In scenarios, mark what may matter.

In responsibility tables, classify whose responsibility it is.

In consolidated maps, understand layer-level impact.

In slice planning, choose attachment points and seams.

In changeability planning, track expected changes globally.

In ADRs, record significant decisions.
```


# Scenario To Implementation Workflow

This document captures the current intermediate conclusion about the planning workflow.

It is a reusable method note, not a final architecture design for one specific project. The goal is to describe how to move from clean user-facing scenarios to domain modeling, aggregate design, implementation planning, ADRs, and changeability planning without mixing concerns too early.

---

## 1. Core Principle

Scenario specifications and implementation planning are separate stages.

```text
Scenario/specification artifacts describe what the system must do.
Implementation planning describes how we will build and evolve it.
```

Clean scenario diagrams/specs must not be polluted with controllers, handlers, repositories, DbContext, EF mapping, table names, SQL joins, or aggregate method names.

The intended planning path is:

```text
1. Pure user-facing scenarios
2. Observable behavior / acceptance notes
3. Domain discovery
4. Domain model draft
5. Aggregate boundary design
6. Application / persistence / testing implementation planning
7. Changeability and extensibility planning
8. ADRs for significant decisions
9. Implementation slices
```

The process is iterative. It is normal to return to earlier stages when later work reveals a missing rule, unclear term, wrong boundary, or risky decision.

The dependency direction should remain clean:

```text
User-facing behavior -> domain model -> implementation plan
```

Avoid the reverse pressure:

```text
Database convenience -> fake domain model -> polluted user scenario
```

---

## 2. Stage 1 — Pure User-Facing Scenarios

### Purpose

Describe behavior from the user's and domain expert's perspective.

At this stage we care about:

```text
actor
screen/application context
user goal
preconditions
main flow
include
extend
invariants
user-visible or externally observable result
priority
```

At this stage we do not care about:

```text
controller
handler
repository
DbContext
EF mapping
SQL table
aggregate method name
command/query class name
```

### Scenario Unit

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

A scenario may contain several screens if they form one coherent user journey.

Example:

```text
Request creation screen
-> applicant data block if missing
-> submit confirmation
-> request status page
```

If a branch has its own user goal or becomes complex, split it into a separate subscenario.

Examples:

```text
Login scenario
-> extend: forgot password
-> separate Password Recovery Scenario

Request creation scenario
-> extend: upload documents
-> separate Request Documents Scenario
```

### Scenario Structure

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
Observable outcomes:
Open questions:
```

### Scenario IDs

Use stable scenario IDs so later planning artifacts can reference scenarios without duplicating text.

Suggested initial set:

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

The exact numbering can evolve, but once an ID is used in implementation planning or ADRs, keep it stable.

---

## 3. Include, Extend, Preconditions, Invariants

These are specification concepts, not automatic domain concepts. They must be classified later.

### Preconditions

Preconditions are conditions that must be true before a scenario starts.

They may belong to different layers.

Examples:

```text
Client is signed in.
```

Usually application/security.

```text
Request is still in Submitted status.
```

Likely domain.

```text
Applicant data is available or can be provided inline.
```

Scenario/application flow, with possible domain consequences.

Rule:

```text
Do not assume every precondition is a domain invariant.
Classify it during domain discovery.
```

### Include

`include` is a mandatory reusable step or subscenario.

Examples:

```text
<<include>> Validate registration form
<<include>> Save account
<<include>> Validate request form
<<include>> Save submitted request
```

An included step can be UI validation, application orchestration, domain invariant enforcement, persistence outcome, or observable system behavior. Classify it later.

### Extend

`extend` is an optional branch, alternative path, error path, or extension point.

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

### Invariants

Invariants are rules that must remain true, but they can live at different levels.

Examples:

```text
Invalid request is not accepted.
```

May involve UI/application/domain validation.

```text
A client cannot submit a request for another client's applicant data.
```

Likely application authorization plus domain reference/ownership rule.

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
Domain invariants drive aggregate boundaries.
But not every scenario invariant is a domain invariant.
```

---

## 4. Stage 1.1 — Observable Behavior / Acceptance Notes

### Purpose

After pure scenarios are drafted, add externally visible or verifiable outcomes.

These notes are still not low-level implementation details.

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
```

Observable behavior notes bridge pure scenarios and implementation planning.

Example:

```text
User-facing:
Client sees Submitted status.

Observable behavior:
Request is stored and later appears in employee submitted queue.

Implementation later:
CreateConnectionRequestCommand persists request; GetSubmittedRequestsQuery reads projection.
```

Only the last line belongs to implementation planning.

---

## 5. Stage 2 — Domain Discovery

### Purpose

Domain discovery begins after scenario behavior is understood.

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

This is the first stage where DDD concepts become primary.

### Classification Question

For every important scenario line, ask:

```text
Is this real business/domain logic,
or is this UI, application orchestration, auth/security, persistence, read model, integration, or infrastructure?
```

Classification categories:

```text
Domain rule
Domain state transition
Domain concept/entity/value object
Application orchestration
Authorization/security
UI/input validation
Persistence concern
Read model/query concern
External integration/infrastructure
Cross-cutting concern
Open question
```

### Domain Discovery Table

| Scenario line | Business meaning | Candidate concept | Rule / state | Layer guess | Notes |
|---|---|---|---|---|---|
| Client submits request | Client asks for connection | Request | initial status Submitted | Domain | Candidate aggregate |
| Applicant data missing | Need applicant identity/data | Applicant data | required before request | Scenario/Application/Domain | Two UX entry points |
| Employee approves request | Request receives decision | Review / Decision | Approved final state | Domain | Transition rule |
| Email notification is sent | Client is informed | Notification | after decision | Application/Infrastructure | Port/outbox candidate |
| Client sees request status | Display state | Request status read model | visible to client | Query/read side | Projection candidate |

### Domain Signals

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

## 6. Stage 3 — Domain Model Draft

### Purpose

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

Do not start with database tables, controllers, or DTOs.

### Entity vs Value Object

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

### Domain Service / Policy

Use a domain service or policy when:

```text
the rule is real business logic
it does not naturally belong to a single entity/aggregate
it coordinates domain concepts but should not know infrastructure
it is not merely application orchestration
```

Application orchestration is not a domain service.

Example application orchestration:

```text
load applicant
check current user
call domain factory
persist aggregate
send notification request
commit transaction
```

---

## 7. Stage 4 — Aggregate Boundary Design

### Working Definition

```text
Aggregate = data + lifecycle + transaction boundary + invariants.
```

More precise:

```text
Aggregate = a cluster of one or more domain objects that must be kept consistent together and changed through one aggregate root.
```

### Questions To Find Aggregates

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

### Aggregate Candidate Table

| Candidate Aggregate | Owns | Stored refs | Protected invariants | Commands/scenarios | Notes |
|---|---|---|---|---|---|
| Account | password hash, role, active state | none | email uniqueness outside aggregate, active login state | Registration/Login | uniqueness may be repository/application concern |
| ApplicantParty | applicant contact/data | ClientAccountId | applicant data completeness | Provide applicant data, submit request | does not own requests |
| ClientRequest | status, details, object address, review child entity later | ApplicantPartyId, reviewer id on review | valid transitions, final state protection | Submit request, review request | owns request lifecycle |
| ContractDraft | draft text/status/version later | RequestId, employee id | draft lifecycle | approval result | may become richer in L2 |
| Notification | notification status/delivery state | recipient id, request id | delivery lifecycle if reliable delivery | notify client | may move to outbox in L3 |

### Aggregate Rules

Use small aggregates by default.

Do not make an aggregate larger only because the database has a foreign key.

```text
A database FK does not imply aggregate ownership.
```

Cross-aggregate references should normally be stored by identity.

Example:

```text
ApplicantParty stores ClientAccountId.
ClientRequest stores ApplicantPartyId.
```

Do not model this as domain ownership unless a real consistency rule requires it:

```text
ClientAccount owns ApplicantParties owns Requests
```

### Validate Aggregates Against Scenarios

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

## 8. Stage 5 — Implementation Planning

### Purpose

Implementation planning begins after domain concepts and aggregate boundaries are drafted.

Plan:

```text
application commands
application queries
transaction boundaries
repositories
ports/adapters
persistence mapping
read models
API endpoints
tests
release slices
```

This stage uses DDD decisions, but it also includes architecture, testing, persistence, and delivery planning.

### Scenario To Implementation Matrix

| Scenario | Domain operation | App command/query | Transaction | Persistence | Read side | Ports/adapters | Tests | ADR |
|---|---|---|---|---|---|---|---|---|
| SC-04 Client Request Creation | Create request | CreateConnectionRequestCommand | create one request | ClientRequests | MyRequestsDto | CurrentUser, Clock | HTTP + domain | ADR if boundary disputed |
| SC-07 Employee Review | Approve/Reject request | ReviewRequestCommand | request status + review child | Requests, Reviews | EmployeeQueueDto | NotificationPort | HTTP + domain | ADR for review ownership |
| SC-08 Approval Result | Create draft + notify | ApprovalPolicy / handler | same transaction or event | ContractDrafts, Notifications | RequestDetailsDto | EmailPort / Outbox later | integration | ADR for notification strategy |

This matrix links clean scenarios to implementation slices without polluting scenarios.

### Application Layer Planning

For each scenario slice define:

```text
command/query name
input DTO shape
authenticated actor/current user needs
aggregates loaded
domain method/factory called
repository operations
transaction boundary
returned result/read model
errors and problem details
tests
```

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

### Persistence Planning

For each aggregate decide:

```text
table(s)
TPH/TPT/separate table strategy if relevant
PK/FK shape
owned value object columns
concurrency strategy if needed
migration path
old/new coexistence if migrating
test database reset strategy
```

Persistence should follow domain boundaries, not define them.

### Ports And Adapters

Identify replaceable or external concerns:

```text
email provider
SMS provider
file storage
external verification service
authentication/current user provider
clock/time provider
document generation
payment/signature provider if future scope appears
database provider if relevant
```

For each port ask:

```text
Is this business policy or infrastructure?
Can the domain stay independent from it?
What interface belongs in application layer?
What adapter belongs in infrastructure?
What tests protect replacement?
```

---

## 9. Stage 6 — Changeability And Extensibility Planning

### Purpose

Changeability planning is separate from DDD and scenarios.

DDD clarifies business boundaries, but it does not automatically make every future change easy.

Changeability planning records:

```text
likely business rule changes
likely workflow changes
likely data shape changes
likely integration changes
likely infrastructure changes
planned seams
decisions not to over-engineer yet
```

### Volatility Map

| Area | Change candidate | Change type | Design response | Avoid for now | Tests/Protection | ADR? |
|---|---|---|---|---|---|---|
| Applicant data | Individual -> entrepreneur/legal entity/anonymous | Business/data/workflow | use ApplicantParty abstraction, keep request tied by applicant identity | dynamic form engine in L1 | request creation tests not hardcoded to only profile-first | ADR when persistence strategy chosen |
| Request workflow | Submitted -> InReview -> Approved/Rejected -> Clarification | Business workflow | explicit status transitions, request aggregate owns lifecycle | overgeneric workflow engine in L1 | domain transition tests | ADR if state model changes |
| Notifications | email -> outbox/SMS/internal | Integration/reliability | notification port, outbox later | reliable delivery in L1 if not needed | adapter tests, integration tests | ADR for outbox timing |
| Verification | mock -> external provider | Integration/business | verification port and result model | provider-specific domain dependency | contract tests | ADR for external integration boundary |
| Documents | no docs -> upload/generated docs | Data/integration | separate document area/table, storage port | file storage leaking into domain | upload scenario tests | ADR for storage strategy |
| Auth/security | cookie -> lockout/rate limit/Windows identity | Security/application | current user abstraction, security events later | domain depending on auth mechanism | auth integration tests | ADR for auth model |
| Read models | simple lists -> filters/reporting | Query side | read models/projections | aggregate hydration for UI | query tests | usually no ADR unless strategy changes |

### Change Types

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

### Design Seams

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

## 10. Stage 7 — ADR Protocol

### Purpose

ADRs document significant architecture decisions and their rationale.

They should not replace planning files or diagrams. They record decisions that matter.

### When To Create ADR

Create an ADR when a decision:

```text
changes aggregate boundaries
changes consistency/transaction strategy
introduces or postpones outbox/event-driven behavior
chooses a persistence strategy
chooses a migration strategy
introduces a new context/bounded area
creates a significant port/adapter boundary
affects testing strategy
is hard to reverse
resolves a non-obvious trade-off
```

Do not create ADRs for every small class or DTO.

### ADR Template

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

Planning:
- domain-model.md
- aggregate-design.md
- implementation-plan.md

Diagrams:
- ...
```

### ADR Links

Do not put full ADR content into diagrams.

Use small references:

```text
ADR-004 separate L1DbContext
ADR-005 request review ownership
ADR-006 notification delivery strategy
```

The reasoning belongs in the ADR file.

---

## 11. Implementation Slices

### Walking Skeleton

A walking skeleton is the minimal end-to-end technical path:

```text
HTTP/API
-> application command/query
-> domain operation or simple behavior
-> persistence
-> read result
-> integration test
```

It proves that the skeleton works before all business complexity is added.

### Vertical Slice

A vertical slice implements one useful scenario across layers.

For each slice define:

```text
Scenario ID:
User-facing behavior:
Domain impact:
Application command/query:
Persistence impact:
Read side impact:
Ports/adapters:
Tests:
Changeability notes:
ADR links:
```

### Slice Levels

```text
L1 = final MVP user-facing path
L2 = richer workflow and diploma extensions
L3 = advanced/cross-cutting capabilities
```

L1/L2/L3 are delivery/roadmap metadata, not the main meaning of scenarios.

Scenario diagrams should normally show final target behavior for the selected scenario, with subtle level markers if useful.

Do not generate three separate versions of the same scenario by level unless explicitly requested.

---

## 12. Recommended Planning Files

This method can be supported by:

```text
planning/scenario-spec.md
planning/domain-discovery.md
planning/aggregate-design.md
planning/scenario-to-implementation-map.md
planning/implementation-plan.md
planning/changeability-map.md
planning/adr/
```

### Responsibilities

`scenario-spec.md`

```text
actor/screen/goal
preconditions
main flow
include
extend
invariants
observable outcomes
priority
```

`domain-discovery.md`

```text
scenario lines
business meaning
candidate concepts
rules/state transitions
layer classification
open questions
```

`aggregate-design.md`

```text
aggregate candidates
owned entities
stored references
protected invariants
rejected boundaries
cross-aggregate rules
```

`scenario-to-implementation-map.md`

```text
scenario
aggregate/domain operation
command/query
transaction
persistence
read side
ports
tests
ADR links
```

`implementation-plan.md`

```text
application layer
persistence layer
API layer
test strategy
migration steps
release slices
```

`changeability-map.md`

```text
change candidates
change type
design response
what not to over-engineer yet
tests/guards
ADR links
```

`adr/`

```text
one file per significant architecture decision
```

---

## 13. Example End-To-End Planning Record

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

### Implementation Planning

```text
Command:
CreateConnectionRequestCommand

Handler responsibilities:
- read current client account id;
- load applicant party;
- verify applicant belongs to current client;
- create request through domain factory;
- save request;
- commit transaction.

Query:
GetMyRequestsQuery / GetSubmittedRequestsQuery

Persistence:
L1ClientRequests table with ApplicantPartyId, Status, Details, ObjectAddress_*, CreatedAt.

Tests:
- HTTP happy path;
- missing applicant party;
- applicant belongs to another client;
- empty details;
- too long details;
- request visible in read model.
```

### Changeability Notes

```text
Likely changes:
- applicant types expand from individual to entrepreneur/legal entity;
- request documents may become required;
- status workflow may add clarification;
- anonymous requests may bypass account/applicant path.

Design response:
- do not make ClientAccount own requests;
- keep request tied to applicant identity/reference;
- keep scenario extension points for documents and anonymous request;
- avoid overgeneric workflow engine in L1.
```

### ADR Candidates

```text
ADR: Request aggregate stores ApplicantPartyId rather than being owned by ApplicantParty.
ADR: Applicant data can be provided profile-first or inline during request creation.
ADR later: request status model when clarification/review workflow expands.
```

---

## 14. Final Working Formula

```text
Scenarios say what must happen.

Observable behavior notes say what can be verified from outside.

Domain discovery says which business meanings, rules and lifecycles are behind the scenarios.

Aggregate design says where transactional consistency and invariants live.

Application planning says how use cases are orchestrated.

Persistence planning says how state is stored without defining the domain.

Changeability planning says where changes are expected and which seams protect them.

ADRs say why important decisions were made.
```

Keep these artifacts linked, but not merged into one overloaded diagram.

---

## 15. Current Status

This file captures an intermediate conclusion.

It should guide the next planning step, but it can evolve after real scenarios, domain discovery tables, aggregate diagrams and implementation slices are drafted.

Expected next step:

```text
Create or refine clean scenario specifications first.
Then derive domain discovery notes from them.
Then design aggregates and implementation slices.
```

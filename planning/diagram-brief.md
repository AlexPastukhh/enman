# Diagram Brief

This file is a working brief for generating project diagrams in draw.io / diagrams.net. It is intended for a separate diagram-generation chat or agent.

The architecture/planning chat prepares this brief. The diagram-generation chat must not make new architecture decisions. It should generate editable `.drawio` diagrams from this brief, the diagram rules, and the reference example.

---

# 1. Required companion files

Use this brief together with:

```text
diagram-generation-rules-with-example.md
final-diagrams-example.drawio
```

`diagram-generation-rules-with-example.md` defines the visual and technical generation rules.

`final-diagrams-example.drawio` is the visual/structural reference example.

This file is the content brief.

---

# 2. Main diagram-generation responsibilities

The diagram generator should:

```text
- generate editable draw.io XML;
- follow the accepted C2/C2.2 visual style;
- use C2.4 line-by-line text-shape implementation;
- avoid text overflow;
- avoid overlaps;
- prefer larger canvas and fewer cards over cramped layout;
- keep diagrams manually editable;
- not make architecture decisions;
- not invent new domain concepts.
```

The diagram generator should not:

```text
- change the accepted palette;
- use the rejected dark C2.5 palette;
- use one large HTML label for card body text;
- use floating relation badges by default;
- overfill pages with planning text;
- turn business use-case diagrams into technical Controller/Handler/Repository diagrams;
- reinterpret Level 1 as the current code state.
```

---

# 3. Diagram philosophy

The diagrams are not a full replacement for planning text.

Planning files contain:

```text
- detailed explanations;
- reasons for decisions;
- alternatives;
- future mistakes to avoid;
- implementation notes;
- migration notes.
```

Diagrams should contain:

```text
- compact visual summaries;
- business use-case flow;
- aggregate/class responsibility;
- database shape;
- traceability between use cases, domain classes, application objects and tables.
```

Formula:

```text
Diagram = compact visual summary + traceability
Planning = detailed source of truth
```

---

# 4. Level interpretation

These diagrams must show **final target levels**, not the current code state.

Do not interpret L1 as “what is currently implemented”.

Use this interpretation:

```text
Level 1 = final MVP business slice
Level 2 = extended diploma workflow
Level 3 = advanced / future capabilities
```

## Level 1 — Final MVP business slice

L1 is the complete minimal end-to-end business MVP.

It includes:

```text
- client account;
- employee account;
- individual applicant profile;
- connection request;
- employee review;
- approve / reject decision;
- simple contract draft on approval;
- email notification;
- client viewing request status/result.
```

## Level 2 — Extended diploma workflow

L2 extends L1 with a richer diploma workflow.

It includes:

```text
- applicant types: ФЛ / ИП / ЮЛ;
- extended applicant data;
- applicant snapshot at request submission;
- request documents;
- generated documents;
- clarification flow;
- mock verification;
- contract templates;
- contract draft versions;
- contract acknowledgement;
- richer request statuses.
```

## Level 3 — Advanced / future capabilities

L3 adds advanced system capabilities.

It includes:

```text
- anonymous requests;
- reliable notification delivery / outbox;
- SMS/internal notification channels;
- audit and security events;
- login attempts;
- account lockout;
- rate limiting;
- Windows identity link;
- external verification integration boundary;
- archive lifecycle.
```

---

# 5. Simplification rules

The previous dense traceability test became overcrowded and had overlapping shapes. New diagrams must be simpler.

## Page rule

```text
One page = one task.
```

## Card rule

Each card should normally contain:

```text
3–6 body lines
```

Large cards are allowed, but they must not become mini planning documents.

## If content does not fit

Do this, in order:

```text
1. Increase canvas size.
2. Split the content into another page.
3. Remove secondary details.
4. Move details into planning text or implementation map.
5. Reduce text slightly only as a last resort.
```

Never solve density by:

```text
- allowing overlap;
- shrinking text to unreadable size;
- forcing all lines into tiny cards;
- placing edge labels over body text.
```

Prefer:

```text
- fewer cards per page;
- larger whitespace corridors;
- numbered flows;
- short edge labels;
- wider pages;
- cards arranged in grids;
- minimal crossing edges.
```

Avoid:

```text
- dense arrow webs;
- many-to-many visual spaghetti;
- long paragraphs in cards;
- repeated explanatory notes;
- mixing business and technical layers on the same page.
```

---

# 6. Traceability rules

The diagrams should show how business use cases relate to implementation.

## Use-case IDs

Every use case must have a stable ID.

Format:

```text
UC-L1-01
UC-L2-03
UC-L3-05
```

## Use-case flow node format

A business-flow node should contain:

```text
Business step title

UC id
Command/Query/Event
Main domain class or bounded area
Main table/read model
```

Example:

```text
Submit connection request

UC-L1-04
Command: CreateConnectionRequest
Domain: ConnectionRequest
Writes: ClientRequests
```

Do not include a full handler/repository/DbContext flow inside use-case nodes.

## Aggregate/class card format

Aggregate cards should contain sections like:

```text
[purpose]
[owns]
[stored refs]
[used by]
```

Example:

```text
ConnectionRequest

[purpose]
Connection request + status workflow

[owns]
RequestReview child entity

[stored refs]
ApplicantPartyId

[used by]
UC-L1-04, UC-L1-06, UC-L1-09
```

## DB table card format

DB table cards should contain sections like:

```text
[identity]
[level fields]
[written by]
[read by]
```

Example:

```text
L1ClientRequests

[identity]
PK Id
FK ApplicantPartyId

[L1 core]
Status
Details
ObjectAddress_*
CreatedAt

[written by]
UC-L1-04, UC-L1-06

[read by]
UC-L1-05, UC-L1-09
```

---

# 7. Recommended page package

Generate diagrams in this reduced package:

```text
00 Level Evolution Overview

01 L1 Business Use-case Flow
02 L1 Aggregate Boundaries
03 L1 DB Schema

04 L2 Business Use-case Flow
05 L2 Aggregate Boundaries
06 L2 DB Schema

07 L3 Business Capability Map
08 L3 Bounded Areas
09 L3 DB Additions

A Technical Application Flow / CQRS
```

Optional later pages:

```text
L1 Use-case Implementation Map
L2 Use-case Implementation Map
L3 Use-case Implementation Map
```

Do not generate optional implementation map pages in the first attempt unless explicitly asked.

The first generation attempt should preferably include only:

```text
00 Level Evolution Overview
01 L1 Business Use-case Flow
02 L1 Aggregate Boundaries
```

After layout is approved, generate the remaining pages.

---

# 8. Page 00 — Level Evolution Overview

## Purpose

Show the whole level progression on one page.

## Layout

Use three large horizontal columns:

```text
Level 1 — Final MVP
Level 2 — Extended Workflow
Level 3 — Advanced Capabilities
```

Use one large card per level.

Use simple arrows:

```text
L1 -> L2 -> L3
```

## Level 1 card

Title:

```text
Level 1 — Final MVP
```

Body:

```text
- client + employee accounts
- individual applicant profile
- connection request
- employee review
- approve / reject
- simple contract draft
- email notification
```

## Level 2 card

Title:

```text
Level 2 — Extended Workflow
```

Body:

```text
- ФЛ / ИП / ЮЛ applicant types
- applicant snapshot
- request documents
- clarification flow
- mock verification
- contract templates / versions
- acknowledgement
```

## Level 3 card

Title:

```text
Level 3 — Advanced Capabilities
```

Body:

```text
- anonymous requests
- audit / security
- outbox / reliable notifications
- SMS / internal messages
- rate limiting / lockout
- external integrations
- archive lifecycle
```

## Note

```text
Levels are final target slices, not current implementation snapshots.
```

---

# 9. Page 01 — L1 Business Use-case Flow

## Purpose

Show final MVP business flow with compact implementation hints.

## Actors / lanes

Use large lanes:

```text
Guest
Client
System
Employee
Notification
```

## Use-case nodes

### UC-L1-01 Register client account

```text
Register client account

UC-L1-01
Command: RegisterClientAccount
Domain: ClientAccount
Writes: Accounts
```

### UC-L1-02 Sign in

```text
Sign in

UC-L1-02
Command/Query: SignIn
Domain: Account
Reads: Accounts
```

### UC-L1-03 Create individual applicant profile

```text
Create individual applicant profile

UC-L1-03
Command: CreateIndividualApplicantParty
Domain: IndividualApplicantParty
Writes: ApplicantParties
```

### UC-L1-04 Submit connection request

```text
Submit connection request

UC-L1-04
Command: CreateConnectionRequest
Domain: ConnectionRequest
Writes: ClientRequests
```

### UC-L1-05 Employee views submitted requests

```text
View submitted requests

UC-L1-05
Query: GetSubmittedRequests
DTO: RequestListItemDto
Reads: ClientRequests
```

### UC-L1-06 Approve or reject request

```text
Approve or reject request

UC-L1-06
Command: ReviewRequest
Domain: ConnectionRequest, RequestReview
Writes: Requests, Reviews
```

### UC-L1-07 Create simple contract draft

```text
Create contract draft

UC-L1-07
Command/Event: CreateContractDraft
Domain: ContractDraft
Writes: ContractDrafts
```

### UC-L1-08 Notify client

```text
Notify client

UC-L1-08
Event: RequestReviewed
Area: Notification
Writes: EmailNotifications
```

### UC-L1-09 View request status/result

```text
View request status/result

UC-L1-09
Query: GetMyRequests
DTO: MyRequestDto
Reads: Requests, Drafts
```

## Flow edges

Use short edge labels:

```text
account
auth
profile
submitted
review
if approved
decision
email/result
```

## Layout advice

Use two rows if needed:

```text
Row 1: Register -> Sign in -> Create profile -> Submit request -> View submitted
Row 2: Review -> Create contract draft / Notify -> Client views result
```

Avoid long diagonal crossing arrows.

## Note

```text
Business flow only. Technical Controller/Handler/Repository flow is in appendix.
```

---

# 10. Page 02 — L1 Aggregate Boundaries

## Purpose

Show final MVP aggregate boundaries and which use cases use each aggregate.

## Aggregate boundaries

Use five visual areas:

```text
Account aggregate
ApplicantParty aggregate
ClientRequest aggregate
ContractDraft
Email Notification concern
```

## Account aggregate card

Title:

```text
ClientAccount / EmployeeAccount
```

Sections:

```text
[purpose]
identity, roles, active state
password hash

[used by]
UC-L1-01, UC-L1-02
UC-L1-05, UC-L1-06
```

## ApplicantParty aggregate card

Title:

```text
IndividualApplicantParty
```

Sections:

```text
[purpose]
physical-person applicant profile

[stored refs]
ClientAccountId

[used by]
UC-L1-03, UC-L1-04, UC-L1-09
```

## ClientRequest aggregate card

Title:

```text
ConnectionRequest
```

Sections:

```text
[purpose]
connection request + status workflow

[owns]
RequestReview child entity

[stored refs]
ApplicantPartyId
ReviewerEmployeeAccountId on review

[used by]
UC-L1-04, UC-L1-06, UC-L1-09
```

## ContractDraft card

Title:

```text
Simple ContractDraft
```

Sections:

```text
[purpose]
simple draft after approval

[stored refs]
RequestId
CreatedByEmployeeAccountId

[used by]
UC-L1-07, UC-L1-09
```

## Notification concern card

Title:

```text
EmailNotification
```

Sections:

```text
[purpose]
send approval/rejection/draft email

[refs]
RecipientAccountId
RequestId optional

[used by]
UC-L1-08
```

## Relations

```text
ClientAccount -> IndividualApplicantParty
label: ClientAccountId

IndividualApplicantParty -> ConnectionRequest
label: ApplicantPartyId

EmployeeAccount -> RequestReview
label: reviewer id / actor, not owner

ConnectionRequest -> ContractDraft
label: on approval / RequestId

ConnectionRequest -> EmailNotification
label: decision event
```

## Note

Use one compact note only:

```text
DB FK does not imply aggregate ownership.
Aggregate cards include [used by] traceability.
```

---

# 11. Page 03 — L1 DB Schema

## Purpose

Show minimal L1 storage shape with use-case read/write traceability.

## Tables

### L1Accounts / Account TPH

```text
[identity]
PK Id
Discriminator AccountType

[L1 core]
Email
PasswordHash
Role
IsActive
CreatedAt

[written by]
UC-L1-01

[read by]
UC-L1-02, UC-L1-06, UC-L1-08
```

### L1ApplicantParties / ApplicantParty TPH

```text
[identity]
PK Id
Discriminator ApplicantPartyType
FK ClientAccountId

[L1 individual]
Email
PhoneNumber
FullName_*
CreatedAt

[written by]
UC-L1-03

[read by]
UC-L1-04, UC-L1-09
```

### L1ClientRequests / ClientRequest TPH

```text
[identity]
PK Id
Discriminator RequestType
FK ApplicantPartyId

[L1 request]
Status
Details
ObjectAddress_*
CreatedAt

[written by]
UC-L1-04, UC-L1-06

[read by]
UC-L1-05, UC-L1-09
```

### L1RequestReviews

```text
[identity]
PK Id
FK RequestId
FK EmployeeAccountId

[L1 review]
Decision
Comment
CreatedAt

[written by]
UC-L1-06

[read by]
UC-L1-09
```

### L1ContractDrafts

```text
[identity]
PK Id
FK RequestId
FK CreatedByEmployeeAccountId

[L1 contract]
ContractNumber
Status
Text
CreatedAt
SentAt nullable

[written by]
UC-L1-07

[read by]
UC-L1-09
```

## Relations

```text
Accounts 1 -> many ApplicantParties
ApplicantParties 1 -> many ClientRequests
ClientRequests 1 -> many RequestReviews
Accounts 1 -> many RequestReviews as reviewer
ClientRequests 1 -> many ContractDrafts
```

## Note

```text
Storage FKs do not mean domain ownership.
Domain avoids one-side navigation collections between aggregate roots.
```

---

# 12. Page 04 — L2 Business Use-case Flow

## Purpose

Show extended diploma business workflow.

## Actors / lanes

```text
Client
System
Verification Service
Employee
Contract / Document System
```

## Use-case nodes

### UC-L2-01 Choose applicant type

```text
Choose applicant type

UC-L2-01
App: SelectApplicantType
Domain: ApplicantParty hierarchy
Writes: ApplicantParties
```

### UC-L2-02 Complete extended applicant data

```text
Complete extended data

UC-L2-02
Command: CompleteApplicantData
Domain: Individual / Entrepreneur / LegalEntity
Writes: ApplicantParties
```

### UC-L2-03 Upload request documents

```text
Upload request documents

UC-L2-03
Command: UploadRequestDocument
Domain: RequestDocument
Writes: RequestDocuments
```

### UC-L2-04 Submit request with applicant snapshot

```text
Submit with applicant snapshot

UC-L2-04
Command: SubmitRequestWithSnapshot
Domain: ClientRequest, ApplicantSnapshot
Writes: ClientRequests
```

### UC-L2-05 Run mock verification

```text
Run mock verification

UC-L2-05
Command/Event: RunVerification
Domain: VerificationRequest / Result
Writes: VerificationResults
```

### UC-L2-06 Review documents and verification

```text
Review docs + verification

UC-L2-06
Command: ReviewExtendedRequest
Domain: ClientRequest, RequestReview
Writes: Reviews, Status
```

### UC-L2-07 Request clarification

```text
Request clarification

UC-L2-07
Command: RequestClarification
Domain: ClientRequest
Writes: Status / History
```

### UC-L2-08 Prepare contract from template

```text
Prepare contract from template

UC-L2-08
Command: PrepareContractDraft
Domain: ContractDraft, Template
Writes: ContractDraftVersions
```

### UC-L2-09 Client acknowledges contract draft

```text
Client acknowledges draft

UC-L2-09
Command: AcknowledgeContract
Domain: ContractAcknowledgement
Writes: Acknowledgements
```

## Flow edges

Use short labels:

```text
type selected
data ready
docs attached
submitted
result
if clarification
if approved
draft sent
acknowledged
```

## Note

```text
L2 adds applicant types, documents, verification, clarification and richer contract lifecycle.
```

---

# 13. Page 05 — L2 Aggregate Boundaries

## Purpose

Show how L2 expands the domain model.

## Areas

```text
ApplicantParty aggregate
ClientRequest aggregate
ContractDraft aggregate
Verification area
Notification / Feedback area
```

## ApplicantParty aggregate

```text
ApplicantParty hierarchy

[types]
IndividualApplicantParty
EntrepreneurApplicantParty
LegalEntityApplicantParty

[owns]
PassportData / ActualAddress
Inn / Ogrn / Ogrnip
Registration / LegalAddress

[used by]
UC-L2-01, UC-L2-02, UC-L2-04
```

## ClientRequest aggregate

```text
ClientRequest workflow

[owns]
ApplicantSnapshot
RequestReview
RequestDocument
RequestHistory / Clarification

[responsibility]
documents
richer statuses
verification state

[used by]
UC-L2-03, UC-L2-04, UC-L2-06, UC-L2-07
```

## ContractDraft aggregate

```text
ContractDraft lifecycle

[owns]
ContractDraftVersion
ContractAcknowledgement

[refs]
ContractTemplateId
RequestId

[used by]
UC-L2-08, UC-L2-09
```

## Verification area

```text
Verification model

[entities]
VerificationRequest
VerificationResult
VerificationCheckResult

[used by]
UC-L2-05, UC-L2-06
```

## Notification / Feedback area

```text
Feedback / Notification

[entities]
NotificationMessage
FeedbackTemplate

[used by]
UC-L2-07, UC-L2-06
```

## Relations

```text
ApplicantParty -> ClientRequest
label: snapshot + ApplicantPartyId

ClientRequest -> Verification
label: verification for request

ClientRequest -> ContractDraft
label: approved request

ContractDraft -> ContractTemplate
label: based on template

ClientRequest -> FeedbackTemplate
label: clarification / rejection feedback
```

## Note

```text
ApplicantSnapshot freezes applicant data at submission.
ContractDraft becomes richer at L2 because versions/templates/acknowledgement create lifecycle.
```

---

# 14. Page 06 — L2 DB Schema

## Purpose

Show L2 storage additions over L1.

Use L2 cards only for additions. Do not redraw the entire L1 schema in full.

## Cards

### ApplicantParties L2 additions

```text
[L2 individual]
PassportData_*
ActualAddress_*

[L2 entrepreneur]
Inn
Ogrnip
RegistrationAddress_*

[L2 legal entity]
OrganizationName
Inn
Ogrn
LegalAddress_*

[written by]
UC-L2-01, UC-L2-02

[read by]
UC-L2-04, UC-L2-06
```

### ClientRequests L2 workflow additions

```text
[L2 workflow]
ApplicantSnapshot_*
AssignedEmployeeId
RequestedPowerKw
Expanded Status
ClarificationComment

[written by]
UC-L2-04, UC-L2-06, UC-L2-07

[read by]
UC-L2-06
```

### RequestDocuments / GeneratedDocuments

```text
[identity]
PK Id
FK RequestId

[L2 document]
DocumentType
FileName
StorageKey
UploadedAt / GeneratedAt

[written by]
UC-L2-03, UC-L2-08

[read by]
UC-L2-06
```

### VerificationRequests / Results

```text
[identity]
PK Id
FK RequestId

[L2 verification]
CheckType
Status
ResultPayload
CheckedAt

[written by]
UC-L2-05

[read by]
UC-L2-06
```

### ContractTemplates / Versions / Acknowledgements

```text
[identity]
PK Id
FK ContractDraftId / TemplateId

[L2 contract]
TemplateText
VersionNumber
AcknowledgedAt

[written by]
UC-L2-08, UC-L2-09

[read by]
UC-L2-08, UC-L2-09
```

### FeedbackTemplates

```text
[identity]
PK Id

[L2 feedback]
Name
Text
IsActive

[used by]
UC-L2-07, UC-L2-06
```

---

# 15. Page 07 — L3 Business Capability Map

## Purpose

Show advanced capabilities as a capability map, not a strict MVP sequence.

## Actors / areas

```text
Anonymous Applicant
System
Notification Delivery
Security / Audit
External Service / Admin
```

## Capability nodes

### UC-L3-01 Submit anonymous request

```text
Submit anonymous request

UC-L3-01
Command: SubmitAnonymousRequest
Area: AnonymousApplicantParty
Writes: Anonymous request support
```

### UC-L3-04 Apply rate limit / anti-abuse

```text
Apply rate limit / anti-abuse

UC-L3-04
Policy: RateLimitRule
Area: Security
Writes: SecurityEvents
```

### UC-L3-02 Reliable notification delivery

```text
Reliable notification delivery

UC-L3-02
Event/Worker: Outbox delivery
Area: OutboxMessage
Writes: DeliveryAttempts
```

### UC-L3-03 Record audit/security event

```text
Record audit/security event

UC-L3-03
Event: AuditAction
Area: AuditLogEntry, SecurityEvent
Writes: Audit/Security tables
```

### UC-L3-05 External verification integration

```text
External verification integration

UC-L3-05
Adapter: ExternalVerificationService
Area: ExternalVerificationLog
Writes: ExternalVerificationLogs
```

### UC-L3-06 Archive request

```text
Archive request

UC-L3-06
Command: ArchiveRequest
Domain: ClientRequest archive lifecycle
Writes: Archive fields / Audit
```

## Note

```text
L3 page is a capability map, not one strict MVP sequence.
```

---

# 16. Page 08 — L3 Bounded Areas

## Purpose

Show advanced/future bounded areas.

## Areas

```text
Anonymous Request area
Reliable Notification Delivery
Security / Audit
External Integration
Archive lifecycle
```

## Anonymous Request area

```text
AnonymousApplicantParty

[purpose]
request without account
limited tracking

[used by]
UC-L3-01
```

## Reliable Notification Delivery

```text
Outbox / Delivery

[entities]
OutboxMessage
EmailDeliveryAttempt
SmsNotification
InternalMessage

[used by]
UC-L3-02, UC-L1-08
```

## Security / Audit

```text
Security / Audit

[entities]
LoginAttempt
AccountLock
SecurityEvent
AuditLogEntry
RateLimitRule
WindowsIdentityLink

[used by]
UC-L3-03, UC-L3-04
```

## External Integration

```text
ExternalVerification

[entities]
ExternalVerificationService adapter
ExternalVerificationLog

[used by]
UC-L3-05
```

## Archive lifecycle

```text
Archive

[purpose]
archive completed/obsolete requests
preserve history

[used by]
UC-L3-06
```

## Note

```text
L3 contains advanced bounded areas. Not every box is a core domain aggregate.
```

---

# 17. Page 09 — L3 DB Additions

## Purpose

Show advanced storage additions without redrawing all L1/L2 tables.

## Cards

### Anonymous support

```text
[options]
ApplicantPartyType = Anonymous
ClientAccountId nullable
OR separate AnonymousApplicantParties table

[written by]
UC-L3-01

[read by]
Employee review flows
```

### OutboxMessages

```text
[identity]
PK Id

[L3 delivery]
EventType
Payload
Status
RetryCount
NextRetryAt
ProcessedAt

[written by]
UC-L3-02

[read by]
Delivery worker
```

### NotificationDeliveryAttempts

```text
[identity]
PK Id
FK OutboxMessageId

[L3 delivery]
Channel
Recipient
Status
Error
AttemptedAt

[written by]
UC-L3-02
```

### SecurityEvents / LoginAttempts

```text
[identity]
PK Id
FK AccountId nullable

[L3 security]
EventType
IpAddress
UserAgent
Succeeded
FailureReason
CreatedAt

[written by]
UC-L3-03, UC-L3-04
```

### AuditLogEntries

```text
[identity]
PK Id

[L3 audit]
ActorAccountId
TargetType
TargetId
Action
Payload
CreatedAt

[written by]
UC-L3-03, UC-L3-06
```

### ExternalVerificationLogs

```text
[identity]
PK Id

[L3 integration]
TargetType
TargetId
Provider
RequestPayload
ResponsePayload
Status

[written by]
UC-L3-05
```

### ClientRequests archive additions

```text
[L3 additions]
ArchivedAt
ArchivedByAccountId
ArchiveReason
Status = Archived

[written by]
UC-L3-06

[read by]
Archive/audit views
```

---

# 18. Appendix — Technical Application Flow / CQRS

This page is optional but useful.

It must be clearly marked as an appendix.

Do not mix this technical page with business use-case pages.

## Purpose

Show implementation flow.

## Lanes

```text
HTTP
Controller
Application / MediatR
Domain
Persistence
Database
```

## Command path

```text
HTTP POST
Controller maps DTO -> command
Command Handler loads aggregates
Domain factory/method executes
Repository Add/Update
SaveChanges in handler
SQL INSERT/UPDATE
```

## Query path

```text
HTTP GET
Controller maps query params -> query
Query Handler executes Dapper SQL
Dapper returns read DTO
No aggregate hydration
No SaveChanges
```

## Rules note

```text
Controllers are thin HTTP boundaries.
Command handlers own normal commits.
Queries use Dapper projections.
```

---

# 19. Generation strategy for the diagram chat

Do not ask the diagram chat to generate every page at once in the first attempt.

Recommended sequence:

```text
Attempt 1:
- 00 Level Evolution Overview
- 01 L1 Business Use-case Flow
- 02 L1 Aggregate Boundaries

Review layout.

Attempt 2:
- 03 L1 DB Schema
- 04 L2 Business Use-case Flow
- 05 L2 Aggregate Boundaries

Review layout.

Attempt 3:
- 06 L2 DB Schema
- 07 L3 Business Capability Map
- 08 L3 Bounded Areas
- 09 L3 DB Additions
- Appendix Technical Application Flow / CQRS
```

If a generated page is overcrowded:

```text
- split it;
- widen the canvas;
- reduce content;
- keep the text readable;
- never allow overlap.
```

---

# 20. Prompt for a separate diagram-generation chat

Use this prompt in a new diagram-generation chat.

```text
You are generating editable draw.io diagrams.

Read and follow:
1. diagram-generation-rules-with-example.md
2. final-diagrams-example.drawio
3. diagram-brief.md

Your job is to generate draw.io XML diagrams only from the provided brief.
Do not make architecture decisions.
Do not invent new domain concepts.
Do not use the rejected dark C2.5 palette.
Do not create card body text as one large HTML label.
Each body line must be a separate draw.io text shape with explicit fontSize.
Use attached edge labels, not floating relation badges.

Priority:
1. no overlap;
2. no text overflow;
3. readable text;
4. compact but not cramped layout;
5. editable shapes;
6. style consistent with the reference example.

If content is too dense:
- widen the canvas;
- split the page;
- remove secondary details;
- use fewer edges;
- never shrink text into unreadable size.

Generate only the requested pages, not the whole pack unless explicitly asked.
Start with:
00 Level Evolution Overview
01 L1 Business Use-case Flow
02 L1 Aggregate Boundaries
```

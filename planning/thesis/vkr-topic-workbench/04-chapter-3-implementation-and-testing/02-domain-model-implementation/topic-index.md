# Topic Index — Chapter 3 / Section 3.2 Domain Model Implementation

Status: active / topic index.

## Section purpose

Section 3.2 explains how the model of the subject area is implemented and how it supports the main scenarios of the application.
It should not be a theoretical DDD explanation; it should show how the actual domain objects keep the system state consistent.

## Topic map

| VKR point | Topic file | Main purpose | Evidence to collect before final text |
|---|---|---|---|
| 3.2.1 | `03-02-01-domain-project-purpose-and-scenario-role.topic.md` | why the domain project is described before scenarios | project structure, domain project reference, scenario usage |
| 3.2.2 | `03-02-02-process-participants-account-applicant-employee.topic.md` | account / applicant / employee separation | Account, ClientAccount, Employee, ApplicantParty, ConnectionRequest code |
| 3.2.3 | `03-02-03-connection-request-lifecycle.topic.md` | request as central object and request lifecycle | ConnectionRequest, ApplicantParty link, request state transitions |
| 3.2.4 | `03-02-04-employee-request-review.topic.md` | employee review behavior: start, approve, reject | SC-07B, SL-EMP-REQ-003/004/005, review code/tests |
| 3.2.5 | `03-02-05-agreement-exchange.topic.md` | agreement exchange as separate post-approval model | AgreementProposalExchange code/tests |
| 3.2.6 | `03-02-06-agreement-proposal-versions-and-documents.topic.md` | proposal versions and document references | AgreementProposal, AgreementDocumentRef, ProposalComment code/tests |
| 3.2.7 | `03-02-07-domain-results-errors-and-impossible-states.topic.md` | Result/Failure, domain errors and impossible states | Error, Errors.L1Domain, value object exceptions, result-returning methods |
| 3.2.8 | `03-02-08-domain-rules-verification.topic.md` | domain-level rule tests | domain tests for request, review, exchange and no partial mutation |

## Disclosure flow

```text
domain project purpose
→ process participants
→ request lifecycle
→ employee review
→ agreement exchange
→ proposal versions and documents
→ domain failures / impossible states
→ domain tests
```

## Evidence package for section 3.2

```text
diagram: Account / ClientAccount / Employee / ApplicantParty / ConnectionRequest
diagram: ConnectionRequest lifecycle
diagram: Employee review lifecycle
diagram: AgreementProposalExchange lifecycle
diagram: AgreementProposal composition
table: domain object responsibilities
table: domain methods and protected rules
table: Result vs exception
table: domain behavior → tests
2-4 short code listings
2-4 screenshots of code/tests, optional
```

## Overclaim guardrails

Do not claim:

```text
full EDMS;
electronic signature;
external registry verification;
automatic contract generation;
object storage;
audit trail of every download;
approval automatically creates agreement exchange.
```

Safe claims:

```text
domain model stores request/review/exchange state;
agreement exchange starts after approved request by a separate employee action;
proposal versions contain document references and metadata;
domain methods return structured failures for expected business refusals;
domain tests check state transitions and no partial mutation.
```

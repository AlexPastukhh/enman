# Scenario Text Specifications Index

Status: current / L1 foundation + near-final L2 Employee Review Agreement scenarios synchronized

Current relevant scenarios:

```text
SC-04   Client Request Creation
SC-05   My Requests / Own Request Details
SC-06   Employee Request Dashboard
SC-07A  Employee Request Details
SC-07B  Employee Request Review
SC-10   Applicant Data
SC-10B  Applicant Parties Future Same-Page Management
SC-13A  Client Agreements
SC-13B  Client Agreement Proposal Details / Response
SC-13C  Employee Agreements
SC-13D  Employee Agreement Proposal Create / Send Version
SC-13E  Agreement Final Refusal
SC-14   Agreement Documents
```

## Current L1 applicant decision

```text
many saved ApplicantParties;
one current/default template per applicant type;
current/default is prefill/default only;
request creation uses explicit Existing/New applicant context.
```

## L2 scenario source direction

Primary L2 domain source:

```text
planning/tables/domain-drafts/domain-draft-02.md
```

Current L2 planning status:

```text
planning/l2-current-planning-status.md
```

L2 scenario family:

```text
Employee / employee-side request review;
AgreementProposalExchange;
AgreementProposal versions;
AgreementDocumentRef metadata references;
ProposalComment;
Client accept active proposal;
Employee final refusal;
Request AgreementExchangeFailed result.
```

Naming guardrail:

```text
Use Employee, Start/Started, StartReview, StartByEmployee,
EmployeeSendNewVersion, AwaitingEmployeeResponse.

Do not use Worker, Open/Opened, EmployeeRef, WorkerRef,
StartByWorker or AwaitingWorkerResponse.
```

Agreement terminology guardrail:

```text
Counter-proposal replacement is SupersededByCounterProposal,
not ordinary Rejected.

Rejected is only explicit rejection/decline.
```

SC-14 guardrail:

```text
Current SC-14 is Agreement Documents / AgreementDocumentRef.
Do not use stale SC-14 Client Data Verification wording for current agreement diagrams.
```

## Current L2 slice alignment

```text
SC-06  -> SL-EMP-REQ-001 / L2-EMP-DASH-001.client
SC-07A -> SL-EMP-REQ-002 / L2-EMP-DETAILS-001.client
SC-07B -> SL-EMP-REQ-003..005 / L2-REVIEW-START/APPROVE/REJECT sidecars
SC-13A -> SL-AGR-EXCH-003 / L2-AGR-EXCH-LIST-001.client
SC-13B -> SL-AGR-EXCH-002,005 / send-proposal + accept sidecars
SC-13C -> SL-AGR-EXCH-003,004 / employee list/details views for existing exchanges
SC-13D -> SL-AGR-EXCH-001,002 / start initial proposal + employee counter-proposal
SC-13E -> SL-AGR-EXCH-006 / final refusal sidecar
SC-14  -> SL-DOC-* future document/reference family and AgreementDocumentRef usage in agreement slices
```

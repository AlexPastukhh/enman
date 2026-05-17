# Scenario Text Specifications Index

Status: current / L1 foundation + L2 Employee Review Agreement scenarios synchronized

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

L2 scenario family:

```text
Employee / employee-side request review;
AgreementProposalExchange;
AgreementProposal versions;
AgreementDocumentRef metadata references;
ProposalComment;
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

## L2 validation / cleanup guardrail

```text
Use scenario-local validation sections, scenario clarifications and concrete slice docs.

Do not use old global validation addendum wording as active L2 source of truth
when it contains stale terms such as DocumentFileRef, ReviewerRef, ProposalAttachment or EmployeeRef.
```

Read before L2 review/agreement diagram work:

```text
planning/diagrams/scenario-clarifications/L2-validation-and-agreement-exchange-source-cleanup.md
planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md
planning/slices/README.md
planning/slices/l2/README.md
```

## SC-14 numbering guardrail

```text
Current agreement context:
  SC-14 — Agreement Documents / AgreementDocumentRef.

Old “SC-14 Client Data Verification” wording is stale/deferred unless explicitly reintroduced under another scenario id.
Do not use one SC number for two different stories in diagrams or scenario indexes.
```

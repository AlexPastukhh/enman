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

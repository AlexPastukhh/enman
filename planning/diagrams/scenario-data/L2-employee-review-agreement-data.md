# L2 Employee / Review / Agreement DATA

Status: L2 DATA draft / derived from Domain Draft 02
Doc version: v0.1.0  

DATA files list what actors enter, see, select, filter by, attach or reference. They do not own validation rules.

## Employee dashboard/details data

Employee sees:

```text
request id / display number if available
request type
request status
created at
applicant/request summary
review state marker
review StartedByEmployeeId relationship to current Employee
```

Employee selects:

```text
request to open details
start review action
approve review action
reject review action
```

Employee enters:

```text
optional RejectionFeedback for rejection, if UI supports it
```

## Review domain data

```text
RequestReview.Status
StartedByEmployeeId
StartedAt
CompletedByEmployeeId?
CompletedAt?
RejectionFeedback?
```

## Agreement exchange data

Client/Employee sees:

```text
exchange id / display number if available
request reference
exchange status
active proposal version
proposal version list
proposal author sender
proposal created at
proposal document metadata
proposal comment, if present
final refusal data, if present
```

## Proposal data

```text
AgreementProposalVersion
AgreementProposalAuthor.Sender
AgreementProposalAuthor.SenderId
AgreementProposalState
AgreementDocumentRef
ProposalComment?
CreatedAt
```

## AgreementDocumentRef data

```text
StorageKey / FileId
OriginalFileName
ContentType
SizeBytes
```

## Final refusal data

```text
FinalRefusedByEmployeeId
FinalRefusedAt
FinalRefusalReason?
```

## Do not expose as actor-entered DATA

```text
AgreementProposalVersion chosen by API/client
ActiveProposal DB id
EmployeeRef
ClientRef
AggregateId in author
file bytes inside domain object
storage adapter details inside domain object
```

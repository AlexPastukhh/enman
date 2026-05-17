# L2 Employee / Review / Agreement Behavior Items

Status: L2 behavior draft / derived from Domain Draft 02

## Employee dashboard/details

```text
L2-EMP-DASH-001 — Employee can see review-relevant requests.
L2-EMP-DASH-002 — Dashboard distinguishes no review, review started by current Employee, and review started by another Employee.
L2-EMP-DASH-003 — Employee-facing scenarios use Employee terminology, not Worker terminology.
L2-EMP-DETAIL-001 — Employee request details show review state.
L2-EMP-DETAIL-002 — If another Employee started review, current Employee cannot start/approve/reject it.
L2-EMP-DETAIL-003 — Approve/reject actions require started review.
```

## Request review

```text
L2-REVIEW-START-001 — Employee can start review for InReview request with no active started review.
L2-REVIEW-START-002 — Starting review stores StartedByEmployeeId and StartedAt.
L2-REVIEW-APPROVE-001 — Employee who started review can approve it.
L2-REVIEW-APPROVE-002 — ApproveReview changes Request status to Approved.
L2-REVIEW-REJECT-001 — Employee who started review can reject it.
L2-REVIEW-REJECT-002 — RejectReview changes Request status to Rejected and stores optional RejectionFeedback.
L2-REVIEW-BLOCK-001 — Approve/reject without started review is blocked.
L2-REVIEW-BLOCK-002 — Another Employee cannot start/approve/reject review already started by someone else.
L2-REVIEW-NW-001 — Failed review command does not change request/review state.
L2-REVIEW-OWN-001 — Review is owned by Request and has no repository.
```

## Agreement exchange / proposals

```text
L2-AGR-CLIENT-LIST-001 — Client can see agreement proposal exchanges for their requests.
L2-AGR-CLIENT-LIST-002 — Client sees exchange status and active proposal version summary.
L2-AGR-CLIENT-ACCEPT-001 — Client can accept active employee proposal while exchange is AwaitingClientConfirmation.
L2-AGR-CLIENT-SEND-001 — Client can send own proposal version in response to active employee proposal.
L2-AGR-EMP-LIST-001 — Employee can see approved requests and agreement exchanges requiring employee action.
L2-AGR-EMP-START-001 — Employee can start exchange for Approved request by sending first proposal.
L2-AGR-EMP-START-002 — First proposal version is 1 and exchange status is AwaitingClientConfirmation.
L2-AGR-EMP-NEW-001 — Employee can send new version when exchange is AwaitingEmployeeResponse.
L2-AGR-EMP-NEW-002 — Employee new version supersedes active client proposal and returns exchange to AwaitingClientConfirmation.
L2-AGR-VERSION-001 — API/client does not choose proposal version; exchange assigns next local version.
L2-AGR-SUPERSEDE-001 — Previous proposal is SupersededByCounterProposal, not Rejected, when replaced by counter-proposal.
L2-AGR-AUTHOR-001 — Proposal author uses Sender and SenderId.
L2-AGR-BOUNDARY-001 — AgreementProposalExchange and Request are separate aggregates.
```

## Final refusal

```text
L2-AGR-FINAL-001 — Employee can final-refuse exchange in AwaitingClientConfirmation or AwaitingEmployeeResponse.
L2-AGR-FINAL-002 — Final refusal marks exchange FinallyRefused and records employee/time/reason fields.
L2-AGR-FINAL-003 — Final refusal does not create a new proposal version.
L2-REQ-AGR-FAIL-001 — Application service marks approved request AgreementExchangeFailed after exchange final refusal.
```

## Agreement documents/comments

```text
L2-AGR-DOC-001 — Agreement proposal version requires AgreementDocumentRef.
L2-AGR-DOC-002 — AgreementDocumentRef stores metadata reference only, not bytes/storage adapter.
L2-AGR-COMMENT-001 — ProposalComment is optional; non-empty if provided.
```

## Naming guardrails

```text
L2-NAME-001 — Use Employee, not Worker.
L2-NAME-002 — Use Start/Started, not Open/Opened.
L2-NAME-003 — Use AwaitingEmployeeResponse, not AwaitingWorkerResponse.
L2-NAME-004 — Do not use EmployeeRef/ClientRef/AggregateId for proposal author.
```

# Domain Notes Register

Status: current domain notes register  
Scope: domain modeling notes that should not be lost but are not yet placed into a specific aggregate, value-object or decision file

## 1. Purpose

Use this register for:

```text
possible future aggregate notes;
possible value object extraction notes;
unresolved invariant notes;
implementation-adjacent domain notes;
notes discovered during scenario/domain/slice work.
```

Do not use it as a dumping ground.

If a note clearly belongs to an aggregate, value object or decision, move it there.

## 2. Entry Format

```text
ID:
Status:
Area:
Note:
Source:
Likely owner:
Target file:
Trigger:
Decision needed:
Do when:
Do not do before:
```

## 3. Current Entries

### DN-001 — Align domain workflows after source/version/cascade model

```text
ID: DN-001
Status: future review
Area: source/version/cascade alignment
Note: After source/version/cascade model is introduced, align domain-discovery-workflow.md, aggregate-drafting-workflow.md, value-object-drafting-workflow.md, scenario-to-aggregate-map.md and templates with source metadata conventions.
Source: domain layer conversion planning
Likely owner: planning/domain/ workflows and templates
Target file: planning/planning-maintenance-register.md may track the durable follow-up
Trigger: source/version/cascade model becomes current
Decision needed: exact source metadata fields for aggregate/value-object drafts
Do when: after source/version/cascade pilot stabilizes
Do not do before: before domain scaffold and source/version model are both stable
```

### DN-002 — AgreementProposalExchange pilot extraction

```text
ID: DN-002
Status: extracted
Area: aggregate extraction
Note: AgreementProposalExchange was extracted as the first aggregate pilot with value object draft files for proposal version, proposal author, document reference, proposal comment and final refusal reason.
Source: domain layer conversion planning, SC-13D behavior items, L2 agreement behavior items, Domain Draft 02 and current AgreementProposal implementation sources in archive
Likely owner: planning/domain/aggregates/agreement-proposal-exchange.md
Target file: planning/domain/aggregates/agreement-proposal-exchange.md
Trigger: completed by first aggregate extraction batch
Decision needed: none for scaffold; scenario open questions remain in scenario-to-aggregate-map.md
Do when: done
Do not do before: n/a
```

### DN-003 — Resolve SC-13D proposal comment and supersede wording questions

```text
ID: DN-003
Status: open
Area: scenario/domain source synchronization
Note: The pilot extraction follows current domain direction: proposal replacement uses SupersededByCounterProposal, and ProposalComment is optional but non-empty when present. Scenario questions Q-SC-13D-001 and Q-SC-13D-002 should be resolved/synchronized later.
Source: planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md
Likely owner: scenario layer + domain layer
Target file: planning/diagrams/scenario-questions-register.md and planning/domain/scenario-to-aggregate-map.md
Trigger: next scenario/domain sync pass
Decision needed: close or update the two SC-13D questions
Do when: before treating SC-13D as fully synchronized
Do not do before: before reviewing current scenario question register
```

### DN-004 — ConnectionRequest extraction and ApplicantParty snapshot question

```text
ID: DN-004
Status: extracted / open follow-up
Area: aggregate extraction / applicant relation
Note: ConnectionRequest was extracted as the second aggregate. It currently stores ApplicantPartyId/ClientAccountId and does not own ApplicantParty creation or immutable applicant snapshot data. Whether request details should include immutable applicant snapshot data remains open.
Source: SC-04 request creation sources, SC-07B request review sources, Domain Draft 02, current Request implementation sources
Likely owner: planning/domain/aggregates/connection-request.md and future ApplicantParty aggregate draft
Target file: planning/domain/aggregates/connection-request.md; future planning/domain/aggregates/applicant-party.md
Trigger: ApplicantParty extraction or request details/read-model review
Decision needed: ApplicantParty reference vs request-local immutable snapshot policy
Do when: before treating request read/detail model as fully synchronized
Do not do before: before ApplicantParty extraction/source review
```

### DN-005 — AgreementExchangeFailed metadata follow-up

```text
ID: DN-005
Status: open
Area: cross-aggregate coordination
Note: ConnectionRequest can be marked AgreementExchangeFailed after final refusal coordination, but current extracted Request draft treats it as status-only. Future review should decide whether to store AgreementProposalExchangeId and failed-at metadata as a value object or child record.
Source: Domain Draft 02, current ConnectionRequest.MarkAgreementExchangeFailed implementation, AgreementProposalExchange aggregate draft
Likely owner: ConnectionRequest aggregate draft and possible future value object
Target file: planning/domain/aggregates/connection-request.md
Trigger: agreement final refusal implementation/status audit or domain-model-overview creation
Decision needed: status-only vs metadata-bearing failure reference
Do when: before finalizing domain overview for request/exchange relationship
Do not do before: before final refusal scenario/application flow is reviewed
```

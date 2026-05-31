# L2 Validation And Agreement Exchange Source Cleanup

Status: accepted clarification / source-of-truth cleanup guardrail  
Doc version: v0.1.0  
Scope: L2 Employee Review and AgreementProposalExchange scenario/spec/diagram wording

## 1. Purpose

This clarification prevents diagram and documentation agents from using stale global validation wording as an active L2 source of truth.

It should be read before producing L2 review/agreement scenario docs, slice docs or diagrams.

## 2. Deprecated Global Validation Addendum

Old global validation addendum wording such as the following is not active L2 guidance:

```text
DocumentFileRef
ReviewerRef
AgreementDocument
ProposalAttachment
EmployeeRef
```

Current L2 wording is:

```text
AgreementDocumentRef
Employee
AgreementProposal.Author.Sender
AgreementProposal.Author.SenderId
SupersededByCounterProposal
AgreementProposalExchange.ClientAccountId
```

If `planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md` still exists in a local checkout, it must not be used as an authoritative L2 source. Prefer deleting it from active indexes or moving it to a deprecated/historical location.

## 3. Validation Placement

Validation/domain constraints should live in scenario-local sections and concrete slice docs.

Use this split:

```text
DTO / FluentValidation:
- request/query/body shape;
- required fields;
- enum parsing;
- ids;
- string length;
- blank string policy;
- document reference field shape.

Application/service:
- resolve current session role/account id;
- load ClientAccount/Employee/Request/AgreementProposalExchange;
- create value objects;
- check existence/duplicate exchange when orchestration requires it;
- call domain method;
- save.

Domain:
- client ownership through ClientAccountId;
- current exchange status;
- whose turn;
- active proposal author;
- accepted/finally refused cannot continue;
- proposal versioning;
- SupersededByCounterProposal replacement;
- Request and AgreementProposalExchange lifecycle transitions.
```

Do not put ownership/lifecycle/turn logic under FluentValidation.

## 4. Accepted L2 Decisions

### RejectReview feedback

```text
RejectReview feedback is optional in the current accepted direction.
Missing/null/blank feedback does not block reject unless endpoint/domain is explicitly changed later.
```

### Proposal replacement

```text
Counter-proposal replacement is not ordinary Rejected.
Use SupersededByCounterProposal or “superseded/replaced by counterproposal”.
Rejected is only explicit rejection/decline.
```

### Agreement exchange participant / ownership

```text
AgreementProposalExchange stores ClientAccountId.
Client actions check client.Id == exchange.ClientAccountId.
Do not add ResponsibleEmployeeId as authorization guard first pass.
Any active Employee can service the same exchange.
Employee identity is tracked per proposal version through Sender/SenderId.
```

### Employee identity

```text
Account -> ClientAccount / Employee.
TPH in L1Accounts.
Employee : Account.
Account.Id == Employee.Id for Employee sessions.
No EmployeeProfile(AccountId).
No EmployeeRef.
```

### Command result mapping

```text
Do not add per-command status enums as current direction.
Handlers/services return existing Result/UnitResult + Error model.
Success -> 204 No Content.
Failure -> existing ProblemDetails/Error mapper.
AgreementExchangeStatus is persisted domain state, not command execution result.
```

## 5. Agreement Exchange Slice Numbering

Canonical numbering:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

If older docs still say:

```text
003 Read
004 Accept
005 Final Refuse
```

update them to the canonical list/details split.

## 6. SC-14 Naming Conflict

Current agreement context uses:

```text
SC-14 — Agreement Documents / AgreementDocumentRef
```

Old “SC-14 Client Data Verification” wording is stale/deferred unless explicitly reintroduced under another scenario id.

Diagram/documentation agents must not use one SC number for two different stories.

## 7. Route Guardrails

Current accepted route direction:

```text
GET /api/agreement-exchanges
POST /api/requests/{requestId}/agreement-exchange/proposals
```

Do not document:

```text
POST /api/agreement-exchanges/{requestId}/proposals
```

because `agreement-exchanges/{...}` reads as `exchangeId`, while this command is request-scoped by `requestId`.

## 8. Diagram Handling

Diagram prompts must tell Diagram Chat to check:

```text
scenario docs
scenario questions register
scenario clarifications
slice questions/register docs
canonical slice README
```

Do not rely only on the old scenario questions register or old global validation addendum for L2.

If a stale term is found in a source:

```text
1. prefer accepted clarification;
2. mark diagram note/question if still ambiguous;
3. do not silently freeze stale wording into draw.io XML.
```

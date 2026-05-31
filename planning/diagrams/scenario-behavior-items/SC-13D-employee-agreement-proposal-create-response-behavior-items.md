# SC-13D — Employee Agreement Proposal Create / Send Version Behavior Items

Status: migrated v1  
Doc version: v0.1.0  
Source scenario: `SC-13D`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-13D-...
planning/diagrams/scenario-data/SC-13D-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| AGR-CMD-EMP-SEND-001 | CMD | Employee sends first agreement proposal | SC-13D | Migrated from pre-domain baseline 5.9. |
| AGR-CMD-EMP-NEW-001 | CMD | Employee sends new version after client proposal | SC-13D | Migrated from pre-domain baseline 5.12; legacy baseline used Rejected, current decision prefers SupersededByCounterProposal/Replaced semantics. |
| AGR-LC-001 | LC | Employee first proposal creates awaiting state | SC-13D | Migrated from pre-domain baseline 6.2. |
| AGR-LC-007 | LC | Employee new version supersedes previous client version | SC-13D | Migrated from legacy baseline AGR-LC-007; corrected away from Rejected wording to SupersededByCounterProposal/Replaced semantics. |
| AGR-IBS-001 | IBS | Proposal has sender | SC-13A..SC-13D | Owning scenario SC-13D; related to agreement reads/responses. |
| AGR-IBS-002 | IBS | Proposal has document/file | SC-13B / SC-13D | Owning scenario SC-13D because creation occurs there; related SC-13B for client response. |
| AGR-VI-001 | VI | Agreement document reference | SC-13B / SC-13D-DATA | Owning scenario SC-13D; related SC-13B for client version. |
| AGR-VI-002 | VI | Proposal text details/comment | SC-13B / SC-13D-DATA | Owning scenario SC-13D; related SC-13B. |
| AGR-UCQ-001 | UCQ | Proposal creation requires Approved request | SC-13D / SC-07B | Migrated from pre-domain baseline 9.2. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### AGR-CMD-EMP-SEND-001 — Employee sends first agreement proposal

Source: `SC-13D`

Required behavior / guarantee:

```text
Employee can start agreement proposal exchange only for an Approved request by sending an agreement document/file with text details/comment.
```

Failure / no-write / preservation guarantee:

```text
If request is not Approved or proposal input is invalid, no agreement proposal is created and request status remains unchanged.
```

Migration note:

```text
Migrated from pre-domain baseline 5.9.
```

#### AGR-CMD-EMP-NEW-001 — Employee sends new version after client proposal

Source: `SC-13D`

Required behavior / guarantee:

```text
Employee can respond to a client-sent proposal version by sending a new employee proposal version; previous client proposal is superseded/replaced by counter-proposal according to current domain decision.
```

Failure / no-write / preservation guarantee:

```text
If previous proposal is not client-sent/SentByClient or new input is invalid, no new proposal is created and previous proposal state remains unchanged.
```

Migration note:

```text
Migrated from pre-domain baseline 5.12; legacy baseline used Rejected, current decision prefers SupersededByCounterProposal/Replaced semantics.
```

### 4.LC — Scenario State / Condition Matrices

#### AGR-LC-001 — Employee first proposal creates awaiting state

Source: `SC-13D`

Required behavior / guarantee:

```text
First employee proposal starts exchange and awaits client confirmation.
```

Failure / no-write / preservation guarantee:

```text
No proposal created if request is not Approved or input invalid.
```

Migration note:

```text
Migrated from pre-domain baseline 6.2.
```

#### AGR-LC-007 — Employee new version supersedes previous client version

Source: `SC-13D`

Required behavior / guarantee:

```text
Employee new version after client proposal supersedes/replaces previous client proposal and creates new employee AwaitingClientConfirmation version.
```

Failure / no-write / preservation guarantee:

```text
Invalid previous state/input leaves previous proposal unchanged and no new proposal.
```

Migration note:

```text
Migrated from legacy baseline AGR-LC-007; corrected away from Rejected wording to SupersededByCounterProposal/Replaced semantics.
```

### 4.IBS — Impossible Business State Candidates

#### AGR-IBS-001 — Proposal has sender

Source: `SC-13A..SC-13D`

Required behavior / guarantee:

```text
Agreement proposal without sender must not exist.
```

Failure / no-write / preservation guarantee:

```text
Invalid proposal input creates no proposal.
```

Migration note:

```text
Owning scenario SC-13D; related to agreement reads/responses.
```

#### AGR-IBS-002 — Proposal has document/file

Source: `SC-13B / SC-13D`

Required behavior / guarantee:

```text
Agreement proposal without attached document/file must not exist.
```

Failure / no-write / preservation guarantee:

```text
Missing document creates no proposal.
```

Migration note:

```text
Owning scenario SC-13D because creation occurs there; related SC-13B for client response.
```

### 4.VI — Value Integrity Items

#### AGR-VI-001 — Agreement document reference

Source: `SC-13B / SC-13D-DATA`

Required behavior / guarantee:

```text
Proposal submission requires accepted agreement document/file reference.
```

Failure / no-write / preservation guarantee:

```text
Invalid/missing document reference prevents proposal creation.
```

Migration note:

```text
Owning scenario SC-13D; related SC-13B for client version.
```

#### AGR-VI-002 — Proposal text details/comment

Source: `SC-13B / SC-13D-DATA`

Required behavior / guarantee:

```text
Proposal submission includes text details/comment if required by core scenario.
```

Failure / no-write / preservation guarantee:

```text
Invalid/missing details behave according to agreed requirement.
```

Migration note:

```text
Owning scenario SC-13D; related SC-13B.
```

### 4.UCQ — Use-Case Coordination Items

#### AGR-UCQ-001 — Proposal creation requires Approved request

Source: `SC-13D / SC-07B`

Required behavior / guarantee:

```text
Employee can start agreement proposal exchange only for Approved request.
```

Failure / no-write / preservation guarantee:

```text
Proposal is not created for non-Approved request; request status unchanged.
```

Migration note:

```text
Migrated from pre-domain baseline 9.2.
```

## 5. Cross-Scenario / Related Items

| Item ID | Owning scenario | Category | Short name | Why related |
|---|---|---|---|---|
| AGR-IBS-003 | SC-13B | IBS | Client does not start exchange | Source mentions `SC-13B / SC-13D` |
| AGR-UCQ-002 | SC-07B | UCQ | Approval enables but does not create proposal | Source mentions `SC-07B / SC-13D` |
| FILE-INT-001 | SC-11 | INT | File/blob storage is infrastructure | Source mentions `SC-11 / SC-13B / SC-13D` |

## 6. Scenario Questions Raised

| Question ID | Affected item(s) | Question | Status |
|---|---|---|---|
| Q-SC-13D-001 | AGR-LC-007 / AGR-CMD-EMP-NEW-001 | Should replaced proposal version be named Superseded/Replaced instead of Rejected? | open |
| Q-SC-13D-002 | AGR-VI-002 | Are proposal text details/comment required or optional? | open |

See `planning/diagrams/scenario-questions-register.md` for full details.

## 7. Downstream Use

This file should be checked when creating or updating:

```text
domain drafts
slice boundary drafts
parent vertical slice files
.client.md sidecars
unit/integration/client/E2E test plans
```

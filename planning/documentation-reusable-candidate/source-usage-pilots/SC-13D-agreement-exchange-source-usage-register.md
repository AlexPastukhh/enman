# SC-13D Agreement Exchange Source Usage Register

Status: pilot skeleton / not audited  
Scope: SC-13D -> AgreementProposalExchange -> SL-AGR-EXCH-001 source usage pilot

## 1. Purpose

This file is the first source usage register pilot for the SC-13D agreement exchange chain.

It is intentionally not fully filled yet. Fill it only after reading the real current source files for the pilot chain.

## 2. Pilot Chain

```text
SC-13D scenario text / DATA / behavior items
  -> scenario artifact/currentness map
  -> scenario-to-aggregate map
  -> AgreementProposalExchange aggregate draft
  -> related value object drafts
  -> SL-AGR-EXCH-001 server slice draft
  -> slice Behavior Coverage and Test / Verification Plan sections
```

## 3. Source Usage Rows

Rows are intentionally not filled yet.

Use this shape during the real pilot fill pass:

| ID | Source artifact | Source scope | Source status | Consumer artifact | Consumer scope | Usage type | Reviewed against | Sync status | Review outcome | Cascade trigger | Notes |
|---|---|---|---|---|---|---|---|---|---|---|---|

## 4. Fill Rules

```text
- Add rows only after reading the source and consumer artifacts.
- Prefer precise source/consumer scopes over whole-file rows when sections are independently reviewable.
- Use unknown/blocked when evidence is missing.
- Do not invent source versions.
- Use reviewed_against markers that can actually be checked.
- Do not copy upstream source truth into this register.
```

## 5. Candidate Sources To Read During Fill

```text
planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md
planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md
planning/diagrams/scenario-data/SC-13D-employee-agreement-proposal-create-response-data.md
planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md
planning/diagrams/scenario-artifact-map.md
planning/domain/scenario-to-aggregate-map.md
planning/domain/aggregates/agreement-proposal-exchange.md
planning/domain/value-objects/
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
```

Confirm current paths during the fill pass before adding rows.

## 6. Not Checked Yet

```text
- Current SC-13D source file content.
- Current scenario-artifact-map row.
- Current scenario-to-aggregate-map row.
- Current value object draft links.
- Current SL-AGR-EXCH-001 source sync status.
- Current implementation/code/tests.
```

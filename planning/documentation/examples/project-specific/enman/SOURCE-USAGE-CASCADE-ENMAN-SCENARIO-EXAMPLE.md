# Source Usage Cascade Enman Scenario Example

Status: active Enman-specific scenario/application-architecture example / demonstration only  
Scope: Enman-specific source usage/cascade pilot example that demonstrates how a reusable field kit can be instantiated for one scenario-driven application architecture

## 1. Purpose

This example preserves the concrete Enman source usage pilot candidate that existed before F5 split the source-usage governance material into a reusable field kit and project profile.

This example is intentionally Enman-specific. It is kept inside the reusable documentation layer only as a worked example for scenario-driven application architecture documentation. It is not reusable rule logic, not an active Enman register, and not portable project configuration.

It demonstrates how the generic source usage model can be instantiated for a scenario/domain/slice project.

Kit owner:

```text
planning/documentation/field-kits/source-usage-cascade-field-kit.md
```

Enman profile:

```text
planning/source-usage-cascade-profile.md
```

This is an example only. It is not an active Enman register and does not switch canonical documentation ownership.

## 2. Preserved Pilot Candidate

Pilot candidate:

```text
SC-13D / AgreementProposalExchange source usage pilot
```

Earlier skeleton register path:

```text
planning/source-usage-pilots/SC-13D-agreement-exchange-source-usage-register.md
```

F5 preserved this as project-specific example knowledge. A later cleanup/export batch should decide whether to keep this project-specific example in Enman only or omit it from a portable starter-kit export.

## 3. Target Chain

```text
SC-13D scenario text / DATA / behavior items
  -> scenario artifact/currentness map
  -> scenario-to-aggregate map
  -> AgreementProposalExchange aggregate draft
  -> related value object drafts
  -> SL-AGR-EXCH-001 server slice draft
  -> slice Behavior Coverage and Test / Verification Plan sections
```

## 4. Preserved Example Source Usage Rows

| source_id | source_scope | source_status | consumer_id | consumer_scope | reviewed_against | sync_status | review_outcome | notes |
|---|---|---|---|---|---|---|---|---|
| SC-13D scenario source | scenario text / DATA / behavior items | candidate | scenario artifact/currentness map | currentness/source mapping | not reviewed in F5 | needs-review | pilot-candidate | preserve candidate chain only |
| scenario-to-aggregate map | AgreementProposalExchange mapping | candidate | aggregate draft | aggregate behavior and methods | not reviewed in F5 | needs-review | pilot-candidate | downstream domain consumer |
| AgreementProposalExchange aggregate draft | methods / invariants / lifecycle | candidate | SL-AGR-EXCH-001 server slice draft | Behavior Coverage and Test / Verification Plan | not reviewed in F5 | needs-review | pilot-candidate | downstream slice consumer |

## 5. What A Later Pilot Should Prove

A later pilot should verify:

```text
- whether these source and consumer scopes are granular enough;
- whether the row shape is too heavy or useful;
- whether metadata-only review is understandable;
- whether downstream affected scopes can be found without broad rereading;
- whether this pilot should become an active Enman source usage register.
```

## 6. Do Not

```text
- Do not treat this example as a filled active register.
- Do not claim the SC-13D chain is current without reading the active source files.
- Do not use this example as universal reusable logic.
- Do not delete the preserved pilot knowledge without an explicit cleanup decision.
```

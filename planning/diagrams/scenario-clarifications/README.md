# Scenario Clarifications Index

Status: current scenario clarification index / diagram and source cleanup guardrails  
Doc version: v0.1.0  
Scope: scenario conflicts, terminology guards and non-blocking questions that must be checked before downstream domain/slice/diagram work

## 1. Purpose

This folder tracks scenario-level clarifications that should be resolved or explicitly marked before downstream work.

Use it when:

```text
- a scenario/specification conflict may produce a wrong diagram or draft;
- terminology has changed but older scenario files may still contain old wording;
- a diagram prompt must not silently choose the wrong interpretation;
- a question is about scenario meaning, not implementation details.
```

## 2. Current Clarifications

| File | Topic | Impact |
|---|---|---|
| `AGR-001-agreement-proposal-replacement-terminology.md` | Agreement proposal replacement must not be drawn as ordinary rejection | lifecycle/state diagrams, SC-13B/SC-13D, AgreementProposalExchange docs |
| `L2-employee-review-agreement-domain-direction.md` | L2 employee review/agreement domain direction | domain aggregate extraction, scenario-to-aggregate mapping, agreement diagrams |
| `L2-validation-and-agreement-exchange-source-cleanup.md` | L2 validation source cleanup, stale global validation addendum guardrail, current agreement ownership/numbering/route decisions | L2 review/agreement diagrams, scenario cleanup, diagram prompts |
| `L2-agreement-scenario-slice-followup-cleanup.md` | Agreement scenario/slice follow-up cleanup | scenario-to-slice alignment and future scenario cleanup |
| `diagram-generation-readiness-guardrails.md` | General guardrails before generating diagrams | all diagrams |
| `scenario-spec-pre-diagram-review.md` | Scenario/spec pre-diagram review notes | diagram readiness and scenario cleanup |

## 3. Rule

If a scenario text file and a clarification file disagree, downstream work must not silently pick the scenario text.

Instead:

```text
1. use the clarification as the current assumption if it has an accepted direction;
2. mark the downstream artifact note with the assumption;
3. create/update scenario cleanup work item for later text-spec correction.
```

## 4. Not A Replacement For Scenario Specs

Clarification files are temporary guardrails.

They do not replace scenario text specs. Once the scenario specs are corrected, keep only a short historical note or remove the clarification from current read order.

## 5. Related Maps

```text
planning/diagrams/scenario-responsibility-map.md
planning/diagrams/scenario-artifact-map.md
planning/diagrams/scenario-questions-register.md
```

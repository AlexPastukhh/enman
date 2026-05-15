# Diagram Generation Readiness Guardrails

Status: current guardrail note  
Scope: what to check before generating diagrams from scenario specifications

## 1. Purpose

Before generating diagrams, check scenario specifications for known conflicts and ambiguity.

Do not let diagrams silently freeze an outdated wording into a visual model.

## 2. Required Pre-Diagram Checks

Before drawing scenario/lifecycle/sequence diagrams:

```text
1. Read the scenario text spec.
2. Read related DATA / validation / security addenda.
3. Read related behavior items, if they exist.
4. Read scenario clarifications in planning/diagrams/scenario-clarifications/.
5. Check whether terminology in the source file conflicts with a clarification.
6. If conflict exists, use the accepted clarification direction or mark the conflict explicitly.
```

## 3. Agreement Proposal Guard

For agreement proposal diagrams:

```text
- do not draw replacement/counterproposal as ordinary Rejected;
- use superseded/replaced by counterproposal;
- use SupersededByCounterProposal if a state name is needed;
- reserve Rejected for explicit rejection/decline.
```

Primary clarification:

```text
planning/diagrams/scenario-clarifications/AGR-001-agreement-proposal-replacement-terminology.md
```

## 4. Diagram Output Notes

If a diagram depends on a clarification, include a short note near the diagram source or generated diagram text:

```text
Note: proposal replacement is modeled as superseded/replaced by counterproposal, not Rejected.
```

## 5. Scenario Cleanup Is Separate

Diagram guardrails do not replace proper source spec cleanup.

If a source scenario is outdated:

```text
- generate diagrams using the current accepted clarification;
- add/update cleanup item for scenario spec correction;
- do not silently rewrite unrelated scenario behavior during diagram work.
```

## 6. Recommended Diagram Prompt Header

```text
Before drawing, check planning/diagrams/scenario-clarifications/.
If a source scenario conflicts with a clarification file, do not silently choose the old scenario wording.
Use accepted clarification direction and mark the assumption.
```

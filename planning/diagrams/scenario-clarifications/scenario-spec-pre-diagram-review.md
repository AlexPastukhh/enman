# Scenario Specification Pre-Diagram Review

Status: review note for later update  
Scope: known scenario clarification work before diagram generation

## 1. Current Review Result

The main known scenario clarification needed before diagram generation is agreement proposal replacement terminology.

Known conflict:

```text
old wording:
previous client-sent agreement proposal becomes Rejected when employee sends another proposal

current direction:
previous client-sent agreement proposal is superseded/replaced by employee counterproposal
```

## 2. Diagram Risk

If not guarded, lifecycle diagrams may incorrectly show:

```text
ClientSentProposal -> Rejected
```

This would incorrectly merge two different domain meanings:

```text
explicit rejection
replacement by counterproposal
```

## 3. Required Action Before Agreement Proposal Diagrams

Use:

```text
planning/diagrams/scenario-clarifications/AGR-001-agreement-proposal-replacement-terminology.md
```

Then either:

```text
- update SC-13B/SC-13D/source specs before diagram generation;
```

or:

```text
- generate diagrams with the clarification note and create a later source-spec cleanup item.
```

## 4. Non-Blocking For Unrelated Diagrams

This clarification blocks only diagrams that involve agreement proposal lifecycle/replacement.

It does not block diagrams for current L1 account/applicant/request creation flows unless those diagrams depend on agreement proposal states.

## 5. Suggested Later Source Updates

When doing the actual scenario-spec cleanup archive, update:

```text
- scenario index / master scenario navigation;
- SC-13B agreement proposal response spec;
- SC-13D agreement proposal create/send spec;
- agreement proposal behavior items;
- agreement proposal lifecycle/domain notes;
- diagram prompts mentioning agreement lifecycle.
```

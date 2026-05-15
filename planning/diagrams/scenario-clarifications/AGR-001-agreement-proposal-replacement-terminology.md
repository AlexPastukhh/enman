# AGR-001 — Agreement Proposal Replacement Terminology

Status: accepted clarification / source-cleanup guardrail  
Scope: SC-13B / SC-13D agreement proposal lifecycle and diagrams

## 1. Problem

Older scenario/index text described agreement proposal replacement as:

```text
previous client-sent proposal becomes Rejected
```

This is not the current accepted meaning for replacement by counterproposal.

## 2. Current Accepted Direction

When an employee sends a counterproposal that replaces a previous client-sent proposal, the previous proposal should be treated as:

```text
SupersededByCounterProposal
```

or in cleaner diploma/scenario wording:

```text
superseded/replaced by the employee counterproposal
замещён встречным вариантом сотрудника
```

It must not be treated as ordinary `Rejected`.

## 3. Meaning Difference

`Rejected` means explicit rejection/decline.

`SupersededByCounterProposal` means the proposal is no longer current because another party sent a replacing counterproposal.

These are not the same lifecycle transition.

## 4. Diagram Guard

Diagram generation must not draw proposal replacement as:

```text
ClientSentProposal -> Rejected
```

Use:

```text
ClientSentProposal -> SupersededByCounterProposal
```

or label the transition:

```text
superseded by employee counterproposal
replaced by employee counterproposal
замещён встречным вариантом сотрудника
```

## 5. Source Summary Cleanup

The current source-summary direction is:

```text
- scenario text/DATA summaries should use superseded/replaced wording for replacement;
- Rejected should remain only for explicit rejection/decline;
- diagram generation should not treat old Rejected-for-replacement wording as a valid source of truth.
```

If any remaining scenario text file, DATA file, behavior item, lifecycle note or old prompt still says `Rejected` for replacement/counterproposal:

```text
- treat that as stale wording;
- do not silently draw it as Rejected;
- use the accepted superseded/replaced direction;
- add a cleanup item for that specific source file.
```

## 6. Open / Future Questions

| ID | Question | Current assumption | Status |
|---|---|---|---|
| Q-AGR-LC-001 | Does `SupersededByCounterProposal` apply only when employee replaces a client-sent proposal, or symmetrically to any proposal replaced by the opposite party's counterproposal? | For now it definitely applies to a client-sent proposal replaced by an employee counterproposal. Symmetry can be decided later. | open |
| Q-AGR-LC-002 | Should final domain enum name use `SupersededByCounterProposal`, `Superseded`, or another term? | Use `SupersededByCounterProposal` for precise technical planning, and “superseded/replaced by counterproposal” in diagrams/diploma text. | future review |
| Q-AGR-LC-003 | Should `Rejected` remain available for explicit decline of an agreement proposal? | Yes. Rejected is still valid for explicit rejection, not replacement. | accepted assumption |

## 7. Source Cleanup Targets

Current cleanup should cover source-summary files used by diagram preflight:

```text
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-questions-register.md
```

Later cleanup should still check detailed sources when agreement implementation planning starts:

```text
SC-13B agreement proposal response spec
SC-13D agreement proposal create/send version spec
agreement proposal behavior items
agreement proposal lifecycle notes
domain draft notes if they contain Rejected for replacement
old diagram prompts that mention agreement proposal lifecycle
```

## 8. Prompt Snippet For Diagram Chat

```text
Do not draw agreement proposal replacement as Rejected.

If SC-13B/SC-13D or an older source still says Rejected for a proposal replaced by a counterproposal,
treat it as stale wording unless a newer explicit decision says otherwise.

Use:
- superseded/replaced by counterproposal
- SupersededByCounterProposal, if a domain state is needed

Rejected is only for explicit rejection, not replacement.
```

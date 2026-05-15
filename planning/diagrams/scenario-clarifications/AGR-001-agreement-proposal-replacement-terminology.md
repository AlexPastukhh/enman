# AGR-001 — Agreement Proposal Replacement Terminology

Status: current clarification before diagram generation  
Scope: SC-13B / SC-13D agreement proposal lifecycle and diagrams

## 1. Problem

Some scenario/index text may still describe agreement proposal replacement as:

```text
previous client-sent proposal becomes Rejected
```

This is not the current preferred meaning.

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

## 5. If Source Specs Still Say Rejected

If scenario index, SC-13B, SC-13D, behavior items or older lifecycle notes still say `Rejected` for replacement/counterproposal:

```text
- treat that as a known documentation conflict;
- do not silently draw it as Rejected;
- add a note to the diagram prompt/output;
- keep a cleanup item to update the source spec later.
```

## 6. Open Question

| ID | Question | Current assumption | Status |
|---|---|---|---|
| Q-AGR-LC-001 | Does `SupersededByCounterProposal` apply only when employee replaces a client-sent proposal, or symmetrically to any proposal replaced by the opposite party's counterproposal? | For now it definitely applies to a client-sent proposal replaced by an employee counterproposal. Symmetry can be decided later. | open |
| Q-AGR-LC-002 | Should domain enum name use `SupersededByCounterProposal`, `Superseded`, or another final term? | Use `SupersededByCounterProposal` for precise technical planning, and “superseded/replaced by counterproposal” in diagrams/diploma text. | open |
| Q-AGR-LC-003 | Should `Rejected` remain available for explicit decline of an agreement proposal? | Yes. Rejected is still valid for explicit rejection, not replacement. | accepted assumption |

## 7. Source Cleanup Targets

Later cleanup should check and update:

```text
scenario index / master scenario navigation
SC-13B agreement proposal response spec
SC-13D agreement proposal create/send version spec
agreement proposal behavior items
agreement proposal lifecycle notes
diagram prompts that mention agreement proposal lifecycle
domain draft notes if they contain Rejected for replacement
```

## 8. Prompt Snippet For Diagram Chat

```text
Do not draw agreement proposal replacement as Rejected.

If SC-13B/SC-13D or scenario index still says Rejected for a proposal replaced by a counterproposal,
treat it as a known documentation conflict.

Use:
- superseded/replaced by counterproposal
- SupersededByCounterProposal, if a domain state is needed

Rejected is only for explicit rejection, not replacement.
```

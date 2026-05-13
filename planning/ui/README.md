# UI Planning Index

Status: current UI planning navigation index  
Scope: textual UI planning and later low-fidelity mockup/prototype generation

## 1. Purpose

UI planning is a parallel planning branch. It checks whether the current use cases can be served by:

```text
- pages;
- visible DATA;
- inputs;
- actions;
- status-dependent states;
- validation/error states;
- empty states;
- navigation.
```

UI planning is not final visual design and not implementation.

## 2. Roles / Chat Types

```text
Architect chat
-> defines workflow, boundaries, prompts and reviews results.

UI planning chat
-> creates textual page plan and UI questions register.

Mockup / prototype chat
-> creates low-fidelity HTML/React mockup from the approved/tentative UI plan.
```

## 3. Current UI Workflow

```text
scenario text specs
-> scenario DATA files
-> validation-related file
-> ui-planning-workflow.md
-> test-site-ui-plan.md
-> ui-questions-register.md
-> optional visual / HTML / React low-fidelity mockup later
```

## 4. Current UI Read Order

For UI planning chat:

```text
1. planning/ui/README.md
2. planning/ui/ui-planning-workflow.md
3. planning/diagrams/scenario-text-specs/
4. planning/diagrams/scenario-data/
5. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
6. planning/tables/pre-domain-variants-input.md
7. planning/diagrams/scenario-diagram-consistency-report.md
8. planning/scenario-specification-principles.md
```

For mockup/prototype chat:

```text
1. planning/ui/README.md
2. planning/ui/mockup-generation-guide.md
3. planning/ui/test-site-ui-plan.md
4. planning/ui/ui-questions-register.md
```

Mockup/prototype chat should read scenario specs or DATA files only when the UI plan is unclear.

## 5. Current UI Files

```text
planning/ui/ui-planning-workflow.md
  Rules for textual UI planning.

planning/ui/test-site-ui-plan.md
  Main textual page plan.

planning/ui/ui-questions-register.md
  Open UI alternatives and decisions.

planning/ui/mockup-generation-guide.md
  Rules for later visual/HTML/React low-fidelity mockup generation.

planning/ui/prompts/ui-planning-chat-prompt.md
  Prompt for the UI planning chat.

planning/ui/prompts/mockup-generation-chat-prompt.md
  Prompt for the mockup/prototype chat.

planning/ui/prompts/architect-ui-review-prompt.md
  Prompt for architectural review of UI planning output.
```

## 6. What Goes Where

| Information | File |
|---|---|
| Mandatory observable user behavior | scenario text specs |
| Visible/input/selectable/filter/attachment DATA | scenario DATA files |
| Client/server/domain validation | validation addendum |
| Invariants and write-state/domain-method pressure | `pre-domain-variants-input.md` |
| Page inventory, page responsibilities, actions, status states | `test-site-ui-plan.md` |
| UI alternatives and unresolved decisions | `ui-questions-register.md` |
| Rules for later HTML/React mockup generation | `mockup-generation-guide.md` |

## 7. Current Next UI Step

Create/fill:

```text
planning/ui/test-site-ui-plan.md
planning/ui/ui-questions-register.md
```

Use:

```text
planning/ui/prompts/ui-planning-chat-prompt.md
```

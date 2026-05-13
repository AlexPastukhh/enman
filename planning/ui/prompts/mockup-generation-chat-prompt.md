# Mockup Generation Chat Prompt

Use this prompt for a dedicated mockup/prototype chat after `test-site-ui-plan.md` and `ui-questions-register.md` exist.

```text
You continue work on repo AlexPastukhh/enman, branch `my-changes`.

Your role:
Mockup / prototype chat.

Your task:
Create a low-fidelity visual/HTML/React test mockup from the existing textual UI plan.

You are not creating final production UI.
You are not designing backend endpoints.
You are not creating database schema.
You are not changing business behavior.

Read:
- planning/ui/README.md
- planning/ui/mockup-generation-guide.md
- planning/ui/test-site-ui-plan.md
- planning/ui/ui-questions-register.md

Read scenario specs or DATA files only if the UI plan is unclear:
- planning/diagrams/scenario-text-specs/
- planning/diagrams/scenario-data/

Do not use stale scenario package summaries as semantic source of truth.

Task:
Generate a low-fidelity test-site mockup that allows manual walkthrough of the active use cases covered by `test-site-ui-plan.md`.

The mockup must:
- implement the current UI plan;
- use fake/sample data;
- stay low-fidelity;
- make scenario walkthrough possible;
- show status-dependent UI states;
- show validation/error/empty states when relevant;
- mark unresolved UI questions visibly when they affect the screen.

The mockup must not:
- invent new pages/actions not in the UI plan;
- invent domain behavior;
- silently resolve open UI questions;
- hardcode backend/API assumptions;
- introduce stale request status Submitted;
- introduce Signed as core agreement status;
- create final visual design.

If an open UI question affects the mockup:
- use current preference only as provisional test behavior;
- add a visible note or internal comment;
- do not mark the question accepted unless asked.

Preferred output:
- a single low-fidelity HTML file, or
- a single React component/page if explicitly requested.

When generating files:
- use repository-relative paths;
- generate complete files, not patches.
```

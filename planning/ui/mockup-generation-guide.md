# Mockup Generation Guide

Status: current guide for later visual / HTML / React low-fidelity mockup generation

## 1. Purpose

This guide is for the mockup/prototype chat.

It describes how to generate a low-fidelity visual or interactive test mockup from the textual UI plan.

The mockup is not final production UI.

## 2. Inputs

Use:

```text
planning/ui/test-site-ui-plan.md
planning/ui/ui-questions-register.md
planning/ui/mockup-generation-guide.md
```

Read scenario specs or DATA files only when the UI plan is unclear.

## 3. Output Options

Depending on request, output may be:

```text
- HTML prototype;
- React prototype;
- low-fidelity wireframe document;
- prompt for an image/wireframe tool.
```

Do not create backend endpoints, database schema or production implementation.

## 4. Mockup Rules

The mockup must:

```text
- implement the current UI plan;
- use fake/sample data;
- stay low-fidelity;
- make scenario walkthrough possible;
- show status-dependent UI states;
- show validation/error/empty states when relevant;
- mark unresolved UI questions visibly when they affect the screen.
```

The mockup must not:

```text
- invent business behavior;
- invent new domain actions;
- silently resolve open UI questions;
- hardcode implementation mechanics not present in UI plan;
- create final visual design;
- create backend/API assumptions.
```

## 5. Handling Open Questions

If `ui-questions-register.md` has an open question that affects the mockup:

```text
- choose the current preference only for test walkthrough;
- mark the choice as tentative in the mockup notes;
- do not change the question status unless explicitly asked.
```

If no current preference exists:

```text
- show the simplest option needed to walk through the scenario;
- add a note that the option is provisional;
- do not treat it as accepted design.
```

## 6. Suggested Mockup Structure

For HTML/React prototype:

```text
- simple navigation shell;
- client pages;
- employee pages;
- fake data state;
- status toggles or demo state controls if useful;
- no backend integration;
- no real authentication;
- no real file upload persistence.
```

## 7. Review Checklist

Before returning a mockup, check:

```text
- every active page in test-site-ui-plan.md is represented or intentionally omitted;
- request statuses InReview / Approved / Rejected are visible somewhere;
- agreement proposal statuses AwaitingClientConfirmation / SentByClient / Accepted / Rejected are visible somewhere;
- forbidden review actions are handled;
- client ownership/access behavior is not contradicted;
- no stale Submitted status is introduced;
- no Signed status is introduced as core;
- no domain behavior is invented.
```

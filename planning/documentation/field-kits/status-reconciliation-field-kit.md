# Status Reconciliation Field Kit

Status: active reusable field kit  
Scope: setup guidance for defining a project's evidence/current-reality model and status vocabulary before running status reconciliation

## 1. Purpose

This field kit helps a project define how documentation status claims should be checked.

It is used before the repeated reconciliation workflow.

It answers:

```text
Does this documentation domain have current-reality/evidence claims?
What counts as evidence?
What status vocabulary should be used?
Where should the project-specific evidence profile live?
Which files should be reconciled against that profile?
```

## 2. Responsibility Split

| Owner | Owns |
|---|---|
| Principles | Invariant: current-state claims need a defined evidence/current-reality model when such claims exist. |
| This field kit | Applicability gate, setup questions, status vocabulary design and project profile shape. |
| Status reconciliation workflow | Repeated reconciliation process after the evidence/status model exists. |
| Project profile/adapter | Concrete evidence order, paths, statuses and project-specific examples. |
| Examples | Demonstrations of applying this kit; not source-of-truth logic. |

## 3. Applicability Gate

Use this kit when docs contain claims like:

```text
implemented;
current;
exists now;
verified;
done;
planned;
deferred;
outdated;
historical;
external-output-ready.
```

Do not force implementation-style reconciliation onto documentation domains without implementation evidence.

Instead, define the domain's current-reality model.

Examples:

| Domain | Possible current-reality/evidence model |
|---|---|
| Software/app project | code, tests, migrations, generated contracts, runtime screenshots, deployed behavior |
| Study notes | source notes, review schedule, cards, learning state |
| Day planning | calendar, task state, completed/blocked items |
| Research notes | source papers, excerpts, citation map, experiment notes |
| Documentation-only project | accepted sources, current user decisions, published docs, superseded notes |

## 4. Setup Questions

Answer these before creating the project profile:

```text
1. What status claims appear in this docs layer?
2. Which claims can become stale?
3. What evidence source can verify each claim type?
4. Which evidence sources win when they conflict?
5. Which statuses should be allowed?
6. Which docs should be reconciled regularly?
7. Which docs are historical snapshots and should not be updated as current truth?
8. Which external-facing outputs need evidence maps?
9. Where will the project evidence/status profile live?
10. Which examples should demonstrate the profile?
```

## 5. Suggested Status Vocabulary

Start small.

Possible statuses:

```text
current / verified
implemented
first-stage implemented
partially implemented
planned
deferred
historical
superseded
needs evidence check
not applicable
```

A project should define which statuses are allowed and what each status requires.

## 6. Project Profile Shape

Create a project-specific file with this shape:

```text
# <Project> Status Evidence Profile

## Purpose
## Applicability
## Status Vocabulary
## Evidence Order
## Evidence Source Categories
## Claims That Need Reconciliation
## Files/Registers That Use This Profile
## External-Output Evidence Notes
## Do Not
```

For the active Enman project profile, see:

```text
planning/status-evidence-profile.md
```

## 7. Handoff To Workflow

After the project profile exists, use the repeated workflow:

```text
planning/documentation/status-reconciliation-workflow.md
```

The workflow should not reinvent the evidence model each time. It should apply the project profile.

## 8. Example

Scenario/project example:

```text
planning/documentation/examples/STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md
```

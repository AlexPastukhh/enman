# Status Reconciliation Workflow

Status: active reusable workflow  
Scope: repeated process for reconciling documentation status claims after a project evidence/current-reality model exists

## 1. Purpose

This workflow keeps documentation status claims aligned with the project's configured evidence/current-reality model.

It is not the setup kit.

Use the setup kit first when the project has not yet defined evidence sources, status vocabulary or status-owner files:

```text
planning/documentation/field-kits/status-reconciliation-field-kit.md
```

## 2. Responsibility Split

| Owner | Owns |
|---|---|
| Principles | Why current-state claims need evidence/current-reality checking. |
| Field kit | How to define the evidence model and status vocabulary for a project/domain. |
| Project evidence profile | The concrete evidence order, statuses and status-owner files for one project. |
| This workflow | Repeated reconciliation process using the configured profile. |
| Examples | Demonstrations only. |

## 3. Inputs

Before running this workflow, identify:

```text
- target docs/registers to reconcile;
- project evidence/status profile;
- evidence sources to check;
- status labels allowed by the profile;
- whether the output is internal, external-facing or historical.
```

For Enman candidate setup:

```text
planning/status-evidence-profile.md
```

## 4. When To Run

Run status reconciliation when:

```text
- evidence/current reality changed;
- generated artifacts changed;
- tests or verification baselines changed;
- a planned item became current/verified;
- a doc still says planned for something now evidenced as current;
- a doc claims current truth but evidence is missing or conflicting;
- external-facing output depends on a status claim.
```

## 5. Reconciliation Process

```text
1. Read the project evidence/status profile.
2. Identify the status claims in scope.
3. Check the configured evidence sources in profile order.
4. Compare docs status with evidence/current reality.
5. Classify each claim as current, planned, deferred, historical, stale, not applicable or needs evidence check according to the profile.
6. Propose docs updates or follow-up items.
7. Preserve historical snapshots when they are intentionally historical.
8. Record evidence checked and evidence not checked.
```

## 6. Output Table

Use this table when helpful:

| Area | Claim | Evidence checked | Status | Docs to update | Next action |
|---|---|---|---|---|---|

## 7. Conflict Handling

If evidence conflicts:

```text
- say which sources conflict;
- do not silently choose the convenient source;
- prefer the profile's evidence order;
- mark assumptions explicitly;
- create a follow-up if the conflict cannot be resolved.
```

## 8. Do Not

```text
- Do not invent project evidence sources during repeated reconciliation.
- Do not treat old status snapshots as current truth.
- Do not force software implementation statuses onto non-software docs domains.
- Do not update external-facing claims without checking the profile's external-output rules.
```

## 9. Related Files

```text
planning/documentation/field-kits/status-reconciliation-field-kit.md
planning/status-evidence-profile.md
planning/documentation/examples/STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md
```

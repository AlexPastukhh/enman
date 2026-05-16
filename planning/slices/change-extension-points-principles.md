# Change Points, Extension Points And Extension Pressure Principles

Status: current planning principles  
Scope: server/client change points, extension points, extension pressure and anti-coupling decisions

## 1. Purpose

This file defines how to identify and document implementation seams that may change or be extended.

It applies to parent slice files, `.client.md` sidecars, shortened drafts, the extension points register, implementation planning questions and architecture reasoning.

## 2. Definitions

```text
Hard invariant
= behavior that should not vary as policy/option.
Example: a request cannot be approved twice.

Change point
= place where current behavior may change by policy, option, config, strategy, adapter, props, hook, mapper or response shape.
Example: missing applicant read response is 200 exists=false vs 404.

Extension point
= place where a future slice can be added.
Example: current applicant read response can later expose verification workflow details.

Extension pressure
= knowledge about likely future extension that affects current implementation planning now.
Example: Account page read model can include verificationStatus now without coupling to the future verification workflow.

Anti-coupling decision
= explicit decision to avoid linking current slice to future behavior.
Example: request creation must not depend on client-supplied applicantPartyId.

Unnecessary abstraction
= seam/interface/config created without enough probability or value.
```

## 3. Key Rule

Known future extension points must be reviewed during current slice planning.

This does not mean every future extension requires abstraction now.

For each extension pressure case, decide explicitly:

```text
- create explicit seam now;
- avoid coupling only;
- follow current convention and accept possible future refactor;
- ignore for now because certainty is low;
- revisit when a later slice starts.
```

The decision must be recorded locally and, if it can affect future slices, in the extension points register.

## 4. Shortened Draft Rule

Shortened drafts should include a compact `Extension / Change Points` section when future behavior can affect current design.

Use this format:

| ID | Type | Area | Current direction | Register sync | Status |
|---|---|---|---|---|---|

Use it for:

```text
- response-shape choices;
- read model fields;
- identity exposure decisions;
- normal missing/empty states;
- anti-coupling constraints;
- invariant/data-policy questions;
- performance/tracking notes.
```

Primary example:

```text
planning/slices/examples/L1-APPLICANT-PARTY-READ-CURRENT-early-short-draft-example.md
```

## 5. Extension Pressure Trade-Off

Extension pressure protects against accidental coupling, but it is not an excuse for overengineering.

Each case should consider:

```text
- probability/certainty;
- time horizon;
- cost of preparing now;
- cost of changing later;
- whether future behavior is already planned as next layer;
- whether current convention creates harmful coupling;
- whether a less beautiful but more local implementation is better now.
```

## 6. Current Handling Options

| Handling | Use when | Example |
|---|---|---|
| Explicit seam now | future extension is high-certainty and seam is cheap | provider port when provider work is imminent |
| Anti-coupling only | extension is likely but details unclear | request creation does not accept applicantPartyId |
| Convention-first | future extension is uncertain and current code remains local | simple page-local component |
| Ignore for now | probability low or preparation cost too high | speculative dashboard customization |
| Revisit later | question should be checked before next layer | extension register entry |

## 7. Parent Slice Sections

Parent slice files should include when relevant:

```text
## Server / Cross-Layer Extension Points
## Server / Cross-Layer Change Points
## Extension Pressure / Anti-Coupling Decisions
```

## 8. Client Sidecar Sections

`.client.md` sidecars should include when relevant:

```text
## Client Extension Points
## Client Behavior Change Points
## Client Extension Pressure / Anti-Coupling Decisions
```

## 9. Implementation Questions

```text
1. Is this a hard invariant, change point, extension point, extension pressure, anti-coupling decision or unnecessary abstraction?
2. Is the future extension likely enough to affect current design?
3. Do we need an explicit seam now or only anti-coupling constraints?
4. What must current implementation avoid so future slice remains easy?
5. Could following the default convention create coupling against the planned extension?
6. Is a less beautiful but more local implementation better for current development situation?
7. What tests should prove that current slice does not accidentally execute future extension behavior?
8. Where should this be recorded so future agents see it?
```

## 10. Test Implications

Tests may prove:

```text
- current behavior works;
- future extension behavior is not accidentally executed now;
- hard invariant cannot be violated;
- change point behavior has current expected decision;
- anti-coupling constraint is visible in API/client behavior;
- a read slice has no write side effects.
```

Example:

```text
Current applicant read returns missing applicant as page state,
but does not create ApplicantParty and does not require clientAccountId.
```

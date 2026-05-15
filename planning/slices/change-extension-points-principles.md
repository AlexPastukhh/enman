# Change Points, Extension Points And Extension Pressure Principles

Status: current planning principles  
Scope: server/client change points, extension points, extension pressure and anti-coupling decisions

## 1. Purpose

This file defines how to identify and document implementation seams that may change or be extended.

It applies to parent slice files, `.client.md` sidecars, the extension points register, implementation planning questions and architecture reasoning.

## 2. Definitions

```text
Hard invariant
= behavior that should not vary as policy/option.
Example: a request cannot be approved twice.

Change point
= place where current behavior may change by policy, option, config, strategy, adapter, props, hook, or mapper.
Example: rejection feedback optional vs required.

Extension point
= place where a future slice can be added.
Example: Approved request can later start agreement proposal creation.

Extension pressure
= knowledge about likely future extension that affects current implementation planning now.
Example: approval should not auto-create agreement proposal or couple approve feature to agreement code.

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

## 4. Extension Pressure Trade-Off

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

## 5. Current Handling Options

| Handling | Use when | Example |
|---|---|---|
| Explicit seam now | future extension is high-certainty and seam is cheap | provider port when provider work is imminent |
| Anti-coupling only | extension is likely but details unclear | approval does not auto-create proposal |
| Convention-first | future extension is uncertain and current code remains local | simple page-local component |
| Ignore for now | probability low or preparation cost too high | speculative dashboard customization |
| Revisit later | question should be checked before next layer | extension register entry |

## 6. Parent Slice Sections

Parent slice files should include:

```text
## Server / Cross-Layer Extension Points
## Server / Cross-Layer Change Points
## Extension Pressure / Anti-Coupling Decisions
```

## 7. Client Sidecar Sections

`.client.md` sidecars should include:

```text
## Client Extension Points
## Client Behavior Change Points
## Client Extension Pressure / Anti-Coupling Decisions
```

## 8. Implementation Questions

```text
1. Is this a hard invariant, change point, extension point, extension pressure, or unnecessary abstraction?
2. Is the future extension likely enough to affect current design?
3. Do we need an explicit seam now or only anti-coupling constraints?
4. What must current implementation avoid so future slice remains easy?
5. Could following the default convention create coupling against the planned extension?
6. Is a less beautiful but more local implementation better for current development situation?
7. What tests should prove that current slice does not accidentally execute future extension behavior?
8. Where should this be recorded so future agents see it?
```

## 9. Test Implications

Tests may prove:

```text
- current behavior works;
- future extension behavior is not accidentally executed now;
- hard invariant cannot be violated;
- change point behavior has current expected decision;
- anti-coupling constraint is visible in API/client behavior.
```

Example:

```text
Approval marks request Approved and applicant Verified,
but does not create agreement proposal.
```

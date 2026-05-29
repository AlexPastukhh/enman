# Source Usage Pilots

Status: pilot folder / experimental  
Scope: temporary source usage register pilots for proving source usage and cascade-review shape

## 1. Purpose

This folder stores pilot source usage registers.

Pilots are used to test how source usage relationships should be recorded before creating a full source usage/cascade workflow or permanent register structure.

## 2. Rules

```text
- Pilot files are experimental.
- Permanent register placement is deferred.
- Pilot fields are not global final schema yet.
- Do not copy pilot fields into all templates before a real pilot proves the shape.
- Do not treat a pilot skeleton as a filled dependency audit.
```

## 3. Current Pilots

| Pilot | Status | Scope |
|---|---|---|
| `SC-13D-agreement-exchange-source-usage-register.md` | skeleton / not audited | SC-13D scenario/behavior sources -> AgreementProposalExchange -> SL-AGR-EXCH-001 |

## 4. Pilot Exit Questions

After filling a pilot, answer:

```text
- Did the row shape identify affected downstream scopes?
- Was source granularity too broad, too detailed or useful?
- Could cascade review avoid broad re-reading?
- Which fields were useful?
- Which fields were noise?
- Should permanent registers be global, per-layer, per-scenario or per-chain?
- Which templates/workflows need updates after the pilot?
```

## 5. Not Source Of Truth

Pilot registers do not override source artifacts, domain drafts, slice drafts, scenario maps or workflow files.

They are review/cascade aids.

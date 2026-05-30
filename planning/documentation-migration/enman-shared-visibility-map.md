# Enman Shared Visibility Map

Status: candidate Enman project-instance map  
Scope: concrete local-detail to shared-visibility targets for Enman documentation

> Candidate note: this file belongs to `planning/documentation-migration/`. It is not active Enman source of truth until a later migration/switch batch approves it.

## 1. Purpose

This file answers the Shared Visibility Map field kit for Enman.

Kit owner:

```text
planning/documentation/field-kits/shared-visibility-map-field-kit.md
```

Repeated workflow:

```text
planning/documentation/local-global-documentation-sync-workflow.md
```

## 2. Shared Visibility Table

| Local detail type | Local owner | Shared visibility target | Mirror condition | Local-only allowed? | Notes |
|---|---|---|---|---|---|
| Slice open question | `planning/slices/**` | `planning/slices/slice-questions-register.md` | affects future slice/domain/API/testing work | yes, if truly local to one slice and marked local-only | Exact target may change after active migration. |
| Slice extension point | `planning/slices/**` | `planning/slices/slice-extension-points-register.md` | future extension or cross-slice reuse likely | yes, with reason | |
| Slice implementation note | `planning/slices/**` | `planning/slices/slice-implementation-notes-register.md` | affects implementation status/evidence/future work | yes, with reason | |
| Scenario question | `planning/diagrams/**` | `planning/diagrams/scenario-questions-register.md` | affects scenario behavior, DATA or behavior items | yes, with reason | |
| ADR candidate | architecture/API/client/testing docs | `planning/adr/adr-candidates.md` | architecture decision may need formal ADR | yes, with reason | |
| Architecture decision note | architecture/API/client/testing docs | `planning/adr/architecture-decision-notes.md` | decision affects multiple docs/layers | no, unless temporary | |
| New file/folder | any planning layer | relevant README/index | file should be discoverable | no | update navigation/read order. |
| External-output wording issue | internal planning docs | `planning/vkr-clean-reference.md` or `planning/thesis/` | affects VKR/thesis/public-facing wording | yes, if not external-facing yet | Candidate mapping only. |

## 3. Do Not

```text
- Do not mirror every local note automatically.
- Do not bury cross-file questions only in local files.
- Do not use this candidate map as active Enman routing before a switch batch.
```

## 4. Related Files

```text
planning/documentation-migration/enman-docs-adapter.md
planning/documentation/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md
```

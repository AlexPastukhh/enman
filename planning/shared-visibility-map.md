# Enman Shared Visibility Map

Status: active Enman shared visibility map  
Scope: concrete local-detail to shared-visibility targets for Enman documentation

## 1. Purpose

This file defines where local Enman documentation details should be mirrored when they affect future work outside one local file.

It is a project-level map.

Reusable setup owner:

```text
planning/documentation/field-kits/shared-visibility-map-field-kit.md
```

Historical origin:

```text
former reusable candidate workspace
```

Active repeated workflow:

```text
planning/documentation/local-global-documentation-sync-workflow.md
```

## 2. Shared Visibility Table

| Local detail type | Local owner | Shared visibility target | Mirror condition | Local-only allowed? | Notes |
|---|---|---|---|---|---|
| Slice open question | `planning/slices/**` | `planning/slices/slice-questions-register.md` | affects future slice/domain/API/testing work | yes, if truly local to one slice and marked local-only | |
| Slice extension point | `planning/slices/**` | `planning/slices/slice-extension-points-register.md` | future extension or cross-slice reuse likely | yes, with reason | |
| Slice implementation note | `planning/slices/**` | `planning/slices/slice-implementation-notes-register.md` | affects implementation status/evidence/future work | yes, with reason | |
| Scenario question | `planning/diagrams/**` | `planning/diagrams/scenario-questions-register.md` | affects scenario behavior, DATA or behavior items | yes, with reason | |
| ADR candidate | architecture/API/client/testing docs | `planning/adr/adr-candidates.md` | architecture decision may need formal ADR | yes, with reason | |
| Architecture decision note | architecture/API/client/testing docs | `planning/adr/architecture-decision-notes.md` | decision affects multiple docs/layers | no, unless temporary | |
| New file/folder | any planning layer | relevant README/index | file should be discoverable | no | update navigation/read order. |
| External-output wording issue | internal planning docs | `planning/vkr-clean-reference.md` or `planning/thesis/` | affects VKR/thesis/public-facing wording | yes, if not external-facing yet | |

## 3. Do Not

```text
- Do not mirror every local note automatically.
- Do not bury cross-file questions only in local files.
- Do not treat this map as reusable documentation architecture; it is Enman project configuration.
- Do not update shared targets without checking whether the target register/index still owns that detail type.
```

## 4. Related Files

```text
planning/documentation/local-global-documentation-sync-workflow.md
planning/documentation/field-kits/shared-visibility-map-field-kit.md
planning/documentation/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md
```

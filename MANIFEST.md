# MANIFEST

Archive: enman-l1-readiness-update-v1.zip

Purpose:
Prepare planning docs for starting the first L1 domain implementation step with an implementation agent.

Repository branch:
my-changes

## Files included

| Path | Action | Purpose |
|---|---|---|
| `planning/README.md` | replace | Update global planning navigation: domain-draft-01 exists; next step is L1 readiness/cut. |
| `planning/planning-workflow-current.md` | replace | Update workflow from “create draft 1” to “review/refine draft 01 then L1 implementation cut”. |
| `planning/tables/README.md` | replace | Update planning tables navigation for saved draft and L1 implementation cut. |
| `planning/tables/00-planning-tables-index.md` | replace | Add implementation-readiness file to active files/read order. |
| `planning/tables/domain-drafts/README.md` | replace | Clarify current draft status and next step. |
| `planning/tables/domain-drafts/domain-draft-01.md` | replace | Refine draft for pre-L1 readiness; move activation guard out of ApplicantParty factory. |
| `planning/l1-domain-implementation-cut.md` | add | New guide for first L1 domain implementation cut and progressive file splitting. |
| `planning/current-state.md` | replace | Reframe as implementation snapshot/background, not target domain model. |
| `planning/domain-model.md` | replace | Reframe as background compatibility note, not current target draft. |
| `APPLY.md` | add to archive root | Manual application instructions. |
| `MANIFEST.md` | add to archive root | This manifest. |

## Deletions

Nothing should be deleted.

## Next step after applying

Review:

```text
planning/l1-domain-implementation-cut.md
planning/tables/domain-drafts/domain-draft-01.md
```

Then give the implementation agent a narrow task:

```text
Implement L1 domain classes and unit tests only.
Do not implement persistence/API/UI.
Do not implement AgreementProposalExchange yet.
```

# Domain Source Sync Register

Status: draft / derived register with scenario and domain Doc version references  
Doc version: v0.1.0  
Scope: source dependency register for active Enman domain aggregate drafts

## 1. Purpose

This register indexes source dependencies for the active domain aggregate drafts.

It is derived from local section-level `Sources:` blocks in:

```text
planning/domain/aggregates/account.md
planning/domain/aggregates/applicant-party.md
planning/domain/aggregates/connection-request.md
planning/domain/aggregates/agreement-proposal-exchange.md
```

This register is not the source of truth for a section's local working context. The local `Sources:` block in the aggregate section remains authoritative for section-level work.

## 2. Register Rules

```text
- Aggregate draft local Sources blocks remain authoritative for section work.
- This register is a navigation/sync index derived from those local Sources blocks.
- Do not treat a source as checked unless the aggregate local Sources block says it was checked.
- Do not invent source versions for files that do not declare Doc version.
- Scenario/domain markdown files under planning/diagrams/ and planning/domain/ are treated as Doc version: v0.1.0 after F7K-D2 unless a file declares another Doc version.
- F7K-D2 was a document-version seed pass, not a semantic re-audit.
- Implementation/test paths listed here are previously checked archive/source-pass evidence unless explicitly rechecked later.
- Historical planning/tables sources remain historical/cross-check sources unless separately versioned and promoted.
- Slice draft refactor stays deferred until this domain register is reviewed.
```

Source version/status values used in this register:

```text
Doc version: v0.1.0
status from file header
version not declared
historical/cross-check source
implementation evidence from prior archive/source pass
not checked / deferred
```

## 3. Active Domain Files

| Domain file | Domain artifact | Doc version | Prepared state | Notes |
|---|---|---|---|---|
| `planning/domain/aggregates/account.md` | Account aggregate | v0.1.0 | local section `Sources:` blocks added | Actor/account identity and capability source. |
| `planning/domain/aggregates/applicant-party.md` | ApplicantParty aggregate | v0.1.0 | local section `Sources:` blocks added | Applicant/contact/template source. |
| `planning/domain/aggregates/connection-request.md` | ConnectionRequest aggregate | v0.1.0 | local section `Sources:` blocks added | Request creation/review lifecycle source. |
| `planning/domain/aggregates/agreement-proposal-exchange.md` | AgreementProposalExchange aggregate | v0.1.0 | local section `Sources:` blocks added | Agreement proposal lifecycle/final refusal source. |

## 4. Source Categories

| Source category | Meaning | Version/status rule |
|---|---|---|
| Format/process | Drafting workflows, templates, source-block templates and modeling principles. | `planning/domain/**/*.md` sources are Doc version: v0.1.0 after F7K-D2. Root-level `planning/source-cascade-sync-workflow.md` and `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` are outside D2 scope unless separately versioned. |
| Scenario text spec | Scenario narrative and primary scenario-specific DATA via `#DATA`. | `planning/diagrams/**/*.md` sources are Doc version: v0.1.0 after F7K-D2 unless a file declares another Doc version. |
| Scenario behavior items | Scenario behavior categories feeding domain methods, invariants, lifecycle and boundary notes. | `planning/diagrams/**/*.md` sources are Doc version: v0.1.0 after F7K-D2 unless a file declares another Doc version. |
| Scenario clarification | Explicit source clarification used to resolve domain direction. | `planning/diagrams/**/*.md` sources are Doc version: v0.1.0 after F7K-D2 unless a file declares another Doc version. |
| Scenario DATA sidecar | Reusable/shared/audited/transitional DATA sidecar source, not primary scenario-specific DATA by default. | `planning/diagrams/**/*.md` sources are Doc version: v0.1.0 after F7K-D2 unless a file declares another Doc version. |
| Historical/cross-check | Old monolithic or table-based domain material retained as historical extraction input. | `planning/tables/**` remains historical/cross-check and version not declared in this register unless separately versioned later. |
| Domain aggregate | Active aggregate draft used as cross-aggregate source. | Use declared Doc version; active aggregates are v0.1.0. |
| Domain value object | Active value-object draft used by aggregate state/invariants/methods. | `planning/domain/**/*.md` sources are Doc version: v0.1.0 after F7K-D2 unless a file declares another Doc version. |
| Implementation/test evidence | Code/tests checked during prior extraction/source pass. | Previously checked evidence; not current proof unless rechecked. |

## 5. Aggregate Source Dependencies

| Consumer domain file | Source group | Source files | Used by sections | Source version/status | Sync status |
|---|---|---|---|---|---|
| `planning/domain/aggregates/account.md` | Format/process | `planning/domain/aggregate-drafting-workflow.md`; `planning/domain/aggregate-draft-template.md`; `planning/domain/domain-modeling-principles.md`; `planning/source-cascade-sync-workflow.md`; `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`; `planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md` | all draft/source-block sections | domain process files under `planning/domain/` are Doc version: v0.1.0 after F7K-D2; root source-cascade/template files are outside D2 scope unless separately versioned | current for v0.1.0 aggregate source-block prep |
| `planning/domain/aggregates/account.md` | Scenario/content | `planning/diagrams/scenario-text-specs/SC-01-register-client.md`; `planning/diagrams/scenario-text-specs/SC-02-login.md`; `planning/diagrams/scenario-text-specs/SC-03B-account-owner-verified.md`; `planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md`; SC-01/SC-02/SC-03B behavior items | Purpose, Source Inputs, Owned State, Domain Methods, Invariants, Lifecycle, Behavior Coverage | Doc version: v0.1.0 for `planning/diagrams/**/*.md` sources after F7K-D2 | current as local aggregate source |
| `planning/domain/aggregates/account.md` | Historical/domain decision | `planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md` | Purpose, Source Inputs, Boundary, Methods, Persistence | historical/cross-check; version not declared in this register unless the source is under `planning/domain/` and declares Doc version | current as accepted account/employee hierarchy source |
| `planning/domain/aggregates/account.md` | Cross-aggregate | `planning/domain/aggregates/applicant-party.md`; `planning/domain/aggregates/connection-request.md`; `planning/domain/aggregates/agreement-proposal-exchange.md` | Boundary, Cross-Aggregate Relations, Source Delta | Doc version: v0.1.0 for prepared aggregate drafts | no semantic re-sync needed now; later changes must be checked |
| `planning/domain/aggregates/applicant-party.md` | Format/process | `planning/domain/aggregate-drafting-workflow.md`; `planning/domain/aggregate-draft-template.md`; `planning/domain/domain-modeling-principles.md`; `planning/source-cascade-sync-workflow.md`; `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`; `planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md` | all draft/source-block sections | domain process files under `planning/domain/` are Doc version: v0.1.0 after F7K-D2; root source-cascade/template files are outside D2 scope unless separately versioned | current for v0.1.0 aggregate source-block prep |
| `planning/domain/aggregates/applicant-party.md` | Scenario/content | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md`; `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md`; `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md`; SC-10/SC-10B/SC-04 behavior items | Purpose, Source Inputs, Owned State, Domain Methods, Invariants, Lifecycle, Behavior Coverage | Doc version: v0.1.0 for `planning/diagrams/**/*.md` sources after F7K-D2 | current as local aggregate source |
| `planning/domain/aggregates/applicant-party.md` | Historical | `planning/tables/domain-drafts/domain-draft-01.md`; `planning/tables/domain-drafts/domain-draft-02.md` | Purpose, Source Inputs, Boundary, Persistence | historical/cross-check; version not declared in this register unless the source is under `planning/domain/` and declares Doc version | current as historical extraction input |
| `planning/domain/aggregates/applicant-party.md` | Value objects | `planning/domain/value-objects/applicant-identity.md`; `planning/domain/value-objects/applicant-contact.md` | Owned State, Methods, Invariants, Value Objects, Behavior Coverage | Doc version: v0.1.0 after F7K-D2 | current as local aggregate source |
| `planning/domain/aggregates/applicant-party.md` | Cross-aggregate | `planning/domain/aggregates/account.md`; `planning/domain/aggregates/connection-request.md` | Boundary, Methods, Relations, Questions | Doc version: v0.1.0 for prepared aggregate drafts | no semantic re-sync needed now; later changes must be checked |
| `planning/domain/aggregates/connection-request.md` | Format/process | `planning/domain/aggregate-drafting-workflow.md`; `planning/domain/aggregate-draft-template.md`; `planning/domain/domain-modeling-principles.md`; `planning/source-cascade-sync-workflow.md`; `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`; `planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md` | all draft/source-block sections | domain process files under `planning/domain/` are Doc version: v0.1.0 after F7K-D2; root source-cascade/template files are outside D2 scope unless separately versioned | current for v0.1.0 aggregate source-block prep |
| `planning/domain/aggregates/connection-request.md` | Scenario/content | `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md`; `planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md`; SC-04/SC-05/SC-06/SC-07A/SC-07B/SC-13D behavior items | Purpose, Source Inputs, Owned State, Methods, Invariants, Lifecycle, Behavior Coverage | Doc version: v0.1.0 for `planning/diagrams/**/*.md` sources after F7K-D2 | current as local aggregate source |
| `planning/domain/aggregates/connection-request.md` | Value objects | `planning/domain/value-objects/object-address.md`; `planning/domain/value-objects/rejection-feedback.md` | Owned State, Methods, Invariants, Value Objects, Behavior Coverage, Persistence | Doc version: v0.1.0 after F7K-D2 | current as local aggregate source |
| `planning/domain/aggregates/connection-request.md` | Cross-aggregate | `planning/domain/aggregates/applicant-party.md`; `planning/domain/aggregates/account.md`; `planning/domain/aggregates/agreement-proposal-exchange.md` | Purpose, Boundary, Methods, Invariants, Relations, Questions | Doc version: v0.1.0 for prepared aggregate drafts | no semantic re-sync needed now; later changes must be checked |
| `planning/domain/aggregates/agreement-proposal-exchange.md` | Format/process | `planning/domain/aggregate-drafting-workflow.md`; `planning/domain/aggregate-draft-template.md`; `planning/domain/domain-modeling-principles.md`; `planning/source-cascade-sync-workflow.md`; `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`; `planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md` | all draft/source-block sections | domain process files under `planning/domain/` are Doc version: v0.1.0 after F7K-D2; root source-cascade/template files are outside D2 scope unless separately versioned | current for v0.1.0 aggregate source-block prep |
| `planning/domain/aggregates/agreement-proposal-exchange.md` | Scenario/content | `planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md`; `planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md`; `planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md`; `planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md`; `planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md` | Purpose, Source Inputs, Owned State, Methods, Invariants, Lifecycle, Behavior Coverage, Questions | Doc version: v0.1.0 for `planning/diagrams/**/*.md` sources after F7K-D2 | current as local aggregate source |
| `planning/domain/aggregates/agreement-proposal-exchange.md` | Value objects | `planning/domain/value-objects/agreement-proposal-version.md`; `planning/domain/value-objects/agreement-proposal-author.md`; `planning/domain/value-objects/agreement-document-ref.md`; `planning/domain/value-objects/proposal-comment.md`; `planning/domain/value-objects/final-refusal-reason.md` | Owned State, Methods, Invariants, Value Objects, Behavior Coverage, Persistence | Doc version: v0.1.0 after F7K-D2 | current as local aggregate source |
| `planning/domain/aggregates/agreement-proposal-exchange.md` | Cross-aggregate | `planning/domain/aggregates/connection-request.md`; `planning/domain/aggregates/account.md` | Purpose, Boundary, Methods, Invariants, Relations, Questions | Doc version: v0.1.0 for prepared aggregate drafts | no semantic re-sync needed now; later changes must be checked |


## 6. Cross-Aggregate Dependencies

| From domain file | To domain file | Dependency | Semantic re-sync needed now? | Why |
|---|---|---|---|---|
| `planning/domain/aggregates/applicant-party.md` | `planning/domain/aggregates/account.md` | scalar `ClientAccountId` owner relation | no | Account update in this series added Doc version/local Sources blocks and did not intentionally change Account semantics. |
| `planning/domain/aggregates/connection-request.md` | `planning/domain/aggregates/applicant-party.md` | `ApplicantPartyId` source for request creation | no | ApplicantParty update in this series added Doc version/local Sources blocks and did not intentionally change ApplicantParty semantics. |
| `planning/domain/aggregates/connection-request.md` | `planning/domain/aggregates/account.md` | `ClientAccountId` and Employee actor ids/capabilities | no | Account actor/capability boundary was not semantically changed in the source-block prep. |
| `planning/domain/aggregates/connection-request.md` | `planning/domain/aggregates/agreement-proposal-exchange.md` | agreement exchange failure coordination / SC-13D relation | no reverse update now | AgreementProposalExchange update in this series added Doc version/local Sources blocks and did not intentionally change exchange semantics. |
| `planning/domain/aggregates/agreement-proposal-exchange.md` | `planning/domain/aggregates/connection-request.md` | Approved request precondition and final refusal coordination | no | ConnectionRequest update in this series added Doc version/local Sources blocks and did not intentionally change request semantics. |
| `planning/domain/aggregates/agreement-proposal-exchange.md` | `planning/domain/aggregates/account.md` | Employee/Client actor ids and capabilities | no | Account semantics unchanged in this source-block prep series. |

Cross-aggregate sync rule:

```text
If an upstream aggregate later changes domain semantics, update:
  1. local Sources / Source Delta in that aggregate;
  2. direct downstream aggregate sections that list it as a Content source;
  3. this register cross-aggregate row;
  4. downstream slice drafts only after the domain layer is reviewed.
```

## 7. Scenario DATA Source Classification

Primary rule:

```text
Primary scenario-specific DATA belongs in the relevant scenario text spec #DATA section.
planning/diagrams/scenario-data/ is reusable/shared/audited/transitional by default.
```

| Domain file | Scenario-specific DATA primary | Sidecars referenced | Status |
|---|---|---|---|
| `planning/domain/aggregates/account.md` | SC-01/SC-02/SC-03B scenario text spec `#DATA` where needed; Doc version: v0.1.0 after F7K-D2 | `planning/diagrams/scenario-data/SC-03B-account-owner-verified-data.md` | sidecar transitional/referenced; Doc version: v0.1.0 after F7K-D2 |
| `planning/domain/aggregates/applicant-party.md` | SC-10/SC-10B/SC-04 scenario text spec `#DATA`; Doc version: v0.1.0 after F7K-D2 | `planning/diagrams/scenario-data/SC-10-applicant-data.md`; `planning/diagrams/scenario-data/SC-10B-my-applicant-parties-data.md`; `planning/diagrams/scenario-data/SC-04-request-creation-data.md` | sidecars transitional/referenced; Doc version: v0.1.0 after F7K-D2 |
| `planning/domain/aggregates/connection-request.md` | SC-04/SC-07B scenario text spec `#DATA`; Doc version: v0.1.0 after F7K-D2 | `planning/diagrams/scenario-data/SC-04-request-creation-data.md`; `planning/diagrams/scenario-data/SC-07B-employee-request-review-data.md` | sidecars transitional/referenced; Doc version: v0.1.0 after F7K-D2 |
| `planning/domain/aggregates/agreement-proposal-exchange.md` | SC-13D scenario text spec `#DATA`; Doc version: v0.1.0 after F7K-D2 | `planning/diagrams/scenario-data/SC-13D-employee-agreement-proposal-create-response-data.md`; `planning/diagrams/scenario-data/L2-employee-review-agreement-data.md` | sidecars transitional/referenced; Doc version: v0.1.0 after F7K-D2 |

## 8. Downstream Sync Notes

```text
- Slice draft refactor is deferred.
- This domain register is an upstream input for future slice-source-sync-register work.
- Do not update slice drafts until this domain register is reviewed.
- Future slice refactor should consume domain aggregate docs and this register, not rebuild the domain source graph from scratch.
- If a scenario source changes before slice refactor, first evaluate the impacted aggregate local Sources blocks and this register.
```

## 9. Not Checked / Deferred

Done:

```text
- Broad Doc version seed for planning/domain and planning/diagrams was completed in F7K-D2.
- This register was synchronized to those seeded Doc version values in F7K-D3.
```

Still deferred:

```text
- Semantic re-audit after the document-version seed.
- Full UI sidecar coverage for all domain flows.
- Full auth/session implementation audit.
- Full EF mapping implementation audit.
- Current implementation/test evidence recheck after these documentation updates.
- Doc version coverage outside planning/domain and planning/diagrams.
- Exact root/source workflow version sync for planning/source-cascade-sync-workflow.md and planning/SOURCE-SECTION-SOURCES-TEMPLATE.md if needed.
- Slice source-sync register.
- Slice draft global refactor.
```

## 10. Source Delta / Change Log

```text
- Created first domain source-sync register from active aggregate local section `Sources:` blocks.
- Registered all four active aggregate drafts as `Doc version: v0.1.0`.
- Recorded source groups for format/process, scenario/content, value objects, historical/cross-check and cross-aggregate dependencies.
- Recorded that no cross-aggregate semantic re-sync is needed now because F7K-A1..A4 changed Doc version/local Sources blocks and did not intentionally change aggregate domain behavior semantics.
- Recorded scenario DATA classification: scenario text spec #DATA is primary; scenario-data sidecars are reusable/shared/audited/transitional by default.
- Updated source dependency rows after F7K-D2 Doc version seed.
- Replaced version-not-declared wording for planning/domain and planning/diagrams source groups with Doc version: v0.1.0 references.
- Kept planning/tables and implementation/test evidence as historical/prior evidence, not newly versioned or re-audited.
- Kept slice refactor deferred until this register is reviewed.
```

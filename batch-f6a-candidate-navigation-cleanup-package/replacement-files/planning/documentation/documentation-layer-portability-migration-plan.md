# Documentation Layer Portability Migration Plan

Status: active migration plan / pre-split decision capture  
Scope: documentation-layer portability decisions before reusable candidate copy, principles split or project adapter extraction.

> This file is a repo-visible migration decision capture. It preserves the current pre-split agreements and boundaries before any reusable candidate copy, principles split or project adapter extraction.

## 1. Command Semantics Decision

### 1.1 `арх`

`арх` means:

- a fresh archive / repository snapshot has been provided;
- the archive should be treated as the current source-state reference;
- the archive is available for reading/planning/review.

`арх` does **not** mean:

- create an output archive;
- generate a replacement package;
- start file edits;
- apply a batch automatically.

Artifact/package generation still requires an explicit command such as:

- `давай архив`;
- `сделай архив`;
- `архив для батча`;
- `собери package`.

Guardrail:

> Do not start artifact/package generation merely because the user says `арх`. Use it as source-mode information only.

## 2. Copy-First Migration Decision

The active `planning/documentation/` layer should not be refactored in place first.

Preferred migration strategy:

1. Keep the current documentation layer as the active working Enman/project-specific baseline.
2. Create a temporary reusable candidate copy.
3. Clean/genericize the candidate copy.
4. Compare the candidate against the active layer.
5. Extract project-specific logic into project adapter/profile and examples.
6. Switch canonical references only after review.
7. Remove or archive the old Enman-specific copy only after verification.

Candidate workspace idea:

```text
planning/documentation-reusable-candidate/
```

Candidate guardrail:

> The reusable candidate copy is temporary and non-canonical until the migration is accepted. Ordinary current project documentation updates should continue to use the active `planning/documentation/` layer.

## 2A. Candidate Baseline State

The first reusable candidate baseline has been created as a copy of the active documentation layer:

```text
planning/documentation-reusable-candidate/
```

Current status:

```text
- candidate exists as a temporary non-canonical migration workspace;
- active documentation remains under planning/documentation/;
- ordinary project documentation updates should continue to use the active documentation layer;
- candidate content should be changed only for portability / reusable migration work.
```

Candidate guardrail file:

```text
planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md
```

Next expected migration step:

```text
Run responsibility-zone classification inside the candidate, starting with planning-docs-architecture-principles.md.
```

## 2B. Candidate Principles Classification State

A pre-split responsibility classification artifact is planned/created for the candidate principles file:

```text
planning/documentation-reusable-candidate/PRINCIPLES-RESPONSIBILITY-CLASSIFICATION.md
```

Purpose:

```text
- classify every section of planning/documentation-reusable-candidate/planning-docs-architecture-principles.md;
- separate reusable principles, scenario-driven profile material, Enman/project adapter mappings, examples and workflow/field-kit details;
- prepare F4 without editing or splitting the candidate principles file yet.
```

Boundary:

```text
This classification artifact does not split, rename, rewrite or move the principles file.
```

## 2C. Candidate Principles Split / Genericization State

F4 candidate split/genericization created the first split candidate outputs:

```text
planning/documentation-reusable-candidate/planning-docs-architecture-principles.md
planning/documentation-reusable-candidate/scenario-domain-slice-docs-profile.md
planning/documentation-reusable-candidate/enman-docs-adapter.md
planning/documentation-reusable-candidate/PORTABILITY-FOLLOWUPS.md
```

Purpose:

```text
- keep reusable documentation architecture invariants in candidate principles;
- extract scenario/domain/slice topology into a specialized reusable profile;
- preserve exact Enman paths, VKR/thesis mappings and register/evidence mappings in a candidate adapter;
- track source usage/status/local-global/examples/naming follow-ups separately instead of dumping them into the adapter.
```

Boundary:

```text
F4 does not switch canonical documentation ownership.
Active documentation remains under planning/documentation/.
F4 does not rewrite active planning/documentation/planning-docs-architecture-principles.md.
F4 does not create the source usage field kit or rewrite status/local-global/source-usage workflows; those are deferred to F5.
```

## 2D. Candidate Field Kit / Project Instance State

F5 candidate field-kit split separated setup, workflow, project-instance and example responsibilities for the deferred status/local-global/source-usage areas.

Created reusable candidate field kits:

```text
planning/documentation-reusable-candidate/status-reconciliation-field-kit.md
planning/documentation-reusable-candidate/shared-visibility-map-field-kit.md
planning/documentation-reusable-candidate/source-usage-cascade-field-kit.md
```

Created candidate Enman project-instance files:

```text
planning/documentation-reusable-candidate/enman-status-evidence-profile.md
planning/documentation-reusable-candidate/enman-shared-visibility-map.md
planning/documentation-reusable-candidate/enman-source-usage-cascade-profile.md
```

Created candidate examples:

```text
planning/documentation-reusable-candidate/examples/STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md
planning/documentation-reusable-candidate/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md
planning/documentation-reusable-candidate/examples/SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md
planning/documentation-reusable-candidate/examples/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md
```

Boundary:

```text
F5 does not promote Enman project-instance files into active planning/ root.
F5 does not switch canonical documentation ownership.
F5 does not rewrite active planning/documentation/ workflows.
```

## 2E. Candidate Navigation / Read-Order Cleanup State

F6A cleaned candidate navigation/read order after F4/F5.

Changed meaning:

```text
- planning/documentation-reusable-candidate/README.md is now a candidate index, not a copied active docs index;
- candidate read order routes to candidate files first;
- active docs references are marked as related active owners;
- F6A does not promote Enman project-instance files into active planning/ root;
- F6A does not switch canonical documentation ownership;
- F6A does not delete the source usage governance bridge.
```

Primary candidate navigation is now:

```text
planning/documentation-reusable-candidate/README.md
```

Candidate guardrail remains:

```text
planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md
```

## 3. Principles File Role Decision

The principles file should own high-level documentation architecture invariants.

Principles describe:

- how the documentation system should be structured;
- what must remain true;
- what must not be violated;
- why ownership and source-of-truth boundaries exist;
- what file-type responsibilities are and why they must stay separate.

Principles should not own:

- exact workflow steps;
- exact template shapes;
- exact concrete project paths;
- exact use-case or command routes;
- large examples;
- current project status inventory;
- project-specific source/evidence/register mappings.

Short definition:

> Principles own stable architecture constraints and design invariants. Workflows own repeated processes. Templates own exact shapes. Responsibility maps own placement. Adapters/profiles own concrete project configuration. Examples demonstrate usage but do not own rules.

## 4. File-Type Taxonomy Decision

The reusable documentation layer should explicitly distinguish these file types.

| File type | Owns | Does not own |
|---|---|---|
| Principles | stable invariants, design constraints, file-type theory | workflow steps, concrete paths, example bodies |
| Workflow | repeated operational process | exact template shape, project-specific config unless it is a project workflow |
| Field Kit | setup toolkit for deriving project-specific workflows/profiles/adapters | ordinary repeated operational process after setup is done |
| Template | exact artifact/output shape | process or routing logic |
| Responsibility map | ownership and placement routing | detailed process or examples |
| README / index | navigation, read order, short file purpose summaries | deep logic, current-state inventory |
| Use-case map | user action/command route, expected output, permission boundary | full workflow logic |
| Adapter / profile | concrete project configuration | universal principles |
| Example | demonstrated application | source-of-truth logic or routing |
| Action log | completed logical documentation actions | future tasks |
| PMR / task register | deferred/waiting tasks and reminders | completed action history |

## 5. Field Kit Decision

`Field Kit` should become a separate file type.

A field kit is not the same as a workflow.

Workflow:

- used repeatedly for an operational process;
- tells the agent/person how to do a recurring task.

Field Kit:

- used to set up project-specific artifacts;
- contains setup questions, decision tables, candidate shapes and checks;
- helps derive a project-specific workflow/profile/adapter/register;
- should usually be used once per project or when project architecture changes.

Example distinction:

| Artifact | Type | Role |
|---|---|---|
| `source-usage-cascade-field-kit.md` | Field Kit | helps define source/consumer model, cascade triggers and register shape for a project |
| `project-source-usage-cascade-workflow.md` | Project workflow | repeated cascade review process for a specific project |
| `SOURCE-USAGE-REGISTER-TEMPLATE.md` | Template | exact table/row shape |
| `project-docs-adapter.md` | Adapter/profile | concrete project sources, paths and mappings |

Guardrail:

> A field kit may contain setup questions and candidate shapes, but once a project-specific workflow/profile exists, ordinary project work should use the project-specific artifact, not re-run the field kit by default.

## 6. Reusable vs Specialized vs Project-Specific Classification

Do not move text to a project adapter merely because it contains a project path or Enman term.

First extract the reusable idea.

Classification levels:

| Level | Meaning | Example |
|---|---|---|
| Fully reusable principle | applies to any documentation system or AI-assisted docs layer | local details that affect future work must be globally discoverable |
| Specialized reusable profile | applies to a class of projects, but not all docs systems | scenario → DATA/behavior → domain → slice → tests/evidence |
| Project adapter/profile | exact project mapping/configuration | in Enman, scenario specs live under `planning/diagrams/...` |
| Example | concrete demonstration | SC-13D / AgreementProposalExchange cascade example |
| Workflow detail | operational steps | how to perform reconciliation or sync |
| Template detail | exact shape | exact table columns |

Review rule:

> If a paragraph contains Enman paths or terms, extract the underlying reusable principle first. Then decide whether the remaining concrete mapping belongs to a specialized profile, project adapter or example.

## 7. Scenario/Domain/Slice Decision

Scenario/domain/slice architecture is not fully universal, but it is also not necessarily Enman-only.

Target classification:

| Content | Target |
|---|---|
| “A docs system should define layers and dependency direction.” | fully reusable principles |
| scenario → DATA/behavior → domain → slice → testing/evidence | scenario-driven reusable profile |
| exact Enman folders, IDs, scenario names and domain concepts | Enman/project adapter or examples |

Potential specialized profile:

```text
scenario-domain-slice-docs-profile.md
```

Purpose:

> Reusable profile for app/product projects where documentation flows from scenario/source behavior into normalized data/behavior items, domain interpretation, implementation slices, verification and external output.

## 8. Enman Adapter Meaning

The project adapter is not a dumping ground for every paragraph containing an Enman reference.

It owns concrete project configuration:

- repo/provider/branch conventions;
- project layer vocabulary;
- exact source paths;
- evidence path map;
- shared visibility map;
- source usage profile;
- project-specific output layers;
- example links.

Example split:

```text
Original concrete statement:
Use planning/slices/ for slice scope truth.

Reusable principle:
A project must define where implementation-planning scope truth lives.

Scenario-driven profile:
Scenario-driven app projects may use slice/scope docs for implementation planning.

Enman adapter:
In Enman, slice scope docs live in planning/slices/.
```

## 9. VKR Decision

VKR/thesis is Enman-specific and should not remain as a named universal reusable layer.

Reusable layer keeps only the generic principle:

> Internal working documentation may use internal workflow terms, but external/public-facing outputs need clean audience-safe wording.

Scenario-driven profile may mention:

> Some projects have external/public-facing output layers that consume internal planning docs.

Enman adapter owns:

- VKR/thesis wording rules;
- `planning/vkr-clean-reference.md`;
- `planning/thesis/`;
- any VKR-specific mapping.

## 10. Treatment Of Three Key Files

### 10.1 `status-reconciliation-workflow.md`

Reusable role:

- should become a status/evidence reconciliation setup workflow or field-kit-like workflow;
- applicable only when the documentation domain has implementation/current evidence;
- if no implementation/evidence layer exists, mark not applicable or define another evidence model.

Principles should keep:

> Current-state claims must be evidence-checkable when the documentation domain has evidence.

Workflow/field kit should own:

- applicability gate;
- evidence order;
- status labels;
- reconciliation steps;
- prompt/update impact.

Project adapter should own:

- exact evidence paths;
- generated artifacts;
- test paths;
- project-specific status examples.

### 10.2 `local-global-documentation-sync-workflow.md`

Reusable role:

- should remain a reusable workflow;
- local/global visibility is relevant to nearly any documentation system.

Principles should keep:

> Local details that affect future work must become globally discoverable or be explicitly marked local-only.

Workflow should own:

- local vs global classification;
- question/assumption/status fields;
- local-only reasons;
- back-reference rule;
- preflight checklist.

Project adapter should own:

- Shared Visibility Map:
  - local discovery type → shared index/register/navigation target.

### 10.3 `source-usage-cascade-governance-plan.md`

Reusable role:

- should probably become a field kit, not a full workflow yet;
- source/consumer/cascade concepts are broadly reusable;
- concrete scenario/domain/slice cascade is a specialized profile/example.

Principles should keep:

> Downstream docs that consume upstream sources need visible source/consumer relationships for targeted cascade review.

Field kit should own:

- source artifact/scope;
- consumer artifact/scope;
- reviewed_against;
- source_status vs sync_status;
- cascade trigger;
- review outcome;
- candidate row shape;
- pilot/exit criteria.

Scenario-driven profile should own:

- scenario → DATA/behavior → domain → slice → testing/evidence as one reusable topology.

Enman adapter/examples should own:

- SC-13D;
- AgreementProposalExchange;
- exact pilot register paths;
- concrete Enman cascade examples.

## 11. Responsibility-Zone Review Workflow Decision

A reusable responsibility-zone review workflow should own this process:

```text
documentation-responsibility-zone-review-workflow.md
```

It should own the repeatable process for:

- reviewing an existing documentation layer;
- separating universal reusable principles from specialized profiles and project-specific adapters;
- extracting principles before moving concrete text;
- avoiding adapter dumping;
- preserving examples;
- planning candidate-copy migration.

Expected output:

- classification table;
- proposed moves;
- profile/adapter/example boundaries;
- unresolved questions;
- delivery safety classification.

This can become a use-case route in use-case maps, but the logic belongs in the workflow.

## 12. Near-Term Plan Without Splitting Principles Yet

Do not split principles yet.

First changes should be limited to what can be done before the principles split:

1. Create a preservation note / decision capture file.
2. Update or plan the principles role definition.
3. Add Field Kit to file-type taxonomy.
4. Add Field Kit / Adapter/Profile boundaries to responsibility guidance.
5. Add the responsibility-zone review workflow as the owner for portability/reusable migration review.
6. Clarify command semantics for `арх`.
7. Mark VKR as Enman-specific in migration plan, but do not move active docs yet.
8. Plan candidate-copy migration.

## 13. Suggested Next Batch Scope

Suggested next batch name:

```text
Batch F1 — Documentation layer migration decisions and field-kit role model
```

Scope:

- capture long decisions in a repo document;
- clarify `арх` semantics if command owner is updated;
- define Principles responsibility;
- introduce Field Kit file type;
- add the responsibility-zone review workflow;
- no active principles split;
- no scenario/domain/slice profile extraction yet;
- no Enman adapter extraction yet.

## 14. Open Questions

1. Where should this preservation note live?
   - Candidate: `planning/documentation/documentation-layer-reusability-decisions.md`
   - Candidate: `planning/documentation/documentation-layer-portability-migration-plan.md`
   - Candidate: outside active docs, inside future candidate workspace.

2. Should the review workflow be narrow portability-only or general responsibility-zone review?
   - Decision: use the general `documentation-responsibility-zone-review-workflow.md`.
   - Reason: the same method applies to portability migration, owner-boundary review and future responsibility-zone audits.

3. Should `Field Kit` file type be added to active docs now?
   - Pro: needed before source usage/status/local-global files are reshaped.
   - Con: if we only change candidate later, active docs remain unchanged until migration.

4. Should `арх` semantics be fixed immediately in the active command/use-case map?
   - Pro: prevents future misunderstanding.
   - Con: touches active response/command routing again.

## 15. Current Preferred Direction

Preferred order:

1. Create this decision capture file as a standalone discussion artifact.
2. Plan a small repo batch that adds:
   - principles role clarification;
   - Field Kit file type;
   - adapter/profile type clarification;
   - responsibility-zone review workflow;
   - `арх` semantics correction.
3. After that, create candidate copy / migration workspace.
4. Then classify and split principles inside the candidate.

## 16. Post-Review Additions / Strengthened Agreements

This section records points that were present in the discussion but needed to be made more explicit in this preservation file.

### 16.1 Documentation-Layer Review Boundary

The portability review is about the **documentation layer architecture**, not about reviewing scenario, slice, domain, API or diagram content itself.

When a reusable docs-layer file references paths such as:

```text
planning/slices/
planning/diagrams/
planning/api/
planning/testing/
```

the review question is not:

```text
Is the slice/scenario/API content correct?
```

The review question is:

```text
Why does the documentation layer reference this path?
Is the reference:
- a reusable principle?
- a scenario-driven profile concept?
- a project adapter mapping?
- an example link?
- workflow detail?
```

Guardrail:

> Do not drift into reviewing project content while reviewing documentation-layer portability. Only classify why the docs layer depends on or mentions that content.

### 16.2 Full Candidate Copy Baseline

For copy-first migration, the initial candidate should be a **full baseline copy** of the active documentation layer unless explicitly narrowed later.

Preferred first migration step:

```text
copy planning/documentation/
  -> planning/documentation-reusable-candidate/
```

Reason:

- preserves all current knowledge;
- enables diff-based cleanup;
- avoids silently losing project-specific or historical information;
- lets us classify before moving/removing anything.

The candidate can later remove, relabel, split or move project-specific files, but the first copy should preserve the whole baseline.

### 16.3 Where To Record The Principles Role

The principles role should be recorded in more than one place, with different depth:

| Place | What it should record |
|---|---|
| Principles file itself | self-definition: principles own invariants, constraints, what must/must not happen |
| Documentation responsibility map | placement rule: principles own file-type theory/invariants, not steps/templates/project config |
| Responsibility-zone review workflow | process: how to classify text as reusable/specialized/project-specific/workflow/detail |
| Use-case map | optional route: if user asks to review docs for portability, activate the responsibility-zone review workflow |

This avoids putting the whole classification algorithm into the principles file.

### 16.4 Principles May Define File Types, But Not Concrete Placement Rows

The principles file may define the general taxonomy of file types:

- principles;
- workflow;
- field kit;
- template;
- responsibility map;
- README/index;
- use-case map;
- adapter/profile;
- example;
- action log;
- task/maintenance register.

But the principles file should not own concrete placement rows.

Correct split:

```text
Principles:
  defines what a workflow is and why it differs from a field kit.

Responsibility map:
  says which concrete file owns a given workflow/field-kit/template/responsibility area.

Workflow:
  says how to perform the process.

Template:
  defines exact shape.

Adapter:
  defines concrete project configuration.
```

### 16.5 Examples Should Be Separate, Not Embedded

Reusable principles, workflows and field kits should avoid embedding large Enman examples directly.

Preferred pattern:

```text
Reusable owner file:
  - defines principle/process/setup questions
  - links to examples

Example file:
  - shows Enman/current-project application

Project adapter/profile:
  - stores exact project mapping and paths
```

This applies especially to:

- status reconciliation examples;
- local/global sync examples;
- source usage/cascade examples;
- scenario/domain/slice profile examples.

### 16.6 Status Reconciliation Evidence Is Domain-Specific

Status reconciliation is not always “implementation vs docs.”

For software/application projects, evidence may be:

- code;
- tests;
- generated artifacts;
- migrations;
- runtime screenshots;
- deployed behavior.

For other documentation domains, evidence may be different:

| Domain | Possible evidence/current reality |
|---|---|
| study notes / repetition system | source notes, repetition schedule, learning state, reviewed cards |
| day planning | calendar, task state, completed/blocked items |
| research notes | source papers, excerpts, citation map, experiment notes |
| non-implementation docs | accepted sources, current user decisions, published notes |

Reusable rule:

> If a documentation domain has current-state claims, it must define the evidence/current-reality model that can verify those claims. If there is no such model, implementation-style status reconciliation is not applicable.

### 16.7 Do Not Split Principles Before Candidate Classification

The principles split should happen only after candidate classification.

Order:

1. Copy active docs layer to reusable candidate.
2. Preserve decisions and role model.
3. Add/prepare responsibility-zone review workflow.
4. Classify principles sections at paragraph/section level.
5. Only then split into:
   - fully reusable principles;
   - scenario-driven profile;
   - project adapter/profile;
   - examples or workflow/field-kit details.

Guardrail:

> Do not split the active principles file directly before the candidate copy and classification pass.

### 16.8 `арх` Must Be Treated As Source-State Even During Planning

If the user says `арх` while asking for planning or review, it means:

- use the provided archive as the latest repo state;
- do not infer that a deliverable package is requested;
- do not say “I will generate archive” unless separately asked.

This applies even when the next logical work might eventually be archive-based.

### 16.9 Near-Term Batch Should Avoid Scenario/Profile Split

The next small repo batch should stay pre-split.

It may include:

- decision capture file;
- principles role clarification;
- Field Kit file type;
- adapter/profile type clarification;
- responsibility-zone review workflow;
- `арх` command semantics correction.

It should not yet include:

- scenario/domain/slice profile extraction;
- Enman adapter extraction;
- principles file split;
- moving VKR files;
- moving source usage pilots or sync notes.

### 16.10 VKR Handling

The current decision is stronger than just “genericize VKR wording.”

VKR/thesis as a named layer, folder or output model is Enman-specific.

Reusable layer keeps only:

```text
Internal working documentation should be separated from external/public-facing output wording.
```

Scenario-driven profile may say:

```text
Some projects have external-facing output layers that consume internal planning docs.
```

Enman adapter/profile owns:

- VKR/thesis as the concrete external output layer for Enman;
- `planning/vkr-clean-reference.md`;
- `planning/thesis/`;
- Enman-specific wording rules.

# Documentation Responsibility Map

Status: current local responsibility map for documentation layer  
Scope: routes documentation-layer information to the correct owner file

## 1. Purpose

Use this file after you already know that new information belongs to the documentation layer.

This file answers:

```text
Where inside planning/documentation/ should this information go?
Which documentation file owns this rule, process, prompt, note or navigation entry?
Which documentation files must be synchronized after the change?
```

This file does not choose between scenario/domain/slice/API/testing/VKR layers. For layer choice, use the root router:

```text
planning/planning-doc-responsibility-map.md
```

## 2. Authority

Canonical documentation architecture theory:

```text
planning/documentation/planning-docs-architecture-principles.md
```

Root layer routing:

```text
planning/planning-doc-responsibility-map.md
```

Documentation layer navigation:

```text
planning/documentation/README.md
```

This file owns only documentation-layer placement.

## 3. Documentation Layer File Responsibilities

| Information type | Owner file | Notes |
|---|---|---|
| Global planning-docs architecture principles | `planning-docs-architecture-principles.md` | Stable invariants, file-type theory, source-of-truth boundaries and no-duplication rules. Not workflow steps, concrete paths or project configuration. |
| Documentation-layer portability migration plan | `documentation-layer-portability-migration-plan.md` | Captures pre-split reusability decisions, migration phases and boundaries before candidate copy / principles split / adapter extraction. |
| Documentation responsibility-zone review process | `documentation-responsibility-zone-review-workflow.md` | How to review existing documentation content and classify reusable principles, specialized profiles, adapter mappings, examples, workflow details and template details. |
| Documentation-layer placement rules | `documentation-responsibility-map.md` | This file. Use when deciding where documentation-layer information belongs. |
| Documentation folder navigation / read order | `README.md` | Index only. It should list files and read order, not duplicate full rules. |
| Broad documentation update process | `documentation-update-workflow.md` | End-to-end process for updating docs after plan/approval. |
| Preflight plan format for broad docs changes | `documentation-update-plan-workflow.md` | What to show before changing broad docs/navigation/status/registers. |
| Local detail to shared index/register sync process | `local-global-documentation-sync-workflow.md` | How local notes/questions should become globally discoverable. |
| Status vs implementation evidence reconciliation | `status-reconciliation-workflow.md` | How to update docs when code/tests/generated artifacts changed. |
| Reviewable answer format and response-level commands | `reviewable-agent-output-and-commands-workflow.md` | Level 1/2/3 answers, sources/coverage, recheck/clarify/keep prev/no ch/section operations. |
| File Update Overview process | `file-update-overview-workflow.md` | Owns when and how to produce the final structured file-change summary for non-trivial file/docs/code update answers. |
| File Update Overview template | `FILE-UPDATE-OVERVIEW-TEMPLATE.md` | Owns the exact reusable File Update Overview block structure. |
| Use-case map creation/update workflow | `use-case-map-workflow.md` | Owns the reusable process for creating, updating and maintaining use-case maps. |
| Use-case map template | `USE-CASE-MAP-TEMPLATE.md` | Owns the exact reusable structure for concrete use-case maps. |
| Root use-case map setup field kit | `field-kits/root-use-case-map-field-kit.md` | Active reusable setup kit for deriving one concrete project root use-case map and common command clusters. Not a runtime router. |
| Scenario/domain/slice route setup field kit | `profiles/scenario-domain-slice-use-case-field-kit.md` | Active profile-specific setup kit for adding scenario/domain/slice route families to a project root use-case map. Not a second map. |
| Working example coverage decision process | `example-coverage-workflow.md` | Decides whether a new/changed template, workflow output, response command, output mode or draft format needs a working example. |
| Documentation-layer working example index | `examples/README.md` | Navigation and coverage index for documentation-layer examples. It does not own routing/source/output/permission logic. |
| Source usage cascade governance pilot plan | `source-usage-cascade-governance-plan.md` | Governance plan for source usage relationships, layer encapsulation, attention preservation and cascade-review pilots. Not the full future workflow. |
| Source usage pilot folder/index | `source-usage-pilots/README.md` | Navigation and rules for experimental pilot registers. Permanent register placement is deferred. |
| Source usage pilot registers | `source-usage-pilots/*.md` | Pilot dependency relationship tables used to test source usage row shape and cascade review. Not global final schema. |
| Documentation logical action log | `documentation-action-log.md` | Records significant completed documentation actions and why they happened. It does not own rules, workflows, task state or source truth. |
| Reusable documentation update prompt | `documentation-update-agent-prompt.md` | Derived prompt for other chats. Not canonical if it conflicts with governance docs. |
| Scoped documentation sync notes | `*sync-note.md` or future `sync-notes/` | Case-specific notes. Not reusable workflows or global principles. |

## 4. Before Adding New Documentation-Layer Information

Classify the new information:

```text
1. Information type:
   architecture principle / workflow step / field-kit setup guidance / adapter-profile mapping / responsibility-zone review / response command / plan format / prompt / sync note / navigation item / status rule / local-global sync rule / working example / example coverage decision / file update overview process / file update overview template / source usage governance / source usage pilot register / documentation action log entry.

2. Existing owner:
   Which file above already owns this type?

3. New file needed?
   Only create a file if no owner exists or the existing owner would become overloaded.

4. Navigation impact:
   Does README.md need to list the file or adjust read order?

5. Root router impact:
   Does planning/planning-doc-responsibility-map.md need to point to this local owner?

6. Duplication risk:
   Would this repeat a rule already owned by another file?

7. Example coverage impact:
   Does adding or changing this information create or change a reusable template, output shape, command behavior or draft format that needs an example coverage decision?

8. Source usage impact:
   Does this information introduce or change source usage relationships, cascade review, stale-reference handling or pilot register shape?

9. Responsibility-zone / portability impact:
   Does this review or move existing content between reusable principles, specialized profiles, project adapters, workflows, field kits, templates or examples?

10. Action log impact:
   Is this a significant logical documentation action that should be recorded in documentation-action-log.md?
```

## 5. Conflict Rules

If documentation-layer files conflict:

```text
- planning-docs-architecture-principles.md wins for stable architecture invariants and file-type theory.
- documentation-layer-portability-migration-plan.md wins for the staged documentation-layer portability migration decision record until a later canonical migration plan replaces it.
- documentation-responsibility-zone-review-workflow.md wins for the process of classifying existing content into responsibility zones.
- documentation-responsibility-map.md wins for documentation-layer placement.
- planning/planning-doc-responsibility-map.md wins for choosing the planning layer.
- workflow files win for their own repeated process steps.
- field-kit files win for setup guidance in their specific setup area; project-specific workflows/profiles win after they are derived and accepted.
- adapter/profile files win for concrete project mappings after they are created.
- file-update-overview-workflow.md wins for File Update Overview process.
- FILE-UPDATE-OVERVIEW-TEMPLATE.md wins for File Update Overview shape.
- use-case-map-workflow.md wins for reusable use-case-map maintenance process.
- USE-CASE-MAP-TEMPLATE.md wins for exact reusable use-case-map shape.
- example-coverage-workflow.md wins for example coverage decision steps.
- examples/README.md is an index and does not override owner workflows, templates or use-case rows.
- source-usage-cascade-governance-plan.md wins for pilot governance until the full cascade workflow exists.
- source-usage-pilots/*.md are pilot artifacts and do not override source files, maps, drafts or future permanent registers.
- documentation-action-log.md is historical/explanatory and does not override owner docs, workflows, templates, use-case rows or PMR entries.
- reviewable-agent-output-and-commands-workflow.md wins for answer format and response-level commands.
- documentation-update-agent-prompt.md is derived/supporting and does not override canonical docs.
- scoped sync notes do not override reusable workflow files.
- README.md is navigation and does not override canonical rules.
```

## 6. When To Create A New Documentation File

Create a new documentation-layer file only when:

```text
- the information is reusable;
- no existing owner file fits;
- keeping it in an existing file would overload that file;
- the new file will be discoverable from README.md;
- the new file has a clear type and suffix.
```

Suggested suffixes:

| File type | Suggested suffix |
|---|---|
| Workflow | `*-workflow.md` |
| Field kit | `*-field-kit.md` |
| Adapter / profile | `*-adapter.md`, `*-profile.md` or project-specific profile file when approved |
| Responsibility map | `*-responsibility-map.md` |
| Template | `*-template.md` or uppercase `<THING>-TEMPLATE.md` for reusable exact output shapes |
| Examples index / examples folder navigation | `examples/README.md` or `*-examples.md` |
| Source usage pilot folder navigation | `source-usage-pilots/README.md` |
| Source usage pilot register | `source-usage-pilots/<scope>-source-usage-register.md` |
| Documentation action log | `documentation-action-log.md` |
| Scoped sync note | `*-sync-note.md` |
| Reusable prompt | `*-agent-prompt.md` |
| Architecture principles | `*-architecture-principles.md` |

## 7. Do Not

```text
- Do not put global architecture theory into workflow files.
- Do not put workflow steps into architecture principles unless they are only high-level principles.
- Do not put field-kit setup guidance into a repeated workflow unless the workflow explicitly owns setup mode.
- Do not treat project adapter/profile mappings as universal principles.
- Do not duplicate owner tables in multiple files.
- Do not put command routing, source-mode, output-mode or permission logic into example files.
- Do not put File Update Overview trigger/shape logic into examples or use-case rows; link to the workflow/template owners.
- Do not put reusable use-case-map workflow/template logic into a concrete use-case map; link to use-case-map-workflow.md and USE-CASE-MAP-TEMPLATE.md.
- Do not move a paragraph into a project adapter only because it contains a concrete path; extract the reusable principle first.
- Do not split or migrate active docs-layer responsibilities before the approved portability/candidate workflow says to do so.
- Do not treat source usage pilot registers as permanent global schema before the pilot is reviewed.
- Do not use the action log as the source of truth for rules or unresolved tasks.
- Do not treat reusable prompts as canonical rules.
- Do not treat scoped sync notes as reusable workflows.
- Do not add a new file without updating README.md when it must be discoverable.
```

## 8. Success Criteria

The documentation layer is well-routed when:

```text
- a new chat can choose the correct documentation owner file without guessing;
- README.md shows navigation and read order;
- architecture principles hold invariants and file-type theory;
- this map handles documentation-layer placement;
- workflow files stay process-focused;
- field kits stay setup-focused and do not replace accepted project workflows/profiles;
- responsibility-zone reviews can classify reusable principles, specialized profiles, adapter mappings, examples and workflow/template details;
- File Update Overview process and shape have clear owners;
- use-case-map workflow and template responsibilities have clear owners;
- example coverage decisions are made by the example coverage workflow;
- examples remain supporting artifacts and link to their owner files instead of copying logic;
- source usage cascade pilots are discoverable and clearly marked experimental;
- significant logical documentation actions have concise action-log entries;
- prompts and sync notes are clearly supporting/scoped artifacts.
```

## 8A. Historical F7C Overlay Note

F7C historically mirrored active reusable use-case-map and command-routing additions into the former candidate workspace before the F7D/F7E switch.

These files are now active reusable setup/workflow/template owners:

```text
field-kits/root-use-case-map-field-kit.md
profiles/scenario-domain-slice-use-case-field-kit.md
use-case-map-workflow.md
USE-CASE-MAP-TEMPLATE.md
documentation-update-workflow.md
```

Historical note: F7C mirrored active reusable additions into the former candidate workspace before the F7D folder switch. After F7D/F7E, `planning/documentation/` is the active reusable documentation layer.

## Portable Starter-Kit Adaptation Owner

```text
planning/documentation/PORTABLE-STARTER-KIT.md
```

Owns one-time guidance for copying/adapting the reusable documentation layer into a new project or documentation domain. It does not own normal daily documentation updates after the target project has its root use-case map and root profiles/maps.

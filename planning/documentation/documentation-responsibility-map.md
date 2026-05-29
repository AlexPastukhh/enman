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
| Global planning-docs architecture principles | `planning-docs-architecture-principles.md` | Theory: layers, file types, source-of-truth boundaries, no-duplication rules. Not workflow steps. |
| Documentation-layer placement rules | `documentation-responsibility-map.md` | This file. Use when deciding where documentation-layer information belongs. |
| Documentation folder navigation / read order | `README.md` | Index only. It should list files and read order, not duplicate full rules. |
| Broad documentation update process | `documentation-update-workflow.md` | End-to-end process for updating docs after plan/approval. |
| Preflight plan format for broad docs changes | `documentation-update-plan-workflow.md` | What to show before changing broad docs/navigation/status/registers. |
| Local detail to shared index/register sync process | `local-global-documentation-sync-workflow.md` | How local notes/questions should become globally discoverable. |
| Status vs implementation evidence reconciliation | `status-reconciliation-workflow.md` | How to update docs when code/tests/generated artifacts changed. |
| Reviewable answer format and response-level commands | `reviewable-agent-output-and-commands-workflow.md` | Level 1/2/3 answers, sources/coverage, recheck/clarify/keep prev/no ch/section operations. |
| Working example coverage decision process | `example-coverage-workflow.md` | Decides whether a new/changed template, workflow output, response command, output mode or draft format needs a working example. |
| Documentation-layer working example index | `examples/README.md` | Navigation and coverage index for documentation-layer examples. It does not own routing/source/output/permission logic. |
| Source usage cascade governance pilot plan | `source-usage-cascade-governance-plan.md` | Governance plan for source usage relationships, layer encapsulation, attention preservation and cascade-review pilots. Not the full future workflow. |
| Source usage pilot folder/index | `source-usage-pilots/README.md` | Navigation and rules for experimental pilot registers. Permanent register placement is deferred. |
| Source usage pilot registers | `source-usage-pilots/*.md` | Pilot dependency relationship tables used to test source usage row shape and cascade review. Not global final schema. |
| Reusable documentation update prompt | `documentation-update-agent-prompt.md` | Derived prompt for other chats. Not canonical if it conflicts with governance docs. |
| Scoped documentation sync notes | `*sync-note.md` or future `sync-notes/` | Case-specific notes. Not reusable workflows or global principles. |

## 4. Before Adding New Documentation-Layer Information

Classify the new information:

```text
1. Information type:
   architecture principle / workflow step / response command / plan format / prompt / sync note / navigation item / status rule / local-global sync rule / working example / example coverage decision / source usage governance / source usage pilot register.

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
```

## 5. Conflict Rules

If documentation-layer files conflict:

```text
- planning-docs-architecture-principles.md wins for global architecture theory.
- documentation-responsibility-map.md wins for documentation-layer placement.
- planning/planning-doc-responsibility-map.md wins for choosing the planning layer.
- workflow files win for their own process steps.
- example-coverage-workflow.md wins for example coverage decision steps.
- examples/README.md is an index and does not override owner workflows, templates or use-case rows.
- source-usage-cascade-governance-plan.md wins for pilot governance until the full cascade workflow exists.
- source-usage-pilots/*.md are pilot artifacts and do not override source files, maps, drafts or future permanent registers.
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
| Responsibility map | `*-responsibility-map.md` |
| Template | `*-template.md` |
| Examples index / examples folder navigation | `examples/README.md` or `*-examples.md` |
| Source usage pilot folder navigation | `source-usage-pilots/README.md` |
| Source usage pilot register | `source-usage-pilots/<scope>-source-usage-register.md` |
| Scoped sync note | `*-sync-note.md` |
| Reusable prompt | `*-agent-prompt.md` |
| Architecture principles | `*-architecture-principles.md` |

## 7. Do Not

```text
- Do not put global architecture theory into workflow files.
- Do not put workflow steps into architecture principles unless they are only high-level principles.
- Do not duplicate owner tables in multiple files.
- Do not put command routing, source-mode, output-mode or permission logic into example files.
- Do not treat source usage pilot registers as permanent global schema before the pilot is reviewed.
- Do not treat reusable prompts as canonical rules.
- Do not treat scoped sync notes as reusable workflows.
- Do not add a new file without updating README.md when it must be discoverable.
```

## 8. Success Criteria

The documentation layer is well-routed when:

```text
- a new chat can choose the correct documentation owner file without guessing;
- README.md shows navigation and read order;
- architecture principles hold theory;
- this map handles documentation-layer placement;
- workflow files stay process-focused;
- example coverage decisions are made by the example coverage workflow;
- examples remain supporting artifacts and link to their owner files instead of copying logic;
- source usage cascade pilots are discoverable and clearly marked experimental;
- prompts and sync notes are clearly supporting/scoped artifacts.
```

# Example Coverage Workflow

Status: current documentation-layer workflow  
Doc version: v0.1.0
Scope: decide when reusable workflows, templates, response commands, output modes and draft formats need working examples

## 1. Purpose

This workflow prevents reusable documentation behavior from becoming ambiguous for future chats.

It answers:

```text
A workflow, template, response command, output mode or draft format was added or changed.
Do we need a working example?
Where should that example be indexed?
What do we record if no example is added?
```

This file does not define command routing, source modes, output modes or permission boundaries. Those stay in the use-case map and owner workflows.

## 2. When To Use

Use this workflow when adding or changing:

```text
- a reusable template;
- a workflow with an expected output;
- a response command;
- an output mode;
- a draft format;
- an archive/package format;
- an examples index;
- a repeated chat failure pattern caused by missing examples.
```

Do not use it for:

```text
- one-off wording edits;
- pure navigation rows with no output shape;
- typo fixes;
- local notes that are not reusable;
- examples that would only duplicate another current example.
```

## 3. Core Rule

```text
If there is a reusable template, a working example is required by default.
```

A working example may be skipped only when:

```text
- the owner file is routing-only;
- the example would duplicate another current example;
- the behavior is trivial and already fully demonstrated by an existing example;
- the owner workflow/template is intentionally unstable and the example is deferred;
- the user explicitly scopes the current update to infrastructure only, with examples to follow.
```

If no example is added, record the reason in the relevant examples index or update plan.

## 4. Decision Steps

```text
1. Identify the changed artifact.
2. Classify the artifact type:
   template / workflow / response command / output mode / draft format / index / project-type/profile-specific reusable example / other.
3. Identify the owner file.
4. Decide whether the artifact has an expected output shape or reusable behavior.
5. Decide example coverage:
   - required;
   - already covered;
   - not needed;
   - deferred.
6. If required, decide where the example belongs.
7. If not needed or deferred, record the reason.
8. Check Docs DRY: do not copy routing/source/output/permission logic into the example.
9. Update navigation/index files if a new example file or examples folder is created.
```

## 5. Coverage Decision Values

Use one of these values in plans or examples indexes:

| Decision | Meaning | Required follow-up |
|---|---|---|
| `required` | A new working example should be added. | Add example or create a tracked follow-up. |
| `covered` | A current example already demonstrates this behavior. | Link to the existing example. |
| `not needed` | No example is useful for this artifact. | Record reason. |
| `deferred` | Example is useful but intentionally delayed. | Record reason and target condition. |

## 6. Where Examples Belong

Default placement:

```text
planning/<layer>/examples/
```

For documentation-layer examples:

```text
planning/documentation/examples/
```

Keep examples near the layer that owns the workflow/template/output. Do not create a global top-level examples folder unless a future architecture update explicitly chooses that model.


Example reuse placement:

```text
fully reusable example:
  planning/documentation/examples/

profile-specific reusable example:
  planning/documentation/examples/profiles/<profile-name>/
  or a clearly marked legacy/project-type folder until migrated

project-local example:
  project/root-local examples area chosen by the concrete project
```

A profile-specific reusable example may support concrete root routes only after the user/project accepts that the example fits the project type/spirit. Do not silently wire candidate examples into a concrete UCM.


## 7. Example File Responsibility

A working example may contain:

```text
- a short statement of what it demonstrates;
- a filled or partial-filled output example;
- a link to the owner workflow/template/use case;
- a short note about why the example is valid.
```

A working example must not own:

```text
- command aliases;
- source-mode rules;
- output-mode rules;
- permission boundaries;
- workflow activation;
- layer placement rules;
- source/version cascade rules.
```

If that logic is needed for understanding, link to the owner file or section instead of copying it.

## 8. Examples Index Responsibility

An examples index is navigation and coverage tracking only.

Recommended columns:

```text
Example ID | Type | Owner | Related use-case/workflow | Status | File | Missing/deferred reason
```

Do not add source-mode, output-mode, permission or command-alias columns to examples indexes unless a future source/version governance update explicitly assigns that responsibility there.

## 9. Output Shape For Documentation Update Plans

When this workflow is activated, include a compact decision block:

```text
Example coverage:
- Changed artifact:
- Owner:
- Decision: required / covered / not needed / deferred
- Reason:
- Example index/file:
- Follow-up, if any:
```

Use this block in plans, archive summaries or review answers when example coverage affects the update.

## 10. Success Criteria

Example coverage works when:

```text
- new templates do not appear without either a working example or a recorded reason;
- command/output/draft behavior has at least one practical demonstration when useful;
- examples are discoverable from an examples index;
- examples do not become second sources of truth;
- example fit is accepted by the user/project before profile-specific examples are wired into concrete routes;
- future chats can find the correct output shape without rereading an entire conversation.
```

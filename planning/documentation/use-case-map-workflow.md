# Use-Case Map Workflow

Status: active reusable documentation-layer workflow  
Doc version: v0.1.0
Scope: how to create, update and maintain use-case maps as reusable routing artifacts across systems

## 1. Purpose

This workflow owns the reusable process for working with use-case maps.

A use-case map is a routing artifact that connects user-visible actions to the docs, workflows, templates, sources, output expectations and permission boundaries that should be used for the action.

Use this workflow for:

```text
- creating a new use-case map for a system;
- updating an existing use-case map;
- adding or changing command/continuation rows;
- adding or changing primary use-case rows;
- deciding which owner workflow/template a row should reference;
- keeping concrete use-case maps from becoming workflow or template owners.
```

Use the template for exact shape:

```text
planning/documentation/USE-CASE-MAP-TEMPLATE.md
```

Setup field kit:

```text
planning/documentation/field-kits/root-use-case-map-field-kit.md
```

Use the field kit when creating or restructuring a root use-case map. Use this workflow for repeated maintenance after the map exists.

## 1A. Setup vs Maintenance Boundary

```text
Field kit
  -> one-time / rare setup for deriving a project root use-case map.

Workflow
  -> repeated maintenance after the project map exists.

Template
  -> exact reusable Markdown shape.

Root use-case map
  -> concrete project routes and commands.
```

A project should normally have one concrete root use-case map.

Do not create a generic reusable use-case map inside the documentation layer. Put reusable setup guidance in the field kit and keep concrete routing in the project root map.

## 2. What A Use-Case Map Owns

A use-case map owns routing for a concrete system or planning surface.

It answers:

```text
- User says X.
- What task type is this?
- Does active context matter?
- How broad should traversal be?
- Which read source mode should be used?
- Which workflows/templates are activated?
- Which docs or sources should be read?
- What is the source of obligation?
- What output is expected?
- What requires explicit permission?
```

The map should make a route reviewable without copying the full workflow logic it routes to.

## 3. What A Use-Case Map Does Not Own

A use-case map is not:

```text
- a full workflow;
- a template owner;
- a responsibility map;
- a README replacement;
- a source-of-truth for the workflow logic it references;
- a general action log;
- a task/reminder register.
```

If a row needs algorithmic steps, link or create a workflow.

If a row needs exact output shape, link or create a template.

If a row needs placement/ownership authority, link or update a responsibility map.

## 4. When To Create A Use-Case Map

Create a new use-case map when a system has repeated user-visible actions that need stable routing.

Good candidates:

```text
- planning system use cases;
- documentation update work;
- note/repetition systems;
- review workflows;
- command-heavy assistant workflows;
- any system where short user commands should resolve to predictable workflow/source/output choices.
```

Do not create a use-case map for one-off tasks that can be handled by a single workflow, README or template.

## 5. When To Update A Use-Case Map

Update an existing map when:

```text
- a new recurring user action appears;
- a command alias is accepted;
- expected output changes;
- required reads or owner workflows change;
- permission boundaries change;
- a route currently points to the wrong owner;
- repeated chat confusion shows that a route needs to be explicit.
```

Do not update a use-case map just because an implementation detail changed. Update the owner workflow/template/source doc instead unless the route itself changed.

## 6. Row Addition / Update Workflow

Before adding or changing a row:

```text
1. Identify the concrete user phrasing or command.
2. Decide whether this is a continuation command, primary use case or detailed trace.
3. Identify active-context behavior.
4. Decide traversal depth and read source mode.
5. Identify activated workflows/templates.
6. Identify required reads.
7. Identify the source of obligation.
8. Write the expected output.
9. Write the permission boundary.
10. Check that the row links to owner files instead of copying their logic.
```

For broad changes, explain why the map itself needs the update and why the owner workflow/template is not the only file changed.

## 6A. Setup-Time Example-Fit Pass

When creating or substantially restructuring a project root use-case map:

```text
1. Read `planning/documentation/examples/README.md`.
2. Identify fully reusable examples that clearly apply to the selected routes/output modes.
3. Identify profile-specific reusable examples that appear to match the project type, workflow style or route families.
4. Show candidate profile-specific examples to the user/project.
5. Record only accepted examples in concrete UCM route rows or detailed traces.
6. Keep examples as demonstration reads only; do not make them command/source/output/permission owners.
```

For ordinary maintenance of an already-established map, do not reread every example by default. Check examples only when the changed route/output/template behavior needs example coverage or when the user asks to review example fit.

## 7. Owner Workflow / Template Linking Rule

Use-case rows should name owner files, not duplicate them.

Use this pattern:

```text
Activated workflows
  workflow names or paths that own the algorithm.

Required reads
  files that must be inspected for this route.

Source of obligation
  root/router/principle/workflow that makes those reads or outputs required.

Expected output
  short description of output shape, with owner template/workflow named when relevant.
```

If a row starts to need long instructions, that is a sign that a workflow file should own the steps.

## 8. Expected Output Rule

Expected output should be reviewable but short.

Good examples:

```text
Level 2 update plan + File Update Overview when files are planned/changed/reviewed.
Replacement package ZIP with MANIFEST.md, APPLY.md and complete replacement files.
Draft update with differences block when previous draft exists.
```

Bad examples:

```text
Full workflow copied into the row.
Long template embedded in the row.
Permission rules copied from another owner file.
```

## 9. Command Alias / Continuation Command Rule

For repeated or continuation commands, keep the row focused on active-context behavior.

A continuation row should explain:

```text
- how it behaves when active context exists;
- how it behaves when no active context exists;
- traversal depth;
- read source mode;
- expected output.
```

Do not make command aliases silently change permissions, source requirements or repository state.

## 10. Cross-System Reuse Rule

Reusable material should be extracted as:

```text
- field kits for setup choices;
- workflows for repeated maintenance;
- templates for exact shape;
- examples for demonstrations.
```

Do not extract a concrete root use-case map into a generic reusable map.

Each project should keep one concrete root map that imports/adapts reusable setup guidance.

For scenario/domain/slice route families, use:

```text
planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
```

## 11. Synchronization With Navigation And Examples

When adding a new reusable use-case-map workflow, template or map:

```text
- update the relevant README/navigation file;
- update the responsibility map if a new owner file is created;
- decide whether an example coverage entry is needed;
- confirm profile-specific example fit with the user before wiring a candidate example into concrete route rows;
- record significant logical documentation actions in the action log.
```

For documentation-layer maps and templates, use:

```text
planning/documentation/documentation-responsibility-map.md
planning/documentation/example-coverage-workflow.md
planning/documentation/documentation-action-log.md
```

## 12. Checks Before Finalizing

Before finalizing a use-case-map change, check:

```text
- each new/changed row has a concrete user trigger;
- active-context behavior is clear when relevant;
- required reads are not broader than needed;
- owner workflows/templates are linked, not copied;
- expected output is specific enough to review;
- permission boundary is explicit;
- reusable rules live in workflow/template files, not the concrete map;
- navigation/responsibility/example/action-log updates were considered.
```

## 13. Do Not

```text
- Do not use a use-case map as a full workflow.
- Do not copy template bodies into use-case rows.
- Do not duplicate responsibility-map ownership tables inside a use-case map.
- Do not hide permission-changing behavior inside expected output text.
- Do not add ad hoc source-version metadata to use-case rows before source/version governance is ready.
- Do not make a concrete project map the source of truth for reusable use-case-map maintenance rules.
```

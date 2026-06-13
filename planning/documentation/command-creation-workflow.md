# Command Creation Workflow

Status: active reusable documentation-layer command creation workflow
Doc version: v0.3.0
Scope: rules-based workflow for creating or changing command routes, owner semantics, examples and optional Tampermonkey projections in any project using the reusable documentation layer

## 1. Purpose

Use this workflow when a chat needs to create, change or plan a command such as:

```text
создай команду
создай новую команду
добавь команду
спланируй команду
new command
create command
add command
```

A command must be created from rules and owner templates, not by copying a previous row blindly.

Existing rows in a concrete project root use-case map are useful examples, but the rule source is the route/table structure, the command owner workflow/template, response-command boundaries and the Tampermonkey projection rules when helper support is in scope.

## 2. Source Of Truth

```text
Primary route owner:
  planning/planning-use-case-map.md

Reusable command/answer behavior:
  planning/documentation/reviewable-agent-output-and-commands-workflow.md

Use-case map workflow and exact shape:
  planning/documentation/use-case-map-workflow.md
  planning/documentation/USE-CASE-MAP-TEMPLATE.md

Example coverage:
  planning/documentation/example-coverage-workflow.md
  planning/documentation/examples/README.md

Reusable Tampermonkey projection, only when in scope:
  planning/documentation/tampermonkey-command-projection-workflow.md
  planning/documentation/tools/tampermonkey/README.md
  planning/documentation/tools/tampermonkey/chat-command-palette.user.js
```

The reusable Tampermonkey helper is a projection/tool artifact. It does not own reusable command semantics.

Tampermonkey command bodies are projections. They do not own command semantics.

## 3. Command Route Row Template

Every new command route must be designed against the concrete use-case map row shape:

```text
| Command / trigger | Meaning | Active-context behavior | Traversal/read mode | Sources / owner files | Expected output |
|---|---|---|---|---|---|
| <aliases> | <what the command means> | <what to do when active context exists or is missing> | <reuse/targeted/full and read-source mode> | <owner files to read> | <answer/output/package behavior and permission boundary> |
```

Do not treat this as a visual table only. Each field is a rule slot.

## 4. Command Creation Steps

```text
1. Define the command family.
   - canonical phrase;
   - aliases;
   - language variants;
   - optional neutral English display name;
   - whether the command is a response modifier, output mode, file/update route, audit route, Goal Map route, parallel-work route or implementation/helper route.

2. Identify the owner semantics.
   - If an existing workflow/template already owns the behavior, link it.
   - If no owner exists and the behavior is reusable, create or update a documentation-layer workflow/template.
   - Do not put full reusable rules into the UCM row or example file.

3. Create or update the UCM route row.
   - Fill every template slot.
   - Include permission boundary in Expected output.
   - Make active-context behavior explicit.
   - Keep any English command name as display/readability metadata, not authority.

4. Decide example coverage.
   - If the command changes a non-trivial output mode or workflow, update examples index.
   - Add a concrete example only when it demonstrates a stable workflow/output and does not copy owner logic.

5. Decide Goal Map/workstream impact.
   - If the command belongs to an active long-running workstream, update the relevant living Goal Map.
   - Do not rewrite unrelated workstream maps.

6. Decide Tampermonkey projection separately.
   - Add or update Tampermonkey profile only when explicitly in scope.
   - Projection must follow planning/documentation/tampermonkey-command-projection-workflow.md.
   - The reusable full helper lives at planning/documentation/tools/tampermonkey/chat-command-palette.user.js.
   - The inserted body must include command, english_name, command_family, source_of_truth, route_read_rule, key_reminders and user_target.

7. Record the applied documentation action.
   - Append main action log after real changed files are known.
   - Do not log planned or local-only work as applied.
```

## 5. Tampermonkey Decision Gate

When creating a command, make this explicit:

```text
Tampermonkey decision:
  add now / defer / do not add

Default:
  defer unless the user explicitly asks to add the command to Tampermonkey or the batch scope includes helper projection.
```

If Tampermonkey is updated, update the reusable projection workflow or reusable full helper only when reusable rules/common command seed projection changes.

Do not recreate a tracked project-local helper folder by default while the reusable-only helper model is active.

## 6. Do Not

```text
- Do not create a command only by copying a similar UCM row.
- Do not make Tampermonkey the source of truth.
- Do not point reusable command creation rules at a project-specific workstream as reusable authority.
- Do not put permission boundaries only in examples.
- Do not edit files, create archives, commit or push from the command-creation command alone.
- Do not start unrelated command expansion while adding one command.
- Do not create placeholder parallel workspaces or sync plans while adding command routes.
- Do not keep a second tracked project-local Tampermonkey helper when reusable-only helper placement is active.
```

## 7. Minimal Answer Shape For Planning A New Command

```text
Command family:
  ...

Command type:
  ...

Owner semantics:
  ...

UCM row plan:
  ...

Example coverage:
  ...

Tampermonkey decision:
  add now / defer / do not add

Boundaries:
  ...

Next action:
  ...
```

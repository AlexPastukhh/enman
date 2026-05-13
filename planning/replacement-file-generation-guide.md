# Replacement File Generation Guide

Status: current workflow rule  
Scope: how planning replacement files should be generated when the user asks for full-file replacements

## 1. Purpose

This guide describes the expected way to generate replacement files for this repository when the user asks for files to replace existing repository files manually.

The current collaboration mode is:

```text
assistant generates complete replacement files
-> user replaces files in repository manually
-> user commits
-> assistant checks current repository state
-> assistant generates the next replacement package if needed
```

This exists because direct agent-driven repository editing may consume too many tokens or be less convenient than replacing prepared files.

## 2. Output Format

When the user asks to generate files for replacement, produce:

```text
- a zip archive with repository-relative paths;
- complete file contents, not patches;
- a short manifest of included files;
- clear instructions about which files should be replaced/added;
- no partial snippets unless the user explicitly asks for snippets.
```

Archive paths must match repository paths exactly.

Example:

```text
planning/README.md
planning/tables/pre-domain-variants-input.md
planning/diagrams/scenario-data/00-scenario-data-index.md
```

Do not place files under extra wrapper directories that would confuse manual replacement.

## 3. Replace Whole Files

Default behavior:

```text
generate complete replacement files
```

Avoid:

```text
- diff-only output;
- “replace this paragraph” instructions;
- partial Markdown blocks;
- mixed old/new content;
- incomplete files that require manual reconstruction.
```

Reason:

The user should be able to copy or replace the whole file safely.

## 4. Include All Necessary Files

If the user says they have not applied the previous archive, the next archive must include:

```text
- all files from the previous relevant archive;
- plus the new files/updates requested now.
```

Do not assume the previous archive was applied unless the user explicitly says it was.

## 5. One Conceptual Update At A Time

Prefer one coherent replacement package per step.

A package may contain multiple files when they belong to one navigation/workflow cleanup.

Avoid mixing unrelated changes such as:

```text
- scenario semantic updates;
- domain variant generation;
- UI planning;
- diagram generation rules;
- repository workflow cleanup;
```

unless the user explicitly asks to bundle them.

## 6. Preserve Existing Useful Content

When replacing index/navigation files:

```text
- preserve current useful decisions;
- remove stale downstream links;
- avoid deleting active scenario lists;
- avoid reintroducing superseded workflow paths;
- make the current next step obvious.
```

Before finalizing a package, check that it does not contradict:

```text
planning/README.md
planning/planning-workflow-current.md
planning/tables/README.md
planning/tables/pre-domain-variants-input.md
```

## 7. Current Official Workflow To Preserve

The current official workflow is:

```text
scenario text specs
-> scenario DATA files
-> validation-related file
-> pre-domain-variants-input.md
-> domain model variant 1
-> domain model variant 2
-> compare/refine variants
-> choose domain model direction
-> then plan aggregates/slices/implementation
```

The active post-scenario bridge is:

```text
planning/tables/pre-domain-variants-input.md
```

Do not reintroduce the old current workflow:

```text
scenario-domain-design-input-core.md
-> domain-discovery-core.md
-> aggregate-boundary-candidates-core.md
-> domain-model-options-core.md
```

Those names may exist only as historical/superseded notes, not as the current path.

## 8. Replacement Package Response Checklist

When returning a package, state:

```text
- archive link;
- files included;
- target repository paths;
- whether files are additions or replacements;
- current next planning step after applying the archive.
```

The final answer should be short and practical.

## 9. Repository Check Rule

When asked to check the repository before generating replacements:

```text
1. inspect current planning entry points and affected files;
2. identify stale references or conflicts;
3. generate replacement files only for the needed coherent update;
4. include all files from previous unapplied package if the user says it was not applied.
```

## 10. No Silent Repository Writes

Do not create or modify files in the repository directly unless the user explicitly asks for direct repository updates.

For this workflow, prefer:

```text
downloadable archive with complete replacement files
```

over direct commits.

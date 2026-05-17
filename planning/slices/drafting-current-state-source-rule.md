# Drafting Current-State Source Rule

Status: current / mandatory for project-state answers and implementation readiness checks

## Rule

When the user asks about the current state of the project, implementation status, existing code, generated contracts, tests, or files, use the current GitHub branch/repository state as the source of truth.

Do not infer current project state from zip archives, previous handoff archives, pasted drafts, or generated documentation packages.

Archives are handoff candidates. They may be unapplied, partially applied, superseded, or inconsistent with the current branch.

## Practical Procedure

```text
If asked:
- what is implemented;
- what is missing;
- whether endpoint/type/file exists;
- what slice is next;
- whether docs are synchronized with code;

then:
1. inspect GitHub/current branch;
2. inspect generated OpenAPI/types when API shape matters;
3. inspect runtime source/tests when implementation status matters;
4. use uploaded archives only as proposed changes/context, not as current state.
```

## Wording

Use:

```text
"According to the current GitHub branch..."
"Current repo evidence shows..."
"The archive says X, but current branch evidence is Y."
```

Do not say `Implemented` only because a previous archive or draft said so.

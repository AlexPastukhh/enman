# Scenario / Domain / Slice Documentation Profile

Status: active reusable specialized profile  
Scope: reusable profile for scenario-driven app/product projects; not universal documentation architecture

## 1. Purpose

This profile describes a reusable documentation topology for projects where planning flows from scenario/source behavior into normalized behavior/data, domain interpretation, implementation slices and verification.

It is extracted from the candidate principles classification as specialized reusable material.

It is not a universal documentation-layer principle.

## 2. Applicability

Use this profile when a project has most of these properties:

```text
- user/business scenarios are important source material;
- scenario text, UI behavior, clarifications or examples are normalized into structured source sets;
- domain interpretation is separated from raw scenario source;
- implementation is planned through slices or comparable delivery scopes;
- verification/evidence depends on tests, contracts, runtime behavior or generated artifacts;
- external-facing outputs may consume internal planning docs.
```

## 3. Non-Applicability

Do not force this profile onto documentation domains that do not have scenario/domain/slice structure.

Examples where this may not apply directly:

```text
- study notes;
- daily planning;
- research literature notes without app behavior;
- simple reference docs;
- documentation systems that do not have implementation slices or scenario sources.
```

Those systems should still use the reusable architecture principles, but define their own project/domain profile.

## 4. Layer Chain

Typical scenario-driven chain:

```text
scenario/source behavior
  -> normalized DATA / behavior item sets
  -> domain interpretation
  -> implementation slices or delivery scopes
  -> API/testing/verification/evidence
  -> optional external-facing output
```

The exact layer names and paths are project adapter content.

## 5. Layer Responsibilities

| Layer | Typical responsibility |
|---|---|
| Scenario/source behavior | Source wording, UI behavior, actor interactions, clarifications and scenario-level intent. |
| DATA / behavior items | Normalized actor inputs, outputs, selections, references, state changes and behavior items derived from scenario/source materials. |
| Domain | Domain concepts, invariants, aggregate boundaries, value objects, decisions and interpretation. |
| Slice / delivery scope | Implementation scope, boundaries, extension points, questions, source mapping and planned work. |
| API/testing/verification | Contracts, generated rules, error contracts, E2E/acceptance checks, proof strategy and evidence selection. |
| External-facing output | Audience-safe explanations, presentations, papers, reports or client-facing docs that consume internal planning material. |

## 6. Source-of-Truth Pattern

Scenario-driven projects should define where each source category lives.

Generic pattern:

```text
scenario behavior truth
  -> scenario/source files;

normalized source extraction
  -> DATA/behavior item files;

domain direction
  -> domain drafts/decisions;

slice scope truth
  -> slice/delivery-scope docs;

verification/current evidence
  -> code/tests/contracts/runtime evidence or the project's evidence model;

external-facing output wording
  -> external-output reference/adapters.
```

Exact file paths belong in the project adapter.

For Enman candidate mappings, see:

```text
planning/documentation-migration/enman-docs-adapter.md
```

## 7. Source Usage / Cascade Pattern

When upstream source changes, downstream consumers should be reviewed.

Typical cascade:

```text
scenario/source behavior changes
  -> review normalized DATA / behavior item sets
  -> review domain interpretation
  -> review slice/delivery-scope docs
  -> review API/testing/verification docs when relevant
  -> review external-facing claims when relevant
```

A downstream review may result in:

```text
reviewed against new source version;
no content change needed;
sync metadata updated;
needs follow-up.
```

Detailed source usage row models and field-kit setup are deferred to:

```text
planning/documentation-migration/PORTABILITY-FOLLOWUPS.md
```

## 8. Local-Global Visibility Pattern

Local scenario/domain/slice files can own detailed context.

Shared indexes/registers should own discoverability when details affect future work outside one local file.

Typical shared visibility questions:

```text
- Does this local question affect another scenario/domain/slice/API/testing file?
- Does this local assumption need future review?
- Is this a shared accepted direction?
- Is there a future owner file that does not exist yet?
- Should this become a register/index row or stay local-only with a reason?
```

Exact shared visibility maps belong in the project adapter.

## 9. Safe Rewrite Examples

In scenario-driven app/product projects, a documentation wording change may be semantically unsafe if it changes or hides:

```text
scenario behavior;
normalized DATA/behavior source;
domain rule;
API/contract requirement;
security or validation requirement;
testing responsibility;
architecture boundary;
slice scope;
current vs planned work status;
external-facing claim about what exists.
```

These examples support the reusable safe rewrite principle. They do not replace the principles file.

## 10. How A Project Instantiates This Profile

A project that uses this profile should create or maintain a project adapter/profile with:

```text
- concrete layer vocabulary;
- exact source file/folder paths;
- root/local responsibility map paths;
- evidence/current-reality profile;
- shared visibility map;
- source usage/cascade conventions;
- external-output mapping;
- examples.
```

## 11. Relation To Reusable Principles

Reusable principles own invariants.

This profile owns one specialized topology for a class of projects.

Project adapters own concrete project configuration.

Examples demonstrate one concrete application.

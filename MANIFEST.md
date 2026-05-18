# MANIFEST

Docs-only archive for slice taxonomy, cross-cutting behavior sources, legacy L1/L2 cleanup, and behavior-to-test trace workflow.

## Purpose

Clarify the documentation model:

```text
scenario sources describe required behavior;
data files and behavior items extract/classify scenario details;
slice drafts describe behavior subset + implementation plan + verification plan;
slice test plans prove behavior items and scenario outcomes.
```

This archive also clarifies that:

```text
- L1/L2 are legacy identifiers, not future folder structure;
- cross-cutting behavior must also have scenario/behavior sources;
- cross-cutting implementation work should still be written as normal client/server slice drafts;
- client/server drafts can be paired parts of one logical slice;
- client-only or server-only slice drafts must be explicitly marked with `SINGLE-`;
- planning/slices/cross-cutting/ is for umbrella/coordination docs, not for dumping implementation details;
- every slice draft must include a Behavior-to-Test Trace;
- tests use implementation details only as setup/action/observation mechanisms.
```

## Files included

```text
planning/slices/README.md
planning/slices/SLICE-FOLDER-MAP.md
planning/slices/SLICE-INDEX.md
planning/slices/SLICE-QUESTIONS.md
planning/slices/slice-test-plan-workflow.md
planning/slices/l2/README.md
planning/slices/client-slice-short-draft-rules-and-example.md

planning/slices/client/README.md
planning/slices/client/CLIENT-SLICE-TEMPLATE.md
planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
planning/slices/client/cross-cutting/README.md
planning/slices/client/examples/CLIENT-CROSS-CUTTING-DEFERRED-VALIDATION-SLICE-EXAMPLE.md

planning/slices/server/README.md
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/server/cross-cutting/README.md

planning/slices/cross-cutting/README.md
planning/slices/cross-cutting/CROSS-CUTTING-UMBRELLA-TEMPLATE.md
planning/slices/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.md

planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-cross-cutting/README.md
planning/diagrams/scenario-cross-cutting/client-behavior/README.md
planning/diagrams/scenario-cross-cutting/client-behavior/CC-CLIENT-FORM-VALIDATION-001-deferred-validation.behavior.md
planning/diagrams/scenario-cross-cutting/client-behavior/CC-CLIENT-FEEDBACK-001-error-feedback.behavior.md
planning/diagrams/scenario-cross-cutting/server-behavior/README.md
planning/diagrams/scenario-cross-cutting/server-client/README.md
planning/diagrams/scenario-cross-cutting/security/README.md
planning/diagrams/scenario-cross-cutting/security/CC-SEC-CSRF-001-unsafe-command-protection.behavior.md

_archive-notes/slice-taxonomy-cross-cutting-testing-workflow-v3/raw-author-message-log.md
_archive-notes/slice-taxonomy-cross-cutting-testing-workflow-v3/derived-decisions.md
```

## Not included

```text
- no runtime UI fixes
- no CSS implementation changes
- no concrete slice draft rewrites
- no mass movement of legacy L1/L2 slice files
- no OpenAPI/generated file changes
```

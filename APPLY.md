# Apply Instructions

Archive:

```text
enman-csrf-cross-cutting-slice-workflow-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-csrf-cross-cutting-slice-workflow-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

## Replace

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/diagrams/README.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
planning/slices/README.md
planning/slices/cross-cutting/README.md
planning/slices/shared/README.md
planning/slices/shared/antiforgery-token-session-context.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/api/api-error-contract.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
```

## Delete

```text
nothing
```

## Notes

This package:
- does not create a new `planning/security/` folder;
- uses existing scenario/specification structure for security addenda;
- creates browser security addendum as cross-cutting security requirement source;
- creates security-derived behavior items for CSRF;
- creates `CC-CSRF-001` as implementation-ready cross-cutting slice;
- updates cross-cutting/helper workflow to require the same source-items-flow-implementation-tests format as business slices;
- keeps `planning/slices/shared/antiforgery-token-session-context.md` as a source/support note, not primary implementation plan.

It does not:
- change code;
- create full numbered ADRs;
- implement ASP.NET antiforgery configuration;
- implement result filter;
- implement client token helper;
- add tests.

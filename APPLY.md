# Apply Instructions

Archive:

```text
enman-client-ui-specs-component-a11y-change-extension-points-workflow-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-client-ui-specs-component-a11y-change-extension-points-workflow-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/diagrams/scenario-ui-specs/README.md
planning/diagrams/scenario-ui-specs/00-scenario-ui-specs-index.md

planning/client/README.md
planning/client/cross-cutting/README.md
planning/client/cross-cutting/CL-FORM-VALIDATION-001-deferred-validation.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/client/cross-cutting/CL-STYLING-001-css-modules-tokens.md
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md

planning/slices/client-component-discovery-guide.md
planning/slices/change-extension-points-principles.md
planning/slices/slice-extension-points-register.md
```

## Replace

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/scenario-specification-principles.md

planning/diagrams/README.md
planning/diagrams/scenario-text-specs/README.md

planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/client-architecture-principles.md
```

## Delete

```text
nothing
```

## Notes

This package adds:
- scenario UI specs area;
- client-wide cross-cutting docs;
- component discovery guide;
- accessibility/styling conventions;
- change points / extension points / extension pressure principles;
- cross-slice extension points register with coverage and questions;
- updated parent slice and `.client.md` template sections.

It does not:
- create concrete `.client.md`;
- create concrete `SC-XX-ui.md`;
- change code/API/domain/tests;
- introduce implementation abstractions;
- move all validation addenda.

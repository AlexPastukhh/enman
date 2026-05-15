# Apply Instructions

Archive:

```text
enman-testing-e2e-playwright-workflow-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-testing-e2e-playwright-workflow-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/testing/README.md
planning/testing/testing-principles.md
planning/testing/e2e-playwright-workflow.md
planning/testing/test-object-patterns.md
planning/testing/playwright-e2e-cleanup-plan.md
```

## Replace

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
```

## Delete

```text
nothing
```

## Notes

This package:
- adds a central testing planning area;
- defines test responsibility boundaries;
- defines E2E Playwright workflow;
- documents explicit Playwright webServer backend+frontend strategy;
- documents Vite proxy / CORS reasoning;
- documents locator policy and Playwright exact matching caveat;
- documents Page Object vs Component Object responsibilities;
- provides a current Playwright cleanup plan.

It does not:
- change code;
- create root playwright.config.ts;
- move tests;
- edit package.json;
- fix current async bugs;
- create full numbered ADRs.

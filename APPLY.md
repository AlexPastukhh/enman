# Apply Instructions

Archive:

```text
enman-cross-cutting-constants-slice-workflow-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-cross-cutting-constants-slice-workflow-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/slices/cross-cutting/README.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
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
planning/api/README.md
planning/api/client-constants-generation.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
planning/constants/README.md
```

## Optional cleanup if the previous constants archive was applied

The primary constants source is now:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

Older detailed files under `planning/constants/` can be deleted or left as stale/superseded notes. Recommended cleanup:

```powershell
Remove-Item planning/constants/client-constants-generation-workflow.md -ErrorAction SilentlyContinue
Remove-Item planning/constants/client-constants-tools-design.md -ErrorAction SilentlyContinue
Remove-Item planning/constants/client-constants-testing-strategy.md -ErrorAction SilentlyContinue
Remove-Item planning/constants/client-facing-error-code-contract-tests.md -ErrorAction SilentlyContinue
Remove-Item planning/constants/client-constants-implementation-plan.md -ErrorAction SilentlyContinue
```

Keep:

```text
planning/constants/README.md
```

as a redirect/pointer only.

## Notes

This package:
- treats constants generation/testing as cross-cutting slice, not only workflow note;
- defines cross-cutting/helper slice terms;
- makes `CC-CONST-001` the primary implementation-ready document;
- adds implementation flow detail filter;
- keeps class/method details in flow only when they clarify key behavior or decisions;
- keeps API docs as API contract references, not implementation slice source of truth.

It does not:
- change code;
- create full numbered ADRs;
- implement Tools generator;
- create `.client.md`;
- migrate FluentValidation ErrorCode usage;
- add convention/golden tests.

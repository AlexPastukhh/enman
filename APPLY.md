# Apply Instructions

Archive:

```text
enman-workflow-sidecar-behavior-items-package-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-workflow-sidecar-behavior-items-package-v1.zip" -DestinationPath . -Force
git status
```

## Add or replace if missing

```text
planning/planning-agent-protocol.md
planning/slices/implementation-principles.md
planning/slices/slice-implementation-notes-register.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
planning/slices/shared/README.md
planning/slices/shared/client-deferred-validation.md
planning/slices/shared/client-server-validation-error-mapping.md
planning/slices/shared/client-form-values-to-api-dto-mapping.md
planning/slices/shared/antiforgery-token-session-context.md
planning/slices/shared/client-applicant-data-prefill-notes.md
```

## Replace

```text
planning/README.md
planning/planning-workflow-current.md
planning/scenario-specification-principles.md
planning/diagrams/README.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/tables/README.md
```

## Delete

```text
nothing
```

## Notes

This package does not create any `.client.md` sidecar for a concrete slice.

Client sidecars are created only when concrete client work starts.

Behavior items migration/cleanup is not done in this package. This package adds the folder/index/rules only.

Workflow centralization audit is not done in this package. It is recorded as a future cleanup step.

Resolved `InReview` direction is preserved.

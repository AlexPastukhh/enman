# Apply Instructions

Archive:

```text
enman-client-ui-shared-support-workflow-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-client-ui-shared-support-workflow-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/slices/shared/README.md
planning/slices/shared/client-deferred-validation.md
planning/slices/shared/client-server-validation-error-mapping.md
planning/slices/shared/antiforgery-token-session-context.md
planning/slices/shared/client-applicant-data-prefill-notes.md
```

## Replace

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/l1-slice-boundary-draft-01.md
planning/slices/SL-REQ-001-create-connection-request.md
```

## Delete

```text
nothing
```

## Notes

This package does not change the resolved `InReview` status direction.

It prepares slice docs for the next implementation step:

```text
complete missing Client/UI for already implemented server-side request creation logic
```

Recommended next planning input:

```text
UI plan for SL-REQ-UI-001 Request creation Client/UI
```

# Apply — Account Email Activation Slice Drafts v1

Archive: `2026-05-19-account-email-activation-slice-drafts-v1.1.zip`

This is a docs-only planning archive.

It adds new slice drafts for minimal account email activation after registration.

## Apply

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\2026-05-19-account-email-activation-slice-drafts-v1.1.zip" -DestinationPath "." -Force
git status
git diff
```

## Added draft files

```text
planning/slices/cross-cutting/CC-AUTH-ACT-001-email-activation-flow.md
planning/slices/server/SL-AUTH-ACT-001-register-pending-and-send-activation-email.server.md
planning/slices/server/SL-AUTH-ACT-002-activate-client-account.server.md
planning/slices/server/SL-AUTH-ACT-003-active-account-guard.server.md
planning/slices/client/SL-AUTH-ACT-001-email-activation-ui-and-protected-gate.client.md
```

## Not included

```text
- no runtime code changes
- no tests changed
- no generated OpenAPI/types changed
- no UI/CSS implementation
- no database migrations
- no implementation audit
```

## Suggested implementation order after accepting drafts

```text
1. Implement SL-AUTH-ACT-001.server.
2. Implement SL-AUTH-ACT-002.server.
3. Implement SL-AUTH-ACT-003.server.
4. Regenerate OpenAPI/types/client constants if API contract changed.
5. Implement SL-AUTH-ACT-001.client.
6. Update Playwright registration/business flows.
```

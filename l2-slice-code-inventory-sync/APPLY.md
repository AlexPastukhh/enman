# Apply L2 Slice Code Inventory Sync

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-slice-code-inventory-sync.zip" -DestinationPath . -Force

git apply .\l2-slice-status-sync.patch

git status
git diff -- planning
```

If the patch does not apply because docs were already edited, open:

```text
planning/slices/l2/L2-current-implementation-inventory.md
l2-slice-status-sync.patch
```

and apply the same status updates manually.

This is docs-only. Do not add runtime code, tests or generated artifacts from this package.

# Apply L1-MY-REQUESTS-LIST-FILTERS.client replacement

This archive adds/replaces the planning sidecar:

```text
planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md
```

It is docs-only. It does not change runtime client code.

## Apply from repository root

PowerShell:

```powershell
Expand-Archive "C:\Users\alexa\Downloads\enman-l1-my-requests-list-filters-client-replacement.zip" -DestinationPath . -Force
```

Or run:

```powershell
.\apply-l1-my-requests-list-filters-client-replacement.ps1
```

Bash:

```bash
./apply-l1-my-requests-list-filters-client-replacement.sh
```

## Suggested checks

Docs-only minimal check:

```powershell
git status --short
```

Optional safety checks:

```powershell
npm.cmd --prefix energymanagement.client run build
npm.cmd --prefix energymanagement.client run test
npm.cmd run check:api
```

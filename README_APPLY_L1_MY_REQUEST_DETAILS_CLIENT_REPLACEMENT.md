# Apply L1-MY-REQUEST-DETAILS.client replacement

This archive adds/replaces the planning sidecar draft for the client request details slice:

```text
planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md
```

It is a docs-sidecar replacement only. It does not modify runtime client code.

## Apply from repository root

PowerShell:

```powershell
cd C:\enman\enman
Expand-Archive "C:\Users\alexa\Downloads\enman-l1-my-request-details-client-replacement.zip" -DestinationPath . -Force
```

Bash:

```bash
cd /path/to/enman
unzip -o ~/Downloads/enman-l1-my-request-details-client-replacement.zip -d .
```

## Verify

```powershell
git status --short
Get-Content .\planning\slices\l1\L1-MY-REQUEST-DETAILS.client.md -TotalCount 40
```

Runtime tests are not required for a docs-only replacement, but can be run as a safety check:

```powershell
npm.cmd --prefix energymanagement.client run build
npm.cmd --prefix energymanagement.client run test
npm.cmd run check:api
npm.cmd run test:e2e -- --list
npm.cmd run test:e2e
```

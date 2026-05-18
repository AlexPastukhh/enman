# Apply — agr-exch-accept-refactor-v1

Docs-only paired draft refactor archive.

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\agr-exch-accept-refactor-v1.zip" -DestinationPath . -Force
.\APPLY-agr-exch-accept-refactor-v1.ps1
git status
git diff -- planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md
git diff -- planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
git diff -- _archive-review/agr-exch-accept-refactor-v1
```

If diff is correct:

```powershell
git add planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md _archive-review/agr-exch-accept-refactor-v1 APPLY-agr-exch-accept-refactor-v1.ps1 APPLY-agr-exch-accept-refactor-v1.md MANIFEST-agr-exch-accept-refactor-v1.md
git status
```

Recommended preservation checks:

```powershell
git grep -n "POST /api/agreement-exchanges/{exchangeId}/accept" planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
git grep -n "204 No Content" planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
git grep -n "ClientAccountId" planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
git grep -n "no new proposal version" planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
git grep -n "Employee accept is out of scope" planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
```

Scope:

```text
Docs only.
No runtime code.
No tests.
No generated artifacts.
```

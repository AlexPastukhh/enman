# Apply L2 Final README / Docs Sync

Run from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-final-readme-doc-sync.zip" -DestinationPath . -Force
.\APPLY-l2-final-readme-doc-sync.ps1
git status
git diff -- planning
```

Recommended stale checks:

```powershell
git grep -n "scenario-server-domain-validation-addendum"
git grep -n "DocumentFileRef"
git grep -n "ReviewerRef"
git grep -n "ProposalAttachment"
git grep -n "EmployeeRef"
git grep -n "ResponsibleEmployeeId"
git grep -n "Client Data Verification"
git grep -n "SL-AGR-EXCH-003.*Read Agreement Exchange"
git grep -n "SL-AGR-EXCH-004.*Accept Active"
git grep -n "SL-AGR-EXCH-005.*Final Refuse"
```

If diff is OK:

```powershell
git add planning APPLY-l2-final-readme-doc-sync.ps1 APPLY-l2-final-readme-doc-sync.md MANIFEST-l2-final-readme-doc-sync.md
git status
```

No runtime code, tests or generated artifacts are included.

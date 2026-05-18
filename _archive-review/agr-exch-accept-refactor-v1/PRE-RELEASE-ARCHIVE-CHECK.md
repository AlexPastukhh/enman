# Pre-Release Archive Check — agr-exch-accept-refactor-v1

## Archive shape

```text
APPLY-agr-exch-accept-refactor-v1.ps1
APPLY-agr-exch-accept-refactor-v1.md
MANIFEST-agr-exch-accept-refactor-v1.md
_replacement-files/agr-exch-accept-refactor-v1/...
_archive-review/agr-exch-accept-refactor-v1/...
```

## Apply behavior

```text
1. Capture existing local originals into _archive-review/agr-exch-accept-refactor-v1/original-files/.
2. Replace only the two selected draft files.
3. Leave runtime code/tests/generated artifacts untouched.
4. Remove staged replacement folder after copy.
```

## Manual review after apply

```powershell
git status
git diff -- planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md
git diff -- planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
git diff -- _archive-review/agr-exch-accept-refactor-v1
```

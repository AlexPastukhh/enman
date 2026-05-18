# APPLY: slice-applicant-party-verification-server-draft-addition-v1

## Apply from repository root

```powershell
cd "C:\enman\enman"

$zip = "C:\Users\alexa\Downloads\slice-applicant-party-verification-server-draft-addition-v1.zip"
$tmp = "$env:TEMP\slice-applicant-party-verification-server-draft-addition-v1"
$review = ".\_archive-review\slice-applicant-party-verification-server-draft-addition-v1\original-files"

Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive -Path $zip -DestinationPath $tmp -Force

$files = @(
  "planning/slices/SL-APPL-VER-001-run-mock-applicant-party-verification-from-employee-request-review.md"
)

foreach ($rel in $files) {
  $relWin = $rel -replace '/', '\'
  if (Test-Path $relWin) {
    $backupPath = Join-Path $review $relWin
    New-Item -ItemType Directory -Force -Path (Split-Path $backupPath) | Out-Null
    Copy-Item $relWin $backupPath -Force
  }
}

Copy-Item "$tmp\planning" "." -Recurse -Force
Copy-Item "$tmp\_archive-review" "." -Recurse -Force
Copy-Item "$tmp\MANIFEST.slice-applicant-party-verification-server-draft-addition-v1.md" ".\MANIFEST.slice-applicant-party-verification-server-draft-addition-v1.md" -Force
Copy-Item "$tmp\APPLY.slice-applicant-party-verification-server-draft-addition-v1.md" ".\APPLY.slice-applicant-party-verification-server-draft-addition-v1.md" -Force

git status
```

## Check

```powershell
git diff -- planning/slices/SL-APPL-VER-001-run-mock-applicant-party-verification-from-employee-request-review.md
```

## Commit

```powershell
git add planning/slices/SL-APPL-VER-001-run-mock-applicant-party-verification-from-employee-request-review.md `
        _archive-review/slice-applicant-party-verification-server-draft-addition-v1/ `
        MANIFEST.slice-applicant-party-verification-server-draft-addition-v1.md `
        APPLY.slice-applicant-party-verification-server-draft-addition-v1.md

git commit -m "Add ApplicantParty verification server slice draft"
```

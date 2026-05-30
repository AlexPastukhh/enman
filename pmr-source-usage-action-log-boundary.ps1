# pmr-source-usage-action-log-boundary.ps1
# Purpose:
#   Targeted local edit for planning/planning-maintenance-register.md.
#   Adds PMR/action-log boundary rules and aligns PMR-002 with source usage/cascade framing.
#
# Run from repository root:
#   powershell -ExecutionPolicy Bypass -File .\pmr-source-usage-action-log-boundary.ps1

$ErrorActionPreference = "Stop"

$path = "planning/planning-maintenance-register.md"

if (-not (Test-Path $path)) {
  throw "File not found: $path. Run this script from the repository root."
}

$raw = Get-Content -Path $path -Raw -Encoding UTF8

# 1) Add PMR vs action-log boundary after the existing purpose rule.
if ($raw -notmatch "This register is not the documentation action log\.") {
  $oldPurpose = "Do not use this register for ordinary product feature work, implementation TODOs or one-off local draft notes."
  $newPurpose = @'
Do not use this register for ordinary product feature work, implementation TODOs or one-off local draft notes.

This register is not the documentation action log.

Use:

```text
planning/planning-maintenance-register.md
  = goals, reminders, deferred work, waiting-for-condition tasks and follow-ups that still need action.

planning/documentation/documentation-action-log.md
  = significant completed logical documentation actions and short explanations of why they happened.
```

Not every logical documentation action creates a PMR entry.

If a logged action creates future work, add or update a PMR entry and reference that PMR relation from the action log.
'@

  if (-not $raw.Contains($oldPurpose)) {
    throw "Could not find PMR purpose anchor."
  }

  $raw = $raw.Replace($oldPurpose, $newPurpose)
  Write-Host "Added PMR/action-log boundary in purpose section."
} else {
  Write-Host "PMR/action-log boundary already present in purpose section."
}

# 2) Replace PMR-002 block only.
$newPmr002 = @'
### PMR-002 — Create source usage cascade workflow after pilot

```text
ID: PMR-002
Status: waiting-for-condition
Area: source usage / cascade / layer encapsulation
Task / reminder:
  Design the full source usage cascade workflow after a real pilot proves the relationship model.
Trigger / condition:
  SC-13D source usage pilot has real audited rows, scenario/domain/slice dependency model is audited, and at least one cascade review simulation is performed.
Why it matters:
  The goal is not file versioning by itself. The goal is to preserve layer encapsulation, avoid duplicated source truth and let downstream work reference already-reviewed upstream artifacts without forcing the user to re-review reconstructed reasoning.
Owner layer:
  documentation / source architecture
Target files:
  future planning/documentation/source-version-cascade-sync-workflow.md or renamed equivalent
  planning/documentation/source-usage-cascade-governance-plan.md
  source usage registers / permanent register location after pilot
Depends on:
  source usage cascade governance plan, SC-13D pilot fill, dependency audit, cascade review simulation
Do when:
  After the first real source usage register proves the row shape and review process.
Do not do before:
  Do not create a full cascade workflow only from abstract assumptions.
Notes:
  Governance plan and pilot skeleton exist. Full workflow is still not created.
```

'@

$oldPmr002Pattern = '(?s)### PMR-002 — Create source/version cascade sync workflow after pilot\s+```text.*?```\s+(?=### PMR-003)'
$newPmr002Pattern = '(?s)### PMR-002 — Create source usage cascade workflow after pilot\s+```text.*?```\s+(?=### PMR-003)'

if ($raw -match $oldPmr002Pattern) {
  $raw = [regex]::Replace($raw, $oldPmr002Pattern, $newPmr002, 1)
  Write-Host "Replaced old PMR-002 block."
} elseif ($raw -match $newPmr002Pattern) {
  $raw = [regex]::Replace($raw, $newPmr002Pattern, $newPmr002, 1)
  Write-Host "PMR-002 already used new heading; normalized block content."
} else {
  throw "Could not find PMR-002 block with expected old or new heading."
}

# 3) Add PMR/action-log maintenance rules.
if ($raw -notmatch "Do not use PMR as the general log of all documentation actions\.") {
  $oldRule = "- Mark entries done/superseded instead of deleting them when traceability is useful."
  $newRule = @'
- Do not use PMR as the general log of all documentation actions.
- If a completed logical change needs history, record it in planning/documentation/documentation-action-log.md.
- If a logged action creates future work, add or update a PMR entry and reference it from the action log.
- Mark entries done/superseded instead of deleting them when traceability is useful.
'@

  if (-not $raw.Contains($oldRule)) {
    throw "Could not find PMR maintenance rules anchor."
  }

  $raw = $raw.Replace($oldRule, $newRule)
  Write-Host "Added PMR/action-log maintenance rules."
} else {
  Write-Host "PMR/action-log maintenance rules already present."
}

# 4) Basic validation before writing.
$requiredMarkers = @(
  "This register is not the documentation action log.",
  "### PMR-002 — Create source usage cascade workflow after pilot",
  "Area: source usage / cascade / layer encapsulation",
  "Governance plan and pilot skeleton exist. Full workflow is still not created.",
  "Do not use PMR as the general log of all documentation actions."
)

foreach ($marker in $requiredMarkers) {
  if ($raw -notlike "*$marker*") {
    throw "Validation failed. Missing marker: $marker"
  }
}

if ($raw -like "*### PMR-002 — Create source/version cascade sync workflow after pilot*") {
  throw "Validation failed. Old PMR-002 heading is still present."
}

# 5) Write file.
Set-Content -Path $path -Value $raw -Encoding UTF8

# 6) Save/copy scoped diff for review.
$files = @(
  "planning/planning-maintenance-register.md"
)

$pkgName = "pmr-source-usage-action-log-boundary"
$diffFile = Join-Path (Get-Location) "$pkgName.diff"

git status --short -- $files
git diff --stat -- $files
git --no-pager diff --no-color --output="$diffFile" -- $files
Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."

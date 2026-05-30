# Batch A2 continuation script v3
# Use after the first A2 script updated:
#   planning/documentation/reviewable-agent-output-and-commands-workflow.md
# and v2 failed before writing planning/planning-use-case-map.md.
#
# This script updates planning/planning-use-case-map.md with robust regex anchors
# and writes a scoped diff for both A2 files.
# It does not commit.

$ErrorActionPreference = "Stop"

function Read-Utf8File {
  param([string]$Path)
  return [System.IO.File]::ReadAllText((Resolve-Path $Path), [System.Text.Encoding]::UTF8)
}

function Write-Utf8NoBomFile {
  param(
    [string]$Path,
    [string]$Content
  )
  $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
  [System.IO.File]::WriteAllText((Resolve-Path $Path), $Content, $utf8NoBom)
}

function Insert-BeforeRegexOnce {
  param(
    [string]$Content,
    [string]$Pattern,
    [string]$Insert,
    [string]$Label,
    [string]$Probe
  )

  if ($Content.Contains($Probe)) {
    Write-Host "Already applied: $Label"
    return $Content
  }

  $options = [System.Text.RegularExpressions.RegexOptions]::Singleline
  $matches = [System.Text.RegularExpressions.Regex]::Matches($Content, $Pattern, $options)
  if ($matches.Count -ne 1) {
    throw "Regex anchor for '$Label' matched $($matches.Count) times; expected 1."
  }

  $m = $matches[0]
  Write-Host "Applied: $Label"
  return $Content.Substring(0, $m.Index) + $Insert + $Content.Substring($m.Index)
}

function Replace-LineRegexOnceOrSkip {
  param(
    [string]$Content,
    [string]$Pattern,
    [string]$NewLine,
    [string]$Label,
    [string]$Probe
  )

  if ($Content.Contains($Probe)) {
    Write-Host "Already applied: $Label"
    return $Content
  }

  $options = [System.Text.RegularExpressions.RegexOptions]::Multiline
  $matches = [System.Text.RegularExpressions.Regex]::Matches($Content, $Pattern, $options)
  if ($matches.Count -ne 1) {
    throw "Line regex for '$Label' matched $($matches.Count) times; expected 1."
  }

  Write-Host "Applied: $Label"
  return [System.Text.RegularExpressions.Regex]::Replace(
    $Content,
    $Pattern,
    { param($m) return $NewLine },
    $options
  )
}

$reviewablePath = "planning/documentation/reviewable-agent-output-and-commands-workflow.md"
$useCasePath = "planning/planning-use-case-map.md"

foreach ($path in @($reviewablePath, $useCasePath)) {
  if (!(Test-Path $path)) {
    throw "Missing target file: $path"
  }
}

$useCase = Read-Utf8File $useCasePath

# 1. Insert section 8A before repeated commands.
$section8A = @'
## 8A. Response Level And File Update Overview References

Answer depth is governed by:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

This use-case map may mark expected response level or output shape in the `Expected output` column, but it does not own Level 1/2/3 trigger logic.

File Update Overview process and shape are owned by:

```text
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
```

Use-case rows may reference File Update Overview as expected output, but must not duplicate its workflow or template logic.

The canonical prior-discussion recheck command is:

```text
обс
```

`обс` means context/discussion recheck. Its semantics are owned by `reviewable-agent-output-and-commands-workflow.md`.

'@

$useCase = Insert-BeforeRegexOnce `
  -Content $useCase `
  -Pattern "(?m)^## 9\. Repeated / Continuation Commands\s*$" `
  -Insert $section8A `
  -Label "insert section 8A" `
  -Probe "## 8A. Response Level And File Update Overview References"

# 2. Insert обс row before перепроверь/recheck row.
$obsRow = '| `обс`, `перепроверь обсуждение`, `context recheck` | Re-check relevant prior discussion, accepted decisions and constraints before answering. | Re-check available prior discussion or say what context is unavailable. | Reuse / targeted; full only if task requires it. | Conversation plus sources only if needed. | Context/discussion recheck result; combine with Level 1/2/3 by task breadth. |' + "`r`n"

$useCase = Insert-BeforeRegexOnce `
  -Content $useCase `
  -Pattern "(?m)^\| ``перепроверь``, ``recheck`` \|.*$" `
  -Insert $obsRow `
  -Label "insert obs repeated command row" `
  -Probe '| `обс`, `перепроверь обсуждение`, `context recheck` |'

# 3. Expected-output markers for serious use-case rows.
$rows = @(
  @{
    Pattern = '(?m)^\| “посмотри / распланируй / проверь docs” \|.*$'
    NewLine = '| “посмотри / распланируй / проверь docs” | Non-trivial planning/docs work | Optional | Full first time; targeted later | GitHub/archive/conversation by context | workflow activation; reviewable output | `planning/README.md`, `workflow-activation-map.md`, `planning-doc-responsibility-map.md` | README + activation map | Level 2 reviewable answer/plan | Edits require approval |'
    Probe = 'Level 2 reviewable answer/plan'
    Label = 'docs planning row expected output'
  },
  @{
    Pattern = '(?m)^\| “обнови docs” \|.*$'
    NewLine = '| “обнови docs” | Documentation update | Optional | Targeted/full by scope | GitHub for writes; archive for read-only if requested | docs update plan/workflow; local-global sync | documentation README/map/workflows | activation map + docs workflows | Level 2 update plan or applied update + File Update Overview when files are planned/changed/reviewed | GitHub writes require approval |'
    Probe = 'Level 2 update plan or applied update + File Update Overview when files are planned/changed/reviewed'
    Label = 'docs update row expected output'
  },
  @{
    Pattern = '(?m)^\| “добавь template / workflow / command / draft format / пример” \|.*$'
    NewLine = '| “добавь template / workflow / command / draft format / пример” | Documentation governance update | Optional | Full first time; targeted later | GitHub/archive/conversation by context | docs update workflow; example coverage decision when template/output shape changes; reviewable output | documentation README/map/workflows; `planning/documentation/example-coverage-workflow.md` when template/output/command/draft example coverage matters; relevant owner workflow/template; principle section references in §3A | docs architecture principles + use-case map + example coverage workflow | Level 2 update plan or replacement package + File Update Overview when files are planned/changed/reviewed; example coverage decision if applicable | Edits/packages require approval |'
    Probe = 'Level 2 update plan or replacement package + File Update Overview when files are planned/changed/reviewed; example coverage decision if applicable'
    Label = 'governance row expected output'
  },
  @{
    Pattern = '(?m)^\| “source usage”, “source/version”, “каскад зависимостей”, “stale downstream”, “версии сорсов”, “инкапсуляция слоёв” \|.*$'
    NewLine = '| “source usage”, “source/version”, “каскад зависимостей”, “stale downstream”, “версии сорсов”, “инкапсуляция слоёв” | Source usage / cascade governance or pilot work | Optional | Full first time; targeted later | GitHub/archive/conversation by context | source usage cascade governance plan; principles §12A/13/14/15; PMR-002 until full workflow exists | `planning/documentation/source-usage-cascade-governance-plan.md`; `planning/documentation/source-usage-pilots/README.md`; pilot register if user asks to work on a pilot; relevant source/domain/slice docs only for real pilot fill | layer encapsulation principle + source usage governance plan | Level 2 governance plan, pilot skeleton, pilot fill plan or replacement package + File Update Overview when files are planned/changed/reviewed | Do not pretend full cascade workflow exists; do not add ad hoc version fields |'
    Probe = 'Level 2 governance plan, pilot skeleton, pilot fill plan or replacement package + File Update Overview when files are planned/changed/reviewed'
    Label = 'source usage row expected output'
  },
  @{
    Pattern = '(?m)^\| “давай архив”, “собери архив”, “replacement package”, “archive for manual apply” \|.*$'
    NewLine = '| “давай архив”, “собери архив”, “replacement package”, “archive for manual apply” | Replacement archive/package generation | Optional | Targeted/full by package scope | GitHub/archive/conversation by context | replacement file generation guide; docs update workflow when docs change; reviewable output | `planning/replacement-file-generation-guide.md`, target files, docs update workflow when docs are changed | replacement guide + workflow activation | Level 2 archive/package response + File Update Overview; ZIP package with `MANIFEST.md`, `APPLY.md`, `replacement-files/<repo-relative-path>` complete files | Direct repo edits not allowed unless separately approved |'
    Probe = 'Level 2 archive/package response + File Update Overview; ZIP package'
    Label = 'archive row expected output'
  },
  @{
    Pattern = '(?m)^\| “поправь ссылки во многих файлах” \|.*$'
    NewLine = '| “поправь ссылки во многих файлах” | Mechanical link/path/name sync | Optional | Targeted/broad search | GitHub or archive | docs update workflow; local-global sync | target files/search source | docs update workflow output mode rules | Level 2 link-sync plan + File Update Overview; one bundled commit when tool-supported | Bundled/bulk mode should be used when approved; if unavailable, stop and disclose before per-file writes |'
    Probe = 'Level 2 link-sync plan + File Update Overview; one bundled commit when tool-supported'
    Label = 'link sync row expected output'
  },
  @{
    Pattern = '(?m)^\| “проверь current/implemented status” \|.*$'
    NewLine = '| “проверь current/implemented status” | Status reconciliation | Optional | Targeted/full by claim | Current branch evidence unless archive explicitly accepted | status reconciliation | status workflow + code/tests/generated artifacts | activation map status chain | Level 2 status findings/sync plan | Current-state claims need evidence |'
    Probe = 'Level 2 status findings/sync plan'
    Label = 'status row expected output'
  }
)

foreach ($r in $rows) {
  $useCase = Replace-LineRegexOnceOrSkip `
    -Content $useCase `
    -Pattern $r.Pattern `
    -NewLine $r.NewLine `
    -Label $r.Label `
    -Probe $r.Probe
}

Write-Utf8NoBomFile $useCasePath $useCase
Write-Host "Updated $useCasePath"

# Review diff for both A2 files.
$files = @(
  $reviewablePath,
  $useCasePath
)

$pkgName = "batch-a2-response-levels-obs-large-files"
$diffFile = Join-Path (Get-Location) "$pkgName.diff"

git status --short -- $files
git diff --stat -- $files
git --no-pager diff --no-color --output="$diffFile" -- $files
Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."

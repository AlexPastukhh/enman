# Batch A2 targeted script for large files
# Applies anchored edits for:
# - planning/documentation/reviewable-agent-output-and-commands-workflow.md
# - planning/planning-use-case-map.md
#
# The script does not commit. It writes a scoped diff and copies it to clipboard.

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

function Replace-ExactOnce {
  param(
    [string]$Content,
    [string]$Old,
    [string]$New,
    [string]$Label
  )
  $idx = $Content.IndexOf($Old, [System.StringComparison]::Ordinal)
  if ($idx -lt 0) {
    throw "Anchor not found for: $Label"
  }
  $idx2 = $Content.IndexOf($Old, $idx + $Old.Length, [System.StringComparison]::Ordinal)
  if ($idx2 -ge 0) {
    throw "Anchor matched more than once for: $Label"
  }
  return $Content.Substring(0, $idx) + $New + $Content.Substring($idx + $Old.Length)
}

function Replace-RegexOnce {
  param(
    [string]$Content,
    [string]$Pattern,
    [string]$New,
    [string]$Label
  )
  $options = [System.Text.RegularExpressions.RegexOptions]::Singleline
  $matches = [System.Text.RegularExpressions.Regex]::Matches($Content, $Pattern, $options)
  if ($matches.Count -ne 1) {
    throw "Regex anchor for '$Label' matched $($matches.Count) times; expected 1."
  }
  return [System.Text.RegularExpressions.Regex]::Replace(
    $Content,
    $Pattern,
    { param($m) return $New },
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

# ---------------------------------------------------------------------------
# reviewable-agent-output-and-commands-workflow.md
# ---------------------------------------------------------------------------

$reviewable = Read-Utf8File $reviewablePath

$oldCommandList = "Response-level commands such as ``level 2``, ``recheck``, ``clarify``, ``keep prev``, ``no ch``, ``без изм``, ``use archive``, ``арх``, ``б из арх``, ``давай драфт`` and ``обнови`` change how the answer should be produced, checked or continued. They do not grant permission to edit files or change repository state."
$newCommandList = "Response-level commands such as ``level 2``, ``recheck``, ``clarify``, ``keep prev``, ``no ch``, ``без изм``, ``use archive``, ``арх``, ``б из арх``, ``давай драфт``, ``обнови`` and ``обс`` change how the answer should be produced, checked or continued. They do not grant permission to edit files or change repository state."
$reviewable = Replace-ExactOnce $reviewable $oldCommandList $newCommandList "reviewable command list includes obs"

$newLevelsBlock = @'
## 2. Core Rule

Use the smallest response format that remains reviewable.

Do not turn every casual answer into a heavy report.

Response levels are primarily about task complexity, breadth and external reviewability. They are not quality levels.

The assistant must still do the reasoning, context recall, source check, web verification, safety check or accepted-decision preservation required by the task even when the final answer is short.

Use more structure when:

```text
- the answer affects planning docs;
- the answer may be reviewed by another chat;
- the answer summarizes repo state;
- the answer proposes a change;
- the answer contains a draft, audit, plan or handoff;
- the answer depends on multiple sources;
- the answer has risks, assumptions or incomplete coverage.
```

## 3. Detail Levels

The user may request a response level with phrases such as:

```text
level 1
lvl 1
ур 1
level 2
lvl 2
ур 2
level 3
lvl 3
ур 3
```

If the user does not specify a level, choose the smallest level that remains useful and reviewable.

Default guidance:

```text
casual / quick question with no Level 2/3 triggers -> Level 1
non-trivial planning / docs / repo analysis -> Level 2
handoff / external review / broad audit / high-risk answer -> Level 3
```

Level selection changes how explicitly the answer exposes task, scope, sources, risks, verification and next steps. It does not lower the required reasoning quality for lower levels.

### 3A. Automatic Level Escalation

Level 1 is allowed only when the task does not contain Level 2 or Level 3 triggers.

Level 2 triggers are conditions where the task is broad or consequential enough that the answer must expose scope, sources/coverage, assumptions, risks, verification and next step.

Use Level 2 automatically when the task involves any of:

```text
- planning / documentation / code / file update planning;
- multi-file checks;
- synchronization between files, docs or layers;
- workflow / rule / use-case / responsibility changes;
- source-of-truth boundary changes;
- archive / replacement package planning or review;
- diff review before commit;
- repo analysis across multiple files;
- non-trivial audit / check / recheck;
- source usage / cascade / stale-reference work;
- any task where a short answer could hide scope, unchecked sources, risks, assumptions or next steps.
```

Use Level 3 automatically when the answer must survive outside the current chat context or the task is broad/high-risk enough that Level 2 would not preserve enough evidence.

Typical Level 3 triggers:

```text
- handoff to another chat/person;
- broad cross-layer audit;
- high-risk source-of-truth consistency review;
- major migration or cascade review;
- external review package;
- output that must be continued without prior context.
```

Do not make the user ask for Level 2 when Level 2 triggers are already present.

If unsure between Level 1 and Level 2, use Level 2.

If unsure between Level 2 and Level 3, use Level 2 unless the answer must serve as a standalone handoff/audit artifact.

### 3B. Work Quality Is Not Reduced By Level

A Level 1 answer may still require careful reasoning, relevant context recall, a narrow file/source check or web verification when needed for correctness.

Do not use Level 1 as permission to skip necessary reasoning, context checks, source checks, safety checks or accepted-decision preservation.

When the work itself requires many files, many sources, synchronization, audit or planning, Level 2 or Level 3 normally applies because the work must be exposed for review.

## 4. Level 1 — Short Answer

Use Level 1 for simple questions, quick decisions, narrow commands or one-step local troubleshooting when the task has no Level 2 or Level 3 triggers.

Template:

```text
## Short Answer

...

## Important Limits

- ...

## Next Step

...
```

Rules:

```text
- Keep it short.
- Mention major uncertainty if it matters.
- Do the checks required by the task even if they are summarized briefly.
- Do not include a full sources block unless the answer depends on specific checked files.
```

## 5. Level 2 — Default Serious Answer

Use Level 2 for non-trivial planning, documentation, repo analysis, draft discussion, file/code update planning, synchronization, diff review, source-of-truth reasoning or architectural reasoning.

Template:

```text
## 1. Short Conclusion

...

## 2. Task And Scope

Task understood as:
...

In scope:
- ...

Out of scope:
- ...

## 3. Sources And Coverage

Checked:
- ...

Not checked:
- ...

Valid sources for this task:
- ...

Supporting / non-canonical sources:
- ...

Answer limits:
- ...

## 4. Answer / Result

...

## 5. Assumptions, Questions And Risks

Assumptions:
- ...

Open questions:
- ...

Risks:
- ...

## 6. Verification

How to verify:
- ...

What another chat/person should check:
- ...

## 7. Next Step

...
```

The `Sources And Coverage` section is the most important part of this level.

It allows another chat to:

```text
- see what the first chat actually checked;
- inspect sources that were not checked;
- challenge invalid source choices;
- verify whether the answer is based on current docs, current code, historical notes or assumptions.
```

## 6. Level 3 — Review / Handoff Answer

Use Level 3 when the output will be passed to another chat/person for review or continuation.

Typical cases:

```text
- audit report;
- documentation update plan review;
- architecture/source-versioning proposal;
- domain/slice/scenario draft review;
- implementation handoff;
- post-edit summary needing verification;
- high-risk repo/doc consistency conclusion;
- broad cross-layer review;
- major migration/cascade review.
```

Template:

```text
## 1. Task / Role / Mode

Task:
...

Role:
...

Mode:
answer / plan / draft / review / apply / handoff

Status:
complete / partial / blocked / needs review

## 2. Scope

In scope:
- ...

Out of scope:
- ...

Explicitly not doing:
- ...

## 3. Sources / Evidence / Coverage

Checked:
- ...

Not checked:
- ...

Valid sources for this task:
- ...

Supporting / non-canonical sources:
- ...

Evidence limits:
- ...

## 4. Result

...

## 5. Findings / Decisions

Finding 1:
- source:
- meaning:
- impact:

Finding 2:
- source:
- meaning:
- impact:

## 6. Assumptions / Questions / Risks

Assumptions:
- ...

Blocking questions:
- ...

Non-blocking questions:
- ...

Risks:
- ...

## 7. Verification

Self-check performed:
- ...

How another chat/person can verify:
- ...

Suggested reviewer focus:
- ...

## 8. Handoff Summary

For another chat:
- task:
- scope:
- key result:
- sources checked:
- sources not checked:
- risks:
- what to review:
- next action:
```

Level 3 is not mandatory for every answer.

Use it when the answer needs to survive outside the original chat context.

## 6A. File Update Overview For File/Docs/Code Updates

For Level 2 or Level 3 answers that plan, create, review or verify non-trivial file, documentation or code changes, end the normal reviewable answer with a File Update Overview when a structured file-change summary would help review.

Use:

```text
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
```

The File Update Overview is a final summary block. It does not replace the main answer.

Use-case rows may reference File Update Overview as an expected output shape, but they do not own its format.
'@

$reviewable = Replace-RegexOnce $reviewable "## 2\. Core Rule.*?(?=\r?\n## 7\. Sources And Coverage Rule)" $newLevelsBlock "replace core/detail levels block"

if ($reviewable.Contains("### Obs / Discussion Context Recheck")) {
  throw "Obs section already exists in reviewable workflow; refusing to duplicate."
}

$obsSection = @'

### Obs / Discussion Context Recheck

Canonical command:

```text
обс
```

Aliases:

```text
перепроверь обсуждение
перепроверь предыдущие обсуждения
вспомни договорённости
context recheck
discussion recheck
```

Purpose:

```text
Re-check relevant prior discussion, accepted decisions, rejected options, naming, scope, non-goals, constraints and follow-ups before answering.
```

`обс` changes prior-discussion coverage. It does not change edit permission and does not by itself force Level 2 or Level 3.

It can combine with any response level:

```text
обс + narrow task with no Level 2/3 triggers
  may produce Level 1 after the relevant discussion is checked.

обс + planning / archive / audit / update task
  normally produces Level 2.

обс + standalone handoff / broad audit
  may produce Level 3.
```

The agent should:

```text
- identify relevant prior discussion available in the current context;
- preserve accepted decisions unless the user asks to reopen them;
- distinguish prior discussion from current repo/file evidence;
- avoid reinventing an already accepted command, mode or boundary;
- report important prior context that was unavailable or not checked;
- avoid treating `обс` as permission to edit files or skip current evidence checks.
```
'@

$commandAnchor = "Response-level commands affect answer format, checking behavior, active context or reuse of previous context. They do not grant permission to edit files, commit changes, delete files, move files, create PRs or skip necessary evidence checks for claims that require current proof.`r`n"
if (-not $reviewable.Contains($commandAnchor)) {
  $commandAnchor = "Response-level commands affect answer format, checking behavior, active context or reuse of previous context. They do not grant permission to edit files, commit changes, delete files, move files, create PRs or skip necessary evidence checks for claims that require current proof.`n"
}
$reviewable = Replace-ExactOnce $reviewable $commandAnchor ($commandAnchor + $obsSection + "`n") "insert obs response-level command"

$oldDoNot = @'
## 12. Do Not

```text
- Do not force a heavy format on every casual answer.
- Do not hide unchecked sources.
- Do not claim current implementation truth without checking current repo evidence.
- Do not mix canonical sources with supporting/historical sources without labeling them.
- Do not make assumptions invisible in prose.
- Do not make another chat guess what was checked.
- Do not use section-level source blocks as a substitute for actually checking sources.
- Do not ignore a `keep prev` correction by rewriting the answer from scratch without preserving the useful previous structure/content.
- Do not use `no ch` / `без изм` to skip targeted checks that are required before writes, deletes, renames or current-state claims.
- Do not use `use archive` / `арх` to overclaim remote branch truth when archive freshness is uncertain.
- Do not treat `драфт` / `обнови` as a new draft request when there is an active draft context.
- Do not silently promote a one-pass additional source into a default template/source requirement.
```
'@
$newDoNot = @'
## 12. Do Not

```text
- Do not force a heavy format on every casual answer.
- Do not treat Level 1 as permission to skip necessary reasoning, context checks, source checks, safety checks or accepted-decision preservation.
- Do not make the user ask for Level 2 when Level 2 triggers are already present.
- Do not hide unchecked sources.
- Do not claim current implementation truth without checking current repo evidence.
- Do not mix canonical sources with supporting/historical sources without labeling them.
- Do not make assumptions invisible in prose.
- Do not make another chat guess what was checked.
- Do not use section-level source blocks as a substitute for actually checking sources.
- Do not ignore a `keep prev` correction by rewriting the answer from scratch without preserving the useful previous structure/content.
- Do not use `no ch` / `без изм` to skip targeted checks that are required before writes, deletes, renames or current-state claims.
- Do not use `use archive` / `арх` to overclaim remote branch truth when archive freshness is uncertain.
- Do not treat `драфт` / `обнови` as a new draft request when there is an active draft context.
- Do not treat `обс` as permission to edit files, skip current evidence checks or reopen accepted decisions without request.
- Do not silently promote a one-pass additional source into a default template/source requirement.
```
'@
$reviewable = Replace-ExactOnce $reviewable $oldDoNot $newDoNot "update Do Not section"

$oldSuccessTail = @'
- active draft/update commands continue the active work instead of restarting from scratch;
- Source Delta makes newly used sources and not-rechecked sources visible when an answer/draft changes;
- response structure helps verification without adding unnecessary bureaucracy.
```
'@
$newSuccessTail = @'
- active draft/update commands continue the active work instead of restarting from scratch;
- Source Delta makes newly used sources and not-rechecked sources visible when an answer/draft changes;
- Level 2/3 escalation happens automatically when task breadth requires reviewability;
- `обс` can re-check prior discussion without being confused with edit permission or answer level;
- File Update Overview is used as a final summary block when non-trivial file/docs/code updates need file responsibility/change visibility;
- response structure helps verification without adding unnecessary bureaucracy.
```
'@
$reviewable = Replace-ExactOnce $reviewable $oldSuccessTail $newSuccessTail "update Success Criteria"

Write-Utf8NoBomFile $reviewablePath $reviewable
Write-Host "Updated $reviewablePath"

# ---------------------------------------------------------------------------
# planning-use-case-map.md
# ---------------------------------------------------------------------------

$useCase = Read-Utf8File $useCasePath

if ($useCase.Contains("## 8A. Response Level And File Update Overview References")) {
  throw "Response Level / File Update Overview section already exists in use-case map; refusing to duplicate."
}

$insert8A = @'
Use a shorter version when the update is simple.

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

## 9. Repeated / Continuation Commands
'@
$useCase = Replace-ExactOnce $useCase "Use a shorter version when the update is simple.`r`n`r`n## 9. Repeated / Continuation Commands" $insert8A "insert section 8A"
if ($useCase -eq (Read-Utf8File $useCasePath)) {
  # fallback LF
  $useCase = Replace-ExactOnce $useCase "Use a shorter version when the update is simple.`n`n## 9. Repeated / Continuation Commands" $insert8A "insert section 8A LF"
}

$oldRows = @'
| `расширь` | Add depth/examples/edge cases without silently changing scope. | Ask what to expand if ambiguous. | Reuse / targeted. | Conversation plus named/new sources if needed. | Expanded section/answer with scope note. |
| `перепроверь`, `recheck` | Recheck active answer/draft/source coverage. | Recheck last answer or ask target. | Targeted / full depending risk. | Conversation plus relevant sources. | Findings and corrections/no-change result. |
'@
$newRows = @'
| `расширь` | Add depth/examples/edge cases without silently changing scope. | Ask what to expand if ambiguous. | Reuse / targeted. | Conversation plus named/new sources if needed. | Expanded section/answer with scope note. |
| `обс`, `перепроверь обсуждение`, `context recheck` | Re-check relevant prior discussion, accepted decisions and constraints before answering. | Re-check available prior discussion or say what context is unavailable. | Reuse / targeted; full only if task requires it. | Conversation plus sources only if needed. | Context/discussion recheck result; combine with Level 1/2/3 by task breadth. |
| `перепроверь`, `recheck` | Recheck active answer/draft/source coverage. | Recheck last answer or ask target. | Targeted / full depending risk. | Conversation plus relevant sources. | Findings and corrections/no-change result. |
'@
$useCase = Replace-ExactOnce $useCase $oldRows $newRows "insert obs repeated command row"

$replacements = @(
  @{
    Old = '| “посмотри / распланируй / проверь docs” | Non-trivial planning/docs work | Optional | Full first time; targeted later | GitHub/archive/conversation by context | workflow activation; reviewable output | `planning/README.md`, `workflow-activation-map.md`, `planning-doc-responsibility-map.md` | README + activation map | Reviewable answer/plan | Edits require approval |'
    New = '| “посмотри / распланируй / проверь docs” | Non-trivial planning/docs work | Optional | Full first time; targeted later | GitHub/archive/conversation by context | workflow activation; reviewable output | `planning/README.md`, `workflow-activation-map.md`, `planning-doc-responsibility-map.md` | README + activation map | Level 2 reviewable answer/plan | Edits require approval |'
    Label = 'docs planning row expected output'
  },
  @{
    Old = '| “обнови docs” | Documentation update | Optional | Targeted/full by scope | GitHub for writes; archive for read-only if requested | docs update plan/workflow; local-global sync | documentation README/map/workflows | activation map + docs workflows | Plan or applied update | GitHub writes require approval |'
    New = '| “обнови docs” | Documentation update | Optional | Targeted/full by scope | GitHub for writes; archive for read-only if requested | docs update plan/workflow; local-global sync | documentation README/map/workflows | activation map + docs workflows | Level 2 update plan or applied update + File Update Overview when files are planned/changed/reviewed | GitHub writes require approval |'
    Label = 'docs update row expected output'
  },
  @{
    Old = '| “добавь template / workflow / command / draft format / пример” | Documentation governance update | Optional | Full first time; targeted later | GitHub/archive/conversation by context | docs update workflow; example coverage decision when template/output shape changes; reviewable output | documentation README/map/workflows; `planning/documentation/example-coverage-workflow.md` when template/output/command/draft example coverage matters; relevant owner workflow/template; principle section references in §3A | docs architecture principles + use-case map + example coverage workflow | Update plan or replacement package; example coverage decision if applicable | Edits/packages require approval |'
    New = '| “добавь template / workflow / command / draft format / пример” | Documentation governance update | Optional | Full first time; targeted later | GitHub/archive/conversation by context | docs update workflow; example coverage decision when template/output shape changes; reviewable output | documentation README/map/workflows; `planning/documentation/example-coverage-workflow.md` when template/output/command/draft example coverage matters; relevant owner workflow/template; principle section references in §3A | docs architecture principles + use-case map + example coverage workflow | Level 2 update plan or replacement package + File Update Overview when files are planned/changed/reviewed; example coverage decision if applicable | Edits/packages require approval |'
    Label = 'governance row expected output'
  },
  @{
    Old = '| “source usage”, “source/version”, “каскад зависимостей”, “stale downstream”, “версии сорсов”, “инкапсуляция слоёв” | Source usage / cascade governance or pilot work | Optional | Full first time; targeted later | GitHub/archive/conversation by context | source usage cascade governance plan; principles §12A/13/14/15; PMR-002 until full workflow exists | `planning/documentation/source-usage-cascade-governance-plan.md`; `planning/documentation/source-usage-pilots/README.md`; pilot register if user asks to work on a pilot; relevant source/domain/slice docs only for real pilot fill | layer encapsulation principle + source usage governance plan | Governance plan, pilot skeleton, pilot fill plan or replacement package | Do not pretend full cascade workflow exists; do not add ad hoc version fields |'
    New = '| “source usage”, “source/version”, “каскад зависимостей”, “stale downstream”, “версии сорсов”, “инкапсуляция слоёв” | Source usage / cascade governance or pilot work | Optional | Full first time; targeted later | GitHub/archive/conversation by context | source usage cascade governance plan; principles §12A/13/14/15; PMR-002 until full workflow exists | `planning/documentation/source-usage-cascade-governance-plan.md`; `planning/documentation/source-usage-pilots/README.md`; pilot register if user asks to work on a pilot; relevant source/domain/slice docs only for real pilot fill | layer encapsulation principle + source usage governance plan | Level 2 governance plan, pilot skeleton, pilot fill plan or replacement package + File Update Overview when files are planned/changed/reviewed | Do not pretend full cascade workflow exists; do not add ad hoc version fields |'
    Label = 'source usage row expected output'
  },
  @{
    Old = '| “давай архив”, “собери архив”, “replacement package”, “archive for manual apply” | Replacement archive/package generation | Optional | Targeted/full by package scope | GitHub/archive/conversation by context | replacement file generation guide; docs update workflow when docs change; reviewable output | `planning/replacement-file-generation-guide.md`, target files, docs update workflow when docs are changed | replacement guide + workflow activation | ZIP package with `MANIFEST.md`, `APPLY.md`, `replacement-files/<repo-relative-path>` complete files | Direct repo edits not allowed unless separately approved |'
    New = '| “давай архив”, “собери архив”, “replacement package”, “archive for manual apply” | Replacement archive/package generation | Optional | Targeted/full by package scope | GitHub/archive/conversation by context | replacement file generation guide; docs update workflow when docs change; reviewable output | `planning/replacement-file-generation-guide.md`, target files, docs update workflow when docs are changed | replacement guide + workflow activation | Level 2 archive/package response + File Update Overview; ZIP package with `MANIFEST.md`, `APPLY.md`, `replacement-files/<repo-relative-path>` complete files | Direct repo edits not allowed unless separately approved |'
    Label = 'archive row expected output'
  },
  @{
    Old = '| “поправь ссылки во многих файлах” | Mechanical link/path/name sync | Optional | Targeted/broad search | GitHub or archive | docs update workflow; local-global sync | target files/search source | docs update workflow output mode rules | Link-sync plan/one bundled commit when tool-supported | Bundled/bulk mode should be used when approved; if unavailable, stop and disclose before per-file writes |'
    New = '| “поправь ссылки во многих файлах” | Mechanical link/path/name sync | Optional | Targeted/broad search | GitHub or archive | docs update workflow; local-global sync | target files/search source | docs update workflow output mode rules | Level 2 link-sync plan + File Update Overview; one bundled commit when tool-supported | Bundled/bulk mode should be used when approved; if unavailable, stop and disclose before per-file writes |'
    Label = 'link sync row expected output'
  },
  @{
    Old = '| “проверь current/implemented status” | Status reconciliation | Optional | Targeted/full by claim | Current branch evidence unless archive explicitly accepted | status reconciliation | status workflow + code/tests/generated artifacts | activation map status chain | Status findings/sync plan | Current-state claims need evidence |'
    New = '| “проверь current/implemented status” | Status reconciliation | Optional | Targeted/full by claim | Current branch evidence unless archive explicitly accepted | status reconciliation | status workflow + code/tests/generated artifacts | activation map status chain | Level 2 status findings/sync plan | Current-state claims need evidence |'
    Label = 'status row expected output'
  }
)

foreach ($r in $replacements) {
  $useCase = Replace-ExactOnce $useCase $r.Old $r.New $r.Label
}

Write-Utf8NoBomFile $useCasePath $useCase
Write-Host "Updated $useCasePath"

# ---------------------------------------------------------------------------
# Review diff
# ---------------------------------------------------------------------------

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

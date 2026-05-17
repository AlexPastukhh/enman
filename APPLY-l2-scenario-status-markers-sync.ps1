# L2 scenario status markers sync
# Docs-only script. It adds a uniform marker rule and scenario-local marker sections.

$ErrorActionPreference = "Stop"

function Write-TextFileUtf8 {
    param(
        [Parameter(Mandatory=$true)][string]$Path,
        [Parameter(Mandatory=$true)][string]$Content
    )
    $dir = Split-Path -Parent $Path
    if ($dir -and -not (Test-Path $dir)) {
        New-Item -ItemType Directory -Force -Path $dir | Out-Null
    }
    Set-Content -Path $Path -Value $Content -Encoding UTF8
}

function Add-SectionIfMissing {
    param(
        [Parameter(Mandatory=$true)][string]$Path,
        [Parameter(Mandatory=$true)][string]$Marker,
        [Parameter(Mandatory=$true)][string]$Section
    )

    if (-not (Test-Path $Path)) {
        Write-Host "Skip missing $Path"
        return
    }

    $text = Get-Content -Raw -Path $Path
    if ($text.Contains($Marker)) {
        Write-Host "Already contains marker $Marker in $Path"
        return
    }

    $newText = $text.TrimEnd() + "`n`n" + $Section.Trim() + "`n"
    Set-Content -Path $Path -Value $newText -Encoding UTF8
    Write-Host "Updated $Path"
}

function Set-ScenarioMarkerSection {
    param(
        [Parameter(Mandatory=$true)][string]$Path,
        [Parameter(Mandatory=$true)][string]$Section
    )

    if (-not (Test-Path $Path)) {
        Write-Host "Skip missing $Path"
        return
    }

    $text = Get-Content -Raw -Path $Path
    $sectionTrimmed = $Section.Trim()

    $pattern = "(?ms)`n## Diagram / Implementation Markers`n.*?(?=`n## [0-9A-Za-z])"
    if ($text -match $pattern) {
        $newText = [regex]::Replace($text, $pattern, "`n" + $sectionTrimmed + "`n", 1)
    } elseif ($text.Contains("## Diagram / Implementation Markers")) {
        Write-Host "Marker heading exists but replacement pattern did not match in $Path; appending sync note"
        $newText = $text.TrimEnd() + "`n`n" + $sectionTrimmed + "`n"
    } else {
        $newText = $text.TrimEnd() + "`n`n" + $sectionTrimmed + "`n"
    }

    Set-Content -Path $Path -Value $newText -Encoding UTF8
    Write-Host "Scenario markers synced: $Path"
}

$markerDoc = @'
# Scenario Status Markers For Diagrams

Status: current / diagram-facing scenario marker rule  
Scope: how scenario docs mark current/planned/deferred scenario elements so VKR diagrams can show the project after L1 without implying that every L2 element is already implemented.

## 1. Purpose

The project is no longer only an L1 client/request foundation. Scenario docs may now describe:

```text
- implemented L1/L2 behavior;
- accepted L2 target behavior;
- planned implementation slices;
- deferred future extensions;
- unresolved questions.
```

For diagrams and diploma/VKR text, future implementation work should be marked consistently.

## 2. Allowed markers

Use the same marker vocabulary as diagram-generation docs:

```text
[CORE]
[IMPLEMENTED]
[DESIGNED]
[PLANNED]
[DEFERRED]
[QUESTION]
```

| Marker | Meaning in scenario docs |
|---|---|
| `[CORE]` | Core diploma/MVP concept or flow. Pair with another marker when implementation status matters. |
| `[IMPLEMENTED]` | Current repo implementation evidence confirms the element. Do not use only because a scenario says it should exist. |
| `[DESIGNED]` | Accepted scenario/domain direction; behavior is stable enough for diagrams but implementation evidence is not required. |
| `[PLANNED]` | Intended future/next implementation work, normally backed by a slice draft or planned slice family. |
| `[DEFERRED]` | Known extension point outside the current cut. |
| `[QUESTION]` | Open conflict or unresolved decision that can affect diagram meaning. |

## 3. Scenario-local marker section

When a scenario mixes current, planned and future behavior, add a section:

```markdown
## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| ... | [PLANNED] | ... |
```

Rules:

```text
- Use [PLANNED] for future implementation work that is in the current L2 plan.
- Use [DEFERRED] for future extension points outside current L2.
- Use [DESIGNED] for accepted domain/scenario semantics that diagrams may show as target model.
- Use [QUESTION] for route/contract/status ambiguity.
- Use [IMPLEMENTED] only when the scenario doc cites or is updated from current implementation evidence.
```

## 4. Diagram preflight rule

Diagram Chat must still verify implementation status from the current repository.

Scenario-local markers are planning hints:

```text
[PLANNED] in a scenario can become [IMPLEMENTED] in a diagram only after repo evidence confirms implementation.
[DESIGNED] can stay [DESIGNED] even when no implementation exists yet.
[DEFERRED] should be shown as outside current cut, not as a main flow.
```

## 5. L2 default interpretation

For L2 Employee/Review/Agreement scenarios:

```text
- review and agreement scenario specs are target/current planning source;
- server/client slice drafts define implementation boundaries;
- agreement exchange commands/reads are mostly [PLANNED] until code/OpenAPI confirms them;
- cross-aggregate/domain invariants can be [DESIGNED];
- old validation/security terms and obsolete SC-14 client-data-verification wording must not become diagram source.
```

## 6. Do not

```text
- Do not create new marker names like [FUTURE] or [TODO].
- Do not mark stale/old wording as [PLANNED].
- Do not use scenario markers to override code evidence.
- Do not draw [DEFERRED] flows as if they are current main flows.
- Do not let [PLANNED] mean “already implemented”.
```

'@

Write-TextFileUtf8 -Path "planning/diagrams/scenario-status-marker-rules.md" -Content $markerDoc

$scenarioPrinciplesSection = @'
## 14. Scenario-local Diagram Status Markers

Marker: SCENARIO-STATUS-MARKERS-2026-05

Scenario docs may mark planned/future/deferred elements for diagram and diploma/VKR work using:

```text
[CORE]
[IMPLEMENTED]
[DESIGNED]
[PLANNED]
[DEFERRED]
[QUESTION]
```

Use:

```text
planning/diagrams/scenario-status-marker-rules.md
```

Rules:

```text
- [PLANNED] marks future implementation work in the current plan.
- [DEFERRED] marks extension points outside the current cut.
- [DESIGNED] marks accepted scenario/domain direction.
- [QUESTION] marks unresolved source/contract conflicts.
- [IMPLEMENTED] requires current repo implementation evidence, not only scenario text.
```

Scenario marker sections are diagram-facing planning hints. They do not replace scenario source-of-truth text, slice docs, domain drafts, current OpenAPI/code evidence or implementation verification.
'@

Add-SectionIfMissing -Path "planning/scenario-specification-principles.md" -Marker "SCENARIO-STATUS-MARKERS-2026-05" -Section $scenarioPrinciplesSection

$diagramsReadmeSection = @'
## Scenario Status Markers

Marker: SCENARIO-STATUS-MARKERS-2026-05

For post-L1/L2 diagrams, scenario files may contain `## Diagram / Implementation Markers` sections.

Read:

```text
planning/diagrams/scenario-status-marker-rules.md
```

Use the same marker vocabulary as diagram generation:

```text
[CORE]
[IMPLEMENTED]
[DESIGNED]
[PLANNED]
[DEFERRED]
[QUESTION]
```

Important:

```text
Scenario markers help diagrams distinguish current target, planned implementation and deferred extensions.
They do not prove implementation status.
Diagram preflight must still inspect current repo evidence before using [IMPLEMENTED].
```
'@

Add-SectionIfMissing -Path "planning/diagrams/README.md" -Marker "SCENARIO-STATUS-MARKERS-2026-05" -Section $diagramsReadmeSection

$diagramPromptSection = @'
## Scenario-local Marker Sections

Marker: SCENARIO-STATUS-MARKERS-2026-05

Before generating diagrams, the Diagram Chat must read scenario-local marker sections when present:

```text
## Diagram / Implementation Markers
```

These sections use:

```text
[CORE]
[IMPLEMENTED]
[DESIGNED]
[PLANNED]
[DEFERRED]
[QUESTION]
```

Rules:

```text
- Treat [PLANNED] as future implementation work, not as already implemented.
- Treat [DEFERRED] as extension/future cut, not current main flow.
- Treat [DESIGNED] as accepted target semantics.
- Promote to [IMPLEMENTED] only with current repo evidence.
- Do not invent non-standard marker names such as [FUTURE] or [TODO].
```
'@

Add-SectionIfMissing -Path "planning/diagrams/diagram-prompt-generation-workflow.md" -Marker "SCENARIO-STATUS-MARKERS-2026-05" -Section $diagramPromptSection

$scenarioIndexSection = @'
## Scenario-local Diagram Markers

Marker: SCENARIO-STATUS-MARKERS-2026-05

L2 scenario specs may include `## Diagram / Implementation Markers` sections so diagrams can show planned/future/deferred items uniformly.

Use:

```text
planning/diagrams/scenario-status-marker-rules.md
```

Interpretation:

```text
[PLANNED] = future implementation work in the current L2 plan.
[DEFERRED] = future extension outside current L2 cut.
[DESIGNED] = accepted domain/scenario target.
[QUESTION] = unresolved source/contract question.
[IMPLEMENTED] = only with current repo evidence.
```
'@

Add-SectionIfMissing -Path "planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md" -Marker "SCENARIO-STATUS-MARKERS-2026-05" -Section $scenarioIndexSection

$planningReadmeSection = @'
## Scenario / Diagram Status Markers

Marker: SCENARIO-STATUS-MARKERS-2026-05

For post-L1/L2 scenario and diagram work, use:

```text
planning/diagrams/scenario-status-marker-rules.md
```

Scenario docs may mark future implementation and deferred extension points uniformly with:

```text
[PLANNED]
[DEFERRED]
[DESIGNED]
[QUESTION]
```

`[IMPLEMENTED]` still requires current repo evidence.
'@

Add-SectionIfMissing -Path "planning/README.md" -Marker "SCENARIO-STATUS-MARKERS-2026-05" -Section $planningReadmeSection

$sc06 = @'
## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| Employee request dashboard/list read | `[PLANNED]` | L2 employee read surface; diagram preflight may promote to `[IMPLEMENTED]` only with repo evidence. |
| Review state markers on rows | `[PLANNED]` | Shows NotStarted / StartedByCurrentEmployee / StartedByAnotherEmployee-style state in the employee dashboard. |
| Dashboard StartReview row entry point | `[PLANNED]` | Future/target host placement for the same StartReview command sidecar. |
| Department/region/assignment visibility model | `[DEFERRED]` | Future authorization/read-filtering extension; first pass uses temporary active-Employee visibility policy. |
| Agreement exchange work | `[DEFERRED]` | Belongs to `SC-13*`, not the employee request dashboard read scenario. |
'@

$sc07a = @'
## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| Employee request details read surface | `[PLANNED]` | L2 details surface for request/applicant/review state. Promote to `[IMPLEMENTED]` only with current repo evidence. |
| Review action availability display | `[PLANNED]` | Details page shows whether Start/Approve/Reject are available or blocked. |
| StartReview details entry point | `[PLANNED]` | Details action area can host the same StartReview feature sidecar as dashboard row. |
| Approve/Reject action area | `[PLANNED]` | Details-only first pass for approve/reject command sidecars. |
| Employee assignment/claiming beyond review start | `[DEFERRED]` | Future workflow; not part of current details scenario. |
| Agreement exchange after approval | `[DEFERRED]` | Belongs to `SC-13D` and related agreement exchange slices. |
'@

$sc07b = @'
## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| StartReview command behavior | `[PLANNED]` | Current L2 planned/implemented candidate; diagram preflight must verify code/OpenAPI before marking `[IMPLEMENTED]`. |
| StartReview two entry points | `[PLANNED]` | Dashboard row and details action area host one command feature, not two scenarios. |
| ApproveReview command behavior | `[PLANNED]` | Details-only review completion command; does not create AgreementProposalExchange. |
| RejectReview command behavior | `[PLANNED]` | Details-only review rejection; feedback is optional in current direction. |
| Agreement exchange start after approval | `[PLANNED]` | Enabled by approval but owned by `SC-13D` / `SL-AGR-EXCH-001`, not by ApproveReview. |
| Employee assignment/department rules | `[DEFERRED]` | Future visibility/assignment extension outside current review command scenarios. |
'@

$sc13a = @'
## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| Client agreement exchange list | `[PLANNED]` | Current L2 planned read surface for existing AgreementProposalExchange records. |
| Client filtering by `AgreementProposalExchange.ClientAccountId` | `[DESIGNED]` | Accepted ownership/participant rule for client access. |
| Exchange status summary | `[PLANNED]` | List shows AwaitingClientConfirmation / AwaitingEmployeeResponse / Accepted / FinallyRefused summary. |
| Proposal version history | `[DEFERRED]` | Full history belongs to details (`SC-13B` / `SC-13D`), not list summary. |
| Document bytes/download | `[DEFERRED]` | Belongs to future document/storage slice, not agreement list. |
'@

$sc13b = @'
## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| Client agreement proposal details | `[PLANNED]` | Client details surface for active proposal and proposal history. |
| Client accept active Employee proposal | `[PLANNED]` | Current L2 planned command slice; success accepts active proposal and exchange. |
| Client sends own counter-proposal version | `[PLANNED]` | Current L2 planned counter-proposal branch inside `SL-AGR-EXCH-002`. |
| `SupersededByCounterProposal` replacement semantics | `[DESIGNED]` | Accepted terminology: counter-proposal replacement is not ordinary `Rejected`. |
| Client final refusal | `[DEFERRED]` | Not part of current first pass; final refusal is Employee-only in current L2. |
| Binary upload/storage | `[DEFERRED]` | Agreement proposal uses `AgreementDocumentRef`; storage/upload is separate future work. |
'@

$sc13c = @'
## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| Employee agreement exchange list | `[PLANNED]` | Employee-facing read surface for existing agreement exchanges. |
| Employee access: any active Employee first pass | `[DESIGNED]` | Accepted first-pass service model; no `ResponsibleEmployeeId` guard. |
| Employee opens existing exchange details | `[PLANNED]` | Existing exchange details/actions are agreement area work. |
| Starting exchange from approved request details | `[PLANNED]` | Start is triggered from Employee request details (`SC-13D`), because the exchange does not exist yet. |
| Department/assignment ownership model | `[DEFERRED]` | Future employee visibility/assignment model if needed. |
'@

$sc13d = @'
## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| Start exchange from Employee request details | `[PLANNED]` | Current L2 planned command: Approved request -> create exchange + initial Employee proposal. |
| Initial Employee proposal version 1 | `[DESIGNED]` | Accepted domain behavior: start is not empty; it creates proposal version 1. |
| Store `ClientAccountId` from approved request owner | `[DESIGNED]` | Accepted participant/ownership invariant for future client actions. |
| Employee sends new version after client counter-proposal | `[PLANNED]` | Current L2 planned Employee branch of `SL-AGR-EXCH-002`. |
| Proposal author `Sender` / `SenderId` | `[DESIGNED]` | Accepted proposal-author model; do not use `EmployeeRef`/`ClientRef`. |
| API/client choosing proposal version | `[DEFERRED]` | Not allowed in current domain; exchange assigns version. |
'@

$sc13e = @'
## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| Employee final refusal command | `[PLANNED]` | Current L2 planned command slice for ending active exchange. |
| Exchange becomes `FinallyRefused` | `[DESIGNED]` | Accepted exchange lifecycle result. |
| Request becomes `AgreementExchangeFailed` | `[DESIGNED]` | Accepted request lifecycle result through separate request domain method. |
| Cross-aggregate orchestration | `[DESIGNED]` | Application service orchestrates exchange and request; aggregates do not mutate each other directly. |
| Client final refusal | `[DEFERRED]` | Out of scope for current first pass. |
| Final refusal as new proposal version/entity | `[DEFERRED]` | Explicitly not current direction; final refusal is direct exchange state. |
'@

$sc14 = @'
## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| `AgreementDocumentRef` metadata reference | `[DESIGNED]` | Accepted value object for proposal document metadata. |
| Proposal stores document metadata, not bytes | `[DESIGNED]` | Current domain/scenario direction. |
| ProposalComment optional value object | `[DESIGNED]` | Optional comment; non-empty/max-length when present. |
| Binary upload/storage adapter | `[DEFERRED]` | Future infrastructure/document slice, not current scenario behavior. |
| Old `SC-14 Client Data Verification` meaning | `[DEFERRED]` | Not current `SC-14`; current `SC-14` means Agreement Documents. |
'@

Set-ScenarioMarkerSection -Path "planning/diagrams/scenario-text-specs/SC-06-employee-request-dashboard.md" -Section $sc06
Set-ScenarioMarkerSection -Path "planning/diagrams/scenario-text-specs/SC-07A-employee-request-details.md" -Section $sc07a
Set-ScenarioMarkerSection -Path "planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md" -Section $sc07b
Set-ScenarioMarkerSection -Path "planning/diagrams/scenario-text-specs/SC-13A-client-agreements.md" -Section $sc13a
Set-ScenarioMarkerSection -Path "planning/diagrams/scenario-text-specs/SC-13B-client-agreement-proposal-details-response.md" -Section $sc13b
Set-ScenarioMarkerSection -Path "planning/diagrams/scenario-text-specs/SC-13C-employee-agreements.md" -Section $sc13c
Set-ScenarioMarkerSection -Path "planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md" -Section $sc13d
Set-ScenarioMarkerSection -Path "planning/diagrams/scenario-text-specs/SC-13E-agreement-final-refusal.md" -Section $sc13e
Set-ScenarioMarkerSection -Path "planning/diagrams/scenario-text-specs/SC-14-agreement-documents.md" -Section $sc14

$syncNote = @'
# L2 Scenario Status Marker Sync

Status: docs-only sync note  
Scope: uniform future/current/deferred markers for L2 scenarios and diagram generation

## Summary

This sync introduces a consistent scenario-local marker section:

```text
## Diagram / Implementation Markers
```

It is used by L2 scenario specs to distinguish:

```text
[DESIGNED] accepted target semantics
[PLANNED] future implementation work in the current L2 plan
[DEFERRED] extension points outside current cut
[QUESTION] unresolved source/contract conflicts
[IMPLEMENTED] current repo implementation evidence only
```

## Why

The project is no longer just the first L1 stage. Scenarios now include L2 review/agreement flows that are partly implemented, partly implementation-ready and partly deferred.

Uniform markers make VKR/diploma diagrams clearer without claiming that all L2 elements are already implemented.

## Rule

Scenario markers are diagram-facing planning hints.

They do not replace:

```text
- current repo code/OpenAPI evidence;
- scenario text source of truth;
- slice docs;
- domain drafts;
- implementation verification.
```

Diagram Chat must still perform repo-grounded preflight before using `[IMPLEMENTED]`.
'@

Write-TextFileUtf8 -Path "planning/documentation/l2-scenario-status-marker-sync-note.md" -Content $syncNote

Write-Host "L2 scenario status marker sync complete."

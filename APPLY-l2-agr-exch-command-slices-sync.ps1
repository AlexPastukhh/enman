$ErrorActionPreference = "Stop"

function Add-BlockIfMissing {
    param(
        [Parameter(Mandatory=$true)][string]$Path,
        [Parameter(Mandatory=$true)][string]$Marker,
        [Parameter(Mandatory=$true)][string]$Block
    )

    if (-not (Test-Path $Path)) {
        $dir = Split-Path $Path -Parent
        if ($dir -and -not (Test-Path $dir)) {
            New-Item -ItemType Directory -Force -Path $dir | Out-Null
        }
        Set-Content -Path $Path -Value "" -Encoding UTF8
    }

    $content = Get-Content -Raw -Path $Path
    if ($content -notlike "*$Marker*") {
        Add-Content -Path $Path -Value "`n$Block`n" -Encoding UTF8
    }
}

# Remove superseded placeholder names if they exist.
$obsolete = @(
    "planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.placeholder.md",
    "planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md",
    "planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md",
    "planning/slices/SL-AGR-EXCH-005-accept-active-agreement-proposal.md"
)

foreach ($path in $obsolete) {
    if (Test-Path $path) {
        Remove-Item $path -Force
    }
}

$navBlock = @"
<!-- L2-AGR-EXCH-COMMAND-SLICES-SYNC -->
## Agreement Exchange Command Slice Sync

Canonical agreement exchange command/read slice set:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Client sidecars added for command/read continuation:

```text
L2-AGR-EXCH-LIST-001.client
L2-AGR-EXCH-DETAILS-001.client
L2-AGR-EXCH-SEND-PROPOSAL-001.client
L2-AGR-EXCH-ACCEPT-001.client
```

Rules:
- current project state questions require GitHub/current branch inspection, not archives;
- server/client full drafts must keep Implementation Checklist near the end;
- server/backend/API slice drafts live in `planning/slices/`;
- client sidecar drafts live in `planning/slices/l2/`.
"@

Add-BlockIfMissing -Path "planning/slices/README.md" -Marker "L2-AGR-EXCH-COMMAND-SLICES-SYNC" -Block $navBlock
Add-BlockIfMissing -Path "planning/slices/l2/README.md" -Marker "L2-AGR-EXCH-COMMAND-SLICES-SYNC" -Block $navBlock

$registerBlock = @"
<!-- L2-AGR-EXCH-COMMAND-SLICES-SYNC -->
## Agreement Exchange Command Slices

| Slice / sidecar | Marker | Source files | Applies to | Status |
|---|---|---|---|---|
| `SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | Agreement exchange scenarios, `CC-CSRF-001` | Shared Client/Employee counter-proposal command; request-scoped route; actor branch in service; domain owns participant/turn/lifecycle invariants | drafted |
| `L2-AGR-EXCH-SEND-PROPOSAL-001-send-agreement-proposal-version.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | Agreement exchange details/read sidecar, `CC-CSRF-001` | Shared Client/Employee send proposal feature from details action slot | drafted |
| `SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | Agreement exchange scenarios, `CC-CSRF-001` | Client-only accept active Employee proposal command; no new version; accepted state through refetch | drafted |
| `L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | Agreement exchange details/read sidecar, `CC-CSRF-001` | Client-only Accept action in Client agreement exchange details action area | drafted |
| `SL-AGR-EXCH-006-final-refuse-agreement-exchange.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | Agreement exchange scenarios, `CC-CSRF-001` | Employee final refusal; full draft pending | planned |
"@
Add-BlockIfMissing -Path "planning/slices/slice-scenario-flow-behavior-register.md" -Marker "L2-AGR-EXCH-COMMAND-SLICES-SYNC" -Block $registerBlock

$questionsBlock = @"
<!-- L2-AGR-EXCH-COMMAND-SLICES-SYNC -->
## Agreement Exchange Command Questions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `Q-L2-AGR-CMD-001` | `SL-AGR-EXCH-002` | endpoint | accepted | One shared endpoint or separate Client/Employee endpoints for counter-proposal? | One shared endpoint first pass: `POST /api/requests/{requestId}/agreement-exchange/proposals`. | Shared server/client wrapper. |
| `Q-L2-AGR-CMD-002` | `SL-AGR-EXCH-002` | actor model | accepted | Introduce `AgreementExchangeActor` now? | No first pass. Controller resolves role/account id; service branches explicitly; domain owns invariants. | Simpler implementation. |
| `Q-L2-AGR-CMD-003` | `SL-AGR-EXCH-002`, `SL-AGR-EXCH-005` | ownership | accepted | How are client actions protected? | `AgreementProposalExchange.ClientAccountId`; client domain methods guard `client.Id == ClientAccountId`. | Participant security. |
| `Q-L2-AGR-CMD-004` | exchange command slices | employee ownership | accepted | Is there `ResponsibleEmployeeId` guard? | No first pass. Any active Employee can service exchange; proposal sender is tracked per version. | Avoids false employee ownership. |
| `Q-L2-AGR-CMD-005` | `SL-AGR-EXCH-005` | accept | accepted | Is accept Client-only? | Yes first pass. Employee accept out of scope. | Client-only endpoint/sidecar. |
| `Q-L2-AGR-CMD-006` | `SL-AGR-EXCH-005` | accept state | accepted | Does accept create a new version? | No. Exchange becomes `Accepted`; active proposal becomes `Accepted`; proposal count unchanged. | Test assertions. |
"@
Add-BlockIfMissing -Path "planning/slices/slice-questions-register.md" -Marker "L2-AGR-EXCH-COMMAND-SLICES-SYNC" -Block $questionsBlock

$extensionBlock = @"
<!-- L2-AGR-EXCH-COMMAND-SLICES-SYNC -->
## Agreement Exchange Command Extension Points

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-L2-AGR-CMD-001` | route shape | Counter-proposal uses request-scoped route first pass; generated OpenAPI remains source during implementation. | accepted |
| `CP-L2-AGR-CMD-002` | actor abstraction | `AgreementExchangeActor` may be introduced later if role branching repeats or grows. | future cleanup |
| `CP-L2-AGR-CMD-003` | employee ownership | `ResponsibleEmployeeId` is not a guard; assignment/ownership can be a future visibility slice. | future |
| `CP-L2-AGR-CMD-004` | accept | Employee accept is out of scope unless future scenario/domain explicitly adds it. | future |
| `CP-L2-AGR-CMD-005` | final refusal | Final refusal remains canonical `SL-AGR-EXCH-006`; full draft still pending. | planned |
"@
Add-BlockIfMissing -Path "planning/slices/slice-extension-points-register.md" -Marker "L2-AGR-EXCH-COMMAND-SLICES-SYNC" -Block $extensionBlock

$implBlock = @"
<!-- L2-AGR-EXCH-COMMAND-SLICES-SYNC -->
## Agreement Exchange Command Implementation Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-L2-AGR-CMD-001` | current-state answers | Inspect GitHub/current branch for actual implementation state. Uploaded drafts and archives are planning input only. | accepted |
| `IMPL-L2-AGR-CMD-002` | server/client full drafts | Keep `Implementation Checklist` near the end of every full server or client draft. | accepted |
| `IMPL-L2-AGR-CMD-003` | counter-proposal server | Controller may branch by role only to call service method; it must not contain lifecycle/turn/ownership logic. | accepted |
| `IMPL-L2-AGR-CMD-004` | counter-proposal domain | Domain validates client ownership, active proposal sender, turn and lifecycle invariants. | accepted |
| `IMPL-L2-AGR-CMD-005` | accept server | Use Client-only endpoint, no body, 204 success, no new proposal version. | accepted |
| `IMPL-L2-AGR-CMD-006` | client wrappers | Command wrappers live in `features/agreement-exchange/<action>/api`, not `shared/api`. | accepted |
"@
Add-BlockIfMissing -Path "planning/slices/slice-implementation-notes-register.md" -Marker "L2-AGR-EXCH-COMMAND-SLICES-SYNC" -Block $implBlock

Write-Host "L2 agreement exchange command slice docs synchronized."

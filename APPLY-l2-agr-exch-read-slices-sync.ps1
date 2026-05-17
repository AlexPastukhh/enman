$ErrorActionPreference = "Stop"

Write-Host "Applying L2 agreement exchange list/details slice sync..."

$oldFiles = @(
  "planning/slices/SL-AGR-EXCH-003-read-agreement-exchange.md",
  "planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md",
  "planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md"
)

foreach ($oldFile in $oldFiles) {
  if (Test-Path $oldFile) {
    Remove-Item $oldFile -Force
    Write-Host "Removed superseded file: $oldFile"
  }
}

function Add-Block-IfMissing {
  param(
    [string]$Path,
    [string]$Marker,
    [string]$Block
  )

  if (-not (Test-Path $Path)) {
    Write-Host "Skip missing file: $Path"
    return
  }

  $content = Get-Content $Path -Raw
  if ($content.Contains($Marker)) {
    Write-Host "Already has marker in $Path"
    return
  }

  $content = $content.TrimEnd() + "`r`n`r`n" + $Block.Trim() + "`r`n"
  Set-Content -Path $Path -Value $content -Encoding UTF8
  Write-Host "Appended block to $Path"
}

$agrNavBlock = @"
## Agreement Exchange Slice Family

Marker: AGR-EXCH-CANONICAL-NUMBERING-2026-05

Canonical server slice files:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Canonical client sidecars:

```text
L2-AGR-EXCH-LIST-001.client — Agreement Exchange List Pages
L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages
```

Superseded file names must not be kept next to canonical names:

```text
SL-AGR-EXCH-003-read-agreement-exchange.md
SL-AGR-EXCH-004-accept-active-agreement-proposal.md
SL-AGR-EXCH-005-final-refuse-agreement-exchange.md
```
"@

Add-Block-IfMissing -Path "planning/slices/README.md" -Marker "AGR-EXCH-CANONICAL-NUMBERING-2026-05" -Block $agrNavBlock
Add-Block-IfMissing -Path "planning/slices/l2/README.md" -Marker "AGR-EXCH-CANONICAL-NUMBERING-2026-05" -Block $agrNavBlock

$registerBlock = @"
## Agreement Exchange Canonical Source Map Addendum

Marker: AGR-EXCH-CANONICAL-NUMBERING-2026-05

| Slice / sidecar | Marker | Source files | Applies to | Status |
|---|---|---|---|---|
| `SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13*`, agreement behavior files | Start exchange with first Employee proposal | drafted |
| `SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13*`, agreement behavior files | Client/Employee counter-proposal versions | planned boundary |
| `SL-AGR-EXCH-003-agreement-exchange-list-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13A`, `SC-13C`, agreement behavior files | Shared agreement exchange list read | drafted |
| `L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13A`, `SC-13C`, agreement behavior files | Shared list pages/widgets for Client and Employee shells | drafted |
| `SL-AGR-EXCH-004-agreement-exchange-details-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13B`, `SC-13D`, agreement behavior files | Shared agreement exchange details read with proposal history | drafted |
| `L2-AGR-EXCH-DETAILS-001-agreement-exchange-details.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13B`, `SC-13D`, agreement behavior files | Shared details pages/widgets for Client and Employee shells | drafted |
| `SL-AGR-EXCH-005-accept-active-agreement-proposal.md` | `[SCENARIO]` / `[BEHAVIOR]` | `SC-13B`, agreement behavior files | Accept active agreement proposal | planned boundary |
| `SL-AGR-EXCH-006-final-refuse-agreement-exchange.md` | `[SCENARIO]` / `[BEHAVIOR]` | `SC-13E`, agreement behavior files | Final refuse agreement exchange | planned boundary |
"@

Add-Block-IfMissing -Path "planning/slices/slice-scenario-flow-behavior-register.md" -Marker "AGR-EXCH-CANONICAL-NUMBERING-2026-05" -Block $registerBlock

$questionsBlock = @"
## Agreement Exchange Read Slice Decisions

Marker: AGR-EXCH-READ-SLICE-DECISIONS-2026-05

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `AGR-EXCH-READ-Q-001` | `SL-AGR-EXCH-003`, `SL-AGR-EXCH-004` | endpoint shape | accepted | Use shared list/details endpoints for Client and Employee first pass? | Yes. Server branches by current session role/access; frontend uses shared entity wrappers/widgets. | Avoids duplicate read slices. |
| `AGR-EXCH-READ-Q-002` | same | client ownership | accepted | How is Client access protected? | Persist and filter by `AgreementProposalExchange.ClientAccountId`. | Requires domain/persistence field. |
| `AGR-EXCH-READ-Q-003` | same | employee ownership | accepted | Does exchange have ResponsibleEmployeeId guard? | No first pass. Any active Employee can service exchange. | Avoids false employee ownership. |
| `AGR-EXCH-READ-Q-004` | `SL-AGR-EXCH-004` | read implementation | accepted | Use application service for details read? | No. Use query handler + read repository / Dapper projection. | Keeps read slice projection-only. |
| `AGR-EXCH-READ-Q-005` | `L2-AGR-EXCH-LIST-001.client`, `L2-AGR-EXCH-DETAILS-001.client` | frontend ownership | accepted | Actor-specific wrappers first pass? | No while response shape is common. Use shared entity API/query/model and actor page shells. | Avoids duplicated client wrappers. |
"@

Add-Block-IfMissing -Path "planning/slices/slice-questions-register.md" -Marker "AGR-EXCH-READ-SLICE-DECISIONS-2026-05" -Block $questionsBlock

$implBlock = @"
## Agreement Exchange Read Slice Implementation Notes

Marker: AGR-EXCH-READ-SLICE-DECISIONS-2026-05

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-AGR-EXCH-READ-001` | list/details server reads | Client access filters by `AgreementProposalExchange.ClientAccountId`. | accepted |
| `IMPL-AGR-EXCH-READ-002` | list/details server reads | Employee access first pass allows any active Employee; do not add `ResponsibleEmployeeId` guard. | accepted |
| `IMPL-AGR-EXCH-READ-003` | details read | Use query handler + read repository / Dapper projection; do not introduce application service for read-only projection. | accepted |
| `IMPL-AGR-EXCH-READ-004` | client sidecars | Use shared entity wrappers under `entities/agreement-exchange/api`; do not add `shared/api/agreementExchangeApi.ts`. | accepted |
| `IMPL-AGR-EXCH-READ-005` | client sidecars | Page shells may differ by actor; query/model/widgets stay shared while response shape is common. | accepted |
"@

Add-Block-IfMissing -Path "planning/slices/slice-implementation-notes-register.md" -Marker "AGR-EXCH-READ-SLICE-DECISIONS-2026-05" -Block $implBlock

$extensionBlock = @"
## Agreement Exchange Read Extension Points

Marker: AGR-EXCH-READ-SLICE-DECISIONS-2026-05

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-AGR-EXCH-READ-001` | read endpoint split | Shared list/details endpoints first pass; split actor endpoints only if behavior diverges. | future if needed |
| `CP-AGR-EXCH-READ-002` | employee assignment | No ResponsibleEmployeeId guard first pass; department/assignment visibility is future. | future |
| `CP-AGR-EXCH-READ-003` | actions in read DTO | AvailableActions may be added later if client derivation becomes too complex. | future |
| `CP-AGR-EXCH-READ-004` | document bytes/download | List/details return document references only; download/storage remains future document slice. | future |
"@

Add-Block-IfMissing -Path "planning/slices/slice-extension-points-register.md" -Marker "AGR-EXCH-READ-SLICE-DECISIONS-2026-05" -Block $extensionBlock

Write-Host "Done. Review git diff -- planning"

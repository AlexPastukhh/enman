$ErrorActionPreference = "Stop"

function Add-Once {
    param(
        [Parameter(Mandatory=$true)][string]$Path,
        [Parameter(Mandatory=$true)][string]$Marker,
        [Parameter(Mandatory=$true)][string]$Text
    )

    if (-not (Test-Path $Path)) {
        Write-Host "skip missing $Path"
        return
    }

    $content = Get-Content $Path -Raw
    if ($content.Contains($Marker)) {
        Write-Host "already synchronized $Path"
        return
    }

    Add-Content -Path $Path -Value "`n<!-- $Marker -->`n$Text`n<!-- /$Marker -->`n"
    Write-Host "updated $Path"
}

$nav = @'
## Agreement exchange final refusal sync

Canonical final refusal drafts:

```text
planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md
planning/slices/l2/L2-AGR-EXCH-FINAL-REFUSE-001-employee-final-refuse-agreement-exchange.client.md
```

`SL-AGR-EXCH-006` is Employee-only backend/API command:

```text
POST /api/agreement-exchanges/{exchangeId}/final-refuse
success: 204 No Content
body: nullable FinalRefuseAgreementExchangeDto? with optional reason
```

The command orchestrates two aggregates through domain methods:

```text
exchange.FinalRefuseProposal(...)
request.MarkAgreementExchangeFailed(...)
```

Client sidecar is Employee details-only first pass. Do not add Client final refusal.
'@

Add-Once -Path "planning/slices/README.md" -Marker "AGR-EXCH-FINAL-REFUSE-SYNC" -Text $nav
Add-Once -Path "planning/slices/l2/README.md" -Marker "AGR-EXCH-FINAL-REFUSE-SYNC" -Text $nav

$register = @'
## Agreement exchange final refusal source-map sync

| Slice / sidecar | Marker | Source files | Applies to | Status |
|---|---|---|---|---|
| `SL-AGR-EXCH-006-final-refuse-agreement-exchange.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | Agreement exchange final refusal scenario/domain direction, `CC-CSRF-001` | Employee final-refuse backend command; exchange becomes `FinallyRefused`, related request becomes `AgreementExchangeFailed` | drafted |
| `L2-AGR-EXCH-FINAL-REFUSE-001-employee-final-refuse-agreement-exchange.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | Agreement exchange details/action-slot direction, `CC-CSRF-001` | Employee final-refuse client command sidecar; Employee details action area only | drafted |
'@

Add-Once -Path "planning/slices/slice-scenario-flow-behavior-register.md" -Marker "AGR-EXCH-FINAL-REFUSE-SYNC" -Text $register

$questions = @'
## Agreement exchange final refusal decisions

```text
SL-AGR-EXCH-006:
- Employee-only first pass;
- Client final refusal is out of scope;
- nullable body is allowed;
- missing/null reason is allowed;
- blank/whitespace-only reason is invalid;
- no ResponsibleEmployeeId guard;
- no command status enum;
- final refusal affects AgreementProposalExchange and related ConnectionRequest;
- application orchestrates both aggregate domain methods;
- success returns 204 No Content.
```
'@

Add-Once -Path "planning/slices/slice-questions-register.md" -Marker "AGR-EXCH-FINAL-REFUSE-SYNC" -Text $questions

$extensions = @'
## Agreement exchange final refusal extension points

```text
- Client-side final refusal: future slice only if explicitly required by scenario/domain direction.
- Final refusal audit fields beyond current exchange fields: future enhancement if DTO/persistence exposes them.
- Request failure timestamp/id fields: only assert/add if domain/persistence explicitly introduces them.
- Rich refusal reason UX/shared confirmation: future client refinement.
```
'@

Add-Once -Path "planning/slices/slice-extension-points-register.md" -Marker "AGR-EXCH-FINAL-REFUSE-SYNC" -Text $extensions

$impl = @'
## Agreement exchange final refusal implementation notes

```text
Use domain methods, not manual state assignment:
  exchange.FinalRefuseProposal(employee, reason, now)
  request.MarkAgreementExchangeFailed(exchange.Id, now)

Use one unit-of-work/transaction. If request marking fails, do not persist partial exchange refusal.

Do not add:
  ResponsibleEmployeeId guard
  per-command status enum
  Client final refusal UI/API
```
'@

Add-Once -Path "planning/slices/slice-implementation-notes-register.md" -Marker "AGR-EXCH-FINAL-REFUSE-SYNC" -Text $impl

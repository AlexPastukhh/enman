$ErrorActionPreference = "Stop"

function Copy-RepoFile($RelativePath) {
    $source = Join-Path $PSScriptRoot $RelativePath
    $target = Join-Path (Get-Location) $RelativePath
    $targetDir = Split-Path $target -Parent
    if (-not (Test-Path $targetDir)) {
        New-Item -ItemType Directory -Path $targetDir -Force | Out-Null
    }
    Copy-Item -Path $source -Destination $target -Force
}

$filesToCopy = @(
    "planning/slices/l2/L2-agreement-exchange-domain-invariants-and-actor-access.md",
    "planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md",
    "planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md",
    "planning/slices/SL-AGR-EXCH-003-read-agreement-exchange.md",
    "planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md",
    "planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md",
    "planning/slices/drafting-current-state-source-rule.md",
    "planning/slices/drafting-implementation-checklist-rule.md",
    "planning/slices/slice-draft-file-naming-and-placement.md"
)

foreach ($file in $filesToCopy) {
    Copy-RepoFile $file
}

function Add-BlockIfMissing($Path, $Marker, $Block) {
    if (-not (Test-Path $Path)) {
        New-Item -ItemType File -Path $Path -Force | Out-Null
    }

    $content = Get-Content -Raw -Path $Path
    if ($content -notlike "*$Marker*") {
        $content = $content.TrimEnd() + "`r`n`r`n" + $Block.Trim() + "`r`n"
        Set-Content -Path $Path -Value $content -Encoding UTF8
    }
}

Add-BlockIfMissing -Path "planning/slices/README.md" -Marker "planning/slices/drafting-current-state-source-rule.md" -Block @"
## Current-State / Drafting Rule Additions

For current project state and implementation status, read current GitHub/repo state first:

```text
planning/slices/drafting-current-state-source-rule.md
```

For full server/client drafts, include a concrete `Implementation Checklist` near the end:

```text
planning/slices/drafting-implementation-checklist-rule.md
```

For predictable slice file names and placement, use:

```text
planning/slices/slice-draft-file-naming-and-placement.md
```

Agreement exchange domain invariant direction:

```text
planning/slices/l2/L2-agreement-exchange-domain-invariants-and-actor-access.md
```
"@

Add-BlockIfMissing -Path "planning/slices/l2/README.md" -Marker "L2-agreement-exchange-domain-invariants-and-actor-access.md" -Block @"
## Agreement Exchange Domain / Access Direction

Agreement exchange slices use this domain/access direction:

```text
planning/slices/l2/L2-agreement-exchange-domain-invariants-and-actor-access.md
```

Summary:

```text
AgreementProposalExchange stores ClientAccountId.
Client actions verify client.Id == ClientAccountId.
Employee ownership is not guarded by ResponsibleEmployeeId in first pass.
Any active Employee can service the exchange.
Proposal authors are tracked per proposal version.
Handlers/controllers stay thin.
Application service branches by actor and calls actor-specific domain methods.
Read services use actor-specific access filters.
```
"@

Add-BlockIfMissing -Path "planning/slices/slice-scenario-flow-behavior-register.md" -Marker "L2-agreement-exchange-domain-invariants-and-actor-access.md" -Block @"
## Agreement Exchange Domain / Access Addendum

Agreement exchange slice drafts use this domain/access addendum as domain-design input:

```text
planning/slices/l2/L2-agreement-exchange-domain-invariants-and-actor-access.md
```

It clarifies:

```text
- AgreementProposalExchange stores ClientAccountId;
- client actions are guarded by ClientAccountId;
- Employee ownership is not guarded by ResponsibleEmployeeId first pass;
- any active Employee can service the exchange;
- proposal authors are tracked per proposal version;
- application service branches by actor;
- domain owns transition and participant/lifecycle invariants;
- read services use actor-specific access filters.
```
"@

Add-BlockIfMissing -Path "planning/slices/slice-questions-register.md" -Marker "SL-AGR-EXCH-ACCESS-Q-001" -Block @"
## Agreement Exchange Actor / Access Questions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `SL-AGR-EXCH-ACCESS-Q-001` | `L2-agreement-exchange-domain-invariants-and-actor-access.md`, `SL-AGR-EXCH-*` | exchange participant | accepted | Should exchange store client-side participant id? | Yes. `AgreementProposalExchange.ClientAccountId`. | Enables domain client ownership checks. |
| `SL-AGR-EXCH-ACCESS-Q-002` | same | client actions | accepted | How do client actions prove ownership? | Domain checks `client.Id == exchange.ClientAccountId`. | Prevents wrong-client exchange actions. |
| `SL-AGR-EXCH-ACCESS-Q-003` | same | employee actions | accepted | Should exchange store `ResponsibleEmployeeId` as access guard? | No first pass. Any active Employee can service exchange. | Assignment model remains future. |
| `SL-AGR-EXCH-ACCESS-Q-004` | same | application layer | accepted | Where does actor branching live? | Agreement exchange application service branches by actor and calls actor-specific domain method. | Keeps controllers/handlers thin. |
| `SL-AGR-EXCH-ACCESS-Q-005` | same | reads | accepted | Are read filters actor-specific? | Yes. Client reads filter by `ClientAccountId`; Employee reads follow temporary active-Employee policy first pass. | Prevents data exposure. |
"@

Add-BlockIfMissing -Path "planning/slices/slice-extension-points-register.md" -Marker "CP-AGR-EXCH-ACCESS-001" -Block @"
## Agreement Exchange Extension Points

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-AGR-EXCH-ACCESS-001` | client participant ownership | `AgreementProposalExchange.ClientAccountId` is the client-side participant guard. | accepted |
| `CP-AGR-EXCH-ACCESS-002` | employee servicing | No `ResponsibleEmployeeId` guard in first pass; any active Employee can service. | accepted temporary |
| `CP-AGR-EXCH-ACCESS-003` | future employee assignment | Department/region/assignment-based employee servicing can be added later. | future |
| `CP-AGR-EXCH-ACCESS-004` | read access | Shared read shape may be used with actor-specific access filters. | accepted |
| `CP-AGR-EXCH-ACCESS-005` | error mapping | Wrong-client / duplicate-exchange errors should use existing Error/ProblemDetails mapping; richer 409/403 cleanup can be separate. | future cleanup |
"@

Add-BlockIfMissing -Path "planning/slices/slice-implementation-notes-register.md" -Marker "IMPL-AGR-EXCH-ACCESS-001" -Block @"
## Agreement Exchange Implementation Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-AGR-EXCH-ACCESS-001` | `SL-AGR-EXCH-*` | `AgreementProposalExchange` stores `ClientAccountId` from the approved request owner. | accepted |
| `IMPL-AGR-EXCH-ACCESS-002` | client exchange commands | Domain checks `client.Id == exchange.ClientAccountId`. | accepted |
| `IMPL-AGR-EXCH-ACCESS-003` | employee exchange commands | Do not add `ResponsibleEmployeeId` as first-pass guard. Any active Employee can service exchange. | accepted |
| `IMPL-AGR-EXCH-ACCESS-004` | app service | Application service loads actor/exchange/request, creates value objects, branches by actor and calls domain methods. | accepted |
| `IMPL-AGR-EXCH-ACCESS-005` | reads | Client reads filter by `ClientAccountId`; Employee reads use first-pass active Employee visibility. | accepted |
| `IMPL-AGR-EXCH-ACCESS-006` | persistence | Add `ClientAccountId` column/index and prefer unique `RequestId`. | accepted |
| `IMPL-DRAFT-RULE-001` | full server/client drafts | Include a concrete near-end `Implementation Checklist`. | accepted |
| `IMPL-DRAFT-RULE-002` | current state answers | Inspect current GitHub/repo state, not handoff archives, when answering implementation status questions. | accepted |
"@

Add-BlockIfMissing -Path "planning/planning-agent-protocol.md" -Marker "drafting-current-state-source-rule.md" -Block @"
## Current-State Source Rule

When asked about current implementation status, existing code, generated API shape, tests, or missing slices, inspect current GitHub/repo state first.

Use archives/pasted handoffs as proposed changes or context, not as proof of current implementation.

See:

```text
planning/slices/drafting-current-state-source-rule.md
```
"@

Add-BlockIfMissing -Path "planning/README.md" -Marker "drafting-current-state-source-rule.md" -Block @"
## Current-State / Slice Drafting Rules

For current implementation status questions, use current GitHub/repo state first:

```text
planning/slices/drafting-current-state-source-rule.md
```

For implementation checklist requirements in full server/client drafts:

```text
planning/slices/drafting-implementation-checklist-rule.md
```

For canonical slice draft file names and placement:

```text
planning/slices/slice-draft-file-naming-and-placement.md
```
"@

Add-BlockIfMissing -Path "planning/slices/l1-slice-drafting-guide.md" -Marker "drafting-implementation-checklist-rule.md" -Block @"
## Implementation Checklist Rule

Full server and client drafts must include a concrete near-end `Implementation Checklist`.

Read:

```text
planning/slices/drafting-implementation-checklist-rule.md
planning/slices/slice-draft-file-naming-and-placement.md
planning/slices/drafting-current-state-source-rule.md
```
"@

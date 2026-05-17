$ErrorActionPreference = "Stop"

# Copy new files into repo-relative locations.
New-Item -ItemType Directory -Force -Path "planning/slices/l2" | Out-Null
New-Item -ItemType Directory -Force -Path "planning/slices/implementation-prompts" | Out-Null

Copy-Item -Force "planning/slices/l2/L2-AGR-EXCH-START-001-start-agreement-exchange-with-initial-employee-proposal.client.md" "planning/slices/l2/L2-AGR-EXCH-START-001-start-agreement-exchange-with-initial-employee-proposal.client.md"
Copy-Item -Force "planning/slices/l2/L2-agreement-exchange-start-sidecar-sync.md" "planning/slices/l2/L2-agreement-exchange-start-sidecar-sync.md"
Copy-Item -Force "planning/slices/implementation-prompts/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.prompt.md" "planning/slices/implementation-prompts/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.prompt.md"

function Add-Once {
    param(
        [string]$Path,
        [string]$Marker,
        [string]$Text
    )
    if (!(Test-Path $Path)) {
        throw "Missing file: $Path"
    }

    $content = Get-Content -Raw -Path $Path
    if ($content -notlike "*$Marker*") {
        Add-Content -Path $Path -Value "`n$Text`n"
    }
}

$block = @'
<!-- AGR-EXCH-START-SIDECAR-SYNC -->
## Agreement exchange start client sidecar sync

Added client sidecar:

```text
planning/slices/l2/L2-AGR-EXCH-START-001-start-agreement-exchange-with-initial-employee-proposal.client.md
```

Parent server slice:

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
```

Placement:

```text
Employee request details action area only.
Exchange does not exist yet, so this does not belong in Agreement Exchange details.
```

Contract guardrail:

```text
Current parent server slice uses request-scoped route + 204.
Client sidecar records preferred exchange-root route + exchangeId response as blocked until OpenAPI/server contract confirms it.
```

Implementation prompt helper:

```text
planning/slices/implementation-prompts/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.prompt.md
```
<!-- /AGR-EXCH-START-SIDECAR-SYNC -->
'@

Add-Once -Path "planning/slices/README.md" -Marker "AGR-EXCH-START-SIDECAR-SYNC" -Text $block
Add-Once -Path "planning/slices/l2/README.md" -Marker "AGR-EXCH-START-SIDECAR-SYNC" -Text $block

$flowBlock = @'
<!-- AGR-EXCH-START-SIDECAR-SYNC -->
## SL-AGR-EXCH-001 / L2-AGR-EXCH-START-001 — Start Agreement Exchange

Scenario/source mapping:

```text
SC-13D Employee Agreement Proposal Create / Send Version
SC-07B Employee Request Review boundary: ApproveReview enables but does not create exchange
SL-AGR-EXCH-001 parent server command
L2-EMP-DETAILS-001 host read sidecar
```

Client sidecar behavior coverage:

```text
Employee starts agreement exchange from Employee request details.
Client sends no employeeId and no proposal version.
Client refetches Employee request details and agreement exchange list after success.
Client navigates to Employee agreement exchange details only if generated response contains exchangeId.
```

Contract question:

```text
Server slice currently documents request-scoped route + 204.
Client sidecar prefers exchange-root route + exchangeId response.
Generated OpenAPI/server slice must resolve before implementation.
```
<!-- /AGR-EXCH-START-SIDECAR-SYNC -->
'@

Add-Once -Path "planning/slices/slice-scenario-flow-behavior-register.md" -Marker "AGR-EXCH-START-SIDECAR-SYNC" -Text $flowBlock

$questionsBlock = @'
<!-- AGR-EXCH-START-SIDECAR-SYNC -->
## Agreement exchange start sidecar questions

| ID | Slice | Status | Question | Current direction |
|---|---|---|---|---|
| `Q-L2-AGR-START-CLIENT-001` | `L2-AGR-EXCH-START-001.client` | blocked | Exact endpoint route? | Prefer `POST /api/agreement-exchanges`, but current parent server slice uses `POST /api/employee/requests/{requestId}/agreement-exchange/start`; generated OpenAPI decides. |
| `Q-L2-AGR-START-CLIENT-002` | `L2-AGR-EXCH-START-001.client` | blocked | Exact initial proposal DTO fields? | Use generated DTO aliases. |
| `Q-L2-AGR-START-CLIENT-003` | `L2-AGR-EXCH-START-001.client` | blocked | Does response include `exchangeId`? | Prefer yes for navigation; fallback to 204 refetch if server contract stays command-only. |
| `Q-L2-AGR-START-CLIENT-004` | `L2-AGR-EXCH-START-001.client` | accepted | Placement? | Employee request details action area only. |
| `Q-L2-AGR-START-CLIENT-005` | `L2-AGR-EXCH-START-001.client` | accepted | Does ApproveReview create exchange? | No. Separate explicit Employee command. |
<!-- /AGR-EXCH-START-SIDECAR-SYNC -->
'@

Add-Once -Path "planning/slices/slice-questions-register.md" -Marker "AGR-EXCH-START-SIDECAR-SYNC" -Text $questionsBlock

$notesBlock = @'
<!-- AGR-EXCH-START-SIDECAR-SYNC -->
## L2-AGR-EXCH-START-001 client implementation notes

```text
Feature placement:
  features/agreement-exchange/start-exchange/*

Host page:
  pages/employee/requests/details

Do not place:
  pages/agreements/details
  pages/employee/agreements/details
  shared/api/agreementExchangeApi.ts
```

Implementation guardrail:

```text
If server returns exchangeId, navigate to /employee/agreements/:exchangeId.
If server returns 204, refetch and remain on Employee request details unless exchangeId can be discovered from refreshed data.
```

This sidecar is blocked until `SL-AGR-EXCH-001` generated OpenAPI exists.
<!-- /AGR-EXCH-START-SIDECAR-SYNC -->
'@

Add-Once -Path "planning/slices/slice-implementation-notes-register.md" -Marker "AGR-EXCH-START-SIDECAR-SYNC" -Text $notesBlock

Write-Host "Applied L2 agreement exchange start client sidecar sync."

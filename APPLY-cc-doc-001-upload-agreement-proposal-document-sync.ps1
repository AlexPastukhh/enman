$ErrorActionPreference = "Stop"

function Ensure-DirForFile {
    param([string]$Path)
    $dir = Split-Path -Parent $Path
    if ($dir -and -not (Test-Path $dir)) {
        New-Item -ItemType Directory -Path $dir -Force | Out-Null
    }
}

function Copy-RepoFile {
    param([string]$RelativePath)
    Ensure-DirForFile $RelativePath
    Copy-Item -Path (Join-Path $PSScriptRoot $RelativePath) -Destination $RelativePath -Force
}

function Add-Or-ReplaceBlock {
    param(
        [string]$Path,
        [string]$StartMarker,
        [string]$EndMarker,
        [string]$Block
    )

    if (-not (Test-Path $Path)) {
        Ensure-DirForFile $Path
        Set-Content -Path $Path -Value ($Block.TrimEnd() + "`n") -Encoding UTF8
        return
    }

    $content = Get-Content -Path $Path -Raw -Encoding UTF8
    $pattern = [regex]::Escape($StartMarker) + "(?s).*?" + [regex]::Escape($EndMarker)
    $replacement = $Block.TrimEnd()

    if ([regex]::IsMatch($content, $pattern)) {
        $content = [regex]::Replace($content, $pattern, $replacement)
    }
    else {
        $content = $content.TrimEnd() + "`n`n" + $replacement + "`n"
    }

    Set-Content -Path $Path -Value $content -Encoding UTF8
}

Copy-RepoFile "planning/slices/cross-cutting/CC-DOC-001-upload-agreement-proposal-document.md"
Copy-RepoFile "planning/documentation/cc-doc-001-upload-agreement-proposal-document-sync-note.md"

$start = "<!-- CC-DOC-001-UPLOAD-AGREEMENT-PROPOSAL-DOCUMENT-SYNC -->"
$end = "<!-- /CC-DOC-001-UPLOAD-AGREEMENT-PROPOSAL-DOCUMENT-SYNC -->"

$planningReadmeBlock = @"
$start
## CC-DOC-001 — Agreement Proposal Document Upload

Cross-cutting/helper slice added for L2 agreement document upload support:

```text
planning/slices/cross-cutting/CC-DOC-001-upload-agreement-proposal-document.md
```

Purpose:

```text
multipart upload -> local private storage -> AgreementDocumentRef-compatible metadata
```

This is not an agreement lifecycle command. It does not create or mutate `AgreementProposalExchange`, `AgreementProposal` or `Request`.

Use it as support for:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
```
$end
"@

Add-Or-ReplaceBlock "planning/README.md" $start $end $planningReadmeBlock

$slicesReadmeBlock = @"
$start
## Agreement Document Upload Helper

Cross-cutting/helper slice:

```text
planning/slices/cross-cutting/CC-DOC-001-upload-agreement-proposal-document.md
```

Direction:

```text
POST /api/agreement-proposal-documents
multipart/form-data: document
auth: Client or Employee
CSRF: required
success: 200 OK AgreementDocumentRefDto
```

Boundary:

```text
Upload document bytes first.
Use returned AgreementDocumentRef-compatible metadata in:
  SL-AGR-EXCH-001
  SL-AGR-EXCH-002
```

`SL-DOC-*` remains available for future business document slices such as download/open, verification, cloud storage replacement or document generation.
$end
"@

Add-Or-ReplaceBlock "planning/slices/README.md" $start $end $slicesReadmeBlock

$l2ReadmeBlock = @"
$start
## Agreement Proposal Document Upload Helper

Cross-cutting/helper slice:

```text
CC-DOC-001 — Upload Agreement Proposal Document
planning/slices/cross-cutting/CC-DOC-001-upload-agreement-proposal-document.md
```

This slice supports agreement exchange commands by producing `AgreementDocumentRef` metadata.

It is consumed by:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
L2-AGR-EXCH-START-001.client
L2-AGR-EXCH-SEND-PROPOSAL-001.client
```

It does not create exchange/proposal state and does not replace `SC-14 — Agreement Documents`.
$end
"@

Add-Or-ReplaceBlock "planning/slices/l2/README.md" $start $end $l2ReadmeBlock

$crossCuttingReadmeBlock = @"
$start
## CC-DOC-001 — Agreement Proposal Document Upload

| File | Purpose | Status |
|---|---|---|
| `CC-DOC-001-upload-agreement-proposal-document.md` | Uploads agreement proposal files to local private storage and returns `AgreementDocumentRef`-compatible metadata for agreement exchange commands. | draft / implementation-ready planning |

Use this helper slice when a business slice needs proposal document bytes uploaded before submitting JSON lifecycle commands.

Do not put document bytes directly into agreement exchange command endpoints first pass.
$end
"@

Add-Or-ReplaceBlock "planning/slices/cross-cutting/README.md" $start $end $crossCuttingReadmeBlock

$checklistBlock = @"
$start
## Agreement Proposal Document Upload Consumer Rule

Agreement exchange command/client slices that need a proposal document must reference:

```text
planning/slices/cross-cutting/CC-DOC-001-upload-agreement-proposal-document.md
```

Use this flow:

```text
1. Upload document bytes through CC-DOC-001.
2. Receive AgreementDocumentRef-compatible metadata.
3. Submit start/counter-proposal command with metadata in JSON body.
```

Do not send binary bytes directly to agreement lifecycle command endpoints first pass.

Do not make each agreement command reimplement upload/storage mechanics.
$end
"@

Add-Or-ReplaceBlock "planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md" $start $end $checklistBlock

$sourceRegisterBlock = @"
$start
## Agreement Proposal Document Upload Helper Source Map

| Slice / sidecar | Marker | Source files | Applies to | Status |
|---|---|---|---|---|
| `CC-DOC-001-upload-agreement-proposal-document.md` | `[CONCERN]` / `[DATA]` / `[BEHAVIOR]` | `SC-14-agreement-documents.md`, `cross-cutting-concerns-drafting-checklist.md` | Multipart upload of agreement proposal document bytes, local private storage and `AgreementDocumentRef` metadata response | drafted |
| `L2-AGR-EXCH-START-001.client` | `[CONCERN]` | `CC-DOC-001` | Client uploads initial proposal document before Start Agreement Exchange command if server contract requires uploaded file ref | planned consumer |
| `L2-AGR-EXCH-SEND-PROPOSAL-001.client` | `[CONCERN]` | `CC-DOC-001` | Client uploads counter-proposal document before Send Proposal Version command if server contract requires uploaded file ref | planned consumer |
$end
"@

Add-Or-ReplaceBlock "planning/slices/slice-scenario-flow-behavior-register.md" $start $end $sourceRegisterBlock

$questionsBlock = @"
$start
## CC-DOC-001 Agreement Proposal Document Upload Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `CC-DOC-001-Q001` | `CC-DOC-001-upload-agreement-proposal-document.md` | slice placement | accepted | Is upload a business lifecycle slice? | No. It is a cross-cutting/helper slice. | Place under `planning/slices/cross-cutting/`. |
| `CC-DOC-001-Q002` | same | domain model | accepted | Add separate `Document` aggregate/table first pass? | No. Keep `AgreementDocumentRef` metadata and local file storage. | Avoids new document aggregate. |
| `CC-DOC-001-Q003` | same | lifecycle boundary | accepted | Does upload create exchange/proposal? | No. Upload only stores file and returns metadata. | Agreement lifecycle remains in `SL-AGR-EXCH-*`. |
| `CC-DOC-001-Q004` | same | auth | accepted | Who can upload? | Authenticated Client or Employee. | Both sides can later send proposal versions. |
| `CC-DOC-001-Q005` | same | storage | accepted | Where are bytes stored first pass? | Local private storage under `App_Data/Documents/agreement-proposals`. | No SQL bytes, no `wwwroot`. |
| `CC-DOC-001-Q006` | same | orphan files | accepted | Can orphan uploads exist first pass? | Yes. Cleanup/dedup is future. | Keeps first implementation simple. |
$end
"@

Add-Or-ReplaceBlock "planning/slices/slice-questions-register.md" $start $end $questionsBlock

$extensionBlock = @"
$start
## CC-DOC-001 Agreement Document Upload Extension Points

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-CC-DOC-001` | document upload | `CC-DOC-001` owns first-pass local upload and `AgreementDocumentRef` metadata response. | planned helper slice |
| `CP-CC-DOC-002` | download/open | Download/open document endpoint is separate future slice. | future |
| `CP-CC-DOC-003` | storage hardening | Virus scanning, magic-byte checks, cloud/object storage and retention cleanup are future hardening. | future |
| `CP-CC-DOC-004` | orphan cleanup | Orphan uploaded files are allowed first pass; cleanup job/policy can be added later. | future |
| `CP-CC-DOC-005` | document aggregate | Separate `Document` aggregate/table is not needed first pass. Revisit only if document lifecycle becomes business-owned. | future if needed |
$end
"@

Add-Or-ReplaceBlock "planning/slices/slice-extension-points-register.md" $start $end $extensionBlock

$implBlock = @"
$start
## CC-DOC-001 Agreement Proposal Document Upload Implementation Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-CC-DOC-001` | upload endpoint | Use `POST /api/agreement-proposal-documents` with multipart/form-data field `document`. | planned |
| `IMPL-CC-DOC-002` | auth/security | Require authenticated Client or Employee and CSRF for multipart POST. | planned |
| `IMPL-CC-DOC-003` | storage | Store under private `App_Data/Documents/agreement-proposals`; do not use `wwwroot`; do not trust original filename as path. | planned |
| `IMPL-CC-DOC-004` | response | Return `AgreementDocumentRefDto` / metadata compatible with `AgreementDocumentRef.Create(...)`. | planned |
| `IMPL-CC-DOC-005` | domain boundary | Do not create `AgreementProposalExchange`, `AgreementProposal`, Request changes or Document aggregate/table. | accepted |
| `IMPL-CC-DOC-006` | tests | Add endpoint integration tests and `LocalDocumentStorage` unit tests. | planned |
$end
"@

Add-Or-ReplaceBlock "planning/slices/slice-implementation-notes-register.md" $start $end $implBlock

$l2StatusBlock = @"
$start
## CC-DOC-001 Agreement Proposal Document Upload Helper

L2 document upload support is now planned as a cross-cutting/helper slice:

```text
CC-DOC-001 — Upload Agreement Proposal Document
planning/slices/cross-cutting/CC-DOC-001-upload-agreement-proposal-document.md
```

It turns uploaded proposal document bytes into `AgreementDocumentRef`-compatible metadata for agreement exchange commands.

It does not create a Document aggregate/table and does not mutate agreement lifecycle state.
$end
"@

Add-Or-ReplaceBlock "planning/l2-current-planning-status.md" $start $end $l2StatusBlock

$sc14Block = @"
$start
## Cross-Cutting Upload Planning Marker

| Scenario element | Marker | Meaning |
|---|---|---|
| `AgreementDocumentRef` metadata value object | `[DESIGNED]` | Agreement proposal versions reference metadata, not bytes. |
| Agreement proposal document upload | `[PLANNED]` | First-pass upload/storage support is owned by `CC-DOC-001`. |
| Local private file storage | `[PLANNED]` | `CC-DOC-001` stores files under `App_Data/Documents/agreement-proposals`. |
| Download/open document endpoint | `[DEFERRED]` | Separate future document slice. |
| Virus scanning/cloud storage/document lifecycle | `[DEFERRED]` | Security/infrastructure hardening beyond first pass. |
| Separate `Document` aggregate/table | `[DEFERRED]` | Not needed first pass. |

`CC-DOC-001` does not change agreement lifecycle state. It only returns metadata that exchange commands can use.
$end
"@

Add-Or-ReplaceBlock "planning/diagrams/scenario-text-specs/SC-14-agreement-documents.md" $start $end $sc14Block

Write-Host "Applied CC-DOC-001 docs sync."

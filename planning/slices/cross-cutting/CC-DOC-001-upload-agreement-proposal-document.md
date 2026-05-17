# CC-DOC-001 — Upload Agreement Proposal Document

Status: cross-cutting/helper slice draft / implementation-ready planning  
Package: `[L2] Agreement Proposal Exchange / Documents`  
Slice type: cross-cutting server upload endpoint + local file storage infrastructure  
Source draft: user-provided `SL-DOC-001 — Upload Agreement Proposal Document`  
Business consumers:
- `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`
- `SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version`
- `L2-AGR-EXCH-START-001.client`
- `L2-AGR-EXCH-SEND-PROPOSAL-001.client`

## 0. Cross-Cutting Placement Decision

This work is tracked as a cross-cutting/helper slice because it is reusable support behavior for multiple agreement exchange commands.

Canonical file:

```text
planning/slices/cross-cutting/CC-DOC-001-upload-agreement-proposal-document.md
```

Relationship to `SL-DOC-*`:

```text
CC-DOC-001 owns upload/storage infrastructure and document-ref-producing API.

SL-DOC-* remains available for future business document scenarios, such as:
- document download/open;
- document verification lifecycle;
- cloud/object storage replacement;
- document generation;
- broader agreement document management.
```

Do not place this as a normal agreement lifecycle command slice. It does not create or mutate `AgreementProposalExchange`.

## 0A. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `CC-DOC-001-Q001` | accepted | Is this a business lifecycle slice or support slice? | Cross-cutting/helper slice. | It lives under `planning/slices/cross-cutting/`. |
| `CC-DOC-001-Q002` | accepted | Does upload create exchange/proposal? | No. Upload stores file and returns `AgreementDocumentRef`-compatible metadata. | Keeps lifecycle commands separate. |
| `CC-DOC-001-Q003` | accepted | Is there a separate `Document` aggregate first pass? | No. | Keeps domain simple. |
| `CC-DOC-001-Q004` | accepted | Who can upload? | Authenticated Client or Employee. | Both sides can later send proposal versions. |
| `CC-DOC-001-Q005` | accepted | Is CSRF required? | Yes. Multipart upload is unsafe browser request. | Reuses `CC-CSRF-001`. |
| `CC-DOC-001-Q006` | accepted | Where is file stored first pass? | Local private storage under `App_Data/Documents/agreement-proposals`. | Not under `wwwroot`; not SQL bytes. |
| `CC-DOC-001-Q007` | accepted | Should exchange commands accept binary? | No first pass. They accept returned document metadata. | Client does upload first, then command. |
| `CC-DOC-001-Q008` | accepted | Should upload response be a read model? | No. It is document-ref metadata for command input. | No download/details resource yet. |

## 0B. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item | How this slice covers it | Status |
|---|---|---|
| User can upload proposal document before exchange command | Multipart upload endpoint returns metadata | target |
| Upload does not mutate agreement lifecycle | Endpoint only stores file and returns ref | target |
| Returned metadata can be used by exchange commands | Validate through `AgreementDocumentRef.Create(...)` before returning | target |
| Anonymous upload blocked | Auth required | target |
| Client and Employee upload supported | `[Authorize(Roles = "Client,Employee")]` | target |
| Browser unsafe upload protected | `CC-CSRF-001` / antiforgery required | target |
| File not exposed from webroot | Store under `App_Data/Documents` | target |
| Original filename not trusted as path | Generate storage key and sanitize original name | target |
| Document aggregate/table not introduced | File storage only; metadata embedded in proposal later | target |

## 0C. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Decision / owner |
|---|---|---|
| Auth/session/actor context | yes | Client or Employee authenticated session. Upload does not accept actor ids from client. |
| Authorization/ownership | limited | Upload produces metadata only; exchange commands later enforce request/exchange ownership. |
| Antiforgery / unsafe request | yes | Multipart POST must use shared antiforgery behavior from `CC-CSRF-001`; do not implement local CSRF mechanics. |
| Request validation / ProblemDetails | yes | File presence, size, content-type and metadata shape are upload validation; lifecycle validation is not here. |
| OpenAPI / generated artifacts | yes | Upload endpoint changes API contract; regenerate OpenAPI/types when implemented. |
| Transaction / atomicity | limited | Store file before lifecycle command; orphan files are accepted first pass and cleanup is future. |
| No-mutation / existing data safety | yes | Does not create exchange/proposal/request changes. |
| Idempotency / retry / double-submit | limited | Repeated upload may create multiple files; cleanup/dedup is future. |
| Concurrency / stale state | no | No domain state transition. |
| File/document boundary | yes | Main ownership of this slice. Bytes stay infrastructure; domain receives metadata reference. |
| Clock/audit actor fields | optional | Basic storage metadata only first pass; audit hardening future. |
| Privacy / data exposure | yes | Private storage root; no public URL; metadata only returned to authenticated user. |
| Client feedback / accessibility | future client sidecar | Client upload UI later owns pending/progress/errors. |
| Testing responsibility split | yes | Integration tests for endpoint; unit tests for `LocalDocumentStorage`. |

---

The original full draft content follows, normalized under the cross-cutting/helper slice id.


Status: preliminary draft
Package: `[L2] Agreement Proposal Exchange / Documents]`
Slice type: server upload endpoint + local file storage infrastructure
Depends on: agreement exchange command DTOs that accept `AgreementDocumentRef` metadata
Current direction: no separate `Document` aggregate first pass.

## 1. Slice Overview

Target behavior:

```text
User selects agreement proposal file in UI.

Client uploads file to server.

Server stores file in local private storage.

Server returns AgreementDocumentRef metadata.

Client passes returned document ref into agreement exchange command:
- start exchange
- send counter-proposal
```

This slice does **not** perform agreement exchange lifecycle transition.

It only turns uploaded file into reusable document reference metadata.

## 2. Why this slice exists

Current agreement exchange commands use document metadata:

```json
{
  "document": {
    "storageKey": "...",
    "originalFileName": "...",
    "contentType": "application/pdf",
    "sizeBytes": 4096
  },
  "comment": "optional"
}
```

So the file must be stored **before** the command is submitted.

Flow:

```text
1. Upload file
2. Receive AgreementDocumentRefDto
3. Submit exchange command with that document ref
```

This keeps file storage separate from domain lifecycle commands.

## 3. Scope

```text
- add document storage abstraction;
- add local filesystem implementation;
- add upload endpoint for agreement proposal documents;
- accept multipart/form-data with IFormFile;
- validate file presence, size, content type;
- save file under App_Data/Documents/agreement-proposals;
- generate safe storage key;
- return storage metadata;
- do not create AgreementProposal;
- do not create AgreementProposalExchange;
- do not modify request/exchange status;
- do not add Document aggregate/table.
```

## 4. Out of Scope

| Out of scope                       | Destination                                    |
| ---------------------------------- | ---------------------------------------------- |
| Start agreement exchange           | `SL-AGR-EXCH-001`                              |
| Send counter-proposal              | `SL-AGR-EXCH-002`                              |
| Download/open document             | future document download slice                 |
| Virus scanning                     | future security hardening                      |
| Cloud/object storage               | future infrastructure replacement              |
| Separate `Document` aggregate      | not needed first pass                          |
| Document verification lifecycle    | future scenario, not this slice                |
| Applicant passport/SNILS documents | separate applicant documents/verification flow |

## 5. API Contract

Endpoint:

```http
POST /api/agreement-proposal-documents
Content-Type: multipart/form-data
```

Auth:

```text
Client or Employee authenticated session
```

Question: should both Client and Employee upload?

Current direction:

```text
Yes. Both sides can upload proposal versions.
```

Authorization:

```csharp
[Authorize(Roles = "Client,Employee")]
[RequireAntiforgeryToken]
```

Request form:

```text
document: IFormFile
```

Optional future fields:

```text
purpose: "AgreementProposal"
```

Response:

```json
{
  "storageKey": "agreement-proposals/7c9c4a6e0c4f4f5b8a1d0d9a7a2f0f20.pdf",
  "originalFileName": "agreement-proposal.pdf",
  "contentType": "application/pdf",
  "sizeBytes": 4096
}
```

DTO direction:

```csharp
public sealed record AgreementDocumentRefDto(
    string StorageKey,
    string OriginalFileName,
    string ContentType,
    long SizeBytes);
```

Success:

```text
200 OK
```

Alternative success:

```text
201 Created
```

Decision first pass:

```text
Use 200 OK because the file is an implementation detail and not a full REST resource with stable document endpoint yet.
```

## 6. Questions / Decisions

| ID                | Status   | Question                                            | Decision                                                                      |
| ----------------- | -------- | --------------------------------------------------- | ----------------------------------------------------------------------------- |
| `SL-DOC-001-Q001` | accepted | Add separate `Document` aggregate?                  | No first pass. Keep `AgreementDocumentRef` metadata.                          |
| `SL-DOC-001-Q002` | accepted | Store file in SQL Server?                           | No. Store file on local filesystem, metadata in proposal table.               |
| `SL-DOC-001-Q003` | accepted | Where store file locally?                           | `App_Data/Documents/agreement-proposals`.                                     |
| `SL-DOC-001-Q004` | accepted | Should upload endpoint perform exchange transition? | No. Upload only stores file and returns ref.                                  |
| `SL-DOC-001-Q005` | accepted | Who can upload?                                     | Authenticated Client or Employee.                                             |
| `SL-DOC-001-Q006` | accepted | CSRF required?                                      | Yes, multipart unsafe browser command.                                        |
| `SL-DOC-001-Q007` | accepted | File name from user trusted?                        | No. Use only sanitized original name for metadata; generate storage filename. |
| `SL-DOC-001-Q008` | accepted | Content type allowed first pass?                    | Prefer `application/pdf`; optionally allow DOC/DOCX if needed.                |
| `SL-DOC-001-Q009` | accepted | Max size?                                           | Add explicit max size. Exact value can be config; first pass e.g. 10 MB.      |
| `SL-DOC-001-Q010` | accepted | Download endpoint now?                              | No. Separate slice.                                                           |
| `SL-DOC-001-Q011` | accepted | Can orphan uploaded files exist?                    | Yes first pass; cleanup later.                                                |
| `SL-DOC-001-Q012` | accepted | Does domain change?                                 | No major domain change; `AgreementDocumentRef` already exists.                |

## 7. Storage Design

Application abstraction:

```csharp
public interface IDocumentStorage
{
    Task<StoredDocumentFile> SaveAsync(
        Stream content,
        string originalFileName,
        string contentType,
        long sizeBytes,
        CancellationToken cancellationToken);

    Task<Stream> OpenReadAsync(
        string storageKey,
        CancellationToken cancellationToken);
}

public sealed record StoredDocumentFile(
    string StorageKey,
    string OriginalFileName,
    string ContentType,
    long SizeBytes);
```

Suggested location:

```text
EnergyManagement.Server/L1/Application/Abstractions/IDocumentStorage.cs
```

Infrastructure implementation:

```text
EnergyManagement.Server/L1/Infrastructure/Documents/LocalDocumentStorage.cs
```

Storage root:

```text
{ContentRoot}/App_Data/Documents
```

Agreement proposal files:

```text
App_Data/Documents/agreement-proposals/{guid}.{extension}
```

Returned storage key:

```text
agreement-proposals/{guid}.{extension}
```

## 8. LocalDocumentStorage Requirements

`LocalDocumentStorage.SaveAsync(...)` must:

```text
- create root directory if missing;
- sanitize original filename with Path.GetFileName;
- generate storage filename with Guid;
- preserve safe extension if allowed;
- save under agreement-proposals subfolder;
- return storage key and metadata;
- not trust client path;
- not write outside storage root.
```

`OpenReadAsync(...)` must:

```text
- combine root path + storageKey;
- call Path.GetFullPath;
- verify resulting path stays under root path;
- reject path traversal;
- open read-only stream.
```

Even if download is out of scope, `OpenReadAsync` can be implemented now for completeness.

## 9. Validation Rules

Upload validation:

```text
document is required
document.Length > 0
document.Length <= MaxAllowedDocumentBytes
content type allowed
original filename present
extension allowed if extension filtering is used
```

Recommended first-pass allowed content types:

```text
application/pdf
```

Optional if needed:

```text
application/vnd.openxmlformats-officedocument.wordprocessingml.document
application/msword
```

Do not rely only on content type for security. For diploma first pass, content-type/extension validation is acceptable, but note that real production would need stronger scanning.

## 10. Controller Direction

Controller:

```csharp
[ApiController]
[Route("api/agreement-proposal-documents")]
public sealed class AgreementProposalDocumentsController : ProjectController
{
    [Authorize(Roles = "Client,Employee")]
    [RequireAntiforgeryToken]
    [HttpPost(Name = "UploadAgreementProposalDocument")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(AgreementDocumentRefDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Upload(
        [FromForm] UploadAgreementProposalDocumentForm form,
        CancellationToken cancellationToken)
}
```

Form:

```csharp
public sealed class UploadAgreementProposalDocumentForm
{
    public IFormFile? Document { get; init; }
}
```

Flow:

```text
1. Validate form/document.
2. Open stream.
3. Save through IDocumentStorage.
4. Convert StoredDocumentFile to AgreementDocumentRefDto.
5. Optionally validate AgreementDocumentRef.Create(...) before returning.
6. Return 200 OK.
```

## 11. Why validate `AgreementDocumentRef.Create` after storage

The upload endpoint should return metadata that is acceptable by existing exchange commands.

So after storage:

```csharp
var documentRef = AgreementDocumentRef.Create(
    stored.StorageKey,
    stored.OriginalFileName,
    stored.ContentType,
    stored.SizeBytes);

if (documentRef.IsFailure)
{
    return ProblemDetailsFromValidation(documentRef.Error);
}
```

Then response uses the same values.

This avoids returning a document ref that later command rejects.

## 12. Program.cs Registration

Register storage:

```csharp
builder.Services.AddTransient<IDocumentStorage, LocalDocumentStorage>();
```

If using options:

```csharp
builder.Services.Configure<LocalDocumentStorageOptions>(
    builder.Configuration.GetSection("DocumentStorage"));
```

Optional options:

```csharp
public sealed class LocalDocumentStorageOptions
{
    public string RootPath { get; init; } = "App_Data/Documents";
    public long MaxAgreementProposalDocumentBytes { get; init; } = 10 * 1024 * 1024;
    public string[] AllowedContentTypes { get; init; } = ["application/pdf"];
}
```

For first pass, constants are acceptable, but config is cleaner.

## 13. Client Flow

Client-side flow later:

```text
file input selected
  ↓
POST /api/agreement-proposal-documents multipart/form-data
  ↓
receive AgreementDocumentRefDto
  ↓
call:
  POST /api/employee/requests/{requestId}/agreement-exchange/start
or
  POST /api/requests/{requestId}/agreement-exchange/proposals
with document ref in JSON body
```

Do not send binary file directly to exchange command endpoints first pass.

## 14. Behavior Coverage

| Behavior                            | How covered                                |
| ----------------------------------- | ------------------------------------------ |
| Authenticated user uploads document | endpoint accepts Client/Employee           |
| Anonymous cannot upload             | `[Authorize]`                              |
| CSRF required                       | `[RequireAntiforgeryToken]`                |
| Empty file rejected                 | upload validation                          |
| Oversized file rejected             | max size validation                        |
| Unsupported content type rejected   | content type validation                    |
| File stored outside webroot         | App_Data/Documents                         |
| Original filename sanitized         | `Path.GetFileName`                         |
| Stored filename generated           | Guid                                       |
| Storage key safe                    | relative generated key                     |
| Returned ref compatible with domain | validate via `AgreementDocumentRef.Create` |
| No exchange transition happens      | endpoint only stores and returns metadata  |

## 15. Test Plan

Integration tests:

```text
unauthenticated upload -> 401
missing CSRF -> 400
empty file -> 422 or 400 according to existing mapper
unsupported content type -> 422
oversized file -> 422
valid pdf upload -> 200
valid upload returns storageKey/originalFileName/contentType/sizeBytes
valid upload writes file under App_Data/Documents/agreement-proposals
storage key does not contain original path
```

Unit tests for `LocalDocumentStorage`:

```text
SaveAsync creates file
SaveAsync sanitizes original filename
SaveAsync returns relative storage key
OpenReadAsync reads saved file
OpenReadAsync rejects path traversal
```

## 16. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text
POST /api/agreement-proposal-documents
multipart/form-data:
  document: binary
responses:
  200 AgreementDocumentRefDto
  400
  401
  403
  422
  500
```

Generation commands:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
npm.cmd run generate:api
npm.cmd run check:api
```

## 17. Guardrails

```text
Do not add Document aggregate.
Do not add Documents table.
Do not store binary file in SQL Server.
Do not put files under wwwroot.
Do not trust original filename as storage path.
Do not mix upload with exchange lifecycle commands.
Do not modify AgreementProposalExchange state here.
Do not create AgreementProposal here.
Do not implement download here.
Do not remove AgreementDocumentRef.
```

## 18. Acceptance Criteria

```text
IDocumentStorage exists.
LocalDocumentStorage saves files under App_Data/Documents/agreement-proposals.
Upload endpoint accepts multipart/form-data.
Upload endpoint requires Client or Employee auth.
Upload endpoint requires CSRF.
Upload endpoint validates file.
Upload endpoint returns AgreementDocumentRefDto-compatible metadata.
Exchange command endpoints can use returned document ref unchanged.
No Document aggregate/table added.
No agreement lifecycle state changed by upload endpoint.
```


## 19. Implementation Checklist

```text
[ ] add AgreementDocumentRefDto
[ ] add UploadAgreementProposalDocumentForm
[ ] add upload validator/request validation for document
[ ] add IDocumentStorage abstraction
[ ] add StoredDocumentFile model
[ ] add LocalDocumentStorage implementation
[ ] add LocalDocumentStorageOptions or first-pass constants
[ ] register IDocumentStorage in server DI
[ ] add AgreementProposalDocumentsController
[ ] require Client or Employee role
[ ] require CSRF token
[ ] accept multipart/form-data with document field
[ ] validate file exists and Length > 0
[ ] validate max allowed size
[ ] validate allowed content type / extension first pass
[ ] sanitize original filename
[ ] generate safe storage key
[ ] save under App_Data/Documents/agreement-proposals
[ ] validate returned metadata with AgreementDocumentRef.Create(...)
[ ] return 200 OK AgreementDocumentRefDto
[ ] do not create Document aggregate/table
[ ] do not create AgreementProposal
[ ] do not create AgreementProposalExchange
[ ] do not mutate Request or Exchange status
[ ] add LocalDocumentStorage unit tests
[ ] add upload endpoint integration tests for auth/CSRF/validation/success
[ ] regenerate OpenAPI/types
```

## 20. Guardrail Summary

```text
CC-DOC-001 is a support/helper slice.

It produces AgreementDocumentRef-compatible metadata.

It does not own agreement lifecycle.

It does not create AgreementProposalExchange.

It does not create AgreementProposal.

It does not add Document aggregate/table.

It stores bytes in local private infrastructure first pass.

It must not store files under wwwroot.

It must not trust original filename as storage path.

It must require auth and CSRF.

It must regenerate OpenAPI/types when implemented.
```

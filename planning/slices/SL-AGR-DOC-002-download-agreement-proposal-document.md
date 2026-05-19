# SL-AGR-DOC-002 — Download Agreement Proposal Document

Status: planned server draft / safe document download extension / implementation not started  
Package: `[L2] Agreement Exchange` + `[L1] Agreement Documents`  
Slice type: backend/API read/download slice  
Primary purpose: allow Client or Employee to download an agreement proposal document that belongs to an agreement exchange they are allowed to read.

Depends on:

- `SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details`
- `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`
- `SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version`
- `AgreementProposalDocumentsController` upload flow
- `IDocumentStorage.OpenReadAsync(...)`
- Client/Employee auth/session
- Agreement exchange read access rules

Implementation direction:

```text
Client or Employee opens agreement exchange details
        ↓
UI shows proposal history and document metadata
        ↓
User clicks Download for one proposal document
        ↓
Server resolves current account + role
        ↓
Server loads agreement exchange details using the same access boundary as details read
        ↓
Server verifies proposalId belongs to this exchange
        ↓
Server reads document ref from that proposal
        ↓
Server opens file from storage by internal storageKey
        ↓
Server returns File(stream, contentType, originalFileName)
```

Critical security decision:

```text
Do not expose a raw storageKey-only download endpoint.
Download must be exchange/proposal-contextual and must verify ownership/access before storage read.
```

---

## 0. Scenario Sources

Related scenario context:

```text
SC-13A — Agreement exchange list / navigation to details
SC-13B — Client agreement proposal details / response
SC-13C — Employee agreements
SC-13D — Employee agreement proposal create/send version
SC-13E — Agreement final refusal
```

Related implemented/read slices:

```text
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages
```

Related document-producing command slices:

```text
SL-AGR-EXCH-001 — initial Employee proposal document
SL-AGR-EXCH-002 — Client/Employee proposal version document
```

Source note:

```text
Agreement proposal documents are already part of proposal details as AgreementDocumentRef metadata.
This slice adds secure retrieval of the stored binary file for a proposal document visible in exchange details.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Current implementation evidence:

```text
AgreementProposalDocumentsController:
  POST /api/agreement-proposal-documents
  saves uploaded file through IDocumentStorage.SaveAsync(...)
  returns AgreementDocumentRefDto

IDocumentStorage:
  SaveAsync(...)
  OpenReadAsync(storageKey, ...)

AgreementExchangeDetailsResponseDto:
  exchangeId
  requestId
  activeProposal
  proposals[]

AgreementProposalDetailsDto:
  proposalId
  version
  sender
  document: AgreementDocumentRefDto
```

Coverage snapshot:

| Behavior / source item | Current state | This slice responsibility | Notes |
|---|---|---|---|
| Proposal documents can be uploaded | implemented upload endpoint | no change | upload remains separate |
| Details show document metadata | implemented read DTO/widget | no change to details DTO required for proposalId path | proposalId + document already available |
| User can download visible document | missing | add secure GET endpoint | Client/Employee only |
| StorageKey must not be authority | risk because metadata includes storageKey | route uses exchangeId + proposalId, not storageKey | server resolves document from exchange details |
| Access must match exchange details access | details read already has role/account access boundary | reuse same read access decision or equivalent query | avoid duplicate/looser policy |
| Server returns binary file | missing | use IDocumentStorage.OpenReadAsync and File(...) | contentType + originalFileName from AgreementDocumentRef |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
planned / not implemented
```

Current server state:

```text
Upload exists.
Storage read abstraction exists.
Download endpoint does not exist.
Agreement exchange details endpoint exists and already performs Client/Employee access checks.
```

Known limitation / drift:

```text
AgreementDocumentRefDto includes storageKey.
Client must not use storageKey as download authority.
Server must treat storageKey only as an internal storage locator after exchange/proposal access is verified.
```

Last sync note:

```text
Server draft only. Client sidecar is L2-AGR-DOC-DOWNLOAD-001.client.
```

---

## 1. Slice Overview

Target behavior:

```text
Client or Employee opens agreement exchange details.

The details response contains proposal history with proposalId and document metadata.

User clicks download for a proposal document.

Browser requests:
  GET /api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download

Server authenticates Client/Employee.

Server loads the agreement exchange through the same access rules as details read.

Server finds proposalId inside active proposal/history for that exchange.

Server opens the proposal document from storage.

Server returns binary file with original filename and content type.
```

This is a read/download slice. It must not mutate agreement exchange, proposal, request or document state.

---

## 2. Scope

This slice owns:

```text
- Client/Employee authenticated document download endpoint;
- exchange-contextual route;
- proposal-contextual route;
- current account and role resolution from session;
- access check equivalent to agreement exchange details read;
- verifying proposal belongs to requested exchange;
- resolving document ref from proposal details;
- opening stored file through IDocumentStorage.OpenReadAsync;
- returning File(stream, contentType, originalFileName);
- not trusting storageKey from client;
- API tests for Client, Employee, unauthorized/forbidden/not found and no-ownership cases.
```

Endpoint direction:

```http
GET /api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download
```

Auth:

```text
Client or Employee
```

CSRF:

```text
Not required for GET read/download.
```

Success:

```http
200 OK
Content-Type: proposal document content type
Content-Disposition: attachment; filename="originalFileName"
```

---

## 3. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Document upload | existing `AgreementProposalDocumentsController.Upload` |
| Changing start/send proposal commands | `SL-AGR-EXCH-001/002` |
| Changing document metadata shape unless needed | separate DTO/read-model slice |
| Raw storageKey download endpoint | explicitly forbidden |
| Document deletion/replacement | future document management slice |
| Virus scanning / real external document service | future integration slice |
| Pre-signed URLs | future storage-provider slice |
| Public/anonymous document download | explicitly out of scope |
| Client UI link/button | `L2-AGR-DOC-DOWNLOAD-001.client` |
| Updating proposal or exchange status | not a download concern |

Important boundary:

```text
This slice returns an existing stored file.
It does not create proposal versions.
It does not accept proposals.
It does not final-refuse exchanges.
It does not upload files.
```

---

## 4. Related Slices / Owners

```text
SL-AGR-EXCH-004
  Owns agreement exchange details read and access visibility.

L2-AGR-EXCH-DETAILS-001.client
  Owns details page/widget and proposal/document display.

AgreementProposalDocumentsController Upload
  Owns multipart upload and creating AgreementDocumentRef metadata.

SL-AGR-DOC-002
  Owns secure binary download for a proposal document.

L2-AGR-DOC-DOWNLOAD-001.client
  Owns rendering download link/button in details UI.
```

---

## 5. Scenario Flow

```text
User opens agreement exchange details
        ↓
System shows proposal history
        ↓
Proposal card shows document metadata and Download action
        ↓
User clicks Download
        ↓
GET /api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download
        ↓
Server authenticates Client/Employee
        ↓
Server verifies user can read exchange
        ↓
Server verifies proposal belongs to exchange
        ↓
Server opens file from storage
        ↓
Browser downloads original file
```

Scenario flow table:

| Step | Layer | Responsibility |
|---|---|---|
| S01 | Client/Employee | Opens exchange details. |
| S02 | Details read | Provides exchangeId, proposalId and document metadata. |
| S03 | UI | Renders Download action for a proposal document. |
| S04 | Server auth | Requires Client or Employee session. |
| S05 | Server access | Checks exchange visibility for current actor. |
| S06 | Server ownership | Ensures proposalId belongs to that exchange. |
| S07 | Storage | Opens file by internal storageKey. |
| S08 | API response | Returns file download response. |

---

## 6. API Contract

Endpoint:

```http
GET /api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download
```

Route params:

```text
exchangeId: long, min 1
proposalId: long, min 1
```

Request body:

```text
none
```

Query:

```text
none
```

Response success:

```text
200 OK binary file stream
```

Response headers direction:

```text
Content-Type: document.contentType
Content-Disposition: attachment; filename="document.originalFileName"
```

Failure responses:

```text
401 Unauthorized
  no authenticated L1 account session

403 Forbidden
  authenticated role is not Client/Employee
  or actor is not allowed to read this exchange

404 NotFound
  exchange does not exist or is not visible to current actor
  proposalId does not belong to this exchange
  proposal has no valid document ref
  storage file is missing, if storage maps missing file as not found

422 UnprocessableEntity
  invalid document metadata / storage key fails domain validation if surfaced as validation problem

500 InternalServerError
  unexpected storage/server failure
```

Do not accept:

```text
storageKey in route
storageKey in query
storageKey in body
accountId / role in body
senderId in body
```

---

## 7. Application / Handler Direction

Preferred implementation:

```text
Add a query/handler:
  DownloadAgreementProposalDocumentQuery(CurrentAccountId, CurrentRole, ExchangeId, ProposalId)

Handler:
  - reuses AgreementExchangeDetailsQuery or equivalent access-checked read repository call;
  - returns not found if details are null;
  - finds proposal by ProposalId in details.Proposals / active proposal;
  - returns not found if proposal is absent;
  - validates proposal.Document metadata enough for storage read;
  - opens storage stream with IDocumentStorage.OpenReadAsync(proposal.Document.StorageKey);
  - returns stream + originalFileName + contentType.
```

Result shape direction:

```csharp
public sealed record DownloadAgreementProposalDocumentResult(
    DownloadAgreementProposalDocumentStatus Status,
    Stream? Stream,
    string? ContentType,
    string? OriginalFileName,
    IReadOnlyList<Error> Errors);

public enum DownloadAgreementProposalDocumentStatus
{
    Success = 1,
    NotFound = 2,
    Forbidden = 3,
    Invalid = 4
}
```

Controller direction:

```csharp
[Authorize(Roles = "Client,Employee")]
[HttpGet("{exchangeId:long:min(1)}/proposals/{proposalId:long:min(1)}/document/download", Name = "DownloadAgreementProposalDocument")]
public async Task<IActionResult> DownloadProposalDocument(
    long exchangeId,
    long proposalId,
    CancellationToken cancellationToken)
```

Controller result mapping:

```text
Success -> File(stream, contentType, originalFileName)
NotFound -> NotFound()
Forbidden -> Forbid()
Invalid -> ProblemDetailsFromValidation(errors)
fallback -> ProblemDetailsFromInternalServerError(...)
```

---

## 8. Security / Protection

Security rules:

```text
- Client can download documents only from exchanges visible to that Client account.
- Employee can download documents only from exchanges visible under current Employee policy.
- Server must resolve account id and role from auth claims.
- Server must never trust applicant/client/employee ids from route/query/body.
- Server must never use a raw storageKey supplied by the client as download authority.
- Server must verify proposalId belongs to exchangeId before calling OpenReadAsync.
- StorageKey is internal storage locator only after access and ownership checks pass.
```

CSRF:

```text
GET download does not need CSRF.
Auth cookie/session still required.
```

IDOR guardrail:

```text
The following endpoint shape is forbidden:
  GET /api/agreement-proposal-documents/{storageKey}
  GET /api/agreement-proposal-documents?storageKey=...

Reason:
  storageKey is metadata and may be visible in DTOs.
  Download authorization must be based on exchange/proposal context, not raw storage key.
```

---

## 9. Questions / Decisions

| ID | Status | Question | Decision / current direction |
|---|---|---|---|
| `SL-AGR-DOC-002-Q001` | accepted | Route by storageKey? | No. Route by `exchangeId` + `proposalId`. |
| `SL-AGR-DOC-002-Q002` | accepted | Can both Client and Employee download? | Yes, if they can read the exchange. |
| `SL-AGR-DOC-002-Q003` | accepted | Does GET require CSRF? | No. Auth required. |
| `SL-AGR-DOC-002-Q004` | accepted | Does this mutate state? | No. Read/download only. |
| `SL-AGR-DOC-002-Q005` | accepted | Should endpoint return original filename? | Yes, from AgreementDocumentRef. |
| `SL-AGR-DOC-002-Q006` | accepted | Should storage missing file be 404? | Prefer 404, unless storage abstraction forces 500 first pass. |
| `SL-AGR-DOC-002-Q007` | future | Add document IDs instead of proposalId route? | Future read-model/domain extension if needed. |

---

## 10. Behavior Coverage

| Behavior | Covered by this slice |
|---|---|
| Client downloads document from own exchange | yes |
| Employee downloads document from visible exchange | yes |
| User cannot download by raw storageKey | yes, route forbids it |
| User cannot download proposal outside exchange | yes, proposal ownership check |
| User cannot download another Client's exchange document | yes, details access check |
| Download does not mutate exchange/request/proposal | yes |
| Upload remains separate | yes |
| UI link rendering | client sidecar |

---

## 11. Test / Verification Plan

Primary rule:

```text
Tests assert access/security and file response behavior, not storage internals.
```

API integration tests:

```text
- Client can download document from own visible exchange.
- Employee can download document from visible exchange.
- unauthenticated request returns 401.
- wrong/unsupported role returns 403.
- Client cannot download another Client's exchange document.
- proposalId from another exchange returns 404.
- missing exchange returns 404.
- missing proposal returns 404.
- response content type equals document content type.
- response content disposition contains original filename.
- response body equals stored test file content.
- download does not change exchange status or proposal state.
```

Storage test direction:

```text
Use existing test storage or fake storage to persist a known document.
Do not bypass the API by directly passing storageKey to route.
```

OpenAPI / generated artifacts:

```text
Expected operationId: DownloadAgreementProposalDocument
Expected path: /api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download
Expected success: binary file response, or documented file response if generator representation is limited.
Do not manually edit generated artifacts.
```

---

## 12. Implementation Checklist

```text
[ ] add download query/result/status or equivalent application service method
[ ] resolve current account and role from claims
[ ] require Client or Employee auth
[ ] add GET route under AgreementExchangesController
[ ] do not add CSRF to GET
[ ] do not accept storageKey from client
[ ] load details/access-check by exchangeId and current actor
[ ] find proposal by proposalId in details.Proposals/active proposal
[ ] validate document metadata
[ ] open file with IDocumentStorage.OpenReadAsync(document.StorageKey)
[ ] return File(stream, contentType, originalFileName)
[ ] add API integration tests
[ ] generate OpenAPI/types through repo commands after implementation
[ ] do not change upload/start/send proposal flows
[ ] do not change domain status/lifecycle
```

---

## 13. Guardrail Summary

```text
Download is exchange/proposal-contextual.
Never authorize by raw storageKey.
Client/Employee must be authenticated.
Access must match agreement exchange details visibility.
Proposal must belong to exchange.
Only then call IDocumentStorage.OpenReadAsync.
Return original filename and content type.
Do not mutate exchange/request/proposal/document state.
Do not implement upload changes here.
```

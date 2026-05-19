# L2-AGR-DOC-DOWNLOAD-001.client — Agreement Proposal Document Download Link

Status: planned client sidecar draft / blocked until `SL-AGR-DOC-002` server endpoint and generated contract exist  
Parent server slice: `SL-AGR-DOC-002 — Download Agreement Proposal Document`  
Host read sidecar: `L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages`  
Actor: Client, Employee  
Slice type: client read/download sidecar  
Placement: agreement exchange details proposal document area

Architecture direction:

```text
Agreement exchange details read DTO provides:
  exchangeId
  proposals[].proposalId
  proposals[].document metadata

Client renders Download link/button per proposal document:
  GET /api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download

Client does not send storageKey as authority.
Server validates exchange/proposal access and returns file.
```

---

## 0. Scenario Sources

Related scenario context:

```text
SC-13B — Client agreement proposal details / response
SC-13C — Employee agreements
SC-13D — Employee agreement proposal create/send version
```

Related slices:

```text
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages
SL-AGR-DOC-002 — Download Agreement Proposal Document
```

Source note:

```text
Agreement exchange details already show proposal document metadata.
This sidecar adds a user-visible download action for documents the user can already see in details.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Current client evidence:

```text
AgreementDocumentRefList currently renders document metadata only.
It does not render download link/button.
```

Current DTO evidence:

```text
AgreementExchangeDetailsResponseDto:
  exchangeId
  activeProposal
  proposals[]

AgreementProposalDetailsDto:
  proposalId
  version
  document: AgreementDocumentRefDto
```

Coverage snapshot:

| Behavior / source item | Current state | This sidecar responsibility | Notes |
|---|---|---|---|
| User sees document metadata | implemented | preserve | filename/type/size remain visible |
| User downloads visible document | missing | add link/button | details page only first pass |
| Client has exchange/proposal context | available in details DTO | pass `exchangeId` + `proposalId` to document display | no storageKey route |
| Server enforces access | server slice | call contextual endpoint only | no client-side authorization claims |
| StorageKey is not authority | metadata may contain storageKey | do not build URL from storageKey | IDOR guardrail |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
planned / blocked until server endpoint and generated contract exist
```

Expected server endpoint:

```http
GET /api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download
```

Known limitation:

```text
If server endpoint is not generated yet, do not implement a fake raw-storageKey download.
Do not infer or construct undocumented URLs.
```

Last sync note:

```text
Client draft only. Server download slice owns access/security and file response.
```

---

## 1. Key Decision

Download URL uses exchange/proposal context:

```text
exchangeId + proposalId
```

Client must not use:

```text
storageKey as route parameter
storageKey as query parameter
storageKey as request body
```

Reason:

```text
storageKey can be visible in metadata and is not an authorization boundary.
Server must verify exchange/proposal ownership before reading storage.
```

---

## 2. Scope

This client sidecar owns:

```text
- rendering Download link/button for proposal documents in agreement exchange details;
- preserving existing document metadata display;
- constructing contextual download URL from exchangeId + proposalId;
- opening browser download through ordinary anchor/link behavior;
- minimal pending/unavailable/accessibility copy if needed;
- no fetchJson file streaming first pass;
- no upload changes;
- no proposal command changes;
- no raw storageKey download.
```

Primary URL:

```http
GET /api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download
```

---

## 3. Out of Scope

```text
- backend endpoint implementation -> SL-AGR-DOC-002;
- document upload flow;
- changing start/send proposal forms;
- changing AgreementDocumentRef shape;
- adding document preview;
- inline PDF/image viewer;
- document deletion/replacement;
- public download links;
- pre-signed URL support;
- broad details page redesign;
- manual generated OpenAPI/type edits;
- styling beyond minimal link/button placement.
```

---

## 4. Related Slices / Owners

```text
SL-AGR-DOC-002
  Owns secure file download endpoint and access checks.

L2-AGR-DOC-DOWNLOAD-001.client
  Owns details UI download action.

L2-AGR-EXCH-DETAILS-001.client
  Owns shared details page/widget and proposal/document display host.

SL-AGR-EXCH-001 / 002
  Own proposal document producing command flows.
```

---

## 5. Visual UI / Scenario Flow

```text
User opens agreement exchange details
        ↓
Proposal history shows proposal cards
        ↓
Each proposal with document shows metadata:
  filename, type, size
        ↓
Document block also shows Download action
        ↓
User clicks Download
        ↓
Browser requests contextual download endpoint
        ↓
Server returns file or auth/error response
```

Scenario flow table:

| Step | Layer | Responsibility |
|---|---|---|
| S01 | Details page | Has exchangeId and proposals. |
| S02 | Proposal card | Has proposalId and document metadata. |
| S03 | Document display | Shows filename/type/size and Download action. |
| S04 | Browser | Navigates to/downloads contextual URL. |
| S05 | Server | Authorizes and streams file. |

---

## 6. Visual Client Implementation Flow

Preferred prop shape:

```tsx
type AgreementDocumentRefListProps = {
  exchangeId: number;
  proposalId: number;
  document?: AgreementDocumentRef | null;
};
```

Download href builder:

```ts
export const getAgreementProposalDocumentDownloadUrl = (
  exchangeId: number,
  proposalId: number,
) =>
  `/api/agreement-exchanges/${encodeURIComponent(String(exchangeId))}` +
  `/proposals/${encodeURIComponent(String(proposalId))}/document/download`;
```

Usage direction:

```text
AgreementExchangeDetailsView / proposal history widget:
  pass details.exchangeId and proposal.proposalId into AgreementDocumentRefList.

AgreementDocumentRefList:
  render metadata as today;
  render <a href={downloadUrl}>Скачать документ</a>;
  do not render link if exchangeId/proposalId invalid or document missing.
```

File organization direction:

```text
src/features/agreement-exchange/download-document/api/getAgreementProposalDocumentDownloadUrl.ts
  owns URL builder only.

src/widgets/agreement-exchange-details/AgreementDocumentRefList.tsx
  owns rendering metadata + link placement.
```

Alternative:

```text
If project prefers no feature folder for simple URL builder, keep helper near details widget.
But do not put business-specific download wrapper in shared/api.
```

---

## 7. Client API / Download Contract

No `fetchJson` first pass:

```text
Use browser download/navigation through an anchor tag.
This avoids manually handling Blob, Content-Disposition and file naming in JS.
```

Anchor direction:

```tsx
<a
  className="agreementExchangeDetails__documentDownload"
  href={getAgreementProposalDocumentDownloadUrl(exchangeId, proposalId)}
>
  Скачать документ
</a>
```

Optional attributes:

```text
download attribute may be omitted because server sets Content-Disposition.
target should stay default same-tab unless product wants new tab.
```

Do not send:

```text
storageKey
senderId
clientId
employeeId
role
```

---

## 8. UI State Rules

Document missing:

```text
render nothing, same as current behavior.
```

Document present but context missing/invalid:

```text
render metadata only;
do not render broken download link.
```

Auth failure from download:

```text
first pass may let browser show server response.
No custom fetch/error UI required.
```

Metadata display:

```text
Keep existing filename/contentType/size formatting.
Add Download action below or near metadata.
```

---

## 9. Security / Protection

Client guardrails:

```text
- Build URL from exchangeId and proposalId only.
- Do not build URL from storageKey.
- Do not expose a raw storageKey download route.
- Do not add client-side account/role parameters.
- Do not assume UI visibility is authorization.
```

Server remains authoritative:

```text
Client/Employee auth
exchange visibility
proposal belongs to exchange
document ref valid
storage file exists
```

---

## 10. Questions / Decisions

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-DOC-DOWNLOAD-001` | accepted | Use storageKey URL? | No. Use exchangeId + proposalId. |
| `Q-L2-AGR-DOC-DOWNLOAD-002` | accepted | Use fetch/Blob? | No first pass. Use anchor to server File response. |
| `Q-L2-AGR-DOC-DOWNLOAD-003` | accepted | Does link show for Client and Employee? | Yes, if details page shows proposal/document. |
| `Q-L2-AGR-DOC-DOWNLOAD-004` | accepted | Does this change upload/send/start flows? | No. |
| `Q-L2-AGR-DOC-DOWNLOAD-005` | blocked | Exact operation/generated name? | Confirm after server implementation/generation. |
| `Q-L2-AGR-DOC-DOWNLOAD-006` | future | Add preview? | Future slice, not download first pass. |

---

## 11. Behavior Coverage

| Behavior | How client sidecar covers it |
|---|---|
| Visible proposal document can be downloaded | renders download link in document block |
| URL is contextual | uses exchangeId + proposalId |
| storageKey is not used as authority | not in href |
| Client and Employee details pages share behavior | shared details widget renders link |
| Missing document produces no broken link | no document -> no render |
| Upload flow remains unchanged | no upload files touched |
| Command flows remain unchanged | no start/send/accept/final-refuse changes |

---

## 12. Test / Verification Plan

Component tests:

```text
- AgreementDocumentRefList renders existing metadata.
- AgreementDocumentRefList renders Download link when document, exchangeId and proposalId are present.
- Download link href is /api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download.
- Download link does not contain document.storageKey.
- AgreementDocumentRefList renders nothing when document is null.
- Details/proposal history passes details.exchangeId and proposal.proposalId to document list.
```

API/helper tests:

```text
- getAgreementProposalDocumentDownloadUrl encodes exchangeId and proposalId.
- helper does not accept storageKey parameter.
```

E2E planned:

```text
Client/Employee opens exchange details with proposal document.
Clicks Download.
Browser receives file response with original filename.
```

Non-goals:

```text
- no fetch Blob tests first pass;
- no storage internals in client tests;
- no unauthorized download UI handling beyond server response;
- no preview tests.
```

---

## 13. Suggested File Placement

```text
src/features/agreement-exchange/download-document/api/
  getAgreementProposalDocumentDownloadUrl.ts
  getAgreementProposalDocumentDownloadUrl.test.ts

src/widgets/agreement-exchange-details/
  AgreementDocumentRefList.tsx
  AgreementDocumentRefList.test.tsx
  agreementExchangeDetails.css
```

If the widget currently receives only `document`, update call sites to pass:

```text
exchangeId
proposalId
```

Do not add:

```text
src/shared/api/agreementDocumentApi.ts
src/entities/agreement-document/api/download.ts
raw storageKey URL helpers
```

---

## 14. Styling / CSS Ownership

Widget CSS owns:

```text
document metadata block
download link placement inside document block
```

Feature/helper has no CSS if it only builds URL.

Page CSS owns only page-level placement.

Do not:

```text
- broad global selectors;
- decorative visual redesign;
- public portal styling;
- page CSS reaching into document link internals.
```

Minimal visual direction:

```text
link or small secondary button under metadata
accessible focus state inherited from existing link/button styles
```

---

## 15. Implementation Checklist

```text
[ ] confirm server endpoint exists
[ ] confirm generated path/operation if using generated route constants
[ ] add URL builder for exchangeId + proposalId
[ ] do not accept storageKey in URL builder
[ ] update AgreementDocumentRefList props: exchangeId, proposalId, document
[ ] render Download link only when document exists and ids are valid
[ ] update proposal history/details call sites
[ ] keep existing document metadata display
[ ] add helper tests
[ ] add component tests
[ ] do not change upload/start/send proposal flows
[ ] do not add fetchJson wrapper for binary first pass
```

---

## 16. Guardrail Summary

```text
This sidecar adds a download action for already-visible proposal documents.
It does not authorize download on the client.
It does not use storageKey as download authority.
It uses exchangeId + proposalId URL only.
Server validates access and streams file.
No upload/command flow changes.
No broad visual redesign.
```

# SL-DOC-001 — Agreement proposal document upload implementation

This patch adds a minimal file upload slice for agreement proposal documents.

Included:
- `IDocumentStorage` and `StoredDocumentFile` abstraction.
- `LocalDocumentStorage` storing files under `App_Data/Documents/agreement-proposals`.
- `POST /api/agreement-proposal-documents` multipart endpoint.
- Client/Employee auth and CSRF requirement.
- Upload DTO/form validation for PDF file presence/content type/size.
- Response returns `AgreementDocumentRefDto` metadata usable by exchange commands.
- Server integration tests for auth, CSRF, validation, and happy path.
- Client `fetchFormData` helper that does not set `Content-Type` manually.
- Client upload API wrapper and unit tests.

Not included:
- No `Document` aggregate/table.
- No SQL binary storage.
- No download endpoint.
- No generated OpenAPI/TypeScript artifacts.
- Existing exchange forms are not fully converted to file-input workflow in this patch.

Known follow-up:
- Replace manual document metadata fields in start/send proposal forms with file upload flow:
  file input -> `uploadAgreementProposalDocument` -> exchange command with returned metadata.

# PRE-RELEASE-CHECK — 2026-05-19-sl-agr-doc-002-download-server-client-draft-v1

Archive type: docs-only slice draft archive.  
Scope: new server slice draft + new client sidecar draft for agreement proposal document download.

## Included replacement/new files

```text
planning/slices/SL-AGR-DOC-002-download-agreement-proposal-document.md
planning/slices/l2/L2-AGR-DOC-DOWNLOAD-001-agreement-proposal-document-download.client.md
```

## Original snapshots

Both files are new. No existing originals were replaced.

```text
_archive-review/2026-05-19-sl-agr-doc-002-download-server-client-draft-v1/original-files/NO_ORIGINALS_NEW_DRAFTS.txt
```

## Evidence checked before drafting

```text
AgreementProposalDocumentsController only owns upload.
IDocumentStorage already has OpenReadAsync.
AgreementExchange details controller/read path already resolves Client/Employee details by exchange.
AgreementExchange details DTO has exchangeId, proposalId, version and document metadata.
Current client AgreementDocumentRefList renders metadata only, no download link.
```

## Guardrails checked

```text
[checked] docs-only archive, no runtime implementation files.
[checked] no Domain.EnergyManagement changes.
[checked] no EnergyManagement.Server runtime changes.
[checked] no energymanagement.client runtime changes.
[checked] no generated OpenAPI/types changes.
[checked] no storageKey-only download endpoint proposed.
[checked] server draft requires exchange/proposal-contextual access check.
[checked] client draft forbids storageKey download URL.
[checked] upload/start/send/accept/final-refuse flows remain out of scope.
```

## Recommended implementation order after draft acceptance

```text
1. Implement SL-AGR-DOC-002 server endpoint and tests.
2. Generate OpenAPI/types.
3. Implement L2-AGR-DOC-DOWNLOAD-001.client link/button and tests.
```

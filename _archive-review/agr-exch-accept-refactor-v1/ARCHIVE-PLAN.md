# Archive Plan — agr-exch-accept-refactor-v1

Status: planned / safe draft-refactor replacement archive

## Purpose

Refactor one paired server/client accept-proposal draft set into the implemented-slice sync format without changing runtime code.

Selected pair:

```text
planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md
planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
```

Explicitly excluded by request:

```text
planning/slices/SL-AGR-EXCH-004-agreement-exchange-details-read.md
planning/slices/l2/L2-AGR-EXCH-DETAILS-001-agreement-exchange-details.client.md
```

## New files

```text
_archive-review/agr-exch-accept-refactor-v1/ARCHIVE-PLAN.md
_archive-review/agr-exch-accept-refactor-v1/ORIGINALS-INDEX.md
_archive-review/agr-exch-accept-refactor-v1/MERGE-RISK-REPORT.md
_archive-review/agr-exch-accept-refactor-v1/PRE-ARCHIVE-SELF-CHECK.md
_archive-review/agr-exch-accept-refactor-v1/PRE-RELEASE-ARCHIVE-CHECK.md
_archive-review/agr-exch-accept-refactor-v1/raw-author-message-log.md
_archive-review/agr-exch-accept-refactor-v1/derived-decisions.md
_archive-review/agr-exch-accept-refactor-v1/original-files/README.md
```

## Replacement files

Replacement files are staged under:

```text
_replacement-files/agr-exch-accept-refactor-v1/...
```

The apply script copies existing local files into:

```text
_archive-review/agr-exch-accept-refactor-v1/original-files/...
```

before replacing project files. This avoids losing local originals when the archive is expanded into the repo root.

## High-risk files

Both replacement files are planning/slice drafts and are high-value handoff docs.
They are not runtime code, tests or generated artifacts.

## Expected post-apply review

```powershell
git diff -- planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
git diff -- _archive-review/agr-exch-accept-refactor-v1
```

Verify:

```text
- no Client accept behavior was removed;
- no Employee accept was introduced;
- no body/DTO was introduced;
- 204 No Content remains;
- no-new-version rule remains;
- ClientAccountId ownership guard remains;
- original local files were captured under _archive-review/agr-exch-accept-refactor-v1/original-files/.
```

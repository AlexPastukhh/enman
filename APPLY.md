# APPLY — sl-emp-req-001-dashboard-server-client-draft-refactor-v1

Archive type: docs-only paired replacement archive.

## Replacement files

```text
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
```

## Review folder

```text
_archive-review/2026-05-19-sl-emp-req-001-dashboard-server-client-draft-refactor-v1/
```

## Apply from repository root

```powershell
Expand-Archive -Path ".\sl-emp-req-001-dashboard-server-client-draft-refactor-v1.zip" -DestinationPath "." -Force
```

## Scope

```text
- refactor server slice draft only;
- refactor paired client sidecar draft only;
- preserve originals under _archive-review;
- use current implementation evidence read-only;
- do not change runtime code;
- do not change tests;
- do not change generated artifacts;
- do not deep-refactor UI/CSS/page flow.
```

## After apply

```powershell
git diff -- planning/slices/SL-EMP-REQ-001-employee-request-list-read.md planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
```

Expected changed files are the two replacement drafts plus archive review files and this manifest/apply metadata.

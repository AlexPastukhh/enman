# Apply

Docs-only archive. Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-agr-exch-003-list-server-client-draft-refactor-v1.zip" -DestinationPath "." -Force
```

This archive refactors the paired server and client list drafts:

```text
planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md
planning/slices/l2/L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md
```

Runtime implementation, tests, deep UI refactor, page flow and redirects are not changed.

Original files are preserved in:

```text
_archive-review/2026-05-19-sl-agr-exch-003-list-server-client-draft-refactor-v1/original-files/
```

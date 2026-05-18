# Originals Index — agr-exch-accept-refactor-v1

Original snapshots are captured by the apply script from the local working tree before replacement.

Expected generated original files after apply:

```text
_archive-review/agr-exch-accept-refactor-v1/original-files/planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md
_archive-review/agr-exch-accept-refactor-v1/original-files/planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
```

Reason:

```text
The local working tree may include docs-only archives not yet pushed to GitHub.
Capturing originals at apply time is safer than using a stale remote snapshot.
```

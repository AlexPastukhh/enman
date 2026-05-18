# MERGE-RISK-REPORT

## Risk level

Low / add-only.

## Potential conflicts

- A file with the same path may already exist locally if it was created after the last shared repo snapshot.
- The apply command backs up an existing target file to `_archive-review/slice-applicant-party-verification-server-draft-addition-v1/original-files/` before copying.

## Manual checks after apply

```powershell
git diff -- planning/slices/SL-APPL-VER-001-run-mock-applicant-party-verification-from-employee-request-review.md
```

Check that the draft appears under `planning/slices/` next to other server slice drafts such as `SL-EMP-REQ-*` and `SL-APPL-*`.

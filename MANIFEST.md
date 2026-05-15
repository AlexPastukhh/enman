# MANIFEST

Archive: `enman-slice-questions-register-sync.zip`

Purpose: synchronize the shared slice questions register with the current implemented backend L1 slice documentation.

## Replace

```text
planning/slices/slice-questions-register.md
```

## Add

```text
MANIFEST.md
APPLY.md
```

## Delete

```text
None
```

## Scope

This archive updates documentation only.

It does not include:

```text
- production code
- generated artifacts
- runtime behavior changes
- GitHub branch/commit/PR changes
- client sidecars
- backend implementation changes
- UI pages
- CSRF implementation
- DB uniqueness constraints
```

## Source slice files used for sync

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
```

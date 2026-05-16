# MANIFEST

Archive: `sl-appl-002-full-draft-and-scope-rules.zip`

Purpose: documentation-only update for slice draft scope rules and full SL-APPL-002 backend/API read-model draft.

## Add

```text
none
```

## Replace

```text
planning/README.md
planning/planning-workflow-current.md
planning/agent-scope-boundaries-and-prompt-safety.md
planning/api/client-server-contract-principles.md
planning/client/README.md
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
```

## Delete

```text
none
```

## Non-goals

```text
- no runtime code changes;
- no generated artifacts;
- no domain changes;
- no client implementation;
- no GitHub writes/branches/commits/PRs;
- no change to My Requests docs except untouched context.
```

## Notes

```text
- SL-APPL-002 now uses flat applicantParties[] API direction.
- Client groups current/default vs other saved cards by isCurrentDefault.
- currentDefaults / otherApplicantParties grouped API arrays are superseded.
- Draft rules now require Scope / Out of scope / Related slices / Future extension points.
```

# MANIFEST — Applicant Current Active Scenario Sync

Archive: `enman-scenario-applicant-current-active-sync.zip`

Purpose:

Synchronize scenario/source documentation after the accepted decision:

```text
One L1 account has one current active ApplicantParty at a time.
Applicant types are alternative shapes of the account-level applicant profile,
not independent simultaneously-active current applicant contexts per type.
```

## Add

```text
(none)
```

## Replace

```text
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-data/SC-10-applicant-data.md
planning/diagrams/scenario-data/SC-04-request-creation-data.md
planning/diagrams/scenario-questions-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-implementation-notes-register.md
```

## Delete

```text
(none)
```

## Scope

This is a documentation-only archive.

It updates scenario text, DATA summaries, scenario questions and shared slice registers so downstream slice/client planning uses the accepted one-current-active-ApplicantParty-per-account direction.

## Non-goals

```text
- no production code changes;
- no runtime behavior changes;
- no generated artifact changes;
- no DB constraints or migrations;
- no client sidecars;
- no request creation UI;
- no My Requests UI/read implementation;
- no CSRF implementation;
- no GitHub writes, branch, commit or PR.
```

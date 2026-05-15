# Scenario Behavior Items

Status: active migration v1  
Scope: per-scenario behavior item files derived from scenario text specs, DATA specs, validation/security addenda and existing compiled baselines

## 1. Purpose

This folder contains per-scenario behavior items.

Behavior items are derived from:

```text
- scenario main flow;
- branches;
- invariants;
- outcomes;
- DATA requirements;
- validation/security addenda;
- existing compiled behavior baselines.
```

They do not invent new behavior.

If behavior item migration reveals missing behavior, update the scenario text spec.

If it reveals missing visible/input/selectable/filter/attachment data, update the DATA file.

If it reveals multiple valid interpretations, add/update an entry in:

```text
planning/diagrams/scenario-questions-register.md
```

## 2. Source Baselines Migrated

This migration uses:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

These files remain useful as compiled downstream baseline / historical domain-draft input.

Future domain/slice/client planning should prefer this folder for scenario-specific behavior coverage, then cross-check compiled baselines when needed.

## 3. Current Files

Use:

```text
00-scenario-behavior-items-index.md
```

as the lightweight global list.

Detailed behavior cards/tables live in per-scenario files:

```text
SC-01-guest-registration-behavior-items.md
SC-02-login-behavior-items.md
SC-03A-password-recovery-request-behavior-items.md
SC-03B-account-owner-verified-behavior-items.md
SC-04-client-request-creation-behavior-items.md
SC-05-my-requests-own-request-details-behavior-items.md
SC-06-employee-request-dashboard-behavior-items.md
SC-07A-employee-request-details-behavior-items.md
SC-07B-employee-request-review-behavior-items.md
SC-10-applicant-data-behavior-items.md
SC-11-request-documents-behavior-items.md
SC-13A-my-agreements-behavior-items.md
SC-13B-agreement-proposal-details-response-behavior-items.md
SC-13C-employee-agreements-behavior-items.md
SC-13D-employee-agreement-proposal-create-response-behavior-items.md
SC-14-client-data-verification-behavior-items.md
SC-15-security-text-specification-behavior-items.md
SC-17-anonymous-request-behavior-items.md
```

## 4. Category Style

Use the same category style as `planning/tables/pre-domain-variants-input.md`.

Default categories:

```text
CMD  — command behavior
LC   — lifecycle / state / condition behavior
IBS  — impossible business state candidate
VI   — value integrity / anti-primitive-obsession item
UCQ  — use-case coordination item
READ — read/access/listing behavior
INT  — integration/side-effect expectation
FUT  — future/deferred behavior
NW   — no-write / failure preservation behavior
SQ   — scenario question / clarification
```

The account activation addendum also introduced:

```text
SEC — security/access policy behavior
```

Do not add new categories without an explicit decision.

## 5. Per-Scenario File Shape

Each per-scenario file uses:

```text
# SC-XX — Scenario Name Behavior Items

## 1. Purpose
## 2. Source Set
## 3. Coverage Items Registry
## 4. Grouped Behavior Items
## 5. Cross-Scenario / Related Items
## 6. Scenario Questions Raised
## 7. Downstream Use
```

## 6. Migration Notes

This is an initial careful migration.

Known corrections made during migration:

```text
- Request creation uses InReview, not Submitted.
- ApplicantData naming is aligned toward ApplicantParty where the current domain model uses ApplicantParty.
- Agreement replacement wording is flagged: legacy baseline said previous client proposal becomes Rejected; current direction prefers SupersededByCounterProposal / Replaced semantics.
- Rejection feedback is treated as optional in the current domain direction; empty-feedback warning belongs to Client/UI.
```

Behavior item cleanup may continue later, but future changes must use the scenario question loop when they affect scenario/DATA/validation semantics.

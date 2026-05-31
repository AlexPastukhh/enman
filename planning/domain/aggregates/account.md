# Domain Aggregate Draft — Account

Status: draft / first-pass extraction  
Doc version: v0.1.0  
Scope: account identity, client account identity, employee account identity and activation/capability boundary

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-01-guest-registration.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-02-login.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-03B-account-owner-verified.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md @ historical/cross-check, version not declared
  Internal dependencies:
    - none
  Not checked:
    - full auth/session implementation and all account tests in current repo state
```

`Account` owns authenticated system identity and account-level capability state.

It covers:

```text
- shared account identity;
- client account registration identity;
- employee account identity;
- account role;
- activation state;
- employee command capability checks used by request review and agreement exchange flows.
```

It does not own ApplicantParty data, Request lifecycle, RequestReview lifecycle or AgreementProposalExchange lifecycle.

## 2. Source Inputs

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-data/README.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-01-guest-registration.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-02-login.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-03B-account-owner-verified.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-01-guest-registration-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-02-login-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-03B-account-owner-verified-behavior-items.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md @ historical/cross-check, version not declared
  Internal dependencies:
    - none
  Not checked:
    - full source/version/cascade alignment for all account/auth/session sources
```

This section is the aggregate-level reviewed source overview. Section-level `Sources:` blocks below are authoritative for local section work.

Scenario text sources:

```text
planning/diagrams/scenario-text-specs/SC-01-guest-registration.md @ Doc version: v0.1.0
planning/diagrams/scenario-text-specs/SC-02-login.md @ Doc version: v0.1.0
planning/diagrams/scenario-text-specs/SC-03B-account-owner-verified.md @ Doc version: v0.1.0
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md @ Doc version: v0.1.0
```

Scenario-specific DATA rule:

```text
Primary scenario-specific DATA belongs in the scenario text spec #DATA section.
Use planning/diagrams/scenario-data/ only for reusable/shared/audited/transitional DATA sidecars.
```

Reusable/shared/audited/transitional DATA sidecars checked or referenced by the prior draft:

```text
planning/diagrams/scenario-data/SC-03B-account-owner-verified-data.md @ Doc version: v0.1.0
```

Behavior/domain sources:

```text
planning/diagrams/scenario-behavior-items/SC-01-guest-registration-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/SC-02-login-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/SC-03B-account-owner-verified-behavior-items.md @ Doc version: v0.1.0
planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md @ historical/cross-check, version not declared
```

Current implementation sources checked in prior archive/source pass:

```text
Domain.EnergyManagement/Accounts/Account.cs
Domain.EnergyManagement/Accounts/ClientAccount.cs
Domain.EnergyManagement/Accounts/AccountRole.cs
Domain.EnergyManagement/Accounts/AccountActivationState.cs
Domain.EnergyManagement/Employees/Employee.cs
```

Not checked:

```text
Full auth/session implementation and all account tests were not audited in this pass.
Full source/version/cascade alignment is still deferred.
Current implementation files are not treated as freshly rechecked evidence unless explicitly reviewed in a later implementation-sync pass.
```

## 3. Aggregate Boundary

```text
Sources:
  Format/process:
    - planning/domain/domain-discovery-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md @ historical/cross-check, version not declared
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
  Internal dependencies:
    - Purpose
  Not checked:
    - current runtime implementation boundary beyond previously checked archive/source pass
```

Aggregate root / base type:

```text
Account
```

Concrete current types:

```text
ClientAccount
Employee
```

Important accepted decision:

```text
Employee is a concrete account type, not a separate EmployeeProfile linked by AccountId.
```

Not part of this aggregate:

```text
ApplicantParty reusable applicant/contact data;
ConnectionRequest and RequestReview;
AgreementProposalExchange;
read dashboards/lists;
file/blob storage;
UI session state.
```

External aggregate references:

```text
Other aggregates store scalar Account/Employee/ClientAccount ids when actor ownership is needed.
```

## 4. Owned State

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-01-guest-registration.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-03B-account-owner-verified.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-01-guest-registration-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-03B-account-owner-verified-behavior-items.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md @ historical/cross-check, version not declared
  Internal dependencies:
    - Aggregate Boundary
  Not checked:
    - current persistence mapping / EF configuration
```

Shared account state:

```text
Email
PasswordHash
Role
IsActive / ActivationState
CreatedAt
```

ClientAccount-specific state:

```text
currently no extra state in first-pass domain model
```

Employee-specific state:

```text
FullName
WindowsLogin?
```

Derived/read-only state:

```text
ActivationState from IsActive
employee capability checks from role/active state
```

Not stored here:

```text
ApplicantParty contact data;
request review state;
agreement proposal sender records beyond scalar employee/client ids;
JWT/session token state.
```

## 5. Domain Methods / Commands

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-01-guest-registration.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-03B-account-owner-verified.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-01-guest-registration-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-03B-account-owner-verified-behavior-items.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md @ historical/cross-check, version not declared
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
  Not checked:
    - application service orchestration
    - current implementation code/tests beyond previously checked archive/source pass
```

### `ClientAccount.Register`

Purpose:
- Create a client account identity.

Input:
- account email;
- password hash;
- createdAt.

Preconditions:
- email is present and valid;
- password hash is present.

State changes:
- creates account with `Role = Client`.

Source behavior:
- client registration/account behavior.

### `Employee.Create`

Purpose:
- Create an employee account identity.

Input:
- email;
- password hash;
- full name;
- optional windows login;
- createdAt.

Preconditions:
- email is present and valid;
- password hash is present;
- full name is present.

State changes:
- creates account with `Role = Employee`.

Source behavior:
- employee review/agreement actor resolution and account/employee TPH decision.

### `EnsureActivated`

Purpose:
- Protect account-only flows that require active account state.

Failure:
- account not activated.

### Employee capability checks

Purpose:
- Protect employee-only commands.

Current methods:

```text
EnsureCanReview
EnsureCanStartAgreementExchange
EnsureCanSendAgreementProposal
EnsureCanFinalRefuseAgreement
```

Failure:
- employee required;
- employee not active.

## 6. Invariants

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-01-guest-registration-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-02-login-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-03B-account-owner-verified-behavior-items.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md @ historical/cross-check, version not declared
    - Domain Methods / Commands
    - Owned State
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
    - Domain Methods / Commands
  Not checked:
    - runtime enforcement / tests unless explicitly reviewed
```

| Invariant | Protected by | Source | Failure/error |
|---|---|---|---|
| Account email is required. | `ClientAccount.Register`, `Employee.Create`. | implementation / auth scenarios | email required |
| Password hash is required. | `ClientAccount.Register`, `Employee.Create`. | implementation / auth scenarios | password required |
| Account has a role. | concrete type constructors. | implementation / decision | n/a |
| Employee has full name. | `Employee.Create`. | implementation / decision | employee full name required |
| Employee id is account id for employee actors. | account-employee TPH decision. | domain decision | avoid EmployeeProfile(AccountId) split |
| Employee command capability requires active employee account. | employee capability checks. | implementation / decision | employee not active / required |

## 7. Lifecycle / State Machine

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-text-specs/SC-03B-account-owner-verified.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-data/SC-03B-account-owner-verified-data.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-03B-account-owner-verified-behavior-items.md @ Doc version: v0.1.0
    - Domain Methods / Commands
    - Invariants
  Internal dependencies:
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - full activation/suspension/deactivation implementation and tests
```

Current activation state direction:

```text
PendingActivation / Active / Suspended / Deactivated are modeled as activation states.
Current implementation derives Active vs PendingActivation from IsActive.
```

Current first-pass commands:

```text
created account -> active account in current constructor behavior
EnsureActivated checks active/pending boundary
```

Open/future transitions:

```text
activation token confirmation;
suspension;
deactivation;
reactivation.
```

## 8. Impossible States Prevented

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-01-guest-registration-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-03B-account-owner-verified-behavior-items.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md @ historical/cross-check, version not declared
    - Invariants
    - Lifecycle / State Machine
    - Domain Methods / Commands
  Internal dependencies:
    - Invariants
    - Lifecycle / State Machine
    - Domain Methods / Commands
  Not checked:
    - current runtime code/tests unless explicitly reviewed
```

| Impossible state | Prevented by | Source |
|---|---|---|
| ClientAccount without email/password hash. | register validation. | implementation |
| Employee without email/password hash/full name. | create validation. | implementation |
| Employee request review command with non-employee actor. | employee capability checks. | implementation / decision |
| Employee actor resolved by separate Employee.AccountId profile in target model. | accepted TPH decision. | domain decision |

## 9. Value Objects Used

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - Domain Methods / Commands
    - Owned State
    - Invariants
  Internal dependencies:
    - Owned State
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - whether Email / PasswordHash / FullName should become separate domain value-object docs
```

| Value object | File | Purpose in this aggregate |
|---|---|---|
| Email | existing implementation value object; no separate domain file yet | Account auth email. |
| PasswordHash | existing implementation value object; no separate domain file yet | Account credentials. |
| FullName | existing implementation value object; no separate domain file yet | Employee name. |

## 10. Cross-Aggregate Relations

```text
Sources:
  Format/process:
    - planning/domain/domain-discovery-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - Aggregate Boundary
    - Domain Methods / Commands
  Internal dependencies:
    - Aggregate Boundary
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - application service implementation
```

References from other aggregates:

```text
ConnectionRequest stores ClientAccountId and employee ids in RequestReview.
AgreementProposalExchange stores ClientAccountId and proposal sender/final-refusal employee ids.
ApplicantParty stores ClientAccountId.
```

Rules not owned here:

```text
ApplicantParty current/default selection;
Request review lifecycle;
Agreement exchange lifecycle;
proposal send/accept/final refusal lifecycle.
```

Application coordination needed:

```text
- resolve current employee actor from auth/session;
- load Employee by account id / employee id for employee commands;
- load ClientAccount for client-owned commands where needed;
- enforce endpoint authorization before domain method call.
```

## 11. Behavior Coverage

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-01-guest-registration-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-02-login-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-03B-account-owner-verified-behavior-items.md @ Doc version: v0.1.0
    - Domain Methods / Commands
    - Invariants
    - Lifecycle / State Machine
    - Impossible States Prevented
    - Cross-Aggregate Relations
  Internal dependencies:
    - Domain Methods / Commands
    - Invariants
    - Lifecycle / State Machine
    - Impossible States Prevented
  Not checked:
    - unrelated scenario branches not mapped to Account
    - all account tests in current repo state
```

| Source item | Covered by | Status | Notes |
|---|---|---|---|
| Client registration | `ClientAccount.Register` | first-pass covered | Full auth flow not audited here. |
| Login / session identity | Account id / role identity | partial / application-owned | Auth/session mechanics outside aggregate draft. |
| Account activation guard | `EnsureActivated` | partial | Full activation workflow deferred. |
| Employee request review actor | `Employee` capability checks | covered / first-pass | RequestReview stores employee scalar ids. |
| Employee agreement exchange actor | `Employee` capability checks | covered / first-pass | AgreementProposalExchange stores actor scalar ids. |

## 12. Persistence / EF Notes

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - Owned State
    - Value Objects Used
    - Aggregate Boundary
    - planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md @ historical/cross-check, version not declared
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
    - Value Objects Used
  Not checked:
    - current EF configuration unless explicitly reviewed
```

Accepted target:

```text
Account
  -> ClientAccount
  -> Employee
```

Persistence direction:

```text
TPH in accounts table with discriminator/role.
Employee.Id == Account.Id for authenticated employee accounts.
```

Do not model target employee as:

```text
EmployeeProfile(AccountId)
```

## 13. Cross-Layer Placement Notes

```text
Sources:
  Format/process:
    - planning/domain/domain-responsibility-map.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
  Content:
    - Aggregate Boundary
    - Domain Methods / Commands
    - Cross-Aggregate Relations
    - Behavior Coverage
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
  Internal dependencies:
    - Aggregate Boundary
    - Cross-Aggregate Relations
    - Behavior Coverage
  Not checked:
    - API/client/slice implementation unless explicitly reviewed
```

Application layer:
- owns registration orchestration, password hashing and auth/session resolution;
- resolves current employee/client actor for command handlers.

API:
- owns endpoint authorization and DTO validation.

Client:
- owns login/register UI and role-specific navigation.

Testing:
- aggregate tests cover account creation/capability methods;
- integration tests cover auth/session/authorization boundaries.

## 14. Questions / Decisions

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
  Content:
    - Source Inputs
    - Behavior Coverage
    - Cross-Layer Placement Notes
    - planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md @ historical/cross-check, version not declared
  Internal dependencies:
    - Source Inputs
    - Behavior Coverage
    - Cross-Aggregate Relations
  Not checked:
    - sources needed to resolve open activation/auth boundary questions
```

Open:
- Should activation/suspension/deactivation become explicit domain commands later?
- Which account verification/security behavior belongs to domain vs auth infrastructure?
- Should Email / PasswordHash / FullName get separate value object docs later, or stay implementation-level common VOs?

Accepted:
- Employee is directly account-derived.
- Employee auth claim id resolves to Employee.Id / Account.Id.
- Domain methods that require employee actor receive Employee, not EmployeeRef.
- Other aggregates store scalar actor ids.

Deferred:
- Full account activation/suspension lifecycle extraction.
- Full auth/session implementation review.

## 15. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/source-cascade-sync-workflow.md @ version not confirmed in this batch
  Content:
    - changed sections in this Account aggregate draft
    - planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.1.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ version not confirmed in this batch
  Internal dependencies:
    - all changed sections in this draft
  Not checked:
    - downstream aggregates/slices not reviewed in this pass
```

```text
- Extracted first-pass Account aggregate draft from accepted account/employee hierarchy decision and current Account/ClientAccount/Employee implementation sources.
- Added `Doc version: v0.1.0`.
- Added local section-level fenced `Sources:` blocks for aggregate draft work.
- Reclassified `## 2. Source Inputs` as overview; section-level `Sources:` blocks are authoritative for local section work.
- Clarified scenario-specific DATA rule: prefer scenario text spec #DATA; use `planning/diagrams/scenario-data/` only for reusable/shared/audited/transitional sidecars.
- No Account domain behavior semantics changed in this pass.
```

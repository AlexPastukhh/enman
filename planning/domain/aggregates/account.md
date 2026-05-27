# Domain Aggregate Draft — Account

Status: draft / first-pass extraction  
Scope: account identity, client account identity, employee account identity and activation/capability boundary

## 1. Purpose

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

Scenario sources:

```text
planning/diagrams/scenario-text-specs/SC-01-register-client.md
planning/diagrams/scenario-text-specs/SC-02-login.md
planning/diagrams/scenario-text-specs/SC-03B-account-owner-verified.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
planning/diagrams/scenario-data/SC-03B-account-owner-verified-data.md
```

Behavior/domain sources:

```text
planning/diagrams/scenario-behavior-items/SC-01-guest-registration-behavior-items.md
planning/diagrams/scenario-behavior-items/SC-02-login-behavior-items.md
planning/diagrams/scenario-behavior-items/SC-03B-account-owner-verified-behavior-items.md
planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md
```

Current implementation sources checked in archive:

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
```

## 3. Aggregate Boundary

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

| Invariant | Protected by | Source | Failure/error |
|---|---|---|---|
| Account email is required. | `ClientAccount.Register`, `Employee.Create`. | implementation / auth scenarios | email required |
| Password hash is required. | `ClientAccount.Register`, `Employee.Create`. | implementation / auth scenarios | password required |
| Account has a role. | concrete type constructors. | implementation / decision | n/a |
| Employee has full name. | `Employee.Create`. | implementation / decision | employee full name required |
| Employee id is account id for employee actors. | account-employee TPH decision. | domain decision | avoid EmployeeProfile(AccountId) split |
| Employee command capability requires active employee account. | employee capability checks. | implementation / decision | employee not active / required |

## 7. Lifecycle / State Machine

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

| Impossible state | Prevented by | Source |
|---|---|---|
| ClientAccount without email/password hash. | register validation. | implementation |
| Employee without email/password hash/full name. | create validation. | implementation |
| Employee request review command with non-employee actor. | employee capability checks. | implementation / decision |
| Employee actor resolved by separate Employee.AccountId profile in target model. | accepted TPH decision. | domain decision |

## 9. Value Objects Used

| Value object | File | Purpose in this aggregate |
|---|---|---|
| Email | existing implementation value object; no separate domain file yet | Account auth email. |
| PasswordHash | existing implementation value object; no separate domain file yet | Account credentials. |
| FullName | existing implementation value object; no separate domain file yet | Employee name. |

## 10. Cross-Aggregate Relations

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

| Source item | Covered by | Status | Notes |
|---|---|---|---|
| Client registration | `ClientAccount.Register` | first-pass covered | Full auth flow not audited here. |
| Login / session identity | Account id / role identity | partial / application-owned | Auth/session mechanics outside aggregate draft. |
| Account activation guard | `EnsureActivated` | partial | Full activation workflow deferred. |
| Employee request review actor | `Employee` capability checks | covered / first-pass | RequestReview stores employee scalar ids. |
| Employee agreement exchange actor | `Employee` capability checks | covered / first-pass | AgreementProposalExchange stores actor scalar ids. |

## 12. Persistence / EF Notes

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
- Extracted first-pass Account aggregate draft from accepted account/employee hierarchy decision and current Account/ClientAccount/Employee implementation sources.
```

# Domain Decision — Account / Employee Hierarchy And TPH

Status: accepted  
Doc version: v0.1.0  
Scope: account identity, employee identity, auth claim mapping and employee command actor resolution

## 1. Decision

`Employee` is a concrete account type.

Target hierarchy:

```text
Account
  -> ClientAccount
  -> Employee
```

Persistence direction:

```text
Table: accounts / L1Accounts
Mapping: TPH
Discriminator: AccountType / Role
Concrete types:
  ClientAccount
  Employee
```

Identity rule:

```text
ClaimTypes.NameIdentifier stores Account.Id.
For Employee sessions:
  Account.Id == Employee.Id.
```

Therefore:

```text
Employee is not a separate profile entity linked by AccountId.
Do not model Employee as EmployeeProfile(AccountId).
Do not resolve Employee by AccountId -> Employee.AccountId in the target model.
```

## 2. Context

Older domain draft material left employee identity ambiguous:

```text
ClientAccount : Account existed.
Employee account appeared as an open question.
Some older sketches used EmployeeRef or separate Employee AccountId-style thinking.
```

That ambiguity conflicts with auth/session actor resolution:

```text
ClaimTypes.NameIdentifier = Account.Id
but separate Employee profile would have different Employee.Id + AccountId
```

The accepted target removes that split.

## 3. Sources

```text
planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md
planning/tables/domain-drafts/domain-draft-02.md
Domain.EnergyManagement/Accounts/Account.cs
Domain.EnergyManagement/Accounts/ClientAccount.cs
Domain.EnergyManagement/Employees/Employee.cs
Domain.EnergyManagement/Accounts/AccountRole.cs
```

Not checked:

```text
Full EF mapping configuration and migration history.
Full auth handler/session implementation.
```

## 4. Options Considered

### Option A — Employee as separate profile linked by AccountId

Pros:
- separates auth account and employee profile.

Cons:
- forces extra lookup by AccountId;
- makes employee command actor ids ambiguous;
- conflicts with target decision that employee id is account id;
- leaks compatibility shape into new domain drafts.

### Option B — Employee as concrete Account subtype

Pros:
- aligns auth claim id with employee domain id;
- simplifies employee actor resolution;
- lets employee capability checks live on Employee;
- matches current target implementation direction.

Cons:
- requires inheritance/TPH persistence discipline;
- broader account activation rules apply to employee accounts.

## 5. Chosen Direction

Use Option B:

```text
Employee : Account
ClientAccount : Account
```

Employee domain methods/capability checks are called with an `Employee` domain object.

Owned state in other aggregates stores scalar ids:

```text
StartedByEmployeeId
CompletedByEmployeeId
FinalRefusedByEmployeeId
SenderId
```

Those ids are Employee.Id, and because `Employee : Account`, they are also the authenticated Account.Id.

## 6. Consequences

Domain:
- request review and agreement exchange commands receive Employee when employee capability matters;
- Account owns activation/role identity;
- Employee owns employee-specific capability checks.

Application:
- employee endpoints resolve actor from current auth Account.Id;
- handlers load Employee by current account id/employee id;
- no target requirement for `GetEmployeeByAccountId` unless temporary compatibility needs it.

Persistence:
- TPH/discriminator mapping should preserve Account/ClientAccount/Employee hierarchy.

API/client:
- role-specific navigation and endpoint access use account role/session identity.

Testing:
- employee command integration tests should verify actor resolution and forbidden non-employee access separately from aggregate method tests.

## 7. Affected Files

Current domain docs:

```text
planning/domain/aggregates/account.md
planning/domain/aggregates/connection-request.md
planning/domain/aggregates/agreement-proposal-exchange.md
planning/domain/scenario-to-aggregate-map.md
```

Historical source:

```text
planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md
```

## 8. Questions / Follow-ups

Open:
- Verify final EF mapping and discriminator names during implementation/persistence audit.
- Verify whether current auth/session implementation fully matches this direction.

Closed:

```text
Q: Should Employee be directly Account-derived or linked by AccountId?
A: Employee is directly Account-derived.

Q: Does account activation apply to Employee?
A: Yes. Employee inherits account activation/session identity.

Q: Does EmployeeRef remain in target employee command flows?
A: No. Domain methods receive Employee, and owned state stores scalar Employee.Id fields.
```

## 9. Change Log

```text
- Extracted from transitional domain draft addendum into planning/domain/decisions/ as first-pass domain decision file.
```

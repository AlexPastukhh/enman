# Domain Draft 02 Addendum — Account / Employee Hierarchy Decision

Status: accepted domain decision / applies to L2 Employee review and agreement proposal slices  
Source context: `planning/tables/domain-drafts/domain-draft-02.md`, `planning/tables/domain-drafts/domain-draft-01.md`  
Scope: Account identity, Employee identity, auth claim mapping, review command actor resolution

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
Table: L1Accounts
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
Do not resolve Employee by AccountId -> Employee.AccountId in the L2 target model.
```

## 2. Why this decision is needed

Older draft material had an ambiguity:

```text
ClientAccount : Account existed in sketches.
Employee account appeared as an open question.
Employee : Account was not explicitly accepted.
Some older sketches used EmployeeRef or separate Employee AccountId-style thinking.
```

That ambiguity creates an implementation problem:

```text
ClaimTypes.NameIdentifier = Account.Id
but Employee has separate Id and AccountId
```

The accepted target removes that split for L2:

```text
current Employee id = current Account id for Employee accounts.
```

## 3. Domain modeling rule

Review/agreement domain methods receive `Employee` when employee capability matters:

```csharp
request.StartReview(employee, startedAt);
request.ApproveReview(employee, approvedAt);
request.RejectReview(employee, feedback, rejectedAt);
exchange.SendEmployeeProposal(employee, documentRef, comment, sentAt);
exchange.FinalRefuseProposal(employee, reason, refusedAt);
```

Owned state stores scalar ids:

```text
StartedByEmployeeId
CompletedByEmployeeId
FinalRefusedByEmployeeId
SenderId
```

Those ids are `Employee.Id`, and because `Employee : Account`, they are also the `Account.Id` from the employee auth claim.

## 4. Code sketch direction

```csharp
public abstract class Account : L1Entity
{
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public AccountRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
}

public sealed class ClientAccount : Account
{
    // Client-specific behavior can live here.
}

public sealed class Employee : Account
{
    public FullName FullName { get; private set; }

    public UnitResult<IReadOnlyList<Error>> EnsureCanReview() { ... }
    public UnitResult<IReadOnlyList<Error>> EnsureCanStartAgreementExchange() { ... }
    public UnitResult<IReadOnlyList<Error>> EnsureCanSendAgreementProposal() { ... }
    public UnitResult<IReadOnlyList<Error>> EnsureCanFinalRefuseAgreement() { ... }
}
```

Do not use this target sketch:

```csharp
public sealed class Employee : L1Entity
{
    public long AccountId { get; private set; }
}
```

That shape is superseded by the accepted `Employee : Account` decision.

## 5. Application/auth resolution rule

Employee endpoints resolve the current employee actor from auth/session:

```text
ClaimTypes.NameIdentifier
        ↓
Account.Id
        ↓
Employee.Id because the authenticated account concrete type is Employee
```

A handler may load the `Employee` aggregate/account by id:

```text
GetEmployeeById(currentAccountId)
```

It should not require:

```text
GetEmployeeByAccountId(currentAccountId)
```

unless temporary compatibility with old code requires it.

## 6. Slice impact

Applies to:

```text
SL-EMP-REQ-001 — Employee Request List Read
SL-EMP-REQ-002 — Employee Request Details Read
SL-EMP-REQ-003 — Start Request Review
future SL-EMP-REQ-004 — Approve Request Review
future SL-EMP-REQ-005 — Reject Request Review
future AgreementProposalExchange employee command slices
```

For reads:

```text
currentEmployeeId = authenticated Employee Account.Id
```

For commands:

```text
load Employee by Account.Id / Employee.Id
pass Employee domain object into Request/Exchange domain method
store Employee.Id in owned state
```

## 7. Migration / compatibility note

If current runtime code already has a separate `Employee` table/profile with `AccountId`, treat that as compatibility or implementation drift to resolve in a scoped domain/persistence slice.

Do not let compatibility shape leak into new L2 target drafts.

## 8. Questions closed

```text
Q: Should Employee be directly Account-derived or linked by AccountId?
A: Employee is directly Account-derived.

Q: Does account activation apply to Employee?
A: Yes. Employee inherits account activation/session identity. Employee-specific capability checks can add stricter rules later.

Q: Does EmployeeRef remain in L2?
A: No. Domain methods receive Employee, and owned state stores scalar Employee.Id fields.
```

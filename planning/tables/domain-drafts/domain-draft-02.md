# Domain Draft 02 — L2 Domain Model: Employee, Request Review, Agreement Proposal Exchange

Status: draft 02 / L2 target domain draft  
Workflow: gradual domain discovery  
Purpose: final-style detailed domain draft for the next broader domain layer after current L1 foundation

## 1. Draft Goal

This draft is a detailed L2 domain model snapshot.

It is not an implementation archive for domain code.

It is not a competing alternative to `domain-draft-01.md`.

It extends the restored ApplicantParty-centric domain draft style with detailed target domain implementation for:

```text
Employee / employee-side domain actor
Request review owned by Request
AgreementProposalExchange
AgreementProposal
AgreementProposalVersion
final agreement refusal
```

Important draft rule:

```text
This draft stores detailed domain implementation direction:
- domain classes;
- owned entities;
- value objects;
- aggregate boundaries;
- state machines;
- impossible states;
- use-case orchestration;
- code sketches.

Code sketches are part of the draft.
They are not disposable examples.
They are still draft-level target code, not current compiled code.
```

Key L2 decisions captured in this draft:

```text
- Use Employee terminology, not Worker.
- Remove EmployeeRef from L2 target model.
- Domain methods receive Employee domain object when employee behavior matters.
- Owned state stores scalar ids where references are needed:
  EmployeeId, ClientAccountId, RequestId, ExchangeId, SenderId.

- Employee is a domain actor/aggregate candidate.
- Review is not an aggregate.
- Request owns Review as child/entity.
- Request exposes public review API.
- Review methods are internal and called by Request.
- Review starts via Request.StartReview(...).
- Approve/reject requires a started review.
- Dashboard/details can show started review state.
- Another employee cannot start or complete a review already started by someone else.
- ReviewDecisionRecord is removed from L2 target model.
- Decision/result data lives inside Review.

- AgreementProposalExchange is a separate aggregate from Request.
- AgreementProposal is a child entity of AgreementProposalExchange.
- AgreementProposalExchange stores ActiveProposalVersion, not ActiveProposalId.
- AgreementProposalVersion is a local per-exchange value object generated only by the exchange.
- AgreementProposalAuthor uses Sender + SenderId.
- AgreementDocumentRef replaces unclear DocumentFileRef.
- ProposalComment is optional; if present, it is a value object.
- Final agreement refusal belongs to AgreementProposalExchange.
- Final refusal is not a separate entity/class.
- FinalRefusalReason may be optional value object.
- Final refusal does not create a new proposal version.
- Application service orchestrates cross-aggregate final refusal:
  exchange.FinalRefuseProposal(...)
  request.MarkAgreementExchangeFailed(...)
  save.
```

L2 naming decisions:

```text
Use:
Employee
Start / Started
StartReview
Review Started
StartByEmployee
EmployeeSendNewVersion
AwaitingEmployeeResponse
AgreementProposalSender.Employee

Do not use:
Worker
Open / Opened
EmployeeRef
ReviewDecisionRecord
DocumentFileRef
AggregateId inside proposal author
```

## 2. Source Inputs

Primary inputs:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
planning/domain-draft-generation-guide.md
planning/tables/domain-drafts/domain-draft-01.md
```

L1 / current implementation compatibility inputs:

```text
planning/current-state.md
planning/domain-model.md
planning/l1-domain-implementation-cut.md
planning/l1-domain-testing-rules.md
Domain.EnergyManagement/L1/Accounts/
Domain.EnergyManagement/L1/Applicants/
Domain.EnergyManagement/L1/Requests/
```

ApplicantParty slice inputs informing Draft 02:

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
```

Boundary:

```text
Current implementation may be narrower than this L2 draft.

When current code conflicts with this L2 target direction:
- current code is compatibility/background;
- this draft records target L2 direction;
- implementation still requires scoped slices and tests.
```

## 3. Current Domain Direction

Current broad domain direction:

```text
Account / ClientAccount
  account/auth identity and activation marker

Employee
  employee-side domain actor for review and agreement proposal actions

ApplicantParty
  persisted reusable applicant/contact/template data owned by ClientAccount

ClientRequest / ConnectionRequest
  request created from ApplicantParty

RequestReview
  child entity owned by Request, not aggregate

AgreementProposalExchange
  post-approval agreement proposal version exchange aggregate

AgreementProposal
  child proposal version inside AgreementProposalExchange

VerificationResult
  request-context-only, deferred/future

AnonymousSubmission
  deferred/open
```

Key retained L1 decisions:

```text
- Account email and applicant email are different concepts.
- ClientAccount.Email = auth/login/recovery email.
- ApplicantParty.Email = applicant contact email.
- Current core registration creates Active account.
- Protected use cases require activated account.
- Business aggregates should not duplicate account activation logic internally.

- Applicant DATA from scenarios is represented as ApplicantParty.
- No separate ApplicantData aggregate in this draft.
- No ApplicantSnapshot in this draft.

- ApplicantParty is reusable applicant/contact/template data.
- ApplicantParty creation is additive.
- Multiple ApplicantParties of the same type for one account are allowed.
- Current/default ApplicantParty is selected per ApplicantPartyType.
- Current/default is client preference / default template / prefill choice.
- Current/default is not verification status.
- Verified and Unverified ApplicantParties can both be current/default.
- Unverified ApplicantParty can be edited in place.
- Verified ApplicantParty edit is blocked for now.
- ApplicantParty delete is hard delete for simplicity.

- ConnectionRequest stores ApplicantPartyId.
- ConnectionRequest does not store ClientAccountId.
- Existing requests remain linked to the ApplicantParty selected/used at request creation time.
- Changing current/default ApplicantParty does not relink or rewrite existing requests.
```

New L2 request/review direction:

```text
- Request review must be explicitly started.
- A request with started review is marked in employee dashboard/details.
- If another employee started review, current employee cannot start/approve/reject it.
- Approve/reject requires started review.
- Request owns Review.
- Review is not separate aggregate.
```

New L2 agreement proposal direction:

```text
- AgreementProposalExchange is separate aggregate from Request.
- Request approval enables exchange start but does not create it.
- Employee starts exchange by sending first proposal.
- Client can accept active employee proposal.
- Client can send own proposal version in response.
- Employee can send new version after client proposal.
- SupersededByCounterProposal means replacement, not final refusal.
- Employee can final-refuse the exchange.
- Final refusal marks exchange as FinallyRefused.
- Application service then asks Request to mark agreement exchange failed.
```

## 4. Aggregate / Class Candidates

### Current / target aggregate candidates

```text
Account
ClientAccount
Employee
ApplicantParty
IndividualApplicantParty
ClientRequest
ConnectionRequest
AgreementProposalExchange
```

### Owned child entities / value-like domain records

```text
RequestReview
AgreementProposal
```

### Value objects

```text
Email
PasswordHash
FullName
PhoneNumber
Address
EmployeeName / FullName
ApplicantPartyType
ApplicantPartyVerificationStatus
RequestStatus
RequestReviewStatus
RejectionFeedback
AgreementProposalVersion
AgreementProposalAuthor
AgreementDocumentRef
ProposalComment
FinalRefusalReason
```

### Future / deferred concepts

```text
Employee assignment / queue
Employee department / permission model
VerificationResult
AnonymousSubmission
SignedAgreement
AgreementConclusion
RequestNumber
AgreementProposalNumber
```

### Not aggregate candidates

```text
Email sender
Password hasher
File/blob storage
Verification provider
Dapper read projections
Session/auth framework
OpenAPI contract artifacts
Generated client constants
Generated TypeScript API types
React Query cache
Route constants
```

# 5. Class-By-Class Model

## 5.1 Account / ClientAccount

### Purpose

`Account` is the base account/auth identity.

`ClientAccount` is the client account type.

`EmployeeAccount` may be introduced for employee login/auth identity, but employee domain behavior is modeled through `Employee`.

### State owned by Account

```text
Id
Email
PasswordHash
Role
IsActive
ActivationState
CreatedAt
```

### Methods / commands

```text
Register(...)
EnsureActivated()
```

### Invariants protected

```text
- Account cannot be created without Email.
- Account cannot be created without PasswordHash.
- Current registration creates Active account.
- Protected use cases require active account.
```

### Code sketch

```csharp
public abstract class Account : L1Entity
{
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public AccountRole Role { get; private set; }

    public AccountActivationState ActivationState =>
        IsActive
            ? AccountActivationState.Active
            : AccountActivationState.PendingActivation;

    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    protected Account(
        Email email,
        PasswordHash passwordHash,
        AccountRole role,
        DateTimeOffset createdAt)
    {
        Guard.IsNotNull(email);
        Guard.IsNotNull(passwordHash);

        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
        CreatedAt = createdAt;
    }

    protected Account()
    {
        Email = null!;
        PasswordHash = null!;
    }

    public UnitResult<IReadOnlyList<Error>> EnsureActivated()
    {
        if (ActivationState != AccountActivationState.Active)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AccountNotActivated]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
```

```csharp
public sealed class ClientAccount : Account
{
    private ClientAccount(
        Email email,
        PasswordHash passwordHash,
        DateTimeOffset createdAt)
        : base(email, passwordHash, AccountRole.Client, createdAt)
    {
    }

    private ClientAccount()
    {
    }

    public static Result<ClientAccount, IReadOnlyList<Error>> Register(
        Email email,
        PasswordHash passwordHash,
        DateTimeOffset createdAt)
    {
        var errors = new List<Error>();

        if (email is null)
        {
            errors.Add(Errors.Account.EmailIsRequired);
        }

        if (passwordHash is null)
        {
            errors.Add(Errors.Account.PasswordIsRequired);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<ClientAccount, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<ClientAccount, IReadOnlyList<Error>>(
            new ClientAccount(email!, passwordHash!, createdAt));
    }
}
```

Future role expansion:

```csharp
public enum AccountRole
{
    Client = 1,
    Employee = 2
}
```

## 5.2 Employee

### Purpose

`Employee` is the domain actor who reviews client requests and performs employee-side agreement proposal actions.

L2 removes `EmployeeRef`.

Domain methods receive `Employee employee`.

Owned records store scalar ids such as:

```text
StartedByEmployeeId
CompletedByEmployeeId
FinalRefusedByEmployeeId
AgreementProposalAuthor.SenderId
```

### State owned

```text
Id
AccountId
FullName / DisplayName
IsActive
CreatedAt
```

### Methods / commands

```text
EnsureCanReview()
EnsureCanStartAgreementExchange()
EnsureCanSendAgreementProposal()
EnsureCanFinalRefuseAgreement()
```

### Invariants protected

```text
- inactive employee cannot perform protected employee domain actions;
- employee must be persisted before being referenced by child records;
- employee domain object is passed to methods when employee capability is part of behavior;
- stored references use employee id, not EmployeeRef.
```

### Code sketch

```csharp
public sealed class Employee : L1Entity
{
    public long AccountId { get; private set; }

    public FullName FullName { get; private set; }

    public bool IsActive { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    private Employee()
    {
        FullName = null!;
    }

    public UnitResult<IReadOnlyList<Error>> EnsureCanReview()
    {
        if (!IsActive)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsNotActive]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> EnsureCanStartAgreementExchange()
    {
        if (!IsActive)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsNotActive]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> EnsureCanSendAgreementProposal()
    {
        if (!IsActive)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsNotActive]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> EnsureCanFinalRefuseAgreement()
    {
        if (!IsActive)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsNotActive]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
```

### Open questions

```text
- Should Employee be directly Account-derived or linked by AccountId?
- Does Employee activation exactly mirror ClientAccount activation?
- Does employee profile live in domain or read model?
- Do we need department/position/permissions in first employee cut?
- Should assignment/claiming be modeled before review start?
```

## 5.3 ApplicantParty

### Purpose

`ApplicantParty` is persisted reusable applicant/contact/template data owned by `ClientAccount`.

Scenario term:

```text
Applicant DATA
```

Domain term:

```text
ApplicantParty
```

No separate `ApplicantData` aggregate.

No `ApplicantSnapshot` by default.

### Class hierarchy

```text
ApplicantParty
  -> IndividualApplicantParty
  -> EntrepreneurApplicantParty [VAR:EXPAND]
  -> LegalEntityApplicantParty [VAR:EXPAND]
```

### State owned

```text
Id
ClientAccountId
Type
VerificationStatus
Email
PhoneNumber
IsCurrentActiveVersion
CreatedAt
```

For `IndividualApplicantParty`:

```text
FullName
```

### Methods / commands

```text
Create(...)
GetDisplayName()
CanMarkVerified()
MarkVerified()
MarkInactiveVersion()
MarkAsCurrentDefaultTemplate()
CanEdit()
Edit(...)
HasMinimumDataForVerification()
```

### Current/default policy

```text
- IsCurrentActiveVersion is current persisted name.
- Refined meaning: current/default template marker.
- New ApplicantParty starts non-current/default.
- First-of-type default/current initialization is application/use-case coordination.
- Current/default is per ApplicantPartyType.
- Current/default can be Verified or Unverified.
- Current/default changes do not relink existing requests.
- Rename IsCurrentActiveVersion later in dedicated cleanup/migration task.
```

### Edit/delete policy

```text
- Unverified ApplicantParty can be edited in place.
- Verified ApplicantParty edit is blocked for now.
- Delete/archive policy: hard delete for simplicity.
```

### Code sketch

```csharp
public abstract class ApplicantParty : L1Entity
{
    public long ClientAccountId { get; protected set; }

    public ApplicantPartyType Type { get; protected set; }

    public ApplicantPartyVerificationStatus VerificationStatus { get; protected set; }

    public Email Email { get; protected set; }

    public PhoneNumber PhoneNumber { get; protected set; }

    public bool IsCurrentActiveVersion { get; protected set; }

    public DateTimeOffset CreatedAt { get; protected set; }

    protected ApplicantParty()
    {
        Email = null!;
        PhoneNumber = null!;
    }

    public abstract string GetDisplayName();

    public UnitResult<IReadOnlyList<Error>> CanMarkVerified()
    {
        if (VerificationStatus == ApplicantPartyVerificationStatus.Verified)
        {
            return UnitResult.Success<IReadOnlyList<Error>>();
        }

        if (!HasMinimumDataForVerification())
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.CannotVerifyIncompleteApplicantParty]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> MarkVerified()
    {
        var canVerify = CanMarkVerified();

        if (canVerify.IsFailure)
        {
            return canVerify;
        }

        VerificationStatus = ApplicantPartyVerificationStatus.Verified;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> MarkInactiveVersion()
    {
        IsCurrentActiveVersion = false;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> MarkAsCurrentDefaultTemplate()
    {
        IsCurrentActiveVersion = true;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> CanEdit()
    {
        if (VerificationStatus == ApplicantPartyVerificationStatus.Verified)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.VerifiedApplicantPartyCannotBeEdited]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    protected abstract bool HasMinimumDataForVerification();
}
```

```csharp
public sealed class IndividualApplicantParty : ApplicantParty
{
    public FullName FullName { get; private set; }

    private IndividualApplicantParty()
    {
        FullName = null!;
    }

    public static Result<IndividualApplicantParty, IReadOnlyList<Error>> Create(
        long clientAccountId,
        FullName fullName,
        Email contactEmail,
        PhoneNumber phoneNumber,
        DateTimeOffset createdAt)
    {
        var errors = new List<Error>();

        if (clientAccountId <= 0)
        {
            errors.Add(Errors.L1Domain.ClientAccountIsRequired);
        }

        if (fullName is null)
        {
            errors.Add(Errors.Account.FirstNameIsRequired);
        }

        if (contactEmail is null)
        {
            errors.Add(Errors.Account.EmailIsRequired);
        }

        if (phoneNumber is null)
        {
            errors.Add(Errors.Account.PhoneNumberIsRequired);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<IndividualApplicantParty, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<IndividualApplicantParty, IReadOnlyList<Error>>(
            new IndividualApplicantParty
            {
                ClientAccountId = clientAccountId,
                Type = ApplicantPartyType.Individual,
                VerificationStatus = ApplicantPartyVerificationStatus.Unverified,
                IsCurrentActiveVersion = false,
                FullName = fullName!,
                Email = contactEmail!,
                PhoneNumber = phoneNumber!,
                CreatedAt = createdAt
            });
    }

    public UnitResult<IReadOnlyList<Error>> Edit(
        FullName fullName,
        Email contactEmail,
        PhoneNumber phoneNumber)
    {
        var canEdit = CanEdit();

        if (canEdit.IsFailure)
        {
            return canEdit;
        }

        var errors = new List<Error>();

        if (fullName is null)
        {
            errors.Add(Errors.Account.FirstNameIsRequired);
        }

        if (contactEmail is null)
        {
            errors.Add(Errors.Account.EmailIsRequired);
        }

        if (phoneNumber is null)
        {
            errors.Add(Errors.Account.PhoneNumberIsRequired);
        }

        if (errors.Count > 0)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(errors);
        }

        FullName = fullName!;
        Email = contactEmail!;
        PhoneNumber = phoneNumber!;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public override string GetDisplayName()
    {
        return $"{FullName.LastName} {FullName.FirstName} {FullName.MiddleName}";
    }

    protected override bool HasMinimumDataForVerification()
    {
        return FullName is not null
            && Email is not null
            && PhoneNumber is not null;
    }
}
```

## 5.4 ClientRequest / ConnectionRequest

### Purpose

`ClientRequest` is the base request domain class.

`ConnectionRequest` is the concrete request type for connection requests.

L2 request review is owned by `ConnectionRequest`.

### State owned

```text
ApplicantPartyId
RequestType
Status
Details
ObjectAddress
Review?
AgreementExchangeFailure?
CreatedAt
```

### RequestStatus

```csharp
public enum RequestStatus
{
    InReview = 1,
    Approved = 2,
    Rejected = 3,
    AgreementExchangeFailed = 4
}
```

Started review is not a request status.

It is derived from:

```text
Review.Status == Started
```

### Public review API

```text
StartReview(employee, startedAt)
ApproveReview(employee, decidedAt)
RejectReview(employee, feedback, decidedAt)
```

### Request invariants

```text
- Request can start review only when Status == InReview.
- Request cannot start review if active started Review already exists.
- Approve/reject requires started Review.
- Only employee who started review can approve/reject it.
- ApproveReview changes Request.Status to Approved.
- RejectReview changes Request.Status to Rejected.
- Failed review command does not change Request status or Review state.
- Request can be marked AgreementExchangeFailed only from Approved.
```

### Code sketch

```csharp
public sealed class ConnectionRequest : ClientRequest
{
    private RequestReview? _review;

    public RequestReview? Review => _review;

    public UnitResult<IReadOnlyList<Error>> StartReview(
        Employee employee,
        DateTimeOffset startedAt)
    {
        if (employee is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
        }

        var canReview = employee.EnsureCanReview();
        if (canReview.IsFailure)
        {
            return canReview;
        }

        if (Status != RequestStatus.InReview)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyInReviewRequestCanStartReview]);
        }

        if (_review is not null && _review.Status == RequestReviewStatus.Started)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestReviewAlreadyStarted]);
        }

        _review = RequestReview.StartForRequest(
            Id,
            employee,
            startedAt);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> ApproveReview(
        Employee employee,
        DateTimeOffset decidedAt)
    {
        if (_review is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestReviewMustBeStarted]);
        }

        if (Status != RequestStatus.InReview)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyInReviewRequestCanBeApproved]);
        }

        var approve = _review.Approve(employee, decidedAt);
        if (approve.IsFailure)
        {
            return approve;
        }

        Status = RequestStatus.Approved;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> RejectReview(
        Employee employee,
        RejectionFeedback? feedback,
        DateTimeOffset decidedAt)
    {
        if (_review is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestReviewMustBeStarted]);
        }

        if (Status != RequestStatus.InReview)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyInReviewRequestCanBeRejected]);
        }

        var reject = _review.Reject(
            employee,
            feedback,
            decidedAt);

        if (reject.IsFailure)
        {
            return reject;
        }

        Status = RequestStatus.Rejected;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> MarkAgreementExchangeFailed(
        long agreementProposalExchangeId,
        DateTimeOffset failedAt)
    {
        if (agreementProposalExchangeId <= 0)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AgreementProposalExchangeIsRequired]);
        }

        if (Status != RequestStatus.Approved)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyApprovedRequestCanBeMarkedAgreementExchangeFailed]);
        }

        Status = RequestStatus.AgreementExchangeFailed;

        // Optional future value object:
        // AgreementExchangeFailure = AgreementExchangeFailureRef.Create(
        //     agreementProposalExchangeId,
        //     failedAt);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
```

## 5.5 RequestReview

### Purpose

`RequestReview` represents started/completed review of a request.

It is not an aggregate.

It is owned by `ConnectionRequest`.

It has no repository.

Public API is on `ConnectionRequest`.

### State owned

```text
RequestId
Status
StartedByEmployeeId
StartedAt
CompletedByEmployeeId?
CompletedAt?
RejectionFeedback?
```

### Status

```csharp
public enum RequestReviewStatus
{
    Started = 1,
    Approved = 2,
    Rejected = 3
}
```

### Code sketch

```csharp
public sealed class RequestReview
{
    public long RequestId { get; private set; }

    public RequestReviewStatus Status { get; private set; }

    public long StartedByEmployeeId { get; private set; }

    public DateTimeOffset StartedAt { get; private set; }

    public long? CompletedByEmployeeId { get; private set; }

    public DateTimeOffset? CompletedAt { get; private set; }

    public RejectionFeedback? RejectionFeedback { get; private set; }

    private RequestReview()
    {
    }

    internal static RequestReview StartForRequest(
        long requestId,
        Employee employee,
        DateTimeOffset startedAt)
    {
        if (requestId <= 0)
        {
            throw new InvalidOperationException("Request must be persisted before review can start.");
        }

        if (employee is null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        return new RequestReview
        {
            RequestId = requestId,
            Status = RequestReviewStatus.Started,
            StartedByEmployeeId = employee.Id,
            StartedAt = startedAt
        };
    }

    internal UnitResult<IReadOnlyList<Error>> Approve(
        Employee employee,
        DateTimeOffset decidedAt)
    {
        var canComplete = CanComplete(employee);
        if (canComplete.IsFailure)
        {
            return canComplete;
        }

        Status = RequestReviewStatus.Approved;
        CompletedByEmployeeId = employee.Id;
        CompletedAt = decidedAt;
        RejectionFeedback = null;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    internal UnitResult<IReadOnlyList<Error>> Reject(
        Employee employee,
        RejectionFeedback? feedback,
        DateTimeOffset decidedAt)
    {
        var canComplete = CanComplete(employee);
        if (canComplete.IsFailure)
        {
            return canComplete;
        }

        Status = RequestReviewStatus.Rejected;
        CompletedByEmployeeId = employee.Id;
        CompletedAt = decidedAt;
        RejectionFeedback = feedback;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    private UnitResult<IReadOnlyList<Error>> CanComplete(Employee employee)
    {
        if (employee is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
        }

        if (Status != RequestReviewStatus.Started)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyStartedReviewCanBeCompleted]);
        }

        if (StartedByEmployeeId != employee.Id)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestReviewStartedByAnotherEmployee]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
```

## 5.6 AgreementProposalExchange

### Purpose

`AgreementProposalExchange` owns post-approval agreement proposal version exchange.

It is a separate aggregate from `Request`.

It must not mutate `Request` directly.

`Request` must not hold navigation to `AgreementProposalExchange`.

Application service orchestrates cross-aggregate effects.

### State owned

```text
Id
RequestId
Status
ActiveProposalVersion
ProposalVersions
FinalRefusedByEmployeeId?
FinalRefusedAt?
FinalRefusalReason?
CreatedAt
```

### Status

```csharp
public enum AgreementExchangeStatus
{
    AwaitingClientConfirmation = 1,
    AwaitingEmployeeResponse = 2,
    Accepted = 3,
    FinallyRefused = 4
}
```

### Final refusal

Final refusal is not a separate entity/class.

It is exchange state + optional value data:

```text
Status = FinallyRefused
FinalRefusedByEmployeeId
FinalRefusedAt
FinalRefusalReason?
```

`FinalRefusalReason` may be optional.

Who/when are required when status is `FinallyRefused`.

### Code sketch

```csharp
public sealed class AgreementProposalExchange : L1Entity
{
    public long RequestId { get; private set; }

    public AgreementExchangeStatus Status { get; private set; }

    public AgreementProposalVersion ActiveProposalVersion { get; private set; }

    private readonly List<AgreementProposal> _proposals = new();

    public IReadOnlyCollection<AgreementProposal> Proposals => _proposals.AsReadOnly();

    public long? FinalRefusedByEmployeeId { get; private set; }

    public DateTimeOffset? FinalRefusedAt { get; private set; }

    public FinalRefusalReason? FinalRefusalReason { get; private set; }

    private AgreementProposalExchange()
    {
    }

    public static Result<AgreementProposalExchange, IReadOnlyList<Error>> StartByEmployee(
        ConnectionRequest approvedRequest,
        AgreementDocumentRef document,
        ProposalComment? comment,
        Employee employee,
        DateTimeOffset startedAt)
    {
        var errors = new List<Error>();

        if (approvedRequest is null)
        {
            errors.Add(Errors.L1Domain.RequestIsRequired);
        }
        else if (approvedRequest.Status != RequestStatus.Approved)
        {
            errors.Add(Errors.L1Domain.AgreementExchangeRequiresApprovedRequest);
        }

        if (document is null)
        {
            errors.Add(Errors.L1Domain.AgreementDocumentIsRequired);
        }

        if (employee is null)
        {
            errors.Add(Errors.L1Domain.EmployeeIsRequired);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<AgreementProposalExchange, IReadOnlyList<Error>>(errors);
        }

        var canSend = employee!.EnsureCanStartAgreementExchange();
        if (canSend.IsFailure)
        {
            return Result.Failure<AgreementProposalExchange, IReadOnlyList<Error>>(canSend.Error);
        }

        var firstVersion = AgreementProposalVersion.First;

        var exchange = new AgreementProposalExchange
        {
            RequestId = approvedRequest!.Id,
            Status = AgreementExchangeStatus.AwaitingClientConfirmation,
            ActiveProposalVersion = firstVersion
        };

        var proposal = AgreementProposal.EmployeeProposal(
            firstVersion,
            document!,
            comment,
            employee!,
            startedAt);

        exchange._proposals.Add(proposal);

        return Result.Success<AgreementProposalExchange, IReadOnlyList<Error>>(exchange);
    }

    public UnitResult<IReadOnlyList<Error>> ClientAcceptActiveProposal(
        ClientAccount client,
        DateTimeOffset acceptedAt)
    {
        if (client is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ClientAccountIsRequired]);
        }

        if (Status != AgreementExchangeStatus.AwaitingClientConfirmation)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyAwaitingClientConfirmationCanBeAccepted]);
        }

        var activeProposal = GetActiveProposal();

        var accept = activeProposal.MarkAccepted();
        if (accept.IsFailure)
        {
            return accept;
        }

        Status = AgreementExchangeStatus.Accepted;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> ClientSendOwnVersion(
        AgreementDocumentRef document,
        ProposalComment? comment,
        ClientAccount client,
        DateTimeOffset createdAt)
    {
        if (Status != AgreementExchangeStatus.AwaitingClientConfirmation)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ClientVersionCanBeSentOnlyWhenAwaitingClientConfirmation]);
        }

        if (document is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AgreementDocumentIsRequired]);
        }

        if (client is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ClientAccountIsRequired]);
        }

        var activeProposal = GetActiveProposal();

        if (activeProposal.Author.Sender != AgreementProposalSender.Employee)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ClientCanRespondOnlyToEmployeeProposal]);
        }

        var supersede = activeProposal.MarkSupersededByCounterProposal();
        if (supersede.IsFailure)
        {
            return supersede;
        }

        var nextVersion = GetNextVersion();

        var clientProposal = AgreementProposal.ClientProposal(
            nextVersion,
            document,
            comment,
            client,
            createdAt);

        _proposals.Add(clientProposal);

        ActiveProposalVersion = nextVersion;
        Status = AgreementExchangeStatus.AwaitingEmployeeResponse;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> EmployeeSendNewVersion(
        AgreementDocumentRef document,
        ProposalComment? comment,
        Employee employee,
        DateTimeOffset createdAt)
    {
        if (Status != AgreementExchangeStatus.AwaitingEmployeeResponse)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeCanRespondOnlyWhenAwaitingEmployeeResponse]);
        }

        if (document is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AgreementDocumentIsRequired]);
        }

        if (employee is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
        }

        var canSend = employee.EnsureCanSendAgreementProposal();
        if (canSend.IsFailure)
        {
            return canSend;
        }

        var activeProposal = GetActiveProposal();

        if (activeProposal.Author.Sender != AgreementProposalSender.Client)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeCanSupersedeOnlyClientProposal]);
        }

        var supersede = activeProposal.MarkSupersededByCounterProposal();
        if (supersede.IsFailure)
        {
            return supersede;
        }

        var nextVersion = GetNextVersion();

        var employeeProposal = AgreementProposal.EmployeeProposal(
            nextVersion,
            document,
            comment,
            employee,
            createdAt);

        _proposals.Add(employeeProposal);

        ActiveProposalVersion = nextVersion;
        Status = AgreementExchangeStatus.AwaitingClientConfirmation;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> FinalRefuseProposal(
        Employee employee,
        FinalRefusalReason? reason,
        DateTimeOffset refusedAt)
    {
        if (employee is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
        }

        var canRefuse = employee.EnsureCanFinalRefuseAgreement();
        if (canRefuse.IsFailure)
        {
            return canRefuse;
        }

        if (Status == AgreementExchangeStatus.Accepted)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AcceptedAgreementExchangeCannotBeRefused]);
        }

        if (Status == AgreementExchangeStatus.FinallyRefused)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AgreementExchangeAlreadyFinallyRefused]);
        }

        if (Status is not AgreementExchangeStatus.AwaitingClientConfirmation
            and not AgreementExchangeStatus.AwaitingEmployeeResponse)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AgreementExchangeCannotBeFinallyRefusedNow]);
        }

        Status = AgreementExchangeStatus.FinallyRefused;
        FinalRefusedByEmployeeId = employee.Id;
        FinalRefusedAt = refusedAt;
        FinalRefusalReason = reason;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    private AgreementProposalVersion GetNextVersion()
    {
        if (_proposals.Count == 0)
        {
            return AgreementProposalVersion.First;
        }

        var maxVersion = _proposals.Max(x => x.Version.Value);

        return new AgreementProposalVersion(maxVersion + 1);
    }

    private AgreementProposal GetActiveProposal()
    {
        return _proposals.Single(x => x.Version == ActiveProposalVersion);
    }
}
```

## 5.7 AgreementProposal

### Purpose

`AgreementProposal` represents one proposal version inside `AgreementProposalExchange`.

It is not an aggregate.

It is owned by `AgreementProposalExchange`.

It has DB technical identity, but its domain identity inside the exchange is `AgreementProposalVersion`.

### State owned

```text
Id
AgreementProposalExchangeId
Version
Author
State
Document
Comment?
CreatedAt
```

### Author

Use `Sender + SenderId`.

Do not use `AggregateId`.

Do not use `EmployeeRef`.

```csharp
public enum AgreementProposalSender
{
    Employee = 1,
    Client = 2
}
```

```csharp
public sealed class AgreementProposalAuthor : ValueObject
{
    public AgreementProposalSender Sender { get; private set; }

    public long SenderId { get; private set; }

    private AgreementProposalAuthor(
        AgreementProposalSender sender,
        long senderId)
    {
        if (senderId <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(senderId),
                "Proposal sender id must be positive.");
        }

        Sender = sender;
        SenderId = senderId;
    }

    private AgreementProposalAuthor()
    {
    }

    public static AgreementProposalAuthor Employee(Employee employee)
    {
        if (employee is null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        return new AgreementProposalAuthor(
            AgreementProposalSender.Employee,
            employee.Id);
    }

    public static AgreementProposalAuthor Client(ClientAccount client)
    {
        if (client is null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        return new AgreementProposalAuthor(
            AgreementProposalSender.Client,
            client.Id);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Sender;
        yield return SenderId;
    }
}
```

### Proposal state

```csharp
public enum AgreementProposalState
{
    AwaitingClientConfirmation = 1,
    SentByClient = 2,
    Accepted = 3,
    SupersededByCounterProposal = 4
}
```

### Code sketch

```csharp
public sealed class AgreementProposal
{
    public long Id { get; private set; }

    public long AgreementProposalExchangeId { get; private set; }

    public AgreementProposalVersion Version { get; private set; }

    public AgreementProposalAuthor Author { get; private set; }

    public AgreementProposalState State { get; private set; }

    public AgreementDocumentRef Document { get; private set; }

    public ProposalComment? Comment { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    private AgreementProposal()
    {
        Author = null!;
        Document = null!;
    }

    internal static AgreementProposal EmployeeProposal(
        AgreementProposalVersion version,
        AgreementDocumentRef document,
        ProposalComment? comment,
        Employee employee,
        DateTimeOffset createdAt)
    {
        if (document is null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        return new AgreementProposal
        {
            Version = version,
            Author = AgreementProposalAuthor.Employee(employee),
            State = AgreementProposalState.AwaitingClientConfirmation,
            Document = document,
            Comment = comment,
            CreatedAt = createdAt
        };
    }

    internal static AgreementProposal ClientProposal(
        AgreementProposalVersion version,
        AgreementDocumentRef document,
        ProposalComment? comment,
        ClientAccount client,
        DateTimeOffset createdAt)
    {
        if (document is null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        return new AgreementProposal
        {
            Version = version,
            Author = AgreementProposalAuthor.Client(client),
            State = AgreementProposalState.SentByClient,
            Document = document,
            Comment = comment,
            CreatedAt = createdAt
        };
    }

    internal UnitResult<IReadOnlyList<Error>> MarkAccepted()
    {
        if (Author.Sender != AgreementProposalSender.Employee)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyEmployeeProposalCanBeAccepted]);
        }

        if (State != AgreementProposalState.AwaitingClientConfirmation)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyAwaitingClientConfirmationProposalCanBeAccepted]);
        }

        State = AgreementProposalState.Accepted;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    internal UnitResult<IReadOnlyList<Error>> MarkSupersededByCounterProposal()
    {
        if (State == AgreementProposalState.Accepted)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AcceptedProposalCannotBeSuperseded]);
        }

        if (State == AgreementProposalState.SupersededByCounterProposal)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ProposalAlreadySuperseded]);
        }

        State = AgreementProposalState.SupersededByCounterProposal;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
```

## 5.8 AgreementProposalVersion

### Purpose

`AgreementProposalVersion` is a value object representing local version number inside one `AgreementProposalExchange`.

It is not:

```text
DB Id
public agreement number
document number
global sequence
cross-exchange identifier
```

It is:

```text
local version number inside one exchange
generated only by AgreementProposalExchange
stored on AgreementProposal
used by AgreementProposalExchange.ActiveProposalVersion
stable ordering of proposal versions
```

### Rules

```text
- first proposal version is 1;
- next proposal version is max existing version + 1;
- versions are unique inside one exchange;
- versions are never reused;
- versions are never edited;
- external API/client cannot choose version;
- final refusal does not create a new version;
- accepted exchange keeps accepted ActiveProposalVersion;
- finally refused exchange keeps last ActiveProposalVersion.
```

### Code sketch

```csharp
public readonly record struct AgreementProposalVersion
    : IComparable<AgreementProposalVersion>
{
    public int Value { get; }

    public AgreementProposalVersion(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                "Agreement proposal version must be positive.");
        }

        Value = value;
    }

    public static AgreementProposalVersion First => new(1);

    public AgreementProposalVersion Next()
    {
        return new AgreementProposalVersion(Value + 1);
    }

    public int CompareTo(AgreementProposalVersion other)
    {
        return Value.CompareTo(other.Value);
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}
```

## 5.9 AgreementDocumentRef

### Purpose

`AgreementDocumentRef` is a domain value object reference to an already accepted agreement/proposal document file.

It is not the file bytes.

It is not storage adapter.

It is not a file upload service.

### State owned

```text
StorageKey / FileId
OriginalFileName
ContentType
SizeBytes
```

### Code sketch

```csharp
public sealed class AgreementDocumentRef : ValueObject
{
    public string StorageKey { get; private set; }

    public string OriginalFileName { get; private set; }

    public string ContentType { get; private set; }

    public long SizeBytes { get; private set; }

    private AgreementDocumentRef(
        string storageKey,
        string originalFileName,
        string contentType,
        long sizeBytes)
    {
        StorageKey = storageKey;
        OriginalFileName = originalFileName;
        ContentType = contentType;
        SizeBytes = sizeBytes;
    }

    private AgreementDocumentRef()
    {
        StorageKey = null!;
        OriginalFileName = null!;
        ContentType = null!;
    }

    public static Result<AgreementDocumentRef, IReadOnlyList<Error>> Create(
        string storageKey,
        string originalFileName,
        string contentType,
        long sizeBytes)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(storageKey))
        {
            errors.Add(Errors.L1Domain.AgreementDocumentStorageKeyIsRequired);
        }

        if (string.IsNullOrWhiteSpace(originalFileName))
        {
            errors.Add(Errors.L1Domain.AgreementDocumentFileNameIsRequired);
        }

        if (string.IsNullOrWhiteSpace(contentType))
        {
            errors.Add(Errors.L1Domain.AgreementDocumentContentTypeIsRequired);
        }

        if (sizeBytes <= 0)
        {
            errors.Add(Errors.L1Domain.AgreementDocumentSizeIsRequired);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<AgreementDocumentRef, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<AgreementDocumentRef, IReadOnlyList<Error>>(
            new AgreementDocumentRef(
                storageKey.Trim(),
                originalFileName.Trim(),
                contentType.Trim(),
                sizeBytes));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return StorageKey;
        yield return OriginalFileName;
        yield return ContentType;
        yield return SizeBytes;
    }
}
```

## 5.10 ProposalComment

### Purpose

`ProposalComment` is optional explanatory text attached to one proposal version.

It is not the agreement document.

It is not request details.

It is sender comment/context.

### Optionality

```text
AgreementProposal.Comment may be null.

If non-null, it must be a valid ProposalComment.
Empty comment text means no comment.
```

### Code sketch

```csharp
public sealed class ProposalComment : ValueObject
{
    public const int MaxLength = 2000;

    public string Value { get; private set; }

    private ProposalComment(string value)
    {
        Value = value;
    }

    private ProposalComment()
    {
        Value = null!;
    }

    public static Result<ProposalComment, IReadOnlyList<Error>> Create(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(Errors.L1Domain.ProposalCommentIsRequired);
        }
        else if (value.Length > MaxLength)
        {
            errors.Add(Errors.L1Domain.ProposalCommentIsTooLong);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<ProposalComment, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<ProposalComment, IReadOnlyList<Error>>(
            new ProposalComment(value.Trim()));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}
```

Application helper:

```csharp
ProposalComment? comment = null;

if (!string.IsNullOrWhiteSpace(commentText))
{
    var commentResult = ProposalComment.Create(commentText);
    if (commentResult.IsFailure)
    {
        return Result.Failure(commentResult.Error);
    }

    comment = commentResult.Value;
}
```

## 5.11 FinalRefusalReason

### Purpose

`FinalRefusalReason` is optional explanation for employee final refusal of agreement exchange.

It is a value object, not an entity.

No separate `AgreementFinalRefusal` class is needed.

### Code sketch

```csharp
public sealed class FinalRefusalReason : ValueObject
{
    public const int MaxLength = 2000;

    public string Value { get; private set; }

    private FinalRefusalReason(string value)
    {
        Value = value;
    }

    private FinalRefusalReason()
    {
        Value = null!;
    }

    public static Result<FinalRefusalReason, IReadOnlyList<Error>> Create(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(Errors.L1Domain.FinalRefusalReasonIsRequired);
        }
        else if (value.Length > MaxLength)
        {
            errors.Add(Errors.L1Domain.FinalRefusalReasonIsTooLong);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<FinalRefusalReason, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<FinalRefusalReason, IReadOnlyList<Error>>(
            new FinalRefusalReason(value.Trim()));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}
```

## 5.12 VerificationResult

Deferred/future supporting concept.

```text
Standalone ApplicantParty save/edit does not start verification.
Verification may be started only from request context.
```

## 5.13 AnonymousSubmission

Deferred/open concept for anonymous request/contact submission.

# 6. Class / Aggregate State Machines

## 6.1 Employee

```text
Employee Active
  -> can start review
  -> can approve/reject started review
  -> can start agreement exchange
  -> can send proposal versions
  -> can final-refuse agreement exchange
```

Future:

```text
PendingActivation -> Active
Active -> Suspended
Active -> Deactivated
```

## 6.2 ApplicantParty

```text
Create IndividualApplicantParty
  -> Unverified

Unverified
  -> MarkVerified
  -> Verified
```

Current/default marker:

```text
New ApplicantParty
  -> non-current/default

First ApplicantParty for account + type
  -> application service calls MarkAsCurrentDefaultTemplate
  -> current/default

Additional same-type ApplicantParty
  -> remains non-current/default

Explicit make current/default
  -> selected ApplicantParty becomes current/default
  -> previous same-type current/default becomes non-current/default
```

## 6.3 Request review

```text
Request InReview, no Review
  -> StartReview(employee)
  -> Request InReview + Review Started

Request InReview + Review Started by employee A
  -> employee A ApproveReview
  -> Request Approved + Review Approved

Request InReview + Review Started by employee A
  -> employee A RejectReview
  -> Request Rejected + Review Rejected
```

Blocked:

```text
Request InReview + Review Started by employee A
  -> employee B StartReview = blocked

Request InReview + Review Started by employee A
  -> employee B ApproveReview/RejectReview = blocked

Request InReview, no Review
  -> ApproveReview/RejectReview = blocked

Request Approved/Rejected
  -> StartReview = blocked
```

## 6.4 AgreementProposalExchange

```text
No exchange
  -> StartByEmployee
  -> AwaitingClientConfirmation

AwaitingClientConfirmation
  -> ClientAcceptActiveProposal
  -> Accepted

AwaitingClientConfirmation
  -> ClientSendOwnVersion
  -> AwaitingEmployeeResponse

AwaitingEmployeeResponse
  -> EmployeeSendNewVersion
  -> AwaitingClientConfirmation
```

Final refusal:

```text
AwaitingClientConfirmation
  -> Employee FinalRefuseProposal
  -> FinallyRefused

AwaitingEmployeeResponse
  -> Employee FinalRefuseProposal
  -> FinallyRefused
```

Application service:

```text
Exchange FinallyRefused
  + Request Approved
  -> request.MarkAgreementExchangeFailed(exchange.Id)
  -> Request AgreementExchangeFailed
```

# 7. Impossible States Covered By Current Model

| Impossible state | Covered by | Status |
|---|---|---|
| Approved request without completed approved Review | `ConnectionRequest.ApproveReview` calls `RequestReview.Approve` before status change | Covered |
| Rejected request without completed rejected Review | `ConnectionRequest.RejectReview` calls `RequestReview.Reject` before status change | Covered |
| Approve/reject without started review | Request requires `_review` before approve/reject | Covered |
| Review completed by employee who did not start it | `RequestReview.CanComplete` checks `StartedByEmployeeId` | Covered |
| Two employees reviewing same request at same time | `StartReview` blocks active Started review | Covered |
| Review as standalone aggregate | Review is owned child inside Request | Covered by placement |
| Proposal without author | `AgreementProposalAuthor` required | Covered |
| Proposal with ambiguous author id | `AgreementProposalAuthor` uses `Sender + SenderId` | Covered |
| Proposal without document reference | `AgreementDocumentRef` required for proposal creation | Covered |
| Proposal with invalid local version | `AgreementProposalVersion` positive invariant | Covered |
| Two active proposal versions | `ActiveProposalVersion` points to one child; state transitions supersede previous proposal | Covered conceptually |
| Client-started exchange without employee proposal | `StartByEmployee` is only exchange start | Covered |
| Superseded proposal treated as rejection | Separate `SupersededByCounterProposal` vs `FinallyRefused` | Covered |
| Final refusal as proposal version | Final refusal changes exchange status only; no new version | Covered |
| Exchange directly mutates Request | Application service orchestrates; aggregate methods stay separate | Covered by placement |
| Request AgreementExchangeFailed from non-Approved | `MarkAgreementExchangeFailed` checks RequestStatus.Approved | Covered |

# 8. Value Objects / Value Integrity Coverage

## Account / employee

```text
Email
PasswordHash
FullName
AccountActivationState
```

## ApplicantParty

```text
ApplicantPartyType
ApplicantPartyVerificationStatus
FullName
Email
PhoneNumber
Address [future for richer applicant data]
```

## Request / review

```text
RequestStatus
RequestReviewStatus
RejectionFeedback
Address
```

## Agreement proposal

```text
AgreementProposalVersion
AgreementProposalAuthor
AgreementDocumentRef
ProposalComment
FinalRefusalReason
AgreementExchangeStatus
AgreementProposalState
AgreementProposalSender
```

Value-integrity notes:

```text
AgreementProposalVersion:
  positive local per-exchange version number.

AgreementProposalAuthor:
  exactly one sender type and positive SenderId.

AgreementDocumentRef:
  document reference metadata, not physical blob.

ProposalComment:
  optional; if present, non-empty and max-length constrained.

FinalRefusalReason:
  optional; if present, non-empty and max-length constrained.
```

# 9. Use-Case Coordination Decisions

## 9.1 Employee authorization / loading

Application/auth layer proves identity and loads `Employee`.

Domain methods receive `Employee`.

```text
Do not pass EmployeeRef.
Do not pass raw employee id when employee capability matters.
```

## 9.2 Request review

`ConnectionRequest` owns review behavior.

Application service owns:

```text
- loading request;
- loading employee;
- calling request.StartReview / ApproveReview / RejectReview;
- coordinating ApplicantParty verification after approval;
- saving transaction.
```

## 9.3 Dashboard/details review started marker

Read model shows:

```text
request is available for review
request review is started by current employee
request review is started by another employee
```

Domain owns the state that enables the read model:

```text
Request.Review.Status
Request.Review.StartedByEmployeeId
```

## 9.4 Agreement exchange final refusal

Exchange and Request are separate aggregates.

Application service orchestration:

```csharp
exchange.FinalRefuseProposal(employee, reason, clock.UtcNow);
request.MarkAgreementExchangeFailed(exchange.Id, clock.UtcNow);
await unitOfWork.SaveChangesAsync(cancellationToken);
```

Rules:

```text
Exchange decides if exchange can be finally refused.
Request decides if it can move from Approved to AgreementExchangeFailed.
Application service does not write request.Status directly.
Exchange does not mutate Request directly.
Request does not navigate into Exchange.
```

## 9.5 File/blob storage

Application service:

```text
- accepts/uploads file;
- receives storage key / file metadata;
- creates AgreementDocumentRef;
- passes AgreementDocumentRef into AgreementProposalExchange method.
```

Domain:

```text
- stores AgreementDocumentRef;
- checks proposal has document ref;
- does not upload/read/delete bytes.
```

# 10. Coverage Against Scenario Behavior Baseline

| Item ID | Status | Draft answer / placement | Covered by | Gap / next action |
|---|---|---|---|---|
| EMP-AUTH-001 | L2 target | Employee is domain actor; auth loads Employee | Employee + app/auth layer | Implement employee auth/account slice |
| EMP-REVIEW-001 | Covered | Employee starts request review | `ConnectionRequest.StartReview` | API/application slice |
| EMP-REVIEW-002 | Covered | Dashboard/details can show review started by another employee | `RequestReview.Status`, `StartedByEmployeeId` | Read model slice |
| REVIEW-CMD-START-001 | Covered | Review must start before approve/reject | `StartReview` | Domain tests |
| REVIEW-CMD-APPROVE-001 | Covered | Started review can approve request | `ApproveReview`, `RequestReview.Approve` | Domain tests |
| REVIEW-CMD-REJECT-001 | Covered | Started review can reject request | `RejectReview`, `RequestReview.Reject` | Domain tests |
| REVIEW-NW-001 | Covered | Failed review commands do not change request/review state | prechecks before mutation | Domain tests |
| AGR-CMD-EMP-SEND-001 | Covered as L2 target | Employee starts exchange from Approved request with first proposal | `AgreementProposalExchange.StartByEmployee` | Domain/application slice |
| AGR-CMD-CLIENT-ACCEPT-001 | Covered as L2 target | Client accepts active employee proposal | `ClientAcceptActiveProposal` | Domain/application slice |
| AGR-CMD-CLIENT-SEND-001 | Covered as L2 target | Client sends own proposal version | `ClientSendOwnVersion` | Domain/application slice |
| AGR-CMD-EMP-NEW-001 | Covered as L2 target | Employee sends new version after client proposal | `EmployeeSendNewVersion` | Domain/application slice |
| AGR-CMD-EMP-FINAL-REFUSE-001 | Covered as L2 target | Employee final-refuses agreement exchange | `FinalRefuseProposal` + request orchestration | Domain/application slice |
| AGR-LC-001 | Covered | No exchange -> AwaitingClientConfirmation | `StartByEmployee` | - |
| AGR-LC-002 | Covered | AwaitingClientConfirmation -> Accepted | `ClientAcceptActiveProposal` | - |
| AGR-LC-003 | Covered | AwaitingClientConfirmation -> AwaitingEmployeeResponse | `ClientSendOwnVersion` | - |
| AGR-LC-006 | Covered | Client own version only once per active employee proposal | exchange status blocks duplicate client response | - |
| AGR-LC-007 | Covered | Employee proposal supersedes client version | `EmployeeSendNewVersion` | - |
| AGR-LC-FINAL-001 | Covered | Awaiting* -> FinallyRefused | `FinalRefuseProposal` | - |
| AGR-IBS-001 | Covered | Proposal has author | `AgreementProposalAuthor` | - |
| AGR-IBS-002 | Covered | Proposal has document ref | `AgreementDocumentRef` | - |
| AGR-IBS-003 | Covered | Client cannot start exchange | no public client start method | - |
| AGR-VI-001 | Covered | Document reference integrity | `AgreementDocumentRef` | exact file policy later |
| AGR-VI-002 | Covered | Proposal comment integrity | `ProposalComment` | - |
| REQ-AGR-FAIL-001 | Covered | Failed exchange reflected on request | `Request.MarkAgreementExchangeFailed` | exact status name decision |
| REQ-UCQ-001 | Covered | Request references ApplicantParty and is not relinked by ApplicantParty default changes | `ApplicantPartyId` + app orchestration | - |

Draft-local IDs such as `EMP-*`, `REVIEW-*`, `AGR-LC-FINAL-*`, and `REQ-AGR-FAIL-*` should be promoted to stable planning IDs if/when the baseline register is updated.

# 11. Cross-Layer Placement Notes

## Employee auth

```text
Domain:
Employee aggregate and capability methods.

Application/auth:
current user identity;
employee authorization policy;
loading Employee;
passing Employee into domain methods.
```

## Review dashboard/details

```text
Domain:
Request owns Review state.

Read/query:
dashboard and details show review started state.

API/UI:
block start/approve/reject actions when read model says another employee started review,
but server/domain remains source of truth.
```

## Agreement proposal document storage

```text
Domain:
AgreementDocumentRef.

Infrastructure:
blob/file storage.

Application:
upload/accept file, create AgreementDocumentRef, call exchange method.
```

## Final refusal orchestration

```text
Exchange:
FinalRefuseProposal.

Request:
MarkAgreementExchangeFailed.

Application:
orchestrates both in one transaction.
```

# 12. Scenario Questions / Gaps For Next Draft

## Employee

```text
1. Should Employee inherit Account, or link to EmployeeAccount by AccountId?
2. Does Employee activation lifecycle equal ClientAccount activation lifecycle?
3. Do we need department/position/permissions in domain now?
4. Does employee assignment/claiming need to be modeled before review start?
5. Can another employee take over a started review after timeout/manual reassignment?
```

## Review

```text
1. Should review started by another employee expire after timeout?
2. Should completed review allow audit trail/history later?
3. Should approval support employee comment?
4. Should rejection feedback remain optional?
5. Should review have explicit StartedAt/CompletedAt from application clock only?
```

## Agreement proposal exchange

```text
1. Exact request status name after final refusal:
   AgreementExchangeFailed
   AgreementNotConcluded
   ProposalRejected

2. Should final refusal be allowed from both:
   AwaitingClientConfirmation
   AwaitingEmployeeResponse?

3. Should FinalRefusalReason be required by UI but optional in domain?

4. Should any authorized employee final-refuse,
   or only the employee who started exchange / active assigned employee?

5. Should request store AgreementExchangeFailureRef:
   ExchangeId + FailedAt,
   or only RequestStatus.AgreementExchangeFailed?

6. Should AgreementProposalNumber be introduced later as public number?
```

# 13. What Changed Since Previous Draft

Compared to Draft 01 / earlier Draft 02 notes:

```text
- Added L2 scope for Employee, RequestReview, AgreementProposalExchange and AgreementProposal.
- Chose Employee terminology, not Worker.
- Removed EmployeeRef from L2 target model.
- Methods receive Employee domain object when employee capability matters.
- References stored as scalar ids such as EmployeeId and SenderId.
- Added Employee aggregate/code sketch.
- Review is now owned child of Request, not aggregate.
- Added RequestReview with StartForRequest, Approve and Reject internal methods.
- Public review API moved to ConnectionRequest.
- ReviewDecisionRecord removed from L2 target direction.
- Added started-review dashboard/details scenario.
- Approve/reject now require started review.
- Another employee cannot start or complete review already started by someone else.
- Added RequestStatus.AgreementExchangeFailed target.
- Added AgreementProposalExchange final refusal.
- Final refusal is exchange state + nullable direct fields, not separate entity/class.
- FinalRefusalReason is optional value object.
- Added AgreementProposal as child entity.
- Added AgreementProposalAuthor with Sender + SenderId.
- Replaced AggregateId with SenderId.
- Replaced DocumentFileRef with AgreementDocumentRef.
- Added ProposalComment value object.
- Added AgreementProposalVersion as positive local per-exchange value object.
- Clarified final refusal does not create a new proposal version.
- Clarified ActiveProposalVersion remains on Accepted and FinallyRefused exchanges.
- Kept aggregate separation:
  exchange.FinalRefuseProposal(...)
  request.MarkAgreementExchangeFailed(...)
  application service orchestrates.
```

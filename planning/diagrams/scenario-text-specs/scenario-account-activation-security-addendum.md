# Scenario Account Activation / Security Addendum

Status: companion addendum to scenario text specifications  
Scope: account activation behavior, protected scenario access, scenario questions

## 1. Purpose

This addendum records account activation semantics without editing every protected scenario file.

Use it together with:

```text
SC-01-guest-registration.md
SC-02-login.md
SC-15-security-text-specification.md
scenario-server-domain-validation-addendum.md
```

## 2. Core Decision

Account has activation state/marker.

Current core decision:

```text
accepted registration creates Active account.
```

Protected client/employee functionality requires activated account.

Non-active account cannot execute protected business commands or access protected protected pages/data.

## 3. Protected Scenarios

The active-account requirement applies to protected scenarios such as:

```text
SC-04 Client Request Creation
SC-05 My Requests / Own Request Details
SC-06 Employee Request Dashboard
SC-07A Employee Request Details
SC-07B Employee Request Review
SC-10 Applicant Data
SC-11 Request Documents
SC-13A My Agreements
SC-13B Agreement Proposal Details / Response
SC-13C Employee Agreements
SC-13D Employee Agreement Proposal Create / Send Version
SC-14 Client Data Verification
```

SC-17 Anonymous Request is not automatically protected by account activation if it remains anonymous/future flow.

Password recovery and future activation completion flows may remain available to non-active account according to future UX decision.

## 4. Current Implementation Direction

Current implementation direction:

```text
Application service checks active account before protected use case.
Account/domain method such as EnsureActivated returns success/error.
```

This keeps activation enforcement centralized and prevents business aggregates from all depending directly on Account.

## 5. Future Implementation Direction

Future implementation direction:

```text
AccountActivated authorization policy / RequireActivatedAccount.
```

Potential optimization:

```text
additional claim such as account_activated = true
```

Reason:

```text
avoid database lookup on every protected request.
```

Open concern:

```text
claim staleness / refresh after activation state changes.
```

## 6. Scenario Questions

Q-ACTIVE-LOGIN-001:

```text
If user created account but did not activate it and then tries to log in, what should happen?
```

Options:

```text
- sign-in fails with activation-required message;
- limited session is issued and activation-required page is shown;
- sign-in succeeds but protected actions are blocked by AccountActivated policy;
- current core avoids this branch because accepted registration creates Active account.
```

Q-ACTIVE-CLAIM-001:

```text
If future AccountActivated policy uses account_activated claim, how is stale claim refreshed after activation state changes?
```

Q-ACTIVE-SCOPE-001:

```text
Does account activation apply identically to client accounts and employee accounts, or only to client accounts in core?
```

## 7. How Domain Drafts Should Use This Addendum

Domain drafts should cover the account activation items from:

```text
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

Expected draft answer direction:

```text
- Account may expose EnsureActivated or equivalent method.
- Application service uses this method before protected use cases.
- Future AccountActivated policy can replace/centralize this check at auth layer.
- Request / ApplicantData / AgreementProposal should not duplicate activation checks internally unless the draft explicitly justifies that decision.
```

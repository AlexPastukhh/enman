# SC-15 — Security Text Specification

## Status
Text specification only.

Auth is not a separate scenario by default. It appears as preconditions, invariants and access rules in concrete scenarios.

## Scope

Security topics:

```text
- login throttling;
- rate limiting;
- account lock;
- account activation;
- no account enumeration;
- recovery token expiration/single-use;
- password recovery abuse protection;
- client own-resource access;
- employee permissions;
- protected scenario access.
```

## Account Activation

### Core rule

Protected client/employee functionality requires activated account.

In current core:

```text
accepted registration creates account with activation state Active.
```

Future activation flow may introduce:

```text
PendingActivation / NonActive -> Active
```

or equivalent activation state.

### Protected functionality

Non-active account cannot:

```text
- create request;
- save applicant data;
- view own requests/details;
- attach request documents;
- view/respond to agreement proposals;
- access employee dashboard;
- review requests;
- create/send agreement proposal versions;
- perform other protected client/employee actions.
```

Non-active account may be allowed only activation/auth-related flows, depending on future UX:

```text
- complete activation;
- request activation resend;
- password recovery if allowed;
- log out.
```

### Server-side enforcement

Account activation is not a UI-only rule.

Protected scenarios must be guarded server-side.

Current implementation direction:

```text
Application service checks active account before protected use case.
Account/domain method such as EnsureActivated returns success/error.
```

Future implementation direction:

```text
Use authorization policy such as AccountActivatedPolicy / RequireActivatedAccount.
The policy may be implemented with an additional claim, e.g. account_activated = true, to avoid database lookup on every protected request.
```

If claim-based activation is used, the system must define how the claim is issued/refreshed after activation state changes.

## Access Invariants

```text
- client resource access is scoped to own resources;
- employee actions require employee permission;
- account activation is required for protected functionality;
- password recovery must not reveal account existence;
- security policies are enforced server-side even if hidden UI actions are bypassed.
```

## Scenario Questions

Q: If user created account but did not activate it and then tries to log in, what should happen?
- sign-in fails with activation-required message;
- limited session is issued and activation-required page is shown;
- sign-in succeeds but protected actions are blocked by AccountActivated policy;
- current core avoids the branch because accepted registration creates Active account.

Q: Should activation state be checked by database lookup on each protected request, or by authorization claim?
- current planning direction: application service + `Account.EnsureActivated`;
- future direction: auth policy with activation claim.

Q: If account activation can change after session/claim issuance, how is stale activation claim invalidated or refreshed?

Q: Does account activation apply identically to client accounts and employee accounts, or only to client accounts in core?

# SC-02 — Login

## Status
Corrected scenario specification draft.

## Purpose
Guest signs in with credentials.

Protected client/employee functionality requires activated account.

## Actor / Screen
Actor: Guest  
Screen: Login screen  
Goal: Sign in

## Entry Points
Entry A: Guest opens login screen.  
Entry B: Guest is redirected to login after trying to access a protected page.

## Preconditions
- Guest is not signed in.
- Login screen is reachable.

## DATA
Login credentials DATA `SC-02-DATA-01`

Input DATA:
```text
- email;
- password.
```

## Main Flow
1. Guest opens login screen.
2. Guest enters credentials.
3. Client-side validation runs automatically.
4. Guest corrects credentials if client-side validation fails.
5. Guest submits sign-in.
6. System checks whether credentials are valid.
7. System checks whether account activation allows protected functionality.
8. If credentials are valid and account is activated, session / app access is issued.
9. Guest is taken to intended app page or default signed-in page.

## Branches

### Credentials valid and account activated
-> session / app access is issued  
-> app page opens

### Credentials valid but account not activated
-> protected functionality is not available  
-> account activation requirement is visible or sign-in is rejected according to future activation UX decision  
-> no protected client/employee scenario can be executed

### Credentials invalid
-> sign-in error is visible  
-> no session / app access is issued  
-> guest remains unauthenticated or without protected access

### Forgot password selected
-> opens Password Recovery Request — SC-03A  
-> Item ref: `SC-02-EXTND-01`

## Invariants
- Session / protected app access is issued only for valid credentials and activated account.
- Non-active account cannot access protected client/employee functionality.
- Client-side navigation restrictions are not trusted; server-side activation check is required for protected scenarios.

Attach to:
```text
credentials valid? / transition into session or protected app access
```

## Server-side / Domain Validation
- Credentials must be validated server-side.
- Account activation must be checked server-side before protected functionality is allowed.
- Current implementation direction: application service guard calls account/domain method such as `EnsureActivated`.
- Future implementation direction: account activation may be enforced by authorization policy, possibly with an `account_activated` claim to avoid database lookup on every protected request.

## Diagram Notes
Invalid credentials are not `[ALT]`.

Non-active account behavior is a scenario question until activation UX is selected.

## Open Questions
Q: If user created account but did not activate it and then tries to log in, what should happen?
- sign-in fails with activation-required message;
- limited session is issued and user is taken to activation-required page;
- sign-in succeeds, but all protected actions are blocked by AccountActivated policy;
- current core avoids this branch because registration creates Active account.

Q: If future account activation is claim-based, when and how is `account_activated` claim refreshed after activation state changes?

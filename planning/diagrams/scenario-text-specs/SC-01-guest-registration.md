# SC-01 — Guest Registration

## Status
Corrected scenario specification draft.  
Doc version: v0.1.0

## Purpose
Guest creates a new account.

In current core, accepted registration creates an activated account so the user can access protected functionality.

Future activation flows may introduce non-active account states, but they are not core unless explicitly introduced.

## Actor / Screen
Actor: Guest  
Screen: Registration screen  
Goal: Register account

## Entry Points
Entry A: Guest opens registration screen.

## Preconditions
- Guest is not signed in.
- Registration screen is reachable.

## DATA
Registration input DATA `SC-01-DATA-01`

Core Input DATA:
```text
- email;
- password;
- password confirmation.
```

Extension / Future DATA:
```text
[EXT][VAR:EXPAND]
- contact data, exact fields not core;
- applicant-related data, if future registration collects part of applicant data;
- profile data, if future registration expands into profile setup;
- activation confirmation data if future activation flow is introduced.
```

## Main Flow
1. Guest opens registration screen.
2. Guest enters registration data.
3. Client-side validation runs automatically.
4. Guest corrects registration data if client-side validation fails.
5. Guest submits registration.
6. System checks whether registration data is accepted.
7. If accepted, account is created.
8. In current core, created account activation marker is set to Active.
9. Registration success state is visible.

## Branches

### Client-side validation errors
-> validation errors are visible  
-> guest corrects registration data  
-> back to entering registration data

### Registration data accepted
-> account is created  
-> account activation marker is Active in current core  
-> registration success state is visible

### Registration data rejected
-> validation/business errors are visible  
-> guest corrects registration data  
-> no account is created

### Future activation required
[VAR:EXPAND]

-> account may be created as non-active / pending activation  
-> protected functionality remains unavailable until activation is completed  
-> activation UX and delivery channel are future decisions

## Invariants
- Invalid registration data does not create an account.
- In current core, accepted registration creates an activated account.
- Non-active account cannot use protected client/employee functionality.
- Future activation flow must not allow protected functionality before account activation.

Attach to:
```text
registration data accepted? / account created transition
```

## Server-side / Domain Validation
- Registration input must be validated server-side.
- Account cannot be created if email/password/password confirmation are not accepted.
- Current core account creation sets activation state to Active.
- If future activation flow is introduced, account activation state must be checked server-side before protected scenarios.

## Outcomes
Guest sees form, can correct errors, and account is created only after accepted data.

In current core, created account is activated and can be used for protected app functionality.

## Open Questions
Q: After registration, is session issued automatically or must user sign in?

Q: If future activation flow is introduced, which activation path is used?
- email confirmation;
- admin/manual activation;
- activation link/code;
- other.

Q: If future activation flow is introduced and user tries to sign in before activation, should the system:
- issue a limited session and show activation-required page;
- reject sign-in with activation-required message;
- allow sign-in but block protected actions through AccountActivated policy?

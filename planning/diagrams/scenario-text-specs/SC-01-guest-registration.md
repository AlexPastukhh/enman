# SC-01 — Guest Registration

## Status
Corrected scenario specification draft.

## Purpose
Guest creates a new account.

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
- profile data, if future registration expands into profile setup.
```

## Main Flow
1. Guest opens registration screen.
2. Guest enters registration data.
3. Client-side validation runs automatically.
4. Guest corrects registration data if client-side validation fails.
5. Guest submits registration.
6. System checks whether registration data is accepted.
7. If accepted, account is created.
8. Registration success state is visible.

## Branches
### Client-side validation errors
-> validation errors are visible  
-> guest corrects registration data  
-> back to entering registration data

### Registration data accepted
-> account is created  
-> registration success state is visible

### Registration data rejected
-> validation/business errors are visible  
-> guest corrects registration data

## Invariants
Invalid registration data does not create an account. Attach to registration data accepted? / submit transition.

## Outcomes
Guest sees form, can correct errors, and account is created only after accepted data.

## Open Questions
Q: After registration, is session issued automatically or must user sign in?

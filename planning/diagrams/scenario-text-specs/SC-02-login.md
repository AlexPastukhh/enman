# SC-02 — Login

## Status
Corrected scenario specification draft.

## Purpose
Guest signs in with credentials.

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
7. If credentials are valid, session is issued.
8. Guest is taken to intended app page or default signed-in page.

## Branches
### Credentials valid
-> session is issued  
-> app page opens

### Credentials invalid
-> sign-in error is visible  
-> no session is issued  
-> guest remains unauthenticated

### Forgot password selected
-> opens Password Recovery Request — SC-03A  
-> Item ref: `SC-02-EXTND-01`

## Invariants
Session is issued only for valid credentials. Attach to credentials valid? / transition into session issued.

## Diagram Notes
Invalid credentials are not `[ALT]`.

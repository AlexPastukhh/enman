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

## DETAIL

Login credentials  
`SC-02-DETAIL-01`

- email;
- password;

## Main Flow

1. Guest opens login screen.
2. Guest enters credentials.
3. Client-side validation runs automatically.
4. Basic validation errors are visible before submit, if present.
5. Guest corrects credentials if client-side validation fails.
6. Guest submits sign-in.
7. System checks whether credentials are valid.
8. If credentials are valid, session is issued.
9. Guest is taken to the intended app page or default signed-in page.

## Branches

### Client-side validation errors

-> credential fields are incomplete or invalid format
-> validation errors are visible
-> guest corrects credentials
-> back to entering credentials

### Credentials valid

-> credentials valid
-> session is issued
-> app page opens

### Credentials invalid

-> credentials invalid
-> sign-in error is visible
-> no session is issued
-> guest remains unauthenticated

### Forgot password selected

-> guest selects forgot password
-> opens Password Recovery Request — SC-03A
-> Item ref: SC-02-EXTND-01

## Invariants

Session is issued only for valid credentials.

Attach to:

- credentials valid?;
- transition into session is issued;

## Step Postconditions

- Session is issued after valid credentials are accepted.

## Outcomes

- Guest can sign in with valid credentials.
- Guest sees sign-in error for invalid credentials.
- No session is issued for invalid credentials.
- Guest can open password recovery.

## Security Text Notes

Document in Markdown companion/security section, not as full diagram page:

- login throttling;
- rate limiting;
- account lock policy, if planned;

## Diagram Notes

- Invalid credentials are not [ALT].
- Forgot password is an off-page/subscenario branch using EXTND item ref.
- Do not put protected-resource invariant on this scenario unless the page access behavior is being modeled directly.

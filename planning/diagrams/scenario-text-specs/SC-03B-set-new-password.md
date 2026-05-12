# SC-03B — Set New Password

## Status

New scenario specification draft. This scenario was missing and should be added.

## Purpose

Guest sets a new password after valid recovery instructions are available.

## Actor / Screen

Actor: Guest  
Screen: Set New Password screen  
Goal: Set new password

## Entry Points

Entry A: Guest opens password reset experience from recovery instructions.

## Preconditions

- Recovery instructions are available.
- Recovery token or recovery context exists.

## DETAIL

New password details  
`SC-03B-DETAIL-01`

- new password;
- password confirmation;
- password policy hints if user-visible;

## Main Flow

1. Guest opens password reset experience from recovery instructions.
2. System checks whether recovery token/context is valid.
3. If valid, Set New Password page opens.
4. Guest enters new password.
5. Client-side password validation runs automatically.
6. Guest corrects password if validation fails.
7. Guest submits new password.
8. Password is updated.
9. Guest can return to Login — SC-02.

## Branches

### Recovery token valid

-> recovery token valid
-> Set New Password page opens
-> guest enters new password

### Recovery token invalid or expired

-> recovery token invalid/expired
-> expired/invalid recovery message visible
-> guest can request new recovery instructions
-> opens Password Recovery Request — SC-03A

### Password validation errors

-> new password does not satisfy policy or confirmation mismatch
-> validation errors are visible
-> guest corrects password
-> back to entering new password

### Password accepted

-> new password accepted
-> password is updated
-> guest can return to login

## Invariants

Password can be updated only with valid recovery token/context.

Attach to:

- recovery token valid?;
- submit new password transition;

## Step Postconditions

- Password is updated after valid recovery context and accepted password.

## Outcomes

- Guest can set new password using valid recovery instructions.
- Expired/invalid recovery instructions do not update password.
- Guest can return to login after password update.

## Security Text Notes

Document in Markdown companion/security section, not as full diagram page:

- recovery token expiration;
- token single-use;
- password policy;
- recovery abuse protection;
- rate limiting;

## Diagram Notes

- Do not describe email provider or token storage mechanics.
- Keep token validity as user-facing/system decision, not implementation.

# SC-03A — Password Recovery Request

## Status

Corrected scenario specification draft. New split from old SC-03.

## Purpose

Guest requests password recovery instructions without revealing whether an account exists.

## Actor / Screen

Actor: Guest  
Screen: Password recovery screen  
Goal: Request password recovery

## Entry Points

Entry A: Guest chooses forgot password from Login — SC-02.

Entry B: Guest opens password recovery screen directly.

## Preconditions

- Guest is not signed in.
- Password recovery screen is reachable.

## DETAIL

Recovery email details  
`SC-03A-DETAIL-01`

- email address;

## Main Flow

1. Guest opens password recovery screen.
2. Guest enters recovery email.
3. Client-side email format validation runs automatically.
4. Guest corrects email if format validation fails.
5. Guest submits recovery request.
6. System checks whether entered email is registered.
7. Neutral confirmation is shown: “If this email is registered, recovery instructions have been sent.”

## Branches

### Email format invalid

-> entered email is not valid email format
-> validation error is visible
-> guest corrects email
-> back to entering recovery email

### Entered email registered

-> entered email registered
-> recovery email/instructions become available
-> guest opens password reset experience from recovery instructions
-> opens Set New Password — SC-03B

### Entered email not registered

-> entered email not registered
-> no recovery email is sent
-> neutral confirmation remains visible
-> account existence is not revealed

## Invariants

Account existence is not revealed.

Attach to:

- entered email registered?;
- neutral confirmation is shown;

## Outcomes

- Guest receives neutral recovery confirmation.
- If email is registered, recovery instructions become available.
- If email is not registered, no account existence is revealed.

## Open Questions

Q: Exact recovery instruction channel is an implementation/UX detail. It may be email, but the scenario should not depend on provider mechanics.

## Diagram Notes

- Use a visible decision branch for Entered email registered?, but do not make the user-visible confirmation reveal the branch result.
- Use off-page link to SC-03B only from the registered-email branch.

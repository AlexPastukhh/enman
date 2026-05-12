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

## DETAIL

Registration data details  
`SC-01-DETAIL-01`

- email;
- password;
- password confirmation;
- required contact data if applicable;
- [VAR:EXPAND]

## Main Flow

1. Guest opens registration screen.
2. Guest enters registration data.
3. Client-side validation runs automatically.
4. Basic validation errors are visible before submit, if present.
5. Guest corrects registration data if client-side validation fails.
6. Guest submits registration.
7. System checks whether registration data is accepted.
8. If accepted, account is created.
9. Registration success state is visible.

## Branches

### Client-side validation errors

-> registration form has basic validation issues
-> validation errors are visible
-> guest corrects registration data
-> back to entering registration data

### Registration data accepted

-> registration data accepted
-> account is created
-> registration success state is visible

### Registration data rejected

-> registration data rejected
-> validation/business errors are visible
-> guest corrects registration data
-> back to entering registration data

## Invariants

Invalid registration data does not create an account.

Attach to:

- registration data accepted?;
- submit registration transition;

## Step Postconditions

- Account is created after accepted registration data.

## Outcomes

- Guest sees registration form.
- Guest sees validation errors when input is invalid.
- Guest can correct registration data.
- Guest account is created only after accepted data.

## Open Questions

Q: After registration, is session issued automatically or must user sign in?

## Diagram Notes

- Use a DETAIL node instead of expanding the registration step into a large text card.
- Show client-side validation as observable UI behavior.
- Show correction loop for invalid registration data.

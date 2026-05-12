# SC-03A — Password Recovery Request

## Status
Corrected scenario specification draft.

## Purpose
Guest requests password recovery email without revealing whether an account exists.

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

## DATA
Recovery email DATA `SC-03A-DATA-01`

Input DATA:
```text
- recovery email.
```

## Main Flow
1. Guest opens password recovery screen.
2. Guest enters recovery email.
3. Client-side email format validation runs automatically.
4. Guest corrects email if format validation fails.
5. Guest submits recovery request.
6. System checks whether entered email is registered.
7. Neutral confirmation is shown: “If this email is registered, recovery instructions have been sent.”

## Branches
### Entered email registered
-> recovery email is sent  
-> guest follows recovery link from email  
-> opens SC-03B

### Entered email not registered
-> no recovery email is sent  
-> neutral confirmation remains visible  
-> account existence is not revealed

## Invariants
Account existence is not revealed. Attach to registered-email decision / neutral confirmation.

## Diagram Notes
Password recovery explicitly uses email.

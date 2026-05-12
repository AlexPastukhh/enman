# SC-03B — Account Owner Verified / Password Reset Choice

## Status
Corrected scenario specification draft. Replaces old `SC-03B — Set New Password`.

## Purpose
Guest follows recovery link from email, becomes account-owner verified for this recovery flow, and chooses whether to set a new password or go to login.

## Actor / Screen
Actor: Guest  
Screen: Account owner verified / password reset choice screen  
Goal: Restore access or return to login

## Entry Points
Entry A: Guest follows recovery link from email.

## Preconditions
- Recovery email was sent.
- Guest has recovery link from email.

## DATA
Recovery link context DATA `SC-03B-DATA-01`
```text
- recovery link from email;
- recovery context state: valid / invalid or expired.
```

New password input DATA `SC-03B-DATA-02`
```text
- new password;
- password confirmation.
```

## Main Flow
1. Guest follows recovery link from email.
2. System checks whether recovery context is valid.
3. If valid, account-owner-verified state opens.
4. Guest chooses next action: set new password or go to Login and try old/current password.

## Branches
### Recovery context invalid or expired
-> invalid/expired recovery message visible  
-> guest can request a new recovery email  
-> opens SC-03A

### Set new password
-> guest enters new password and confirmation  
-> client-side password validation runs automatically  
-> password is updated  
-> Q: auto-login or require login?

### Login with old/current password
-> opens Login — SC-02  
-> password is not changed in this branch

## Invariants
Password can be updated only with valid recovery context.

## Open Questions
Q: After password reset, do we automatically sign the user in or require login with the new password?

# Scenario Behavior Coverage Baseline Addendum — Account Activation

Status: active addendum  
Scope: account activation, protected use cases, auth policy placement, scenario questions

## 1. Purpose

This file extends:

```text
planning/tables/pre-domain-variants-input.md
```

It adds stable coverage items for account activation without rewriting the whole baseline file.

These items must be considered by domain drafts together with the main baseline.

## 2. Item Categories Used

```text
CMD  — command behavior
LC   — lifecycle / state / condition behavior
UCQ  — use-case coordination item
SEC  — security/access policy behavior
SQ   — scenario question / requirement clarification
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Detail section | Initial status |
|---|---|---|---|---|---|
| ACC-CMD-LOGIN-001 | CMD | Login with activation requirement | SC-02 / SC-15 | 4.1 | Needs draft answer |
| ACC-LC-001 | LC | Registration creates Active account in core | SC-01 | 5.1 | Needs draft answer |
| ACC-LC-002 | LC | Non-active account cannot use protected functionality | SC-02 / SC-15 | 5.1 | Needs draft answer |
| ACC-LC-003 | LC | Future PendingActivation to Active flow | SC-01 / SC-15 | 5.1 | Deferred / future |
| ACC-UCQ-001 | UCQ | Protected use cases require active account without polluting every aggregate | SC-02 / SC-15 | 6.1 | Needs draft answer |
| ACC-SEC-001 | SEC | Activated account required for protected functionality | SC-02 / SC-15 | 7.1 | Needs draft answer |
| ACC-SQ-001 | SQ | Non-active account login behavior | SC-02 / SC-15 | 8.1 | Question |
| ACC-SQ-002 | SQ | Claim-based activation policy refresh | SC-02 / SC-15 | 8.1 | Question |
| ACC-SQ-003 | SQ | Activation scope for client vs employee accounts | SC-15 | 8.1 | Question |

## 4. Command Behavior Cards

### 4.1 ACC-CMD-LOGIN-001 — Login with activation requirement

Source:

```text
SC-02 Login
SC-15 Security Text Specification
```

Required behavior / guarantee:

```text
Valid credentials and activated account allow protected app access.
```

Failure / no-write guarantee:

```text
Invalid credentials do not issue session/protected access.
Non-active account must not gain protected client/employee functionality.
```

Related items:

```text
ACC-LC-002
ACC-SEC-001
ACC-SQ-001
```

Draft must explain:

```text
- whether login/session is treated as auth boundary outside business domain;
- how activated account requirement is enforced before protected use cases;
- how non-active login behavior is handled or left as scenario question.
```

## 5. Scenario State / Condition Matrix

### 5.1 Account activation condition

Condition concept:

```text
Account activation state / marker
```

Known scenario states:

```text
Active
NonActive / PendingActivation [future]
```

| ID | Current condition | Scenario action | Result condition | Allowed? | Required behavior / guarantee | Failure / no-write guarantee | Draft must explain |
|---|---|---|---|---|---|---|
| ACC-LC-001 | no account exists | accepted registration | account exists, activation state Active in current core | Yes | Current core registration creates activated account. | Invalid registration creates no account. | How account activation state is represented and why current core registration creates Active. |
| ACC-LC-002 | account NonActive / PendingActivation | protected client/employee functionality | unchanged | No | Non-active account cannot use protected functionality. | Protected command/read does not execute; business state unchanged. | How active-account guard is enforced before protected use cases. |
| ACC-LC-003 | account PendingActivation [future] | complete activation [future] | account Active | Future | Future activation flow may activate account. | Failed activation keeps account non-active. | Whether this is deferred and what future activation flow may require. |

## 6. Use-Case Coordination Items

### 6.1 ACC-UCQ-001 — Account activation / protected use-case coordination

Required behavior / guarantee:

```text
Protected business use cases require activated account, but Request / AgreementProposal / ApplicantData should not all duplicate account activation logic internally.
```

Concepts involved:

```text
- account activation state;
- protected use cases;
- business commands;
- read/access behavior;
- future auth policy/claims.
```

Failure / no-write guarantee:

```text
If account is not activated, protected use case does not execute and business state remains unchanged.
```

Draft must explain:

```text
- current placement: application service guard + Account.EnsureActivated;
- future placement: AccountActivated authorization policy;
- whether policy may be backed by account_activated claim;
- why business aggregates can assume active actor context after guard/policy.
```

## 7. Security / Policy Items

### 7.1 ACC-SEC-001 — Activated account required for protected functionality

Required behavior / guarantee:

```text
Activated account is required for protected client/employee functionality.
```

Source:

```text
SC-02
SC-15
scenario-account-activation-security-addendum.md
```

Draft must explain:

```text
- current enforcement by application service + Account.EnsureActivated;
- future enforcement by AccountActivated policy/claim;
- why this is not duplicated inside every business aggregate;
- how non-active account attempts are rejected before business state changes.
```

Expected first draft answer direction:

```text
Resolved outside current business aggregate.

Account/Auth boundary owns activation marker.
Application service calls Account.EnsureActivated before protected use cases.
Future AccountActivated policy may use account_activated claim.
Request / ApplicantData / AgreementProposal assume active actor context.
```

## 8. Scenario Questions / Requirement Gaps

### 8.1 Account activation questions

| ID | Scenario question | Source | Why it matters | Current planning note |
|---|---|---|---|---|
| ACC-SQ-001 | If user created account but did not activate it and then tries to log in, what should happen? | SC-02 / SC-15 | Different choices change auth/session UX and authorization placement. | Options: reject login; issue limited session and show activation-required page; allow session but block protected actions by AccountActivated policy. Current core avoids branch because registration creates Active account. |
| ACC-SQ-002 | If future AccountActivated policy uses `account_activated` claim, how is stale claim refreshed after activation state changes? | SC-02 / SC-15 | Claim avoids DB lookup but can become stale. | Future implementation question. |
| ACC-SQ-003 | Does account activation apply identically to client accounts and employee accounts, or only to client accounts in core? | SC-15 | Employee flows may require same policy or separate employee-status policy. | Draft should avoid assuming silently. |

## 9. How Domain Drafts Use This Addendum

Each domain draft should include these items in the normal coverage table:

```text
Item ID | Status | Draft answer / placement | Covered by | Gap / next action
```

Example:

```text
ACC-LC-001 | Covered | Registration creates Active account in current core. | Account.Register / Account activation state | Future PendingActivation deferred.
ACC-SEC-001 | Resolved outside current business aggregate | Protected use cases require activated account; current guard is application service + Account.EnsureActivated; future guard is AccountActivated policy/claim. | Cross-layer placement note + Account.EnsureActivated | Resolve inactive login UX.
ACC-SQ-001 | Question | Non-active login behavior is not fixed. | Scenario question | Choose fail-login vs limited-session vs policy-blocked actions.
```

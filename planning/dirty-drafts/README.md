# Dirty Drafts / Raw Notes

Status: intentionally dirty / non-canonical preservation area  
Purpose: keep raw architecture and diploma/thesis notes that may be useful if a later cleanup accidentally loses context.

## Rule

Files in this folder are **not** authoritative scenario, slice, domain or implementation specs.

They are allowed to contain:

```text
- raw discussion notes;
- thesis/diploma wording drafts;
- unresolved or historical context;
- temporary explanations;
- implementation-review notes;
- rejected/changed decisions, if marked by later canonical docs.
```

Canonical docs still live in:

```text
planning/diagrams/
planning/slices/
planning/tables/domain-drafts/
planning/api/
planning/architecture/
planning/client/
planning/testing/
```

Use dirty drafts only as recovery/context material.

## Current files

```text
planning/dirty-drafts/diploma-architecture-decisions-raw-draft.md
```

This file preserves a raw architecture/diploma draft covering:

```text
- legacy runtime cleanup;
- L1/L2 runtime boundary;
- client API ownership;
- generated OpenAPI aliases;
- CSRF/antiforgery;
- Employee dashboard/read side;
- StartReview command decision;
- Employee : Account / TPH;
- Windows Auth as sign-in provider;
- ClaimsPrincipalFactory;
- testing strategy;
- migrations and generated artifact workflow;
- thesis-ready wording snippets;
- TestServer vs Windows Negotiate Auth integration-test failure and fix;
- test auth scheme-provider split: full fake auth vs EmployeeWindows-only fake;
- why real Negotiate is mocked globally in test host while cookie auth remains real;
```

## Navigation

When using dirty drafts:

```text
1. Read canonical scenario/slice/domain docs first.
2. Use dirty draft only for context or thesis wording recovery.
3. If a dirty note conflicts with canonical docs, canonical docs win.
4. Promote useful text into canonical docs through a normal docs sync.
```


## Latest preserved additions

```text
Sections 34-40 preserve the TestServer / Windows Negotiate Auth integration-test failure,
why ordinary tests started returning 500, and the accepted test-host fix:
mock only EmployeeWindows/Negotiate globally while keeping cookie auth real.
```

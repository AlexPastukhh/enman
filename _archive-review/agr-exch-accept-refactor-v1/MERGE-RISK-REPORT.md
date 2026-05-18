# Merge Risk Report — agr-exch-accept-refactor-v1

## Files replaced

```text
planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md
planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
```

## Risk level

Medium.

Reason:

```text
These are slice handoff drafts. Replacing them can accidentally remove decisions, guardrails or implementation notes if the refactor is incomplete.
```

## Checked preservation targets

```text
- Client-only accept boundary;
- endpoint: POST /api/agreement-exchanges/{exchangeId}/accept;
- no request body;
- success: 204 No Content;
- ClientAccountId ownership guard;
- AwaitingClientConfirmation lifecycle precondition;
- active proposal must be Employee-authored;
- exchange/proposal become Accepted;
- no new proposal version;
- Employee accept out of scope;
- counter-proposal/final refusal out of scope;
- CSRF required;
- no per-command status enum;
- no FluentValidation for lifecycle/ownership;
- OpenAPI/generated artifact workflow preserved;
- paired client/server ownership preserved.
```

## Post-apply checks

```powershell
git diff -- planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md
git diff -- planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
git grep -n "Employee accept" planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
git grep -n "204 No Content" planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
git grep -n "ClientAccountId" planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
git grep -n "new proposal version" planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
```

If a needed decision is missing, create a small correction archive for only the affected file.

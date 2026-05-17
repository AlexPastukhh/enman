# L2 Agreement Exchange Final Refuse Slice Sync

Status: docs-only sync / final refusal full server + client drafts added

## Added / replaced

```text
planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md
planning/slices/l2/L2-AGR-EXCH-FINAL-REFUSE-001-employee-final-refuse-agreement-exchange.client.md
```

## Canonical responsibility

```text
SL-AGR-EXCH-006
  Employee backend/API command slice.
  POST /api/agreement-exchanges/{exchangeId}/final-refuse.
  Employee-only.
  Nullable body with optional reason.
  Orchestrates AgreementProposalExchange + ConnectionRequest through domain methods.
  Success: 204 No Content.

L2-AGR-EXCH-FINAL-REFUSE-001.client
  Employee client command sidecar.
  Hosted only in Employee agreement exchange details action area.
  Optional reason form.
  No Client final-refuse first pass.
  Refreshes exchange details/list and request details only when cache/query key exists.
```

## Guardrails

```text
- No ResponsibleEmployeeId guard.
- No command status enum.
- No manual status setting in application layer.
- Domain calls:
    exchange.FinalRefuseProposal(...)
    request.MarkAgreementExchangeFailed(...)
- Missing/null body is allowed.
- Missing/null reason is allowed.
- Whitespace-only reason is invalid.
- No proposal version is created.
- Do not assert request failure timestamp/id unless those fields exist.
```

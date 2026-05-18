# Merge Risk Report

## Summary

Risk level: low for docs-only replacement if no parallel edits were made to the same two planning files after source fetch.

## Risks

| Risk | Level | Mitigation |
|---|---:|---|
| Parallel changes to the same planning files | medium | Re-check git diff before applying if another refactor was merged locally. |
| Runtime implementation mismatch | known / not checked | This is a docs-only refactor; implementation audit is intentionally out of scope. |
| Generated artifacts drift | none in archive | No generated artifacts are included. Regenerate only in runtime/API implementation mode. |
| UI/page-flow assumptions | low | Runtime UI/page-flow/redirect audit is explicitly out of scope. |
| Question ID drift | low | Existing client Q IDs are preserved; server draft had no stable Q IDs and now adds new server IDs. |

## Not included

```text
- runtime code changes
- tests
- generated OpenAPI/types
- runtime UI implementation
- page-flow/redirect audit
- navigation updates
```

# Algorithm: repo-check before implementation text

Run before writing about implementation, especially chapter 3.

## Required behavior

1. Do not write implementation text from memory.
2. Check current repo/archive.
3. Check slice drafts.
4. Check scenarios, DATA, domain drafts, ADR/questions/decisions.
5. Check code and tests.
6. Separate:
   - implemented;
   - tested;
   - manual/demo only;
   - designed only;
   - planned/deferred;
   - requires check;
   - do not mention.
7. Only then write section draft text.

## Guardrails

- Account activation is not a central implemented VKR flow without explicit repo-check.
- Approve does not automatically generate a contract unless code proves it.
- Mock external verification is not real integration.
- Document metadata/reference is not full EDO/ECM/storage.
- Email notifications require repo-check before claiming full implementation.

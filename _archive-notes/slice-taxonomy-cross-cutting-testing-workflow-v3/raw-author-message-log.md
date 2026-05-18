# Raw Author Message Log

Status: archive-local raw notes / not normative / for thesis-writing voice extraction only

## Captured author messages/topics

- There are files with L1/L2 in names; check whether they are needed or whether they interfere.
- Old L1/L2 files may be historical, but L1/L2 should not be a future planning structure.
- Files about implementation of common things are OK, but they should be real slice drafts with typical client/server slice draft structure.
- A common implementation slice may not have one concrete business scenario, but it still needs a narrow common behavior scenario source.
- A slice is behavior + implementation plan + testing/verification.
- Behavior must come from scenarios, even if it is common across many scenarios.
- Common UI/client behavior should have scenario behavior sources where UI scenarios live or near them.
- There is also a need for scenario bases for server cross-cutting slices.
- Some cross-cutting concerns are server-client concerns, like CSRF, where both server and client work may be needed.
- CSRF needs dangerous/security scenarios describing what behavior we avoid and what protection is required.
- After scenario sources, data files and behavior items, we can create slice drafts.
- Data files register what the user enters/sees and related data details.
- Behavior items are smaller than slices.
- One scenario can contain many behavior items and one slice can implement an independent chain of behavior items.
- Client and server slice drafts are parts of one logical slice, split for convenience.
- Slices can be client-only, server-only, paired, extension/follow-up, or cross-cutting.
- Cross-cutting slice drafts should exist under client/server folders for side-specific implementation.
- The cross-cutting folder should be for umbrella/coordination, not a dump for implementation details.
- If a slice draft has no expected counterpart on the other side, this must be visible in the file name.
- Prefer an explicit marker like SINGLE- over ambiguous hidden assumptions.
- Tests should verify behavior items and scenario flow, not implementation details as primary proof.
- Slice drafts need explicit test workflow close to slices, while general principles stay in planning/testing.
- Test plan should show which behavior item is tested, what implementation is used as mechanism, whether a bad implementation can pass, and whether a good refactor can break the test.
- Escape risk means whether a bad vertical implementation can pass the test and still break the scenario.
- Refactor risk means whether a behavior-preserving refactor can break the test.
- Direct DB setup is allowed only for preconditions; behavior proof should go through public boundary.
- Negative command tests should ask what must not change.
- CSRF full behavior belongs to CSRF concern tests; feature tests can have one smoke.

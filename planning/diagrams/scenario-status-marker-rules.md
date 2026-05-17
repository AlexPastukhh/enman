# Scenario Status Markers For Diagrams

Status: current / diagram-facing scenario marker rule  
Scope: how scenario docs mark current/planned/deferred scenario elements so VKR diagrams can show the project after L1 without implying that every L2 element is already implemented.

## 1. Purpose

The project is no longer only an L1 client/request foundation. Scenario docs may now describe:

```text
- implemented L1/L2 behavior;
- accepted L2 target behavior;
- planned implementation slices;
- deferred future extensions;
- unresolved questions.
```

For diagrams and diploma/VKR text, future implementation work should be marked consistently.

## 2. Allowed markers

Use the same marker vocabulary as diagram-generation docs:

```text
[CORE]
[IMPLEMENTED]
[DESIGNED]
[PLANNED]
[DEFERRED]
[QUESTION]
```

| Marker | Meaning in scenario docs |
|---|---|
| `[CORE]` | Core diploma/MVP concept or flow. Pair with another marker when implementation status matters. |
| `[IMPLEMENTED]` | Current repo implementation evidence confirms the element. Do not use only because a scenario says it should exist. |
| `[DESIGNED]` | Accepted scenario/domain direction; behavior is stable enough for diagrams but implementation evidence is not required. |
| `[PLANNED]` | Intended future/next implementation work, normally backed by a slice draft or planned slice family. |
| `[DEFERRED]` | Known extension point outside the current cut. |
| `[QUESTION]` | Open conflict or unresolved decision that can affect diagram meaning. |

## 3. Scenario-local marker section

When a scenario mixes current, planned and future behavior, add a section:

```markdown
## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| ... | [PLANNED] | ... |
```

Rules:

```text
- Use [PLANNED] for future implementation work that is in the current L2 plan.
- Use [DEFERRED] for future extension points outside current L2.
- Use [DESIGNED] for accepted domain/scenario semantics that diagrams may show as target model.
- Use [QUESTION] for route/contract/status ambiguity.
- Use [IMPLEMENTED] only when the scenario doc cites or is updated from current implementation evidence.
```

## 4. Diagram preflight rule

Diagram Chat must still verify implementation status from the current repository.

Scenario-local markers are planning hints:

```text
[PLANNED] in a scenario can become [IMPLEMENTED] in a diagram only after repo evidence confirms implementation.
[DESIGNED] can stay [DESIGNED] even when no implementation exists yet.
[DEFERRED] should be shown as outside current cut, not as a main flow.
```

## 5. L2 default interpretation

For L2 Employee/Review/Agreement scenarios:

```text
- review and agreement scenario specs are target/current planning source;
- server/client slice drafts define implementation boundaries;
- agreement exchange commands/reads are mostly [PLANNED] until code/OpenAPI confirms them;
- cross-aggregate/domain invariants can be [DESIGNED];
- old validation/security terms and obsolete SC-14 client-data-verification wording must not become diagram source.
```

## 6. Do not

```text
- Do not create new marker names like [FUTURE] or [TODO].
- Do not mark stale/old wording as [PLANNED].
- Do not use scenario markers to override code evidence.
- Do not draw [DEFERRED] flows as if they are current main flows.
- Do not let [PLANNED] mean вЂњalready implementedвЂќ.
```


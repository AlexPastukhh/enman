# Scenario Advanced Package — SC-15..SC-18

Suggested repository target paths, not written by this task:

- `planning/diagrams/scenario-advanced-package.drawio`
- `planning/diagrams/scenario-advanced-package.md`
- `planning/diagrams/scenario-advanced-package-preview.png`
- `planning/diagrams/scenario-diagram-consistency-report.md`

## Pages created

| Page | Scenario | Ref | Package marker |
|---|---|---|---|
| 15 | Security / Account Protection Scenario | SC-15 | [EXT:L3] |
| 16 | Reliable Notification Delivery Scenario | SC-16 | [EXT:L3] |
| 17 | Anonymous Request Scenario | SC-17 | [EXT:L3] |
| 18 | Archive / Audit Scenario | SC-18 | [EXT:L3] |

No SC-00..SC-14 pages, global overview, domain/aggregate/DB diagrams, implementation diagrams, responsibility tables, or scenario-to-slice maps were created.

## Source/package availability

Read and used:

- `planning/diagram-brief.md`
- `planning/diagram-scenario-spec.md`
- `planning/diagram-generation-rules-with-example.md`
- `planning/diagram-prompting-guide.md`
- `planning/diagram-common-mistakes.md`
- `planning/diagram-examples-index.md`
- `planning/examples/scenario-login-correct-v3.md`
- `planning/examples/scenario-login-correct-v3.drawio`
- `planning/examples/scenario-login-correct-v3.svg`
- `planning/examples/scenario-login-correct-v3.png`

Core and extension package files were checked at the requested paths but were unavailable in the repository branch:

- `planning/diagrams/scenario-core-package.md`
- `planning/diagrams/scenario-core-package.drawio`
- `planning/diagrams/scenario-extension-package.md`
- `planning/diagrams/scenario-extension-package.drawio`

No uploaded package artifacts were available in this chat. Therefore, cross-page naming for SC-00..SC-14 uses the known scenario catalog titles from the task prompt and is called out in the consistency report as a limitation.

## Key off-page links

- SC-15 uses `SC-15-EXTND-01` for `Login Scenario — SC-02`.
- SC-16 uses `SC-16-EXTND-01..03` for `Approval Result — SC-08`, `Rejection Result — SC-09`, and `Contract Acknowledgement — SC-13` as off-page source scenarios that can require notification.
- SC-17 uses `SC-17-EXTND-01..03` for `Registration — SC-01`, `Login — SC-02`, and `Client Request Creation — SC-04`.
- SC-18 uses `SC-18-EXTND-01..03` for `Employee Request Dashboard — SC-06`, `Employee Request Review — SC-07`, and `Client Request Status / Result — SC-05`.

All off-page links are purple and use `EXTND`, not `EXT`, in item refs.

## Key invariants and attachment points

| Scenario | Invariant | Attached to enforcement point |
|---|---|---|
| SC-15 | Protected resource is accessible only after authentication. | `Authenticated?` decision (`SC-15-BR-01`). |
| SC-16 | Business decision/result is not lost, reverted, or corrupted because notification failed. | Notification-attempt boundary (`SC-16-STEP-02`) and failure preservation branch. |
| SC-17 | Invalid anonymous request data does not create a submitted request. | `Request data valid?` decision (`SC-17-BR-01`). |
| SC-17 | Anonymous request has enough contact data for follow-up. | Contact data step (`SC-17-STEP-02`) before validation/submission. |
| SC-18 | Archived request must not lose required audit/history. | Archive transition/result (`SC-18-SPOST-01`). |
| SC-18 | Non-final or otherwise ineligible request cannot be archived. | `Archive allowed?` decision (`SC-18-BR-01`). |

## Marker usage

- `[CORE]` appears on required behavior inside advanced scenarios, especially SC-15 protected-resource behavior and cross-cutting invariants/outcomes that must hold for the advanced scenario to be correct.
- `[EXT:L3]` marks the advanced/cross-cutting scenario package and selected advanced behavior. It is not used in item refs.
- `[ALT]` is not used. None of the error/failure branches are alternative paths that achieve the same goal; they are negative/invalid/failure branches.
- `[VAR:REPLACE]` appears only in SC-16 planning notes for replaceable notification delivery strategy/channel.
- `[VAR:MODIFY]` appears only in SC-17 for the uncertain submitted-vs-draft product decision.
- `[RISK]` and `[ADR?]` appear only in planning notes or uncertain behavior nodes where failure semantics, anonymous-request scope, or reliability mechanism need later decisions.

## Open questions

SC-16:

- Which reliability/follow-up mechanism is selected later?
- Can notification channel/provider be replaced?
- What exact failure visibility is required for support?

SC-17:

- Does anonymous request create a full request or only a contact request/draft?
- Does guest need to create an account later?
- How does guest track status?
- Is anonymous request actually in scope?

SC-18:

- The diagram assumes archive eligibility is checked inside `Archive allowed?` instead of using `request is final` as a scenario precondition.

## Self-check results

- Repository write rule followed: no repository files were created, updated, deleted, moved, renamed, or committed.
- Generated files were returned as reviewable artifacts only.
- Approved dark theme used: dark background, subtle grid, high-contrast text, dark node bodies, semantic colored borders/fills.
- No light/default draw.io theme was used.
- Main flow is the visual backbone on each page.
- Preconditions do not duplicate decisions.
- Invariants attach to enforcement points.
- Error/failure/rejection branches are red and are not marked `[ALT]`.
- Off-page links are purple and use `EXTND` item refs.
- Includes are cyan and attached to the step requiring them.
- End states are compact summary blocks, not connector webs.
- No implementation details such as controllers, endpoints, handlers, repositories, DbContext, database tables, outbox tables, provider APIs, jobs, queues, middleware, cookies/tokens, audit tables, event store, or triggers appear.
- Text was sized conservatively and nodes were widened to prevent overflow.

## Optional reference files

The canonical example PNG at `planning/examples/scenario-login-correct-v3.png` was available. PNG availability was not treated as a blocker because the canonical `.drawio` and `.svg` were also available and used.

## Artifact handling

These outputs were generated outside the repository and returned for review. Suggested target paths are listed above, but nothing was written to the repository.

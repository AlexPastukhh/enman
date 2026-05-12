# Scenario CORE Package — SC-00..SC-09

Suggested repository target paths for review placement:

```text
planning/diagrams/scenario-core-package.drawio
planning/diagrams/scenario-core-package.md
planning/diagrams/scenario-core-package-preview.png
```

No repository files were created, updated, deleted, moved, renamed, or committed. These files were generated as reviewable artifacts only.

## Pages created

1. **00 Scenario Overview / Navigation Map**
2. **01 Guest Registration Scenario — SC-01**
3. **02 Login Scenario — SC-02**
4. **03 Password Recovery Scenario — SC-03**
5. **04 Client Request Creation Scenario — SC-04**
6. **05 Client Request Status / Result Scenario — SC-05**
7. **06 Employee Request Dashboard Scenario — SC-06**
8. **07 Employee Request Review Scenario — SC-07**
9. **08 Approval Result: Contract Draft + Email Notification — SC-08**
10. **09 Rejection Result Scenario — SC-09**

No pages were created for SC-10..SC-14, SC-15..SC-18, domain/aggregate/DB diagrams, implementation diagrams, responsibility tables, or scenario-to-slice maps.

## Scenario refs covered

- SC-00: Scenario Overview / Navigation Map
- SC-01: Guest Registration
- SC-02: Login
- SC-03: Password Recovery
- SC-04: Client Request Creation
- SC-05: Client Request Status / Result
- SC-06: Employee Request Dashboard
- SC-07: Employee Request Review
- SC-08: Approval Result
- SC-09: Rejection Result

## Key off-page links

- **SC-02-EXTND-01**: Forgot password selected → Password Recovery — SC-03.
- **SC-03-EXTND-01**: Set new password subscenario, shown as `[EXT:L2]`.
- **SC-04-EXTND-01**: Request Documents — SC-11, shown only as compact future `[EXT:L2]` link.
- **SC-05-EXTND-01**: Approved result details → Approval Result — SC-08.
- **SC-05-EXTND-02**: Rejected result details → Rejection Result — SC-09.
- **SC-06-EXTND-01**: Employee opens request review → Employee Request Review — SC-07.
- **SC-07-EXTND-01**: Approval branch → Approval Result — SC-08.
- **SC-07-EXTND-02**: Rejection branch → Rejection Result — SC-09.
- **SC-07-EXTND-03**: Clarification — SC-12, shown only as compact future `[EXT:L2][VAR:MODIFY][ADR?]` link.
- **SC-08-EXTND-01**: Reliable Notification Delivery — SC-16, shown only as compact future `[EXT:L3]` link.
- **SC-09-EXTND-01**: Reliable Notification Delivery — SC-16, shown only as compact future `[EXT:L3]` link.

`EXTND` is used in item refs. `[EXT]` is used only as a roadmap/scope marker.

## Key invariants and attachment points

- **SC-01-INV-01**: Invalid registration data must not create an account. Attached to **Registration data valid?**.
- **SC-02-INV-01**: Session is issued only for valid credentials. Attached to **Credentials valid?**.
- **SC-03-INV-01**: Do not reveal account existence. Attached to **Recovery request acceptable?**.
- **SC-04-INV-01**: Invalid request is not accepted. Attached to **Request valid?**.
- **SC-05-INV-01**: Client can see only requests/results available to them. Attached to **Select or view request** visibility.
- **SC-06-INV-01**: Employee only sees requests they may review. Attached to **Submitted requests are visible**.
- **SC-07-INV-01**: Final request cannot be reviewed again. Attached to **Review decision?** / finalization decision point.
- **SC-08-INV-01**: Notification failure must not change review decision. Attached to **Notification is sent**.
- **SC-09-INV-01**: Shown rejection result must correspond to recorded rejection decision. Attached to **Rejection decision exists → visible result**.

## `[ALT]` usage

`[ALT]` is used only in **SC-04**:

- **SC-04-STEP-03B**: Provide individual applicant data inline `[ALT][VAR:EXPAND]`.

Reason: this is an alternative way to continue the same request-creation goal when saved applicant data is missing. Invalid/error branches are not marked `[ALT]`.

## `[EXT]`, `[VAR]`, `[RISK]`, and `[ADR?]` usage

- `[EXT:L2]`:
  - SC-03 Set new password subscenario.
  - SC-04 Request Documents — SC-11.
  - SC-07 Clarification — SC-12.
- `[EXT:L3]`:
  - SC-08 / SC-09 Reliable Notification Delivery — SC-16.
- `[VAR:EXPAND]`:
  - SC-04 Applicant data available? and inline applicant data path, because applicant-data variants may expand.
- `[VAR:MODIFY]`:
  - SC-07 Review decision? and Clarification future branch, because the review workflow may change.
- `[RISK][ADR?]`:
  - SC-08 notification failure rule, because reliable notification behavior may require a later decision.
- `[ADR?]`:
  - SC-01 open question about automatic session after registration.
  - SC-07 Clarification future branch.

Markers were kept selective and visible; they were not applied to every node.

## Open questions

- **SC-01-Q-01**: After registration, is a session issued automatically, or must the user explicitly sign in?
- **SC-08 notification behavior**: Reliable delivery is shown as future SC-16; core SC-08 only shows the observable notification-send outcome.
- **SC-09 notification behavior**: Notification is shown as observable behavior; reliable delivery remains future SC-16.

## Reference file notes

- The approved canonical Login example files were available in the preferred `planning/examples/` paths, including `.md`, `.drawio`, `.svg`, and `.png`.
- The SC-04 proof-of-layout example is listed as TODO in the examples index and was not found in repository search. This was not treated as a blocker; SC-04 was generated from the requested scenario semantics and the approved SC-02 visual baseline.
- The preview image generated here is an overview-page PNG preview for quick review. The `.drawio` file contains all generated pages.

## Self-check results

### Repository write rule

- No repository files were created, updated, deleted, moved, renamed, or committed.
- Generated outputs are returned as downloadable review artifacts only.
- Repository target paths are suggested but not written.

### Dark theme

- Dark background is used on every page.
- No white/light canvas is used.
- Semantic colors are readable on dark background.
- Visual style follows the approved canonical dark Login example family.

### Semantic correctness

- Preconditions do not duplicate decision branches.
- Invariants attach to enforcement points, decisions, or protected visibility transitions.
- Invalid/error branches are not marked `[ALT]`.
- `[ALT]` is used narrowly for the SC-04 applicant-data inline path only.
- `[EXT]` is not used in item refs.
- `EXTND` is used for off-page/subscenario item refs.
- Step-level postconditions attach to producing steps.
- Scenario end states are compact secondary summaries.
- No implementation details appear.
- Purple is used only for off-page/subscenario/future links.

### Visual correctness

- Main flow is visually obvious on each scenario page.
- Diagrams are scenario/use-case flow diagrams, not text-card summaries.
- Layout is spacious and uses conceptual lanes.
- No long connector web to end-state summaries is used.
- Secondary connectors are local where possible.
- Text-fit check passed for all generated vertices.
- Connector labels use semantic wording such as `starts`, `next`, `checks`, `include`, `if valid`, `if invalid`, `results in`, `protected by`, `opens subscenario`, and `future branch`.
- XML validation passed.

# Diagram Common Mistakes

Status: permanent catalog of repeated diagram mistakes and reject criteria.

Purpose: help future diagram-generation agents avoid mistakes that appeared in previous scenario diagram attempts.

## 1. Text Cards Instead Of Scenario Flow

Mistake:

```text
The page is mostly large disconnected cards such as Preconditions, Main flow, Invariants and Acceptance Criteria.
```

Why wrong:

```text
A scenario diagram should visualize behavior, branches and relationships.
It should not be a Markdown spec pasted into boxes.
```

Bad example:

```text
One big card: Main Flow
One big card: Preconditions
One big card: Acceptance Criteria
```

Correct approach:

```text
Actor/screen -> action nodes -> decision nodes -> branches -> outcomes.
Attach includes, invariants and postconditions to the exact relevant behavior.
```

Review/reject rule:

```text
Reject if the diagram is understandable only as text cards and not as flow.
```

## 2. Connector Overlap / Connector Web

Mistake:

```text
Many connectors overlap, cross card bodies or form a web around the page.
```

Why wrong:

```text
Relationships become unreadable and secondary links compete with the main flow.
```

Bad example:

```text
Every final branch has a long dashed connector to End states.
```

Correct approach:

```text
Use a compact end-state block without connectors when the summary is obvious.
Keep secondary connectors short and local.
```

Review/reject rule:

```text
Reject if connectors cross shape bodies, body text or each other in dense corridors.
```

## 3. Secondary Connectors Crossing Main Flow

Mistake:

```text
include, protected by, summarized as or opens subscenario connectors cross the main flow corridor.
```

Why wrong:

```text
The main flow is the visual backbone and must stay clean.
```

Bad example:

```text
Forgot password link crosses the Credentials valid? success branch.
```

Correct approach:

```text
Move the secondary node to a side lane or remove the connector if placement already communicates the relation.
```

Review/reject rule:

```text
Reject if secondary connectors visually compete with primary next-step links.
```

## 4. Visible Lane Lines Treated As Mandatory Final Style

Mistake:

```text
Lane discipline becomes visible swimlane lines on every final scenario diagram.
```

Why wrong:

```text
Lanes are usually conceptual layout discipline, not required final decoration.
```

Bad example:

```text
Every scenario page has heavy visible lane separators even when they add visual noise.
```

Correct approach:

```text
Use conceptual lanes. Draw visible lane lines only for proof-of-layout/debug examples or when explicitly requested.
```

Review/reject rule:

```text
Reject if lane lines dominate the scenario and were not requested.
```

## 5. Invariant Attached To Late Consequence Instead Of Enforcement Point

Mistake:

```text
Invariant is connected to a later state instead of the decision/transition where the rule is enforced.
```

Why wrong:

```text
The diagram misrepresents where the rule is protected.
```

Bad example:

```text
Remain unauthenticated
-> protected by
No session for invalid credentials
```

Correct approach:

```text
Credentials valid?
-> protected by
Session is issued only for valid credentials
```

or:

```text
Transition into Session is issued
-> protected by
Session is issued only for valid credentials
```

Review/reject rule:

```text
Reject if an invariant is far from the step, decision, transition or state where it is enforced.
```

## 6. Purple Used For Actor Choice Instead Of Off-Page Transition

Mistake:

```text
Purple is used for every actor choice.
```

Why wrong:

```text
Purple means the path leaves the current scenario page.
It does not mean actor choice in general.
```

Bad example:

```text
Any optional in-page choice is purple.
```

Correct approach:

```text
Purple only for subscenario links, off-page transitions, placeholders for another page or branches not expanded here.
```

Review/reject rule:

```text
Reject if normal in-page actor choices are purple only because they are choices.
```

## 7. [ALT] Used For Errors Or Every Optional Path

Mistake:

```text
Invalid credentials, validation errors or every optional user action are marked [ALT].
```

Why wrong:

```text
[ALT] has a narrow meaning: an alternative path that still achieves the same or equivalent goal when the normal path is not suitable.
```

Bad example:

```text
Invalid credentials [ALT]
Forgot password selected [ALT]
```

Correct approach:

```text
Invalid credentials -> negative/error branch.
Forgot password selected -> opens subscenario Password Recovery - SC-03.
```

Only use `[ALT]` for forgot password if explicitly treated as an alternative way to achieve the same access goal.

Review/reject rule:

```text
Reject if [ALT] is used just because a branch is not the success path.
```

## 8. [EXT] Used In Item IDs Instead Of EXTND

Mistake:

```text
Forgot password selected
SC-02-EXT-01
```

Why wrong:

```text
[EXT] is a roadmap/scope marker. It is not an item-ref code.
```

Correct approach:

```text
Forgot password selected
SC-02-EXTND-01
```

Review/reject rule:

```text
Reject if EXT appears in item refs where EXTND should be used.
```

## 9. Generic "extend" Connector Label

Mistake:

```text
Connector label: extend
```

Why wrong:

```text
Readers need a concrete relationship, not UML-ish jargon.
```

Bad example:

```text
Login -> extend -> Forgot password
```

Correct approach:

```text
Open login screen -> if selected -> Forgot password selected
Forgot password selected -> opens subscenario -> Password Recovery - SC-03
```

Review/reject rule:

```text
Reject generic "extend" labels unless explicitly requested.
```

## 10. Acceptance Cards Duplicating The Flow

Mistake:

```text
Large acceptance cards repeat every branch already shown in the flow.
```

Why wrong:

```text
The diagram should express acceptance through behavior and outcomes.
```

Bad example:

```text
Acceptance Criteria card duplicates success, invalid login and forgot password branches.
```

Correct approach:

```text
Use compact end states or attach short acceptance notes only when needed.
```

Review/reject rule:

```text
Reject if acceptance cards become more important than the scenario flow.
```

## 11. Preconditions Duplicating Decision Branches

Mistake:

```text
Credentials are valid
```

is listed as a Login precondition while the diagram also has:

```text
Credentials valid?
```

Why wrong:

```text
The condition is scenario behavior, not an external fact before the scenario.
```

Correct approach:

```text
Use a decision branch for credentials validity.
Use preconditions only for facts that are true before the scenario starts.
```

Review/reject rule:

```text
Reject if a precondition duplicates a decision diamond.
```

## 12. Step-Level Postconditions Forgotten

Mistake:

```text
A step produces an important result but no local postcondition/outcome is shown.
```

Why wrong:

```text
The diagram loses the causal link between action and observable result.
```

Bad example:

```text
Submit sign-in -> Redirect
```

with no visible `Session is issued`.

Correct approach:

```text
Submit sign-in -> Session is issued -> Redirect to requested app page.
```

Review/reject rule:

```text
Reject if important results are only hidden in a final summary.
```

## 13. Scenario-Level Postconditions Drawn As Central Flow

Mistake:

```text
The end-state summary block is placed in the middle of the main flow.
```

Why wrong:

```text
Scenario-level postconditions summarize end states; they are not primary action nodes.
```

Correct approach:

```text
Keep end states as compact secondary summary.
```

Review/reject rule:

```text
Reject if summary blocks visually dominate the scenario.
```

## 14. Markers Removed Entirely

Mistake:

```text
All [CORE], [ALT], [EXT], [VAR:*], [RISK] and [ADR?] markers are removed.
```

Why wrong:

```text
Markers carry planning meaning and roadmap context.
```

Correct approach:

```text
Use markers selectively but visibly on key nodes and meaningful branches.
```

Review/reject rule:

```text
Reject if planning meaning is lost because all markers disappeared.
```

## 15. Markers Overused Everywhere

Mistake:

```text
Every node has multiple markers.
```

Why wrong:

```text
Markers become noise and stop clarifying planning meaning.
```

Correct approach:

```text
Use [CORE] on key main-flow nodes and required branches.
Use other markers only when they add planning value.
```

Review/reject rule:

```text
Reject if markers overwhelm the scenario labels.
```

## 16. Implementation Details Leaking Into Scenario Diagrams

Mistake:

```text
Command, handler, repository, DbContext, table or React hook details dominate a scenario page.
```

Why wrong:

```text
Scenario diagrams are user-facing behavioral specifications.
```

Correct approach:

```text
Move technical flow to CQRS appendix, aggregate diagrams or DB diagrams.
```

Review/reject rule:

```text
Reject scenario pages that are command/domain/table maps.
```

## 17. Machine-Only IDs Without Readable Labels

Mistake:

```text
SC-02-STEP-03
```

appears without a readable label.

Why wrong:

```text
Humans should not have to decode machine-only refs.
```

Correct approach:

```text
Submit sign-in
SC-02-STEP-03
```

Review/reject rule:

```text
Reject if IDs are not paired with readable labels.
```

## 18. End-State Summary Links Creating A Web

Mistake:

```text
Every branch has a long connector to End states / Postconditions.
```

Why wrong:

```text
Summary connectors create visual noise and hide the scenario flow.
```

Correct approach:

```text
Use compact end-state block without connectors when placement is clear.
```

Review/reject rule:

```text
Reject if end-state summary links create a web.
```

## 19. Invariant From Another Scenario Placed On Current Page

Mistake:

```text
Login scenario contains:
Protected app page is accessible only after authentication.
```

Why wrong:

```text
That invariant belongs to protected-resource-access behavior, not the login flow.
```

Correct approach:

```text
Move the invariant to the scenario where protected resource access is attempted.
```

Review/reject rule:

```text
Reject unrelated global invariants on a specific scenario page.
```

## 20. Trying To Fit A Large Scenario Into Too Small A Canvas

Mistake:

```text
The diagram is dense, cramped and full of crossed connectors because the canvas is too small.
```

Why wrong:

```text
Compactness is less important than readability and semantic clarity.
```

Correct approach:

```text
Enlarge the canvas, separate conceptual lanes or split a complex branch into a subscenario page.
```

Review/reject rule:

```text
Reject if the diagram becomes dense just to fit a small canvas.
```

## 21. Text Escaping Outside Shape Boundaries

Mistake:

```text
Text, refs, markers or connector labels overflow outside their shapes or overlap other text.
```

Why wrong:

```text
The diagram becomes unreadable and cannot be accepted as a polished scenario artifact.
```

Bad example:

```text
Text overflows outside End states / Postconditions card.
```

Correct approach:

```text
Increase shape size, reduce text, split content, move details to a note, or use a larger canvas.
```

Review/reject rule:

```text
Reject if text escapes outside a shape, touches borders too closely, refs/markers overlap body text, connector labels overlap node text, or a node is too small for its content.
```

## 22. Light Theme Used Instead Of Approved Dark Theme

Mistake:

```text
Using a light/default theme when the project's approved examples use the dark theme.
```

Why wrong:

```text
The diagram may be semantically acceptable, but it breaks visual consistency across the planning documentation.
```

Bad example:

```text
A scenario diagram with white background and pale nodes when the approved examples use dark grid/background.
```

Correct approach:

```text
Use the approved dark theme from scenario-login-correct-v3 unless the user explicitly asks for light theme.
```

Review/reject rule:

```text
Reject or revise if the diagram uses a light theme without explicit request.
```

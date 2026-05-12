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
include, protected by, summarized as, details or opens subscenario connectors cross the main flow corridor.
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

## 22. Inconsistent Or Low-Contrast Theme

Mistake:

```text
The diagram uses a theme that is inconsistent across pages, unreadable, low contrast, or incompatible with semantic colors.
```

Why wrong:

```text
Theme is visual presentation, not scenario semantics.
Light or dark can be acceptable, but the chosen theme must preserve readability, semantic color meaning and package consistency.
```

Bad examples:

```text
One scenario package mixes light and dark pages without explicit reason.
Semantic colors are unreadable on the chosen background.
Pale nodes make markers and refs hard to read.
The diagram silently changes visual language from page to page.
```

Correct approach:

```text
Use one consistent readable theme per package.
Preserve semantic color meanings.
Ensure text and connector labels are readable on the chosen background.
```

Review/reject rule:

```text
Reject or revise if the theme is inconsistent, low-contrast, unreadable or makes semantic colors ambiguous.
```

## 23. Writing To The Repository Without Explicit Request

Mistake:

```text
Writing generated diagrams, previews, summaries or planning updates into the repository when the user only asked to generate diagrams.
```

Why wrong:

```text
Generated diagrams should be reviewed before being committed.
Diagram generation and repository modification are separate actions.
```

Correct approach:

```text
Generate reviewable artifacts and suggest target paths.
Allowed artifact outputs include draw.io output, SVG/PNG preview output, Markdown summary output, patch proposals and prompts for another agent.
```

Review/reject rule:

```text
Reject or revise if the agent modifies repository files without an explicit repo-write request.
```

## 24. Forcing Proof-Of-Layout When User Asked For Another Mode

Mistake:

```text
The user asks for one scenario page or a scenario package, but the diagram agent refuses and generates only a proof-of-layout page.
```

Why wrong:

```text
Proof-of-layout is useful for calibration, but it is not mandatory for every request.
The agent must follow the user's requested generation mode.
```

Bad example:

```text
User: Generate the extension scenario package.
Agent: I will only create one proof page and wait for approval.
```

Correct approach:

```text
If the user asks for proof / sample / layout test, generate proof only.
If the user asks for one scenario, generate one scenario page.
If the user asks for a package, generate the requested package directly.
Use proof first only when requested or when the task is explicitly a calibration/smoke-test task.
```

Review/reject rule:

```text
Reject if the agent adds a proof step that the user did not request and the task already has an approved style/baseline.
```

## 25. Global Scenario Overview Turned Into Mega-Workflow

Mistake:

```text
The global scenario overview is drawn as one huge workflow containing all steps, branches, includes, invariants and postconditions from SC-01..SC-18.
```

Why wrong:

```text
Global overview is a navigation/area map, not a scenario.
It should help the reader see which scenario pages exist and how they are grouped by user/application area.
```

Bad example:

```text
One giant diagram:
Registration steps -> Login steps -> Request creation branches -> Employee review branches -> Notifications -> Archive,
with all invariants and outcomes connected into one connector web.
```

Correct approach:

```text
Create an area/navigation map.
Group scenario cards by area:
- Guest / Auth Area
- Client Request Area
- Employee Review Area
- Result / Notification / System Area

Show scenario titles, refs, scope markers and short purpose.
Show only high-level relationships between scenario pages.
Do not include detailed scenario internals.
```

Review/reject rule:

```text
Reject if the global overview contains detailed scenario steps, decision branches, includes, invariants, postconditions, acceptance details, implementation details or connector web.
```

## 26. Vague Input Or Filter Step Without Details

Mistake:

```text
A scenario uses a broad step such as "Enter registration data", "Fill request details" or "Filter/search requests" without clarifying what data or criteria matter.
```

Why wrong:

```text
The flow node is too vague for later planning, but making it a huge text block would damage the diagram.
```

Bad example:

```text
Enter registration data
SC-01-STEP-02
```

with no indication of what data is entered.

Correct approach:

```text
Use a compact flow step plus a secondary DETAIL node.

Enter registration data
SC-01-STEP-02

details ->
Registration data details
SC-01-DETAIL-01
- email
- password
- password confirmation
```

For search/filter:

```text
Filter/search requests
SC-05-STEP-03

details ->
Request filter criteria
SC-05-DETAIL-01
- status
- request type
- date/period
```

Review/reject rule:

```text
Reject if important input/filter/search steps remain too vague and no DETAIL node or equivalent clarification is provided.
```

## 27. Validation Error Treated As Final Scenario Failure By Default

Mistake:

```text
Invalid input always leads to a terminal "request/account/document not created" end state.
```

Why wrong:

```text
Most validation errors are correctable. The actor can usually fix the input and continue the same scenario.
```

Bad example:

```text
Request invalid?
-> validation errors visible
-> request not created
```

as the only invalid branch.

Correct approach:

```text
Request invalid?
-> validation errors visible
-> client corrects request details
-> back to Fill request details
```

Keep `not created/not saved/not attached` as invariant or invalid-attempt outcome:

```text
Invalid request is not accepted.
Invalid registration data must not create an account.
Rejected document is not attached.
```

Review/reject rule:

```text
Reject if correctable validation errors are always modeled as terminal scenario failure without a correction loop or clear abandon/cancel path.
```

## 28. "Opened From" Relationship Modeled As Wrong Off-Page Link

Mistake:

```text
A scenario uses a purple off-page link only to explain that it may be opened from another scenario.
```

Why wrong:

```text
Purple/off-page links mean the current scenario leaves to another page/subscenario.
If the issue is how this scenario starts, it is often better represented as multiple entry points with their own preconditions.
```

Bad example:

```text
Extended Applicant Data
-> purple link "opened from Request Creation"
```

when the diagram is trying to explain that applicant data can be reached in more than one way.

Correct approach:

```text
Entry A:
Client opens applicant data page directly.
Precondition: client is signed in and applicant data page is reachable.

Entry B:
Client reaches applicant data from Request Creation — SC-04.
Precondition: request creation is in progress and applicant data is missing or needs update.
```

Review/reject rule:

```text
Reject if purple links are used to describe entry context rather than an actual transition leaving the current scenario.
```

## 29. Filter/Search/Select Collapsed Into One Step

Mistake:

```text
The diagram uses one combined step: "filter/search/select request".
```

Why wrong:

```text
Finding a request and selecting a request are different user behaviors.
Filter/search criteria also need their own detail if they matter for planning.
```

Bad example:

```text
Filter/search/select request
SC-06-STEP-03
```

Correct approach:

```text
Filter/search requests
SC-06-STEP-03

details ->
Employee dashboard filter criteria
SC-06-DETAIL-01

Select request for review
SC-06-STEP-04
```

Review/reject rule:

```text
Reject if filter/search/select are collapsed and the scenario loses the distinction between search support and selecting an item.
```

## 30. Generated Scenario Packages Placed In A Non-Canonical Folder

Mistake:

```text
Generated scenario packages are placed in an unexpected second canonical folder.
```

Why wrong:

```text
Planning agents need one stable source of truth for scenario packages.
Multiple canonical-looking folders create ambiguity.
```

Correct approach:

```text
Use planning/diagrams/ for generated scenario packages, overview and consistency reports.

Use planning/examples/ only for approved canonical examples.
```

Typical scenario package paths:

```text
planning/diagrams/scenario-core-package.drawio
planning/diagrams/scenario-core-package.md
planning/diagrams/scenario-extension-package.drawio
planning/diagrams/scenario-extension-package.md
planning/diagrams/scenario-advanced-package.drawio
planning/diagrams/scenario-advanced-package.md
planning/diagrams/scenario-overview.drawio
planning/diagrams/scenario-overview.md
planning/diagrams/scenario-diagram-consistency-report.md
```

Review/reject rule:

```text
Reject if generated scenario packages are split across competing canonical folders without explicit user instruction.
```

# Diagram Scenario Specification

Status: source of truth for user-facing use-case/scenario diagram semantics  
Scope: scenario/use-case diagrams only, not domain/aggregate/DB/CQRS diagrams

---

## 0. Purpose

This file defines the logic, structure and semantic grammar for user-facing scenario/use-case diagrams.

It does not define draw.io visual construction details such as palette, typography, card implementation, XML structure, or line-by-line text rendering. For those rules, read:

```text
planning/diagram-generation-rules-with-example.md
```

This file answers:

```text
What should scenario diagrams mean?
What belongs on scenario pages?
How should scenario flow, branches, preconditions, postconditions, invariants and outcomes be represented?
```

---

## 1. Core Rule

Scenario/use-case diagrams are behavioral specification diagrams.

They describe:

```text
actor
screen/application context
user goal
preconditions
main flow
decision branches
actor choices
include steps
subscenario links
invariants
step-level postconditions
scenario-level end states
observable outcomes
lightweight planning markers
```

They must not primarily describe:

```text
controller
endpoint
handler
repository
DbContext
EF mapping
SQL table
aggregate method
command/query class
React component/hook
```

Technical trace notes are allowed only as small secondary notes when explicitly useful. They must not dominate the scenario diagram.

Semantic correctness is as important as visual correctness.

A scenario diagram must be semantically correct, not only visually clean.

A visually clean diagram is still wrong if:

```text
- an invariant is attached to the wrong step/state/transition;
- branch type is mislabeled;
- [ALT], [EXT] or [VAR] markers are used incorrectly;
- a precondition duplicates a decision branch;
- a step postcondition is shown as a scenario precondition;
- an off-page subscenario link is drawn as a normal current-flow branch;
- an error branch is marked as [ALT] without reason.
```

---

## 2. Main Scenario Unit

The main unit is:

```text
Actor + application screen/context + user goal
```

Examples:

```text
Guest — Registration screen — register account
Guest — Login screen — sign in
Guest — Password recovery screen — restore access
Client — Request creation screen — submit connection request
Client — Request status page — see request status/result
Employee — Request dashboard — find submitted requests
Employee — Request review screen — approve or reject request
System — Notification processing — notify client about decision
```

A scenario page may include several screens if they are part of one coherent user journey.

Example:

```text
Request creation page
-> applicant data block if missing
-> submit confirmation
-> request status page
```

If a branch has its own user goal or becomes complex, split it into a separate subscenario page.

Examples:

```text
Login scenario
-> Password Recovery Scenario

Request Creation Scenario
-> Request Documents Scenario

Employee Review Scenario
-> Clarification Scenario
```

---

## 3. Scenario Diagram Is Not A Text Spec On Canvas

A scenario diagram must visualize scenario structure.

Bad direction:

```text
large disconnected cards:
- Scenario context
- Main flow
- Preconditions
- Include / Extend
- Invariants
- Acceptance Criteria
- Observable Outcomes
```

This is only a Markdown spec placed into boxes.

Better direction:

```text
Actor/screen node
-> user-facing action nodes
-> system-visible result nodes
-> decision/branch nodes
-> include nodes attached to required steps
-> optional actor-choice/subscenario nodes attached to branch points
-> invariant notes anchored to protected step/state/branch
-> step postconditions/outcomes attached to producing step
-> compact scenario end states
```

The main flow should be the visual backbone.

---

## 4. Mandatory Smoke Test Protocol

Do not ask a diagram agent to generate the full scenario package immediately.

Always start with one proof-of-layout page.

Recommended smoke-test page:

```text
SC-04 Client Request Creation
```

Alternative:

```text
SC-02 Login
```

Prompt requirement:

```text
Generate only one proof-of-layout page first.
Do not generate the full package yet.
The goal is to validate layout, connector routing, density, labels and visual language.
```

The sample must prove:

```text
- main flow is visually obvious;
- the diagram is not just text cards;
- includes are attached to exact required steps;
- branches are connected to exact decision/action points;
- invariants are anchored to protected behavior/state;
- step postconditions/outcomes are attached to producing steps;
- scenario end states are compact;
- connectors are meaningful and readable;
- layout is spacious;
- implementation details are absent.
```

Only after the sample is accepted should the full package be generated.

---

## 5. Recommended Scenario Page Structure

A scenario page should usually contain:

```text
Top:
Scenario title + strict ref + actor/screen/goal

Left:
Actor/screen context and scenario-level preconditions

Center:
Main scenario flow as connected action/result nodes

Branch area:
Decision branches, actor choices and subscenario links attached to exact branch points

Bottom or side:
Includes, invariants, step-level postconditions, observable outcomes and compact end states

Side rail:
Open questions or lightweight planning markers if needed
```

The main flow must dominate the page visually.

Metadata must not compete with the main flow.

---

## 6. Node Types

### 6.1 Actor / Screen Node

Contains:

```text
Actor:
Screen/context:
Goal:
Level marker:
```

Example:

```text
Guest
Login screen
Goal: Sign in
L1
```

---

### 6.2 Main Flow Node

Contains:

```text
short action/result label
strict item ref
small marker if useful
```

Example:

```text
Submit sign-in
SC-02-STEP-03
```

Do not put long paragraphs inside flow nodes.

---

### 6.3 Decision / Branch Node

Represents a conditional result inside the scenario.

Example:

```text
Credentials valid?
SC-02-BR-01
```

Branches:

```text
if valid
if invalid
```

A decision node is not an extension by itself.

---

### 6.4 Actor Choice Node

Represents a deliberate actor choice.

Example:

```text
Forgot password selected
SC-02-EXTND-01
```

Connector labels:

```text
if selected
opens subscenario
```

---

### 6.5 Include Node

Use explicit `<<include>>` for mandatory reusable steps.

Example:

```text
<<include>>
Validate login form
SC-02-INC-01
```

An include must attach to the exact step that requires it.

Do not put all includes into one disconnected summary card.

---

### 6.6 Optional / Subscenario Link Node

Use for optional selected paths, complex branches or linked subscenarios.

Example:

```text
Open subscenario:
Password Recovery — SC-03
```

Use `EXTND` in item refs for these branch/link items.

Do not use `EXT` in item refs because `[EXT]` is reserved for the roadmap marker.

---

### 6.7 Invariant Node

Invariants must attach to the step, state or branch they protect.

Example:

```text
Invariant
No session is issued for invalid credentials
SC-02-INV-01
```

Connector label:

```text
protected by
```

---

### 6.8 Step-Level Postcondition Node

A step-level postcondition is a result produced by a specific step.

Example:

```text
Session is issued
SC-02-SPOST-01
```

or:

```text
Request is stored
SC-04-SPOST-01
```

Attach it to the step that produces it.

---

### 6.9 Observable Outcome Node

Observable outcomes are externally visible or verifiable results.

Examples:

```text
Session is issued
Request is visible in employee queue
Client sees approval result
Email notification is sent
```

Use them only when they add useful verification meaning beyond the immediate flow node.

---

### 6.10 Scenario End States / Postconditions Block

Use a compact block for final scenario states.

Example for Login:

```text
End states / Postconditions

Valid login:
- session exists;
- requested app page opens.

Invalid login:
- no session exists;
- error is visible.

Forgot password:
- password recovery scenario opens.
```

This block should be compact and secondary.

---

### 6.11 Acceptance Criteria

Acceptance criteria remain important in the text scenario spec.

Default visual rule:

```text
Do not draw large standalone acceptance cards by default.
```

A scenario diagram should express acceptance through flow and branches.

If acceptance must be visible, use a compact `End states / acceptance summary` block or attach a very short acceptance note to the exact behavior.

---

## 7. Item Reference Codes

Use readable labels plus strict refs.

Readable first, strict ref second.

Recommended codes:

```text
PRE       scenario-level precondition
SPRE      step-level precondition
STEP      main flow step
BR        decision/branch node
INC       include
EXTND     optional actor choice / extension / subscenario branch item
INV       invariant
POST      scenario-level postcondition/end state
SPOST     step-level postcondition
OUT       observable outcome
AC        acceptance criterion, usually in spec not diagram
Q         open question
```

Examples:

```text
SC-02-PRE-01
SC-02-SPRE-01
SC-02-STEP-03
SC-02-BR-01
SC-02-INC-01
SC-02-EXTND-01
SC-02-INV-01
SC-02-POST-01
SC-02-SPOST-01
SC-02-OUT-01
SC-02-Q-01
```

Important rule:

```text
Use EXTND in item refs.
Reserve [EXT] for roadmap/scope markers.
```

Bad:

```text
SC-02-EXT-01
```

Good:

```text
SC-02-EXTND-01
```

---

## 8. Preconditions

There are two kinds of preconditions.

### 8.1 Scenario-Level Preconditions

Scenario-level preconditions describe facts outside the scenario that must already be true before the scenario starts.

Good examples:

```text
Guest is not signed in.
Login screen is reachable.
Client is signed in.
Employee has access to review dashboard.
```

Usually, preconditions are external to the scenario.

Do not use a precondition for something already expressed by a decision/branch inside the diagram.

Bad example:

```text
Credentials are valid.
```

Why bad:

```text
The diagram already has:
Credentials valid?
-> if valid
-> if invalid
```

So `credentials are valid` is not a scenario precondition. It is a branch condition/result inside the scenario.

### 8.2 Step-Level Preconditions

Some complex steps can have their own preconditions.

Step-level preconditions are allowed when:

```text
- the condition is external to that step;
- the condition is not already better expressed as a decision diamond;
- the condition clarifies why the step can happen;
- the condition comes from previous scenario/system state.
```

Example:

```text
Step:
Set new password

Step-level precondition:
Password recovery token exists and is not expired.
```

But if the condition naturally branches inside the flow, prefer a decision node:

```text
Token valid?
-> if valid: allow password reset
-> if invalid: show expired token message
```

Rule:

```text
Prefer decision branches for conditions that are part of scenario behavior.
Use preconditions for facts that exist before the scenario/step and are not the main behavior being demonstrated.
```

---

## 9. Postconditions

There are two kinds of postconditions.

### 9.1 Scenario-Level Postconditions / End States

Scenario-level postconditions summarize possible final states of the scenario.

For Login:

```text
Valid login:
- session exists;
- requested app page opens.

Invalid login:
- no session exists;
- error is visible.

Forgot password:
- password recovery scenario opens.
```

Show them as a compact `End states / Postconditions` block.

Do not make this block more visually important than the main flow.

### 9.2 Step-Level Postconditions

Complex steps can produce concrete results.

Attach them to the step that creates them.

Examples:

```text
Submit request
-> Request is stored
-> Request status becomes Submitted

Approve request
-> Request status becomes Approved
-> Review decision is recorded

Submit sign-in
-> Session is issued
```

Rule:

```text
Step-level postconditions/outcomes attach to the producing step.
Scenario-level postconditions summarize final end states.
```

---

## 10. Decision Branches, Actor Choices, Errors And Optional Paths

Do not use `extend` as a generic connector label.

Use intuitive connector labels that make sense to readers unfamiliar with UML.

Good connector labels:

```text
if valid
if invalid
if missing
if selected
opens subscenario
requires
results in
visible as
protected by
```

Avoid vague UML-ish labels:

```text
extend
related
link
```

unless explicitly needed.

---

### 10.1 Decision Node

Decision nodes represent conditional outcomes inside the scenario.

Example:

```text
Credentials valid?
-> if valid: Session is issued
-> if invalid: Show sign-in error
```

This is normal scenario behavior, not an extension.

---

### 10.2 Actor Choice

Actor choice is when the actor deliberately chooses a different action/path.

Example:

```text
Guest selects "Forgot password"
-> opens subscenario Password Recovery — SC-03
```

Use labels:

```text
if selected
opens subscenario
```

---

### 10.3 Error / Negative Outcome Branch

Error handling can be required current behavior.

Do not automatically mark errors as `[ALT]`.

Example:

```text
Invalid credentials
-> show sign-in error
-> no session is issued
```

This is a required negative outcome branch.

Default visual treatment:

```text
red error/negative branch + clear connector label, e.g. "if invalid"
```

Use `[CORE]` only when it is important to emphasize that this error handling is mandatory target behavior.

Do not use `[ALT]` just because the branch is not the success path.

---

### 10.4 Narrow `[ALT]` Rule

Use `[ALT]` narrowly.

`[ALT]` means:

```text
A planned alternative way for the actor to achieve the same or equivalent user goal
when the normal/current step is not suitable.
```

Use `[ALT]` only when all of these are true:

```text
1. There is a current intended step/path.
2. That step/path is not suitable or not available in this situation.
3. Another path lets the actor still reach the same or equivalent goal.
4. The alternative path belongs to the current target behavior, not just to a future roadmap idea.
```

Good example:

```text
Applicant data already exists
-> reuse saved applicant data.

Applicant data missing
-> provide applicant data inline. [ALT]
```

Reason:

```text
Both paths allow the actor to continue request creation.
```

Another good example:

```text
Client cannot complete applicant data inline
-> open profile/applicant data page
-> return to request creation. [ALT]
```

Reason:

```text
The normal inline step is not suitable, but another path still supports the same goal.
```

Do not use `[ALT]` for:

```text
- every invalid/error branch;
- every optional actor action;
- every linked subscenario;
- every future feature;
- every branch in a decision diamond.
```

For Login:

```text
Invalid credentials -> show sign-in error
```

is not `[ALT]`. It is an error/negative outcome branch.

```text
Forgot password selected -> opens Password Recovery — SC-03
```

is usually not `[ALT]` for Login, because the actor is no longer achieving the same immediate goal of signing in with credentials. It is better represented as:

```text
optional actor choice / recovery subscenario link
```

Use `[ALT]` for password recovery only if the project explicitly treats recovery as an alternative way to achieve the same login/access goal in the current target behavior.

Default rule:

```text
If unsure, do not use [ALT].
Use clear branch labels instead.
```

---

### 10.5 Roadmap `[EXT]` Marker

`[EXT]` is a roadmap/scope marker.

It means:

```text
additional behavior/capability that is not required for core
```

Examples:

```text
Upload documents. [EXT:L2]
Anonymous request. [EXT:L3]
Reliable outbox delivery. [EXT:L3]
```

`[EXT]` is not a connector label and not an item-ref code.

Use it as a small marker only when a behavior is truly future/additional.

---

## 11. Include Semantics

`<<include>>` means a mandatory reusable step or subscenario required for the current scenario to complete.

Examples:

```text
<<include>> Validate registration form
<<include>> Save account
<<include>> Validate login form
<<include>> Validate request form
```

Visual rules:

```text
- attach include to the exact step that requires it;
- do not put all includes into one disconnected summary card;
- keep include nodes compact;
- use <<include>> explicitly.
```

Example:

```text
Submit sign-in
-> <<include>> Validate login form
```

---

## 12. Invariant Semantics

Invariants must be anchored to the step, state or branch they protect.

An invariant must be attached to the step, decision, transition or state where the rule is enforced.

Do not attach an invariant to a late consequence state if enforcement happened earlier.

Good:

```text
Invalid credentials branch
-- protected by -->
No session is issued for invalid credentials.
```

Better for Login:

```text
Credentials valid?
-- protected by -->
Session is issued only for valid credentials.
```

or:

```text
Transition to Session is issued
-- protected by -->
Session is issued only for valid credentials.
```

Bad:

```text
Remain unauthenticated
-- protected by -->
No session for invalid credentials.
```

Why bad:

```text
The invariant is attached to a late consequence state.
The rule is enforced at the credentials decision or the transition into Session is issued.
```

Good:

```text
Submit request
-- protected by -->
Invalid request is not accepted.
```

Bad:

```text
Login scenario contains:
Protected app page is accessible only after authentication.
```

Why bad:

```text
That invariant belongs better to a Protected Resource Access scenario,
where the actor tries to open a protected page.
```

Rule:

```text
Do not place unrelated global invariants on a scenario page.
Place invariants where the protected behavior/state is visible in the flow.
```

---

## 13. Acceptance Criteria In Diagrams

Acceptance criteria remain part of the scenario spec.

But diagrams should usually not draw large acceptance cards.

Reason:

```text
The flow itself should express acceptance.
```

Example:

```text
valid credentials -> session issued -> redirect
invalid credentials -> error -> no session
forgot password -> password recovery path
```

This already expresses the behavioral acceptance for Login.

Default rule:

```text
Do not draw large standalone acceptance cards by default.
```

If acceptance needs to be shown visually, use:

```text
- compact End states / acceptance summary block;
- or a short acceptance note attached to exact behavior.
```

Do not let acceptance blocks compete with the main flow.

---

## 14. Observable Outcomes

Observable outcomes are externally visible or verifiable results.

Allowed examples:

```text
account is saved
session is issued
applicant data is saved
request is stored
request status becomes Submitted
employee sees request in queue
client sees approval or rejection result
email notification is sent
invalid input does not create saved data
```

Frame outcomes as observable behavior, not low-level implementation.

Bad:

```text
Command: CreateConnectionRequest
Domain: ConnectionRequest
Writes: ClientRequests
```

Good:

```text
Client submits request
Outcome: request is stored and visible with Submitted status
```

Optional small trace note:

```text
Trace: request stored as connection request
```

Only use trace notes when they help and do not dominate the diagram.

---

## 15. Marker Model For Diagrams

Use lightweight markers from the planning workflow.

Scope / roadmap:

```text
[CORE]    required target behavior
[ALT]     narrow alternative path; see narrow ALT rule
[EXT]     additional scenario/slice/capability, not required for core
[DEFER]   known but intentionally postponed
```

Change / volatility:

```text
[VAR:EXPAND]    current model may gain more variants
[VAR:MODIFY]    current rule/workflow/acceptance may change
[VAR:REPLACE]   implementation/provider/strategy may be replaced
[VAR:REMOVE]    behavior may be removed/deprecated; use rarely
```

Risk / decision:

```text
[RISK]    uncertainty or risk
[ADR?]    likely architecture decision needed later
```

Important distinction:

```text
[EXT] = what can be added.
[VAR] = what can change in existing behavior/model/rule/workflow/provider/strategy.
```

Use markers sparingly.

Bad:

```text
Every node has [CORE][VAR][ADR?].
```

Good:

```text
Applicant data missing? [ALT][VAR:EXPAND]
Upload documents [EXT:L2]
Clarification [EXT:L2][VAR:MODIFY][ADR?]
```

Markers should clarify planning, not overwhelm the diagram.

Do not remove planning markers entirely.

Use markers selectively but visibly.

Recommended marker visibility:

```text
[CORE] may be used on key main-flow nodes and key required branches.
[ALT], [EXT], [VAR:*], [RISK] and [ADR?] should be used only where they add planning meaning.
```

Markers are not visual type.

```text
Color = visual role in this diagram.
Marker = planning meaning.
Connector label = semantic relationship.
```

---

## 16. Level Interpretation For Scenario Diagrams

Use levels as final target scenario levels, not as current code state.

```text
L1 = final MVP user-facing scenarios
L2 = extended diploma workflow branches/scenarios
L3 = advanced/future scenario branches or cross-cutting capabilities
```

Important:

```text
Do not interpret L1 as the current implemented code subset.
```

For scenario diagrams:

```text
L1 is the main MVP path.
L2 is optional or extended workflow around the same scenario.
L3 is future or cross-cutting capability related to the scenario.
```

Scenario diagrams should normally show the final target behavior for the selected scenario, not three separate versions of the same scenario by level.

L1/L2/L3 labels are roadmap and implementation-planning metadata.

They may be shown as subtle badges, small labels, border accents or legend markers, but they must not dominate the scenario.

The primary reading order must remain user-facing:

```text
actor -> screen/context -> user goal -> main flow -> branches -> observable outcomes/end states
```

Do not generate separate V1/V2/V3 versions of the same scenario unless explicitly requested.

---

## 17. Applicant Data Naming

Do not use this as the primary user-facing label:

```text
Create individual applicant profile
```

Use:

```text
Provide individual applicant data
```

Reason:

```text
The user provides applicant data.
The system may store it internally as applicant data / ApplicantParty,
but the user-facing scenario is about providing data.
```

There are two valid UX entry points.

### 17.1 Profile-First

```text
Client opens profile/applicant data page
-> provides individual applicant data
-> later starts request
-> system reuses saved applicant data
```

### 17.2 Request-First / Inline

```text
Client starts request
-> system sees applicant data is missing
-> request form asks for applicant data inline
-> system saves applicant data
-> request submission continues
```

Do not duplicate the domain concept in user-facing diagrams.

The scenario is about providing applicant data, not about creating an `ApplicantParty`.

For this specific case, inline applicant data can be `[ALT]` because it is an alternative way to continue the same request-creation goal when saved applicant data is missing.

---

## 18. Recommended Login Scenario Grammar

Use this as a reference for the intended scenario style.

```text
Actor / Screen:
Guest — Login screen

Scenario preconditions:
- Guest is not signed in.
- Login screen is reachable.

Main flow:
Guest opens login screen [CORE]
-> enters credentials [CORE]
-> submits sign-in [CORE]
-> <<include>> Validate login form [CORE]
-> Credentials valid? [CORE]
   -> if valid:
      Session is issued [CORE]
      Redirect to requested app page [CORE]
   -> if invalid:
      Show sign-in error [CORE or unmarked required negative branch]
      Remain unauthenticated [CORE or unmarked required negative branch]

Off-page transition:
Forgot password selected
-> opens subscenario Password Recovery - SC-03

Item ref:
SC-02-EXTND-01
```

Side constraints:

```text
Invariant:
Session is issued only for valid credentials.

Attach to:
- Credentials valid?
- or the transition into Session is issued.

Step-level postcondition:
Session is issued.
Attach to valid branch / submit sign-in result.

Scenario end states:
Valid login -> session exists, app page opens.
Invalid login -> no session, error shown.
Forgot password -> recovery path opens.
```

Do not add:

```text
Credentials are valid
```

as a precondition.

Do not add:

```text
Protected page requires authentication
```

as an invariant on this scenario unless the scenario is explicitly about protected page access.

Do not mark invalid credentials as `[ALT]`.

Do not mark forgot password as `[ALT]` by default.

Forgot password should use:

```text
SC-02-EXTND-01
```

because it is an off-page/subscenario branch item.

---

## 19. Color Guidance For Scenario Semantics

Use this distinction:

```text
Color = visual role in this diagram.
Marker = planning meaning.
Connector label = semantic relationship.
```

Use this palette meaning:

```text
Green  = current scenario flow / success path / normal in-page behavior
Red    = negative, invalid, error, rejection or failure branch shown on this page
Purple = off-page transition / subscenario link only
Blue   = decision node, compact end-state block, observable summary
Cyan   = include / mandatory supporting step
Yellow = invariant / rule / constraint
Olive  = precondition
Gray   = actor/screen/context, metadata, legend
```

Important purple rule:

```text
Purple does not mean actor choice in general.
Purple means the path leaves the current scenario page.
```

Use purple only for:

```text
- subscenario link;
- off-page transition;
- placeholder for another scenario page;
- branch not expanded here.
```

Do not color normal in-page actor choices purple.

If an actor choice continues inside the current scenario, color it according to its role:

```text
green for normal/current flow;
red for negative branch;
blue for decision/summary if appropriate.
```

For Login:

```text
Forgot password selected -> Password Recovery - SC-03
```

is purple because it is an off-page subscenario transition, not because it is an actor choice.

Markers clarify planning meaning:

```text
[CORE]
[ALT]
[EXT:L2]
[VAR:EXPAND]
[VAR:MODIFY]
[RISK]
[ADR?]
```

Do not rely on markers alone for visual type.

---

## 20. Connector Semantics

Every connector must mean something.

Useful connector labels:

```text
starts
next
requires
include
if selected
opens subscenario
if valid
if invalid
if missing
use saved data
provide inline
results in
visible as
protected by
notifies
```

Avoid vague labels:

```text
then
link
related
extend
```

unless there is a clear reason.

Connector rules:

```text
- do not route connectors through shape bodies;
- do not route connectors through body text;
- do not stack several connectors on the same side of one shape;
- distribute anchors across shape sides;
- avoid overlapping connectors;
- if routing becomes messy, split the page or create a subscenario page.
```

---

## 20A. Lane-Based Layout And Anti-Overlap Rules

Scenario diagrams must use lane discipline.

The goal is to prevent connector overlap, shape overlap, visual noise and unclear relationships.

A scenario page should not be optimized for compactness. A larger spacious page is preferred over a dense page.

Lane discipline is conceptual.

Visible lane guide lines are optional and should not be drawn by default in final scenario diagrams.

Use visible lane lines only for:

```text
- proof-of-layout/debug examples;
- internal layout review;
- cases where the user explicitly wants visible lanes.
```

Do not require visible lane lines for all scenario diagrams.

Lane discipline should still be followed:

```text
- keep main flow clean;
- keep error branch separate;
- keep off-page/subscenario links separate;
- keep invariants local;
- keep end-state summary compact.
```

---

### 20A.1 Layout Lanes

Use separate visual lanes for different semantic roles.

Recommended lanes:

```text
Main flow lane:
primary scenario path, usually left-to-right or top-to-bottom.

Success/result lane:
successful result nodes after decisions.

Negative/error lane:
invalid/error/negative outcome branches.

Actor-choice/subscenario lane:
optional actor choices and links to subscenario pages.

Constraint lane:
invariants and required constraints attached to the behavior they protect.

Summary lane:
compact scenario end states / postconditions.
```

These lanes do not need to be visibly drawn as swimlanes, but the layout must respect them.

Bad:

```text
main flow, error flow, recovery path, invariant and end-state links all mixed in the same area
```

Good:

```text
main flow remains clean;
error branch has its own area;
subscenario link has its own area;
invariant is local to the branch it protects;
end states are compact and do not create connector web.
```

---

### 20A.2 Main Flow Must Stay Clean

The main flow is the visual backbone.

Rules:

```text
- Do not route secondary connectors across the main flow.
- Do not place invariants, end-state summaries or subscenario links inside the main flow corridor.
- Do not allow dashed summary/protection links to visually compete with primary next-step links.
- If a secondary connector would cross the main flow, move the node closer, move it to a side lane, or remove the connector.
```

Primary connectors:

```text
starts
next
checks
if valid
if invalid
results in
```

Secondary connectors:

```text
include
protected by
summarized as
visible as
opens subscenario
condition for
```

Secondary connectors must stay short and local whenever possible.

---

### 20A.3 Error / Negative Branch Lane

Negative branches should have their own clear lane.

Example:

```text
Credentials valid?
-> if invalid:
   Show sign-in error
   -> Remain unauthenticated
   -> local invariant near decision: Session is issued only for valid credentials
```

Rules:

```text
- Put negative/error nodes together.
- Do not mix error branch nodes with actor-choice/subscenario nodes.
- Do not route recovery/subscenario links through the error lane.
- Attach error-related invariants close to the error branch.
```

---

### 20A.4 Actor-Choice / Subscenario Lane

Actor-choice and subscenario links should be visually separated from error/result branches.

Example:

```text
Forgot password selected
-> opens subscenario Password Recovery - SC-03
```

Rules:

```text
- Put optional actor choices in a separate lower or side lane.
- Keep the connector from the source step to the actor choice simple.
- Use labels like "if selected" and "opens subscenario".
- Do not use generic "extend" connector labels.
- Do not route subscenario links through error branches, invariants or end-state blocks.
```

---

### 20A.5 Invariant Placement

Invariants must be local.

Rules:

```text
- Place invariant nodes near the step, decision, transition, state or branch where the rule is enforced.
- Use a short "protected by" connector.
- Do not place unrelated global invariants on the page.
- Do not stretch invariant connectors across half the diagram.
- If the invariant protects another scenario, move it to that scenario.
```

Good:

```text
Credentials valid?
-> protected by
Invariant: Session is issued only for valid credentials
```

Bad:

```text
Login scenario contains a far-away invariant:
Protected app page is accessible only after authentication.
```

That belongs to a protected-resource-access scenario.

---

### 20A.6 End States / Postconditions Placement

Scenario-level end states are usually a compact summary.

Default rule:

```text
Do not draw long connectors from every final branch to the end-states block.
```

Prefer:

```text
- one compact End states / Postconditions block;
- no connectors if the block is obviously a summary;
- or only very short local connectors when they do not create visual noise.
```

Reject the layout if:

```text
- end-state summary links cross the main flow;
- summary links create a web around the page;
- several dashed summary links overlap each other;
- the summary block becomes visually more important than the scenario flow.
```

---

### 20A.7 Include Placement

Include nodes must be attached to the exact required step.

Rules:

```text
- Put include nodes directly above, below or near the step that requires them.
- Use a short "include" connector.
- Do not place all includes in a disconnected summary card.
- Do not route include connectors through other nodes.
```

Example:

```text
Submit sign-in
-> include
<<include>> Validate login form
```

---

### 20A.8 Connector Anchor Rules

Before drawing a connector, choose the least congested side of the source and target nodes.

Rules:

```text
- Do not stack multiple unrelated connectors on the same side of one shape.
- Use different sides when a node has multiple relationships.
- Use top/bottom anchors for vertical relationships.
- Use left/right anchors for horizontal flow.
- Keep connector labels away from node text and other connector labels.
- Do not route connectors through shape bodies.
- Do not route connectors through text inside shapes.
- Do not allow connector labels to overlap lines or nodes.
```

If a node needs many connectors, either:

```text
- split the node;
- move related nodes closer;
- convert a distant connector into a local note;
- create a subscenario page;
- enlarge the canvas.
```

---

### 20A.9 Long Connector Rule

Long connectors are allowed only when they are necessary and visually clean.

Avoid long connectors for:

```text
- summarized as
- protected by
- condition for
- visible as
- opens subscenario
```

These should usually be short and local.

If a long connector is needed, it must:

```text
- not cross the main flow;
- not cross node bodies;
- not overlap another connector;
- have a clear label;
- use a free visual lane.
```

---

### 20A.10 When To Remove A Connector

Not every relationship needs a drawn connector.

Remove or avoid a connector when:

```text
- the relation is obvious from placement and title;
- the connector creates visual noise;
- the connector would cross the main flow;
- the connector would create a web of summary links;
- the connector is only decorative.
```

Example:

```text
End states / Postconditions
```

can often be shown as a compact block without connectors.

---

### 20A.11 Anti-Overlap Rejection Criteria

Reject or revise a diagram if:

```text
- any connector passes through a shape body;
- any connector passes through body text;
- connector labels overlap each other;
- connector labels overlap shapes;
- more than two unrelated connectors visually merge into one corridor;
- secondary connectors cross the main flow;
- invariants are far from the protected branch/state;
- end-state summary links create a web;
- actor-choice/subscenario links collide with error branches;
- the diagram becomes dense just to fit a small canvas.
```

The correct fix is usually:

```text
- enlarge the canvas;
- separate lanes;
- move secondary nodes closer to their source;
- remove unnecessary summary connectors;
- split a complex branch into a subscenario page.
```

---

### 20A.12 Login Layout Example Rule

For Login-like scenarios, prefer this layout shape:

```text
Top / main lane:
Actor/Screen -> Open login screen -> Enter credentials -> Submit sign-in -> Credentials valid?

Success lane:
Credentials valid? -> if valid -> Session is issued -> Redirect to requested app page

Error lane:
Credentials valid? -> if invalid -> Show sign-in error -> Remain unauthenticated
Credentials valid? -> protected by -> Session is issued only for valid credentials

Actor-choice/subscenario lane:
Open login screen -> if selected -> Forgot password selected -> opens subscenario Password Recovery - SC-03

Summary lane:
End states / Postconditions as compact block, usually without long connectors.
```

Do not mix:

```text
forgot password path
error branch
invariant
end-state summary links
```

in the same crowded lower-center area.

---

## 21. Scenario Page Splitting Rules

Create a separate subscenario page when:

```text
- branch has its own user goal;
- branch has more than 3-4 meaningful steps;
- branch introduces a separate actor/screen;
- branch has its own acceptance criteria;
- branch has its own extension/change markers;
- connectors become crowded;
- page starts looking like a command/domain/table map.
```

Examples:

```text
Login -> Password Recovery — SC-03
Request Creation -> Request Documents — SC-11
Employee Review -> Clarification — SC-12
Approval -> Reliable Notification Delivery — SC-16
```

Prefer scenario pages over one giant all-system map.

Large canvas is allowed.

A clean large diagram is better than a dense small one.

---

## 22. Recommended Scenario Page Package

### Overview

```text
00 Scenario Overview / Navigation Map
```

### Core Scenario Pages

```text
01 Guest Registration Scenario
02 Login Scenario
03 Password Recovery Scenario
04 Client Request Creation Scenario
05 Client Request Status / Result Scenario
06 Employee Request Dashboard Scenario
07 Employee Request Review Scenario
08 Approval Result: Contract Draft + Email Notification
09 Rejection Result Scenario
```

### Extension Scenario Pages

```text
10 Extended Applicant Data Scenario
11 Request Documents Scenario
12 Clarification Scenario
13 Contract Acknowledgement Scenario
14 Mock Verification Scenario
```

### Advanced / Cross-Cutting Scenario Pages

```text
15 Security / Account Protection Scenario
16 Reliable Notification Delivery Scenario
17 Anonymous Request Scenario
18 Archive / Audit Scenario
```

---

## 23. Recommended Generation Batches

### First Batch

```text
00 Scenario Overview
01 Guest Registration Scenario
02 Login Scenario
04 Client Request Creation Scenario
06 Employee Request Dashboard Scenario
07 Employee Request Review Scenario
```

### Second Batch

```text
03 Password Recovery Scenario
05 Client Request Status / Result Scenario
08 Approval Result
09 Rejection Result
10 Extended Applicant Data
11 Request Documents
12 Clarification
```

### Third Batch

```text
13 Contract Acknowledgement
14 Mock Verification
15 Security / Account Protection
16 Reliable Notification Delivery
17 Anonymous Request
18 Archive / Audit
```

But before any batch generation:

```text
Generate one proof-of-layout page first.
```

---

## 24. Review Checklist

Accept a sample page only if:

```text
- it is understandable without reading a separate spec;
- it looks like scenario flow, not text cards;
- main flow is visually obvious;
- includes are attached to exact required steps;
- decision branches are clear;
- optional actor choices/subscenario links are clear;
- invariants are connected to the enforcement point they protect;
- step-level postconditions/outcomes are attached to producing steps;
- scenario end states are compact;
- connectors do not overlap shape bodies;
- connectors do not all attach to one side;
- there is enough whitespace;
- markers are present only where useful;
- implementation details are absent.
```

Reject or revise if:

```text
- the diagram is mostly large text blocks;
- connectors are decorative rather than meaningful;
- labels are too small;
- page is too dense;
- branches are crammed into the main page;
- old implementation concepts appear;
- [ALT] is used for normal errors;
- EXT is used in item refs instead of EXTND.
- secondary connectors cross the main flow;
- end-state summary links create a web around the page;
- invariants are far from the branch/state they protect;
- actor-choice/subscenario links collide with error branches;
- more than two unrelated connectors visually merge into one corridor;
- the diagram becomes dense just to fit a small canvas.
```

---

## 25. Prompt Snippet For Future Diagram Agents

```text
Create a scenario/use-case flow diagram, not a text-card summary.

Generate one proof-of-layout page first.
Do not generate the full package until the sample page is accepted.

Semantic correctness and visual correctness are both required.

The main flow must be the visual backbone.

Use intuitive connector labels instead of UML jargon.
Do not label optional links as "extend" unless explicitly needed.
Use labels such as:
- if selected
- opens subscenario
- if valid
- if invalid
- if missing
- results in
- protected by

Use EXTND in item IDs for extension/optional branch items.
Reserve [EXT] for roadmap/scope markers.

Do not mark every error path as [ALT].
Error/invalid branches are usually negative outcome branches, not ALT.

Use [ALT] narrowly:
only when a normal step/path is not suitable and another path lets the actor still achieve the same or equivalent goal.

Use purple only for off-page/subscenario links.
Do not use purple for normal in-page actor choices.

Do not create preconditions that are already represented by decision diamonds.
Preconditions are usually external facts before the scenario or step.

Support both scenario-level and step-level postconditions.
Attach step-level postconditions/outcomes to the step that produces them.
Show scenario-level postconditions as compact end states.

Invariants must attach to the enforcement point: the step, decision, transition or state where the rule is protected.
Do not place unrelated global invariants on a scenario page.

Do not draw large standalone acceptance cards by default.
Let the scenario flow express acceptance.
Use a compact end-state/acceptance summary only if needed.

Use readable labels plus strict refs.
Keep layout spacious.
Avoid connector overlaps.
Do not route connectors through shape bodies.
Distribute connector anchors across sides.
Avoid implementation details.

Use lane discipline:
- main flow lane;
- success/result lane;
- negative/error lane;
- actor-choice/subscenario lane;
- constraint/invariant lane;
- compact summary/end-state lane.

Do not let secondary connectors cross the main flow.
Do not create long summary connector webs.
Place invariants near the branch/state they protect.
Prefer removing summary connectors over creating visual noise.
Enlarge the canvas instead of making the diagram dense.
```

# Diagram Scenario Specification

Status: source of truth for user-facing use-case/scenario diagram semantics  
Scope: scenario/use-case diagrams only, not domain/aggregate/DB/CQRS diagrams

---

## 0. Purpose

This file defines the logic, structure and semantic grammar for user-facing scenario/use-case diagrams.

It does **not** define draw.io visual construction details such as typography, XML structure, line-by-line text rendering, theme selection or palette tuning. For those rules, read:

```text
planning/diagram-generation-rules-with-example.md
```

For prompt protocol, read:

```text
planning/diagram-prompting-guide.md
```

For common mistakes and reject criteria, read:

```text
planning/diagram-common-mistakes.md
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
provider API
outbox table
database table
background job implementation
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
- an error branch is marked as [ALT] without reason;
- implementation details dominate the page.
```

Visual correctness is also required:

```text
- one consistent readable theme is used within a package;
- semantic colors preserve their meanings and remain readable;
- main flow is clear;
- no text-card-only diagrams;
- connectors do not overlap;
- text fits inside shapes;
- no connector web;
- layout is spacious.
```

Theme is visual presentation, not scenario semantics.

Scenario diagrams may use a light or dark theme. The selected theme must be consistent, readable and compatible with semantic colors.

---

## 2. Generation Mode Note

This specification defines scenario semantics. It does not force one generation workflow for every prompt.

Common generation modes:

```text
1. Proof-of-layout only
2. Single scenario page
3. Scenario package
4. Global scenario overview / navigation map
5. Documentation-only or prompt-only task
```

Use the mode requested by the user.

Proof-of-layout is useful, but it is not mandatory for every request.

Do **not** automatically add a proof step when:

```text
- the user asked for one scenario page;
- the user asked for a package;
- the user asked for a global overview;
- an approved style/baseline already exists;
- the task is not explicitly a calibration/smoke-test task.
```

Use proof-of-layout when:

```text
- the user explicitly asks for proof / sample / one-page layout first;
- the diagram type is new or not yet calibrated;
- the visual style is new or disputed;
- no approved example exists;
- the scenario is complex, uncertain or likely to produce connector/layout problems;
- a full package would be expensive to redo if the layout is wrong.
```

When a proof page is requested, it should test:

```text
- main flow shape;
- semantic correctness;
- connector routing;
- lane discipline;
- text density;
- visual style;
- anti-overlap rules;
- marker usage.
```

---

## 3. Canonical Scenario Artifact Locations

Use these folder roles:

```text
planning/examples/
= approved canonical examples

planning/diagrams/
= canonical generated scenario diagram packages, scenario overview and consistency reports
```

Canonical generated scenario packages should be placed under `planning/diagrams/` when the user explicitly asks to update the repository.

Typical generated scenario artifacts:

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

Do not use a second canonical folder for the same scenario packages.

Generated artifacts may suggest these paths, but must not write repository files unless the user explicitly asks for repository modification.

---

## 4. Main Scenario Unit

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

## 5. Scenario Diagram Is Not A Text Spec On Canvas

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

## 6. Global Scenario Overview

A global scenario overview is **not** one unified scenario.

It is a scenario-area navigation map.

Suggested target files:

```text
planning/diagrams/scenario-overview.drawio
planning/diagrams/scenario-overview.md
```

Purpose:

```text
Show what scenario pages exist.
Show which user/application area they belong to.
Show only high-level relationships between scenario pages.
```

It should group scenarios by areas such as:

```text
Guest / Auth Area
Client Request Area
Employee Review Area
Result / Notification / System Area
```

Scenario cards in the overview should show:

```text
Scenario title
Scenario ref
Scope marker
Short purpose
```

Example:

```text
Client Request Creation
SC-04
[CORE]
Submit connection request
```

Do not include detailed scenario internals on the global overview:

```text
no detailed steps
no decision branches
no includes
no invariants
no postconditions
no acceptance details
no implementation details
no connector web
```

The overview may show high-level links such as:

```text
SC-01 Registration -> SC-02 Login
SC-02 Login -> SC-03 Password Recovery
SC-04 Request Creation -> SC-05 Request Status / Result
SC-06 Employee Dashboard -> SC-07 Employee Review
SC-07 Employee Review -> SC-08 Approval Result
SC-07 Employee Review -> SC-09 Rejection Result
SC-08 / SC-09 -> SC-16 Reliable Notification Delivery
```

If the map becomes crowded, remove links and mention omitted relationships in the Markdown summary.

---

## 7. Recommended Scenario Page Structure

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

## 8. Node Types

### 8.1 Actor / Screen Node

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

### 8.2 Main Flow Node

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

### 8.3 Decision / Branch Node

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

### 8.4 Actor Choice Node

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

### 8.5 Include Node

Use explicit `<<include>>` for mandatory reusable steps.

Example:

```text
<<include>>
Validate login form
SC-02-INC-01
```

An include must attach to the exact step that requires it.

Do not put all includes into one disconnected summary card.

### 8.6 Optional / Subscenario Link Node

Use for optional selected paths, complex branches or linked subscenarios.

Example:

```text
Open subscenario:
Password Recovery — SC-03
```

Use `EXTND` in item refs for these branch/link items.

Do not use `EXT` in item refs because `[EXT]` is reserved for the roadmap marker.

### 8.7 Invariant Node

Invariants must attach to the step, state or branch they protect.

Example:

```text
Invariant
Session is issued only for valid credentials
SC-02-INV-01
```

Connector label:

```text
protected by
```

### 8.8 Step-Level Postcondition Node

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

### 8.9 Observable Outcome Node

Observable outcomes are externally visible or verifiable results.

Examples:

```text
Session is issued
Request is visible in employee queue
Client sees approval result
Email notification is sent
```

Use them only when they add useful verification meaning beyond the immediate flow node.

### 8.10 Details / Criteria Node

Use a `DETAIL` node for user-visible form, input, search or filter details that would make a flow node too large.

Examples:

```text
Registration data details
SC-01-DETAIL-01

Request filter criteria
SC-05-DETAIL-01

Employee dashboard filter criteria
SC-06-DETAIL-01
```

A details/criteria node may describe:

```text
- fields the actor must provide;
- user-visible validation inputs;
- filter/search criteria;
- document criteria;
- contact/follow-up data;
- variants that may expand later.
```

It must not describe:

```text
- DTO fields as implementation;
- database columns;
- entity class properties;
- API contract internals;
- React component state.
```

Attach the details node to the exact flow step it clarifies.

Example:

```text
Enter registration data
SC-01-STEP-02

details ->
Registration data details
SC-01-DETAIL-01
- email
- password
- password confirmation
[VAR:EXPAND]
```

Use details nodes to make vague steps more precise without turning the main flow node into a large text card.

### 8.11 Scenario End States / Postconditions Block

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

### 8.12 Acceptance Criteria

Acceptance criteria remain important in the text scenario spec.

Default visual rule:

```text
Do not draw large standalone acceptance cards by default.
```

A scenario diagram should express acceptance through flow and branches.

If acceptance must be visible, use a compact `End states / acceptance summary` block or attach a very short acceptance note to the exact behavior.

---

## 9. Item Reference Codes

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
DETAIL    details / criteria node
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
SC-02-DETAIL-01
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

## 10. Preconditions

There are two kinds of preconditions.

### 10.1 Scenario-Level Preconditions

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

### 10.2 Step-Level Preconditions

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

### 10.3 Multiple Entry Points

Some scenarios can be opened from more than one context.

Do not automatically represent every "opened from" relationship as a purple off-page link inside the scenario.

If a scenario has distinct entry contexts, prefer explicit entry points with their own preconditions/context.

Example:

```text
Entry A:
Client opens applicant data page directly.
Precondition: client is signed in and applicant data screen is reachable.

Entry B:
Client reaches applicant data from Request Creation — SC-04.
Precondition: request creation is in progress and applicant data is missing or needs update.
```

Use purple off-page links only when the current page actually opens or leaves to another scenario.

---

## 11. Postconditions

There are two kinds of postconditions.

### 11.1 Scenario-Level Postconditions / End States

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

### 11.2 Step-Level Postconditions

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

## 12. Decision Branches, Actor Choices, Errors And Optional Paths

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
details
```

Avoid vague UML-ish labels:

```text
extend
related
link
```

unless explicitly needed.

### 12.1 Decision Node

Decision nodes represent conditional outcomes inside the scenario.

Example:

```text
Credentials valid?
-> if valid: Session is issued
-> if invalid: Show sign-in error
```

This is normal scenario behavior, not an extension.

### 12.2 Actor Choice

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

### 12.3 Error / Negative Outcome Branch

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

### 12.4 Correctable Validation Errors

Validation errors often do not mean that the whole scenario has failed.

If validation errors are correctable, show the correction loop:

```text
invalid data
-> validation errors are visible
-> actor corrects input
-> back to input step
```

Keep `not saved`, `not created`, `not attached` or `not submitted` as:

```text
- invariant;
- invalid-attempt outcome;
- compact end-state summary;
```

not always as the final end of the whole scenario.

Use a final negative end state only when the user abandons the scenario or the invalid attempt itself is being summarized.

### 12.5 Narrow `[ALT]` Rule

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

### 12.6 Roadmap `[EXT]` Marker

`[EXT]` is a roadmap/scope marker.

It means:

```text
additional behavior/capability that is not required for core
```

Examples:

```text
Upload documents. [EXT:L2]
Anonymous request. [EXT:L3]
Reliable notification delivery. [EXT:L3]
```

`[EXT]` is not a connector label and not an item-ref code.

Use it as a small marker only when a behavior is truly future/additional.

---

## 13. Include Semantics

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

## 14. Invariant Semantics

Invariants must be anchored to the step, state or branch they protect.

An invariant must be attached to the step, decision, transition or state where the rule is enforced.

Do not attach an invariant to a late consequence state if enforcement happened earlier.

Good:

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
Submit request / Request valid?
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

## 15. Acceptance Criteria In Diagrams

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

## 16. Observable Outcomes

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

## 17. Marker Model For Diagrams

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

## 18. Level Interpretation For Scenario Diagrams

Use levels as final target scenario levels, not as current code state.

```text
L1 = final MVP user-facing scenarios
L2 = extended workflow branches/scenarios
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

## 19. Applicant Data Naming

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

### 19.1 Profile-First

```text
Client opens profile/applicant data page
-> provides individual applicant data
-> later starts request
-> system reuses saved applicant data
```

### 19.2 Request-First / Inline

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

## 20. Recommended Login Scenario Grammar

Use this as a reference for the intended scenario style.

Approved canonical example files:

```text
planning/examples/scenario-login-correct-v3.drawio
planning/examples/scenario-login-correct-v3.svg
planning/examples/scenario-login-correct-v3.png
planning/examples/scenario-login-correct-v3.md
```

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

Purple is used for the Password Recovery path because it leaves the current scenario page.

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
details
```

Secondary connectors must stay short and local whenever possible.

### 20A.3 End-State Summary

Do not draw a long connector from every final branch into the end-state block.

Prefer:

```text
compact End states / Postconditions block
placed near the final branches
with zero or minimal connectors
```

A summary block may be visually associated by placement alone.

### 20A.4 Text Fit

Text must fit inside every shape.

No label, ref, marker, or body text may overflow outside the shape boundary.

If text does not fit:

```text
- increase the shape size;
- reduce the text;
- split the node;
- move details to a note;
- or use a larger canvas.
```

Never accept overflow as valid.

---

## 21. Color Guidance For Scenario Semantics

Use this distinction:

```text
Color = visual role in this diagram.
Marker = planning meaning.
Connector label = semantic relationship.
```

Color semantics define what each color means.

Theme choice is controlled by `diagram-generation-rules-with-example.md`.

Use one consistent readable theme per package.

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

## 22. Connector Semantics

Every connector must mean something.

Useful connector labels:

```text
starts
next
checks
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
details
notifies
future branch
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

## 23. Scenario Diagram Self-Check

Before accepting a scenario diagram, check:

### Semantic correctness

```text
- scenario page is user-facing behavior, not implementation flow;
- main unit is actor + screen/context + goal;
- preconditions do not duplicate decisions;
- decisions are decisions, not generic extensions;
- invalid/error branches are not [ALT] by default;
- correctable validation errors loop back to input where appropriate;
- [ALT] is used narrowly;
- [EXT] is not used in item refs;
- EXTND is used for off-page/subscenario item refs;
- DETAIL is used for user-visible input/filter/search details when needed;
- invariants attach to enforcement points;
- step postconditions attach to producing steps;
- scenario end states are compact;
- observable outcomes are externally visible/verifiable;
- no unrelated global invariant is placed on a specific scenario page.
```

### Visual correctness

```text
- one consistent readable theme is used within the package;
- semantic colors remain meaningful and readable;
- main flow is visually obvious;
- diagram is not a text-card summary;
- no connector web;
- no connector crosses shape bodies;
- no connector crosses body text;
- secondary connectors do not cross the main flow;
- text fits inside shapes;
- connector labels do not overlap nodes;
- layout is spacious.
```

### Marker/color correctness

```text
- color means visual role;
- marker means planning meaning;
- connector label means semantic relationship;
- purple means off-page/subscenario only;
- red means negative/error/rejection/failure branch;
- yellow means invariant/rule/constraint;
- cyan means include/mandatory supporting step;
- markers are selective but visible.
```

---

## 24. Minimal Prompt Add-On

Use this short block in scenario diagram prompts:

```text
Read planning/diagram-scenario-spec.md.

Scenario diagrams are user-facing behavioral specification diagrams.
Do not create command/domain/table maps.

Use the requested generation mode:
- proof-only;
- single scenario;
- package;
- global overview;
- prompt/docs-only.

Do not add a proof step unless the user asked for proof or the task is explicitly a calibration/smoke-test task.

Use one consistent readable theme and preserve semantic color meanings.

Main flow must be the visual backbone.
No text-card summaries.
Invariants attach to enforcement points.
Preconditions do not duplicate decision branches.
Correctable validation errors should usually loop back to input.
Step postconditions attach to producing steps.
Use EXTND in item refs.
Use DETAIL for user-visible input/filter/search criteria when needed.
Use [ALT] narrowly.
Purple only means off-page/subscenario link.
No generic "extend" connector.
No implementation details.
All text must fit inside shapes.
No connector web.
```

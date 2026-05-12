# Rules for Creating Project Diagrams in draw.io

Status: permanent visual/construction guide for editable draw.io diagrams.

Scope: visual construction, draw.io/editability rules, layout discipline and theme rules.

This file does **not** define scenario semantics. For scenario/use-case semantics, read:

```text
planning/diagram-scenario-spec.md
```

For prompt protocol, read:

```text
planning/diagram-prompting-guide.md
```

For repeated mistakes and reject criteria, read:

```text
planning/diagram-common-mistakes.md
```

For approved examples, read:

```text
planning/diagram-examples-index.md
```

---

## 0. Repository Write Permission

Diagram generation and repository modification are separate actions.

Diagram-generation agents must not create, update, delete, move, rename or commit files in the repository unless the user explicitly asks them to modify the repository.

Default output should be reviewable artifacts only:

```text
- draw.io file output;
- SVG/PNG preview output;
- Markdown summary output;
- suggested target paths.
```

Agents may suggest repository paths, but must not write to those paths without explicit instruction.

Allowed without an explicit repo-write request:

```text
- generate downloadable files;
- provide a patch proposal;
- list intended repository paths;
- explain where the user should place files;
- create a prompt for another agent.
```

Not allowed without an explicit repo-write request:

```text
- create files in repository;
- update existing repository files;
- delete files;
- rename files;
- move files;
- commit changes;
- open PR;
- modify planning docs.
```

Requests such as `"generate diagrams"`, `"create a draw.io file"`, `"make a preview"` and `"give me the package"` mean generate reviewable artifacts, not write to the repository.

Only write to the repository when the user explicitly says `"add these files to the repository"`, `"commit this to the repo"`, `"update planning/..."`, `"create the file in GitHub"` or `"modify the repository"`.

If unclear, do not write. Return generated artifacts and ask for explicit repo-write instruction.

---

## 1. Theme Boundary Rule

There are two visual baselines in the repository history:

```text
1. Approved dark scenario/use-case theme.
2. Older C2/C2.2 light-card technical diagram baseline.
```

Do not mix them.

### 1.1 Scenario / Use-Case Diagrams

Scenario/use-case diagrams must use the approved dark project theme by default.

Canonical scenario visual baseline:

```text
planning/examples/scenario-login-correct-v3.drawio
planning/examples/scenario-login-correct-v3.svg
planning/examples/scenario-login-correct-v3.png
planning/examples/scenario-login-correct-v3.md
```

Scenario diagrams should use:

```text
- dark background / dark grid;
- high-contrast readable text;
- dark node bodies;
- semantic colors from planning/diagram-scenario-spec.md;
- spacious layout;
- readable node sizes;
- no text overflow;
- no connector web.
```

Do not use:

```text
- white canvas;
- default draw.io light theme;
- old C2 light card bodies as the scenario default;
- pale low-contrast cards;
- unreadable text on dark background.
```

A scenario diagram may be semantically correct and still be rejected if it uses a light/default theme without explicit user request.

### 1.2 Domain / Aggregate / DB / Technical Appendix Diagrams

The older C2/C2.2 light-card baseline may still be used for non-scenario technical diagrams unless a later task says otherwise.

This includes:

```text
- domain model diagrams;
- aggregate boundary diagrams;
- DB schema diagrams;
- technical CQRS/application flow appendix diagrams.
```

For these diagram families, light card bodies and dark text may be acceptable.

Important:

```text
Do not apply the old light-card C2 baseline to scenario/use-case diagrams.
```

### 1.3 final-diagrams-example.drawio

`final-diagrams-example.drawio` is secondary / legacy visual reference material.

Use it for:

```text
- editable grouped card construction;
- attached edge labels;
- line-by-line text implementation;
- general C2 visual language for technical diagrams;
- historical visual context.
```

Do not use it as:

```text
- canonical scenario visual baseline;
- logical scenario structure reference;
- required page package;
- semantic model for use-case diagrams;
- source of architecture decisions;
- reason to replace the dark scenario theme with a light theme.
```

---

## 2. Main Goal

Generated diagrams should be editable technical/planning diagrams, not just pretty images.

They should be:

```text
- readable without excessive zoom;
- editable in draw.io / diagrams.net;
- compact but not cramped;
- visually consistent within their diagram family;
- easy to manually polish;
- structurally clear enough for planning documentation.
```

The goal is not perfect automatic layout.

The goal is:

```text
good generated layout + quick manual polish
```

Small manual fixes are acceptable.

Not acceptable:

```text
- text overflow;
- unreadably tiny text;
- major shape overlap;
- connector web;
- connectors through body text;
- wrong theme for the diagram family;
- invalid draw.io XML;
- diagrams that cannot be edited as normal draw.io shapes.
```

---

## 3. Diagram Family Separation

Create separate diagram types. Do not mix concerns.

### 3.1 Scenario / Use-Case Diagrams

Scenario diagrams are user-facing behavioral specification diagrams.

They should be organized around:

```text
actor + screen/application context + user goal
```

They show:

```text
actor / screen
goal
preconditions
main flow
decision branches
include
subscenario links
invariants
step-level postconditions
observable outcomes
compact end states
```

They must not be organized around:

```text
Command
Domain
Writes/Reads table
Controller/Handler/Repository/DbContext
```

Scenario semantics are controlled by:

```text
planning/diagram-scenario-spec.md
```

Scenario visual baseline is the approved dark theme.

### 3.2 Domain Model Diagrams

Domain model diagrams show:

```text
- domain concepts/classes;
- aggregate roots;
- properties;
- stored reference IDs;
- factories;
- behavior;
- value objects;
- domain rules.
```

Recommended card sections:

```text
[properties]
[factory]
[behavior]
[value objects]
[rule]
[domain method policy]
[optional policy]
```

### 3.3 Aggregate Boundary Diagrams

Aggregate boundary diagrams show ownership and boundaries.

They should show:

```text
- aggregate boundary;
- aggregate root;
- owned child entities, if any;
- stored reference IDs;
- ID-reference arrows;
- notes explaining FK vs ownership.
```

Important domain/visual rule:

```text
A database FK does not imply aggregate ownership.
```

Do not draw one aggregate as owning another aggregate merely because the database has a foreign key.

### 3.4 DB Schema Diagrams

DB diagrams are for storage shape.

Show:

```text
- tables;
- PK/FK;
- discriminator columns;
- TPH if used;
- persisted columns;
- owned value-object column groups;
- schema evolution by level, if useful.
```

Do not show:

```text
- domain factory methods;
- behavior methods;
- rich domain rules except short notes if needed.
```

### 3.5 Technical CQRS / Application Flow Diagrams

CQRS/application flow is an optional technical appendix diagram family.

Do not merge it into user-facing scenario pages.

Possible lanes:

```text
HTTP
Controller
Application / MediatR
Domain
Persistence
Database
```

Command flow may show:

```text
Frontend
Controller
Command Handler
Aggregate
Repository / UoW
SQL tables
```

Query flow may show:

```text
Frontend
Controller
Query Handler
Dapper
SQL tables
```

These are implementation/technical diagrams, not scenario diagrams.

---

## 4. Scenario Diagram Visual Rules

For scenario/use-case diagrams, use the semantic colors from `diagram-scenario-spec.md`:

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

Important:

```text
Purple does not mean actor choice in general.
Purple means the path leaves the current scenario page.
```

Scenario diagram layout must follow:

```text
- main flow is the visual backbone;
- no text-card-only summaries;
- conceptual lane discipline;
- visible lane guide lines are not required by default;
- no connector web;
- no long connectors from every branch to the final summary;
- secondary connectors stay short and local;
- invariants attach to enforcement points;
- include nodes attach to exact required steps;
- step postconditions attach to producing steps;
- end states remain compact and secondary.
```

If a scenario page becomes too dense:

```text
- enlarge the canvas;
- move secondary nodes closer to their anchor point;
- remove unnecessary summary connectors;
- split a complex branch into a subscenario page.
```

---

## 5. Editable Shape Construction

Generated draw.io diagrams must be editable.

Use normal draw.io shapes and groups.

Avoid producing one giant image or one giant HTML label that cannot be conveniently edited.

### 5.1 Card / Node Structure

For larger technical cards, prefer grouped editable parts:

```text
card group
  header shape
  body background shape
  section heading text shape
  divider line
  body line text shape
  body line text shape
  body line text shape
  ...
```

This is especially important for domain/aggregate/DB/CQRS technical cards.

For scenario flow nodes, compact editable shapes are acceptable, but the text still must be readable and must fit inside the node.

### 5.2 Avoid One Large HTML Body Label

Do not generate a card body as one large HTML label when the content has multiple sections or many lines.

Reason:

```text
draw.io may not reliably scale multiline rich HTML body text.
Large HTML labels are harder to edit and often cause text overflow.
```

Preferred:

```text
one body line = one text shape
```

or, for compact scenario nodes:

```text
one compact editable label that fits cleanly inside the shape
```

### 5.3 Text Must Fit

Text overflow is not acceptable.

No label, ref, marker, or body text may escape outside the shape boundary.

If text does not fit:

```text
- increase the shape size;
- reduce the text;
- split the node;
- move details to a note;
- or use a larger canvas.
```

Never accept overflow as valid.

Reject/revise if:

```text
- text escapes outside a shape;
- text touches borders too closely;
- refs/markers overlap body text;
- connector labels overlap node text;
- node is too small for its content.
```

---

## 6. Text Size Guidance

Use readable text.

Recommended ranges:

```text
page title:       34–38
card header:      28–34
body text:        18–22
section heading:  18–22
edge labels:      16–20
notes:            16–18
legend:           15–17
lane titles:      18–22
```

For compact scenario flow nodes, body text can be smaller if needed, but should still remain readable.

Avoid returning to visually tiny 12px body text.

---

## 7. Text Overflow Prevention

Preventing overflow is mandatory.

### 7.1 Width

Prefer wider nodes/cards over dense wrapping.

Widen shapes for:

```text
- long scenario titles;
- long strict refs;
- long domain names;
- long command/handler labels in technical diagrams;
- long property names;
- multiline notes.
```

### 7.2 Height

Allocate enough vertical space for wrapping.

Approximate sizing:

```text
headerHeight = headerFont * 2.2 to 2.3
sectionHeight = sectionFont * 1.5 to 1.6
baseLineHeight = bodyFont * 1.5 to 1.6
topPadding = 20 to 28
bottomPadding = 20 to 28
sectionGap = 14 to 20
sidePadding = 24 to 32
dividerGap = 8 to 12
```

If a line may wrap, allocate extra height:

```text
availableWidth = cardWidth - 2 * sidePadding
maxCharsPerLine ≈ availableWidth / (bodyFont * 0.56)
lineSlots = ceil(lineLength / maxCharsPerLine)
lineHeight = baseLineHeight * lineSlots
```

### 7.3 Do Not Clip

Do not allow:

```text
- body lines outside the body background;
- text extending below the card;
- text hidden under another shape;
- labels sitting on body text.
```

---

## 8. Layout Principles

Main rule:

```text
compact, but not cramped
```

Priorities:

```text
1. readability
2. semantic clarity
3. no text overflow
4. no major overlaps
5. clean connector routing
6. compactness
7. easy manual polish
```

Keep enough spacing for:

```text
- edge routing;
- edge labels;
- manual repositioning;
- readable separation of logical areas.
```

Do not overcompact the layout just to fit everything into a smaller page.

A clean large diagram is better than a dense small one.

---

## 9. Edge Labels And Routing

Use attached edge labels.

Do not use floating standalone badges by default.

Edge labels should:

```text
- be attached to the connector;
- be readable;
- have enough contrast;
- not cover body text;
- preferably sit in open corridors between shapes.
```

Useful scenario connector labels:

```text
starts
next
checks
if valid
if invalid
if missing
if selected
continue
include
results in
protected by
opens subscenario
future branch
```

Avoid vague labels:

```text
extend
related
link
then
```

unless explicitly justified.

Connector routing rules:

```text
- do not route connectors through shape bodies;
- do not route connectors through body text;
- do not stack unrelated connectors on the same side/point when other ports are free;
- distribute anchors across shape sides;
- avoid overlapping connectors;
- if routing becomes messy, move nodes, enlarge canvas or split the page.
```

If 3-4 edges stack on top of each other, reroute or split the page.

---

## 10. Aggregate Boundary Visual Rules

Aggregate boundaries should make ownership explicit.

Important rule:

```text
A database FK does not mean aggregate ownership.
```

For one-root aggregates:

```text
top padding:     enough for the aggregate title
side padding:    compact
bottom padding:  compact
```

Do not create huge empty aggregate containers.

Draw separate aggregate boxes for separate aggregate roots.

Connect aggregates through stored ID reference arrows when needed.

Use labels like:

```text
stores id only
ClientAccountId
ApplicantPartyId
```

---

## 11. Technical Diagram Palette Guidance

This section applies to non-scenario technical diagrams unless a task says otherwise.

Do not use this palette as the default for scenario/use-case diagrams.

### 11.1 L1 / DB Tables

```text
header: #009E73
body:   #E6F4EF
stroke: #007A5A
```

### 11.2 Domain / Aggregate Root Cards

```text
header: #E69F00
body:   #FFF4D8
stroke: #B87900
```

### 11.3 Aggregate Boundary Containers

```text
fill:   #FFF7ED
stroke: #B87900
title:  #92400E
```

### 11.4 Value Object Cards

```text
header: #56B4E9
body:   #EAF7FD
stroke: #2A8DBF
```

### 11.5 Rules / Notes

```text
header/accent: #F0E442
body:          #FFFBE0
stroke:        #B8A900
```

### 11.6 Neutral Technical Flow Blocks

```text
header: #334155
body:   #F1F5F9
stroke: #64748B
```

### 11.7 Technical Flow Lanes

```text
fill:   #F8FAFC
stroke: #CBD5E1
title:  #334155
```

---

## 12. Rejected Approaches And Mistakes

### 12.1 Rejected: Light Theme For Scenario Diagrams

Mistake:

```text
Using a white/default draw.io canvas for scenario/use-case diagrams.
```

Correct:

```text
Use the approved dark scenario theme from scenario-login-correct-v3 unless the user explicitly asks for light theme.
```

### 12.2 Rejected: Applying Old C2 Light Cards To Scenario Diagrams

Mistake:

```text
Reading the old technical C2 palette and using light card bodies for scenario pages.
```

Correct:

```text
Scenario diagrams use the approved dark scenario theme.
The old C2 light-card palette is only for non-scenario technical diagrams unless explicitly requested.
```

### 12.3 Rejected: One Large HTML Body Label

Mistake:

```text
A whole card body is one large HTML label.
```

Correct:

```text
Use editable grouped shapes and separate text shapes for multi-line card bodies.
```

### 12.4 Rejected: Floating Relation Badges

Mistake:

```text
Relationship labels are separate floating shapes rather than attached edge labels.
```

Correct:

```text
Use attached edge labels.
```

### 12.5 Rejected: Connector Web

Mistake:

```text
Every branch connects to every summary block or distant note.
```

Correct:

```text
Keep secondary connectors local.
Prefer no connector over a connector web when placement already communicates the relationship.
```

---

## 13. Checklist Before Returning A Diagram

Before returning a `.drawio` file, check:

### File

```text
- XML is valid;
- file opens in draw.io;
- pages are clearly named;
- artifacts are reviewable and editable.
```

### Theme

```text
- scenario diagrams use approved dark theme;
- technical/domain/DB diagrams use the requested or appropriate family theme;
- old C2 light-card baseline is not applied to scenario pages by mistake.
```

### Text

```text
- font is readable;
- text fits inside every shape;
- refs/markers do not overlap labels;
- no clipped text;
- no tiny 12px body text in large cards.
```

### Shapes

```text
- nodes/cards are wide enough;
- long lines have enough height;
- diagrams are compact but not cramped;
- layout is easy to manually polish.
```

### Connectors

```text
- labels are attached to edges;
- connectors do not cross shape bodies;
- connectors do not cross body text;
- connector labels do not overlap important text;
- no connector web.
```

### Scenario Semantics

For scenario/use-case diagrams, also check:

```text
- main flow is visual backbone;
- diagram is not a text-card summary;
- invariants attach to enforcement points;
- includes attach to exact required steps;
- step postconditions attach to producing steps;
- preconditions do not duplicate decision branches;
- no implementation details dominate the page;
- purple means off-page/subscenario link only.
```

### Repository Write Rule

```text
- do not write generated artifacts into repository unless explicitly asked;
- return reviewable artifacts and suggested target paths.
```

---

## 14. Short Prompt For A Future Diagram Agent

```text
Create editable draw.io XML diagrams for the Energy Management project.

Read:
- planning/diagram-brief.md
- planning/diagram-scenario-spec.md
- planning/diagram-generation-rules-with-example.md
- planning/diagram-prompting-guide.md
- planning/diagram-common-mistakes.md
- planning/diagram-examples-index.md

For scenario/use-case diagrams:
- use the approved dark theme from planning/examples/scenario-login-correct-v3.*;
- do not use the old light C2 palette;
- follow semantic colors from diagram-scenario-spec.md;
- keep main flow as visual backbone;
- avoid text-card summaries;
- keep connectors local and readable.

For domain/aggregate/DB/technical appendix diagrams:
- use the appropriate technical diagram family rules;
- old C2/C2.2 light-card baseline may be used unless overridden;
- keep diagrams editable.

Use final-diagrams-example.drawio only as secondary / legacy visual-style reference.
Do not treat it as scenario semantics or as reason to replace the dark scenario theme.

Do not create, update, delete, move, rename or commit repository files unless explicitly asked.
Return reviewable artifacts and suggested target paths.

Validate XML before returning.
```

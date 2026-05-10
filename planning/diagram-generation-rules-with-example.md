# Rules for Creating Project Diagrams in draw.io

This file captures the current agreed rules for generating project diagrams so that a new chat or AI agent can recreate the same kind of diagram we converged on.

The current accepted baseline is:

```text
C2/C2.2 visuals + C2.4 text implementation
```

This means:

```text
Use C2/C2.2 for:
- visual style
- palette
- layout discipline
- spacing
- attached edge labels
- compact-but-not-cramped structure

Use C2.4 for:
- text rendering
- every body line as a separate draw.io text shape
- every section heading as a separate draw.io text shape
- body background as a separate shape
```

The latest accepted test artifact that reflects this direction is:

```text
energy-management-final-test-diagrams-c2-line-text-v2.drawio
```

It is not meant to be pixel-perfect, but it reflects the current target style:
- accepted C2 palette;
- readable large body text;
- body text generated as real draw.io text-shapes;
- cards refitted to prevent text overflow;
- attached edge labels;
- aggregate boundaries compacted on sides and bottom;
- CQRS flow widened enough to avoid body text leaving the cards.
---

# Reference Example File

Alongside this rules file, keep the accepted example draw.io file:

```text
final-diagrams-example.drawio
```

This file is the current visual and structural reference example.

Use it as the baseline for:

```text
- overall visual style;
- accepted C2 palette;
- card composition;
- spacing and layout discipline;
- compact-but-not-cramped structure;
- aggregate boundary sizing;
- attached edge labels;
- line-by-line body text rendering;
- CQRS lane structure;
- card width/height refitting under larger text.
```

Do not treat `final-diagrams-example.drawio` as a pixel-perfect template.

Treat it as:

```text
a style reference
a structure reference
a practical example of the accepted implementation
```

The agent should preserve the same overall visual direction unless a later planning note explicitly overrides it.

## What to copy from the example

Copy these principles:

```text
- light accepted C2 palette;
- cards as groups;
- header shape + body background shape;
- separate section heading text-shapes;
- separate body line text-shapes;
- readable large body text;
- no single big HTML body label;
- attached edge labels;
- compact aggregate boundaries;
- wider CQRS cards where text is long;
- manual-polish-friendly layout.
```

## What not to copy blindly

Do not blindly copy:

```text
- exact coordinates;
- exact page size;
- exact edge bend points;
- accidental local compromises;
- minor overlap that may have been manually fixable;
- exact number of pixels for every card.
```

The goal is to reproduce the **same visual language and construction method**, not to duplicate every coordinate.

## If rules and example conflict

If a conflict appears between this rules file and the example:

```text
1. Follow the rules file for explicit conventions.
2. Use the example file for visual interpretation.
3. Do not invent a new palette or rendering approach.
4. If unsure, preserve the accepted C2 palette and C2.4 line-text card structure.
```


---

# 1. Main Goal

The generated diagrams should be editable technical diagrams, not just pretty images.

They should be:

```text
- readable without excessive zoom;
- editable in draw.io / diagrams.net;
- compact but not cramped;
- visually consistent;
- easy to manually polish;
- structurally clear enough for diploma/planning documentation.
```

The goal is not perfect automatic layout. The goal is:

```text
good generated layout + quick manual polish
```

Small manual fixes are acceptable. Major text overflow, unreadable cards, or broken structure are not acceptable.

---

# 2. Accepted Baseline

## 2.1 Visual baseline

Use the visual style from the C2/C2.2 variants.

This includes:

```text
- light card bodies;
- clear colored headers;
- dark text inside cards;
- enough spacing between major blocks;
- attached edge labels;
- no floating relationship badges;
- aggregate boundaries with compact side/bottom padding;
- wide enough cards for technical text.
```

## 2.2 Text implementation baseline

Use the text implementation from C2.4.

This is the most important technical rule:

```text
Do not generate a card body as one large HTML label.
```

Instead, every card must be constructed as:

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

This is necessary because draw.io did not reliably apply larger font sizes inside a single rich HTML body label. In tests, the body visually stayed around 12px even when the parent shape style said `fontSize=22`.

The fix was:

```text
Each body line must be a separate draw.io text shape with its own explicit fontSize.
```

This must be preserved.

---

# 3. Rejected Approaches and Mistakes

## 3.1 Rejected: C+ floating badges

At one point, relation labels were generated as separate floating badge shapes.

Problem:

```text
The label is not attached to the edge.
When a shape or edge is moved manually, the label stays behind.
```

Decision:

```text
Do not use standalone floating relation badges by default.
Use attached edge labels.
```

Exception:

```text
Only use floating labels if explicitly requested for a special diagram.
```

## 3.2 Rejected: dark C2.5 palette

A later test accidentally switched to a darker/dirty palette.

Problem:

```text
It broke the accepted visual direction.
It made diagrams visually heavier and less readable.
It did not match the C2/C2.2 baseline the user accepted.
```

Decision:

```text
Do not use the C2.5 dark palette.
Do not replace the accepted C2 palette unless explicitly asked.
```

Rejected dark colors included examples like:

```text
#0F2E29
#2E2110
#102734
#111827
#0F172A
```

These should not become the default card body palette.

## 3.3 Rejected: one large HTML body label

A single body shape with HTML lines/spans was tested.

Problem:

```text
draw.io did not reliably scale the body text.
Even with fontSize set in the parent shape or inline spans, text visually remained too small in some cases.
```

Decision:

```text
Do not use one large HTML label for card body text.
Generate separate text shapes for each line.
```

## 3.4 Mistake: increasing text without refitting shapes

Increasing font size without recalculating card sizes caused overflow.

Decision:

```text
Whenever text size increases, card width and height must be recalculated.
```

The correct rule is:

```text
larger text + refitted shapes
```

not:

```text
larger text inside old boxes
```

## 3.5 Mistake: narrow CQRS flow cards

The CQRS/use-case diagram had text leaving the cards because command handler and flow blocks were too narrow.

Decision:

```text
For flow diagrams, make lanes and cards wider.
For long lines, calculate extra height if wrapping may happen.
```

---

# 4. Accepted Palette

Use this palette unless the user explicitly asks to change it.

## 4.1 L1 / DB tables

```text
header: #009E73
body:   #E6F4EF
stroke: #007A5A
```

Usage:
- L1 current persisted schema;
- DB tables in L1 diagrams.

## 4.2 Domain / aggregate root cards

```text
header: #E69F00
body:   #FFF4D8
stroke: #B87900
```

Usage:
- aggregate roots;
- domain classes;
- domain model cards.

## 4.3 Aggregate boundary containers

```text
fill:   #FFF7ED
stroke: #B87900
title:  #92400E
```

Usage:
- aggregate boundary boxes that contain one aggregate root card.

## 4.4 Value object cards

```text
header: #56B4E9
body:   #EAF7FD
stroke: #2A8DBF
```

Usage:
- shared value object area;
- value object cards.

## 4.5 Rules / notes

```text
header/accent: #F0E442
body:          #FFFBE0
stroke:        #B8A900
```

Usage:
- domain rules;
- notes;
- conventions;
- warning/explanation cards.

## 4.6 Neutral flow blocks

```text
header: #334155
body:   #F1F5F9
stroke: #64748B
```

Usage:
- frontend/controller blocks;
- neutral infrastructure blocks.

## 4.7 Flow lanes

```text
fill:   #F8FAFC
stroke: #CBD5E1
title:  #334155
```

Usage:
- large background lanes in CQRS/use-case flow diagrams.

---

# 5. Typography

Use large readable text.

Recommended ranges:

```text
page title:       34–38
card header:      30–34
body text:        20–22
section heading:  18–22
edge labels:      18–20
notes:            16–18
legend:           15–17
lane titles:      18–22
```

For compact flow blocks, body text can be slightly smaller if necessary:

```text
flow small card body: 18–19
flow small card section: 16–17
flow small card header: 26–28
```

But avoid returning to visually tiny 12px body text.

---

# 6. Card Construction

Every card should be a group of editable shapes.

## 6.1 Required structure

```text
card group
  header shape
  body background shape
  section heading text shape
  divider line
  body line text shape
  body line text shape
  ...
```

## 6.2 Header shape

Header:
- has colored background;
- contains the card title;
- uses large bold text;
- is a separate child shape inside the card group.

Example style direction:

```text
rounded=1
whiteSpace=wrap
html=1
arcSize=8
fillColor=<header color>
strokeColor=<stroke color>
strokeWidth=2
fontColor=#FFFFFF
fontSize=30–34
fontStyle=1
align=center
verticalAlign=middle
```

## 6.3 Body background shape

Body background:
- is only the background rectangle;
- should not contain the whole text;
- sits below the header;
- uses the accepted body color.

Example style direction:

```text
rounded=1
whiteSpace=wrap
html=1
arcSize=6
fillColor=<body color>
strokeColor=<stroke color>
strokeWidth=2
```

## 6.4 Section heading text shapes

Each section heading is a separate text shape.

Example sections:

```text
[identity]
[L1 core]
[properties]
[factory]
[behavior]
[ownership]
[stored reference]
[value objects]
[rule]
```

Section heading style:
- text shape;
- explicit fontSize;
- bold;
- orange/dark accent color;
- no fill/stroke.

Example direction:

```text
text
html=1
strokeColor=none
fillColor=none
whiteSpace=nowrap or wrap
fontSize=18–22
fontColor=#92400E
fontStyle=1
fontFamily=Consolas
```

## 6.5 Divider line

After each section heading, add a subtle divider line.

Example:

```text
shape=line
strokeWidth=1
strokeColor=#CBD5E1
```

## 6.6 Body line text shapes

Each body line is a separate text shape with explicit fontSize.

Example:

```text
Email : nvarchar(320)
PasswordHash : nvarchar(max)
Create(email, passwordHash)
```

Each line must have:
- its own `mxCell`;
- its own geometry;
- explicit `fontSize`;
- enough height for wrapping if the line is long.

Do not use one text shape for all body lines.

---

# 7. Text Overflow Prevention

Preventing overflow is mandatory.

## 7.1 Card width

Prefer wider cards over dense wrapping.

Especially widen cards for:
- long table names;
- long class names;
- factory method signatures;
- command names;
- CQRS handlers;
- long property names.

## 7.2 Card height calculation

Use approximate sizing rules:

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

## 7.3 Wrapping calculation

If a body line may wrap, allocate extra height.

Approximate rule:

```text
availableWidth = cardWidth - 2 * sidePadding
maxCharsPerLine ≈ availableWidth / (bodyFont * 0.56)
lineSlots = ceil(lineLength / maxCharsPerLine)
lineHeight = baseLineHeight * lineSlots
```

This prevents long lines from leaving the card.

This rule fixed the CQRS-flow issue where command handler lines overflowed.

## 7.4 Do not clip

Do not allow:
- body lines outside the body background;
- text extending below the card;
- text hidden under another shape;
- labels sitting on body text.

---

# 8. Layout Principles

## 8.1 Main principle

```text
compact, but not cramped
```

## 8.2 Priorities

```text
1. readability
2. no text overflow
3. no major overlaps
4. compactness
5. easy manual polish
```

## 8.3 Spacing

Keep enough spacing for:
- edge routing;
- edge labels;
- manual repositioning;
- readable separation of logical areas.

Do not overcompact the layout just to fit everything into a smaller page.

## 8.4 Manual polish

The generated layout does not need to be perfect.

Acceptable:
- small edge label adjustments;
- minor edge rerouting;
- moving a note slightly.

Not acceptable:
- text overflow;
- broken card sizes;
- unreadably tiny text;
- huge accidental overlaps;
- wrong palette.

---

# 9. Edge Labels

## 9.1 Required rule

Use attached edge labels.

Do not use floating standalone badges by default.

## 9.2 Edge label styling

Use:

```text
fontSize=18–20
fontStyle=1
labelBackgroundColor=#FFFFFF
labelBorderColor=#CBD5E1
fontColor=#111827
```

## 9.3 Edge label positioning

Use offset when useful:

```text
<mxPoint as="offset" x="..." y="..."/>
```

Labels should:
- be readable;
- not cover body text;
- preferably sit in open corridors between cards.

If draw.io places labels slightly oddly, this is acceptable if quick to fix manually.

---

# 10. Aggregate Boundaries

Aggregate boundary diagrams must make ownership explicit.

Important domain rule:

```text
A database FK does not imply aggregate ownership.
```

Current L1 aggregate roots:

```text
ClientAccount
IndividualApplicantParty
ConnectionRequest
```

Current L1 inter-aggregate references:

```text
IndividualApplicantParty stores ClientAccountId
ConnectionRequest stores ApplicantPartyId
```

Important visual rule:

```text
Do not draw ClientAccount as owning ApplicantParties.
Do not draw ApplicantParty as owning Requests.
Draw them as separate aggregate boxes connected by ID reference arrows.
```

## 10.1 Boundary sizing

Aggregate boundary containers should not be huge.

For one-root aggregates:

```text
top padding:     enough for the aggregate title
side padding:    compact
bottom padding:  compact
```

Top padding should remain visually clear. Sides and bottom should be tighter.

---

# 11. Diagram Families

Create separate diagram types. Do not mix concerns.

## 11.1 DB diagrams

DB diagrams are for storage shape.

Show:
- tables;
- PK/FK;
- discriminator columns;
- TPH;
- persisted columns;
- owned value-object column groups;
- L1/L2/L3 schema evolution.

Do not show:
- domain factory methods;
- behavior methods;
- rich domain rules except short notes if needed.

Recommended sections:

```text
[identity]
[L1 core]
[L2 additions]
[L3 additions]
```

## 11.2 Aggregate boundary diagrams

Show:
- aggregate boundary;
- aggregate root;
- stored reference IDs;
- ID-reference arrows;
- notes explaining FK vs ownership.

Recommended sections:

```text
[root]
[stored reference]
[properties]
[factory]
[ownership]
```

## 11.3 Domain model diagrams

Show:
- domain classes;
- aggregate roots;
- properties;
- stored reference IDs;
- factories;
- behavior;
- value objects;
- rules.

Recommended sections:

```text
[properties]
[factory]
[behavior]
[value objects]
[rule]
[domain method policy]
[optional policy]
```

## 11.4 CQRS / use-case flow diagrams

Use lanes:

```text
HTTP
Controller
Application / MediatR
Domain
Persistence
Database
```

Command flow:

```text
Frontend
Controller
Command Handler
Aggregate
Repository / UoW
SQL tables
```

Query flow:

```text
Frontend
Controller
Query Handler
Dapper
SQL tables
```

Rules:

```text
Commands use domain + EF and commit in handler.
Queries use Dapper read models and do not touch domain aggregates.
```

The CQRS flow needs wider cards than initially expected because handler steps contain long lines.

---

# 12. Semantic Text Coloring

Use moderate line-level coloring. Do not overdo it.

Recommended:

```text
PK line:              #BE185D
FK / Id line:         #0369A1
Discriminator line:   #B45309
method line:          #7E22CE
normal line:          #111827
section heading:      #92400E
```

Avoid complex multi-span IDE-like coloring if it risks breaking font rendering.

The current safe approach is:

```text
one line = one text shape = one primary line color
```

---

# 13. Page Content for the Current Project

## 13.1 DB L1 current schema

Tables/cards:

```text
L1Accounts / Account TPH
L1ApplicantParties / ApplicantParty TPH
L1ClientRequests / ClientRequest TPH
```

Relationships:

```text
L1Accounts 1 → many L1ApplicantParties
label: ClientAccountId

L1ApplicantParties 1 → many L1ClientRequests
label: ApplicantPartyId
```

DB note:

```text
DB diagram:
• storage shape only
• no domain methods
• TPH shown by discriminator columns
```

## 13.2 L1 aggregate boundaries

Aggregate boxes:

```text
Account aggregate
  ClientAccount

ApplicantParty aggregate
  IndividualApplicantParty

ClientRequest aggregate
  ConnectionRequest
```

Relationship labels:

```text
stores id only
ClientAccountId

stores id only
ApplicantPartyId
```

Boundary note:

```text
Boundary rule:
DB FK does not mean aggregate ownership.
Application service loads related aggregate and checks cross-aggregate rules.
```

## 13.3 L1 domain model

Main cards:

```text
ClientAccount
IndividualApplicantParty
ConnectionRequest
Shared Value Objects
Domain Rules
```

Important reference labels:

```text
ClientAccountId
ApplicantPartyId
uses VOs
```

## 13.4 CQRS / use-case flow

Lanes:

```text
HTTP
Controller
Application / MediatR
Domain
Persistence
Database
```

Blocks:

```text
Frontend
Controller
Command Handler
Query Handler
Aggregate
L1 Repositories / UoW
Dapper
L1 SQL tables
```

Command arrows:

```text
HTTP POST
MediatR Send
call domain
Add + SaveChanges
INSERT/UPDATE
```

Query arrows:

```text
HTTP GET
MediatR Send
execute SQL
SELECT
```

CQRS note:

```text
CQRS rule:
Commands use domain + EF and commit in handler.
Queries use Dapper read models and do not touch domain aggregates.
```

---

# 14. Checklist Before Returning a Diagram

Before returning a `.drawio` file, check:

## File

```text
- XML is valid
- file opens in draw.io
- pages are clearly named
```

## Palette

```text
- uses accepted C2 palette
- does not use rejected C2.5 dark palette
```

## Text

```text
- body is not one big HTML label
- each body line is a separate text shape
- each section heading is a separate text shape
- font size is visibly larger than 12px
- no text overflows card boundaries
```

## Cards

```text
- cards are wide enough
- long lines have enough vertical height
- no clipped text
- cards are not excessively empty
```

## Aggregate boundaries

```text
- compact sides and bottom
- enough top padding for title
- no huge empty containers
```

## Edges

```text
- labels attached to edges
- no floating relation badges
- labels readable
- labels not covering body text where avoidable
```

## Diagram semantics

```text
- DB diagram does not show domain methods
- aggregate diagram clearly shows ID references, not ownership
- domain model shows methods/rules/value objects
- CQRS flow separates command/query paths
```

---

# 15. Short Prompt for a Future Agent

Use this as a compact instruction block:

```text
Create editable draw.io XML diagrams using the accepted diagram convention:

C2/C2.2 visuals + C2.4 text implementation.

Use `final-diagrams-example.drawio` as the visual and structural reference example.
Do not copy it pixel-perfectly; copy its visual language, palette, card construction, text rendering approach, and layout discipline.

Use the accepted C2 palette:
- L1/DB: header #009E73, body #E6F4EF, stroke #007A5A
- Domain/root: header #E69F00, body #FFF4D8, stroke #B87900
- Aggregate boundary: fill #FFF7ED, stroke #B87900, title #92400E
- Value objects: header #56B4E9, body #EAF7FD, stroke #2A8DBF
- Rules/notes: body #FFFBE0, stroke #B8A900
- Neutral blocks: header #334155, body #F1F5F9, stroke #64748B
- Lanes: fill #F8FAFC, stroke #CBD5E1

Do not use the rejected dark C2.5 palette.

Do not create card bodies as one big HTML label.
Each card must be a group:
  header shape
  body background shape
  section heading text shape
  divider line
  one separate text shape per body line

Each body line must have explicit fontSize, usually 20–22.
Refit card width/height to prevent text overflow.
If text may wrap, reserve extra vertical height.

Use attached edge labels, not floating badges.
Use labelBackgroundColor and labelBorderColor.

Keep layout compact but not cramped.
Aggregate boundaries should keep top padding for the title but compact sides/bottom.

Generate separate pages:
1. DB schema
2. Aggregate boundaries
3. Domain model
4. CQRS/use-case flow

Validate XML before returning.
```

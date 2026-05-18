# Visual Evidence Protocol

Status: initial

## Purpose

The VKR should be personalized through diagrams, screenshots, tables and explanatory text. Visuals are not decoration; they are evidence for project decisions and implementation results.

## Main pattern

```text
claim
-> visual evidence
-> explanation
-> conclusion
```

## Visual types

### Diagrams

Use diagrams for domain analysis and design:

```text
process before automation
target process after automation
request lifecycle
document/agreement exchange lifecycle
domain model
ERD subset
frontend/backend architecture
API contract flow
vertical slice flow
```

### Screenshots

Use screenshots for implementation and preddiploma demonstration:

```text
registration/login
account/applicant
create request
my requests list
request details
employee dashboard
employee request details
agreement exchange list/details
document reference fields
```

### Tables

Use tables for compact explanation:

```text
actors and actions
problem -> manifestation -> project solution
requirement -> source -> implementation evidence
scenario -> API -> UI -> tests
domain entity -> table -> purpose
test type -> checked scenario
```

## Large diagrams

Do not put unreadable full diagrams in the main text.

Use:

```text
main text: simplified overview
appendix: full diagram
optional: readable fragments
```

Example for SC-04:

```text
Main text:
Figure 2.x — High-level client request creation scenario

Appendix:
Figure A.x — Full client request creation scenario
Figure A.x — Applicant selection fragment
Figure A.x — Validation and successful request creation fragment
```

## Screenshot caption rule

Each screenshot needs:

```text
number
clear caption
1-3 explanatory sentences
connection to VKR task
```

Bad:

```text
Figure 3.5 — Requests page
```

Better:

```text
Figure 3.5 — Client request list with processing status
```

Then explain what the screen proves.

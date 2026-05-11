# Diagram Scenario Specification

This file defines the logic and content structure for user-facing use-case/scenario diagrams.

It does not define draw.io visual construction. For visual style, palette, typography, card construction and edge rendering, read `diagram-generation-rules-with-example.md`.

## Core Rule

Use-case diagrams are behavioral specification diagrams.

They should describe:

- user-facing scenario;
- actor plus screen/application context;
- user goal;
- required user-visible actions;
- optional branches;
- observable system behavior;
- preconditions, postconditions and invariants.

They should not describe:

- controller, handler, repository or DbContext flow;
- EF mapping;
- aggregate internals;
- command/domain/table structure as the primary card content.

Technical hints are allowed only as secondary trace notes when useful. They must not dominate the diagram.

## Main Scenario Unit

The main unit is:

```text
Actor + application screen/context + user goal
```

Examples:

- Guest - Registration screen
- Guest - Login screen
- Client - Request creation screen
- Client - Request status page
- Employee - Request dashboard
- Employee - Request review screen
- System - Notification processing

A scenario page may include multiple screens when they belong to one coherent user journey.

Example:

```text
Request creation page
-> applicant data block if missing
-> submit confirmation
-> request status page
```

If a branch has its own user goal or becomes complex, split it into a separate subscenario page.

Examples:

- Login scenario links to Password recovery scenario.
- Request creation scenario links to Request documents scenario.
- Employee review scenario links to Clarification scenario.

## Scenario Card / Page Structure

Each scenario page may include these sections.

### Actor / Screen

Who acts and in which application context.

### Goal

What the user is trying to achieve.

### Preconditions

Conditions that must be true before the scenario starts.

### Main Flow

Required user-visible steps.

### Include

Mandatory reusable steps or subscenarios.

### Extend

Optional branches, alternatives, errors or future extension points.

### Invariants

Rules that must remain true during the scenario.

### Observable Outcomes

Externally visible or verifiable results.

Allowed examples:

- account is saved;
- session is issued;
- applicant data is saved;
- request is stored;
- request status becomes `Submitted`;
- employee sees request in queue;
- client sees approval or rejection result;
- email notification is sent.

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

## Include / Extend Semantics

### <<include>>

Mandatory reusable step or subscenario required for the current scenario to complete.

Examples:

- Validate registration form
- Save account
- Validate request form
- Save submitted request

### <<extend>>

Optional branch, alternative flow, error flow or extension point.

Examples:

- Forgot password -> Password recovery scenario
- Applicant data missing -> Fill applicant data block
- Upload documents, L2
- Clarification requested, L2
- Account locked, L3
- Rate limit exceeded, L3
- Anonymous request, L3

A complex `<<extend>>` branch should become its own page.

## Level Interpretation For Scenario Diagrams

Use levels as final target scenario levels, not as current code state.

- L1 = final MVP user-facing scenarios.
- L2 = extended diploma workflow branches/scenarios.
- L3 = advanced/future scenario branches or cross-cutting capabilities.

Important:

```text
Do not interpret L1 as the current implemented code subset.
```

For scenario diagrams:

- L1 is the main MVP path.
- L2 is optional or extended workflow around the same scenario.
- L3 is future or cross-cutting capability related to the scenario.

## Applicant Data Naming

Do not use this as the primary user-facing label:

```text
Create individual applicant profile
```

Use:

```text
Provide individual applicant data
```

Reason:

The user is not necessarily thinking "I create an applicant profile". The user provides applicant data. The system may store it internally as applicant data / `ApplicantParty`.

There are two valid UX entry points.

### Profile-First

```text
Client opens profile/applicant data page
-> provides individual applicant data
-> later starts request
-> system reuses saved applicant data
```

### Request-First / Inline

```text
Client starts request
-> system sees applicant data is missing
-> request form asks for applicant data inline
-> system saves applicant data
-> request submission continues
```

Do not duplicate the domain concept in user-facing diagrams. The scenario is about providing applicant data, not about the user creating an `ApplicantParty`.

## Recommended Scenario Page Package

### Overview

```text
00 Scenario Overview / Navigation Map
```

### L1 Core Scenario Pages

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

### L2 Extension Scenario Pages

```text
10 Extended Applicant Data Scenario
11 Request Documents Scenario
12 Clarification Scenario
13 Contract Acknowledgement Scenario
14 Mock Verification Scenario
```

### L3 Advanced Scenario / Capability Pages

```text
15 Security / Account Protection Scenario
16 Reliable Notification Delivery Scenario
17 Anonymous Request Scenario
18 Archive / Audit Scenario
```

## Recommended Generation Batches

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

## Scenario Page Splitting Rules

- Prefer scenario pages over giant all-system maps.
- One page should represent one coherent scenario, screen or workflow.
- Large canvas is allowed.
- A clean large diagram is better than a dense small one.
- If routing becomes messy, split the scenario into another page.
- If an extension branch has its own user goal, split it into another page.
- If a page starts to look like a command/domain/table map, move technical details into an appendix or a separate domain/DB diagram.

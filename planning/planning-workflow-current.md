# Current Planning Workflow

Status: current / GitHub line-link workflow synchronized

Always separate current implementation from target scenario direction.

## 1. Current Target Direction

```text
ApplicantParty:
  many saved ApplicantParties;
  one current/default template per applicant type;
  explicit default switch is separate behavior.

Request creation:
  explicit Existing/New applicant context;
  New applicant + request is one atomic server operation.

My Requests:
  list, filters and details are separate slices/sidecars;
  status is the first filter entry in an extensible filter model.
```

## 2. Current Implementation Baseline

Known current repo evidence includes:

```text
- L1 backend register/login/current-user/logout;
- create individual ApplicantParty;
- current individual ApplicantParty read;
- create connection request using current implemented server model;
- My Requests list endpoint;
- Own Request Details endpoint;
- OpenAPI artifact and generated TypeScript types;
- first-stage client My Requests list UI.
```

Client details and filters are still planned/implementation-ready sidecars unless current repo code proves otherwise.

## 3. Repo Evidence Link Rule

When explaining current code/docs, implementation status, test coverage or a concrete consistency problem, provide GitHub Markdown links to exact lines or ranges.

Read:

```text
planning/repo-grounded-github-line-links-workflow.md
```

Required behavior:

```text
- read the current file before linking;
- use #Lx or #Lx-Ly anchors;
- prefer commit SHA links;
- use branch links only as fallback;
- do not use whole-file links for specific implementation claims.
```

## 4. Slice Drafting Rules

```text
Behavior Coverage is not Test Coverage.
Scenario Flow and Behavior Items come from source files via slice-scenario-flow-behavior-register.md.
ApplicantPartyId is API support, not behavior item.
E2E asserts visible state/outcome, not refetch mechanics or backend internals.
Server API slices must consider request-level FluentValidation separately from application/domain validation.
```

## 5. Agent Scope Rules

Implementation prompts must include explicit scope boundaries:

```text
- can change code in listed implementation area;
- cannot change Domain.EnergyManagement unless explicitly in scope;
- cannot change planning docs unless explicitly in scope;
- cannot update generated artifacts unless explicitly asked or generation is part of the implementation task;
- may read docs freely for context.
```

Use:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
```

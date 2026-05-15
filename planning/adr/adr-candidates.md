# ADR Candidates

Status: current candidate list  
Scope: promotion backlog for decisions that may later become full numbered ADRs

## Current Relevant Candidates

| Candidate ID | Candidate ADR | Accepted decision note(s) | Why it may need full ADR | Current direction | Priority |
|---|---|---|---|---|---|
| ADR-CAND-019 | Generated client constants | ADN-052, ADN-058, ADN-059, ADN-060 | Stable error code/field constants need deterministic generation and tests. | `CC-CONST-001` cross-cutting slice + explicit generator command + artifact sync check. | High |
| ADR-CAND-023 | Constants testing strategy | ADN-058, ADN-059, ADN-060 | Important cross-cutting testing strategy for API/client contract artifacts. | Generated JSON checks + critical literal tests + generator shape/writer/checker tests. | Medium/High |
| ADR-CAND-024 | Cross-cutting/helper slice workflow | ADN-061, ADN-062 | Support behavior with implementation/test flow needs a consistent documentation model. | Use `planning/slices/cross-cutting/` and keep flow behavior-first. | Medium |
| ADR-CAND-025 | Test responsibility split | ADN-063, ADN-064 | Important quality strategy: prevents E2E from duplicating component/API tests and supports diploma explanation. | E2E is cross-layer wiring; component tests own detailed UI. | High |
| ADR-CAND-026 | Playwright E2E infrastructure | ADN-065, ADN-066, ADN-070 | E2E reproducibility depends on explicit backend/frontend startup and repo-level config. | Root Playwright config + webServer backend/frontend + Vite proxy. | Medium/High |
| ADR-CAND-027 | Locator and accessibility testing policy | ADN-067, ADN-068 | Stable accessible locators connect UI design, a11y and tests. | Role/label-first; exact matching via exact:true or escaped regex. | Medium |
| ADR-CAND-028 | Page Object vs Component Object test architecture | ADN-069 | Clarifies what helpers may abstract and where UI-state details belong. | Thin E2E Page Objects; detailed Component Objects for client tests. | Medium |

## Likely Future Full ADRs

```text
ADR-0008 API error contract: ProblemDetails + ServerError + stable error codes
ADR-0009 OpenAPI structural contract + generated constants split
ADR-0010 Client constants generation and testing strategy
ADR-0011 Cross-cutting/helper slice documentation model
ADR-0012 E2E testing responsibility and Playwright infrastructure
ADR-0013 Accessible locator strategy and test object patterns
```

Do not create full numbered ADRs until explicitly requested.

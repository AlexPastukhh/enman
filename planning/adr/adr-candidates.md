# ADR Candidates

Status: current candidate list  
Scope: promotion backlog for decisions that may later become full numbered ADRs

## Current Relevant Candidates

| Candidate ID | Candidate ADR | Accepted decision note(s) | Why it may need full ADR | Current direction | Priority |
|---|---|---|---|---|---|
| ADR-CAND-019 | Generated client constants | ADN-052, ADN-058, ADN-059, ADN-060, ADN-082, ADN-084 | Stable error code/field constants need deterministic generation and tests. | `CC-CONST-001` cross-cutting slice + explicit generator command + artifact sync check. | High |
| ADR-CAND-023 | Constants testing strategy | ADN-058, ADN-059, ADN-060 | Important cross-cutting testing strategy for API/client contract artifacts. | Generated JSON checks + critical literal tests + generator shape/writer/checker tests. | Medium/High |
| ADR-CAND-024 | Cross-cutting/helper slice workflow | ADN-061, ADN-062, ADN-071, ADN-072 | Support behavior with implementation/test flow needs a consistent documentation model. | Use `planning/slices/cross-cutting/`, same format as business slices, flow behavior-first. | Medium |
| ADR-CAND-025 | Test responsibility split | ADN-063, ADN-064 | Important quality strategy: prevents E2E from duplicating component/API tests and supports diploma explanation. | E2E is cross-layer wiring; component tests own detailed UI. | High |
| ADR-CAND-029 | CSRF/antiforgery cross-cutting slice | ADN-073, ADN-074, ADN-075, ADN-076, ADN-077, ADN-078, ADN-079 | Cookie auth + browser unsafe commands require clear security boundary, client/server support, error normalization and tests. | CC-CSRF-001 cross-cutting slice with security-derived behavior items. | High |
| ADR-CAND-030 | Client/server contract artifact split | ADN-080, ADN-081, ADN-082, ADN-083, ADN-084, ADN-087 | Central architecture decision for client/server contract and generated artifacts. | OpenAPI structural contract + generated semantic constants. | High |
| ADR-CAND-031 | OpenAPI generation and client type strategy | ADN-081, ADN-083, ADN-085, ADN-086, ADN-087 | Defines endpoint metadata quality, Shared/openapi.json, generated TS types and client wrappers. | Generate OpenAPI types first; keep thin handwritten wrappers. | High |

## Likely Future Full ADRs

```text
ADR-0008 API error contract: ProblemDetails + ServerError + stable error codes
ADR-0009 OpenAPI structural contract + generated constants split
ADR-0010 Client constants generation and testing strategy
ADR-0011 Cross-cutting/helper slice documentation model
ADR-0012 E2E testing responsibility and Playwright infrastructure
ADR-0013 Accessible locator strategy and test object patterns
ADR-0014 Antiforgery/CSRF support for cookie-authenticated browser API
ADR-0015 Client/server contract artifacts: OpenAPI + generated semantic constants
```

Do not create full numbered ADRs until explicitly requested.

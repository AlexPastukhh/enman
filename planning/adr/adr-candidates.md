# ADR Candidates

Status: current candidate list  
Scope: promotion backlog for decisions that may later become full numbered ADRs

## Current Relevant Candidates

| Candidate ID | Candidate ADR | Accepted decision note(s) | Why it may need full ADR | Current direction | Priority |
|---|---|---|---|---|---|
| ADR-CAND-019 | Generated client constants | ADN-052, ADN-058, ADN-059, ADN-060 | Stable error code/field constants need deterministic generation and tests. | `CC-CONST-001` cross-cutting slice + explicit generator command + artifact sync check. | High |
| ADR-CAND-023 | Constants testing strategy | ADN-058, ADN-059, ADN-060 | Important cross-cutting testing strategy for API/client contract artifacts. | Generated JSON checks + critical literal tests + generator shape/writer/checker tests. | Medium/High |
| ADR-CAND-024 | Cross-cutting/helper slice workflow | ADN-061, ADN-062 | Support behavior with implementation/test flow needs a consistent documentation model. | Use `planning/slices/cross-cutting/` and keep flow behavior-first. | Medium |

## Likely Future Full ADRs

```text
ADR-0008 API error contract: ProblemDetails + ServerError + stable error codes
ADR-0009 OpenAPI structural contract + generated constants split
ADR-0010 Client constants generation and testing strategy
ADR-0011 Cross-cutting/helper slice documentation model
```

Do not create full numbered ADRs until explicitly requested.

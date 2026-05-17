

## Request Creation Client Extensions

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-REQ-CLIENT-001` | saved ApplicantParty selector | Initial implementation uses simple saved ApplicantParty selector; richer search/filter/sort remains future refinement. | future review |
| `CP-REQ-CLIENT-002` | post-create navigation | My Requests handoff preferred until command returns requestId; direct details navigation can be revisited later. | future review |

## Server Testing Extension Notes

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-SERVER-TEST-001` | backend command tests | Prefer API/integration + DB state assertions over mocks as primary behavior proof for state-changing L1 server slices. | accepted |


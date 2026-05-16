# Slice Extension Points Register

Status: active / applicant-template-per-type synchronized

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-APPL-MULTI-001` | ApplicantParty storage | many saved ApplicantParties over time. | accepted |
| `CP-APPL-DEFAULT-001` | default template | one current/default per applicant type. | accepted |
| `CP-APPL-DEFAULT-002` | create behavior | first-of-type initializes; additional same-type does not switch. | target |
| `CP-APPL-SVC-001` | application service | shared service, no SaveChanges. | accepted |
| `CP-REQ-APPL-CONTEXT-001` | request API | explicit Existing/New applicant context. | accepted |
| `CP-REQ-APPL-PICKER-001` | request UI | future picker can choose any owned saved ApplicantParty. | future |
| `CP-REQ-APPL-ATOMIC-001` | transaction | New applicant + request committed together. | accepted |
| `CP-REQ-APPL-SNAPSHOT-001` | history/read model | snapshot/reference unresolved. | future review |
| `CP-APPL-DELETE-001` | management | delete/archive/hide semantics unresolved. | future review |

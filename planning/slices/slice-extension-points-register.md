# Slice Extension Points Register

Status: active / SL-APPL-003.client full sidecar synchronized

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-APPL-MULTI-001` | ApplicantParty storage | many saved ApplicantParties over time. | accepted |
| `CP-APPL-PAGE-001` | ApplicantParty page model | one Applicant Parties page / section contains default/current templates, other saved ApplicantParties and add/action areas. | accepted |
| `CP-APPL-READ-001` | ApplicantParty read API | flat `applicantParties[]` list; client groups by `isCurrentDefault`. | implemented backend / client-consumable |
| `CP-APPL-DEFAULT-001` | default template | one current/default per applicant type; top page area highlights current/default templates. | accepted |
| `CP-APPL-DEFAULT-002` | create behavior | first account+type can initialize current/default; additional same-type create remains non-default. | implemented |
| `CP-APPL-DEFAULT-003` | explicit default action backend | make current/default backend command is implemented. | implemented backend |
| `CP-APPL-DEFAULT-004` | explicit default action client | client same-page action belongs to `SL-APPL-003.client`; use feature button/action + entity action slot. | full sidecar draft |
| `CP-APPL-ACTION-SLOT-001` | entity card/list extensibility | entity display UI may expose optional action slot/render prop without owning command logic. | accepted |
| `CP-APPL-NAMING-001` | persisted marker naming | current code uses `IsCurrentActiveVersion`; API/docs use `isCurrentDefault` / current-default template. Rename is later cleanup. | cleanup later |
| `CP-APPL-LIFECYCLE-001` | delete/archive lifecycle | delete/archive/edit/version remains future lifecycle work. | future review |
| `CP-REQ-CLIENT-001` | saved ApplicantParty selector | request creation Existing branch uses saved ApplicantParty selector; current/default is initial selection only. | implemented/drafted |
| `CP-REQ-CLIENT-002` | post-create navigation | My Requests handoff preferred until command returns requestId. | accepted |
| `CP-SERVER-TEST-001` | backend command tests | prefer API/integration + DB state assertions over mocks as primary behavior proof for state-changing L1 server slices. | accepted |

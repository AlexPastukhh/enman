# Scenario Extension Package — SC-10..SC-14

Suggested repository targets, not written by this generation step:

- `planning/diagrams/scenario-extension-package.drawio`
- `planning/diagrams/scenario-extension-package.md`
- `planning/diagrams/scenario-extension-package-preview.png`

## Pages created

1. **10 Extended Applicant Data Scenario — SC-10**  
   Client provides or updates richer applicant data.
2. **11 Request Documents Scenario — SC-11**  
   Client attaches or manages request documents.
3. **12 Clarification Scenario — SC-12**  
   Employee requests clarification and client provides a response.
4. **13 Contract Acknowledgement Scenario — SC-13**  
   Client reviews an approved contract draft and acknowledges it if required.
5. **14 Mock Verification Scenario — SC-14**  
   System performs mock/external verification and exposes the result.

No pages were created for SC-00..SC-09 or SC-15..SC-18.

## Source files read

Required planning files were read from branch `my-changes`:

- `planning/diagram-brief.md`
- `planning/diagram-scenario-spec.md`
- `planning/diagram-generation-rules-with-example.md`
- `planning/diagram-prompting-guide.md`
- `planning/diagram-common-mistakes.md`
- `planning/diagram-examples-index.md`

Canonical approved example files were read:

- `planning/examples/scenario-login-correct-v3.md`
- `planning/examples/scenario-login-correct-v3.drawio`
- `planning/examples/scenario-login-correct-v3.svg`
- `planning/examples/scenario-login-correct-v3.png`

Optional high-level context was also checked:

- `planning/scenario-to-implementation-workflow-v5-consolidated.md`

## Core package availability

The accepted first/core package was **not available** at the expected paths:

- `planning/diagrams/scenario-core-package.md`
- `planning/diagrams/scenario-core-package.drawio`

Repository search did not find an alternate `scenario-core-package` artifact. Therefore cross-page consistency was checked against the known scenario catalog and the user-provided scenario guidance, not against an accepted core package. Missing core files were not treated as permission to regenerate SC-00..SC-09 or to write files into the repository.

## Key links to core scenarios

- **SC-10** links to **Request Creation — SC-04** for profile/request-first entry and inline applicant data.
- **SC-11** links to **Request Creation — SC-04** and **Client Request Status / Result — SC-05** for document entry points.
- **SC-12** links to **Employee Request Review — SC-07** and **Client Request Status / Result — SC-05**.
- **SC-13** links to **Approval Result — SC-08**.
- **SC-14** links to **Request Creation — SC-04** and **Employee Request Review — SC-07**.

All off-page links use purple styling and `EXTND` item refs. No `EXT` item refs were used.

## Invariants and attachment points

- **SC-10-INV-01** — Invalid applicant data is not saved. Attached to `Applicant data valid?` / validation decision.
- **SC-11-INV-01** — Rejected/invalid document is not attached. Attached to `Document accepted?` / validation decision.
- **SC-12-INV-01** — Request cannot be approved/rejected while required clarification is unresolved. Attached to the unresolved clarification gating state.
- **SC-12-INV-02** — Invalid clarification response is not submitted. Attached to `Clarification response valid?` / validation decision.
- **SC-13-INV-01** — Client cannot acknowledge unavailable contract draft. Attached to `Contract draft readable/available?` / availability decision.
- **SC-14-INV-01** — Request must not proceed as verified without successful verification result. Attached to `Verification result available?` / verification decision.

## `[ALT]` usage

`[ALT]` is used only once:

- **SC-10-EXTND-02** — Alternative entry: provide applicant data inline in Request Creation — SC-04.

Reason: profile-first and request-first/inline entry both support the same user goal of providing applicant data.

No validation/error branches are marked `[ALT]`.

## Extension, variation, risk and ADR markers

- `[EXT:L2]` appears on scenario context cards to mark the package level.
- `[CORE]` appears on required behavior within each extension scenario.
- `[VAR:EXPAND]` appears on SC-10 applicant type expansion and SC-11 document rule/type expansion.
- `[VAR:MODIFY]` appears on SC-12 clarification status/workflow and SC-13 acknowledgement requirement.
- `[VAR:REPLACE]` appears on SC-14 mock/external verification provider replacement.
- `[RISK][ADR?]` appears only on SC-14 verification unavailable handling.
- `[ADR?]` appears on SC-12 workflow/status gating because clarification modifies final-decision rules and may need an explicit state-model decision.

## Open questions

- **SC-10:** Which applicant types are in scope after individual applicant data: entrepreneur, legal entity, or both?
- **SC-11:** Are documents always optional, or can specific document types become required before submission/review?
- **SC-12:** Does clarification pause review timers or assign ownership to a specific employee?
- **SC-13:** Is acknowledgement always required, or conditional by contract/request type?
- **SC-14:** For unavailable verification, should the visible behavior be retry, manual handling, or deferred review blocking?

## Self-check results

Repository write rule:

- No repository files were created, updated, deleted, moved, renamed or committed.
- Generated files were returned as reviewable sandbox artifacts only.
- Repository paths are suggested targets only.

Dark theme:

- Dark background is used on every page.
- No white/light canvas was used.
- Semantic colors follow the approved dark scenario palette.
- Visual style follows the approved `scenario-login-correct-v3` family.

Semantic correctness:

- Preconditions do not duplicate decision branches.
- Invariants are attached to enforcement points or gated states.
- Invalid/error branches are red and are not marked `[ALT]`.
- `[ALT]` is used narrowly and only where justified.
- `EXTND` is used for off-page/subscenario item refs.
- Step postconditions attach to producing steps.
- Scenario end states are compact and secondary.
- No implementation details appear: no controllers, endpoints, handlers, repositories, DbContext, SQL tables, provider APIs, queues, jobs, cloud buckets, filesystem mechanics, React components or hooks.
- Purple is used only for off-page/subscenario links.

Visual correctness:

- Main flows are visually obvious.
- Pages are scenario/use-case flow diagrams, not text-card summaries.
- Secondary connectors are kept local where practical.
- End-state blocks do not create connector webs.
- Shape sizes were chosen to keep labels, refs and markers inside nodes.
- Connector labels use meaningful relationship names and avoid generic `extend`.

## Optional reference file notes

The canonical PNG reference was available. If it had been missing, the SVG and draw.io files would have remained sufficient and the theme would still have stayed dark.

## Output status

Generated files are returned as reviewable artifacts only and were not written to the repository.

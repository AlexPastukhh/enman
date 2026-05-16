# Slice Questions Register

Status: active / client short-draft rules, OpenAPI workflow and ApplicantParty read sidecar synchronized

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `CC-API-Q-001` | `CC-API-001` / `planning/api/generated-artifact-check-workflow.md` | generated artifacts | accepted | Can generated artifacts be manually edited or reconstructed? | No. Use repo generation commands only. | Prevents fake/stale OpenAPI and TS artifacts. |
| `CC-API-Q-002` | `CC-API-001` / `planning/api/generated-artifact-check-workflow.md` | check workflow | accepted | Why can `check:api` fail after generation? | Final `git diff --exit-code` compares working tree to index; stage generated artifacts before no-extra-drift check. | Local workflow stability. |
| `CL-DRAFT-Q-001` | `client-slice-short-draft-rules-and-example.md` | drafting | accepted | Should new client drafters invent new draft forms? | No. Follow canonical examples and keep form identical unless user asks otherwise. | Prevents inconsistent sidecars. |
| `CL-DRAFT-Q-002` | `client-slice-short-draft-rules-and-example.md` | flows | accepted | Is Scenario Flow allowed to contain implementation details? | No. Scenario Flow is scenario-sourced user/system behavior for this slice only. | Prevents false behavior coverage. |
| `CL-DRAFT-Q-003` | `client-slice-short-draft-rules-and-example.md` | behavior | accepted | Are implementation details behavior items? | No. Query keys, wrappers, generated types and cache invalidation are implementation notes or tests, not behavior items. | Prevents invented behavior IDs. |
| `CL-LAYER-Q-001` | `client-layering-for-read-and-command-slices.md` | client architecture | accepted | Why are multiple entity API fetch wrappers in shared/api? | `shared/api` is the low-level client/server boundary, grouped by layer rather than entity. | Prevents unnecessary architecture churn. |
| `CL-LAYER-Q-002` | `client-layering-for-read-and-command-slices.md` | placement | accepted | Where should read-only UI live? | `entities/<entity>/ui`; features are for command/user-action behavior. | Normalizes sidecar drafts. |
| `SL-APPL-002-CLIENT-Q-001` | `SL-APPL-002-account-applicant-parties-read.client.md` | client read | accepted direction | Flat response or grouped response? | Flat `applicantParties[]`; client groups by `isCurrentDefault`. | API/UI mapping. |
| `SL-APPL-002-CLIENT-Q-002` | `SL-APPL-002-account-applicant-parties-read.client.md` | layering | accepted direction | Where does ApplicantParty read list UI live? | `entities/applicant-party/ui/*`, because this is read/display UI. | Avoids feature-read placement. |
| `SL-APPL-Q-006` | `SC-10` / `SC-10B` / `SL-APPL-*` | page model | accepted direction | Is ApplicantParty management split into two current pages? | No. One Applicant Parties page / section; SC-10B is future same-page management addendum. | Scenario wording and client sidecar placement. |

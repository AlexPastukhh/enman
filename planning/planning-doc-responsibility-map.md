# Planning Document Responsibility Map

Status: current responsibility map / architecture boundary and ApplicantParty one-page direction synchronized

## 1. Core Rule

A file should contain only content that belongs to its responsibility zone.

When a local file contains information that affects future work, synchronize it with the correct shared index/register.

## 2. Agent / Workflow Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/README.md` | Stable planning navigation and source-of-truth map; must not duplicate detailed current implementation status |
| `planning/planning-workflow-current.md` | Workflow rules, repository-edit workflow and historical/current-state reminders; concrete inventory sections are not implementation truth |
| `planning/vkr-clean-reference.md` | Clean VKR/thesis terminology, internal-to-clean mapping and evidence map for VKR-facing materials |
| `planning/planning-agent-protocol.md` | Role-level protocol, read rules, handoff rules and global do-not rules |
| `planning/agent-scope-boundaries-and-prompt-safety.md` | Rules for prompt creators and implementation agents: read broadly, change only explicit scope |
| `planning/repo-grounded-github-line-links-workflow.md` | User-facing GitHub line-link rules for repo-grounded code/docs/status explanations |
| `planning/planning-doc-responsibility-map.md` | Ownership map for planning documentation |
| `planning/replacement-file-generation-guide.md` | Archive/package generation rules for manual repo application |

## 3. VKR / Thesis Clean Reference Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/vkr-clean-reference.md` | Clean terminology and evidence mapping for VKR, presentation, defense speech and practice-report wording |
| `planning/dirty-drafts/` | Non-canonical recovery notes only; useful wording must be checked against canonical docs and rewritten before VKR use |

VKR-facing materials must not use internal planning labels such as `L1`, `L2`, `dirty draft`, `agent`, `prompt` or implementation archive terminology.

## 4. Repository Edit Responsibility

Direct GitHub edits from ChatGPT are preferred for small scoped documentation changes because they create visible commits that can be inspected and reverted independently.

By default, use one file per commit. Large generated replacement archives should be reserved for broad file/package generation when direct scoped commits are less practical.

## 5. Architecture Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/architecture/` | Architecture-level boundary maps and transition notes that are broader than one slice/API/test workflow |
| `planning/architecture/backend-legacy-and-l1-boundaries.md` | Backend legacy/L1 boundary map for cleanup, test classification, handler validation boundary and thesis/diploma architecture explanation |

Architecture docs do not replace slice docs, scenario sources, API contract docs, testing docs or ADRs.

## 6. Scenario Source Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/scenario-specification-principles.md` | Scenario specification principles and source-of-truth rules |
| `planning/scenario-domain-validation-principles.md` | Client-side vs server/domain validation distinction and domain-design input rules |
| `planning/diagrams/scenario-drafting-workflow.md` | Practical scenario draft workflow |
| `planning/diagrams/scenario-text-specs/` | Scenario text specs and addenda |
| `planning/diagrams/scenario-data/` | Scenario DATA files: entered/seen/selected/filtered/attached/referenced data only |
| `planning/diagrams/scenario-ui-specs/` | `[UI-SCENARIO]` UI-visible requirements and accepted UI decisions, not React implementation |
| `planning/diagrams/scenario-behavior-items/` | Scenario-derived, UI-scenario-derived and concern-derived behavior items |
| `planning/diagrams/scenario-questions-register.md` | Scenario/domain questions that can change scenario behavior, DATA, UI requirements or diagrams |

## 7. Slice Discovery Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/slices/README.md` | Slice-planning navigation, active backend/client slice files, source register and shared registers |
| `planning/slices/l1/README.md` | L1 client sidecar navigation and current client slice status |
| `planning/slices/draft-driven-discovery-principles.md` | Draft-driven discovery principles |
| `planning/slices/l1-slice-drafting-guide.md` | Practical L1 slice drafting workflow and templates |
| `planning/slices/slice-scenario-flow-behavior-register.md` | Maps slices/client sidecars to scenario text, DATA, UI and behavior item sources |
| `planning/slices/slice-questions-register.md` | Shared overview of currently relevant local questions, including implemented slices |
| `planning/slices/slice-extension-points-register.md` | Cross-slice extension points and change pressure |
| `planning/slices/slice-implementation-notes-register.md` | Concrete future implementation/client/testing notes |
| `planning/slices/SL-*.md` | Active parent backend/business slice docs |
| `planning/slices/*.client.md` and `planning/slices/l1/*.client.md` | Client sidecar docs for concrete client work or implemented client logic reconciliation |
| `planning/slices/cross-cutting/` | Cross-cutting/helper slices such as API, constants, CSRF and server validation |

Scenario Flow and Behavior Items for slices come from `slice-scenario-flow-behavior-register.md` and the source files it points to.

Questions/extension/implementation registers do not replace scenario source files.

## 8. Client Planning Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/client/` | Client-wide planning index and client convention navigation |
| `planning/client/cross-cutting/` | Client-wide reusable UI/client implementation conventions |

Concrete feature flow and status belongs in the relevant `.client.md` sidecar.

## 9. API / Testing / ADR Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/api/` | API contract principles, OpenAPI structural contract, API error contract and constants relationship |
| `planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | Server request validation / FluentValidation boundary rules for slices |
| `planning/testing/` | Cross-slice testing principles, E2E workflow, test object patterns |
| `planning/adr/` | ADR workflow, architecture decision notes and ADR candidates |

## 10. Evidence Link Responsibility

Repo-grounded line-link rules belong in:

```text
planning/repo-grounded-github-line-links-workflow.md
```

Other workflow docs may link to that file, but should not duplicate detailed line-link mechanics unless needed for a local role checklist.

## 11. Responsibility Decision Heuristic

```text
1. scenario text/DATA/UI/behavior source -> planning/diagrams/scenario-*/
2. slice-to-source mapping -> planning/slices/slice-scenario-flow-behavior-register.md
3. active parent backend/business slice file -> planning/slices/SL-*.md
4. client sidecar file -> planning/slices/**/*.client.md
5. slice-wide question overview -> planning/slices/slice-questions-register.md
6. extension/change pressure -> planning/slices/slice-extension-points-register.md
7. future implementation/client/testing note -> planning/slices/slice-implementation-notes-register.md
8. architecture-level cleanup / legacy-current boundary -> planning/architecture/
9. agent scope/prompt safety rule -> planning/agent-scope-boundaries-and-prompt-safety.md
10. repo-grounded evidence line links -> planning/repo-grounded-github-line-links-workflow.md
11. client-wide UI/client convention -> planning/client/cross-cutting/
12. client/server contract split -> planning/api/client-server-contract-principles.md
13. test layer boundaries / E2E workflow -> planning/testing/
14. accepted/current architecture decision -> architecture-decision-notes.md
15. possible future full ADR -> adr-candidates.md
16. VKR/presentation/defense clean wording -> planning/vkr-clean-reference.md
17. raw non-canonical recovery wording -> planning/dirty-drafts/
```

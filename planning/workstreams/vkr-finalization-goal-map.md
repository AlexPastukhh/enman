# VKR Finalization Goal Map

Status: active local workstream Goal Map / VKR finalization
Doc version: v0.1.5
Owner format: `planning/goal-map-principles-workflow-template.md`
Scope: living map for finishing the VKR text, visuals, source references, appendices, and implementation/text consistency for the Enman diploma work.

This is a living workstream map. Update it when VKR text structure, domain implementation scope, visual-material decisions, source-reference coverage, appendix/code evidence, or next action changes.

Encoding note: this version intentionally avoids non-ASCII symbols in status markers and headings to prevent mojibake in PowerShell/git diff pipelines.

## 0. Current Snapshot

Current goal:
  Finish the VKR final version for the Enman project while keeping implementation evidence truthful: complete missing thesis blocks, align text with current implementation, keep applicant-party support accurate, rebalance visuals, move excessive code evidence to appendices, strengthen testing evidence, clarify terminology, check appendices and references, and increase volume with meaningful material.

Current focus:
  Applicant-party implementation, refactor support and database table naming are synchronized in the living map. The current narrow package closes REF-DB-1 by renaming L1* database table names through EF mapping, migration, raw SQL, test DB and seed/demo updates. No frontend, error-code cleanup or thesis DOCX edit is started by this package.

Active slice:
  REF-DB-1 - Rename L1 Database Tables.

Latest completed:
  - Root planning source rules were clarified in conversation: legacy/current-like planning files must not be used as primary onboarding/current-state sources.
  - Current VKR focus was redirected to diploma finalization rather than the old L1/L2 implementation cut.
  - Current thesis draft has been inspected in prior chat context for structure, figures, captions, screenshots, code placement, and missing final blocks.
  - Applicant party type scope was resolved: Individual, IndividualEntrepreneur and LegalEntity share the same request process through ApplicantParty; applicant type differences are expressed through fields, validation and display, not through separate ConnectionRequest subclasses.
  - VKR-3A was completed: the domain model supports IndividualApplicantParty, IndividualEntrepreneurApplicantParty and LegalEntityApplicantParty with the required requisites/value objects.
  - VKR-3A-DB was completed: EF mapping, migrations and test DB support applicant-party subtype requisites.
  - VKR-3B-1 was completed: CreateConnectionRequest supports inline applicant-party input for Individual, IndividualEntrepreneur and LegalEntity through newApplicantParty / InlineApplicantPartyForRequestDto.
  - VKR-3B-2 was completed: account applicant management can create Individual, IndividualEntrepreneur and LegalEntity applicant parties separately, and created parties can be reused as existing applicants.
  - REF-0 through REF-2 were completed: accidental diff artifact was removed, ApplicantPartiesController was extracted, and create applicant response naming was made type-neutral.
  - REF-SMALL-1 through REF-SMALL-3 were completed: CreateApplicantPartyResponseDto includes applicantPartyType, the legacy frontend create-individual feature was removed, and inline request applicant DTO naming was clarified.
  - REF-APP-1 was completed: applicant-party API contracts, validators, commands, handlers and queries were moved into applicant-party feature folders.
  - TEST-INFRA-1 was completed: integration tests use a test-only NoopEmailSender through WebAppFactory instead of production Program.cs fallback.
  - REF-DB-1 was prepared in this package: L1* active database table names are renamed to clean names through EF mapping, migration, raw SQL, test DB and seed/demo updates.

Next action:
  Apply this narrow REF-DB-1 package, run backend build and Tests.EnergyManagement, verify that no active L1* table references remain outside historical migrations, and paste the diff before commit. After this package, choose VKR-1A for thesis text structure or stop and stabilize.

Parallel planning notes:
  - `REF-DB-1 - Rename L1 database tables` is closed only if the rename migration, raw SQL, test DB reset and seed/demo updates are applied and tests pass.
  - `TEST-INFRA-1 - NoopEmailSender in integration tests` is closed. Production Program.cs must keep SmtpEmailSender.
  - VKR visual/source/testing/terminology work remains separate and must not be mixed into code cleanup packages.

Resolved decisions:
  - DEC-VKR-3: Applicant party type completeness was resolved by implementing IndividualEntrepreneurApplicantParty and LegalEntityApplicantParty without separate request workflows.
  - DEC-VKR-5: Applicant type support was expanded beyond domain-only; API/UI support exists for inline request creation and separate account applicant creation.

Open decisions:
  - DEC-VKR-1: Whether the final chapter 2 should keep 2.8 only or add separate 2.9/2.10 for rezervirovanie and legitimnost if required by the methodical guide.
  - DEC-VKR-2: Visual balance and code evidence placement. Decide which code screenshots remain in chapter 3, which are replaced with text listings, and which move to appendices.
  - DEC-VKR-4: How many sources are required by final department rules and how to distribute references across chapters.
  - DEC-VKR-6: Chapter 1 diagrams. Decide whether to add both analytical diagrams: request processing flow and transition from request decision to agreement stage.
  - DEC-VKR-7: Chapter 2 design diagrams. Decide final set of project diagrams: application algorithm, architecture, domain model, request lifecycle, agreement exchange lifecycle.
  - DEC-VKR-8: Additional UI screenshots. Decide whether to add screenshots for applicant management, client request card, employee dashboard, agreement exchange working state, and attached document upload/download evidence.
  - DEC-VKR-9: Introduction and chapter conclusions. Decide final placement and wording for Introduction, chapter 1 conclusion, chapter 2 conclusion and chapter 3 conclusion.
  - DEC-VKR-10: Testing evidence in section 3.6. Decide which test result tables and screenshots are factual and can be shown without overclaim.
  - DEC-VKR-11: Terminology cleanup. Decide final wording for client/user/account/applicant and agreement exchange/proposal/version/document/file terms.
  - DEC-VKR-12: Appendix structure. Decide whether Appendix A is missing and how to organize appendices A-E.
  - DEC-VKR-13: Source/reference consistency. Decide final pass for in-text references and bibliography numbering.

Invariants:
  - Do not use legacy/current-like planning files as primary source-of-truth for current workflow or implementation state.
  - Do not claim electronic signature, legal EDMS, external registry integration, automatic contract generation, or full ECM/SED functionality unless implemented and verified.
  - Do not start new functional code slices from a map-sync command.
  - Use current VKR document, active thesis workbench docs, scenario/domain/slice docs, repo/code, and tests before changing implementation or writing implementation claims.
  - Long code and auxiliary screenshots should go to appendices when they overload the main text.
  - Applicant types must not create separate request workflows unless explicitly required; Individual, IndividualEntrepreneur and LegalEntity applicants share the same request process through ApplicantParty.
  - Applicant type differences are represented through fields, validation and display name, not through separate ConnectionRequest subclasses.
  - Current implementation supports applicant-party types in domain, persistence, API, UI inline request creation and separate applicant management; screenshots/tests still decide what the thesis may claim.
  - Applicant should be described as a person or organization on whose behalf a request is created, not only as an individual person.
  - Tables that work as classifications, mappings, requirements, checks or reference lists should stay as tables and should not be converted into overloaded diagrams.
  - Chapter 1 should not contain code, database, API or UI screenshots; it should use analytical tables and 1-2 process diagrams.
  - Chapter 2 should use design diagrams, not implementation screenshots.
  - Chapter 3 should keep UI screenshots, database evidence, key test evidence and only selected short code evidence.
  - Do not duplicate the same database relationship diagram in chapter 2 and chapter 3; chapter 2 may contain a project/domain model, while chapter 3 may contain factual DB evidence.
  - Section 3.6 must prove what was checked and what result was obtained, not only describe the testing approach.
  - Testing claims must distinguish automatic tests, manual checks, prepared-but-not-run checks and future work.
  - Chapter conclusions must summarize chapter results and must not introduce new design or implementation claims.
  - Introduction must frame the work and must not duplicate chapter 1.
  - Appendices should contain heavy code/listing/testing material and must be referenced from the main text.
  - English technical terms should be either translated or explained at first use.
  - Production email configuration must stay strict: Program.cs uses SmtpEmailSender. NoopEmailSender is test-only and belongs in integration test DI.
  - Database table rename must not affect API routes, JSON fields, frontend generated types, domain classes or l1.* error-code strings.

Update rule:
  Update this map after each meaningful VKR finalization batch: final-block insertion, source/reference pass, visual-material pass, domain implementation completeness pass, appendix/code-evidence pass, testing-evidence pass, terminology pass, implementation cleanup pass, or final formatting pass.

Planning rule:
  When planning inside the VKR finalization workstream, consult this map first, choose a narrow slice, state boundaries, then update the map after the batch if status/next action/evidence changes.

## 1. Roadmap

Status labels:

```text
DONE     completed and backed by visible evidence
NOW      active work
NEXT     next planned work
TODO     planned but not active
BLOCKED  blocked or needs a decision
```

| Phase | Phase goal | Work directions / slices | Phase verification | Status |
|---|---|---|---|---|
| Phase 0 - Map synchronization | Durable living map exists for the current VKR finalization goal. | VKR-0 - Goal Map Sync | Future chats can find this map and see current goal, slices, invariants and next action. | DONE |
| Phase 1 - Structure closure | VKR has all required final structural blocks. | VKR-1 - Structure Closure And Final Blocks; VKR-1A - Introduction And Chapter Conclusions | Document contains introduction, conclusions by chapters, final conclusion, sources, appendices plan and consistent contents. | NEXT |
| Phase 2 - Text consistency | VKR text matches implemented scope and avoids overclaim. | VKR-2 - Text/Implementation Consistency Audit | Claims are marked implemented/designed/deferred and checked against code/tests/screenshots. | TODO |
| Phase 3 - Domain completeness | Domain model is sufficient for the current applicant-party thesis narrative. | VKR-3 - Domain Completeness / Missing Class Decision; VKR-3A - Applicant Party Type Domain Completeness | Applicant-party class gap is implemented and tested; no separate request workflow was introduced. | DONE |
| Phase 4 - Visual balance | Main text has intentional diagrams/screenshots and appendices hold heavy evidence. | VKR-4 - Visual Materials Rebalance; VKR-4A - Chapter 1 Analytical Diagrams; VKR-4B - Chapter 2 Design Diagrams; VKR-4C - Chapter 3 Screenshot And Code Evidence Selection; VKR-4D - Appendix Visual/Listing Relocation | Chapter 1 has 1-2 analytical diagrams; chapter 2 has core design diagrams; chapter 3 keeps essential UI/DB/test/code evidence; excessive code screenshots move to appendices; numbering is checked. | TODO |
| Phase 5 - Source references | References exist both in text and in bibliography. | VKR-5 - Source/Bibliography Pass | Every non-trivial external claim has `[n]`; bibliography has enough distinct sources and no dead filler. | TODO |
| Phase 6 - Testing evidence and meaningful volume | Missing volume is filled with relevant content and section 3.6 proves results. | VKR-6 - Content Expansion And Appendix Evidence; VKR-6A - Section 3.6 Testing Evidence Strengthening | Added pages support thesis goals; section 3.6 contains factual tables/screenshots of checks and final demo-flow evidence. | TODO |
| Phase 7 - Terminology and appendices | Terms and appendix structure are consistent. | VKR-7A - Terminology Cleanup; VKR-8A - Appendices And Source Reference Final Pass | Applicant/agreement/technical terms are clarified; appendices are continuous and referenced; sources and in-text references are consistent. | TODO |
| Phase 8 - Final formatting | File is ready for final review. | VKR-7 - Final Formatting And Numbering | Captions, lists, contents, page breaks, styles, appendices and references are consistently formatted. | TODO |

## 2. Detailed Work Directions / Slices

### VKR-0 - Goal Map Sync

Status:
  DONE.

Purpose:
  Create a durable living map for the currently accepted VKR finalization goal.

Acceptance criteria:
  - `planning/workstreams/vkr-finalization-goal-map.md` exists.
  - Current snapshot names VKR finalization as the goal.
  - Slices cover structure, consistency, domain completeness, visuals, sources, testing evidence, terminology, appendices, volume and formatting.
  - No old L1/L2/current-state file is treated as active primary source.

Evidence:
  - Replacement archives that created and synchronized this file.

### VKR-1 - Structure Closure And Final Blocks

Status:
  NEXT.

Purpose:
  Close missing thesis structure before deeper edits.

Target outcomes:
  - Introduction exists.
  - Conclusions after chapter 1, chapter 2 and chapter 3 exist.
  - Overall conclusion exists.
  - Bibliography section exists.
  - Appendices section exists with intended appendix groups.
  - Contents/heading structure matches actual document.
  - Current facts are respected: the current VKR may already contain Conclusion, Bibliography and Appendix B, while Introduction and chapter conclusions still need attention.

Acceptance criteria:
  - Current DOCX heading extraction shows all required final blocks.
  - No invented sections are added without document support.
  - If methodical requirement needs 2.9/2.10, decision is recorded.

### VKR-1A - Introduction And Chapter Conclusions

Status:
  TODO / recommended first concrete batch under VKR-1.

Purpose:
  Add missing introduction and chapter conclusions without rewriting the whole thesis.

Target outcomes:
  - Add Introduction before chapter 1.
  - Add conclusions after chapter 1, chapter 2 and chapter 3.
  - Keep existing Conclusion if present, but verify that it closes the whole VKR rather than only chapter 3.
  - Keep existing bibliography and appendix headings if present.

Acceptance criteria:
  - Introduction contains relevance, object, subject, goal, tasks, practical significance and thesis structure.
  - Chapter conclusions summarize chapter results only.
  - Introduction frames the work and does not duplicate chapter 1.
  - Conclusions do not introduce new design or implementation claims.

### VKR-2 - Text/Implementation Consistency Audit

Status:
  TODO.

Purpose:
  Ensure thesis statements match implemented project state.

Target outcomes:
  - Claims about domain model, API, UI, storage and tests are checked against code/tests/screenshots.
  - Designed/deferred features are not presented as implemented.
  - Legacy/L1/L2/internal workflow wording is removed from final thesis text.
  - Text about applicant types distinguishes domain support from UI/API support.

Acceptance criteria:
  - Risky claims list is reviewed.
  - Overclaim guardrails are applied.
  - Implementation evidence is attached or claim is softened.

### VKR-3 - Domain Completeness / Missing Class Decision

Status:
  DONE for current applicant-party gap; monitor future thesis claims.

Purpose:
  Decide whether any missing domain classes/elements must be added for thesis completeness.

Target outcomes:
  - Compare chapter 3 domain narrative with actual `Domain.EnergyManagement` classes.
  - Identify missing class gaps that block the thesis story.
  - Implement only necessary gaps or adjust thesis wording.

Acceptance criteria:
  - Each suspected gap has a decision: implement / defer / remove claim.
  - New classes, if added, have tests or clear evidence.
  - Applicant party type gap is resolved either by implementing VKR-3A or by softening thesis wording.
  - No unnecessary extra request types, employee roles, notification model or audit trail are added only for volume.
  - No code slice starts without explicit user command after planning.

### VKR-3A - Applicant Party Type Domain Completeness

Status:
  DONE.

Purpose:
  Complete applicant party modeling for the VKR narrative without creating separate request workflows.

Target outcomes:
  - ApplicantPartyType includes Individual, IndividualEntrepreneur and LegalEntity.
  - IndividualEntrepreneurApplicantParty exists for individual entrepreneur applicants.
  - LegalEntityApplicantParty exists for legal entity applicants.
  - Common request process remains based on ApplicantParty -> ConnectionRequest.
  - Differences between applicant types are expressed through fields, validation and display name.
  - No IndividualConnectionRequest / EntrepreneurConnectionRequest / LegalEntityConnectionRequest subclasses are introduced unless explicitly justified.

Implemented scope:
  - Added IndividualEntrepreneurApplicantParty.
  - Added LegalEntityApplicantParty.
  - Added value objects: Inn, Ogrn, Ogrnip, Kpp, OrganizationName.
  - Extended ApplicantPartyType.
  - Extended EF discriminator/mapping and persistence support.
  - Added domain/integration test coverage for applicant type behavior.

Acceptance criteria:
  - Domain model supports three applicant types.
  - Existing individual applicant scenario remains compatible.
  - ConnectionRequest still references ApplicantPartyId and does not depend on concrete applicant subtype.
  - Tests cover creation/display/validation for new applicant types.
  - VKR text can honestly say that the domain model supports different applicant types.
  - VKR text can also mention UI/API support for applicant types when supported by screenshots/tests from VKR-3B-1/VKR-3B-2.

### VKR-3B-1 - Inline Applicant Type Support In Connection Request

Status:
  DONE.

Purpose:
  Support creating a new applicant party inside the CreateConnectionRequest flow without creating separate request workflows.

Target outcomes:
  - Existing/New applicant flow remains stable.
  - Public JSON field `newApplicantParty` remains stable.
  - Internal type name is `InlineApplicantPartyForRequestDto`.
  - New inline applicant supports Individual, IndividualEntrepreneur and LegalEntity.
  - ConnectionRequest remains linked to ApplicantParty.

Acceptance criteria:
  - Request creation with existing applicant still works.
  - Request creation with new Individual, IndividualEntrepreneur and LegalEntity works.
  - No separate ConnectionRequest subclasses are introduced.

### VKR-3B-2 - Separate Applicant Party Creation Endpoints And Account UI

Status:
  DONE.

Purpose:
  Allow applicant parties to be created separately in account applicant management and reused later as existing applicants.

Target outcomes:
  - `POST /api/applicant-parties/individual` exists.
  - `POST /api/applicant-parties/individual-entrepreneur` exists.
  - `POST /api/applicant-parties/legal-entity` exists.
  - Account applicant creation UI uses the broader `features/applicant-party/create` feature with an applicant type selector.
  - Legacy frontend `features/applicant-party/create-individual` is removed.

Acceptance criteria:
  - Created applicant parties appear in applicant lists.
  - Created applicant parties can be selected as existing applicants during request creation.

## 2A. Implementation Support / Refactor Cleanup Slices

### REF-0 - Remove Accidental Diff Artifact

Status:
  DONE.

Purpose:
  Remove accidental repo-stored diff/package artifacts from implementation commits.

### REF-1 - Extract ApplicantPartiesController

Status:
  DONE.

Purpose:
  Move applicant-party endpoints out of broad AppController while keeping routes stable.

### REF-2 - Create Applicant Party Response Naming Cleanup

Status:
  DONE.

Purpose:
  Use type-neutral create response naming for all applicant-party subtype creation endpoints.

### REF-SMALL-1 - Applicant Create Response Type Completion

Status:
  DONE.

Purpose:
  Include `applicantPartyType` in `CreateApplicantPartyResponseDto` so frontend callers can verify the created subtype.

### REF-SMALL-2 - Remove Legacy Frontend Create Individual Feature

Status:
  DONE.

Purpose:
  Remove the old individual-only frontend feature after replacement by the broader applicant-party create feature.

### REF-SMALL-3 - Rename Inline Request Applicant DTO

Status:
  DONE.

Purpose:
  Rename internal inline request applicant input from `CreateConnectionRequestNewApplicant*` to `InlineApplicantPartyForRequest*` while keeping public JSON field `newApplicantParty`.

### REF-APP-1 - Applicant Party Feature Folder Cleanup

Status:
  DONE.

Purpose:
  Move applicant-party DTOs, validators, commands, handlers and queries into applicant-party feature folders without changing routes, JSON fields, domain, DB or error codes.

### TEST-INFRA-1 - NoopEmailSender In Integration Tests

Status:
  DONE.

Purpose:
  Remove SMTP configuration log noise from integration tests by replacing `IEmailSender` in test DI only.

Acceptance criteria:
  - `Program.cs` still registers `SmtpEmailSender`.
  - Integration test `WebAppFactory` replaces `IEmailSender` with test-only `NoopEmailSender` by default.
  - Tests that need to inspect email can still use `WithEmailSender(...)`.

### REF-DB-1 - Rename L1 Database Tables

Status:
  DONE after this package is applied and tests pass.

Purpose:
  Rename `L1*` database tables to clean table names for implementation clarity and final evidence.

Implemented scope:
  - EF table mappings use clean table names.
  - A dedicated rename migration transitions existing databases from `L1*` names to clean names.
  - Current EF snapshot uses clean table names.
  - Active raw SQL/read-side queries use clean table names.
  - Test database reset/schema patch and e2e seed/demo SQL use clean table names.

Boundaries:
  - API routes, JSON fields, frontend and OpenAPI schema are not changed.
  - Domain class names are not changed.
  - `l1.*` error-code strings are not changed.

### VKR-4 - Visual Materials Rebalance

Status:
  TODO.

Purpose:
  Improve final visual layout by balancing analytical diagrams, design diagrams, implementation screenshots, code evidence and appendix materials.

Target outcomes:
  - Add 1-2 analytical diagrams to chapter 1 if needed.
  - Add architecture/domain/API/data diagrams to chapter 2 if needed.
  - Keep essential UI screenshots in chapter 3.
  - Add missing UI screenshots only where they prove user-facing behavior.
  - Move excessive code screenshots to appendices or replace them with short text listings.
  - Keep useful classification/mapping/check tables as tables.
  - Check known figure-numbering risks, including possible missing Figure 3.31 and Figure 3.36.

Acceptance criteria:
  - Figure list is intentional, not accidental.
  - No missing figure numbers.
  - Main text is not overloaded by small code screenshots.
  - Tables 2.10 and 2.11 remain tables unless a separate explicit decision changes them.
  - Role/state/action matrix remains a table.

### VKR-4A - Chapter 1 Analytical Diagrams

Status:
  TODO.

Purpose:
  Add minimal analytical process diagrams to chapter 1 without turning it into a technical design chapter.

Target outcomes:
  - Add Figure 1.1 - High-level client request processing flow.
  - Add Figure 1.2 - Transition from request decision to agreement-document stage.
  - Keep existing chapter 1 tables as tables.

Acceptance criteria:
  - Diagrams explain process and decision branching.
  - No code/API/database/UI screenshots are added to chapter 1.
  - Tables 1.1-1.9 remain tables unless a specific formatting issue is found.

### VKR-4B - Chapter 2 Design Diagrams

Status:
  TODO.

Purpose:
  Add design-level diagrams to make chapter 2 look like a project/design chapter, not only a set of tables.

Target outcomes:
  - Add application workflow diagram based on table 2.2.
  - Add architecture diagram: React frontend -> ASP.NET Core API -> application handlers -> Domain.EnergyManagement -> EF Core / SQL Server -> local document storage.
  - Add domain model diagram based on table 2.9.
  - Add request lifecycle diagram.
  - Add agreement exchange lifecycle diagram.
  - Add project storage/document structure diagram only if it does not duplicate tables 2.10 and 2.11.
  - Keep tables 2.10 and 2.11 as tables.

Acceptance criteria:
  - Diagrams are design-level, not implementation screenshots.
  - Tables 2.10 and 2.11 are not duplicated by a second full DB relationship scheme.
  - Role/state/action matrix remains a table.

### VKR-4C - Chapter 3 Screenshot And Code Evidence Selection

Status:
  TODO.

Purpose:
  Keep chapter 3 evidence strong but not overloaded with small code screenshots.

Target outcomes:
  - Keep key UI screenshots: login/registration, request form, validation error, client request list, employee request review, agreement exchange, final refusal or completion.
  - Add missing UI screenshots if needed: applicant management, client request card, employee dashboard, agreement exchange with document/version, upload/download evidence.
  - Keep only 4-8 most important code evidence items in the main text.
  - Prefer short text listings for code where possible.
  - Move long code screenshots/listings to appendices.

Acceptance criteria:
  - Chapter 3 proves implementation through UI, DB, tests and selected code.
  - Main text is not dominated by small code screenshots.
  - Each screenshot has a clear reason to exist.

### VKR-4D - Appendix Visual/Listing Relocation

Status:
  TODO.

Purpose:
  Move heavy auxiliary evidence out of the main text and into appendices.

Target outcomes:
  - Appendix for domain code listings.
  - Appendix for server code listings.
  - Appendix for client code listings.
  - Appendix for tests.
  - Appendix for additional screenshots if needed.

Acceptance criteria:
  - Main text references appendices where extended evidence is stored.
  - Long code is not duplicated in both main text and appendices.
  - Appendix materials support the thesis rather than add filler.

### VKR-5 - Source/Bibliography Pass

Status:
  TODO.

Purpose:
  Add correct source references and bibliography.

Target outcomes:
  - In-text references use `[n]` or `[n, page x]` where needed.
  - Bibliography contains distinct sources, not repeated mentions.
  - Technology docs, standards, methodology and research sources are distributed by chapter.
  - Every source from bibliography is checked for actual in-text use.
  - Every in-text `[n]` reference is checked against bibliography.
  - Bibliography numbering is checked after edits.

Acceptance criteria:
  - Every bibliography item is cited at least once or intentionally justified.
  - Chapter 1 and 2 have enough external support.
  - Chapter 3 uses official technology docs and own implementation evidence appropriately.
  - No bibliography filler remains without actual use.

### VKR-6 - Content Expansion And Appendix Evidence

Status:
  TODO.

Purpose:
  Add missing volume with meaningful thesis content.

Target outcomes:
  - Expand weak sections through explanation, tables and evidence, not filler.
  - Add appendices with selected code listings, API fragments, UI screenshots and test materials.
  - Keep main text readable.
  - Strengthen section 3.6 through factual checks and evidence rather than extra theory.

Acceptance criteria:
  - Added pages support thesis goals.
  - Appendices are referenced from main text.
  - Long code is not duplicated in main text and appendix.

### VKR-6A - Section 3.6 Testing Evidence Strengthening

Status:
  TODO.

Purpose:
  Turn section 3.6 from a testing-approach description into factual evidence of what was checked and what result was obtained.

Target outcomes:
  - Add table: environment and test execution order.
  - Add table: domain rule coverage.
  - Add table: scenario-to-check traceability.
  - Rework 3.6.4 as final demo-flow verification.
  - Add 2-3 factual screenshots of test/check results.

Acceptance criteria:
  - Section 3.6 distinguishes automatic tests from manual checks.
  - No unrun checks are described as passed.
  - Each testing screenshot/table has a clear evidence role.
  - 3.6.4 shows the main user process from login to agreement exchange.
  - Testing evidence proves results, not only the testing approach.

### VKR-7A - Terminology Cleanup

Status:
  TODO.

Purpose:
  Make thesis terminology consistent before final proofreading.

Target outcomes:
  - Clarify client / user / client account / applicant / employee.
  - Clarify agreement exchange / proposal / version / document / file.
  - Explain API, DTO, UI, DB, endpoint, handler, ProblemDetails, Result/Failure at first use.
  - Replace or explain English terms: review, exchange, proposal, backend, frontend, domain, endpoint, handler, failure, snapshot.
  - Add a short terminology block in or near chapter 1.1 if needed.

Acceptance criteria:
  - Applicant is not described only as an individual person.
  - Applicant is described as a person or organization on whose behalf a request is created.
  - Technical terms are understandable in Russian academic text.
  - Class names remain in parentheses where useful.

### VKR-8A - Appendices And Source Reference Final Pass

Status:
  TODO.

Purpose:
  Check final appendices, source references and bibliography consistency.

Target outcomes:
  - Check why Appendix B exists while Appendix A may be missing.
  - Organize appendices A-E if needed.
  - Move long code and auxiliary screenshots to appendices.
  - Check every source is referenced in text.
  - Check every `[n]` reference exists in bibliography.
  - Check bibliography numbering after edits.

Acceptance criteria:
  - Appendix structure is continuous and meaningful.
  - Main text references appendix materials.
  - Source list and in-text references are consistent.
  - Heavy code/testing materials are in appendices rather than cluttering the main text.

### VKR-7 - Final Formatting And Numbering

Status:
  TODO.

Purpose:
  Prepare final DOCX for review.

Target outcomes:
  - Heading styles and contents are consistent.
  - Table/figure/listing numbering is fixed.
  - Bibliography and appendices are formatted.
  - Page breaks and captions are checked.

Acceptance criteria:
  - DOCX render check passes visually.
  - Tables/figures/listings have no orphan captions.
  - Automatic contents can be generated or manually matches actual headings.

## 3. Whole Picture

The final result should be a coherent diploma package:

```text
analysis chapter
-> design chapter
-> implementation and verification chapter
-> conclusion and references
-> appendices with heavy evidence
```

The thesis should show:

```text
problem and process
-> design decisions
-> implemented software structure
-> domain/server/client/storage/test evidence
-> final limitations and future development
```

Applicant modeling principle:

```text
ClientAccount
-> ApplicantParty
-> ConnectionRequest
```

ApplicantParty may represent an individual, individual entrepreneur or legal entity. The request lifecycle remains shared; applicant type affects required fields, validation and display only.

Chapter responsibility principle:

```text
chapter 1:
  subject meaning and problem;

chapter 2:
  design decision;

chapter 3:
  implementation;

section 3.6:
  evidence of what was checked and what result was obtained.
```

## 4. Source Boundaries

Primary current planning/root onboarding chain:

```text
planning/README.md
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/agent-roles-and-required-actions.md
planning/root-source-sync-register.md
planning/documentation-action-log.md
```

Legacy/current-like files listed by the user are not primary onboarding/current-state sources. Treat them as historical context or cleanup candidates only when explicitly asked.

For VKR content and wording, prefer:

```text
current VKR DOCX;
planning/thesis/vkr-topic-workbench/**;
planning/vkr-clean-reference.md when available;
scenario specs / behavior items / scenario data;
active domain/slice docs;
repo code and tests;
examples/methodical materials for formatting and structure.
```

## 4A. Visual-Material Principle

Use each visual type for its strongest role:

```text
tables:
  classifications, requirements, mappings, checks, role/action matrices;

diagrams:
  processes, state transitions, architecture, domain relations, scenario pipelines;

UI screenshots:
  proof of implemented user-facing behavior;

code listings:
  short evidence of key implementation decisions;

appendices:
  long code, auxiliary screenshots, extended tests and heavy evidence.
```

Chapter-specific target:

```text
chapter 1:
  tables + 1-2 analytical diagrams;

chapter 2:
  tables + 4-5 design diagrams;

chapter 3:
  UI screenshots + DB evidence + tests + selected short code evidence;

appendices:
  long code, additional screenshots, extended test materials.
```

Do not convert these materials into diagrams by default:

```text
Table 2.10 - data storage structure;
Table 2.11 - storage relationships;
role/state/action table;
requirements tables;
tools tables;
testing scenario tables;
Result/error/exception tables.
```

## 4B. Testing-Evidence Principle

Section 3.6 should answer:

```text
what was checked;
how it was checked;
what result was obtained;
what evidence proves it.
```

Testing wording must distinguish:

```text
automatic tests;
manual checks;
prepared-but-not-run checks;
future work.
```

Do not write `passed` for a check that was not actually run.

## 4C. Terminology Principle

Use stable Russian terms and keep class names in parentheses where useful:

```text
client / user of web application / client account;
applicant as person or organization;
employee as service-contour user;
request as process object;
agreement exchange as post-approval document negotiation;
agreement proposal as one sent proposal version;
document as file attached to a proposal;
document reference as metadata/storage key.
```

English technical terms should be translated or explained at first use:

```text
review -> request review (`RequestReview`);
exchange -> agreement exchange (`AgreementProposalExchange`);
proposal -> agreement proposal (`AgreementProposal`);
backend -> server-side part;
frontend -> client-side part;
domain -> domain model / domain project;
endpoint -> API method;
handler -> command handler;
failure -> unsuccessful result;
snapshot -> object state before operation.
```

## 5. Next Action

Recommended next action:

```text
Apply and verify the current REF-DB-1 package: table rename migration, EF mapping/snapshot, raw SQL, test DB and seed/demo updates. After that, choose VKR-1A for thesis text structure or stop and stabilize.
```

Suggested first batch boundary:

```text
- inspect current DOCX headings;
- confirm which final blocks already exist;
- add/repair Introduction;
- add chapter conclusions;
- decide whether 2.9/2.10 must exist;
- do not change implementation code;
- do not rebalance visuals, testing evidence or references in the same batch unless explicitly requested.
```

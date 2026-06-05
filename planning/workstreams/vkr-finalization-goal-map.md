# VKR Finalization Goal Map

Status: active local workstream Goal Map / VKR finalization
Doc version: v0.1.1
Owner format: `planning/goal-map-principles-workflow-template.md`
Scope: living map for finishing the VKR text, visuals, source references, appendices, and implementation/text consistency for the Enman diploma work.

This is a living workstream map. Update it when VKR text structure, domain implementation scope, visual-material decisions, source-reference coverage, appendix/code evidence, or next action changes.

Encoding note: this version intentionally avoids non-ASCII symbols in status markers and headings to prevent mojibake in PowerShell/git diff pipelines.

## 0. Current Snapshot

Current goal:
  Finish the VKR final version for the Enman project: complete missing thesis blocks, align text with current implementation, add missing domain classes only if needed for VKR completeness, rebalance visuals, move excessive code evidence to appendices, and increase volume with meaningful material.

Current focus:
  Map synchronized. No next functional slice is started by this map-sync batch.

Active slice:
  none selected / next-slice decision pending.

Latest completed:
  - Root planning source rules were clarified in conversation: legacy/current-like planning files must not be used as primary onboarding/current-state sources.
  - Current VKR focus was redirected to diploma finalization rather than the old L1/L2 implementation cut.
  - Current thesis draft has been inspected in prior chat context for structure, figures, captions, screenshots, code placement, and missing final blocks.

Next action:
  Choose and run the first narrow VKR finalization slice. Recommended first slice: `VKR-1 - Structure Closure And Final Blocks`, because it establishes the missing thesis end blocks and prevents later visual/source work from drifting.

Open decisions:
  - DEC-VKR-1: Whether the final chapter 2 should keep 2.8 only or add separate 2.9/2.10 for rezervirovanie and legitimnost if required by the methodical guide.
  - DEC-VKR-2: Which code screenshots remain in the main chapter 3 and which move to appendices.
  - DEC-VKR-3: Which missing domain classes are truly required for VKR completeness and which are unnecessary implementation expansion.
  - DEC-VKR-4: How many sources are required by final department rules and how to distribute references across chapters.

Invariants:
  - Do not use legacy/current-like planning files as primary source-of-truth for current workflow or implementation state.
  - Do not claim electronic signature, legal EDMS, external registry integration, automatic contract generation, or full ECM/SED functionality unless implemented and verified.
  - Do not start new functional code slices from a map-sync command.
  - Use current VKR document, active thesis workbench docs, scenario/domain/slice docs, repo/code, and tests before changing implementation or writing implementation claims.
  - Long code and auxiliary screenshots should go to appendices when they overload the main text.

Update rule:
  Update this map after each meaningful VKR finalization batch: final-block insertion, source/reference pass, visual-material pass, domain implementation completeness pass, appendix/code-evidence pass, or final formatting pass.

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
| Phase 0 - Map synchronization | Durable living map exists for the current VKR finalization goal. | VKR-0 - Goal Map Sync | Future chats can find this map and see current goal, slices, invariants and next action. | DONE after applying this archive |
| Phase 1 - Structure closure | VKR has all required final structural blocks. | VKR-1 - Structure Closure And Final Blocks | Document contains introduction, conclusions by chapters, conclusion, sources, appendices plan and consistent contents. | NEXT |
| Phase 2 - Text consistency | VKR text matches implemented scope and avoids overclaim. | VKR-2 - Text/Implementation Consistency Audit | Claims are marked implemented/designed/deferred and checked against code/tests/screenshots. | TODO |
| Phase 3 - Domain completeness | Domain model is sufficient for the thesis narrative. | VKR-3 - Domain Completeness / Missing Class Decision | Missing domain classes are either implemented, explicitly deferred or removed from thesis claims. | TODO |
| Phase 4 - Visual balance | Main text has the right figures/screenshots and appendices hold heavy evidence. | VKR-4 - Visual Materials Rebalance | Chapter 1/2 diagrams are added where needed; excessive code screenshots moved/replaced; figure numbering checked. | TODO |
| Phase 5 - Source references | References exist both in text and in bibliography. | VKR-5 - Source/Bibliography Pass | Every non-trivial external claim has `[n]`; bibliography has enough distinct sources and no dead filler. | TODO |
| Phase 6 - Meaningful volume | Missing volume is filled with relevant content, not filler. | VKR-6 - Content Expansion And Appendix Evidence | Added volume comes from explanations, tables, diagrams, appendices, test evidence and careful conclusions. | TODO |
| Phase 7 - Final formatting | File is ready for final review. | VKR-7 - Final Formatting And Numbering | Captions, lists, contents, page breaks, styles, appendices and references are consistently formatted. | TODO |

## 2. Detailed Work Directions / Slices

### VKR-0 - Goal Map Sync

Status:
  DONE after applying this archive.

Purpose:
  Create a durable living map for the currently accepted VKR finalization goal.

Acceptance criteria:
  - `planning/workstreams/vkr-finalization-goal-map.md` exists.
  - Current snapshot names VKR finalization as the goal.
  - Slices cover structure, consistency, domain completeness, visuals, sources, volume and formatting.
  - No old L1/L2/current-state file is treated as active primary source.

Evidence:
  - This replacement archive.

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

Acceptance criteria:
  - Current DOCX heading extraction shows all required final blocks.
  - No invented sections are added without document support.
  - If methodical requirement needs 2.9/2.10, decision is recorded.

### VKR-2 - Text/Implementation Consistency Audit

Status:
  TODO.

Purpose:
  Ensure thesis statements match implemented project state.

Target outcomes:
  - Claims about domain model, API, UI, storage and tests are checked against code/tests/screenshots.
  - Designed/deferred features are not presented as implemented.
  - Legacy/L1/L2/internal workflow wording is removed from final thesis text.

Acceptance criteria:
  - Risky claims list is reviewed.
  - Overclaim guardrails are applied.
  - Implementation evidence is attached or claim is softened.

### VKR-3 - Domain Completeness / Missing Class Decision

Status:
  TODO.

Purpose:
  Decide whether any missing domain classes/elements must be added for thesis completeness.

Target outcomes:
  - Compare chapter 3 domain narrative with actual `Domain.EnergyManagement` classes.
  - Identify missing class gaps that block the thesis story.
  - Implement only necessary gaps or adjust thesis wording.

Acceptance criteria:
  - Each suspected gap has a decision: implement / defer / remove claim.
  - New classes, if added, have tests or clear evidence.
  - No code slice starts without explicit user command after planning.

### VKR-4 - Visual Materials Rebalance

Status:
  TODO.

Purpose:
  Improve final visual layout.

Target outcomes:
  - Add 1-2 analytical diagrams to chapter 1 if needed.
  - Add architecture/domain/API/data diagrams to chapter 2 if needed.
  - Keep essential UI screenshots in chapter 3.
  - Move excessive code screenshots to appendices or replace with short text listings.

Acceptance criteria:
  - Figure list is intentional, not accidental.
  - No missing figure numbers.
  - Main text is not overloaded by small code screenshots.

### VKR-5 - Source/Bibliography Pass

Status:
  TODO.

Purpose:
  Add correct source references and bibliography.

Target outcomes:
  - In-text references use `[n]` or `[n, page x]` where needed.
  - Bibliography contains distinct sources, not repeated mentions.
  - Technology docs, standards, methodology and research sources are distributed by chapter.

Acceptance criteria:
  - Every bibliography item is cited at least once or intentionally justified.
  - Chapter 1 and 2 have enough external support.
  - Chapter 3 uses official technology docs and own implementation evidence appropriately.

### VKR-6 - Content Expansion And Appendix Evidence

Status:
  TODO.

Purpose:
  Add missing volume with meaningful thesis content.

Target outcomes:
  - Expand weak sections through explanation, tables and evidence, not filler.
  - Add appendices with selected code listings, API fragments, UI screenshots and test materials.
  - Keep main text readable.

Acceptance criteria:
  - Added pages support thesis goals.
  - Appendices are referenced from main text.
  - Long code is not duplicated in main text and appendix.

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

## 5. Next Action

Recommended next action:

```text
Run VKR-1 - Structure Closure And Final Blocks.
```

Suggested first batch boundary:

```text
- inspect current DOCX headings;
- decide whether 2.9/2.10 must exist;
- draft/insert missing introduction, chapter conclusions, final conclusion, bibliography heading and appendices plan;
- do not change implementation code;
- do not rebalance visuals in the same batch unless explicitly requested.
```

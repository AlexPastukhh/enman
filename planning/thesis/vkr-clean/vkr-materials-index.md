# VKR Materials Index

Status: draft / section-drafting and reviewer workflow synchronized  
Scope: navigation for clean VKR materials

## 1. Entry Points

| File | Purpose |
|---|---|
| `README.md` | Purpose and boundaries of `planning/thesis/vkr-clean/` |
| `vkr-materials-index.md` | Navigation index for VKR materials |
| `vkr-outline.md` | Working structure of the VKR |
| `evidence-map.md` | Source/evidence mapping for text claims |
| `terminology.md` | Domain and technical terminology |

## 2. Methodical And Introductory Support

| File | Purpose |
|---|---|
| `format-and-methodical-requirements.md` | Methodical requirements, VKR volume, defense materials, code/applications rules and practical structure guidance |
| `definitions-abbreviations.md` | Candidate "Определения, обозначения и сокращения" section |
| `introduction-draft.md` | Draft introduction for the VKR topic |
| `existing-solutions-analysis.md` | Draft analysis of alternatives and justification of custom web application |
| `literature-plan.md` | Bibliography collection plan and source categories |

These files are relatively stable and can be developed while implementation work continues.

## 3. Writing Protocol

| File | Purpose |
|---|---|
| `writing-protocol/README.md` | Entry point for controlled VKR text assembly |
| `writing-protocol/source-provenance-protocol.md` | Rules for using project artifacts, implementation evidence, research and author analysis |
| `writing-protocol/section-card-template.md` | Template for preparing each subsection before drafting |
| `writing-protocol/research-usage-rules.md` | Rules for using Deep Research and external sources without turning text into compilation |
| `writing-protocol/chapter-section-question-map.md` | Project-centered questions for future VKR subsections |
| `writing-protocol/page-fragment-checklist.md` | Quick paragraph/table/figure checklist |
| `writing-protocol/pilot-section-existing-solutions.md` | Pilot draft for the “Анализ существующих решений” subsection |

Use this section before expanding chapter drafts. It helps keep each paragraph tied to the ООО «ЗСК» project, not to generic theory.

## 4. Section Draft Workflow

| File / material | Purpose |
|---|---|
| `section-drafts/README.md` | Entry point for subsection draft workflow |
| `section-drafts/vkr-section-drafting-workflow.md` | Current short/full draft workflow and coordinator loop |
| `section-drafts/reviewer-workflow.md` | Reviewer process, reviewer roles and feedback consolidation |
| `section-drafts/reviewer-prompts.md` | Reusable prompts for content, structure and style/originality reviewer chats |
| `section-drafts/short-draft-template.md` | Chat-first short draft template |
| `section-drafts/full-draft-template.md` | Full draft attempt template |
| `section-drafts/full-draft-review-checklist.md` | Checklist for reviewing full draft attempts |
| `section-drafts/fragment-bank.md` | Bank of successful fragments, transitions, conclusions and reusable formulations |
| `section-drafts/section-draft-register.md` | Register of subsection draft statuses and review state |

Short drafts are normally written in chat first and are not saved as files by default.  
Full draft attempts, reviewer outputs, consolidation notes and reusable fragments are stored as files.

## 5. VKR Drafting Roles

| Role | Responsibility |
|---|---|
| VKR Coordinator & Drafter | Owns high-level VKR planning, prepares short drafts, creates full draft archive attempts, consolidates reviewer feedback and maintains fragment/status docs |
| VKR Content Reviewer | Checks whether a draft answers the project question and preserves core VKR lines: client requests, document flow, contracts/documents and notifications |
| VKR Structure Reviewer | Checks form, subsection placement, intro/body/conclusion, chapter boundaries and visual/table/application placement |
| VKR Style & Originality Reviewer | Checks style, generic theory, template phrases, AI-like wording, citation needs and project-specific rewriting opportunities |
| Documentation Keeper | Keeps navigation/workflow/register docs synchronized; does not write VKR section text unless explicitly asked |

## 6. Chapter Drafts

| File | Purpose |
|---|---|
| `chapter-1-problem-domain.md` | Subject domain, problem, relevance, goal and tasks |
| `chapter-2-requirements-and-design.md` | Requirements, scenarios, design and architecture draft |
| `chapter-3-implementation.md` | Implementation, API, database, UI and testing draft |
| `chapter-4-lifecycle-and-deployment.md` | Deployment, operation, limitations and future work draft |

Chapter files are not the only source of truth. Update clean source files and section-drafts workflow files first, then expand chapters.

## 7. Clean Source Files

| File | Purpose |
|---|---|
| `clean-requirements.md` | Short functional and non-functional requirement summary |
| `functional-specification.md` | Textual behavior specification: actors, preconditions, main flow, alternatives, postconditions |
| `clean-use-cases.md` | Earlier use-case/scenario summary |
| `use-case-diagrams-plan.md` | Use Case diagrams and explanatory text plan |
| `clean-data-requirements.md` | Input, visible and stored data requirements |
| `clean-domain-model.md` | Domain model and lifecycle explanations |
| `clean-architecture.md` | Client-server architecture and design approach |
| `api-contract-and-client-server-sync.md` | API contract, OpenAPI, generated constants and frontend/backend synchronization |
| `clean-database-design.md` | Database design and persistence description |
| `clean-ui-description.md` | User interface description |
| `clean-testing.md` | Testing strategy and verification |
| `clean-results-and-future-work.md` | Results and further development |

## 8. Literature And Research Support

| Material | Purpose |
|---|---|
| Literature candidates | Candidate sources for VKR bibliography |
| Recommended source selection | Working 45-source selection for methodical requirement |
| Source-to-section mapping | Shows which sources support which VKR section |
| Reference status notes | Separates core, official, reference-only and caution sources |

Research materials should support the project text, not replace it.

## 9. Diagram And Planning Extraction Files

| File | Purpose |
|---|---|
| `visuals-and-diagrams-plan.md` | Diagram list, placement and content plan |
| `planning-to-vkr-extraction-map.md` | Mapping from planning/slices/API/testing docs to VKR sections |

Final diagram generation should follow `planning/diagrams/diagram-prompt-generation-workflow.md` and `planning/diagrams/drawio-diagram-generation-workflow.md`.

## 10. Expanded Drafts

| Material | Purpose |
|---|---|
| Chapter 1 expanded draft | Longer working text for analysis/problem domain |
| Chapter 2 expanded draft | Longer working text for requirements/design |
| Chapter 3 expanded draft | Longer working text for implementation/testing |
| Missing materials checklist | INSERT/TODO list for screenshots, diagrams, tables, sources and repo checks |

Expanded drafts are working material. They should be cleaned and source-checked before final VKR text.

## 11. Related Working Files Outside `planning/thesis/vkr-clean/`

| File | Purpose |
|---|---|
| `planning/vkr-formulation-guide.md` | Internal wording guide |
| `planning/vkr-work-context-current.md` | Current VKR context, implementation baseline and next steps |
| `planning/thesis/presentation/README.md` | Defense materials overview |
| `planning/thesis/presentation/slide-outline.md` | Slide structure |
| `planning/thesis/presentation/speech-draft.md` | Speech draft |
| `planning/thesis/presentation/demo-script.md` | Demo plan |
| `planning/thesis/presentation/visuals-needed.md` | Visuals needed for defense |
| `planning/thesis/presentation/predefense-expanded-draft.md` | Expanded pre-defense text draft |

## 12. Current Next Steps

```text
1. Write short drafts in chat before creating full draft files.
2. Use section-drafts/vkr-section-drafting-workflow.md to move from short draft to full draft attempt.
3. Use reviewer-workflow.md after full draft v1.
4. Send full draft v1 to Content / Structure / Style & Originality reviewer chats when useful.
5. Consolidate reviewer feedback in the Coordinator/Drafter chat.
6. Move successful fragments to fragment-bank.md.
7. Use section-draft-register.md to track draft/review/status.
8. Prepare full draft v2 from consolidated feedback.
9. Before final chapter text, recheck repo evidence for each implemented claim.
```

# VKR Materials Index

Status: draft  
Scope: navigation for clean VKR materials

## 1. Entry Points

| File | Purpose |
|---|---|
| `README.md` | Purpose and boundaries of `vkr-clean/` |
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

## 4. Chapter Drafts

| File | Purpose |
|---|---|
| `chapter-1-problem-domain.md` | Subject domain, problem, relevance, goal and tasks |
| `chapter-2-requirements-and-design.md` | Requirements, scenarios, design and architecture draft |
| `chapter-3-implementation.md` | Implementation, API, database, UI and testing draft |
| `chapter-4-lifecycle-and-deployment.md` | Deployment, operation, limitations and future work draft |

Chapter files are not the only source of truth. Update clean source files first, then expand chapters.

## 5. Clean Source Files

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

## 6. Literature And Research Support

| Material | Purpose |
|---|---|
| Literature candidates | Candidate sources for VKR bibliography |
| Recommended source selection | Working 45-source selection for methodical requirement |
| Source-to-section mapping | Shows which sources support which VKR section |
| Reference status notes | Separates core, official, reference-only and caution sources |

Research materials should support the project text, not replace it.

## 7. Diagram And Planning Extraction Files

| File | Purpose |
|---|---|
| `visuals-and-diagrams-plan.md` | Diagram list, placement and content plan |
| `planning-to-vkr-extraction-map.md` | Mapping from planning/slices/API/testing docs to VKR sections |

Final diagram generation should follow `planning/diagrams/diagram-prompt-generation-workflow.md` and `planning/diagrams/drawio-diagram-generation-workflow.md`.

## 8. Expanded Drafts

| Material | Purpose |
|---|---|
| Chapter 1 expanded draft | Longer working text for analysis/problem domain |
| Chapter 2 expanded draft | Longer working text for requirements/design |
| Chapter 3 expanded draft | Longer working text for implementation/testing |
| Missing materials checklist | INSERT/TODO list for screenshots, diagrams, tables, sources and repo checks |

Expanded drafts are working material. They should be cleaned and source-checked before final VKR text.

## 9. Related Working Files Outside `vkr-clean/`

| File | Purpose |
|---|---|
| `planning/vkr-formulation-guide.md` | Internal wording guide |
| `planning/vkr-work-context-current.md` | Current VKR context, implementation baseline and next steps |
| `presentation/README.md` | Defense materials overview |
| `presentation/slide-outline.md` | Slide structure |
| `presentation/speech-draft.md` | Speech draft |
| `presentation/demo-script.md` | Demo plan |
| `presentation/visuals-needed.md` | Visuals needed for defense |
| `presentation/predefense-expanded-draft.md` | Expanded pre-defense text draft |

## 10. Current Next Steps

```text
1. Use writing-protocol/ before expanding chapter drafts.
2. Use source-provenance-protocol.md to decide whether a paragraph comes from project artifacts, implementation evidence, research or author analysis.
3. Use pilot-section-existing-solutions.md as the first test subsection.
4. Build final comparison table for existing solutions from research materials.
5. Expand chapter 1 from the pilot section, introduction draft and existing-solutions analysis.
6. Expand chapter 2 from functional-specification.md, clean-architecture.md and api-contract-and-client-server-sync.md.
7. Expand chapter 3 from clean-ui-description.md, clean-testing.md and repo evidence.
8. Before final chapter text, recheck repo evidence for each implemented claim.
```

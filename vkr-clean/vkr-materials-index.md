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

## 2. Chapter Drafts

| File | Purpose |
|---|---|
| `chapter-1-problem-domain.md` | Subject domain, problem, relevance, goal and tasks |
| `chapter-2-requirements-and-design.md` | Requirements, scenarios, design and architecture draft |
| `chapter-3-implementation.md` | Implementation, API, database, UI and testing draft |
| `chapter-4-lifecycle-and-deployment.md` | Deployment, operation, limitations and future work draft |

Chapter files are not the only source of truth. Update clean source files first, then expand chapters.

## 3. Clean Source Files

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

## 4. Diagram And Planning Extraction Files

| File | Purpose |
|---|---|
| `visuals-and-diagrams-plan.md` | Diagram list, placement and content plan |
| `planning-to-vkr-extraction-map.md` | Mapping from planning/slices/API/testing docs to VKR sections |

## 5. Related Working Files Outside `vkr-clean/`

| File | Purpose |
|---|---|
| `planning/vkr-formulation-guide.md` | Internal wording guide |
| `planning/vkr-work-context-current.md` | Current VKR context, implementation baseline and next steps |
| `presentation/README.md` | Defense materials overview |
| `presentation/slide-outline.md` | Slide structure |
| `presentation/speech-draft.md` | Speech draft |
| `presentation/demo-script.md` | Demo plan |
| `presentation/visuals-needed.md` | Visuals needed for defense |

## 6. Current Next Steps

```text
1. Use this index and planning/vkr-work-context-current.md as entry points.
2. Build diagrams from visuals-and-diagrams-plan.md and use-case-diagrams-plan.md.
3. Expand chapter 2 from functional-specification.md, clean-architecture.md and api-contract-and-client-server-sync.md.
4. Expand chapter 3 from clean-ui-description.md, clean-testing.md and repo evidence.
5. Before final chapter text, recheck repo evidence for each implemented claim.
```

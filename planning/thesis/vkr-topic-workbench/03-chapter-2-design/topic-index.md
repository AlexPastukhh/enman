# Topic Index — Chapter 2 Design

Status: synchronized / chapter-level index

## Chapter purpose

Chapter 2 discloses the design of the web application: roles, access boundaries, main scenarios, domain model, lifecycles, architecture, data/file storage, API/frontend-backend interaction and user interface design.

## Active VKR point folders

| Folder | VKR point meaning | Topic files | Status |
|---|---|---:|---|
| `01-requirements-roles-scenarios/` | 2.1 Requirements, roles and main scenarios | 1 | active |
| `02-domain-model-lifecycles/` | 2.2 Domain model and lifecycles | 3 | active |
| `03-architecture/` | 2.3 Application architecture | 2 | active |
| `04-data-and-file-storage/` | 2.4 Data and file storage design | 3 | active |
| `05-api-frontend-backend/` | 2.5 API and frontend/backend interaction | 4 | active |
| `06-user-interface/` | 2.6 User interface design | 4 | active |

## Topic map

```text
01-requirements-roles-scenarios/
└─ 01-roles-and-main-scenarios.topic.md

02-domain-model-lifecycles/
├─ 01-domain-model-core.topic.md
├─ 02-request-lifecycle.topic.md
└─ 03-agreement-exchange-lifecycle.topic.md

03-architecture/
├─ 01-application-architecture-overview.topic.md
└─ 02-frontend-backend-responsibilities.topic.md

04-data-and-file-storage/
├─ 01-data-storage-model.topic.md
├─ 02-file-storage-and-document-metadata.topic.md
└─ 03-storage-boundaries-and-limitations.topic.md

05-api-frontend-backend/
├─ 01-api-contract-overview.topic.md
├─ 02-read-and-command-api.topic.md
├─ 03-validation-and-error-contract.topic.md
└─ 04-file-api-upload-download.topic.md

06-user-interface/
├─ 01-ui-structure-and-navigation.topic.md
├─ 02-request-and-review-ui-flow.topic.md
├─ 03-agreement-exchange-ui-flow.topic.md
└─ 04-ui-states-validation-and-errors.topic.md
```

## Disclosure flow

```text
roles and scenarios
→ domain model and lifecycles
→ architecture
→ data/file storage
→ API/frontend-backend contract
→ user interface
```

## Synchronization rule

When a point folder or topic changes, update:

1. this chapter-level index;
2. the local child index if the block has one;
3. visual evidence notes if a diagram/table/screenshot plan changes;
4. source/research links if the topic needs external or repo evidence.

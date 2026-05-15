# Documentation Update Workflow Index

Status: current documentation-update workflow index  
Scope: documentation-only agents, status reconciliation, archive generation and navigation updates

## 1. Purpose

This folder defines how documentation-only chats/agents should update planning docs.

A documentation update agent must:

```text
- read current repo state before changing docs;
- reconcile docs with implemented/planned/deferred status;
- update navigation/responsibility maps together with new docs;
- create archive packages for manual application;
- never write directly to GitHub unless explicitly asked.
```

## 2. Files

```text
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/documentation/documentation-update-agent-prompt.md
```

Related archive/package guide:

```text
planning/replacement-file-generation-guide.md
```

## 3. Read Order For Documentation-Only Work

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/planning-agent-protocol.md
4. planning/planning-doc-responsibility-map.md
5. planning/documentation/README.md
6. planning/documentation/documentation-update-workflow.md
7. planning/documentation/status-reconciliation-workflow.md
8. planning/replacement-file-generation-guide.md
9. relevant domain/API/testing/slice/client docs for the requested area
```

## 4. Documentation-Only Agent Rule

Documentation-only work must not:

```text
- change code;
- create branches;
- create commits;
- open pull requests;
- push to GitHub;
- call GitHub mutation tools;
- implement backend/client/API behavior;
- silently rewrite unrelated docs.
```

Output is an archive with complete replacement/add files unless the user explicitly asks for direct repository writes.

## 5. Draft-Driven Discovery Link

All slice families use draft-driven discovery:

```text
domain drafts
business slice drafts
client sidecar drafts
cross-cutting/helper slice drafts
documentation/status reconciliation drafts
```

Primary source:

```text
planning/slices/draft-driven-discovery-principles.md
```

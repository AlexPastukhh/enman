# Apply Instructions

Archive:

```text
enman-documentation-update-workflow-and-draft-driven-discovery-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-documentation-update-workflow-and-draft-driven-discovery-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/documentation/README.md
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/documentation/documentation-update-agent-prompt.md
planning/slices/draft-driven-discovery-principles.md
```

## Replace

```text
planning/README.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/replacement-file-generation-guide.md
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/client-architecture-principles.md
```

## Delete

```text
nothing
```

## Notes

This package:
- adds documentation-only update workflow docs;
- adds a reusable documentation update agent prompt;
- adds status reconciliation workflow;
- strengthens archive rules: archive-only by default, no direct GitHub writes/mutations;
- adds draft-driven discovery principles;
- explicitly applies draft-driven discovery to client sidecars and all slice families;
- updates navigation and responsibility map.

It does not:
- change code;
- create commits/branches/PRs;
- create full numbered ADRs;
- create client sidecar files;
- reconcile every current implementation status.

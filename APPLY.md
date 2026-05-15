# Apply Instructions

Archive:

```text
enman-client-sidecar-architecture-mapping-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-client-sidecar-architecture-mapping-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/slices/client-architecture-principles.md
```

## Replace

```text
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/README.md
planning/planning-doc-responsibility-map.md
```

## Delete

```text
nothing
```

## Notes

This package adds client architecture mapping rules for `.client.md` sidecars.

It records:
- planning slice and frontend feature are not 1:1;
- read slice -> pages + entities (+ widgets if reused);
- command slice -> pages + features + entities;
- entity query hook rules;
- component placement rules;
- filtering as read query state unless persisted;
- review page as read context unless entering review creates server-side state;
- mandatory `Client Architecture Mapping` section in `.client.md`.

It does not:
- create a concrete `.client.md`;
- change code;
- change behavior items;
- change API/domain/tests;
- introduce widgets as mandatory structure.

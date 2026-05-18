# Archive Plan — sl-emp-req-002-details-server-client-draft-refactor-v1

## Scope

Refactor one paired server/client planning unit:

```text
SL-EMP-REQ-002 — Employee Request Details Read
L2-EMP-DETAILS-001.client — Employee Request Details
```

## Inputs

```text
current uploaded repository snapshot;
current server/client draft originals;
current implementation files and tests used read-only;
implemented slice sync workflow and current client/server slice templates.
```

## Allowed changes

```text
- replace the two planning draft files;
- add archive review folder with originals and checks;
- add root APPLY/MANIFEST files.
```

## Forbidden changes

```text
- runtime source code;
- tests;
- generated OpenAPI/types;
- UI runtime files;
- navigation/page-flow files;
- unrelated planning docs.
```

## Preflight decisions

```text
- current implementation exists, so status uses implementation evidence inspected read-only;
- test names are referenced as current evidence, not executed in this pass;
- hosted command actions are documented as composition, not read sidecar ownership;
- future source registry IDs remain pending.
```

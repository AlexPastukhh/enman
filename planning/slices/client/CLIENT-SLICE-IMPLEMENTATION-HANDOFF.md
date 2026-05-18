# Client Slice Implementation Handoff

Status: current handoff checklist

Every client implementation archive must include:

```text
MANIFEST.md
APPLY.md
changed source files
changed tests
```

If API shape changed and generated artifacts changed, include:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Do not manually edit generated artifacts.

## Required summary

```text
slice id/title
scope
files changed
checks run
checks not run / failed with reason
generated artifacts status
apply command
targeted tests
```

## Required checks when possible

```text
npm --prefix ./energymanagement.client install
npm --prefix ./energymanagement.client run build
targeted tests for changed files
zip integrity
```

If lint fails only on existing unrelated issues, state that explicitly.

## UI/CSS handoff requirements

When a client slice changes UI/CSS, include:

```text
Visual layout changes
CSS files changed
CSS ownership by layer
manual visual routes checked
known visual limitations
```

## Generated artifact reminder

Generation workflow usually follows:

```text
backend/API implementation
generate OpenAPI
generate API types
check API diff
build/tests
```

Generated artifacts must come from repo commands, not manual edits.

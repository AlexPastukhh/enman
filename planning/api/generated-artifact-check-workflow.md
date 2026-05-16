# Generated Artifact Check Workflow

Status: current / OpenAPI and generated TypeScript artifact workflow clarified  
Scope: local developer workflow after backend API source changes, generated artifact checks, docs/archive handoff notes

## 1. Purpose

This note fixes the recurring confusion around `npm run check:api`.

`check:api` is not only a generator. It is a stability check over generated artifacts.

Current root script shape:

```text
npm run check:openapi
npm run generate:api-types
git diff --exit-code Shared/openapi.json energymanagement.client/src/shared/api/generated/openapi-types.ts
```

That means `check:api` can fail when generated files are correct but not staged, because the final `git diff --exit-code` compares working tree files against the index.

## 2. Source Of Truth

Never hand-edit generated artifacts.

Source of truth:

```text
backend API source/metadata
        ↓
npm run generate:openapi
        ↓
Shared/openapi.json
        ↓
npm run generate:api-types
        ↓
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Generated artifacts included in an implementation archive must be files produced by repo generation commands, not manually reconstructed JSON/TypeScript.

## 3. Normal Local Workflow After API Source Changes

Run from repository root:

```powershell
npm run generate:openapi
npm run generate:api-types

git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts

npm run check:api
```

If `npm run check:api` passes after staging, generated artifacts are stable.

## 4. Why Staging Matters

The final step in `check:api` is:

```text
git diff --exit-code Shared/openapi.json energymanagement.client/src/shared/api/generated/openapi-types.ts
```

`git diff` without `--cached` compares working tree files to the index.

So this can happen:

```text
- generator output is correct;
- generated files changed as part of the slice;
- files are not staged;
- git diff still sees changes;
- check:api fails.
```

Staging generated artifacts before `check:api` lets the command answer the intended question:

```text
Did the check/generation command produce additional generated drift beyond the files we are about to commit/apply?
```

## 5. Recovery Workflow When check:api Fails On Generated Files

```powershell
git status --short .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts

git restore --staged .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts

npm run generate:openapi
npm run generate:api-types

git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts

npm run check:api
```

If the command still prints diff after this sequence, generated artifacts are still stale or the generator is not deterministic for the current source state.

## 6. Archive Rule

For an implementation archive that changes API shape, include:

```text
- source code changes;
- Shared/openapi.json produced by npm run generate:openapi;
- openapi-types.ts produced by npm run generate:api-types;
- MANIFEST.md;
- APPLY.md.
```

Do not include generated artifacts produced by manual editing or sandbox reconstruction.

For documentation-only archives, do not include generated artifacts.

## 7. OpenAPI Formatting Note

The OpenAPI generator/formatter may serialize characters such as `+` as escaped JSON text like `\u002B`.

Do not manually normalize the artifact to a preferred visual form.

Use the generated file exactly as produced by the current repo command.

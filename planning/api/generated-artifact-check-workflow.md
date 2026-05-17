# Generated Artifact Check Workflow

Status: current / explicit API-shape generation and `check:api` expectations clarified  
Scope: local workflow after backend API source changes, generated artifact checks, implementation archive handoff rules

## 1. Purpose

This note fixes the recurring confusion around generated OpenAPI/TypeScript artifacts and `check:api`.

When backend/API shape changes, the generated artifacts are expected to change.

That is not a problem by itself.

The problem is omitting generated artifacts from the implementation handoff or expecting `check:api` to pass while generated artifacts are still only unstaged working-tree changes.

## 2. Source Of Truth

Never hand-edit generated artifacts.

Source chain:

```text
backend API source / endpoint metadata / DTO metadata
        ↓
EnergyManagement.Tools generate-openapi
        ↓
Shared/openapi.json
        ↓
client generate:api-types
        ↓
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Generated artifacts included in an implementation archive must be produced by repo generation commands, not manually reconstructed JSON/TypeScript.

## 3. Correct Workflow After Backend/API Shape Changes

Run from repository root:

```powershell
cd C:\enman\enman

# 1. Regenerate OpenAPI, no --check
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json

# 2. Regenerate TypeScript API types
npm.cmd --prefix energymanagement.client run generate:api-types

# 3. Verify client build/tests
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

After this, these files may be modified:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

That is expected when API shape changed.

## 4. Why `check:api` Can Still Fail

`check:api` ends with a generated-artifact drift check equivalent to:

```text
git diff --exit-code Shared/openapi.json energymanagement.client/src/shared/api/generated/openapi-types.ts
```

That command says:

```text
After generation/check commands, there must be no generated-artifact diff left outside the expected repository state.
```

In a commit/PR workflow, the generated files must be included in the commit.

In an archive/handoff workflow, the generated files must be included in the archive/handoff.

If they are just modified in the working tree, `git diff --exit-code` is supposed to fail.

## 5. Practical Workflow For Server/API Slices

```powershell
# implement server endpoint / DTO / metadata changes first

dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix energymanagement.client run generate:api-types

git status --short
```

Expected when API shape changed:

```text
M Shared/openapi.json
M energymanagement.client/src/shared/api/generated/openapi-types.ts
```

These files must travel with the same implementation handoff as the server API change.

Implementation archive/commit should include:

```text
- server/API source files for the slice;
- related tests;
- Shared/openapi.json;
- energymanagement.client/src/shared/api/generated/openapi-types.ts;
- planning docs intentionally updated for the implementation.
```

## 6. Client-Only Slice Rule

A client-only slice should not change API shape by itself.

Usually it should not touch generated artifacts.

Exception:

```text
A backend endpoint was added earlier but generated artifacts were not refreshed.
```

In that case `check:api` may reveal that the repository is already out of sync. The fix is still to regenerate and include generated artifacts in the appropriate API-contract sync or implementation handoff, not to hand-edit generated files.

## 7. Build/Test Notes

For the client after API type generation:

```powershell
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

For server build/test gates, use the relevant project commands, for example:

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

A typo such as `otnet build` is not meaningful if the corrected `dotnet build` passes.

Line-ending warnings such as `LF will be replaced by CRLF` are not failures.

## 8. Archive Rule

For documentation-only archives:

```text
Do not include generated artifacts.
```

For implementation archives that change API shape:

```text
Include generated artifacts.
```

Required generated files:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Do not include generated artifacts produced by manual editing or sandbox reconstruction.

## 9. OpenAPI Formatting Note

The generator/formatter may serialize characters such as `+` as escaped JSON text like `\u002B`.

Do not manually normalize generated artifacts to preferred visual formatting.

Use generated files exactly as produced by the current repo command.

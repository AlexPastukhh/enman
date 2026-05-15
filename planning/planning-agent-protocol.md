# Planning Agent Protocol

Status: current collaboration protocol

## 1. Core Rule

Do not continue implementation planning through a question that may change required behavior, API contract, client-facing constants, or cross-layer responsibility.

## 2. Cross-Cutting / Helper Slice Rule

When a user identifies work as cross-cutting or helper-like, do not bury it only in workflow docs.

Create or update a cross-cutting/helper slice if the work has:

```text
- observable/support behavior;
- concrete implementation flow;
- independent tests;
- multiple consumers;
- contract/helper/tooling responsibility.
```

Use:

```text
planning/slices/cross-cutting/
```

## 3. Constants Capture Rule

When planning or implementing client-facing constants, identify:

```text
- source C# constant;
- generated JSON artifact path;
- whether client imports it;
- ordinary validation / important domain / critical behavioral classification;
- whether integration tests should read generated JSON;
- whether a literal integration contract test is needed;
- whether Shared/*.json must be regenerated;
- whether --check should be run;
- whether client parser/message/behavior tests must change.
```

Use:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

## 4. Implementation Flow Detail Filter

Slice flow may include involved classes, methods and short code snippets.

Do so only when they clarify behavior, boundary, trade-off, testability, no-write/no-side-effect guarantees, generated artifact shape or API/client contract.

Routine code mechanics should be described high-level.

If class/method details make the flow noisy, suggest a sibling `.impl.md` file.

Do not create `.impl.md` in advance.

## 5. Error Code Rule

```text
- Client-facing error codes are API contract.
- Client code must not hardcode error code strings.
- Ordinary codes are checked through generated JSON.
- Critical behavioral codes also get literal integration contract tests.
- `--check` is custom Tools command behavior, not built-in dotnet behavior.
```

## 6. Do Not

```text
- Do not use hosted service generation as primary constants generation path.
- Do not make --check modify files.
- Do not literal-test every validation code by default.
- Do not add regex convention tests or golden-file tests unless explicitly decided.
- Do not migrate FluentValidation ErrorMessage/ErrorCode usage without inspecting helpers/tests.
- Do not turn implementation flow into full code listing.
```

# Maybe For Optional Results

Status: target implementation convention / repository refactor pending  
Scope: repository/query APIs, application handler boundaries and slice implementation planning

## 1. Purpose

Use `Maybe<T>` for actions where absence of the resulting object is a normal possible outcome.

This convention is especially important for repository/query methods.

It prevents three common problems:

```text
- treating expected absence as an exception;
- leaking null checks through application code;
- forcing repositories to decide use-case meaning such as not found, invalid credentials or validation error.
```

## 2. Core Rule

```text
If an operation may legitimately return no object, return `Maybe<T>`.
```

Examples:

```text
GetByIdAsync(id) -> Maybe<Entity>
GetByEmailAsync(email) -> Maybe<Account>
GetCurrentActiveIndividualByClientAccountIdAsync(accountId) -> Maybe<IndividualApplicantParty>
FindLatest...Async(...) -> Maybe<T>
```

Absence is not automatically a failure.

The application/use-case layer decides what `Maybe.None` means in context.

## 3. Repository Rule

Repositories should model persistence lookup results, not use-case decisions.

Repository methods should return `Maybe<T>` when:

```text
- the database row may normally be absent;
- the caller needs the object if it exists;
- absence is not a repository infrastructure error;
- absence can have different meanings in different use cases.
```

Repositories should not convert absence into:

```text
- domain errors;
- validation errors;
- ProblemDetails;
- `InvalidCredentials`;
- `Unauthorized`;
- exceptions;
- fake/empty entities.
```

The repository returns `Maybe.None`; the handler chooses the semantic result.

## 4. Application Boundary Rule

Application handlers unwrap `Maybe<T>` and map absence to the correct use-case result.

Examples:

```text
Maybe<Account>.None in login
        -> InvalidCredentials

Maybe<Account>.None in current-user
        -> NotFound / Unauthorized boundary handling, depending on controller/use-case policy

Maybe<IndividualApplicantParty>.None in request creation
        -> ApplicantPartyIsRequired validation/domain error

Maybe<CurrentApplicant>.None in Account page read query
        -> successful empty state if the UI should show applicant creation form
```

Do not make repositories choose these outcomes.

## 5. Maybe vs Result vs UnitResult vs bool

| Shape | Use when |
|---|---|
| `Maybe<T>` | Operation may return an object or no object, and absence is a normal result. |
| `Result<T, Error>` | Operation can fail with a meaningful domain/application error and may also return data. |
| `UnitResult<Error>` | Operation can fail with a meaningful error but returns no data on success. |
| `bool` | Caller only asks an existence predicate and does not need the object. |
| nullable `T?` | Existing legacy/current code only; avoid for new repository APIs where absence is expected. |

Examples:

```text
ExistsByEmailAsync(email) -> bool
GetByEmailAsync(email) -> Maybe<Account>
CreateConnectionRequest(...) -> UnitResult<IReadOnlyList<Error>>
```

## 6. Current Repo Status

Current L1 repository APIs still use nullable returns in several places, for example:

```text
Task<Account?> GetByIdAsync(...)
Task<Account?> GetByEmailAsync(...)
Task<ApplicantParty?> GetByIdAsync(...)
Task<IndividualApplicantParty?> GetCurrentActiveIndividualByClientAccountIdAsync(...)
Task<ClientRequest?> GetByIdAsync(...)
```

Current handlers unwrap these nullable values locally.

This convention does not claim the refactor is already implemented.

It defines the target for:

```text
- new repository APIs;
- repository refactors;
- new read slices;
- future current-applicant read query;
- future request/list/detail repository reads.
```

## 7. Slice Documentation Rule

When a slice uses repository reads where absence is normal, the slice implementation flow should state:

```text
Repository returns Maybe<T> for optional lookup.
Application handler maps Maybe.None to <use-case result>.
```

If current implementation still uses nullable returns, write:

```text
Current implementation returns nullable `T?`.
Target convention: use `Maybe<T>` for this repository lookup when refactored.
```

Do not overclaim `Maybe<T>` implementation when repo evidence still shows nullable returns.

## 8. Client/API Boundary Note

`Maybe<T>` is an internal server/application implementation convention.

It does not automatically mean the public API returns an optional object.

API behavior must still be explicit:

```text
- 200 with body;
- 200/204 without body;
- 404 Not Found;
- 401 Unauthorized;
- 422 ProblemDetails;
- successful empty state response.
```

The endpoint contract decides what the client sees.

## 9. Testing Rule

When `Maybe.None` maps to a meaningful use-case outcome, tests should verify the mapping at the correct layer.

Examples:

```text
- login with missing email returns safe invalid credentials;
- current-user with missing account returns the chosen auth/not-found boundary result;
- create request without current active applicant returns ApplicantPartyIsRequired and does not write;
- current applicant read with no applicant returns the chosen empty-state contract.
```

Repository tests may verify lookup absence if repository behavior is directly tested.

Application/API tests should verify the use-case outcome.

## 10. Do Not

```text
- Do not use exceptions for normal missing rows.
- Do not return fake empty entities.
- Do not let repositories construct ProblemDetails or UI/API errors.
- Do not use `Result.Failure` only to say that a lookup returned no row when absence is normal and context-dependent.
- Do not keep adding new nullable repository APIs when the absence case is expected.
- Do not expose `Maybe<T>` directly through HTTP API contracts.
```

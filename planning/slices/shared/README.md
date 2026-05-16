# Shared Slice Notes

Status: current shared notes index  
Scope: reusable support notes that are not necessarily full slices

## 1. Purpose

This folder contains reusable notes/helpers that may support multiple slices.

A shared note is not automatically a cross-cutting/helper slice.

If a shared note gains observable behavior, implementation flow, tests and multiple consumers, create or update a cross-cutting/helper slice under:

```text
planning/slices/cross-cutting/
```

## 2. Current Shared Notes

```text
antiforgery-token-session-context.md
maybe-for-optional-results.md
```

## 3. Maybe For Optional Results

Use:

```text
planning/slices/shared/maybe-for-optional-results.md
```

for the target server/application convention that repository/query APIs should use `Maybe<T>` when absence of the resulting object is a normal possible outcome.

Current note:

```text
- current L1 repositories still use nullable returns in several places;
- the Maybe note is a target convention for new/refactored repository APIs;
- application handlers own mapping from Maybe.None to use-case meaning.
```

## 4. Antiforgery Note

The antiforgery shared note is now a source/support note.

Primary implementation-ready cross-cutting slice:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Behavior items:

```text
planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md
```

Security requirements source:

```text
planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
```

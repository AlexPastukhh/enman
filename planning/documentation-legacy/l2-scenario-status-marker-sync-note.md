# L2 Scenario Status Marker Sync

Status: docs-only sync note  
Scope: uniform future/current/deferred markers for L2 scenarios and diagram generation

## Summary

This sync introduces a consistent scenario-local marker section:

```text
## Diagram / Implementation Markers
```

It is used by L2 scenario specs to distinguish:

```text
[DESIGNED] accepted target semantics
[PLANNED] future implementation work in the current L2 plan
[DEFERRED] extension points outside current cut
[QUESTION] unresolved source/contract conflicts
[IMPLEMENTED] current repo implementation evidence only
```

## Why

The project is no longer just the first L1 stage. Scenarios now include L2 review/agreement flows that are partly implemented, partly implementation-ready and partly deferred.

Uniform markers make VKR/diploma diagrams clearer without claiming that all L2 elements are already implemented.

## Rule

Scenario markers are diagram-facing planning hints.

They do not replace:

```text
- current repo code/OpenAPI evidence;
- scenario text source of truth;
- slice docs;
- domain drafts;
- implementation verification.
```

Diagram Chat must still perform repo-grounded preflight before using `[IMPLEMENTED]`.

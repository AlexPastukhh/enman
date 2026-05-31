# Scenario Advanced Package — Superseded Summary

Status: superseded / compatibility note  
Doc version: v0.1.0  
Scope: previous generated advanced diagram package summary

## Important

This Markdown summary is no longer the semantic source of truth for scenario planning.

Use the corrected textual scenario specifications instead:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/scenario-specification-principles.md
planning/diagrams/scenario-diagram-consistency-report.md
```

## Why superseded

The previous advanced package summary described older scenario pages:

```text
SC-15 Security / Account Protection Scenario
SC-16 Reliable Notification Delivery Scenario
SC-18 Archive / Audit Scenario
```

The corrected scenario model now uses:

```text
SC-15 Security Text Specification — text-only, not a full scenario diagram by default
SC-16 Removed: notification navigation belongs to SC-05 / SC-13 / review-result flows
SC-17 Anonymous Request
SC-18 Archive / Audit — deferred / low priority
```

## Current advanced / policy set

```text
SC-15  Security Text Specification
SC-16  Removed standalone notification scenario
SC-17  Anonymous Request
SC-18  Archive / Audit — deferred
```

## Planning rule

Do not build responsibility tables from the old advanced package summary.

If visual `.drawio` pages still exist for the old package, treat them as stale visual references only until regenerated from corrected text specs.

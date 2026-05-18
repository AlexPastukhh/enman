# Merge Risk Report

## Main risk: workflow change

This archive adds a new mandatory stage: research bridge.

Risk:
- Topic generation may become too heavy if research bridge is used for every small topic.

Mitigation:
- Use research bridge only when a topic needs external support.
- Keep research short and integrated into the topic flow.

## Main risk: topic overwrite

The archive writes:

```text
planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/02-documents-and-feedback/01-request-decision-document-chain.topic.md
```

If a local version exists, review the diff before commit.

## Research source reports

The archive stores raw research reports under:

```text
planning/thesis/vkr-topic-workbench/00-research-materials/source-reports/
```

Risk:
- The reports contain internal citation markers from research collection.

Mitigation:
- Treat these reports as raw/support material.
- Run citation pass before using any reference in final VKR text.

## No cleanup

This archive does not delete files.

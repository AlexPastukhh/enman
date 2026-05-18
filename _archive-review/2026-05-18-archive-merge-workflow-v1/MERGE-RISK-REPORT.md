# Merge Risk Report

Archive: `archive-merge-workflow-with-originals-v1`  
Review folder: `_archive-review/2026-05-18-archive-merge-workflow-v1/`

## Summary

This archive intentionally avoids replacing the large main `planning/slices/README.md` because that file contains important guardrails and legacy navigation.

Navigation is updated through:

```text
planning/slices/SLICE-FOLDER-MAP.md
planning/slices/SLICE-INDEX.md
planning/slices/SLICE-QUESTIONS.md
```

All three replacements are additive merges with archived originals.

## File risks

### planning/slices/SLICE-FOLDER-MAP.md

Risk:
- common navigation file;
- losing old folder rules would make future docs placement ambiguous.

Post-apply checks:
- existing scenario source folders remain;
- existing slice planning folders remain;
- existing migration/deprecated compatibility rules remain;
- new archive workflow folder section exists.

### planning/slices/SLICE-INDEX.md

Risk:
- common navigation index;
- losing old slice rows would break legacy/current slice discovery.

Post-apply checks:
- workflow docs table remains;
- legacy/current slice index remains;
- cross-cutting scenario and slice index remains;
- archive workflow docs table exists.

### planning/slices/SLICE-QUESTIONS.md

Risk:
- decision register;
- losing old accepted questions would erase decisions.

Post-apply checks:
- all existing Q-CLIENT/Q-MIGRATION/Q-TAXONOMY/Q-NAMING/Q-TEST rows remain;
- Q-ARCHIVE rows exist.

## Second-step recommendation

After applying, run post-apply review.

If all three replacement files preserved existing content, no correction archive is needed.

If any information is missing, create a smaller correction archive that touches only the affected files and includes its own unique `_archive-review/<slug>/` folder.

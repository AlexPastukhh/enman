# Scenario Folder Replacement Notes

Apply this archive as merge/overwrite, not destructive delete, unless you intentionally want to remove existing visual artifacts.

Keep existing `.drawio`, `.png`, `.svg` files if they are not included here.

New root file:

```text
planning/scenario-specification-principles.md
```

Root docs replaced:

```text
planning/diagram-brief.md
planning/diagram-scenario-spec.md
planning/diagram-prompting-guide.md
planning/diagram-common-mistakes.md
```

New/updated folders:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
```

Manual cleanup if old files exist:

```text
remove: planning/diagrams/scenario-text-specs/SC-03B-set-new-password.md
use:    planning/diagrams/scenario-text-specs/SC-03B-account-owner-verified.md

remove: planning/diagrams/scenario-text-specs/SC-12-review-feedback-correction-navigation.md
use:    planning/diagrams/scenario-text-specs/SC-12-merged-review-feedback-correction-navigation.md

remove: planning/diagrams/scenario-text-specs/SC-13-my-agreements-agreement-response.md
use:    planning/diagrams/scenario-text-specs/SC-13-pending-agreement-proposal-model.md
```

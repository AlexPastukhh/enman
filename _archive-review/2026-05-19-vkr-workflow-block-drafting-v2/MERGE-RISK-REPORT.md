# Merge Risk Report — 2026-05-19-vkr-workflow-block-drafting-v2

## Purpose

This correction archive updates VKR workflow/navigation after the decision to stop using a fragment-bank-centered workflow and to use a block-based topic-to-section drafting workflow.

## Main risks

| Risk | Mitigation |
|---|---|
| Existing `fragment-bank.md` may contain useful text | The file is not deleted. It is replaced with a deprecated note and original content is preserved in `_archive-review/2026-05-19-vkr-workflow-block-drafting-v2/original-files/` |
| New chats may still use old linear workflow | `VKR-WORKFLOW-SOURCE-OF-TRUTH.md`, `NEW-CHAT-ONBOARDING.md`, and workbench README now point to the block-based workflow |
| Legacy folders may be used as active | Active/support/legacy rules are explicit in navigation |
| New folders may be added without README/index updates | Navigation maintenance protocol now says navigation update is mandatory when structure changes |
| Section drafts may be generated from nowhere | Section drafting now starts from blocks, questions and disclosure plans derived from topic drafts |
| Chat-specific rules may be lost | `chat-action-algorithms/` is introduced as the place for mandatory chat behavior rules |

## Delete policy

No project files are physically deleted by this archive. Deprecated materials are marked as deprecated/legacy and may be removed later only by a separate cleanup archive after harvest.

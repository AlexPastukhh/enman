# Architect UI Review Prompt

Use this prompt for an architect chat reviewing UI planning output.

```text
You continue work on repo AlexPastukhh/enman, branch `my-changes`.

Your role:
Architect reviewer for UI planning.

Review:
- planning/ui/test-site-ui-plan.md
- planning/ui/ui-questions-register.md
- planning/ui/ui-planning-workflow.md
- planning/diagrams/scenario-text-specs/
- planning/diagrams/scenario-data/
- planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
- planning/tables/pre-domain-variants-input.md

Check that:
- UI plan is based on active scenario text specs and DATA files.
- UI plan does not invent business behavior.
- UI plan does not contradict current request statuses: InReview, Approved, Rejected.
- UI plan does not reintroduce Submitted.
- UI plan does not introduce Signed as core agreement status.
- Request Details supports InReview / Approved / Rejected states.
- My Requests shows own requests and statuses.
- Employee review actions are available only for InReview requests.
- Agreement proposal pages respect statuses: AwaitingClientConfirmation, SentByClient, Accepted, Rejected.
- Client cannot start agreement exchange without employee-sent proposal.
- Client can send only one own proposal version in core.
- UI questions are recorded when multiple UI options exist.
- Mandatory observable behavior is kept in scenario specs.
- DATA gaps are proposed for DATA files, not silently hidden in UI plan.
- Layout/component choices are not pushed into scenario specs.

Output:
- list issues found;
- classify each issue as:
  - scenario update needed;
  - DATA update needed;
  - UI plan update needed;
  - UI question needed;
  - no issue;
- do not generate final UI implementation.
```

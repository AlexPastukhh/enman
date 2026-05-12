# Diagram Common Mistakes

Status: permanent catalog of repeated diagram mistakes and reject criteria.

## 1. Text Cards Instead Of Scenario Flow

Reject if the page is mostly disconnected text cards instead of actor/screen -> actions -> decisions -> branches -> outcomes.

## 2. Connector Web

Reject if connectors cross shape bodies/text or create a web. Keep secondary connectors short and local.

## 3. Invariant Attached To Late Consequence

Bad: `Remain unauthenticated -> protected by -> No session for invalid credentials`.

Good: `Credentials valid? -> protected by -> Session is issued only for valid credentials`.

## 4. Purple Used For Normal Actor Choice

Purple/off-page styling means the path leaves current scenario page. Do not use it for every choice.

## 5. [ALT] Used For Errors

Invalid credentials/validation errors are not `[ALT]` by default. `[ALT]` is narrow alternative path to same/equivalent goal.

## 6. [EXT] Used In Item IDs

Bad: `SC-02-EXT-01`. Good: `SC-02-EXTND-01`.

## 7. Generic extend Connector

Use concrete labels: `if selected`, `opens subscenario`, `requires`, `visible as`, `protected by`, `data`.

## 8. Preconditions Duplicate Branches

Do not put `Credentials are valid` as precondition when scenario has `Credentials valid?` branch.

## 9. Vague Input / Visible / Filter Step Without DATA

Bad: `Enter registration data`, `View details`, `Filter requests` with no DATA.

Good: attach DATA node/spec such as `Registration input DATA — SC-01-DATA-01` or `Own Request Details visible DATA — SC-05-DATA-03`.

## 10. DATA Block Contains Flow Rules

Reject if DATA contains branches, invariants, access rules or state-transition rules. Put those in scenario specs.

## 11. DATA Block Contains Validation Or Test Sections

Reject if DATA files contain `Validation / rules` or `Testable behavior`. Validation belongs to scenario specs; tests belong to testing map.

## 12. DATA Field Added Without Scenario Purpose

Reject arbitrary date/period/phone/address/priority/assignment fields added only because they are common.

## 13. Visible DATA Missing

Reject if scenario depends on actor-visible information but does not name what the actor sees.

## 14. Validation Error Is Terminal By Default

Correctable validation errors should loop back to input unless abandonment/final failure is explicit.

## 15. Opened From Modeled As Wrong Off-Page Link

Use entry points for how a scenario starts. Use off-page links only when current scenario leaves to another scenario/page.

## 16. Filter/Search/Select Collapsed

Separate filter/search, select/open item, and act on selected item.

## 17. Generated Packages In Wrong Folder

Use `planning/diagrams/` for generated packages, overview, text specs, DATA specs and reports. Use `planning/examples/` only for approved examples.

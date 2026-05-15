# Shared Support — Client FormValues To API DTO Mapping

Status: support note  
Marker: `[SHARED SUPPORT][CLIENT/UI][CROSS-SLICE]`

## Purpose

Document the rule that client form values and API DTOs may coincide in simple cases but should be separated when form state contains UI-only or normalized values.

## Rule

If a form submits fields one-to-one with no transformation, FormValues and DTO may temporarily share the same shape.

Use separate FormValues and API DTO when there is:

```text
trim / normalization
empty string -> null
confirmation fields
UI-only checkbox/state
select value string -> DTO number
flat form -> nested DTO
File / FormData upload
filters/search values
warning/confirmation state that should not be sent to server
```

## Examples

Reject request:

```text
FormValues:
feedbackText: string
emptyFeedbackConfirmed?: boolean

DTO:
requestId: number
rejectionFeedback: string | null

Mapping:
trim feedbackText
empty after trim -> null
do not send emptyFeedbackConfirmed
```

Request creation:

```text
FormValues may include applicantPartyId as string, address fields and UI-only state.
DTO may require applicantPartyId as number, nested objectAddress and details string.
```

## Sidecar Usage

Each `.client.md` should describe FormValues, DTO from parent API contract, mapping rules and mapping tests if non-trivial.

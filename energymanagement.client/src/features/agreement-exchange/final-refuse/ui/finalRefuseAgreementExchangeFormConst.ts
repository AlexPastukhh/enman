export const finalRefuseAgreementExchangeFormConst = {
  title: "Final refuse agreement exchange",
  description:
    "This final refusal closes the agreement exchange and marks the related request as agreement-exchange failed.",
  reasonLabel: "Final refusal reason",
  reasonPlaceholder: "Optional reason for final refusal",
  reasonHelpText: "Reason is optional. Whitespace-only reason is not valid.",
  actionLabel: "Final refuse",
  pendingLabel: "Refusing exchange...",
  confirmationText:
    "Final refusal cannot be undone in this flow. The related request will be marked as agreement-exchange failed.",
  confirmationLabel: "Confirm final refusal",
  cancelConfirmationLabel: "Cancel",
  blankReasonError: "Enter a reason or leave the field empty.",
  reasonTooLongError: "Reason must be 2000 characters or fewer.",
  defaultErrorMessage: "Could not finally refuse this agreement exchange.",
} as const;

export const finalRefusalReasonMaxLength = 2000;

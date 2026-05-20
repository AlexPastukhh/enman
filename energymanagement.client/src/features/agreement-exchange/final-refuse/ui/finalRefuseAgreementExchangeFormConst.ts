export const finalRefuseAgreementExchangeFormConst = {
  title: "Финально отказаться от согласования договора",
  description:
    "Финальный отказ закрывает согласование договора и переводит связанную заявку в статус незавершённого согласования договора.",
  reasonLabel: "Причина финального отказа",
  reasonPlaceholder: "Необязательная причина финального отказа",
  reasonHelpText: "Причина необязательна. Строка только из пробелов недопустима.",
  actionLabel: "Финально отказаться",
  pendingLabel: "Отказываемся от согласования...",
  confirmationText:
    "Финальный отказ нельзя отменить в этом сценарии. Связанная заявка будет отмечена как заявка с незавершённым согласованием договора.",
  confirmationLabel: "Подтвердить финальный отказ",
  cancelConfirmationLabel: "Отмена",
  blankReasonError: "Введите причину или оставьте поле пустым.",
  reasonTooLongError: "Причина должна содержать не более 2000 символов.",
  defaultErrorMessage: "Не удалось финально отказаться от согласования договора.",
} as const;

export const finalRefusalReasonMaxLength = 2000;

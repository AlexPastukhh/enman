export const finalRefuseAgreementExchangeFormConst = {
  title: "Финально отказаться от договорного обмена",
  description:
    "Финальный отказ закрывает договорной обмен и переводит связанную заявку в статус незавершённого договорного обмена.",
  reasonLabel: "Причина финального отказа",
  reasonPlaceholder: "Необязательная причина финального отказа",
  reasonHelpText: "Причина необязательна. Строка только из пробелов недопустима.",
  actionLabel: "Финально отказаться",
  pendingLabel: "Отказываемся от обмена...",
  confirmationText:
    "Финальный отказ нельзя отменить в этом сценарии. Связанная заявка будет отмечена как заявка с незавершённым договорным обменом.",
  confirmationLabel: "Подтвердить финальный отказ",
  cancelConfirmationLabel: "Отмена",
  blankReasonError: "Введите причину или оставьте поле пустым.",
  reasonTooLongError: "Причина должна содержать не более 2000 символов.",
  defaultErrorMessage: "Не удалось финально отказаться от договорного обмена.",
} as const;

export const finalRefusalReasonMaxLength = 2000;

export const createIndividualApplicantPartyConst = {
  formTitle: "Данные заявителя",
  readOnlyTitle: "Данные заявителя",
  successMessage: "Данные заявителя сохранены.",

  firstNameLabel: "Имя",
  firstNamePlaceholder: "Введите имя",
  firstNameInputId: "applicantFirstNameInput",
  firstNameErrorsId: "applicantFirstNameErrors",

  middleNameLabel: "Отчество",
  middleNamePlaceholder: "Введите отчество",
  middleNameInputId: "applicantMiddleNameInput",
  middleNameErrorsId: "applicantMiddleNameErrors",

  lastNameLabel: "Фамилия",
  lastNamePlaceholder: "Введите фамилию",
  lastNameInputId: "applicantLastNameInput",
  lastNameErrorsId: "applicantLastNameErrors",

  emailLabel: "Email заявителя",
  emailPlaceholder: "Введите email заявителя",
  emailInputId: "applicantEmailInput",
  emailErrorsId: "applicantEmailErrors",

  phoneNumberLabel: "Телефон",
  phoneNumberPlaceholder: "+79001234567",
  phoneNumberInputId: "applicantPhoneNumberInput",
  phoneNumberErrorsId: "applicantPhoneNumberErrors",

  verificationStatusLabel: "Статус проверки",
  verificationStatusLabels: {
    Unverified: "Данные не проверены",
    Verified: "Данные проверены",
  },

  submitButtonText: "Сохранить данные заявителя",
  submittingButtonText: "Сохраняем...",
} as const;

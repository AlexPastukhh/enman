export const createApplicantPartyConst = {
  formTitle: "Данные заявителя",
  successMessage: "Данные заявителя сохранены.",

  applicantPartyTypeLabel: "Тип заявителя",
  applicantPartyTypeInputId: "applicantPartyTypeInput",
  applicantPartyTypeErrorsId: "applicantPartyTypeErrors",
  applicantPartyTypes: {
    Individual: "Физическое лицо",
    IndividualEntrepreneur: "Индивидуальный предприниматель",
    LegalEntity: "Юридическое лицо",
  },

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

  organizationNameLabel: "Наименование организации",
  organizationNamePlaceholder: "Введите наименование",
  organizationNameInputId: "applicantOrganizationNameInput",
  organizationNameErrorsId: "applicantOrganizationNameErrors",

  innLabel: "ИНН",
  innPlaceholder: "1234567890",
  innInputId: "applicantInnInput",
  innErrorsId: "applicantInnErrors",

  kppLabel: "КПП",
  kppPlaceholder: "123456789",
  kppInputId: "applicantKppInput",
  kppErrorsId: "applicantKppErrors",

  ogrnLabel: "ОГРН",
  ogrnPlaceholder: "1234567890123",
  ogrnInputId: "applicantOgrnInput",
  ogrnErrorsId: "applicantOgrnErrors",

  ogrnipLabel: "ОГРНИП",
  ogrnipPlaceholder: "123456789012345",
  ogrnipInputId: "applicantOgrnipInput",
  ogrnipErrorsId: "applicantOgrnipErrors",

  emailLabel: "Email заявителя",
  emailPlaceholder: "Введите email заявителя",
  emailInputId: "applicantEmailInput",
  emailErrorsId: "applicantEmailErrors",

  phoneNumberLabel: "Телефон",
  phoneNumberPlaceholder: "+79001234567",
  phoneNumberInputId: "applicantPhoneNumberInput",
  phoneNumberErrorsId: "applicantPhoneNumberErrors",

  submitButtonText: "Сохранить данные заявителя",
  submittingButtonText: "Сохраняем...",
} as const;

import { errorCodes } from "../../../globConstants";

export class ValidTestData {
  static validEmail = "test@example.com" as const;
  static validPassword = "ValidPass123!" as const;

  static validLoginData = [
    {
      email: "test1@example.com",
      password: "ValidPass123!",
    },
    {
      email: "user+alias@example.co.uk",
      password: "Strong#Password1",
    },
  ];

  static validRegisterData = [
    {
      email: "test1@example.com",
      password: "ValidPass123!",
      passwordConfirmation: "ValidPass123!",
    },
    {
      email: "user+alias@example.co.uk",
      password: "Strong#Password1",
      passwordConfirmation: "Strong#Password1",
    },
    {
      email: "first.last@domain.io",
      password: "Aa1!Aa1!Aa1!A",
      passwordConfirmation: "Aa1!Aa1!Aa1!A",
    },
    {
      email: "sub.domain@service.example",
      password: "LongerPass$12345",
      passwordConfirmation: "LongerPass$12345",
    },
    {
      email: "edge.case+test@org.net",
      password: "EdgeCasePass12(",
      passwordConfirmation: "EdgeCasePass12(",
    },
  ];
}


export class InvalidTestData {
  static invalidRegisterUxData = [
    {
      // invalid email + invalid password + mismatched confirmation
      email: "not-an-email",
      password: "Short1!",
      passwordConfirmation: "Different1!",
      expectedFieldErrors: [
        errorCodes.Email.IsInvalid,
        errorCodes.Password.IsTooShort,
        errorCodes.PasswordConfirmation.DoesNotMatch,
      ],
    },
    {
      // invalid email + password without a special character
      email: "badformat",
      password: "NoSpecials12345",
      passwordConfirmation: "NoSpecials12345",
      expectedFieldErrors: [
        errorCodes.Email.IsInvalid,
        errorCodes.Password.LacksSpecialChars,
        null,
      ],
    },
  ];

  static invalidLoginUxData = [
    {
      // invalid email + invalid password
      email: "not-an-email",
      password: "Short1!",
      expectedFieldErrors: [
        errorCodes.Email.IsInvalid,
        errorCodes.Password.IsTooShort,
      ],
    },
  ];

  static mixedRegisterData = [
    {
      // valid email + invalid password + valid matching confirmation for the typed password
      email: "mixed-password@example.com",
      password: "Short1!",
      passwordConfirmation: "Short1!",
      expectedFieldErrors: [
        null,
        errorCodes.Password.IsTooShort,
        null,
      ],
    },
    {
      // invalid email + valid password and confirmation
      email: "not-an-email",
      password: "ValidPass123!",
      passwordConfirmation: "ValidPass123!",
      expectedFieldErrors: [
        errorCodes.Email.IsInvalid,
        null,
        null,
      ],
    },
    {
      // valid email/password + mismatched confirmation
      email: "mixed-confirmation@example.com",
      password: "ValidPass123!",
      passwordConfirmation: "DifferentPass123!",
      expectedFieldErrors: [
        null,
        null,
        errorCodes.PasswordConfirmation.DoesNotMatch,
      ],
    },
  ];

  static mixedLoginData = [
    {
      // valid email + invalid password
      email: "mixed-login@example.com",
      password: "Short1!",
      expectedFieldErrors: [
        null,
        errorCodes.Password.IsTooShort,
      ],
    },
    {
      // invalid email + valid password
      email: "not-an-email",
      password: "ValidPass123!",
      expectedFieldErrors: [
        errorCodes.Email.IsInvalid,
        null,
      ],
    },
  ];

  static invalidLoginData = [
    {
      // invalid email format
      email: "not-an-email",
      password: "ValidPass123!",
      expectedErrors: [errorCodes.Email.IsInvalid],
    },
    {
      // password too short (<12)
      email: "shortpass@example.com",
      password: "Short1!",
      expectedErrors: [errorCodes.Password.IsTooShort],
    },
    {
      // invalid email + password too short
      email: "not-an-email",
      password: "Short1!",
      expectedErrors: [
        errorCodes.Email.IsInvalid,
        errorCodes.Password.IsTooShort,
      ],
    },
  ];

  static invalidRegisterData = [
    {
      // invalid email format
      email: "not-an-email",
      password: "ValidPass123!",
      passwordConfirmation: "ValidPass123!",
      expectedErrors: [errorCodes.Email.IsInvalid] ,
    },
    {
      // password too short (<12)
      email: "shortpass@example.com",
      password: "Short1!",
      passwordConfirmation: "Short1!",
      expectedErrors: [errorCodes.Password.IsTooShort],
    },
    {
      // missing special character
      email: "nospecial@example.com",
      password: "NoSpecials12345",
      passwordConfirmation: "NoSpecials12345",
      expectedErrors: [errorCodes.Password.LacksSpecialChars],
    },
    {
      // password confirmation mismatch
      email: "mismatch@example.com",
      password: "ValidPass123!",
      passwordConfirmation: "DifferentPass123!",
      expectedErrors: [errorCodes.PasswordConfirmation.DoesNotMatch],
    },
    {
      // password too long (>50)
      email: "toolong@example.com",
      password: "ThisPasswordIsWayTooLongAndExceedsFiftyCharacters!!",
      passwordConfirmation:
        "ThisPasswordIsWayTooLongAndExceedsFiftyCharacters!!",
      expectedErrors: [errorCodes.Password.IsTooLong],
    },
    {
      // invalid email + password too short
      email: "not-an-email",
      password: "Short1!",
      passwordConfirmation: "Short1!",
      expectedErrors: [
        errorCodes.Email.IsInvalid,
        errorCodes.Password.IsTooShort,
      ] ,
    },
    {
      // invalid email + missing special character
      email: "badformat",
      password: "NoSpecials12345",
      passwordConfirmation: "NoSpecials12345",
      expectedErrors: [
        errorCodes.Email.IsInvalid,
        errorCodes.Password.LacksSpecialChars,
      ],
    },
    {
      // short password + confirmation mismatch
      email: "user@example.com",
      password: "Short1!",
      passwordConfirmation: "Different1!",
      expectedErrors: [
        errorCodes.Password.IsTooShort,
        errorCodes.PasswordConfirmation.DoesNotMatch,
      ],
    },
    {
      // missing special char + confirmation mismatch
      email: "user2@example.com",
      password: "NoSpecials12345",
      passwordConfirmation: "NoSpecials12345Different",
      expectedErrors: [
        errorCodes.Password.LacksSpecialChars,
        errorCodes.PasswordConfirmation.DoesNotMatch,
      ],
    },
    {
      // too long password + missing special char (still reports too long and may also report lacks special chars)
      email: "user3@example.com",
      password: "ThisPasswordIsWayTooLongAndExceedsFiftyCharacters!!",
      passwordConfirmation:
        "ThisPasswordIsWayTooLongAndExceedsFiftyCharacters!!",
      expectedErrors: [
        errorCodes.Password.IsTooLong,
      ],
    },
    {
      // invalid email + too long password + confirmation mismatch
      email: "bad-email",
      password: "ThisPasswordIsWayTooLongAndExceedsFiftyCharacters!!",
      passwordConfirmation: "DifferentLongPassword!!",
      expectedErrors: [
        errorCodes.Email.IsInvalid,
        errorCodes.Password.IsTooLong,
        errorCodes.PasswordConfirmation.DoesNotMatch,
      ],
    },
  ];
}

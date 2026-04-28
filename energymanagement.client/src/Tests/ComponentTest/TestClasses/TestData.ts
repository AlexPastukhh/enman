import { errorCodes } from "../../../globConstants";

export class ValidTestData {
  static validEmail = "test@example.com" as const;
  static validPassword = "ValidPass123!" as const;

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
  static invalidRegisterData = [
    {
      // invalid email format
      email: "not-an-email",
      password: "ValidPass123!",
      passwordConfirmation: "ValidPass123!",
      err: [errorCodes.Email.IsInvalid] ,
    },
    {
      // password too short (<12)
      email: "shortpass@example.com",
      password: "Short1!",
      passwordConfirmation: "Short1!",
      err: [errorCodes.Password.IsTooShort],
    },
    {
      // missing special character
      email: "nospecial@example.com",
      password: "NoSpecials12345",
      passwordConfirmation: "NoSpecials12345",
      err: [errorCodes.Password.LacksSpecialChars],
    },
    {
      // password confirmation mismatch
      email: "mismatch@example.com",
      password: "ValidPass123!",
      passwordConfirmation: "DifferentPass123!",
      err: [errorCodes.PasswordConfirmation.DoesNotMatch],
    },
    {
      // password too long (>50)
      email: "toolong@example.com",
      password: "ThisPasswordIsWayTooLongAndExceedsFiftyCharacters!!",
      passwordConfirmation:
        "ThisPasswordIsWayTooLongAndExceedsFiftyCharacters!!",
      err: [errorCodes.Password.IsTooLong],
    },
    {
      // invalid email + password too short
      email: "not-an-email",
      password: "Short1!",
      passwordConfirmation: "Short1!",
      err: [
        errorCodes.Email.IsInvalid,
        errorCodes.Password.IsTooShort,
      ] ,
    },
    {
      // invalid email + missing special character
      email: "badformat",
      password: "NoSpecials12345",
      passwordConfirmation: "NoSpecials12345",
      err: [
        errorCodes.Email.IsInvalid,
        errorCodes.Password.LacksSpecialChars,
      ],
    },
    {
      // short password + confirmation mismatch
      email: "user@example.com",
      password: "Short1!",
      passwordConfirmation: "Different1!",
      err: [
        errorCodes.Password.IsTooShort,
        errorCodes.PasswordConfirmation.DoesNotMatch,
      ],
    },
    {
      // missing special char + confirmation mismatch
      email: "user2@example.com",
      password: "NoSpecials12345",
      passwordConfirmation: "NoSpecials12345Different",
      err: [
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
      err: [
        errorCodes.Password.IsTooLong,
      ],
    },
    {
      // invalid email + too long password + confirmation mismatch
      email: "bad-email",
      password: "ThisPasswordIsWayTooLongAndExceedsFiftyCharacters!!",
      passwordConfirmation: "DifferentLongPassword!!",
      err: [
        errorCodes.Email.IsInvalid,
        errorCodes.Password.IsTooLong,
        errorCodes.PasswordConfirmation.DoesNotMatch,
      ],
    },
  ];
}

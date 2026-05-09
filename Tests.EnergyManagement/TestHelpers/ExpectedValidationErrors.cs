using EnergyManagement.Server.Api.Contracts.Auth;
using EnergyManagement.Server.Api.Contracts.Requests;
using EnergyManagement.Server.Data;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.TestHelpers
{
    public static class ExpectedValidationErrors
    {
        public static ServerValidationError EmailIsRequired =>
            Create(AuthFieldNames.Login.Email, Errors.Account.EmailIsRequired.Code);

        public static ServerValidationError EmailIsInvalid =>
            Create(AuthFieldNames.Login.Email, Errors.Account.EmailIsInvalid.Code);

        public static ServerValidationError EmailIsRegisteredAlready =>
            Create(AuthFieldNames.Login.Email, Errors.Account.EmailIsRegisteredAlready.Code);

        public static ServerValidationError EmailWasntRegistered =>
            Create(AuthFieldNames.Login.Email, Errors.Account.EmailWasntRegistered.Code);

        public static ServerValidationError PasswordIsRequired =>
            Create(AuthFieldNames.Login.Password, Errors.Account.PasswordIsRequired.Code);

        public static ServerValidationError PasswordIsTooLong =>
            Create(AuthFieldNames.Login.Password, Errors.Account.PasswordIsTooLong.Code);

        public static ServerValidationError PasswordLacksSpecialCharacters =>
            Create(AuthFieldNames.Login.Password, Errors.Account.PasswordLacksSpecialCharacters.Code);

        public static ServerValidationError PasswordIsWrong =>
            Create(AuthFieldNames.Login.Password, Errors.Account.PasswordIsWrong.Code);

        public static ServerValidationError PasswordConfirmationIsRequired =>
            Create(AuthFieldNames.Register.PasswordConfirmation, Errors.Account.PasswordConfirmationIsRequired.Code);

        public static ServerValidationError PasswordsDontMatch =>
            Create(AuthFieldNames.Register.PasswordConfirmation, Errors.Account.PasswordConfirmationDoesntMatch.Code);

        public static class CreateConnectionRequest
        {
            public static ServerValidationError RequestDetailsIsRequired =>
                Create(RequestFieldNames.CreateIndividualRequest.RequestDetails,
                    Errors.ClientRequestErrors.ClientRequestTextIsRequired.Code);

            public static ServerValidationError RequestDetailsIsTooLong =>
                Create(RequestFieldNames.CreateIndividualRequest.RequestDetails,
                    Errors.ClientRequestErrors.ClientRequestTextIsTooLong.Code);

            public static ServerValidationError PostalCodeIsRequired =>
                Create(RequestFieldNames.CreateIndividualRequest.PostalCode,
                    Errors.AddressErrors.PostalCodeIsRequired.Code);

            public static ServerValidationError PostalCodeIsInvalid =>
                Create(RequestFieldNames.CreateIndividualRequest.PostalCode,
                    Errors.AddressErrors.PostalCodeIsInvalid.Code);

            public static ServerValidationError RegionIsRequired =>
                Create(RequestFieldNames.CreateIndividualRequest.Region,
                    Errors.AddressErrors.RegionIsRequired.Code);

            public static ServerValidationError RegionIsTooLong =>
                Create(RequestFieldNames.CreateIndividualRequest.Region,
                    Errors.AddressErrors.RegionIsTooLong.Code);

            public static ServerValidationError CityIsRequired =>
                Create(RequestFieldNames.CreateIndividualRequest.City,
                    Errors.AddressErrors.CityIsRequired.Code);

            public static ServerValidationError CityIsTooLong =>
                Create(RequestFieldNames.CreateIndividualRequest.City,
                    Errors.AddressErrors.CityIsTooLong.Code);

            public static ServerValidationError StreetIsRequired =>
                Create(RequestFieldNames.CreateIndividualRequest.Street,
                    Errors.AddressErrors.StreetIsRequired.Code);

            public static ServerValidationError StreetIsTooLong =>
                Create(RequestFieldNames.CreateIndividualRequest.Street,
                    Errors.AddressErrors.StreetIsTooLong.Code);

            public static ServerValidationError HouseIsRequired =>
                Create(RequestFieldNames.CreateIndividualRequest.House,
                    Errors.AddressErrors.HouseIsRequired.Code);

            public static ServerValidationError HouseIsTooLong =>
                Create(RequestFieldNames.CreateIndividualRequest.House,
                    Errors.AddressErrors.HouseIsTooLong.Code);

            public static ServerValidationError BuildingIsTooLong =>
                Create(RequestFieldNames.CreateIndividualRequest.Building,
                    Errors.AddressErrors.BuildingIsTooLong.Code);

            public static ServerValidationError ApartmentIsTooLong =>
                Create(RequestFieldNames.CreateIndividualRequest.Apartment,
                    Errors.AddressErrors.ApartmentIsTooLong.Code);
        }

        private static ServerValidationError Create(string fieldName, string errorCode)
        {
            return ServerValidationError.Create(fieldName, errorCode);
        }
    }
}

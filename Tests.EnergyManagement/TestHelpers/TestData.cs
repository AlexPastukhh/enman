using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Api.Contracts.Auth;
using EnergyManagement.Server.Api.Contracts.Requests;
using EnergyManagement.Server.Api.Routes;
using EnergyManagement.Server.Data;
using static Domain.EnergyManagement.Common.Error;
using static Domain.EnergyManagement.Common.Error.Errors;

namespace Tests.EnergyManagement.TestHelpers
{
    public static class ValidTestData
    {
        // Personal Names
        public const string FirstName = "John";
        public const string MiddleName = "Michael";
        public const string LastName = "Doe";

        // Long Name for boundary testing
        public const string ValidLongName = "ThisIsAVeryLongNameThatExceedsFiftyCharacters1234567890";

        // Contact Information
        public const string ValidEmail = "adfdf@gmail.com";
        public const string TestPhoneNumber = "79237554726";

        // Security
        public const string ValidPassword = "adfsf@dfsd4324AF";
        public const string DifferentValidPassword = "DifferentPassword456@";

        //Requests
        public const string RequestDetails = "Request DetailsRequest DetailsRequest DetailsRequest DetailsRequest DetailsRequest DetailsRequest Details";
        public const string PostalCode = "123456";
        public const string Region = "Region";
        public const string City = "City";
        public const string Street = "Street";
        public const string House = "House";
        public const string Building = "Building";
        public const string Apartment = "Apartment";

        public static Address GetAddressWithApartment()
        {
            return Address.Create(PostalCode, Region, City, Street, House, null, Apartment).Value;
        }
        public static Address GetAddressWithApartmentAndBuilding()
        {
            return Address.Create(PostalCode, Region, City, Street, House, Building, Apartment).Value;
        }
        public static Address GetAddressWithOutApartmentAndBuilding()
        {
            return Address.Create(PostalCode, Region, City, Street, House, null, null).Value;
        }
        public static Address GetAddressWithBuilding()
        {
            return Address.Create(PostalCode, Region, City, Street, House, Building, null).Value;
        }
        public static AddressDto GetAddressDtoWithAllProps()
        {
            return new AddressDto
                    (
                        ValidTestData.PostalCode,
                        ValidTestData.Region,
                        ValidTestData.City,
                        ValidTestData.Street,
                        ValidTestData.House,
                        ValidTestData.Building,
                        ValidTestData.Apartment
                    );
        }
        public static (FullName, Email, PhoneNumber, Password) GetAllIndividualsValues()
        {
            var (email, password) = GetBaseDataForIndividual();
            var (fullName, phone) = GetDataForIndividualToProvide();
            return (fullName, email, phone, password);
        }

        public static (IndividualClient, string) GetBaseIndividualAndPasswordStr()
        {
            var (email, password, passwordStr) = GetBaseDataForIndividualAndPassword();
            return (IndividualClient.Create(email, password).Value, passwordStr);
        }
        public static IndividualClient GetIndividualWithoutFullData()
        {
            var (individual, _) = GetIndividualWithOutFullDataAndHisPassword();
            return individual;
        }
        public static IndividualClient GetIndividualWithAllData()
        {
            var (individual, _) = GetBaseIndividualAndPasswordStr();
            var (fullName, phone) = GetDataForIndividualToProvide();
            individual.ProvideDataOrThrow(phone, fullName);
            return individual;
        }
        public static (IndividualClient, string) GetIndividualWithAllDataAndPassword()
        {
            var (individual, password) = GetBaseIndividualAndPasswordStr();
            var (fullName, phone) = GetDataForIndividualToProvide();
            individual.ProvideDataOrThrow(phone, fullName);
            return (individual, password);
        }


        public static (IndividualClient, string) GetIndividualWithOutFullDataAndHisPassword()
        {
            var (email, password, passwordStr) = GetBaseDataForIndividualAndPassword();
            var individual = IndividualClient.Create(email, password).Value;
            return (individual, passwordStr);
        }

        public static (Email, Password, string) GetBaseDataForIndividualAndPassword()
        {
            var email = Email.Create(ValidEmail).Value;
            var passwordStr = ValidPassword;
            var password = Password.Create(passwordStr).Value;
            return (email, password, passwordStr);
        }
        public static (Email, Password) GetBaseDataForIndividual()
        {
            var (email, password, _) = GetBaseDataForIndividualAndPassword();
            return (email, password);
        }
        public static (FullName, PhoneNumber) GetDataForIndividualToProvide()
        {
            var fullName = FullName.Create(FirstName, MiddleName, LastName).Value;
            var phone = PhoneNumber.Create(TestPhoneNumber).Value;
            return (fullName, phone);
        }
    }

    public static class InvalidTestData
    {
        public const string LongPassword = "ThisIsAVeryLongPasswordThatExceedsOneHundredCharactersToTestTheMaximumLengthValidationRule1234567890!@#$%^&*()";
        public const string LongPasswordWithoutSpecialChars = "ThisIsAVeryLongPasswordThatExceedsOneHundredCharactersToTestTheMaximumLengthValidationRuleWithoutAnySpecialChars1234567890111111";
        public const string PasswordWithoutSpecialChars = "PasswordWithoutSpecialChars1234567890";
        // Invalid Phone Numbers
        public const string InvalidPhoneString = "InValidPhoneNumber";
        public const string EmptyPhone = "";
        public const string TooLongPhone = "792375547262";

        // Invalid Emails
        public const string InvalidEmailNoAt = "invalidEmail";
        public const string InvalidEmailNoDomain = "invalidEmail.com";
        public const string InvalidEmailNoAddress = "invalidEmail@";

        // Invalid Names (various edge cases)
        public const string Whitespace = " ";
        public const string EmptyString = "";
        public const string InvalidPostalCode = "ABC";
        public const int LongAddressDataLength = 301;

        // Invalid Request Data
        public const int InvalidRequestLength = 3001;

    }
    public static class ServerValidationErrors
    {
        public class Register
        {
            public static ServerValidationError EmailIsRequired =
                ServerValidationError.Create(
                            AuthFieldNames.Register.Email,
                            Account.EmailIsRequired.Code,
                            AuthRoutes.RegisterIndividualPath);
            public static ServerValidationError EmailIsInvalid =
                ServerValidationError.Create(
                            AuthFieldNames.Register.Email,
                            Account.EmailIsInvalid.Code,
                            AuthRoutes.RegisterIndividualPath);

            public static ServerValidationError EmailIsRegisteredAlready =
                ServerValidationError.Create(
                            AuthFieldNames.Register.Email,
                            Account.EmailIsRegisteredAlready.Code,
                            AuthRoutes.RegisterIndividualPath);

            public static ServerValidationError PasswordIsRequired =
            ServerValidationError.Create(
                        AuthFieldNames.Register.Password,
                        Account.PasswordIsRequired.Code,
                        AuthRoutes.RegisterIndividualPath);

            public static ServerValidationError PasswordIsTooLong =
                ServerValidationError.Create(
                            AuthFieldNames.Register.Password,
                            Account.PasswordIsTooLong.Code,
                            AuthRoutes.RegisterIndividualPath);

            public static ServerValidationError PasswordConfirmationIsRequired =
            ServerValidationError.Create(
                        AuthFieldNames.Register.PasswordConfirmation,
                        Account.PasswordConfirmationIsRequired.Code,
                        AuthRoutes.RegisterIndividualPath);

            public static ServerValidationError PasswordsDontMatch =
                ServerValidationError.Create(
                            AuthFieldNames.Register.PasswordConfirmation,
                            Account.PasswordConfirmationDoesntMatch.Code,
                            AuthRoutes.RegisterIndividualPath);

            public static ServerValidationError EmailWasntRegistered =
            ServerValidationError.Create(
                        AuthFieldNames.Register.Email,
                        Account.EmailWasntRegistered.Code,
                        AuthRoutes.RegisterIndividualPath);


            public static ServerValidationError PasswordLacksSpecialCharacters =
                ServerValidationError.Create(
                            AuthFieldNames.Register.Password,
                            Account.PasswordLacksSpecialCharacters.Code,
                            AuthRoutes.RegisterIndividualPath);

        }


        public static class Login
        {
            public static ServerValidationError EmailIsRequired =
                ServerValidationError.Create(
                            AuthFieldNames.Login.Email,
                            Account.EmailIsRequired.Code,
                            AuthRoutes.LoginPath);
            public static ServerValidationError EmailIsInvalid =
                ServerValidationError.Create(
                            AuthFieldNames.Login.Email,
                            Account.EmailIsInvalid.Code,
                            AuthRoutes.LoginPath);
            public static ServerValidationError EmailIsRegisteredAlready =
                ServerValidationError.Create(
                            AuthFieldNames.Login.Email,
                            Account.EmailIsRegisteredAlready.Code,
                            AuthRoutes.LoginPath);

            public static ServerValidationError PasswordIsRequired =
            ServerValidationError.Create(
                        AuthFieldNames.Login.Password,
                        Account.PasswordIsRequired.Code,
                        AuthRoutes.LoginPath);

            public static ServerValidationError PasswordIsTooLong =
                ServerValidationError.Create(
                            AuthFieldNames.Login.Password,
                            Account.PasswordIsTooLong.Code,
                            AuthRoutes.LoginPath);

            public static ServerValidationError PasswordIsWrong =
                ServerValidationError.Create(
                            AuthFieldNames.Login.Password,
                            Account.PasswordIsWrong.Code,
                            AuthRoutes.LoginPath);

            public static ServerValidationError EmailWasntRegistered =
            ServerValidationError.Create(
                        AuthFieldNames.Login.Email,
                        Account.EmailWasntRegistered.Code,
                        AuthRoutes.LoginPath);


            public static ServerValidationError PasswordLacksSpecialCharacters =
                ServerValidationError.Create(
                            AuthFieldNames.Login.Password,
                            Account.PasswordLacksSpecialCharacters.Code,
                            AuthRoutes.LoginPath);

        }

        public static class CreateConnectionRequest
        {
            public static ServerValidationError RequestDetailsIsRequired =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.RequestDetails,
                    Errors.ClientRequestErrors.ClientRequestTextIsRequired.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError RequestDetailsIsTooLong =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.RequestDetails,
                    Errors.ClientRequestErrors.ClientRequestTextIsTooLong.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);


            public static ServerValidationError PostalCodeIsRequired =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.PostalCode,
                    Errors.AddressErrors.PostalCodeIsRequired.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError PostalCodeIsInvalid =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.PostalCode,
                    Errors.AddressErrors.PostalCodeIsInvalid.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError RegionIsRequired =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.Region,
                    Errors.AddressErrors.RegionIsRequired.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError RegionIsTooLong =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.Region,
                    Errors.AddressErrors.RegionIsTooLong.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError CityIsRequired =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.City,
                    Errors.AddressErrors.CityIsRequired.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError CityIsTooLong =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.City,
                    Errors.AddressErrors.CityIsTooLong.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError StreetIsRequired =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.Street,
                    Errors.AddressErrors.StreetIsRequired.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError StreetIsTooLong =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.Street,
                    Errors.AddressErrors.StreetIsTooLong.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError HouseIsRequired =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.House,
                    Errors.AddressErrors.HouseIsRequired.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError HouseIsTooLong =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.House,
                    Errors.AddressErrors.HouseIsTooLong.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError BuildingIsTooLong =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.Building,
                    Errors.AddressErrors.BuildingIsTooLong.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);

            public static ServerValidationError ApartmentIsTooLong =
                ServerValidationError.Create(
                    RequestFieldNames.CreateIndividualRequest.Apartment,
                    Errors.AddressErrors.ApartmentIsTooLong.Code,
                    ClientRequestRoutes.IndivCreateConnectionRequestPath);
        }





    }
}

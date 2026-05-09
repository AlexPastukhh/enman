using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Data;

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

        private static (IndividualClient, string) GetBaseIndividualAndPasswordStr()
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


        private static (IndividualClient, string) GetIndividualWithOutFullDataAndHisPassword()
        {
            var (email, password, passwordStr) = GetBaseDataForIndividualAndPassword();
            var individual = IndividualClient.Create(email, password).Value;
            return (individual, passwordStr);
        }

        private static (Email, Password, string) GetBaseDataForIndividualAndPassword()
        {
            var email = Email.Create(ValidEmail).Value;
            var passwordStr = ValidPassword;
            var password = Password.Create(passwordStr).Value;
            return (email, password, passwordStr);
        }
        private static (Email, Password) GetBaseDataForIndividual()
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
}

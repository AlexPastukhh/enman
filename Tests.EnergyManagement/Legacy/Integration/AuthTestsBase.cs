using System.Collections.Generic;
using EnergyManagement.Server.Data;
using Tests.EnergyManagement.TestHelpers;
using Tests.EnergyManagement.Legacy.TestHelpers;
using Tests.EnergyManagement.Integration;
using Xunit.Abstractions;
using static Tests.EnergyManagement.Legacy.TestHelpers.ExpectedValidationErrors;
namespace Tests.EnergyManagement.Legacy.Integration
{
    
    public abstract class AuthTestsBase:LegacyIntegrationTest
    {

        public AuthTestsBase(WebAppFactory factory, ITestOutputHelper output) : base(factory, output)
        {
        }
        
        
        
         
        public static IEnumerable<object[]> GetInvalidRegisterData()
        {
            yield return new object[]
            {
                InvalidTestData.Whitespace,InvalidTestData.Whitespace,ValidTestData.ValidPassword,
                new List<ServerValidationError>
                {
                    ExpectedValidationErrors.EmailIsRequired,
                    ExpectedValidationErrors.PasswordIsRequired,
                    ExpectedValidationErrors.PasswordsDontMatch
                }
            };
            yield return new object[]
            {
                ValidTestData.ValidEmail,InvalidTestData.EmptyString,ValidTestData.ValidPassword,
                new List<ServerValidationError>
                {
                    ExpectedValidationErrors.PasswordIsRequired,
                    ExpectedValidationErrors.PasswordsDontMatch
                }
            };
            yield return new object[]
            {
                InvalidTestData.EmptyString,ValidTestData.ValidPassword,InvalidTestData.EmptyString,
                new List<ServerValidationError>
                {
                    ExpectedValidationErrors.EmailIsRequired,
                    ExpectedValidationErrors.PasswordConfirmationIsRequired

                }
            };
            yield return new object[]
            {
                ValidTestData.ValidEmail,ValidTestData.ValidPassword,ValidTestData.ValidPassword +"1",
                new List<ServerValidationError>
                {
                    ExpectedValidationErrors.PasswordsDontMatch
                }
            };
            yield return new object[]
            {
                ValidTestData.ValidEmail,InvalidTestData.PasswordWithoutSpecialChars,InvalidTestData.PasswordWithoutSpecialChars,
                new List<ServerValidationError>
                {
                    ExpectedValidationErrors.PasswordLacksSpecialCharacters
                }
            };
        }
        
        public static IEnumerable<object[]> GetInvalidLoginData()
        {
            yield return BadLoginValidationCases.NoEmailAndPassword;
            yield return BadLoginValidationCases.NoEmail;
            yield return BadLoginValidationCases.InvalidEmail;
            yield return BadLoginValidationCases.NoPasswordAndEmailUnregistered;
            yield return BadLoginValidationCases.InvalidLongPassword;
            yield return BadLoginValidationCases.PasswordWithoutSpecialCharacters;
            yield return BadLoginValidationCases.PasswordIsWrong;
            
        }
        
    }
    public static class BadLoginValidationCases
        {
            public static object[] NoEmailAndPassword =>
                new object[]
                {
                    InvalidTestData.Whitespace,
                    InvalidTestData.Whitespace,
                    false,
                    new List<ServerValidationError>
                    {
                        ExpectedValidationErrors.EmailIsRequired,
                        ExpectedValidationErrors.PasswordIsRequired
                    }
                };
            
            public static object[] NoEmail =>
                new object[]
                {
                    InvalidTestData.Whitespace,
                    ValidTestData.ValidPassword,
                    false,
                    new List<ServerValidationError>
                    {
                        ExpectedValidationErrors.EmailIsRequired
                    }
                };
            public static object[] InvalidEmail =>
                new object[]
                {
                    InvalidTestData.InvalidEmailNoAddress,
                    ValidTestData.ValidPassword,
                    false,
                    new List<ServerValidationError>
                    {
                        ExpectedValidationErrors.EmailIsInvalid
                    }
                };
            public static object[] EmailWasntRegistered =>
            new object[]
            {
                ValidTestData.ValidEmail,
                ValidTestData.ValidPassword,
                false,
                new List<ServerValidationError>
                {
                    ExpectedValidationErrors.EmailWasntRegistered
                }
            };
            public static object[] NoPasswordAndEmailUnregistered =>
                new object[]
                {
                    ValidTestData.ValidEmail,
                    InvalidTestData.Whitespace,
                    false,
                    new List<ServerValidationError>
                    {
                        ExpectedValidationErrors.EmailWasntRegistered,
                        ExpectedValidationErrors.PasswordIsRequired
                    }
                };
            
            public static object[] InvalidLongPassword =>
                new object[]
                {
                    ValidTestData.ValidEmail,
                    InvalidTestData.LongPassword,
                    true,
                    new List<ServerValidationError>
                    {
                        ExpectedValidationErrors.PasswordIsTooLong
                    }
                };

            public static object[] PasswordWithoutSpecialCharacters =>
                new object[]
                {
                    ValidTestData.ValidEmail,
                    InvalidTestData.PasswordWithoutSpecialChars,
                    true,
                    new List<ServerValidationError>
                    {
                        ExpectedValidationErrors.PasswordLacksSpecialCharacters
                    }
                };
            
            public static object[] PasswordIsWrong =>
                new object[]
                {
                    ValidTestData.ValidEmail,
                    ValidTestData.DifferentValidPassword,
                    true,
                    new List<ServerValidationError>
                    {
                        ExpectedValidationErrors.PasswordIsWrong
                    }
                };
            
            
        }
        
    public static class BadRegisterValidationCases
        {
            public static object[] NoEmailAndPasswordAndConfirmation =>
                new object[]
                {
                    InvalidTestData.Whitespace,InvalidTestData.Whitespace,InvalidTestData.Whitespace,
                    new List<ServerValidationError>
                    {
                        EmailIsRequired,
                        PasswordIsRequired,
                        PasswordConfirmationIsRequired
                    }
                };
            
            public static object[] NoEmail =>
                new object[]
                {
                    InvalidTestData.Whitespace,ValidTestData.ValidPassword,ValidTestData.ValidPassword,
                    new List<ServerValidationError>
                    {
                        EmailIsRequired
                    }
                };
            public static object[] InvalidEmail =>
                new object[]
                {
                    InvalidTestData.InvalidEmailNoAddress,ValidTestData.ValidPassword,ValidTestData.ValidPassword,
                    new List<ServerValidationError>
                    {
                        EmailIsInvalid
                    }
                };
            public static object[] EmptyPasswordAndConfirmation =>
                new object[]
                {
                    ValidTestData.ValidEmail,InvalidTestData.Whitespace,InvalidTestData.Whitespace,
                    new List<ServerValidationError>
                    {
                        EmailIsRequired,
                        PasswordIsRequired,
                        PasswordConfirmationIsRequired
                    }
                };
                
            public static object[] EmptyPasswordPlusDontMatch =>
                new object[]
                {
                    ValidTestData.ValidEmail,InvalidTestData.Whitespace,ValidTestData.ValidPassword,
                    new List<ServerValidationError>
                    {
                        EmailIsRequired,
                        PasswordIsRequired,
                        PasswordsDontMatch
                    }
                };
            
            public static object[] InvalidLongPassword =>
                new object[]
                {
                    ValidTestData.ValidEmail,InvalidTestData.LongPassword,InvalidTestData.LongPassword,
                    new List<ServerValidationError>
                    {
                        PasswordIsTooLong
                    }
                };
            
            
        }
}



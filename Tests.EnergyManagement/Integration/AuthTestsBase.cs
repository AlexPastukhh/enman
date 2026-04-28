using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server;
using EnergyManagement.Server.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;
using static Domain.EnergyManagement.Common.Error.Errors;
using EnergyManagement.Server.Data;
using static Tests.EnergyManagement.TestHelpers.ServerValidationErrors;
namespace Tests.EnergyManagement.Integration
{
    
    public abstract class AuthTestsBase:IntegrationTest
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
                    ServerValidationErrors.Register.EmailIsRequired,
                    ServerValidationErrors.Register.PasswordIsRequired,
                    ServerValidationErrors.Register.PasswordsDontMatch
                }
            };
            yield return new object[]
            {
                ValidTestData.ValidEmail,InvalidTestData.EmptyString,ValidTestData.ValidPassword,
                new List<ServerValidationError>
                {
                    ServerValidationErrors.Register.PasswordIsRequired,
                    ServerValidationErrors.Register.PasswordsDontMatch
                }
            };
            yield return new object[]
            {
                InvalidTestData.EmptyString,ValidTestData.ValidPassword,InvalidTestData.EmptyString,
                new List<ServerValidationError>
                {
                    ServerValidationErrors.Register.EmailIsRequired,
                    ServerValidationErrors.Register.PasswordConfirmationIsRequired

                }
            };
            yield return new object[]
            {
                ValidTestData.ValidEmail,ValidTestData.ValidPassword,ValidTestData.ValidPassword +"1",
                new List<ServerValidationError>
                {
                    ServerValidationErrors.Register.PasswordsDontMatch
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
                        ServerValidationErrors.Login.EmailIsRequired,
                        ServerValidationErrors.Login.PasswordIsRequired
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
                        ServerValidationErrors.Login.EmailIsRequired
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
                        ServerValidationErrors.Login.EmailIsInvalid
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
                    ServerValidationErrors.Login.EmailWasntRegistered
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
                        ServerValidationErrors.Login.EmailWasntRegistered,
                        ServerValidationErrors.Login.PasswordIsRequired
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
                        ServerValidationErrors.Login.PasswordIsTooLong
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
                        ServerValidationErrors.Login.PasswordIsWrong
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
                        Register.EmailIsRequired,
                        Register.PasswordIsRequired,
                        Register.PasswordConfirmationIsRequired
                    }
                };
            
            public static object[] NoEmail =>
                new object[]
                {
                    InvalidTestData.Whitespace,ValidTestData.ValidPassword,ValidTestData.ValidPassword,
                    new List<ServerValidationError>
                    {
                        Register.EmailIsRequired
                    }
                };
            public static object[] InvalidEmail =>
                new object[]
                {
                    InvalidTestData.InvalidEmailNoAddress,ValidTestData.ValidPassword,ValidTestData.ValidPassword,
                    new List<ServerValidationError>
                    {
                        Register.EmailIsInvalid
                    }
                };
            public static object[] EmptyPasswordAndConfirmation =>
                new object[]
                {
                    ValidTestData.ValidEmail,InvalidTestData.Whitespace,InvalidTestData.Whitespace,
                    new List<ServerValidationError>
                    {
                        Register.EmailIsRequired,
                        Register.PasswordIsRequired,
                        Register.PasswordConfirmationIsRequired
                    }
                };
                
            public static object[] EmptyPasswordPlusDontMatch =>
                new object[]
                {
                    ValidTestData.ValidEmail,InvalidTestData.Whitespace,ValidTestData.ValidPassword,
                    new List<ServerValidationError>
                    {
                        Register.EmailIsRequired,
                        Register.PasswordIsRequired,
                        Register.PasswordsDontMatch
                    }
                };
            
            public static object[] InvalidLongPassword =>
                new object[]
                {
                    ValidTestData.ValidEmail,InvalidTestData.LongPassword,InvalidTestData.LongPassword,
                    new List<ServerValidationError>
                    {
                        Register.PasswordIsTooLong
                    }
                };
            
            
        }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyManagement.Server;
using EnergyManagement.Server.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration
{
    public abstract class ClientRequestsTestsBase : IntegrationTest
    {


        public ClientRequestsTestsBase(WebAppFactory factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        public static IEnumerable<object[]> GetInvalidConnectionRequestData()
        {
            return InvalidConnectionRequestCases.GetInvalidCases();
        }

        private class InvalidConnectionRequestCases
        {
            public static object[] MissingRequestDetails
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.RequestDetails = InvalidTestData.Whitespace;
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ServerValidationErrors.CreateConnectionRequest.RequestDetailsIsRequired
                        }
                    };
                }
            }

            

            public static object[] MissingPostalCode
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.Address!.PostalCode = InvalidTestData.Whitespace;
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ServerValidationErrors.CreateConnectionRequest.PostalCodeIsRequired
                        }
                    };
                }
            }

            public static object[] InvalidPostalCodeFormat
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.Address!.PostalCode = InvalidTestData.InvalidPostalCode;
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ServerValidationErrors.CreateConnectionRequest.PostalCodeIsInvalid
                        }
                    };
                }
            }

            public static object[] RequestDetailsTooLong
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.RequestDetails = new string('x', 2000);
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ServerValidationErrors.CreateConnectionRequest.RequestDetailsIsTooLong
                        }
                    };
                }
            }

            public static object[] MissingRequestDetailsAndCity
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.RequestDetails = InvalidTestData.Whitespace;
                    dto.Address!.City = InvalidTestData.Whitespace;
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ServerValidationErrors.CreateConnectionRequest.RequestDetailsIsRequired,
                            ServerValidationErrors.CreateConnectionRequest.CityIsRequired
                        }
                    };
                }
            }

            public static object[] InvalidPostalCodeAndRegionTooLong
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.Address!.PostalCode = InvalidTestData.InvalidPostalCode;
                    dto.Address!.Region = new string('x', InvalidTestData.LongAddressDataLength);
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ServerValidationErrors.CreateConnectionRequest.PostalCodeIsInvalid,
                            ServerValidationErrors.CreateConnectionRequest.RegionIsTooLong
                        }
                    };
                }
            }

            public static object[] BuildingAndApartmentTooLong
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.Address!.Building = new string('x', InvalidTestData.LongAddressDataLength);
                    dto.Address!.Apartment = new string('x', InvalidTestData.LongAddressDataLength);
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ServerValidationErrors.CreateConnectionRequest.BuildingIsTooLong,
                            ServerValidationErrors.CreateConnectionRequest.ApartmentIsTooLong
                        }
                    };
                }
            }

            public static IEnumerable<object[]> GetInvalidCases() => new[]
            {
                MissingRequestDetails,
                MissingPostalCode,
                InvalidPostalCodeFormat,
                RequestDetailsTooLong,
                MissingRequestDetailsAndCity,
                InvalidPostalCodeAndRegionTooLong,
                BuildingAndApartmentTooLong,
            };

            public static CreateIndividualRequestDto ValidConnectionRequestDto()
            {
                return new CreateIndividualRequestDto
                (
                    "valid request",
                    ValidTestData.GetAddressDtoWithAllProps()

                );
            }

        }

    }
}
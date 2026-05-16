using System.Collections.Generic;
using EnergyManagement.Server.Data;
using Tests.EnergyManagement.TestHelpers;
using Tests.EnergyManagement.Legacy.TestHelpers;
using Tests.EnergyManagement.Integration;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Legacy.Integration
{
    public abstract class ClientRequestsTestsBase : LegacyIntegrationTest
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
                            ExpectedValidationErrors.CreateConnectionRequest.RequestDetailsIsRequired
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
                            ExpectedValidationErrors.CreateConnectionRequest.PostalCodeIsRequired,
                            ExpectedValidationErrors.CreateConnectionRequest.PostalCodeIsInvalid
                        }
                    };
                }
            }

            public static object[] MissingRegion
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.Address!.Region = InvalidTestData.Whitespace;
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ExpectedValidationErrors.CreateConnectionRequest.RegionIsRequired
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
                            ExpectedValidationErrors.CreateConnectionRequest.PostalCodeIsInvalid
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
                            ExpectedValidationErrors.CreateConnectionRequest.RequestDetailsIsTooLong
                        }
                    };
                }
            }

            public static object[] CityTooLong
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.Address!.City = new string('x', InvalidTestData.LongAddressDataLength);
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ExpectedValidationErrors.CreateConnectionRequest.CityIsTooLong
                        }
                    };
                }
            }

            public static object[] MissingStreet
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.Address!.Street = InvalidTestData.Whitespace;
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ExpectedValidationErrors.CreateConnectionRequest.StreetIsRequired
                        }
                    };
                }
            }

            public static object[] StreetTooLong
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.Address!.Street = new string('x', InvalidTestData.LongAddressDataLength);
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ExpectedValidationErrors.CreateConnectionRequest.StreetIsTooLong
                        }
                    };
                }
            }

            public static object[] MissingHouse
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.Address!.House = InvalidTestData.Whitespace;
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ExpectedValidationErrors.CreateConnectionRequest.HouseIsRequired
                        }
                    };
                }
            }

            public static object[] HouseTooLong
            {
                get
                {
                    var dto = ValidConnectionRequestDto();
                    dto.Address!.House = new string('x', InvalidTestData.LongAddressDataLength);
                    return new object[]
                    {
                        dto,
                        new List<ServerValidationError>
                        {
                            ExpectedValidationErrors.CreateConnectionRequest.HouseIsTooLong
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
                            ExpectedValidationErrors.CreateConnectionRequest.RequestDetailsIsRequired,
                            ExpectedValidationErrors.CreateConnectionRequest.CityIsRequired
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
                            ExpectedValidationErrors.CreateConnectionRequest.PostalCodeIsInvalid,
                            ExpectedValidationErrors.CreateConnectionRequest.RegionIsTooLong
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
                            ExpectedValidationErrors.CreateConnectionRequest.BuildingIsTooLong,
                            ExpectedValidationErrors.CreateConnectionRequest.ApartmentIsTooLong
                        }
                    };
                }
            }

            public static IEnumerable<object[]> GetInvalidCases() => new[]
            {
                MissingRequestDetails,
                MissingPostalCode,
                MissingRegion,
                InvalidPostalCodeFormat,
                RequestDetailsTooLong,
                CityTooLong,
                MissingStreet,
                StreetTooLong,
                MissingHouse,
                HouseTooLong,
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



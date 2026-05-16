using System.Collections.Generic;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using FluentAssertions;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Shared
{
    public class PassportTests
    {
        public const string ValidSeries = "3452";
        public const string ValidNumber = "345254";
        public const string ValidCode = "345-263";
        public const string ValidIssuedBy ="UFMS of St.Petersburg";
        public static string GetDateOnlyStringOfNowPlus(int addYears=0,int addMonths =0,int addDays=0)
        {
            return DateOnly.FromDateTime(
                DateTimeOffset.UtcNow
                    .AddYears(addYears)
                    .AddMonths(addMonths)
                    .AddDays(addDays)
                    .DateTime).ToString();
        }
        public static IEnumerable<object[]>GetValidPassportTestData()
        {
            yield return new object[]
            {
                ValidSeries,
                ValidNumber,
                ValidIssuedBy,
                ValidCode,
                GetDateOnlyStringOfNowPlus(addMonths:-1)
            };
            
            yield return new object[]
            {
                "5356",
                "234 452",
                "UFMS of Moscow",
                "345666",
                GetDateOnlyStringOfNowPlus(addMonths:-12)
            };
            
            yield return new object[]
            {
                "5 356",
                " 234 452",
                "UFMS of Moscow",
                "34 5666 ",
                GetDateOnlyStringOfNowPlus(addMonths:-6)
            };
        }
        
        public static IEnumerable<object[]>GetInvalidPassportTestData()
        {
            yield return new object[]
            {
                "ValidSeries",
                "ValidNumber",
                "ValidIssuedBy",
                "ValidCode",
                "ValidIssuedAtTime",
                new List<Error>{
                    Errors.Passport.SeriesIsInvalid,
                    Errors.Passport.NumberIsInvalid,
                    Errors.Passport.CodeIsInvalid,
                    Errors.Passport.IssuedAtTimeIsInvalid
                }
                
            };
            
            yield return new object[]
            {
                ValidSeries,
                "252",
                ValidIssuedBy,
                ValidCode,
                GetDateOnlyStringOfNowPlus(addMonths:-12),
                new List<Error>{Errors.Passport.NumberIsInvalid}
            };
            
            yield return new object[]
            {
                ValidSeries,
                ValidNumber,
                " ",
                ValidCode,
                GetDateOnlyStringOfNowPlus(addMonths:-12),
                new List<Error>{Errors.Passport.IssuedByIsRequired}
            };
            
            yield return new object[]
            {
                ValidSeries,
                ValidNumber,
                ValidIssuedBy,
                ValidCode,
                GetDateOnlyStringOfNowPlus(addMonths:6),
                new List<Error>{Errors.Passport.IssuedAtTimeIsInTheFuture}
            };
        }
        
        [Theory]
        [MemberData(nameof(GetValidPassportTestData))]
        public void CreatePassportDataSuccessfully(string validSeries,string validNumber,string validIssuedBy,string validCode,string validIssuedAtTime)
        {
            //Act
            var createPassportData = PassportData.Create(validSeries,validNumber,validIssuedBy,validCode,validIssuedAtTime);
            //Assert
            createPassportData.IsSuccess.Should().BeTrue();
            createPassportData.Value.Should().BeOfType<PassportData>();                   
        }
        
        [Theory]
        [MemberData(nameof(GetInvalidPassportTestData))]
        public void CantCreatePassportData(
            string validSeries,
            string validNumber,
            string validIssuedBy,
            string validCode,
            string validIssuedAtTime,
            List<Error> errors)
        {
            //Act
            var createPassportData = PassportData.Create(validSeries,validNumber,validIssuedBy,validCode,validIssuedAtTime);
            //Assert
            createPassportData.IsSuccess.Should().BeFalse();
            createPassportData.Error.All(e=>errors.Contains(e)).Should().BeTrue();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.DocumentManaging;
using Xunit.Abstractions;
using Xunit.Sdk;
using EnergyManagement.Server.Data;

namespace Tests.EnergyManagement.TestHelpers
{
    public abstract class TestIndividualBase{
        protected Maybe<IndividualClient> RegisteredIndividual { get;  set; }
        public string EmailOrThrow => RegisteredIndividual.Value.Email.Value;
        public long Id => RegisteredIndividual.Value.Id;
        protected Maybe<ITestOutputHelper> _output;
        public  Maybe<string> OriginalPasswordString { get; private set; }
        public void SetRegisteredIndividual(IndividualClient individual,string originalPassword,ITestOutputHelper output)
        {
            if(RegisteredIndividual.HasValue) throw new XunitException("RegisteredIndividual is already set");
            if(OriginalPasswordString.HasValue) throw new XunitException("OriginalPasswordString is already set");
            
            RegisteredIndividual = individual;
            OriginalPasswordString = originalPassword;
            _output = Maybe.From(output);
        }
        
        public void UpdateRegisteredIndividual(IndividualClient individual,string newPasswordString = "")
        {
            if(RegisteredIndividual.HasNoValue) throw new XunitException("RegisteredIndividual wasnt  set before");
            if(OriginalPasswordString.HasNoValue) throw new XunitException("OriginalPasswordString wasnt  set before");
            
            RegisteredIndividual = individual;
            if(!string.IsNullOrWhiteSpace(newPasswordString))
            {
                OriginalPasswordString = newPasswordString;
            }
        }
        public  IReadOnlyList<Claim>GetClaims()
        {
            if(RegisteredIndividual.HasNoValue) throw new XunitException("RegisteredIndividual is not set");
            var claims = IntegrationTestHelper.GetClaimsForIndividual(RegisteredIndividual.Value);
            return claims;
        }

    }
    public class AuthTestsIndividual: TestIndividualBase
    {
        
        public LoginDto LoginDto => new LoginDto(
            RegisteredIndividual.Value.Email.Value,OriginalPasswordString.Value);
        
        
        
        public bool HasFullName(FullName fullName)
        {
            if(fullName==null) throw new XunitException(nameof(fullName) + " is null");
            
            if (RegisteredIndividual.Value.FullName.HasValue==false)
            {
                throw new XunitException("Registered individual has no full name");
            }
            return RegisteredIndividual.Value.FullName.Value.Equals(fullName);
        }
        
        public bool HasPhoneNumber(PhoneNumber phoneNumber)
        {
            if(phoneNumber is null) throw new XunitException(nameof(phoneNumber) + " is null");
            
            if (RegisteredIndividual.Value.PhoneNumber.HasValue==false)
            {
                throw new XunitException("Registered individual has no phone number");
            }
            return RegisteredIndividual.Value.PhoneNumber.Value.Equals(phoneNumber);
        }
        
        
        
    }
    public class RequestsTestIndividual: TestIndividualBase
    {
        public Maybe<IReadOnlyList<IndividualRequest>> Requests =>
            RegisteredIndividual.HasValue
                ? Maybe.From(RegisteredIndividual.Value.ClientRequests)
                : Maybe.None;
        public int GetCountOfConnectionRequestsLikeDto(CreateIndividualRequestDto dto)
        {
            if(dto is null) throw new XunitException(nameof(dto) + " is null");
            
            var count =Requests.Value.Count(r=>r.Type == RequestType.Connection && AssertRequestFromDto(r,dto).IsSuccess);
            return count;
        }
      
        public UnitResult<string> AssertRequestFromDto(IndividualRequest req1,CreateIndividualRequestDto dto)
        {
            if(req1 is null) throw new XunitException(nameof(req1) + " is null");
            if(dto is null) throw new XunitException(nameof(dto) + " is null");

            var diffs = new List<string>();

            if (!Equals(req1.RequestDetails, dto.RequestDetails))
                diffs.Add($"RequestDetails: expected '{dto.RequestDetails}' actual '{req1.RequestDetails}'");

            if (!Equals(req1.RequestDetails, dto.RequestDetails))
                diffs.Add($"RequestDetails: expected '{dto.RequestDetails}' actual '{req1.RequestDetails}'");

            // Manual, explicit comparison of Address DTO -> Address instance (no reflection)
            var reqAddr = req1.Address;
            var dtoAddr = dto.Address;

            if (dtoAddr == null && reqAddr != null)
            {
                diffs.Add("Address: expected 'null' actual non-null");
            }
            else if (dtoAddr != null && reqAddr == null)
            {
                diffs.Add("Address: expected non-null actual 'null'");
            }
            else if (dtoAddr != null && reqAddr != null)
            {
                // Compare postal code
                if (!Equals(reqAddr.PostalCode, dtoAddr.PostalCode))
                    diffs.Add($"Address.PostalCode: expected '{dtoAddr.PostalCode}' actual '{reqAddr.PostalCode}'");

                // Compare region
                if (!Equals(reqAddr.Region, dtoAddr.Region))
                    diffs.Add($"Address.Region: expected '{dtoAddr.Region}' actual '{reqAddr.Region}'");

                // Compare city
                if (!Equals(reqAddr.City, dtoAddr.City))
                    diffs.Add($"Address.City: expected '{dtoAddr.City}' actual '{reqAddr.City}'");

                // Compare street
                if (!Equals(reqAddr.Street, dtoAddr.Street))
                    diffs.Add($"Address.Street: expected '{dtoAddr.Street}' actual '{reqAddr.Street}'");

                // Compare house
                if (!Equals(reqAddr.House, dtoAddr.House))
                    diffs.Add($"Address.House: expected '{dtoAddr.House}' actual '{reqAddr.House}'");

                // Compare building (nullable)
                if (!Equals(reqAddr.Building, dtoAddr.Building))
                    diffs.Add($"Address.Building: expected '{dtoAddr.Building}' actual '{reqAddr.Building}'");

                // Compare apartment (nullable)
                if (!Equals(reqAddr.Apartment, dtoAddr.Apartment))
                    diffs.Add($"Address.Apartment: expected '{dtoAddr.Apartment}' actual '{reqAddr.Apartment}'");
            }


            if (diffs.Count == 0)
                return UnitResult.Success<string>();

            var info = string.Join("; ", diffs);
            return UnitResult.Failure<string>(info);
        }
        
        private UnitResult<string> AssertSimilarRequests(IndividualRequest req1,IndividualRequest req2)
        {
            if(req1 is null) throw new XunitException(nameof(req1) + " is null");
            if(req2 is null) throw new XunitException(nameof(req2) + " is null");

            var diffs = new List<string>();

            if (req1.RequestDateTime != req2.RequestDateTime)
                diffs.Add($"RequestDateTime: expected {req2.RequestDateTime:o} actual {req1.RequestDateTime:o}");

            if (req1.Type != req2.Type)
                diffs.Add($"Type: expected {req2.Type} actual {req1.Type}");

            if (!Equals(req1.RequestDetails, req2.RequestDetails))
                diffs.Add($"RequestDetails: expected '{req2.RequestDetails}' actual '{req1.RequestDetails}'");

            if (!Equals(req1.Address, req2.Address))
                diffs.Add($"Address: expected '{req2.Address}' actual '{req1.Address}'");

            if (!Equals(req1.Client, req2.Client))
            {
                string expectedClient = req2.Client == null ? "null" : req2.Client.Id.ToString();
                string actualClient = req1.Client == null ? "null" : req1.Client.Id.ToString();
                diffs.Add($"Client.Id: expected {expectedClient} actual {actualClient}");
            }

            if (diffs.Count == 0)
                return UnitResult.Success<string>();

            var info = string.Join("; ", diffs);
            return UnitResult.Failure<string>(info);
        }
    }
    
}
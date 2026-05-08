using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnergyManagement.Server.Data
{
        public class ServerExceptionDto
        {
            [JsonPropertyName("message")]
            public string Message { get; set; }
            [JsonPropertyName("stackTrace")]
            public string StackTrace { get; set; }

            public ServerExceptionDto(string message, string? stackTrace)
            {
                Message = message;
                StackTrace = stackTrace ?? "no stack trace available";
            }
            
            public static ServerExceptionDto FromException(Exception ex)
            {
                return new ServerExceptionDto(ex.Message, ex.StackTrace);
            }
        }    public class RegisterClientDto
    {
        
        [JsonPropertyName("email")]
        public string Email {get;set;}
        [JsonPropertyName("password")]
        public string Password {get;set;}
        [JsonPropertyName("passwordConfirmation")]
        public string PasswordConfirmation {get;set;}
        public RegisterClientDto(
            string email,
            string password,
            string passwordConfirmation)
        {
            Email = email;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
        }
    }
    
    public class ProvideIndividualClientsDataDto
    {
        [JsonPropertyName("phoneNumber")]
        [JsonInclude]
        public string PhoneNumber {get;set;}              
        [JsonPropertyName("fullNameDto")]
        [JsonInclude]
        public FullNameDto FullNameDto {get;set;}
        [JsonConstructor]
        public ProvideIndividualClientsDataDto(string phoneNumber,FullNameDto fullNameDto)
        {
            PhoneNumber = phoneNumber;
            FullNameDto = fullNameDto;
        }
    }
    
    public class LoginDto
    {
        [JsonPropertyName("email")]
        public string Email {get;set;}
        [JsonPropertyName("password")]
        public string Password {get;set;}
        public LoginDto(
            string email,
            string password)
        {
            Email = email;
            Password = password;
        }
    }
    
    public class FullNameDto
    {   
        [JsonPropertyName("firstName")]
        [JsonInclude]
        public string FirstName {get;set;}
        [JsonPropertyName("middleName")]
        [JsonInclude]
        public string MiddleName {get;set;}
        [JsonPropertyName("lastName")]
        [JsonInclude]
        public string LastName {get;set;}
        [JsonConstructor]
        public FullNameDto(string firstName,string middleName,string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
        
    }
    public class UserDto
    {
        [JsonPropertyName("id")]
        [JsonInclude]
        public long Id {get;set;}
        [JsonPropertyName("email")]
        [JsonInclude]
        public string Email {get;set;}
        [JsonConstructor]
        public UserDto(long id,string email)
        {
            Id = id;
            Email = email;
        }
    }
    public class CreateIndividualRequestDto 
    {
        [JsonPropertyName("requestDetails")]
        [JsonInclude]
        public string RequestDetails {get;set;}
        [JsonPropertyName("address")]
        [JsonInclude]
        public AddressDto Address {get;set;}
        [JsonConstructor]
        public CreateIndividualRequestDto(string requestDetails,AddressDto address)
        {
            RequestDetails = requestDetails;
            Address = address;
        }
    }
    public class AddressDto
    {
        [JsonPropertyName("postalCode")]
        [JsonInclude]
        public string PostalCode { get; set; }

        /// <summary>Регион проживания.</summary>
        [JsonPropertyName("region")]
        [JsonInclude]
        public string Region { get; set; }

        /// <summary>Город.</summary>
        [JsonPropertyName("city")]
        [JsonInclude]
        public string City { get; set; }

        /// <summary>Улица.</summary>
        [JsonPropertyName("street")]
        [JsonInclude]
        public string Street { get; set; }

        /// <summary>Номер дома.</summary>
        [JsonPropertyName("house")]
        [JsonInclude]
        public string House { get; set; }

        /// <summary>Корпус (если есть).</summary>
        [JsonPropertyName("building")]
        [JsonInclude]
        public string? Building { get; set; }

        /// <summary>Квартира (если есть).</summary>
        [JsonPropertyName("apartment")]
        [JsonInclude]
        public string? Apartment { get; set; }

        [JsonConstructor]
        public AddressDto(
            string postalCode,
            string region,
            string city,
            string street,
            string house,
            string? building,
            string? apartment)
        {
            PostalCode = postalCode;
            Region = region;
            City = city;
            Street = street;
            House = house;
            Building = building;
            Apartment = apartment;
        }
    }
}
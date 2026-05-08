using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Api.Contracts.Auth;
using EnergyManagement.Server.Api.Contracts.Requests;
using EnergyManagement.Server.Data;
using EnergyManagement.Server.Repositories;
using FluentValidation;
using FluentValidation.Results;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Contracts
{
    public class RegisterClientDtoValidator:AbstractValidator<RegisterClientDto>
    {
        public RegisterClientDtoValidator( IClientRepository clientRepository)
        {
            
            RuleFor(dto => dto.Email).MustBeValueObject(
                Email.Create,
                AuthFieldNames.Register.Email);
            
            RuleFor(dto => dto.Email).Custom((value,context)=>
            {
                var isEmailExists = clientRepository.IsClientExist(value);
                if (isEmailExists)
                {
                    context.AddFailure(
                        AuthFieldNames.Register.Email,
                        Errors.Account.EmailIsRegisteredAlready.Code);
                }
            }).When(dto=>Email.Create(dto.Email).IsSuccess);
            
            RuleFor(dto => dto.Password).MustBeValueObject(
                Password.Create,
                AuthFieldNames.Register.Password);
                
            RuleFor(dto => dto.PasswordConfirmation)
                .Custom((value,context)=>
                {
                    if(string.IsNullOrWhiteSpace(value))
                    {
                        context.AddFailure(
                        AuthFieldNames.Register.PasswordConfirmation,
                        Errors.Account.PasswordConfirmationIsRequired.Code);
                    }
                    else if(value!=context.InstanceToValidate.Password)
                    {
                        context.AddFailure(
                        AuthFieldNames.Register.PasswordConfirmation,
                        Errors.Account.PasswordConfirmationDoesntMatch.Code);
                    }
                });
        }
    }
    public class LoginClientDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginClientDtoValidator( IClientRepository clientRepository)
        {
            
            RuleFor(dto => dto.Email).MustBeValueObject(Email.Create,
                AuthFieldNames.Login.Email);
            
            RuleFor(dto => dto.Email).Custom((value,context)=>
            {
                var isEmailExists = clientRepository.IsClientExist(value);
                if (!isEmailExists)
                {
                    context.AddFailure(
                        AuthFieldNames.Login.Email,
                        Errors.Account.EmailWasntRegistered.Code);
                }
            }).When(dto=>Email.Create(dto.Email).IsSuccess);
            
            RuleFor(dto => dto.Password).MustBeValueObject(
                Password.Create,
                AuthFieldNames.Login.Password);
            
        }
    }
    
    public class ProvideIndividualClientsDataDtoValidator:AbstractValidator<ProvideIndividualClientsDataDto>
    {
        public ProvideIndividualClientsDataDtoValidator()
        {
            
            RuleFor(dto => dto.PhoneNumber).MustBeValueObject(
                PhoneNumber.Create,
                AuthFieldNames.ProvideIndividualClientData.Phone);
            
            RuleFor(dto=>dto.FullNameDto)
                .MustBeFullName(AuthFieldNames.ProvideIndividualClientData.FullName);
        }
    }
    public class CreateIndividualRequestDtoValidator:AbstractValidator<CreateIndividualRequestDto>
    {
        private string _mapAddressErrorToName(string errorCode)
        {
                if( errorCode ==Errors.AddressErrors.PostalCodeIsRequired.Code)
                {
                    return RequestFieldNames.CreateIndividualRequest.PostalCode;
                }
                if( errorCode ==Errors.AddressErrors.RegionIsRequired.Code)
                {
                    return RequestFieldNames.CreateIndividualRequest.Region;
                }
                if( errorCode ==Errors.AddressErrors.CityIsRequired.Code)
                {
                    return RequestFieldNames.CreateIndividualRequest.City;
                }
                if( errorCode ==Errors.AddressErrors.StreetIsRequired.Code)
                {
                    return RequestFieldNames.CreateIndividualRequest.Street;
                }
                if( errorCode ==Errors.AddressErrors.HouseIsRequired.Code)
                {
                    return RequestFieldNames.CreateIndividualRequest.House;
                }
                return RequestFieldNames.CreateIndividualRequest.Address;
            
        }
        public CreateIndividualRequestDtoValidator()
        {
            
            RuleFor(dto=>dto.RequestDetails)
                .NotEmpty()
                .WithMessage(Errors.ClientRequestErrors.ClientRequestTextIsRequired.Code)
                .MaximumLength(1000)
                .WithMessage(Errors.ClientRequestErrors.ClientRequestTextIsTooLong.Code);
            
            RuleFor(dto=>dto.Address)
                .Custom((address,context)=>
                {
                    if(address is null)
                    {
                        context.AddFailure(
                            RequestFieldNames.CreateIndividualRequest.Address,
                            Errors.AddressErrors.AddressIsRequired.Code);
                    }
                    
                    if(address != null)
                    {
                        var createAddress = Address.Create(
                            address.PostalCode,
                            address.Region,
                            address.City,
                            address.Street,
                            address.House,
                            address.Building,
                            address.Apartment);
                        
                        if(createAddress.IsFailure)
                        {
                            foreach(var error in createAddress.Error)
                            {
                                context.AddFailure(
                                    _mapAddressErrorToName(error.Code),
                                    error.Code);
                            }
                            
                        }
                    }
                });
        }
    }
    
       
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
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
                SharedConst.RegisterClientCnsts.EmailFieldName);
            
            RuleFor(dto => dto.Email).Custom((value,context)=>
            {
                var isEmailExists = clientRepository.IsClientExist(value);
                if (isEmailExists)
                {
                    context.AddFailure(
                        SharedConst
                            .RegisterClientCnsts
                            .EmailFieldName,
                        Errors.Account.EmailIsRegisteredAlready.Code);
                }
            }).When(dto=>Email.Create(dto.Email).IsSuccess);
            
            RuleFor(dto => dto.Password).MustBeValueObject(
                Password.Create,
                SharedConst.RegisterClientCnsts.PasswordFieldName);
                
            RuleFor(dto => dto.PasswordConfirmation)
                .Custom((value,context)=>
                {
                    if(string.IsNullOrWhiteSpace(value))
                    {
                        context.AddFailure(
                        SharedConst
                            .RegisterClientCnsts
                            .PasswordConfirmationFieldName,
                        Errors.Account.PasswordConfirmationIsRequired.Code);
                    }
                    else if(value!=context.InstanceToValidate.Password)
                    {
                        context.AddFailure(
                        SharedConst
                            .RegisterClientCnsts
                            .PasswordConfirmationFieldName,
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
                SharedConst.LoginConstants.EmailFieldName);
            
            RuleFor(dto => dto.Email).Custom((value,context)=>
            {
                var isEmailExists = clientRepository.IsClientExist(value);
                if (!isEmailExists)
                {
                    context.AddFailure(
                        SharedConst
                            .LoginConstants
                            .EmailFieldName,
                        Errors.Account.EmailWasntRegistered.Code);
                }
            }).When(dto=>Email.Create(dto.Email).IsSuccess);
            
            RuleFor(dto => dto.Password).MustBeValueObject(
                Password.Create,
                SharedConst.LoginConstants.PasswordFieldName);
            
        }
    }
    
    public class ProvideIndividualClientsDataDtoValidator:AbstractValidator<ProvideIndividualClientsDataDto>
    {
        public ProvideIndividualClientsDataDtoValidator()
        {
            
            RuleFor(dto => dto.PhoneNumber).MustBeValueObject(
                PhoneNumber.Create,
                SharedConst.ProvideIndividualClientsData.PhoneFieldName);
            
            RuleFor(dto=>dto.FullNameDto)
                .MustBeFullName(SharedConst.ProvideIndividualClientsData.FullNameFieldName);
        }
    }
    public class CreateIndividualRequestDtoValidator:AbstractValidator<CreateIndividualRequestDto>
    {
        private string _mapAddressErrorToName(string errorCode)
        {
                if( errorCode ==Errors.AddressErrors.PostalCodeIsRequired.Code)
                {
                    return SharedConst.IndividualRequestCnsts.PostalCodeFieldName;
                }
                if( errorCode ==Errors.AddressErrors.RegionIsRequired.Code)
                {
                    return SharedConst.IndividualRequestCnsts.RegionFieldName;
                }
                if( errorCode ==Errors.AddressErrors.CityIsRequired.Code)
                {
                    return SharedConst.IndividualRequestCnsts.CityFieldName;
                }
                if( errorCode ==Errors.AddressErrors.StreetIsRequired.Code)
                {
                    return SharedConst.IndividualRequestCnsts.StreetFieldName;
                }
                if( errorCode ==Errors.AddressErrors.HouseIsRequired.Code)
                {
                    return SharedConst.IndividualRequestCnsts.HouseFieldName;
                }
                return SharedConst.IndividualRequestCnsts.AddressFieldName;
            
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
                            SharedConst.IndividualRequestCnsts.AddressFieldName,
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
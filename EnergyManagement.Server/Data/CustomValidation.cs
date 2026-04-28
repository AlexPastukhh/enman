using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Data;
using FluentValidation;


namespace EnergyManagement.Server.Contracts
{
    public static class CustomValidation
    {
        public static IRuleBuilder<TDto,string>
            MustBeValueObject<TDto,TValueObject>(
            this IRuleBuilder<TDto,string>builder,
            Func<string,Result<TValueObject,IReadOnlyList<Error>>>factory,string mapErrorToName)
            where TDto:class
             
        {
            return builder.Custom((str, context) =>
            {
                var result = factory.Invoke(str);
                if (result.IsFailure)
                {
                    foreach (var error in result.Error)
                    {
                        context.AddFailure(mapErrorToName,error.Code);
                    } 
                }
            });
        }
        
        public static IRuleBuilder<TDto, string> 
    MustBePassword<TDto>(this IRuleBuilder<TDto, string> builder,string mapErrorToName)
    where TDto:class
        { 
            return builder.Custom((str, context) =>
            {
                var result = Password.Create(str);
                if (result.IsFailure)
                {
                     
                    foreach (var error in result.Error)
                    {
                        context.AddFailure(mapErrorToName,error.Code);
                    }
                }
            });
        }
        
        
        public static IRuleBuilder<TDto, FullNameDto> 
        MustBeFullName<TDto>(this IRuleBuilder<TDto, FullNameDto> builder,string mapErrorToName)
        where TDto : class
        {
            return builder.Custom((dto, context) =>
            {
                var result = FullName.Create(dto.FirstName,dto.MiddleName,dto.LastName);
                if (result.IsFailure)
                {
                    
                    foreach (var error in result.Error)
                    {
                        context.AddFailure(mapErrorToName,error.Code);
                    }
                }

            });
        }
        
        
        
        
        
    }
}
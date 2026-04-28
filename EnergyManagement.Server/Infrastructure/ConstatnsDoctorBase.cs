using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error.Errors;

namespace EnergyManagement.Server.Infrastructure
{
    public abstract class ConstatnsDoctorBase
    {
        protected UnitResult<IReadOnlyList<Error>> ValidateConstantsHierarchy<TConfig>(TConfig _config)
        where TConfig : class
    {
        try
        {
            var errorList = new List<Error>();
        
            var validateRoot=ValidateRoot<TConfig>(_config);
            if(validateRoot.IsFailure)
            {
                errorList.Add(validateRoot.Error);
                return UnitResult.Failure<IReadOnlyList<Error>>(errorList);
            }
            
            var result = ValidateAllProperties<TConfig>(_config);
            if(result.IsFailure)
            {
                errorList.AddRange(result.Error);
            }
            
            if(errorList.Any())
            {
                return UnitResult.Failure<IReadOnlyList<Error>>(errorList);
            }
            return UnitResult.Success<IReadOnlyList<Error>>();
        }
        catch (System.Exception)
        {
            
            throw;
        }
        

        
    }
    

    
    // ==================== INDIVIDUAL VALIDATION METHODS ====================
    private UnitResult<Error> ValidateRoot<TConfig>(TConfig _config)
    {
        return _config == null
            ? UnitResult.Failure<Error>(
                ConstantsErrors.RootNotFound)
            : UnitResult.Success<Error>();
    }
        protected UnitResult<IReadOnlyList<Error>> ValidateAllProperties<TClass>(TClass instance)
        where TClass : class
        {
            try
            {
                
                var type = typeof(TClass);
                
                if(type == typeof(string))
                {
                    throw new InvalidOperationException(
                        "Type string is not supported for constants validation.");
                }
                
                var errors = new List<Error>();
                if(type.IsAbstract || type.IsInterface)
                {
                    throw new InvalidOperationException(
                        $"Type {type.FullName} must be a concrete class to validate its properties.");
                }
                
                var properties = type.GetProperties();
                
                foreach(var prop in properties)
                {
                    var value = prop.GetValue(instance);
                    if(value is null)
                    {
                        errors.Add(ConstantsErrors.ConstantNotFound($"{type.Name}.{prop.Name}"));
                    }else if(!value.GetType().IsClass || value.GetType() == typeof(string) || value.GetType().IsPrimitive)
                    {
                        continue;
                    }else
                    {
                        var method = this.GetType()
                        .GetMethod(nameof(ValidateAllProperties), BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.MakeGenericMethod(prop.PropertyType);
                    
                        var result =(UnitResult<IReadOnlyList<Error>>)method!.Invoke(this, new object?[] { value})!;
                        if(result.IsFailure)
                        {
                            errors.AddRange(result.Error);
                        }
                    }
                    
                    
                    
                }
                
                if(errors.Any())
                {
                    return UnitResult.Failure<IReadOnlyList<Error>>(errors);
                }
                
                return UnitResult.Success<IReadOnlyList<Error>>();
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
        }
    }
}
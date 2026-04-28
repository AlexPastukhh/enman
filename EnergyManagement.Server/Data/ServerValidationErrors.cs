using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using FluentValidation.Results;
using EnergyManagement.Server.Data;

namespace EnergyManagement.Server.Data
{
    
    
    public class ServerValidationError:ValueObject
    {
        public string FieldName{get;set;}
        public string ErrorCode{get;set;}
        [JsonConstructor]
        private ServerValidationError(string fieldName, string errorCode)
        {
            FieldName = fieldName;
            ErrorCode = errorCode;
        }
        private ServerValidationError(){}
        public static ServerValidationError Create(string fieldName, string errorCode, string path)
            => new ServerValidationError(fieldName,
                string.IsNullOrWhiteSpace(path) ? errorCode : $"{path}.{errorCode}");

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return FieldName;
            yield return ErrorCode;
        }
    }
    
}
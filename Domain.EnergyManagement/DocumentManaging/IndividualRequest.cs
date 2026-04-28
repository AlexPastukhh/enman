using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;
namespace Domain.EnergyManagement.DocumentManaging
{
    /// <summary>
    /// Запрос, исходящий от физического лица. Наследует общую логику запроса из <see cref="ClientRequest"/>.
    /// Содержит ссылку на клиента и адрес, на который направлен запрос.
    /// </summary>
    /// <remarks>
    /// Типичный жизненный цикл запроса показан на диаграмме:
    /// ![Request timeline](../docs/images/request_timeline.svg)
    /// </remarks>
    public class IndividualRequest:ClientRequest
    {
        /// <summary>Клиент, создавший запрос.</summary>
        public IndividualClient Client { get; }
        /// <summary>Адрес, связанный с запросом.</summary>
        public Address Address { get; }
        
        private IndividualRequest(
            IndividualClient client,
            Address address, 
            DateTimeOffset requestDate, 
            RequestType requestType, 
            string requestDetails):base(requestDetails,requestDate,requestType)
        {
            Client = client;
            Address = address;
        }
        
        private IndividualRequest():base()
        {
        }
        
        /// <summary>
        /// Внутренний фабричный метод для создания запроса с валидацией входных данных.
        /// Возвращает список ошибок при некорректных значениях.
        /// </summary>
        internal static Result<IndividualRequest, IReadOnlyList<Error>> Create(
            IndividualClient client,
            Address address,
            DateTimeOffset requestDate,
            RequestType requestType,
            string requestDetails)
        {
            Guard.IsNotNull(client);
            Guard.IsNotNull(requestDate);
            Guard.IsNotNull(requestType);
            
            var errors = new List<Error>();
            
            Guard.IsFalse(requestDate > DateTimeOffset.Now, nameof(requestDate), Errors.General.DateTimeIsInFuture.Code);
            
            if(string.IsNullOrWhiteSpace(requestDetails))
            {
                errors.Add(Errors.ClientRequestErrors.ClientRequestTextIsRequired);
            }
            
            if(requestDetails.Length > 3000)
            {
                errors.Add(Errors.ClientRequestErrors.ClientRequestTextIsTooLong);
            }
            
            if (errors.Any())
            {
                return Result.Failure<IndividualRequest, IReadOnlyList<Error>>(errors);
            }
            
            return Result.Success<IndividualRequest, IReadOnlyList<Error>>(new IndividualRequest(client, address,requestDate, requestType, requestDetails));
        }
    }

    public enum RequestType
    {
        Connection
        
    }
}
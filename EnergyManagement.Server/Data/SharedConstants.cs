using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using static EnergyManagement.Server.Data.SharedConst;
using EnergyManagement.Server.Data;
using System.Reflection;

namespace EnergyManagement.Server.Data
{
    public class SharedConst
    {

        public static class GeneralConstants
        {
            public static int ValidationErrorStatusCode => 422;
            public static string ErrorsCollectionName => "errors";
            public static string ExceptionExtensionName => "exception";
        }
        public class AppRoutes
        {
            public const string AuthControllerRoute = "api/auth";
            public const string RegisterIndividual = "registerIndividual";
            public const string RegisterIndividualPath = AuthControllerRoute + "/" + RegisterIndividual;
            public const string ProvideIndividualClientsData = "provideIndividualClientsData";
            public const string ProvideIndividualClientsDataPath = AuthControllerRoute + "/" + ProvideIndividualClientsData;
            public const string Login = "login";
            public const string LoginPath = AuthControllerRoute + "/" + Login;
            public const string GetUser = "getUser";
            public const string GetUserPath = AuthControllerRoute + "/" + GetUser;

            public const string ClientRequestControllerRoute = "api/ClientRequest";
            public const string IndivCreateConnectionRequest = "IndivCreateConnectionRequest";
            public const string IndivCreateConnectionRequestPath = ClientRequestControllerRoute + "/" + IndivCreateConnectionRequest;
        }

        public class RegisterClientCnsts
        {
           

            public static string EmailFieldName => JsonField.Of<RegisterClientDto>(regCl=>regCl.Email);
            public static string PasswordFieldName => JsonField.Of<RegisterClientDto>(regCl=>regCl.Password);
            public static string PasswordConfirmationFieldName => JsonField.Of<RegisterClientDto>(regCl=>regCl.PasswordConfirmation);
        }

        public class LoginConstants
        {
            private static readonly Serilog.ILogger _logger = Log.ForContext<LoginConstants>();

            static LoginConstants()
            {
            }

            public static string EmailFieldName => JsonField.Of<LoginDto>(x => x.Email);
            public static string PasswordFieldName => JsonField.Of<LoginDto>(x => x.Password);
        }

        public class ProvideIndividualClientsData
        {
            private static readonly Serilog.ILogger _logger = Log.ForContext<ProvideIndividualClientsData>();

            static ProvideIndividualClientsData()
            {
                _logger.Information("ProvideIndividualClientsDataCnsts static constructor starting");
            }

            public static string PhoneFieldName => JsonField.Of<ProvideIndividualClientsDataDto>(x => x.PhoneNumber);
            public static string FullNameFieldName => JsonField.Of<ProvideIndividualClientsDataDto>(x => x.FullNameDto);
            
        }

        public class IndividualRequestCnsts
        {
            
            public static string RequestDetailsFieldName => JsonField.Of<CreateIndividualRequestDto>(x => x.RequestDetails);
            public static string AddressFieldName => JsonField.Of<CreateIndividualRequestDto>(x => x.Address);
            public static string PostalCodeFieldName => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.PostalCode);
            public static string RegionFieldName => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.Region);
            public static string CityFieldName => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.City);
            public static string StreetFieldName => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.Street);
            public static string HouseFieldName => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.House);
            public static string ApartmentFieldName => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.Apartment);
            public static string BuildingFieldName => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.Building);
        }
    }

    

    

    // public class ConstantsHelper
    // {
    //     public static string GetPropertyPath<T, TProperty>(
    //         Expression<Func<T, TProperty>> expression)
    //     {
    //         var memberExpression = expression.Body as MemberExpression;
    //         if (memberExpression == null)
    //             throw new ArgumentException("Expression must be a property access");

    //         var path = new List<string>();
    //         while (memberExpression != null)
    //         {
    //             path.Add(memberExpression.Member.Name);
    //             memberExpression = memberExpression.Expression as MemberExpression;
    //         }

    //         path.Reverse();
    //         return string.Join(".", path);
    //     }
    // }
}
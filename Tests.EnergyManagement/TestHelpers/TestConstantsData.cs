// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using CSharpFunctionalExtensions;
// using Domain.EnergyManagement.Common;
// using EnergyManagement.Server.Data;
// using EnergyManagement.Server.Infrastructure;
// using Microsoft.Extensions.Diagnostics.HealthChecks;
// using static Domain.EnergyManagement.Common.Error.Errors;

// namespace Tests.EnergyManagement.TestHelpers
// {
//     public class FakeDoctor : ConstatnsDoctorBase
//     {
//         private static TestConstantsConfig _config= FakeSharedService.ValidTestConfig;
//         public FakeDoctor()
//         {
            
//         }
//         public Task<HealthCheckResult> CheckHealthAsync(
//         HealthCheckContext context, 
//         CancellationToken cancellationToken = default)
//         {
//             try
//             {
//                 var validate = ValidateConstantsHierarchy<TestConstantsConfig>(_config);
//                 if(validate.IsFailure)
//                 {
//                     return Task.FromResult(HealthCheckResult.Unhealthy(validate.Error.ToString()));
//                 }
//                 return Task.FromResult(HealthCheckResult.Healthy()); 
//             }catch(Exception ex)
//             {
//                 return Task.FromResult(
//                     HealthCheckResult.Unhealthy($"Exception during constants validation: {ex.Message}"));
//             }
               
//         }
        
//         public Task<HealthCheckResult> CheckInvalidHealthAsync(
//         HealthCheckContext context,
//         TestConstantsConfig invalidConfig, 
//         CancellationToken cancellationToken = default)
//         {
//             try
//             {
                
//                 var validate = ValidateConstantsHierarchy<TestConstantsConfig>(invalidConfig);
//                 if(validate.IsFailure)
//                 {
//                     return Task.FromResult(HealthCheckResult.Unhealthy(validate.Error.ToString()));
//                 }
//                 else
//                 {
                    
//                     return Task.FromResult(HealthCheckResult.Healthy());
//                 }
//             }catch(Exception ex){
//                 return Task.FromResult(
//                     HealthCheckResult.Unhealthy($"Exception during constants validation: {ex.Message}"));
//             }
            
                
               
//         }
        
    
//     }
//     public class FakeSharedService : SharedFileServiceBase
//     {
//         private static TestConstantsConfig _config;
//         public static TestConstantsConfig ValidTestConfig => _config;
//         private static TestConstantsConfig[] _invalidConfigs;
//         public static TestConstantsConfig[] InvalidTestConfigs => _invalidConfigs;
        
//         private const string _validPath = "C:\\Users\\Пользователь\\source\\repos\\EnergyManagement\\Shared\\testconstants.json";
//         static FakeSharedService()
//         {
//             try
//             {
//                 _config = LoadTestConstants();
//                 _invalidConfigs = new TestConstantsConfig[]
//                 {
//                     LoadConstants<TestConstantsConfig>("C:\\Users\\Пользователь\\source\\repos\\EnergyManagement\\Shared\\invalidtestconstants\\noEmailInRegister.json"),
//                     LoadConstants<TestConstantsConfig>("C:\\Users\\Пользователь\\source\\repos\\EnergyManagement\\Shared\\invalidtestconstants\\NoGeneral.json")
//                 };
//             }catch(Exception)
//             {
                
//             }
            
//         }
//         public static TestConstantsConfig LoadTestConstants()
//         {
//             return LoadConstants<TestConstantsConfig>(_validPath);
//         }
//     }
//     public class TestConstantsConfig
//     {
//         public AuthConstantsConfig AuthConstants { get; set; }
//         public GeneralConstantsConfig GeneralConstants { get; set; }
        
        
//     }
    
//     public class GeneralConstantsConfig
//     {
//         public int ValidationErrorStatusCode { get; set; }
//         public string ErrorsCollectionName { get; set; }
//         public string ExceptionExtensionName { get; set; }
//     }
//     public class ExceptionExtensionConfig
//     {
//         public string ExceptionExtensionName { get; set; }
//         public string Message { get; set; }
//         public string StackTrace { get; set; }
//     }

//     public class AuthConstantsConfig
//     {
//         // JSON has "registerIndividualClient" not "RegisterIndividual"
//         public RegisterIndividualConstantsConfig RegisterIndividualClient { get; set; }
//         public ProvideIndClientsDataConfig ProvideIndividualClientsData {get;set;}

        
//     }

//     public class RegisterIndividualConstantsConfig  // ← Changed name
//     { 
//         public FormFieldWithDto Email { get; set; }      // ← Need different class
//         public FormFieldWithDto Password { get; set; }
//         public FormFieldWithDto PasswordConfirmation { get; set; }
//     }

//     public class FormFieldWithDto  // ← New class with extra property
//     {
//         public string FieldName { get; set; }
//         public string DtoFieldName { get; set; }  // ← Added this
//         public int CharactersLimit { get; set; }
//     }
//     public class ProvideIndClientsDataConfig
//     {
//         public FormFieldWithDto PhoneNumber { get; set; }      // ← Need different class
//         public FormFieldWithDto FirstName { get; set; }
//         public FormFieldWithDto MiddleName { get; set; }
//         public FormFieldWithDto LastName { get; set; }
//     }
// }
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using EnergyManagement.Server.Data;

// namespace EnergyManagement.Server.Data
// {
//     public class ConstantsToWrite
//     {
//         public AuthConstantsConfig AuthConstants { get; set; }
//         public GeneralConstantsConfig GeneralConstants { get; set; }
//         public RoutesConfig Routes { get; set; }
//         private ConstantsToWrite(
//             AuthConstantsConfig authConstants,
//             GeneralConstantsConfig generalConstants,
//             RoutesConfig routes)
//         {
//             AuthConstants = authConstants;
//             GeneralConstants = generalConstants;
//             Routes = routes;
//         }
//         public static ConstantsToWrite Create()
//         {
//             return new ConstantsToWrite(
//                 AuthConstantsConfig.Create(),
//                 GeneralConstantsConfig.Create(),
//                 RoutesConfig.Create());
//         }
        
        
//     }
//     public class RoutesConfig
//     {
//         public string RegisterIndividualPath { get; set; } 
//         public string ProvideIndividualClientsDataPath { get; set; } 
//         public string LoginPath { get; set; }
//         public string GetUserPath { get; set; }
//         public RoutesConfig(string registerIndividualPath, string provideIndividualClientsDataPath, string loginPath, string getUserPath)
//         {
//             RegisterIndividualPath = registerIndividualPath;
//             ProvideIndividualClientsDataPath = provideIndividualClientsDataPath;
//             LoginPath = loginPath;
//             GetUserPath = getUserPath;
//         }
//         public static RoutesConfig Create()
//         {
//             return new RoutesConfig(
//                 AuthRoutes.RegisterIndividualPath,
//                 AuthRoutes.ProvideIndividualClientsDataPath,
//                 AuthRoutes.LoginPath,
//                 AuthRoutes.GetUserPath);
//         }
//     }
    
//     public class GeneralConstantsConfig
//     {
//         public  int ValidationErrorStatusCode { get; set; }
//         public string ErrorsCollectionName { get; set; }
//         public string ExceptionExtensionName { get; set; }
//         public GeneralConstantsConfig()
//         {
        
//         }
        
//         public static GeneralConstantsConfig Create()
//         {
//             return new GeneralConstantsConfig{
//                 ValidationErrorStatusCode=ProblemDetailsContract.ValidationStatusCode,
//                 ErrorsCollectionName=ProblemDetailsContract.ErrorsExtension,
//                 ExceptionExtensionName=ProblemDetailsContract.ExceptionExtension
//             };
//         }
//     }
    

//     public class AuthConstantsConfig
//     {
//         // JSON has "registerIndividualClient" not "RegisterIndividual"
//         public RegisterIndividualConstantsConfig RegisterIndividualClient { get; set; }
//         public LoginConstantsConfig Login { get; set; }
//         public ProvideIndClientsDataConfig ProvideIndividualClientsData {get;set;}
//         private AuthConstantsConfig(
//             RegisterIndividualConstantsConfig registerIndividualClient,
//             LoginConstantsConfig login,
//             ProvideIndClientsDataConfig provideIndividualClientsData)
//         {
//             RegisterIndividualClient = registerIndividualClient;
//             Login = login;
//             ProvideIndividualClientsData = provideIndividualClientsData;
//         }
//         public static AuthConstantsConfig Create()
//         {
//             return new AuthConstantsConfig(
//                 RegisterIndividualConstantsConfig.Create(),
//                 LoginConstantsConfig.Create(),
//                 ProvideIndClientsDataConfig.Create());
//         }

        
//     }

//     public class LoginConstantsConfig
//     {
//         public FormFieldWithDto Email { get; set; }      // ← Need different class
//         public FormFieldWithDto Password { get; set; }
//         private LoginConstantsConfig(
//             FormFieldWithDto email,
//             FormFieldWithDto password)
//         {
//             Email = email;
//             Password = password;
//         }
//         public static LoginConstantsConfig Create()
//         {
//             var dto = new LoginDto("","");
//             return new LoginConstantsConfig(
//                 FormFieldWithDto.Create(
//                     AuthFieldNames.Login.Email,
//                     nameof(dto.Email)),
//                 FormFieldWithDto.Create(
//                     AuthFieldNames.Login.Password,
//                     nameof(dto.Password)));
//         }
//     }

//     public class RegisterIndividualConstantsConfig  // ← Changed name
//     { 
//         public FormFieldWithDto Email { get; set; }      // ← Need different class
//         public FormFieldWithDto Password { get; set; }
//         public FormFieldWithDto PasswordConfirmation { get; set; }
//         private RegisterIndividualConstantsConfig(
//             FormFieldWithDto email,
//             FormFieldWithDto password,
//             FormFieldWithDto passwordConfirmation)
//         {
//             Email = email;
//             Password = password;
//             PasswordConfirmation = passwordConfirmation;
//         }
//         public static RegisterIndividualConstantsConfig Create()
//         {
//             var registerDto =new RegisterClientDto("","","");
            
//             return new RegisterIndividualConstantsConfig(
//                 FormFieldWithDto.Create(
//                     AuthFieldNames.Register.Email,
//                     nameof(registerDto.Email)),
//                 FormFieldWithDto.Create(
//                     AuthFieldNames.Register.Password,
//                     nameof(registerDto.Password)),
//                 FormFieldWithDto.Create(
//                     AuthFieldNames.Register.PasswordConfirmation,
//                     nameof(registerDto.PasswordConfirmation)));
//         }
//     }

//     public class FormFieldWithDto  // ← New class with extra property
//     {
//         public string FieldName { get; set; }
//         public string DtoFieldName { get; set; }
//         private FormFieldWithDto(string fieldName, string dtoFieldName)
//         {
//             FieldName = fieldName;
//             DtoFieldName = dtoFieldName;
//         }
//         public static FormFieldWithDto Create(string fieldName, string dtoFieldName)
//         {
//             return new FormFieldWithDto(fieldName, dtoFieldName);
//         }
//     }
//     public class ProvideIndClientsDataConfig
//     {
//         public FormFieldWithDto PhoneNumber { get; set; }      // ← Need different class
//         public FormFieldWithDto FirstName { get; set; }
//         public FormFieldWithDto MiddleName { get; set; }
//         public FormFieldWithDto LastName { get; set; }
//         private ProvideIndClientsDataConfig(
//             FormFieldWithDto phoneNumber,
//             FormFieldWithDto firstName,
//             FormFieldWithDto middleName,
//             FormFieldWithDto lastName)
//         {
//             PhoneNumber = phoneNumber;
//             FirstName = firstName;
//             MiddleName = middleName;
//             LastName = lastName;
//         }
//         // public static ProvideIndClientsDataConfig Create()
//         // {
//         //     var dto = new ProvideIndividualClientsDataDto("",new FullNameDto("","",""));
            
//         //     // return new ProvideIndClientsDataConfig(
//         //     //     FormFieldWithDto.Create(
//         //     //         AuthFieldNames.ProvideIndividualClientData.Phone,
//         //     //         nameof(dto.PhoneNumber)),
//         //     //     FormFieldWithDto.Create(
//         //     //         AuthFieldNames.ProvideIndividualClientData.FullName,
//         //     //         nameof(dto.FullNameDto.FirstName)),
//         //     //     FormFieldWithDto.Create(
//         //     //         AuthFieldNames.ProvideIndividualClientData.FullName,
//         //     //         nameof(dto.FullNameDto.MiddleName)),
//         //     //     FormFieldWithDto.Create(
//         //     //         AuthFieldNames.ProvideIndividualClientData.FullName,
//         //     //         nameof(dto.FullNameDto.LastName)));
//         // }
//     }
// }
